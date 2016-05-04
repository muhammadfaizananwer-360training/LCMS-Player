using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions
{
    public class CourseEvaluationQuestions
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string quetiontype;

        public string Quetiontype
        {
            get { return quetiontype; }
            set { quetiontype = value; }
        }

        private string alignment;

        public string Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        private bool unlimitedTF;

        public bool UnlimitedTF
        {
            get { return unlimitedTF; }
            set { unlimitedTF = value; }
        }

        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }

        private bool required;

        public bool Required
        {
            get { return required; }
            set { required = value; }
        }

        private List<CourseEvaluationAnswer> courseEvaluationAnswer;

        public List<CourseEvaluationAnswer> CourseEvaluationAnswer
        {
            get { return courseEvaluationAnswer; }
            set { courseEvaluationAnswer = value; }
        }
        private int questionNo;

        public int QuestionNo
        {
            get { return questionNo; }
            set { questionNo = value; }
        }
    }
}
