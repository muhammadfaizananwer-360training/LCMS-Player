using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerStatisticsAnswer
    {
        private int learnerStatisticsAnswerID;
        public int LearnerStatisticsAnswerID
        {
            get { return learnerStatisticsAnswerID; }
            set { learnerStatisticsAnswerID = value; }
        }
        private string assessmentItemAnswerGUID;

        public string AssessmentItemAnswerGUID
        {
            get { return assessmentItemAnswerGUID; }
            set { assessmentItemAnswerGUID = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private int learnerStatisticsID;

        public int LearnerStatisticsID
        {
            get { return learnerStatisticsID; }
            set { learnerStatisticsID = value; }
        }
        public LearnerStatisticsAnswer()
        {
            this.assessmentItemAnswerGUID = string.Empty;
            this.learnerStatisticsAnswerID = 0;
            this.value = string.Empty;
            this.learnerStatisticsID = 0;
        }

    }
}
