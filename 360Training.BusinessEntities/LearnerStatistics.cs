using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerStatistics
    {
        private long learnerStatisticsID;

        public long LearnerStatisticsID
        {
            get { return learnerStatisticsID; }
            set { learnerStatisticsID = value; }
        }
        private int learningSession_ID;

        public int LearningSession_ID
        {
            get { return learningSession_ID; }
            set { learningSession_ID = value; }
        }

        private long learnerEnrollment_ID;

        public long LearnerEnrollment_ID
        {
            get { return learnerEnrollment_ID; }
            set { learnerEnrollment_ID = value; }
        }

        private int timeInSeconds;

        public int TimeInSeconds
        {
            get { return timeInSeconds; }
            set { timeInSeconds = value; }
        }
        private string statistic_Type;

        public string Statistic_Type
        {
            get { return statistic_Type; }
            set { statistic_Type = value; }
        }
        private string item_GUID;

        public string Item_GUID
        {
            get { return item_GUID; }
            set { item_GUID = value; }
        }
        private string assessmentType;

        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }
        private int numberAnswersCorrect;

        public int NumberAnswersCorrect
        {
            get { return numberAnswersCorrect; }
            set { numberAnswersCorrect = value; }
        }
        private int numberAnswersIncorrect;

        public int NumberAnswersIncorrect
        {
            get { return numberAnswersIncorrect; }
            set { numberAnswersIncorrect = value; }
        }
        private bool correctTF;

        public bool CorrectTF
        {
            get { return correctTF; }
            set { correctTF = value; }
        }
        private string assessmentItemID;

        public string AssessmentItemID
        {
            get { return assessmentItemID; }
            set { assessmentItemID = value; }
        }
        private int assessmentAttemptNumber;

        public int AssessmentAttemptNumber
        {
            get { return assessmentAttemptNumber; }
            set { assessmentAttemptNumber = value; }
        }
        private string scene_GUID;

        public string Scene_GUID
        {
            get { return scene_GUID; }
            set { scene_GUID = value; }
        }

       
        private List<LearnerStatisticsAnswer> learnerStatisticsAnswers;

        public List<LearnerStatisticsAnswer> LearnerStatisticsAnswers
        {
            get { return learnerStatisticsAnswers; }
            set { learnerStatisticsAnswers = value; }
        }
        private bool maxAtemptActionTaken;

        public bool MaxAtemptActionTaken
        {
            get { return maxAtemptActionTaken; }
            set { maxAtemptActionTaken = value; }
        }
        private int remediationCount;

        public int RemediationCount
        {
            get { return remediationCount; }
            set { remediationCount = value; }
        }

        private double rawScore;

        public double RawScore
        {
            get { return rawScore; }
            set { rawScore = value; }
        }

        private bool isPass;

        public bool IsPass
        {
            get { return isPass; }
            set { isPass = value; }
        }

        private string correctAnswerGuids;

        public string CorrectAnswerGuids
        {
            get { return correctAnswerGuids; }
            set { correctAnswerGuids = value; }
        }

        //LCMS-9213
        private bool answerProvided;
        public bool AnswerProvided
        {
            get { return answerProvided; }
            set { answerProvided = value; }
        }

        //LCMS-9213
        private string questionType;
        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }


        // LCMS-9213
        private string answerTexts;
        public string AnswerTexts  
        {
            get { return answerTexts; }
            set { answerTexts = value; }
        }

        private int testID;

        public int TestID
        {
            get { return testID; }
            set { testID = value; }
        }
        
        //LCMS-10266
        private bool isRepeatedAssessmentAttempt;

        public bool IsRepeatedAssessmentAttempt
        {
            get { return isRepeatedAssessmentAttempt; }
            set { isRepeatedAssessmentAttempt = value; }
        }
        // End  LCMS-10266


        //Abdus Samad
        //LCMS-12105
        //Start
        private bool isAssessmentItemToogled;

        public bool IsAssessmentItemToogled
        {
            get { return isAssessmentItemToogled; }
            set { isAssessmentItemToogled = value; }
        }
        //Stop


        public LearnerStatistics()
        {
            this.assessmentAttemptNumber = 0;
            this.rawScore = -1;
            this.assessmentItemID = string.Empty;
            this.assessmentType = string.Empty;
            this.correctTF = false;
            this.item_GUID = string.Empty;
            this.learnerStatisticsID = 0;
            this.learningSession_ID = 0;
            this.numberAnswersCorrect = 0;
            this.numberAnswersIncorrect = 0;
            this.statistic_Type = string.Empty;
            this.timeInSeconds = 0;
            this.scene_GUID = string.Empty;
            this.learnerStatisticsAnswers = new List<LearnerStatisticsAnswer>();
            this.maxAtemptActionTaken = false;
            this.remediationCount = 0;
            this.isPass = false;
            this.answerProvided = false;
            this.questionType = string.Empty;
            this.answerTexts = string.Empty;
            this.testID = 0;
            this.isRepeatedAssessmentAttempt = false;
        }
    }
}
