using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{ 
    public class CourseEvaluation 
    {

        private int Id;
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private int islockedtf;
        public int IsLockedTF
        {
            get { return islockedtf; }
            set { islockedtf = value; }
        }

        private string courseevaluationvent;
        public string Event
        {
            get { return courseevaluationvent; }
            set { courseevaluationvent = value; }
        }

        private int showalltf;
        public int ShowAllTF
        {
            get { return showalltf; }
            set { showalltf = value; }
        }

        private int questionsperpage;
        public int QuestionsPerPage
        {
            get { return questionsperpage; }
            set { questionsperpage = value; }
        }

        private int contentownerId;
        public int ContentOwnerID
        {
            get { return contentownerId; }
            set { contentownerId = value; }
        }

        private List<CourseEvaluationQuestion> courseevaluationquestions;
        public List<CourseEvaluationQuestion> CourseEvaluationQuestions
        {
            get { return courseevaluationquestions; }
            set { courseevaluationquestions = value; }
        }

        public CourseEvaluation() 
        {
            this.Id = 0;
            this.name = "";
            this.questionsperpage = 0;            
            this.contentownerId = 0;
            this.courseevaluationquestions = new List<CourseEvaluationQuestion>();
        }        
    }
}
