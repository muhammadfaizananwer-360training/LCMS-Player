using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseApproval
{
    [XmlRootAttribute("Command")]
    public class ShowCourseApproval : IDisposable
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private CourseApprovalMessage courseApprovalMessage;
        [XmlElement("CommandData")]
        public CourseApprovalMessage CourseApprovalMessage
        {
            get { return courseApprovalMessage; }
            set { courseApprovalMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
