using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorAuthenticationResult
{
    public class ProctorAuthenticationResult : IDisposable
    {

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
