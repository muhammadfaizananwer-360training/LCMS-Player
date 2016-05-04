using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage
{
    public class CustomMessage : IDisposable
    {

        private string customMessageType;
        public string CustomMessageType
        {
            get { return customMessageType; }
            set { customMessageType = value; }
        }

        private string messageHeading;
        public string MessageHeading
        {
            get { return messageHeading; }
            set { messageHeading = value; }
        }

        private string messageText;
        public string MessageText
        {
            get { return messageText; }
            set { messageText = value; }
        }

        private string messageImageURL;
        public string MessageImageURL
        {
            get { return messageImageURL; }
            set { messageImageURL = value; }
        }

        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }
        private string redirectURL;

        public string RedirectURL
        {
            get { return redirectURL; }
            set { redirectURL = value; }
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
