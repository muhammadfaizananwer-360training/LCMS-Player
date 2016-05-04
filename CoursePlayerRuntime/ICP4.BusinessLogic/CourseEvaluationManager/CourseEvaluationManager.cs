using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CommunicationLogic.CommunicationCommand;
using System.Configuration;
using ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions;
using System.Net;


namespace ICP4.BusinessLogic.CourseEvaluation
{
    public class CourseEvaluationManager : IDisposable
    {
        public CourseEvaluationManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }

        #region Constants
        private const char QUESTIONSEPARATOR = ',';
        private const char ANSWERSEPARATOR = ',';
        private const char MULTIPLEANSWERCHOICESEPARATOR = ';';
        private const char QUESTIONTYPESEPARATOR = ',';

        #endregion

        public bool IsCourseEvaluationTaken(int courseID, int learnerID, int learningSessionID, string surveyType)
        {
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
            if (isPreview == true)
            {

                if (surveyType ==  _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                {
                    if (System.Web.HttpContext.Current.Session["IsSpecialQuestionnaireAttempted"] != null)
                    {return true;}
                    else
                    {return false;}
                }
                else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                {
                    if (System.Web.HttpContext.Current.Session["IsCourseEvaluationAttempted"] != null)
                    { return true; }
                    else
                    { return false; }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    try
                    {
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                        trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                        return  trackingService.IsCourseEvaluationAttempted(courseID, learnerID, learningSessionID, surveyType);
                    }
                    catch (Exception ex)
                    {
                        ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                        return false;
                    }
                }
            }
        }
        public ICPTrackingService.CourseEvaluationResult SetCourseEvaluationResultObject(ICPTrackingService.CourseEvaluationResult courseEvaluationResult, string[] questionIds, string[] answerIdsAndTexts, string[] questionTypes, int courseID, int surveyID, int learnerID, int learningSessionID, DateTime startDate)
        {
            try
            {
                List<ICPTrackingService.CourseEvaluationResultAnswer> courseEvaluationResultAnswers = new List<ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResultAnswer>();
                if (courseEvaluationResult.CourseEvaluationResultAnswers != null)
                    courseEvaluationResultAnswers.AddRange(courseEvaluationResult.CourseEvaluationResultAnswers);
                ICPTrackingService.CourseEvaluationResultAnswer courseEvaluationResultAnswer = null;

                for (int index = 0; index <= questionIds.Length - 1; index++)
                {
                    int currentQuestionId = Convert.ToInt32(questionIds[index]);
                    
                    if (questionTypes[index].Equals(CourseEvaluationQuestionType.MultiSelect))
                    {
                        String[] mssqAnswers = answerIdsAndTexts[index].Split(MULTIPLEANSWERCHOICESEPARATOR);
                        
                        for (int answerIndex = 0; answerIndex < mssqAnswers.Length; answerIndex++)
                        {
                            courseEvaluationResultAnswer = new ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResultAnswer();
                            courseEvaluationResultAnswer.CourseEvaluationQuestionID = currentQuestionId;
                            courseEvaluationResultAnswer.CourseEvaluationAnswerType = string.Empty;
                            if (mssqAnswers[answerIndex] != string.Empty && mssqAnswers[answerIndex] != null)
                            {
                                courseEvaluationResultAnswer.CourseEvaluationAnswerID = Convert.ToInt32(mssqAnswers[answerIndex]);
                            }
                            
                            courseEvaluationResultAnswers.Add(courseEvaluationResultAnswer);
                        }                        

                    }
                    else
                    {
                        courseEvaluationResultAnswer = new ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResultAnswer();
                        courseEvaluationResultAnswer.CourseEvaluationQuestionID = currentQuestionId;
                        courseEvaluationResultAnswer.CourseEvaluationAnswerType = string.Empty;

                        switch (questionTypes[index])
                        {
                            case CourseEvaluationQuestionType.FillInTheBlank:
                            case CourseEvaluationQuestionType.Text:
                                courseEvaluationResultAnswer.CourseEvaluationResultAnswerText = answerIdsAndTexts[index];
                                break;
                            case CourseEvaluationQuestionType.SingleSelect:
                                if (answerIdsAndTexts[index] != string.Empty && answerIdsAndTexts[index] != null)
                                    courseEvaluationResultAnswer.CourseEvaluationAnswerID = Convert.ToInt32(answerIdsAndTexts[index]);
                                break;
                        }
                        courseEvaluationResultAnswers.Add(courseEvaluationResultAnswer);
                    }
                }
                    
                courseEvaluationResult.CourseEvaluationResultAnswers = courseEvaluationResultAnswers.ToArray();
                courseEvaluationResult.CourseID = courseID;
                courseEvaluationResult.LearnerID = learnerID;
                courseEvaluationResult.SurveyID = surveyID;
                courseEvaluationResult.LearningSessionID = learningSessionID;
                //LCMS-4567
                courseEvaluationResult.StartDate = startDate;
                return courseEvaluationResult;
            }
            catch (Exception exception)
            {
                ExceptionPolicyForLCMS.HandleException(exception, "ICPException");
                throw;
            }

        }
        public object StartCourseEvaluation(ICPCourseService.SequenceItem sequenceItem, bool isNormalDirection, string surveyType)
        {
            
            int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int learnerSessionIDPrimary = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
            int numberOfQuestions = 0;
            bool isCourseEvaluationTaken = false;

            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                ICPCourseService.CourseConfiguration courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                isCourseEvaluationTaken = IsCourseEvaluationTaken(courseID, learnerID, learnerSessionIDPrimary, surveyType);
                using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                {
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    numberOfQuestions = courseService.GetCourseEvaluationQuestionsCount(courseID, surveyType);
                }

                if (isCourseEvaluationTaken == false && numberOfQuestions > 0)
                {

                    if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                    {
                        return CreateCourseEvaluationCommandObject(sequenceItem, !courseConfiguration.MustCompleteSpecialQuestionnaire, numberOfQuestions, courseConfiguration.SpecialQuestionnaireInstructions, surveyType);
                    }
                    if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                    {
                        return CreateCourseEvaluationCommandObject(sequenceItem, !courseConfiguration.PlayerMustCompleteCourseEvaluatio, numberOfQuestions, courseConfiguration.PlayerCourseEvaluationInstructions, surveyType);                 
                    }

                    return null;

                }
                else
                {
                    using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                    {
                        return courseManager.ResumeCourseFromCourseEvaluation(isNormalDirection);
                    }
                }
            }

        }
        private object CreateCourseEvaluationCommandObject(ICPCourseService.SequenceItem sequenceItem, bool isSkippable, int numberOfQuestions, string courseEvaluationIntructions, string surveyType)
        {


            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

