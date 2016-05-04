using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.OpenCommand
{
    public class OpenCommandMessage
    {
        private bool contuniueAskingValidationQuestion;

        public bool ContuniueAskingValidationQuestion
        {
            get { return contuniueAskingValidationQuestion; }
            set { contuniueAskingValidationQuestion = value; }
        }
    }
}
