using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP4.BusinessLogic.IntegerationManager
{
    public class IntegerationStatistics
    {
        private bool isCourseCompleted;

        public bool IsCourseCompleted
        {
            get { return isCourseCompleted; }
            set { isCourseCompleted = value; }
        }

        private bool isCourseLocked;

        public bool IsCourseLocked
        {
            get { return isCourseLocked; }
            set { isCourseLocked = value; }
        }

        private string lockReason;

        public string LockReason
        {
            get { return lockReason; }
            set { lockReason = value; }
        }

        private string integerationStatisticsType;

        public string IntegerationStatisticsType
        {
            get { return integerationStatisticsType; }
            set { integerationStatisticsType = value; }
        }

        private string learningSessionGuid;

        public string LearningSessionGuid
        {
            get { return learningSessionGuid; }
            set { learningSessionGuid = value; }
        }

        private int courseTimeSpent;

        public int CourseTimeSpent
        {
            get { return courseTimeSpent; }
            set { courseTimeSpent = value; }
        }

        private double percentageCourseProgress;

        public double PercentageCourseProgress
        {
            get { return percentageCourseProgress; }
            set { percentageCourseProgress = value; }
        }

        private double assessmentScore;

        public double AssessmentScore
        {
            get { return assessmentScore; }
            set { assessmentScore = value; }
        }

        private bool isAssessmentPassed;

        public bool IsAssessmentPassed
        {
            get { return isAssessmentPassed; }
            set { isAssessmentPassed = value; }
        }

        private string assessmentType;// == CourseManager.LearnerStatisticsType.PreAssessment

        public string AssessmentType
        {
            get { return assessmentType; }
            set { assessmentType = value; }
        }

        private string courseGuid;

        public string CourseGuid
        {
            get { return courseGuid; }
            set { courseGuid = value; }
        }
        private long enrollment_Id;

        public long Enrollment_Id
        {
            get { return enrollment_Id; }
            set { enrollment_Id = value; }
        }

        private string certificateURL;

        public string CertificateURL
        {
            get { return certificateURL; }
            set { certificateURL = value; }
        }
        
    }

}
