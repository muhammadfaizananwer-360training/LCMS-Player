using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Configuration;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace ICP4.BusinessLogic.IntegerationManager
{
    public class IntegrationDirectDB : Integeration
    {
        public void SynchStatsToExternalSystem(IntegerationStatistics integerationStatistics)
        {
            try
            {
                //ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                //trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                //trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
                //trackingService.UpdateLearnerCourseStatistics(integerationStatistics.Enrollment_Id, integerationStatistics.CertificateURL, integerationStatistics.IsCourseCompleted);
                DbCommand dbCommand = null;
                bool isUpdated = false;

                    // This SP updates the Learner Course Statistics against an LearningSessionGUID
                    dbCommand = db.GetStoredProcCommand("ICP_UPDATE_LEARNERCOURSESTATISTICS_WITH_LEARNINGSESSION");
                    db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, integerationStatistics.LearningSessionGuid);
                    db.AddInParameter(dbCommand, "@CERTIFICATE_URL", DbType.String, integerationStatistics.CertificateURL);
                    db.AddInParameter(dbCommand, "@ISCOMPLETED", DbType.Boolean, integerationStatistics.IsCourseCompleted);
                    if (db.ExecuteNonQuery(dbCommand) > 0)
                        isUpdated = true;

            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");            

            }
            finally
            {
                SynchStatsToExternalSystemLMS(integerationStatistics);
            }
         
        }
        public void SynchStatsToExternalSystemLMS(IntegerationStatistics integerationStatistics)
        {
            Logger.Write("LMS SynchStatsToExternalSystem : " + DateTime.Now.ToString(), "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
            LearningSessionComplete.lmsLcmsService lmsLcmsService = new ICP4.BusinessLogic.LearningSessionComplete.lmsLcmsService();
            lmsLcmsService.Url = ConfigurationManager.AppSettings["LearningSessionCompleteURL"];
            lmsLcmsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();

            if (integerationStatistics.IntegerationStatisticsType == IntegerationStatisticsType.AssessmentCompletion)
            {
                LearningSessionComplete.LearnerStatsTransferRequest request = new ICP4.BusinessLogic.LearningSessionComplete.LearnerStatsTransferRequest();
                request.assessmentScore = integerationStatistics.AssessmentScore;
                request.assessmentScoreSpecified = true;
                request.assessmentType = integerationStatistics.AssessmentType;
                request.courseCompleted = integerationStatistics.IsCourseCompleted;
                request.courseCompletedSpecified = true;
                request.eventDate = DateTime.Now;
                request.eventDateSpecified = true;
                request.learningSessionId = integerationStatistics.LearningSessionGuid;
                request.transactionGUID = Guid.NewGuid().ToString();
                request.certificateURL = integerationStatistics.CertificateURL;
                request.courseCompletedSpecified = true;

                int IntegerationLMS_WS_Retries = 1;
                int.TryParse(ConfigurationManager.AppSettings["IntegerationLMS_WS_Retries"], out IntegerationLMS_WS_Retries);
                if (IntegerationLMS_WS_Retries == 0)
                    IntegerationLMS_WS_Retries = 1;

                for (int i = 0; i < IntegerationLMS_WS_Retries; i++)
                {
                    try
                    {
                        LearningSessionComplete.LearnerStatsTransferResponse response = lmsLcmsService.LearnerStatsTransfer(request);
                        break;
                    }
                    catch (System.Net.Sockets.SocketException sexp)
                    {
                        lmsLcmsService.Url = ConfigurationManager.AppSettings["LearningSessionCompleteURL_Cluster"];
                        LearningSessionComplete.LearnerStatsTransferResponse response = lmsLcmsService.LearnerStatsTransfer(request);

                        break;

                    }
                    catch (Exception exp)
                    {
                        string msg;
                        msg = exp.ToString();
                        msg = msg + "\n";
                        msg = msg + "------------------------------------------------------------------------------------";
                        msg = msg + "\n";
                        msg = msg + "LearningSessionGuid=" + integerationStatistics.LearningSessionGuid + ",IntegerationStatisticsType=" + integerationStatistics.IntegerationStatisticsType;

                        Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    }
                }
            }
            /*else if (integerationStatistics.IntegerationStatisticsType == IntegerationStatisticsType.CourseCompletion)
            {
                LearningSessionComplete.LearnerCourseProgressRequest request = new ICP4.BusinessLogic.LearningSessionComplete.LearnerCourseProgressRequest();
                request.learningSessionId = integerationStatistics.LearningSessionGuid;
                request.timeInLearningSession = integerationStatistics.CourseTimeSpent;
                request.timeInLearningSessionSpecified = true;
                request.transactionGUID = Guid.NewGuid().ToString();
                request.eventDate = DateTime.Now;
                request.eventDateSpecified = true;
                //request.courseProgress = (int)integerationStatistics.PercentageCourseProgress;
                //request.courseProgressSpecified = true;
                request.courseCompleted = integerationStatistics.IsCourseCompleted;
                request.courseCompletedSpecified = true;
                
                
                int IntegerationLMS_WS_Retries = 1;
                int.TryParse(ConfigurationManager.AppSettings["IntegerationLMS_WS_Retries"], out IntegerationLMS_WS_Retries);
                if (IntegerationLMS_WS_Retries == 0)
                    IntegerationLMS_WS_Retries = 1;

                for (int i = 0; i < IntegerationLMS_WS_Retries; i++)
                {
                    try
                    {
                        LearningSessionComplete.LearnerCourseProgressResponse response = lmsLcmsService.LearnerCourseProgress(request);

                        break;
                    }
                    catch (Exception exp)
                    {
                        string msg;
                        msg = exp.ToString();
                        msg = msg + "\n";
                        msg = msg + "------------------------------------------------------------------------------------";
                        msg = msg + "\n";
                        msg = msg + "LearningSessionGuid=" + integerationStatistics.LearningSessionGuid + ",IntegerationStatisticsType=" + integerationStatistics.IntegerationStatisticsType;

                        Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    }
                }
            }*/
            else if (integerationStatistics.IntegerationStatisticsType == IntegerationStatisticsType.ProgressCompletion || integerationStatistics.IntegerationStatisticsType == IntegerationStatisticsType.CourseCompletion)
            {

                LearningSessionComplete.LearnerCourseProgressRequest request = new ICP4.BusinessLogic.LearningSessionComplete.LearnerCourseProgressRequest();
                request.learningSessionId = integerationStatistics.LearningSessionGuid;
                request.timeInLearningSession = integerationStatistics.CourseTimeSpent;
                request.timeInLearningSessionSpecified = true;
                request.transactionGUID = Guid.NewGuid().ToString();
                request.eventDate = DateTime.Now;
                request.eventDateSpecified = true;
                request.courseProgress = (int)integerationStatistics.PercentageCourseProgress;
                request.courseProgressSpecified = true;
                request.courseCompleted = integerationStatistics.IsCourseCompleted;
                request.courseCompletedSpecified = true;
                request.certificateURL = integerationStatistics.CertificateURL;
                request.courseCompletedSpecified = true;


                int IntegerationLMS_WS_Retries = 1;
                int.TryParse(ConfigurationManager.AppSettings["IntegerationLMS_WS_Retries"], out IntegerationLMS_WS_Retries);
                if (IntegerationLMS_WS_Retries == 0)
                    IntegerationLMS_WS_Retries = 1;

                for (int i = 0; i < IntegerationLMS_WS_Retries; i++)
                {
                    try
                    {
                        LearningSessionComplete.LearnerCourseProgressResponse response = lmsLcmsService.LearnerCourseProgress(request);
                        if (response.transactionResultMessage.Contains("http://"))
                        {
                            System.Web.HttpContext.Current.Session["RedirectURL"] = response.transactionResultMessage.ToString();
                        }
                        break;
                    }
                    catch (System.Net.Sockets.SocketException sexp)
                    {
                        lmsLcmsService.Url = ConfigurationManager.AppSettings["LearningSessionCompleteURL_Cluster"];
                        LearningSessionComplete.LearnerCourseProgressResponse response = lmsLcmsService.LearnerCourseProgress(request);

                        break;

                    }
                    catch (Exception exp)
                    {
                        string msg;
                        msg = exp.ToString();
                        msg = msg + "\n";
                        msg = msg + "------------------------------------------------------------------------------------";
                        msg = msg + "\n";
                        msg = msg + "LearningSessionGuid=" + integerationStatistics.LearningSessionGuid + ",IntegerationStatisticsType=" + integerationStatistics.IntegerationStatisticsType;

                        Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    }
                }


                //request.eventDate = DateTime.Now;

                //LearningSessionComplete.lmsLcmsService lmsLcmsService = new ICP4.BusinessLogic.LearningSessionComplete.lmsLcmsService();
                //lmsLcmsService.Url = ConfigurationManager.AppSettings["LearningSessionCompleteURL"];
                //lmsLcmsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                //ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();


            }




        }
    }
}
