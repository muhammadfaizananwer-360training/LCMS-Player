using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowGlossaryInDetail
{
    [XmlRootAttribute("Command")]
    public class ShowGlossaryInDetail : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private GlossaryDetail glossaryDetail;
        [XmlElement("CommandData")]
        public GlossaryDetail GlossaryDetail
        {
            get { return glossaryDetail; }
            set { glossaryDetail = value; }
        }
        

        

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
        

    }
    
}
