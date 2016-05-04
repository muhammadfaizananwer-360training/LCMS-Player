using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.CourseCompletionCommand
{
     
    [XmlRootAttribute("Command")]
    public class ShowCourseCompletion : IDisposable
    {
        private string commandName;

        [XmlAttribute] 
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseCompletion courseCompletion;
        [XmlElement("CommandData")]
        public CourseCompletion CourseCompletion
        {
            get { return courseCompletion; }
            set { courseCompletion = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