            ICPCourseService.CourseEvaluation cEval = null;
            using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
            {
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                cEval = courseService.GetCourseEvaluationByCourseID(courseID, surveyType);
            }


            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();

            ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation.ShowCourseEvaluation showCourseEvaluation = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation.ShowCourseEvaluation();
            ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation.CourseEvaluation courseEvaluation = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluation.CourseEvaluation();
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {

                if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                {
                    courseEvaluation.ImageURL = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageSpecialQuestionnaireMessage, brandCode, variant);
                    courseEvaluation.Heading = cEval.Name;
                    courseEvaluation.CourseEvaluationStartButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSpecialQuestionnaireStartButton, brandCode, variant);
                }
                else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                {
                    courseEvaluation.ImageURL = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageCourseEvaluationMessage, brandCode, variant);
                    courseEvaluation.Heading = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingCourseEvaluationMessage, brandCode, variant);
                    courseEvaluation.CourseEvaluationStartButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentCourseEvaluationStartButton, brandCode, variant);
                }

                if (isSkippable)
                {

                    if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                    {
                        courseEvaluation.ContentText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSpecialQuestionnaireMessage, brandCode, variant);
                        courseEvaluation.CourseEvaluationSkipButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSpecialQuestionnaireSkipButton, brandCode, variant);
                    }
                    else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                    {

                        courseEvaluation.ContentText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSkippableCourseEvaluationMessage, brandCode, variant);
                        courseEvaluation.CourseEvaluationSkipButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentCourseEvaluationSkipButton, brandCode, variant);
                    }
                    courseEvaluation.IsSkippable = true;
                }
                else
                {
                    if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                    {
                        courseEvaluation.ContentText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSpecialQuestionnaireMessage, brandCode, variant);
                    }
                    else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                    {
                        courseEvaluation.ContentText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentNonSkippableCourseEvaluationMessage, brandCode, variant);
                    }
                    
                    courseEvaluation.CourseEvaluationSkipButton = string.Empty;
                    courseEvaluation.IsSkippable = false;
                }


                //if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                //{
                //    courseEvaluation.CourseEvaluationStartButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentSpecialQuestionnaireStartButton, brandCode, variant);
                //}
                //else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                //{
                //    courseEvaluation.CourseEvaluationStartButton = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentCourseEvaluationStartButton, brandCode, variant);
                //}
                

                StringBuilder sb = new StringBuilder(courseEvaluation.ContentText);
                if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                {
                    sb.Replace("$NumberOfQuestions", numberOfQuestions.ToString());
                }
                sb.Replace("$CourseEvalInstructs", courseEvaluationIntructions);
                courseEvaluation.ContentText = sb.ToString();
            }
            showCourseEvaluation.CourseEvaluation = courseEvaluation;
            showCourseEvaluation.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCourseEvaluation;
            return showCourseEvaluation;

        }
        #region Course Evaluation Questions
        public object LoadCourseEvaluationInSession()
        {


            ICPCourseService.CourseEvaluation courseEvaluation = null;
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);


            int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            ICPCourseService.SequenceItem sequenceItem = new CourseManager.CourseManager ().GetSequenceItem(courseID, seqNo);
            //bool isSpecialQuestionnaire = (sequenceItem.SequenceItemType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire);
            string surveyType = sequenceItem.SequenceItemType;

            using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
            {
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                courseEvaluation = courseService.GetCourseEvaluationByCourseID(courseID, surveyType);
            }
            //setting up the Session variables
            System.Web.HttpContext.Current.Session["CourseEvaluation"] = courseEvaluation;
            System.Web.HttpContext.Current.Session["CourseEvaluationIndex"] = 0;
            System.Web.HttpContext.Current.Session["CourseEvaluationResult"] = null;
            //LCMS-4567
            System.Web.HttpContext.Current.Session["CourseEvaluationStartDate"] = DateTime.Now;
            
            if (courseEvaluation.ShowAllTF > 0)
            {
                System.Web.HttpContext.Current.Session["CourseEvaluationIndex"] = courseEvaluation.CourseEvaluationQuestions.Length;
                //Get the question chunk
                List<ICPCourseService.CourseEvaluationQuestion> currentQuestionList = GetNextCourseEvaluationQuestionsChunk(0, courseEvaluation.CourseEvaluationQuestions.Length, courseEvaluation.CourseEvaluationQuestions);
                //return the Command Object
                return CreateCourseEvaluationQuestionsCommandObject(courseEvaluation.QuestionsPerPage, currentQuestionList,courseEvaluation.Name,0);
            }
            // we need to check the no. of questions
            else
            {
                //Get the question chunk
                List<ICPCourseService.CourseEvaluationQuestion> currentQuestionList = GetNextCourseEvaluationQuestionsChunk(0, courseEvaluation.QuestionsPerPage, courseEvaluation.CourseEvaluationQuestions);
                //This will be current index + current list count in other case (when it is from next questions call)
                // here this is 0 + list count
                System.Web.HttpContext.Current.Session["CourseEvaluationIndex"] = currentQuestionList.Count;
                //return the command object
                return CreateCourseEvaluationQuestionsCommandObject(courseEvaluation.QuestionsPerPage, currentQuestionList,courseEvaluation.Name,0);
            }

        }
        public object GetNextCourseEvaluationQuestionsList(string[] questiongIds, string[] answerIds, string[] questionTypes)
        {
            ICPCourseService.CourseEvaluation courseEvaluation = (ICPCourseService.CourseEvaluation)System.Web.HttpContext.Current.Session["CourseEvaluation"];
            int currentIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseEvaluationIndex"]);
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int surveyID = courseEvaluation.ID;
            int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);


            int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            ICPCourseService.SequenceItem sequenceItem = new CourseManager.CourseManager().GetSequenceItem(courseID, seqNo);
