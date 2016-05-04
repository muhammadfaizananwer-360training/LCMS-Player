using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using _360Training.BusinessEntities;
using _360Training.TrackingServiceDataLogic.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace _360Training.TrackingServiceDataLogic.StudentTrackingDA
{
    public class LegacyStudentTrackingDA: IDisposable, ILegacyStudentTrackingDA 
    {
        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        public LegacyStudentTrackingDA()
        {
            db = DatabaseFactory.CreateDatabase("360CoursewareTrainingServiceDB");
        }
        #endregion 

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        #endregion

        #region ILegacyStudentTrackingDAMembers

        #endregion


        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyLearnerStatistics</returns>        
        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORSCORM(int courseID, int studentID, int Epoch)
        {
            DbCommand dbCommand = null;
            try
            {               
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LCMS_CONNECTOR_STATS_FORSCORM);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                db.AddOutParameter(dbCommand, "@COMPLETED", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@FINALSCORE", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PERCENTAGECOMPLETE", DbType.Int32, 4);                
                db.ExecuteNonQuery(dbCommand);

                LegacyLearnerStatistics legacyLearnerStatistics = new LegacyLearnerStatistics();
                LegacyCourseTestingResult legacyCourseTestingResult = new LegacyCourseTestingResult(); 
                List<LegacyCourseTestingResult> legacyCourseTestingResultlist = new List<LegacyCourseTestingResult>();  

                legacyLearnerStatistics.Completed = dbCommand.Parameters["@COMPLETED"].Value==DBNull.Value ? -1 : Convert.ToInt32(dbCommand.Parameters["@COMPLETED"].Value);
                legacyCourseTestingResult.RawScore = dbCommand.Parameters["@FINALSCORE"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@FINALSCORE"].Value);
                legacyCourseTestingResultlist.Add(legacyCourseTestingResult);
                legacyCourseTestingResult.StatisticsType = LearnerStatisticsType.PostAssessment;
                legacyLearnerStatistics.LegacyCourseTestingResult = legacyCourseTestingResultlist;
                legacyLearnerStatistics.PercentageCompleted = dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value);
                
                return legacyLearnerStatistics;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }            
        }

        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyLearnerStatistics</returns>        
        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP3(int courseID, int studentID, int Epoch)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LCMS_CONNECTOR_STATS_FORICP3);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                db.AddOutParameter(dbCommand, "@COMPLETED", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@TimeSpent", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PERCENTAGECOMPLETE", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@studentRecord_id", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);

                LegacyLearnerStatistics legacyLearnerStatistics = new LegacyLearnerStatistics();
                legacyLearnerStatistics.Completed = dbCommand.Parameters["@COMPLETED"].Value == DBNull.Value ? -1 : Convert.ToInt32(dbCommand.Parameters["@COMPLETED"].Value);
                legacyLearnerStatistics.TimeSpent = dbCommand.Parameters["@TimeSpent"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@TimeSpent"].Value);
                legacyLearnerStatistics.PercentageCompleted = dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value);
                legacyLearnerStatistics.StudentRecordId = dbCommand.Parameters["@studentRecord_id"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@studentRecord_id"].Value);

                legacyLearnerStatistics.LegacyCourseTestingResult = GET_LCMS_CONNECTOR_STATS_FORTESTING(courseID, studentID, Epoch);
                return legacyLearnerStatistics;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }             
        }

        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyLearnerStatistics</returns>        
        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP2(int courseID, int studentID, int Epoch)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LCMS_CONNECTOR_STATS_FORICP2);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                db.AddOutParameter(dbCommand, "@COMPLETED", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@TimeSpent", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PERCENTAGECOMPLETE", DbType.Int32, 4);                
                db.AddOutParameter(dbCommand, "@studentRecord_id", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);

                LegacyLearnerStatistics legacyLearnerStatistics = new LegacyLearnerStatistics();
                legacyLearnerStatistics.Completed = dbCommand.Parameters["@COMPLETED"].Value == DBNull.Value ? -1 : Convert.ToInt32(dbCommand.Parameters["@COMPLETED"].Value);
                legacyLearnerStatistics.TimeSpent = dbCommand.Parameters["@TimeSpent"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@TimeSpent"].Value);
                legacyLearnerStatistics.PercentageCompleted = dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@PERCENTAGECOMPLETE"].Value);
                legacyLearnerStatistics.StudentRecordId = dbCommand.Parameters["@studentRecord_id"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@studentRecord_id"].Value);

                legacyLearnerStatistics.LegacyCourseTestingResult = GET_LCMS_CONNECTOR_STATS_FORTESTING(courseID, studentID, Epoch);  
                return legacyLearnerStatistics;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyCertificateInfo</returns>        
        public LegacyCertificateInfo GET_LCMS_CONNECTOR_STATS_CERTIFICATE(int courseID, int studentID, int Epoch)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LCMS_CONNECTOR_STATS_FORCERTIFICATE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                db.AddOutParameter(dbCommand, "@CERTIFICATEEXISTS", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@CurrentAsv", DbType.Int32, 4);                
                db.ExecuteNonQuery(dbCommand);

                LegacyCertificateInfo legacyCertificateInfo = new LegacyCertificateInfo();
                legacyCertificateInfo.AsvId = dbCommand.Parameters["@CurrentAsv"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@CurrentAsv"].Value);
                legacyCertificateInfo.CertificateExists = dbCommand.Parameters["@CertificateExists"].Value == DBNull.Value ? false : Convert.ToBoolean(dbCommand.Parameters["@CertificateExists"].Value);
                

                return legacyCertificateInfo;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }  
        }

        /// <summary>
        /// This method Update the TestingID
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// <param name="Epoch">int Epoch</param>        
        /// <param name="LearningSessionGuid">string LearningSessionGuid</param>
        /// <param name="TestingId">int TestingId</param>
        /// <returns></returns>        
        public void UPDATE_LCMS_STUDENTCOURSE(int courseID, int studentID, int Epoch, string LearningSessionGuid, int TestingId,int status)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LCMS_STUDENTCOURSE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                db.AddInParameter(dbCommand, "@LearningSessionGuid",DbType.String ,LearningSessionGuid);
                db.AddInParameter(dbCommand, "@Testing_Id", DbType.Int32, TestingId);
                db.AddInParameter(dbCommand, "@status", DbType.Int32, status);
                
                db.ExecuteNonQuery(dbCommand);                
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");                
            }
        }

        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyLearnerStatistics</returns>        
        public List<LegacyCourseTestingResult> GET_LCMS_CONNECTOR_STATS_FORTESTING(int courseID, int studentID, int Epoch)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LCMS_CONNECTOR_STATS_FORTESTING);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@STUDENT_ID", DbType.Int32, studentID);
                db.AddInParameter(dbCommand, "@EPOCH", DbType.Int32, Epoch);
                                                
                List<LegacyCourseTestingResult> legacyCourseTestingResultlist = new List<LegacyCourseTestingResult>();                

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        LegacyCourseTestingResult legacyCourseTestingResult = new LegacyCourseTestingResult();
                        legacyCourseTestingResult.TestingId = Convert.ToInt32(dataReader["testing_id"]);
                        if (dataReader["Quiz_Marker"].ToString().Length > 0)
                        {
                            legacyCourseTestingResult.StatisticsType = LearnerStatisticsType.Quiz;
                        }
                        if (dataReader["Final_Marker"].ToString().Length > 0)
                        {
                            legacyCourseTestingResult.StatisticsType = LearnerStatisticsType.PostAssessment;
                        }

                        legacyCourseTestingResult.TimeStamp = Convert.ToDateTime(dataReader["date_taken"]);
                        legacyCourseTestingResult.RawScore = Convert.ToInt32(dataReader["grade"]);
                        legacyCourseTestingResultlist.Add(legacyCourseTestingResult);
                    }
                }
                
                return legacyCourseTestingResultlist;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

    }
}
