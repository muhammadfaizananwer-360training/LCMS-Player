using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.CourseServiceDataLogic.CourseDA
{
    public interface ICourseDA
    {
        /// <summary>
        /// This method is used to get all contentobjects belong to certain course.
        /// </summary>
        /// <param name="courseID">CourseId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing contentobjects, null otherwise.</returns>
        List<ContentObject> GetCourseContentObjects(int courseID);
        /// <summary>
        /// This method is used to get all scenes belong to certain contentobject.
        /// </summary>
        /// <param name="contentObjectID">ContentObjectId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing scenes, null otherwise.</returns>
        List<Scene> GetContentObjectScenes(int contentObjectID);
        /// <summary>
        /// This method is used to get all assets belong to certain Scene.
        /// </summary>
        /// <param name="sceneID">AssetID non-zero integer value.</param>
        /// <returns>This method will return list of Asset items representing assets, null otherwise.</returns>
        Asset GetAsset(int assetID);

        /// <summary>
        /// This method is used to get all assets Information.
        /// </summary>
        /// <param name="sceneID">SceneId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing assets, null otherwise.</returns>
        List<Asset> GetSceneAssets(int sceneID);
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
        List<GlossaryItem> GetCourseSceneGlossaryItems(int courseID, int sceneID);
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
        /// This method gets the scene template HTMLURL
        /// </summary>
        /// <param name="sceneTemplateID"></param>
        /// <returns></returns>
        string GetSceneTemplateHtmlURL(int sceneTemplateID);
        /// <summary>
        /// This method gets the CourseIntroPage
        /// </summary>
        /// <param name="courseID">in courseID</param>
        /// <returns>CourseIntroPage object</returns>
        CourseIntroPage GetIntroPage(int courseID);
        /// <summary>
        /// This method returns the course end page
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>CourseEndPage object</returns>
        CourseEndPage GetEndPage(int courseID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        string GetCourseGUID(int courseID);
        /// <summary>
        /// This method is used to get all demoable contentobjects belonging to certain course.
        /// </summary>
        /// <param name="courseID">CourseId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing contentobjects, null otherwise.</returns>
        List<ContentObject> GetCourseDemoableContentObjects(int courseID);
        /// <summary>
        /// This method selects scene templates by category
        /// </summary>
        /// <param name="sceneTemplateCategory">string sceneTemplateCategory</param>
        /// <returns>List of SceneTemplate object</returns>
        List<SceneTemplate> GetCategorySceneTemplates(string sceneTemplateCategory);
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
        string CreateValidationUnlockRequest(string learningSessionId, string strGUID, string brandCode, string variant);
        SceneTemplate GetSceneTemplateWithHTMLVariant(int sceneTemplateID, bool isText, bool isVOText, bool isVisualTop, bool isHeading);
        /// <summary>
        /// This method returns all scenes related to a course via contentobjects
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of Scene objects</returns>
        List<Scene> GetCourseScenes(int courseID);
        /// <summary>
        /// This method returns all demoable scenes related to a course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of Scene objects</returns>
        List<Scene> GetCourseDemoableScenes(int courseID);
        /// <summary>
        /// Gets the course type by course guid
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        string GetCourseTypeByGUID(string courseGUID);
         /// <summary>
        /// This method gets the root contentobjects associated with the course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>returns list of contentobjects</returns>
        List<ContentObject> GetCourseRootContentObject(int courseID);
        string GetContentObjectName(string ContentObjectGUID);
        /// <summary>
        /// This method will be having information related to the Select Course Approval Message
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="LearnerID"></param>
        /// <param name="CourseApprovalID"></param>
        /// <returns></returns>
        bool SelectCourseApprovalMessage(string CourseID, string LearnerID, string CourseApprovalID, string learnerSessionGUID);

        #region DocuSign LCMS-11217
        DocuSignLearner GetLearnerData(int CourseID, string LearnerSessionID, int LearnerID, int EnrollmentID);
        int SaveEnvelopeId(int EnrollmentID, string EnvelopeId);
        string GetEnvelopeId(int EnrollmentID);
        int SaveStatusAfterDocuSignProcessComplete(int EnrollmentID);
        bool GetDocuSignedAffidavitStatus(int EnrollmentID);
        #endregion


        //LCMS-13475
        //Abdus Samad
        //Start
        /// <summary>
        /// Get Multiple Quiz Configuration Count
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        bool GetMultipleQuizConfigurationCount(int courseID);
        //Stop
        string GetCourseGroupsByCourse(int CourseID);
    }
}
