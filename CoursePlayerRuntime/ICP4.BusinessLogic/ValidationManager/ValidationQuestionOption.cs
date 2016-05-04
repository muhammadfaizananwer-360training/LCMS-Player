using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.ValidationManager
{
    public class ValidationQuestionOption
    {
        private int validitionQuestionOptionId;

        public int ValiditionQuestionOptionId
        {
            get { return validitionQuestionOptionId; }
            set { validitionQuestionOptionId = value; }
        }

        private string optionLabel;
        public string OptionLabel
        {
            get { return optionLabel; }
            set { optionLabel = value; }
        }

        private string optionValue;
        public string OptionValue
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }
    }
}
