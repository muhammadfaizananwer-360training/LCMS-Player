using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowEOCTextCommand
{
    [XmlRootAttribute("Command")]
    public class ShowCourseEndTextOnRelaunch : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseEndTextOnRelaunch courseEndTextOnRelaunch;
        [XmlElement("CommandData")]
        public CourseEndTextOnRelaunch CourseEndTextOnRelaunch
        {
            get { return courseEndTextOnRelaunch; }
            set { courseEndTextOnRelaunch = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
