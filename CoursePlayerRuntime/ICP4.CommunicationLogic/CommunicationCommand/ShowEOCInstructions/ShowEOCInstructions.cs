using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
 
namespace ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions
{
    [XmlRootAttribute("Command")]
    public class ShowEOCInstructions : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private EOCInstructions eocInstructions;
        [XmlElement("CommandData")]
        public EOCInstructions EOCInstructions
        {
            get { return eocInstructions; }
            set { eocInstructions = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
