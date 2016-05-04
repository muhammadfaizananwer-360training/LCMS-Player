using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class Scene
    {
        private int sceneID;
        public int SceneID
        {
            get { return sceneID; }
            set { sceneID = value; }
        }
       
        private string contentText;
        public string ContentText
        {
            get { return contentText; }
            set { contentText = value; }
        }
        private int contenObjectID;
        public int ContenObjectID
        {
            get { return contenObjectID; }
            set { contenObjectID = value; }
        }
        private string scene_GUID;
        public string Scene_GUID
        {
            get { return scene_GUID;}
            set { scene_GUID = value; }
        }
        private string sceneTemplateType;
        public string SceneTemplateType
        {
            get { return sceneTemplateType;}
            set { sceneTemplateType = value; }
        }
        private int sceneTemplateID;
        public int SceneTemplateID
        {
            get { return sceneTemplateID; }
            set { sceneTemplateID = value; }
        }
        private string sceneName;

        public string SceneName
        {
            get { return sceneName; }
            set { sceneName = value; }
        }
        private int duration;

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private bool isTitleVisible;

        public bool IsTitleVisible
        {
            get { return isTitleVisible; }
            set { isTitleVisible = value; }
        }

        private bool isViewStreaming;

        public bool IsViewStreaming
        {
            get { return isViewStreaming; }
            set { isViewStreaming = value; }
        }
        private bool isTopicTileVisible;

        public bool IsTopicTileVisible
        {
            get { return isTopicTileVisible; }
            set { isTopicTileVisible = value; }
        }
        private int displayOrder;

        public int DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }
        private string contentObjectName;

        public string ContentObjectName
        {
            get { return contentObjectName; }
            set { contentObjectName = value; }
        }

        private bool isPlayPauseFeature;

        public bool IsPlayPauseFeature
        {
            get { return isPlayPauseFeature; }
            set { isPlayPauseFeature = value; }
        }

        #region Video Streaming

        private string videoFilename;

        public string VideoFilename
        {
            get { return videoFilename; }
            set { videoFilename = value; }
        }

        private string streamingServerApplication;

        public string StreamingServerApplication
        {
            get { return streamingServerApplication; }
            set { streamingServerApplication = value; }
        }

        private int startQueueHours;

        public int StartQueueHours
        {
            get { return startQueueHours; }
            set { startQueueHours = value; }
        }

        private int startQueueMinutes;

        public int StartQueueMinutes
        {
            get { return startQueueMinutes; }
            set { startQueueMinutes = value; }
        }

        private int startQueueSeconds;

        public int StartQueueSeconds
        {
            get { return startQueueSeconds; }
            set { startQueueSeconds = value; }
        }

        private int endQueueHours;

        public int EndQueueHours
        {
            get { return endQueueHours; }
            set { endQueueHours = value; }
        }

        private int endQueueMinutes;

        public int EndQueueMinutes
        {
            get { return endQueueMinutes; }
            set { endQueueMinutes = value; }
        }

        private int endQueueSeconds;

        public int EndQueueSeconds
        {
            get { return endQueueSeconds; }
            set { endQueueSeconds = value; }
        }

        private int videoHeight;

        public int VideoHeight
        {
            get { return videoHeight; }
            set { videoHeight = value; }
        }

        private int videoWidth;

        public int VideoWidth
        {
            get { return videoWidth; }
            set { videoWidth = value; }
        }

        private bool fullScreen;

        public bool FullScreen
        {
            get { return fullScreen; }
            set { fullScreen = value; }
        }


        private bool displayStandardTF;

        public bool DisplayStandardTF
        {
            get { return displayStandardTF; }
            set { displayStandardTF = value; }

        }

        private bool displayWideScreenTF;

        public bool DisplayWideScreenTF
        {
            get { return displayWideScreenTF; }
            set { displayWideScreenTF = value; }

        }


        //Added by Abdus Samad //Embeded Code WLCMS-2609 //Start 
        private bool isEmbedCode;

        public bool IsEmbedCode
        {
            get { return isEmbedCode; }
            set { isEmbedCode = value; }
        }

        private string embedCode;

        public string EmbedCode
        {
            get { return embedCode; }
            set { embedCode = value; }
        }
        //Added by Abdus Samad //Embeded Code WLCMS-2609 //Stop 


        #endregion

        public Scene()
        {
            this.sceneID = 0;
            this.contenObjectID = 0;
            this.scene_GUID = string.Empty;
            this.sceneTemplateType = string.Empty;
            this.sceneTemplateID = 0;
            this.sceneName = string.Empty;
            this.duration = 0;
            this.isTitleVisible = false;
            this.isTopicTileVisible = false;
            this.displayOrder = 0;
            this.contentObjectName = string.Empty;
            this.videoFilename = string.Empty;
            this.streamingServerApplication = string.Empty;
            this.startQueueHours = 0;
            this.startQueueMinutes = 0;
            this.startQueueSeconds = 0;
            this.endQueueHours = 0;
            this.endQueueMinutes = 0;
            this.endQueueSeconds = 0;
            this.videoHeight = 0;
            this.videoWidth = 0;
            this.fullScreen = false;

            //Added by Abdus Samad //Embeded Code WLCMS-2609 //Start 
            this.isEmbedCode = false;
            this.embedCode = string.Empty;
            //Added by Abdus Samad //Embeded Code WLCMS-2609 //Stop
        }
    }
}
