using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class AssessmentItem
    {
        private String assessmentItemGuid;

        public String AssessmentItemGuid
        {
            get { return assessmentItemGuid; }
            set { assessmentItemGuid = value; }
        }

        private int assessmentItemID;

        public int AssessmentItemID
        {
            get { return assessmentItemID; }
            set { assessmentItemID = value; }
        }

        private int assessmentBinderID;

        public int AssessmentBinderID
        {
            get { return assessmentBinderID; }
            set { assessmentBinderID = value; }
        }

        private string assessmentBinderName;

        public string AssessmentBinderName
        {
            get { return assessmentBinderName; }
            set { assessmentBinderName = value; }
        }


        private string questionStem;

        public string QuestionStem
        {
            get { return questionStem; }
            set { questionStem = value; }
        }

        private string questionType;

        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        private List<AssessmentItemAnswer> assessmentAnswers;

        public List<AssessmentItemAnswer> AssessmentAnswers
        {
            get { return assessmentAnswers; }
            set { assessmentAnswers = value; }
        }

        private string feedback;

        public string Feedback
        {
            get { return feedback; }
            set { feedback = value; }
        }

        private string correctfeedback;
        public string Correctfeedback
        {
            get { return correctfeedback; }
            set { correctfeedback = value; }
        }

        private string incorrectfeedback;
        public string Incorrectfeedback
        {
            get { return incorrectfeedback; }
            set { incorrectfeedback = value; }
        }

        private bool disablerandomizeanswerchoicetf;
        public bool Disablerandomizeanswerchoicetf
        {
            get { return disablerandomizeanswerchoicetf; }
            set { disablerandomizeanswerchoicetf = value; }
        }

        private string assessmentItemTemplateType;

        public string AssessmentItemTemplateType
        {
            get { return assessmentItemTemplateType; }
            set { assessmentItemTemplateType = value; }
        }

        private string feedbacktype;
        public string Feedbacktype
        {
            get { return feedbacktype; }
            set { feedbacktype = value; }
        }

        private double scoreWeight;

        public double ScoreWeight
        {
            get { return scoreWeight; }
            set { scoreWeight = value; }
        }

        private int testID;

        public int TestID
        {
            get { return testID; }
            set { testID = value; }
        }

        public AssessmentItem()
        {
            this.assessmentAnswers = new List<AssessmentItemAnswer>();
            this.assessmentItemID = 0;
            assessmentBinderID = 0;
            this.questionStem = string.Empty;
            this.questionType = BusinessEntities.QuestionType.TrueFalse;
            this.Status = string.Empty;
            this.assessmentItemGuid = string.Empty;
            this.feedback = string.Empty;
            this.incorrectfeedback = string.Empty;
            this.disablerandomizeanswerchoicetf = false;
            this.assessmentItemTemplateType = string.Empty;
            this.feedbacktype = string.Empty;
            this.scoreWeight = 0;
            this.testID = 0; 
        }
    }
}
