using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowStartAssessment
{
     
    [XmlRootAttribute("Command")]
    public class ShowStartAssessment : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private StartAssessmentMessage startAssessmentMessage;
        [XmlElement("CommandData")]
        public StartAssessmentMessage StartAssessmentMessage
        {
            get { return startAssessmentMessage; }
            set { startAssessmentMessage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
