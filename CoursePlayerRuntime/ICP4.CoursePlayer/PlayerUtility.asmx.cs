using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ICP4.BusinessLogic.CacheManager;
using ICP4.DataLogic.PlayerServerDA;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.BusinessLogic.ICPCourseService;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP4.BusinessLogic.CourseManager;
using _360Training.BusinessEntities;



namespace ICP4.CoursePlayer
{
    /// <summary>
    /// Summary description for PlayerUtility
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PlayerUtility : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

/*
        [WebMethod]
        public bool AddUpdateCache(
                    int publishedCourseId,

                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration,
                    ICP4.BusinessLogic.ICPCourseService.Sequence courseSequence,
                    ICP4.BusinessLogic.ICPCourseService.Sequence courseDemoSequence,
                    ICP4.BusinessLogic.ICPCourseService.TableOfContent courseTOC)
        {
            
            using (CacheManager cacheManager = new CacheManager())
            {
                return cacheManager.AddUpdateCourseCache(publishedCourseId, courseConfiguration, courseSequence, courseDemoSequence, courseTOC);                
            }            

        }
*/

        [WebMethod]
        public bool InvalidateCache(int publishedCourseId)
        {
            return InvalidateCacheAndNotifyToAllRemainingServers(publishedCourseId, false);
        }

        [WebMethod]
        public bool InvalidateCacheAndNotifyToAllRemainingServers(int publishedCourseId, bool notifytoAllRemainingServers)
        {
            PlayerServerDA playerServerDA = new PlayerServerDA(ConfigurationManager.AppSettings["PlayerServerSettingsFilePath"].ToString());

            try
            {
                using (CacheManager cacheManager = new CacheManager())
                {
                    cacheManager.InvalidateCache(publishedCourseId, notifytoAllRemainingServers, 0);
                    return cacheManager.InvalidateCache(publishedCourseId, notifytoAllRemainingServers, 1);
                    //throw new Exception("Hi test exception email");
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                playerServerDA.AddPlayerServerCacheLog(System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2].ToString(), publishedCourseId, 0, ex.ToString());
                EmailUtility emailUtility = new EmailUtility();
                String fromEmail = ConfigurationManager.AppSettings["FromEmailCacheInvalidation"].ToString();
                String toEmail = ConfigurationManager.AppSettings["ToEmailCacheInvalidation"].ToString();
                String subject = "Exception (" + ex.Message + ") while cache invalidating - " + System.Net.Dns.GetHostName() + " : " + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2];
                String body = "<b> Host Name : </b>" + System.Net.Dns.GetHostName();
                body += "<br><b> Host Address : </b>" + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2];
                body += "<br><b> DateTime : </b>" + System.DateTime.Now;
                body += "<br><br><b> Exception : </b><br>" + ex.ToString();


