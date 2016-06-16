using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using _360Training.BusinessEntities;
using _360Training.TrackingServiceBusinessLogic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Configuration;
using CommonAPI.Utility;
using System.Net;
using ICP4.BusinessLogic.IntegerationManager;

namespace _360Training.TrackingService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.360training.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TrackingService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// This method saves the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int curseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="sequenceNo">int sequenceNo</param>
        /// <returns>returns true if success,else false</returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public bool SaveStudentCourseTrack(int courseID, int learnerID, int sequenceNo)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveStudentCourseTrack(courseID, learnerID, sequenceNo);
            }
        }
        /// <summary>
        /// This method gets the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>int sequenceNo.</returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetStudentCourseTrack(int courseID, int learnerID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetStudentCourseTrack(courseID,learnerID);
            }
        }
        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        [WebMethod]
        public bool EndSession(string sessionID, DateTime endTime, int totalTimeSpent, double percentageCourseProgress,long enrollment_Id)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.EndSession(sessionID, endTime, totalTimeSpent, percentageCourseProgress, enrollment_Id);
            }
        }

        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        [WebMethod]
        public bool EndSessionLegacy(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.EndSessionLegacy(sessionID, endTime,MAXPERCENTAGECOURSEATTENDED);
            }
        }

        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        [WebMethod]
        public bool EndSessionExternal(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.EndSessionLegacy(sessionID, endTime, MAXPERCENTAGECOURSEATTENDED);
            }
        }

        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        [WebMethod]
        public bool EndSessionLegacyScorm(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED, bool PASSEDASSESSMENT, decimal RAWSCORE)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.EndSessionLegacyScorm(sessionID, endTime, MAXPERCENTAGECOURSEATTENDED, PASSEDASSESSMENT, RAWSCORE);
            }
        }

        ///// <summary>
        ///// This method saves the learner statistics
        ///// </summary>
        ///// <param name="learnerStatistics">learnerStatistics object</param>
        ///// <returns>true if successfull,else false</returns>
        //[WebMethod]
        //public bool SaveLearnerStatistics()
        //{
        //    LearnerStatistics learnerStatistics = new LearnerStatistics();
        //    List<LearnerStatisticsAnswer> learnerStatisticsAnswers=new List<LearnerStatisticsAnswer>();
        //    learnerStatisticsAnswers.Add(new LearnerStatisticsAnswer());
        //    learnerStatistics.LearnerStatisticsAnswers = learnerStatisticsAnswers;
        //    using (TrackingManager trackingManager = new TrackingManager())
        //    {
        //        return trackingManager.SaveLearnerStatistics(learnerStatistics);
        //    }
        //}
        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        [WebMethod]
        public bool SaveLearnerStatistics(LearnerStatistics learnerStatistics)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveICP4LearnerStatistics(learnerStatistics);
            }
        }

        [WebMethod]
        public bool SaveLearnerStatistics_Scene(LearnerStatistics learnerStatistics)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveLearnerStatistics_Scene(learnerStatistics);
            }
        }

        [WebMethod]
        public bool SaveLearnerStatisticsWithStatisticDateTime(LearnerStatistics learnerStatistics, DateTime StatisticDateTime)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveLearnerStatisticsWithStatisticDateTime(learnerStatistics, StatisticDateTime);
            }

        }

        [WebMethod]
        public bool SaveLearnerStatisticsFromLegacyVU(LearnerStatistics learnerStatistics, LearnerStatisticsAnswer learnerStatisticsAnswer)
        {
            learnerStatistics.LearnerStatisticsAnswers = new List<LearnerStatisticsAnswer>();
            learnerStatistics.LearnerStatisticsAnswers.Add(learnerStatisticsAnswer);

            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveLearnerStatistics(learnerStatistics);
            }

            return true;
        }

        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        [WebMethod]
        public int GetLearningSessionID(string learningSessionId)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearningSessionID(learningSessionId);
                //trackingManager.SaveLearnerStatistics(learnerStatistics);
                
            }
        }

        /// <summary>
        /// This method returns the learner course track info object
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        [WebMethod]
        public LearnerCourseTrackInfo GetLearnerCoursetrackInfo(string sessionID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearnerCoursetrackInfo(sessionID);
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
        [WebMethod]
        public bool SaveLearnerCourseBookmark(int courseID, int learnerID, int enrollmentID, string item_GUID, string sceneGUID, string flashSceneNo, string bookMarkTitle, string lastScene, bool isMovieEnded, bool nextButtonState, string firstSceneName, DateTime createddate)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveLearnerCourseBookmark(courseID, learnerID, enrollmentID, item_GUID, sceneGUID, flashSceneNo, bookMarkTitle, lastScene, isMovieEnded, nextButtonState, firstSceneName, createddate);
            }
        }
        /// <summary>
        /// This method returns the LearnerCourseBookmarkInfo object
        /// </summary>
        /// <param name="bookMarkID">int learnerCourseBookMarkID</param>
        /// <returns>BookMarkInfo object</returns>
        [WebMethod]
        public LearnerCourseBookMarkInfo GetLearnerCourseBookMarkInfo(int learnerCourseBookMarkID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearnerCourseBookMarkInfo(learnerCourseBookMarkID);
            }
        }

        /// <summary>
        /// This method returns all LearnerCourseBookmarkInfo objects belonging to a particular course
        /// and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>BookMarkInfo object</returns>
        [WebMethod]
        public List<LearnerCourseBookMarkInfo> GetAllLearnerCourseBookMarksInfo(int courseID, int learnerID,int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetAllLearnerCourseBookMarksInfo(courseID,learnerID,enrollmentID);
            }
        }

        /// <summary>
        /// This method delete the bookmarks information against the bookmarkID
        /// and learner
        /// </summary>
        /// <param name="bookmarkID">int bookmarkID</param>        
        /// <returns>BookMarkInfo object</returns>
        [WebMethod]
        public bool DeleteLearnerCourseBookMarksInfo(int bookmarkID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.DeleteLearnerCourseBookMarksInfo(bookmarkID);
            }
        }
        /// <summary>
        /// This method gets all LeanerCourseTrackInfo records related to a a particular
        /// course and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        [WebMethod]
        public List<LearnerCourseTrackInfo> GetAllLearnerCourseTrackInfo(int courseID, int learnerID,int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetAllLearnerCourseTrackInfo(courseID, learnerID,enrollmentID);
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the assessment 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="statisticsType">string statisticsType</param>
        /// <param name="assessmentType">string assessmentType</param>
        /// <returns>list of learnerstaistics object</returns>
        [WebMethod]
        public List<LearnerStatistics> GetPreviouslyAskedQuestions(string learnerSessionGUID, string assessmentType, int remediationCount, int examID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPreviouslyAskedQuestions(learnerSessionGUID, assessmentType,remediationCount, examID);
            }
        }

        [WebMethod]
        public List<LearnerStatistics> GetPreviouslyAskedQuestionsQuiz(string learnerSessionGUID, string assessmentType, int remediationCount,int contentObjectId)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPreviouslyAskedQuestionsQuiz(learnerSessionGUID, assessmentType, remediationCount, contentObjectId);
            }
        }
        /// <summary>
        /// This method returns the preassessmentAttemp no
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetPreAssessmentAttemptNo(string learnerSessionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPreAssessmentAttemptNo(learnerSessionGUID);
            }
        }
        /// <summary>
        /// This method returns the postassessmentAttemp no
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetPostAssessmentAttemptNo(string learnerSessionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPostAssessmentAttemptNo(learnerSessionGUID);
            }
        }
        /// <summary>
        /// This method returns the quizAttempt no
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>int attemptNo</returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public int GetQuizAssessmentAttemptNo(string learnerSessionGUID,string contentObjectGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetQuizAttemptNo(learnerSessionGUID,contentObjectGUID);
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the preassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        [WebMethod]
        public List<LearnerStatistics> GetPreAssessmentResult(string learnerSessionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPreAssessmentResult(learnerSessionGUID);
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the postassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        [WebMethod]
        public List<LearnerStatistics> GetPostAssessmentResult(string learnerSessionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPostAssessmentResult(learnerSessionGUID);
            }
        }
        /// <summary>
        /// This method returns the learnerstatistics records of the quizresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="contentObjectID">string contentObjectGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        [WebMethod]
        public List<LearnerStatistics> GetQuizResult(string learnerSessionGUID, string contentObjectGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetQuizResult(learnerSessionGUID,contentObjectGUID);
            }
        }

        /// <summary>
        /// This method returns the learnerstatistics records of the Practice Exam result 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="contentObjectID">string contentObjectGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        [WebMethod]
        public List<LearnerStatistics> GetPracticeExamResult(string learnerSessionGUID, string contentObjectGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPracticeExamResult(learnerSessionGUID, contentObjectGUID);
            }
        }
        /// <summary>
        /// This method returns the course locked status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>boolean true if locked,false if not and if exception is thrown</returns>
        [WebMethod]
        public bool IsCourseLocked(int enrollmentID,out string lockType)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.IsCourseLocked(enrollmentID,out lockType);
            }
        }
        /// <summary>
        /// This method locks/unlocks the course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="date">datetime date</param>
        /// <param name="islock">bool isLock</param>
        /// <returns>boolean true if successfull,else false</returns>
        [WebMethod]
        public bool LockUnlockCourse(int courseID, int learnerID,int enrollmentID, DateTime date,string lockType, bool islock)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.LockUnlockCourse(courseID, learnerID,enrollmentID,date,lockType,islock);
            }

        }

        [WebMethod]
        public bool LockCourseDuringAssessment(int courseID, int enrollmentID, DateTime date, string lockType, bool islock)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.LockCourseDuringAssessment(courseID, enrollmentID, date, lockType, islock);
            }
        }

        [WebMethod]
        public bool UpdateCourseStatusDuringAssessment(int courseID, int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateCourseStatusDuringAssessment(courseID, enrollmentID);
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="learnerID"></param>
        /// <param name="date"></param>
        /// <param name="islock"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateLearnerStatisticsMaximumAttemptAction(long learnerStatisticsID, bool maxAttemptActionTaken)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateLearnerStatisticsMaximumAttemptAction(learnerStatisticsID, maxAttemptActionTaken);
            }

        }
        /// <summary>
        /// This method checks wether course is completed or not from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        [WebMethod]
        public bool IsCourseCompleted(int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.IsCourseCompleted(enrollmentID);
            }

        }

        /// <summary>
        /// This method checks wether course is completed or not from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        [WebMethod]
        public bool GetDocuSignStatusByEnrollmentId(int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetDocuSignStatusByEnrollmentId(enrollmentID);
            }

        }



        /// <summary>
        /// This method gets course time spend based on enrollment ID
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>total seconds</returns>
        [WebMethod]
        public int GetLearnerTimeSpent(int enrollmentID, string learningSessionGuid)
        {
            using (TrackingManager trackingManager = new TrackingManager())
                {
					return trackingManager.GetLearnerTimeSpent(enrollmentID, learningSessionGuid);
                }         
        }

        [WebMethod]
        public int GetLearnerTimeSpentbyTime(int enrollmentID, DateTime startTime, DateTime endTime, string learningSessionGuid)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearnerTimeSpentByTime(enrollmentID, startTime, endTime, learningSessionGuid);
            }
        }

        /// <summary>
        /// This method calculates the course completion status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>true if completed,else false</returns>
        [WebMethod]
        public LearnerCourseCompletionStatus GetCourseCompletionStatus(int courseID, CourseCompletionPolicy courseCompletionPolicy, long learnerID, long enrollmentID, int QuizCount, DateTime regDate, double courseProgress, int courseApprovalID,int source)
        {
            //LCMS-6304 - Starts
            /*
            DateTime regDate = DateTime.Today;
            using (VUConnectorService.LCMS_VUConnectorServiceService lCMS_VUConnectorServiceService = new _360Training.TrackingService.VUConnectorService.LCMS_VUConnectorServiceService())
            {
                lCMS_VUConnectorServiceService.Url = ConfigurationManager.AppSettings["LCMS_VUConnectorService"];
                lCMS_VUConnectorServiceService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
                lCMS_VUConnectorServiceService.UseDefaultCredentials = true;

                string regDateStr = lCMS_VUConnectorServiceService.GetStudentCourseRegDate(enrollmentID);
                DateTime.TryParse(regDateStr, out regDate);
            }
             */
            //LCMS-6304 - Ends

            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetCourseCompletionStatus(courseID, courseCompletionPolicy, learnerID, enrollmentID, QuizCount, regDate, courseProgress, courseApprovalID,source);
            }

        }
        /// <summary>
        /// This methood inserts the record in the learning session table
        /// </summary>
        /// <param name="courseID">string courseGUID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <param name="startTime">DateTime StartTime</param>
        /// <param name="uniqueUserGUID">string uniqueUserGUID</param>
        /// <param name="learningSessionID">string learningSessionGUID</param>
        /// <param name="sourceVU">int sourceVU</param>
        /// <returns>returns true if successfull else false</returns>
        [WebMethod]
        public bool InsertLearningSession(string courseGUID, int learnerID, int enrollmentID, DateTime startTime, string uniqueUserGUID, string learningSessionGUID, int sourceVU,int brandingID,int languageID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.InsertLearningSession(courseGUID, learnerID, enrollmentID, startTime, uniqueUserGUID, learningSessionGUID, sourceVU,brandingID,languageID);
            }

        }
        /// <summary>
        /// This method gets the source of courseplayer execution
        /// </summary>
        /// <param name="learningSessionGUID">string </param>
        /// <returns>returns int sourceID</returns>
        [WebMethod]
        public int GetSource(string learnerSessionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetSource(learnerSessionGUID);
            }
        }
         /// <summary>
        /// This method gets the brandcode and variant by learning sessionID
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        [WebMethod]
        public void GetLearningSessionBrandcodeVariant(string learningSessionGUID, out string brandCode, out string variant)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                 trackingManager.GetLearningSessionBrandcodeVariant(learningSessionGUID,out brandCode,out variant);
            }
        }

        /// <summary>
        /// This method gets the brandcode and variant by enrollmentID
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        [WebMethod]
        public void GetEnrollmentIDBrandcodeVariant(int enrollmentID, out string brandCode, out string variant)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                trackingManager.GetEnrollmentIDBrandcodeVariant(enrollmentID, out brandCode, out variant);
            }
        }


        /// <summary>
        /// This method saves the learner validation statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerValidationStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        [WebMethod]
        public int SaveLearnerValidationStatistics(LearnerValidationStatistics learnerValidationStatistics)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveLearnerValidationStatistics(learnerValidationStatistics);
            }
        }
        /// <summary>
        /// This method gets the course type of the course associated with a learning sesison
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <returns>string CourseType</returns>
        [WebMethod]
        public string GetLearningSessionCourseTypeAndUrl(string learningSessionGUID,out string URL)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearningSessionCourseType(learningSessionGUID,out URL);
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
        [WebMethod]
        public void GetInfoForLMSVUConnector(string learningSessionGUID, out string emailAddress, out string firstName, out string lastName, out string courseGUID, out int epoch,out int learnerID
                                            , out string phone, out string officePhone, out string streetAddress, out string city, out string zipCode, out string state, out string country, out string middleName,out string userName)
        {
            //string emailAddress;
            //string firstName;
            //string lastName;
            //string courseGUID;
            //int epoch;
            using (TrackingManager trackingManager = new TrackingManager())
            {
                trackingManager.GetInfoForLMSVUConnector(learningSessionGUID,out emailAddress,out firstName,out lastName,out courseGUID,out epoch,out  learnerID
                                                         ,out phone,out officePhone,out streetAddress,out city,out zipCode,out state,out country,out middleName,out userName);
            }
        }
        /// <summary>
        /// This method updates the answer of learner validation statistics
        /// </summary>
        /// <param name="answerText">string answer text</param>
        /// <returns>return true if successfull else false</returns>
        [WebMethod]
        public bool UpdateValidationLearnerStatisticsAnswer(int learnerValidationStatisticsID, string answerText, bool isCorrect, bool isAnswered,DateTime saveTime)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateValidationLearnerStatisticsAnswer(learnerValidationStatisticsID,answerText,isCorrect,isAnswered,saveTime);
            }
        }
         /// <summary>
        /// This method returns the validation statistics of a particular enrollment
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <returns>list of LearnerValidationStatistics</returns>
        [WebMethod]
		public List<LearnerValidationStatistics> GetCurrentLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, out int minutesSinceLastValidation)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
				return trackingManager.GetCurrentLearnerValidationStatistics(enrollmentID, courseId, learnerId, source, out minutesSinceLastValidation);
            }
        }
        [WebMethod]
        public LearnerProfile GetUserProfile(string TransactionGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetUserProfile(TransactionGUID);
            }
        }
        [WebMethod]
        public bool UpdateLearnerProfile(string TransactionGUID,LearnerProfile profile)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateLearnerProfile(TransactionGUID,profile);
            }
        }
        [WebMethod]
        public bool UpdateCompleteLearnerProfile(string emailAddress,string firstName,string lastName,int learnerID
                                            , string phone, string officePhone, string streetAddress, string city, string zipCode, string state, string country,string middleName)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateCompletedLearnerProfile(emailAddress,firstName,lastName,learnerID,phone,officePhone,streetAddress,city,zipCode,state,country,middleName);
            }
        }

        [WebMethod]
        public void GetLearnerCourseMetaCertificateInfo(int Course_ID, int Learner_ID, int Enrollment_ID, out string CourseName, out string ApprovedCourseHours, out DateTime CompletionDate, out string FirstName, out string LastName, out string CertificateNumber, out DateTime CertificateIssueDate)
        {
                        
            using (TrackingManager trackingManager = new TrackingManager())
            {
                trackingManager.GetLearnerCourseMetaCertificateInfo(Course_ID, Learner_ID, Enrollment_ID, out CourseName, out ApprovedCourseHours, out CompletionDate, out FirstName, out LastName, out CertificateNumber, out CertificateIssueDate);
            }
        }
        /// <summary>
        /// This method unlocks the last record of a locked course for a particular learner
        /// </summary>
        /// <param name="learnerID"></param>
        /// <param name="courseGUID"></param>
        /// <returns></returns>
        [WebMethod]
        [Obsolete("This mehtod is obsolete and may not return desired results", true)]
        public bool UnlockLockedCourse(int learnerID,string courseGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UnlockLockedCourse(learnerID,courseGUID);
            }
        }
        [WebMethod]
        public string GetLearningSessionResourceValueOfKey(string learningSessionID, string resourceKey)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearningSessionResourceValueOfKey(learningSessionID, resourceKey);
            }
        }
        [WebMethod]
        public string GetResourceValueOfResourceKey(string brandCode,string variant,string resourceKey)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetResourceValueOfResourceKey(brandCode,variant,resourceKey);
            }
        }
        [WebMethod]
        public List<LearnerStatistics> GetPreviousLearnerStatistics(string learningSessionID,string currentItemGUID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetPreviousLearnerStatistics(learningSessionID, currentItemGUID);
            }
        }
        /// <summary>
        /// This method returns the redirect URL by learningSesssion id,returns empty if an exception occurs or record not found
        /// </summary>
        /// <param name="learningSessionID">string learningSessionID</param>
        /// <returns>string redirectURL</returns>
        [WebMethod]
        public string GetLearningSessionRedirectURL(string learningSessionID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetLearningSessionRedirectURL(learningSessionID);
            }
        }

        [WebMethod]
        public string LearningSessionEndedLMS(string learnerSessioGUID,bool completed,string certificateURL)
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
                return "";
            }
            catch (Exception ex)
            {
                Exception exp1 = new Exception("learnerSessioGUID = " + learnerSessioGUID + " " + ex.ToString(), ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(exp1, "Exception Policy");
                return exp1.Message;
                /*
                LearningSessionComplete.LearningSessionCompleteRequest request = new LearningSessionComplete.LearningSessionCompleteRequest();
                request.learningSessionId = learnerSessioGUID;
                request.transactionGUID = Guid.NewGuid().ToString();
                request.courseCompleted = completed;
                request.courseCompletedSpecified = completed;
                request.certificateURL = certificateURL;

                LearningSessionComplete.lmsLcmsService lmsLcmsService = new LearningSessionComplete.lmsLcmsService();
                lmsLcmsService.Url = System.Configuration.ConfigurationManager.AppSettings["LearningSessionCompleteURL"];
                LearningSessionComplete.LearningSessionCompleteResponse response = lmsLcmsService.LearningSessionComplete(request);
                return response.transactionResultMessage;
                */
            }

        }
        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        [WebMethod]
        public bool SaveCourseEvaluationResult(CourseEvaluationResult courseEvaluationResult)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveCourseEvaluationResult(courseEvaluationResult);
            }
        }
        [WebMethod]
        public bool IsCourseEvaluationAttempted(int courseID, int learnerID, int learningSessionID, string surveyType)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.IsCourseEvaluationAttempted(courseID, learnerID, learningSessionID, surveyType);
            }
        }

        [WebMethod]
        public void LegacyStatsRecorder(string learningSessionGuid,int courseId,int studentId,int epoch,string playerVersion,int source)
        { 
            /*
             * source = 1 for Unload Event and 0 for service
            */
            LegacyTrackingManager legacyTrackingManager = new LegacyTrackingManager(learningSessionGuid, courseId, studentId, epoch, playerVersion, source);
            legacyTrackingManager.CreateThread();
        }

        [WebMethod]
        public int SaveAssessmentScore(LearnerStatistics learnerStatistics,int masteryScore)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveAssessmentScore(learnerStatistics, masteryScore);
            }
        }

        [WebMethod]
        public int SaveAssessmentScore_Game(LearnerStatistics learnerStatistics, int masteryScore)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveAssessmentScore_Game(learnerStatistics, masteryScore);
            }
        }

        [WebMethod]
        public bool SaveAssessmentItem(LearnerStatistics learnerStatistics)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveAssessmentItem(learnerStatistics);
            }

        }


        [WebMethod]
        public bool UpdateLearnerCourseStatistics(long enrollmentId, string certificateURL, bool isCompleted)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateLearnerCourseStatistics(enrollmentId,certificateURL, isCompleted);
            }

        }

        [WebMethod]
        public int AuthenticateProctor(long courseID, long learnerID, string learningSessionID, string proctorLogin, string proctorPassword)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.AuthenticateProctor(courseID, learnerID, learningSessionID, proctorLogin, proctorPassword);
            }
        }

        [WebMethod]
        public int AuthenticateSpecialPostAssessmentValidation(long courseID, long learnerID, string learningSessionID, string DRELicenseNumber, string DriverLicenseNumber)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.AuthenticateSpecialPostAssessmentValidation(courseID, learnerID, learningSessionID, DRELicenseNumber, DriverLicenseNumber);  
            }
        }

        [WebMethod]
        public int AuthenticateNYInsuranceValidation(long courseID, long learnerID, string learningSessionID, string MonitorNumber)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.AuthenticateNYInsuranceValidation(courseID, learnerID, learningSessionID, MonitorNumber);
            }
        }

        [WebMethod]
        public bool ProctorLoginRequirementCriteriaMeets(string learningSessionGuid)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.ProctorLoginRequirementCriteriaMeets(learningSessionGuid);
            }
        }

        [WebMethod]
        public string AuthenticateMMAPP(string userName,string password,string courseGuid)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.AuthenticateMMAPP(userName,password,courseGuid);
            }
        }


        [WebMethod]
        public bool UpdateStatisticsForFlashCourse(string lsGuid, float score, bool isCompleted, bool isPass, int assessmentAttemptNo)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.UpdateStatisticsForFlashCourse(lsGuid, score, isCompleted, isPass, assessmentAttemptNo);
            }
        }

        // LCMS-9213
        [WebMethod]
        public int GetAssessmentTimeForAllSessions(string learningSessionGuid, string assessmentType, int contentObjectID, int examID, string type, int assessmentConfigurationID)
        {
            return new TrackingManager().GetAssessmentTimeForAllSessions(learningSessionGuid, assessmentType, contentObjectID, examID, type, assessmentConfigurationID);
        }

        // LCMS-10266
        [WebMethod]
        public bool ResetAssessmentItemStatistics(string learningSessionGuid, string statisticsType, string assessmentType, string itemGUID, int attemptNumber, int remediationCount)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.ResetAssessmentItemStatistics(learningSessionGuid, statisticsType, assessmentType, itemGUID, attemptNumber, remediationCount);
            }
        }

        //LCMS-12532 Yasin
        [WebMethod]
        public bool GetValidationIdendityQuestions(int learnerID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetValidationIdendityQuestions(learnerID);
            }
        }

        
        //LCMS-12532 Yasin
        [WebMethod]
        public bool SaveValidationIdendityQuestions(int QS1, string Answer1, int QS2, string Answer2, int QS3, string Answer3, int QS4, string Answer4, int QS5, string Answer5, int learnerID, int QuestionSet1, int QuestionSet2, int QuestionSet3, int QuestionSet4, int QuestionSet5)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveValidationIdendityQuestions(QS1, Answer1, QS2, Answer2, QS3, Answer3, QS4, Answer4, QS5, Answer5, learnerID, QuestionSet1, QuestionSet2, QuestionSet3, QuestionSet4, QuestionSet5);
            }
        }


        // LCMS-10877
        [WebMethod]
        public int SaveCourseRating(int CourseID, int Rating, int EnrollmentID, out string CourseGuid, out double AvgRating, out int TotalRating)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveCourseRating(CourseID, Rating, EnrollmentID,out  CourseGuid, out  AvgRating, out  TotalRating);
            }
        }

        [WebMethod]
        public CourseRating SaveCourseRatingNPS(CourseRating courseRating)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveCourseRatingNPS(courseRating);
            }
        }

        // LCMS-10877
        [WebMethod]
        public int GetUserCourseRating(int CourseID, int EnrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetUserCourseRating(CourseID, EnrollmentID);
            }
        }

        [WebMethod]
        public CourseRating GetUserCourseRatingNPS(int CourseID, int EnrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetUserCourseRatingNPS(CourseID, EnrollmentID);
            }
        }

        //LCMS-11974
        //Abdus Samad
        //Start
        [WebMethod]
        public LearnerProfile GetUserProfileInformation(int enrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetUserProfileInformation(enrollmentID);
            }
        }

        //STOP

        //LCMS-12502
        //Abdus Samad
        //Start
        [WebMethod]
        public bool SaveDocuSignRoleAgainstLearnerEnrollment(string envelopId, int learnerEnrollmentID, string roleName)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveDocuSignRoleAgainstLearnerEnrollment(envelopId, learnerEnrollmentID, roleName);
            }
        }

        [WebMethod]
        public bool SaveEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.SaveEnvelopStatusAgainstDocuSignRole(learnerEnrollmentID, roleName);
            }
        }


        [WebMethod]
        public bool GetEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetEnvelopStatusAgainstDocuSignRole(learnerEnrollmentID, roleName);
            }
        }

        //Stop

        [WebMethod]
        public LearningSessionInformation CheckIfLearningSessionOpen(string LearningSessionID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.CheckIfLearningSessionOpen(LearningSessionID);
            }
        }


        [WebMethod]
        public string GetCourseIDAgainstLearningSessionGUID(string LearningSessionGuid)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetCourseIDAgainstLearningSessionGUID(LearningSessionGuid);
            }
        }

        [WebMethod]
        public int GetTotalTimeSpent(string LearningSessionGuid, int EnrollmentID)
        {
            using (TrackingManager trackingManager = new TrackingManager())
            {
                return trackingManager.GetTotalTimeSpent(LearningSessionGuid, EnrollmentID);
            }
        }

        
    }

}
//