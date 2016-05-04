using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class Asset
    {
        private int assetID;
        public int AssetID
        {
            get { return assetID; }
            set { assetID = value; }
        }
        private string assetType;
        public string AssetType
        {
            get { return assetType; }
            set { assetType = value; }
        }
        private string assetName;
        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }
        private string uRL;
        public string URL
        {
            get { return uRL; }
            set { uRL = value; }
        }
        private string asset_GUID;
        public string Asset_GUID
        {
            get { return asset_GUID;}
            set { asset_GUID=value;}
        }
        private string assetSceneOrientation;
        public string AssetSceneOrientation
        {
            get { return assetSceneOrientation;}
            set { assetSceneOrientation=value;}
        }
        private string contentText;

        public string ContentText
        {
            get { return contentText; }
            set { contentText = value; }
        }
        private bool isTopicTitleVisible;

        public bool IsTopicTitleVisible
        {
            get { return isTopicTitleVisible; }
            set { isTopicTitleVisible = value; }
        }

        private string actionScriptVersion;

        public string ActionScriptVersion
        {
            get { return actionScriptVersion; }
            set { actionScriptVersion = value; }
        }
        //LCMS-11217
        private long affidavitTemplateId;

        public long AffidavitTemplateId
        {
            get { return affidavitTemplateId; }
            set { affidavitTemplateId = value; }
        }
        private string displayText1;

        public string DisplayText1
        {
            get { return displayText1; }
            set { displayText1 = value; }
        }

        private string displayText2;

        public string DisplayText2
        {
            get { return displayText2; }
            set { displayText2 = value; }
        }

        private string displayText3;

        public string DisplayText3
        {
            get { return displayText3; }
            set { displayText3 = value; }
        }

        //End

        public Asset()
        {
            this.assetID = 0;
            this.assetType = string.Empty;
            this.uRL = string.Empty;
            this.asset_GUID = string.Empty;
            this.assetSceneOrientation = string.Empty;
            this.contentText = string.Empty;
            this.isTopicTitleVisible = false;
            this.actionScriptVersion = string.Empty; 
            //LCMS-11217
            this.affidavitTemplateId = 0;
            this.displayText1 = string.Empty;
            this.displayText2 = string.Empty;
            this.displayText3 = string.Empty;


        }
    }
}
