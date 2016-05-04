using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class MatchingAssessmentItemAnswer : AssessmentItemAnswer
    {
        private string rightItemText;

        public string RightItemText
        {
            get { return rightItemText; }
            set { rightItemText = value; }
        }

        private int rightItemOrder;

        public int RightItemOrder
        {
            get { return rightItemOrder; }
            set { rightItemOrder = value; }
        }

        private string leftItemText;

        public string LeftItemText
        {
            get { return leftItemText; }
            set { leftItemText = value; }
        }

        private int leftItemOrder;

        public int LeftItemOrder
        {
            get { return leftItemOrder; }
            set { leftItemOrder = value; }
        }

        public MatchingAssessmentItemAnswer()
        {
            this.rightItemText = String.Empty;
            this.rightItemOrder = 0;
            this.leftItemText = String.Empty;
            this.leftItemOrder = 0;
        }
    }
}
