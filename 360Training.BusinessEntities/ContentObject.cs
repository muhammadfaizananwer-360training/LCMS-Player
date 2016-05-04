using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class ContentObject
    {
        private int contentObjectID;
        public int ContentObjectID
        {
            get { return contentObjectID; }
            set { contentObjectID = value;}
        }
        private int parentContentObjectID;
        public int ParentContentObjectID
        {
            get { return parentContentObjectID; }
            set { parentContentObjectID = value; }
        }
        private bool alowQuizTF;
        public bool AlowQuizTF
        {
            get { return alowQuizTF; }
            set { alowQuizTF = value; }
        }
        private List<ContentObject> contentObjects;

        public List<ContentObject> ContentObjects
        {
            get { return contentObjects; }
            set { contentObjects = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string contentObject_GUID;
        public string ContentObject_GUID
        {
            get { return contentObject_GUID; }
            set { contentObject_GUID=value;}
        }
        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
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

        public ContentObject()
        {
            this.alowQuizTF = false;
            this.contentObjectID = 0;
            this.contentObjects=new List<ContentObject>();
            this.name=string.Empty;
            this.parentContentObjectID=0;
            this.contentObject_GUID = string.Empty;
            this.displayOrder = 0;
        }
    }
}
