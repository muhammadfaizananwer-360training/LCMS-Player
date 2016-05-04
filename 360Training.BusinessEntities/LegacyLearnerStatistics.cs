using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LegacyLearnerStatistics
    {
        public LegacyLearnerStatistics()
        {
        }

        private string learningSessionGuid;

        public string LearningSessionGuid
        {
            get { return learningSessionGuid; }
            set { learningSessionGuid = value; }
        }
        private int timeSpent;

        public int TimeSpent
        {
            get { return timeSpent; }
            set { timeSpent = value; }
        }
        private int percentageCompleted;

        public int PercentageCompleted
        {
            get { return percentageCompleted; }
            set { percentageCompleted = value; }
        }

        private List<LegacyCourseTestingResult> legacyCourseTestingResult;

        public List<LegacyCourseTestingResult> LegacyCourseTestingResult
        {
            get { return legacyCourseTestingResult; }
            set { legacyCourseTestingResult = value; }
        }

        //private int assessmentType;

        //public int AssessmentType
        //{
        //    get { return assessmentType; }
        //    set { assessmentType = value; }
        //}

        private string legacyCourseType;

        public string LegacyCourseType
        {
            get { return legacyCourseType; }
            set { legacyCourseType = value; }
        }

        private int studentRecordId;

        public int StudentRecordId
        {
            get { return studentRecordId; }
            set { studentRecordId = value; }
        }


        private int completed;

        public int Completed
        {
            get { return completed; }
            set { completed = value; }
        }
        private string certificateURL;

        public string CertificateURL
        {
            get { return certificateURL; }
            set { certificateURL = value; }
        }

    }
}
