using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.CourseServiceBusinessLogic
{
    public interface ICourseManager
    {
        /// <summary>
        /// This method gets the sequence object based on course configuration
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration Object</param>
        /// <returns>Sequence object</returns>
        Sequence GetSequence(int courseID, CourseConfiguration courseConfiguration);
        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        CourseConfiguration GetCourseConfiguration(int courseID);
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of glossaryitem objects</returns>
        List<GlossaryItem> GetCourseGlossaryItems(int courseID);
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of glossaryitem objects</returns>
        List<GlossaryItem> GetCourseSceneGlossaryItems(int courseID,int sceneID); 
        /// <summary>
        /// This method returns the glossary item definition
        /// </summary>
        /// <param name="glossaryItemID">int glossaryitemID</param>
        /// <returns>GlossaryItem object</returns>
        GlossaryItem GetGlossaryItemDefinition(int glossaryItemID);
        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        string GetCourseName(int courseID);
        /// <summary>
        /// This method gets the CourseMaterialInfo objects associated to a Course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of CourseMaterialInfo object</returns>
        List<CourseMaterialInfo> GetCourseMaterialInfo(int courseID);
        /// <summary>
        /// This mehod gets the scene template HTML
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        SceneTemplate GetSceneTemplateHTML(int sceneTemplateID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        string GetCourseGUID(int courseID);
        /// <summary>
        /// This method gets the validation questions and their answers
        /// </summary>
        /// <param name="variant">int learnerid</param>
        /// <param name="variant">string variant</param>
        /// <returns>list of object validation questions</returns>
        List<ValidationQuestion> GetValidationQuestions(int learnerID, string variant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        bool CreateValidationUnlockRequest(string learningSessionId,string brandCode,string variant);
        SceneTemplate GetSceneTemplateWithHTMLVariant(int sceneTemplateID, bool isText, bool isVOText, bool isVisualTop, bool isHeading);
        /// <summary>
        /// Gets the course type by course guid
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        string GetCourseTypeByGUID(string courseGUID);

        /// <summary>
        /// This method gets the Asset Object
        /// </summary>
        /// <param name="courseID">Asset assetID</param>
        /// <param name="courseConfiguration">CourseConfiguration Object</param>
        /// <returns>Sequence object</returns>
        Asset GetAsset(int assetID);
        bool SaveLearnerCourseMessage(int courseID, string learningSessionGuid);
        bool CheckLearnerCourseMessage(int courseID, string learningSessionGuid);


        //bool ProctorLoginRequirementCriteriaMeets(string learningSessionGuid);
        //int AuthenticateProctor(long courseID, long learnerID, string learningSessionID, string proctorLogin, string proctorPassword);

        #region DocuSign LCMS-11217
        DocuSignLearner GetLearnerData(int CourseID, string LearnerSessionID, int LearnerID, int EnrollmentID);
        int SaveEnvelopeId(int EnrollmentID, string EnvelopeId);
        string GetEnvelopeId(int EnrollmentID);
        int SaveStatusAfterDocuSignProcessComplete(int EnrollmentID);
        bool GetDocuSignedAffidavitStatus(int EnrollmentID);
        CourseInfo GetCourseInformation(int EnrollmentID);
        int GetEnrollmentIdAgainstEnvelopeId(string EnvelopeId);
        #endregion

    }
}
