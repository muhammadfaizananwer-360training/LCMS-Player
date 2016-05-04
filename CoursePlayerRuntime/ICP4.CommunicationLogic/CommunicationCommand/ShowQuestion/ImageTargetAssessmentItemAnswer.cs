using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
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
