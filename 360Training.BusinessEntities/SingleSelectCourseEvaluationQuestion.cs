using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class SingleSelectCourseEvaluationQuestion: CourseEvaluationQuestion 
    {
        public SingleSelectCourseEvaluationQuestion()         
        {
            base.QuestionType = BusinessEntities.CourseEvaluationQuestionType.SingleSelect; 
        }
    }
}
