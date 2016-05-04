using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowLocalizedInfo
{
   
    [XmlRootAttribute("Command")]
    public class ShowLocalizedInfo : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private LocalizedInfo localizedInfo;
        [XmlElement("CommandData")]
        public LocalizedInfo LocalizedInfo
        {
            get { return localizedInfo; }
            set { localizedInfo = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
