using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    [Serializable]
    public class ImageTargetAssessmentItemAnswer : AssessmentItemAnswer
    {
        private List<ImageTargetCoordinate> imageTargetCoordinates;

        public List<ImageTargetCoordinate> ImageTargetCoordinates
        {
            get { return imageTargetCoordinates; }
            set { imageTargetCoordinates = value; }
        }

        public ImageTargetAssessmentItemAnswer()
        {
            this.imageTargetCoordinates = new List<ImageTargetCoordinate>();
        }
    }
}
