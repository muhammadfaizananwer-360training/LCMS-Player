using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowDocuSignProcess
{
    public class DocuSignProcess
    {
        private int courseId;
        public int CourseID
        {
            get { return courseId; }
            set { courseId = value; }
        }

        private int courseapprovalId;
        public int CourseApprovalID
        {
            get { return courseapprovalId; }
            set { courseapprovalId = value; }
        }

        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }

        private string affidavitURL;
        public string AffidavitURL
        {
            get { return affidavitURL; }
            set { affidavitURL = value; }
        }
        //LCMS-11217
        private bool isDocuSignAffidavit;

        public bool IsDocuSignAffidavit
        {
            get { return isDocuSignAffidavit; }
            set { isDocuSignAffidavit = value; }
        }

        private string sceneText;
        public string SceneText
        {
            get { return sceneText; }
            set { sceneText = value; }
        }


        private string url;
        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        //LCMS-11217
        private bool isDocuSignDualSigner;

        public bool IsDocuSignDualSigner
        {
            get { return isDocuSignDualSigner; }
            set { isDocuSignDualSigner = value; }
        }


        public DocuSignProcess()
        {
            isDocuSignAffidavit = false;
        }
    }
}
