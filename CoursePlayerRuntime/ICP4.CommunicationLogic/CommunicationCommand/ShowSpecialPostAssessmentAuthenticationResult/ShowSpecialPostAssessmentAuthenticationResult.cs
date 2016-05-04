using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSpecialPostAssessmentAuthenticationResult
{
    public class ShowSpecialPostAssessmentAuthenticationResult : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private SpecialPostAssessmentAuthenticationResult specialPostAssessmentAuthenticationResult;
        [XmlElement("CommandData")]
        public SpecialPostAssessmentAuthenticationResult SpecialPostAssessmentAuthenticationResult
        {
            get { return specialPostAssessmentAuthenticationResult; }
            set { specialPostAssessmentAuthenticationResult = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
