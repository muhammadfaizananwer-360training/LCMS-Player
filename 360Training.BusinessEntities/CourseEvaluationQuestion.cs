using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public abstract class CourseEvaluationQuestion 
    {
        private int questionId;
        public int QuestionID
        {
            get { return questionId; }
            set { questionId = value; }
        }

        private string questiontext;
        public string QuestionText
        {
            get { return questiontext; }
            set { questiontext = value; }
        }

        private string questiontype;
        public string QuestionType
        {
            get { return questiontype; }
            set { questiontype = value; }
        }

        private string alignment;
        public string Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        private int dropdowntf;
        public int DropDownTF
        {
            get { return dropdowntf; }
            set { dropdowntf = value; }
        }

        private int unlimitedtf;
        public int UnLimitedTF
        {
            get { return unlimitedtf; }
            set { unlimitedtf = value; }
        }

        private int displayorder;
        public int DisplayOrder
        {
            get { return displayorder; }
            set { displayorder = value; }
        }

        private bool required;
        public bool Required
        {
            get { return required; }
            set { required = value; }
        }

        private List<CourseEvaluationAnswer> courseevaluationanswers;
        public List<CourseEvaluationAnswer> CourseEvaluationAnswers
        {
            get { return courseevaluationanswers; }
            set { courseevaluationanswers = value; }
        }



        public CourseEvaluationQuestion() 
        {
            this.questionId = 0;
            this.questiontext = "";
            this.questiontype = "";            
            this.alignment = "";
            this.dropdowntf = 0;
            this.unlimitedtf = 0;
            this.displayorder = 0;
            this.required = false;
            this.courseevaluationanswers =new List<CourseEvaluationAnswer>();
        }        

    }
}
