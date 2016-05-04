using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions
{
    public class CourseEvaluationRoot
    {
        List<CourseEvaluationQuestions> courseEvaluationQuestions;

        public List<CourseEvaluationQuestions> CourseEvaluationQuestions
        {
            get { return courseEvaluationQuestions; }
            set { courseEvaluationQuestions = value; }
        }
        private int questionsPerPage;

        public int QuestionsPerPage
        {
            get { return questionsPerPage; }
            set { questionsPerPage = value; }
        }
        private string courseEvaluationName;

        public string CourseEvaluationName
        {
            get { return courseEvaluationName; }
            set { courseEvaluationName = value; }
        }
    }
}
