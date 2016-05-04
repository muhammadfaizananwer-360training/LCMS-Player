using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowEOCTextCommand
{
    public class CourseEndTextOnRelaunch : IDisposable
    {

        private string courseEndHeading;
        public string CourseEndHeading
        {
            get { return courseEndHeading; }
            set { courseEndHeading = value; }
        }

        private string courseEndText;
        public string CourseEndText
        {
            get { return courseEndText; }
            set { courseEndText = value; }
        }

        

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