//            bool isSpecialQuestionnaire = (sequenceItem.SequenceItemType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire);
            string surveyType = sequenceItem.SequenceItemType;



            //LCMS-4567
            DateTime StartDate = Convert.ToDateTime(System.Web.HttpContext.Current.Session["CourseEvaluationStartDate"]);

            int learnerSessionIDPrimary = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
            try
            {
                if (System.Web.HttpContext.Current.Session["CourseEvaluationResult"] == null)
                {
                    System.Web.HttpContext.Current.Session["CourseEvaluationResult"] = SetCourseEvaluationResultObject(new ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult(), questiongIds, answerIds, questionTypes, courseID, surveyID, learnerID, learnerSessionIDPrimary, StartDate);
                }
                else
                {
                    System.Web.HttpContext.Current.Session["CourseEvaluationResult"] = SetCourseEvaluationResultObject((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)System.Web.HttpContext.Current.Session["CourseEvaluationResult"], questiongIds, answerIds, questionTypes, courseID, surveyID, learnerID, learnerSessionIDPrimary, StartDate);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            if (currentIndex == courseEvaluation.CourseEvaluationQuestions.Length)
            {
                if (!isPreview)
                {
                    //Submit the result
                    using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                    {
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                        trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                        trackingService.SaveCourseEvaluationResult((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)System.Web.HttpContext.Current.Session["CourseEvaluationResult"]);
                    }
                }
                else
                {
                    if (surveyType == _360Training.BusinessEntities.SequenceItemType.SpecialQuestionnaire)
                    {
                        System.Web.HttpContext.Current.Session["IsSpecialQuestionnaireAttempted"] = true;
                    }
                    else if (surveyType == _360Training.BusinessEntities.SequenceItemType.CourseEvaluation)
                    {
                        System.Web.HttpContext.Current.Session["IsCourseEvaluationAttempted"] = true;
                    }
                }
                return FinishCourseEvaluation();
            }
            else
            {
                //Get the question chunk
                List<ICPCourseService.CourseEvaluationQuestion> currentQuestionList = GetNextCourseEvaluationQuestionsChunk(currentIndex, courseEvaluation.QuestionsPerPage, courseEvaluation.CourseEvaluationQuestions);
                //This is current index + current list count
                System.Web.HttpContext.Current.Session["CourseEvaluationIndex"] = currentIndex + currentQuestionList.Count;
                //return the command object
                return CreateCourseEvaluationQuestionsCommandObject(courseEvaluation.QuestionsPerPage, currentQuestionList, courseEvaluation.Name, currentIndex);
            }
        }
        /// <summary>
        /// This method saves the course evaluaiton stats if course evaluation is completed
        /// </summary>
        /// <returns>retrun true if stats completed and saved,false if not completd or not saved or any exception</returns>
        public bool SaveCourseEvaluationStatsIfCompleted()
        {
            bool isCourseEvaluationCompleted = false;
            bool isCourseEvaluationSaved = false;

            try
            {
                if (System.Web.HttpContext.Current.Session["CourseEvaluation"] != null &&
                    System.Web.HttpContext.Current.Session["CourseEvaluationResult"] != null)
                {
                    isCourseEvaluationCompleted = IsCourseEvaluationCompleted((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)System.Web.HttpContext.Current.Session["CourseEvaluationResult"],
                                                (ICP4.BusinessLogic.ICPCourseService.CourseEvaluation)System.Web.HttpContext.Current.Session["CourseEvaluation"]);

                    if (isCourseEvaluationCompleted)
                    {
                        using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                        {
                            trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                            trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                            isCourseEvaluationSaved = trackingService.SaveCourseEvaluationResult((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)System.Web.HttpContext.Current.Session["CourseEvaluationResult"]);
                        }
                    }
                }
                else
                {
                    return false;
                }
                return isCourseEvaluationSaved;
            }
            catch (Exception exception)
            {
                ExceptionPolicyForLCMS.HandleException(exception, "ICPException");
                return false;
            }

        }
        /// <summary>
        /// This method checks wether the course evaluation is completed or not, the completion is determined if all response requuired questions are answered
        /// </summary>
        /// <param name="courseEvaluationResult"></param>
        /// <returns>retrun true if completed</returns>
        private bool IsCourseEvaluationCompleted(ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult courseEvaluationResult, ICPCourseService.CourseEvaluation courseEvaluation)
        {
            bool isFound = false;
            for (int courseEvaluationIndex = 0; courseEvaluationIndex <= courseEvaluation.CourseEvaluationQuestions.Length - 1; courseEvaluationIndex++)
            {
                if (courseEvaluation.CourseEvaluationQuestions[courseEvaluationIndex].Required == true)
                {
                    isFound = false;
                    for (int courseEvalResultIndex = 0; courseEvalResultIndex <= courseEvaluationResult.CourseEvaluationResultAnswers.Length - 1; courseEvalResultIndex++)
                    {
                        if (courseEvaluation.CourseEvaluationQuestions[courseEvaluationIndex].QuestionID == courseEvaluationResult.CourseEvaluationResultAnswers[courseEvalResultIndex].CourseEvaluationQuestionID)
                        {
                            isFound = true;
                            break;
                        }

                    }
                    if (isFound == false)
                        return false;
                }
            }
            return true;

        }
        /// <summary>
        /// Gets the number of questions based on the current index.
        /// It will return the remaining number of questions in case the no. of questions per page is less than the remaining count
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <param name="questionsPerPage"></param>
        /// <param name="courseEvaluationQuestionsListContainingAllQuestions"></param>
        /// <returns></returns>
        private List<ICPCourseService.CourseEvaluationQuestion> GetNextCourseEvaluationQuestionsChunk(int currentIndex, int questionsPerPage, ICPCourseService.CourseEvaluationQuestion[] courseEvaluationQuestionsListContainingAllQuestions)
        {
            List<ICPCourseService.CourseEvaluationQuestion> nextQuestionlist = new List<ICP4.BusinessLogic.ICPCourseService.CourseEvaluationQuestion>();
            int upperBoundIndex = currentIndex + questionsPerPage;
            if (upperBoundIndex >= courseEvaluationQuestionsListContainingAllQuestions.Length)
            {
                upperBoundIndex = courseEvaluationQuestionsListContainingAllQuestions.Length;
            }
            for (int count = currentIndex; count < upperBoundIndex; count++)
            {
                nextQuestionlist.Add(courseEvaluationQuestionsListContainingAllQuestions[count]);
            }
            return nextQuestionlist;
        }
        /// <summary>
        /// Creates show question command object
        /// </summary>
        /// <param name="numberOfQuestions"></param>
        /// <param name="currentCourseEvaluationQuestions"></param>
        /// <returns></returns>
        private object CreateCourseEvaluationQuestionsCommandObject(int numberOfQuestions, List<ICPCourseService.CourseEvaluationQuestion> currentCourseEvaluationQuestions, string courseEvaluationName, int currentIndex)
        {
            ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.ShowCourseEvaluationQuestions showCourseEvaluationQuestions = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.ShowCourseEvaluationQuestions();
            ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationRoot courseEvaluationRoot = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationRoot();
            courseEvaluationRoot.CourseEvaluationQuestions = TranslateCourseEvaluationQuestions(currentCourseEvaluationQuestions, currentIndex);
            courseEvaluationRoot.QuestionsPerPage = numberOfQuestions;
            courseEvaluationRoot.CourseEvaluationName = courseEvaluationName;
            showCourseEvaluationQuestions.CourseEvaluation = courseEvaluationRoot;
            showCourseEvaluationQuestions.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCourseEvaluationQuestions;
            return showCourseEvaluationQuestions;
        }
        private List<CourseEvaluationQuestions> TranslateCourseEvaluationQuestions(List<ICPCourseService.CourseEvaluationQuestion> webCourseEvaluationQuestions, int currentIndex)
        {
            currentIndex++;
            List<CourseEvaluationQuestions> clientCourseEvaluationList = new List<CourseEvaluationQuestions>();
            foreach (ICPCourseService.CourseEvaluationQuestion webCourseEvaluationQuestion in webCourseEvaluationQuestions)
            {
                CourseEvaluationQuestions clientCourseEvalQuestion = new CourseEvaluationQuestions();
                clientCourseEvalQuestion.Alignment = webCourseEvaluationQuestion.Alignment;
                clientCourseEvalQuestion.DisplayOrder = webCourseEvaluationQuestion.DisplayOrder;
                clientCourseEvalQuestion.Id = webCourseEvaluationQuestion.QuestionID;
                clientCourseEvalQuestion.Quetiontype = webCourseEvaluationQuestion.QuestionType;
                clientCourseEvalQuestion.Required = webCourseEvaluationQuestion.Required;
                clientCourseEvalQuestion.Text = webCourseEvaluationQuestion.QuestionText;
                clientCourseEvalQuestion.UnlimitedTF = Convert.ToBoolean(webCourseEvaluationQuestion.UnLimitedTF);
                clientCourseEvalQuestion.CourseEvaluationAnswer = TranslateCourseEvaluationAnswers(webCourseEvaluationQuestion.CourseEvaluationAnswers);
                clientCourseEvalQuestion.QuestionNo = currentIndex++;
                clientCourseEvaluationList.Add(clientCourseEvalQuestion);
            }
            return clientCourseEvaluationList;
        }
        private List<CourseEvaluationAnswer> TranslateCourseEvaluationAnswers(ICPCourseService.CourseEvaluationAnswer[] webCourseEvaluationAnswers)
        {
            List<CourseEvaluationAnswer> clientCourseEvaluationAnswerList = new List<CourseEvaluationAnswer>();
            foreach (ICPCourseService.CourseEvaluationAnswer webCourseEvaluationAnswer in webCourseEvaluationAnswers)
            {
                CourseEvaluationAnswer clientCourseEvalAnswer = new CourseEvaluationAnswer();
                clientCourseEvalAnswer.Displayorder = webCourseEvaluationAnswer.DisplayOrder.ToString();
                clientCourseEvalAnswer.Id = webCourseEvaluationAnswer.ID;
                clientCourseEvalAnswer.Label = webCourseEvaluationAnswer.Label;
                clientCourseEvalAnswer.Value = webCourseEvaluationAnswer.Value;
                clientCourseEvaluationAnswerList.Add(clientCourseEvalAnswer);
            }
            return clientCourseEvaluationAnswerList;
        }

        #endregion
        public object SkipCourseEvaluation()
        {
            return FinishCourseEvaluation();
        }
        private object FinishCourseEvaluation()
        {
            System.Web.HttpContext.Current.Session.Remove("CourseEvaluation");
            System.Web.HttpContext.Current.Session.Remove("CourseEvaluationIndex");
            System.Web.HttpContext.Current.Session.Remove("CourseEvaluationResult");
            using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
            {
                return courseManager.ResumeCourseFromCourseEvaluation(true);
            }
        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
