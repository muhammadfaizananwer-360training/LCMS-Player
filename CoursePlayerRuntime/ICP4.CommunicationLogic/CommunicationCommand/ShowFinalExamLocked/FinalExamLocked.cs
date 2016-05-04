using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowFinalExamLocked
{
    public class FinalExamLocked : IDisposable
    {
        private string finalExamLockedHeading;

        public string FinalExamLockedHeading
        {
            get { return finalExamLockedHeading; }
            set { finalExamLockedHeading = value; }
        }
        private string finalExamLockedHelpText;

        public string FinalExamLockedHelpText
        {
            get { return finalExamLockedHelpText; }
            set { finalExamLockedHelpText = value; }
        }
        private string finalExamLockedImage;

        public string FinalExamLockedImage
        {
            get { return finalExamLockedImage; }
            set { finalExamLockedImage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
