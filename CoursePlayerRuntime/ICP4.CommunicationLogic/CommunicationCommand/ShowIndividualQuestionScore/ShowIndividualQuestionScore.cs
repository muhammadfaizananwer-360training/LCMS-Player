using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore
{
    [XmlRootAttribute("Command")]
    public class ShowIndividualQuestionScore
    {

        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private List<IndividualQuestionScore> individualQuestionScores;
        [XmlArrayAttribute("CommandData")]
        public List<IndividualQuestionScore> IndividualQuestionScores
        {
            get { return individualQuestionScores; }
            set { individualQuestionScores = value; }
        }



        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    
}
