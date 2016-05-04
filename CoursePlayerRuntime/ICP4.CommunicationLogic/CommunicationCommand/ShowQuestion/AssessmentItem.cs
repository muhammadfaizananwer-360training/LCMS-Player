using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
{
    public class AssessmentItem
    {

        private string assessmentItemGuid;
        public string AssessmentItemGuid
        {
            get { return assessmentItemGuid; }
            set { assessmentItemGuid = value; }
        }

        private int assessmentItemID;

        public int AssessmentItemID
        {
            get { return assessmentItemID; }
            set { assessmentItemID = value; }
        }

        private string questionStem;

        public string QuestionStem
        {
            get { return questionStem; }
            set { questionStem = value; }
        }

        private string questionType;

        public string QuestionType
        {
            get { return questionType; }
            set { questionType = value; }
        }


        private int questionNo;
        public int QuestionNo
        {
            get { return questionNo; }
            set { questionNo = value; }
        }

        private int totalQuestion;
        public int TotalQuestion
        {
            get { return totalQuestion; }
            set { totalQuestion = value; }
        }

        private bool remidiationMode;
        public bool RemidiationMode
        {
            get { return remidiationMode; }
            set { remidiationMode = value; }
        }

        private StudentAnswer studentAnswer;
        public StudentAnswer StudentAnswer
        {
            get { return studentAnswer; }
            set { studentAnswer = value; }
        }

        private bool contentRemidiationAvailable;
        public bool ContentRemidiationAvailable
        {
            get { return contentRemidiationAvailable; }
            set { contentRemidiationAvailable = value; }
        }

        private List<AssessmentItemAnswer> assessmentAnswers;
        public List<AssessmentItemAnswer> AssessmentAnswers
        {
            get { return assessmentAnswers; }
            set { assessmentAnswers = value; }
        }
        private string templateHTML;

        public string TemplateHTML
        {
            get { return templateHTML; }
            set { templateHTML = value; }
        }
        private string audioURL;

        public string AudioURL
        {
            get { return audioURL; }
            set { audioURL = value; }
        }
        private string templateType;

        public string TemplateType
        {
            get { return templateType; }
            set { templateType = value; }
        }
        private string visualTopType;

        public string VisualTopType
        {
            get { return visualTopType; }
            set { visualTopType = value; }
        }

        private bool policyEachQuestionAnswered;

        public bool PolicyEachQuestionAnswered
        {
            get { return policyEachQuestionAnswered; }
            set { policyEachQuestionAnswered = value; }
        }


        public AssessmentItem()
        {
            this.assessmentAnswers = new List<AssessmentItemAnswer>();
            this.assessmentItemID = 0;
            this.questionStem = string.Empty;
            this.questionType = ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion. QuestionType.TrueFalse;
            this.assessmentItemGuid = string.Empty;
            this.questionNo = 0;
            this.totalQuestion = 0;
            this.audioURL = string.Empty;
            this.templateHTML = string.Empty;
            this.templateType = string.Empty;
            this.visualTopType = string.Empty;
            this.policyEachQuestionAnswered = false;
        }
    }
}
