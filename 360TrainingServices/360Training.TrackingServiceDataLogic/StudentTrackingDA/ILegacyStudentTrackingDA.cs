using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.TrackingServiceDataLogic.StudentTrackingDA
{
    public interface ILegacyStudentTrackingDA
    {
        /// <summary>
        /// This method gets the statistics of Scorm course into VU
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="studentID">int studentID</param>
        /// /// <param name="Epoch">int Epoch</param>
        /// <returns>LegacyLearnerStatistics</returns> 
        LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORSCORM(int courseID, int studentID, int Epoch);
        LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP3(int courseID, int studentID, int Epoch);
        LegacyLearnerStatistics GET_LCMS_CONNECTOR_STATS_FORICP2(int courseID, int studentID, int Epoch);
        LegacyCertificateInfo GET_LCMS_CONNECTOR_STATS_CERTIFICATE(int courseID, int studentID, int Epoch);
        void UPDATE_LCMS_STUDENTCOURSE(int courseID, int studentID, int Epoch, string LearningSessionGuid, int TestingId,int status);
    }
}
