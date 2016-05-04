using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using _360Training.BusinessEntities;
using _360Training.AssessmentServiceBusinessLogic;

namespace _360Training.AssessmentService
{
    /// <summary>
    /// Summary description for AssessmentService
    /// </summary>
    [WebService(Namespace = "http://www.360training.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AssessmentService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //[XmlInclude(typeof(TrueFalseQuestion))]
        //[XmlInclude(typeof(MatchingQuestion))]
        //[XmlInclude(typeof(MultipleChoiceQuestion))]
        //[XmlInclude(typeof(SingleSelectQuestion))]
        //[XmlInclude(typeof(MultipleSelectQuestion))]
        //[XmlInclude(typeof(FillInTheBlankQuestion))]
        //[XmlInclude(typeof(TextInputQuestion))]
        //[XmlInclude(typeof(NumericInputQuestion))]
        //[XmlInclude(typeof(OrderingQuestion))]
        //[XmlInclude(typeof(ImageTargetQuestion))]
        //[XmlInclude(typeof(RatingQuestion))]
        //[XmlInclude(typeof(AssessmentItemAnswer))]
        //[XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        //[XmlInclude(typeof(FITBAssessmentItemAnswer))]
        //[XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        //[XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        //public List<AssessmentItem> GetPreAssessmentAssessmentItems()
        //{
        //    int courseID = 93;
        //    List<string> previouslyAskedQuestionIDs = new List<string>();
        //    previouslyAskedQuestionIDs.Add("d11322a5-e2ec-43e9-9fb6-483f1f9de2");
        //    previouslyAskedQuestionIDs.Add("df4d993a-e8a9-4476-b89f-f09944e727");
        //    //previouslyAskedQuestionIDs.Add("987679a6-8580-4e71-ac56-217693b300");
        //    CourseConfiguration courseConfiguration = new CourseConfiguration();
        //    using (AssessmentManager assessmentManager = new AssessmentManager())
        //    {
        //        return assessmentManager.GetQuizAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionIDs);
        //    }
        //}

        /// <summary>
        /// This method returns the list of postAssessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem object</returns>
        [WebMethod]
        [XmlInclude(typeof(TrueFalseQuestion))]
        [XmlInclude(typeof(MatchingQuestion))]
        [XmlInclude(typeof(MultipleChoiceQuestion))]
        [XmlInclude(typeof(SingleSelectQuestion))]
        [XmlInclude(typeof(MultipleSelectQuestion))]
        [XmlInclude(typeof(FillInTheBlankQuestion))]
        [XmlInclude(typeof(TextInputQuestion))]
        [XmlInclude(typeof(NumericInputQuestion))]
        [XmlInclude(typeof(OrderingQuestion))]
        [XmlInclude(typeof(ImageTargetQuestion))]
        [XmlInclude(typeof(RatingQuestion))]
        [XmlInclude(typeof(AssessmentItemAnswer))]
        [XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        [XmlInclude(typeof(FITBAssessmentItemAnswer))]
        [XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        [XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        public List<AssessmentItem> GetPostAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionIDs, int examID)
        {            
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionIDs, examID);
            }
        }
        /// <summary>
        /// This method returns the list of pre assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        [WebMethod]
        [XmlInclude(typeof(TrueFalseQuestion))]
        [XmlInclude(typeof(MatchingQuestion))]
        [XmlInclude(typeof(MultipleChoiceQuestion))]
        [XmlInclude(typeof(SingleSelectQuestion))]
        [XmlInclude(typeof(MultipleSelectQuestion))]
        [XmlInclude(typeof(FillInTheBlankQuestion))]
        [XmlInclude(typeof(TextInputQuestion))]
        [XmlInclude(typeof(NumericInputQuestion))]
        [XmlInclude(typeof(OrderingQuestion))]
        [XmlInclude(typeof(ImageTargetQuestion))]
        [XmlInclude(typeof(RatingQuestion))]
        [XmlInclude(typeof(AssessmentItemAnswer))]
        [XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        [XmlInclude(typeof(FITBAssessmentItemAnswer))]
        [XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        [XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        public List<AssessmentItem> GetPreAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionIDs, int examID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, previouslyAskedQuestionIDs, examID);
            }
        }
        /// <summary>
        /// This method returns the list of quiz assessmentitems
        /// </summary>
        /// <param name="courseID">int contentObjectID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        [WebMethod]
        [XmlInclude(typeof(TrueFalseQuestion))]
        [XmlInclude(typeof(MatchingQuestion))]
        [XmlInclude(typeof(MultipleChoiceQuestion))]
        [XmlInclude(typeof(SingleSelectQuestion))]
        [XmlInclude(typeof(MultipleSelectQuestion))]
        [XmlInclude(typeof(FillInTheBlankQuestion))]
        [XmlInclude(typeof(TextInputQuestion))]
        [XmlInclude(typeof(NumericInputQuestion))]
        [XmlInclude(typeof(OrderingQuestion))]
        [XmlInclude(typeof(ImageTargetQuestion))]
        [XmlInclude(typeof(RatingQuestion))]
        [XmlInclude(typeof(AssessmentItemAnswer))]
        [XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        [XmlInclude(typeof(FITBAssessmentItemAnswer))]
        [XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        [XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        public List<AssessmentItem> GetQuizAssessmentItems(int courseId, int contentObjectID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionIDs, int examID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetQuizAssessmentAssessmentItems(courseId, contentObjectID, courseConfiguration, previouslyAskedQuestionIDs, examID);
            }
        }

        /// <summary>
        /// This method returns the list of quiz assessmentitems
        /// </summary>
        /// <param name="courseID">int contentObjectID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        [WebMethod]
        [XmlInclude(typeof(TrueFalseQuestion))]
        [XmlInclude(typeof(MatchingQuestion))]
        [XmlInclude(typeof(MultipleChoiceQuestion))]
        [XmlInclude(typeof(SingleSelectQuestion))]
        [XmlInclude(typeof(MultipleSelectQuestion))]
        [XmlInclude(typeof(FillInTheBlankQuestion))]
        [XmlInclude(typeof(TextInputQuestion))]
        [XmlInclude(typeof(NumericInputQuestion))]
        [XmlInclude(typeof(OrderingQuestion))]
        [XmlInclude(typeof(ImageTargetQuestion))]
        [XmlInclude(typeof(RatingQuestion))]
        [XmlInclude(typeof(AssessmentItemAnswer))]
        [XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        [XmlInclude(typeof(FITBAssessmentItemAnswer))]
        [XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        [XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        public List<AssessmentItem> GetPracticeExamAssessmentItems(int courseId, int contentObjectID, AssessmentConfiguration assessmentConfiguration, List<string> previouslyAskedQuestionIDs, int examID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetPracticeExamAssessmentItems(courseId, contentObjectID, assessmentConfiguration, previouslyAskedQuestionIDs, examID);
            }
        }
        /// <summary>
        /// This method returns the sceneGUID for the given AssessmentItemID belonging to the given course
        /// </summary>
        /// <param name="assessmentItemGUID">string assessmentItemGUID</param>
        /// <param name="courseID">int courseID</param>
        /// <returns>string sceneGUID</returns>
        [WebMethod]
        public List<string> GetAssessmentItemScene(string assessmentItemGUID,int courseID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentItemScene(assessmentItemGUID,courseID);
            }
        }
       /// <summary>
        /// This method returns the assessmentitem template by assessmentitemID alongwith the assets associated with that template
       /// </summary>
       /// <param name="assessmentItemID"></param>
       /// <param name="assessmentItemAssets">output list of assessmentitemassets</param>
       /// <returns>assessmentiemtemplate </returns>
        [WebMethod]
        public AssessmentItemTemplate GetAssessmentItemTemplate(int assessmentItemID, out List<AssessmentItemAsset> assessmentItemAssets)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentItemTemplate(assessmentItemID,out assessmentItemAssets);
            }
        }


        /// <summary>
        /// This method returns the assessment items when Random Alternate policy is turned on (for learner mode)
        /// </summary>
        /// <param name="assessmentItemID"></param>
        /// <param name="assessmentItemAssets">output list of assessmentitemassets</param>
        /// <returns>List of assessment items</returns>
        [WebMethod]
        public List<AssessmentItem> GetAssessmentItemsForRandomAlternate(string learnerSessionID, AssessmentConfiguration assessmentConfig,bool isPauseResumeCase)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {                
                return assessmentManager.GetAssessmentItemsForRandomAlternate(learnerSessionID, assessmentConfig,isPauseResumeCase);
            }
         
        }


        /// <summary>
        /// This method returns the assessment items when Random Alternate Multiple Item banks policy is turned on (for learner mode)
        /// </summary>
        /// <param name="assessmentItemID"></param>
        /// <param name="assessmentItemAssets">output list of assessmentitemassets</param>
        /// <returns>List of assessment items</returns>
        [WebMethod]
        public List<AssessmentItem> GetTestForRandomAlternate(string learnerSessionID, AssessmentConfiguration assessmentConfig, bool isPauseResumeCase)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetTestForRandomAlternateMultipleItemBanks(learnerSessionID, assessmentConfig,isPauseResumeCase);
            }

        }

