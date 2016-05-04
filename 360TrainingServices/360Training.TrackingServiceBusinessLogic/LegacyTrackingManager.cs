using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using _360Training.TrackingServiceDataLogic.StudentTrackingDA;
using _360Training.BusinessEntities;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
//using CommonAPI.Utility;
using System.Net;


namespace _360Training.TrackingServiceBusinessLogic
{
    public class LegacyTrackingManager
    {
        CustomBackGroundWorker cbgw = null;
        string learningSessionGuid;
        int courseId; 
        int studentId; 
        int epoch;
        string playerVersion;
        int source;


        private LegacyTrackingManager()
        {
        }

        public LegacyTrackingManager(string learningSessionGuid, int courseId, int studentId, int epoch, string playerVersion,int source)
        {
            this.learningSessionGuid = learningSessionGuid;
            this.courseId = courseId;
            this.studentId = studentId;
            this.epoch = epoch;
            this.playerVersion = playerVersion;
            this.source = source;
            Trace.WriteLine("learningSessionGuid = " + learningSessionGuid);
            Trace.Flush();
        }

        public void CreateThread()
        {
            cbgw = new CustomBackGroundWorker();
            cbgw.OnCompleted += new CustomBackGroundWorker.OnCompletedDelegate(CallOnComplete);
            cbgw.AMethod = new delegateMethod(DoWork);
            cbgw.StartWorker(cbgw);
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DateTime eventDate = DateTime.Now;
                LegacyStudentTrackingDA legacyStudentTrackingDA = new LegacyStudentTrackingDA();
                StudentTrackingDA studentTrackingDA = new StudentTrackingDA();
                LegacyLearnerStatistics legacyLearnerStatistics = null;
                LegacyCertificateInfo legacyCertificateInfolegacy = null;
                ICP4.BusinessLogic.IntegerationManager.Integeration integeration = ICP4.BusinessLogic.IntegerationManager.IntegerationFactory.GetObject(0);
                int testing_id = 0;
                int status = 0;
                bool completed = false;
                int currentTimeSpent = 0;


                if (playerVersion.Equals("2"))
                {
                    legacyLearnerStatistics = legacyStudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORICP2(this.courseId, this.studentId, this.epoch);

                    currentTimeSpent = studentTrackingDA.UpdateLearningSessionEndTimeLegacyICP(DateTime.Now, learningSessionGuid, legacyLearnerStatistics.PercentageCompleted, legacyLearnerStatistics.TimeSpent);

                    foreach (LegacyCourseTestingResult legacyCourseTestingResult in legacyLearnerStatistics.LegacyCourseTestingResult)
                    {
                        if (legacyCourseTestingResult.RawScore > -1)
                        {
                            studentTrackingDA.UpdateLearningSessionEndTimeLegacyTesting(DateTime.Now, learningSessionGuid, legacyCourseTestingResult.StatisticsType, legacyCourseTestingResult.RawScore);
                            
                            ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics integerationStatistics = new ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics();
                            integerationStatistics.AssessmentType = legacyCourseTestingResult.StatisticsType;
                            integerationStatistics.AssessmentScore = legacyCourseTestingResult.RawScore;
                            integerationStatistics.LearningSessionGuid = learningSessionGuid;
                            integerationStatistics.IntegerationStatisticsType = ICP4.BusinessLogic.IntegerationManager.IntegerationStatisticsType.AssessmentCompletion;
                            integeration.SynchStatsToExternalSystem(integerationStatistics);
                            
                            if (legacyCourseTestingResult.TestingId > testing_id)
                                testing_id = legacyCourseTestingResult.TestingId;
                        }
                    }
                }
                else if (playerVersion.Equals("3"))
                {
                    legacyLearnerStatistics = legacyStudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORICP3(this.courseId, this.studentId, this.epoch);

                    currentTimeSpent = studentTrackingDA.UpdateLearningSessionEndTimeLegacyICP(DateTime.Now, learningSessionGuid, legacyLearnerStatistics.PercentageCompleted, legacyLearnerStatistics.TimeSpent);
                    foreach (LegacyCourseTestingResult legacyCourseTestingResult in legacyLearnerStatistics.LegacyCourseTestingResult)
                    {
                        if (legacyCourseTestingResult.RawScore > -1)
                        {
                            studentTrackingDA.UpdateLearningSessionEndTimeLegacyTesting(DateTime.Now, learningSessionGuid, legacyCourseTestingResult.StatisticsType, legacyCourseTestingResult.RawScore);

                            ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics integerationStatistics = new ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics();
                            integerationStatistics.AssessmentType = legacyCourseTestingResult.StatisticsType;
                            integerationStatistics.AssessmentScore = legacyCourseTestingResult.RawScore;
                            integerationStatistics.LearningSessionGuid = learningSessionGuid;
                            integerationStatistics.IntegerationStatisticsType = ICP4.BusinessLogic.IntegerationManager.IntegerationStatisticsType.AssessmentCompletion;
                            integeration.SynchStatsToExternalSystem(integerationStatistics);
                            
                            if (legacyCourseTestingResult.TestingId > testing_id)
                                testing_id = legacyCourseTestingResult.TestingId;
                        }
                    }

                    //LCMS-4762 removing this logic
                    //if (this.source == 1)
                      //  status = 1;
                    //LCMS-4762 removing this logic
                }
                else if (playerVersion.Equals("14"))
                {
                    legacyLearnerStatistics = legacyStudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORSCORM(this.courseId, this.studentId, this.epoch);
                    
                    //Log will be recorded when course has been completed or certificate issued.
                    if (legacyLearnerStatistics.Completed == 1 || legacyLearnerStatistics.Completed == 5)
                    {
                        currentTimeSpent = studentTrackingDA.UpdateLearningSessionEndTimeLegacyICP(DateTime.Now, learningSessionGuid, legacyLearnerStatistics.PercentageCompleted, legacyLearnerStatistics.TimeSpent);
                        
                        foreach (LegacyCourseTestingResult legacyCourseTestingResult in legacyLearnerStatistics.LegacyCourseTestingResult)
                        {
                            if (legacyCourseTestingResult.RawScore > -1)
                            {
                                studentTrackingDA.UpdateLearningSessionEndTimeLegacyTesting(DateTime.Now, learningSessionGuid, LearnerStatisticsType.PostAssessment, legacyCourseTestingResult.RawScore);

                                ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics integerationStatistics = new ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics();
                                integerationStatistics.AssessmentType = legacyCourseTestingResult.StatisticsType;
                                integerationStatistics.AssessmentScore = legacyCourseTestingResult.RawScore;
                                integerationStatistics.LearningSessionGuid = learningSessionGuid;
                                integerationStatistics.IntegerationStatisticsType = ICP4.BusinessLogic.IntegerationManager.IntegerationStatisticsType.AssessmentCompletion;
                                integeration.SynchStatsToExternalSystem(integerationStatistics);

                                if (legacyCourseTestingResult.TestingId > testing_id)
                                    testing_id = legacyCourseTestingResult.TestingId;
                            }
                        }
                    }

                }
                else
                {

                }

                //update testing_id with status

                if (legacyLearnerStatistics.Completed == 1 || legacyLearnerStatistics.Completed == 5)
                {
                    legacyCertificateInfolegacy = legacyStudentTrackingDA.GET_LCMS_CONNECTOR_STATS_CERTIFICATE(this.courseId, this.studentId, this.epoch);
                    //legacyCertificateInfolegacy = new LegacyCertificateInfo();
                    if (legacyCertificateInfolegacy.CertificateExists)
                    {
                        //legacyCertificateInfolegacy.CertificateURL = legacyCertificateInfolegacy.CertificateURL.Replace("<LegacyClientId>", ConfigurationManager.AppSettings["LegacyClientId"]);
                        legacyCertificateInfolegacy.CertificateURL = ConfigurationManager.AppSettings["LegacyCertificateURL"];
                        legacyCertificateInfolegacy.CertificateURL = legacyCertificateInfolegacy.CertificateURL + "cn=" + this.courseId + "&assoc=" + ConfigurationManager.AppSettings["LegacyClientId"] + "&sid=" + this.studentId + "&asv=" + legacyCertificateInfolegacy.AsvId + "&epoch=" + this.epoch + "&cId=" + ConfigurationManager.AppSettings["LegacyClientId"];
                    }
                    status = 1;
                    completed = true;
                }

                legacyStudentTrackingDA.UPDATE_LCMS_STUDENTCOURSE(this.courseId, this.studentId, this.epoch, this.learningSessionGuid, testing_id, status);

                legacyStudentTrackingDA = null;
                studentTrackingDA = null;

                try
                {
                    /*
                    LearningSessionComplete.LearningSessionCompleteRequest request = new LearningSessionComplete.LearningSessionCompleteRequest();
                    request.learningSessionId = this.learningSessionGuid;
                    request.transactionGUID = Guid.NewGuid().ToString();
                    request.courseCompleted = completed;
                    request.courseCompletedSpecified = completed;
                    request.eventDate = eventDate;
                    request.eventDateSpecified = true;
                    if(legacyCertificateInfolegacy != null)
                        request.certificateURL = legacyCertificateInfolegacy.CertificateURL;

                    LearningSessionComplete.lmsLcmsService lmsLcmsService = new LearningSessionComplete.lmsLcmsService();
                    lmsLcmsService.Url = System.Configuration.ConfigurationManager.AppSettings["LearningSessionCompleteURL"];
                    ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
                    LearningSessionComplete.LearningSessionCompleteResponse response = lmsLcmsService.LearningSessionComplete(request);
                     */
 

                    ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics integerationStatistics = new ICP4.BusinessLogic.IntegerationManager.IntegerationStatistics();
                    integerationStatistics.PercentageCourseProgress = legacyLearnerStatistics.PercentageCompleted;
                    integerationStatistics.LearningSessionGuid = this.learningSessionGuid;
                    integerationStatistics.IsCourseCompleted = completed;
                    integerationStatistics.CourseTimeSpent = currentTimeSpent;
                    integerationStatistics.IntegerationStatisticsType = ICP4.BusinessLogic.IntegerationManager.IntegerationStatisticsType.CourseCompletion;
                    if (legacyCertificateInfolegacy != null)
                        integerationStatistics.CertificateURL = legacyCertificateInfolegacy.CertificateURL;
                    integeration.SynchStatsToExternalSystem(integerationStatistics);

                    //Trace.WriteLine("Do Work LMS = " + response.transactionResult.ToString());
                    //response.transactionResultMessage;
                }
                catch (System.Net.Sockets.SocketException sexp)
                {
                    Exception exp1 = new Exception("learnerSessioGUID = " + learningSessionGuid + " " + sexp.ToString(), sexp.InnerException);
                    ExceptionPolicyForLCMS.HandleException(exp1, "ICPException");
                    /*
                    LearningSessionComplete.LearningSessionCompleteRequest request = new LearningSessionComplete.LearningSessionCompleteRequest();
                    request.learningSessionId = learningSessionGuid;
                    request.transactionGUID = Guid.NewGuid().ToString();
                    //request.eventDate = DateTime.Now;

                    LearningSessionComplete.lmsLcmsService lmsLcmsService = new LearningSessionComplete.lmsLcmsService();
                    lmsLcmsService.Url = ConfigurationManager.AppSettings["LearningSessionCompleteURL_Cluster"];
                    lmsLcmsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
                    LearningSessionComplete.LearningSessionCompleteResponse response = lmsLcmsService.LearningSessionComplete(request);
                    */

                }

                catch (Exception ex)
                {
                    Exception exp1 = new Exception("learnerSessioGUID = " + this.learningSessionGuid + " " + ex.ToString(), ex.InnerException);
                    ExceptionPolicyForLCMS.HandleException(exp1, "Exception Policy");
                    /*
                    LearningSessionComplete.LearningSessionCompleteRequest request = new LearningSessionComplete.LearningSessionCompleteRequest();
                    request.learningSessionId = this.learningSessionGuid;
                    request.transactionGUID = Guid.NewGuid().ToString();
                    request.eventDate = eventDate;
                    request.eventDateSpecified = true;
                    request.courseCompleted = completed;
                    request.courseCompletedSpecified = completed;
                    if (legacyCertificateInfolegacy != null)
                        request.certificateURL = legacyCertificateInfolegacy.CertificateURL;

                    LearningSessionComplete.lmsLcmsService lmsLcmsService = new LearningSessionComplete.lmsLcmsService();
                    lmsLcmsService.Url = System.Configuration.ConfigurationManager.AppSettings["LearningSessionCompleteURL"];
                    LearningSessionComplete.LearningSessionCompleteResponse response = lmsLcmsService.LearningSessionComplete(request);
                     * */
                    //response.transactionResultMessage;

                }


                Trace.WriteLine("Do Work learningSessionGuid = " + learningSessionGuid);
                Trace.Flush();
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }  
        }

        public void CallOnComplete(RunWorkerCompletedEventArgs e)
        {


            Trace.WriteLine("Completed learningSessionGuid = " + learningSessionGuid);
            Trace.Flush();
            cbgw = null;
        }

    }
}
