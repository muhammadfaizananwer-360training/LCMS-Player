using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class SuggestedCourse
    {
        private string courseName;

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }
        private string courseGuid;

        public string CourseGuid
        {
            get { return courseGuid; }
            set { courseGuid = value; }
        }

        private string courseImageUrl;

        public string  CourseImageUrl
        {
            get { return  courseImageUrl; }
            set { courseImageUrl = value; }
        }

        private string storeAddress;

        public string StoreAddress
        {
            get { return storeAddress; }
            set { storeAddress = value; }
        }
            
    }
}
