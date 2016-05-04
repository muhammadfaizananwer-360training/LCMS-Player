using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseLevelRating
{
    [XmlRootAttribute("Command")]
    public class ShowCourseLevelRating : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseLevelRating courseLevelRating;
        [XmlElement("CommandData")]
        public CourseLevelRating CourseLevelRating
        {
            get { return courseLevelRating; }
            set { courseLevelRating = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
