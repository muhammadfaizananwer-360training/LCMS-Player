using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Collections.Generic;
using _360Training.BusinessEntities;
using _360Training.CourseServiceBusinessLogic;

namespace _360Training.CourseService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.360training.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CourseService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// This method returns the sequence object 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>Sequence object</returns>
        //[WebMethod]
        //public Sequence GetSequences(int courseID)
        //{
        //    using (CourseManager courseManager = new CourseManager())
        //    {
        //        return courseManager.GetSequence(courseID);
        //    }
        //}
        /// <summary>
        /// This method returns the sequence object on the basis of course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <returns>Sequence object</returns>
        [WebMethod]
        public Sequence GetSequence(int courseID, CourseConfiguration courseConfiguration)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSequence(courseID, courseConfiguration);
            }
        }
        /// <summary>
        /// This method gets the course configuration object
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>CourseConfiguration object</returns>
        [WebMethod]
        public CourseConfiguration GetCourseConfiguaration(int courseConfigurationID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseConfiguration(courseConfigurationID);
            }
        }

        /// <summary>
        /// This method gets the course configuration object
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>CourseConfiguration object</returns>
        [WebMethod]
        public int GetCourseConfiguarationID(int courseID, int source, int courseApprovalID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseConfigurationID(courseID, source, courseApprovalID);   
            }
        }

        /// <summary>
        /// This method gets the course configuration object
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseID">int courseApprovalID</param> 
        /// <returns>CourseConfiguration object</returns>
        [WebMethod]
        public CourseConfiguration GetCourseApprovalCourseConfiguaration(int courseID, int courseApprovalID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseApprovalCourseConfiguration(courseID, courseApprovalID);   
            }
        }

        /// <summary>
        /// This method gets the Practice Exam Assessment configuration object
        /// </summary>
        /// <param name="courseID">int examID</param>
        /// <returns>AssessmentConfiguration object</returns>
        [WebMethod]
        public AssessmentConfiguration GetPraceticeExamAssessmentConfiguration(int examID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetPraceticeExamAssessmentConfiguration(examID); 
            }
        }

        /// <summary>
        /// This method gets the Quiz Exam Assessment configuration object
        /// </summary>
        /// <param name="courseID">int examID</param>
        /// <returns>AssessmentConfiguration object</returns>
        [WebMethod]
        public AssessmentConfiguration GetQuizExamAssessmentConfiguration(int contentObjectID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetQuizExamAssessmentConfiguration(contentObjectID);
            }
        }

        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        [WebMethod]
        public string GetCourseName(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseName(courseID);
            }
        }

        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        [WebMethod]
        public string[] GetCourseNameAndDescription(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseNameAndDescription(courseID);
            }
        }

        /// <summary>
        /// This mehod gets the scene template HTML
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        [WebMethod]
        public SceneTemplate GetSceneTemplateHTML(int sceneTemplateID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSceneTemplateHTML(sceneTemplateID);
            }
        }

        /// <summary>
        /// This mehod gets the scene template HTML
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        [WebMethod]
        public SceneTemplate GetSceneTemplateHTMLByType(string type)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSceneTemplateHTMLByNameAndType(type);
            }
        }

        /// <summary>
        /// This method gets the EOC Instructions against course id
        /// </summary>
        /// <param name="courseId">int courseId</param>
        /// <returns>EOCInstructions object</returns>
        [WebMethod]
        public EOCInstructions GetEOCInstructions_LMS(int courseId)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetEOCInstructions_LMS(courseId);
            }
        }

        [WebMethod]
        public String GetCourseEOCInstructions(int courseId)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseEOCInstructions(courseId);
            }
        }
        
        /// <summary>
        /// This method gets the scene template HTML variant according to the parameters
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        [WebMethod]
        public SceneTemplate GetSceneTemplateWithHTMLVariant(int sceneTemplateID,bool isText,bool isVOText,bool isVisualTop,bool isHeading)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSceneTemplateWithHTMLVariant(sceneTemplateID,isText,isVOText,isVisualTop,isHeading);
            }
        }
        [WebMethod]
        public Sequence GetCourseDemoSequence(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseDemoSequence(courseID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetOriginalCourseID(int offeredcourseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetOriginalCourseID(offeredcourseID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetCourseGUID(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseGUID(courseID);
            }
        }
        /// <summary>
        /// Gets the course type by course guid
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetCourseTypeByGUID(string courseGUID)
        {           
                using (CourseManager courseManager = new CourseManager())
                {
                    return courseManager.GetCourseTypeByGUID(courseGUID);
                }         
        }

        [WebMethod]
        public string GetContentObjectName(int contentobjectID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetContentObjectName(contentobjectID);
            }
        }
        [WebMethod]
        public string GetContentObjectNameByGUID(string ContentObjectGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetContentObjectName(ContentObjectGUID);
            }
        }
        [WebMethod]
        public string GetContentObjectNameByExamGUID(string ExamGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetContentObjectNameByExamGUID(ExamGUID);  
            }
        }
        [WebMethod]
        public string GetSceneGUID(int sceneID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSceneGUID(sceneID);
            }
        }

        [WebMethod]
        public string GetAssetGUID(int assetID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetAssetGUID(assetID);
            }
        }
        [WebMethod]
        public int GetCourseID(string courseGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseID(courseGUID);
            }
        }
		/// <summary>
        /// This method gets Assets of scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>Asset List</returns>
        [WebMethod]
        public List<Asset> GetSceneAssets(int sceneID)
        {
            using(CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetSceneAssets(sceneID);
            }
        }

        [WebMethod]
        public Asset GetAsset(int assetID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetAsset(assetID);
            }
        }

        [WebMethod]
        public Asset GetAffidavitAsset(int affidativeID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetAffidavitAsset(affidativeID); 
            }
        }

        #region TableOfContent
        /// <summary>
        /// This method gets the table of content
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>TableOfContent object</returns>
        [WebMethod]
        public TableOfContent GetTableOfContent(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetTableOfContent(courseID);
            }
        }
        #endregion

        #region GlossaryItem
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of glossaryitem objects</returns>
        [WebMethod]
        public List<GlossaryItem> GetCourseGlossaryItems(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseGlossaryItems(courseID);
            }
        }
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int curseID</param>
        /// <param name="?">int sceneID</param>
        /// <returns>GlossaryItem object list</returns>
        [WebMethod]
        public List<GlossaryItem> GetCourseSceneGlossaryItems(int courseID,int sceneID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseSceneGlossaryItems(courseID,sceneID);
            }
        }
        /// <summary>
        /// This method returns the glossary item definition
        /// </summary>
        /// <param name="glossaryItemID">int glossaryitemID</param>
        /// <returns>GlossaryItem object</returns>
        [WebMethod]
        public GlossaryItem GetGlossaryItemDefinition(int glossaryItemID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetGlossaryItemDefinition(glossaryItemID);
            }
        }
        #endregion

        #region CourseMaterial
        /// <summary>
        /// This method gets the CourseMaterialInfo objects associated to a Course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of CourseMaterialInfo object</returns>
        [WebMethod]
        public List<CourseMaterialInfo> GetCourseMaterialInfo(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseMaterialInfo(courseID);
            }
        }
        #endregion

        #region Validation Questions
        /// <summary>
        /// This method gets the validation questions and their answers
        /// </summary>
        /// <param name="variant">int learnerid</param>
        /// <param name="variant">string variant</param>
        /// <returns>list of object validation questions</returns>
        [WebMethod]
        public List<ValidationQuestion> GetValidationQuestions(int learnerID, string variant)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetValidationQuestions(learnerID,variant);
            }
        }

        [WebMethod]
        public bool CreateValidationUnlockRequest(string learningSessionId,string brandCode,string variant)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.CreateValidationUnlockRequest(learningSessionId, brandCode, variant);
            }
        }
        [WebMethod]
        public SceneTemplate GetValidationOrientaionSceneTemplate()
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetValidationSceneOrientationTemplateHTML();
            }
        }
        #endregion

        #region Course Evaluation
        [WebMethod]
        [XmlInclude(typeof(BusinessEntities.FillInTheBlankCourseEvaluationQuestion))]
        [XmlInclude(typeof(BusinessEntities.MultipleSelectCourseEvaluationQuestion))]
        [XmlInclude(typeof(BusinessEntities.SingleSelectCourseEvaluationQuestion))]
        [XmlInclude(typeof(BusinessEntities.TextCourseEvaluationQuestion))]
        public CourseEvaluation GetCourseEvaluationByCourseID(int courseID, string surveyType)
        {
           using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseEvaluationByCourseID(courseID, surveyType);
            }
        }        
        [WebMethod]
        public int GetCourseEvaluationQuestionsCount(int courseID, string surveyType)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseEvaluationQuestionsCount(courseID, surveyType);
            }
        }
        #endregion 

        #region Course Approval
        /// <summary>
        /// This method returns the list of Course Approval
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of Course Approval objects</returns>
        [WebMethod]
        public List<CourseApproval> GetCourseCourseApproval(int courseID, string learningSessionGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseCourseApproval(courseID, learningSessionGUID); 
            }
        }

        /// <summary>
        /// This method returns the true/false
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of true/false</returns>
        [WebMethod]
        public bool CheckLearnerCourseCourseApproval(int courseID, string learningSessionGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.CheckLearnerCourseCourseApproval(courseID, learningSessionGUID);
            }
        }

        /// <summary>
        /// This method save the Learner Course Approval
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>
        /// <param name="CourseApprovalID">int CourseApprovalID</param> 
        /// <returns>int</returns>
        [WebMethod]
        public int SaveLearnerCourseApproval(int courseID, string learningSessionGUID, int CourseApprovalID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveLearnerCourseApproval(courseID, learningSessionGUID, CourseApprovalID);   
            }
        }

        /// <summary>
        /// This method save the Learner Course Message
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>        
        /// <returns>int</returns>
        [WebMethod]
        public bool SaveLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveLearnerCourseMessage(courseID, learningSessionGuid);
            }
        }

        /// <summary>
        /// This method save the Learner Course Message
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>        
        /// <returns>int</returns>
        [WebMethod]
        public bool CheckLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.CheckLearnerCourseMessage(courseID, learningSessionGuid);
            }
        }

        /// <summary>
        /// This method gets the Course Approval' Affidavit ID
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>
        /// <returns>Affidavit ID</returns>
        [WebMethod]
        public int GetCourseApprovalAffidavit(int courseID, int courseApprovalID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseApprovalAffidavit(courseID, courseApprovalID); 
            }
        }

        /// <summary>
        /// This method returns CourseApprovalCertificate
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>        
        /// <returns>CourseApprovalCertificate</returns>
        [WebMethod]
        public CourseApprovalCertificate GetCourseApprovalCertificate(int courseApprovalID, int enrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseApprovalCertificate(courseApprovalID, enrollmentID);
            }
        }
        #endregion

        #region Sub Content Owner
        /// <summary>
        /// This method gets the Original Course ID into Sub Content Owner
        /// </summary>
        /// <param name="courseID">int courseID</param>        
        /// <returns>Course ID</returns>
        [WebMethod]
        public int GetOriginalCourseIDFromSubContentOwner(int offerCourseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetOriginalCourseIDFromSubContentOwner(offerCourseID);
            }
        }
        #endregion

        //LCMS-10392
        [WebMethod]
        public string GetCourseKeywords(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseKeywords(courseID);
            }
        }

        //[WebMethod]
        //public bool ProctorLoginRequirementCriteriaMeets(string learningSessionGuid)
        //{
        //    using (CourseManager courseManager = new CourseManager())
        //    {
        //        return courseManager.ProctorLoginRequirementCriteriaMeets(learningSessionGuid);
        //    }
        //}

        [WebMethod]
        #region DocuSign LCMS-11217
        public DocuSignLearner GetLearnerData(int CourseID, string LearnerSessionID, int LearnerID, int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetLearnerData(CourseID, LearnerSessionID, LearnerID, EnrollmentID);
            }
        }

        [WebMethod]
        public int SaveEnvelopeId(int EnrollmentID, string EnvelopeId)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveEnvelopeId(EnrollmentID, EnvelopeId);
            }
        }

       
        [WebMethod]
        public string GetEnvelopeId(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetEnvelopeId(EnrollmentID);
            }
        }

        [WebMethod]
        public int SaveStatusAfterDocuSignProcessComplete(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveStatusAfterDocuSignProcessComplete(EnrollmentID);
            }
        }
        [WebMethod]
        public int SaveStatusAfterDocuSignProcessCompleted(string EnvelopeId)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveStatusAfterDocuSignProcessComplete(EnvelopeId);
            }
        }
        [WebMethod]
        public bool GetDocuSignedAffidavitStatus(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetDocuSignedAffidavitStatus(EnrollmentID);
            }
        }
        [WebMethod]
        public string GetCourseStatusByEnrollmentId(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseStatusByEnrollmentId(EnrollmentID);
            }
        }
        [WebMethod]
        public int SaveAffidavitAcknowledgmentStatus(bool courseApprovalAffidavitStatus, int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveCourseApprovalAffidavitStatus(courseApprovalAffidavitStatus, EnrollmentID);
            }
        }
        [WebMethod]
        public bool GetAffidavitAcknowledgmentStatus(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetAffidavitAcknowledgmentStatus(EnrollmentID);
            }
        }

        [WebMethod]
        public CourseInfo GetCourseInformation( int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseInformation( EnrollmentID);
            }
        }
        #endregion

        //Suggested Course Panel LCMS-11878
        [WebMethod]
        public List<BusinessEntities.SuggestedCourse> GetCourseNameAgainstCourseGuids(List<string> courseGuids)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseNameAgainstCourseGuids(courseGuids);
            }
        }


        [WebMethod]
        public string GetCourseStoreId(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseStoreId(courseID);
            }
        }
        //LCMS-11974 DocuSign Decline
        //Abdus Samad 
        //Start
        [WebMethod]
        public int SaveStatusAfterDocuSignDecline(int EnrollmentID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SaveStatusAfterDocuSignDecline(EnrollmentID);
            }
        }
        //Stop

        [WebMethod]
        public int GetEnrollmentIdAgainstEnvelopeId(string EnvelopeId)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetEnrollmentIdAgainstEnvelopeId(EnvelopeId);
            }
        }


        [WebMethod]
        public bool SelectCourseApprovalMessage(string CourseID, string LearnerID, string CourseApprovalID, string LearnerSessionGUID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.SelectCourseApprovalMessage(CourseID, LearnerID, CourseApprovalID, LearnerSessionGUID);
            }
        }

        [WebMethod]
        public bool GetMultipleQuizConfigurationCount(int courseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetMultipleQuizConfigurationCount(courseID);
            }
        }

        [WebMethod]
        public string GetCourseGroupsByCourse(int CourseID)
        {
            using (CourseManager courseManager = new CourseManager())
            {
                return courseManager.GetCourseGroupsByCourse(CourseID);
            }
        }
        
    }
}
