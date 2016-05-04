using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    
    public class AssessmentItemAsset  
    {
        
        private Asset assets;

        public Asset Assets
        {
            get { return assets; }
            set { assets = value; }
        }

        private bool isVisualTF;

        public bool IsVisualTF
        {
            get { return isVisualTF; }
            set { isVisualTF = value; }
        }
        private bool isAudioTF;

        public bool IsAudioTF
        {
            get { return isAudioTF; }
            set { isAudioTF = value; }
        }
        public AssessmentItemAsset()
        {
            this.isAudioTF = false;
            this.isVisualTF = false;
        }

       
    }
}
