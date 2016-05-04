using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerValidationStatistics
    {
        private int learnerValidationStatisticsId;

        public int LearnerValidationStatisticsId
        {
            get { return learnerValidationStatisticsId; }
            set { learnerValidationStatisticsId = value; }
        }

        private string answerText;

        public string AnswerText
        {
            get { return answerText; }
            set { answerText = value; }
        }

        private bool isCorrect;

        public bool IsCorrect
        {
            get { return isCorrect; }
            set { isCorrect = value; }
        }

        private DateTime saveTime;

        public DateTime SaveTime
        {
            get { return saveTime; }
            set { saveTime = value; }
        }
        private int enrollmentID;

        public int EnrollmentID
        {
            get { return enrollmentID; }
            set { enrollmentID = value; }
        }
        private int questionID;

        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }
        public LearnerValidationStatistics()
        {
            this.saveTime = System.DateTime.Now;
        }
        private bool isAnswered;

        public bool IsAnswered
        {
            get { return isAnswered; }
            set { isAnswered = value; }
        }
    }
}
