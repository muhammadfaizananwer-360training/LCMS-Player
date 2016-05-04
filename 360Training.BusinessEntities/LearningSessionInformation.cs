using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearningSessionInformation
    {
        private string learningSessionOutputID;

        public string LearningSessionOutputID
        {
            get { return learningSessionOutputID; }
            set { learningSessionOutputID = value; }
        }

        private DateTime oldLearningSessionStartTime;

        public DateTime OldLearningSessionStartTime
        {
            get { return oldLearningSessionStartTime; }
            set { oldLearningSessionStartTime = value; }
        }


        public LearningSessionInformation()
        {


        }


    }
}
