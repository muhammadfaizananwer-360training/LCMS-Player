using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ValidationIdentityQuestion
{
    [XmlRootAttribute("Command")]
    public class ShowValidationIdentityQuestion : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }



        private ValidationIdentityQuestion validationIdentityQuestion;
        [XmlElement("CommandData")]
        public ValidationIdentityQuestion ValidationIdentityQuestion
        {
            get { return validationIdentityQuestion; }
            set { validationIdentityQuestion = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}