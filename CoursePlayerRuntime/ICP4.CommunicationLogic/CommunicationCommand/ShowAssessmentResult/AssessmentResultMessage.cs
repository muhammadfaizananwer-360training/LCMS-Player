using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowAssessmentResult
{
    public class AssessmentResultMessage
    {
        private string assessmentResultMessageHeading;
        public string AssessmentResultMessageHeading
        {
            get { return assessmentResultMessageHeading; }
            set { assessmentResultMessageHeading = value; }
        }

        private string assessmentResultMessageText;
        public string AssessmentResultMessageText
        {
            get { return assessmentResultMessageText; }
            set { assessmentResultMessageText = value; }
        }

        private string assessmentResultMessageImageUrl;
        public string AssessmentResultMessageImageUrl
        {
            get { return assessmentResultMessageImageUrl; }
            set { assessmentResultMessageImageUrl = value; }
        }

        private string templateassessmentResult;
        public string TemplateassessmentResult
        {
            get { return templateassessmentResult; }
            set { templateassessmentResult = value; }
        }

        private string templateChartData;
        public string TemplateChartData
        {
            get { return templateChartData; }
            set { templateChartData = value; }
        }  
    }
}
