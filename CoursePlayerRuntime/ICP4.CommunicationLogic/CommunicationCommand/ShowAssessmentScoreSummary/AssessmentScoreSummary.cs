using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowAssessmentScoreSummary
{
    public class AssessmentScoreSummary : IDisposable
    {


        private string assessmentType;
        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }

        private string headingAssesmentScoreSummaryText;
        public string HeadingAssesmentScoreSummaryText
        {
            get { return headingAssesmentScoreSummaryText; }
            set { headingAssesmentScoreSummaryText = value; }
        }

        private string contentAssesmentScoreSummaryText;
        public string ContentAssesmentScoreSummaryText
        {
            get { return contentAssesmentScoreSummaryText; }
            set { contentAssesmentScoreSummaryText = value; }
        }

        private string percentageScoreText;
        public string PercentageScoreText
        {
            get { return percentageScoreText; }
            set { percentageScoreText = value; }
        }
        
        private string passFailScoreText;
        public string PassFailScoreText
        {
            get { return passFailScoreText; }
            set { passFailScoreText = value; }
        }
       
        private string imagePassFailScoreURL;
        public string ImagePassFailScoreURL
        {
            get { return imagePassFailScoreURL; }
            set { imagePassFailScoreURL = value; }
        }
        
        private string imageAssessmentSummaryScoreUrl;
        public string ImageAssessmentSummaryScoreUrl
        {
            get { return imageAssessmentSummaryScoreUrl; }
            set { imageAssessmentSummaryScoreUrl = value; }
        }



        private string individualScoreHeaderText;
        public string IndividualScoreHeaderText
        {
            get { return individualScoreHeaderText; }
            set { individualScoreHeaderText = value; }
        }

        private string individualScoreHeading;
        public string IndividualScoreHeading
        {
            get { return individualScoreHeading; }
            set { individualScoreHeading = value; }
        }
        /*following list will be used when assessment is not Exam*/
        private ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore showIndividualQuestionScore;
        public ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore ShowIndividualQuestionScore
        {
            get { return showIndividualQuestionScore; }
            set { showIndividualQuestionScore = value; }
        }

        private bool exam;
        public bool IsExam
        {
            get { return exam; }
            set { exam = value; }
        }

        private bool showGraph;
        public bool IsShowGraph
        {
            get { return showGraph; }
            set { showGraph = value; }
        }

        private string assessmentName;
        public string AssessmentName
        {
            get { return assessmentName; }
            set { assessmentName = value; }
        }

        private string headingTopicScoreChart;
        public string HeadingTopicScoreChart
        {
            get { return headingTopicScoreChart; }
            set { headingTopicScoreChart = value; }
        }

        private string contentTopicScoreChart;
        public string ContentTopicScoreChart
        {
            get { return contentTopicScoreChart; }
            set { contentTopicScoreChart = value; }
        }

        /*following list will be used when assessment is Exam*/
        private List<ICP4.CommunicationLogic.CommunicationCommand.ShowTopicScoreSummary.ShowTopicScoreSummary> showTopicScoreSummaries;
        public List<ICP4.CommunicationLogic.CommunicationCommand.ShowTopicScoreSummary.ShowTopicScoreSummary> ShowTopicScoreSummaries
        {
            get { return showTopicScoreSummaries; }
            set { showTopicScoreSummaries = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
