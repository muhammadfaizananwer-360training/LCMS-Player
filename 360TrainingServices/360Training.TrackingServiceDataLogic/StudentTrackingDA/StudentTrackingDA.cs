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
    public class StudentTrackingDA : IDisposable, IStudentTrackingDA
    {
        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        public StudentTrackingDA()
        {
            db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IStudentTrackingDAMembers
        /// <summary>
        /// This method gets the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>int sequenceNo.</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetStudentCourseTrack(int courseID, int learnerID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets the sequenceno 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SEQUENCENO);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                int sequenceNo = -1;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sequenceNo = dataReader["SEQUENCENO"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["SEQUENCENO"]);
                    }
                }
                return sequenceNo;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method saves the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int curseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="sequenceNo">int sequenceNo</param>
        /// <returns>returns true if success,else false</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public bool SaveStudentCourseTrack(int courseID, int learnerID, int sequenceNo)
        {
            DbCommand dbCommand = null;
            bool operationSuccessfull = false;
            try
            {
                //This SP inserts the sequenceno if not exists else updates it 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_SEQUENCENO);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@SEQUENCENO", DbType.Int32, sequenceNo);
                int rows = db.ExecuteNonQuery(dbCommand);
                if (rows > 0)
                    operationSuccessfull = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                operationSuccessfull = false;
            }
            return operationSuccessfull;
        }
        /// <summary>
        /// This method updates the learning session endtime
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <param name="endTime">datetime endtime</param>
        /// <returns>true if successfull,else false</returns>
        public bool UpdateLearningSessionEndtime(string sessionID, DateTime endTime, int totalTimeSpent, double percentageCourseProgress, long enrollment_Id)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNINGSESSION_ENDTIME);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, sessionID);
                db.AddInParameter(dbCommand, "@TOTALTIMESPENT", DbType.Int32, totalTimeSpent);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int64, enrollment_Id);
                db.AddInParameter(dbCommand, "@MAXPERCENTCOURSEATTENDED", DbType.Double, percentageCourseProgress);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }

        /// <summary>
        /// This method updates the learning session endtime
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <param name="endTime">datetime endtime</param>
        /// <returns>true if successfull,else false</returns>
        public bool UpdateLearningSessionEndtimeLegacy(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNINGSESSION_ENDTIME_LEGACY);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, sessionID);
                db.AddInParameter(dbCommand, "@MAXPERCENTAGECOURSEATTENDED", DbType.Decimal, MAXPERCENTAGECOURSEATTENDED);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }

        /// <summary>
        /// This method updates the learning session endtime
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <param name="endTime">datetime endtime</param>
        /// <returns>true if successfull,else false</returns>
        public bool UpdateLearningSessionEndtimeLegacyScorm(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED, bool PASSEDASSESSMENT, decimal RAWSCORE)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNINGSESSION_ENDTIME_LEGACY_SCORM);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, sessionID);
                db.AddInParameter(dbCommand, "@MAXPERCENTAGECOURSEATTENDED", DbType.Decimal, MAXPERCENTAGECOURSEATTENDED);
                db.AddInParameter(dbCommand, "@PASSEDASSESSMENT", DbType.Boolean, PASSEDASSESSMENT);
                db.AddInParameter(dbCommand, "@ACHEIVEDASSESSMENTMASTERY", DbType.Boolean, PASSEDASSESSMENT);
                db.AddInParameter(dbCommand, "@RAWSCORE", DbType.Decimal, RAWSCORE);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }

        /// <summary>
        /// This method updates the learning session endtime of legacy ICP
        /// </summary>        
        /// <param name="endTime">datetime endtime</param>
        /// <param name="LEARNINGSESSION_GUID">string LEARNINGSESSION_GUID</param>
        /// <param name="MAXPERCENTAGECOURSEATTENDED">decimal MAXPERCENTAGECOURSEATTENDED</param>
        /// <param name="TIMESPENT">int TIMESPENT</param>
        /// <returns>true if successfull,else false</returns>
        public int UpdateLearningSessionEndTimeLegacyICP(DateTime endTime, string LEARNINGSESSION_GUID, decimal MAXPERCENTAGECOURSEATTENDED, int TIMESPENT)
        {
            DbCommand dbCommand = null;
            int currentTime = 0;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNINGSESSION_ENDTIME_LEGACY_ICP);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, LEARNINGSESSION_GUID);
                db.AddInParameter(dbCommand, "@MAXPERCENTAGECOURSEATTENDED", DbType.Decimal, MAXPERCENTAGECOURSEATTENDED);
                db.AddInParameter(dbCommand, "@TIMESPENT", DbType.Int32, TIMESPENT);
                db.AddOutParameter(dbCommand, "@TIMESPENT_DIFF", DbType.Int32, 0);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    currentTime = Convert.ToInt32(dbCommand.Parameters["@TIMESPENT_DIFF"].Value);
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }
            return currentTime;
        }

        /// <summary>
        /// This method updates the learning session endtime of legacy tesing
        /// </summary>        
        /// <param name="endTime">datetime endtime</param>
        /// <param name="LEARNINGSESSION_GUID">string LEARNINGSESSION_GUID</param>
        /// <param name="ASSESSMENTTYPE">string ASSESSMENTTYPE</param>
        /// <param name="RAWSCORE">int RAWSCORE</param>
        /// <returns>true if successfull,else false</returns>
        public bool UpdateLearningSessionEndTimeLegacyTesting(DateTime endTime, string LEARNINGSESSION_GUID, string ASSESSMENTTYPE, int RAWSCORE)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNINGSESSION_ENDTIME_LEGACY_TESTING);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, LEARNINGSESSION_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, ASSESSMENTTYPE);
                db.AddInParameter(dbCommand, "@RAWSCORE", DbType.Int32, RAWSCORE);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }



        /// <summary>
        /// This method updates the answer of learner validation statistics
        /// </summary>
        /// <param name="answerText">string answer text</param>
        /// <returns>return true if successfull else false</returns>
        public bool UpdateValidationLearnerStatisticsAnswer(int learnerValidationStatisticsID, string answerText, bool isCorrect, bool isAnswered, DateTime saveTime)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the  answer of learner validation statistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNERVALIDATIONSTATISTICS_ANSWER);
                db.AddInParameter(dbCommand, "@ANSWERTEXT", DbType.String, answerText);
                db.AddInParameter(dbCommand, "@LEARNERVALIDATIONSTATISTICS_ID", DbType.Int32, learnerValidationStatisticsID);
                db.AddInParameter(dbCommand, "@CORRECTTF", DbType.Boolean, isCorrect);
                db.AddInParameter(dbCommand, "@ANSWEREDTF", DbType.Boolean, isAnswered);
                db.AddInParameter(dbCommand, "@SAVETIME", DbType.DateTime, saveTime);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }
        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>NonZero LearnerStatisticsID if successfull, else 0</returns>
        public int SaveLearnerStatistics(LearnerStatistics learnerStatistics)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNERSTATISTICS);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, learnerStatistics.Item_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, learnerStatistics.AssessmentType);
                db.AddInParameter(dbCommand, "@NUMOFCORRECT", DbType.Int32, learnerStatistics.NumberAnswersCorrect);
                db.AddInParameter(dbCommand, "@NUMOFINCORRECT", DbType.Int32, learnerStatistics.NumberAnswersIncorrect);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, learnerStatistics.AssessmentAttemptNumber);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.String, learnerStatistics.AssessmentItemID);
                db.AddInParameter(dbCommand, "@CORRECTTF", DbType.Boolean, learnerStatistics.CorrectTF);
                db.AddInParameter(dbCommand, "@SCENE_GUID", DbType.String, learnerStatistics.Scene_GUID);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, learnerStatistics.MaxAtemptActionTaken);
                db.AddInParameter(dbCommand, "@STATISTICDATE", DbType.DateTime, DateTime.Now);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;
            }
            return learnerStatisticsID;
        }
        /// <summary>
        /// This method saves the learner statistics for ICP4
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>NonZero LearnerStatisticsID if successfull, else 0</returns>
        public int SaveICP4LearnerStatistics(LearnerStatistics learnerStatistics)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_LEARNERSTATISTICS);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, learnerStatistics.Item_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, learnerStatistics.AssessmentType);
                db.AddInParameter(dbCommand, "@NUMOFCORRECT", DbType.Int32, learnerStatistics.NumberAnswersCorrect);
                db.AddInParameter(dbCommand, "@NUMOFINCORRECT", DbType.Int32, learnerStatistics.NumberAnswersIncorrect);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, learnerStatistics.AssessmentAttemptNumber);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.String, learnerStatistics.AssessmentItemID);
                db.AddInParameter(dbCommand, "@CORRECTTF", DbType.Boolean, learnerStatistics.CorrectTF);
                db.AddInParameter(dbCommand, "@SCENE_GUID", DbType.String, learnerStatistics.Scene_GUID);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, learnerStatistics.MaxAtemptActionTaken);
                db.AddInParameter(dbCommand, "@STATISTICDATE", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@REMEDIATIONCOUNT", DbType.Int32, learnerStatistics.RemediationCount);
                db.AddInParameter(dbCommand, "@WEIGHTEDSCORE", DbType.Double, learnerStatistics.RawScore);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);


                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;
            }
            return learnerStatisticsID;
        }

        public int SaveAssessmentScore(LearnerStatistics learnerStatistics, int mastoryScore)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_SAVEASSESSMENTSCORE);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, learnerStatistics.LearnerEnrollment_ID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, learnerStatistics.Item_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, learnerStatistics.AssessmentType);
                db.AddInParameter(dbCommand, "@NUMOFCORRECT", DbType.Int32, learnerStatistics.NumberAnswersCorrect);
                db.AddInParameter(dbCommand, "@NUMOFINCORRECT", DbType.Int32, learnerStatistics.NumberAnswersIncorrect);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, learnerStatistics.AssessmentAttemptNumber);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, learnerStatistics.MaxAtemptActionTaken);
                db.AddInParameter(dbCommand, "@STATISTICDATE", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@REMEDIATIONCOUNT", DbType.Int32, learnerStatistics.RemediationCount);
                if (learnerStatistics.RawScore == -2)
                    db.AddInParameter(dbCommand, "@WEIGHTEDSCORE", DbType.Double, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "@WEIGHTEDSCORE", DbType.Double, learnerStatistics.RawScore);
                db.AddInParameter(dbCommand, "@ISPASS", DbType.Double, learnerStatistics.IsPass);
                db.AddInParameter(dbCommand, "@MASTERYSCORE", DbType.Double, mastoryScore);
                db.AddInParameter(dbCommand, "@REPEATEDASSESSMENTATTEMPT", DbType.Boolean, learnerStatistics.IsRepeatedAssessmentAttempt);

                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);


                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;

                string mailBody = CreateBody(learnerStatistics, mastoryScore);
                SendEmail(System.Configuration.ConfigurationSettings.AppSettings["ToEmailAddressSaveAssessmentScore"], System.Configuration.ConfigurationSettings.AppSettings["FromEmailAddressSaveAssessmentScore"], "Information for Exception in SaveAssessmentScore", mailBody);
                
            }
            return learnerStatisticsID;
        }

        public int SaveAssessmentScore_Game(LearnerStatistics learnerStatistics, int mastoryScore)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_SAVEASSESSMENTSCORE_GAME);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, learnerStatistics.LearnerEnrollment_ID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, learnerStatistics.Item_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, learnerStatistics.AssessmentType);
                db.AddInParameter(dbCommand, "@NUMOFCORRECT", DbType.Int32, learnerStatistics.NumberAnswersCorrect);
                db.AddInParameter(dbCommand, "@NUMOFINCORRECT", DbType.Int32, learnerStatistics.NumberAnswersIncorrect);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, learnerStatistics.AssessmentAttemptNumber);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, learnerStatistics.MaxAtemptActionTaken);
                db.AddInParameter(dbCommand, "@STATISTICDATE", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@REMEDIATIONCOUNT", DbType.Int32, learnerStatistics.RemediationCount);
                db.AddInParameter(dbCommand, "@WEIGHTEDSCORE", DbType.Double, learnerStatistics.RawScore);
                db.AddInParameter(dbCommand, "@ISPASS", DbType.Double, learnerStatistics.IsPass);
                db.AddInParameter(dbCommand, "@MASTERYSCORE", DbType.Double, mastoryScore);

                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);


                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;
            }
            return learnerStatisticsID;
        }

        public bool SaveAssessmentItem(LearnerStatistics learnerStatistics)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP saves the learnerstatistics


                StringBuilder answerGuids = new StringBuilder("");
                if (learnerStatistics.LearnerStatisticsAnswers == null || learnerStatistics.LearnerStatisticsAnswers.Count == 0)
                {
                    dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_SAVEASSESSMENTITEM);
                    db.AddInParameter(dbCommand, "@ANSWEREDCORRECTLY", DbType.Boolean, learnerStatistics.CorrectTF);
                    db.AddInParameter(dbCommand, "@ASSESSMENTITEMANSWERGUID", DbType.String, "");
                    db.AddInParameter(dbCommand, "@ANSWERPROVIDED", DbType.Int32, false);

                    db.AddInParameter(dbCommand, "@ASSESSMENTITEMGUID", DbType.String, learnerStatistics.AssessmentItemID);
                    db.AddInParameter(dbCommand, "@CORRECTASSESSMENTITEMANSWERGUID", DbType.String, learnerStatistics.CorrectAnswerGuids);
                    db.AddInParameter(dbCommand, "@LEARNERSTATISTIC_ID", DbType.String, learnerStatistics.LearnerStatisticsID);
                    db.AddInParameter(dbCommand, "@TESTID", DbType.Int32, learnerStatistics.TestID);
                    db.AddInParameter(dbCommand, "@ISASSESSMENTITEMTOOGLED", DbType.Boolean, learnerStatistics.IsAssessmentItemToogled);

                    bool isUpdated = db.ExecuteNonQuery(dbCommand) > 0;
                }
                else
                {


                    //-----------------------------------------------------------------

                    foreach (LearnerStatisticsAnswer assessmentItemAnswer in learnerStatistics.LearnerStatisticsAnswers)
                    {

                        if (answerGuids.ToString() != "")
                        {
                            answerGuids.Append(",");
                        }

                        //if (assessmentItemAnswer.Value != null && assessmentItemAnswer.Value != string.Empty)
                        // {
                        if (assessmentItemAnswer.AssessmentItemAnswerGUID != null && assessmentItemAnswer.AssessmentItemAnswerGUID != string.Empty)
                            answerGuids.Append(assessmentItemAnswer.AssessmentItemAnswerGUID);
                        else
                            answerGuids.Append(assessmentItemAnswer.Value);

                        // }
                    }
                    //-----------------------------------------------------------------



                    //foreach (LearnerStatisticsAnswer assessmentItemAnswer in learnerStatistics.LearnerStatisticsAnswers)
                    //{
                    //answerGuids.Append(assessmentItemAnswer.AssessmentItemAnswerGUID + ",");
                    //if(assessmentItemAnswer.Value !=null && assessmentItemAnswer.Value != string.Empty)
                    //{
                    //    answerGuids.Append(assessmentItemAnswer.Value + ",");
                    //}


                    dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_SAVEASSESSMENTITEM);
                    db.AddInParameter(dbCommand, "@ANSWEREDCORRECTLY", DbType.Boolean, learnerStatistics.CorrectTF);
                    //if (assessmentItemAnswer.Value != null && assessmentItemAnswer.Value != string.Empty)
                    //{
                    //    if (assessmentItemAnswer.AssessmentItemAnswerGUID != null && assessmentItemAnswer.AssessmentItemAnswerGUID != string.Empty)
                    //        assessmentItemAnswer.AssessmentItemAnswerGUID = assessmentItemAnswer.AssessmentItemAnswerGUID + "," + Convert.ToString(assessmentItemAnswer.Value);
                    //    else
                    //        assessmentItemAnswer.AssessmentItemAnswerGUID = assessmentItemAnswer.Value;

                    //}
                    //db.AddInParameter(dbCommand, "@ASSESSMENTITEMANSWERGUID", DbType.String, assessmentItemAnswer.AssessmentItemAnswerGUID);
                    db.AddInParameter(dbCommand, "@ASSESSMENTITEMANSWERGUID", DbType.String, answerGuids.ToString());


                    // Waqas Zakai
                    // LCMS-11066
                    // START

                    //if (learnerStatistics.LearnerStatisticsAnswers == null || learnerStatistics.LearnerStatisticsAnswers.Count == 0)
                    if (learnerStatistics.AnswerProvided)
                        db.AddInParameter(dbCommand, "@ANSWERPROVIDED", DbType.Int32, true);
                    else
                        db.AddInParameter(dbCommand, "@ANSWERPROVIDED", DbType.Int32, false);
                    // LCMS-11066
                    // END

                    db.AddInParameter(dbCommand, "@ASSESSMENTITEMGUID", DbType.String, learnerStatistics.AssessmentItemID);
                    db.AddInParameter(dbCommand, "@CORRECTASSESSMENTITEMANSWERGUID", DbType.String, learnerStatistics.CorrectAnswerGuids);
                    db.AddInParameter(dbCommand, "@LEARNERSTATISTIC_ID", DbType.String, learnerStatistics.LearnerStatisticsID);
                    db.AddInParameter(dbCommand, "@TESTID", DbType.Int32, learnerStatistics.TestID);
                    db.AddInParameter(dbCommand, "@ISASSESSMENTITEMTOOGLED", DbType.Boolean, learnerStatistics.IsAssessmentItemToogled);

                    bool isUpdated = db.ExecuteNonQuery(dbCommand) > 0;

                    //} // for
                }
                return true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
            return false;
        }

        public int SaveLearnerStatistics_Scene(LearnerStatistics learnerStatistics)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_INSERT_ICP4_LEARNERSTATISTICS_SCENE);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int64, learnerStatistics.LearnerEnrollment_ID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int64, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@SCENE_GUID", DbType.String, learnerStatistics.Scene_GUID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);



                if (db.ExecuteNonQuery(dbCommand) > 0 && dbCommand.Parameters["@NEWID"].Value != DBNull.Value)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;
            }
            return learnerStatisticsID;
        }


        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>NonZero LearnerStatisticsID if successfull, else 0</returns>
        public int SaveLearnerStatisticsWithStatisticDateTime(LearnerStatistics learnerStatistics, DateTime StatisticDateTime)
        {
            DbCommand dbCommand = null;
            int learnerStatisticsID = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNERSTATISTICS);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learnerStatistics.LearningSession_ID);
                db.AddInParameter(dbCommand, "@TIMEINSECONDS", DbType.Int32, learnerStatistics.TimeInSeconds);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, learnerStatistics.Statistic_Type);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, learnerStatistics.Item_GUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, learnerStatistics.AssessmentType);
                db.AddInParameter(dbCommand, "@NUMOFCORRECT", DbType.Int32, learnerStatistics.NumberAnswersCorrect);
                db.AddInParameter(dbCommand, "@NUMOFINCORRECT", DbType.Int32, learnerStatistics.NumberAnswersIncorrect);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, learnerStatistics.AssessmentAttemptNumber);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.String, learnerStatistics.AssessmentItemID);
                db.AddInParameter(dbCommand, "@CORRECTTF", DbType.Boolean, learnerStatistics.CorrectTF);
                db.AddInParameter(dbCommand, "@SCENE_GUID", DbType.String, learnerStatistics.Scene_GUID);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, learnerStatistics.MaxAtemptActionTaken);
                db.AddInParameter(dbCommand, "@STATISTICDATE", DbType.DateTime, StatisticDateTime);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                learnerStatisticsID = 0;
            }
            return learnerStatisticsID;
        }

        /// <summary>
        /// This method returns the learner course track info object
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        public LearnerCourseTrackInfo GetLearnerCoursetrackInfo(string sessionID)
        {
            DbCommand dbCommand = null;
            LearnerCourseTrackInfo learnerCourseTrackInfo = new LearnerCourseTrackInfo();
            try
            {
                //This SP returns the learncoursetrackinfoobject including the learnerid the courseid and the last scene_GUID 
                //of the last session of that user for that course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERCOURSETRACKEINFO);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, sessionID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerCourseTrackInfo.CourseID = dataReader["COURSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSE_ID"]);
                        learnerCourseTrackInfo.LearnerID = dataReader["LEARNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNER_ID"]);
                        learnerCourseTrackInfo.ItemGUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        learnerCourseTrackInfo.LearnerSessionID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                        learnerCourseTrackInfo.SceneGUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        learnerCourseTrackInfo.StatisticsType = dataReader["STATISTICSTYPE"] == DBNull.Value ? "" : dataReader["STATISTICSTYPE"].ToString();
                        learnerCourseTrackInfo.EnrollmentID = dataReader["ENROLLMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENROLLMENT_ID"]);
                        learnerCourseTrackInfo.RedirectURL = dataReader["REDIRECTURL"] == DBNull.Value ? "" : dataReader["REDIRECTURL"].ToString();
                        learnerCourseTrackInfo.TotalTimeSpent = dataReader["TOTALTIMESPENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TOTALTIMESPENT"]);
                        learnerCourseTrackInfo.TimeSpent = dataReader["TIMESPENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TIMESPENT"]);
                        learnerCourseTrackInfo.FirstAccessDateTime = dataReader["FISTACCESSDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dataReader["FISTACCESSDATE"]);
                        learnerCourseTrackInfo.RegAccessDateTime = dataReader["REGDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dataReader["REGDATE"]);
                        learnerCourseTrackInfo.LearningSessionStartDateTime = dataReader["LEARNINGSESSIONSTARTDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dataReader["LEARNINGSESSIONSTARTDATE"]);
                        learnerCourseTrackInfo.CourseApprovalID = dataReader["COURSEAPPROVALID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSEAPPROVALID"]);
                        learnerCourseTrackInfo.CourseConfigurationID = dataReader["COURSECONFIGURATIONID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSECONFIGURATIONID"]);
                        learnerCourseTrackInfo.IsLockedCourseDuringAssessment = dataReader["ISLOCKEDCOURSEDURINGASSESSMENT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISLOCKEDCOURSEDURINGASSESSMENT"]);
                        //Course Completion
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsCompleteAfterNOUniqueCourseVisitAchieved = dataReader["COMPLETEAFTERUNIQUECOUREVISITSTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETEAFTERUNIQUECOUREVISITSTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsembeddedAcknowledgmentAchieved = dataReader["COMPLETEDACKNOWLEDGEMENTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETEDACKNOWLEDGEMENTTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsViewEverySceneInCourseAchieved = dataReader["COMPLETEDALLSCENESTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETEDALLSCENESTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess = dataReader["COMPLETEWITHINTIMEFROMFIRSTACCESSTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETEWITHINTIMEFROMFIRSTACCESSTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate = dataReader["COMPLETEWITHINTIMEFROMREGISTRATIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETEWITHINTIMEFROMREGISTRATIONTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsCourseCompleted = dataReader["COURSECOMPLETETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COURSECOMPLETETF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsRespondToCourseEvaluationAchieved = dataReader["COURSEEVALUATIONCOMPLETETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COURSEEVALUATIONCOMPLETETF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsPostAssessmentAttempted = dataReader["POSTASSESSMENTATTEMPTEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["POSTASSESSMENTATTEMPTEDTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsPostAssessmentMasteryAchieved = dataReader["POSTASSESSMENTMASTERYACHIEVEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["POSTASSESSMENTMASTERYACHIEVEDTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsPreAssessmentMasteryAchieved = dataReader["PREASSESSMENTMASTERYACHIEVEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PREASSESSMENTMASTERYACHIEVEDTF"]);
                        learnerCourseTrackInfo.LearnerCourseCompletionStatus.IsQuizMasteryAchieved = dataReader["QUIZMASTERYACHIEVEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["QUIZMASTERYACHIEVEDTF"]);

                    }
                }
                return learnerCourseTrackInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public bool Insert_SetupEnrollment(long Enrollment_ID, long LearningSession_ID)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_INSERT_SETUPENROLLMENT);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int64, Enrollment_ID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int64, LearningSession_ID);
                db.ExecuteNonQuery(dbCommand);

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// This method returns the learner course track info objectfor the current session
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        public LearnerCourseTrackInfo GetCurrentLearnerCoursetrackInfo(string sessionID)
        {
            DbCommand dbCommand = null;
            LearnerCourseTrackInfo learnerCourseTrackInfo = new LearnerCourseTrackInfo();
            try
            {
                //This SP returns the learncoursetrackinfoobject including the learnerid the courseid and the last scene_GUID 
                //of the last session of that user for that course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CURRENT_LEARNERCOURSETRACKINFO);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, sessionID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerCourseTrackInfo.CourseID = dataReader["COURSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSE_ID"]);
                        learnerCourseTrackInfo.LearnerID = dataReader["LEARNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNER_ID"]);
                        learnerCourseTrackInfo.ItemGUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        learnerCourseTrackInfo.LearnerSessionID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                        learnerCourseTrackInfo.SceneGUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        learnerCourseTrackInfo.StatisticsType = dataReader["STATISTICSTYPE"] == DBNull.Value ? "" : dataReader["STATISTICSTYPE"].ToString();
                        learnerCourseTrackInfo.EnrollmentID = dataReader["ENROLLMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENROLLMENT_ID"]);
                        learnerCourseTrackInfo.RedirectURL = dataReader["REDIRECTURL"] == DBNull.Value ? "" : dataReader["REDIRECTURL"].ToString();
                    }
                }
                return learnerCourseTrackInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method saves the bookmark info 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="item_GUID">string itemGUID</param>
        /// <param name="flashSceneNo">string flashSceneNo</param>
        /// <param name="bookMarkTitle">string bookMarkTitle</param>
        /// <returns>boolean true if suucessfull,else false</returns>
        public bool SaveLearnerCourseBookmark(int courseID, int learnerID, int enrollmentID, string item_GUID, string sceneGUID, string flashSceneNo, string bookMarkTitle, string lastScene, bool isMovieEnded, bool nextButtonState, string firstSceneName, DateTime createddate)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP saves the learnercoursebookmarkinfo
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNERCOURSEBOOKMARK);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, item_GUID);
                db.AddInParameter(dbCommand, "@SCENE_GUID", DbType.String, sceneGUID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.String, enrollmentID);
                db.AddInParameter(dbCommand, "@FLASHSCENENO", DbType.String, flashSceneNo);
                db.AddInParameter(dbCommand, "@BOOKMARKTITLE", DbType.String, bookMarkTitle);
                db.AddInParameter(dbCommand, "@LASTSCENE", DbType.String, lastScene);
                db.AddInParameter(dbCommand, "@ISMOVIEENDED", DbType.Boolean, isMovieEnded);
                db.AddInParameter(dbCommand, "@NEXTBUTTONSTATE", DbType.Boolean, nextButtonState);
                db.AddInParameter(dbCommand, "@FIRSTSCENENAME", DbType.String, firstSceneName);
                db.AddInParameter(dbCommand, "@CREATEDDATE", DbType.DateTime, createddate);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        /// <summary>
        /// This method deleted the bookmark info 
        /// </summary>
        /// <param name="ID">int bookmarkID</param>        
        /// <returns>boolean true if suucessfull,else false</returns>
        public bool DeleteLearnerCourseBookmark(int bookmarkID)
        {
            DbCommand dbCommand = null;
            bool isDeleted = false;
            try
            {
                //This SP saves the learnercoursebookmarkinfo
                dbCommand = db.GetStoredProcCommand(StoredProcedures.DELETE_LEARNERCOURSEBOOKMARK);
                db.AddInParameter(dbCommand, "@ID", DbType.Int32, bookmarkID);                

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isDeleted = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isDeleted = false;
            }
            return isDeleted;
        }
        /// <summary>
        /// This method returns the bookmarkInfo object
        /// </summary>
        /// <param name="bookMarkID">int learnerCourseBookMarkID</param>
        /// <returns>LearnerCourseBookMarkInfo object</returns>
        public LearnerCourseBookMarkInfo GetLearnerCourseBookMarkInfo(int learnerCourseBookMarkID)
        {
            DbCommand dbCommand = null;
            LearnerCourseBookMarkInfo bookMarkInfo = new LearnerCourseBookMarkInfo();
            try
            {
                //This SP returns the bookMarkInfo object 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERCOURSEBOOKMARK);
                db.AddInParameter(dbCommand, "@ID", DbType.Int32, learnerCourseBookMarkID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        bookMarkInfo.BookMarkInfoID = learnerCourseBookMarkID;
                        bookMarkInfo.Item_GUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        bookMarkInfo.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        bookMarkInfo.LearnerID = dataReader["LEARNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNER_ID"]);
                        bookMarkInfo.CourseID = dataReader["COURSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSE_ID"]);
                        bookMarkInfo.FlashSceneNo = dataReader["FLASHSCENENO"] == DBNull.Value ? "" : dataReader["FLASHSCENENO"].ToString();
                        bookMarkInfo.BookMarkTitle = dataReader["BOOKMARKTITLE"] == DBNull.Value ? "" : dataReader["BOOKMARKTITLE"].ToString();
                        bookMarkInfo.LastScene = dataReader["LASTSCENE"] == DBNull.Value ? "" : dataReader["LASTSCENE"].ToString();
                        bookMarkInfo.IsMovieEnded = dataReader["ISMOVIEENDED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISMOVIEENDED"]);
                        bookMarkInfo.NextButtonState = dataReader["NEXTBUTTONSTATE"] == DBNull.Value ? true : Convert.ToBoolean(dataReader["NEXTBUTTONSTATE"]);
                        bookMarkInfo.FirstSceneName = dataReader["FIRSTSCENENAME"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["FIRSTSCENENAME"]);
                    }
                }
                return bookMarkInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns all LearnerCourseBookmarkInfo objects belonging to a particular course
        /// and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>BookMarkInfo object</returns>
        public List<LearnerCourseBookMarkInfo> GetAllLearnerCourseBookMarksInfo(int courseID, int learnerID, int enrollmentID)
        {
            DbCommand dbCommand = null;
            List<LearnerCourseBookMarkInfo> bookMarksInfo = new List<LearnerCourseBookMarkInfo>();
            LearnerCourseBookMarkInfo learnerCourseBookMarkInfo = null;
            try
            {
                //This SP returns the bookMarkInfo object 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERCOURSEBOOKMARKS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerCourseBookMarkInfo = new LearnerCourseBookMarkInfo();
                        learnerCourseBookMarkInfo.BookMarkInfoID = Convert.ToInt32(dataReader["ID"]);
                        learnerCourseBookMarkInfo.BookMarkTitle = dataReader["BOOKMARKTITLE"] == DBNull.Value ? "" : dataReader["BOOKMARKTITLE"].ToString();
                        learnerCourseBookMarkInfo.CreatedDate = dataReader["CREATED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["CREATED_DATE"]);
                        bookMarksInfo.Add(learnerCourseBookMarkInfo);
                    }
                }
                return bookMarksInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets all LeanerCourseTrackInfo records related to a a particular
        /// course and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        public List<LearnerCourseTrackInfo> GetAllLearnerCourseTrackInfo(int courseID, int learnerID, int enrollmentID)
        {
            DbCommand dbCommand = null;
            List<LearnerCourseTrackInfo> learnerCourseTrackInfos = new List<LearnerCourseTrackInfo>();
            try
            {
                //This SP returns the learncoursetrackinfoobject related to a particular course and larner
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ALL_LEARNERCOURSETRACKINFO);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.String, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.String, learnerID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.String, enrollmentID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerCourseTrackInfo learnerCourseTrackInfo;
                    while (dataReader.Read())
                    {
                        learnerCourseTrackInfo = new LearnerCourseTrackInfo();
                        learnerCourseTrackInfo.CourseID = courseID;
                        learnerCourseTrackInfo.LearnerID = learnerID;
                        learnerCourseTrackInfo.ItemGUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        learnerCourseTrackInfo.SceneGUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        learnerCourseTrackInfo.TimeSpent = dataReader["TIMESPENT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["TIMESPENT"]);
                        learnerCourseTrackInfos.Add(learnerCourseTrackInfo);
                    }
                }
                return learnerCourseTrackInfos;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the learnerstatistics records of the assessment 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="statisticsType">string statisticsType</param>
        /// <param name="assessmentType">string assessmentType</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsQuizAssessments(string learnerSessionGUID, string assessmentType, int remediationCount, int contentObjectId)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of questions of dfferent types of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_QUIZASSESSMENTS_NEW);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectId);
                db.AddInParameter(dbCommand, "@REMEDIATION_COUNT", DbType.Int32, remediationCount);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        //learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        //learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;
                        //learnerStatistic.TimeInSeconds = dataReader["TIMEINSECONDS"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["TIMEINSECONDS"]);
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.Question;
                        learnerStatistic.AssessmentType = assessmentType;//dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();
                        //learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        //learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPTNUMBER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPTNUMBER"]);
                        learnerStatistic.AssessmentItemID = dataReader["ASSESSMENTITEMGUID"] == DBNull.Value ? string.Empty : dataReader["ASSESSMENTITEMGUID"].ToString();
                        //learnerStatistic.Item_GUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        //learnerStatistic.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        learnerStatistic.CorrectTF = dataReader["ANSWEREDCORRECTLY"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSWEREDCORRECTLY"]);


                        // LCMS-9213
                        //---------------------------------------------------------------
                        learnerStatistic.AnswerProvided = dataReader["ANSWERPROVIDED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSWERPROVIDED"]);
                        learnerStatistic.QuestionType = dataReader["QUESTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["QUESTIONTYPE"]);
                        learnerStatistic.AnswerTexts = dataReader["ASSESSMENTITEMANSWERGUID"] == DBNull.Value ? "" : Convert.ToString(dataReader["ASSESSMENTITEMANSWERGUID"]);
                        learnerStatistic.CorrectAnswerGuids = dataReader["CORRECTASSESSMENTITEMANSWERGUID"] == DBNull.Value ? "" : Convert.ToString(dataReader["CORRECTASSESSMENTITEMANSWERGUID"]);
                        learnerStatistic.TimeInSeconds = dataReader["TOTALTIMESPENTINSECONDSASSESSMENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TOTALTIMESPENTINSECONDSASSESSMENT"]);
                        //---------------------------------------------------------------

                        //Added By Abdus Samad
                        //LCMS-12105
                        //START                  
                        learnerStatistic.IsAssessmentItemToogled = dataReader["ISASSESSMENTITEMTOOGLED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISASSESSMENTITEMTOOGLED"]);
                        //STOP


                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the learnerstatistics records of the assessment 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="statisticsType">string statisticsType</param>
        /// <param name="assessmentType">string assessmentType</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsAssessments(string learnerSessionGUID, string assessmentType, int remediationCount, int examID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of questions of dfferent types of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERSTATISTICS_ASSESSMENTS);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);
                //db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);
                db.AddInParameter(dbCommand, "@REMEDIATION_COUNT", DbType.Int32, remediationCount);
                db.AddInParameter(dbCommand, "@EXAMID", DbType.Int32, examID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        //learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        //learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;
                        //learnerStatistic.TimeInSeconds = dataReader["TIMEINSECONDS"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["TIMEINSECONDS"]);
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.Question;
                        learnerStatistic.AssessmentType = assessmentType;//dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();
                        //learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        //learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPTNUMBER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPTNUMBER"]);
                        learnerStatistic.AssessmentItemID = dataReader["ASSESSMENTITEMGUID"] == DBNull.Value ? string.Empty : dataReader["ASSESSMENTITEMGUID"].ToString();
                        //learnerStatistic.Item_GUID = dataReader["ITEM_GUID"] == DBNull.Value ? "" : dataReader["ITEM_GUID"].ToString();
                        //learnerStatistic.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        learnerStatistic.CorrectTF = dataReader["ANSWEREDCORRECTLY"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSWEREDCORRECTLY"]);


                        // LCMS-9213
                        //---------------------------------------------------------------
                        learnerStatistic.AnswerProvided = dataReader["ANSWERPROVIDED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSWERPROVIDED"]);
                        learnerStatistic.QuestionType = dataReader["QUESTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["QUESTIONTYPE"]);
                        learnerStatistic.AnswerTexts = dataReader["ASSESSMENTITEMANSWERGUID"] == DBNull.Value ? "" : Convert.ToString(dataReader["ASSESSMENTITEMANSWERGUID"]);
                        learnerStatistic.CorrectAnswerGuids = dataReader["CORRECTASSESSMENTITEMANSWERGUID"] == DBNull.Value ? "" : Convert.ToString(dataReader["CORRECTASSESSMENTITEMANSWERGUID"]);
                        learnerStatistic.TimeInSeconds = dataReader["TOTALTIMESPENTINSECONDSASSESSMENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TOTALTIMESPENTINSECONDSASSESSMENT"]);
                        //---------------------------------------------------------------

                        //Added By Abdus Samad
                        //LCMS-12105
                        //START                  
                        learnerStatistic.IsAssessmentItemToogled = dataReader["ISASSESSMENTITEMTOOGLED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISASSESSMENTITEMTOOGLED"]);
                        //STOP

                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the validation statistics of a particular enrollment
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <returns>list of LearnerValidationStatistics</returns>
		public List<LearnerValidationStatistics> GetCurrentLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, ref int minutesSinceLastValidation)
        {
            DbCommand dbCommand = null;
            DbCommand minDbCommand = null;
            List<LearnerValidationStatistics> learnerValidationStatistics = new List<LearnerValidationStatistics>();

            try
            {
                //Select all learner validation statistics records of
                // a particular enrollment of a learner
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERENROLLMENT_VALIDATIONSTATISTICS);
                db.AddInParameter(dbCommand, "@ENROLMENT_ID", DbType.Int32, enrollmentID);
			
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerValidationStatistics learnerValidaitonStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerValidaitonStatistic = new LearnerValidationStatistics();
                        learnerValidaitonStatistic.LearnerValidationStatisticsId = Convert.ToInt32(dataReader["ID"]);
                        learnerValidaitonStatistic.AnswerText = dataReader["ANSWER_TEXT"] == DBNull.Value ? string.Empty : dataReader["ANSWER_TEXT"].ToString();
                        learnerValidaitonStatistic.QuestionID = dataReader["QUESTION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["QUESTION_ID"]);
                        learnerValidaitonStatistic.IsCorrect = dataReader["CORRECTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CORRECTTF"]);
                        learnerValidaitonStatistic.SaveTime = dataReader["SAVETIME"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["SAVETIME"]);
                        learnerValidaitonStatistic.IsAnswered = dataReader["ANSWEREDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSWEREDTF"]);
                        learnerValidaitonStatistic.EnrollmentID = enrollmentID;
                        learnerValidationStatistics.Add(learnerValidaitonStatistic);
                    }
                }
                minDbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERENROLLMENT_TIMESINCELASTVALIDATIONQUESTION);
                db.AddInParameter(minDbCommand, "@ENROLMENT_ID", DbType.Int32, enrollmentID);
                db.AddInParameter(minDbCommand, "@LEARNER_ID", DbType.Int32, learnerId);
                db.AddInParameter(minDbCommand, "@COURSE_ID", DbType.Int32, courseId);
                db.AddInParameter(minDbCommand, "@SOURCE", DbType.Int32, source);
                using (IDataReader dataReader = db.ExecuteReader(minDbCommand))
                {
                    while (dataReader.Read())
                    {
                        minutesSinceLastValidation = dataReader["SECONDS"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SECONDS"]);
                    }
                }
                return learnerValidationStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                minutesSinceLastValidation = 0;
                return null;
            }
        }
        /// <summary>
        /// This method returns the postassessmentAttemp no.
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetLearnerStatisticsPostAssessmentAttemptNo(string learnerSessionGUID)
        {
            DbCommand dbCommand = null;
            int assessmentAttemptNo = 0;
            try
            {
                //This SP returns the asessmenr attemp no of the specifed assessmentType
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERSTATISTICS_POSTASSESSMENTATTEMPTNO);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PostAssessment);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, LearnerStatisticsType.PostAssessmentEnd);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assessmentAttemptNo = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                    }
                }
                return assessmentAttemptNo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method returns the preassessmentAttemp no.
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetLearnerStatisticsPreAssessmentAttemptNo(string learnerSessionGUID)
        {
            DbCommand dbCommand = null;
            int assessmentAttemptNo = 0;
            try
            {
                //This SP returns the asessmenr attemp no of the specifed assessmentType
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERSTATISTICS_PREASSESSMENTATTEMPTNO);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PreAssessment);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, LearnerStatisticsType.PreAssessmentEnd);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assessmentAttemptNo = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                    }
                }
                return assessmentAttemptNo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method returns the assessmentAttemp noof the specified assessment type 
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetLearnerStatisticsQuizAttemptNo(string learnerSessionGUID, string contentObjectGUID)
        {
            DbCommand dbCommand = null;
            int assessmentAttemptNo = 0;
            try
            {
                //This SP returns the quiz attemp no of the specifed contentObject
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERSTATISTICS_QUIZATTEMPTNO);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.Quiz);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, LearnerStatisticsType.QuizEnd);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.String, contentObjectGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assessmentAttemptNo = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                    }
                }
                return assessmentAttemptNo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the preassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsPreAssessmentResults(string learnerSessionGUID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of pre assessment of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_PREPOSTASSESSMENTRESULT_OPTIMIZED);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PreAssessment);
                //db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PreAssessment);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.PreAssessmentEnd;
                        learnerStatistic.AssessmentType = LearnerStatisticsType.PreAssessment;
                        learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistic.RemediationCount = dataReader["REMEDIATIONCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["REMEDIATIONCOUNT"]);
                        learnerStatistic.RawScore = dataReader["RAWSCORE"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["RAWSCORE"]);
                        learnerStatistic.IsPass = dataReader["PASSEDASSESSMENT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PASSEDASSESSMENT"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the postassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsPostAssessmentResults(string learnerSessionGUID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of pre assessment of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_PREPOSTASSESSMENTRESULT_OPTIMIZED);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PostAssessment);
                //db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PostAssessment);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.PostAssessmentEnd;
                        learnerStatistic.AssessmentType = LearnerStatisticsType.PostAssessment;
                        learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistic.RemediationCount = dataReader["REMEDIATIONCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["REMEDIATIONCOUNT"]);
                        learnerStatistic.RawScore = dataReader["RAWSCORE"] == DBNull.Value ? -2 : Convert.ToDouble(dataReader["RAWSCORE"]);
                        learnerStatistic.IsPass = dataReader["PASSEDASSESSMENT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PASSEDASSESSMENT"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the quizresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsQuizResults(string learnerSessionGUID, string contentObjectID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of pre assessment of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_QUIZRESULT_OPTIMIZED);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.Quiz);
                //db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.Quiz);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_GUID", DbType.String, contentObjectID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.QuizEnd;
                        learnerStatistic.AssessmentType = LearnerStatisticsType.Quiz;
                        learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistic.RemediationCount = dataReader["REMEDIATIONCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["REMEDIATIONCOUNT"]);
                        learnerStatistic.RawScore = dataReader["RAWSCORE"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["RAWSCORE"]);
                        learnerStatistic.IsPass = dataReader["PASSEDASSESSMENT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PASSEDASSESSMENT"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the learnerstatistics records of the Practice Exam Result
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetLearnerStatisticsPracticeExamResults(string learnerSessionGUID, string contentObjectID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            try
            {
                //This SP selects all learner statistics records of pre assessment of 
                //a particular learner and course on the session passed as parameter
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_QUIZRESULT_OPTIMIZED);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PracticeExam);
                //db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, LearnerStatisticsType.PracticeExam);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_GUID", DbType.String, contentObjectID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    LearnerStatistics learnerStatistic = null;
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        learnerStatistic.LearnerStatisticsID = Convert.ToInt32(dataReader["ID"]);
                        learnerStatistic.LearningSession_ID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNINGSESSION_ID"]); ;

                        // Fix for LCMS-7826
                        //------------------------------------------------
                        //learnerStatistic.Statistic_Type = LearnerStatisticsType.QuizEnd;
                        //learnerStatistic.AssessmentType = LearnerStatisticsType.Quiz;
                        learnerStatistic.Statistic_Type = LearnerStatisticsType.PracticeExamEnd;
                        learnerStatistic.AssessmentType = LearnerStatisticsType.PracticeExam;
                        //------------------------------------------------

                        learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                        learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? -1 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                        learnerStatistic.AssessmentAttemptNumber = dataReader["ASSESSMENTATTEMPT_NO"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSESSMENTATTEMPT_NO"]);
                        //learnerStatistic.MaxAtemptActionTaken = dataReader["MAXATTEMPTREACHACTIONTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MAXATTEMPTREACHACTIONTF"]);
                        learnerStatistic.RemediationCount = dataReader["REMEDIATIONCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["REMEDIATIONCOUNT"]);
                        learnerStatistic.RawScore = dataReader["RAWSCORE"] == DBNull.Value ? 0 : Convert.ToDouble(dataReader["RAWSCORE"]);
                        learnerStatistic.IsPass = dataReader["PASSEDASSESSMENT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PASSEDASSESSMENT"]);
                        learnerStatistics.Add(learnerStatistic);
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method saves the learnerStatisticsAnswer
        /// </summary>
        /// <param name="learnerStatisticsAnswer">object learnerStatisticsAnswer</param>
        /// <returns>true if successfull else false</returns>
        public bool SaveLearnerStatisticsAnswer(LearnerStatisticsAnswer learnerStatisticsAnswer)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP saves the learnerstatistics answer
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNERSTATISTICSANSWER);
                db.AddInParameter(dbCommand, "@LEARNERSTATISTICS_ID", DbType.Int32, learnerStatisticsAnswer.LearnerStatisticsID);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEMANSWER_GUID", DbType.String, learnerStatisticsAnswer.AssessmentItemAnswerGUID);
                db.AddInParameter(dbCommand, "@VALUE", DbType.String, learnerStatisticsAnswer.Value);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }
        /// <summary>
        /// This method inserts new record in lock unlock course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="date">datetime date</param>
        /// <param name="islock">bool isLock</param>
        /// <returns>boolean true if successfull,else false</returns>
        public bool SaveLockedCourse(int courseID, int learnerID, int enrollmentID, DateTime date, string lockType, bool islock)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP saves the course locked record
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LOCKEDCOURSE);
                db.AddInParameter(dbCommand, "@COURSELOCKED", DbType.Boolean, islock);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@DATE", DbType.DateTime, date);
                db.AddInParameter(dbCommand, "@LOCKTYPE", DbType.String, lockType);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        public bool LockCourseDuringAssessment(int courseID, int enrollmentID, DateTime date, string lockType, bool islock)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP saves the course locked During Assessment records
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LOCKEDCOURSEDURINGASSESSMENT);
                db.AddInParameter(dbCommand, "@COURSELOCKED", DbType.Boolean, islock);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@DATE", DbType.DateTime, date);
                db.AddInParameter(dbCommand, "@LOCKTYPE", DbType.String, lockType);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        public bool UpdateCourseStatusDuringAssessment(int courseID, int enrollmentID)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP upadtes the course status During Assessment 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LOCKEDCOURSEDURINGASSESSMENT);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        /// <summary>
        /// This method returns the course locked status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>boolean true if locked,false if not and if exception is thrown</returns>
        public bool GetLockedCourseCourseLocked(int enrollmentID, ref string lockType)
        {
            DbCommand dbCommand = null;
            bool isLocked = false;
            try
            {
                //This SP returns the quiz attemp no of the specifed contentObject
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LOCKEDCOURSE_COURSELOCKED);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isLocked = dataReader["COURSELOCKED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COURSELOCKED"]);
                        lockType = dataReader["LOCKTYPE"] == DBNull.Value ? string.Empty : dataReader["LOCKTYPE"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isLocked = false;
            }
            return isLocked;
        }
        /// <summary>
        /// This method updates teh learnerstatistics assessment end record's 
        /// </summary>
        /// <param name="learnerStatisticsID"></param>
        /// <returns></returns>
        public bool UpdateLearnerStatisticsMaximumAttemptAction(long learnerStatisticsID, bool maxAttemptActionTaken)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                //This SP updates the learningsession endtime
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNERSTATISTICS_MAXATTEMPTREACHACTIONTF);
                db.AddInParameter(dbCommand, "@ID", DbType.Int32, learnerStatisticsID);
                db.AddInParameter(dbCommand, "@MAXATTEMPTREACHACTIONTF", DbType.Boolean, maxAttemptActionTaken);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }
        /// <summary>
        /// This method gets the course completion policies and also gets the required data
        /// for the application of policies
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseSceneCount">int courseSceneCount</param>
        /// <returns>object CourseConfiguration with completion policies filled from data source</returns>
        public CourseConfiguration GetCourseCompletionConfiguration(int courseID)
        {
            DbCommand dbCommand = null;
            //courseSceneCount = 0;
            CourseConfiguration courseConfiguration = new CourseConfiguration();
            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_COURSECOMPLETIONCONFIGURATION);
                db.AddOutParameter(dbCommand, "@COMPLETION_POSTASSESSMENTATTEMPTED", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_POSTASSESSMENTMASTERY", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_PREASSESSMENTMASTERY", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_QUIZMASTERY", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_VIEWEVERYSCENEINCOURSE", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT", DbType.Int32, 4);
                //db.AddOutParameter(dbCommand, "@COURSESCENECOUNT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@POSTASSESSMENTMASTERY", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PREASSESSMENTMASTERY", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@QUIZMASTERY", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEMINUTE", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COMPLETION_UNITOFMUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIME", DbType.String, 50);
                db.AddOutParameter(dbCommand, "@COMPLETION_RESPONDTOCOURSEEVALUATION", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEDAT_REG", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COMPLETION_EMBEDDEDACKNOWLEDGEMENTENABLED", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETION_POSTASSESSMENTSCORETYPE", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@COMPLETION_PREASSESSMENTSCORETYPE", DbType.String, 100);
                db.AddOutParameter(dbCommand, "@COMPLETION_QUIZASSESSMENTSCORETYPE", DbType.String, 100);

                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                db.ExecuteNonQuery(dbCommand);
                courseConfiguration.CompletionPostAssessmentAttempted = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_POSTASSESSMENTATTEMPTED"].Value);
                courseConfiguration.CompletionPostAssessmentMastery = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_POSTASSESSMENTMASTERY"].Value);
                courseConfiguration.CompletionPreAssessmentMastery = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_PREASSESSMENTMASTERY"].Value);
                courseConfiguration.CompletionQuizMastery = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_QUIZMASTERY"].Value);
                courseConfiguration.CompletionViewEverySceneInCourse = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_VIEWEVERYSCENEINCOURSE"].Value);
                courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit = Convert.ToInt32(dbCommand.Parameters["@COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT"].Value);
                courseConfiguration.PostAssessmentConfiguration.MasteryScore = Convert.ToInt32(dbCommand.Parameters["@POSTASSESSMENTMASTERY"].Value);
                courseConfiguration.PreAssessmentConfiguration.MasteryScore = Convert.ToInt32(dbCommand.Parameters["@PREASSESSMENTMASTERY"].Value);
                courseConfiguration.QuizConfiguration.MasteryScore = Convert.ToInt32(dbCommand.Parameters["@QUIZMASTERY"].Value);
                courseConfiguration.CompletionRespondToCourseEvaluation = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_RESPONDTOCOURSEEVALUATION"].Value);
                //courseSceneCount = Convert.ToInt32(dbCommand.Parameters["@COURSESCENECOUNT"].Value);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute = Convert.ToInt32(dbCommand.Parameters["@COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEMINUTE"].Value);
                courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime = Convert.ToString(dbCommand.Parameters["@COMPLETION_UNITOFMUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIME"].Value);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay = Convert.ToInt32(dbCommand.Parameters["@COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEDAT_REG"].Value);
                courseConfiguration.EmbeddedAcknowledgmentEnabled = Convert.ToBoolean(dbCommand.Parameters["@COMPLETION_EMBEDDEDACKNOWLEDGEMENTENABLED"].Value);
                courseConfiguration.PostAssessmentConfiguration.ScoreType = Convert.ToString(dbCommand.Parameters["@COMPLETION_POSTASSESSMENTSCORETYPE"].Value);
                courseConfiguration.PreAssessmentConfiguration.ScoreType = Convert.ToString(dbCommand.Parameters["@COMPLETION_PREASSESSMENTSCORETYPE"].Value);
                courseConfiguration.QuizConfiguration.ScoreType = Convert.ToString(dbCommand.Parameters["@COMPLETION_QUIZASSESSMENTSCORETYPE"].Value);


            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
            return courseConfiguration;
        }
        /// <summary>
        /// This method gets the course completion data
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="learnerID"></param>
        /// <param name="courseConfiguration"></param>
        /// <param name="postAssessmentNoOfCorrect"></param>
        /// <param name="postAssessmentNoOfInCorrect"></param>
        /// <param name="preAssessmentNoOfCorrect"></param>
        /// <param name="preAssessmentNoOfInCorrect"></param>
        /// <param name="postAssessmentAttempted"></param>
        /// <param name="visitedSceneCount"></param>
        /// <param name="courseVisitCount"></param>
        /// <returns></returns>
        public List<LearnerStatistics> GetCourseCompletionData(int courseID, int learnerID, int enrollmentID
                                                               , CourseConfiguration courseConfiguration
                                                               , out int postAssessmentNoOfCorrect, out int postAssessmentNoOfInCorrect
                                                                , out int preAssessmentNoOfCorrect, out int preAssessmentNoOfInCorrect
                                                                , out bool postAssessmentAttempted
                                                                , out int visitedSceneCount
                                                                , out int courseVisitCount
                                                                , out DateTime firstVisitDateTime
                                                                , out bool hasRespondedToCourseEvaluation
                                                                )
        {
            DbCommand dbCommand = null;
            postAssessmentNoOfCorrect = 0;
            postAssessmentNoOfInCorrect = 0;
            preAssessmentNoOfCorrect = 0;
            preAssessmentNoOfInCorrect = 0;
            postAssessmentAttempted = false;
            visitedSceneCount = 0;
            courseVisitCount = 0;
            firstVisitDateTime = DateTime.Now;
            hasRespondedToCourseEvaluation = false;

            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            LearnerStatistics learnerStatistic = null;
            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERSTATISTICS_COURSECOMPLETIONDATA);
                db.AddInParameter(dbCommand, "@COMPLETION_POSTASSESSMENTATTEMPTED", DbType.Boolean, courseConfiguration.CompletionPostAssessmentAttempted);
                db.AddInParameter(dbCommand, "@COMPLETION_POSTASSESSMENTMASTERY", DbType.Boolean, courseConfiguration.CompletionPostAssessmentMastery);
                db.AddInParameter(dbCommand, "@COMPLETION_PREASSESSMENTMASTERY", DbType.Boolean, courseConfiguration.CompletionPreAssessmentMastery);
                db.AddInParameter(dbCommand, "@COMPLETION_QUIZMASTERY", DbType.Boolean, courseConfiguration.CompletionQuizMastery);
                db.AddInParameter(dbCommand, "@COMPLETION_VIEWEVERYSCENEINCOURSE", DbType.Boolean, courseConfiguration.CompletionViewEverySceneInCourse);
                db.AddInParameter(dbCommand, "@COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT", DbType.Int32, courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit);
                db.AddInParameter(dbCommand, "@COMPLETION_RESPONDTOCOURSEEVALUATION", DbType.Boolean, courseConfiguration.CompletionRespondToCourseEvaluation);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
                db.AddOutParameter(dbCommand, "@POSTASSESSMENT_NUMOFCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@POSTASSESSMENT_NUMOFINCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PREASSESSMENT_NUMOFCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PREASSESSMENT_NUMOFINCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@POSTASSESSMENTATTEMPTED", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@VISITEDSCENECOUNT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSEVISITCOUNT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@FIRSTACCESSDATETIME", DbType.DateTime, 50);
                db.AddOutParameter(dbCommand, "@HASRESPONDEDTOCOURSEEVALUATION", DbType.Boolean, 1);

                //This is a fix for execute reader in case of output parameters  
                DataSet dataSet = db.ExecuteDataSet(dbCommand);
                DataTable dataTable = null;
                if (dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
                postAssessmentNoOfCorrect = Convert.ToInt32(dbCommand.Parameters["@POSTASSESSMENT_NUMOFCORRECT"].Value);
                postAssessmentNoOfInCorrect = Convert.ToInt32(dbCommand.Parameters["@POSTASSESSMENT_NUMOFINCORRECT"].Value);
                preAssessmentNoOfCorrect = Convert.ToInt32(dbCommand.Parameters["@PREASSESSMENT_NUMOFCORRECT"].Value);
                preAssessmentNoOfInCorrect = Convert.ToInt32(dbCommand.Parameters["@PREASSESSMENT_NUMOFINCORRECT"].Value);
                postAssessmentAttempted = Convert.ToBoolean(dbCommand.Parameters["@POSTASSESSMENTATTEMPTED"].Value);
                visitedSceneCount = Convert.ToInt32(dbCommand.Parameters["@VISITEDSCENECOUNT"].Value);
                courseVisitCount = Convert.ToInt32(dbCommand.Parameters["@COURSEVISITCOUNT"].Value);
                firstVisitDateTime = Convert.ToDateTime(dbCommand.Parameters["@FIRSTACCESSDATETIME"].Value);
                hasRespondedToCourseEvaluation = Convert.ToBoolean(dbCommand.Parameters["@HASRESPONDEDTOCOURSEEVALUATION"].Value);

                if (dataSet.Tables.Count > 0)
                {
                    using (IDataReader dataReader = dataTable.CreateDataReader())
                    {
                        while (dataReader.Read())
                        {
                            learnerStatistic = new LearnerStatistics();
                            learnerStatistic.NumberAnswersCorrect = dataReader["NUMOFCORRECT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NUMOFCORRECT"]);
                            learnerStatistic.NumberAnswersIncorrect = dataReader["NUMOFINCORRECT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NUMOFINCORRECT"]);
                            learnerStatistics.Add(learnerStatistic);
                        }
                    }
                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        public void GetCourseCompletionData_Optimized(int courseID, long learnerID, long enrollmentID
                                                                , CourseCompletionPolicy courseCompletionPolicy
                                                                , out bool postAssessment_Passed
                                                                , out bool preAssessment_Passed
                                                                , out bool postAssessmentAttempted
                                                                , out int visitedScenePercent
                                                                , out int courseVisitCount
                                                                , out DateTime firstVisitDateTime
                                                                , out bool hasRespondedToCourseEvaluation
                                                                , out int quiz_Count_Passed
                                                                , out int quiz_Count_Required
                                                                , out bool embedded_Acknowledgement_Required_And_Agreed
                                                                , out bool quiz_PassedTF
                                                                , out bool viewEverySceneInCourseTF
                                                                , out bool completeAfterUniqueVisitTF
                                                                , out bool completeAfterRegDateTF
                                                                , out bool completeAfterFirstAccessDateTF
                                                                , out bool docusignAffidavit_Marked
                                                                , out bool docusignSigned_Marked
                                                                , out bool docusignDeclined_Marked 
                                                                , int source)
        {
            DbCommand dbCommand = null;
            postAssessment_Passed = false;
            preAssessment_Passed = false;
            postAssessmentAttempted = false;
            courseVisitCount = 0;
            visitedScenePercent = 0;
            firstVisitDateTime = DateTime.Now;
            hasRespondedToCourseEvaluation = false;
            quiz_Count_Passed = 0;
            quiz_Count_Required = 0;
            embedded_Acknowledgement_Required_And_Agreed = false;

            quiz_PassedTF = false;
            viewEverySceneInCourseTF = false;
            completeAfterUniqueVisitTF = false;
            completeAfterRegDateTF = false;
            completeAfterFirstAccessDateTF = false;

            //Abdus Samad LCMS-11888
            //START
            docusignAffidavit_Marked = false;
            docusignSigned_Marked = false;
            //END

            docusignDeclined_Marked = false;
            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_SELECT_LEARNERSTATISTICS_COURSECOMPLETIONDATA_OPTIMIZED_NEW);
                db.AddInParameter(dbCommand, "@COMPLETION_POSTASSESSMENTATTEMPTED", DbType.Boolean, courseCompletionPolicy.PostAssessmentAttempted);
                db.AddInParameter(dbCommand, "@COMPLETION_POSTASSESSMENTMASTERY", DbType.Boolean, courseCompletionPolicy.PostAssessmentMasteryAchived);
                db.AddInParameter(dbCommand, "@COMPLETION_PREASSESSMENTMASTERY", DbType.Boolean, courseCompletionPolicy.PreAssessmentMasteryAchived);
                db.AddInParameter(dbCommand, "@COMPLETION_QUIZMASTERY", DbType.Boolean, courseCompletionPolicy.QuizMasteryAchived);
                db.AddInParameter(dbCommand, "@COMPLETION_VIEWEVERYSCENEINCOURSE", DbType.Boolean, courseCompletionPolicy.ViewEverySceneInCourse);
                db.AddInParameter(dbCommand, "@COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT", DbType.Int32, courseCompletionPolicy.CompleteAfterNumberOfUniqueVisits);
                db.AddInParameter(dbCommand, "@COMPLETION_RESPONDTOCOURSEEVALUATION", DbType.Boolean, courseCompletionPolicy.RespondToCourseEvaluation);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
                db.AddInParameter(dbCommand, "@COMPLETION_EMBEDDEDACKNOWLEDGEMENTENABLED", DbType.Boolean, courseCompletionPolicy.EnableEmbeddedAknowledgement);
                db.AddInParameter(dbCommand, "@SOURCE", DbType.Int32, source); // Added by Abdus Samad For LCMS-13016 

                db.AddOutParameter(dbCommand, "@POSTASSESSMENT_PASSED", DbType.Boolean, 1);
                //db.AddOutParameter(dbCommand, "@POSTASSESSMENT_NUMOFINCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@PREASSESSMENT_PASSED", DbType.Boolean, 1);
                //db.AddOutParameter(dbCommand, "@PREASSESSMENT_NUMOFINCORRECT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@POSTASSESSMENTATTEMPTED", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@VISITEDSCENE_PERCENT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSEVISITCOUNT", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@FIRSTACCESSDATETIME", DbType.DateTime, 50);
                db.AddOutParameter(dbCommand, "@HASRESPONDEDTOCOURSEEVALUATION", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@QUIZ_COUNT_PASSED", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@QUIZ_COUNT_REQUIRED", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@ISEMBEDDEDACKNOWLEDGEMENTAGREED", DbType.Boolean, 1);
                //This is a fix for execute reader in case of output parameters  
                db.AddOutParameter(dbCommand, "@QUIZ_PASSEDTF", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@VIEWEVERYSCENEINCOURSETF", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETEAFTERNOUNIQUECOURSEVISITTF", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETEAFTERNOREGDATETF", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@COMPLETEAFTERNOFIRSTACCESSDATETF", DbType.Boolean, 1);

                //Abdus Samad LCMS-11888
                //START
                db.AddOutParameter(dbCommand, "@DOCUSIGNAFFIDAVITMARKEDTF", DbType.Boolean, 1);
                db.AddOutParameter(dbCommand, "@DOCUSIGNSIGNEDMARKEDTF", DbType.Boolean, 1);
                //END

                db.AddOutParameter(dbCommand, "@DOCUSIGNDECLINEDMARKEDTF", DbType.Boolean, 1);

                db.ExecuteNonQuery(dbCommand);

                postAssessment_Passed = Convert.ToBoolean(dbCommand.Parameters["@POSTASSESSMENT_PASSED"].Value);
                preAssessment_Passed = Convert.ToBoolean(dbCommand.Parameters["@PREASSESSMENT_PASSED"].Value);
                postAssessmentAttempted = Convert.ToBoolean(dbCommand.Parameters["@POSTASSESSMENTATTEMPTED"].Value);
                visitedScenePercent = Convert.ToInt32(dbCommand.Parameters["@VISITEDSCENE_PERCENT"].Value);
                courseVisitCount = Convert.ToInt32(dbCommand.Parameters["@COURSEVISITCOUNT"].Value);
                firstVisitDateTime = Convert.ToDateTime(dbCommand.Parameters["@FIRSTACCESSDATETIME"].Value);
                hasRespondedToCourseEvaluation = Convert.ToBoolean(dbCommand.Parameters["@HASRESPONDEDTOCOURSEEVALUATION"].Value);
                quiz_Count_Passed = Convert.ToInt32(dbCommand.Parameters["@QUIZ_COUNT_PASSED"].Value);
                quiz_Count_Required = Convert.ToInt32(dbCommand.Parameters["@QUIZ_COUNT_REQUIRED"].Value);
                if (dbCommand.Parameters["@ISEMBEDDEDACKNOWLEDGEMENTAGREED"] != null && dbCommand.Parameters["@ISEMBEDDEDACKNOWLEDGEMENTAGREED"].Value!=null)
                {
                    embedded_Acknowledgement_Required_And_Agreed = Convert.ToBoolean(dbCommand.Parameters["@ISEMBEDDEDACKNOWLEDGEMENTAGREED"].Value);
                }
                else
                {
                    embedded_Acknowledgement_Required_And_Agreed = false;
                }

                quiz_PassedTF = Convert.ToBoolean(dbCommand.Parameters["@QUIZ_PASSEDTF"].Value);
                viewEverySceneInCourseTF = Convert.ToBoolean(dbCommand.Parameters["@VIEWEVERYSCENEINCOURSETF"].Value);
                completeAfterUniqueVisitTF = Convert.ToBoolean(dbCommand.Parameters["@COMPLETEAFTERNOUNIQUECOURSEVISITTF"].Value);
                completeAfterRegDateTF = Convert.ToBoolean(dbCommand.Parameters["@COMPLETEAFTERNOREGDATETF"].Value);
                completeAfterFirstAccessDateTF = Convert.ToBoolean(dbCommand.Parameters["@COMPLETEAFTERNOFIRSTACCESSDATETF"].Value);

                //Abdus Samad LCMS-11888
                //START
                docusignAffidavit_Marked = Convert.ToBoolean(dbCommand.Parameters["@DOCUSIGNAFFIDAVITMARKEDTF"].Value);
                docusignSigned_Marked = Convert.ToBoolean(dbCommand.Parameters["@DOCUSIGNSIGNEDMARKEDTF"].Value);
                //END

                docusignDeclined_Marked = Convert.ToBoolean(dbCommand.Parameters["@DOCUSIGNDECLINEDMARKEDTF"].Value);

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }
        }

        public bool UpdateCourseCompletionStatistic(long enrollmentID
                                                       , bool isCourseCompletedViaPostAssessmentAttempted
                                                       , bool isCourseCompletedViaPostAssessmentMastery
                                                        , bool isCourseCompletedViaPreAssessmentMastery
                                                        , bool isCourseCompletedViaQuizMastery
                                                        , bool isCourseCompletedViaViewEveryScene
                                                        , bool isCourseCompletedViaUniqueVisit
                                                        , bool isCourseEvaluationComplete
                                                        , bool isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime
                                                        , bool isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration
                                                        , bool isEmbedded_Acknowledgement_Required_And_Agreed
                                                        , bool isCourseCompleted
														, long learnerId
														,int courseId
														,int source
                                                        )
        {
            DbCommand dbCommand = null;

            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP4_UPDATECOURSECOMPLETION);
                db.AddInParameter(dbCommand, "@COMPLETEAFTERUNIQUECOUREVISITSTF", DbType.Boolean, isCourseCompletedViaUniqueVisit);
                db.AddInParameter(dbCommand, "@COMPLETEDACKNOWLEDGEMENTTF", DbType.Boolean, isEmbedded_Acknowledgement_Required_And_Agreed);
                db.AddInParameter(dbCommand, "@COMPLETEDALLSCENESTF", DbType.Boolean, isCourseCompletedViaViewEveryScene);
                db.AddInParameter(dbCommand, "@COMPLETEWITHINTIMEFROMFIRSTACCESSTF", DbType.Boolean, isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime);
                db.AddInParameter(dbCommand, "@COMPLETEWITHINTIMEFROMREGISTRATIONTF", DbType.Boolean, isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration);
                db.AddInParameter(dbCommand, "@COURSECOMPLETETF", DbType.Boolean, isCourseCompleted);
                db.AddInParameter(dbCommand, "@COURSEEVALUATIONCOMPLETETF", DbType.Boolean, isCourseEvaluationComplete);
                db.AddInParameter(dbCommand, "@POSTASSESSMENTATTEMPTEDTF", DbType.Boolean, isCourseCompletedViaPostAssessmentAttempted);
                db.AddInParameter(dbCommand, "@POSTASSESSMENTMASTERYACHIEVEDTF", DbType.Boolean, isCourseCompletedViaPostAssessmentMastery);
                db.AddInParameter(dbCommand, "@PREASSESSMENTMASTERYACHIEVEDTF", DbType.Boolean, isCourseCompletedViaPreAssessmentMastery);
                db.AddInParameter(dbCommand, "@QUIZMASTERYACHIEVEDTF", DbType.Boolean, isCourseCompletedViaQuizMastery);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int64, enrollmentID);
				db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int64, learnerId);
				db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int64, courseId);
				db.AddInParameter(dbCommand, "@SOURCE", DbType.Int32, source);
				

                //This is a fix for execute reader in case of output parameters  
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    return true;



            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }

            return false;
        }

        /// <summary>
        /// This methood inserts the record in the learning session table
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <param name="startTime">DateTime StartTime</param>
        /// <param name="uniqueUserGUID">string uniqueUserGUID</param>
        /// <param name="learningSessionID">string learningSessionID</param>
        /// <param name="sourceVU">int sourceVU</param>
        /// <returns>returns true if successfull else false</returns>
        public bool SaveLearningSession(string courseGUID, int learnerID, int enrollmentID, DateTime startTime, string uniqueUserGUID, string learningSessionGUID, int sourceVU, int brandingID, int languageID)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                //This SP saves the learningsesion info
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNINGSESSION);
                db.AddInParameter(dbCommand, "@COURSE_GUID", DbType.String, courseGUID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@STARTTIME", DbType.DateTime, startTime);
                db.AddInParameter(dbCommand, "@UNIQUEUSER_GUID", DbType.String, uniqueUserGUID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionGUID);
                db.AddInParameter(dbCommand, "@SOURCE_VU", DbType.String, sourceVU);
                db.AddInParameter(dbCommand, "@BRANDING_ID", DbType.Int32, brandingID);
                db.AddInParameter(dbCommand, "@LANGUAGE_ID", DbType.Int32, languageID);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isInserted = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }
        /// <summary>
        /// Thhis method gets the source of courseplayer execution
        /// </summary>
        /// <param name="learningSessionGUID">string </param>
        /// <returns>returns int sourceID</returns>
        public int GetSource(string learningSessionGUID)
        {
            DbCommand dbCommand = null;
            int source = 0;
            try
            {
                //This SP returns the source of courseplayer
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SOURCE);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_ID", DbType.String, learningSessionGUID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        source = dataReader["SOURCE_VU"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SOURCE_VU"]);
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                source = 0;
            }
            return source;
        }
        /// <summary>
        /// This method gets the brandcode and variant by learning sessionID
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        public void GetLearningSessionBrandcodeVariant(string learningSessionGUID, ref string brandCode, ref string variant)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the brand code and variant for a learning session
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNINGSESSION_BRANDCODE_VARIANT);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_GUID", DbType.String, learningSessionGUID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        brandCode = dataReader["BRANDKEY"] == DBNull.Value ? string.Empty : dataReader["BRANDKEY"].ToString();
                        variant = dataReader["VARIANT"] == DBNull.Value ? string.Empty : dataReader["VARIANT"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                brandCode = string.Empty;
                variant = string.Empty;
            }
        }

        /// <summary>
        /// This method gets the brandcode and variant by enrollmentID
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        public void GetEnrollmentIDBrandcodeVariant(int enrollmentID, ref string brandCode, ref string variant)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the brand code and variant for a learning session
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_ENROLLMENTID_BRANDCODE_VARIANT);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        brandCode = dataReader["BRANDKEY"] == DBNull.Value ? string.Empty : dataReader["BRANDKEY"].ToString();
                        variant = dataReader["VARIANT"] == DBNull.Value ? string.Empty : dataReader["VARIANT"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                brandCode = string.Empty;
                variant = string.Empty;
            }
        }
        /// <summary>
        /// This method saves the learner validation statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerValidationStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        public int SaveLearnerValidationStatistics(LearnerValidationStatistics learnerValidationStatistics)
        {
            int learnerValidationStatisticsID = 0;
            DbCommand dbCommand = null;
            try
            {
                //This SP saves the learnerValidationStatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_LEARNERVALIDATIONSTATISTICS);
                db.AddInParameter(dbCommand, "@ANSWER_TEXT", DbType.String, learnerValidationStatistics.AnswerText);
                db.AddInParameter(dbCommand, "@QUESTION_ID", DbType.Int32, learnerValidationStatistics.QuestionID);
                db.AddInParameter(dbCommand, "@CORRECTTF", DbType.Boolean, learnerValidationStatistics.IsCorrect);
                db.AddInParameter(dbCommand, "@SAVETIME", DbType.DateTime, learnerValidationStatistics.SaveTime);
                db.AddInParameter(dbCommand, "@ENROLMENT_ID", DbType.Int32, learnerValidationStatistics.EnrollmentID);
                db.AddInParameter(dbCommand, "@ANSWEREDTF", DbType.Boolean, learnerValidationStatistics.IsAnswered);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    learnerValidationStatisticsID = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);
                return learnerValidationStatisticsID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        /// <summary>
        /// This method gets the course type of the course associated with a learning sesison
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <returns>string CourseType</returns>
        public string GetLearningSessionCourseType(string learningSessionGUID, ref string url)
        {
            DbCommand dbCommand = null;
            string courseType = string.Empty;
            try
            {
                //This SP returns the courseType for coure assocaited with a learning session
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSETYPE);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, learningSessionGUID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseType = dataReader["COURSETYPE"] == DBNull.Value ? string.Empty : dataReader["COURSETYPE"].ToString();
                        url = dataReader["LINK"] == DBNull.Value ? string.Empty : dataReader["LINK"].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                courseType = string.Empty;
            }
            return courseType;
        }
        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="emailAddress">string emailAddress</param>
        /// <param name="firstName">string firstName</param>
        /// <param name="lastName">string lastName</param>
        /// <param name="courseGUID">string courseGUID</param>
        /// <param name="epoch">int epoch</param>
        public void GetInfoForLMSVUConnector(string learningSessionGUID, ref string emailAddress, ref string firstName, ref string lastName, ref string courseGUID, ref int epoch, ref int learnerID
                                            , ref string phone, ref string officePhone, ref string streetAddress, ref string city, ref string zipCode, ref string state, ref string country, ref string middleName, ref string userName)
        {
            DbCommand dbCommand = null;

            try
            {
                //This SP gets the information required for LMS VU connector
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_INFOFORLMSVUCONNECTOR);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_ID", DbType.String, learningSessionGUID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        emailAddress = dataReader["EMAILADDRESS"] == DBNull.Value ? string.Empty : dataReader["EMAILADDRESS"].ToString();
                        firstName = dataReader["FIRSTNAME"] == DBNull.Value ? string.Empty : dataReader["FIRSTNAME"].ToString();
                        lastName = dataReader["LASTNAME"] == DBNull.Value ? string.Empty : dataReader["LASTNAME"].ToString();
                        courseGUID = dataReader["GUID"] == DBNull.Value ? string.Empty : dataReader["GUID"].ToString();
                        learnerID = dataReader["LEARNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNER_ID"]);
                        epoch = dataReader["EPOCH"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["EPOCH"]);
                        phone = dataReader["PHONE"] == DBNull.Value ? string.Empty : dataReader["PHONE"].ToString();
                        officePhone = dataReader["OFFICEPHONE"] == DBNull.Value ? string.Empty : dataReader["OFFICEPHONE"].ToString();
                        streetAddress = dataReader["STREETADDRESS"] == DBNull.Value ? string.Empty : dataReader["STREETADDRESS"].ToString();
                        city = dataReader["CITY"] == DBNull.Value ? string.Empty : dataReader["CITY"].ToString();
                        zipCode = dataReader["ZIPCODE"] == DBNull.Value ? string.Empty : dataReader["ZIPCODE"].ToString();
                        state = dataReader["STATE"] == DBNull.Value ? string.Empty : dataReader["STATE"].ToString();
                        country = dataReader["COUNTRY"] == DBNull.Value ? string.Empty : dataReader["COUNTRY"].ToString();
                        middleName = dataReader["MIDDLENAME"] == DBNull.Value ? string.Empty : dataReader["MIDDLENAME"].ToString();
                        userName = dataReader["USERNAME"] == DBNull.Value ? string.Empty : dataReader["USERNAME"].ToString();

                    }
                }
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                emailAddress = string.Empty;
                firstName = string.Empty;
                lastName = string.Empty;
                courseGUID = string.Empty;
                epoch = 0;
                phone = string.Empty;
                officePhone = string.Empty;
                streetAddress = string.Empty;
                city = string.Empty;
                zipCode = string.Empty;
                state = string.Empty;
                country = string.Empty;
                middleName = string.Empty;
                userName = string.Empty;
            }
        }

        /// <summary>
        /// This method will return the Primary Key associated with learning Session guid passed as string param.
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        public int GetLearningSessionID(string learningSessionId)
        {
            DbCommand dbCommand = null;
            int learningSession_ID = 0;

            try
            {
                //This SP saves the learnerValidationStatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_LEARNING_SESSION_ID);
                db.AddInParameter(dbCommand, "@learningSessionId", DbType.String, learningSessionId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        learningSession_ID = Convert.ToInt32(dataReader[0]);

                }
                return learningSession_ID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        public string GetLearningSessionGuid(int learningSessionId)
        {
            DbCommand dbCommand = null;
            string learningSession_ID = "";

            try
            {
                //This SP saves the learnerValidationStatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_GET_LEARNINGSESSION_ID_STR);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learningSessionId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        learningSession_ID = Convert.ToString(dataReader[0]);

                }
                return learningSession_ID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }
        }

        public string GetLearningSessionResourceValueOfKey(string learningSessionID, string resourceKey)
        {
            DbCommand dbCommand = null;
            string resourceValue = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNINGSESSION_RESOURCEKEY_RESOURCEVALUE);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_GUID", DbType.String, learningSessionID);
                db.AddInParameter(dbCommand, "@RESOURCEKEY", DbType.String, resourceKey);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        resourceValue = dataReader["RESOURCEVALUE"].ToString();

                }
                return resourceValue;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }

        }
        public string GetResourceValueOfResourceKey(string brandCode, string variant, string resourceKey)
        {
            DbCommand dbCommand = null;
            string resourceValue = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_RESOURCEKEY_RESOURCEVALUE);
                db.AddInParameter(dbCommand, "@BRANDKEY", DbType.String, brandCode);
                db.AddInParameter(dbCommand, "@VARIANT", DbType.String, variant);
                db.AddInParameter(dbCommand, "@RESOURCEKEY", DbType.String, resourceKey);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        resourceValue = dataReader["RESOURCEVALUE"].ToString();

                }
                return resourceValue;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        public List<LearnerStatistics> GetPreviousLearnerStatistics(string learningSessionID, string currentItemGUID)
        {
            DbCommand dbCommand = null;
            List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            LearnerStatistics learnerStatistic = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_PREVIOUS_LEARNERSTATISTICS);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, learningSessionID);
                db.AddInParameter(dbCommand, "@CURRENTITEMGUID", DbType.String, currentItemGUID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerStatistic = new LearnerStatistics();
                        learnerStatistic.Item_GUID = dataReader["ITEM_GUID"] == DBNull.Value ? string.Empty : dataReader["ITEM_GUID"].ToString();
                        learnerStatistic.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? string.Empty : dataReader["SCENE_GUID"].ToString();
                        learnerStatistic.Statistic_Type = dataReader["STATISTICSTYPE"] == DBNull.Value ? string.Empty : dataReader["STATISTICSTYPE"].ToString();
                        learnerStatistics.Add(learnerStatistic);
                    }

                }
                return learnerStatistics;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the redirect URL by learningSesssion id,returns empty if an exception occurs or record not found
        /// </summary>
        /// <param name="learningSessionID">string learningSessionID</param>
        /// <returns>string redirectURL</returns>
        public string GetLearningSessionRedirectURL(string learningSessionID)
        {
            DbCommand dbCommand = null;
            string redirectURL = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNINGSESSION_REDIRECTURL);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_GUID", DbType.String, learningSessionID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        redirectURL = dataReader["REDIRECTURL"] == DBNull.Value ? string.Empty : dataReader["REDIRECTURL"].ToString();
                    }

                }
                return redirectURL;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        /// <summary>
        /// Thhis method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        public bool GetLearnerCourseStatisticsCompleted(int enrollmentID)
        {
            DbCommand dbCommand = null;
            bool isCompleted = false;
            try
            {
                //This SP course completionstatus from learnercoursestatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNERCOURSESTATISTICS_COMPLETED);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isCompleted = dataReader["COMPLETED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETED"]);
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isCompleted = false;
            }
            return isCompleted;
        }
        /// <summary>
        /// Thhis method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        public bool GetDocuSignStatusByEnrollmentId(int enrollmentID)
        {
            DbCommand dbCommand = null;
            bool isSigned = false;
            try
            {
                //This SP course completionstatus from learnercoursestatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_DOCUSIGNSTATUS);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isSigned = dataReader["SIGNED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SIGNED"]);
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isSigned = false;
            }
            return isSigned;
        }

        /// <summary>
        /// This method gets course time spend based on enrollment ID
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>total seconds</returns>
        public int GetLearnerTimeSpent(int enrollmentID, string learningSessionGuid)
        {
            DbCommand dbCommand = null;
            int totalTimeSpent = 0;
            try
            {
                //This SP course completionstatus from learnercoursestatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_LEARNER_TIMESPENT);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
				db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, learningSessionGuid);
				

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        totalTimeSpent = dataReader["TOTALTIMESPENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TOTALTIMESPENT"]);
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                totalTimeSpent = -1;
            }
            return totalTimeSpent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enrollmentID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
		public int GetLearnerTimeSpentByTime(int enrollmentID, DateTime startTime, DateTime endTime, string learningSessionGuid)
        {
            DbCommand dbCommand = null;
            int totalTimeSpent = 0;
            try
            {
                //This SP course completionstatus from learnercoursestatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_GET_LEARNER_TIMESPENT_BY_TIME);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, enrollmentID);
                db.AddInParameter(dbCommand, "@STARTTIME", DbType.DateTime, startTime);
                db.AddInParameter(dbCommand, "@ENDTIME", DbType.DateTime, endTime);
				db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, learningSessionGuid);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        totalTimeSpent = dataReader["TOTALTIMESPENT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TOTALTIMESPENT"]);
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                totalTimeSpent = -1;
            }
            return totalTimeSpent;
        }


        /// <summary>
        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="COurse ID">Int learnerID</param>
        /// <param name="Learner ID">Int learnerID</param>
        /// <param name="CourseName">string CourseName</param>
        /// <param name="ApprovedCourseHours">string ApprovedCourseHours</param>
        /// <param name="Completion Date">string Completion Date</param>        
        public void GetLearnerCourseMetaCertificateInfo(int Course_ID, int Learner_ID, int Enrollment_ID, ref string CourseName, ref string ApprovedCourseHours, ref DateTime completionDate, ref string FirstName, ref string LastName, ref string CertificateNumber, ref DateTime CertificateIssueDate)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the learncoursetrackinfoobject including the learnerid the courseid and the last scene_GUID 
                //of the last session of that user for that course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_CERTIFICATE_INFO);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.String, Course_ID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.String, Learner_ID);
                db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.String, Enrollment_ID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        CourseName = dataReader["COURSENAME"] == DBNull.Value ? string.Empty : dataReader["COURSENAME"].ToString();
                        ApprovedCourseHours = dataReader["APPROVEDCOURSEHOURS"] == DBNull.Value ? string.Empty : dataReader["APPROVEDCOURSEHOURS"].ToString();
                        completionDate = dataReader["COMPLETIONDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["COMPLETIONDATE"]);
                        FirstName = dataReader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["FirstName"]);
                        LastName = dataReader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["LastName"]);
                        CertificateNumber = dataReader["CertificateNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["CertificateNumber"]);
                        CertificateIssueDate = dataReader["CertificateIssueDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["CertificateIssueDate"]);
                    }
                }

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                //return null;
            }
        }
        #region course unlocking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        /// 
        public LearnerProfile GetUserProfile(String TransactionGUID)
        {
            DbCommand dbCommand = null;
            LearnerProfile learnerProfile = new LearnerProfile();

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_LEARNERPROFILE);
                db.AddInParameter(dbCommand, "@GUID", DbType.String, TransactionGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerProfile.Id = dataReader["LEARNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LEARNER_ID"]);
                        learnerProfile.FirstName = dataReader["FIRSTNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["FIRSTNAME"]);
                        learnerProfile.LastName = dataReader["LASTNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["LASTNAME"]);
                        learnerProfile.EmailAddress = dataReader["EMAILADDRESS"] == DBNull.Value ? "" : Convert.ToString(dataReader["EMAILADDRESS"]);
                        learnerProfile.MobilePhone = dataReader["MOBILEPHONE"] == DBNull.Value ? "" : Convert.ToString(dataReader["MOBILEPHONE"]);
                        learnerProfile.OfficePhone = dataReader["OFFICEPHONE"] == DBNull.Value ? "" : Convert.ToString(dataReader["OFFICEPHONE"]);
                        learnerProfile.Address1 = dataReader["STREETADDRESS"] == DBNull.Value ? "" : Convert.ToString(dataReader["STREETADDRESS"]);
                        learnerProfile.Address2 = dataReader["STREETADDRESS2"] == DBNull.Value ? "" : Convert.ToString(dataReader["STREETADDRESS2"]);
                        learnerProfile.Address3 = dataReader["STREETADDRESS3"] == DBNull.Value ? "" : Convert.ToString(dataReader["STREETADDRESS3"]);
                        learnerProfile.City = dataReader["CITY"] == DBNull.Value ? "" : Convert.ToString(dataReader["CITY"]);
                        learnerProfile.State = dataReader["STATE"] == DBNull.Value ? "" : Convert.ToString(dataReader["STATE"]);
                        learnerProfile.ZipCode = dataReader["ZIPCODE"] == DBNull.Value ? "" : Convert.ToString(dataReader["ZIPCODE"]);
                        learnerProfile.Country = dataReader["COUNTRY"] == DBNull.Value ? "" : Convert.ToString(dataReader["COUNTRY"]);
                        learnerProfile.LearningSessionID = dataReader["LEARNINGSESSION_ID"] == DBNull.Value ? "" : Convert.ToString(dataReader["LEARNINGSESSION_ID"]);
                        learnerProfile.BrandCode = dataReader["BrandCode"] == DBNull.Value ? "" : Convert.ToString(dataReader["BrandCode"]);
                        learnerProfile.Variant = dataReader["Variant"] == DBNull.Value ? "" : Convert.ToString(dataReader["Variant"]);

                    }
                }

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return learnerProfile;
        }

        public bool UpdateLearnerProfile(string TransactionGUID, LearnerProfile profile)
        {
            DbCommand dbCommand = null;
            bool result = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LEARNERPROFILE);
                db.AddInParameter(dbCommand, "@TRANS_GUID", DbType.String, TransactionGUID);
                db.AddInParameter(dbCommand, "@FIRSTNAME", DbType.String, profile.FirstName);
                db.AddInParameter(dbCommand, "@LASTNAME", DbType.String, profile.LastName);
                db.AddInParameter(dbCommand, "@EMAILADDRESS", DbType.String, profile.EmailAddress);
                db.AddInParameter(dbCommand, "@MOBILEPHONE", DbType.String, profile.MobilePhone);
                db.AddInParameter(dbCommand, "@OFFICEPHONE", DbType.String, profile.OfficePhone);
                db.AddInParameter(dbCommand, "@STREETADDRESS", DbType.String, profile.Address1);
                db.AddInParameter(dbCommand, "@STREETADDRESS2", DbType.String, profile.Address2);
                db.AddInParameter(dbCommand, "@STREETADDRESS3", DbType.String, profile.Address3);
                db.AddInParameter(dbCommand, "@CITY", DbType.String, profile.City);
                db.AddInParameter(dbCommand, "@STATE", DbType.String, profile.State);
                db.AddInParameter(dbCommand, "@COUNTRY", DbType.String, profile.Country);
                db.AddInParameter(dbCommand, "@ZIPCODE", DbType.String, profile.ZipCode);
                db.ExecuteNonQuery(dbCommand);
                result = true;


            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                result = false;

            }
            return result;
        }
        public bool UpdateCompletedLearnerProfile(string emailAddress, string firstName, string lastName, int learnerID
                                            , string phone, string officePhone, string streetAddress, string city, string zipCode, string state, string country, string middleName)
        {
            DbCommand dbCommand = null;
            bool result = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_COMPLETED_LEARNERPROFILE);
                db.AddInParameter(dbCommand, "@FIRSTNAME", DbType.String, firstName);
                db.AddInParameter(dbCommand, "@LASTNAME", DbType.String, lastName);
                db.AddInParameter(dbCommand, "@EMAILADDRESS", DbType.String, emailAddress);
                db.AddInParameter(dbCommand, "@MOBILEPHONE", DbType.String, phone);
                db.AddInParameter(dbCommand, "@OFFICEPHONE", DbType.String, officePhone);
                db.AddInParameter(dbCommand, "@STREETADDRESS", DbType.String, streetAddress);
                db.AddInParameter(dbCommand, "@CITY", DbType.String, city);
                db.AddInParameter(dbCommand, "@STATE", DbType.String, state);
                db.AddInParameter(dbCommand, "@COUNTRY", DbType.String, country);
                db.AddInParameter(dbCommand, "@ZIPCODE", DbType.String, zipCode);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@MIDDLENAME", DbType.String, middleName);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    result = true;


            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                result = false;

            }
            return result;
        }

        public bool UnlockLockedCourse(string TransactionGUID)
        {
            DbCommand dbCommand = null;
            bool result = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UNLOCK_LOCKEDCOURSE);
                db.AddInParameter(dbCommand, "@GUID ", DbType.String, TransactionGUID);
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                result = false;

            }
            return result;
        }
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public bool UpdateLatestLockedCourse(int learnerID, string courseGUID)
        {
            DbCommand dbCommand = null;
            bool result = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_LOCKEDCOURSE);
                db.AddInParameter(dbCommand, "@LEARNER_ID ", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@COURSE_GUID ", DbType.String, courseGUID);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    result = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                result = false;

            }
            return result;
        }
        public bool GetLearnerCredential(string TransactionGUID, LearnerProfile profile)
        {
            DbCommand dbCommand = null;
            bool vFlag = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_LEARNERCREDENTIAL);
                db.AddInParameter(dbCommand, "@GUID", DbType.String, TransactionGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        profile.Username = dataReader["USERNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["USERNAME"]);
                        profile.Password = dataReader["PASSWORD"] == DBNull.Value ? "" : Convert.ToString(dataReader["PASSWORD"]);
                        profile.CompanyName = dataReader["NAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["NAME"]);
                        profile.Website = dataReader["WEBSITE"] == DBNull.Value ? "" : Convert.ToString(dataReader["WEBSITE"]);
                        vFlag = true;
                    }
                }

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return vFlag;
        }
        #endregion

        #region Course Evaluation
        public int SaveSurveyResult(CourseEvaluationResult courseEvaluationResult)
        {
            DbCommand dbCommand = null;
            int surveyResultID = 0;
            try
            {
                //This SP saves the course evaluation result
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_SURVEYRESULT);
                if (courseEvaluationResult.SurveyID > 0)
                    db.AddInParameter(dbCommand, "@survey_id", DbType.Int32, courseEvaluationResult.SurveyID);
                else
                    db.AddInParameter(dbCommand, "@survey_id", DbType.Int32, DBNull.Value);
                if (courseEvaluationResult.LearnerID > 0)
                    db.AddInParameter(dbCommand, "@learner_id", DbType.Int32, courseEvaluationResult.LearnerID);
                else
                    db.AddInParameter(dbCommand, "@learner_id", DbType.Int32, DBNull.Value);
                if (courseEvaluationResult.CourseID > 0)
                    db.AddInParameter(dbCommand, "@course_id", DbType.Int32, courseEvaluationResult.CourseID);
                else
                    db.AddInParameter(dbCommand, "@course_id", DbType.Int32, DBNull.Value);
                if (courseEvaluationResult.LearningSessionID > 0)
                    db.AddInParameter(dbCommand, "@learningsession_id", DbType.Int32, courseEvaluationResult.LearningSessionID);
                else
                    db.AddInParameter(dbCommand, "@learningsession_id", DbType.Int32, DBNull.Value);
                db.AddOutParameter(dbCommand, "@new_id", DbType.Int64, 8);

                //LCMS-4567
                db.AddInParameter(dbCommand, "@STARTDATE", DbType.DateTime, courseEvaluationResult.StartDate);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    surveyResultID = Convert.ToInt32(dbCommand.Parameters["@new_id"].Value);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                surveyResultID = 0;
            }
            return surveyResultID;
        }
        public bool SaveSurveyResultAnswer(CourseEvaluationResultAnswer courseEvaluationResultAnswer, int surveyResultID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP saves the course evaluation result answers
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_SURVEYRESULTANSWER);
                db.AddInParameter(dbCommand, "@surveyresult_id", DbType.Int32, surveyResultID);
                if (courseEvaluationResultAnswer.CourseEvaluationAnswerID > 0)
                    db.AddInParameter(dbCommand, "@surveyanswer_id", DbType.Int32, courseEvaluationResultAnswer.CourseEvaluationAnswerID);
                else
                    db.AddInParameter(dbCommand, "@surveyanswer_id", DbType.Int32, DBNull.Value);
                if (courseEvaluationResultAnswer.CourseEvaluationAnswerType != string.Empty)
                    db.AddInParameter(dbCommand, "@surveyresultanswertype", DbType.String, courseEvaluationResultAnswer.CourseEvaluationAnswerType);
                else
                    db.AddInParameter(dbCommand, "@surveyresultanswertype", DbType.String, DBNull.Value);
                if (courseEvaluationResultAnswer.CourseEvaluationResultAnswerText != string.Empty)
                    db.AddInParameter(dbCommand, "@surveyanswertext", DbType.String, courseEvaluationResultAnswer.CourseEvaluationResultAnswerText);
                else
                    db.AddInParameter(dbCommand, "@surveyanswertext", DbType.String, DBNull.Value);
                if (courseEvaluationResultAnswer.CourseEvaluationQuestionID > 0)
                    db.AddInParameter(dbCommand, "@surveyquestion_id", DbType.Int32, courseEvaluationResultAnswer.CourseEvaluationQuestionID);
                else
                    db.AddInParameter(dbCommand, "@surveyquestion_id", DbType.Int32, DBNull.Value);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                    return true;
                else
                    return false;

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        public bool GetCourseEvaluationResultID(int courseID, int learnerID, int learningSessionID, string surveyType)
        {
            DbCommand dbCommand = null;
            bool isAttempted = false;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSEEVALUATIONRESULT_ID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.Int32, learningSessionID);
                db.AddInParameter(dbCommand, "@SURVEYTYPE", DbType.String, surveyType);
                if (db.ExecuteScalar(dbCommand) != null)
                    isAttempted = true;
                return isAttempted;


            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        #endregion
        #endregion


        public bool UpdateLearnerCourseStatistics(long enrollmentId, string certificateURL, bool isCompleted)
        {
            DbCommand dbCommand = null;
            bool isUpdated = false;
            try
            {
                // This SP updates the Learner Course Statistics against an enrollment
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_UPDATE_LEARNERCOURSESTATISTICS);
                db.AddInParameter(dbCommand, "@CURRENTENROLLMENT_ID", DbType.Int64, enrollmentId);
                db.AddInParameter(dbCommand, "@CERTIFICATE_URL", DbType.String, certificateURL);
                db.AddInParameter(dbCommand, "@ISCOMPLETED", DbType.Boolean, isCompleted);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isUpdated = true;
            }
            catch (Exception exp)
            {
                ExceptionPolicy.HandleException(exp, "Exception Policy");
                isUpdated = false;
            }
            return isUpdated;
        }


        public int AuthenticateProctor(long courseID, long learnerID, string learningSessionID, string proctorLogin, string proctorPassword)
        {
            DbCommand dbCommand = null;

            try
            {

                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_AUTHENTICATEPROCTOR);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int64, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int64, learnerID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionID);
                db.AddInParameter(dbCommand, "@PROCTOR_USERNAME", DbType.String, proctorLogin);
                db.AddInParameter(dbCommand, "@PROCTOR_PASSWORD", DbType.String, proctorPassword);
                db.AddOutParameter(dbCommand, "@ERRORCODE", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCommand);

                dbCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.ReturnValue;

                if (dbCommand.Parameters["@ERRORCODE"].Value != DBNull.Value)
                {
                    return Convert.ToInt32(dbCommand.Parameters["@ERRORCODE"].Value);
                }

                return -1;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }

        public int AuthenticateSpecialPostAssessmentValidation(long courseID, long learnerID, string learningSessionID, string DRELicenseNumber, string DriverLicenseNumber)
        {
            DbCommand dbCommand = null;

            try
            {

                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_AUTHENTICATESPECIALPOSTASSESMENTVALIDATION);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int64, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int64, learnerID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionID);
                db.AddInParameter(dbCommand, "@DRELICENSENUMBER", DbType.String, DRELicenseNumber);
                db.AddInParameter(dbCommand, "@DRIVERLICENSENUMBER", DbType.String, DriverLicenseNumber);
                db.AddOutParameter(dbCommand, "@ERRORCODE", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCommand);

                dbCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.ReturnValue;

                if (dbCommand.Parameters["@ERRORCODE"].Value != DBNull.Value)
                {
                    return Convert.ToInt32(dbCommand.Parameters["@ERRORCODE"].Value);
                }

                return -1;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }

        public int AuthenticateNYInsuranceValidation(long courseID, long learnerID, string learningSessionID, string MonitorNumber)
        {
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_AUTHENTICATENYINSURANCEVALIDATION);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int64, courseID);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int64, learnerID);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionID);
                db.AddInParameter(dbCommand, "@MONITORNUMBER", DbType.String, MonitorNumber);
                db.AddOutParameter(dbCommand, "@ERRORCODE", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCommand);

                dbCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.ReturnValue;

                if (dbCommand.Parameters["@ERRORCODE"].Value != DBNull.Value)
                {
                    return Convert.ToInt32(dbCommand.Parameters["@ERRORCODE"].Value);
                }

                return -1;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }



        public bool ProctorLoginRequirementCriteriaMeets(string learningSessionGuid)
        {
            DbCommand dbCommand = null;

            try
            {
                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_CHECKFORPROCTORLOGINREQUIREMENT);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddOutParameter(dbCommand, "@RETURNVALUE", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);

                dbCommand.Parameters["@RETURNVALUE"].Direction = ParameterDirection.ReturnValue;

                if (dbCommand.Parameters["@RETURNVALUE"].Value != DBNull.Value)
                {
                    int RETURNVALUE = Convert.ToInt32(dbCommand.Parameters["@RETURNVALUE"].Value);
                    if (RETURNVALUE == -1)
                        return false;
                }

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }

        }


        public string GetLearnerGuid(string userName)
        {
            DbCommand dbCommand = null;

            try
            {
                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand("ICP_GETUSERGUID");
                db.AddInParameter(dbCommand, "@USERNAME", DbType.String, userName);
                db.AddOutParameter(dbCommand, "@LEARNER_GUID", DbType.String, 255);
                db.ExecuteNonQuery(dbCommand);

                //dbCommand.Parameters["@RETURNVALUE"].Direction = ParameterDirection.ReturnValue;

                if (dbCommand.Parameters["@LEARNER_GUID"].Value != DBNull.Value)
                {
                    return dbCommand.Parameters["@LEARNER_GUID"].Value.ToString();

                }

                return "";
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }

        }

        public string AUTHENTICATE_MMAPP(string userName, string password, string courseGuid)
        {
            DbCommand dbCommand = null;

            try
            {

                dbCommand = db.GetStoredProcCommand("ICP_AUTHENTICATE_MMAPP");
                db.AddInParameter(dbCommand, "@COURSE_GUID", DbType.String, courseGuid);
                db.AddInParameter(dbCommand, "@USERNAME", DbType.String, userName);
                db.AddInParameter(dbCommand, "@PASSWORD", DbType.String, password);
                // db.AddOutParameter(dbCommand, "@RETURNVALUE", DbType.String, 280);

                //db.ExecuteNonQuery(dbCommand);
                string returnValue = db.ExecuteScalar(dbCommand).ToString();



                if (returnValue != null)
                {
                    return returnValue;
                }

                //if (dbCommand.Parameters["@RETURNVALUE"].Value != DBNull.Value)
                //{
                //    return dbCommand.Parameters["@RETURNVALUE"].Value.ToString();
                //}



                return "";
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }

        }



        public string InsertBasicStatisticsForFlashCourseAndReturnLearningSessionID(string lsGuid, double score, bool isCompleted, bool isPass, int assessmentAttemptNo)
        {
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("ICP_INSERTBASICSTATSFORFLASHCOURSE");
                db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, lsGuid);
                db.AddInParameter(dbCommand, "@SCORE", DbType.Double, score);
                db.AddInParameter(dbCommand, "@ISCOMPLETED", DbType.Boolean, isCompleted);
                db.AddInParameter(dbCommand, "@ISPASSED", DbType.Boolean, isPass);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPTNUMBER", DbType.Int32, assessmentAttemptNo);


                object lsAndEnrollmentIds = db.ExecuteScalar(dbCommand);

                return Convert.ToString(lsAndEnrollmentIds);
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }

            return "";
        }

        // LCMS-9213
        public int GetAssessmentTimeForAllSessions(string learningSessionGuid, string assessmentType, int contentObjectID, int examID, string type, int assessmentConfigurationID)
        {
            DbCommand dbCommand = null;

            try
            {
                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand("ICP_GETASSESSMENTTIMEFORENROLLMENT");
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);
                db.AddInParameter(dbCommand, "@EXAM_ID", DbType.Int32, examID);
                db.AddInParameter(dbCommand, "@TYPE", DbType.String, type);
                db.AddInParameter(dbCommand, "@ASSESSMENTCONFIGURATION_ID", DbType.Int32, assessmentConfigurationID);

                int totalAssessmentTime = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                return totalAssessmentTime;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        //LCMS-10266
        public bool ResetAssessmentItemStatistics(string learningSessionGuid, string statisticsType, string assessmentType, string itemGUID, int attemptNumber, int remediationCount)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.UPDATE_ICP4_ASSESSMENTITEM_RESETBYATTEMPT);
                db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@STATISTICSTYPE", DbType.String, statisticsType);
                db.AddInParameter(dbCommand, "@ITEM_GUID", DbType.String, itemGUID);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);
                db.AddInParameter(dbCommand, "@ASSESSMENTATTEMPT_NO", DbType.Int32, attemptNumber);
                db.AddInParameter(dbCommand, "@REMEDIATIONCOUNT", DbType.Int32, remediationCount);

                bool isUpdated = db.ExecuteNonQuery(dbCommand) > 0;

                return true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        //LCMS-10266
        //LCMS-12532 Yasin
        public bool GetValidationIdendityQuestions(int learnerID)
        {
            DbCommand dbCommand = null;
            bool isValidationIdentityQuestionAskedBefore = false;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.CHECK_ISVALIDATIONQUESTION_ASKEDBEFORE);
                db.AddInParameter(dbCommand, "@learnerID", DbType.Int32, learnerID);
                db.AddOutParameter(dbCommand, "@isValidationIdentityQuestionAskedBefore", DbType.Boolean, 4);

                db.ExecuteNonQuery(dbCommand);
                isValidationIdentityQuestionAskedBefore = dbCommand.Parameters["@isValidationIdentityQuestionAskedBefore"].Value == DBNull.Value ? false : Convert.ToBoolean(dbCommand.Parameters["@isValidationIdentityQuestionAskedBefore"].Value);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isValidationIdentityQuestionAskedBefore = false;

            }


            return isValidationIdentityQuestionAskedBefore;
        }
        

        //LCMS-12532 Yasin
        public bool SaveValidationIdendityQuestions(int QS1, string Answer1, int QS2, string Answer2, int QS3, string Answer3, int QS4, string Answer4, int QS5, string Answer5, int learnerID, int QuestionSet1, int QuestionSet2, int QuestionSet3, int QuestionSet4, int QuestionSet5)
        {
            DbCommand dbCommand = null;
            int Status = 0;
            bool isInserted = false;
            //string CourseGuid = string.Empty;
            //double AvgRating = 0.0;
            //int TotalRating = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_VALIDATION_IDENTITY_QUESTIONS_ANSWERS);

                db.AddInParameter(dbCommand, "@learnerID", DbType.Int32, learnerID);
                db.AddInParameter(dbCommand, "@QS1", DbType.Int32, QS1);
                db.AddInParameter(dbCommand, "@Answer1", DbType.String, Answer1);
                db.AddInParameter(dbCommand, "@QS2", DbType.Int32, QS2);
                db.AddInParameter(dbCommand, "@Answer2", DbType.String, Answer2);
                db.AddInParameter(dbCommand, "@QS3", DbType.Int32, QS3);
                db.AddInParameter(dbCommand, "@Answer3", DbType.String, Answer3);
                db.AddInParameter(dbCommand, "@QS4", DbType.Int32, QS4);
                db.AddInParameter(dbCommand, "@Answer4", DbType.String, Answer4);
                db.AddInParameter(dbCommand, "@QS5", DbType.Int32, QS5);
                db.AddInParameter(dbCommand, "@Answer5", DbType.String, Answer5);
                db.AddInParameter(dbCommand, "@QuestionSet1", DbType.Int32, QuestionSet1);
                db.AddInParameter(dbCommand, "@QuestionSet2", DbType.Int32, QuestionSet2);
                db.AddInParameter(dbCommand, "@QuestionSet3", DbType.Int32, QuestionSet3);
                db.AddInParameter(dbCommand, "@QuestionSet4", DbType.Int32, QuestionSet4);
                db.AddInParameter(dbCommand, "@QuestionSet5", DbType.Int32, QuestionSet5);

                isInserted =  db.ExecuteNonQuery(dbCommand) > 0;
                


            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isInserted = false;

            }


            return isInserted;
        }

        
        
        //LCMS-10877
        public CourseRating SaveCourseRatingNPS(CourseRating courseRating)
        {
            DbCommand dbCommand = null;
            int Status = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_COURSE_RATINGNPS);
                db.AddInParameter(dbCommand, "@NPS_RATING", DbType.Int16, courseRating.NPS_RATING);
                db.AddInParameter(dbCommand, "@USER_REVIEW_TEXT", DbType.String, courseRating.USER_REVIEW_TEXT);
                db.AddInParameter(dbCommand, "@RATING_COURSE", DbType.Int16, courseRating.RATING_COURSE);
                db.AddInParameter(dbCommand, "@RATING_CS", DbType.Int16, courseRating.RATING_CS);
                db.AddInParameter(dbCommand, "@RATING_LEARNINGTECH", DbType.Int16, courseRating.RATING_LEARNINGTECH);
                db.AddInParameter(dbCommand, "@RATING_SHOPPINGEXP", DbType.Int16, courseRating.RATING_SHOPPINGEXP);
                db.AddInParameter(dbCommand, "@RATING_COURSE_SECONDARY", DbType.String, courseRating.RATING_COURSE_SECONDARY);
                db.AddInParameter(dbCommand, "@RATING_CS_SECONDARY", DbType.String, courseRating.RATING_CS_SECONDARY);
                db.AddInParameter(dbCommand, "@RATING_LEARNINGTECH_SECONDARY", DbType.String, courseRating.RATING_LEARNINGTECH_SECONDARY);
                db.AddInParameter(dbCommand, "@RATING_SHOPPINGEXP_SECONDARY", DbType.String, courseRating.RATING_SHOPPINGEXP_SECONDARY);
                db.AddInParameter(dbCommand, "@RATING", DbType.Int32, courseRating.RATING_COURSE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseRating.CourseID);
                db.AddInParameter(dbCommand, "@LEARNER_ENROLLMENT_ID", DbType.Int32, courseRating.EnrollmentID);
                db.AddOutParameter(dbCommand, "@RATING_STATUS", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@TOTAL_RATING", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSE_GUID", DbType.String, 255);
                db.AddOutParameter(dbCommand, "@AVG_RATIING", DbType.Double, 4);
                db.AddInParameter(dbCommand, "@RATING_COURSE_SECONDARY_Q", DbType.String, courseRating.RATING_COURSE_SECONDARY_Q);
                db.AddInParameter(dbCommand, "@RATING_CS_SECONDARY_Q", DbType.String, courseRating.RATING_CS_SECONDARY_Q);
                db.AddInParameter(dbCommand, "@RATING_LEARNINGTECH_SECONDARY_Q", DbType.String, courseRating.RATING_LEARNINGTECH_SECONDARY_Q);
                db.AddInParameter(dbCommand, "@RATING_SHOPPINGEXP_SECONDARY_Q", DbType.String, courseRating.RATING_SHOPPINGEXP_SECONDARY_Q);



                db.ExecuteNonQuery(dbCommand);
                Status = Convert.ToInt32(dbCommand.Parameters["@RATING_STATUS"].Value);
                courseRating.TotalRating = Convert.ToInt32(dbCommand.Parameters["@TOTAL_RATING"].Value);
                courseRating.CourseGuid = Convert.ToString(dbCommand.Parameters["@COURSE_GUID"].Value);
                courseRating.AvgRating = Convert.ToDouble(dbCommand.Parameters["@AVG_RATIING"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                Status = 3;
        
            }


            return courseRating;

        }

        public int SaveCourseRating(int CourseID, int Rating, int EnrollmentID, out string CourseGuid, out double AvgRating, out int TotalRating)
        {
            DbCommand dbCommand = null;
            int Status= 0;
            //string CourseGuid = string.Empty;
            //double AvgRating = 0.0;
            //int TotalRating = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_COURSE_RATING);
                db.AddInParameter(dbCommand, "@RATING", DbType.Int32, Rating);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32,CourseID);
                db.AddInParameter(dbCommand, "@LEARNER_ENROLLMENT_ID", DbType.Int32, EnrollmentID);

                db.AddOutParameter(dbCommand, "@RATING_STATUS", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@TOTAL_RATING", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSE_GUID", DbType.String,255);
                db.AddOutParameter(dbCommand, "@AVG_RATIING", DbType.Double, 4);

                db.ExecuteNonQuery(dbCommand);
                Status = Convert.ToInt32(dbCommand.Parameters["@RATING_STATUS"].Value);
                TotalRating = Convert.ToInt32(dbCommand.Parameters["@TOTAL_RATING"].Value);
                CourseGuid = Convert.ToString(dbCommand.Parameters["@COURSE_GUID"].Value);
                AvgRating = Convert.ToDouble(dbCommand.Parameters["@AVG_RATIING"].Value);



            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                Status = 3;
                TotalRating = 0;
                CourseGuid = "";
                AvgRating = 0;
                
            }


            return Status;
        }


        public CourseRating GetUserCourseRatingNPS(int CourseID, int EnrollmentID)
        {
            DbCommand dbCommand = null;
            CourseRating courseRating = new CourseRating();
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSE_USER_RATINGNPS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseID);
                db.AddInParameter(dbCommand, "@LEARNER_ENROLLMENT_ID", DbType.Int64, EnrollmentID);
                courseRating.CourseID = CourseID;
                courseRating.EnrollmentID = EnrollmentID;

                IDataReader dataReader = db.ExecuteReader(dbCommand);
                while (dataReader.Read())
                {
                    courseRating.Rating = dataReader["Rating"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Rating"]);
                    courseRating.NPS_RATING = dataReader["NPS_RATING"] == DBNull.Value ? (short)0 : Convert.ToInt16(dataReader["NPS_RATING"]);
                    courseRating.USER_REVIEW_TEXT = dataReader["USER_REVIEW_TEXT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["USER_REVIEW_TEXT"]);
                    courseRating.RATING_COURSE = dataReader["RATING_COURSE"] == DBNull.Value ? (short)0 : Convert.ToInt16(dataReader["RATING_COURSE"]);
                    courseRating.RATING_CS = dataReader["RATING_CS"] == DBNull.Value ? (short)0 : Convert.ToInt16(dataReader["RATING_CS"]);
                    courseRating.RATING_SHOPPINGEXP = dataReader["RATING_SHOPPINGEXP"] == DBNull.Value ? (short)0 : Convert.ToInt16(dataReader["RATING_SHOPPINGEXP"]);
                    courseRating.RATING_LEARNINGTECH = dataReader["RATING_LEARNINGTECH"] == DBNull.Value ? (short)0 : Convert.ToInt16(dataReader["RATING_LEARNINGTECH"]);

                    courseRating.RATING_COURSE_SECONDARY = dataReader["RATING_COURSE_SECONDARY"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_COURSE_SECONDARY"]);
                    courseRating.RATING_CS_SECONDARY = dataReader["RATING_CS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_CS_SECONDARY"]);
                    courseRating.RATING_SHOPPINGEXP_SECONDARY = dataReader["RATING_SHOPPINGEXP"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_SHOPPINGEXP_SECONDARY"]);
                    courseRating.RATING_LEARNINGTECH_SECONDARY = dataReader["RATING_LEARNINGTECH"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_LEARNINGTECH_SECONDARY"]);

                    courseRating.RATING_COURSE_SECONDARY_Q = dataReader["RATING_COURSE_SECONDARY_Q"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_COURSE_SECONDARY_Q"]);
                    courseRating.RATING_CS_SECONDARY_Q = dataReader["RATING_CS_SECONDARY_Q"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_CS_SECONDARY_Q"]);
                    courseRating.RATING_SHOPPINGEXP_SECONDARY_Q = dataReader["RATING_SHOPPINGEXP_SECONDARY_Q"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_SHOPPINGEXP_SECONDARY_Q"]);
                    courseRating.RATING_LEARNINGTECH_SECONDARY_Q = dataReader["RATING_SHOPPINGEXP_SECONDARY_Q"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["RATING_SHOPPINGEXP_SECONDARY_Q"]);

                }
                
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                

            }


            return courseRating;
        }

        //LCMS-10877

        public int GetUserCourseRating(int CourseID, int EnrollmentID)
        {
            DbCommand dbCommand = null;
            int LearnerRating = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSE_USER_RATING);              
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseID);
                db.AddInParameter(dbCommand, "@LEARNER_ENROLLMENT_ID", DbType.Int32, EnrollmentID);
                db.AddOutParameter(dbCommand, "@RATING_LEARNER", DbType.Int32, 4);
      
                db.ExecuteNonQuery(dbCommand);
                LearnerRating = dbCommand.Parameters["@RATING_LEARNER"].Value == DBNull.Value ? 0 : Convert.ToInt32(dbCommand.Parameters["@RATING_LEARNER"].Value);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                LearnerRating = -1;

            }


            return LearnerRating;
        }
        
        //LCMS-10877


        //Abdus Samad 
        //LCMS-11888
        //START

        /// <summary>
        /// This method returns Course Approval Affidavit
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>        
        /// <returns>int</returns>
        public int GetCourseApprovalAffidavitForTrackingService(int courseID, int courseApprovalID)
        {
            DbCommand dbCommand = null;
            try
            {
                int affidavitID = 0;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSEAPPROVALAFFIDAVIT);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@COURSEAPPROVAL_ID", DbType.Int32, courseApprovalID);

                IDataReader dataReader = db.ExecuteReader(dbCommand);
                while (dataReader.Read())
                {
                    affidavitID = dataReader["AFFIDAVIT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["AFFIDAVIT_ID"]);
                }

                return affidavitID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// This method returns the Asset
        /// </summary>
        /// <param name="affidativeID"></param>
        /// <returns></returns>
        public Asset GetAffidavitAssetForTrackingService(int affidativeID)
        {
            DbCommand dbCommand = null;
            Asset asset = null;
            try
            {
                asset = new Asset();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_ASSET);
                db.AddInParameter(dbCommand, "@ASSET_ID", DbType.Int32, affidativeID);
                db.AddInParameter(dbCommand, "@VERSION_ID", DbType.Int32, 0);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        asset.URL = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        asset.AssetType = dataReader["ASSETTYPE"] == DBNull.Value ? "" : dataReader["ASSETTYPE"].ToString();
                        asset.AssetName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        asset.ContentText = dataReader["CONTENT"] == DBNull.Value ? "" : dataReader["CONTENT"].ToString();
                        //LCMS-11217
                        asset.AffidavitTemplateId = (dataReader["AFFIDAVITTEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt64(dataReader["AFFIDAVITTEMPLATE_ID"]));
                        asset.DisplayText1 = dataReader["DISPLAYTEXT1"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT1"].ToString();
                        asset.DisplayText2 = dataReader["DISPLAYTEXT2"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT2"].ToString();
                        asset.DisplayText3 = dataReader["DISPLAYTEXT3"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT3"].ToString();
                        //End
                    }
                }
                return asset;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return asset;
            }
        }
        //Abdus Samad 
        //LCMS-11888
        //STOP



        //LCMS-11974 //DocuSign Decline
        //Abdus Samad Start
        public LearnerProfile GetUserProfileInformation(int enrollmentID)
        {
            DbCommand dbCommand = null;
            LearnerProfile learnerProfile = new LearnerProfile();

            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_LEARNERPROFILEINFORMATION);
                db.AddInParameter(dbCommand, "@ENROLLMENTID", DbType.Int32, enrollmentID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        learnerProfile.FullName = dataReader["FULLNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["FULLNAME"]);
                        learnerProfile.Address1 = dataReader["STREETADDRESS"] == DBNull.Value ? "" : Convert.ToString(dataReader["STREETADDRESS"]);
                        learnerProfile.EmailAddress = dataReader["EMAILADDRESS"] == DBNull.Value ? "" : Convert.ToString(dataReader["EMAILADDRESS"]);
                        learnerProfile.OfficePhone = dataReader["PHONENUMBER"] == DBNull.Value ? "" : Convert.ToString(dataReader["PHONENUMBER"]);
                        learnerProfile.Username = dataReader["USERNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["USERNAME"]);
                        learnerProfile.CourseName = dataReader["COURSENAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["COURSENAME"]);
                        learnerProfile.BusinessKey = dataReader["BUSSINESSKEY"] == DBNull.Value ? "" : Convert.ToString(dataReader["BUSSINESSKEY"]);

                        //LCMS-12526
                        //Abdus Samad
                        //Start
                        learnerProfile.FirstName = dataReader["FIRSTNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["FIRSTNAME"]);
                        learnerProfile.LastName = dataReader["LASTNAME"] == DBNull.Value ? "" : Convert.ToString(dataReader["LASTNAME"]);
                        learnerProfile.MobilePhone = dataReader["MOBILEPHONE"] == DBNull.Value ? "" : Convert.ToString(dataReader["MOBILEPHONE"]);
                        //Stop
                    }
                }

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return learnerProfile;
        }
       //Abdus Samad //Stop

        //LCMS-12502
        //Abdus Samad Start
        /// <summary>
        /// This function is used to 
        /// </summary>
        /// <param name="envelopId"></param>
        /// <param name="learnerEnrollmentID"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool SaveDocuSignRoleAgainstLearnerEnrollment(string envelopId, int learnerEnrollmentID, string roleName)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                // This SP updates the Learner Course Statistics against an enrollment
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_DOCUSIGNROLEAGAINSTLEARNERENROLLMENT);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, learnerEnrollmentID);
                db.AddInParameter(dbCommand, "@ENVELOP_ID", DbType.String, envelopId);
                db.AddInParameter(dbCommand, "@ROLENAME", DbType.String, roleName);
                isInserted = db.ExecuteNonQuery(dbCommand) > 0;
                   
            }
            catch (Exception exp)
            {
                ExceptionPolicy.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="envelopId"></param>
        /// <param name="learnerEnrollmentID"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool SaveEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName)
        {
            DbCommand dbCommand = null;
            bool isInserted = false;
            try
            {
                // This SP updates the Learner Course Statistics against an enrollment
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_UPDATE_ENVELOPSTATUSAGAINSTDOCUSIGNROLE);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, learnerEnrollmentID);
                db.AddInParameter(dbCommand, "@ROLENAME", DbType.String, roleName);
                isInserted = db.ExecuteNonQuery(dbCommand) > 0;

            }
            catch (Exception exp)
            {
                ExceptionPolicy.HandleException(exp, "Exception Policy");
                isInserted = false;
            }
            return isInserted;
        }

        public bool GetEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName)
        {
            DbCommand dbCommand = null;
            bool isSignedStatus = false;
            try
            {
                // This SP updates the Learner Course Statistics against an enrollment
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_ENVELOPSTATUSAGAINSTDOCUSIGNROLE);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, learnerEnrollmentID);
                db.AddInParameter(dbCommand, "@ROLENAME", DbType.String, roleName);
                db.AddOutParameter(dbCommand, "@SIGNEDSTATUSTF", DbType.Boolean, 50);
                db.ExecuteNonQuery(dbCommand);

                isSignedStatus = Convert.ToBoolean(dbCommand.Parameters["@SIGNEDSTATUSTF"].Value);


            }
            catch (Exception exp)
            {
                ExceptionPolicy.HandleException(exp, "Exception Policy");
                isSignedStatus = false;
            }
            return isSignedStatus;
        }


        //LCMS-12502
        //Abdus Samad Stop

        public string CreateBody(LearnerStatistics learnerStatistics, int mastoryScore)
        {
    
            string mailBody = @"<table><tr><td>Information related to the exception in Save Assessment Score listed below:</td></tr>";
            mailBody = mailBody + @"<tr><td><b>LEARNINGSESSION ID: </b>" + learnerStatistics.LearningSession_ID + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>ENROLLMENT ID: </b>" + learnerStatistics.LearnerEnrollment_ID + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>TIMEINSECONDS: </b>" + learnerStatistics.TimeInSeconds + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>STATISTICSTYPE: </b>" + learnerStatistics.Statistic_Type + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>ITEM_GUID: </b>" + learnerStatistics.Item_GUID + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>ASSESSMENTTYPE: </b>" + learnerStatistics.AssessmentType + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>NUMOFCORRECT: </b>" + learnerStatistics.NumberAnswersCorrect + "</td></tr>";
            mailBody = mailBody + @"<tr> <td><b>NUMOFINCORRECT: </b>" + learnerStatistics.NumberAnswersIncorrect + "</td></tr>";
            mailBody = mailBody + @" <tr><td><b>ASSESSMENTATTEMPT NO: </b>" + learnerStatistics.AssessmentAttemptNumber + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>MAXATTEMPTREACHACTIONTF: </b>" + learnerStatistics.MaxAtemptActionTaken + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>STATISTICDATE: </b>" + DateTime.Now + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>REMEDIATIONCOUNT: </b>" + learnerStatistics.RemediationCount + "</td></tr>";
            if (learnerStatistics.RawScore == -2)              
                mailBody = mailBody + @"<tr><td><b>WEIGHTEDSCORE: </b>" + DBNull.Value + "</td></tr>";
            else
                mailBody = mailBody + @"<tr><td><b>WEIGHTEDSCORE: </b>" + learnerStatistics.RawScore + "</td></tr>";                  
            mailBody = mailBody + @"<tr><td><b>ISPASS: </b>" + learnerStatistics.IsPass + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>MASTERYSCORE: </b>" + mastoryScore + "</td></tr>";
            mailBody = mailBody + @"<tr><td><b>REPEATEDASSESSMENTATTEMPT: </b>" + learnerStatistics.IsRepeatedAssessmentAttempt + "</td></tr>";       
            mailBody = mailBody + @"</table>";
            return mailBody;

        }

       public bool SendEmail(string ToEmail, string FromEmail, string Subject, string MailBody)
         {

             try
             {
                 System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                 smtpClient.UseDefaultCredentials = false;
                 smtpClient.Credentials = new System.Net.NetworkCredential();
                 //smtpClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["SMTPUser"], System.Configuration.ConfigurationSettings.AppSettings["SMTPPassword"]);
                 smtpClient.Host = System.Configuration.ConfigurationSettings.AppSettings["SMTPAddress"];
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

       public LearningSessionInformation CheckIfLearningSessionOpen(string LearningSessionID)
       {
           DbCommand dbCommand = null;        

           LearningSessionInformation LearningSessionInformation = new LearningSessionInformation();
           try
           {
               // This SP updates the Learner Course Statistics against an enrollment
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_LEARNINGSESSION);           
               db.AddInParameter(dbCommand, "@LEARNINGSESSION_ID", DbType.String, LearningSessionID);
               db.AddOutParameter(dbCommand, "@LEARNINGSESSIONOUTPUT_ID", DbType.String, 50);
               db.AddOutParameter(dbCommand, "@OLDLEARNINGSESSIONSTARTTIME", DbType.DateTime, 50);
               db.ExecuteNonQuery(dbCommand);                        

               
               if (dbCommand.Parameters["@LEARNINGSESSIONOUTPUT_ID"].Value != DBNull.Value)
               {
                   LearningSessionInformation.LearningSessionOutputID = Convert.ToString(dbCommand.Parameters["@LEARNINGSESSIONOUTPUT_ID"].Value);
               }

               if (dbCommand.Parameters["@OLDLEARNINGSESSIONSTARTTIME"].Value != DBNull.Value)
               {
                   LearningSessionInformation.OldLearningSessionStartTime = Convert.ToDateTime(dbCommand.Parameters["@OLDLEARNINGSESSIONSTARTTIME"].Value);
               }              


           }
           catch (Exception exp)
           {
               ExceptionPolicy.HandleException(exp, "Exception Policy");
               
           }
           return LearningSessionInformation;  
       
       }


       public string GetCourseIDAgainstLearningSessionGUID(string LearningSessionGuid)
       {
           DbCommand dbCommand = null;
           string CourseId = string.Empty;        
           try
           {               
               // This SP updates the Learner Course Statistics against an enrollment
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_COURSEID_BY_LEARNINGSESSIONGUID);
               db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, LearningSessionGuid);
               db.AddOutParameter(dbCommand, "@COURSE_ID", DbType.String, 50);           
               db.ExecuteNonQuery(dbCommand);


               if (dbCommand.Parameters["@COURSE_ID"].Value != DBNull.Value)
               {
                   CourseId = Convert.ToString(dbCommand.Parameters["@COURSE_ID"].Value);
               }

               return CourseId;


           }
           catch (Exception exp)
           {
               ExceptionPolicy.HandleException(exp, "Exception Policy");

           }

           return CourseId;

       }


       public int GetTotalTimeSpent(string LearningSessionGuid, int EnrollmentID)
       {
           DbCommand dbCommand = null;
           int TotalTimeShouldAdjusted = 0;
           try
           {
               // This SP updates the Learner Course Statistics against an enrollment
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_LEARNER_TIMESPENT);
               db.AddInParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, LearningSessionGuid);
               db.AddInParameter(dbCommand, "@ENROLLMENT_ID", DbType.Int32, EnrollmentID);
               db.AddOutParameter(dbCommand, "@TOTALTIMESHOULDBEADJUSTED", DbType.Int32, 50);
               db.ExecuteNonQuery(dbCommand);


               if (dbCommand.Parameters["@TOTALTIMESHOULDBEADJUSTED"].Value != DBNull.Value)
               {
                   TotalTimeShouldAdjusted = Convert.ToInt32(dbCommand.Parameters["@TOTALTIMESHOULDBEADJUSTED"].Value);
               }

               return TotalTimeShouldAdjusted;


           }
           catch (Exception exp)
           {
               ExceptionPolicy.HandleException(exp, "Exception Policy");

           }

           return TotalTimeShouldAdjusted;

       }
    }
}