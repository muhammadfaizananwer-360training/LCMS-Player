using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseIntroPage
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public CourseIntroPage()
        {
            this.url = string.Empty;
        }

    }
}
