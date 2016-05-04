using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseInfo
{
    [XmlRootAttribute("Command")]
    public class ShowCourseInfo : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseInfo courseInfo;
        [XmlElement("CommandData")]
        public CourseInfo CourseInfo
        {
            get { return courseInfo; }
            set { courseInfo = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
