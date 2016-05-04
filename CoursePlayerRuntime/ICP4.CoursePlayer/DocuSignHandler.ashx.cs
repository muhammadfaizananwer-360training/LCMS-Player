using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Collections.Generic;
using System.Web.SessionState;
using System.IO;
using System.Threading;
using System.Configuration;
using ICP4.BusinessLogic.CourseManager;

using ICP4.BusinessLogic.DocuSignManager;
using ICP4.BusinessLogic.DocuSignAPI;
using System.Xml;
using System.Xml.Serialization;
using LCMS.Common;


using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using ICP4.BusinessLogic.ICPTrackingService;
using ICP4.BusinessLogic.ICPCourseService;

namespace ICP4.CoursePlayer
{
    /// <summary>
    /// Summary description for DocuSignHandler
    /// </summary>
    public class DocuSignHandler : IHttpHandler, IRequiresSessionState
    {
        HttpContext _context;

        string updatedURL;

        public void ProcessRequest(HttpContext context)
        {
            _context = context;
                          
            var occurredEvent = context.Request["event"];

            //This is used to write the event occured 
            Logger.Write("Event Occured :" + occurredEvent, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");


            updatedURL = ConvertURLFromHttpToHttps(_context);


            switch (occurredEvent)
            {
               
                case DocuSignEvents.SignComplete:
                    {
                        
                        SignComplete();
                        break;
                    }
                case DocuSignEvents.ViewComplete:
                    {
                        ViewComplete();
                        break;
                    }
                case DocuSignEvents.Cancel:
                    {
                       SignCancel();                        
                       break; 
                    }
                case DocuSignEvents.Decline:
                    {
                        SignDecline();
                        break;
                    }
                case DocuSignEvents.Timeout:
                    {
                        SignTimeout();
                        break; 
                    }
                case DocuSignEvents.TTLExpired:
                    {
                        SignExpired();
                        break;
                    }
                case DocuSignEvents.IDCheckFailed:
                    {
                        break;
                    }
                case DocuSignEvents.AccessCodeFailed:
                    {
                        break;    
                    }
                case DocuSignEvents.Exception:
                    {
                        break;
                    }
                case DocuSignEvents.FaxPending:
                    {
                        break;
                    }
                
            }
          
        }
       
        protected void SignComplete()
        {
            try
            {
                bool dualSignerRole = false;
                string envelopeId = _context.Request["envelopeId"];

                DocuSignHelper dsh = new DocuSignHelper();

                APIServiceSoapClient client = dsh.CreateAPIProxy();

                EnvelopeStatus status = client.RequestStatus(envelopeId);

                if (status.RecipientStatuses.Count() > 0)
                {
                    if (status.RecipientStatuses[0].Status == RecipientStatusCode.Completed)
                    {
                        SaveDocuSignRoleStatus(status.RecipientStatuses[0].UserName);
                    }

                    if (status.RecipientStatuses.Count() == 3)
                    {
                        dualSignerRole = true;
                    }

                }                          
                  
                if (status.Status == EnvelopeStatusCode.Completed)
                    {                        
                        SignCompleteDocuSign(envelopeId, client);
                    }

                 _context.Response.Redirect(updatedURL + "Redirector.aspx" + "?op=complete" + "&rollname=" + dualSignerRole, false);
              
               // _context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=complete" + "&rollname=" + dualSignerRole, false);
                
            }
            catch (ThreadAbortException ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPExcep4");
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICP");
               
            }


            
        }
        protected void SignTimeout()
        {
            _context.Response.Redirect(updatedURL +  "Redirector.aspx" + "?op=timeout");
           //_context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=timeout");

        }
        protected void SignExpired()
        {
             _context.Response.Redirect(updatedURL + "Redirector.aspx" + "?op=expired");
            //_context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=expired");
        }
        protected void SignCancel()
        {
          
               _context.Response.Redirect(updatedURL + "Redirector.aspx" + "?op=cancel");       
            //_context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=cancel");
          
        }
        protected void SignDecline()
        {
            DocuSignDecline();
            _context.Response.Redirect(updatedURL + "Redirector.aspx" + "?op=decline");
            //_context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=decline");
        }
        protected void ViewComplete()
        {
            _context.Response.Redirect(updatedURL + "Redirector.aspx" + "?op=viewcomplete");
            //_context.Response.Redirect(VirtualPathUtility.ToAbsolute("~/Redirector.aspx") + "?op=viewcomplete");
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region DocuSignDecline
    
        public void DocuSignDecline()
        {
            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);

            LearnerProfile serviceLearnerProfile;

            using (CourseService courseService = new CourseService())
            {
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];

                courseService.SaveStatusAfterDocuSignDecline(enrollmentID);
            }

            using (TrackingService trackingService = new TrackingService())
            {
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                serviceLearnerProfile = trackingService.GetUserProfileInformation(enrollmentID);
            }
            string mailBody = CreateBody(serviceLearnerProfile);
            SendEmail(ConfigurationManager.AppSettings["ToEmailDocuSignDecline"], ConfigurationManager.AppSettings["FromEmailDocuSignDecline"], ConfigurationManager.AppSettings["SubjectEmailDocuSignDecline"], mailBody);

        }

