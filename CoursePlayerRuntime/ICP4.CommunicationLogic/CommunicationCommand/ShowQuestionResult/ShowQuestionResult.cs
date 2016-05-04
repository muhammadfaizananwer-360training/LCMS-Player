using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestionResult
{
    [XmlRootAttribute("Command")]
    public class ShowQuestionResult : IDisposable
    {
        private string commandName;
        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private QuestionResult questionResult;
        [XmlElement("CommandData")]
        public QuestionResult QuestionResult
        {
            get { return questionResult; }
            set { questionResult = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
