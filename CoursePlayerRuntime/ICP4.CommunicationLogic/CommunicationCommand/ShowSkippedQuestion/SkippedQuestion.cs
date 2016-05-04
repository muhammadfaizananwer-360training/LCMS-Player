﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSkippedQuestion
{
    public class SkippedQuestion : IDisposable
    {
        private int assessmentItemID;
        public int AssessmentItemID
        {
            get { return assessmentItemID; }
            set { assessmentItemID = value; }
        }


        private string assessmentItemStem;
        public string AssessmentItemStem
        {
            get { return assessmentItemStem; }
            set { assessmentItemStem = value; }
        }

        private string assessmentItemType;
        public string AssessmentItemType
        {
            get { return assessmentItemType; }
            set { assessmentItemType = value; }
        }


        private int questionNo;
        public int QuestionNo
        {
            get { return questionNo; }
            set { questionNo = value; }
        }

        //Abdus Samad LCMS-12105
        //START
        private bool assessmentToogleFlag;
        public bool AssessmentToogleFlag
        {
            get { return assessmentToogleFlag; }
            set { assessmentToogleFlag = value; }
        }
        //STOP
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
