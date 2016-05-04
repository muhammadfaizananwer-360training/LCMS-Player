using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP4.BusinessLogic.CourseManager;
using System.Configuration;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ICP4.BusinessLogic.IntegerationManager
{
    public class IntegerationVU:Integeration
    {
        #region Integeration Members

        public void SynchStatsToExternalSystem(IntegerationStatistics integerationStatistics)
        {
            Logger.Write("SynchStatsToExternalSystem : " + DateTime.Now.ToString() + ":" + integerationStatistics.IntegerationStatisticsType, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
            int studentCourseStatus = 2;

            if (integerationStatistics.IsCourseLocked)
            {
                switch (integerationStatistics.LockReason)
                {
                    case LockingReason.ValidationFailed:
                        studentCourseStatus = 8;
                        break;
                    case LockingReason.MaxAttemptReachPreAssessment:
                        studentCourseStatus = 9;
                        break;
                    case LockingReason.MaxAttemptReachLessonAssessment:
                        studentCourseStatus = 10;
                        break;
                    case LockingReason.MaxAttemptReachPostAssessment:
                        studentCourseStatus = 11;
                        break;
                    case LockingReason.FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute:
                        studentCourseStatus = 12;
                        break;
                    case LockingReason.IdleUserTimeElapsed:
                        studentCourseStatus = 13;
                        break;
                    case LockingReason.FailedCompletionMustCompleteWithinSpecificAmountOfTimeAfterRegistration:
                        studentCourseStatus = 14;
                        break;
                    case LockingReason.MaxAttemptReachPracticeExam:
                        studentCourseStatus = 15;
                        break;
                    case LockingReason.MustStartCourseWithinSpecificAmountOfTimeAfterRegistration:
                        studentCourseStatus = 16;
                        break;

                }
            }
            else if(integerationStatistics.IsCourseCompleted)
            {
                studentCourseStatus = 1;
            }

            using (LCMS_VUConnectorService.LCMS_VUConnectorServiceService lCMS_VUConnectorServiceService = new ICP4.BusinessLogic.LCMS_VUConnectorService.LCMS_VUConnectorServiceService())
            {
                lCMS_VUConnectorServiceService.Url = ConfigurationManager.AppSettings["LCMS_VUConnectorService"];
                lCMS_VUConnectorServiceService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
                lCMS_VUConnectorServiceService.UseDefaultCredentials = true;

                int IntegerationVU_WS_Retries = 1;
                int.TryParse(ConfigurationManager.AppSettings["IntegerationVU_WS_Retries"], out IntegerationVU_WS_Retries);
                if (IntegerationVU_WS_Retries == 0)
                    IntegerationVU_WS_Retries = 1;


                for (int i = 0; i < IntegerationVU_WS_Retries; i++)
                {
                    try
                    {
                        if (lCMS_VUConnectorServiceService.UpdateCourseStatus(integerationStatistics.CourseGuid, integerationStatistics.Enrollment_Id, studentCourseStatus, integerationStatistics.CourseTimeSpent, integerationStatistics.PercentageCourseProgress, integerationStatistics.AssessmentScore, integerationStatistics.IsAssessmentPassed))
                            break;
                    }
                    catch (Exception exp)
                    {
                        string msg;
                        msg = exp.ToString();
                        msg = msg + "\n";
                        msg = msg + "------------------------------------------------------------------------------------";
                        msg = msg + "\n";
                        msg = msg + "LearningSessionGuid=" + integerationStatistics.LearningSessionGuid + ",studentCourseStatus=" + studentCourseStatus;

                        Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    }
                }
                

            }
        }

        #endregion
    }
}
