using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class TOCItem
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int parentID;
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private bool alowQuizTF;
        public bool AlowQuizTF
        {
            get { return alowQuizTF; }
            set { alowQuizTF = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string guid;
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private int maxQuizQuestionsToAsk;

        public int MaxQuizQuestionsToAsk
        {
            get { return maxQuizQuestionsToAsk; }
            set { maxQuizQuestionsToAsk = value; }
        }

        private bool overrideMaxQuizQuestionsToAsk;

        public bool OverrideMaxQuizQuestionsToAsk
        {
            get { return overrideMaxQuizQuestionsToAsk; }
            set { overrideMaxQuizQuestionsToAsk = value; }
        }

        private List<TOCItem> tocitems;

        public List<TOCItem> TOCItems
        {
            get { return tocitems; }
            set { tocitems = value; }
        }

        public bool IsExam()
        {
            return Type == TOCItemType.Exam;
        }
        public TOCItem() 
        {
            this.alowQuizTF = false;
            this.ID = 0;
            this.TOCItems = new List<TOCItem>();  
            this.name = string.Empty;
            this.parentID = 0;
            this.GUID = string.Empty;            
        } 
    }
}
