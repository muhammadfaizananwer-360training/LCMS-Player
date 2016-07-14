using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Transactions;
using _360Training.BusinessEntities;
using _360Training.TrackingServiceDataLogic.StudentTrackingDA;
using ICP4.BusinessLogic.IntegerationManager;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.BusinessLogic.ICPCourseService;
using ICP4.BusinessLogic;
using System.Web;

namespace _360Training.TrackingServiceBusinessLogic
{
    public class TrackingManager : IDisposable, ITrackingManager
    {
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ITrackingManagerMethods
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
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.SaveStudentCourseTrack(courseID, learnerID, sequenceNo);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        /// <summary>
        /// This method gets the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>int sequenceNo.</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetStudentCourseTrack(int courseID, int learnerID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetStudentCourseTrack(courseID, learnerID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        public bool EndSession(string sessionID, DateTime endTime, int totalTimeSpent, double percentageCourseProgress, long enrollment_Id)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateLearningSessionEndtime(sessionID, endTime, totalTimeSpent, percentageCourseProgress, enrollment_Id);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        public bool EndSessionLegacy(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED)
        {

            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    studentTrackingDA.UpdateLearningSessionEndtimeLegacy(sessionID, endTime, MAXPERCENTAGECOURSEATTENDED);

                    Integeration integeration = ICP4.BusinessLogic.IntegerationManager.IntegerationFactory.GetObject(0);
                    IntegerationStatistics integerationStatistics = new IntegerationStatistics();

                    integerationStatistics = new IntegerationStatistics();
                    integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.CourseCompletion;
                    integerationStatistics.LearningSessionGuid = sessionID;
                    integerationStatistics.IsCourseCompleted = false;
                    //integerationStatistics.Enrollment_Id = enrollmentID;
                    integerationStatistics.CourseGuid = "";
                    integerationStatistics.PercentageCourseProgress = (double)MAXPERCENTAGECOURSEATTENDED;

                    integeration.SynchStatsToExternalSystem(integerationStatistics);
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }


        public bool LearningSessionEndedLMS(string learnerSessioGUID, bool completed, string certificateURL)
        {
            try
            {
                Integeration integeration = ICP4.BusinessLogic.IntegerationManager.IntegerationFactory.GetObject(0);
                IntegerationStatistics integerationStatistics = new IntegerationStatistics();

                integerationStatistics = new IntegerationStatistics();
                integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.CourseCompletion;
                integerationStatistics.LearningSessionGuid = learnerSessioGUID;
                integerationStatistics.IsCourseCompleted = completed;
                integerationStatistics.CertificateURL = certificateURL;
                //integerationStatistics.Enrollment_Id = enrollmentID;
                integerationStatistics.CourseGuid = "";
                integerationStatistics.PercentageCourseProgress = 0;

                integeration.SynchStatsToExternalSystem(integerationStatistics);
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        public bool EndSessionLegacyScorm(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED, bool PASSEDASSESSMENT, decimal RAWSCORE)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateLearningSessionEndtimeLegacyScorm(sessionID, endTime, MAXPERCENTAGECOURSEATTENDED, PASSEDASSESSMENT, RAWSCORE);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }


        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        public bool SaveLearnerStatistics(LearnerStatistics learnerStatistics)
        {
            bool returnValue = false;
            try
            {
                int learnerStatisticsID = 0;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    learnerStatisticsID = studentTrackingDA.SaveLearnerStatistics(learnerStatistics);
                    if (learnerStatisticsID > 0)
                    {
                        foreach (LearnerStatisticsAnswer learnerStatisticsAnswer in learnerStatistics.LearnerStatisticsAnswers)
                        {
                            learnerStatisticsAnswer.LearnerStatisticsID = learnerStatisticsID;
                            studentTrackingDA.SaveLearnerStatisticsAnswer(learnerStatisticsAnswer);

                        }
                        returnValue = true;
                    }
                    else
                        returnValue = false;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }
        /// <summary>
        /// This method saves the learner statistics fro ICP4
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        public bool SaveICP4LearnerStatistics(LearnerStatistics learnerStatistics)
        {
            bool returnValue = false;
            try
            {
                int learnerStatisticsID = 0;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    learnerStatisticsID = studentTrackingDA.SaveICP4LearnerStatistics(learnerStatistics);
                    if (learnerStatisticsID > 0)
                    {
                        foreach (LearnerStatisticsAnswer learnerStatisticsAnswer in learnerStatistics.LearnerStatisticsAnswers)
                        {
                            learnerStatisticsAnswer.LearnerStatisticsID = learnerStatisticsID;
                            studentTrackingDA.SaveLearnerStatisticsAnswer(learnerStatisticsAnswer);

                        }
                        returnValue = true;
                    }
                    else
                        returnValue = false;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }

        public bool SaveLearnerStatistics_Scene(LearnerStatistics learnerStatistics)
        {
            bool returnValue = false;
            try
            {
                int learnerStatisticsID = 0;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    learnerStatisticsID = studentTrackingDA.SaveLearnerStatistics_Scene(learnerStatistics);
                    if (learnerStatisticsID > 0)
                    {
                        returnValue = true;
                    }
                    else
                        returnValue = false;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        public bool SaveLearnerStatisticsWithStatisticDateTime(LearnerStatistics learnerStatistics, DateTime StatisticDateTime)
        {

            bool returnValue = false;
            try
            {
                /*
                int learnerStatisticsID = 0;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    learnerStatisticsID = studentTrackingDA.SaveLearnerStatisticsWithStatisticDateTime(learnerStatistics, StatisticDateTime);
                    if (learnerStatisticsID > 0)
                    {
                        foreach (LearnerStatisticsAnswer learnerStatisticsAnswer in learnerStatistics.LearnerStatisticsAnswers)
                        {
                            learnerStatisticsAnswer.LearnerStatisticsID = learnerStatisticsID;
                            studentTrackingDA.SaveLearnerStatisticsAnswer(learnerStatisticsAnswer);

                        }
                        returnValue = true;
                    }
                    else
                        returnValue = false;
                }
                 */
                string LearningSessionGuid = GetLearningSessionID(learnerStatistics.LearningSession_ID);
                Integeration integeration = ICP4.BusinessLogic.IntegerationManager.IntegerationFactory.GetObject(0);
                IntegerationStatistics integerationStatistics = new IntegerationStatistics();

                if (learnerStatistics.Statistic_Type == _360Training.BusinessEntities.LearnerStatisticsType.ContentAsset)
                {

                    returnValue = EndSession(LearningSessionGuid, StatisticDateTime, learnerStatistics.TimeInSeconds, 0, 0);
                    integerationStatistics = new IntegerationStatistics();
                    integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.CourseCompletion;
                    integerationStatistics.LearningSessionGuid = LearningSessionGuid;
                    integerationStatistics.IsCourseCompleted = false;
                    integerationStatistics.CourseTimeSpent = learnerStatistics.TimeInSeconds;
                    //integerationStatistics.Enrollment_Id = enrollmentID;
                    integerationStatistics.CourseGuid = "";
                    //integerationStatistics.PercentageCourseProgress = courseProgress;
                }
                else
                {
                    using (TrackingManager trackingManager = new TrackingManager())
                    {
                        learnerStatistics.AssessmentType = ICP4.BusinessLogic.CourseManager.LearnerStatisticsTypeTranslator.ConvertLearnerSequenceTypeToAssessmentType(learnerStatistics.AssessmentType);
                        learnerStatistics.RawScore = (learnerStatistics.NumberAnswersCorrect * 1.0) * 100 / (learnerStatistics.NumberAnswersCorrect + learnerStatistics.NumberAnswersIncorrect);
                        learnerStatistics.IsPass = learnerStatistics.CorrectTF;
                        SaveAssessmentScore(learnerStatistics, 80);
                        returnValue = true;
                    }

                    integerationStatistics = new IntegerationStatistics();
                    integerationStatistics.AssessmentType = learnerStatistics.AssessmentType;
                    integerationStatistics.AssessmentScore = learnerStatistics.RawScore;
                    integerationStatistics.IsAssessmentPassed = learnerStatistics.CorrectTF;
                    integerationStatistics.LearningSessionGuid = LearningSessionGuid;
                    integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.AssessmentCompletion;
                }

                integeration.SynchStatsToExternalSystem(integerationStatistics);
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }

        /// <summary>
        /// This method returns the learner course track info object
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        public LearnerCourseTrackInfo GetLearnerCoursetrackInfo(string sessionID)
        {
            try
            {
                LearnerCourseTrackInfo learnerCourseTrackInfo = new LearnerCourseTrackInfo();
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    //learnerCourseTrackInfo = studentTrackingDA.GetCurrentLearnerCoursetrackInfo(sessionID);
                    //if (learnerCourseTrackInfo.CourseID==0)//means no records were found
                    learnerCourseTrackInfo = studentTrackingDA.GetLearnerCoursetrackInfo(sessionID);
                    studentTrackingDA.Insert_SetupEnrollment(learnerCourseTrackInfo.EnrollmentID, learnerCourseTrackInfo.LearnerSessionID);
                    //Insert Completion and Attendence record



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
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.SaveLearnerCourseBookmark(courseID, learnerID, enrollmentID, item_GUID, sceneGUID, flashSceneNo, bookMarkTitle, lastScene, isMovieEnded, nextButtonState, firstSceneName, createddate);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        /// <summary>
        /// This method returns the LearnerCourseBookmarkInfo object
        /// </summary>
        /// <param name="bookMarkID">int learnerCourseBookMarkID</param>
        /// <returns>BookMarkInfo object</returns>
        public LearnerCourseBookMarkInfo GetLearnerCourseBookMarkInfo(int learnerCourseBookMarkID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerCourseBookMarkInfo(learnerCourseBookMarkID);
                }
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
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetAllLearnerCourseBookMarksInfo(courseID, learnerID, enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method delete the bookmark against bookmarkID        
        /// </summary>
        /// <param name="courseID">int bookmarkID</param>        
        /// <returns>true/false</returns>
        public bool DeleteLearnerCourseBookMarksInfo(int bookmarkID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.DeleteLearnerCourseBookmark(bookmarkID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
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
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetAllLearnerCourseTrackInfo(courseID, learnerID, enrollmentID);
                }
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
        public List<LearnerStatistics> GetPreviouslyAskedQuestions(string learnerSessionGUID, string assessmentType, int remediationCount, int examID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsAssessments(learnerSessionGUID, assessmentType, remediationCount, examID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public List<LearnerStatistics> GetPreviouslyAskedQuestionsQuiz(string learnerSessionGUID, string assessmentType, int remediationCount, int contentObjectId)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsQuizAssessments(learnerSessionGUID, assessmentType, remediationCount, contentObjectId);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the assessmentAttempt no of preassessment
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetPreAssessmentAttemptNo(string learnerSessionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsPreAssessmentAttemptNo(learnerSessionGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method returns the postassessmentAttempt no 
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetPostAssessmentAttemptNo(string learnerSessionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsPostAssessmentAttemptNo(learnerSessionGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }
        /// <summary>
        /// This method returns the quizAttempt no of the specified contentobject
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>int attempNo</returns>
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetQuizAttemptNo(string learnerSessionGUID, string contentObjectGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsQuizAttemptNo(learnerSessionGUID, contentObjectGUID);
                }
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
        public List<LearnerStatistics> GetPreAssessmentResult(string learnerSessionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsPreAssessmentResults(learnerSessionGUID);
                }
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
        public List<LearnerStatistics> GetPostAssessmentResult(string learnerSessionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsPostAssessmentResults(learnerSessionGUID);
                }
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
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetQuizResult(string learnerSessionGUID, string contentObjectGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsQuizResults(learnerSessionGUID, contentObjectGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the learnerstatistics records of the Practice Exam result 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        public List<LearnerStatistics> GetPracticeExamResult(string learnerSessionGUID, string contentObjectGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerStatisticsPracticeExamResults(learnerSessionGUID, contentObjectGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method locks unlocks the course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="date">datetime date</param>
        /// <param name="islock">bool isLock</param>
        /// <returns>boolean true if successfull,else false</returns>
        public bool LockUnlockCourse(int courseID, int learnerID, int enrollmentID, DateTime date, string lockType, bool islock)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.SaveLockedCourse(courseID, learnerID, enrollmentID, date, lockType, islock);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        public bool LockCourseDuringAssessment(int courseID, int enrollmentID, DateTime date, string lockType, bool islock)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.LockCourseDuringAssessment(courseID, enrollmentID, date, lockType, islock);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        public bool UpdateCourseStatusDuringAssessment(int courseID, int enrollmentID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateCourseStatusDuringAssessment(courseID, enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        
        /// <summary>
        /// This method returns the course locked status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>boolean true if locked,false if not and if exception is thrown</returns>
        public bool IsCourseLocked(int enrollmentID, out string lockType)
        {
            try
            {
                lockType = string.Empty;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLockedCourseCourseLocked(enrollmentID, ref lockType);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                lockType = string.Empty;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="learnerStatisticsID"></param>
        /// <param name="maxAttemptActionTaken"></param>
        /// <returns></returns>
        public bool UpdateLearnerStatisticsMaximumAttemptAction(long learnerStatisticsID, bool maxAttemptActionTaken)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateLearnerStatisticsMaximumAttemptAction(learnerStatisticsID, maxAttemptActionTaken);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        //public bool IsCourseCompleted(int courseID, int learnerID)
        //{
        //    try
        //    {
        //        return CalculateCourseCompletionStatus(courseID, learnerID);
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
        //        return false;
        //    }
        //}
        /// <summary>
        /// This method calculates the course completion status 
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="learnerID"></param>
        /// <returns>true if completed,else false</returns>
        public LearnerCourseCompletionStatus GetCourseCompletionStatus(int courseID, CourseCompletionPolicy courseCompletionPolicy, long learnerID, long enrollmentID, int QuizCount, DateTime registrationDate, double courseProgress, int courseApprovalID,int source)
        {
            //CourseConfiguration courseConfiguration = new CourseConfiguration();
            LearnerCourseCompletionStatus learnerCourseCompletionStatus = new LearnerCourseCompletionStatus();
            //List<LearnerStatistics> learnerStatistics = new List<LearnerStatistics>();
            int courseSceneCount = 0;
            //int postAssessmentNoOfCorrect = 0;
            //int postAssessmentNoOfInCorrect = 0;
            //int preAssessmentNoOfCorrect = 0;
            //int preAssessmentNoOfInCorrect = 0;
            bool postAssessmentAttempted = false;
            //int visitedSceneCount = 0;
            int courseVisitCount = 0;
            DateTime firstAccessDateTime = DateTime.Now;
            bool postAssessment_Passed = false;
            bool preAssessment_Passed = false;
            int visitedScenePercent = 0;
            int quiz_Count_Passed = 0;
            int quiz_Count_Required = 0;
            bool embedded_Acknowledgement_Required_And_Agreed = false;


            bool isCourseCompletedViaPostAssessment = false;
            bool isCourseCompletedViaPreAssessment = false;
            bool isCourseCompletedViaQuiz = false;
            bool isCourseCompletedViaViewEveryScene = false;
            bool isCourseCompletedViaUniqueVisit = false;
            bool isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime = false;
            bool isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration = false;
            bool isCourseCompleted = false;
            bool hasRespondedToCourseEvaluation = false;
            bool quiz_PassedTF;
            bool viewEverySceneInCourseTF;
            bool completeAfterUniqueVisitTF;
            bool completeAfterRegDateTF;
            bool completeAfterFirstAccessDateTF;

            //Abdus Samad LCMS-11888
            //START
            bool isCourseCompletedViaDocuSignAffidavit = false;
            bool isCourseCompletedViaDocuSignSigned = false;
            bool docusignAffidavit_Marked = false;
            bool docusignSigned_Marked = false;
            //END
            bool isCourseCompletedViaDocuSignDeclined = false;
            bool docusignSigned_Declined= false;
            try
            {

                StudentTrackingDA studentTrackingDA = new StudentTrackingDA();

                //courseConfiguration= studentTrackingDA.GetCourseCompletionConfiguration(courseID);
                studentTrackingDA.GetCourseCompletionData_Optimized(courseID, learnerID, enrollmentID, courseCompletionPolicy
                                                        , out postAssessment_Passed
                                                        , out preAssessment_Passed
                                                        , out postAssessmentAttempted
                                                        , out visitedScenePercent
                                                        , out courseVisitCount
                                                        , out firstAccessDateTime
                                                        , out hasRespondedToCourseEvaluation
                                                        , out quiz_Count_Passed
                                                        , out quiz_Count_Required
                                                        , out embedded_Acknowledgement_Required_And_Agreed
                                                        , out quiz_PassedTF
                                                        , out viewEverySceneInCourseTF
                                                        , out completeAfterUniqueVisitTF
                                                        , out completeAfterRegDateTF
                                                        , out completeAfterFirstAccessDateTF
                                                        , out docusignAffidavit_Marked
                                                        , out docusignSigned_Marked
                                                        , out docusignSigned_Declined
                                                        ,source);

                #region Course Completion Checking Logic

                //if (courseConfiguration.PreAssessmentConfiguration.ScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                //{
                //    courseConfiguration.CompletionPreAssessmentMastery = false;                  
                //}

                //if (courseConfiguration.PostAssessmentConfiguration.ScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                //{
                //    courseConfiguration.CompletionPostAssessmentMastery = false;
                //}

                //if (courseConfiguration.QuizConfiguration.ScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                //{
                //    courseConfiguration.CompletionQuizMastery = false;
                //}

                if (courseCompletionPolicy.PostAssessmentMasteryAchived)
                {
                    //isCourseCompletedViaPostAssessment=CalculateAssessmentResult(courseConfiguration.PostAssessmentMasteryScore,postAssessmentNoOfCorrect,postAssessmentNoOfInCorrect);
                    if (courseCompletionPolicy.PostAssessmentScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                    {
                        isCourseCompletedViaPostAssessment = false;
                    }
                    else
                    {
                        isCourseCompletedViaPostAssessment = postAssessment_Passed;
                    }
                }
                else if (courseCompletionPolicy.PostAssessmentAttempted)
                {
                    isCourseCompletedViaPostAssessment = postAssessmentAttempted;
                }
                if (courseCompletionPolicy.PreAssessmentMasteryAchived)
                {
                    //isCourseCompletedViaPreAssessment = CalculateAssessmentResult(courseConfiguration.PreAssessmentMasteryScore, preAssessmentNoOfCorrect,  preAssessmentNoOfInCorrect);
                    if (courseCompletionPolicy.PreAssessmentScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                    {
                        isCourseCompletedViaPreAssessment = false;
                    }
                    else
                    {
                        isCourseCompletedViaPreAssessment = preAssessment_Passed;
                    }
                }
                if (courseCompletionPolicy.QuizMasteryAchived)
                {
                    if (courseCompletionPolicy.QuizScoreType.ToUpper().Equals(ScoreType.NoResults.ToUpper()))
                    {
                        isCourseCompletedViaQuiz = false;
                    }
                    else
                    {
                        //foreach (LearnerStatistics learnerStatistic in learnerStatistics)
                        //{
                        //    isCourseCompletedViaQuiz = CalculateAssessmentResult(courseConfiguration.QuizMasteryScore, learnerStatistic.NumberAnswersCorrect, learnerStatistic.NumberAnswersIncorrect);
                        //    if (isCourseCompletedViaQuiz == false)
                        //        break;
                        //}
                        if (quiz_Count_Passed >= QuizCount || quiz_PassedTF)
                        {
                            isCourseCompletedViaQuiz = true;
                        }
                        else
                        {
                            isCourseCompletedViaQuiz = false;
                        }
                    }
                }
                if (courseCompletionPolicy.ViewEverySceneInCourse)
                {
                    if (courseProgress >= 100 || viewEverySceneInCourseTF)
                        isCourseCompletedViaViewEveryScene = true;
                }
                if (courseCompletionPolicy.CompleteAfterNumberOfUniqueVisits > 0)
                {
                    if (courseVisitCount >= courseCompletionPolicy.CompleteAfterNumberOfUniqueVisits || completeAfterUniqueVisitTF)
                        isCourseCompletedViaUniqueVisit = true;
                }
                if (courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime > 0)
                {
                    DateTime maximumDateTime = DateTime.Now;
                    switch (courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTimeUnit)
                    {
                        case TimeUnit.Minutes:
                            maximumDateTime = firstAccessDateTime.AddMinutes(courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime);
                            break;
                        case TimeUnit.Months:
                            maximumDateTime = firstAccessDateTime.AddMonths(courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime);
                            maximumDateTime = new DateTime(maximumDateTime.Year, maximumDateTime.Month, maximumDateTime.Day, 23, 59, 59);
                            break;
                        case TimeUnit.Days:
                            maximumDateTime = firstAccessDateTime.AddDays(courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime);
                            maximumDateTime = new DateTime(maximumDateTime.Year, maximumDateTime.Month, maximumDateTime.Day, 23, 59, 59);
                            break;
                        default:
                            maximumDateTime = firstAccessDateTime.AddMinutes(courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime);
                            break;
                    }
                    if (maximumDateTime > DateTime.Now || completeAfterFirstAccessDateTF)
                    {
                        isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime = true;
                    }
                }

                if (courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfDayAfterRegistration > 0)
                {
                    DateTime maximumDateTime = DateTime.Now;
                    maximumDateTime = registrationDate.AddDays(courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfDayAfterRegistration);
                    maximumDateTime = new DateTime(maximumDateTime.Year, maximumDateTime.Month, maximumDateTime.Day, 23, 59, 59);

                    if (maximumDateTime > DateTime.Now || completeAfterRegDateTF)
                    {
                        isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration = true;
                    }
                }


                bool docuSignAffidavitPolicy = false;
                bool docuSignSignedPolicy = false;
                bool docuSignDeclinedPolicy = false;
                             
                int affidivatID = studentTrackingDA.GetCourseApprovalAffidavitForTrackingService(courseID, courseApprovalID);
                
                _360Training.BusinessEntities.Asset asset = new  _360Training.BusinessEntities.Asset();

                asset = studentTrackingDA.GetAffidavitAssetForTrackingService(affidivatID);
           
                if (affidivatID > 0 && asset.AssetType == AssetType.Affidavit)
                {
                    docuSignAffidavitPolicy = true;
                    if (docusignAffidavit_Marked)
                    {
                        isCourseCompletedViaDocuSignAffidavit = true;
                    }

                }

                if (asset.AffidavitTemplateId > 0)
                {
                    docuSignSignedPolicy = true;
                    docuSignDeclinedPolicy = true;
                    if (docusignSigned_Marked)
                    {
                        isCourseCompletedViaDocuSignSigned = true;

                    }
                    else 
                        if(docusignSigned_Declined)
                        {
                            isCourseCompletedViaDocuSignDeclined = true;
                        }

                }



                if (
                    (isCourseCompletedViaPostAssessment || (!courseCompletionPolicy.PostAssessmentMasteryAchived && !courseCompletionPolicy.PostAssessmentAttempted))
                    && (isCourseCompletedViaPreAssessment || !courseCompletionPolicy.PreAssessmentMasteryAchived)
                    && (isCourseCompletedViaQuiz || !courseCompletionPolicy.QuizMasteryAchived)
                    && (isCourseCompletedViaViewEveryScene || !courseCompletionPolicy.ViewEverySceneInCourse)
                    && (isCourseCompletedViaUniqueVisit || courseCompletionPolicy.CompleteAfterNumberOfUniqueVisits <= 0)
                    && (hasRespondedToCourseEvaluation || !courseCompletionPolicy.RespondToCourseEvaluation)
                    && (isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime || courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfTime <= 0)
                    && (isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration || courseCompletionPolicy.MustCompleteWithInSpecifiedAmountOfDayAfterRegistration <= 0)
                    && (embedded_Acknowledgement_Required_And_Agreed || !courseCompletionPolicy.EnableEmbeddedAknowledgement)
                    && (isCourseCompletedViaDocuSignAffidavit || !docuSignAffidavitPolicy)
                    && ((isCourseCompletedViaDocuSignSigned || !docuSignSignedPolicy)
                    || (isCourseCompletedViaDocuSignDeclined ||!docuSignDeclinedPolicy))
                    )
                {
                   

                    isCourseCompleted = true;
                }
                #endregion

                bool isSaved = studentTrackingDA.UpdateCourseCompletionStatistic(enrollmentID, postAssessmentAttempted, isCourseCompletedViaPostAssessment
    , isCourseCompletedViaPreAssessment, isCourseCompletedViaQuiz, isCourseCompletedViaViewEveryScene
    , isCourseCompletedViaUniqueVisit, hasRespondedToCourseEvaluation, isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime
    , isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration, embedded_Acknowledgement_Required_And_Agreed
    , isCourseCompleted,learnerID,courseID,source);

                learnerCourseCompletionStatus.IsCompleteAfterNOUniqueCourseVisitAchieved = isCourseCompletedViaUniqueVisit;
                learnerCourseCompletionStatus.IsCourseCompleted = isCourseCompleted;
                learnerCourseCompletionStatus.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate = isCourseCompletedViaCompletedWithinSpecifiedAmountOfDayFromRegistration;
                learnerCourseCompletionStatus.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess = isCourseCompletedViaCompletedWithinSpecifiedAmountOfTime;
                learnerCourseCompletionStatus.IsPostAssessmentAttempted = postAssessmentAttempted;
                learnerCourseCompletionStatus.IsPostAssessmentMasteryAchieved = isCourseCompletedViaPostAssessment;
                learnerCourseCompletionStatus.IsPreAssessmentMasteryAchieved = isCourseCompletedViaPreAssessment;
                learnerCourseCompletionStatus.IsQuizMasteryAchieved = isCourseCompletedViaQuiz;
                learnerCourseCompletionStatus.IsRespondToCourseEvaluationAchieved = hasRespondedToCourseEvaluation;
                learnerCourseCompletionStatus.IsViewEverySceneInCourseAchieved = isCourseCompletedViaViewEveryScene;
                learnerCourseCompletionStatus.IsembeddedAcknowledgmentAchieved = embedded_Acknowledgement_Required_And_Agreed;

                studentTrackingDA = null;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }
            return learnerCourseCompletionStatus;
        }
        /// <summary>
        /// This method calculate assessment result.
        /// </summary>
        /// <param name="masteryScore">MasteryScore integer vale, policy variable</param>
        /// <param name="numberOfCorrect">NumberOfCorrect integer value, represent the number of question which was correctly answered</param>
        /// <param name="numberOfInCorrect">NumberOfInCorrect integer value, represent the number of question which was incorrectly answered</param>
        /// <returns>Boolean value true if pass,false if fails or if unable to calculate</returns>
        private bool CalculateAssessmentResult(int masteryScore, int numberOfCorrect, int numberOfInCorrect)
        {
            int totalQuestions = numberOfCorrect + numberOfInCorrect;
            if (totalQuestions <= 0)
                return false;
            if ((numberOfCorrect / Convert.ToDecimal(totalQuestions)) * 100 >= masteryScore)
                return true;
            else
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
        public bool InsertLearningSession(string courseGUID, int learnerID, int enrollmentID, DateTime startTime, string uniqueUserGUID, string learningSessionGUID, int sourceVU, int brandingID, int languageID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.SaveLearningSession(courseGUID, learnerID, enrollmentID, startTime, uniqueUserGUID, learningSessionGUID, sourceVU, brandingID, languageID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        /// <summary>
        /// Thhis method gets the source of courseplayer execution
        /// </summary>
        /// <param name="learningSessionGUID">string </param>
        /// <returns>returns int sourceID</returns>
        public int GetSource(string learningSessionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetSource(learningSessionGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        /// <summary>
        /// This method gets the brandcode and variant by learning sessionID
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        public void GetLearningSessionBrandcodeVariant(string learningSessionGUID, out string brandCode, out string variant)
        {
            try
            {
                brandCode = string.Empty;
                variant = string.Empty;

                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    studentTrackingDA.GetLearningSessionBrandcodeVariant(learningSessionGUID, ref brandCode, ref variant);
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
        public void GetEnrollmentIDBrandcodeVariant(int enrollmentID, out string brandCode, out string variant)
        {
            try
            {
                brandCode = string.Empty;
                variant = string.Empty;

                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    studentTrackingDA.GetEnrollmentIDBrandcodeVariant(enrollmentID, ref brandCode, ref variant);
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
            int returnValue = 0;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    returnValue = studentTrackingDA.SaveLearnerValidationStatistics(learnerValidationStatistics);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = 0;
            }
            return returnValue;
        }
        /// <summary>
        /// This method gets the course type of the course associated with a learning sesison
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <returns>string CourseType</returns>
        public string GetLearningSessionCourseType(string learningSessionGUID, out string url)
        {
            try
            {
                url = string.Empty;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearningSessionCourseType(learningSessionGUID, ref url);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                url = string.Empty;
                return string.Empty;
            }
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
        public void GetInfoForLMSVUConnector(string learningSessionGUID, out string emailAddress, out string firstName, out string lastName, out string courseGUID, out int epoch, out int learnerID
                                             , out string phone, out string officePhone, out string streetAddress, out string city, out string zipCode, out string state, out string country, out string middleName, out string userName)
        {
            emailAddress = string.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
            courseGUID = string.Empty;
            epoch = 0;
            learnerID = 0;
            phone = string.Empty;
            officePhone = string.Empty;
            streetAddress = string.Empty;
            city = string.Empty;
            zipCode = string.Empty;
            state = string.Empty;
            country = string.Empty;
            middleName = string.Empty;
            userName = string.Empty;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    studentTrackingDA.GetInfoForLMSVUConnector(learningSessionGUID, ref emailAddress, ref firstName, ref lastName, ref courseGUID, ref epoch, ref learnerID
                                                                , ref phone, ref officePhone, ref streetAddress, ref city, ref zipCode, ref state, ref country, ref  middleName, ref userName);
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
                learnerID = 0;
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
        /// This method updates the answer of learner validation statistics
        /// </summary>
        /// <param name="answerText">string answer text</param>
        /// <returns>return true if successfull else false</returns>
        public bool UpdateValidationLearnerStatisticsAnswer(int learnerValidationStatisticsID, string answerText, bool isCorrect, bool isAnswered, DateTime saveTime)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateValidationLearnerStatisticsAnswer(learnerValidationStatisticsID, answerText, isCorrect, isAnswered, saveTime);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }


        /// <summary>
        /// This method will return the Primary Key associated with learning Session guid passed as string param.
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        public int GetLearningSessionID(string learningSessionId)
        {


            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearningSessionID(learningSessionId);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// This method will return the Primary Key associated with learning Session guid passed as string param.
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        public string GetLearningSessionID(int learningSessionId)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearningSessionGuid(learningSessionId);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }
        }

        /// <summary>
        /// This method returns the validation statistics of a particular enrollment
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <returns>list of LearnerValidationStatistics</returns>
		public List<LearnerValidationStatistics> GetCurrentLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, out int minutesSinceLastValidation)
        {
            try
            {
                minutesSinceLastValidation = 0;
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
					return studentTrackingDA.GetCurrentLearnerValidationStatistics(enrollmentID, courseId, learnerId, source, ref minutesSinceLastValidation);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                minutesSinceLastValidation = 0;
                return null;
            }
        }
        public string GetLearningSessionResourceValueOfKey(string learningSessionID, string resourceKey)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearningSessionResourceValueOfKey(learningSessionID, resourceKey);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string GetResourceValueOfResourceKey(string brandCode, string variant, string resourceKey)
        {

            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetResourceValueOfResourceKey(brandCode, variant, resourceKey);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        public List<LearnerStatistics> GetPreviousLearnerStatistics(string learningSessionID, string currentItemGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetPreviousLearnerStatistics(learningSessionID, currentItemGUID);
                }
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
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearningSessionRedirectURL(learningSessionID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="COurse ID">Int learnerID</param>
        /// <param name="Learner ID">Int learnerID</param>
        /// <param name="CourseName">string CourseName</param>
        /// <param name="ApprovedCourseHours">string ApprovedCourseHours</param>
        /// <param name="Completion Date">string Completion Date</param>        

        public void GetLearnerCourseMetaCertificateInfo(int Course_ID, int Learne_ID, int Enrollment_ID, out string CourseName, out string ApprovedCourseHours, out DateTime CompletionDate, out string FirstName, out string LastName, out string CertificateNumber, out DateTime CertificateIssueDate)
        {
            CourseName = string.Empty;
            ApprovedCourseHours = string.Empty;
            CompletionDate = DateTime.MinValue;
            FirstName = string.Empty;
            LastName = string.Empty;
            CertificateNumber = string.Empty;
            CertificateIssueDate = DateTime.MinValue;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    studentTrackingDA.GetLearnerCourseMetaCertificateInfo(Course_ID, Learne_ID, Enrollment_ID, ref CourseName, ref ApprovedCourseHours, ref CompletionDate, ref FirstName, ref LastName, ref CertificateNumber, ref CertificateIssueDate);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                CourseName = string.Empty;
                ApprovedCourseHours = string.Empty;
                CompletionDate = DateTime.MinValue;
                FirstName = string.Empty;
            }
        }

        /// <summary>
        /// Thhis method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        public bool IsCourseCompleted(int enrollmentID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerCourseStatisticsCompleted(enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// Thhis method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        public bool GetDocuSignStatusByEnrollmentId(int enrollmentID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetDocuSignStatusByEnrollmentId(enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }




        /// <summary>
        /// This method gets course time spend based on enrollment ID
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>total seconds</returns>
        public int GetLearnerTimeSpent(int enrollmentID, string learningSessionGuid)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
					return studentTrackingDA.GetLearnerTimeSpent(enrollmentID, learningSessionGuid);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }

		public int GetLearnerTimeSpentByTime(int enrollmentID, DateTime startTime, DateTime endTime, string learningSessionGuid)
        {
            try
            {

                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerTimeSpentByTime(enrollmentID, startTime, endTime, learningSessionGuid);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return -1;
            }
        }


        #region Course unlocking
        public LearnerProfile GetUserProfile(string TransactionGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetUserProfile(TransactionGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public bool UpdateLearnerProfile(string TransactionGUID, LearnerProfile profile)
        {
            bool result;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    result = studentTrackingDA.UpdateLearnerProfile(TransactionGUID, profile);
                    if (result)
                    {
                        UnlockLockedCourse(TransactionGUID, profile);
                    }
                }
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
            bool result;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    result = studentTrackingDA.UpdateCompletedLearnerProfile(emailAddress, firstName, lastName, learnerID, phone, officePhone, streetAddress, city, zipCode, state, country, middleName);

                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                result = false;

            }
            return result;
        }
        private bool UnlockLockedCourse(string TransactionGUID, LearnerProfile profile)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    bool isCourseUnlocked = studentTrackingDA.UnlockLockedCourse(TransactionGUID);

                    if (isCourseUnlocked == true)
                    {
                        String username, pwd, compnayname, website;
                        username = string.Empty;
                        pwd = string.Empty;
                        compnayname = string.Empty;
                        website = string.Empty;
                        studentTrackingDA.GetLearnerCredential(TransactionGUID, profile);
                        string mailBody = CreateBody(profile);
                        isCourseUnlocked = SendEmail(profile.EmailAddress, ConfigurationManager.AppSettings["FromEmailValidationUnlock"], ConfigurationManager.AppSettings["BccEmailValidationUnlock"], "Course Unlock Request", mailBody);
                        // SendEmail("saeeda.riaz@360training.com", ConfigurationManager.AppSettings["FromEmailValidationUnlock"], ConfigurationManager.AppSettings["BccEmailValidationUnlock"], "Course Unlock Request", mailBody);
                    }
                    return isCourseUnlocked;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public bool UnlockLockedCourse(int learnerID, string courseGUID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.UpdateLatestLockedCourse(learnerID, courseGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }
        #endregion

        #region emailing
        public string CreateBody(LearnerProfile profile)
        {



            string mailBody = @"<table ><tr><td><b>PERSONAL INFORMATION</b></td><td> &nbsp;</td><td>&nbsp;</td> </tr>";
            mailBody = mailBody + @"<tr><td><b>First Name: </b>" + profile.FirstName + "</td><td><b>Last Name: </b>" + profile.LastName + "</td><td>   &nbsp;</td></tr>";
            mailBody = mailBody + @"<tr><td><b>Address1: </b>" + profile.Address1 + "</td><td><b>Address2: </b>" + profile.Address2 + "</td><td><b>Address3: </b>" + profile.Address3 + "</td></tr>";
            mailBody = mailBody + @"<tr> <td><b>Zipcode: </b>" + profile.ZipCode + "</td><td><b>City: </b>" + profile.City + "</td><td><b>Country: </b>" + profile.Country + "</td></tr>";
            mailBody = mailBody + @" <tr><td><b>Phone: </b>" + profile.OfficePhone + "</td><td><b>Email address: </b>" + profile.EmailAddress + "</td><td>&nbsp;</td></tr>";
            mailBody = mailBody + @" <tr><td><br/><b>ACCOUNT INFORMATION</b></td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            mailBody = mailBody + @"<tr><td><b>Username: </b>" + profile.Username + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            mailBody = mailBody + @"<tr><td><b>Password: </b>" + profile.Password + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            mailBody = mailBody + @"<tr><td><b>Client VU link:</b> " + profile.Website + "</td><td></td><td></td></tr>";
            mailBody = mailBody + @"<tr><td><br/>Sincerely,<br />&nbsp;" + profile.CompanyName + " </td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            mailBody = mailBody + @"</table>      ";
            return mailBody;

        }
        public bool SendEmail(string ToEmail, string FromEmail, string BCCEmail, string Subject, string MailBody)
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
                mailMessage.Bcc.Add(BCCEmail);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #endregion

        #region Course Evaluation

        public bool SaveCourseEvaluationResult(CourseEvaluationResult courseEvaluationResult)
        {
            bool returnValue = false;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    int surveyResutID = studentTrackingDA.SaveSurveyResult(courseEvaluationResult);
                    if (surveyResutID > 0)
                    {
                        foreach (CourseEvaluationResultAnswer courseEvaluationResultAnswer in courseEvaluationResult.CourseEvaluationResultAnswers)
                        {
                            studentTrackingDA.SaveSurveyResultAnswer(courseEvaluationResultAnswer, surveyResutID);
                        }
                        return true;
                    }
                    else
                        return false;

                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }
        public bool IsCourseEvaluationAttempted(int courseID, int learnerID, int learningSessionID, string surveyType)
        {
            bool returnValue;
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    returnValue = studentTrackingDA.GetCourseEvaluationResultID(courseID, learnerID, learningSessionID, surveyType);

                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                returnValue = false;
            }
            return returnValue;
        }
        #endregion

        #region Legacy Connector
        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORSCORM(int courseID, int studentID, int Epoch)
        {
            try
            {

                using (LegacyStudentTrackingDA legacystudentTrackingDA = new LegacyStudentTrackingDA())
                {
                    return legacystudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORSCORM(courseID, studentID, Epoch);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public LegacyCertificateInfo GET_LCMS_CONNECTOR_STATS_CERTIFICATE(int courseID, int studentID, int Epoch)
        {
            try
            {
                using (LegacyStudentTrackingDA legacystudentTrackingDA = new LegacyStudentTrackingDA())
                {
                    return legacystudentTrackingDA.GET_LCMS_CONNECTOR_STATS_CERTIFICATE(courseID, studentID, Epoch);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP3(int courseID, int studentID, int Epoch)
        {
            try
            {
                using (LegacyStudentTrackingDA legacystudentTrackingDA = new LegacyStudentTrackingDA())
                {
                    return legacystudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORICP3(courseID, studentID, Epoch);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP2(int courseID, int studentID, int Epoch)
        {
            try
            {
                using (LegacyStudentTrackingDA legacystudentTrackingDA = new LegacyStudentTrackingDA())
                {
                    return legacystudentTrackingDA.GET_LCMS_CONNECTOR_STATS_FORICP2(courseID, studentID, Epoch);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public void UPDATE_LCMS_STUDENTCOURSE(int courseID, int studentID, int Epoch, string LearningSessionGuid, int TestingId, int status)
        {
            try
            {
                using (LegacyStudentTrackingDA legacystudentTrackingDA = new LegacyStudentTrackingDA())
                {
                    legacystudentTrackingDA.UPDATE_LCMS_STUDENTCOURSE(courseID, studentID, Epoch, LearningSessionGuid, TestingId, status);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
            }
        }
        #endregion

        public int SaveAssessmentScore(LearnerStatistics learnerStatistics, int mastoryScore)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveAssessmentScore(learnerStatistics, mastoryScore);
            }
        }

        public int SaveAssessmentScore_Game(LearnerStatistics learnerStatistics, int mastoryScore)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveAssessmentScore_Game(learnerStatistics, mastoryScore);
            }
        }

        public bool SaveAssessmentItem(LearnerStatistics learnerStatistics)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveAssessmentItem(learnerStatistics);
            }

        }


        public bool UpdateLearnerCourseStatistics(long enrollmentId, string certificateURL, bool isCompleted)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.UpdateLearnerCourseStatistics(enrollmentId, certificateURL, isCompleted);
            }
        }

        public int AuthenticateProctor(long courseID, long learnerID, string learningSessionID, string proctorLogin, string proctorPassword)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.AuthenticateProctor(courseID, learnerID, learningSessionID, proctorLogin, proctorPassword);
            }
        }

        public int AuthenticateSpecialPostAssessmentValidation(long courseID, long learnerID, string learningSessionID, string DRELicenseNumber, string DriverLicenseNumber)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.AuthenticateSpecialPostAssessmentValidation(courseID, learnerID, learningSessionID, DRELicenseNumber, DriverLicenseNumber);
            }
        }

        public int AuthenticateNYInsuranceValidation(long courseID, long learnerID, string learningSessionID, string MonitorNumber)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.AuthenticateNYInsuranceValidation(courseID, learnerID, learningSessionID, MonitorNumber);
            }
        }

        public bool ProctorLoginRequirementCriteriaMeets(string learningSessionGuid)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.ProctorLoginRequirementCriteriaMeets(learningSessionGuid);
            }
        }

        public string AuthenticateMMAPP(string userName, string password, string courseGuid)
        {

            //  return "7be655e0-2616-4029-b982-970d335dc179";

            string userGuid = "";
            string returnValue = "";
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                userGuid = studentTrackingDA.GetLearnerGuid(userName);
                password = LCMS.Common.PasswordSHA1Encryption.Encrypt(password, userGuid);
                returnValue = studentTrackingDA.AUTHENTICATE_MMAPP(userName, password, courseGuid);
            }

            return returnValue;
        }



        public bool UpdateStatisticsForFlashCourse(string lsGuid, double score, bool isCompleted, bool isPass, int assessmentAttemptNo)
        {
            try
            {
                string lsAndEnrollmentIds = "";
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    lsAndEnrollmentIds = studentTrackingDA.InsertBasicStatisticsForFlashCourseAndReturnLearningSessionID(lsGuid, score, isCompleted, isPass, assessmentAttemptNo);
                    return true;
                    /*
                    if (lsAndEnrollmentIds != "")
                    {
                        int learnerEnrollmentID = Convert.ToInt32(lsAndEnrollmentIds.Split(',')[0]);
                        int learningsessionID = Convert.ToInt32(lsAndEnrollmentIds.Split(',')[1]);
                        

                        LearnerStatistics learnerStatistics = new LearnerStatistics();
                        learnerStatistics.AssessmentType = "PostAssessment";
                        learnerStatistics.RawScore = score;
                        learnerStatistics.CorrectTF = isPass;
                        learnerStatistics.IsPass = learnerStatistics.CorrectTF;
                        learnerStatistics.LearningSession_ID = learningsessionID;
                        learnerStatistics.LearnerEnrollment_ID = learnerEnrollmentID;
                        //learnerStatistics.NumberAnswersCorrect = score;
                        //learnerStatistics.NumberAnswersIncorrect = 100-score;
                        SaveLearnerStatisticsWithStatisticDateTime(learnerStatistics, DateTime.Now);
                        return true;
                    }
                    */
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
            return false;
        }


        // LCMS-9213
        public int GetAssessmentTimeForAllSessions(string learningSessionGuid, string assessmentType, int contentObjectID, int examID, string type, int assessmentConfigurationID)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.GetAssessmentTimeForAllSessions(learningSessionGuid, assessmentType, contentObjectID, examID, type, assessmentConfigurationID);
            }
        }
        //LCMS-10266
        public bool ResetAssessmentItemStatistics(string learningSessionGuid, string statisticsType, string assessmentType, string itemGUID, int attemptNumber, int remediationCount)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.ResetAssessmentItemStatistics(learningSessionGuid, statisticsType, assessmentType, itemGUID, attemptNumber, remediationCount);
            }

        }
        //End- LCMS-10266
        //LCMS-12532 Yasin
        public bool GetValidationIdendityQuestions(int learnerID)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.GetValidationIdendityQuestions(learnerID);
            }
        }
        
        
        //LCMS-12532 Yasin
        
        public bool SaveValidationIdendityQuestions(int QS1, string Answer1, int QS2, string Answer2, int QS3, string Answer3, int QS4, string Answer4, int QS5, string Answer5, int learnerID, int QuestionSet1, int QuestionSet2, int QuestionSet3, int QuestionSet4, int QuestionSet5)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveValidationIdendityQuestions(QS1, Answer1, QS2, Answer2, QS3, Answer3, QS4, Answer4, QS5, Answer5, learnerID, QuestionSet1, QuestionSet2, QuestionSet3, QuestionSet4, QuestionSet5);
            }
        }
        
        //LCMS-10877
        public int SaveCourseRating(int CourseID, int Rating, int EnrollmentID, out string CourseGuid, out double AvgRating, out int TotalRating)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveCourseRating(CourseID, Rating, EnrollmentID, out CourseGuid, out AvgRating, out TotalRating);
            }
        }

        public CourseRating SaveCourseRatingNPS(CourseRating courseRating)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveCourseRatingNPS(courseRating);
            }
        }

        public int GetUserCourseRating(int CourseID, int EnrollmentID)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.GetUserCourseRating(CourseID, EnrollmentID);
            }
        }

        public CourseRating GetUserCourseRatingNPS(int CourseID, int EnrollmentID)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.GetUserCourseRatingNPS(CourseID, EnrollmentID);
            }
        }
        //LCMS-10877


        #endregion
        //

        //LCMS-11974
        //Abdus Samad
        //Start
        public LearnerProfile GetUserProfileInformation(int enrollmentID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetUserProfileInformation(enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        //Stop

        public LearnerProfile GetLearnerInformationForMarketo(int learnerID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetLearnerInformationForMarketo(learnerID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public bool SavePlayerMarketoLog(string eventTYPE, int enrollmentID, string packetJSON)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SavePlayerMarketoLog(eventTYPE, enrollmentID, packetJSON);
            }
        }

        

        //LCMS-12502
        //Abdus Samad Start
        public bool SaveDocuSignRoleAgainstLearnerEnrollment(string envelopId ,int learnerEnrollmentID, string roleName)
        {
            using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
            {
                return studentTrackingDA.SaveDocuSignRoleAgainstLearnerEnrollment(envelopId, learnerEnrollmentID,roleName);
            }
        }

        public bool SaveEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID,string roleName)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.SaveEnvelopStatusAgainstDocuSignRole(learnerEnrollmentID, roleName);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        }


        public bool GetEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetEnvelopStatusAgainstDocuSignRole(learnerEnrollmentID, roleName);
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        }
        //Stop


        public LearningSessionInformation CheckIfLearningSessionOpen(string LearningSessionID)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.CheckIfLearningSessionOpen(LearningSessionID);
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return null;
            }
        }
        //Stop
        public string GetCourseIDAgainstLearningSessionGUID(string LearningSessionGuid)
        {
            try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetCourseIDAgainstLearningSessionGUID(LearningSessionGuid);
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return null;
            }
        }


        public int GetTotalTimeSpent(string LearningSessionGuid, int EnrollmentID)
        {
         try
            {
                using (StudentTrackingDA studentTrackingDA = new StudentTrackingDA())
                {
                    return studentTrackingDA.GetTotalTimeSpent(LearningSessionGuid, EnrollmentID);
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        
        
        }
    }

}
