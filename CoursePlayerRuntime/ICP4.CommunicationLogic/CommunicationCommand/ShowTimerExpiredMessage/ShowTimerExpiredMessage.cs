using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTimerExpiredMessage
{
    [XmlRootAttribute("Command")]
    public class ShowTimerExpiredMessage : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private TimerExpiredMessage timerExpiredMessage;
        [XmlElement("CommandData")]
        public TimerExpiredMessage TimerExpiredMessage
        {
            get { return timerExpiredMessage; }
            set { timerExpiredMessage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
