using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowFinalExamLocked
{
    [XmlRootAttribute("Command")]
    public class ShowFinalExamLocked : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private FinalExamLocked finalExamLocked;
        [XmlElement("CommandData")]
        public FinalExamLocked FinalExamLocked
        {
            get { return finalExamLocked; }
            set { finalExamLocked = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
