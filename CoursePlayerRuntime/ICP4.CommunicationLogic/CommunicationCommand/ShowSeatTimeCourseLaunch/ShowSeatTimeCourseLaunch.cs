using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSeatTimeCourseLaunch
{
    [XmlRootAttribute("Command")]
    public class ShowSeatTimeCourseLaunch : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private SeatTimeCourseLaunch seatTimeCourseLaunch;
        [XmlElement("CommandData")]
        public SeatTimeCourseLaunch SeatTimeCourseLaunch
        {
            get { return seatTimeCourseLaunch; }
            set { seatTimeCourseLaunch = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
