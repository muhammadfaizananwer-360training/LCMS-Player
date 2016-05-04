using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseLocked
{
    public class CourseLockedMessage
    {
        private string courseLockedMessageHeading;
        public string CourseLockedMessageHeading
        {
            get { return courseLockedMessageHeading; }
            set { courseLockedMessageHeading = value; }
        }

        private string courseLockedMessageText;
        public string CourseLockedMessageText
        {
            get { return courseLockedMessageText; }
            set { courseLockedMessageText = value; }
        }

        private string courseLockedMessageImageUrl;
        public string CourseLockedMessageImageUrl
        {
            get { return courseLockedMessageImageUrl; }
            set { courseLockedMessageImageUrl = value; }
        }

        private string contentunlockcoursebutton;
        public string ContentUnlockCourseButton
        {
            get { return contentunlockcoursebutton; }
            set { contentunlockcoursebutton = value; }
        }

        private string lockType;

        public string LockType
        {
            get { return lockType; }
            set { lockType = value; }
        }
        private string redirectURL;

        public string RedirectURL
        {
            get { return redirectURL; }
            set { redirectURL = value; }
        }
    }
}
