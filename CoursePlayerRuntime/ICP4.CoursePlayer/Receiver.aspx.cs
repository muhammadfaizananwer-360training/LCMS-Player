/*

Copyright (C) DocuSign, Inc.  All rights reserved.

This source code is intended only as a supplement to DocuSign SDK 
and/or on-line documentation.

This sample is designed to demonstrate DocuSign features and is not intended 
for production use. Code and policy for a production application must be 
developed to meet the specific data and security requirements of the 
application.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICP4.CoursePlayer.net.docusign.test;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Configuration;
using ICP4.BusinessLogic.CourseManager;
using LCMS.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using ICP4.BusinessLogic.CacheManager;
using ICP4.BusinessLogic.IntegerationManager;
using _360Training.BusinessEntities;
using ICP4.CommunicationLogic.CommunicationCommand;
using ICP4.BusinessLogic.ICPCourseService;
using ICP4.BusinessLogic.ICPTrackingService;
using ICP4.BusinessLogic.DocuSignManager;

namespace ICP4.CoursePlayer
{
    public partial class Receiver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int Enrollment_ID1 = 223163;

            //MarkCourseAsCompleted(Enrollment_ID1);

            //DocuSignDecline("4d751961-aaa1-45a0-ac5a-b2172513a839");
            
            if (Request.ContentType.ToLower().Contains("xml") == true)
            {
                StreamReader sr = new StreamReader(Request.InputStream);
                string xml = sr.ReadToEnd();

                XmlReader reader = new XmlTextReader(new StringReader(xml));
                XmlSerializer serializer = new XmlSerializer(typeof(DocuSignEnvelopeInformation), "http://www.docusign.net/API/3.0");
                DocuSignEnvelopeInformation envelopeInfo = serializer.Deserialize(reader) as DocuSignEnvelopeInformation;


                //Write Log
                //************************************** 
                Logger.Write("Envelop Status :" + envelopeInfo.EnvelopeStatus.Status, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                //**************************************


                //Write Log
                //**************************************               
                Logger.Write("Envelop ID :" + envelopeInfo.EnvelopeStatus.EnvelopeID, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                //**************************************            

                EnvelopeStatus(envelopeInfo);

            }    
       
        }


         public void EnvelopeStatus(DocuSignEnvelopeInformation envelopeInfo)
        {
           
           
            if (envelopeInfo.EnvelopeStatus.Status == EnvelopeStatusCode.Completed)
            {
                DocuSignSignComplete(envelopeInfo);   
            }

            else if (envelopeInfo.EnvelopeStatus.Status == EnvelopeStatusCode.Declined)
            {

                DocuSignDecline(envelopeInfo.EnvelopeStatus.EnvelopeID);
            }

            else
            {
                SaveEnvelopStatusWithRespectToRoleForProvider(envelopeInfo,0);
            
            }
           
        }

         #region DocuSignDecline

         public void DocuSignDecline(string EnvelopeID)
         {
             int Enrollment_ID;
             using (CourseService courseService = new CourseService())
             {
                 courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                 Enrollment_ID = courseService.GetEnrollmentIdAgainstEnvelopeId(EnvelopeID);
                 courseService.SaveStatusAfterDocuSignDecline(Enrollment_ID);
             }                      
             

           ICP4.BusinessLogic.ICPTrackingService.LearnerProfile serviceLearnerProfile;        
        
             using (TrackingService trackingService = new TrackingService())
             {
                 trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                 serviceLearnerProfile = trackingService.GetUserProfileInformation(Enrollment_ID);
             }
             string mailBody = CreateBody(serviceLearnerProfile);
             SendEmail(ConfigurationManager.AppSettings["ToEmailDocuSignDecline"], ConfigurationManager.AppSettings["FromEmailDocuSignDecline"], ConfigurationManager.AppSettings["SubjectEmailDocuSignDecline"], mailBody);

             MarkCourseAsCompleted(Enrollment_ID);

         }

         public string CreateBody(ICP4.BusinessLogic.ICPTrackingService.LearnerProfile profile)
         {

             string mailBody = @"<table><tr><td>Affidavit has been declined for the student listed below:</td></tr>";
             mailBody = mailBody + @"<tr><td><b>Full Name: </b>" + profile.FullName + "</td></tr>";
             mailBody = mailBody + @"<tr><td><b>Address: </b>" + profile.Address1 + "</td></tr>";
             mailBody = mailBody + @"<tr> <td><b>Email Address: </b>" + profile.EmailAddress + "</td></tr>";
             mailBody = mailBody + @" <tr><td><b>Phone Number: </b>" + profile.OfficePhone + "</td></tr>";
             mailBody = mailBody + @"<tr><td><b>UserName: </b>" + profile.Username + "</td></tr>";
             mailBody = mailBody + @"<tr><td><b>Course Name: </b>" + profile.CourseName + "</td></tr>";
             mailBody = mailBody + @"<tr><td><b>Business Key: </b>" + profile.BusinessKey + "</td></tr>";
             mailBody = mailBody + @"</table>";
             return mailBody;

         }

         public bool SendEmail(string ToEmail, string FromEmail, string Subject, string MailBody)
         {

             try
             {
                 System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                 smtpClient.UseDefaultCredentials = false;
                 smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUser"], ConfigurationManager.AppSettings["SMTPPassword"]);
                 smtpClient.Host = ConfigurationManager.AppSettings["SMTPAddress"];
                 System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                 System.Net.Mail.MailAddress fromMailAddress = new System.Net.Mail.MailAddress(FromEmail);//(ConfigurationManager.AppSettings["FromEmailValidationUnlock"]);
                 mailMessage.From = fromMailAddress;
                 mailMessage.IsBodyHtml = true;
                 mailMessage.Subject = Subject;
                 mailMessage.To.Add(ToEmail);
                 mailMessage.Body = MailBody;
                 smtpClient.Send(mailMessage);
                 return true;
             }
             catch (Exception e)
             {
                 return false;
             }

         }


         #endregion        
 
         #region DocuSignSignComplete

         public void DocuSignSignComplete(DocuSignEnvelopeInformation envelopeInfo)
         {

             DocuSignHelper dsh = new DocuSignHelper();
             ICP4.BusinessLogic.DocuSignAPI.APIServiceSoapClient client = dsh.CreateAPIProxy();
             ICP4.BusinessLogic.DocuSignAPI.EnvelopePDF pdf = null;

             // Download all documents as one pdf
             pdf = client.RequestPDF(envelopeInfo.EnvelopeStatus.EnvelopeID);

             int Enrollment_ID;

             using (CourseManager courseManager = new CourseManager())
             {
                 Enrollment_ID = courseManager.SaveStatusAfterDocuSignProcessComplete(envelopeInfo.EnvelopeStatus.EnvelopeID);

             }

             string directoryPath = System.Web.HttpContext.Current.Server.MapPath(VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["signedpdfs"]));

             FileInfo directoryInfo = new FileInfo(directoryPath);
             if (!directoryInfo.Directory.Exists)
             {
                 directoryInfo.Create();
             }

             string fileName = Enrollment_ID + "_DocuSign" + ".pdf";
             string path = directoryPath + fileName;

             System.IO.FileStream _FileStream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
             // Writes a block of bytes to this stream using data from a byte array.
             // _FileStream.Write(envelopeInfo.DocumentPDFs[0].PDFBytes, 0, envelopeInfo.DocumentPDFs[0].PDFBytes.Length);
             _FileStream.Write(pdf.PDFBytes, 0, pdf.PDFBytes.Length);

             // close file stream
             _FileStream.Flush();
             _FileStream.Close();


             //Write Log
             //**************************************            
             Logger.Write("File Name" + fileName, "PlayerDocuSign", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
             //**************************************

             UploadDocuSignDocument(path, fileName);

             MarkCourseAsCompleted(Enrollment_ID);

            SaveEnvelopStatusWithRespectToRoleForApprover(envelopeInfo, Enrollment_ID);

         }
                     
         public bool UploadDocuSignDocument(string path, string fileName)
         {
             try
             {
                 bool fileStatus;

                 string strServerName = System.Configuration.ConfigurationManager.AppSettings["FTPServerPath"];
                 string strUser = System.Configuration.ConfigurationManager.AppSettings["FTPUser"];
                 string strPassword = System.Configuration.ConfigurationManager.AppSettings["FTPPassword"];
                 FTPclientNet ftpClient = new FTPclientNet(strServerName, strUser, strPassword);
                 string dirPath = System.Configuration.ConfigurationManager.AppSettings["DirectoryPath"] + "/" + fileName;

                 fileStatus = ftpClient.Upload(path, dirPath);

                 if (fileStatus == true)
                 {
                     System.IO.File.Delete(path);

                 }

                 return fileStatus;

             }

             catch (Exception ex)
             {

                 ExceptionPolicy.HandleException(ex, "ICPException");
                 return false;

             }


         }

         #endregion
        
         #region MarkCourseCompletionRegion

         public void MarkCourseAsCompleted(int enrollmentID)
         {

             _360Training.BusinessEntities.CourseInfo courseInfo = null;

             CourseManager courseManager = new CourseManager();

             courseInfo = courseManager.GetCourseInformation(enrollmentID);


             using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
             {
                 trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                 IntegerationStatistics integerationStatistics = new IntegerationStatistics();

                 ICP4.BusinessLogic.ICPTrackingService.LearnerCourseCompletionStatus learnerCourseCompletionStatus = (ICP4.BusinessLogic.ICPTrackingService.LearnerCourseCompletionStatus)Session["LearnerCourseCompletionStatus"];

                 DateTime registrationDate = DateTime.Now;

                 int source = trackingService.GetSource(courseInfo.LearnerSessionGuid);

                 System.Web.HttpContext.Current.Session["CourseConfigurationID"] = courseInfo.CourseConfigId;

                 System.Web.HttpContext.Current.Session["Source"] = source;

                 int QuizCount = courseManager.GetValidQuizCount(courseInfo.CourseId);

                 ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = GetCourseConfiguration(courseInfo.CourseConfigId, source);

                 double percentageCourseProgress = 100;

                 int courseApprovalID = 0;
                 if (System.Web.HttpContext.Current.Session["CourseApprovalID"] != null)
                 {
                     courseApprovalID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseApprovalID"]);
                 }

                 learnerCourseCompletionStatus = trackingService.GetCourseCompletionStatus(courseInfo.CourseId, courseManager.CourseCompletionPolicyEntity2BizTranslator(courseConfig), courseInfo.LearnerId, enrollmentID, QuizCount, registrationDate, percentageCourseProgress, courseApprovalID,source);


                 Integeration integeration = IntegerationFactory.GetObject(source);
                 if (integerationStatistics == null)
                     integerationStatistics = new IntegerationStatistics();
                 integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.CourseCompletion;
                 integerationStatistics.PercentageCourseProgress = percentageCourseProgress;
                 integerationStatistics.LearningSessionGuid = courseInfo.LearnerSessionGuid;
                 integerationStatistics.IsCourseCompleted = learnerCourseCompletionStatus.IsCourseCompleted;

                 integeration.SynchStatsToExternalSystem(integerationStatistics);

             }
         }

         private ICP4.BusinessLogic.ICPCourseService.CourseConfiguration GetCourseConfiguration(int CourseConfigurationID, int Source)
         {

             CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
             ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration = new ICP4.BusinessLogic.ICPCourseService.CourseConfiguration();
             ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
             courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
             courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
             int source = Source;
             int courseConfigurationID = CourseConfigurationID;
             courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);

             if (courseConfiguration == null)
             {
                 courseConfiguration = courseService.GetCourseConfiguaration(courseConfigurationID);
                 cacheManager.CreateCourseConfigurationInCache(CourseConfigurationID, courseConfiguration);
             }

             return courseConfiguration;
         }
                  
         #endregion

         #region Update Role Status 
         
         public void SaveEnvelopStatusWithRespectToRoleForProvider(DocuSignEnvelopeInformation envelopeInfo, int enrollmentId)
         {

             if (envelopeInfo.EnvelopeStatus.RecipientStatuses.Count() == 3)
             {
                 if (envelopeInfo.EnvelopeStatus.RecipientStatuses[1].Status == RecipientStatusCode.Completed)
                 {
                     SaveDocuSignRoleStatus(envelopeInfo.EnvelopeStatus.RecipientStatuses[1].UserName, envelopeInfo, enrollmentId);
                 }
             }
         }

         public void SaveEnvelopStatusWithRespectToRoleForApprover(DocuSignEnvelopeInformation envelopeInfo, int enrollmentId)
         {

             if (envelopeInfo.EnvelopeStatus.RecipientStatuses.Count() == 3)
             {
                 if (envelopeInfo.EnvelopeStatus.RecipientStatuses[2].Status == RecipientStatusCode.Completed)
                 {
                     //Some times the provider is not updated so we have added this comment to forcefully update the provider
                     SaveDocuSignRoleStatus(ConfigurationManager.AppSettings["RollNameForProvider"], envelopeInfo, enrollmentId);
                     SaveDocuSignRoleStatus(envelopeInfo.EnvelopeStatus.RecipientStatuses[2].UserName, envelopeInfo, enrollmentId);
                 }
             }
         }

         public bool SaveDocuSignRoleStatus(string roleName, DocuSignEnvelopeInformation envelopeInfo, int enrollmentId)
         {
             bool statusUpdated = false;
             int enrollID;

             if (enrollmentId == 0)
                 using (ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                 {
                     courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                     courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                     enrollID = courseService.GetEnrollmentIdAgainstEnvelopeId(envelopeInfo.EnvelopeStatus.EnvelopeID);
                 }
             else
             {
                 enrollID = enrollmentId;
             }


             using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
             {
                 //int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                 trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                 trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                 statusUpdated = trackingService.SaveEnvelopStatusAgainstDocuSignRole(enrollID, roleName);

             }

             return statusUpdated;
         }

         #endregion

     
    
    }
}
