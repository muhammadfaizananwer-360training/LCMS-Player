using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class FillInTheBlankCourseEvaluationQuestion : CourseEvaluationQuestion
    {
        public FillInTheBlankCourseEvaluationQuestion()         
        {
            base.QuestionType = BusinessEntities.CourseEvaluationQuestionType.FillInTheBlank;   
        }
    }
}
