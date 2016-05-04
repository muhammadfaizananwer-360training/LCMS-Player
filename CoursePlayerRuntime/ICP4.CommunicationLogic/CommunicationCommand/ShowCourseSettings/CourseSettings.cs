using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseSettings
{
    public class CourseSettings :IDisposable
    {
        private bool showInstructorInfo;
        public bool ShowInstructorInfo
        {
            get { return showInstructorInfo; }
            set { showInstructorInfo = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
