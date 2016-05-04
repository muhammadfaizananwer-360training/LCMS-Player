using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;

namespace _360Training.AssessmentServiceBusinessLogic
{
    public interface IAssessmentManager
    {
       /// <summary>
        /// This method returns the list of assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        List<AssessmentItem> GetPreAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID);
        /// <summary>
        /// This method returns the list of post assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        List<AssessmentItem> GetPostAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID);
        /// <summary>
        /// This method returns the list of quiz assessmentitems
        /// </summary>
        /// <param name="courseID">int contentObjectID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        List<AssessmentItem> GetQuizAssessmentAssessmentItems(int courseId, int contentObjectID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID);
        /// <summary>
        /// This method returns the sceneGUIDs for the given AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemGUID">string assessmentItemGUID</param>
        /// <returns>string sceneGUID</returns>
        List<string> GetAssessmentItemScene(string assessmentItemGUID,int courseID);
         /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID and the assets associated with them
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        AssessmentItemTemplate GetAssessmentItemTemplate(int assessmentItemID, out List<AssessmentItemAsset> assessmentItemAssets);
        /// <summary>
        /// This mehtid gets the knowledge check assessment items by scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of assessmentitems object</returns>
        List<AssessmentItem> GetKnowledgeCheckAssessmentItems(int sceneID);
        System.Data.DataTable GetAssessmentAnswerItemIDsByGuid(string answerGuids);
        List<AssessmentItem> GetAssessmentItemsByGUIDs(string assessmentItemGuids);
        List<AssessmentItemResult> GetLearnerAssessmentItemResults(int enrollmentID, string assessmentType);
    }
}
