using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions
{
    [XmlRootAttribute("Command")]
    public class ShowCourseEvaluationQuestions : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseEvaluationRoot courseEvaluation;
        [XmlElement("CommandData")]
        public CourseEvaluationRoot CourseEvaluation
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
