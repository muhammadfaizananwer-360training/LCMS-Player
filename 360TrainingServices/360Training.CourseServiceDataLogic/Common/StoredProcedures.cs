using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.CourseServiceDataLogic.Common
{
    public class StoredProcedures
    {
       
            #region Course Constants
            public static readonly string SELECT_COURSE_CONTENTOBJECTS = "ICP_SELECT_COURSE_CONTENTOBJECT";
            public static readonly string SELECT_COURSE_SEQUANCE = "ICP_SELECT_COURSE_SEQUANCE";
            public static readonly string SELECT_COURSE_CONTENTOBJECTINOUTLINE = "ICP_SELECT_COURSE_CONTENTOBJECTINOUTLINE";
            public static readonly string SELECT_COURSE_NAME= "ICP_SELECT_COURSE_NAME";
            public static readonly string SELECT_COURSE_GUID = "ICP_SELECT_COURSE_GUID";
            public static readonly string SELECT_COURSETYPE_BY_GUID = "ICP_SELECT_COURSETYPE_BY_GUID";
            public static readonly string SELECT_COURSE_DEMOABLECONTENTOBJECTS = "ICP_SELECT_COURSE_DEMOABLECONTENTOBJECT";
            public static readonly string SELECT_LEARNER_VALIDATIONQUESTIONS = "ICP_SELECT_LEARNER_VALIDATIONQUESTIONS";
            public static readonly string SELECT_LEARNER_VALIDATIONQUESTIONOPTIONS = "ICP_SELECT_LEARNER_VALIDATIONQUESTIONOPTIONS";
            public static readonly string SELECT_COURSE_ID = "ICP_SELECT_COURSE_ID";
            public static readonly string SELECT_COURSE_ROOT_CONTENTOBJECT = "ICP_SELECT_COURSE_ROOT_CONTENTOBJECT";        
            public static readonly string GET_VSC_INFO = "GET_VSC_INFO";
            public static readonly string SELECT_ORIGINAL_COURSE_ID = "ICP_SELECT_ORIGINAL_COURSE_ID";
            public static readonly string SELECT_COURSE_IMAGE_ASSET = "ICP_GET_COURSE_IMAGE_ASSET";

           
            #endregion

            #region CourseConfiguration Constants
            public static readonly string GET_COURSECONFIGURATION = "ICP_SELECT_COURSECONFIGURATION";
            public static readonly string GET_COURSECONFIGURATION_ID = "ICP_SELECT_COURSECONFIGURATIONID";    
            public static readonly string GET_COURSECONFIGURATIONTEMPLATE = "ICP_SELECT_COURSECONFIGURATIONTEMPLATE";
            public static readonly string GET_COURSECONFIGURATIONTEMPLATE_ID_BY_COURSEAPPROVAL = "ICP_SELECT_COURSECONFIGURATIONTEMPLATE_ID_BY_COURSEAPPROVAL";            
            public static readonly string SELECT_PRACTICEEXAM_ASSESSMENTCONFIGURATION = "ICP_SELECT_PRACTICEEXAM_ASSESSMENTCONFIGURATION";
            public static readonly string GET_COURSEAPPROVAL_COURSECONFIGURATION = "ICP_SELECT_COURSEAPPROVAL_COURSECONFIGURATION";
            public static readonly string GET_COURSELASTPUBLISHEDDATE = "ICP_SELECT_COURSELASTPUBLISHEDDATE";
            public static readonly string GET_COURSEAPPROVALAFFIDAVIT = "ICP_SELECT_COURSEAPPROVALAFFIDAVIT";
            public static readonly string GET_COURSEAPPROVAL_CERTIFICATE = "ICP_SELECT_COURSE_APPROVAL_CERTIFICATE";

            public static readonly string SELECT_QUIZEXAM_ASSESSMENTCONFIGURATION = "ICP_SELECT_QUIZEXAM_ASSESSMENTCONFIGURATION";
            #endregion

            #region ContentObject Constants
            public static readonly string SELECT_CONTENTOBJECT_SCENES = "ICP_SELECT_CONTENTOBJECT_SCENE";
            public static readonly string SELECT_CONTENTOBJECT_DEMOABLESCENES = "ICP_SELECT_CONTENTOBJECT_DEMOABLESCENE";
            public static readonly string SELECT_CONTENTOBJECT_NAME = "ICP_SELECT_CONTENTOBJECT_NAME";
            public static readonly string SELECT_CONTENTOBJECT_NAME_BY_GUID = "ICP_SELECT_CONTENTOBJECT_NAME_BY_GUID";
            public static readonly string SELECT_CONTENTOBJECT_NAME_BY_EXAM_GUID = "ICP_SELECT_CONTENTOBJECT_NAME_BY_EXAM_GUID";
            #endregion

            #region Scene Constants
            public static readonly string SELECT_SCENE_ASSETS = "ICP_SELECT_SCENE_ASSET";
            public static readonly string SELECT_SCENETEMPLATE_HTMLURL = "ICP_SELECT_SCENETEMPLATE_HTMLURL";
            public static readonly string SELECT_SCENETEMPLATECATEGORY_SCENETEMPLATE = "ICP_SELECT_SCENETEMPLATECATEGORY_SCENETEMPLATE";
            public static readonly string SELECT_SCENETEMPLATEVARIANT_HTMLURL = "ICP_SELECT_SCENETEMPLATEVARIANT_HTMLURL";
            public static readonly string SELECT_SCENETEMPLATEVARIANT = "ICP_SELECT_SCENETEMPLATEVARIANT";
            public static readonly string SELECT_SCENETEMPLATETYPE_HTMLURL= "ICP_SELECT_SCENETEMPLATETYPE_HTMLURL";
            public static readonly string SELECT_SCENE_GUID = "ICP_SELECT_SCENE_GUID";
            public static readonly string SELECT_ASSET_GUID= "ICP_SELECT_ASSET_GUID";
            public static readonly string SELECT_COURSE_SCENES = "ICP_SELECT_COURSE_SCENES";
            public static readonly string SELECT_COURSE_DEMOABLESCENES = "ICP_SELECT_COURSE_DEMOABLESCENES";
            public static readonly string GET_ASSET = "GET_ASSET";
            #endregion

            #region GlossaryItemConstatnts
            public static readonly string SELECT_COURSE_GLOSSARYITEMS = "ICP_SELECT_COURSE_GLOSSARYITEMS";
            public static readonly string SELECT_COURSE_SCENE_GLOSSARYITEMS = "ICP_SELECT_COURSE_SCENE_GLOSSARYITEMS";
            public static readonly string SELECT_GLOSSARYITEM_DEFINITION = "ICP_SELECT_GLOSSARYITEM_DEFINITION";
            #endregion

            #region CourseMaterialConstants
            public static readonly string SELECT_COURSE_COURSEMATERIAL = "ICP_SELECT_COURSE_COURSEMATERIAL";
            #endregion

            #region Intro/End Page Constants
            public static readonly string SELECT_COURSE_ENDPAGE="ICP_SELECT_COURSE_ENDPAGE";
            public static readonly string SELECT_COURSE_INTROPAGE = "ICP_SELECT_COURSE_INTROPAGE";
            public static readonly string LMS_GET_COURSE_EOCINSTRUCTIONS = "LMS_GET_COURSE_EOCINSTRUCTIONS";
            public static readonly string LMS_GET_EOCINSTRUCTIONS = "LMS_GET_EOCINSTRUCTIONS";
            #endregion

        #region Validation Constants
            public static readonly string CREATE_VALIDATION_UNLOCK_REQUEST = "CREATE_VALIDATION_UNLOCK_REQUEST";

        #endregion

        #region Course Evaluation Constants
            public static readonly string SELECT_COURSE_SURVEY = "ICP_SELECT_COURSE_SURVEY";
            public static readonly string SELECT_SURVEY_QUESTIONS = "ICP_SELECT_SURVEY_QUESTIONS";
            public static readonly string SELECT_SURVEY_ANSWERS = "ICP_SELECT_SURVEY_ANSWERS";
            public static readonly string SELECT_COURSE_SURVEYQUESTIONS_COUNT = "ICP_SELECT_COURSE_SURVEYQUESTIONS_COUNT";
        #endregion 

            #region Course Approval
            public static readonly string SELECT_SELECT_COURSE_COURSEAPPROVAL = "ICP_SELECT_COURSE_COURSEAPPROVAL";
            public static readonly string CHECK_LEARNER_COURSE_COURSEAPPROVAL = "ICP_CHECK_LEARNER_COURSE_COURSEAPPROVAL";
            public static readonly string INSERT_ICP4_LEARNERCOURSEAPPROVAL = "INSERT_ICP4_LEARNERCOURSEAPPROVAL";
            public static readonly string INSERT_ICP4_LEARNERCOURSEMESSAGE = "INSERT_ICP4_LEARNERCOURSEMESSAGE";
            public static readonly string ICP_CHECK_LEARNER_COURSE_MESSAGE = "ICP_CHECK_LEARNER_COURSE_MESSAGE";
        
            #endregion

            #region Sub Content Owner
            public static readonly string SELECT_COURSE_ID_FROM_SUB_CONTENTOWNER = "ICP_SELECT_COURSE_ID_FROM_SUB_CONTENTOWNER";
            #endregion

            public static readonly string ICP_SELECT_CONTENTOBJECT_ASSESSMENTITEMS_COUNT = "ICP_SELECT_CONTENTOBJECT_ASSESSMENTITEMS_COUNT";
          
            //LCMS-10392
            public static readonly string ICP_GET_COURSE_KEYWORDS = "ICP_GET_COURSE_KEYWORDS";

            //LCMS-11878
            public static readonly string ICP_GET_COURSE_NAME_COURSEGUIDS = "ICP_GET_COURSE_NAME_COURSEGUIDS";
            public static readonly string ICP_GET_COURSE_STOREID = "ICP_GET_COURSE_STOREID";

        #region DocuSign LCMS-11217
            public static readonly string ICP_GET_DOCUSIGN_LEARNER_DATA = "ICP_GET_DOCUSIGN_LEARNER_DATA";
            public static readonly string ICP_SAVE_ENVELOPE_ID = "ICP_SAVE_ENVELOPE_ID";
            public static readonly string ICP_GET_ENVELOPE_ID = "ICP_GET_ENVELOPE_ID";
            public static readonly string ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETE = "ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETE";
            public static readonly string ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETED = "ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETED";
            public static readonly string ICP_GET_LEARNERCOURSESTATUS_BY_ENROLLMENT_ID = "ICP_GET_LEARNERCOURSESTATUS_BY_ENROLLMENT_ID";
            public static readonly string ICP_GET_CORUSE_STATUS_BY_LEARNERENROLLMENT_ID = "ICP_GET_CORUSE_STATUS_BY_LEARNERENROLLMENT_ID";
            public static readonly string ICP_SET_CORUSE_APPROVAL_AFFIDAVIT_STATUS_BY_LEARNERENROLLMENT_ID = "ICP_SET_CORUSE_APPROVAL_AFFIDAVIT_STATUS_BY_LEARNERENROLLMENT_ID";
            public static readonly string ICP_GET_AFFIDAVIT_ACKNOWLDGEMENT_BY_LEARNERENROLLMENT_ID = "ICP_GET_AFFIDAVIT_ACKNOWLDGEMENT_BY_LEARNERENROLLMENT_ID";
            public static readonly string ICP_GET_COURSEINFORMATION_BY_ENROLLMENT_ID = "ICP_GET_COURSEINFORMATION_BY_ENROLLMENT_ID";
        #endregion

        //LCMS-11974 DocuSign Decline
        //Abdus Samad 
        //Start
            public static readonly string ICP_SET_STATUS_AFTER_DOCUSIGN_DECLINE = "ICP_SET_STATUS_AFTER_DOCUSIGN_DECLINE";
            public static readonly string ICP_GET_ENROLLMENTID_AGAINST_ENVELOPID = "ICP_GET_ENROLLMENTID_AGAINST_ENVELOPID";
        //Stop

        //LCMS-12332
        //Abdus Samad
        //Start
            public static readonly string ICP_SELECT_COURSE_COURSEAPPROVALMESSAGE = "ICP_SELECT_COURSE_COURSEAPPROVALMESSAGE";
        //Stop


            //LCMS-13475
            //Abdus Samad
            //Start
            public static readonly string ICP_CHECK_COURSE_MULTIPLEQUIZCONFIGURATION = "ICP_CHECK_COURSE_MULTIPLEQUIZCONFIGURATION";
            //Stop
            public static readonly string ICP_SELECT_COURSEGROUPS_BY_COURSE = "SELECT_COURSEGROUPS_BY_COURSE";
        
    }
}
