using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowEmbeddedAcknowledgment
{
    [XmlRootAttribute("Command")]
    public class ShowEmbeddedAcknowledgment : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private EmbeddedAcknowledgment embeddedAcknowledgment;
        [XmlElement("CommandData")]
        public EmbeddedAcknowledgment EmbeddedAcknowledgment
        {
            get { return embeddedAcknowledgment; }
            set { embeddedAcknowledgment = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
