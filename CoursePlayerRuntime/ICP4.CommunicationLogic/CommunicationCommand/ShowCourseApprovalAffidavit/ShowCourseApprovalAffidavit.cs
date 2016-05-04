using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseApprovalAffidavit
{
    [XmlRootAttribute("Command")]
    public class ShowCourseApprovalAffidavit : IDisposable 
    {
        private string commandName;
        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }


        private CourseApprovalAffidavit affidavit;

        public CourseApprovalAffidavit Affidavit
        {
            get { return affidavit; }
            set { affidavit = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
