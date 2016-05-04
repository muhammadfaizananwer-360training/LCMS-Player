using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowErrorMessage
{
    public class ErrorMessage : IDisposable
    {

        private string errorMessageHeading;
        public string ErrorMessageHeading
        {
            get { return errorMessageHeading; }
            set { errorMessageHeading = value; }
        }

        private string errorMessageText;
        public string ErrorMessageText
        {
            get { return errorMessageText; }
            set { errorMessageText = value; }
        }

        private string errorMessageImageURL;
        public string ErrorMessageImageURL
        {
            get { return errorMessageImageURL; }
            set { errorMessageImageURL = value; }
        }

        private string errorMessageButtonText;
        public string ErrorMessageButtonText
        {
            get { return errorMessageButtonText; }
            set { errorMessageButtonText = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
