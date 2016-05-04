using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorMessage
{
    [XmlRootAttribute("Command")]
    public class ShowProctorMessage : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private ProctorMessage proctorMessage;
        [XmlElement("CommandData")]
        public ProctorMessage ProctorMessage
        {
            get { return proctorMessage; }
            set { proctorMessage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
