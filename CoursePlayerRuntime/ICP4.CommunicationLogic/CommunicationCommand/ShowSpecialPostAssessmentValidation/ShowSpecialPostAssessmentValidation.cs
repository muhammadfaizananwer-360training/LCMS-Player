using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSpecialPostAssessmentValidation
{
    [XmlRootAttribute("Command")]
    public class ShowSpecialPostAssessmentValidation : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private SpecialPostAssessmentValidation specialPostAssessmentValidation;
        [XmlElement("CommandData")]
        public SpecialPostAssessmentValidation SpecialPostAssessmentValidation
        {
            get { return specialPostAssessmentValidation; }
            set { specialPostAssessmentValidation = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
