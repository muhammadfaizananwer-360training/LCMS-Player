using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseConfiguration
    {
        private int courseConfigurationID;

        public int CourseConfigurationID
        {
            get { return courseConfigurationID; }
            set { courseConfigurationID = value; }
        }

        private DateTime lastmodifiedDateTime;

        public DateTime LastModifiedDateTime
        {
            get { return lastmodifiedDateTime; }
            set { lastmodifiedDateTime = value; }
        }
        #region Player
        private bool playerEnableIntroPage;

        public bool PlayerEnableIntroPage
        {
            get { return playerEnableIntroPage; }
            set { playerEnableIntroPage = value; }
        }
        private bool playerEnableContent;

        public bool PlayerEnableContent
        {
            get { return playerEnableContent; }
            set { playerEnableContent = value; }
        }
        private bool playerEnableEndOfCourseScene;

        public bool PlayerEnableEndOfCourseScene
        {
            get { return playerEnableEndOfCourseScene; }
            set { playerEnableEndOfCourseScene = value; }
        }
        private bool playerAllowUserToReviewCourseAfterCompletion;

        public bool PlayerAllowUserToReviewCourseAfterCompletion
        {
            get { return playerAllowUserToReviewCourseAfterCompletion; }
            set { playerAllowUserToReviewCourseAfterCompletion = value; }
        }
        private int playerIdleUserTimeout;

        public int PlayerIdleUserTimeout
        {
            get { return playerIdleUserTimeout; }
            set { playerIdleUserTimeout = value; }
        }
        private string playerCourseFlow; //enum

        public string PlayerCourseFlow
        {
            get { return playerCourseFlow; }
            set { playerCourseFlow = value; }
        }
        private bool playerEnforceTimedOutline;

        public bool PlayerEnforceTimedOutline
        {
            get { return playerEnforceTimedOutline; }
            set { playerEnforceTimedOutline = value; }
        }
        private bool playerEnableOrientaionScenes;

        public bool PlayerEnableOrientaionScenes
        {
            get { return playerEnableOrientaionScenes; }
            set { playerEnableOrientaionScenes = value; }
        }
        private bool playerCourseEvaluation;

        public bool PlayerCourseEvaluation
        {
            get { return playerCourseEvaluation; }
            set { playerCourseEvaluation = value; }
        }
        private string playerDisplayCourseEvaluation;

        public string PlayerDisplayCourseEvaluation
        {
            get { return playerDisplayCourseEvaluation; }
            set { playerDisplayCourseEvaluation = value; }
        }
        private bool playerMustCompleteCourseEvaluatio;

        public bool PlayerMustCompleteCourseEvaluatio
        {
            get { return playerMustCompleteCourseEvaluatio; }
            set { playerMustCompleteCourseEvaluatio = value; }
        }
        private string playerCourseEvaluationInstructions;

        public string PlayerCourseEvaluationInstructions
        {
            get { return playerCourseEvaluationInstructions; }
            set { playerCourseEvaluationInstructions = value; }
        }

        private string playerEndOfCourseInstructions;

        public string PlayerEndOfCourseInstructions
        {
            get { return playerEndOfCourseInstructions; }
            set { playerEndOfCourseInstructions = value; }
        }

        //LCMS-10392
        private bool playerShowAmazonAffiliatePanel;
        public bool PlayerShowAmazonAffiliatePanel
        {
            get { return this.playerShowAmazonAffiliatePanel; }
            set { this.playerShowAmazonAffiliatePanel = value; }
        }

        //LCMS-11878
        //Abdus Samad 
        //Start
     
        private bool playerShowCoursesRecommendationPanel;
        public bool PlayerShowCoursesRecommendationPanel
        {
            get { return this.playerShowCoursesRecommendationPanel; }
            set { this.playerShowCoursesRecommendationPanel = value; }
        }
        //Stop

        //LCMS-
        //Waqas Zakai
        //Start

        private bool playerRestrictIncompleteJSTemplate;
        public bool PlayerRestrictIncompleteJSTemplate
        {
            get { return this.playerRestrictIncompleteJSTemplate; }
            set { this.playerRestrictIncompleteJSTemplate = value; }
        }
        //Stop




        // LCMS-8796
        //-------------------------------------------------------------

        #region SpecialQuestionnaire

        private bool specialQuestionnaire;
        public bool SpecialQuestionnaire
        {
            get { return specialQuestionnaire; }
            set { specialQuestionnaire = value; }
        }

        private String displaySpecialQuestionnaire;
        public String DisplaySpecialQuestionnaire
        {
            get { return displaySpecialQuestionnaire; }
            set { displaySpecialQuestionnaire = value; }
        }

        private bool mustCompleteSpecialQuestionnaire;
        public bool MustCompleteSpecialQuestionnaire
        {
            get { return mustCompleteSpecialQuestionnaire; }
            set { mustCompleteSpecialQuestionnaire = value; }
        }


        private string specialQuestionnaireInstructions;
        public string SpecialQuestionnaireInstructions
        {
            get { return specialQuestionnaireInstructions; }
            set { specialQuestionnaireInstructions = value; }
        }

        #endregion

        //-------------------------------------------------------------

        


        #endregion

        #region Completion
        private bool completionPostAssessmentAttempted;

        public bool CompletionPostAssessmentAttempted
        {
            get { return completionPostAssessmentAttempted; }
            set { completionPostAssessmentAttempted = value; }
        }
        private bool completionPostAssessmentMastery;

        public bool CompletionPostAssessmentMastery
        {
            get { return completionPostAssessmentMastery; }
            set { completionPostAssessmentMastery = value; }
        }
        private bool completionPreAssessmentMastery;

        public bool CompletionPreAssessmentMastery
        {
            get { return completionPreAssessmentMastery; }
            set { completionPreAssessmentMastery = value; }
        }
        private bool completionQuizMastery;

        public bool CompletionQuizMastery
        {
            get { return completionQuizMastery; }
            set { completionQuizMastery = value; }
        }
        private bool completionSurvey;

        public bool CompletionSurvey
        {
            get { return completionSurvey; }
            set { completionSurvey = value; }
        }
        private bool completionViewEverySceneInCourse;

        public bool CompletionViewEverySceneInCourse
        {
            get { return completionViewEverySceneInCourse; }
            set { completionViewEverySceneInCourse = value; }
        }
        private int completionCompleteAfterNOUniqueCourseVisit;

        public int CompletionCompleteAfterNOUniqueCourseVisit
        {
            get { return completionCompleteAfterNOUniqueCourseVisit; }
            set { completionCompleteAfterNOUniqueCourseVisit = value; }
        }
        private int completionMustCompleteWithinSpecifiedAmountOfTimeMinute;

        public int CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute
        {
            get { return completionMustCompleteWithinSpecifiedAmountOfTimeMinute; }
            set { completionMustCompleteWithinSpecifiedAmountOfTimeMinute = value; }
        }
        private int completionMustCompleteWithinSpecifiedAmountOfTimeDay;

        public int CompletionMustCompleteWithinSpecifiedAmountOfTimeDay
        {
            get { return completionMustCompleteWithinSpecifiedAmountOfTimeDay; }
            set { completionMustCompleteWithinSpecifiedAmountOfTimeDay = value; }
        }

        private bool certificateEnabled;

        public bool CertificateEnabled
        {
            get { return certificateEnabled; }
            set { certificateEnabled = value; }
        }

        private Int32 certificateAssetID;

        public Int32 CertificateAssetID
        {
            get { return certificateAssetID; }
            set { certificateAssetID = value; }
        }
        private string completionUnitOfMustCompleteWithInSpecifiedAmountOfTime;

        public string CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime
        {
            get { return completionUnitOfMustCompleteWithInSpecifiedAmountOfTime; }
            set { completionUnitOfMustCompleteWithInSpecifiedAmountOfTime = value; }
        }
        private bool completionRespondToCourseEvaluation;

        public bool CompletionRespondToCourseEvaluation
        {
            get { return completionRespondToCourseEvaluation; }
            set { completionRespondToCourseEvaluation = value; }
        }
        
        #endregion

        #region Assessments
        private AssessmentConfiguration preAssessmentConfig;
        public AssessmentConfiguration PreAssessmentConfiguration
        {
            get { return preAssessmentConfig; }
            set { preAssessmentConfig = value; }
        }

        private AssessmentConfiguration postAssessmentConfig;
        public AssessmentConfiguration PostAssessmentConfiguration
        {
            get { return postAssessmentConfig; }
            set { postAssessmentConfig = value; }
        }
        private AssessmentConfiguration quizConfig;
        public AssessmentConfiguration QuizConfiguration
        {
            get { return quizConfig; }
            set { quizConfig = value; }
        }

        private AssessmentConfiguration practiceAssessmentConfig;
        public AssessmentConfiguration PracticeAssessmentConfiguration
        {
            get { return practiceAssessmentConfig; }
            set { practiceAssessmentConfig = value; }
        }
        #endregion

        /*
        #region PreAssessment
        private bool preAssessmentEnabled;

        public bool PreAssessmentEnabled
        {
            get { return preAssessmentEnabled; }
            set { preAssessmentEnabled = value; }
        }
        private int preAssessmentNOQuestion;

        public int PreAssessmentNOQuestion
        {
            get { return preAssessmentNOQuestion; }
            set { preAssessmentNOQuestion = value; }
        }
        private int preAssessmentMasteryScore;

        public int PreAssessmentMasteryScore
        {
            get { return preAssessmentMasteryScore; }
            set { preAssessmentMasteryScore = value; }
        }
        private bool preAssessmentRandomizeQuestion;

        public bool PreAssessmentRandomizeQuestion
        {
            get { return preAssessmentRandomizeQuestion; }
            set { preAssessmentRandomizeQuestion = value; }
        }
        private bool preAssessmentRandomizeAnswers;

        public bool PreAssessmentRandomizeAnswers
        {
            get { return preAssessmentRandomizeAnswers; }
            set { preAssessmentRandomizeAnswers = value; }
        }
        private int preAssessmentEnforceMaximumTimeLimit;

        public int PreAssessmentEnforceMaximumTimeLimit
        {
            get { return preAssessmentEnforceMaximumTimeLimit; }
            set { preAssessmentEnforceMaximumTimeLimit = value; }
        }
        private int preAssessmentMaximumNOAttempt;

        public int PreAssessmentMaximumNOAttempt
        {
            get { return preAssessmentMaximumNOAttempt; }
            set { preAssessmentMaximumNOAttempt = value; }
        }
        private string preAssessmentActionToTakeAfterFailingMaxAttempt; //enum

        public string PreAssessmentActionToTakeAfterFailingMaxAttempt
        {
            get { return preAssessmentActionToTakeAfterFailingMaxAttempt; }
            set { preAssessmentActionToTakeAfterFailingMaxAttempt = value; }
        }
        private string preAssessmentScoreType;

        public string PreAssessmentScoreType
        {
            get { return preAssessmentScoreType; }
            set { preAssessmentScoreType = value; }
        }
        private bool preAssessmentProctoredAssessment;

        public bool PreAssessmentProctoredAssessment
        {
            get { return preAssessmentProctoredAssessment; }
            set { preAssessmentProctoredAssessment = value; }
        }
        private bool preAssessmentAllowSkippingQuestion;

        public bool PreAssessmentAllowSkippingQuestion
        {
            get { return preAssessmentAllowSkippingQuestion; }
            set { preAssessmentAllowSkippingQuestion = value; }
        }
        private bool preAssessmentEnforceUniqueQuestionsOnRetake;

        public bool PreAssessmentEnforceUniqueQuestionsOnRetake
        {
            get { return preAssessmentEnforceUniqueQuestionsOnRetake; }
            set { preAssessmentEnforceUniqueQuestionsOnRetake = value; }
        }
        private bool preAssessmentQuestionLevelResult;

        public bool PreAssessmentQuestionLevelResult
        {
            get { return preAssessmentQuestionLevelResult; }
            set { preAssessmentQuestionLevelResult = value; }
        }
        private bool preAssessmentContentRemediation;

        public bool PreAssessmentContentRemediation
        {
            get { return preAssessmentContentRemediation; }
            set { preAssessmentContentRemediation = value; }
        }
        private bool preAssessmentScoreAsYouGo;

        public bool PreAssessmentScoreAsYouGo
        {
            get { return preAssessmentScoreAsYouGo; }
            set { preAssessmentScoreAsYouGo = value; }
        }
        private bool preAssessmentShowQuestionAnswerSummary;

        public bool PreAssessmentShowQuestionAnswerSummary
        {
            get { return preAssessmentShowQuestionAnswerSummary; }
            set { preAssessmentShowQuestionAnswerSummary = value; }
        }
        private bool preAssessmentAllowPauseResumeAssessment;

        public bool PreAssessmentAllowPauseResumeAssessment
        {
            get { return preAssessmentAllowPauseResumeAssessment; }
            set { preAssessmentAllowPauseResumeAssessment = value; }
        }
		private bool preAssessmentRestrictiveMode;

        public bool PreAssessmentRestrictiveMode
        {
            get { return preAssessmentRestrictiveMode; }
            set { preAssessmentRestrictiveMode = value; }
        }
        #endregion
        */
        /*
        #region Post Assessment
        
        private bool postAssessmentEnabled;

        public bool PostAssessmentEnabled
        {
            get { return postAssessmentEnabled; }
            set { postAssessmentEnabled = value; }
        }
        private int postAssessmentNOQuestion;

        public int PostAssessmentNOQuestion
        {
            get { return postAssessmentNOQuestion; }
            set { postAssessmentNOQuestion = value; }
        }
        private int postAssessmentMasteryScore;

        public int PostAssessmentMasteryScore
        {
            get { return postAssessmentMasteryScore; }
            set { postAssessmentMasteryScore = value; }
        }
        private bool postAssessmentRandomizeQuestion;

        public bool PostAssessmentRandomizeQuestion
        {
            get { return postAssessmentRandomizeQuestion; }
            set { postAssessmentRandomizeQuestion = value; }
        }
        private bool postAssessmentRandomizeAnswers;

        public bool PostAssessmentRandomizeAnswers
        {
            get { return postAssessmentRandomizeAnswers; }
            set { postAssessmentRandomizeAnswers = value; }
        }
        private int postAssessmentEnforceMaximumTimeLimit;

        public int PostAssessmentEnforceMaximumTimeLimit
        {
            get { return postAssessmentEnforceMaximumTimeLimit; }
            set { postAssessmentEnforceMaximumTimeLimit = value; }
        }
        private int postAssessmentMaximumNOAttempt;

        public int PostAssessmentMaximumNOAttempt
        {
            get { return postAssessmentMaximumNOAttempt; }
            set { postAssessmentMaximumNOAttempt = value; }
        }
        private string postAssessmentActionToTakeAfterFailingMaxAttempt; //enum

        public string PostAssessmentActionToTakeAfterFailingMaxAttempt
        {
            get { return postAssessmentActionToTakeAfterFailingMaxAttempt; }
            set { postAssessmentActionToTakeAfterFailingMaxAttempt = value; }
        }

        // Added by Mustafa for LCMS-2694
        //private bool postAssessmentRefreshMaxAttemptOnRetake;

        //public bool PostAssessmentRefreshMaxAttemptOnRetake
        //{
        //    get { return postAssessmentRefreshMaxAttemptOnRetake; }
        //    set { postAssessmentRefreshMaxAttemptOnRetake = value; }
        //}
        

        private string postAssessmentScoreType;

        public string PostAssessmentScoreType
        {
            get { return postAssessmentScoreType; }
            set { postAssessmentScoreType = value; }
        }
        private bool postAssessmentProctoredAssessment;

        public bool PostAssessmentProctoredAssessment
        {
            get { return postAssessmentProctoredAssessment; }
            set { postAssessmentProctoredAssessment = value; }
        }
        private bool postAssessmentAllowSkippingQuestion;

        public bool PostAssessmentAllowSkippingQuestion
        {
            get { return postAssessmentAllowSkippingQuestion; }
            set { postAssessmentAllowSkippingQuestion = value; }
        }
        private bool postAssessmentEnforceUniqueQuestionsOnRetake;

        public bool PostAssessmentEnforceUniqueQuestionsOnRetake
        {
            get { return postAssessmentEnforceUniqueQuestionsOnRetake; }
            set { postAssessmentEnforceUniqueQuestionsOnRetake = value; }
        }
        private bool postAssessmentQuestionLevelResult;

        public bool PostAssessmentQuestionLevelResult
        {
            get { return postAssessmentQuestionLevelResult; }
            set { postAssessmentQuestionLevelResult = value; }
        }
        private bool postAssessmentContentRemediation;

        public bool PostAssessmentContentRemediation
        {
            get { return postAssessmentContentRemediation; }
            set { postAssessmentContentRemediation = value; }
        }
        private bool postAssessmentScoreAsYouGo;

        public bool PostAssessmentScoreAsYouGo
        {
            get { return postAssessmentScoreAsYouGo; }
            set { postAssessmentScoreAsYouGo = value; }
        }
        private bool postAssessmentShowQuestionAnswerSummary;

        public bool PostAssessmentShowQuestionAnswerSummary
        {
            get { return postAssessmentShowQuestionAnswerSummary; }
            set { postAssessmentShowQuestionAnswerSummary = value; }
        }
        private bool postAssessmentAllowPauseResumeAssessment;

        public bool PostAssessmentAllowPauseResumeAssessment
        {
            get { return postAssessmentAllowPauseResumeAssessment; }
            set { postAssessmentAllowPauseResumeAssessment = value; }
        }

		private bool postAssessmentRestrictiveMode;

        public bool PostAssessmentRestrictiveMode
        {
            get { return postAssessmentRestrictiveMode; }
            set { postAssessmentRestrictiveMode = value; }
        }
        private int postAssessmentMinimumTimeBeforeStart;

        public int PostAssessmentMinimumTimeBeforeStart
        {
            get { return postAssessmentMinimumTimeBeforeStart; }
            set { postAssessmentMinimumTimeBeforeStart = value; }
        }
        private string postAssessmentMinimumTimeBeforeStartUnit;

        public string PostAssessmentMinimumTimeBeforeStartUnit
        {
            get { return postAssessmentMinimumTimeBeforeStartUnit; }
            set { postAssessmentMinimumTimeBeforeStartUnit = value; }
        }         
        #endregion 
        */
        /*
        #region Quiz
        private bool quizEnabled;

        public bool QuizEnabled
        {
            get { return quizEnabled; }
            set { quizEnabled = value; }
        }
        private int quizNOQuestion;

        public int QuizNOQuestion
        {
            get { return quizNOQuestion; }
            set { quizNOQuestion = value; }
        }
        private int quizMasteryScore;

        public int QuizMasteryScore
        {
            get { return quizMasteryScore; }
            set { quizMasteryScore = value; }
        }
        private bool quizRandomizeQuestion;

        public bool QuizRandomizeQuestion
        {
            get { return quizRandomizeQuestion; }
            set { quizRandomizeQuestion = value; }
        }
        private bool quizRandomizeAnswers;

        public bool QuizRandomizeAnswers
        {
            get { return quizRandomizeAnswers; }
            set { quizRandomizeAnswers = value; }
        }
        private int quizEnforceMaximumTimeLimit;

        public int QuizEnforceMaximumTimeLimit
        {
            get { return quizEnforceMaximumTimeLimit; }
            set { quizEnforceMaximumTimeLimit = value; }
        }
        private int quizMaximumNOAttempt;

        public int QuizMaximumNOAttempt
        {
            get { return quizMaximumNOAttempt; }
            set { quizMaximumNOAttempt = value; }
        }
        private string quizActionToTakeAfterFailingMaxAttempt; //enum

        public string QuizActionToTakeAfterFailingMaxAttempt
        {
            get { return quizActionToTakeAfterFailingMaxAttempt; }
            set { quizActionToTakeAfterFailingMaxAttempt = value; }
        }

        // Added by Mustafa for LCMS-2694
        //private bool quizRefreshMaxAttemptOnRetake;

        //public bool QuizRefreshMaxAttemptOnRetake
        //{
        //    get { return quizRefreshMaxAttemptOnRetake; }
        //    set { quizRefreshMaxAttemptOnRetake = value; }
        //}
        
        private string quizScoreType;

        public string QuizScoreType
        {
            get { return quizScoreType; }
            set { quizScoreType = value; }
        }
        private bool quizProctoredAssessment;

        public bool QuizProctoredAssessment
        {
            get { return quizProctoredAssessment; }
            set { quizProctoredAssessment = value; }
        }
        private bool quizAllowSkippingQuestion;

        public bool QuizAllowSkippingQuestion
        {
            get { return quizAllowSkippingQuestion; }
            set { quizAllowSkippingQuestion = value; }
        }
        private bool quizEnforceUniqueQuestionsOnRetake;

        public bool QuizEnforceUniqueQuestionsOnRetake
        {
            get { return quizEnforceUniqueQuestionsOnRetake; }
            set { quizEnforceUniqueQuestionsOnRetake = value; }
        }
        private bool quizQuestionLevelResult;

        public bool QuizQuestionLevelResult
        {
            get { return quizQuestionLevelResult; }
            set { quizQuestionLevelResult = value; }
        }
        private bool quizContentRemediation;

        public bool QuizContentRemediation
        {
            get { return quizContentRemediation; }
            set { quizContentRemediation = value; }
        }
        private bool quizScoreAsYouGo;

        public bool QuizScoreAsYouGo
        {
            get { return quizScoreAsYouGo; }
            set { quizScoreAsYouGo = value; }
        }
        private bool quizShowQuestionAnswerSummary;

        public bool QuizShowQuestionAnswerSummary
        {
            get { return quizShowQuestionAnswerSummary; }
            set { quizShowQuestionAnswerSummary = value; }
        }
        private bool quizAllowPauseResumeAssessment;

        public bool QuizAllowPauseResumeAssessment
        {
            get { return quizAllowPauseResumeAssessment; }
            set { quizAllowPauseResumeAssessment = value; }
        }
		        private bool quizAssessmentRestrictiveMode;

        public bool QuizAssessmentRestrictiveMode
        {
            get { return quizAssessmentRestrictiveMode; }
            set { quizAssessmentRestrictiveMode = value; }
        }

        #endregion
        */
        #region Validation

        private bool validationRequireIdentityValidation;

        public bool ValidationRequireIdentityValidation
        {
            get { return validationRequireIdentityValidation; }
            set { validationRequireIdentityValidation = value; }
        }
        private int validationTimeBetweenQuestion;

        public int ValidationTimeBetweenQuestion
        {
            get { return validationTimeBetweenQuestion; }
            set { validationTimeBetweenQuestion = value; }
        }
        private int validationTimeToAnswerQuestion;

        public int ValidationTimeToAnswerQuestion
        {
            get { return validationTimeToAnswerQuestion; }
            set { validationTimeToAnswerQuestion = value; }
        }
        private int validationNOMissedQuestionsAllowed;

        public int ValidationNOMissedQuestionsAllowed
        {
            get { return validationNOMissedQuestionsAllowed; }
            set { validationNOMissedQuestionsAllowed = value; }
        }
        private int validationNOValidationQuestion;

        public int ValidationNOValidationQuestion
        {
            get { return validationNOValidationQuestion; }
            set { validationNOValidationQuestion = value; }
        }
        #endregion 


        private bool allowCourseRating;

        public bool AllowCourseRating
        {
            get { return allowCourseRating; }
            set { allowCourseRating = value; }
        }
        private bool playerAllowTOCDisplaySlides;

        public bool PlayerAllowTOCDisplaySlides
        {
            get { return playerAllowTOCDisplaySlides; }
            set { playerAllowTOCDisplaySlides = value; }
        }

        #region EmbeddedAcknowledgment
        private bool embeddedAcknowledgmentEnabled;

        public bool EmbeddedAcknowledgmentEnabled
        {
            get { return embeddedAcknowledgmentEnabled; }
            set { embeddedAcknowledgmentEnabled = value; }
        }

        private string embeddedAcknowledgmentText;

        public string EmbeddedAcknowledgmentText
        {
            get { return embeddedAcknowledgmentText; }
            set { embeddedAcknowledgmentText = value; }
        }

        #endregion

        #region MaximumSeatTime
        private bool seattimeenabled;
        public bool SeatTimeEnabled
        {
            get { return seattimeenabled; }
            set { seattimeenabled = value; }
        }

        private int seattimeinhour;
        public int SeatTimeInHour
        {
            get { return seattimeinhour; }
            set { seattimeinhour = value; }
        }

        private int seattimeinMin;
        public int SeatTimeInMin
        {
            get { return seattimeinMin; }
            set { seattimeinMin = value; }
        }


        private string messageseattimeexceeds;
        public string MessageSeatTimeExceeds
        {
            get { return messageseattimeexceeds; }
            set { messageseattimeexceeds = value; }
        }

        private string messageseattimecourselaunch;
        public string MessageSeatTimeCourseLaunch
        {
            get { return messageseattimecourselaunch; }
            set { messageseattimecourselaunch = value; }
        }
        #endregion


        #region Idle TimeOut
        private string actiontotakeuponidletimeout;
        public string ActionToTakeUponIdleTimeOut
        {
            get { return actiontotakeuponidletimeout; }
            set { actiontotakeuponidletimeout = value; }
        }
        #endregion

        #region Must Start Course Wihtin Specified Amount of Time After Registration Date
        private bool mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF;
        public bool MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF
        {
            get { return mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF; }
            set { mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF = value; }
        }

        private int mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
        public int MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate
        {
            get { return mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate; }
            set { mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = value; }
        }

        private string unitmustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
        public string UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate
        {
            get { return unitmustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate; }
            set { unitmustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = value; }
        }
        #endregion



        #region Special Post Assessment Validation
        private bool isRequireProctorValidation;
        public bool IsRequireProctorValidation
        {
            get { return isRequireProctorValidation; }
            set { isRequireProctorValidation = value; }
        }

        private bool isANSIValidation;
        public bool IsANSIValidation
        {
            get { return isANSIValidation; }
            set { isANSIValidation = value; }
        }

        private bool isNYInsuranceValidation;
        public bool IsNYInsuranceValidation
        {
            get { return isNYInsuranceValidation; }
            set { isNYInsuranceValidation = value; }
        }

        private bool isRequireLearnerValidation;
        public bool IsRequireLearnerValidation
        {
            get { return isRequireLearnerValidation; }
            set { isRequireLearnerValidation = value; }
        }

        private bool isCARealStateValidation;
        public bool IsCARealStateValidation
        {
            get { return isCARealStateValidation; }
            set { isCARealStateValidation = value; }
        }

        #endregion

        //LCMS-10536
        #region InstructorInfo

        private bool instructorInfoEnabled;

        public bool InstructorInfoEnabled
        {
            get { return instructorInfoEnabled; }
            set { instructorInfoEnabled = value; }
        }
        private string instructorInfoText;

        public string InstructorInfoText
        {
            get { return instructorInfoText; }
            set { instructorInfoText = value; }
        }
        #endregion


        public CourseConfiguration()
        {
            
            playerEnableIntroPage	= true;
            playerEnableContent	= false;
            playerEnableEndOfCourseScene	= true;
            playerAllowUserToReviewCourseAfterCompletion	= false;
            playerIdleUserTimeout	= -1;
            playerCourseFlow	=BusinessEntities.CourseFlow.FirstTimeLinear;
            playerEnforceTimedOutline	= true;
            playerCourseEvaluation = false;
            playerDisplayCourseEvaluation = BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment;
            playerMustCompleteCourseEvaluatio = false;
            playerCourseEvaluationInstructions = string.Empty;
            playerAllowTOCDisplaySlides = false;

            completionPostAssessmentAttempted	=   true;
            completionPostAssessmentMastery	=   true;
            completionPreAssessmentMastery	=   false;
            CompletionQuizMastery	=   true;
            completionSurvey	=   false;
            completionViewEverySceneInCourse	=   false;
            completionCompleteAfterNOUniqueCourseVisit	=   -1;
            completionMustCompleteWithinSpecifiedAmountOfTimeMinute	=   -1;
            completionMustCompleteWithinSpecifiedAmountOfTimeDay	=   -1;
            completionUnitOfMustCompleteWithInSpecifiedAmountOfTime = BusinessEntities.TimeUnit.Minutes;
            completionRespondToCourseEvaluation = false;
            //certificate
            certificateEnabled = false;
            certificateAssetID = 0;
            PreAssessmentConfiguration = new AssessmentConfiguration();
            PreAssessmentConfiguration.Enabled = true;
            PreAssessmentConfiguration.NOQuestion = -1;
            PreAssessmentConfiguration.MasteryScore = 80;
            PreAssessmentConfiguration.RandomizeAnswers = true;
            PreAssessmentConfiguration.RandomizeQuestion = true;
            PreAssessmentConfiguration.EnforceMaximumTimeLimit = -1;
            PreAssessmentConfiguration.MaximumNOAttempt = -1;
            PreAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt = BusinessEntities.AfterMaxFailAction.GoToNextLesson;
            PreAssessmentConfiguration.ScoreType = BusinessEntities.ScoreType.PercentScore;
            PreAssessmentConfiguration.ProctoredAssessment = false;
            //PreAssessmentConfiguration.AllowSkippingQuestion = false;
            PreAssessmentConfiguration.EnforceUniqueQuestionsOnRetake = false;
            PreAssessmentConfiguration.QuestionLevelResult = false;
            PreAssessmentConfiguration.ContentRemediation = false;
            //PreAssessmentConfiguration.ScoreAsYouGo = false;
            PreAssessmentConfiguration.ShowQuestionAnswerSummary = false;
            PreAssessmentConfiguration.AllowPauseResumeAssessment = false;
            PreAssessmentConfiguration.MinimumTimeBeforeStart = 0;
            PreAssessmentConfiguration.MinimumTimeBeforeStartUnit = TimeUnit.Minutes;
            PreAssessmentConfiguration.AssessmentResultEnabled = false;

            PostAssessmentConfiguration = new AssessmentConfiguration();
            PostAssessmentConfiguration.Enabled = true;
            PostAssessmentConfiguration.NOQuestion = -1;
            PostAssessmentConfiguration.MasteryScore = 80;
            PostAssessmentConfiguration.RandomizeAnswers = true;
            PostAssessmentConfiguration.RandomizeQuestion = true;
            PostAssessmentConfiguration.EnforceMaximumTimeLimit = -1;
            PostAssessmentConfiguration.MaximumNOAttempt = -1;
            PostAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt = BusinessEntities.AfterMaxFailAction.GoToNextLesson;
            PostAssessmentConfiguration.ScoreType = BusinessEntities.ScoreType.PercentScore;
            PostAssessmentConfiguration.ProctoredAssessment = false;
            //PostAssessmentConfiguration.AllowSkippingQuestion = false;
            PostAssessmentConfiguration.EnforceUniqueQuestionsOnRetake = false;
            PostAssessmentConfiguration.QuestionLevelResult = false;
            PostAssessmentConfiguration.ContentRemediation = false;
            //PostAssessmentConfiguration.ScoreAsYouGo = false;
            PostAssessmentConfiguration.ShowQuestionAnswerSummary = false;
            PostAssessmentConfiguration.AllowPauseResumeAssessment = false;
            PostAssessmentConfiguration.MinimumTimeBeforeStart = 0;
            PostAssessmentConfiguration.MinimumTimeBeforeStartUnit = TimeUnit.Minutes;
            PostAssessmentConfiguration.AssessmentResultEnabled = false;


            QuizConfiguration = new AssessmentConfiguration();
            QuizConfiguration.Enabled = true;
            QuizConfiguration.NOQuestion = -1;
            QuizConfiguration.MasteryScore = 80;
            QuizConfiguration.RandomizeAnswers = true;
            QuizConfiguration.RandomizeQuestion = true;
            QuizConfiguration.EnforceMaximumTimeLimit = -1;
            QuizConfiguration.MaximumNOAttempt = -1;
            QuizConfiguration.ActionToTakeAfterFailingMaxAttempt = BusinessEntities.AfterMaxFailAction.GoToNextLesson;
            QuizConfiguration.ScoreType = BusinessEntities.ScoreType.PercentScore;
            QuizConfiguration.ProctoredAssessment = false;
            //QuizConfiguration.AllowSkippingQuestion = false;
            QuizConfiguration.EnforceUniqueQuestionsOnRetake = false;
            QuizConfiguration.QuestionLevelResult = false;
            QuizConfiguration.ContentRemediation = false;
            //QuizConfiguration.ScoreAsYouGo = false;
            QuizConfiguration.ShowQuestionAnswerSummary = false;
            QuizConfiguration.AllowPauseResumeAssessment = false;
            QuizConfiguration.MinimumTimeBeforeStart = 0;
            QuizConfiguration.MinimumTimeBeforeStartUnit = TimeUnit.Minutes;
            QuizConfiguration.AssessmentResultEnabled = false;

            PracticeAssessmentConfiguration = new AssessmentConfiguration();
            PracticeAssessmentConfiguration.Enabled = true;
            PracticeAssessmentConfiguration.NOQuestion = -1;
            PracticeAssessmentConfiguration.MasteryScore = 80;
            PracticeAssessmentConfiguration.RandomizeAnswers = true;
            PracticeAssessmentConfiguration.RandomizeQuestion = true;
            PracticeAssessmentConfiguration.EnforceMaximumTimeLimit = -1;
            PracticeAssessmentConfiguration.MaximumNOAttempt = -1;
            PracticeAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt = BusinessEntities.AfterMaxFailAction.RetakeLesson;
            PracticeAssessmentConfiguration.ScoreType = BusinessEntities.ScoreType.PercentScore;
            PracticeAssessmentConfiguration.ProctoredAssessment = false;
            //PostAssessmentConfiguration.AllowSkippingQuestion = false;
            PracticeAssessmentConfiguration.EnforceUniqueQuestionsOnRetake = false;
            PracticeAssessmentConfiguration.QuestionLevelResult = false;
            PracticeAssessmentConfiguration.ContentRemediation = false;
            //PostAssessmentConfiguration.ScoreAsYouGo = false;
            PracticeAssessmentConfiguration.ShowQuestionAnswerSummary = false;
            PracticeAssessmentConfiguration.AllowPauseResumeAssessment = false;
            PracticeAssessmentConfiguration.MinimumTimeBeforeStart = 0;
            PracticeAssessmentConfiguration.MinimumTimeBeforeStartUnit = TimeUnit.Minutes;
            PracticeAssessmentConfiguration.AssessmentResultEnabled = false;

            /*
            preAssessmentEnabled	=   true;
            preAssessmentNOQuestion	=   -1;
            preAssessmentMasteryScore	=   80;
            preAssessmentRandomizeQuestion	=   true;
            preAssessmentRandomizeAnswers	=   true;
            preAssessmentEnforceMaximumTimeLimit	=   -1;
            preAssessmentMaximumNOAttempt	=   -1; //-1 means unlimited
            preAssessmentActionToTakeAfterFailingMaxAttempt =   BusinessEntities.AfterMaxFailAction.GoToNextLesson;  
            preAssessmentScoreType  =   BusinessEntities.ScoreType.PercentScore;  
            preAssessmentProctoredAssessment    =  false;
            preAssessmentAllowSkippingQuestion	=   true;
            preAssessmentEnforceUniqueQuestionsOnRetake =   false;
            preAssessmentQuestionLevelResult	=   false;
            preAssessmentContentRemediation	=   false;
            preAssessmentScoreAsYouGo	=   false;
            preAssessmentShowQuestionAnswerSummary	=   false;
            preAssessmentAllowPauseResumeAssessment =   false;

            postAssessmentEnabled	=   true;
            postAssessmentNOQuestion	=   -1;
            postAssessmentMasteryScore	=   80;
            postAssessmentRandomizeQuestion	=   true;
            postAssessmentRandomizeAnswers	=   true;
            postAssessmentEnforceMaximumTimeLimit	=   -1;
            postAssessmentMaximumNOAttempt	=   -1; //-1 means unlimited
            postAssessmentActionToTakeAfterFailingMaxAttempt =   BusinessEntities.AfterMaxFailAction.GoToNextLesson;  
            postAssessmentScoreType  =   BusinessEntities.ScoreType.PercentScore;  
            postAssessmentProctoredAssessment    =  false;
            postAssessmentAllowSkippingQuestion	=   true;
            postAssessmentEnforceUniqueQuestionsOnRetake =   false;
            postAssessmentQuestionLevelResult	=   false;
            postAssessmentContentRemediation	=   false;
            postAssessmentScoreAsYouGo	=   false;
            postAssessmentShowQuestionAnswerSummary	=   false;
            postAssessmentAllowPauseResumeAssessment =   false;
            postAssessmentMinimumTimeBeforeStart = 0;
            postAssessmentMinimumTimeBeforeStartUnit = TimeUnit.Minutes;
            
            quizEnabled	=   true;
            quizNOQuestion	=   -1;
            quizMasteryScore	=   80;
            quizRandomizeQuestion	=   true;
            quizRandomizeAnswers	=   true;
            quizEnforceMaximumTimeLimit	=   -1;
            quizMaximumNOAttempt	=   -1; //-1 means unlimited
            quizActionToTakeAfterFailingMaxAttempt =   BusinessEntities.AfterMaxFailAction.GoToNextLesson;  
            quizScoreType  =   BusinessEntities.ScoreType.PercentScore;  
            quizProctoredAssessment    =  false;
            quizAllowSkippingQuestion	=   true;
            quizEnforceUniqueQuestionsOnRetake =   false;
            quizQuestionLevelResult	=   false;
            quizContentRemediation	=   false;
            quizScoreAsYouGo	=   false;
            quizShowQuestionAnswerSummary	=   false;
            quizAllowPauseResumeAssessment =   false;

            */
            validationRequireIdentityValidation	= false;
            validationTimeBetweenQuestion	=   600;
            validationTimeToAnswerQuestion	=  30;
            validationNOMissedQuestionsAllowed	=   3;
            validationNOValidationQuestion = 1;

            //Embedded Ack. 
            embeddedAcknowledgmentEnabled = false;
            embeddedAcknowledgmentText = string.Empty;

            seattimeenabled = false;
            seattimeinhour = 0;
            messageseattimeexceeds = string.Empty;
            messageseattimecourselaunch = string.Empty;

            mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF = false;
            mustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = 12;
            unitmustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = TimeUnit.Months;
            isRequireProctorValidation = false;
            isRequireLearnerValidation = false;
            isANSIValidation = false;
            isNYInsuranceValidation = false;
            isCARealStateValidation = false;

            //LCMS-10536
            instructorInfoEnabled = false;
            instructorInfoText = string.Empty;

        }
        

    }
}
