using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class AssessmentItemAnswer
    {
        private String assessmentItemAnswerGuid;

        public String AssessmentItemAnswerGuid
        {
            get { return assessmentItemAnswerGuid; }
            set { assessmentItemAnswerGuid = value; }
        }


        private int assessmentItemAnswerID;

        public int AssessmentItemAnswerID
        {
            get { return assessmentItemAnswerID; }
            set { assessmentItemAnswerID = value; }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private bool isCorrect;

        public bool IsCorrect
        {
            get { return isCorrect; }
            set { isCorrect = value; }
        }

        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
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


        private bool usedefaultfeedbacktf;
        public bool Usedefaultfeedbacktf
        {
            get { return usedefaultfeedbacktf; }
            set { usedefaultfeedbacktf = value; }
        }

        public AssessmentItemAnswer()
        {
            this.assessmentItemAnswerID = 0;
            this.label = string.Empty;
            this.value = string.Empty;
            this.isCorrect = false;
            this.displayOrder = 0;
            this.assessmentItemAnswerGuid = string.Empty;
            this.feedback = string.Empty;
            this.correctfeedback = string.Empty;
            this.incorrectfeedback = string.Empty;
            this.usedefaultfeedbacktf = false;
        }
    }
}
