using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowDocuSignRequirementAffidavit
{
    [XmlRootAttribute("Command")]
    public class ShowDocuSignRequirementAffidavit : IDisposable 
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private DocuSignRequirementAffidavit affidavit;

        public DocuSignRequirementAffidavit Affidavit
        {
            get { return affidavit; }
            set { affidavit = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
