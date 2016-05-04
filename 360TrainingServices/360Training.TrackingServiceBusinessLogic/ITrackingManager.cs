using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.TrackingServiceBusinessLogic
{
    public interface ITrackingManager
    {
        /// <summary>
        /// This method updates the session endtime
        /// </summary>
        /// <param name="sessionID">string sessionID</param>
        /// <param name="endTime">DateTime endTime</param>
        /// <returns>true if succeeds ,else false</returns>
        bool EndSession(string sessionID, DateTime endTime, int totalTime, double percentageCourseProgress, long enrollment_Id);
        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        bool SaveLearnerStatistics(LearnerStatistics learnerStatistics);

        /// <summary>
        /// This method saves the learner statistics
        /// </summary>
        /// <param name="learnerStatistics">learnerStatistics object</param>
        /// <returns>true if successfull,else false</returns>
        bool SaveLearnerStatisticsWithStatisticDateTime(LearnerStatistics learnerStatistics, DateTime StatisticDateTime);

        /// <summary>
        /// This method returns the learner course track info object
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
        bool SaveLearnerCourseBookmark(int courseID, int learnerID,int enrollmentID, string item_GUID, string sceneGUID, string flashSceneNo, string bookMarkTitle, string lastScene, bool isMovieEnded, bool nextButtonState,string firstSceneName);
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
        List<LearnerCourseBookMarkInfo> GetAllLearnerCourseBookMarksInfo(int courseID, int learnerID,int enrollmentID);
        /// <summary>
        /// This method gets all LeanerCourseTrackInfo records related to a a particular
        /// course and learner
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>LearnerCourseTrackInfo object</returns>
        List<LearnerCourseTrackInfo> GetAllLearnerCourseTrackInfo(int courseID, int learnerID,int enrollmentID);
        /// <summary>
        /// This method returns the learnerstatistics records of the assessment 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="statisticsType">string statisticsType</param>
        /// <param name="assessmentType">string assessmentType</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetPreviouslyAskedQuestions(string learnerSessionGUID, string assessmentType,int remediationCount, int examID);
        /// <summary>
        /// This method returns the learnerstatistics records of the preassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetPreAssessmentResult(string learnerSessionGUID);
        /// <summary>
        /// This method returns the learnerstatistics records of the postassessmentresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetPostAssessmentResult(string learnerSessionGUID);
        /// <summary>
        /// This method returns the learnerstatistics records of the quizresult 
        /// </summary>
        /// <param name="learnerSessionGUID">string learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>list of learnerstaistics object</returns>
        List<LearnerStatistics> GetQuizResult(string learnerSessionGUIDstring, string contentObjectGUID);
        /// <summary>
        /// This method returns the assessmentAttempt no of preassessment
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        int GetPreAssessmentAttemptNo(string learnerSessionGUID);
        /// <summary>
        /// This method returns the postassessmentAttempt no 
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <returns>int attempNo</returns>
        int GetPostAssessmentAttemptNo(string learnerSessionGUID);
        /// <summary>
        /// This method returns the quizAttempt no of the specified contentobject
        /// </summary>
        /// <param name="learnerSessionGUID">strin learnerSessionGUID</param>
        /// <param name="contentObjectGUID">string contentObjectGUID</param>
        /// <returns>int attempNo</returns>
        int GetQuizAttemptNo(string learnerSessionGUID, string contentObjectGUID);
        /// <summary>
        /// This method locks/unlocks the course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <param name="date">datetime date</param>
        /// <param name="islock">bool isLock</param>
        /// <returns>boolean true if successfull,else false</returns>
        bool LockUnlockCourse(int courseID,int learnerID,int enrollmentID,DateTime date,string lockType,bool islock);
        /// <summary>
        /// This method returns the course locked status 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="learnerID">int learnerID</param>
        /// <returns>boolean true if locked,false if not and if exception is thrown</returns>
        bool IsCourseLocked(int enrollmentID,out string lockType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="learnerStatisticsID"></param>
        /// <param name="maxAttemptActionTaken"></param>
        /// <returns></returns>
        bool UpdateLearnerStatisticsMaximumAttemptAction(long learnerStatisticsID, bool maxAttemptActionTaken);
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
        bool InsertLearningSession(string courseGUID, int learnerID, int enrollmentID, DateTime startTime, string uniqueUserGUID, string learningSessionGUID, int sourceVU,int brandingID,int languageID);
        /// <summary>
        /// This method calculates the course completion status 
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="learnerID"></param>
        /// <returns>true if completed,else false</returns>
        LearnerCourseCompletionStatus GetCourseCompletionStatus(int courseID, CourseCompletionPolicy courseCompletionPolicy, long learnerID, long enrollmentID, int QuizCount, DateTime registrationDate, double courseProgress, int courseApprovalID, int source);
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
        void GetLearningSessionBrandcodeVariant(string learningSessionGUID, out string brandCode, out string variant);
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
        string GetLearningSessionCourseType(string learningSessionGUID,out string url);
        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="learningSessionGUID">string learningSessionGUID</param>
        /// <param name="emailAddress">string emailAddress</param>
        /// <param name="firstName">string firstName</param>
        /// <param name="lastName">string lastName</param>
        /// <param name="courseGUID">string courseGUID</param>
        /// <param name="epoch">int epoch</param>
        void GetInfoForLMSVUConnector(string learningSessionGUID, out string emailAddress, out string firstName, out string lastName, out string courseGUID, out int epoch, out int learnerID
                                             , out string phone, out string officePhone, out string streetAddress, out string city, out string zipCode, out string state, out string country, out string middleName,out string userName);

        /// <summary>
        /// This method updates the answer of learner validation statistics
        /// </summary>
        /// <param name="answerText">string answer text</param>
        /// <returns>return true if successfull else false</returns>
        bool UpdateValidationLearnerStatisticsAnswer(int learnerValidationStatisticsID, string answerText, bool isCorrect, bool isAnswered,DateTime saveTime);


        /// <summary>
        /// This method gets the information required for LMS VU connector
        /// </summary>
        /// <param name="COurse ID">Int learnerID</param>
        /// <param name="Learner ID">Int learnerID</param>
        /// <param name="CourseName">string CourseName</param>
        /// <param name="ApprovedCourseHours">string ApprovedCourseHours</param>
        /// <param name="Completion Date">string Completion Date</param>        

        void GetLearnerCourseMetaCertificateInfo(int Course_ID, int Learne_ID, int Enrollment_ID, out string CourseName, out string ApprovedCourseHours, out DateTime CompletionDate, out string FirstName, out string LastName, out string CertificateNumber, out DateTime CertificateIssueDate);
        /// <summary>
        /// This method will return the Primary Key associated with learning Session guid passed as string param.
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        int GetLearningSessionID(string learningSessionId);
         /// <summary>
        /// This method returns the validation statistics of a particular enrollment
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <returns>list of LearnerValidationStatistics</returns>
		List<LearnerValidationStatistics> GetCurrentLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, out int minutesSinceLastValidation);
        bool UnlockLockedCourse(int learnerID, string courseGUID);
        string GetLearningSessionResourceValueOfKey(string learningSessionID, string resourceKey);
        string GetResourceValueOfResourceKey(string brandCode, string variant, string resourceKey);
        List<LearnerStatistics> GetPreviousLearnerStatistics(string learningSessionID, string currentItemGUID);
        bool EndSessionLegacy(string sessionID, DateTime endTime, decimal MAXPERCENTAGECOURSEATTENDED);
        /// <summary>
        /// Thhis method gets course completion status from leaernercoursestatistics
        /// </summary>
        /// <param name="learningSessionGUID">int enrollmentID</param>
        /// <returns>returns bool isCoursecompleted</returns>
        bool IsCourseCompleted(int enrollmentID);
        bool UpdateCompletedLearnerProfile(string emailAddress, string firstName, string lastName, int learnerID
                                            , string phone, string officePhone, string streetAddress, string city, string zipCode, string state, string country,string middleName);
        #region course unlocking
        LearnerProfile GetUserProfile(string learningSessionId);
        bool UpdateLearnerProfile(string TransactionGUID, LearnerProfile profile);
        #endregion
        bool IsCourseEvaluationAttempted(int courseID, int learnerID, int learningSessionID, string surveyType);
        bool SaveCourseEvaluationResult(CourseEvaluationResult courseEvaluationResult);

        bool UpdateLearnerCourseStatistics(long enrollmentId, string certificateURL, bool isCompleted);

        int GetAssessmentTimeForAllSessions(string learningSessionGuid, string assessmentType, int contentObjectID, int examID, string type, int assessmentConfigurationID);
        bool ResetAssessmentItemStatistics(string learningSessionGuid, string statisticsType, string assessmentType, string itemGUID, int attemptNumber, int remediationCount);

        int SaveCourseRating(int CourseID, int Rating, int EnrollmentID,out string CourseGuid, out double AvgRating, out int TotalRating);

        //Abdus Samad 
        //LCMS-11974
        //Start
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enrollmentID"></param>
        /// <returns></returns>
         LearnerProfile GetUserProfileInformation(int enrollmentID);
        //Stop

         //Abdus Samad
         //lcms-12502
         //Start
        /// <summary>
        /// This function saves information related to the roles attached in the docusign envelop
        /// </summary>
        /// <param name="envelopId"></param>
        /// <param name="learnerEnrollmentID"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
         bool SaveDocuSignRoleAgainstLearnerEnrollment(string envelopId, int learnerEnrollmentID, string roleName);


        /// <summary>
        /// This function is used to update the status against the role
        /// </summary>
        /// <param name="learnerEnrollmentID"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
         bool SaveEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName);

         /// <summary>
         /// This function is used to update the status against the role
         /// </summary>
         /// <param name="learnerEnrollmentID"></param>
         /// <param name="roleName"></param>
         /// <returns></returns>
         bool GetEnvelopStatusAgainstDocuSignRole(int learnerEnrollmentID, string roleName);
        //Stop

        string GetCourseIDAgainstLearningSessionGUID(string LearningSessionGuid);

        void GetEnrollmentIDBrandcodeVariant(int enrollmentID, out string brandCode, out string variant);

    }
}
//