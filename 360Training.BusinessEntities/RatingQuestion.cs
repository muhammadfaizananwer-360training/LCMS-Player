using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class RatingQuestion:AssessmentItem
    {

        private string lowValueLabel;

        public string LowValueLabel
        {
            get { return lowValueLabel; }
            set { lowValueLabel = value; }
        }

        private string highValueLabel;

        public string HighValueLabel
        {
            get { return highValueLabel; }
            set { highValueLabel = value; }
        }

        private int rating;

        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public RatingQuestion()
        {
            this.lowValueLabel = String.Empty;
            this.highValueLabel = String.Empty;
            this.rating = 0;
        }
    }
}
