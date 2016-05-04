using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseEndPage
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public CourseEndPage()
        {
            this.url = string.Empty;
        }
    }
}
