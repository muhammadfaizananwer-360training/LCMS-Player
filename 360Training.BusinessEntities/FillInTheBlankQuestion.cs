using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public abstract class FillInTheBlankQuestion :AssessmentItem
    {
        private bool isAnswerCaseSensitive;

        public bool IsAnswerCaseSensitive
        {
            get { return isAnswerCaseSensitive; }
            set { isAnswerCaseSensitive = value; }
        }

        public FillInTheBlankQuestion()
        {
            this.isAnswerCaseSensitive = false;
        }
    }
}
