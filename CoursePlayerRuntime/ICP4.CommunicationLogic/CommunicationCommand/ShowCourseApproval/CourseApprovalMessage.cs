using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseApproval
{
    public class CourseApprovalMessage
    {
        private string courseApprovalMessageHeading;
        public string CourseApprovalMessageHeading
        {
            get { return courseApprovalMessageHeading; }
            set { courseApprovalMessageHeading = value; }
        }

        private string courseApprovalMessageText;
        public string CourseApprovalMessageText
        {
            get { return courseApprovalMessageText; }
            set { courseApprovalMessageText = value; }
        }

        private string courseApprovalMessageImageUrl;
        public string CourseApprovalMessageImageUrl
        {
            get { return courseApprovalMessageImageUrl; }
            set { courseApprovalMessageImageUrl = value; }
        }

        private string contentcourseApprovalbutton;
        public string ContentCourseApprovalButton
        {
            get { return contentcourseApprovalbutton; }
            set { contentcourseApprovalbutton = value; }
        }

        private string templatecourseApproval;
        public string TemplatecourseApproval
        {
            get { return templatecourseApproval; }
            set { templatecourseApproval = value; }
        }

        private string courseName;
        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }
    }
}
