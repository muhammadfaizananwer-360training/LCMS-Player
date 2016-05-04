using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.SeatTimeExceedCommand
{
    [XmlRootAttribute("Command")]
    public class ShowSeatTimeExceed : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return CommandNames.ShowSeatTimeExceed; }
            set { commandName = value; }
        }


        private SeatTimeExceedMessage seatTimeExceedMessage;
        [XmlElement("CommandData")]
        public SeatTimeExceedMessage SeatTimeExceedMessage
        {
            get { return seatTimeExceedMessage; }
            set { seatTimeExceedMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
