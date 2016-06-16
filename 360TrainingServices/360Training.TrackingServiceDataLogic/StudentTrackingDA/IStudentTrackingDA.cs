using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.TrackingServiceDataLogic.StudentTrackingDA
{
    public interface IStudentTrackingDA
    {

        /// <param name="COurse ID">Int learnerID</param>
        /// <param name="Learner ID">Int learnerID</param>
        /// <param name="CourseName">string CourseName</param>
        /// <param name="ApprovedCourseHours">string ApprovedCourseHours</param>
        /// <param name="Completion Date">string Completion Date</param> 
        void GetLearnerCourseMetaCertificateInfo(int Course_ID, int Learner_ID, int Enrollment_ID, ref string CourseName, ref string ApprovedCourseHours, ref DateTime completionDate, ref string FirstName, ref string LastName, ref string CertificateNumber, ref DateTime CertificateIssueDate);

        /// <summary>
        /// This method saves the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int curseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="sequenceNo">int sequenceNo</param>
        /// <returns>returns true if success,else false</returns>
        bool SaveStudentCourseTrack(int courseID, int learnerID, int sequenceNo);
        /// <summary>
        /// This method gets the sequence no. of given student and course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>int sequenceNo.</returns>
        int GetStudentCourseTrack(int courseID, int learnerID);
        /// <summary>
        /// This method updates the learning session endtime
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <param name="endTime">datetime endtime</param>
        /// <returns>true if successfull,else false</returns>
        bool UpdateLearningSessionEndtime(string sessionID, DateTime endTime, int totalTime, double percentageCourseProgress, long enrollment_Id);
        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>NonZero LearnerStatisticsID if successfull, else 0</returns>
        int SaveLearnerStatistics(LearnerStatistics learnerStatistics);

        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>NonZero LearnerStatisticsID if successfull, else 0</returns>
        int SaveLearnerStatisticsWithStatisticDateTime(LearnerStatistics learnerStatistics, DateTime StatisticDateTime);

        /// <summary>
        /// This metho returns the learner course track info object
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        LearnerCourseTrackInfo GetLearnerCoursetrackInfo(string sessionID);
        /// <summary>
        /// This method saves the bookmark info 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="item_GUID">string itemGUID</param>
        /// <param name="flashSceneNo">string flashSceneNo</param>
        /// <param name="bookMarkTitle">string bookMarkTitle</param>
        /// <returns>boolean true if suucessfull,else false</returns>
        bool SaveLearnerCourseBookmark(int courseID, int learnerID, int enrollmentID, string item_GUID, string sceneGUID, string flashSceneNo, string bookMarkTitle, string lastScene, bool isMovieEnded, bool nextButtonState, string firstSceneName, DateTime createddate);
        /// <summary>
        /// This method returns the LearnerCourseBookmarkInfo object
        /// </summary>
        /// <param name="bookMarkID">int learnerCourseBookMarkID</param>
        /// <returns>BookMarkInfo object</returns>
        LearnerCourseBookMarkInfo GetLearnerCourseBookMarkInfo(int learnerCourseBookMarkID);
        /// <summary>
        /// This method returns all LearnerCourseBookmarkInfo objects belonging to a particular course
        /// and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>BookMarkInfo object</returns>
        List<LearnerCourseBookMarkInfo> GetAllLearnerCourseBookMarksInfo(int courseID, int learnerID, int enrollmentID);
        /// <summary>
        /// This method gets all LeanerCourseTrackInfo records related to a a particular
        /// course and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        List<LearnerCourseTrackInfo> GetAllLearnerCourseTrackInfo(int courseID, int learnerID, int enrollmentID);
        /// <summary>
        /// This method returns the learnerstatistics records of the assessment 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="statisticsType">string statisticsType</param>
        /// <param name="assessmentType">string assessmentType</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetLearnerStatisticsAssessments(string learnerSessionGUID, string assessmentType, int remediationCount, int examID);
        /// <summary>
        /// This method returns the postassessmentAttemp no.
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        int GetLearnerStatisticsPostAssessmentAttemptNo(string learnerSessionGUID);
        /// <summary>
        /// This method returns the preassessmentAttemp no.
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        int GetLearnerStatisticsPreAssessmentAttemptNo(string learnerSessionGUID);
        /// <summary>
        /// This method returns the assessmentAttemp noof the specified assessment type 
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>int attemptNo, 0 if no records found else the attempt No</returns>
        int GetLearnerStatisticsQuizAttemptNo(string learnerSessionGUID, string contentObjectGUID);
        /// <summary>
        /// This method returns the learner course track info object for the current session
        /// </summary>
        /// <param name="sessionID">int sessionID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        LearnerCourseTrackInfo GetCurrentLearnerCoursetrackInfo(string sessionID);
        /// <summary>
        /// This method returns the learnerstatistics records of the preassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetLearnerStatisticsPreAssessmentResults(string learnerSessionGUID);
        /// <summary>
        /// This method returns the learnerstatistics records of the postassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetLearnerStatisticsPostAssessmentResults(string learnerSessionGUID);
        /// <summary>
        /// This method returns the learnerstatistics records of the quizresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetLearnerStatisticsQuizResults(string learnerSessionGUID, string contentObjectID);
        /// <summary>
        /// This method saves the learnerStatisticsAnswer
        /// </summary>
        /// <param name="learnerStatisticsAnswer">object learnerStatisticsAnswer</param>
        /// <returns>true if successfull else false</returns>
        bool SaveLearnerStatisticsAnswer(LearnerStatisticsAnswer learnerStatisticsAnswer);
        /// <summary>
        /// This method inserts new record in lock unlock course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="date">datetime date</param>
        /// <param name="islock">bool isLock</param>
        /// <returns>boolean true if successfull,else false</returns>
        bool SaveLockedCourse(int courseID, int learnerID, int enrollmentID, DateTime date, string lockType, bool islock);
        /// <summary>
        /// This method returns the course locked status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>boolean true if locked,false if not and if exception is thrown</returns>
        bool GetLockedCourseCourseLocked(int enrollmentID, ref string lockType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="learnerStatisticsID"></param>
        /// <param name="maxAttemptActionTaken"></param>
        /// <returns></returns>
        bool UpdateLearnerStatisticsMaximumAttemptAction(long learnerStatisticsID, bool maxAttemptActionTaken);
        /// <summary>
        /// This method gets the course completion policies and also gets the required data
        /// for the application of policies
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>object CourseConfiguration with completion policies filled from data source</returns>
        CourseConfiguration GetCourseCompletionConfiguration(int courseID);
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
        List<LearnerStatistics> GetCourseCompletionData(int courseID, int learnerID, int enrollmentID
                                                               , CourseConfiguration courseConfiguration
                                                               , out int postAssessmentNoOfCorrect, out int postAssessmentNoOfInCorrect
                                                                , out int preAssessmentNoOfCorrect, out int preAssessmentNoOfInCorrect
                                                                , out bool postAssessmentAttempted
                                                                , out int visitedSceneCount
                                                                , out int courseVisitCount
                                                                , out DateTime firstAccessDateTime
                                                                , out bool hasRespondedToCourseEvaluation
                                                                );
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
        bool SaveLearningSession(string courseGUID, int learnerID, int enrollmentID, DateTime startTime, string uniqueUserGUID, string learningSessionGUID, int sourceVU, int brandingID, int languageID);
        /// <summary>
        /// Thhis method gets the source of courseplayer execution
        /// </summary>
        /// <param name="learningSessionGUID">string </param>
        /// <returns>returns int sourceID</returns>
        int GetSource(string learningSessionGUID);
        /// <summary>
        /// This method gets the brandcode and variant by learning sessionID
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="brandCode">string brandCode</param>
        /// <param name="variant">string variant</param>
        void GetLearningSessionBrandcodeVariant(string learningSessionGUID, ref string brandCode, ref string variant);
        /// <summary>
        /// This method saves the learner validation statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerValidationStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        int SaveLearnerValidationStatistics(LearnerValidationStatistics learnerValidationStatistics);
        /// <summary>
        /// This method gets the course type of the course associated with a learning sesison
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <returns>string CourseType</returns>
        string GetLearningSessionCourseType(string learningSessionGUID, ref string url);
        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="emailAddress">string emailAddress</param>
        /// <param name="firstName">string firstName</param>
        /// <param name="lastName">string lastName</param>
        /// <param name="courseGUID">string courseGUID</param>
        /// <param name="epoch">int epoch</param>
        void GetInfoForLMSVUConnector(string learningSessionGUID, ref string emailAddress, ref string firstName, ref string lastName, ref string courseGUID, ref int epoch, ref int learnerID
                                            , ref string phone, ref string officePhone, ref string streetAddress, ref string city, ref string zipCode, ref string state, ref string country, ref string middleName, ref string userName);


        /// <summary>
        /// This method will return the Primary Key associated with learning Session guid passed as string param.
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        int GetLearningSessionID(string learningSessionId);

        /// <summary>
        /// This method updates the answer of learner validation statistics
        /// </summary>
        /// <param name="answerText">string answer text</param>
        /// <returns>return true if successfull else false</returns>
        bool UpdateValidationLearnerStatisticsAnswer(int learnerValidationStatisticsID, string answerText, bool isCorrect, bool isAnswered, DateTime saveTime);
        /// <summary>
        /// This method returns the validation statistics of a particular enrollment
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <returns>list of LearnerValidationStatistics</returns>
		List<LearnerValidationStatistics> GetCurrentLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, ref int minutesSinceLastValidation);
        bool UpdateLatestLockedCourse(int learnerID, string courseGUID);
        List<LearnerStatistics> GetPreviousLearnerStatistics(string learningSessionID, string currentItemGUID);
        string GetLearningSessionResourceValueOfKey(string learningSessionID, string resourceKey);
        string GetResourceValueOfResourceKey(string brandCode, string variant, string resourceKey);
        bool UpdateLearningSessionEndtimeLegacy(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED);
        /// <summary>
        /// This method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        bool GetLearnerCourseStatisticsCompleted(int enrollmentID);
        bool UpdateCompletedLearnerProfile(string emailAddress, string firstName, string lastName, int learnerID
                                            , string phone, string officePhone, string streetAddress, string city, string zipCode, string state, string country, string middleName);
        #region course unlocking
        LearnerProfile GetUserProfile(string learningSessionId);
        bool UpdateLearnerProfile(string TransactionGUID, LearnerProfile profile);
        bool UnlockLockedCourse(string TransactionGUID);
        bool GetLearnerCredential(string TransactionGUID, LearnerProfile profile);

        #endregion

        int SaveSurveyResult(CourseEvaluationResult courseEvaluationResult);
        bool SaveSurveyResultAnswer(CourseEvaluationResultAnswer courseEvaluationResultAnswer, int surveyResultID);
        bool GetCourseEvaluationResultID(int courseID, int learnerID, int learningSessionID, string surveyType);

        bool UpdateLearnerCourseStatistics(long enrollmentId, string certificateURL, bool isCompleted);
        int GetAssessmentTimeForAllSessions(string learningSessionGuid, string assessmentType, int contentObjectID, int examID, string type, int assessmentConfigurationID);
        bool ResetAssessmentItemStatistics(string learningSessionGuid, string statisticsType, string assessmentType, string itemGUID, int attemptNumber, int remediationCount);

           //LCMS-11974 //DocuSign Decline
        //Abdus Samad Start
       LearnerProfile GetUserProfileInformation(int enrollmentID);
        //Abdus Samad Stop

       LearningSessionInformation CheckIfLearningSessionOpen(string LearningSessionID);

       string GetCourseIDAgainstLearningSessionGUID(string LearningSessionGuid);

       bool DeleteLearnerCourseBookmark(int bookmarkID);
	   
	   void GetEnrollmentIDBrandcodeVariant(int enrollmentID, ref string brandCode, ref string variant);

    }
}
