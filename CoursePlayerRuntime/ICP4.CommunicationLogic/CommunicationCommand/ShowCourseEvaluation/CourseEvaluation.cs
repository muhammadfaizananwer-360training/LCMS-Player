using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation
{
    public class CourseEvaluation
    {
        private string imageURL;

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        private string contentText;

        public string ContentText
        {
            get { return contentText; }
            set { contentText = value; }
        }
        private string heading;

        public string Heading
        {
            get { return heading; }
            set { heading = value; }
        }
        private string courseEvaluationStartButton;

        public string CourseEvaluationStartButton
        {
            get { return courseEvaluationStartButton; }
            set { courseEvaluationStartButton = value; }
        }
        private string courseEvaluationSkipButton;

        public string CourseEvaluationSkipButton
        {
            get { return courseEvaluationSkipButton; }
            set { courseEvaluationSkipButton = value; }
        }

        private bool isSkippable;

        public bool IsSkippable
        {
            get { return isSkippable; }
            set { isSkippable = value; }
        }
    }
}
