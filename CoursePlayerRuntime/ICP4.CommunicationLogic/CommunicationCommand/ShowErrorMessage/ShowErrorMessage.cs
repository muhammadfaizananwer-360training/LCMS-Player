using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowErrorMessage
{
    [XmlRootAttribute("Command")]
    public  class ShowErrorMessage : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private ErrorMessage errorMessage;
        [XmlElement("CommandData")]
        public ErrorMessage ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