        [WebMethod]
        public List<AssessmentItem> TestGetAssessmentItemsForBankID(int BankID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.TestGetAssessmentItemsForBankID(BankID);
            }

        }

        //LCMS-9882
        [WebMethod]
        public List<AssessmentItemBank> GetAssessmentItemsByAssessmentBankIDs(string assessmentBankIDs)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
            }

        }


        
        /// <summary>
        /// This method returns the assessment items when Random Alternate policy is turned on (for preview mode)
        /// </summary>
        /// <param name="askedBank">comma separated list of asked banks</param>
        /// <param name="assessmentItemAssets">output list of assessmentitemassets</param>
        /// <returns>ArrayList</returns>
        [WebMethod]
        public ArrayList GetAssessmentItemsForRandomAlternateInPreviewMode(string askedBank, AssessmentConfiguration assessmentConfig)
        {            
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentItemsForRandomAlternateInPreviewMode(askedBank, assessmentConfig);
            }
         
        }

        /// <summary>
        /// This method returns the assessment items when Random Alternate multiple item banks policy is turned on (for preview mode)
        /// </summary>
        /// <param name="askedBank">comma separated list of asked tests</param>
        /// <param name="assessmentItemAssets">output list of assessmentitemassets</param>
        /// <returns>ArrayList</returns>
        [WebMethod]
        public ArrayList GetTestForRandomAlternateMultipleItemBankInPreviewMode(string askedTests, AssessmentConfiguration assessmentConfig)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetTestForRandomAlternateMultipleItemBankInPreviewMode(askedTests, assessmentConfig);
            }

        }

        // LCMS-9213
        [WebMethod]
        public DataTable GetAssessmentAnswerItemIDsByGuid(string answerGuids)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentAnswerItemIDsByGuid(answerGuids);
            }
         
        }



        // LCMS-9213
        [WebMethod]
        public List<AssessmentItem> GetAssessmentItemsByGUIDs(string assessmentItemGuids)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetAssessmentItemsByGUIDs(assessmentItemGuids);
            }
        }

        //LCMS-13896
        [WebMethod]
        /// <summary>
        /// This method returns the list of assessment Item result
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <param name="assessmentType">int assessmentType</param>
        /// <returns>list of course approvals objects</returns>
        public List<AssessmentItemResult> GetLearnerAssessmentItemResults(int enrollmentID, string assessmentType)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetLearnerAssessmentItemResults(enrollmentID, assessmentType);
            }
        }

        #region Knowlege Check
        [WebMethod]
        [XmlInclude(typeof(TrueFalseQuestion))]
        [XmlInclude(typeof(MatchingQuestion))]
        [XmlInclude(typeof(MultipleChoiceQuestion))]
        [XmlInclude(typeof(SingleSelectQuestion))]
        [XmlInclude(typeof(MultipleSelectQuestion))]
        [XmlInclude(typeof(FillInTheBlankQuestion))]
        [XmlInclude(typeof(TextInputQuestion))]
        [XmlInclude(typeof(NumericInputQuestion))]
        [XmlInclude(typeof(OrderingQuestion))]
        [XmlInclude(typeof(ImageTargetQuestion))]
        [XmlInclude(typeof(RatingQuestion))]
        [XmlInclude(typeof(AssessmentItemAnswer))]
        [XmlInclude(typeof(ImageTargetAssessmentItemAnswer))]
        [XmlInclude(typeof(FITBAssessmentItemAnswer))]
        [XmlInclude(typeof(OrderingAssessmentItemAnswer))]
        [XmlInclude(typeof(MatchingAssessmentItemAnswer))]
        public List<AssessmentItem> GetKnowledgeCheckAssessmentItems(int sceneID)
        {
            using (AssessmentManager assessmentManager = new AssessmentManager())
            {
                return assessmentManager.GetKnowledgeCheckAssessmentItems(sceneID);
            }
        }
        #endregion
    }
}
