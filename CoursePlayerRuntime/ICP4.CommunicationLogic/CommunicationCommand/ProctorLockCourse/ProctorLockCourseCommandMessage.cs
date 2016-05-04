using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ProctorLock
{
    public class ProctorLockCourseCommandMessage
    {
        private bool lockCourse;

        public bool LockCourse
        {
            get { return lockCourse; }
            set { lockCourse = value; }
        }

        private string lockCourseMessage;

        public string LockCourseMessage
        {
            get { return lockCourseMessage; }
            set { lockCourseMessage = value; }
        }
    }
}
