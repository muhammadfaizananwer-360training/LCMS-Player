using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowSlide
{
   
    public class SlideMediaAsset : IDisposable
    {
        private string mediaAssetID;
        public string MediaAssetID
        {
            get { return mediaAssetID; }
            set { mediaAssetID = value; }
        }

        private string mediaAssetSceneID;
        public string MediaAssetSceneID
        {
            get { return mediaAssetSceneID; }
            set { mediaAssetSceneID = value; }
        }

        private string mediaAssetType;
        public string MediaAssetType
        {
            get { return mediaAssetType; }
            set { mediaAssetType = value; }
        }

        private int contentObjectID;
        public int ContentObjectID
        {
            get { return contentObjectID; }
            set { contentObjectID = value; }
        }

        private string flashURL;
        public string FlashURL
        {
            get { return flashURL; }
            set { flashURL = value; }
        }

        private string templateHtml;
        public string TemplateHtml
        {
            get { return templateHtml; }
            set { templateHtml = value; }
        }

        private string visualTopType;

        public string VisualTopType
        {
            get { return visualTopType; }
            set { visualTopType = value; }
        }
        private string audioURL;
        public string AudioURL
        {
            get { return audioURL; }
            set { audioURL = value; }
        }


        private string flashSceneNo;
        public string FlashSceneNo
        {
            get { return flashSceneNo; }
            set { flashSceneNo = value; }
        }


        private string flashFirstSceneName;
        public string FlashFirstSceneName
        {
            get { return flashFirstSceneName; }
            set { flashFirstSceneName = value; }
        }

        private string lastScene;
        public string LastScene
        {
            get { return lastScene; }
            set { lastScene = value; }
        }

        private bool isMovieEnded;
        public bool IsMovieEnded
        {
            get { return isMovieEnded; }
            set { isMovieEnded = value; }
        }

        private bool nextButtonState;
        public bool NextButtonState
        {
            get { return nextButtonState; }
            set { nextButtonState = value; }
        }


        private bool remidiationMode;
        public bool RemidiationMode
        {
            get { return remidiationMode; }
            set { remidiationMode = value; }
        }

        private int sceneDurationTimer;

        public int SceneDurationTimer
        {
            get { return sceneDurationTimer; }
            set { sceneDurationTimer = value; }
        }
        private bool showBookMark;

        public bool ShowBookMark
        {
            get { return showBookMark; }
            set { showBookMark = value; }
        }

        private bool isAssessmentStartMessage;

        public bool IsAssessmentStartMessage
        {
            get { return isAssessmentStartMessage; }
            set { isAssessmentStartMessage = value; }
        }
        private int assessmentTimer;

        public int AssessmentTimer
        {
            get { return assessmentTimer; }
            set { assessmentTimer = value; }
        }
        private bool allowSkipping;

        public bool AllowSkipping
        {
            get { return allowSkipping; }
            set { allowSkipping = value; }
        }
        private bool disableBackButton;

        public bool DisableBackButton
        {
            get { return disableBackButton; }
            set { disableBackButton = value; }
        }
        private string titleBreadCrumb;

        public string TitleBreadCrumb
        {
            get { return titleBreadCrumb; }
            set { titleBreadCrumb = value; }
        }

        private int courseProgressPercentage;

        public int CourseProgressPercentage
        {
            get { return courseProgressPercentage; }
            set { courseProgressPercentage = value; }
        }
        private bool viewStreaming;

        public bool ViewStreaming
        {
            get { return viewStreaming; }
            set { viewStreaming = value; }
        }

        //Added By Abdus Samad
        //LCMS-12267
        //Start
        private bool playPauseFeature;

        public bool PlayPauseFeature
        {
            get { return playPauseFeature; }
            set { playPauseFeature = value; }
        }
        //Stop

        private bool playerAllowTOCDisplaySlides;

        public bool PlayerAllowTOCDisplaySlides
        {
            get { return playerAllowTOCDisplaySlides; }
            set { playerAllowTOCDisplaySlides = value; }
        }


        private string courseProgressToolTip;

        public string CourseProgressToolTip
        {
            get { return courseProgressToolTip; }
            set { courseProgressToolTip = value; }
        }
        private bool enableAllTOC;

        public bool EnableAllTOC
        {
            get { return enableAllTOC; }
            set { enableAllTOC = value; }
        }
        private bool showGradeAssessment;

        public bool ShowGradeAssessment
        {
            get { return showGradeAssessment; }
            set { showGradeAssessment = value; }
        }

		private bool isRestrictiveAssessmentEngine;
        public bool IsRestrictiveAssessmentEngine
        {
            get { return isRestrictiveAssessmentEngine; }
            set { isRestrictiveAssessmentEngine = value; }
        }

        private string sceneTemplateType;

        public string SceneTemplateType
        {
            get { return sceneTemplateType; }
            set { sceneTemplateType = value; }
        }

        private string videoFileName;

        public string VideoFileName
        {
            get { return videoFileName; }
            set { videoFileName = value; }
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

        private bool displayStandard;

        public bool DisplayStandard
        {
            get { return displayStandard; }
            set { displayStandard = value; }
        }
        private bool displayWideScreen;

        public bool DisplayWideScreen
        {
            get { return displayWideScreen; }
            set { displayWideScreen = value; }
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

        private string streamingServer;

        public string StreamingServer
        {
            get { return streamingServer; }
            set { streamingServer = value; }
        }

        private string streamingServerURL;

        public string StreamingServerURL
        {
            get { return streamingServerURL; }
            set { streamingServerURL = value; }
        }

        private string actionScriptVersion;

        public string ActionScriptVersion
        {
            get { return actionScriptVersion; }
            set { actionScriptVersion = value; }
        }

        private int examID;
        public int ExamID
        {
            get { return examID; }
            set { examID = value; }
        }

        //Abdus Samad
        //LCMS-12332
        //Start
        private bool isCourseApproval;
        public bool IsCourseApproval
        {
            get { return isCourseApproval; }
            set { isCourseApproval = value; }
        }

        private string courseApprovalMessage;

        public string CourseApprovalMessage
        {
            get { return courseApprovalMessage; }
            set { courseApprovalMessage = value; }
        }


        //Stop



        // MC
        #region MC Templates

        private string mc_scene_xml;
        public string MCSceneXml
        {
            get { return mc_scene_xml; }
            set { mc_scene_xml = value; }
        }

        #endregion

  


        // LCMS-9213
        // -------------------------------------------------------

        private bool isAssessmentResumeMessage;
        public bool IsAssessmentResumeMessage
        {
            get { return isAssessmentResumeMessage; }
            set { isAssessmentResumeMessage = value; }
        }


        private bool isConfigurationModified;
        public bool IsConfigurationModified
        {
            get { return isConfigurationModified; }
            set { isConfigurationModified = value; }
        }


        private int initialAssessmentTimerValue;

        public int InitialAssessmentTimerValue
        {
            get { return initialAssessmentTimerValue; }
            set { initialAssessmentTimerValue = value; }
        }

        private bool lockoutClickAwayToActiveWindow;
        public bool LockoutClickAwayToActiveWindow
        {
            get { return lockoutClickAwayToActiveWindow; }
            set { lockoutClickAwayToActiveWindow = value; }
        }

        // -------------------------------------------------------
        private string logOutText="";
        public string LogOutText
        {
            get { return logOutText; }
            set { logOutText = value; }
        }


        private bool isJSPlayerEnabled;
        public bool IsJSPlayerEnabled
        {
            get { return isJSPlayerEnabled; }
            set { isJSPlayerEnabled = value; }
        }

        private int sceneSequenceID;
        public int SceneSequenceID
        {
            get { return sceneSequenceID; }
            set { sceneSequenceID = value; }
        }

        private bool isHTML5Content;
        public bool IsHTML5Content
        {
            get { return isHTML5Content; }
            set { isHTML5Content = value; }
        }

        private string html5Message = "";
        public string HTML5Message
        {
            get { return html5Message; }
            set { html5Message = value; }
        }


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


        
    }
}
