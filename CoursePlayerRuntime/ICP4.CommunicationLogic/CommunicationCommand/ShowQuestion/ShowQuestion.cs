using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    [XmlRootAttribute("Command")]
    public class ShowQuestion : IDisposable
    {
        private string commandName;
        private string assessmentType;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        [XmlAttribute]
        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }

        private AssessmentItem assessmentItem;
        [XmlElement("CommandData")]
        public AssessmentItem AssessmentItem
        {
            get { return assessmentItem; }
            set { assessmentItem = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
