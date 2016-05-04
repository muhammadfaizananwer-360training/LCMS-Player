using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseApproval
    {
        private int courseapprovalID;
        public int CourseApprovalID
        {
            get { return courseapprovalID; }
            set { courseapprovalID = value; }
        }

        private string holdingRegulator;
        public string HoldingRegulator
        {
            get { return holdingRegulator; }
            set { holdingRegulator = value; }
        }

        private string creditType;
        public string CreditType
        {
            get { return creditType; }
            set { creditType = value; }
        }


        private double approvedCourseHour;
        public double ApprovedCourseHour
        {
            get { return approvedCourseHour; }
            set { approvedCourseHour = value; }
        }

        private string courseapprovalCode;
        public string CourseApprovalCode
        {
            get { return courseapprovalCode; }
            set { courseapprovalCode = value; }
        }

        private string courseName;
        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }

        private string credentialName;
        public string CredentialName
        {
            get { return credentialName; }
            set { credentialName = value; }
        }

    }
}
