using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class CourseEvaluationResultAnswer
    {
        private int courseEvaluationAnswerID;

        public int CourseEvaluationAnswerID
        {
            get { return courseEvaluationAnswerID; }
            set { courseEvaluationAnswerID = value; }
        }

        private int courseEvaluationQuestionID;

        public int CourseEvaluationQuestionID
        {
            get { return courseEvaluationQuestionID; }
            set { courseEvaluationQuestionID = value; }
        }

        private string courseEvaluationResultAnswerType;

        public string CourseEvaluationAnswerType
        {
            get { return courseEvaluationResultAnswerType; }
            set { courseEvaluationResultAnswerType = value; }
        }

        private string courseEvaluationResultAnswerText;

        public string CourseEvaluationResultAnswerText
        {
            get { return courseEvaluationResultAnswerText; }
            set { courseEvaluationResultAnswerText = value; }
        }
        public  CourseEvaluationResultAnswer()
        {
            this.courseEvaluationAnswerID = 0;
            this.courseEvaluationQuestionID = 0;
            this.courseEvaluationResultAnswerText = string.Empty;
            this.courseEvaluationResultAnswerType = string.Empty;
        }
    }
}
