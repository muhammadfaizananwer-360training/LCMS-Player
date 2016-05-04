using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{



    public class AssessmentConfiguration
    {

        public static readonly string ASSESSMENTYPE_QUIZ = "Quiz";
        public static readonly string ASSESSMENTYPE_PREASSESSMENT = "PreAssessment";
        public static readonly string ASSESSMENTYPE_POSTASSESSMET = "PostAssessment";
        public static readonly string ASSESSMENTYPE_PRACTICEEXAM = "PracticeExam";


        public static readonly string ADVANCEQUESTIONSELECTIONTYPE_MINMAX = "MinMax";
        public static readonly string ADVANCEQUESTIONSELECTIONTYPE_DISCRETE = "Discrete";
        public static readonly string ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE = "RandomAlternate";

        public static readonly string GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED = "AfterAssessmentIsSubmitted";
        public static readonly string GRADEQUESTION_AFTER_EACH_QUESTION_IS_ANSWERED = "AfterEachQuestionIsAnswered";

        public static readonly string SHOWTHELEARNER_NORESULTS = BusinessEntities.ScoreType.NoResults;
        


        public AssessmentConfiguration()
        {
            id = 0;
            useWeightedScore = false;
            advanceQuestionSelectionType = ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE;
            enabled = true;
            noQuestion = -1;
            masteryScore = 80;
            randomizeQuestion = true;
            randomizeAnswers = true;
            enforceMaximumTimeLimit = -1;
            maximumNOAttempt = -1; //-1 means unlimited
            actionToTakeAfterFailingMaxAttempt = BusinessEntities.AfterMaxFailAction.GoToNextLesson;
            scoreType = BusinessEntities.ScoreType.PercentScore;
            proctoredAssessment = false;
            allowSkippingQuestion = true;
            enforceUniqueQuestionsOnRetake = false;
            questionLevelResult = false;
            contentRemediation = false;
            scoreAsYouGo = false;
            showQuestionAnswerSummary = false;
            allowPauseResumeAssessment = false;
            restrictiveMode = false;
            minimumTimeBeforeStart = 0;
            minimumTimeBeforeStartUnit = TimeUnit.Minutes;
            strictlyEnforcePolicyToBeUsed = false;
            displaySeatTimeSatisfiedMessageTF = false;
            allowPostAssessmentAfterSeatTimeSatisfiedTF = false;
            maxAttemptHandlerEnabled = false;
            assessmentResultEnabled = false;
        }


        #region Exam Prep Policies    

        private string assessmentType;
        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }

        private string advanceQuestionSelectionType;
        public string AdvanceQuestionSelectionType
        {
            get { return advanceQuestionSelectionType; }
            set { advanceQuestionSelectionType = value; }
        }



        private bool useWeightedScore;
        public bool UseWeightedScore
        {
            get { return useWeightedScore; }
            set { useWeightedScore = value; }
        }

        /*
        private bool useWeightedScore;
        public bool UseWeightedScore
        {
            get { return useWeightedScore; }
            set { useWeightedScore = value; }
        }

        private bool useMinMaxQuestionsSelection;
        public bool UseMinMaxQuestionsSelection
        {
            get { return useMinMaxQuestionsSelection; }
            set { useMinMaxQuestionsSelection = value; }
        }

        private bool useDiscreteQuestionsSelection;
        public bool UseDiscreteQuestionsSelection
        {
            get { return useDiscreteQuestionsSelection; }
            set { useDiscreteQuestionsSelection = value; }
        }
        */


        #endregion


        #region Assessment Policies

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private int noQuestion;
        public int NOQuestion
        {
            get { return noQuestion; }
            set { noQuestion = value; }
        }
        
        private int masteryScore;
        public int MasteryScore
        {
            get { return masteryScore; }
            set { masteryScore = value; }
        }
        
        private bool randomizeQuestion;
        public bool RandomizeQuestion
        {
            get { return randomizeQuestion; }
            set { randomizeQuestion = value; }
        }

        private bool randomizeAnswers;
        public bool RandomizeAnswers
        {
            get { return randomizeAnswers; }
            set { randomizeAnswers = value; }
        }

        private int enforceMaximumTimeLimit;
        public int EnforceMaximumTimeLimit
        {
            get { return enforceMaximumTimeLimit; }
            set { enforceMaximumTimeLimit = value; }
        }

        private int maximumNOAttempt;
        public int MaximumNOAttempt
        {
            get { return maximumNOAttempt; }
            set { maximumNOAttempt = value; }
        }

        private string actionToTakeAfterFailingMaxAttempt; //enum
        public string ActionToTakeAfterFailingMaxAttempt
        {
            get { return actionToTakeAfterFailingMaxAttempt; }
            set { actionToTakeAfterFailingMaxAttempt = value; }
        }

        private string scoreType;
        public string ScoreType
        {
            get { return scoreType; }
            set { scoreType = value; }
        }
        
        private bool proctoredAssessment;
        public bool ProctoredAssessment
        {
            get { return proctoredAssessment; }
            set { proctoredAssessment = value; }
        }

        private bool allowSkippingQuestion;
        public bool AllowSkippingQuestion
        {
            get { return allowSkippingQuestion; }
            set { allowSkippingQuestion = value; }
        }

        private bool enforceUniqueQuestionsOnRetake;
        public bool EnforceUniqueQuestionsOnRetake
        {
            get { return enforceUniqueQuestionsOnRetake; }
            set { enforceUniqueQuestionsOnRetake = value; }
        }

        private bool questionLevelResult;
        public bool QuestionLevelResult
        {
            get { return questionLevelResult; }
            set { questionLevelResult = value; }
        }

        private bool contentRemediation;
        public bool ContentRemediation
        {
            get { return contentRemediation; }
            set { contentRemediation = value; }
        }

        private bool scoreAsYouGo;
        public bool ScoreAsYouGo
        {
            get { return scoreAsYouGo; }
            set { scoreAsYouGo = value; }
        }

        private bool showQuestionAnswerSummary;
        public bool ShowQuestionAnswerSummary
        {
            get { return showQuestionAnswerSummary; }
            set { showQuestionAnswerSummary = value; }
        }

        private bool allowPauseResumeAssessment;
        public bool AllowPauseResumeAssessment
        {
            get { return allowPauseResumeAssessment; }
            set { allowPauseResumeAssessment = value; }
        }

        private bool restrictiveMode;
        public bool RestrictiveMode
        {
            get { return restrictiveMode; }
            set { restrictiveMode = value; }
        }

        private int minimumTimeBeforeStart;
        public int MinimumTimeBeforeStart
        {
            get { return minimumTimeBeforeStart; }
            set { minimumTimeBeforeStart = value; }
        }

        private string minimumTimeBeforeStartUnit;
        public string MinimumTimeBeforeStartUnit
        {
            get { return minimumTimeBeforeStartUnit; }
            set { minimumTimeBeforeStartUnit = value; }
        }

        private bool strictlyEnforcePolicyToBeUsed;
        public bool StrictlyEnforcePolicyToBeUsed
        {
            get { return strictlyEnforcePolicyToBeUsed; }
            set { strictlyEnforcePolicyToBeUsed = value; }
        }

        private string gradeQuestions;

        public string GradeQuestions
        {
            get { return gradeQuestions; }
            set { gradeQuestions = value; }
        }



        private bool displaySeatTimeSatisfiedMessageTF;
        public bool DisplaySeatTimeSatisfiedMessageTF
        {
            get { return displaySeatTimeSatisfiedMessageTF; }
            set { displaySeatTimeSatisfiedMessageTF = value; }
        }


        private bool allowPostAssessmentAfterSeatTimeSatisfiedTF;
        public bool AllowPostAssessmentAfterSeatTimeSatisfiedTF
        {
            get { return allowPostAssessmentAfterSeatTimeSatisfiedTF; }
            set { allowPostAssessmentAfterSeatTimeSatisfiedTF = value; }
        }

        private string noResultText;

        public string NoResultText
        {
            get { return noResultText; }
            set { noResultText = value; }
        }


        private bool maxAttemptHandlerEnabled;
        public bool MaxAttemptHandlerEnabled
        {
            get { return maxAttemptHandlerEnabled; }
            set { maxAttemptHandlerEnabled = value; }
        }

        private bool lockoutFuntionalityClickAwayToActiveWindowEnable;
        public bool LockoutFuntionalityClickAwayToActiveWindowEnable
        {
            get { return lockoutFuntionalityClickAwayToActiveWindowEnable; }
            set { lockoutFuntionalityClickAwayToActiveWindowEnable = value; }
        }

        private bool assessmentResultEnabled;

        public bool AssessmentResultEnabled
        {
            get { return assessmentResultEnabled; }
            set { assessmentResultEnabled = value; }
        } 

        #endregion

       

    }
}
