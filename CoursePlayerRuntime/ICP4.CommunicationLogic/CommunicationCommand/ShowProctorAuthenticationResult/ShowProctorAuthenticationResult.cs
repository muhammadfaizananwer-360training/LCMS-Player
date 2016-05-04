using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorAuthenticationResult
{
    [XmlRootAttribute("Command")]
    public class ShowProctorAuthenticationResult : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private ProctorAuthenticationResult proctorAuthenticationResult;
        [XmlElement("CommandData")]
        public ProctorAuthenticationResult ProctorAuthenticationResult
        {
            get { return proctorAuthenticationResult; }
            set { proctorAuthenticationResult = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
