using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowStartAssessment
{
    public class StartAssessmentMessage : IDisposable
    {

        private string startupHeading;
        public string StartupHeading
        {
            get { return startupHeading; }
            set { startupHeading = value; }
        }

        private string startupMessage;
        public string StartupMessage
        {
            get { return startupMessage; }
            set { startupMessage = value; }
        }

        private string startupImageUrl;
        public string StartupImageUrl
        {
            get { return startupImageUrl; }
            set { startupImageUrl = value; }
        }

        private string buttonText;
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }

        private int assessmentTimer;
        public int AssessmentTimer
        {
            get { return assessmentTimer; }
            set { assessmentTimer = value; }
        }


        private bool allowSkipping;
        public bool AllowSkipping
        {
            get { return allowSkipping; }
            set { allowSkipping = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
