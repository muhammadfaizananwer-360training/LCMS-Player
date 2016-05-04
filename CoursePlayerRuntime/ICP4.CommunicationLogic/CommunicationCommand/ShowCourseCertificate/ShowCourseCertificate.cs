using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseCertificate
{
    [XmlRootAttribute("Command")]
    public class ShowCourseCertificate : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private CourseCertificate certificates;

        public CourseCertificate Certificates
        {
            get { return certificates; }
            set { certificates = value; }
        }
      
        
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
