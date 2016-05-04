using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseEvaluationResult
    {
        private List<CourseEvaluationResultAnswer> courseEvaluationResultAnswers;

        public List<CourseEvaluationResultAnswer> CourseEvaluationResultAnswers
        {
            get { return courseEvaluationResultAnswers; }
            set { courseEvaluationResultAnswers = value; }
        }
        private int courseID;

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }

        private int surveyID;

        public int SurveyID
        {
            get { return surveyID; }
            set { surveyID = value; }
        }

        private int learnerID;

        public int LearnerID
        {
            get { return learnerID; }
            set { learnerID = value; }
        }
        public void CourseEvaluationResultAnswer()
        {
            courseEvaluationResultAnswers = new List<CourseEvaluationResultAnswer>();
        }

        private int learningSessionID;

        public int LearningSessionID
        {
            get { return learningSessionID; }
            set { learningSessionID = value; }
        }

        //LCMS-4567
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

    }
}
