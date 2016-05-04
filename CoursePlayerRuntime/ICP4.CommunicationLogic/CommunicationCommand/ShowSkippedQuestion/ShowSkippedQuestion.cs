using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSkippedQuestion
{
    [XmlRootAttribute("Command")]
    public class ShowSkippedQuestion : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private string logOutText;
        [XmlAttribute]
        public string LogOutText
        {
            get { return logOutText; }
            set { logOutText = value; }
        }

        private List<SkippedQuestion> skippedQuestions;
        [XmlArrayAttribute("CommandData")]
        public List<SkippedQuestion> SkippedQuestions
        {
            get { return skippedQuestions; }
            set { skippedQuestions = value; }
        }



        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
