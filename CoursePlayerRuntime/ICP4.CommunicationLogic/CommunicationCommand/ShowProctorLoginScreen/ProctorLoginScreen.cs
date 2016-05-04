using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorLoginScreen
{
    public class ProctorLoginScreen :IDisposable
    {
        private string proctorLoginScreenContent;
        public string ProctorLoginScreenContent
        {
            get { return proctorLoginScreenContent; }
            set { proctorLoginScreenContent = value; }
        }

        private string proctorLoginScreenHeading;
        public string ProctorLoginScreenHeading
        {
            get { return proctorLoginScreenHeading; }
            set { proctorLoginScreenHeading = value; }
        }

        private string proctorLoginScreenHeadingImage;
        public string ProctorLoginScreenHeadingImage
        {
            get { return proctorLoginScreenHeadingImage; }
            set { proctorLoginScreenHeadingImage = value; }
        }

        private string proctorLoginScreenSubmitButtonText;
        public string ProctorLoginScreenSubmitButtonText
        {
            get { return proctorLoginScreenSubmitButtonText; }
            set { proctorLoginScreenSubmitButtonText = value; }
        }

        private string proctorLoginScreenProgressAnimation;
        public string ProctorLoginScreenProgressAnimation
        {
            get { return proctorLoginScreenProgressAnimation; }
            set { proctorLoginScreenProgressAnimation = value; }
        }

        private string proctorLoginScreenAttempts;
        public string ProctorLoginScreenAttempts
        {
            get { return proctorLoginScreenAttempts; }
            set { proctorLoginScreenAttempts = value; }
        }

        private string proctorLoginTableText;

        public string ProctorLoginTableText
        {
            get { return proctorLoginTableText; }
            set { proctorLoginTableText = value; }
        }
        private string proctorLoginIDText;

        public string ProctorLoginIDText
        {
            get { return proctorLoginIDText; }
            set { proctorLoginIDText = value; }
        }
        private string proctorPasswordText;

        public string ProctorPasswordText
        {
            get { return proctorPasswordText; }
            set { proctorPasswordText = value; }
        }
        private string proctorLoginIDRequiredValidationText;

        public string ProctorLoginIDRequiredValidationText
        {
            get { return proctorLoginIDRequiredValidationText; }
            set { proctorLoginIDRequiredValidationText = value; }
        }
        private string proctorPasswordRequiredValidationText;

        public string ProctorPasswordRequiredValidationText
        {
            get { return proctorPasswordRequiredValidationText; }
            set { proctorPasswordRequiredValidationText = value; }
        }

        private string proctorattemptMessageText;

        public string ProctorattemptMessageText
        {
            get { return proctorattemptMessageText; }
            set { proctorattemptMessageText = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}