                String smtpServer = ConfigurationManager.AppSettings["SMTPAddress"].ToString();
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());

                emailUtility.sendMail(toEmail, fromEmail, "", "", subject, body, smtpServer, smtpPort);
                return false;
            }
        }

        [WebMethod]
        public bool InvalidateCourseConfigurationCache(int courseConfigurationID)
        {
            using (CacheManager cacheManager = new CacheManager())
            {
                return cacheManager.InvalidateCourseConfigurationCache(courseConfigurationID); 
            }
        }

        [WebMethod]
        public bool InvalidateCourseApprovalCache(int courseApprovalID, int courseID)
        {
            using (CacheManager cacheManager = new CacheManager())
            {
                cacheManager.InvalidateCourseApprovalCache(courseApprovalID);
                return InvalidateCacheAndNotifyToAllRemainingServers(courseID, false);
            }
        }

        [WebMethod]
        public int GetSessionTimeOutKeyValue()
        {
            System.Web.Configuration.SessionStateSection sessionStateSection = (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
            return sessionStateSection.Timeout.Minutes;
        }

        [WebMethod]
        public string GetAppSettingKeyValue(string Key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Key].ToString();
        }

        [WebMethod]
        public bool InvalidateBrandCache(string brandCode, string variant)
        {
            BusinessLogic.BrandManager.BrandManager brandManager = new BusinessLogic.BrandManager.BrandManager();

            return brandManager.UpdateBrand(variant, brandCode);
        }


        [WebMethod]
        public List<LockedCourseStatus> GetCourseLockedStatus(string listEnrollmentID)
        {                     
            try
            {  
               string[] namesArray = listEnrollmentID.Split(',');
               List<string> namesList = new List<string>(namesArray.Length);
               namesList.AddRange(namesArray);

               List<LockedCourseStatus> suggestedCourseList = null;
          
               suggestedCourseList = GetLockedCourseStatus(namesList.ToArray());


               return suggestedCourseList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
             
        }

        public List<LockedCourseStatus> GetLockedCourseStatus(string[] listEnrollmentID)
        {
            try
            {
                string enrollmentID = string.Empty;

                int courseConfigurationID = 0;

                List<LockedCourseStatus> LearnerEnrollmentList = null;

                LearnerEnrollmentList = new List<LockedCourseStatus>();

                for (int i = 0; i < listEnrollmentID.Length; i++)
                {
                    enrollmentID = listEnrollmentID[i];

                    string LockStatus = string.Empty;

                    Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");

                    System.Data.Common.DbCommand dbCommand = null;

                    LockedCourseStatus lockedCourseStaus = new LockedCourseStatus();

                    string LockType = string.Empty;

                    dbCommand = db.GetStoredProcCommand("ICP_GET_LOCKEDCOURSE_STATUS");


                    db.AddInParameter(dbCommand, "@ENROLLMENTID", DbType.String, enrollmentID);


                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {                            

                            lockedCourseStaus.EnrollmentID = dataReader["ENROLLMENT_ID"] == DBNull.Value ? string.Empty : dataReader["ENROLLMENT_ID"].ToString();

                            lockedCourseStaus.LockedStatus = dataReader["LOCKEDSTATUS"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["LOCKEDSTATUS"]);
                             
                            //lockedCourseStaus.LockedType = dataReader["LOCKTYPE"] == DBNull.Value ? string.Empty : dataReader["LOCKTYPE"].ToString();

                            LockType = dataReader["LOCKTYPE"] == DBNull.Value ? string.Empty : dataReader["LOCKTYPE"].ToString();

                            courseConfigurationID = Convert.ToInt32(dataReader["COURSECONFIGURATIONID"] == DBNull.Value ? string.Empty : dataReader["COURSECONFIGURATIONID"].ToString());

                            string ScreenText = dataReader["FINALMESSAGE"] == DBNull.Value ? string.Empty : dataReader["FINALMESSAGE"].ToString();

                            lockedCourseStaus.FinalMessage = StatusofEnrollment(lockedCourseStaus.EnrollmentID, LockType, ScreenText, courseConfigurationID);

                            LearnerEnrollmentList.Add(lockedCourseStaus);
                           
                        }

                    }
                }

                return LearnerEnrollmentList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        public string StatusofEnrollment(string enrollmentID, string lockingReason, string FinalMessage, int courseConfigurationID)
        {                     
            _360Training.BusinessEntities.CourseConfiguration courseConfiguration;

            using (CourseLockingManagement Management = new CourseLockingManagement())
            {
                 courseConfiguration = Management.GetCourseConfiguration(courseConfigurationID);
            }

            string ScreenMessageAppears = string.Empty;           

            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {            
                if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ValidationFailed)
                {         
                    ScreenMessageAppears = FinalMessage;   
               
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MaxAttemptReach)
                {
               
                    ScreenMessageAppears = FinalMessage;

                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MaxAttemptReachPostAssessment)
                {
                    
                    string CourseLockMessageText = FinalMessage;

                    CourseLockMessageText = CourseLockMessageText.Replace("$NUMBERINWORDS", CommonAPI.Utility.NumberToWordConvertor.NumberToText(courseConfiguration.PostAssessmentConfiguration.MaximumNOAttempt, false, ", First , Second , Third , Fourth , Fifth , Sixth , Seventh , Eight , Ninth", ", One , Two , Three , Four , Five , Six , Seven , Eight , Nine ", "Ten , Eleven , Twelve , Thirteen , Fourteen , Fifteen , Sixteen , Seventeen , Eighteen , Nineteen", "Twenty , Thirty , Forty , Fifty , Sixty , Seventy , Eighty , Ninety", "Thousand , Lakh , Crore"));

                    ScreenMessageAppears = CourseLockMessageText;
              
                }
                
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MaxAttemptReachLessonAssessment)
                {
                    string ContentObjectName = "";
                  
                    string CourseLockMessageText = FinalMessage;
                    CourseLockMessageText = CourseLockMessageText.Replace("$MAXATTEMPTNUMBER", CommonAPI.Utility.NumberToWordConvertor.NumberToText(courseConfiguration.QuizConfiguration.MaximumNOAttempt, false, ", First , Second , Third , Fourth , Fifth , Sixth , Seventh , Eight , Ninth", ", One , Two , Three , Four , Five , Six , Seven , Eight , Nine ", "Ten , Eleven , Twelve , Thirteen , Fourteen , Fifteen , Sixteen , Seventeen , Eighteen , Nineteen", "Twenty , Thirty , Forty , Fifty , Sixty , Seventy , Eighty , Ninety", "Thousand , Lakh , Crore"));
                    CourseLockMessageText = CourseLockMessageText.Replace("$CONTENTOBJECTNAME", ContentObjectName);
                    if (ContentObjectName.Equals(""))
                    {
                        CourseLockMessageText = CourseLockMessageText.Replace("quiz - ", "quiz");
                    }

                    ScreenMessageAppears = CourseLockMessageText;
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MaxAttemptReachPracticeExam)
                {
                    string ContentObjectName = "";
                    
                    string CourseLockMessageText = FinalMessage;
                    CourseLockMessageText = CourseLockMessageText.Replace("$MAXATTEMPTNUMBER", CommonAPI.Utility.NumberToWordConvertor.NumberToText(courseConfiguration.QuizConfiguration.MaximumNOAttempt, false, ", First , Second , Third , Fourth , Fifth , Sixth , Seventh , Eight , Ninth", ", One , Two , Three , Four , Five , Six , Seven , Eight , Nine ", "Ten , Eleven , Twelve , Thirteen , Fourteen , Fifteen , Sixteen , Seventeen , Eighteen , Nineteen", "Twenty , Thirty , Forty , Fifty , Sixty , Seventy , Eighty , Ninety", "Thousand , Lakh , Crore"));
                    CourseLockMessageText = CourseLockMessageText.Replace("$CONTENTOBJECTNAME", ContentObjectName);
                    if (ContentObjectName.Equals(""))
                    {
                        CourseLockMessageText = CourseLockMessageText.Replace("practice exam - ", "practice exam");
                    }
                        ScreenMessageAppears = CourseLockMessageText;
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MaxAttemptReachPreAssessment)
                {
                    
                    string CourseLockMessageText = FinalMessage;

                    CourseLockMessageText = CourseLockMessageText.Replace("$MAXATTEMPTNUMBER", CommonAPI.Utility.NumberToWordConvertor.NumberToText(courseConfiguration.PreAssessmentConfiguration.MaximumNOAttempt, false, ", First , Second , Third , Fourth , Fifth , Sixth , Seventh , Eight , Ninth", ", One , Two , Three , Four , Five , Six , Seven , Eight , Nine ", "Ten , Eleven , Twelve , Thirteen , Fourteen , Fifteen , Sixteen , Seventeen , Eighteen , Nineteen", "Twenty , Thirty , Forty , Fifty , Sixty , Seventy , Eighty , Ninety", "Thousand , Lakh , Crore"));

                    ScreenMessageAppears = CourseLockMessageText;
                             
                }

                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute)
                {
                
                    string CourseLockMessageText = FinalMessage;
                    string numDuration = courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute.ToString();
                    string duration = courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime;

                    if (duration.ToLower().EndsWith("s"))
                    {
                        duration = duration.Replace("s", "(s)");
                    }

                    CourseLockMessageText = CourseLockMessageText.Replace("$DURATION", numDuration + " " + duration);

                    ScreenMessageAppears = CourseLockMessageText;                
                
                }                
                
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.FailedCompletionMustCompleteWithinSpecificAmountOfTimeAfterRegistration)
                {

                    string CourseLockMessageText = FinalMessage;
                  
                    string numDuration = courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay.ToString();

                    CourseLockMessageText = CourseLockMessageText.Replace("$DURATION", numDuration + " " + "Day(s)");

                    ScreenMessageAppears = CourseLockMessageText;
                    
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MustStartCourseWithinSpecificAmountOfTimeAfterRegistration)
                {
                    
                    string CourseLockMessageText = FinalMessage;
                    int numDuration = courseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
                    string UnitMustStartWithinSpecifiedAmountOfTimeAfterRegistrationDate = courseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;

                    switch (UnitMustStartWithinSpecifiedAmountOfTimeAfterRegistrationDate)
                    {
                        case _360Training.BusinessEntities.TimeUnit.Minutes:
                            if (numDuration == 1)
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Minute)");
                            }
                            else
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Minutes)");
                            }
                            break;
                        case _360Training.BusinessEntities.TimeUnit.Months:
                            if (numDuration == 1)
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Month)");
                            }
                            else
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Months)");
                            }

                            break;
                        case _360Training.BusinessEntities.TimeUnit.Days:
                            if (numDuration == 1)
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Day)");
                            }
                            else
                            {
                                CourseLockMessageText = CourseLockMessageText.Replace("#DURATION", "(" + numDuration.ToString() + " " + "Days)");
                            }
                            break;
                    }

                    CourseLockMessageText = CourseLockMessageText.Replace("$DURATION", numDuration + " " + "Day(s)");
                    ScreenMessageAppears = CourseLockMessageText;
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.NothingEnable)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.GeneralCase)
                {
                    ScreenMessageAppears = FinalMessage;
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.IdleUserTimeElapsed)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.CoursePublishedScene)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.CoursePublishedAssessment)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ReportingFieldMisMatch)
                {
                    ScreenMessageAppears = FinalMessage;   
                }

                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.CourseApprovalNotAttachedWithCourse)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ReportingFieldNotAttachedWithCourseApproval)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.MonitorFieldMisMatch)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ClickingAwayFromActiveWindow)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ProctorLoginFailed)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ProctorAccountNotActive)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
                else if (lockingReason == ICP4.BusinessLogic.CourseManager.LockingReason.ProctorNotPartOfCredential)
                {
                    ScreenMessageAppears = FinalMessage;   
                }
            }

            //showCourseLocked.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCourseLocked;
            //showCourseLocked.CourseLockedMessage = courseLockedMessage;

            return ScreenMessageAppears;




        }

    }
}
