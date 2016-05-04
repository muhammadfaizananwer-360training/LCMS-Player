using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class ImageTargetQuestion:AssessmentItem
    {
        private string imageURL;

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        public ImageTargetQuestion()
        {
            imageURL = string.Empty;
        }
    }
}
