using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSpecialPostAssessmentValidation
{
    public class SpecialPostAssessmentValidation : IDisposable
    {

        private string template;
        public string Template
        {
            get { return template; }
            set { template = value; }
        }

        private string nYInsuranceRequiredValidationMessage;

        public string NYInsuranceRequiredValidationMessage
        {
            get { return nYInsuranceRequiredValidationMessage; }
            set { nYInsuranceRequiredValidationMessage = value; }
        }

        private string cARealStateRequiredValidationMessage1;

        public string CARealStateRequiredValidationMessage1
        {
            get { return cARealStateRequiredValidationMessage1; }
            set { cARealStateRequiredValidationMessage1 = value; }
        }
        private string cARealStateRequiredValidationMessage2;

        public string CARealStateRequiredValidationMessage2
        {
            get { return cARealStateRequiredValidationMessage2; }
            set { cARealStateRequiredValidationMessage2 = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
