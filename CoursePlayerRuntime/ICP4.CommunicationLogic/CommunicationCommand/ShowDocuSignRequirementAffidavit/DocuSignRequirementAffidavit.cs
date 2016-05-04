using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowDocuSignRequirementAffidavit
{
    public class DocuSignRequirementAffidavit
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
        public DocuSignRequirementAffidavit()
        {
            isDocuSignAffidavit = false;
        }
    }
}
