using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
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

  

        private string leftItemText;

        public string LeftItemText
        {
            get { return leftItemText; }
            set { leftItemText = value; }
        }



        public MatchingAssessmentItemAnswer()
        {
            this.rightItemText = String.Empty;
            this.leftItemText = String.Empty;
        }
    }
}
