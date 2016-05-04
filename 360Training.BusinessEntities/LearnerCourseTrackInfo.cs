using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerCourseTrackInfo
    {


        private int learnerSessionID;

        public int LearnerSessionID
        {
            get { return learnerSessionID; }
            set { learnerSessionID = value; }
        }

        private int courseID;

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }
        private int learnerID;

        public int LearnerID
        {
            get { return learnerID; }
            set { learnerID = value; }
        }
        private string sceneGUID;

        public string SceneGUID
        {
            get { return sceneGUID; }
            set { sceneGUID = value; }
        }
        private string itemGUID;

        public string ItemGUID
        {
            get { return itemGUID; }
            set { itemGUID = value; }
        }

        private string statisticsType;

        public string StatisticsType
        {
            get { return statisticsType; }
            set { statisticsType = value; }
        }
        private int enrollmentID;

        public int EnrollmentID
        {
            get { return enrollmentID; }
            set { enrollmentID = value; }
        }
        private string redirectURL;

        public string RedirectURL
        {
            get { return redirectURL; }
            set { redirectURL = value; }
        }

        private int totalTimeSpent;

        public int TotalTimeSpent
        {
            get { return totalTimeSpent; }
            set { totalTimeSpent = value; }
        }

        private int timeSpent;

        public int TimeSpent
        {
            get { return timeSpent; }
            set { timeSpent = value; }
        }

        private DateTime firstAccessDateTime;

        public DateTime FirstAccessDateTime
        {
            get { return firstAccessDateTime; }
            set { firstAccessDateTime = value; }
        }

        private DateTime regAccessDateTime;

        public DateTime RegAccessDateTime
        {
            get { return regAccessDateTime; }
            set { regAccessDateTime = value; }
        }

        private DateTime learningSessionStartDateTime;
        public DateTime LearningSessionStartDateTime
        {
            get { return learningSessionStartDateTime; }
            set { learningSessionStartDateTime = value; }
        }

        private LearnerCourseCompletionStatus learnerCourseCompletionStatus;

        public LearnerCourseCompletionStatus LearnerCourseCompletionStatus
        {
            get { return learnerCourseCompletionStatus; }
            set { learnerCourseCompletionStatus = value; }
        }

        private int courseApprovalID;
        public int CourseApprovalID
        {
            get { return courseApprovalID; }
            set { courseApprovalID = value; }
        }

        private int courseConfigurationID;
        public int CourseConfigurationID
        {
            get { return courseConfigurationID; }
            set { courseConfigurationID = value; }
        }

        private bool islockedcourseduringassessment;
        public bool IsLockedCourseDuringAssessment
        {
            get { return islockedcourseduringassessment; }
            set { islockedcourseduringassessment = value; }
        }

        public LearnerCourseTrackInfo()
        {
            this.courseID = 0;
            this.learnerID = 0;
            this.sceneGUID = string.Empty;
            this.itemGUID = string.Empty;
            this.statisticsType = string.Empty;
            this.enrollmentID = 0;
            this.redirectURL = string.Empty;
            this.totalTimeSpent = 0;
            this.learnerCourseCompletionStatus = new LearnerCourseCompletionStatus();
            this.CourseApprovalID = 0;
            this.CourseConfigurationID = 0;
            this.IsLockedCourseDuringAssessment = false;
            
        }

    }
}
