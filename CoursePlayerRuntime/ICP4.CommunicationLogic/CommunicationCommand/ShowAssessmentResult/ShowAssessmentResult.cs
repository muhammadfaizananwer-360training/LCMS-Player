using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowAssessmentResult
{
    [XmlRootAttribute("Command")]
    public class ShowAssessmentResult : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private AssessmentResultMessage assessmentResultMessage;
        [XmlElement("CommandData")]
        public AssessmentResultMessage AssessmentResultMessage
        {
            get { return assessmentResultMessage; }
            set { assessmentResultMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
