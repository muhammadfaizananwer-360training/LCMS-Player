using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseLocked
{
    [XmlRootAttribute("Command")]
    public class ShowCourseLocked : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseLockedMessage courseLockedMessage;
        [XmlElement("CommandData")]
        public CourseLockedMessage CourseLockedMessage
        {
            get { return courseLockedMessage; }
            set { courseLockedMessage = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
