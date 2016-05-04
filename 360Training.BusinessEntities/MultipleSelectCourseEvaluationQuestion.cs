using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class MultipleSelectCourseEvaluationQuestion: CourseEvaluationQuestion 
    {

        public MultipleSelectCourseEvaluationQuestion()         
        {
            base.QuestionType = BusinessEntities.CourseEvaluationQuestionType.MultiSelect;    
        }

    }
}
