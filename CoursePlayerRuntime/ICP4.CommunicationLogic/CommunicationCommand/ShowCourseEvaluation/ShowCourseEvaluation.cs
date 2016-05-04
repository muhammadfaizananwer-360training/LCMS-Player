using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation
{
    [XmlRootAttribute("Command")]
    public class ShowCourseEvaluation:IDisposable
    {
        
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseEvaluation courseEvaluation;
        [XmlElement("CommandData")]
        public CourseEvaluation CourseEvaluation
        {
            get { return courseEvaluation; }
            set { courseEvaluation = value; }
        }
        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
