using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowAnswerReview
{
    [XmlRootAttribute("Command")]
    public class ShowAnswerReview : IDisposable
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

        private List<AnswerReview> answerReviews;
        [XmlArrayAttribute("CommandData")]
        public List<AnswerReview> AnswerReviews
        {
            get { return answerReviews; }
            set { answerReviews = value; }
        }



        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
