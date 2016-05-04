using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using _360Training.BusinessEntities;
using _360Training.CourseServiceDataLogic.CourseDA;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling; 

namespace _360Training.CourseServiceBusinessLogic
{
    public class CourseManager:ICourseManager,IDisposable
    {
        #region ICourseManagerMembers
        
        #region Course Methods
        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        public string GetCourseName(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseName(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetCourseGUID(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseGUID(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetCourseID(string courseGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseID(courseGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        /// <summary>
        /// Gets the course type by course guid
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetCourseTypeByGUID(string courseGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseTypeByGUID(courseGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
      
        #endregion

        #region ContentObject
        public string GetContentObjectName(int contentObjectID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetContentObjectName(contentObjectID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string GetContentObjectName(string ContentObjectGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetContentObjectName(ContentObjectGUID); 
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string GetContentObjectNameByExamGUID(string ExamGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetContentObjectNameByExamGUID(ExamGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        #endregion

        #region Scene
        /// <summary>
        /// This mehod gets the scene template HTML
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        public SceneTemplate GetSceneTemplateHTML(int sceneTemplateID)
        {
            SceneTemplate sceneTemplate = new SceneTemplate();

            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    string sceneTemplateHTMLURL=courseDA.GetSceneTemplateHtmlURL(sceneTemplateID);                    
                    if (sceneTemplateHTMLURL != string.Empty)
                    {
                        
                        using (TextReader textReader = new StreamReader(sceneTemplateHTMLURL))
                        {
                            sceneTemplate.TemplateHTML = textReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        sceneTemplate.TemplateHTML = string.Empty;
                    }
                }
                return sceneTemplate;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets the scene template HTML variant according to the parameters
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        public SceneTemplate GetSceneTemplateWithHTMLVariant(int sceneTemplateID, bool isText, bool isVOText, bool isVisualTop,bool isHeading)
        {
            SceneTemplate sceneTemplate = new SceneTemplate();

            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    sceneTemplate = courseDA.GetSceneTemplateWithHTMLVariant(sceneTemplateID,isText,isVOText,isVisualTop,isHeading);                    
                    if (sceneTemplate.TemplateHTMLURL!= string.Empty)
                    {
                        
                        if (sceneTemplate.SceneTemplateType == "MC")
                        {
                            sceneTemplate.TemplateHTMLURL = sceneTemplate.TemplateHTMLURL + ConfigurationSettings.AppSettings["MCTemplateFileForICP"];                            
                           // sceneTemplate.TemplateHTMLURL = "../Custom_Template/MC_Template/template.html";
                        }
                                                
                        using (TextReader textReader = new StreamReader(sceneTemplate.TemplateHTMLURL))
                        {
                            sceneTemplate.TemplateHTML = textReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        sceneTemplate.TemplateHTML = string.Empty;
                    }
                    sceneTemplate.TemplateHTMLURL = string.Empty;
                }
                return sceneTemplate;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetSceneGUID(int sceneID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetSceneGUID(sceneID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

		/// <summary>
        /// This method gets Assets of scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>Asset List</returns>
        public List<Asset> GetSceneAssets(int sceneID)
        {
            try
            {
                List<Asset> assetList = new List<Asset>();
                using (CourseDA courseDA = new CourseDA())
                {
                    assetList=  courseDA.GetSceneAssets(sceneID);
                    Asset asset = new Asset();
                    asset.AssetType = AssetType.Text;
                    asset.AssetSceneOrientation = "$Heading";
                    assetList.Add(asset);
                    return assetList;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion
        
        #region Asset Methods


        public Asset GetAsset(int assetID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetAsset(assetID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public Asset GetAffidavitAsset(int affidativeID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetAffidavitAsset(affidativeID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetAssetGUID(int assetID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetAssetGUID(assetID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        #endregion
        
        #region Sequence Methods
        /// <summary>
        /// This method gets the sequence object based on course configuration
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration Object</param>
        /// <returns>Sequence object</returns>
        public Sequence GetSequence(int courseID, CourseConfiguration courseConfiguration)
        {
            Sequence sequence = new Sequence();
            FillSequence(sequence, courseID, courseConfiguration);
            return sequence;
        }
        /// <summary>
        /// This method fills the sequence object for a particular course 
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        private void FillSequence(Sequence sequence, int courseID, CourseConfiguration courseConfiguration)
        {
            using (CourseDA courseDA = new CourseDA())
            {
                /*
                int affidavit = 0;
                if (courseApprovalID > 0)
                {
                    affidavit = courseDA.GetCourseApprovalAffidavit(courseID, courseApprovalID);
                }
                */
                sequence.LastPublishedDateTime = courseDA.GetCourseLastPublishedDate(courseID);

                List<SceneTemplate> defaultSceneTemplates = new List<SceneTemplate>();
                defaultSceneTemplates = courseDA.GetCategorySceneTemplates(SceneTemplateType.DEFAULT);

                if (courseConfiguration.PlayerEnableIntroPage == true
                    && courseConfiguration.PlayerEnableContent == true)
                {
                    CourseIntroPage courseIntroPage = new CourseIntroPage();
                    courseIntroPage = GetCourseIntroPage(courseID);
                    FillIntroPage(sequence, courseIntroPage);
                }
                {
                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem.SequenceItemType = SequenceItemType.CourseIntroduction;
                    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CI);
                    sequence.SequenceItems.Add(sequenceItem);
                }
                //if(courseConfiguration.SeatTimeEnabled==true && courseConfiguration.SeatTimeInHour > 0)  
                //{
                //    SequenceItem sequenceItem = new SequenceItem();
                //    sequenceItem.SequenceItemType = SequenceItemType.MaximumSeatTimeCourseLaunch; 
                //    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.MST);
                //    sequence.SequenceItems.Add(sequenceItem);
                //}

                //For LCMS-11217
                List<SceneTemplate> afdtSceneTemplates = new List<SceneTemplate>();
                afdtSceneTemplates = courseDA.GetCategorySceneTemplates(SceneTemplateType.AFDT);
                //End

                if (courseConfiguration.PlayerEnableOrientaionScenes == true)
                {
                    List<SceneTemplate> orientationSceneTemplates = new List<SceneTemplate>();
                    orientationSceneTemplates = courseDA.GetCategorySceneTemplates(SceneTemplateType.ORIENTATION);

                    SequenceItem sequenceItem = null;
                    {
                        sequenceItem = new SequenceItem();
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(orientationSceneTemplates, SceneTemplateType.DC);
                        sequenceItem.SequenceItemType = SequenceItemType.DisclaimerScene;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                    
                    {
                        sequenceItem = new SequenceItem();
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(orientationSceneTemplates, SceneTemplateType.CR);
                        sequenceItem.SequenceItemType = SequenceItemType.CopyrightScene;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                    {
                        sequenceItem = new SequenceItem();
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(orientationSceneTemplates, SceneTemplateType.MS);
                        sequenceItem.SequenceItemType = SequenceItemType.MissionStatementScene;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                    {
                        sequenceItem = new SequenceItem();
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(orientationSceneTemplates, SceneTemplateType.CS);
                        sequenceItem.SequenceItemType = SequenceItemType.CustomerSupportScene;
                        sequence.SequenceItems.Add(sequenceItem);
                    }

                }

                {
                    //Course Loading Screen is commented
                    //SequenceItem sequenceItem = new SequenceItem();
                    //sequenceItem.SequenceItemType = SequenceItemType.CourseLoading;
                    //sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CL);
                    //sequence.SequenceItems.Add(sequenceItem);
                }

                //if (courseConfiguration.PreAssessmentConfiguration.Enabled == true)
                //{
                //    SequenceItem sequenceItem = new SequenceItem();
                //    sequenceItem.SequenceItemType = SequenceItemType.PreAssessment;
                //    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.PRA);
                //    sequence.SequenceItems.Add(sequenceItem);
                //}
                //List<ContentObject> contentObjectSequenceItems = new List<ContentObject>();
                ////List<Scene> sceneSequenceItems = new List<Scene>();
                //contentObjectSequenceItems = courseDA.GetCourseContentObjects(courseID);

                //if (courseConfiguration.PlayerEnableContent == true)
                //{
                //    sceneSequenceItems = courseDA.GetCourseScenes(courseID);
                //}

                //if (contentObjectSequenceItems.Count > 0)
                //{
                //    int sceneTemplateLessonAssessmentID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.LA);
                //    //int sceneTemplateLessonIntroductionID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.LIN);
                //    FillContentObjectAndSceneSequenceItems(contentObjectSequenceItems, sceneSequenceItems, sequence);
                //    if (courseConfiguration.QuizConfiguration.Enabled == true)
                //    {
                //        FillQuizSequenceItems(sequence, contentObjectSequenceItems, sceneTemplateLessonAssessmentID);
                //    }
                //}

                List<SequenceItem> coursesequance = new List<SequenceItem>();
                coursesequance = courseDA.GetCourseSequance(courseID);
                List<ContentObject> contentObjectSequenceItems = new List<ContentObject>();
                if (IsExistsExamPrePostOnSequance(coursesequance, SequenceItemType.PreAssessment) == false)
                {
                    if (courseConfiguration.PreAssessmentConfiguration.Enabled == true)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.PreAssessment;
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.PRA);
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                    //List<Scene> sceneSequenceItems = new List<Scene>();                    
                }

                sequence.SequenceItems.AddRange(coursesequance);
                contentObjectSequenceItems = courseDA.GetCourseContentObjects(courseID);

                if (coursesequance.Count > 0)
                {
                    int sceneTemplateLessonAssessmentID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.LA);
                    if (courseConfiguration.QuizConfiguration.Enabled == true)
                    {
                        FillQuizSequenceItems(sequence, contentObjectSequenceItems, sceneTemplateLessonAssessmentID);
                    }
                }



                // LCMS-8796
                //----------------------------------------------------------------------
                if (courseConfiguration.SpecialQuestionnaire == true)
                {
                    if (courseConfiguration.DisplaySpecialQuestionnaire == DisplayCourseEvaluation.BeforePostAssessment || courseConfiguration.DisplaySpecialQuestionnaire == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.SpecialQuestionnaire;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }
                //----------------------------------------------------------------------


                //LMS-11877
                if (courseConfiguration.AllowCourseRating)
                {
                    if (courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforePostAssessment || courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.CourseRatingScene;
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CLR);
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }

                if (courseConfiguration.PlayerCourseEvaluation == true)
                {
                    if (courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforePostAssessment || courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.CourseEvaluation;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }
                

                //If Embedded ACK is enabled then add it in the sequence item
                if (courseConfiguration.EmbeddedAcknowledgmentEnabled)
                {
                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem.SequenceItemType = SequenceItemType.EmbeddedAcknowledgmentScene;
                    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.ACK);
                    sequence.SequenceItems.Add(sequenceItem);
                }

                

                if (IsExistsExamPrePostOnSequance(sequence.SequenceItems, SequenceItemType.PostAssessment) == false)
                {
                    if (courseConfiguration.PostAssessmentConfiguration.Enabled == true)
                    {
                        // LCMS-9455
                        //------------------------------------------------
                        //if (courseConfiguration.IsProctorLoginEnabled) // If Proctor Login is turned ON
                        //{
                        //    SequenceItem proctorLoginSequenceItem = new SequenceItem();
                        //    proctorLoginSequenceItem.SequenceItemType = SequenceItemType.ProctorLoginScreen;
                        //    sequence.SequenceItems.Add(proctorLoginSequenceItem);

                        //}
                        //------------------------------------------------

                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.PostAssessment;
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.POA);
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }



                // LCMS-8796
                //----------------------------------------------------------------------
                if (courseConfiguration.SpecialQuestionnaire == true)
                {
                    if (courseConfiguration.DisplaySpecialQuestionnaire == DisplayCourseEvaluation.AfterPostAssessment || courseConfiguration.DisplaySpecialQuestionnaire == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.SpecialQuestionnaire;
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }
                //----------------------------------------------------------------------


                //LMS-11877
                if (courseConfiguration.AllowCourseRating)
                {
                    if (courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.AfterPostAssessment || courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.CourseRatingScene;
                        sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CLR);
                        sequence.SequenceItems.Add(sequenceItem);
                    }
                }


                if (courseConfiguration.PlayerCourseEvaluation == true)
                {
                    if (courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.AfterPostAssessment || courseConfiguration.PlayerDisplayCourseEvaluation == DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                    {
                        SequenceItem sequenceItem = new SequenceItem();
                        sequenceItem.SequenceItemType = SequenceItemType.CourseEvaluation;
                        sequence.SequenceItems.Add(sequenceItem);

                       
                    }
                }                         

                if (courseConfiguration.PlayerEnableEndOfCourseScene == true
                    && courseConfiguration.PlayerEnableContent == true)
                {
                    FillEOCInstructionsPage(sequence);

                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem.SequenceItemType = SequenceItemType.CourseCertificateScene;
                    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CERT);
                    sequence.SequenceItems.Add(sequenceItem);

                    //if (courseConfiguration.CertificateEnabled == true
                    //    && courseConfiguration.CertificateAssetID > 0)
                    //{
                    //    SequenceItem sequenceItem = new SequenceItem();
                    //    sequenceItem.SequenceItemType = SequenceItemType.CourseCertificateScene;
                    //    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CERT);
                    //    sequence.SequenceItems.Add(sequenceItem);
                    //}

                    //CourseEndPage courseEndPage = new CourseEndPage();
                    //courseEndPage = GetCourseEndPage(courseID);
                    //FillEndPage(sequence, courseEndPage);
                }

                else //if (courseConfiguration.CertificateEnabled == true && courseConfiguration.CertificateAssetID > 0)
                {
                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem.SequenceItemType = SequenceItemType.CourseCertificateScene;
                    sequenceItem.SceneTemplateID = GetSceneTemplateIDByType(defaultSceneTemplates, SceneTemplateType.CERT);
                    sequence.SequenceItems.Add(sequenceItem);
                }

                CourseEndPage courseEndPage = new CourseEndPage();
                courseEndPage = GetCourseEndPage(courseID);
                FillEndPage(sequence, courseEndPage);

            }
                        
        }

        private bool IsExistsExamPrePostOnSequance(List<SequenceItem> sequanceItems, string AssessmentType) 
        {
            foreach (SequenceItem sequanceitem in sequanceItems)
            {
                if (sequanceitem.ExamType!=null)
                {
                
                    if (sequanceitem.ExamType.Equals(AssessmentType))                
                    {                    
                        return true;
                    }                    
                }
            }
            return false;
        }
        /// <summary>
        /// This method parses the list and returns the sceneTemplateID of the firs occurence of a specific type in the list 
        /// </summary>
        /// <param name="sceneTemplates"></param>
        /// <returns></returns>
        private int GetSceneTemplateIDByType(List<SceneTemplate> sceneTemplates,string type)
        {            
            foreach (SceneTemplate sceneTemplate in sceneTemplates)
            {
                if (sceneTemplate.SceneTemplateType == type)
                    return sceneTemplate.SceneTemplateID;
            }
            return 0;

        }
        private void FillIntroPage(Sequence sequence, CourseIntroPage courseIntroPage)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemType = SequenceItemType.IntroPage;
            Asset asset =new Asset();
            asset.URL=courseIntroPage.Url;
            sequenceItem.Assets.Add(asset);
            sequence.SequenceItems.Add(sequenceItem);
        }
        private void FillEndPage(Sequence sequence, CourseEndPage courseEndPage)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemType = SequenceItemType.EndOfCourseScene;
            Asset asset = new Asset();
            asset.URL = courseEndPage.Url;
            sequenceItem.Assets.Add(asset);
            sequence.SequenceItems.Add(sequenceItem);
        }
        private void FillEOCInstructionsPage(Sequence sequence)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemType = SequenceItemType.EOCInstructions;
            sequence.SequenceItems.Add(sequenceItem);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        private void FillCertificatePage(Sequence sequence)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemType = SequenceItemType.CourseCertificateScene;
            sequence.SequenceItems.Add(sequenceItem);

        }

        /// <summary>
        /// This method fills the sequence items for contentobject in sequence list
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="contentObjectSequenceItems">Contentobject list</param>
        /// <param name="count">int count(index of current contentobejctsequenceitem in scope)</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        private void FillContentObjectSequenceItems(Sequence sequence, List<ContentObject> contentObjectSequenceItems, int count, CourseConfiguration courseConfiguration,int sceneTemplateLessonAssessmentID,int sceneTemplateLessonIntroductionID)
        {
            if (count == contentObjectSequenceItems.Count) //Return condition
                return;
            //To insert contentobjects after their parents we need the parentcontentobject index
            int parentIndex = GetParentContentObjectIndex(sequence,contentObjectSequenceItems[count].ParentContentObjectID);
            
            //To insert contentobjects which are at same level,with the same order as they
            //are retreived from the database,we need to find the last child of the 
            //parent content object so that we can insert new contentobject after its
            //already added sibling
            int lastChildIndex;
            if (parentIndex == -1)//case when there is no parent
            {
                //set last child index as the last available item of the sequence
                lastChildIndex = sequence.SequenceItems.Count - 1;
            }
            else
            {
                lastChildIndex = GetLastChildIndex(sequence, parentIndex);
            }
            int insertIndex = lastChildIndex + 1;
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem=TranslateContentObjectSequenceItem(contentObjectSequenceItems[count]);
            sequence.SequenceItems.Insert(insertIndex, sequenceItem);
            //To insert the quiz only if the following policy states true and contentobject
            //allows it.
            if (courseConfiguration.QuizConfiguration.Enabled == true &&
                            contentObjectSequenceItems[count].AlowQuizTF == true)
            {
                insertIndex++;
                SequenceItem quizSequenceItem = new SequenceItem();
                quizSequenceItem.SequenceItemType = SequenceItemType.Quiz;
                quizSequenceItem.ParentID = contentObjectSequenceItems[count].ContentObjectID;
                quizSequenceItem.Item_GUID = contentObjectSequenceItems[count].ContentObject_GUID;
                quizSequenceItem.SceneTemplateID = sceneTemplateLessonAssessmentID;
                sequence.SequenceItems.Insert(insertIndex, quizSequenceItem);
                insertIndex--;///so tht scenes and assets get inserted before quiz
            }
            insertIndex++;
            //To get the scenes and assets only if the following policy states true
            if (courseConfiguration.PlayerEnableContent == true)
            {
                List<Scene> sceneSequenceItems = new List<Scene>();
                using (CourseDA courseDA = new CourseDA())
                {
                    sceneSequenceItems = courseDA.GetContentObjectScenes(contentObjectSequenceItems[count].ContentObjectID);
                    //if (sceneSequenceItems.Count > 0)
                    //{
                        //SequenceItem sceneIntroductionSequenceItem = new SequenceItem();
                        //sceneIntroductionSequenceItem.SequenceItemType = SequenceItemType.LessonIntroductionScene;
                        //sceneIntroductionSequenceItem.SceneTemplateID = sceneTemplateLessonIntroductionID;
                        //sceneIntroductionSequenceItem.ParentID=contentObjectSequenceItems[count].ContentObjectID;
                        //sceneIntroductionSequenceItem.Item_GUID = contentObjectSequenceItems[count].ContentObject_GUID;
                        //sceneIntroductionSequenceItem.SceneGUID = contentObjectSequenceItems[count].ContentObject_GUID;
                        //sceneIntroductionSequenceItem.ContentObjectName = contentObjectSequenceItems[count].Name;
                        //sequence.SequenceItems.Insert(insertIndex, sceneIntroductionSequenceItem);
                        //insertIndex++;
                        if (sceneSequenceItems.Count > 0)
                        {
                            FillSceneSequenceItems(sequence, sceneSequenceItems, 0, contentObjectSequenceItems[count].ContentObjectID, contentObjectSequenceItems[count].Name, insertIndex);
                        }
                    //}
                }
            }
                FillContentObjectSequenceItems(sequence, contentObjectSequenceItems, count+1,courseConfiguration,sceneTemplateLessonAssessmentID,sceneTemplateLessonIntroductionID);
            

        }
        /// <summary>
        /// This method fills the course structure in sequence 
        /// </summary>
        /// <param name="contentObjectSequenceItems">list object ContentObject</param>
        /// <param name="sceneSequenceItems">list object Scene</param>
        /// <param name="sequence">object sequence</param>
        private void FillContentObjectAndSceneSequenceItems(List<ContentObject> contentObjectSequenceItems, List<Scene> sceneSequenceItems, Sequence sequence)
        {
            int COindex = 0;
            int sceneIndex = 0;

            //Merging of two lists
            while (COindex < contentObjectSequenceItems.Count && sceneIndex < sceneSequenceItems.Count)
            {
                if (contentObjectSequenceItems[COindex].DisplayOrder == sceneSequenceItems[sceneIndex].DisplayOrder)
                {
                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem = TranslateContentObjectSequenceItem(contentObjectSequenceItems[COindex]);
                    sequence.SequenceItems.Add(sequenceItem);
                    COindex++;
                    sceneIndex++;
                }
                else if (contentObjectSequenceItems[COindex].DisplayOrder < sceneSequenceItems[sceneIndex].DisplayOrder)
                {
                    SequenceItem sequenceItem = new SequenceItem();
                    sequenceItem = TranslateContentObjectSequenceItem(contentObjectSequenceItems[COindex]);
                    sequence.SequenceItems.Add(sequenceItem);
                    COindex++;
                }
                else
                {
                    //we do not insert scenes in sequence instead we insert assets
                    List<Asset> assets = new List<Asset>();
                    using (CourseDA courseDA = new CourseDA())
                    {
                        //assets = courseDA.GetSceneAssets(sceneSequenceItems[sceneIndex].SceneID);
                        //if (assets.Count > 0)
                            FillAssetSequenceItems(sequence, assets, 0, sceneSequenceItems[sceneIndex].ContenObjectID, sceneSequenceItems[sceneIndex].ContentObjectName, sceneSequenceItems[sceneIndex], sequence.SequenceItems.Count, true);
                    }
                    sceneIndex++;
                }
            }
            while (COindex < contentObjectSequenceItems.Count)
            {
                SequenceItem sequenceItem = new SequenceItem();
                sequenceItem = TranslateContentObjectSequenceItem(contentObjectSequenceItems[COindex]);
                sequence.SequenceItems.Add(sequenceItem);

                COindex++;
            }
            while (sceneIndex < sceneSequenceItems.Count)
            {
                //we do not insert scenes in sequence instead we insert assets
                List<Asset> assets = new List<Asset>();
                using (CourseDA courseDA = new CourseDA())
                {
                    //assets = courseDA.GetSceneAssets(sceneSequenceItems[sceneIndex].SceneID);
                    //if (assets.Count > 0)
                        FillAssetSequenceItems(sequence, assets, 0, sceneSequenceItems[sceneIndex].ContenObjectID, sceneSequenceItems[sceneIndex].ContentObjectName, sceneSequenceItems[sceneIndex], sequence.SequenceItems.Count, true);
                }
                sceneIndex++;
            }

        }

        private bool HasContentObjectValidQuiz(int ContentObjectId)
        {
            using (CourseDA courseDA = new CourseDA())
            {
                return courseDA.GetQuizAssessmentItems(ContentObjectId);
            }
        }

        /// <summary>
        /// This method fills the quizes in sequence
        /// </summary>
        /// <param name="sequence">object sequence</param>
        /// <param name="contentObjects">list object contentobject</param>
        /// <param name="sceneTemplateLessonAssessmentID">int sceneTemplateID</param>
        private void FillQuizSequenceItems(Sequence sequence, List<ContentObject> contentObjects, int sceneTemplateLessonAssessmentID)
        {
            foreach (ContentObject contentObject in contentObjects)
            {
                if (contentObject.AlowQuizTF == true)
                {
                    for (int index = 0; index <= sequence.SequenceItems.Count - 1; index++)
                    {
                        if (sequence.SequenceItems[index].SequenceItemType == SequenceItemType.ContentObject
                            && sequence.SequenceItems[index].SequenceItemID == contentObject.ContentObjectID)
                        {
                            int lastChildContentObjectIndex = FindLastChildContentObjectIndexInSequence(sequence, index);
                            //LCMS-2648
                            //Need to update this logic with a back track impelementation to Get the Last Child index of the current CO
                            //int lastChildIndex = FindLastChildIndexInSequence(sequence, lastChildContentObjectIndex);
                            int lastChildIndex = GetLastChildIndexInSequenceWithBackTrack(sequence, lastChildContentObjectIndex,index);
                            //LCMS-2648 END
                            int parentLastchildIndex = FindLastChildIndexInSequence(sequence, index);
                            SequenceItem quizSequenceItem = new SequenceItem();
                            quizSequenceItem.SequenceItemType = SequenceItemType.Quiz;
                            quizSequenceItem.ParentID = contentObject.ContentObjectID;
                            quizSequenceItem.Item_GUID = contentObject.ContentObject_GUID;
                            quizSequenceItem.SceneTemplateID = sceneTemplateLessonAssessmentID;
                            quizSequenceItem.IsValidQuiz = HasContentObjectValidQuiz(contentObject.ContentObjectID);
                            sequence.SequenceItems.Insert(lastChildIndex > parentLastchildIndex ? lastChildIndex + 1 : parentLastchildIndex + 1, quizSequenceItem);
                            break;
                        }
                    }
                }

            }
        }
        private int FindLastChildContentObjectIndexInSequence(Sequence sequence, int startIndex)
        {
            int lastChildIndex = -1;

            for (int index = startIndex; index < sequence.SequenceItems.Count; index++)
            {
                if (sequence.SequenceItems[index].SequenceItemType == SequenceItemType.ContentObject
                    && sequence.SequenceItems[index].ParentID == sequence.SequenceItems[startIndex].SequenceItemID)
                {
                    lastChildIndex = index;
                }
            }
            if (lastChildIndex != -1)
                return FindLastChildContentObjectIndexInSequence(sequence, lastChildIndex);
            else
                return startIndex;
        }
        private int FindLastChildIndexInSequence(Sequence sequence, int startIndex)
        {
            int lastChildIndex = -1;
            for (int index = startIndex; index <= sequence.SequenceItems.Count - 1; index++)
            {
                if (sequence.SequenceItems[index].ParentID == sequence.SequenceItems[startIndex].SequenceItemID)
                    lastChildIndex = index;
            }
            if (lastChildIndex != -1)
            {
                return lastChildIndex;
            }
            else
            {
                return startIndex;
            }
        }
        /// <summary>
        /// Gets the Last Child available in the current index hierarchy
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="lastContentObjectIndex"></param>
        /// <param name="currentContentObjectIndex"></param>
        /// <returns></returns>
        private int GetLastChildIndexInSequenceWithBackTrack(Sequence sequence, int lastContentObjectIndex,int currentContentObjectIndex)
        {
            int lastChildIndex = lastContentObjectIndex;
            for (int index = lastContentObjectIndex; index >= currentContentObjectIndex; index--)
            {
                int lastFoundChildIndexOfCurrentCO = FindLastChildIndexInSequence(sequence, index);
                if (lastFoundChildIndexOfCurrentCO > lastChildIndex)
                {
                    lastChildIndex = lastFoundChildIndexOfCurrentCO;
                }
            }
            return lastChildIndex;
        }
        /// <summary>
        /// This method gets the index of the parent contentobject 
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="contentObjectSequenceItem">ContentObjectSequenceItem object</param>
        /// <returns>int,-1 if parent not found else the index of parent</returns>
        private int GetParentContentObjectIndex(Sequence sequence, int parentContentObjectID)
        {
            for (int index = 0; index < sequence.SequenceItems.Count; index++)
            {
                if ((parentContentObjectID == sequence.SequenceItems[index].SequenceItemID)
                    && (sequence.SequenceItems[index].SequenceItemType == SequenceItemType.ContentObject))
                {
                    return index;
                }
            }
            return -1;
        }
        /// <summary>
        /// This method gets the last child index of the parent contentobjectsequenceitem
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="parentIndex">int parentIndex</param>
        /// <returns>int,returns the parent index if no child found else returns child index</returns>
        private int GetLastChildIndex(Sequence sequence, int parentIndex)
        {
            int lastChildIndexOnsameLevel = GetLastChildIndexOnSameLevel(sequence, parentIndex);
            return lastChildIndexOnsameLevel;
        }
        private int GetLastChildIndexOnSameLevel(Sequence sequence, int parentIndex)
        {
            int lastChildIndex = parentIndex;
            for (int index = 0; index < sequence.SequenceItems.Count; index++)
            {
                if (sequence.SequenceItems[index].ParentID == sequence.SequenceItems[parentIndex].SequenceItemID
                    && sequence.SequenceItems[index].SequenceItemType != SequenceItemType.Quiz)
                {
                    lastChildIndex = index;//sequence.SequenceItems[parentIndex].SequenceItemID;
                    ///This for loop is for the assets and quiz contained in that last content object
                    for (int assetIndex = index; assetIndex < sequence.SequenceItems.Count; assetIndex++)
                    {
                        if (sequence.SequenceItems[assetIndex].ParentID == sequence.SequenceItems[index].SequenceItemID)
                            lastChildIndex = assetIndex;
                    }
                }
            }
            return lastChildIndex;
        }
        
        /// <summary>
        /// This method iterates through the SceneSequenceItems 
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="sceneSequenceItems">scene list object</param>
        /// <param name="count">int count,index of the current sceneSequenceItem</param>
        /// <param name="contentObjectID">int contentobjectID</param>
        /// <param name="insertIndex">int insertIndex</param>
        private void FillSceneSequenceItems(Sequence sequence,List<Scene> sceneSequenceItems,int count,int contentObjectID,string contentObjectName,int insertIndex)
        {
            if (count == sceneSequenceItems.Count)
                return;
            sceneSequenceItems[count].ContenObjectID = contentObjectID;
            List<Asset> assetSequenceItems = new List<Asset>();
            using (CourseDA courseDA = new CourseDA())
            {
                assetSequenceItems = courseDA.GetSceneAssets(sceneSequenceItems[count].SceneID);
                FillSceneSequenceItems(sequence, sceneSequenceItems, count + 1, contentObjectID,contentObjectName, insertIndex);
                if (assetSequenceItems.Count > 0)
                {
                    FillAssetSequenceItems(sequence, assetSequenceItems, 0, contentObjectID,contentObjectName, sceneSequenceItems[count],insertIndex,true);
                }
            }
        }
        /// <summary>
        /// This method fills the asset sequene items in the sequence list
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="assetSequenceItems">Asset list object</param>
        /// <param name="count">int count,index of the current AssetSequenceItem</param>
        /// <param name="contentObjectID">int contentobjectid</param>
        /// <param name="sceneSequenceItem">SceneSequenceItem object</param>
        /// <param name="insertIndex">int insertIndex</param>
        /// <param name="isFirstItem">boolean </param>
 		private void FillAssetSequenceItems(Sequence sequence, List<Asset> assetSequenceItems, int count, int contentObjectID,string contentObjectName,  Scene sceneSequenceItem,int insertIndex,bool isFirstItem)
        {
            //if (count == assetSequenceItems.Count)//The return condition
            //    return;
            
            //To fill asset sequence items according to the scene template
            if (sceneSequenceItem.SceneTemplateType == SceneTemplateType.BF)
            {
                //If this condition is satisfied it will insert new asset on every iteration 
                //if the assetis of known type according to the scene template
                
                //if (assetSequenceItems[count].AssetType == AssetType.FlashObject)
                //{
                    SequenceItem sequenceItem = new SequenceItem();
                    //sequenceItem.Assets.Add(assetSequenceItems[count]);
                    sequenceItem.SequenceItemType = SequenceItemType.FlashAsset;
                    sequenceItem.ParentID = contentObjectID;
                    sequenceItem.ContentObjectName = contentObjectName;
                    sequenceItem.Item_GUID = sceneSequenceItem.Scene_GUID;
                    //sequenceItem.SequenceItemID = assetSequenceItems[count].AssetID;
                    sequenceItem.SceneTemplateID = sceneSequenceItem.SceneTemplateID;
                    sequenceItem.SceneGUID = sceneSequenceItem.Scene_GUID;
                    sequenceItem.sceneID = sceneSequenceItem.SceneID;
                    sequenceItem.IsViewStreamingInScene = sceneSequenceItem.IsViewStreaming;
                    sequenceItem.IsPlayPauseFeatureInScene = sceneSequenceItem.IsPlayPauseFeature; //Added By Abdus Samad For LCMS-12267
                    sequenceItem.SceneName = sceneSequenceItem.SceneName;
                    sequence.SequenceItems.Insert(insertIndex++, sequenceItem);
                //}
                //FillAssetSequenceItems(sequence, assetSequenceItems, count + 1, contentObjectID,contentObjectName, sceneSequenceItem, insertIndex,isFirstItem);
            }
            else
            {
                //If this condition is satisied then it will insert the new asset only on
                //first time and will update it on every iteration provided it is of known type
                //according to the template
                
                bool isInsertable = false;
                SequenceItem sequenceItem = new SequenceItem();
                
                ///Following code is for adding asset item to get scene name
                //Asset sceneAsset = new Asset();
                //sceneAsset.AssetType = AssetType.Text;
                
                
                //sceneAsset.AssetSceneOrientation = "$Heading";
                //if (sceneSequenceItem.IsTitleVisible == true)
                //    sceneAsset.ContentText = sceneSequenceItem.SceneName;
                //else
                //    sceneAsset.ContentText = string.Empty;
                //sceneAsset.IsTopicTitleVisible = sceneSequenceItem.IsTopicTileVisible;


                //sequenceItem.Assets.Add(sceneAsset);
                //////////////////////////////////////////////////////
                
                //sequenceItem.Assets.Add(assetSequenceItems[count]);
                if (sceneSequenceItem.SceneTemplateType == SceneTemplateType.KC)
                {
                    sequenceItem.SequenceItemType = SequenceItemType.KnowledgeCheck;
                }
                else
                {
                    sequenceItem.SequenceItemType = SequenceItemType.ContentAsset;
                }
                sequenceItem.ParentID = contentObjectID;
                sequenceItem.ContentObjectName = contentObjectName;
                sequenceItem.Item_GUID = sceneSequenceItem.Scene_GUID;
                sequenceItem.SequenceItemID = sceneSequenceItem.SceneID;
                sequenceItem.SceneTemplateID = sceneSequenceItem.SceneTemplateID;
                sequenceItem.SceneGUID = sceneSequenceItem.Scene_GUID;
                sequenceItem.sceneID = sceneSequenceItem.SceneID;
                sequenceItem.SceneDuration= sceneSequenceItem.Duration;
                sequenceItem.IsViewStreamingInScene = sceneSequenceItem.IsViewStreaming;
                sequenceItem.IsPlayPauseFeatureInScene = sceneSequenceItem.IsPlayPauseFeature; //Added By Abdus Samad For LCMS-12267
                sequenceItem.SceneName = sceneSequenceItem.SceneName;
                sequenceItem.isTitleVisible = sceneSequenceItem.IsTitleVisible;
                sequenceItem.isTopicTitleVisible = sceneSequenceItem.IsTopicTileVisible;
                
                if (sceneSequenceItem.SceneTemplateType == SceneTemplateType.VSC)
                {
                    sequenceItem.VideoFilename = sceneSequenceItem.VideoFilename;
                    sequenceItem.StreamingServerApplication = sceneSequenceItem.StreamingServerApplication;
                    sequenceItem.VideoHeight = sceneSequenceItem.VideoHeight;
                    sequenceItem.VideoWidth = sceneSequenceItem.VideoWidth;
                    
                    sequenceItem.StartQueueHours = sceneSequenceItem.StartQueueHours;
                    sequenceItem.StartQueueMinutes = sceneSequenceItem.StartQueueMinutes;
                    sequenceItem.StartQueueSeconds = sceneSequenceItem.StartQueueSeconds;

                    sequenceItem.EndQueueHours = sceneSequenceItem.EndQueueHours;
                    sequenceItem.EndQueueMinutes = sceneSequenceItem.EndQueueMinutes;
                    sequenceItem.EndQueueSeconds = sceneSequenceItem.EndQueueSeconds;
                    sequenceItem.FullScreen = sceneSequenceItem.FullScreen;
                    sequenceItem.DisplayStandardTF = sceneSequenceItem.DisplayStandardTF;
                    sequenceItem.DisplayWideScreenTF = sceneSequenceItem.DisplayWideScreenTF;

                    //Added by Abdus Samad //Embeded Code WLCMS-2609 //Start 
                    sequenceItem.IsEmbedCode = sceneSequenceItem.IsEmbedCode;
                    sequenceItem.EmbedCode = sceneSequenceItem.EmbedCode;
                    //Added by Abdus Samad //Embeded Code WLCMS-2609 //Stop
                }

                sequence.SequenceItems.Insert(insertIndex, sequenceItem);

               
                //switch (assetSequenceItems[count].AssetType.ToString())
                //{
                //    default:
                //        {
                //            isInsertable = false;
                //            break;
                //        }
                //    case AssetType.AudioClip:
                //        {
                //            if (isFirstItem == false)
                //            {
                //                sequence.SequenceItems[insertIndex].Assets.Add(assetSequenceItems[count]);
                //            }
                //            else
                //            {
                //                isInsertable = true;
                //            }
                //            break;
                //        }
                //    case AssetType.Image:
                //        {
                //            if (isFirstItem == false)
                //            {
                //                sequence.SequenceItems[insertIndex].Assets.Add(assetSequenceItems[count]);
                //            }
                //            else
                //            {
                //                isInsertable = true;
                //            }
                //            break;
                //        }
                //    case AssetType.Text:
                //        {
                //            if (isFirstItem == false)
                //            {
                //                sequence.SequenceItems[insertIndex].Assets.Add(assetSequenceItems[count]);
                //            }
                //            else
                //            {
                //                isInsertable = true;
                //            }
                //            break;
                //        }
                //    case AssetType.MovieClip:
                //        {
                //            if (isFirstItem == false)
                //            {
                //                sequence.SequenceItems[insertIndex].Assets.Add(assetSequenceItems[count]);
                //            }
                //            else
                //            {
                //                isInsertable = true;
                //            }
                //            break;
                //        }
                //    case AssetType.FlashObject:
                //        {
                //            if (isFirstItem == false)
                //            {
                //                sequence.SequenceItems[insertIndex].Assets.Add(assetSequenceItems[count]);
                //            }
                //            else
                //            {
                //                isInsertable = true;
                //            }
                //            break;
                //        }

                //}
                //Insret only if it is not inserted previously and is of a known type
                //according to template
                //if (isFirstItem == true && isInsertable==true)
                //{
                //    sequence.SequenceItems.Insert(insertIndex, sequenceItem);
                //    isFirstItem = false;
                //}
                
                //FillAssetSequenceItems(sequence, assetSequenceItems, count+1, contentObjectID,contentObjectName, sceneSequenceItem, insertIndex,isFirstItem);
            }

        }	
        #region CourseDemoSequence
        /// <summary>
        /// This method gets the course demo sequence object 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>Sequence object</returns>
        public Sequence GetCourseDemoSequence(int courseID)
        {
            Sequence sequence = new Sequence();
            FillCourseDemoSequence(sequence, courseID);
            return sequence;
        }
        /// <summary>
        /// This method fills the demo sequence object for a particular course 
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="courseID">int courseID</param>
        private void FillCourseDemoSequence(Sequence sequence, int courseID)
        {
            List<ContentObject> contentObjectSequenceItems = new List<ContentObject>();
            using (CourseDA courseDA = new CourseDA())
            {
                List<Scene> sceneSequenceItems = new List<Scene>();
                sceneSequenceItems = courseDA.GetCourseDemoableScenes(courseID);
                if (sceneSequenceItems.Count > 0)
                {
                    List<Asset> assets = null;
                    foreach (Scene sceneSequenceItem in sceneSequenceItems)
                    {
                        assets = new List<Asset>();
                        assets = courseDA.GetSceneAssets(sceneSequenceItem.SceneID);
                        if (assets.Count > 0)
                            FillAssetSequenceItems(sequence, assets, 0, sceneSequenceItem.ContenObjectID, sceneSequenceItem.ContentObjectName, sceneSequenceItem, sequence.SequenceItems.Count, true);

                    }
                }

            }
        }   
        /// <summary>
        /// This method fills the sequence items for contentobject in demo sequence list
        /// </summary>
        /// <param name="sequence">Sequence object</param>
        /// <param name="contentObjectSequenceItems">Contentobject list</param>
        /// <param name="count">int count(index of current contentobejctsequenceitem in scope)</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        private void FillContentObjectSequenceItemsForDemoCourse(Sequence sequence, List<ContentObject> contentObjectSequenceItems, int count)
        {
            if (count == contentObjectSequenceItems.Count) //Return condition
                return;
            //To insert contentobjects after their parents we need the parentcontentobject index
            int parentIndex = GetParentContentObjectIndex(sequence, contentObjectSequenceItems[count].ParentContentObjectID);

            //To insert contentobjects which are at same level,with the same order as they
            //are retreived from the database,we need to find the last child of the 
            //parent content object so that we can insert new contentobject after its
            //already added sibling
            int lastChildIndex;
            if (parentIndex == -1)//case when there is no parent
            {
                //set last child index as the last available item of the sequence
                lastChildIndex = sequence.SequenceItems.Count - 1;
            }
            else
            {
                lastChildIndex = GetLastChildIndex(sequence, parentIndex);
            }
            int insertIndex = lastChildIndex + 1;
            
            
                List<Scene> sceneSequenceItems = new List<Scene>();
                using (CourseDA courseDA = new CourseDA())
                {
                    sceneSequenceItems = courseDA.GetContentObjectScenes(contentObjectSequenceItems[count].ContentObjectID);
                    if (sceneSequenceItems.Count > 0)
                    {
                        FillSceneSequenceItems(sequence, sceneSequenceItems, 0, contentObjectSequenceItems[count].ContentObjectID, contentObjectSequenceItems[count].Name, insertIndex);
                    }
                }
            
            FillContentObjectSequenceItemsForDemoCourse(sequence, contentObjectSequenceItems, count + 1);
        }
        #endregion

        # region Entity Translators
        private SequenceItem TranslateContentObjectSequenceItem(ContentObject contentObject)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemID = contentObject.ContentObjectID;
            sequenceItem.ParentID = contentObject.ParentContentObjectID;
            sequenceItem.SequenceItemType = SequenceItemType.ContentObject;
            sequenceItem.Item_GUID = contentObject.ContentObject_GUID;
            sequenceItem.ContentObjectName = contentObject.Name;
            return sequenceItem;
        }
        private SequenceItem TranslateSceneSequenceItem(Scene scene)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemID = scene.SceneID;
            sequenceItem.SequenceItemType = SequenceItemType.Scene;
            sequenceItem.ParentID = scene.ContenObjectID;
            sequenceItem.Item_GUID = scene.Scene_GUID;
            if (scene.SceneTemplateType == "VSC")
            {
                sequenceItem.VideoFilename = scene.VideoFilename;
                sequenceItem.StreamingServerApplication = scene.StreamingServerApplication;
                sequenceItem.VideoHeight = scene.VideoHeight;
                sequenceItem.VideoWidth = scene.VideoWidth;

                sequenceItem.StartQueueHours = scene.StartQueueHours;
                sequenceItem.StartQueueMinutes = scene.StartQueueMinutes;
                sequenceItem.StartQueueSeconds = scene.StartQueueSeconds;

                sequenceItem.EndQueueHours = scene.EndQueueHours;
                sequenceItem.EndQueueMinutes = scene.EndQueueMinutes;
                sequenceItem.EndQueueSeconds = scene.EndQueueSeconds;

                sequenceItem.FullScreen = scene.FullScreen;
                sequenceItem.DisplayStandardTF = scene.DisplayStandardTF;
                sequenceItem.DisplayWideScreenTF = scene.DisplayWideScreenTF;


                //Added by Abdus Samad //Embeded Code WLCMS-2609 //Start 
                 sequenceItem.IsEmbedCode  = scene.IsEmbedCode;
                 sequenceItem.EmbedCode  = scene.EmbedCode;
                //Added by Abdus Samad //Embeded Code WLCMS-2609 //Stop
            }
            return sequenceItem;
        }
        private SequenceItem TranslateAssetSequenceItem(Asset asset)
        {
            SequenceItem sequenceItem = new SequenceItem();
            sequenceItem.SequenceItemID = asset.AssetID;
            sequenceItem.Item_GUID = asset.Asset_GUID;
            return sequenceItem;
        }
        #endregion
        #endregion
        
        #region TableOfContent Methods
        /// <summary>
        /// This method gets the table of content
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>TableOfContent object</returns>
        public TableOfContent GetTableOfContent(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    List<TOCItem> arrangedTOCItems = new List<TOCItem>();
                    TableOfContent tableOfContent = new TableOfContent();
                    List<ContentObject> rootContentObjects = new List<ContentObject>();
                    rootContentObjects = courseDA.GetCourseRootContentObject(courseID);
                    if (rootContentObjects.Count > 0)
                    {
                        tableOfContent.TOCItems = courseDA.GetCourseContentObjectsInCourseOutline(courseID, rootContentObjects[0].ContentObjectID);
                        foreach (TOCItem tocitem in tableOfContent.TOCItems)
                        {
                            AddToTree(tocitem, arrangedTOCItems, rootContentObjects[0]);
                        }
                        tableOfContent.TOCItems = arrangedTOCItems;
                        return tableOfContent;
                    }
                    else
                    {
                        tableOfContent.TOCItems= new List<TOCItem>();
                        return tableOfContent;
                    }
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method is used to add child content object to its parent.
        /// </summary>
        /// <param name="contentObject">ContentObject object</param>
        /// <param name="arrangedContenObjects">List of arranged contentobject objects.</param>
        private void AddToTree(TOCItem tocitem, List<TOCItem> arrangedTOCItems,ContentObject rootNode)
        {
            if (tocitem.ParentID == rootNode.ContentObjectID)
            {
                arrangedTOCItems.Add(tocitem);
                return;
            }
            foreach (TOCItem TOCItem in arrangedTOCItems)
            {

                if (tocitem.ParentID == TOCItem.ID)
                {
                    if (TOCItem.TOCItems == null)
                        TOCItem.TOCItems = new List<TOCItem>();
                    TOCItem.TOCItems.Add(tocitem);
                    return;
                }
                else
                {
                    if (TOCItem.TOCItems == null)
                        return;
                    AddToTree(tocitem, TOCItem.TOCItems, rootNode);
                }
            }

        }
        #endregion

        #region Course Configuration
        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        public CourseConfiguration GetCourseConfiguration_OLD(int courseID, int source)
        {
            try
            {
                
                using (CourseDA courseDA = new CourseDA())
                {
                    CourseConfiguration mainCourseConfiguration = courseDA.GetCourseConfiguration(courseID);
                    List<CourseConfiguration> courseApprovalCourseConfigurationTemplates = null;

                    if (source == 0) // If course is launched from LMS
                    {
                        courseApprovalCourseConfigurationTemplates = courseDA.GetCourseApprovalCourseConfigurationTemplates(courseID);
                    }

                    if (courseApprovalCourseConfigurationTemplates == null || courseApprovalCourseConfigurationTemplates.Count == 0)
                    {
                        return mainCourseConfiguration;
                    }

                    foreach (CourseConfiguration cc in courseApprovalCourseConfigurationTemplates)
                    {

                        /*Policy : Enable Oreintation Scene, OVERRIDDEN : TRUE*/
                        if (cc.PlayerEnableOrientaionScenes == true)
                        {
                            mainCourseConfiguration.PlayerEnableOrientaionScenes = true;
                        }

                        /*Policy : Enable End of Course scene, OVERRIDDEN : TRUE*/
                        /*Policy : End of Course scene, OVERRIDDEN : Greater Lenght*/
                        if (cc.PlayerEnableEndOfCourseScene)
                        {
                            if (cc.PlayerEndOfCourseInstructions == null)
                            {
                                cc.PlayerEndOfCourseInstructions = "";
                            }

                            if (mainCourseConfiguration.PlayerEndOfCourseInstructions == null)
                            {
                                mainCourseConfiguration.PlayerEndOfCourseInstructions = "";
                            }

                            if (!mainCourseConfiguration.PlayerEnableEndOfCourseScene || cc.PlayerEndOfCourseInstructions.Length > mainCourseConfiguration.PlayerEndOfCourseInstructions.Length)
                            {
                                mainCourseConfiguration.PlayerEnableEndOfCourseScene = true;
                                mainCourseConfiguration.PlayerEndOfCourseInstructions = cc.PlayerEndOfCourseInstructions;
                            }
                        }

                        /*Policy : Enable Content, OVERRIDDEN : TRUE*/
                        if (cc.PlayerEnableContent == true)
                        {
                            mainCourseConfiguration.PlayerEnableContent = true;
                        }

                        /*Policy : Enable Content, OVERRIDDEN : TRUE*/
                        if (cc.PlayerEnforceTimedOutline == true)
                        {
                            mainCourseConfiguration.PlayerEnforceTimedOutline = true;
                        }

                        /*Policy : Idle User Time out (seconds), OVERRIDDEN : Lesser Value*/
                        if (mainCourseConfiguration.PlayerIdleUserTimeout > cc.PlayerIdleUserTimeout)
                        {
                            mainCourseConfiguration.PlayerIdleUserTimeout = cc.PlayerIdleUserTimeout;
                        }

                        /*Policy : Action to take upon Idle timeout, OVERRIDDEN : Close Course*/
                        if (cc.ActionToTakeUponIdleTimeOut == BusinessEntities.IdleTimeOut.CloseCourse)
                        {
                            mainCourseConfiguration.ActionToTakeUponIdleTimeOut = cc.ActionToTakeUponIdleTimeOut;
                        }

                        /*Policy : Course Flow, OVERRIDDEN : First Time Linear*/
                        if (cc.PlayerCourseFlow == BusinessEntities.CourseFlow.FirstTimeLinear)
                        {
                            mainCourseConfiguration.PlayerCourseFlow = BusinessEntities.CourseFlow.FirstTimeLinear;
                        }

                        /*Policy : Allow User to Review Course After Completion, OVERRIDDEN : FALSE*/
                        if (cc.PlayerAllowUserToReviewCourseAfterCompletion == false)
                        {
                            mainCourseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion = false; 
                        }
                        
                        /*Policy : Course Completion Certificate, OVERRIDDEN : TRUE*/
                        // LCMS-8789
                        // -------------------------------------------------
                        //if (cc.CertificateEnabled == true)
                        //{
                        //    mainCourseConfiguration.CertificateEnabled = true;
                        //}

                        if (cc.CertificateEnabled == true && mainCourseConfiguration.CertificateEnabled == false)
                        {
                            mainCourseConfiguration.CertificateEnabled = true;
                            mainCourseConfiguration.CertificateAssetID = cc.CertificateAssetID;
                        }

                        // -------------------------------------------------
                        /*Policy : Course Evaluation, OVERRIDDEN : TRUE*/
                        if (cc.PlayerCourseEvaluation == true)
                        {
                            mainCourseConfiguration.PlayerCourseEvaluation = true;
                        }

                        /*Policy : Display Course Evaluation, OVERRIDDEN : Before & After Assessment*/
                        if (cc.PlayerDisplayCourseEvaluation == BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                        {
                            mainCourseConfiguration.PlayerDisplayCourseEvaluation = cc.PlayerDisplayCourseEvaluation;
                        }

                        /*Policy : Must Complete Course Evaluation, OVERRIDDEN : TRUE*/
                        if (cc.PlayerMustCompleteCourseEvaluatio == true)
                        {
                            mainCourseConfiguration.PlayerMustCompleteCourseEvaluatio = true;
                        }


                        // LCMS-8796 START
                        //---------------------------------------------------------------

                        if (cc.SpecialQuestionnaire  == true)
                        {
                            mainCourseConfiguration.SpecialQuestionnaire = true;
                        }

                        /*Policy : Display Course Evaluation, OVERRIDDEN : Before & After Assessment*/
                        if (cc.DisplaySpecialQuestionnaire == BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment)
                        {
                            mainCourseConfiguration.DisplaySpecialQuestionnaire = cc.DisplaySpecialQuestionnaire;
                        }

                        /*Policy : Must Complete Course Evaluation, OVERRIDDEN : TRUE*/
                        if (cc.MustCompleteSpecialQuestionnaire == true)
                        {
                            mainCourseConfiguration.MustCompleteSpecialQuestionnaire = true;
                        }

                        //---------------------------------------------------------------
                        // LCMS-8796 END

                        /*Policy : Post Assessment Attempted, OVERRIDDEN : TRUE*/
                        if (cc.CompletionPostAssessmentAttempted == true)
                        {
                            mainCourseConfiguration.CompletionPostAssessmentAttempted = true;
                        }

                        /*Policy : Post Assessment Mastery, OVERRIDDEN : TRUE*/
                        if (cc.CompletionPostAssessmentMastery == true)
                        {
                            mainCourseConfiguration.CompletionPostAssessmentMastery = true;
                        }

                        /*Policy : Pre Assessment Mastery, OVERRIDDEN : TRUE*/
                        if (cc.CompletionPreAssessmentMastery == true)
                        {
                            mainCourseConfiguration.CompletionPreAssessmentMastery = true;
                        }

                        /*Policy : Quiz Mastery, OVERRIDDEN : TRUE*/
                        if (cc.CompletionQuizMastery == true)
                        {
                            mainCourseConfiguration.CompletionQuizMastery = true;
                        }

                        /*Policy : Survey, OVERRIDDEN : TRUE*/
                        if (cc.CompletionSurvey == true)
                        {
                            mainCourseConfiguration.CompletionSurvey = true;
                        }

                        /*Policy : View every scene in course, OVERRIDDEN : TRUE*/
                        if (cc.CompletionViewEverySceneInCourse == true)
                        {
                            mainCourseConfiguration.CompletionViewEverySceneInCourse = true;
                        }

                        /*Policy : Complete after number of unique visits (launches) of course, OVERRIDDEN : Greater Value*/
                        if (mainCourseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit < cc.CompletionCompleteAfterNOUniqueCourseVisit)
                        {
                            mainCourseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit = cc.CompletionCompleteAfterNOUniqueCourseVisit;
                        }
                        
                        /*Policy : Must complete within specified amount of time (minutes) after first visit, OVERRIDDEN : Lesser Value*/
                        if (cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute > 0)
                        {
                            long mainCompMin = 0;
                            long ccCompMin = 0;

                            if (mainCourseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Hours)
                            {
                                mainCompMin = mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * 60;
                            }
                            else if (mainCourseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Days)
                            {
                                mainCompMin = mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * (24 * 60);
                            }
                            else if (mainCourseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Months)
                            {
                                mainCompMin = mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * ((24 * 60) * 30);
                            }
                            else
                            {
                                mainCompMin = mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute;
                            }

                            if (cc.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Hours)
                            {
                                ccCompMin = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * 60;
                            }
                            else if (cc.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Days)
                            {
                                ccCompMin = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * (24 * 60);
                            }
                            else if (cc.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime == TimeUnit.Months)
                            {
                                ccCompMin = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute * ((24 * 60) * 30);
                            }
                            else
                            {
                                ccCompMin = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute;
                            }

                            if (mainCompMin <= 0 || mainCompMin > ccCompMin)
                            {
                                mainCourseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime = cc.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime;
                                mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute;
                            }

                        }
                        /*Policy : Must complete within specified amount of time (days) from registration date, OVERRIDDEN : Lesser Value*/
                        if (cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay > 0)
                        {
                            if (mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay <= 0 || mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay > cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay)
                            {
                                mainCourseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay = cc.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay;
                            }
                        }

                        /*Policy : Respond to Course Evaluation, OVERRIDDEN : TRUE*/
                        if (cc.CompletionRespondToCourseEvaluation == true)
                        {
                            mainCourseConfiguration.CompletionRespondToCourseEvaluation = true;
                        }

                        /* ASSESSMENT CONFIGURATION POLICIES - START*/
                        for (int i = 1; i <= 3; i++)
                        {
                            AssessmentConfiguration mainAssessmentConfiguration = null;
                            AssessmentConfiguration courseApprovalAssessmentConfiguration = null;
                            bool flag = false;
                            bool overrideCourseaApprovalAssessment = false;

                            if (i == 1)
                            {
                                mainAssessmentConfiguration = mainCourseConfiguration.PreAssessmentConfiguration;
                                courseApprovalAssessmentConfiguration = cc.PreAssessmentConfiguration;
                            }
                            else if (i == 2)
                            {
                                mainAssessmentConfiguration = mainCourseConfiguration.QuizConfiguration;
                                courseApprovalAssessmentConfiguration = cc.QuizConfiguration;
                            }
                            else if (i == 3)
                            {
                                mainAssessmentConfiguration = mainCourseConfiguration.PostAssessmentConfiguration;
                                courseApprovalAssessmentConfiguration = cc.PostAssessmentConfiguration;
                            }

                            if (mainAssessmentConfiguration == null && courseApprovalAssessmentConfiguration == null)
                            {
                                flag = true;
                            }
                            else if (mainAssessmentConfiguration == null && courseApprovalAssessmentConfiguration != null)
                            {
                                overrideCourseaApprovalAssessment = true;
                                flag = true;
                            }
                            else if (mainAssessmentConfiguration.Enabled && !courseApprovalAssessmentConfiguration.Enabled)
                            {
                                flag = true;
                            }
                            else if (!mainAssessmentConfiguration.Enabled && courseApprovalAssessmentConfiguration.Enabled)
                            {
                                overrideCourseaApprovalAssessment = true;
                                flag = true;
                            }
                            else if (!mainAssessmentConfiguration.Enabled && !courseApprovalAssessmentConfiguration.Enabled)
                            {
                                flag = true;
                            }

                            if (overrideCourseaApprovalAssessment)
                            {
                                if (i == 1)
                                {
                                    mainCourseConfiguration.PreAssessmentConfiguration = cc.PreAssessmentConfiguration;
                                }
                                else if (i == 2)
                                {
                                    mainCourseConfiguration.QuizConfiguration = cc.QuizConfiguration;
                                }
                                else if (i == 3)
                                {
                                    mainCourseConfiguration.PostAssessmentConfiguration = cc.PostAssessmentConfiguration;
                                }
                            }

                            if (flag)
                            {
                                continue;
                            }

                            /*Policy : Assessment Enable, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.Enabled == true)
                            {
                                mainAssessmentConfiguration.Enabled = true;
                            }

                            /*Policy : Number of questions, OVERRIDDEN : Greater Value*/
                            if (mainAssessmentConfiguration.NOQuestion < courseApprovalAssessmentConfiguration.NOQuestion)
                            {
                                mainAssessmentConfiguration.NOQuestion = courseApprovalAssessmentConfiguration.NOQuestion;
                            }

                            /*Policy : Mastery score, OVERRIDDEN : Greater Value*/
                            if (mainAssessmentConfiguration.MasteryScore < courseApprovalAssessmentConfiguration.MasteryScore)
                            {
                                mainAssessmentConfiguration.MasteryScore = courseApprovalAssessmentConfiguration.MasteryScore;
                            }

                            /*Policy : Randomize questions, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.RandomizeQuestion == true)
                            {
                                mainAssessmentConfiguration.RandomizeQuestion = true;
                            }

                            /*Policy : Randomize answers, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.RandomizeAnswers == true)
                            {
                                mainAssessmentConfiguration.RandomizeAnswers = true;
                            }

                            /*Policy : Ask unique questions on retakes, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
                            {
                                mainAssessmentConfiguration.EnforceUniqueQuestionsOnRetake = true;
                            }

                            /*Policy : Automatically Grade Assessment After/Enforce maximum time limit (in minutes), OVERRIDDEN : Lesser Value/TRUE*/
                            if (mainAssessmentConfiguration.EnforceMaximumTimeLimit > courseApprovalAssessmentConfiguration.EnforceMaximumTimeLimit)
                            {
                                mainAssessmentConfiguration.EnforceMaximumTimeLimit = courseApprovalAssessmentConfiguration.EnforceMaximumTimeLimit;
                            }

                            /*Policy : Allow skipping questions, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.AllowSkippingQuestion == true)
                            {
                                mainAssessmentConfiguration.AllowSkippingQuestion = true;
                            }

                            /*Policy : Enable Advacne Question Selection, OVERRIDDEN : Discrete Question Selection*/
                            if (courseApprovalAssessmentConfiguration.AdvanceQuestionSelectionType == BusinessEntities.AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE)
                            {
                                mainAssessmentConfiguration.AdvanceQuestionSelectionType = courseApprovalAssessmentConfiguration.AdvanceQuestionSelectionType;
                            }

                            /*Policy : Enable Proctored Assessment, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.ProctoredAssessment == true)
                            {
                                mainAssessmentConfiguration.ProctoredAssessment = true;
                            }

                            /*Policy : Grade Assessment , OVERRIDDEN : After Assessment is submitted*/
                            if (courseApprovalAssessmentConfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED)
                            {
                                mainAssessmentConfiguration.GradeQuestions = courseApprovalAssessmentConfiguration.GradeQuestions;
                            }

                            /*Policy : Allow reviewing Question and Answers after grading , OVERRIDDEN : FALSE*/
                            if (courseApprovalAssessmentConfiguration.ShowQuestionAnswerSummary == false)
                            {
                                mainAssessmentConfiguration.ShowQuestionAnswerSummary = false;
                            }

                            /*Policy : Show feedback while reviewing Questions, OVERRIDDEN : FALSE*/
                            if (courseApprovalAssessmentConfiguration.QuestionLevelResult == false)
                            {
                                mainAssessmentConfiguration.QuestionLevelResult = false;
                            }

                            /*Policy : Content Remidiation, OVERRIDDEN : FALSE*/
                            if (courseApprovalAssessmentConfiguration.ContentRemediation == false)
                            {
                                mainAssessmentConfiguration.ContentRemediation = false;
                            }

                            /*Policy : Show The Learner, OVERRIDDEN : No Score, then Pass Fail, then Percent Score*/
                            if (mainAssessmentConfiguration.ScoreType != ScoreType.NoResults && courseApprovalAssessmentConfiguration.ScoreType == ScoreType.NoResults)
                            {
                                mainAssessmentConfiguration.ScoreType = courseApprovalAssessmentConfiguration.ScoreType;
                            }
                            else if (mainAssessmentConfiguration.ScoreType != ScoreType.PassFail && courseApprovalAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                            {
                                mainAssessmentConfiguration.ScoreType = courseApprovalAssessmentConfiguration.ScoreType;
                            }
                            else if (mainAssessmentConfiguration.ScoreType != ScoreType.PercentScore && courseApprovalAssessmentConfiguration.ScoreType == ScoreType.PercentScore)
                            {
                                mainAssessmentConfiguration.ScoreType = courseApprovalAssessmentConfiguration.ScoreType;
                            }

                            /*Policy : Maximum Attempt Handler, OVERRIDDEN : TRUE*//*LCMS-8542*/
                            if (courseApprovalAssessmentConfiguration.MaxAttemptHandlerEnabled)
                            {
                                mainAssessmentConfiguration.MaxAttemptHandlerEnabled = courseApprovalAssessmentConfiguration.MaxAttemptHandlerEnabled;

                                /*Policy : Action to take after failing max attempts, OVERRIDDEN : Lock Course*/
                                if (courseApprovalAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt == AfterMaxFailAction.LockCourse)
                                {
                                    mainAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt = courseApprovalAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt;
                                }/*LCMS-8542*/
                                else if (courseApprovalAssessmentConfiguration.AssessmentType == AssessmentConfiguration.ASSESSMENTYPE_QUIZ)
                                {
                                    if (mainAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt != AfterMaxFailAction.LockCourse)
                                    {
                                        if (courseApprovalAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt == AfterMaxFailAction.RetakeLesson)
                                        {
                                            mainAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt = courseApprovalAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt;
                                        }
                                    }
                                }

                                /*Policy : After failing attempts, OVERRIDDEN : Lesser Value*/
                                if (mainAssessmentConfiguration.MaximumNOAttempt > courseApprovalAssessmentConfiguration.MaximumNOAttempt)
                                {
                                    mainAssessmentConfiguration.MaximumNOAttempt = courseApprovalAssessmentConfiguration.MaximumNOAttempt;
                                }
                            }
                            /*Policy : Restrictive Mode, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.RestrictiveMode == true)
                            {
                                mainAssessmentConfiguration.RestrictiveMode = true;
                            }

                            /*Policy : Weighted Score, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.UseWeightedScore == true)
                            {
                                mainAssessmentConfiguration.UseWeightedScore = true;
                            }


                            /*MIN SEAT TIME - START*/

                            /*Policy : Minimum Seat Time Message, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.DisplaySeatTimeSatisfiedMessageTF == true)
                            {
                                mainAssessmentConfiguration.DisplaySeatTimeSatisfiedMessageTF = true;
                            }

                            /*Policy : Minimum Seat Time Message, OVERRIDDEN : TRUE*/
                            if (courseApprovalAssessmentConfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF == true)
                            {
                                mainAssessmentConfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF = true;
                            }

                            /*Policy : Minimum Seat Time before unlock, OVERRIDDEN : TRUE*/
                            int ccaMin = courseApprovalAssessmentConfiguration.MinimumTimeBeforeStart;
                            int mccMin = mainAssessmentConfiguration.MinimumTimeBeforeStart;

                            if (courseApprovalAssessmentConfiguration.MinimumTimeBeforeStartUnit == BusinessEntities.TimeUnit.Hours)
                            {
                                ccaMin *= 60;
                            }

                            if (mainAssessmentConfiguration.MinimumTimeBeforeStartUnit == BusinessEntities.TimeUnit.Hours)
                            {
                                mccMin *= 60;
                            }

                            if (mccMin < ccaMin)
                            {
                                mainAssessmentConfiguration.MinimumTimeBeforeStart = courseApprovalAssessmentConfiguration.MinimumTimeBeforeStart;
                                mainAssessmentConfiguration.MinimumTimeBeforeStartUnit = courseApprovalAssessmentConfiguration.MinimumTimeBeforeStartUnit;
                            }

                            /*MIN SEAT TIME - START*/

                        }
                        /* ASSESSMENT CONFIGURATION POLICIES - END*/

                        /*VALIDATION POLICY - START*/

                        /*Policy : Require Identity Validation, OVERRIDDEN : TRUE*/
                        if (cc.ValidationRequireIdentityValidation == true)
                        {
                            mainCourseConfiguration.ValidationRequireIdentityValidation = true;

                            /*Policy : Time between question (seconds), OVERRIDDEN : Lesser Value*/
                            if (mainCourseConfiguration.ValidationTimeBetweenQuestion > cc.ValidationTimeBetweenQuestion)
                            {
                                mainCourseConfiguration.ValidationTimeBetweenQuestion = cc.ValidationTimeBetweenQuestion;
                            }

                            /*Policy : Time to answer question (seconds), OVERRIDDEN : Lesser Value*/
                            if (mainCourseConfiguration.ValidationTimeToAnswerQuestion > cc.ValidationTimeToAnswerQuestion)
                            {
                                mainCourseConfiguration.ValidationTimeToAnswerQuestion = cc.ValidationTimeToAnswerQuestion;
                            }

                            /*Policy : Number of missed questions allowed, OVERRIDDEN : Lesser Value*/
                            if (mainCourseConfiguration.ValidationNOMissedQuestionsAllowed > cc.ValidationNOMissedQuestionsAllowed)
                            {
                                mainCourseConfiguration.ValidationNOMissedQuestionsAllowed = cc.ValidationNOMissedQuestionsAllowed;
                            }

                            /*Policy : Number of validation questions, OVERRIDDEN : Lesser Value*/
                            if (mainCourseConfiguration.ValidationNOValidationQuestion > cc.ValidationNOValidationQuestion)
                            {
                                mainCourseConfiguration.ValidationNOValidationQuestion = cc.ValidationNOValidationQuestion;
                            }
                        }

                        /*VALIDATION POLICY - END*/

                        /*DISCLAIMER POLICY - START*/

                        /*Policy : Enbeded Ack Enabled, OVERRIDDEN : TRUE*/
                        /*Policy : Enbeded Ack Enabled, OVERRIDDEN : Gerater Value Length*/
                        
                        if (mainCourseConfiguration.EmbeddedAcknowledgmentText == null)
                        {
                            mainCourseConfiguration.EmbeddedAcknowledgmentText = "";
                        }

                        if (cc.EmbeddedAcknowledgmentText == null)
                        {
                            mainCourseConfiguration.EmbeddedAcknowledgmentText = "";
                        }

                        if (cc.EmbeddedAcknowledgmentEnabled && cc.EmbeddedAcknowledgmentText.Length > mainCourseConfiguration.EmbeddedAcknowledgmentText.Length)
                        {
                            mainCourseConfiguration.EmbeddedAcknowledgmentEnabled = true;
                            mainCourseConfiguration.EmbeddedAcknowledgmentText = cc.EmbeddedAcknowledgmentText;
                        }


                        /*DISCLAIMER POLICY - END*/

                        /*MAXIMUM SEAT TIME POLICY - START*/

                        /*Policy : Maximum Seat Time, OVERRIDDEN : Greater Value*/
                        if (cc.SeatTimeEnabled == true)
                        {
                            if (cc.SeatTimeInHour > 0 || cc.SeatTimeInMin > 0)
                            {
                                int ccaMax = cc.SeatTimeInMin + (cc.SeatTimeInHour > 0 ? cc.SeatTimeInHour * 60 : 0);
                                int mccMax = mainCourseConfiguration.SeatTimeInMin + (mainCourseConfiguration.SeatTimeInHour > 0 ? mainCourseConfiguration.SeatTimeInHour * 60 : 0);

                                if (mccMax < ccaMax)
                                {
                                    mainCourseConfiguration.SeatTimeInHour = cc.SeatTimeInHour;
                                    mainCourseConfiguration.SeatTimeInMin = cc.SeatTimeInMin;
                                }
                            }

                            /*Policy : Message Seat time course launch, OVERRIDDEN : Gerater Value Length*/
                            if (mainCourseConfiguration.MessageSeatTimeCourseLaunch == null)
                            {
                                mainCourseConfiguration.MessageSeatTimeCourseLaunch = "";
                            }

                            if (cc.MessageSeatTimeCourseLaunch == null)
                            {
                                cc.MessageSeatTimeCourseLaunch = "";
                            }

                            if (!mainCourseConfiguration.SeatTimeEnabled || cc.MessageSeatTimeCourseLaunch.Length > mainCourseConfiguration.MessageSeatTimeCourseLaunch.Length)
                            {
                                mainCourseConfiguration.MessageSeatTimeCourseLaunch = cc.MessageSeatTimeCourseLaunch;
                            }

                            /*Policy : Message Seat time exceeds, OVERRIDDEN : Gerater Value Length*/
                            if (mainCourseConfiguration.MessageSeatTimeExceeds == null)
                            {
                                mainCourseConfiguration.MessageSeatTimeExceeds = "";
                            }

                            if (cc.MessageSeatTimeExceeds == null)
                            {
                                cc.MessageSeatTimeExceeds = "";
                            }

                            if (!mainCourseConfiguration.SeatTimeEnabled || mainCourseConfiguration.MessageSeatTimeExceeds.Length < cc.MessageSeatTimeExceeds.Length)
                            {
                                mainCourseConfiguration.MessageSeatTimeExceeds = cc.MessageSeatTimeExceeds;
                            }
                            

                            mainCourseConfiguration.SeatTimeEnabled = true;

                        
                        }
                        /*MAXIMUM SEAT TIME POLICY - END*/

                        /*Policy : Must Start course within specified amount of time after resigtration Date, OVERRIDDEN : Lesser Value*/
                        // LCMS-8422
                        if (cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate > 0 && cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF==true)
                        {
                            long mainMSCMin = 0;
                            long ccMSCMin = 0;

                            if (mainCourseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Minutes)
                            {
                                mainMSCMin = mainCourseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
                            }
                            else if (mainCourseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Days)
                            {
                                mainMSCMin = mainCourseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate * (24 * 60);
                            }
                            else if (mainCourseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Months)
                            {
                                mainMSCMin = mainCourseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate * ((24 * 60) * 30);
                            }

                            if (cc.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Minutes)
                            {
                                ccMSCMin = cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
                            }
                            else if (cc.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Days)
                            {
                                ccMSCMin = cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate * (24 * 60);
                            }
                            else if (cc.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate == TimeUnit.Months)
                            {
                                ccMSCMin = cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate * ((24 * 60) * 30);
                            }

                            if (mainMSCMin <= 0 || mainMSCMin > ccMSCMin)
                            {
                                mainCourseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = cc.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
                                mainCourseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = cc.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate;
                            }
                        }
                        /*Must Start course within specified amount of time after resigtration Date POLICY-END*/
                    }
                    return mainCourseConfiguration;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        public CourseConfiguration GetCourseConfiguration(int courseConfigurationID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    CourseConfiguration mainCourseConfiguration = courseDA.GetCourseConfiguration(courseConfigurationID);                    
                    return mainCourseConfiguration;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns course configuration ID
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseID">int source</param>         
        /// <returns>courseconfigurationID int</returns>
        public int GetCourseConfigurationID(int courseID, int source, int courseApprovalID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseConfigurationID(courseID, source, courseApprovalID);                    
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseID">int courseApprovalID</param>
        /// <returns>courseconfiguration object</returns>
        public CourseConfiguration GetCourseApprovalCourseConfiguration(int courseID, int courseApprovalID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    CourseConfiguration mainCourseConfiguration = courseDA.GetCourseApprovalCourseConfiguration(courseID, courseApprovalID);   
                    return mainCourseConfiguration;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns Practice Exam Assessment configuration
        /// </summary>
        /// <param name="courseID">int examID</param>
        /// <returns>AssessmentConfiguration object</returns>
        public AssessmentConfiguration GetPraceticeExamAssessmentConfiguration(int examID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetPraceticeExamAssessmentConfiguration(examID); 
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns Practice Exam Assessment configuration
        /// </summary>
        /// <param name="courseID">int examID</param>
        /// <returns>AssessmentConfiguration object</returns>
        public AssessmentConfiguration GetQuizExamAssessmentConfiguration(int contentObjectID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetQuizExamAssessmentConfiguration(contentObjectID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion
        
        #region GlossaryItem
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of glossaryitem objects</returns>
        public List<GlossaryItem> GetCourseGlossaryItems(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseGlossaryItems(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of glossaryitem objects</returns>
        public List<GlossaryItem> GetCourseSceneGlossaryItems(int courseID, int sceneID) 
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseSceneGlossaryItems(courseID,sceneID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the glossary item definition
        /// </summary>
        /// <param name="glossaryItemID">int glossaryitemID</param>
        /// <returns>GlossaryItem object</returns>
        public GlossaryItem GetGlossaryItemDefinition(int glossaryItemID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetGlossaryItemDefinition(glossaryItemID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion

        #region CourseMaterial
        /// <summary>
        /// This method gets the CourseMaterialInfo objects associated to a Course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of CourseMaterialInfo object</returns>
        public List<CourseMaterialInfo> GetCourseMaterialInfo(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseMaterialInfo(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        
        #endregion

        #region Intro/End Page
        private CourseIntroPage GetCourseIntroPage(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetIntroPage(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public EOCInstructions GetEOCInstructions_LMS(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetEOCInstructions(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public String GetCourseEOCInstructions(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseEOCInstructions(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        private CourseEndPage GetCourseEndPage(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetEndPage(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
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
        public List<ValidationQuestion> GetValidationQuestions(int learnerID, string variant)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetValidationQuestions(learnerID,variant);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="learningSessionId"></param>
        /// <returns></returns>
        public bool CreateValidationUnlockRequest(string learningSessionId, string brandCode, string variant)
        {
            try
            {
                
                string strGUID = Guid.NewGuid().ToString("N");
                string emailAddress = string.Empty;

                using (CourseDA courseDA = new CourseDA())
                {
                    emailAddress = courseDA.CreateValidationUnlockRequest(learningSessionId, strGUID,brandCode,variant);
                }

                if (emailAddress == string.Empty)
                    return false;

                string mailBody = @"" +
                    @"Please use the following link to reset your validation questions: " +
                    @"<a href=" + ConfigurationManager.AppSettings["ValidationShowProfile"] + "?GUID=" + strGUID + ">reset your validation questions</a>";
                //emailAddress
                return SendEmail(emailAddress, ConfigurationManager.AppSettings["FromEmailValidationUnlock"], ConfigurationManager.AppSettings["BccEmailValidationUnlock"], "Course Unlock Request", mailBody);
                // SendEmail("saeeda.riaz@360training.com", "riaz_sadia@hotamail.com", "s@s.com", "Course is locked", mailBody);//SendEmail(emailAddress, ConfigurationManager.AppSettings["FromEmailValidationUnlock"], ConfigurationManager.AppSettings["BccEmailValidationUnlock"], "Course Unlock Request", mailBody);
                
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }

        }

        #endregion

        #region emailing
        public bool SendEmail(string ToEmail, string FromEmail, string BCCEmail, string Subject, string MailBody)
        {

            //try
            //{
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();                
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUser"], ConfigurationManager.AppSettings["SMTPPassword"]);
                smtpClient.Host = ConfigurationManager.AppSettings["SMTPAddress"];
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                System.Net.Mail.MailAddress fromMailAddress = new System.Net.Mail.MailAddress(FromEmail);//(ConfigurationManager.AppSettings["FromEmailValidationUnlock"]);
                mailMessage.From = fromMailAddress;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = Subject;
                mailMessage.To.Add(ToEmail);
                mailMessage.Body = MailBody;
                mailMessage.Bcc.Add(BCCEmail);
                smtpClient.Send(mailMessage);
                return true;
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}

        }
        public SceneTemplate GetValidationSceneOrientationTemplateHTML()
        {
            SceneTemplate sceneTemplate = new SceneTemplate();

            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    
                    string sceneTemplateHTMLURL = courseDA.GetSceneTemplateTypeHtmlURL(SceneTemplateType.VIC);
                    if (sceneTemplateHTMLURL != string.Empty)
                    {
                        using (TextReader textReader = new StreamReader(sceneTemplateHTMLURL))
                        {
                            sceneTemplate.TemplateHTML = textReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        sceneTemplate.TemplateHTML = string.Empty;
                    }
                }
                return sceneTemplate;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion

        #region Course Evaluation
        public CourseEvaluation GetCourseEvaluationByCourseID(int courseID, string  surveyType)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    CourseEvaluation courseEvaluation = courseDA.GetCourseEvaluationByCourseID(courseID, surveyType);
                    if (courseEvaluation != null)
                        courseEvaluation.CourseEvaluationQuestions = GetCourseEvaluationQuestions(courseEvaluation.ID);
                    return courseEvaluation;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public List<CourseEvaluationQuestion> GetCourseEvaluationQuestions(int courseEvaluationID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    //First Get all the Questions 
                    List<CourseEvaluationQuestion> courseEvaluationQuestions = courseDA.GetCourseEvaluationQuestions(courseEvaluationID);
                    //Second get all the answers
                    if (courseEvaluationQuestions != null)
                    {
                        List<CourseEvaluationAnswer> courseEvaluationAnswers = courseDA.GetCourseEvaluationAnswerByCourseEvaluation(courseEvaluationID);
                        //Finally, bind all the answers to their questions
                        if (courseEvaluationAnswers != null)
                        {
                            foreach (CourseEvaluationQuestion evalQuestion in courseEvaluationQuestions)
                            {
                                List<CourseEvaluationAnswer> questionAnswers = GetAnswersForQuestion(evalQuestion.QuestionID, courseEvaluationAnswers);
                                if (questionAnswers != null)
                                    evalQuestion.CourseEvaluationAnswers.AddRange(questionAnswers);
                            }
                        }
                    }
                    return courseEvaluationQuestions;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        private List<CourseEvaluationAnswer> GetAnswersForQuestion(int questionID, List<CourseEvaluationAnswer> lookupList)
        {
            return lookupList.FindAll(delegate(CourseEvaluationAnswer courseEvaluationAnswer) { return courseEvaluationAnswer.QuestionID == questionID; });
        }
        public int GetCourseEvaluationQuestionsCount(int courseID, string surveyType)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    int questionsCount = 0;
                    questionsCount = courseDA.GetCourseEvaluationQuestionsCount(courseID, surveyType);
                    return questionsCount;
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        #endregion

        # region Course Approval
        /// <summary>
        /// This method returns the list of Course Approval
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of CourseApproval objects</returns>
        public List<CourseApproval> GetCourseCourseApproval(int courseID, string learningSessionGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseCourseApproval(courseID, learningSessionGUID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the true/false
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>true/false</returns>
        public bool CheckLearnerCourseCourseApproval(int courseID, string learningSessionGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.CheckLearnerCourseCourseApproval(courseID, learningSessionGUID); 
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return true;
            }
        }

        /// <summary>
        /// This method save the Learner Course Approval
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>
        /// <param name="CourseApprovalID">int CourseApprovalID</param> 
        /// <returns>int</returns>
        public int SaveLearnerCourseApproval(int courseID, string learningSessionGUID,  int CourseApprovalID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveLearnerCourseApproval(courseID, learningSessionGUID, CourseApprovalID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// This method save the Learner Course Message
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>        
        /// <returns>int</returns>
        public bool SaveLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveLearnerCourseMessage(courseID, learningSessionGuid);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// This method save the Learner Course Message
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="CourseApprovalID">string learningSessionGUID</param>        
        /// <returns>int</returns>
        public bool CheckLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.CheckLearnerCourseMessage(courseID, learningSessionGuid);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

        /// <summary>
        /// This method gets the Course Approval' Affidavit ID
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>
        /// <returns>Affidavit ID</returns>
        public int GetCourseApprovalAffidavit(int courseID, int courseApprovalID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseApprovalAffidavit(courseID, courseApprovalID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }            
        }

        /// <summary>
        /// This method returns CourseApprovalCertificate
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>        
        /// <returns>CourseApprovalCertificate</returns>
        public CourseApprovalCertificate GetCourseApprovalCertificate(int courseApprovalID, int enrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseApprovalCertificate(courseApprovalID, enrollmentID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        #endregion

        public SceneTemplate GetSceneTemplateHTMLByNameAndType(string type)
        {
            SceneTemplate sceneTemplate = new SceneTemplate();

            try
            {
                using (CourseDA courseDA = new CourseDA())
                {

                    string sceneTemplateHTMLURL = courseDA.GetSceneTemplateTypeHtmlURL(type);
                    if (sceneTemplateHTMLURL != string.Empty)
                    {
                        using (TextReader textReader = new StreamReader(sceneTemplateHTMLURL))
                        {
                            sceneTemplate.TemplateHTML = textReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        sceneTemplate.TemplateHTML = string.Empty;
                    }
                }
                return sceneTemplate;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }




        #endregion

        #region Sub Content Owner
        /// <summary>
        /// This method gets the Original Course ID into Sub Content Owner
        /// </summary>
        /// <param name="courseID">int courseID</param>        
        /// <returns>Course ID</returns>
        public int GetOriginalCourseIDFromSubContentOwner(int offerCourseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetOriginalCourseIDFromSubContentOwner(offerCourseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        #endregion

        //LCMS-10392
        public string GetCourseKeywords(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseKeywords(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region DocuSign LCMS-11217
        public DocuSignLearner GetLearnerData(int CourseID, string LearnerSessionID, int LearnerID, int EnrollmentID)
        {
            DocuSignLearner docuSignLearnerData = new DocuSignLearner();
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetLearnerData(CourseID, LearnerSessionID, LearnerID, EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return null;
            }

        }

        public int SaveEnvelopeId(int EnrollmentID, string EnvelopeId)
        {   
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveEnvelopeId( EnrollmentID, EnvelopeId);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }
        public string GetEnvelopeId(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetEnvelopeId(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return string.Empty;
            }
        }
        public int SaveStatusAfterDocuSignProcessComplete(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveStatusAfterDocuSignProcessComplete(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }
        public int SaveStatusAfterDocuSignProcessComplete(string EnvelopeId)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveStatusAfterDocuSignProcessComplete(EnvelopeId);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }
        public bool GetDocuSignedAffidavitStatus(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetDocuSignedAffidavitStatus(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        }
        public string GetCourseStatusByEnrollmentId(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseStatusByEnrollmentId(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return string.Empty;
            }
        }
        public int SaveCourseApprovalAffidavitStatus(bool courseApprovalAffidavitStatus, int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveAffidavitAcknowledgmentStatus(courseApprovalAffidavitStatus, EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }
        public bool GetAffidavitAcknowledgmentStatus(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetAffidavitAcknowledgmentStatus(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        }

        public CourseInfo GetCourseInformation(int EnrollmentID)
        {
            CourseInfo courseInfoData = new CourseInfo();
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseInformation(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return null;
            }

        }
        
        #endregion

        //Suggested Course Panel LCMS-11878
        public List<BusinessEntities.SuggestedCourse> GetCourseNameAgainstCourseGuids(List<string> courseGuids)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseNameAgainstCourseGuids(courseGuids);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
                
        public string GetCourseStoreId(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetCourseStoreId(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        //LCMS-11974 DocuSign Decline
        //Abdus Samad 
        //Start
        public int SaveStatusAfterDocuSignDecline(int EnrollmentID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SaveStatusAfterDocuSignDecline(EnrollmentID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }
        //Stop


        public int GetEnrollmentIdAgainstEnvelopeId(string EnvelopeId)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetEnrollmentIdAgainstEnvelopeId(EnvelopeId);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return 0;
            }
        }

        public bool SelectCourseApprovalMessage(string CourseID, string LearnerID, string CourseApprovalID, string learnerSessionGUID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.SelectCourseApprovalMessage(CourseID, LearnerID, CourseApprovalID, learnerSessionGUID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        
        
        
        }


        public bool GetMultipleQuizConfigurationCount(int courseID)
        {
            try
            {
                using (CourseDA courseDA = new CourseDA())
                {
                    return courseDA.GetMultipleQuizConfigurationCount(courseID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
                return false;
            }
        }


    }
}
//