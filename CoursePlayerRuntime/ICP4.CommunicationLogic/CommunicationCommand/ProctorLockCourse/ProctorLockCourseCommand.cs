using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ProctorLock
{
    [XmlRootAttribute("Command")]
    public class ProctorLockCourseCommand : IDisposable
    {
        private string commandName;

        [XmlAttribute]
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        private ProctorLockCourseCommandMessage proctorLockCourseCommandMessage;
        [XmlElement("CommandData")]
        public ProctorLockCourseCommandMessage ProctorLockCourseCommandMessage
        {
            get { return proctorLockCourseCommandMessage; }
            set { proctorLockCourseCommandMessage = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
