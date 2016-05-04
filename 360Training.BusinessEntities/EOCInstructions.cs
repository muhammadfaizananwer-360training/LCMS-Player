using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class EOCInstructions
    {

        private int courseId;

        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        
        private string courseConfigurationEOCInstructions;

        public string CourseConfigurationEOCInstructions
        {
            get { return courseConfigurationEOCInstructions; }
            set { courseConfigurationEOCInstructions = value; }
        }


        private string courseEOCInstructions;

        public string CourseEOCInstructions
        {
            get { return courseEOCInstructions; }
            set { courseEOCInstructions = value; }
        }


        private string brandingEOCInstructions;

        public string BrandingEOCInstructions
        {
            get { return brandingEOCInstructions; }
            set { brandingEOCInstructions = value; }
        }







    }
}
