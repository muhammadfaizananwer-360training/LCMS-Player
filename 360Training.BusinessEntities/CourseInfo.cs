using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseInfo
    {
        private string courseName;

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }
        private string language;

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        private int courseId;

        public int CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }

        private int courseconfigId;

        public int CourseConfigId
        {
            get { return courseconfigId; }
            set { courseconfigId = value; }
        }


        private int courseapprovalId;

        public int CourseApprovalId
        {
            get { return courseapprovalId; }
            set { courseapprovalId = value; }
        }


        private int learnerId;

        public int LearnerId
        {
            get { return learnerId; }
            set { learnerId = value; }
        }

        private string learnerSessionGuid;

        public string  LearnerSessionGuid
        {
            get { return learnerSessionGuid; }
            set { learnerSessionGuid = value; }
        }
       
    }
}
