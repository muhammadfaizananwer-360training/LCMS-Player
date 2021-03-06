﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowTopicScoreSummary
{
    [XmlRootAttribute("Command")]
    public class ShowTopicScoreSummary : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private TopicScoreSummary topicScoreSummary;
        [XmlElement("CommandData")]
        public TopicScoreSummary TopicScoreSummary
        {
            get { return topicScoreSummary; }
            set { topicScoreSummary = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
