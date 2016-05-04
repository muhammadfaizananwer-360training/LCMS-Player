using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.StartValidation
{
    [XmlRootAttribute("Command")]
    public class StartValidation:IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private StartValidationMessage startValidationMessage;
        [XmlElement("CommandData")]
        public StartValidationMessage StartValidationMessage
        {
            get { return startValidationMessage; }
            set { startValidationMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
