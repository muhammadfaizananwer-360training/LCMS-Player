using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    
    public class AssessmentItemTemplate
    {
        private int contentOwnerID;

        public int ContentOwnerID
        {
            get { return contentOwnerID; }
            set { contentOwnerID = value; }
        }
        private int assessmentItemTemplateID;

        public int AssessmentItemTemplateID
        {
            get { return assessmentItemTemplateID; }
            set { assessmentItemTemplateID = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string templateHTML;

        public string TemplateHTML
        {
            get { return templateHTML; }
            set { templateHTML = value; }
        }

        private bool isAudioAssesTF;    

        public bool IsAudioAssesTF
        {
            get { return isAudioAssesTF; }
            set { isAudioAssesTF = value; }
        }
        private bool isVisualAssetTF;

        public bool IsVisualAssetTF
        {
            get { return isVisualAssetTF; }
            set { isVisualAssetTF = value; }
        }

        public AssessmentItemTemplate()
        {
            this.assessmentItemTemplateID = 0;
            this.isAudioAssesTF = false;
            this.isVisualAssetTF = false;
            this.name = string.Empty;
            this.templateHTML = string.Empty;
            this.contentOwnerID = 0;
        }
        
    }
       
}
