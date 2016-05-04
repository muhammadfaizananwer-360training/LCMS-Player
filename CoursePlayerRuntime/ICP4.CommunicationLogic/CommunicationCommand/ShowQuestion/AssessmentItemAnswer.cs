using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    [Serializable]
    public class AssessmentItemAnswer
    {
        private string assessmentItemAnswerGuid;

        public string AssessmentItemAnswerGuid
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



        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }

        public AssessmentItemAnswer()
        {
            this.assessmentItemAnswerID = 0;
            this.label = string.Empty;
            this.value = string.Empty;
            this.displayOrder = 0;
            this.assessmentItemAnswerGuid = string.Empty;
        }

    }
}
