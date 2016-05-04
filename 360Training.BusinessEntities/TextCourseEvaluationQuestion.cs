using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class TextCourseEvaluationQuestion : CourseEvaluationQuestion 
    {
        public TextCourseEvaluationQuestion()         
        {
            base.QuestionType = BusinessEntities.CourseEvaluationQuestionType.Text;   
        }
    }
}
