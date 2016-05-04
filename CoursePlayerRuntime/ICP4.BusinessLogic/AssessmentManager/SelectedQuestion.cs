using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.BusinessLogic.AssessmentManager
{
    public class SelectedQuestion
    {
        int assessmentParentId;

        public int AssessmentParentId
        {
            get { return assessmentParentId; }
            set { assessmentParentId = value; }
        }
        string assessmentType;

        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }

        private List<QuestionInfo> questionInfos;

        public List<QuestionInfo> QuestionInfos
        {
            get { return questionInfos; }
            set { questionInfos = value; }
        }

        public SelectedQuestion()
        {
            assessmentParentId = 0;
            assessmentType = "";
            questionInfos = new List<QuestionInfo>();
            
        }

    }
}
