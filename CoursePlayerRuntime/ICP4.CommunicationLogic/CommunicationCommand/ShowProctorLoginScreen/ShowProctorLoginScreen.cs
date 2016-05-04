using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowProctorLoginScreen
{
    [XmlRootAttribute("Command")]
    public class ShowProctorLoginScreen : IDisposable
    {

        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }
        

        private ProctorLoginScreen proctorLoginScreen;
        [XmlElement("CommandData")]
        public ProctorLoginScreen ProctorLoginScreen
        {
            get { return proctorLoginScreen; }
            set { proctorLoginScreen = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
