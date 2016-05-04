using System;
using System.Collections.Generic;
using System.Text;
 
namespace ICP4.CommunicationLogic.CommunicationCommand.EOCInstructions
{
    public class EOCInstructions
    {

        private int LMS_vu;

        public int LMS_VU
        {
            get { return LMS_vu; }
            set { LMS_vu = value; }
        }

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

        private string headingEOCInstructions;

        public string HeadingEOCInstructions
        {
            get { return headingEOCInstructions; }
            set { headingEOCInstructions = value; }
        }


    }
}