        public string CreateBody(LearnerProfile profile)
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
 

        #region SignCompleteDocuSign

        public void SignCompleteDocuSign(string envelopeId, APIServiceSoapClient client)
        {
            int enrollmentID = 0;
            EnvelopePDF pdf = null;

            if (System.Web.HttpContext.Current.Session["EnrollmentID"] != null)
            {
                enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.QueryString["enrollmentID"] != null)
                {
                    enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["enrollmentID"]);

                }
            }

            string fileName = enrollmentID + "_DocuSign" + ".pdf";

            // Download all documents as one pdf
            pdf = client.RequestPDF(envelopeId);

            // Open file for reading
            string directoryPath = System.Web.HttpContext.Current.Server.MapPath(VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["signedpdfs"]));

            FileInfo directoryInfo = new FileInfo(directoryPath);
            if (!directoryInfo.Directory.Exists)
            {
                directoryInfo.Create();
            }

            // string fileName = Guid.NewGuid().ToString() + ".pdf";
            string path = directoryPath + fileName;

            System.IO.FileStream _FileStream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(pdf.PDFBytes, 0, pdf.PDFBytes.Length);

            // close file stream
            _FileStream.Flush();
            _FileStream.Close();

            using (CourseManager courseManager = new CourseManager())
            {
                courseManager.SaveStatusAfterDocuSignProcessComplete();
            }

            UploadDocuSignDocument(path, fileName);

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


        #region SaveDocuSignRoleStatus

        public bool SaveDocuSignRoleStatus(string roleName)
        {
            bool statusUpdated = false;
            using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
            {
                int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                if (roleName == ConfigurationManager.AppSettings["RollNameForProctor"])
                {
                    statusUpdated = trackingService.SaveEnvelopStatusAgainstDocuSignRole(enrollmentID, roleName);
                
                }
                else
                {
                    statusUpdated = trackingService.SaveEnvelopStatusAgainstDocuSignRole(enrollmentID, ConfigurationManager.AppSettings["RollNameForLearner"]);
                }


                
            }

            return statusUpdated;
        }

        #endregion


        private string ConvertURLFromHttpToHttps(HttpContext context)
        {
            string strReplace = string.Empty;

            if (context.Request.Url.ToString().Contains("http://"))
            {
               
                string strURL = context.Request.Url.ToString();
                Array arrURL = strURL.Split('/');              
                string strHttpsPath;            
                strHttpsPath = "https://" + arrURL.GetValue(2).ToString() + context.Request.ApplicationPath + "/";
                strReplace = strHttpsPath;
               
            }
            return strReplace;
        
        }
    
    
    }
    
}
