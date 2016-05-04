using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore
{
    public class IndividualQuestionScore
    {
        private int assessmentItemID;
        public int AssessmentItemID
        {
            get { return assessmentItemID; }
            set { assessmentItemID = value; }
        }

        private string assessmentItemStem;
        public string AssessmentItemStem
        {
            get { return assessmentItemStem; }
            set { assessmentItemStem = value; }
        }

        private List<string> answers = new List<string>();
        public List<string> Answers
        {
            get { return answers; }
            set { answers = value; }
        }

        private int questionNo;
        public int QuestionNo
        {
            get { return questionNo; }
            set { questionNo = value; }
        }

        private bool isCorrect;
        public bool IsCorrect
        {
            get { return isCorrect; }
            set { isCorrect = value; }
        }
        private string assessmentItemType;
        public string AssessmentItemType
        {
            get { return assessmentItemType; }
            set { assessmentItemType = value; }
        }

        
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
