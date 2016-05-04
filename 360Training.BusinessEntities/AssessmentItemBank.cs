using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class AssessmentItemBank  
    {
        private String assessmentBankGuid;

        public String AssessmentBankGuid
        {
            get { return assessmentBankGuid; }
            set { assessmentBankGuid = value; }
        }


        private int assessmentBankID;

        public int AssessmentBankID
        {
            get { return assessmentBankID; }
            set { assessmentBankID = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string keywords;

        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }
        private int parentAssesmentItemBankID;

        public int ParentAssesmentItemBankID
        {
            get { return parentAssesmentItemBankID; }
            set { parentAssesmentItemBankID = value; }
        }

        private List<AssessmentItemBank> assessmentBanks;

        public List<AssessmentItemBank> AssessmentBanks
        {
            get { return assessmentBanks; }
            set { assessmentBanks = value; }
        }

        private List<AssessmentItem> assessmentItems;

        public List<AssessmentItem> AssessmentItems
        {
            get { return assessmentItems; }
            set { assessmentItems = value; }
        }

        private long learningObjectiveAssessmentBankID;

        public long LearningObjectiveAssessmentBankID
        {
            get { return learningObjectiveAssessmentBankID; }
            set { learningObjectiveAssessmentBankID = value; }
        }

        private double assessmentItemBankWeightage;
        public double AssessmentItemBankWeightage
        {
            get { return assessmentItemBankWeightage; }
            set { assessmentItemBankWeightage = value; }
        }

        public AssessmentItemBank()
        {
            this.assessmentBankID = 0;
            this.assessmentBanks = new List<AssessmentItemBank>();
            this.assessmentItems = new List<AssessmentItem>();
            this.description = string.Empty;
            this.keywords = string.Empty;
            this.name = string.Empty;
            this.parentAssesmentItemBankID = 0;
            this.assessmentBankGuid = string.Empty;
            this.learningObjectiveAssessmentBankID = 0;
            this.assessmentItemBankWeightage = 0;

        }

     

    }
}
