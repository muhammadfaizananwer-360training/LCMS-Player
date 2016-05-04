using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowAssessmentScoreSummary
{
    [XmlRootAttribute("Command")]
    public class ShowAssessmentScoreSummary : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private AssessmentScoreSummary assessmentScoreSummary;
        [XmlElement("CommandData")]
        public AssessmentScoreSummary AssessmentScoreSummary
        {
            get { return assessmentScoreSummary; }
            set { assessmentScoreSummary = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
