using System;
using System.Collections.Generic;

using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class OrderingAssessmentItemAnswer : AssessmentItemAnswer
    {
        private int correctOrder;

        public int CorrectOrder
        {
            get { return correctOrder; }
            set { correctOrder = value; }
        }

        public OrderingAssessmentItemAnswer()
        {
            this.correctOrder = 0;
        }
    }
}
