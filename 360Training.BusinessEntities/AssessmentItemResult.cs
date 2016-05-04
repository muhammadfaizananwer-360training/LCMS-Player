using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class AssessmentItemResult
    {
        private int assessmentItemResultlID;
        public int AssessmentItemResultlID
        {
            get { return assessmentItemResultlID; }
            set { assessmentItemResultlID = value; }
        }

        private string majorCategory;
        public string MajorCategory
        {
            get { return majorCategory; }
            set { majorCategory = value; }
        }

        private int totalAssessment;
        public int TotalAssessment
        {
            get { return totalAssessment; }
            set { totalAssessment = value; }
        }

        private int totalanswerCorrect;
        public int TotalAnswerCorrect
        {
            get { return totalanswerCorrect; }
            set { totalanswerCorrect = value; }
        }

        private int totalanswerInCorrect;
        public int TotalAnswerInCorrect
        {
            get { return totalanswerInCorrect; }
            set { totalanswerInCorrect = value; }
        }

        private double answerCorrectPercentage;
        public double AnswerCorrectPercentage
        {
            get { return answerCorrectPercentage; }
            set { answerCorrectPercentage = value; }
        }

        private double answerInCorrectPercentage;
        public double AnswerInCorrectPercentage
        {
            get { return answerInCorrectPercentage; }
            set { answerInCorrectPercentage = value; }
        }
    }
}
