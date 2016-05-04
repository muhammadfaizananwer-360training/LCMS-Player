using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorMessage
{
 
    public class ProctorMessage : IDisposable
    {
        private string proctorMessageHeading;
        public string ProctorMessageHeading
        {
            get { return proctorMessageHeading; }
            set { proctorMessageHeading = value; }
        }

        private string proctorMessageText;
        public string ProctorMessageText
        {
            get { return proctorMessageText; }
            set { proctorMessageText = value; }
        }

        private string proctorMessageImageUrl;
        public string ProctorMessageImageUrl
        {
            get { return proctorMessageImageUrl; }
            set { proctorMessageImageUrl = value; }
        }


        private string proctorMessageButtonText;
        public string ProctorMessageButtonText
        {
            get { return proctorMessageButtonText; }
            set { proctorMessageButtonText = value; }
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

        private bool showGradeAssessment;

        public bool ShowGradeAssessment
        {
            get { return showGradeAssessment; }
            set { showGradeAssessment = value; }
        }
		private bool isRestrictiveAssessmentEngine;
        public bool IsRestrictiveAssessmentEngine
        {
            get { return isRestrictiveAssessmentEngine; }
            set { isRestrictiveAssessmentEngine = value; }
        }

        public bool IsAssessmentResumeMessage { get; set; }

        public int InitialAssessmentTimerValue { get; set; }
        public bool IsConfigurationModified { get; set; }

        private bool lockoutClickAwayToActiveWindow;
        public bool LockoutClickAwayToActiveWindow
        {
            get { return lockoutClickAwayToActiveWindow; }
            set { lockoutClickAwayToActiveWindow = value; }
        }

        private string logOutText ="";
        public string LogOutText
        {
            get { return logOutText; }
            set { logOutText = value; }
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
