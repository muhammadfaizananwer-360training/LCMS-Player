using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{    
    public class CourseEvaluationAnswer
    {
        private int Id;
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        private int questionId;
        public int QuestionID
        {
            get { return questionId; }
            set { questionId = value; }
        }

        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private int displayorder;
        public int DisplayOrder
        {
            get { return displayorder; }
            set { displayorder = value; }
        }

        private int courseevaluationresultanswerId;
        public int CourseEvaluationResultAnswerID
        {
            get { return courseevaluationresultanswerId; }
            set { courseevaluationresultanswerId = value; }
        }

        public CourseEvaluationAnswer() 
        {
            this.Id = 0;
            this.questionId = 0;
            this.label = "";
            this.Value = "";
            this.displayorder = 0;
            this.courseevaluationresultanswerId = 0;

        }
        
    }
}
