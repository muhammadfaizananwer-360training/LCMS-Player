using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions
{
    public class CourseEvaluationAnswer
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string displayorder;

        public string Displayorder
        {
            get { return displayorder; }
            set { displayorder = value; }
        }
    }
}
