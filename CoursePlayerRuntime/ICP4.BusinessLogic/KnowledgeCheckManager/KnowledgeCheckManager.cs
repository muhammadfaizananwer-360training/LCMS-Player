using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CommunicationLogic.CommunicationCommand;
using ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion;
using System.Web;
using System.Net;
using System.Collections;


namespace ICP4.BusinessLogic.KnowledgeCheckManager
{
    public class KnowledgeCheckManager: IDisposable
    {
        public KnowledgeCheckManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }
        #region Initialize/Load methods
        public object StartKnowledgeCheck(ICPCourseService.SequenceItem sequenceItem)
        {
            bool isLoaded = false;
            try
            {
                isLoaded = LoadKnowledgeCheck(sequenceItem.sceneID);
                if (isLoaded)
                {
                    return CreateKnowledgeCheckStartCommandObject(sequenceItem);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                ExceptionPolicyForLCMS.HandleException(exception, "ICPException");
                UnloadKnowledgeCheck();
                return null;
            }
        }
        private bool LoadKnowledgeCheck(int sceneID)
        {
            bool isSuccessfull = false;
            try
            {
                ICPAssessmentService.AssessmentItem[] assessmentItems = GetKnowledgeCheckAssessmentItems(sceneID);
                if (assessmentItems != null && assessmentItems.Length > 0)
                {
                    System.Web.HttpContext.Current.Session["KnowledgeCheckAssessmentItems"] = assessmentItems;
                    System.Web.HttpContext.Current.Session["KnowledgeCheckSequenceNo"] = 0;
                    System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] = true;
                    isSuccessfull =true;
                }
                else
                {
                    isSuccessfull =false;
                }
                return isSuccessfull;
            }
            catch (Exception exception)
            {
                ExceptionPolicyForLCMS.HandleException(exception, "ICPException");
                throw;
            }
        }
        private ICPAssessmentService.AssessmentItem[] GetKnowledgeCheckAssessmentItems(int sceneID)
        {
            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
            {
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]); 
                return assessmentService.GetKnowledgeCheckAssessmentItems(sceneID);
            }
        }
        #endregion

        #region Finalize/ Unload Methods
        private object EndKnowledgeCheck()
        {
            UnloadKnowledgeCheck();
            using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
            {
                return courseManager.ResumeCourseFromKnowledgeCheck();
            }
            
        }
        private void UnloadKnowledgeCheck()
        {
            System.Web.HttpContext.Current.Session.Remove("KnowledgeCheckAssessmentItems");
            System.Web.HttpContext.Current.Session.Remove("KnowledgeCheckSequenceNo");
            System.Web.HttpContext.Current.Session.Remove("KnowledgeCheckInProgress");
        }
        #endregion

        #region Execution/Play Methods
        
        public object SubmitQuestionResult(int assessmentItemID, List<int> studentAssessmentAnswerIDs, List<string> studentAssessmentAnswerStrings)
        {
            string feedback = "";
            int questionIndex = 0;
            bool corrrectlyAnswered = false;
            object returnObject = new object();
            int assessmentSequenceNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["KnowledgeCheckSequenceNo"]);
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["KnowledgeCheckAssessmentItems"];
            bool endAssessment = assessmentSequenceNo == assessmentItems.Length ? true : false;

            corrrectlyAnswered = CheckQuestionResult(assessmentItemID, studentAssessmentAnswerIDs, studentAssessmentAnswerStrings, ref questionIndex);
            /*if (!corrrectlyAnswered)
            {
                feedback = assessmentItems[questionIndex].Feedback;
            }*/
            using(AssessmentManager.AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
            {
                feedback = assessmentManager.GetFeedback(assessmentItems, studentAssessmentAnswerIDs, studentAssessmentAnswerStrings, questionIndex, corrrectlyAnswered);
            }
            #region Old Logic
            /*
            if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.Single))
            {
                feedback = assessmentItems[questionIndex].Feedback;
            }
            else if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.CorrectIncorrect))
            {
                if (corrrectlyAnswered)
                {
                    feedback = assessmentItems[questionIndex].Feedback;
                }
                else
                {
                    feedback = assessmentItems[questionIndex].Incorrectfeedback;
                }
            }
            else if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.AnswerChoice))
            {
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Ordering))
                {
                    #region "Ordering"
                    for (int i = 0; i < studentAssessmentAnswerIDs.Count; i++)
                    {
                        for (int j = 0; j < assessmentItems[questionIndex].AssessmentAnswers.Length; j++)
                        {
                            if (studentAssessmentAnswerIDs[i].Equals(assessmentItems[questionIndex].AssessmentAnswers[j].AssessmentItemAnswerID))
                            {
                                feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[j].Feedback + "</p>";
                                break;
                            }
                        }
                    }
                    #endregion
                }
                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    #region "FITB"
                    ArrayList array = new ArrayList(assessmentItems[questionIndex].AssessmentAnswers.Length);

                    for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                    {
                        array.Add(assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID);
                    }
                    array.Sort();

                    for (int index = 0; index < array.Count; index++)
                    {
                        for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                        {
                            if (array[index].Equals(assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID))
                            {
                                if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                                {
                                    feedback += assessmentItems[questionIndex].Feedback;
                                    break;
                                }
                                else
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[i].Feedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Feedback + "</p>";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    for (int index = 0; index < studentAssessmentAnswerIDs.Count; index++)
                    {
                        for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID.Equals(studentAssessmentAnswerIDs[index]))
                            {
                                if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p>";
                                    break;
                                }
                                else
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[i].Feedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Feedback + "</p>";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.AnswerChoiceCorrectIncorrect))
            {
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    #region Fill in the blank Question

                    bool isCaseSensitive = ((ICPAssessmentService.FillInTheBlankQuestion)assessmentItems[questionIndex]).IsAnswerCaseSensitive;
                    bool isCorrect = false;

                    for (int index = 0; index < studentAssessmentAnswerStrings.Count; index++)
                    {
                        if (assessmentItems[questionIndex].AssessmentAnswers[index].Usedefaultfeedbacktf)
                        {
                            feedback += assessmentItems[questionIndex].Feedback;
                        }
                        else
                        {
                            isCorrect = false;
                            for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                            {
                                if (String.Compare(studentAssessmentAnswerStrings[i], assessmentItems[questionIndex].AssessmentAnswers[index].Label, !isCaseSensitive) == 0)
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[index].Correctfeedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[index].Correctfeedback + "</p>";
                                        isCorrect = true;
                                        break;
                                    }
                                }
                            }
                            if (!isCorrect)
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[index].Incorrectfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[index].Incorrectfeedback + "</p>";
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    int studentAnswerOrder = 0;
                    for (int index = 0; index < studentAssessmentAnswerIDs.Count; index++)
                    {
                        studentAnswerOrder++;
                        for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID.Equals(studentAssessmentAnswerIDs[index]))
                            {
                                //Below check for Default Feedback
                                if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p>";
                                    break;
                                }

                                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Ordering))
                                {
                                    #region Ordering
                                    if (((ICPAssessmentService.OrderingAssessmentItemAnswer)assessmentItems[questionIndex].AssessmentAnswers[i]).CorrectOrder == studentAnswerOrder)
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p>";
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching))
                                {
                                    #region "Matching"
                                    if (((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItems[questionIndex].AssessmentAnswers[i]).RightItemText.Equals(studentAssessmentAnswerStrings[i]))
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p>";
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region "Others"

                                    if (assessmentItems[questionIndex].AssessmentAnswers[i].IsCorrect)
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p>";
                                            break;
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                }

            }
             **/
            #endregion Old Logic

            using (AssessmentManager.AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
            {
                returnObject = assessmentManager.CreateQuestionResultCommandObject(corrrectlyAnswered, feedback,endAssessment);
            }
            return returnObject;

        }
        public bool CheckQuestionResult(int assessmentItemID, List<int> studentAssessmentAnswerIDs, List<string> studentAssessmentAnswerStrings, ref int questionIndex)
        {
            bool correctlyAnswered = false;
            //Get data from session related to asked question
            #region Getting data for asked Question

            questionIndex = 0;
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["KnowledgeCheckAssessmentItems"];
            ICPAssessmentService.AssessmentItem assessmentItem = new ICPAssessmentService.AssessmentItem();
            for (questionIndex = 0; questionIndex < assessmentItems.Length; questionIndex++)
            {
                if (assessmentItemID == assessmentItems[questionIndex].AssessmentItemID)
                {
                    assessmentItem = assessmentItems[questionIndex];
                    break;
                }
            }
            #endregion

            //Check answer if required and create an returning object based on policy
            #region Answer Checking

            if (assessmentItem.QuestionType == QuestionType.TrueFalse || assessmentItem.QuestionType == QuestionType.SingleSelectMCQ || assessmentItem.QuestionType == QuestionType.ImageTarget)
            {
                #region Checking Single choice / True False / ImageTarget  Question
                foreach (int studentAnswerID in studentAssessmentAnswerIDs)
                {
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        if (studentAnswerID == assessmentItemAnswer.AssessmentItemAnswerID && assessmentItemAnswer.IsCorrect)
                        {
                            correctlyAnswered = true;
                            break;
                        }
                    }
                    if (correctlyAnswered)
                    {
                        break;
                    }
                }
                #endregion
            }
            else if (assessmentItem.QuestionType == QuestionType.MultipleSelectMCQ)
            {
                #region Checking Multiple Choice Question

                int thisOptionIsCorrect = 0;
                int totalCorrectOptions = 0;

                foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                {
                    if (assessmentItemAnswer.IsCorrect)
                    {
                        totalCorrectOptions++;
                    }
                }

                foreach (int studentAnswerID in studentAssessmentAnswerIDs)
                {
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        if (studentAnswerID == assessmentItemAnswer.AssessmentItemAnswerID && assessmentItemAnswer.IsCorrect)
                        {
                            thisOptionIsCorrect++;
                            break;
                        }
                    }
                }

                if (studentAssessmentAnswerIDs.Count == thisOptionIsCorrect && thisOptionIsCorrect == totalCorrectOptions)
                    correctlyAnswered = true;
                else
                    correctlyAnswered = false;


                #endregion
            }
            else if (assessmentItem.QuestionType == QuestionType.Ordering)
            {
                #region Checking Ordering Question
                int studentAnswerOrder = 0;
                int thisOptionIsCorrect = 0;
                foreach (int studentAnswerID in studentAssessmentAnswerIDs)
                {
                    studentAnswerOrder++;
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        if (studentAnswerID == assessmentItemAnswer.AssessmentItemAnswerID && ((ICPAssessmentService.OrderingAssessmentItemAnswer)assessmentItemAnswer).CorrectOrder == studentAnswerOrder)
                        {
                            thisOptionIsCorrect++;
                            break;
                        }
                    }
                }
                if (thisOptionIsCorrect == assessmentItem.AssessmentAnswers.Length)
                {
                    correctlyAnswered = true;
                }
                else
                {
                    correctlyAnswered = false;
                }
                #endregion
            }
            else if (assessmentItem.QuestionType == QuestionType.TextInputFITB)
            {
                #region Checking Fill in the blank Question
                //Old Logic
                /*
                // int thisOptionIsCorrect = 0;
                // int totalCorrectOptions = 0;
                bool isCaseSensitive = ((ICPAssessmentService.FillInTheBlankQuestion)assessmentItem).IsAnswerCaseSensitive;

                //totalCorrectOptions = assessmentItem.AssessmentAnswers.Length;

                //int stundetAnswerIndex = 0;
                //int actualAnswerIndex = 0;
                foreach (string studentAnswerText in studentAssessmentAnswerStrings)
                {
                    //  stundetAnswerIndex++;
                    // actualAnswerIndex = 0;
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        //  actualAnswerIndex++;
                        if (String.Compare(studentAnswerText, assessmentItemAnswer.Label, !isCaseSensitive) == 0) //&& stundetAnswerIndex == actualAnswerIndex)
                        {
                            correctlyAnswered = true;
                            //thisOptionIsCorrect++;
                            break;
                        }
                    }
                    if (correctlyAnswered == true)
                        break;
                }

                //if (studentAssessmentAnswerStrings.Count == thisOptionIsCorrect && thisOptionIsCorrect == totalCorrectOptions)
                //    correctlyAnswered = true;
                //else
                //    correctlyAnswered = false;

                */
                bool isCaseSensitive = ((ICPAssessmentService.FillInTheBlankQuestion)assessmentItem).IsAnswerCaseSensitive;

                int totalCorrectOptions = assessmentItem.AssessmentAnswers.Length;
                int stundentSelectedAnswers = 0;

                if (assessmentItem.AssessmentAnswers.Length == studentAssessmentAnswerStrings.Count)
                {
                    for (int i = 0; i < assessmentItem.AssessmentAnswers.Length; i++)
                    {

                        if (String.Compare(studentAssessmentAnswerStrings[assessmentItem.AssessmentAnswers[i].DisplayOrder - 1], assessmentItem.AssessmentAnswers[i].Label, !isCaseSensitive) == 0)
                        {
                            stundentSelectedAnswers++;
                        }
                    }
                }

                if (totalCorrectOptions.Equals(stundentSelectedAnswers))
                {
                    correctlyAnswered = true;
                }
                #endregion
            }
            else if (assessmentItem.QuestionType == QuestionType.Matching)
            {
                #region Checking Matching Question

                int thisOptionIsCorrect = 0;
                int totalCorrectOptions = 0;

                totalCorrectOptions = assessmentItem.AssessmentAnswers.Length;

                for (int studentAnswerIndex = 0; studentAnswerIndex < studentAssessmentAnswerIDs.Count; studentAnswerIndex++)
                {
                    if (studentAssessmentAnswerStrings.Count > studentAnswerIndex)
                    {
                        if (studentAssessmentAnswerIDs[studentAnswerIndex] == ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItem.AssessmentAnswers[studentAnswerIndex]).AssessmentItemAnswerID && studentAssessmentAnswerStrings[studentAnswerIndex] == ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItem.AssessmentAnswers[studentAnswerIndex]).RightItemText)
                            thisOptionIsCorrect++;
                    }
                }

                if (thisOptionIsCorrect == totalCorrectOptions)
                    correctlyAnswered = true;
                else
                    correctlyAnswered = false;


                #endregion
            }


            #endregion


            return correctlyAnswered;
        }
        public object GetNextKnowledgeCheckQuestion()
        {
            object returnObject = new object();

            int assessmentSequenceNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["KnowledgeCheckSequenceNo"]);
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["KnowledgeCheckAssessmentItems"];
            if (assessmentSequenceNo <= assessmentItems.Length - 1)
            {
                ICPAssessmentService.AssessmentItem assessmentItem = new ICPAssessmentService.AssessmentItem();
                assessmentItem = assessmentItems[assessmentSequenceNo];
                System.Web.HttpContext.Current.Session["KnowledgeCheckSequenceNo"] = assessmentSequenceNo + 1;

                using (AssessmentManager.AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
                {
                    string audioURL = string.Empty;
                    string templatetype = string.Empty;
                    string visualTopType = string.Empty;
                    string assessmentItemTemplate = assessmentManager.GetAssessmentItemTemplate(assessmentItem.AssessmentItemID, out audioURL, out templatetype, out visualTopType);

                    returnObject = assessmentManager.CreateQuestionCommandObject(assessmentItem, assessmentSequenceNo + 1, assessmentItems.Length, null, false, false, false, "", assessmentItemTemplate, audioURL, templatetype, visualTopType, "");
                }
                return returnObject;
            }
            else
            {
                return EndKnowledgeCheck();
            }
         
        }
        #endregion

        #region Communication Command creation methods
        /// <summary>
        /// This method creates the command object for knowledge check start message
        /// </summary>
        /// <param name="sequenceItem">ICPCourseService.SequenceItem sequenceItem</param>
        /// <returns></returns>
        private object CreateKnowledgeCheckStartCommandObject(ICPCourseService.SequenceItem sequenceItem)
        {
            ICP4.CommunicationLogic.CommunicationCommand.ShowSlide.ShowSlide showSlide = new ICP4.CommunicationLogic.CommunicationCommand.ShowSlide.ShowSlide();
            ICP4.CommunicationLogic.CommunicationCommand.ShowSlide.SlideMediaAsset slideMediaAsset = new ICP4.CommunicationLogic.CommunicationCommand.ShowSlide.SlideMediaAsset();

         
            int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            int courseID=Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

            //slideMediaAsset.IsJSPlayerEnabled = System.Configuration.ConfigurationManager.AppSettings["isJSPlayerEnabled"];
            slideMediaAsset.IsJSPlayerEnabled = CheckForJSEnabledForCourse(Convert.ToString(courseID));
 
            slideMediaAsset.MediaAssetID = sequenceItem.Item_GUID;
            slideMediaAsset.MediaAssetType = CourseManager.SequenceItemTypeName.ContentAsset;
            slideMediaAsset.MediaAssetSceneID = sequenceItem.SceneGUID;
            slideMediaAsset.ContentObjectID = sequenceItem.ParentID;
            slideMediaAsset.RemidiationMode = false;
            slideMediaAsset.FlashSceneNo = "";
            slideMediaAsset.FlashURL = "";
            slideMediaAsset.IsMovieEnded = false;
            slideMediaAsset.LastScene = "";
            slideMediaAsset.NextButtonState = true;
            slideMediaAsset.SceneDurationTimer = 0;
            slideMediaAsset.ShowBookMark = false;
            slideMediaAsset.IsAssessmentStartMessage = true;
            slideMediaAsset.AllowSkipping = false;
            slideMediaAsset.ShowGradeAssessment = false;
            slideMediaAsset.IsRestrictiveAssessmentEngine = false;
            slideMediaAsset.AssessmentTimer = -1;

            #region Course Progress Bar
            using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
            {
                //int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentindex"]);
                string courseName = Convert.ToString(HttpContext.Current.Session["CourseName"]);
                double percentageCourseProgress = 0;
                {
                    try
                    {
                        int visitedSceneCount;
                        int totalSceneCount;
                        percentageCourseProgress = courseManager.CalculateCourseProgressPercentageFromSequence(courseID, seqNo, Convert.ToInt32(System.Web.HttpContext.Current.Session["TotalNoOfViewableScenes"]), out visitedSceneCount, out totalSceneCount);

                        if (percentageCourseProgress > 0)
                        {
                            slideMediaAsset.CourseProgressPercentage = Convert.ToInt32(Math.Round(percentageCourseProgress));
                            slideMediaAsset.CourseProgressToolTip = courseManager.GetToolTipForProgressBar(courseID, courseName, seqNo, totalSceneCount, visitedSceneCount);
                        }
                    }
                    catch (Exception exception)
                    {
                        ExceptionPolicyForLCMS.HandleException(exception, "ICPException");
                        slideMediaAsset.CourseProgressPercentage = 0;
                        slideMediaAsset.CourseProgressToolTip = string.Empty;
                    }
                }
            }
            #endregion

            string HTML = "";
            string icpFileSystem = ConfigurationManager.AppSettings["ICPFileSystem"];
            using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
            {
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                HTML = courseService.GetSceneTemplateHTML(sequenceItem.SceneTemplateID).TemplateHTML;
            }
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                StringBuilder sb = new StringBuilder(HTML);
                sb.Replace("$Heading", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingKnowledgeCheck, brandCode, variant));
                sb.Replace("$VisualTop", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageKnowledgeCheck, brandCode, variant));
                sb.Replace("$Text", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentKnowledgeCheck, brandCode, variant));
                sb.Replace("$StartButton", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentKnowledgeCheckStartButton, brandCode, variant));

                HTML = sb.ToString();
            }

            slideMediaAsset.TemplateHtml = HTML;
            slideMediaAsset.TemplateHtml = slideMediaAsset.TemplateHtml.Replace("\n", "");

            showSlide.MediaAsset = slideMediaAsset;
            showSlide.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowSlide;
            return showSlide;
        }

        public bool CheckForJSEnabledForCourse(string courseId)
        {
            string strValue = ConfigurationManager.AppSettings["JSPlayerCourseIDs"];

            bool result = false;

            if (strValue.Contains(courseId))
            {

                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}

