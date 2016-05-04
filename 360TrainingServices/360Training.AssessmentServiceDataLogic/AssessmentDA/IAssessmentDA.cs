using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;


namespace _360Training.AssessmentServiceDataLogic.AssessmentDA
{
    public interface IAssessmentDA
    {
        /// <summary>
        /// This method returns the list of assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem object</returns>
        List<AssessmentItem> GetPreAssessmentAssessmentItems(int courseID, bool use_Individual_AssessmentItem);
        /// <summary>
        /// This method returns the list of post assessmentItems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem objects</returns>
        List<AssessmentItem> GetPostAssessmentAssessmentItems(int courseID, bool use_Individual_AssessmentItem);
        /// <summary>
        /// This method returns the list of quiz assessmentItems
        /// </summary>
        /// <param name="contentObjectID">int contentObjectID</param>
        /// <returns>list of assessmentItem object</returns>
        List<AssessmentItem> GetQuizAssessmentItems(int contentObjectID, bool use_Individual_AssessmentItem);
        /// <summary>
        /// This method returns the sceneGUIDs for the given AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemGUID">string assessmentItemGUID</param>
        /// <returns>string sceneGUID</returns>
        List<string> GetAssessmentItemScene(string assessmentItemGUID,int courseID);
        /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        AssessmentItemTemplate GetAssessmentItemAssessmentItemTemplate(int assessmentItemTemplateID);
        /// <summary>
        /// This method gets the assessmentitem assets by assessmentitemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>List of AssessmentItemAsset object</returns>
        List<AssessmentItemAsset> GetAssessmentItemAssets(int assessmentItemID);
        /// <summary>
        /// This mehtid gets the knowledge check assessment items by scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of assessmentitems object</returns>
        List<AssessmentItem> GetSceneAssessmentItems(int sceneID);

        System.Data.DataTable GetAssessmentAnswerItemIDsByGuid(string answerGuids);
    }
}
