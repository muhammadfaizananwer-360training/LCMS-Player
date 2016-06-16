using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ICP4.CommunicationLogic.CommunicationCommand.ShowCourseInfo
{
    public class CourseInfo : IDisposable
    {

        private string courseName;

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }

        private string courseDescription;

        public string CourseDescription
        {
            get { return courseDescription; }
            set { courseDescription = value; }
        }

        private int idleTimeOut;

        public int IdleTimeOut
        {
            get { return idleTimeOut; }
            set { idleTimeOut = value; }
        }

        private int expireTimeout;

        public int ExpireTimeout
        {
            get { return expireTimeout; }
            set { expireTimeout = value; }
        }

        private int courseTimer;

        public int CourseTimer
        {
            get { return courseTimer; }
            set { courseTimer = value; }
        }

        private int totaltimespent;

        public int TotalTimeSpent
        {
            get { return totaltimespent; }
            set { totaltimespent = value; }
        }

        private string totalTimeSpentStr;
        public string TotalTimeSpentStr
        {
            get { return totalTimeSpentStr; }
            set { totalTimeSpentStr = value; }
        }


        private int validationQuestionAskingTimer;
        public int ValidationQuestionAskingTimer
        {
            get { return validationQuestionAskingTimer; }
            set { validationQuestionAskingTimer = value; }
        }
        private int initialValidationQuestionAskingTimer;

        public int InitialValidationQuestionAskingTimer
        {
            get { return initialValidationQuestionAskingTimer; }
            set { initialValidationQuestionAskingTimer = value; }
        }




        private int minimumTimeBeforeStartingPostAssessment;
        public int MinimumTimeBeforeStartingPostAssessment
        {
            get { return minimumTimeBeforeStartingPostAssessment; }
            set { minimumTimeBeforeStartingPostAssessment = value; }
        }


        private string minimumTimeBeforeStartingPostAssessmentUnit;
        public string MinimumTimeBeforeStartingPostAssessmentUnit
        {
            get { return minimumTimeBeforeStartingPostAssessmentUnit; }
            set { minimumTimeBeforeStartingPostAssessmentUnit = value; }
        }
        
        private string logOutTexts="";
        public string logOutText
        {
            get { return logOutTexts; }
            set { logOutTexts = value; }
        }
        private bool showInstructorInfo;
        public bool ShowInstructorInfo
        {
            get { return showInstructorInfo; }
            set { showInstructorInfo = value; }
        }

        private string showInstructorText;
        public string ShowInstructorText
        {
            get { return showInstructorText; }
            set { showInstructorText = value; }
        }

        private string showInstructorImage;
        public string ShowInstructorImage
        {
            get { return showInstructorImage; }
            set { showInstructorImage = value; }
        }

        private bool showAmazonAffiliatePanel;
        public bool ShowAmazonAffiliatePanel
        {
            get { return showAmazonAffiliatePanel; }
            set { showAmazonAffiliatePanel = value; }
        }

        private bool showCoursesRecommendationPanel;
        public bool ShowCoursesRecommendationPanel
        {
            get { return showCoursesRecommendationPanel; }
            set { showCoursesRecommendationPanel = value; }
        }

        private bool restrictIncompleteJSTemplate;
        public bool RestrictIncompleteJSTemplate
        {
            get { return restrictIncompleteJSTemplate; }
            set { restrictIncompleteJSTemplate = value; }
        }

        public CourseInfo()
        {
            //Abdus Samad
            //LCMS-12700
            //Start
            ValidationQuestionAskingTimer = -1;
            InitialValidationQuestionAskingTimer = -1;
            //Stop
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
