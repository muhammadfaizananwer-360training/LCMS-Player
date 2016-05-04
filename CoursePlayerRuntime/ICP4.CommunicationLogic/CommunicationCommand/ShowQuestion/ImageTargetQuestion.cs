using System;
using System.Collections.Generic;
using System.Text;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion
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
        //private Image imageAsset;

        //public Image ImageAsset
        //{
        //    get { return imageAsset; }
        //    set { imageAsset = value; }
        //}

        //public ImageTargetQuestion()
        //{
        //    imageAsset = new Image();
        //}
    }
}
