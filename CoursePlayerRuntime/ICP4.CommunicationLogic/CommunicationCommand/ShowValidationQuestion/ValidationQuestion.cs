using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowValidationQuestion
{
    public class ValidationQuestion
    {
        int validationQuestionID;

        public int ValidationQuestionID
        {
            get { return validationQuestionID; }
            set { validationQuestionID = value; }
        }
        string validationQuestionText;

        public string ValidationQuestionText
        {
            get { return validationQuestionText; }
            set { validationQuestionText = value; }
        }
        int validationQuestionTimer;

        public int ValidationQuestionTimer
        {
            get { return validationQuestionTimer; }
            set { validationQuestionTimer = value; }
        }

        string validationQuestionType;

        public string ValidationQuestionType
        {
            get { return validationQuestionType; }
            set { validationQuestionType = value; }
        }

        string validationQuestionOptions;

        public string ValidationQuestionOptions
        {
            get { return validationQuestionOptions; }
            set { validationQuestionOptions = value; }
        }

    }
}
