using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestionResult
{
 
    public class QuestionResult : IDisposable
    {

        private string headingQuestionFeedback;

        public string HeadingQuestionFeedback
        {
            get { return headingQuestionFeedback; }
            set { headingQuestionFeedback = value; }
        }
        private string imageQuestionFeedbackUrl;

        public string ImageQuestionFeedbackUrl
        {
            get { return imageQuestionFeedbackUrl; }
            set { imageQuestionFeedbackUrl = value; }
        }
        private string imageCorrectIncorectUrl;

        public string ImageCorrectIncorectUrl
        {
            get { return imageCorrectIncorectUrl; }
            set { imageCorrectIncorectUrl = value; }
        }
        private string contentCorrectIncorrect;

        public string ContentCorrectIncorrect
        {
            get { return contentCorrectIncorrect; }
            set { contentCorrectIncorrect = value; }
        }
        
        private bool isCorrectlyAnswered;
        public bool IsCorrectlyAnswered
        {
            get { return isCorrectlyAnswered; }
            set { isCorrectlyAnswered = value; }
        }
        private string questionFeedBack;
        public string QuestionFeedBack
        {
            get { return questionFeedBack; }
            set { questionFeedBack = value; }
        }

        private bool endAssessment;

        public bool EndAssessment
        {
            get { return endAssessment; }
            set { endAssessment = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
