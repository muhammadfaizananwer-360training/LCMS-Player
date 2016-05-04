using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LegacyCourseTestingResult
    {
        public LegacyCourseTestingResult()
        {
        }
        private string statisticsType;

        public string StatisticsType
        {
            get { return statisticsType; }
            set { statisticsType = value; }
        }

        private int testingId;

        public int TestingId
        {
            get { return testingId; }
            set { testingId = value; }
        }
        private int rawScore;

        public int RawScore
        {
            get { return rawScore; }
            set { rawScore = value; }
        }
        //private int isPassed;

        //public int IsPassed
        //{
        //    get { return isPassed; }
        //    set { isPassed = value; }
        //}
        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
    }
}
