using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP4.BusinessLogic.CourseManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Configuration;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ICP4.BusinessLogic.CourseManager
{
    public class ProctorManagement : IDisposable
    {

        public ProctorManagement()
        { 
        }

        
        public bool LockCourse(String LearningSessionGuid, String Mode, string SecurityCode)
        {
            try
            {
                if (!SecurityCode.Equals("360training"))
                    return false;

                Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
                //trackingService.UpdateLearnerCourseStatistics(integerationStatistics.Enrollment_Id, integerationStatistics.CertificateURL, integerationStatistics.IsCourseCompleted);
                DbCommand dbCommand = null;
                bool isUpdated = false;

                // This SP updates the Learner Course Statistics against an LearningSessionGUID
                dbCommand = db.GetStoredProcCommand("LOCK_COURSE_BY_PROCTOR");
                db.AddInParameter(dbCommand, "@LearningSessionGuid", DbType.String, LearningSessionGuid);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
                System.Diagnostics.Trace.WriteLine("LockCourse : 1" + LearningSessionGuid + ":" + Mode + ":" + SecurityCode + ":" + isUpdated);
                System.Diagnostics.Trace.Flush();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                return false;
            }

        }

        
        public bool UnLockCourse(String LearningSessionGuid, String Mode, string SecurityCode)
        {
            try
            {
                if (!SecurityCode.Equals("360training"))
                    return false;

                Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
                //trackingService.UpdateLearnerCourseStatistics(integerationStatistics.Enrollment_Id, integerationStatistics.CertificateURL, integerationStatistics.IsCourseCompleted);
                DbCommand dbCommand = null;
                bool isUpdated = false;

                // This SP updates the Learner Course Statistics against an LearningSessionGUID
                dbCommand = db.GetStoredProcCommand("UNLOCK_COURSE_BY_PROCTOR");
                db.AddInParameter(dbCommand, "@LearningSessionGuid", DbType.String, LearningSessionGuid);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                return false;
            }

        }

        public object IsLockCourse(object currentCommand, String LearningSessionGuid)
        {
            try
            {
                if (this.IsLockCourse(LearningSessionGuid))
                {
                    System.Web.HttpContext.Current.Session["CurrentCommand"] = currentCommand;
                    ICP4.CommunicationLogic.CommunicationCommand.ProctorLock.ProctorLockCourseCommand
                           proctorLockCourseCommand = new ICP4.CommunicationLogic.CommunicationCommand.ProctorLock.ProctorLockCourseCommand();
                    proctorLockCourseCommand.CommandName = "ProctorLockCourseCommand";
                    ICP4.CommunicationLogic.CommunicationCommand.ProctorLock.ProctorLockCourseCommandMessage proctorLockCourseCommandMessage
                        = new ICP4.CommunicationLogic.CommunicationCommand.ProctorLock.ProctorLockCourseCommandMessage();
                    proctorLockCourseCommandMessage.LockCourse = true;
                   // proctorLockCourseCommandMessage.LockCourseMessage = "<br> Hello, Your course is locked. Please use the chat window to communicate with your proctor.And click <button class='button'> Here</button> to Unlock. <br> <br>";
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
                        string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
                        proctorLockCourseCommandMessage.LockCourseMessage = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.OnlineProctorMessage, brandCode, variant);
                    }
                       
                    proctorLockCourseCommand.ProctorLockCourseCommandMessage = proctorLockCourseCommandMessage;
                    return proctorLockCourseCommand;
                }
                else
                    return currentCommand;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                return currentCommand;
            }
        }

        public bool IsLockCourse(String LearningSessionGuid)
        {
            try
            {
 
                Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
                //trackingService.UpdateLearnerCourseStatistics(integerationStatistics.Enrollment_Id, integerationStatistics.CertificateURL, integerationStatistics.IsCourseCompleted);
                DbCommand dbCommand = null;
                bool isUpdated = false;

                // This SP updates the Learner Course Statistics against an LearningSessionGUID
                dbCommand = db.GetStoredProcCommand("ISLOCK_COURSE_BY_PROCTOR");
                db.AddInParameter(dbCommand, "@LearningSessionGuid", DbType.String, LearningSessionGuid);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        isUpdated = (bool)dataReader[0];
                }

                return isUpdated;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                return false;
            }

        }

        #region IDisposable Members


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
