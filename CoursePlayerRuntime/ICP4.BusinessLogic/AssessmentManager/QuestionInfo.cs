using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.AssessmentManager
{
    public class QuestionInfo
    {
        public QuestionInfo()
        {
            questionID = 0;
            learningObjectiveID = 0;
            isCorrectlyAnswered = false;
            isSkipped = false;
            answerIDs = new List<int>();
            answerTexts = new List<string>();
        
        }

        private int questionID;
        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        private int learningObjectiveID;
        public int AssessmentBinderID
        {
            get { return learningObjectiveID; }
            set { learningObjectiveID = value; }
        }

        private string learningObjectiveName;
        public string AssessmentBinderName
        {
            get { return learningObjectiveName; }
            set { learningObjectiveName = value; }
        }

        
        private string questionType;
        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }

        private string questionGuid;
        public string QuestionGuid
        {
            get { return questionGuid; }
            set { questionGuid = value; }
        }

        private bool isCorrectlyAnswered;
        public bool IsCorrectlyAnswered
        {
            get { return isCorrectlyAnswered; }
            set { isCorrectlyAnswered = value; }
        }
        
        private bool isSkipped;
        public bool IsSkipped
        {
            get { return isSkipped; }
            set { isSkipped = value; }
        }


        private List<int> answerIDs;
        public List<int> AnswerIDs
        {
            get { return answerIDs; }
            set { answerIDs = value; }
        }
        
        private List<string> answerTexts;
        public List<string> AnswerTexts
        {
            get { return answerTexts; }
            set { answerTexts = value; }
        }

        private double scoreWeight;

        public double ScoreWeight
        {
            get { return scoreWeight; }
            set { scoreWeight = value; }
        }

        private bool isExam;
        public bool IsExam
        {
            get { return isExam; }
            set { isExam = value; }
        }

        private int topicID;
        public int TopicID
        {
            get { return topicID; }
            set { topicID = value; }
        }

        private int topicNumber;
        public int TopicNumber
        {
            get { return topicNumber; }
            set { topicNumber = value; }
        }

        private String topicName;
        public String TopicName
        {
            get { return topicName; }
            set { topicName = value; }
        }

        private string correctAssessmentItemGuids;

        public string CorrectAssessmentItemGuids
        {
            get { return correctAssessmentItemGuids; }
            set { correctAssessmentItemGuids = value; }
        }

        private int testID;

        public int TestID
        {
            get { return testID; }
            set { testID = value; }
        }

        //Abdus Samad
        //LCMS-12105
        //Start
        bool toogleFlag;

        public bool ToogleFlag
        {
            get { return toogleFlag; }
            set { toogleFlag = value; }
        }
        //Stop
    }
}
