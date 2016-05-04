using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTimerExpiredMessage
{
    public class TimerExpiredMessage
    {
        private string timerExpiredMessageHeading;
        public string TimerExpiredMessageHeading
        {
            get { return timerExpiredMessageHeading; }
            set { timerExpiredMessageHeading = value; }
        }

        private string timerExpiredMessageText;
        public string TimerExpiredMessageText
        {
            get { return timerExpiredMessageText; }
            set { timerExpiredMessageText = value; }
        }

        private string timerExpiredMessageImageUrl;
        public string TimerExpiredMessageImageUrl
        {
            get { return timerExpiredMessageImageUrl; }
            set { timerExpiredMessageImageUrl = value; }
        }


        private string timerExpiredMessageButtonText;
        public string TimerExpiredMessageButtonText
        {
            get { return timerExpiredMessageButtonText; }
            set { timerExpiredMessageButtonText = value; }
        }


    }
}
