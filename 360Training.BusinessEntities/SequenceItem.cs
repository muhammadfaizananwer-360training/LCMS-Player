using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class SequenceItem
    {
        
        private  int sequenceItemID;
        public int SequenceItemID
        {
            get { return sequenceItemID; }
            set { sequenceItemID = value; }
        }
        
        private string item_GUID;
        public string Item_GUID
        {
            get { return item_GUID;}
            set { item_GUID=value;}
        }
        
        private string sequeneItemType;
        public string SequenceItemType
        {
            get { return sequeneItemType; }
            set { sequeneItemType = value; }
        }
        
        private int parentID;
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
            
        private List<Asset> assets;
        public List<Asset> Assets
        {
            get { return assets; }
            set { assets = value; }
        }
        
        private int sceneTemplateID;
        public int SceneTemplateID
        {
            get { return sceneTemplateID; }
            set { sceneTemplateID = value; }
        }
        
        private string sceneGUID;
        public string SceneGUID
        {
            get { return sceneGUID; }
            set { sceneGUID = value; }
        }
        private int sceneDuration;

        public int SceneDuration
        {
            get { return sceneDuration; }
            set { sceneDuration = value; }
        }
        private string contentObjectName;

        public string ContentObjectName
        {
            get { return contentObjectName; }
            set { contentObjectName = value; }
        }

        private bool isAllowQuizinContentObject;

        public bool IsAllowQuizInContentObject
        {
            get { return isAllowQuizinContentObject; }
            set { isAllowQuizinContentObject = value; }
        }

        private bool isNotActive;

        public bool IsNotActive
        {
            get { return isNotActive; }
            set { isNotActive = value; }
        }

        private bool isViewStreamingInScene;

        public bool IsViewStreamingInScene
        {
            get { return isViewStreamingInScene; }
            set { isViewStreamingInScene = value; }
        }

        //Added By Abdus Samad
        //lcms-12267
        //Start
        private bool isPlayPauseFeatureInScene;

        public bool IsPlayPauseFeatureInScene
        {
            get { return isPlayPauseFeatureInScene; }
            set { isPlayPauseFeatureInScene = value; }
        }
        //Stop

        private string sceneName;

        public string SceneName
        {
            get { return sceneName; }
            set { sceneName = value; }
        }
		
		public int sceneID;
        public int SceneID
        {
            get { return sceneID;}
            set { sceneID = value;}
        }
        
        public bool isTitleVisible;
        public bool IsTitleVisible
        {
            get { return isTitleVisible; }
            set { isTitleVisible = value; }
        }

        public bool isTopicTitleVisible;
        public bool IsTopicTitleVisible
        {
            get { return isTopicTitleVisible; }
            set { isTopicTitleVisible = value; }
        }

        private bool isValidQuiz;

        public bool IsValidQuiz
        {
            get { return isValidQuiz; }
            set { isValidQuiz = value; }
        }

        public int examID;
        public int ExamID
        {
            get { return examID; }
            set { examID = value; }
        }

        private string examName;
        public string ExamName
        {
            get { return examName; }
            set { examName = value; }
        }

        private string examType;
        public string ExamType
        {
            get { return examType; }
            set { examType = value; }
        }

        private string examGUID;
        public string ExamGUID
        {
            get { return examGUID; }
            set { examGUID = value; }
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


        // MC
        #region MC Templates

        private string mc_scene_xml;

        public string MCSceneXml
        {
            get { return mc_scene_xml; }
            set { mc_scene_xml = value; }
        }

        #endregion

        public SequenceItem()
        {
            this.parentID = 0;
            this.sequenceItemID = 0;
            this.sequeneItemType = string.Empty;
            this.assets = new List<Asset>();
            this.item_GUID = string.Empty;
            this.sceneGUID = string.Empty;
            this.sceneTemplateID = 0;
            this.sceneDuration = 0;
            this.contentObjectName = string.Empty;
            this.isViewStreamingInScene = false;
            this.isPlayPauseFeatureInScene = false; //Added by Abdus Samad For LCMS-12267

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
            this.isValidQuiz = false;
            this.mc_scene_xml = "";
        }
    }
}
