using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseCertificate
{
    public class CourseCertificate
    {              

        private int courseId;

        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }


        private string courseConfigurationCourseCertificate;

        public string CourseConfigurationCourseCertificate
        {
            get { return courseConfigurationCourseCertificate; }
            set { courseConfigurationCourseCertificate = value; }
        }


        private string certificate;

        public string Certificate
        {
            get { return certificate; }
            set { certificate = value; }
        }

       


        private string brandingCourseCertificate;

        public string BrandingCourseCertificate
        {
            get { return brandingCourseCertificate; }
            set { brandingCourseCertificate = value; }
        }

        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }

        private bool downloadButtonEnabled;
        public bool DownloadButtonEnabled
        {
            get { return downloadButtonEnabled; }
            set { downloadButtonEnabled = value; }
        }

        private string certificateURL;
        public string CertificateURL
        {
            get { return certificateURL; }
            set { certificateURL = value; }
        }


    }
}
