using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CommunicationLogic.CommunicationCommand;
using ICP4.BusinessLogic.CacheManager;
using ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion;
using ICP4.CommunicationLogic.CommunicationCommand.ShowQuestionResult;
using ICP4.CommunicationLogic.CommunicationCommand.ShowSkippedQuestion;
using ICP4.CommunicationLogic.CommunicationCommand.ShowAssessmentScoreSummary;
using ICP4.CommunicationLogic.CommunicationCommand.ShowAnswerReview;
using ICP4.CommunicationLogic.CommunicationCommand.ShowTimerExpiredMessage;
using ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore;
using ICP4.CommunicationLogic.CommunicationCommand.ShowTopicScoreSummary;
using CommonAPI.Utility;
using ICP4.BusinessLogic.CourseManager;
using ICP4.BusinessLogic.IntegerationManager;

namespace ICP4.BusinessLogic.AssessmentManager
{
    public class AssessmentManager : IDisposable
    {
        public AssessmentManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }

        /// <summary>
        /// This method gets question for Pre/Post assessment from Asessment services and put it into session
        /// </summary>
        /// <param name="courseID">CourseID integer value</param>
        /// <param name="learnerSessionID">LearnerSessionID string value</param>
        /// <param name="courseConfiguration">CourseConfiguration</param>
        /// <param name="assessmentType">AssessmentType</param>
        /// <returns>Return true in case of success, fasle otherwise</returns>
        public bool LoadPreOrPostAssessmentIntoSession(int courseID, string learnerSessionID, ICPAssessmentService.CourseConfiguration courseConfiguration, string assessmentType, int ExamID, int totalQuestions,ref int modifiedAttemptNumber)
        {
            try
            {
                //Get question from assessment webservice
                ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService();
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                ICPAssessmentService.AssessmentItem[] assessmentItems = null;
                //Get previously asked question list from tracking webservice
                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                string[] assessmentItemGUIDs = null;

                // LCMS-9213
                //List<string> askedAssessmentItemsAttributes = new List<string>(); // [AnsweredCorrectly|AnswerProvided] // LCMS-9213
                List<ICPTrackingService.LearnerStatistics> askedAssessmentItemsAttributes = new List<ICPTrackingService.LearnerStatistics>(); 
                
                int lastAttemptNumber = 1;
                if (HttpContext.Current.Session["LastAssessmentAttemptNo"] != null)
                {
                    int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out lastAttemptNumber);
                    lastAttemptNumber = lastAttemptNumber - 1;
                }
                // Changed by Waqas Zakai
                // LCMS-6984
                // START
                if ((assessmentType == CourseManager.LearnerStatisticsType.PreAssessment) || (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment && !(courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) && !(courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)))
	            {                
					if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsPreview"]) == false)
	                {
	                    //Get previously asked question list from tracking webservice
	                    //ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
	                    //trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
	                    //trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
	                    ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestions(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]),0);
	                    assessmentItemGUIDs = new string[learnerStatisticList.Length];
                        //askedAssessmentItemsAttributes = new string[learnerStatisticList.Length]; // LCMS-9213
	                    int i = 0;
                        int j = 0;
	                    //Create list for assessment Service
	                    foreach (ICPTrackingService.LearnerStatistics learnerStatistic in learnerStatisticList)
                        {
                            // Fix for LCMS-8289
                            //-----------------------------------------------------------------
                            //assessmentItemGUIDs[i++] = learnerStatistic.Item_GUID;
                            if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                            {
                                if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                {
                                    assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                }
                            }
                            else
                            {
                                assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                            }
                            //-----------------------------------------------------------------

                            // LCMS-9213
                            if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                            {
                                if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                                {
                                    if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                    {
                                        ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                        ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                        ls.CorrectTF = learnerStatistic.CorrectTF;
                                        ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                        ls.QuestionType = learnerStatistic.QuestionType;
                                        ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                        ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                        ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                        //Added By Abdus Samad 
                                        //lcms-12105
                                        //Start
                                        ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                        //Stop
                                        askedAssessmentItemsAttributes.Add(ls);
                                    }
                                }
                                else
                                {
                                    ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                    ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                    ls.CorrectTF = learnerStatistic.CorrectTF;
                                    ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                    ls.QuestionType = learnerStatistic.QuestionType;
                                    ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                    ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                    ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                    //Added By Abdus Samad 
                                    //lcms-12105
                                    //Start
                                    ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                    //Stop
                                    askedAssessmentItemsAttributes.Add(ls);
                                }


                                //askedAssessmentItemsAttributes.Add(learnerStatistic.AssessmentItemID + "|" + learnerStatistic.CorrectTF.ToString().ToLower() + "|" + learnerStatistic.AnswerProvided.ToString().ToLower() + "|" + learnerStatistic.QuestionType.ToString() + "|" + learnerStatistic.AnswerTexts.ToString() + "|" + learnerStatistic.CorrectAnswerGuids.ToString());
                            }
                        }


                        if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                        {
                            modifiedAttemptNumber = lastAttemptNumber;
                            HttpContext.Current.Session["RandomAlternateWithPauseResume"] = true;
                        }

                        if (assessmentType == CourseManager.LearnerStatisticsType.PreAssessment)
                        {
                            assessmentItems = assessmentService.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs, ExamID);
                        }
                        else if (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment)
                        {
                            assessmentItems = assessmentService.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs, ExamID);
                        }
	                }
	                else
	                {
	                    if (assessmentType == CourseManager.LearnerStatisticsType.PreAssessment)
	                    {
                            //courseConfiguration.PreAssessmentConfiguration.AdvanceQuestionSelectionType = ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_MINMAX;
                            //assessmentItems = assessmentService.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs);
                            if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"] != null)
	                        {
	                            if (courseConfiguration.PreAssessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
	                            {
	                                assessmentItemGUIDs = new string[((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"]).Count];
	                                assessmentItemGUIDs = ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"]).ToArray();
	                            }
	                            else
	                            {
	                                assessmentItemGUIDs = new string[0];
	                            }
	                        }
	                        else                        
	                        {
	                            assessmentItemGUIDs = new string[0];
	                        }
	                    }
	                    else if (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment)
	                    {
                            //assessmentItems = assessmentService.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs);
	                        if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"] != null)
	                        {
	                            if (courseConfiguration.PostAssessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
	                            {
	                                assessmentItemGUIDs = new string[((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"]).Count];
	                                assessmentItemGUIDs = ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"]).ToArray();
	                            }
	                            else
	                            {
	                                assessmentItemGUIDs = new string[0];
	                            }
	                        }
	                        else
	                        {
	                            assessmentItemGUIDs = new string[0];
	                        }
	                    }

                        if (assessmentType == CourseManager.LearnerStatisticsType.PreAssessment)
                        {
                            assessmentItems = assessmentService.GetPreAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs, ExamID);
                        }
                        else if (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment)
                        {
                            assessmentItems = assessmentService.GetPostAssessmentAssessmentItems(courseID, courseConfiguration, assessmentItemGUIDs, ExamID);
                        }                        
                    }
				}
                // END         
                else if (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment && courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE)
                {
                    bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);

                    if (isPreview)
                    {
                            
                        string askedBanks = Convert.ToString(HttpContext.Current.Session["AskedAssessmentItemBanksForRandomAlternate"]);


                        object[] arr = assessmentService.GetAssessmentItemsForRandomAlternateInPreviewMode(askedBanks, courseConfiguration.PostAssessmentConfiguration);
                        string currentBank = arr[0].ToString();


                        if (askedBanks != "")
                        {
                            askedBanks += ",";
                        }

                        askedBanks += currentBank;
                        HttpContext.Current.Session["AskedAssessmentItemBanksForRandomAlternate"] = askedBanks;
                        assessmentItems = (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[])arr[1];
                    }
                    else
                    {
                        ///// Code added for  LCMS-10266// Same logic as LCMS-9213
                        bool isresumeCase = false;
                        if (courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True")
                        {
                            ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestions(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]),0);
                            assessmentItemGUIDs = new string[learnerStatisticList.Length];
                            int i = 0;
                            int j = 0;
                            foreach (ICPTrackingService.LearnerStatistics learnerStatistic in learnerStatisticList)
                            {
                                if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                                {
                                    if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                    {
                                        assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                    }
                                }
                                else
                                {
                                    assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                }

                                if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                                {
                                    if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                                    {
                                        if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                        {
                                            ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                            ls.AssessmentItemID = learnerStatistic.AssessmentItemID;                                            
                                            ls.CorrectTF = learnerStatistic.CorrectTF;
                                            ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                            ls.QuestionType = learnerStatistic.QuestionType;
                                            ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                            ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                            ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                            //Added By Abdus Samad 
                                            //lcms-12105
                                            //Start
                                            ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                            //Stop
                                            askedAssessmentItemsAttributes.Add(ls);
                                        }
                                    }
                                    else
                                    {
                                        ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                        ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                        ls.CorrectTF = learnerStatistic.CorrectTF;
                                        ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                        ls.QuestionType = learnerStatistic.QuestionType;
                                        ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                        ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                        ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                        //Added By Abdus Samad 
                                        //lcms-12105
                                        //Start
                                        ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                        //Stop
                                        askedAssessmentItemsAttributes.Add(ls);
                                    }


                                    //askedAssessmentItemsAttributes.Add(learnerStatistic.AssessmentItemID + "|" + learnerStatistic.CorrectTF.ToString().ToLower() + "|" + learnerStatistic.AnswerProvided.ToString().ToLower() + "|" + learnerStatistic.QuestionType.ToString() + "|" + learnerStatistic.AnswerTexts.ToString() + "|" + learnerStatistic.CorrectAnswerGuids.ToString());
                                }

                                
                                //if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                                //{
                                //    ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                //    ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                //    ls.CorrectTF = learnerStatistic.CorrectTF;
                                //    ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                //    ls.QuestionType = learnerStatistic.QuestionType;
                                //    ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                //    ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                //    ls.TimeInSeconds = learnerStatistic.TimeInSeconds;

                                //    askedAssessmentItemsAttributes.Add(ls);
                                //}
                            }
                            ICPTrackingService.LearnerStatistics[] learnerStatisticsAssessment = trackingService.GetPostAssessmentResult(learnerSessionID);// Fix for  	LCMS-11086 Added check for rawscore =-2 in below condition
                            bool pauseCase=false;
                            if (learnerStatisticsAssessment!=null && learnerStatisticsAssessment.Length>0&& learnerStatisticsAssessment[learnerStatisticsAssessment.Length - 1].RawScore == -2)
                            {
                                pauseCase = true;
                            }
                            // End Fix for LCMS-11086 Added check for rawscore =-2 in below condition
                            if ((totalQuestions > askedAssessmentItemsAttributes.Count && askedAssessmentItemsAttributes.Count > 0 && assessmentItemGUIDs != null && assessmentItemGUIDs.Length > 0) || (pauseCase || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True"))
                            {
                                // As we do not want to increment the attempt number in this specific case so updating the session variable by seting the flag
                                modifiedAttemptNumber = lastAttemptNumber;
                                //if(System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="False")
                                    HttpContext.Current.Session["RandomAlternateWithPauseResume"] = true;
                                if (askedAssessmentItemsAttributes.Count > 0)
                                    isresumeCase = true;
                            }
                        }
                        ///////end code added for LCMS-10266

                        assessmentItems = assessmentService.GetAssessmentItemsForRandomAlternate(learnerSessionID, courseConfiguration.PostAssessmentConfiguration, isresumeCase);
                    }
                }
                // Random Alternate With Multiple Item Banks
                else if (assessmentType == CourseManager.LearnerStatisticsType.PostAssessment && courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)
                {
                    # region Hard Coded AssessmentItems
                    //List<ICPAssessmentService.AssessmentItem> assessmentItemsList = new List<ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem>(); 
                    //ICPAssessmentService.AssessmentItem[] assessmentItems1 = assessmentService.TestGetAssessmentItemsForBankID(25479);
                    //assessmentItems1 = assessmentService.TestGetAssessmentItemsForBankID(25479);
                    //foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems1)
                    //{
                    //    assessmentItem.AssessmentBinderID = 25479;
                    //    assessmentItem.AssessmentBinderName = "ZAK_9622_Blank";
                    //    assessmentItem.ScoreWeight = 0.5;
                    //    assessmentItemsList.Add(assessmentItem);
                    //}
                    

                    //ICPAssessmentService.AssessmentItem[] assessmentItems2 = assessmentService.TestGetAssessmentItemsForBankID(25325);
                    //foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems2)
                    //{
                    //    assessmentItem.AssessmentBinderID = 25325;
                    //    assessmentItem.AssessmentBinderName = "ZAK_4.9_BANK_2";
                    //    assessmentItem.ScoreWeight = 0.4;
                    //    assessmentItemsList.Add(assessmentItem);                          
                    //}

                    //ICPAssessmentService.AssessmentItem[] assessmentItems3 = assessmentService.TestGetAssessmentItemsForBankID(25324);
                    //foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems3)
                    //{
                    //    assessmentItem.AssessmentBinderID = 25324;
                    //    assessmentItem.AssessmentBinderName = "ZAK_4.9_BANK_1";
                    //    assessmentItem.ScoreWeight = 0.1;
                    //    assessmentItemsList.Add(assessmentItem);
                    //}
                    //assessmentItems = assessmentItemsList.ToArray();
                    #endregion

                    bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);

                    if (isPreview)
                    {

                        string askedTests = Convert.ToString(HttpContext.Current.Session["AskedAssessmentItemBanksForRandomAlternateMultipleItemBank"]);


                        object[] arr = assessmentService.GetTestForRandomAlternateMultipleItemBankInPreviewMode(askedTests, courseConfiguration.PostAssessmentConfiguration);
                        string currentBank = arr[0].ToString();


                        if (askedTests != "")
                        {
                            askedTests += ",";
                        }

                        askedTests += currentBank;
                        HttpContext.Current.Session["AskedAssessmentItemBanksForRandomAlternateMultipleItemBank"] = askedTests;
                        assessmentItems = (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[])arr[1];
                    }
                    else
                    {
                        ///// Code added for  LCMS-10266//Same logic as LCMS-9213
                        bool isresumeCase = false;
                        if (courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True")
                        {
                            ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestions(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]),0);
                            assessmentItemGUIDs = new string[learnerStatisticList.Length];
                            int i = 0;
                            int j = 0;
                            foreach (ICPTrackingService.LearnerStatistics learnerStatistic in learnerStatisticList)
                            {
                                if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                                {
                                    if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                    {
                                        assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                    }
                                }
                                else
                                {
                                    assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                }

                                if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                                {
                                    if (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString().Equals("True"))
                                    {
                                        if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                                        {
                                            ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                            ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                            ls.CorrectTF = learnerStatistic.CorrectTF;
                                            ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                            ls.QuestionType = learnerStatistic.QuestionType;
                                            ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                            ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                            ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                            //Added By Abdus Samad 
                                            //lcms-12105
                                            //Start
                                            ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                            //Stop
                                            askedAssessmentItemsAttributes.Add(ls);
                                        }
                                    }
                                    else
                                    {
                                        ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                        ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                        ls.CorrectTF = learnerStatistic.CorrectTF;
                                        ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                        ls.QuestionType = learnerStatistic.QuestionType;
                                        ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                        ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                        ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                        //Added By Abdus Samad 
                                        //lcms-12105
                                        //Start
                                        ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                        //Stop
                                        askedAssessmentItemsAttributes.Add(ls);
                                    }


                                    //askedAssessmentItemsAttributes.Add(learnerStatistic.AssessmentItemID + "|" + learnerStatistic.CorrectTF.ToString().ToLower() + "|" + learnerStatistic.AnswerProvided.ToString().ToLower() + "|" + learnerStatistic.QuestionType.ToString() + "|" + learnerStatistic.AnswerTexts.ToString() + "|" + learnerStatistic.CorrectAnswerGuids.ToString());
                                }
                                //assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                                //if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                                //{
                                //    ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                //    ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                //    ls.CorrectTF = learnerStatistic.CorrectTF;
                                //    ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                //    ls.QuestionType = learnerStatistic.QuestionType;
                                //    ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                //    ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                //    ls.TimeInSeconds = learnerStatistic.TimeInSeconds;

                                //    askedAssessmentItemsAttributes.Add(ls);
                                //}
                            }
                            ICPTrackingService.LearnerStatistics[] learnerStatisticsAssessment = trackingService.GetPostAssessmentResult(learnerSessionID);// Fix for  	LCMS-11086 Added check for rawscore =-2 in below condition
                            bool pauseCase = false;
                            if (learnerStatisticsAssessment != null && learnerStatisticsAssessment.Length > 0 && learnerStatisticsAssessment[learnerStatisticsAssessment.Length - 1].RawScore == -2)
                            {
                                pauseCase = true;
                            }
                            // End Fix for LCMS-11086 Added check for rawscore =-2 in below condition
                            if ((totalQuestions > askedAssessmentItemsAttributes.Count && askedAssessmentItemsAttributes.Count > 0 && assessmentItemGUIDs != null && assessmentItemGUIDs.Length > 0) || (pauseCase))
                            {
                                // As we do not want to increment the attempt number in this specific case so updating the session variable by setting the flag
                                modifiedAttemptNumber = lastAttemptNumber;
                                HttpContext.Current.Session["RandomAlternateWithPauseResume"] = true;
                                if (askedAssessmentItemsAttributes.Count > 0)
                                    isresumeCase = true;
                            }
                        }
                        ///////end code added for LCMS-10266

                        assessmentItems = assessmentService.GetTestForRandomAlternate(learnerSessionID, courseConfiguration.PostAssessmentConfiguration, isresumeCase);
                    }
                }

                #region Hardcoded Image Question

                //ICPAssessmentService.ImageTargetQuestion imageQuestion = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetQuestion();
                //imageQuestion.AssessmentItemID = 999;
                //imageQuestion.AssessmentItemGuid = "32423423423423";
                //imageQuestion.QuestionStem = "Click on correct box";
                //imageQuestion.QuestionType = QuestionType.ImageTarget;
                //imageQuestion.ImageURL = "";
                //imageQuestion.AssessmentAnswers = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetAssessmentItemAnswer[4];


                //ICPAssessmentService.ImageTargetAssessmentItemAnswer imageTargetAssessmentItemAnswer = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetAssessmentItemAnswer();
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerID = 1;
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerGuid = "sad2123 21asdasdas";
                //imageTargetAssessmentItemAnswer.IsCorrect = false;

                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate[1];
                //ICPAssessmentService.ImageTargetCoordinate imageTargetCoordinate = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate();
                //imageTargetCoordinate.XPos = 90;
                //imageTargetCoordinate.YPos = 10;
                //imageTargetCoordinate.Width = 200;
                //imageTargetCoordinate.Height = 50;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates[0] = imageTargetCoordinate;
                //imageQuestion.AssessmentAnswers[0] = imageTargetAssessmentItemAnswer;



                //imageTargetAssessmentItemAnswer = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetAssessmentItemAnswer();
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerID = 2;
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerGuid = "aad2123 21asdasdas";
                //imageTargetAssessmentItemAnswer.IsCorrect = false;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate[1];
                //imageTargetCoordinate = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate();
                //imageTargetCoordinate.XPos = 250;
                //imageTargetCoordinate.YPos = 5;
                //imageTargetCoordinate.Width = 94;
                //imageTargetCoordinate.Height = 93;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates[0] = imageTargetCoordinate;
                //imageQuestion.AssessmentAnswers[1] = imageTargetAssessmentItemAnswer;



                //imageTargetAssessmentItemAnswer = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetAssessmentItemAnswer();
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerID = 3;
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerGuid = "bad2123 21asdasdas";
                //imageTargetAssessmentItemAnswer.IsCorrect = false;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate[1];
                //imageTargetCoordinate = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate();
                //imageTargetCoordinate.XPos = 200;
                //imageTargetCoordinate.YPos = 150;
                //imageTargetCoordinate.Width = 94;
                //imageTargetCoordinate.Height = 93;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates[0] = imageTargetCoordinate;
                //imageQuestion.AssessmentAnswers[2] = imageTargetAssessmentItemAnswer;

                //imageTargetAssessmentItemAnswer = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetAssessmentItemAnswer();
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerID = 4;
                //imageTargetAssessmentItemAnswer.AssessmentItemAnswerGuid = "kad2123 21asdasdas";
                //imageTargetAssessmentItemAnswer.IsCorrect = false;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate[1];
                //imageTargetCoordinate = new ICP4.BusinessLogic.ICPAssessmentService.ImageTargetCoordinate();
                //imageTargetCoordinate.XPos = 0;
                //imageTargetCoordinate.YPos = 0;
                //imageTargetCoordinate.Width = 94;
                //imageTargetCoordinate.Height = 93;
                //imageTargetAssessmentItemAnswer.ImageTargetCoordinates[0] = imageTargetCoordinate;
                //imageQuestion.AssessmentAnswers[3] = imageTargetAssessmentItemAnswer;


                //for (int i = 0; i < assessmentItems.Length; i++)
                //{
                //    if (assessmentItems[i].QuestionType == QuestionType.SingleSelectMCQ)
                //    {
                //        assessmentItems[i] = imageQuestion;
                //        break;
                //    }
                //}
                #endregion

                
                if (assessmentItems.Length > 0)
                {
                    //Copy assessment list to another list (which will be used for question asking and marking but it will contain limited data)
                    SelectedQuestion selectedQuestions = new SelectedQuestion();
                    QuestionInfo questionInfo = new QuestionInfo();
                    selectedQuestions.AssessmentType = assessmentType;
                    selectedQuestions.QuestionInfos = new List<QuestionInfo>();

                    foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
                    {
                        questionInfo = new QuestionInfo();
                        questionInfo.QuestionID = assessmentItem.AssessmentItemID;
                        questionInfo.QuestionGuid = assessmentItem.AssessmentItemGuid;
                        questionInfo.QuestionType = assessmentItem.QuestionType;
                        questionInfo.AssessmentBinderID = assessmentItem.AssessmentBinderID;
                        questionInfo.IsCorrectlyAnswered = false;
                        questionInfo.IsSkipped = false;
                        questionInfo.ScoreWeight = assessmentItem.ScoreWeight;
                        questionInfo.AssessmentBinderName = assessmentItem.AssessmentBinderName;
                        // For Random Alternate Multiple Item Banks
                        questionInfo.TestID = Convert.ToInt32(assessmentItem.TestID); 
                        /*LCMS-7422 set is exam - start*/
                        questionInfo.IsExam = ExamID > 0;
                        /*LCMS-7422 set is exam - start*/
                        selectedQuestions.QuestionInfos.Add(questionInfo);
                    }
                                        
                    //questionInfo = new QuestionInfo();
                    //questionInfo.QuestionID = 138275;
                    //questionInfo.QuestionGuid = "398441a4-e7e0-4df4-8d65-53322ba85471";
                    //questionInfo.QuestionType = "True False";
                    //questionInfo.AssessmentBinderID = 25479;
                    //questionInfo.IsCorrectlyAnswered = false;
                    //questionInfo.IsSkipped = false;
                    //questionInfo.ScoreWeight = 0.30;
                    //questionInfo.AssessmentBinderName = "ZAK_9622_Blank";
                    ///*LCMS-7422 set is exam - start*/
                    //questionInfo.IsExam = ExamID > 0;
                    ///*LCMS-7422 set is exam - start*/
                    //selectedQuestions.QuestionInfos.Add(questionInfo);

                    //QuestionInfo questionInfo1 = new QuestionInfo();
                    //questionInfo1 = new QuestionInfo();
                    //questionInfo1.QuestionID = 136438;
                    //questionInfo1.QuestionGuid = "32c38e4e-0872-47d8-8d3a-d6be48803f5a";
                    //questionInfo1.QuestionType = "Single Select MCQ";
                    //questionInfo1.AssessmentBinderID = 25479;
                    //questionInfo1.IsCorrectlyAnswered = false;
                    //questionInfo1.IsSkipped = false;
                    //questionInfo1.ScoreWeight = 0.40;
                    //questionInfo1.AssessmentBinderName = "ZAK_9622_Blank";
                    ///*LCMS-7422 set is exam - start*/
                    //questionInfo1.IsExam = ExamID > 0;
                    ///*LCMS-7422 set is exam - start*/
                    //selectedQuestions.QuestionInfos.Add(questionInfo1);

                    //QuestionInfo questionInfo2 = new QuestionInfo();
                    //questionInfo2 = new QuestionInfo();
                    //questionInfo2.QuestionID = 70794;
                    //questionInfo2.QuestionGuid = "8f9a2dea-505e-490e-8f46-29e85ffdbcc2";
                    //questionInfo2.QuestionType = "Multiple Select MCQ";
                    //questionInfo2.AssessmentBinderID = 13009;
                    //questionInfo2.IsCorrectlyAnswered = false;
                    //questionInfo2.IsSkipped = false;
                    //questionInfo2.ScoreWeight = 0.50;
                    //questionInfo2.AssessmentBinderName = "zakLCMS-3657_Ver_5_AIBank";
                    ///*LCMS-7422 set is exam - start*/
                    //questionInfo2.IsExam = ExamID > 0;
                    ///*LCMS-7422 set is exam - start*/
                    //selectedQuestions.QuestionInfos.Add(questionInfo2);


                    //Put both list into session
                    System.Web.HttpContext.Current.Session["AssessmentItemList"] = assessmentItems;
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;
                    System.Web.HttpContext.Current.Session["ExamID"] = ExamID;

                    //Put SequenceNo into session(this Sequence No points to SelectedQuestions list)
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = 0;



                    // LCMS-9213
                    //--------------------------------------------------------------
                    if (assessmentType == LearnerStatisticsType.PreAssessment && courseConfiguration.PreAssessmentConfiguration.AllowPauseResumeAssessment)
                    {
                        if (totalQuestions > askedAssessmentItemsAttributes.Count && assessmentItemGUIDs !=null && assessmentItemGUIDs.Length > 0)
                        {
                            //HttpContext.Current.Session["askedAssessmentItemGUIDs"] = assessmentItemGUIDs;
                            HttpContext.Current.Session["askedAssessmentItemsAttributes"] = askedAssessmentItemsAttributes;
                            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = askedAssessmentItemsAttributes.Count;
                        }
                    }
                    else if (assessmentType == LearnerStatisticsType.PostAssessment && (courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True"))
                    {
                        if (totalQuestions > askedAssessmentItemsAttributes.Count && assessmentItemGUIDs !=null && assessmentItemGUIDs.Length > 0)
                        {
                            //HttpContext.Current.Session["askedAssessmentItemGUIDs"] = assessmentItemGUIDs;
                            HttpContext.Current.Session["askedAssessmentItemsAttributes"] = askedAssessmentItemsAttributes;
                            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = askedAssessmentItemsAttributes.Count;
                        }
                    }                    
                    //--------------------------------------------------------------


                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }

        }


        /// <summary>
        /// This method gets question for Quiz from Asessment services and put it into session
        /// </summary>
        /// <param name="courseID">CourseID integer value</param>
        /// <param name="learnerSessionID">LearnerSessionID string value</param>
        /// <param name="courseConfiguration">CourseConfiguration</param>
        /// <param name="assessmentType">AssessmentType</param>
        /// <returns>Return true in case of success, fasle otherwise</returns>
        public bool LoadQuizIntoSession(int courseID, string learnerSessionID, ICPAssessmentService.CourseConfiguration courseConfiguration, string assessmentType, int contectObjectID, int ExamID, int totalQuestions)
        {
            try
            {
                //Get previously asked question list from tracking webservice
                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                
                string[] assessmentItemGUIDs = null;
                
                //List<string> askedAssessmentItemsAttributes = new List<string>(); // [assessmentItemGUID|AnsweredCorrectly|AnswerProvided] // LCMS-9213
                List<ICPTrackingService.LearnerStatistics> askedAssessmentItemsAttributes = new List<ICPTrackingService.LearnerStatistics>(); 
                
                int lastAttemptNumber = 1;
                if (HttpContext.Current.Session["LastAssessmentAttemptNo"] != null)
                {
                    int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(),out lastAttemptNumber);
                    lastAttemptNumber = lastAttemptNumber -1;
                }
                // Changed by Waqas Zakai
                // LCMS-6984
                // START
                if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsPreview"]) == false)
                {
                    //ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestions(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]));
                    ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestionsQuiz(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]), contectObjectID);
                    assessmentItemGUIDs = new string[learnerStatisticList.Length];
                    //askedAssessmentItemsAttributes = new string[learnerStatisticList.Length]; // LCMS-9213
                    int i = 0;
                    int j = 0;
                    //Create list for assessment Service
                    foreach (ICPTrackingService.LearnerStatistics learnerStatistic in learnerStatisticList)
                    {                        
                        // Fix for LCMS-8289
                        // ----------------------------------------------------
                        //assessmentItemGUIDs[i++] = learnerStatistic.Item_GUID;
                        assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                        // ----------------------------------------------------
                        // LCMS-9213
                        if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                        {
                            //---------------------------------------------------------------------------------------------
                            ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                            ls.AssessmentItemID                     = learnerStatistic.AssessmentItemID;
                            ls.CorrectTF                            = learnerStatistic.CorrectTF;
                            ls.AnswerProvided                       = learnerStatistic.AnswerProvided;
                            ls.QuestionType                         = learnerStatistic.QuestionType;
                            ls.AnswerTexts                          = learnerStatistic.AnswerTexts;
                            ls.CorrectAnswerGuids                   = learnerStatistic.CorrectAnswerGuids;
                            ls.TimeInSeconds                        = learnerStatistic.TimeInSeconds;
                            //Added By Abdus Samad 
                            //lcms-12105
                            //Start
                            ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                            //Stop
                            askedAssessmentItemsAttributes.Add(ls);
                            
                            
                            //askedAssessmentItemsAttributes.Add(learnerStatistic.AssessmentItemID + "|" + learnerStatistic.CorrectTF.ToString().ToLower() + "|" + learnerStatistic.AnswerProvided.ToString().ToLower() + "|" + learnerStatistic.QuestionType.ToString() + "|" + learnerStatistic.AnswerTexts.ToString() + "|" + learnerStatistic.CorrectAnswerGuids.ToString());
                            //---------------------------------------------------------------------------------------------
                        }
                    }
                }
                else
                {
                    if (assessmentType == CourseManager.LearnerStatisticsType.Quiz)
                    {
                        if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"] != null)
                        {
                            if (courseConfiguration.QuizConfiguration.EnforceUniqueQuestionsOnRetake == true)
                            {
                                assessmentItemGUIDs = new string[((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"]).Count];
                                assessmentItemGUIDs = ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"]).ToArray();
                            }
                            else
                            {
                                assessmentItemGUIDs = new string[0];
                            }
                        }
                        else
                        {
                            assessmentItemGUIDs = new string[0];
                        }
                    }
                }

                //Get question from assessment webservice
                ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService();
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPAssessmentService.AssessmentItem[] assessmentItems = null;
                assessmentItems = assessmentService.GetQuizAssessmentItems(courseID, contectObjectID, courseConfiguration, assessmentItemGUIDs, ExamID);                

                if (assessmentItems.Length > 0)
                {

                    //Copy assessment list to another list (which will be used for question asking and marking but it will contain limited data)
                    SelectedQuestion selectedQuestions = new SelectedQuestion();
                    QuestionInfo questionInfo = new QuestionInfo();
                    selectedQuestions.AssessmentType = assessmentType;
                    selectedQuestions.QuestionInfos = new List<QuestionInfo>();

                    foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
                    {
                        questionInfo = new QuestionInfo();
                        questionInfo.QuestionID = assessmentItem.AssessmentItemID;
                        questionInfo.IsCorrectlyAnswered = false;
                        questionInfo.IsSkipped = false;
                        questionInfo.QuestionGuid = assessmentItem.AssessmentItemGuid;
                        questionInfo.AssessmentBinderID = assessmentItem.AssessmentBinderID;
                        questionInfo.QuestionType = assessmentItem.QuestionType;
                        questionInfo.ScoreWeight = assessmentItem.ScoreWeight;
                        questionInfo.AssessmentBinderName = assessmentItem.AssessmentBinderName;
                        
                        /*LCMS-7422 set is exam - start*/
                        questionInfo.IsExam = ExamID > 0;
                        /*LCMS-7422 set is exam - start*/
                        
                        selectedQuestions.QuestionInfos.Add(questionInfo);
                    }



                    //Put both list into session
                    System.Web.HttpContext.Current.Session["AssessmentItemList"] = assessmentItems;
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;

                    //Put SequenceNo into session(this Sequence No points to SelectedQuestions list)
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = 0;

                    // LCMS-9213
                    //--------------------------------------------------------------
                    if (assessmentType == LearnerStatisticsType.Quiz && courseConfiguration.QuizConfiguration.AllowPauseResumeAssessment)
                    {
                        if (totalQuestions > askedAssessmentItemsAttributes.Count && assessmentItemGUIDs != null && assessmentItemGUIDs.Length > 0)
                        {
                            //HttpContext.Current.Session["askedAssessmentItemGUIDs"] = assessmentItemGUIDs;
                            HttpContext.Current.Session["askedAssessmentItemsAttributes"] = askedAssessmentItemsAttributes;
                            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = askedAssessmentItemsAttributes.Count;
                        }
                    }
                    //--------------------------------------------------------------


                    System.Web.HttpContext.Current.Session["ExamID"] = ExamID;


                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }

        }


        /// <summary>
        /// This method gets question for Practice Exam from Asessment services and put it into session
        /// </summary>
        /// <param name="courseID">CourseID integer value</param>
        /// <param name="learnerSessionID">LearnerSessionID string value</param>
        /// <param name="courseConfiguration">CourseConfiguration</param>
        /// <param name="assessmentType">AssessmentType</param>
        /// <returns>Return true in case of success, fasle otherwise</returns>
        public bool LoadPracticeExamIntoSession(int courseID, string learnerSessionID, ICPAssessmentService.AssessmentConfiguration assessmentConfiguration, string assessmentType, int contectObjectID, int ExamID ,int totalQuestions)
        {
            try
            {
                //Get previously asked question list from tracking webservice
                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                List<ICPTrackingService.LearnerStatistics> askedAssessmentItemsAttributes = new List<ICPTrackingService.LearnerStatistics>();
                string[] assessmentItemGUIDs = null;

                int lastAttemptNumber = 1;
                if (HttpContext.Current.Session["LastAssessmentAttemptNo"] != null)
                {
                    int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out lastAttemptNumber);
                    lastAttemptNumber = lastAttemptNumber - 1;
                }

                // Changed by Waqas Zakai
                // LCMS-6984
                // START
                if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsPreview"]) == false)
                {
                    ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestions(learnerSessionID, assessmentType, Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]), ExamID);
                    //TODO: ICPTrackingService.LearnerStatistics[] learnerStatisticList = trackingService.GetPreviouslyAskedQuestionsPracticeExam(learnerSessionID);
                    
                    assessmentItemGUIDs = new string[learnerStatisticList.Length];
                    int i = 0;
                    //Create list for assessment Service
                    foreach (ICPTrackingService.LearnerStatistics learnerStatistic in learnerStatisticList)
                    {
                        // Fix for LCMS-8289
                        //---------------------------------------------------------------
                        //assessmentItemGUIDs[i++] = learnerStatistic.Item_GUID;
                        //assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                        ////---------------------------------------------------------------
                         //////////////////////////////Yasin/////////////////////////////////////////
                        if (assessmentConfiguration.AllowPauseResumeAssessment)
                        {
                            if (learnerStatistic.AnswerProvided == true && learnerStatistic.CorrectAnswerGuids.Length > 0)
                            {
                                assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                            }
                        }
                        else
                        {
                            assessmentItemGUIDs[i++] = learnerStatistic.AssessmentItemID;
                        }
                        //---------------------------------------------------------------
                         //////////////////////////////Yasin/////////////////////////////////////////
                        if (assessmentConfiguration.AllowPauseResumeAssessment)
                        {
                            if (lastAttemptNumber == learnerStatistic.AssessmentAttemptNumber)
                            {
                                //Commented By Abdus Samad For bug Fixation LCMS-12402

                               // if (learnerStatistic.AnswerProvided == true || learnerStatistic.CorrectAnswerGuids.Length > 0)
                               // {
                                    ICPTrackingService.LearnerStatistics ls = new ICPTrackingService.LearnerStatistics();
                                    ls.AssessmentItemID = learnerStatistic.AssessmentItemID;
                                    ls.CorrectTF = learnerStatistic.CorrectTF;
                                    ls.AnswerProvided = learnerStatistic.AnswerProvided;
                                    ls.QuestionType = learnerStatistic.QuestionType;
                                    ls.AnswerTexts = learnerStatistic.AnswerTexts;
                                    ls.CorrectAnswerGuids = learnerStatistic.CorrectAnswerGuids;
                                    ls.TimeInSeconds = learnerStatistic.TimeInSeconds;
                                    //Added By Abdus Samad 
                                    //lcms-12105
                                    //Start
                                    ls.IsAssessmentItemToogled = learnerStatistic.IsAssessmentItemToogled;
                                    //Stop
                                    askedAssessmentItemsAttributes.Add(ls);
                                //}                                
                            }
                        }
                         //////////////////////////////Yasin/////////////////////////////////////////
                    }
                }
                else
                {
                    if (assessmentType == CourseManager.LearnerStatisticsType.PracticeExam)
                    {
                        if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"] != null)
                        {
                            if (assessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
                            {
                                assessmentItemGUIDs = new string[((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"]).Count];
                                assessmentItemGUIDs = ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"]).ToArray();
                            }
                            else
                            {
                                assessmentItemGUIDs = new string[0];
                            }
                        }
                        else
                        {
                            assessmentItemGUIDs = new string[0];
                        }
                    }
                }

                //Get question from assessment webservice
                ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService();
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPAssessmentService.AssessmentItem[] assessmentItems = null;
                assessmentItems = assessmentService.GetPracticeExamAssessmentItems(courseID, contectObjectID, assessmentConfiguration, assessmentItemGUIDs, ExamID);
               

                if (assessmentItems.Length > 0)
                {

                    //Copy assessment list to another list (which will be used for question asking and marking but it will contain limited data)
                    SelectedQuestion selectedQuestions = new SelectedQuestion();
                    QuestionInfo questionInfo = new QuestionInfo();
                    selectedQuestions.AssessmentType = assessmentType;
                    selectedQuestions.QuestionInfos = new List<QuestionInfo>();

                    foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
                    {
                        questionInfo = new QuestionInfo();
                        questionInfo.QuestionID = assessmentItem.AssessmentItemID;
                        questionInfo.IsCorrectlyAnswered = false;
                        questionInfo.IsSkipped = false;
                        questionInfo.QuestionGuid = assessmentItem.AssessmentItemGuid;
                        questionInfo.AssessmentBinderID = assessmentItem.AssessmentBinderID;
                        questionInfo.QuestionType = assessmentItem.QuestionType;
                        questionInfo.ScoreWeight = assessmentItem.ScoreWeight;
                        questionInfo.AssessmentBinderName = assessmentItem.AssessmentBinderName;
                        
                        /*LCMS-7422 set is exam - start*/
                        questionInfo.IsExam = ExamID > 0;
                        /*LCMS-7422 set is exam - start*/
                        selectedQuestions.QuestionInfos.Add(questionInfo);

                    }



                    //Put both list into session
                    System.Web.HttpContext.Current.Session["AssessmentItemList"] = assessmentItems;
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;

                    //Put SequenceNo into session(this Sequence No points to SelectedQuestions list)
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = 0;
                    System.Web.HttpContext.Current.Session["ExamID"] = ExamID;
                    //////////////////////////////Yasin/////////////////////////////////////////
                    if (assessmentType == LearnerStatisticsType.PracticeExam && assessmentConfiguration.AllowPauseResumeAssessment)
                    {
                        if (totalQuestions > askedAssessmentItemsAttributes.Count && assessmentItemGUIDs != null && assessmentItemGUIDs.Length > 0)
                        {
                            //HttpContext.Current.Session["askedAssessmentItemGUIDs"] = assessmentItemGUIDs;
                            HttpContext.Current.Session["askedAssessmentItemsAttributes"] = askedAssessmentItemsAttributes;
                            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = askedAssessmentItemsAttributes.Count;
                        }
                    }
                     //////////////////////////////Yasin/////////////////////////////////////////


                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }

        }


        /// <summary>
        /// This method gets next question from question list based on SelectedQuestionSeqeunceNo and sets SelectedQuestionSeqeunceNo to next possible question
        /// Apart from this method also cater the scenarios like when question comes to an end. It also checks some policies
        /// </summary>
        /// <returns>Based on condition this mehod will return command object</returns>
        public object GetNextQuestion()
        {
            // Here two cases is to be considered
            // 1: Normal sequential flow
            // 2: Asked only Skipped Question

            //We have to put some sort of signal variable in Session to execute each case when required
            string logString = string.Empty;
            try
            {
                logString = "LearningSession Guid : " + System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                //System.Diagnostics.Trace.WriteLine(logString);
                //System.Diagnostics.Trace.Flush();
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }

            object returnObject = new object();

            int examID = 0;

            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }

            try
            {
                //Change Made by Waqas Zakai 1st July 2010
                // LCMS-4488
                // START
                using (ICP4.BusinessLogic.CourseManager.CourseManager coursemanager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START                        
                    if (coursemanager.IsCoursePublished() == true)
                    {
                        if (System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] == null)
                        {
                            object CoursePublishCommand = coursemanager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedAssessment);
                            //SessionAbandonOnAssessment();
                            return CoursePublishCommand;
                        }
                        else
                        {
                            object CoursePublishCommand = coursemanager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                            coursemanager.SessionAbandonOnScene(); 
                            return CoursePublishCommand;
                        }
                    }
                    //END
                    if (coursemanager.MustCourseCompleteWithinSpecificAmountOfTime() == false)
                    {
                        //System.Diagnostics.Trace.WriteLine(logString + " MustCourseCompleteWithinSpecificAmountOfTime");
                        //System.Diagnostics.Trace.Flush();
                        return coursemanager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute);
                    }
                }
                // END

                int assessmentSequenceNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]);
                ICPAssessmentService.AssessmentItem[] assessmentItems = null;
                assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
                if (assessmentItems !=null && assessmentSequenceNo <= assessmentItems.Length - 1)
                {
                    ICPAssessmentService.AssessmentItem assessmentItem = new ICPAssessmentService.AssessmentItem();
                    assessmentItem = assessmentItems[assessmentSequenceNo];

                    //System.Diagnostics.Trace.WriteLine(logString + " assessmentSequenceNo:" + assessmentSequenceNo + " assessmentItem.ID:" + assessmentItem.AssessmentItemID + " assessmentItems.Length:" + assessmentItems.Length);
                    //System.Diagnostics.Trace.Flush();


                    if (System.Web.HttpContext.Current.Session["AssessmentFlow"].ToString() == "NormalSequentialFlow")
                    {
                        //System.Diagnostics.Trace.WriteLine(logString + " AssessmentFlow:NormalSequentialFlow");
                        //System.Diagnostics.Trace.Flush();

                        //Setting Index for Next Question
                        System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = assessmentSequenceNo + 1;
                    }
                    else if (System.Web.HttpContext.Current.Session["AssessmentFlow"].ToString() == "AnswerRemainingQuestion")
                    {
                        //System.Diagnostics.Trace.WriteLine(logString + " AssessmentFlow:AnswerRemainingQuestion");
                        //System.Diagnostics.Trace.Flush();

                        //Setting Index for Next Question
                        int questionIndex;
                        questionIndex = GetIndexOfNextSkippedQuestion(assessmentSequenceNo);
                        System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = questionIndex;

                    }
                    else if (System.Web.HttpContext.Current.Session["AssessmentFlow"].ToString() == "AskSpecifiedQuestion")
                    {
                        //System.Diagnostics.Trace.WriteLine(logString + " AssessmentFlow:AskSpecifiedQuestion");
                        //System.Diagnostics.Trace.Flush();

                        //Setting Index for Next Question
                        System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = assessmentItems.Length;

                    }
                    //Below check is because for answerreview screen we want selected answers to be displayed.
                    //if (System.Web.HttpContext.Current.Session["AssessmentFlow"].ToString() == "AskSpecifiedQuestion")
                    //{
                    SelectedQuestion selectedQuestion = null;
                    selectedQuestion = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
                    string audioURL = string.Empty;
                    string templatetype = string.Empty;
                    string visualTopType = string.Empty;
                    //Improvement for Text Only Template Type
                    string assessmentItemTemplate = string.Empty;
                    if (assessmentItem.AssessmentItemTemplateType == "Text Only")
                    {
                        templatetype = "Text Only";
                        assessmentItemTemplate = GetTextOnlyTemplate();
                        //assessmentItemTemplate = GetAssessmentItemTemplate(assessmentItem.AssessmentItemID, out audioURL, out templatetype, out visualTopType);
                    }
                    else
                    {
                        //Improvement for Text Only Template Type
                        assessmentItemTemplate = GetAssessmentItemTemplate(assessmentItem.AssessmentItemID, out audioURL, out templatetype, out visualTopType);
                    }

                    returnObject = CreateQuestionCommandObject(assessmentItem, assessmentSequenceNo + 1, assessmentItems.Length, selectedQuestion.QuestionInfos[assessmentSequenceNo], false, false, false, "", assessmentItemTemplate, audioURL, templatetype, visualTopType, "");
                    //System.Diagnostics.Trace.WriteLine(logString + " CreateQuestionCommandObject:Done");
                    //System.Diagnostics.Trace.Flush();
                    //}
                    //else
                    //{
                    //    returnObject = CreateQuestionCommandObject(assessmentItem, assessmentSequenceNo + 1, assessmentItems.Length, null, false, false,false);
                    //}
                }
                else
                {
                    int masteryScore = 80;
                    bool showPercentageScore = false;
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    bool scoreAsYouGo = false;
                    bool allowSkipping = false;

                    #region Get Policy variables
                    string itemType = "";
                    ICPCourseService.CourseConfiguration courseConfiguration = null;
                    ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
                    ICPCourseService.SequenceItem sequenceItem = null;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        itemType = sequenceItem.SequenceItemType;
                    }

                    if (itemType == CourseManager.SequenceItemTypeName.Exam)
                    {
                        itemType = sequenceItem.ExamType;
                    }

                    if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                    {
                        #region Pre Assessment
                        assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;
                        if (courseConfiguration.PreAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                        {
                            showPercentageScore = false;
                        }
                        else
                        {
                            showPercentageScore = true;
                        }

                        masteryScore = courseConfiguration.PreAssessmentConfiguration.MasteryScore;
                        scoreAsYouGo = courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo;
                        allowSkipping = courseConfiguration.PreAssessmentConfiguration.AllowSkippingQuestion;
                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                    {
                        #region Quiz
                        assessmentConfiguration = courseConfiguration.QuizConfiguration;
                        if (courseConfiguration.QuizConfiguration.ScoreType == ScoreType.PassFail)
                        {
                            showPercentageScore = false;
                        }
                        else
                        {
                            showPercentageScore = true;
                        }

                        masteryScore = courseConfiguration.QuizConfiguration.MasteryScore;
                        scoreAsYouGo = courseConfiguration.QuizConfiguration.ScoreAsYouGo;
                        allowSkipping = courseConfiguration.QuizConfiguration.AllowSkippingQuestion;
                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                    {
                        assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                        #region Post Assessment
                        if (courseConfiguration.PostAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                        {
                            showPercentageScore = false;
                        }
                        else
                        {
                            showPercentageScore = true;
                        }

                        masteryScore = courseConfiguration.PostAssessmentConfiguration.MasteryScore;
                        scoreAsYouGo = courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo;
                        allowSkipping = courseConfiguration.PostAssessmentConfiguration.AllowSkippingQuestion;
                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                    {
                        #region PracticeExam
                        ICPCourseService.AssessmentConfiguration ExamAssessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                        ExamAssessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];

                        if (ExamAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                        {
                            showPercentageScore = false;
                        }
                        else
                        {
                            showPercentageScore = true;
                        }

                        masteryScore = ExamAssessmentConfiguration.MasteryScore;
                        scoreAsYouGo = ExamAssessmentConfiguration.ScoreAsYouGo;
                        allowSkipping = ExamAssessmentConfiguration.AllowSkippingQuestion;
                        #endregion
                    }
                    #endregion

                    int numberOfCorrect = 0;
                    int numberOfInCorrect = 0;
                    bool isAssessmentPass = false;
                    if (CheckIsThereAnySkippedQuestion())
                    {
                        returnObject = CreateSkippedQuestionCommandObject();
                    }
                    else
                    {

                        SelectedQuestion selectedQuestion = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];


                        // LCMS-9213
                        //--------------------------------------------------------------------------------
                            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

                            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

                            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }
                            
                            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                            {
                                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];                                
                            }
                        //--------------------------------------------------------------------------------



                        if (scoreAsYouGo)                        
                        {
                            DataTable dt = null;
                            double score = -1;

                            
                            
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                            }


                            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

                            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

                            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }
                            
                            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                            {
                                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];                                
                            }

                            if (assessmentConfiguration != null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
                            {
                                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                                try
                                {
                                    score = (double)dt.Compute("SUM(WeightedScore)", "");
                                }
                                catch (Exception exp)
                                {
                                    score = 0.0;
                                }
                                isAssessmentPass = (score >= masteryScore);
                            }
                            else
                            {

                                if (examID > 0 && (assessmentConfiguration!=null && assessmentConfiguration.ScoreType != ScoreType.NoResults)) // calculate the data table for exam to summarize score topic wise
                                {
                                    int temp1 = 0, temp2 = 0;
                                    dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref temp1, ref temp2);
                                }

                                CalculateAssessmentResult(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                                score = (Convert.ToDouble(numberOfCorrect) / Convert.ToDouble(numberOfCorrect + numberOfInCorrect)) * 100.00;
                                if (score >= masteryScore)
                                    isAssessmentPass = true;
                                else
                                    isAssessmentPass = false;
                            }

//                            isAssessmentPass = CalculateAssessmentResult(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                            int currAttemptNo = 1;
                            int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);

                            // Fix for  	LCMS-11086
                            if ((courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True")&& (courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE ||
                                                                                                   courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS))
                            {
                                if (Convert.ToBoolean(HttpContext.Current.Session["AssessmentAllQuestionsAttempted"]) == false && Convert.ToBoolean(HttpContext.Current.Session["AssessmentAllQuestionsDisplayed"]) == false)
                                {
                                    score = -2;
                                    isAssessmentPass = false;
                                }
                            }
                            // End fix for  	LCMS-11086

                            long learnerStatistic_Id = SaveAssessmentEndTrackingInfo(selectedQuestion, numberOfCorrect, numberOfInCorrect, currAttemptNo, score, isAssessmentPass, masteryScore);

                            SaveQuestionTrackingInfo(selectedQuestion, learnerStatistic_Id,false);

                            returnObject = CreateAssessmentScoreSummaryCommandObject(showPercentageScore, isAssessmentPass, numberOfCorrect, assessmentItems.Length, currAttemptNo, score, dt);

                            //System.Diagnostics.Trace.WriteLine(logString + " scoreAsYouGo " + " isAssessmentPass:" + isAssessmentPass + " masteryScore:" + masteryScore);
                            //System.Diagnostics.Trace.Flush();


                        }
                        else
                        {
                            //System.Diagnostics.Trace.WriteLine(logString + " CreateAnswerReviewCommandObject");
                            //System.Diagnostics.Trace.Flush();

                            returnObject = CreateAnswerReviewCommandObject();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return returnObject;

        }


        /// <summary>
        /// This method marks all the unanswered question as incorrect, caulate the result for assessment and returns the AssessmentScoreSummary command.
        /// </summary>
        /// <param name="masteryScore">Mastery score integer value, policy variable</param>
        /// <param name="showPercentageScore">showPercentageScore boolean value, to represent whether the score should be displayed as percentage or not</param>
        /// <returns>AssessmentScoreSummary command</returns>
        public object ContinueGradingWithoutAnswering(int masteryScore, bool showPercentageScore)
        {
            object returnObject = new object();
            bool isAssessmentPass = false;
            int noOfCorrect = 0;
            int noOfIncorrect = 0;
            SelectedQuestion selectedQuestions = null;
            int examID = 0;

            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            MarkUnAnsweredQuestionsIncorrect(selectedQuestions);

            DataTable dt = null;
            double score = -1;


            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }


            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;
            string itemType = string.Empty;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                itemType = sequenceItem.SequenceItemType;
            }

            if (itemType == CourseManager.SequenceItemTypeName.Exam)
            {
                itemType = sequenceItem.ExamType;
            }

            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
            {
                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
            }

            if (assessmentConfiguration != null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
            {
                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                try 
                {
                    score = (double)dt.Compute("SUM(WeightedScore)", "");
                }
                catch(Exception exp)
                {
                    score = 0.0;
                }
                isAssessmentPass = (score >= masteryScore);
            }
            else
            {

                if (examID > 0 && (assessmentConfiguration!=null && assessmentConfiguration.ScoreType != ScoreType.NoResults)) // calculate the data table for exam to summarize score topic wise
                {
                    int temp1 = 0, temp2 = 0;
                    dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref temp1, ref temp2);
                }

                CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                score = (Convert.ToDouble(noOfCorrect) / Convert.ToDouble(selectedQuestions.QuestionInfos.Count)) * 100.00;
                if (score >= masteryScore)
                    isAssessmentPass = true;
                else
                    isAssessmentPass = false;
            }

            //isAssessmentPass = CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
            int currAttemptNo = 1;
            int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);
            long learnerStatistic_Id = SaveAssessmentEndTrackingInfo(selectedQuestions, noOfCorrect, noOfIncorrect, currAttemptNo, score, isAssessmentPass, masteryScore);

            SaveQuestionTrackingInfo(selectedQuestions, learnerStatistic_Id,false);
 

            returnObject = CreateAssessmentScoreSummaryCommandObject(showPercentageScore, isAssessmentPass, noOfCorrect, selectedQuestions.QuestionInfos.Count, currAttemptNo, score, dt);
            return returnObject;
        }



        /// <summary>
        /// This method marks all the unanswered question as incorrect, caulate the result for assessment and returns the AssessmentScoreSummary command. But it doesn't count the attempt
        /// </summary>
        /// <param name="masteryScore">Mastery score integer value, policy variable</param>
        /// <param name="showPercentageScore">showPercentageScore boolean value, to represent whether the score should be displayed as percentage or not</param>
        /// <returns>AssessmentScoreSummary command</returns>
        public object ReturnToAssessmentResults(int masteryScore, bool showPercentageScore)
        {
            object returnObject = new object();
            bool isAssessmentPass = false;
            int noOfCorrect = 0;
            int noOfIncorrect = 0;
            int examID = 0;
            SelectedQuestion selectedQuestions = null;

            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            MarkUnAnsweredQuestionsIncorrect(selectedQuestions);

            DataTable dt = null;
            double score = -1;

            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }

            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
            }

            string itemType = "";
            if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.Exam)
            {
                itemType = sequenceItem.ExamType;
            }
            else
            {
                itemType = sequenceItem.SequenceItemType;
            }



            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }

            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
            {   
                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
            }

            if (assessmentConfiguration != null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
            {
                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                try
                {
                    score = (double)dt.Compute("SUM(WeightedScore)", "");
                }
                catch (Exception exp)
                {
                    score = 0.0;
                }
                isAssessmentPass = (score >= masteryScore);
            }
            else
            {

                if (examID > 0 && (assessmentConfiguration!=null && assessmentConfiguration.ScoreType != ScoreType.NoResults)) // calculate the data table for exam to summarize score topic wise
                {
                    int temp1 = 0, temp2 = 0;
                    dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref temp1, ref temp2);
                }
                
                CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                score = (Convert.ToDouble(noOfCorrect) / Convert.ToDouble(selectedQuestions.QuestionInfos.Count)) * 100.00;
                if (score >= masteryScore)
                    isAssessmentPass = true;
                else
                    isAssessmentPass = false;
            }



            //isAssessmentPass = CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);


            int currentAttemptNo = 1;
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
            if (isPreview == true)
            {
                ICPTrackingService.LearnerStatistics[] learnerStatisticsPreview = null;
                learnerStatisticsPreview = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                if (learnerStatisticsPreview != null && learnerStatisticsPreview.Length > 0)
                {
                    currentAttemptNo = learnerStatisticsPreview[learnerStatisticsPreview.Length - 1].AssessmentAttemptNumber;
                }
            }
            else
            {
                int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currentAttemptNo);
            }

            returnObject = CreateAssessmentScoreSummaryCommandObject(showPercentageScore, isAssessmentPass, noOfCorrect, selectedQuestions.QuestionInfos.Count, currentAttemptNo, score, dt);
            return returnObject;
        }


        /// <summary>
        /// This method calculate the result for assessment and returns the AssessmentScoreSummary command.
        /// </summary>
        /// <param name="masteryScore">Mastery score integer value, policy variable</param>
        /// <param name="showPercentageScore">showPercentageScore boolean value, to represent whether the score should be displayed as percentage or not</param>
        /// <returns>AssessmentScoreSummary command</returns>
        public object FinishGradingAssessment(int masteryScore, bool showPercentageScore)
        {
            object returnObject = new object();
            bool isAssessmentPass = false;
            int noOfCorrect = 0;
            int noOfIncorrect = 0;
            int examID = 0;
            SelectedQuestion selectedQuestions = null;

            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }

            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];


            //isAssessmentPass = CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
            
            DataTable dt = null;
            double score = -1;




            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
            }


            if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.PreAssessment)
            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

            else if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.Quiz)
            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

            else if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.PostAssessment)
            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }

            else if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.Exam)
            {
                if (sequenceItem.ExamType == SequenceItemTypeName.PreAssessment)
                {
                    assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; 
                }

                if (sequenceItem.ExamType == SequenceItemTypeName.PostAssessment)
                {
                    assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; 
                }

                if (sequenceItem.ExamType == SequenceItemTypeName.Quiz)
                {
                    assessmentConfiguration = courseConfiguration.QuizConfiguration;
                }

                if (sequenceItem.ExamType == SequenceItemTypeName.PracticeExam)
                {
                    ICPCourseService.AssessmentConfiguration practiceExamAssessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                    practiceExamAssessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                    assessmentConfiguration = practiceExamAssessmentConfiguration;
                }
                
            }

            if (assessmentConfiguration != null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
            {
                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                try
                {
                    score = (double)dt.Compute("SUM(WeightedScore)", "");
                }
                catch (Exception exp)
                {
                    score = 0.0;
                }
                isAssessmentPass = (score >= masteryScore);
            }
            else
            {

                if (examID > 0 && (assessmentConfiguration != null && assessmentConfiguration.ScoreType != ScoreType.NoResults)) // calculate the data table for exam to summarize score topic wise
                {
                    int temp1 = 0, temp2 = 0;
                    dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref temp1, ref temp2);
                }

                CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                score = (Convert.ToDouble(noOfCorrect) / Convert.ToDouble(selectedQuestions.QuestionInfos.Count)) * 100.00;
                if (score >= masteryScore)
                    isAssessmentPass = true;
                else
                    isAssessmentPass = false;

            }

            int currAttemptNo = 1;
            int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);
            long learnerStatistics_Id = SaveAssessmentEndTrackingInfo(selectedQuestions, noOfCorrect, noOfIncorrect, currAttemptNo, score, isAssessmentPass, masteryScore);

            SaveQuestionTrackingInfo(selectedQuestions, learnerStatistics_Id,false);

            returnObject = CreateAssessmentScoreSummaryCommandObject(showPercentageScore, isAssessmentPass, noOfCorrect, selectedQuestions.QuestionInfos.Count, currAttemptNo, score, dt);
            return returnObject;
        }
        /// <summary>
        /// This method grades assessment i.e takes user to the end of assessment and returns answer review command object or the command object 
        /// returned by the FinishGradingAssessment method
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>Based on condition this mehod will return command object</returns>
        public object GradeAssessment(int courseID)
        {
            //Change Made by Waqas Zakai 1st March 2011
            // LCMS-6461
            // START    
            using (ICP4.BusinessLogic.CourseManager.CourseManager coursemanager = new ICP4.BusinessLogic.CourseManager.CourseManager())
            {
                if (coursemanager.IsCoursePublished() == true)
                {
                    object CoursePublishCommand = coursemanager.CreateCourseLockedCommandObject(courseID, LockingReason.CoursePublishedAssessment);
                    SessionAbandonOnAssessment();
                    return CoursePublishCommand;
                }
            }
            //END
            object returnObject = new object();
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            bool scorAsYouGo = false;
            int masteryScore = 0;
            bool showPercentageScore = false;
            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
            foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                questionInfo.IsSkipped = false;
            }
            //Setup variable based on polices.
            #region Get Policy variables

            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;

            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
            }


            if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.PreAssessment)
            {
                if (courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo == true)
                {
                    scorAsYouGo = true;
                    masteryScore = courseConfiguration.PreAssessmentConfiguration.MasteryScore;
                    if (courseConfiguration.PreAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }
                }
            }
            else if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.Quiz)
            {
                if (courseConfiguration.QuizConfiguration.ScoreAsYouGo == true)
                {
                    scorAsYouGo = true;
                    masteryScore = courseConfiguration.QuizConfiguration.MasteryScore;
                    if (courseConfiguration.QuizConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }
                }
            }
            else if (sequenceItem.SequenceItemType == CourseManager.SequenceItemTypeName.PostAssessment)
            {
                if (courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo == true)
                {
                    scorAsYouGo = true;
                    masteryScore = courseConfiguration.PostAssessmentConfiguration.MasteryScore;
                    if (courseConfiguration.PostAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }
                }
            }
            #endregion

            //We have set this session variable to the end of the list as after grade assessment user always
            //move to the end of the assessment.
            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = selectedQuestions.QuestionInfos.Count;
            if (scorAsYouGo == false)
            {
                returnObject = CreateAnswerReviewCommandObject();
            }
            else
            {
                returnObject = FinishGradingAssessment(masteryScore, showPercentageScore);
            }
            return returnObject;
        }
        /// <summary>
        /// This method puts AssessmentEnd track record through tracking service
        /// </summary>
        /// <param name="selectedQuestion">SelectedQuestion an object, represent all the question which were asked during assessment</param>
        /// <param name="noOfAnswersCorrect">NoOfAnswersCorrect integer value</param>
        /// <param name="noOfAnswersInCorrect">NoOfAnswersInCorrect integer value</param>
        /// <returns>In case of success, returns true. False otherwise.</returns>
        public long SaveAssessmentEndTrackingInfo(SelectedQuestion selectedQuestion, int noOfAnswersCorrect, int noOfAnswersInCorrect, int currentAttemptNo, double weightedScore, bool isCurrentAssessmentPassed, int masteryScore)
        {
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);

            if (System.Web.HttpContext.Current.Session["Current_Assessment_LearnerStatistics_ID"] != null)
                System.Web.HttpContext.Current.Session.Remove("Current_Assessment_LearnerStatistics_ID");

            //currentAttemptNo = 1;
            if (isPreview)
            {
                ICPTrackingService.LearnerStatistics[] learnerStatisticsPreview = null;
                learnerStatisticsPreview = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                if (learnerStatisticsPreview != null && learnerStatisticsPreview.Length > 0)
                {
                    currentAttemptNo = learnerStatisticsPreview[learnerStatisticsPreview.Length - 1].AssessmentAttemptNumber;
                }
                else
                {
                    currentAttemptNo = 0;
                }
                SaveAssessmentEndTrackingInfoInSession(selectedQuestion, noOfAnswersCorrect, noOfAnswersInCorrect, weightedScore, isCurrentAssessmentPassed);//, double weightedScore
                return 1;
            }
            try
            {
                string learenrSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                
                ICPCourseService.CourseConfiguration courseConfiguration = null;
                
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);                
                }

                //int attemptNo = 0;
                DateTime assessmentStartTime = Convert.ToDateTime(System.Web.HttpContext.Current.Session["AssessmentStartTime"]);

                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                /*
                ICPTrackingService.LearnerStatistics[] previousLearnerStatistics = null;



                switch (selectedQuestion.AssessmentType)
                {
                    case CourseManager.LearnerStatisticsType.PreAssessment:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.PreAssessmentEnd;
                            previousLearnerStatistics = trackingService.GetPreAssessmentResult(learenrSessionID);
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PostAssessment:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.PostAssessmentEnd;
                            previousLearnerStatistics = trackingService.GetPostAssessmentResult(learenrSessionID);
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.Quiz:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.QuizEnd;
                            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                            ICPCourseService.SequenceItem sequenceItem = null;
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex);
                                learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            }
                            previousLearnerStatistics = trackingService.GetQuizResult(learenrSessionID, sequenceItem.Item_GUID);
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PracticeExam:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.PracticeExamEnd;
                            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                            ICPCourseService.SequenceItem sequenceItem = null;
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex);
                                learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            }
                            previousLearnerStatistics = trackingService.GetPracticeExamResult(learenrSessionID, sequenceItem.Item_GUID);
                            break;
                        }
                }
                bool isPass = false;
                using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {

                    bool actionTaken = false;
                    int learnerStatisticID = 0;
                    int remediationCount = 0;
                    courseManager.GetLastAssessmentResult(previousLearnerStatistics, 1, out isPass, out attemptNo, out actionTaken, out learnerStatisticID, out remediationCount);
                }
                */




                // LCMS-9213
                //----------------------------------------------------------------------
                List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics> previouslyAskedQuestionsWithAttr = new List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>();
                int previousTime = 0;
                if (HttpContext.Current.Session["askedAssessmentItemsAttributes"] != null && (Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) > 1))
                {                 
                    previouslyAskedQuestionsWithAttr = (List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>)HttpContext.Current.Session["askedAssessmentItemsAttributes"];
                    if (previouslyAskedQuestionsWithAttr.Count > 0)
                    {
                        previousTime = previouslyAskedQuestionsWithAttr[0].TimeInSeconds;
                    }
                }

                //----------------------------------------------------------------------




                learnerStatistics.AssessmentAttemptNumber = currentAttemptNo;
                //currentAttemptNo = attemptNo;// learnerStatistics.AssessmentAttemptNumber;
                learnerStatistics.AssessmentType = selectedQuestion.AssessmentType;
                learnerStatistics.LearningSession_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
                learnerStatistics.NumberAnswersCorrect = noOfAnswersCorrect;
                learnerStatistics.NumberAnswersIncorrect = noOfAnswersInCorrect;
                learnerStatistics.RawScore = weightedScore;
                learnerStatistics.MaxAtemptActionTaken = false;
                learnerStatistics.IsPass = isCurrentAssessmentPassed;
                learnerStatistics.RemediationCount = Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]);
                //learnerStatistics.TimeInSeconds = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assessmentStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                learnerStatistics.TimeInSeconds = previousTime + Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assessmentStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                learnerStatistics.LearnerEnrollment_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                //LCMS-10266
                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PostAssessment
                    && (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True" || courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment)
                    && (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True" || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)
                    )
                {
                    //// As in pause resume case it never considers pausing on question 1 to be pause/resume scenario and never logs its time
                    //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) <= 1 && System.Web.HttpContext.Current.Session["OriginalAttemptNumber"]!=null)
                    //{
                    //    learnerStatistics.AssessmentAttemptNumber =Convert.ToInt32(System.Web.HttpContext.Current.Session["OriginalAttemptNumber"]);
                    //}
                        learnerStatistics.IsRepeatedAssessmentAttempt = (HttpContext.Current.Session["RandomAlternateWithPauseResume"] != null ? true : false);
                }
                //End LCMS-10266/////////
                int examID = 0;
                if (System.Web.HttpContext.Current.Session["ExamID"] != null)
                {
                    examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
                }
                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.Quiz || selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PracticeExam || examID>0)
                {
                    learnerStatistics.Statistic_Type = selectedQuestion.AssessmentType;// CourseManager.LearnerStatisticsType.QuizEnd;
                    
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    ICPCourseService.SequenceItem sequenceItem = null;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                    }
                }
                //isSaved = trackingService.SaveLearnerStatistics(learnerStatistics);
                
                

                long learnerStatistics_ID = trackingService.SaveAssessmentScore(learnerStatistics, masteryScore);

                ICPCourseService.AssessmentConfiguration currentAssessmentConfiguration = null;

                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PreAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.Quiz)
                {
                    currentAssessmentConfiguration = courseConfiguration.QuizConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PostAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PracticeExam)
                {
                    currentAssessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                }

                //LCMS-8469 - In Case of No Result, LMS Web Service will not be called to update assessment score
                if (currentAssessmentConfiguration.ScoreType != ScoreType.NoResults)
                {
                    //LMS Integeration
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    if (source == 0)
                    {
                        IntegerationStatistics integerationStatistics = new IntegerationStatistics();
                        integerationStatistics.AssessmentType = selectedQuestion.AssessmentType;
                        integerationStatistics.AssessmentScore = weightedScore;
                        integerationStatistics.IsAssessmentPassed = isCurrentAssessmentPassed;
                        integerationStatistics.LearningSessionGuid = learenrSessionID;
                        integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.AssessmentCompletion;
                        HttpContext.Current.Session["IntegerationStatistics"] = integerationStatistics;
                        //Integeration integeration = IntegerationFactory.GetObject(source);
                        //integeration.SynchStatsToExternalSystem(integerationStatistics);
                    }
                }

                System.Web.HttpContext.Current.Session["AssessmentType"] = selectedQuestion.AssessmentType;

                return learnerStatistics_ID;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return 0;

            }
        }


        
        // LCMS-9882
        public long SaveAssessmentEndTrackingInfo_ForGameTemplate(string assessmentType, int noOfAnswersCorrect, int noOfAnswersInCorrect, int currentAttemptNo, double weightedScore, bool isCurrentAssessmentPassed, int masteryScore, int assessmentTimeInSeconds, int remediationCount)
        {

            if (System.Web.HttpContext.Current.Session["Current_Assessment_LearnerStatistics_ID"] != null)
                System.Web.HttpContext.Current.Session.Remove("Current_Assessment_LearnerStatistics_ID");


            /*
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);

            
            //currentAttemptNo = 1;
            
            if (isPreview)
            {
                ICPTrackingService.LearnerStatistics[] learnerStatisticsPreview = null;
                learnerStatisticsPreview = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                if (learnerStatisticsPreview != null && learnerStatisticsPreview.Length > 0)
                {
                    currentAttemptNo = learnerStatisticsPreview[learnerStatisticsPreview.Length - 1].AssessmentAttemptNumber;
                }
                else
                {
                    currentAttemptNo = 0;
                }
                SaveAssessmentEndTrackingInfoInSession(selectedQuestion, noOfAnswersCorrect, noOfAnswersInCorrect, weightedScore, isCurrentAssessmentPassed);//, double weightedScore
                return 1;
            }
            */

            try
            {
                string learenrSessionID = Convert.ToString(System.Web.HttpContext.Current.Session["LearnerSessionID"]);
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);


                ICPCourseService.CourseConfiguration courseConfiguration = null;

                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                }

                //int attemptNo = 0;
                DateTime assessmentStartTime = Convert.ToDateTime(System.Web.HttpContext.Current.Session["AssessmentStartTime"]);

                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
               

                /*
                // LCMS-9213
                //----------------------------------------------------------------------
                List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics> previouslyAskedQuestionsWithAttr = new List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>();
                int previousTime = 0;
                if (HttpContext.Current.Session["askedAssessmentItemsAttributes"] != null && (Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) > 1))
                {
                    previouslyAskedQuestionsWithAttr = (List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>)HttpContext.Current.Session["askedAssessmentItemsAttributes"];
                    if (previouslyAskedQuestionsWithAttr.Count > 0)
                    {
                        previousTime = previouslyAskedQuestionsWithAttr[0].TimeInSeconds;
                    }
                }

                //----------------------------------------------------------------------
                */





                // TO GET THE HIGHEST SCORE (START)
                //-----------------------------------------------------------------------------
             
                int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);             
                ICPCourseService.SequenceItem sequenceItem = null;
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                }


                // Commenting it out because the logic has been moved to Stored Procedure
                //-------------------------------------------------------------------------------
                /*
                double highestScore = 0;
                ICPTrackingService.LearnerStatistics[] results = null;
                switch (assessmentType)
                {
                    case CourseManager.LearnerStatisticsType.PreAssessment:
                        {
                            results = trackingService.GetPreAssessmentResult(learenrSessionID);
                            if (results.Length > 0)
                            {
                                highestScore = results[0].RawScore;
                            }
                            //learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PostAssessment:
                        {
                            results = trackingService.GetPostAssessmentResult(learenrSessionID);
                            if (results.Length > 0)
                            {
                                highestScore = results[0].RawScore;
                            }
                            //learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.Quiz:
                        {
                            results = trackingService.GetQuizResult(learenrSessionID, sequenceItem.Item_GUID);
                            if (results.Length > 0)
                            {
                                highestScore = results[0].RawScore;
                            }

                            //learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PracticeExam:
                        {
                            results = trackingService.GetPracticeExamResult(learenrSessionID, sequenceItem.Item_GUID);
                            if (results.Length > 0)
                            {
                                highestScore = results[0].RawScore;
                            }

                            //learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            break;
                        }
                }

                if (weightedScore < highestScore && results !=null && results.Length > 0 && results[0].Item_GUID != string.Empty) { return 0; }
                */
                //-------------------------------------------------------------------------------

                learnerStatistics.Item_GUID = sequenceItem.Item_GUID;

                //-----------------------------------------------------------------------------
                // TO GET THE HIGHEST SCORE (END)


                learnerStatistics.AssessmentAttemptNumber = currentAttemptNo;
                //currentAttemptNo = attemptNo;// learnerStatistics.AssessmentAttemptNumber;
                learnerStatistics.AssessmentType = assessmentType;
                learnerStatistics.LearningSession_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
                learnerStatistics.NumberAnswersCorrect = noOfAnswersCorrect;
                learnerStatistics.NumberAnswersIncorrect = noOfAnswersInCorrect;
                learnerStatistics.RawScore = weightedScore;
                learnerStatistics.MaxAtemptActionTaken = false;
                learnerStatistics.IsPass = isCurrentAssessmentPassed;
                learnerStatistics.RemediationCount = remediationCount;
                //learnerStatistics.TimeInSeconds = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assessmentStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                learnerStatistics.TimeInSeconds = assessmentTimeInSeconds;
                learnerStatistics.LearnerEnrollment_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                learnerStatistics.Statistic_Type = assessmentType;
                /*
                int examID = 0;
                if (System.Web.HttpContext.Current.Session["ExamID"] != null)
                {
                    examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
                }

                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.Quiz || selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PracticeExam || examID > 0)
                {
                    learnerStatistics.Statistic_Type = selectedQuestion.AssessmentType;// CourseManager.LearnerStatisticsType.QuizEnd;

                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    ICPCourseService.SequenceItem sequenceItem = null;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                    }
                }
                */
                //isSaved = trackingService.SaveLearnerStatistics(learnerStatistics);
                long learnerStatistics_ID = trackingService.SaveAssessmentScore_Game(learnerStatistics, masteryScore);


                /*
                ICPCourseService.AssessmentConfiguration currentAssessmentConfiguration = null;

                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PreAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.Quiz)
                {
                    currentAssessmentConfiguration = courseConfiguration.QuizConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PostAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                }
                else if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PracticeExam)
                {
                    currentAssessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                }

                //LCMS-8469 - In Case of No Result, LMS Web Service will not be called to update assessment score
                if (currentAssessmentConfiguration.ScoreType != ScoreType.NoResults)
                {
                    //LMS Integeration
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    if (source == 0)
                    {
                        IntegerationStatistics integerationStatistics = new IntegerationStatistics();
                        integerationStatistics.AssessmentType = selectedQuestion.AssessmentType;
                        integerationStatistics.AssessmentScore = weightedScore;
                        integerationStatistics.IsAssessmentPassed = isCurrentAssessmentPassed;
                        integerationStatistics.LearningSessionGuid = learenrSessionID;
                        integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.AssessmentCompletion;
                        HttpContext.Current.Session["IntegerationStatistics"] = integerationStatistics;
                        //Integeration integeration = IntegerationFactory.GetObject(source);
                        //integeration.SynchStatsToExternalSystem(integerationStatistics);
                    }
                }
                */
                System.Web.HttpContext.Current.Session["AssessmentType"] = assessmentType;

                return learnerStatistics_ID;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return 0;

            }
        }

        
        
        
        //LCMS-9213
        public long SaveAssessmentEndTrackingInfo_PauseResume(SelectedQuestion selectedQuestion, int currentAttemptNo, int masteryScore, int previousTimeSpent)
        {
            try
            {
                long learnerStatistics_ID = 0;

                learnerStatistics_ID = Convert.ToInt64(System.Web.HttpContext.Current.Session["Current_Assessment_LearnerStatistics_ID"]);

                if (learnerStatistics_ID > 0)
                    return learnerStatistics_ID;

                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                DateTime assessmentStartTime = Convert.ToDateTime(System.Web.HttpContext.Current.Session["AssessmentStartTime"]);
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);


                if (System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] == null || Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) == 0)
                {
                    previousTimeSpent = 0;
                }

                learnerStatistics.AssessmentAttemptNumber = currentAttemptNo;
                learnerStatistics.AssessmentType = selectedQuestion.AssessmentType;
                learnerStatistics.LearningSession_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
                learnerStatistics.NumberAnswersCorrect = 0;
                learnerStatistics.NumberAnswersIncorrect = 0;
                learnerStatistics.RawScore = -1;
                learnerStatistics.MaxAtemptActionTaken = false;
                learnerStatistics.IsPass = false;
                learnerStatistics.RemediationCount = Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]);
                learnerStatistics.TimeInSeconds = previousTimeSpent + Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assessmentStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                learnerStatistics.LearnerEnrollment_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                //LCMS-10266
                ICPCourseService.CourseConfiguration courseConfiguration = null;
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                }
                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PostAssessment 
                    && (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True" || courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment)
                    && (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True" || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)
                    )
                {
                    learnerStatistics.IsRepeatedAssessmentAttempt = (HttpContext.Current.Session["RandomAlternateWithPauseResume"] != null ? true : false);
                }
                //End LCMS-10266/////////
                int examID = 0;

                if (System.Web.HttpContext.Current.Session["ExamID"] != null)
                {
                    examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
                }

                if (selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.Quiz || selectedQuestion.AssessmentType == CourseManager.LearnerStatisticsType.PracticeExam || examID > 0)
                {
                    learnerStatistics.Statistic_Type = selectedQuestion.AssessmentType;// CourseManager.LearnerStatisticsType.QuizEnd;

                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    ICPCourseService.SequenceItem sequenceItem = null;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                    }
                }
                //isSaved = trackingService.SaveLearnerStatistics(learnerStatistics);
                learnerStatistics_ID = trackingService.SaveAssessmentScore(learnerStatistics, masteryScore);
                System.Web.HttpContext.Current.Session["Current_Assessment_LearnerStatistics_ID"] = learnerStatistics_ID;
                return learnerStatistics_ID;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return 0;

            }
        }
        //LCMS-9213

        /// <summary>
        /// This method puts AssessmentEnd track record through session
        /// </summary>
        /// <param name="selectedQuestion">SelectedQuestion an object, represent all the question which were asked during assessment</param>
        /// <param name="noOfAnswersCorrect">NoOfAnswersCorrect integer value</param>
        /// <param name="noOfAnswersInCorrect">NoOfAnswersInCorrect integer value</param>
        /// <returns>In case of success, returns true. False otherwise.</returns>
        public bool SaveAssessmentEndTrackingInfoInSession(SelectedQuestion selectedQuestion, int noOfAnswersCorrect, int noOfAnswersInCorrect, double weightedScore, bool isCurrentAssessmentPassed)
        {
            try
            {
                int attemptNo = 0;
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                ICPTrackingService.LearnerStatistics[] learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[1];
                ICPTrackingService.LearnerStatistics learnerStatistic = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                ICPTrackingService.LearnerStatistics[] previousLearnerStatistics = null;
                switch (selectedQuestion.AssessmentType)
                {
                    case CourseManager.LearnerStatisticsType.PreAssessment:
                        {
                            learnerStatistic.Statistic_Type = CourseManager.LearnerStatisticsType.PreAssessmentEnd;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PostAssessment:
                        {
                            learnerStatistic.Statistic_Type = CourseManager.LearnerStatisticsType.PostAssessmentEnd;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.Quiz:
                        {
                            learnerStatistic.Statistic_Type = CourseManager.LearnerStatisticsType.QuizEnd;
                            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                            ICPCourseService.SequenceItem sequenceItem = null;
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                                learnerStatistic.Item_GUID = sequenceItem.Item_GUID;
                            }
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PracticeExam:
                        {
                            learnerStatistic.Statistic_Type = CourseManager.LearnerStatisticsType.PracticeExamEnd;
                            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                            ICPCourseService.SequenceItem sequenceItem = null;
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                                learnerStatistic.Item_GUID = sequenceItem.Item_GUID;
                            }
                            break;
                        }
                }
                if (System.Web.HttpContext.Current.Session["AssessmentEndStats"] != null)
                {
                    previousLearnerStatistics = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                }
                if (previousLearnerStatistics == null)
                    previousLearnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[0];

                using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                    bool isPass = false;
                    bool actionTaken = false;
                    long learnerStatisticID = 0;
                    int remediationCount = 0;
                    courseManager.GetLastAssessmentResult(previousLearnerStatistics, 1, out isPass, out attemptNo, out actionTaken, out learnerStatisticID, out remediationCount);
                }

                learnerStatistic.AssessmentAttemptNumber = attemptNo + 1;
                learnerStatistic.AssessmentType = selectedQuestion.AssessmentType;
                learnerStatistic.LearningSession_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
                learnerStatistic.NumberAnswersCorrect = noOfAnswersCorrect;
                learnerStatistic.NumberAnswersIncorrect = noOfAnswersInCorrect;
                learnerStatistic.MaxAtemptActionTaken = false;
                learnerStatistic.RawScore = weightedScore;
                learnerStatistic.IsPass = isCurrentAssessmentPassed;
                learnerStatistics[0] = learnerStatistic;
                System.Web.HttpContext.Current.Session["AssessmentEndStats"] = learnerStatistics;
                System.Web.HttpContext.Current.Session["AssessmentType"] = selectedQuestion.AssessmentType;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }
        }
        /// <summary>
        /// This method puts question track record through tracking service.
        /// </summary>
        /// <param name="selectedQuestion">SelectedQuestion an object, represent all the question which were asked during assessment</param>
        /// <returns>In case of success, returns true. False otherwise.</returns>
        public bool SaveQuestionTrackingInfo(SelectedQuestion selectedQuestion,long learnerStatistic_Id, bool isSaveSingle)
        {
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
            if (isPreview)//as we do not want tracking in preview mode
            {
                return true;
            }
            try
            {
                bool isSaved = false;
                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPTrackingService.LearnerStatistics learnerStatistics = null;
                int questionAttemptedLength = selectedQuestion.QuestionInfos.Count;


                //int.TryParse(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"].ToString(), out questionAttemptedLength);

                if (questionAttemptedLength > selectedQuestion.QuestionInfos.Count)
                    questionAttemptedLength = selectedQuestion.QuestionInfos.Count;

                if (isSaveSingle)
                    questionAttemptedLength = 1;
               // else
                 //   questionAttemptedLength = questionAttemptedLength - 1;
                
                //foreach (QuestionInfo questionInfo in selectedQuestion.QuestionInfos)
                for (int index = 0; index < questionAttemptedLength; index++)
                {
                    QuestionInfo questionInfo = selectedQuestion.QuestionInfos[index];
                    learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                    learnerStatistics.AssessmentItemID = questionInfo.QuestionGuid;
                    learnerStatistics.AssessmentType = selectedQuestion.AssessmentType;
                    learnerStatistics.CorrectTF = questionInfo.IsCorrectlyAnswered;
                    learnerStatistics.Item_GUID = questionInfo.QuestionGuid;
                    learnerStatistics.LearningSession_ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerSessionIDPrimary"]);
                    learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.Question;
                    learnerStatistics.RemediationCount = Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]);
                    learnerStatistics.IsAssessmentItemToogled = questionInfo.ToogleFlag;

                    int arrayLength = questionInfo.AnswerIDs.Count < questionInfo.AnswerTexts.Count ? questionInfo.AnswerTexts.Count : questionInfo.AnswerIDs.Count;

                    learnerStatistics.LearnerStatisticsAnswers = new ICPTrackingService.LearnerStatisticsAnswer[arrayLength];

                    for (int initializerIndex = 0; initializerIndex < arrayLength; initializerIndex++)
                    {
                        learnerStatistics.LearnerStatisticsAnswers[initializerIndex] = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatisticsAnswer();
                    }

                    for (int studentAnswerIDIndex = 0; studentAnswerIDIndex < questionInfo.AnswerIDs.Count; studentAnswerIDIndex++)
                    {
                        string assessmentAnswerItemGUID = GetAssessmentAnswerItemGUID(questionInfo.QuestionID, questionInfo.AnswerIDs[studentAnswerIDIndex]);
                        learnerStatistics.LearnerStatisticsAnswers[studentAnswerIDIndex].AssessmentItemAnswerGUID = assessmentAnswerItemGUID;
                    }
                    for (int studentAnswerTextIndex = 0; studentAnswerTextIndex < questionInfo.AnswerTexts.Count; studentAnswerTextIndex++)
                    {
                        //LCMS-4061
                        learnerStatistics.LearnerStatisticsAnswers[studentAnswerTextIndex].Value += questionInfo.AnswerTexts[studentAnswerTextIndex];
                    }
                    learnerStatistics.CorrectAnswerGuids = questionInfo.CorrectAssessmentItemGuids;
                    learnerStatistics.LearnerStatisticsID = learnerStatistic_Id;

                    // Waqas Zakai
                    // LCMS-11066
                    // START
                    if (questionInfo.IsSkipped)
                    {
                        learnerStatistics.AnswerProvided = false;
                    }
                    else
                    {
                        learnerStatistics.AnswerProvided = true;
                    }                    
                    // LCMS-11066
                    // END
                    learnerStatistics.TestID = Convert.ToInt32(questionInfo.TestID);

                    //isSaved = trackingService.SaveLearnerStatistics(learnerStatistics);
                    isSaved = trackingService.SaveAssessmentItem(learnerStatistics);
                }

                return isSaved;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }

        }


        /// <summary>
        /// This method calculates the result of a question.
        /// </summary>
        /// <param name="assessmentItemID">AssessmentItemID integer value, represent the ID of question whom result is to be calculated</param>
        /// <param name="studentAssessmentAnswerIDs">StudentAssessmentAnswerIDs list of integer, represent the list AnswerIDs which student have responded.</param>
        /// <param name="studentAssessmentAnswerStrings">StudentAssessmentAnswerStrings list of string, represent the list AnswerTexts which student have responded.</param>
        /// <param name="questionIndex"></param>
        /// <returns></returns>
        public bool CheckQuestionResult(int assessmentItemID, List<int> studentAssessmentAnswerIDs, List<string> studentAssessmentAnswerStrings,bool toogleFlag, ref int questionIndex)
        {


            bool correctlyAnswered = false;
            //Get data from session related to asked question
            #region Getting data for asked Question

            questionIndex = 0;
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
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


            //Putting information back to session variable
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            selectedQuestions.QuestionInfos[questionIndex].IsCorrectlyAnswered = correctlyAnswered;
            selectedQuestions.QuestionInfos[questionIndex].AnswerIDs = new List<int>();
            selectedQuestions.QuestionInfos[questionIndex].AnswerTexts = new List<string>();
            selectedQuestions.QuestionInfos[questionIndex].ToogleFlag = toogleFlag;


            foreach (int studentAnswerID in studentAssessmentAnswerIDs)
            {
                selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Add(studentAnswerID);
            }
            foreach (string studentAnswerString in studentAssessmentAnswerStrings)
            {
                selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Add(studentAnswerString);
            }
            foreach (ICPAssessmentService.AssessmentItemAnswer correctAnswer in assessmentItem.AssessmentAnswers)
            {
                if (correctAnswer.IsCorrect)
                    selectedQuestions.QuestionInfos[questionIndex].CorrectAssessmentItemGuids += correctAnswer.AssessmentItemAnswerGuid + ",";
            }

            // Changed by Waqas Zakai 
            // LCMS LCMS-6984
            // START
            if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsPreview"]) == true)
            {

                if (selectedQuestions.AssessmentType == LearnerStatisticsType.PreAssessment)
                {
                    if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"] == null)
                    {
                        List<string> AskedAssessmentItemPre = new List<string>();
                        AskedAssessmentItemPre.Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid);
                        System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"] = AskedAssessmentItemPre;
                    }
                    else
                    {
                        ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPRE"]).Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid.ToString());
                    }
                }

                if (selectedQuestions.AssessmentType == LearnerStatisticsType.Quiz)
                {
                    if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"] == null)
                    {
                        List<string> AskedAssessmentItemPre = new List<string>();
                        AskedAssessmentItemPre.Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid);
                        System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"] = AskedAssessmentItemPre;
                    }
                    else
                    {
                        ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDQUIZ"]).Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid.ToString());
                    }
                }

                if (selectedQuestions.AssessmentType == LearnerStatisticsType.PostAssessment)
                {
                    if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"] == null)
                    {
                        List<string> AskedAssessmentItemPre = new List<string>();
                        AskedAssessmentItemPre.Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid);
                        System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"] = AskedAssessmentItemPre;
                    }
                    else
                    {
                        ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPOST"]).Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid.ToString());
                    }
                }

                if (selectedQuestions.AssessmentType == LearnerStatisticsType.PracticeExam)
                {
                    if (System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"] == null)
                    {
                        List<string> AskedAssessmentItemPracticeExam = new List<string>();
                        AskedAssessmentItemPracticeExam.Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid);
                        System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"] = AskedAssessmentItemPracticeExam;
                    }
                    else
                    {
                        ((List<string>)System.Web.HttpContext.Current.Session["AskedAssessmentItemsGUIDPracticeExam"]).Add(selectedQuestions.QuestionInfos[questionIndex].QuestionGuid.ToString());
                    }
                } 
            }
            // END


            //Updating Session
            System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;


            return correctlyAnswered;
        }


        /// <summary>
        /// This method return either ShowQuestion or ShowQuestionResult command according to policy
        /// Basically after calculating question result system will call this method to check whether to send next question or to send Question Result for previously asked question
        /// </summary>
        /// <param name="courseID">CourseID integer value, used to get policies from cache</param>
        /// <param name="courseSequenceIndex">CourseSequenceIndex integer value, represent the index of main course sequence</param>
        /// <param name="questionIndex">QuestionIndex integer value, represent the question index of question list stored in session</param>
        /// <returns>ShowQuestion or ShowQuestionResult command</returns>
        public object GetNextAvailableItem(int courseID, int courseSequenceIndex, int questionIndex, bool isSkipping)
        {
            object returnObject = new object();
            bool corrrectlyAnswered = false;
            bool askNextQuestion = true;
            bool showQuestionScore = false;
            bool showQuestionFeedBack = false;
            bool allowSkipping = false;
            string itemType = null;
            bool isAllowPauseResumeAssessment = false;

            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            //Setup variable based on polices.
            #region Get Policy variables

            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
            }

            if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
            {
                itemType = sequenceItem.ExamType;
            }
            else
            {
                itemType = sequenceItem.SequenceItemType;
            }


            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
            {
                #region Pre Assessment
                //if (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count == 0 && selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count == 0 && courseConfiguration.PreAssessmentConfiguration.AllowSkippingQuestion && !courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo)
                if (isSkipping && courseConfiguration.PreAssessmentConfiguration.AllowSkippingQuestion && !courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo) //Abdus Samad LCMS-12310 
                    allowSkipping = true;

                if (courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo)// && (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count > 0 || selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count > 0))
                {
                    showQuestionScore = true;
                    askNextQuestion = false;
                    if (courseConfiguration.PreAssessmentConfiguration.QuestionLevelResult)
                    {
                        showQuestionFeedBack = true;
                    }
                }

                isAllowPauseResumeAssessment = courseConfiguration.PreAssessmentConfiguration.AllowPauseResumeAssessment;

                #endregion
            }
            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
            {
                #region Quiz
                //if (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count == 0 && selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count == 0 && courseConfiguration.QuizConfiguration.AllowSkippingQuestion && !courseConfiguration.QuizConfiguration.ScoreAsYouGo)
                if (isSkipping && courseConfiguration.QuizConfiguration.AllowSkippingQuestion && !courseConfiguration.QuizConfiguration.ScoreAsYouGo)//Abdus Samad LCMS-12310 
                    allowSkipping = true;

                if (courseConfiguration.QuizConfiguration.ScoreAsYouGo)// && (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count > 0 || selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count > 0))
                {
                    showQuestionScore = true;
                    askNextQuestion = false;
                    if (courseConfiguration.QuizConfiguration.QuestionLevelResult)
                    {
                        showQuestionFeedBack = true;
                    }
                }
                isAllowPauseResumeAssessment = courseConfiguration.QuizConfiguration.AllowPauseResumeAssessment;
                #endregion
            }
            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
            {
                #region Post Assessment
                //if (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count == 0 && selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count == 0 && courseConfiguration.PostAssessmentConfiguration.AllowSkippingQuestion && !courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo)                
                if (isSkipping && courseConfiguration.PostAssessmentConfiguration.AllowSkippingQuestion && !courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo)//Abdus Samad LCMS-12310 
                    allowSkipping = true;


                if (courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo)// && (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count > 0 || selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count > 0))
                {
                    showQuestionScore = true;
                    askNextQuestion = false;
                    if (courseConfiguration.PostAssessmentConfiguration.QuestionLevelResult)
                    {
                        showQuestionFeedBack = true;
                    }
                }
                isAllowPauseResumeAssessment = (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True" || courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment) ;
                #endregion
            }
            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
            {
                #region Practice Exam
                ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                //if (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count == 0 && selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count == 0 && assessmentConfiguration.AllowSkippingQuestion && !assessmentConfiguration.ScoreAsYouGo)
                if (isSkipping && assessmentConfiguration.AllowSkippingQuestion && !assessmentConfiguration.ScoreAsYouGo)//Abdus Samad LCMS-12310 
                    allowSkipping = true;

                if (assessmentConfiguration.ScoreAsYouGo)// && (selectedQuestions.QuestionInfos[questionIndex].AnswerIDs.Count > 0 || selectedQuestions.QuestionInfos[questionIndex].AnswerTexts.Count > 0))
                {
                    showQuestionScore = true;
                    askNextQuestion = false;
                    if (assessmentConfiguration.QuestionLevelResult)
                    {
                        showQuestionFeedBack = true;
                    }
                }
                isAllowPauseResumeAssessment = assessmentConfiguration.AllowPauseResumeAssessment;

                #endregion
            }
            #endregion
            if (HttpContext.Current.Session["AssessmentAllQuestionsAttempted"] == null) // Fix for LCMS-11066
                selectedQuestions.QuestionInfos[questionIndex].IsSkipped = allowSkipping;
            corrrectlyAnswered = selectedQuestions.QuestionInfos[questionIndex].IsCorrectlyAnswered;
            System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;
            //LCMS-9213
            if (isAllowPauseResumeAssessment)
            {
                bool isPreview = true;
                bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);

                if (!isPreview)
                {
                    int currAttemptNo = 1;
                    int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);

                    long learnerStatistics_Id = SaveAssessmentEndTrackingInfo_PauseResume(selectedQuestions, currAttemptNo, 0, 0);
                    SelectedQuestion singleSelectedQuestions = new SelectedQuestion();
                    singleSelectedQuestions.AssessmentParentId = selectedQuestions.AssessmentParentId;
                    singleSelectedQuestions.AssessmentType = selectedQuestions.AssessmentType;
                    singleSelectedQuestions.QuestionInfos.Add(selectedQuestions.QuestionInfos[questionIndex]);
                    SaveQuestionTrackingInfo(singleSelectedQuestions, learnerStatistics_Id,true);
                }
            }
            //LCMS-9213
            //With the help of policy variables we have to decide what to do next
            if (askNextQuestion)
            {
                returnObject = GetNextQuestion();
            }
            else if (showQuestionScore)
            {
                string feedback = string.Empty;

                //LCMS-4961
                if (showQuestionFeedBack)
                {
                    ICPAssessmentService.AssessmentItem[] assessmentItems = null;
                    assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];

                    feedback = GetFeedback(assessmentItems, selectedQuestions.QuestionInfos[questionIndex].AnswerIDs, selectedQuestions.QuestionInfos[questionIndex].AnswerTexts, questionIndex, corrrectlyAnswered);
                }

                returnObject = CreateQuestionResultCommandObject(corrrectlyAnswered, feedback, false);
            }

            return returnObject;

        }


        public string GetFeedback(ICPAssessmentService.AssessmentItem[] assessmentItems, List<int> AnswerIDs, List<string> AnswerTexts, int questionIndex, bool corrrectlyAnswered)
        {
            string feedback = string.Empty;


            //LCMS-4961
            if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.NoFeedback))
            {
                return feedback;
            }

            
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            string NoFeedbackText = string.Empty;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                NoFeedbackText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.FeedbackNotProvidedText, brandCode, variant);
            }
                //Check for Unanswered
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching))
                {
                    if (GetNullCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "<b>" + NoFeedbackText + "</b>";
                        return feedback;
                    }
                }
                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    if (GetEmptyCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "<b>" + NoFeedbackText + "</b>";
                        return feedback;
                    }
                }
                else
                {
                    if (AnswerIDs == null || AnswerIDs.Count == 0)
                    {
                        feedback = "<b>" + NoFeedbackText + "</b>";
                        return feedback;
                    }
                }

            if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.Single))
            {
                feedback = assessmentItems[questionIndex].Feedback;
            }
            else if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.CorrectIncorrect))
            {
                if (corrrectlyAnswered)
                {
                    feedback = assessmentItems[questionIndex].Correctfeedback;
                }
                else
                {
                    feedback = assessmentItems[questionIndex].Incorrectfeedback;
                }
            }
            else if (assessmentItems[questionIndex].Feedbacktype.Equals(_360Training.BusinessEntities.FeedbackType.AnswerChoice))
            {

                //Check for Unanswered
                /*
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching))
                {
                    if (GetNullCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }
                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    if (GetEmptyCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }
                else
                {
                    if (AnswerIDs == null || AnswerIDs.Count == 0)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }*/
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Ordering))
                {
                    #region "Ordering"
                    if (OnlyDefaultFeedback(assessmentItems, questionIndex))
                    {
                        return (assessmentItems[questionIndex].Feedback);
                    }

                    for (int i = 0; i < AnswerIDs.Count; i++)
                    {
                        for (int j = 0; j < assessmentItems[questionIndex].AssessmentAnswers.Length; j++)
                        {
                            if (AnswerIDs[i].Equals(assessmentItems[questionIndex].AssessmentAnswers[j].AssessmentItemAnswerID))
                            {
                                if (assessmentItems[questionIndex].AssessmentAnswers[j].Usedefaultfeedbacktf)
                                {
                                    if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                        feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                                    break;
                                }
                                else
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[j].Feedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[j].Feedback + "</p><br>";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    #region "FITB"

                    if (OnlyDefaultFeedback(assessmentItems, questionIndex))
                    {
                        return (assessmentItems[questionIndex].Feedback);
                    }

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
                                    if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                        feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                                    break;
                                }
                                else
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[i].Feedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Feedback + "</p><br>";
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

                    if (OnlyDefaultFeedback(assessmentItems, questionIndex))
                    {
                        return (assessmentItems[questionIndex].Feedback);
                    }

                    if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching) && GetNullCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "<b>" + NoFeedbackText + ".</b>";
                        return feedback;
                    }

                    for (int index = 0; index < AnswerIDs.Count; index++)
                    {
                        for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID.Equals(AnswerIDs[index]))
                            {
                                if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                                {
                                    if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                        feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                                    break;
                                }
                                else
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[i].Feedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Feedback + "</p><br>";
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

                //Check for Unanswered
                /*
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching))
                {
                    if (GetNullCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }
                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    if (GetEmptyCountinAnswerText(AnswerTexts) == assessmentItems[questionIndex].AssessmentAnswers.Length)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }
                else
                {
                    if (AnswerIDs == null || AnswerIDs.Count == 0)
                    {
                        feedback = "This question was not attempted therefore no feedback may be provided.";
                        return feedback;
                    }
                }*/

                //Check for Unanswered

                if (OnlyDefaultFeedback(assessmentItems, questionIndex))
                {
                    return (assessmentItems[questionIndex].Feedback);
                }

                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.TextInputFITB))
                {
                    #region Fill in the blank Question

                    bool isCaseSensitive = ((ICPAssessmentService.FillInTheBlankQuestion)assessmentItems[questionIndex]).IsAnswerCaseSensitive;
                    bool isCorrect = false;


                    for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                    {
                        if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                        {
                            if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                        }
                        else
                        {
                            isCorrect = false;
                            for (int index = 0; index < AnswerTexts.Count; index++)
                            {
                                if (String.Compare(AnswerTexts[assessmentItems[questionIndex].AssessmentAnswers[i].DisplayOrder-1], assessmentItems[questionIndex].AssessmentAnswers[i].Label, !isCaseSensitive) == 0)
                                {
                                    if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                    {
                                        feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                        isCorrect = true;
                                        break;
                                    }
                                }
                            }
                            if (!isCorrect)
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (!assessmentItems[questionIndex].QuestionType.Equals(QuestionType.MultipleSelectMCQ))
                {
                    int studentAnswerOrder = 0;
                    for (int index = 0; index < AnswerIDs.Count; index++)
                    {
                        studentAnswerOrder++;
                        for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID.Equals(AnswerIDs[index]))
                            {
                                //Below check for Default Feedback
                                if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                                {
                                    if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                        feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                                    break;
                                }

                                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Ordering))
                                {
                                    #region Ordering
                                    if (((ICPAssessmentService.OrderingAssessmentItemAnswer)assessmentItems[questionIndex].AssessmentAnswers[i]).CorrectOrder == studentAnswerOrder)
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                                else if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.Matching))
                                {
                                    #region "Matching"

                                    if (((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItems[questionIndex].AssessmentAnswers[i]).RightItemText.Equals(AnswerTexts[i]))
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                            break;
                                        }
                                    }
                                    #endregion
                                }
                                else if (!assessmentItems[questionIndex].QuestionType.Equals(QuestionType.MultipleSelectMCQ))
                                {

                                    #region "Others"


                                    if (assessmentItems[questionIndex].AssessmentAnswers[i].IsCorrect)
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                        {
                                            feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                            break;
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                }
                //LCMS-5438
                if (assessmentItems[questionIndex].QuestionType.Equals(QuestionType.MultipleSelectMCQ))
                {
                    for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
                    {
                        //Below check for Default Feedback
                        if (assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                        {
                            if (feedback.IndexOf(assessmentItems[questionIndex].Feedback) == -1)
                                feedback += "<p>" + assessmentItems[questionIndex].Feedback + "</p><br>";
                            continue;
                        }

                        if (AnswerIDs.Contains(assessmentItems[questionIndex].AssessmentAnswers[i].AssessmentItemAnswerID))
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].IsCorrect)
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                }
                            }
                            else
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                }
                            }
                        }
                        else
                        {
                            if (assessmentItems[questionIndex].AssessmentAnswers[i].IsCorrect)
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback + "</p><br>";
                                }
                            }
                            else
                            {
                                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Incorrectfeedback.Equals(string.Empty))
                                {
                                    feedback += "<p>" + assessmentItems[questionIndex].AssessmentAnswers[i].Correctfeedback + "</p><br>";
                                }
                            }
                        }
                    }
                }
                //LCMS-5438
            }

            

            //end LCMS-4961

            //start LCMS-5277 
            /*
            if (feedback != string.Empty)
            {
                string[] lines = System.Text.RegularExpressions.Regex.Split(feedback, "</p>");
                List<string> items = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Replace("<p><p>", "").Trim().Equals(string.Empty) && !items.Contains(line.Replace("<p><p>", "").Trim() + "<br><br>"))
                    {
                        items.Add(line.Replace("<p><p>", "").Trim() + "<br><br>");
                    }
                }
                feedback = "<p>";
                foreach (string item in items)
                {
                    feedback += item;
                }
                feedback = feedback.Remove(feedback.LastIndexOf("<br>") - 4) + "</p>";
            }
             */
            //end LCMS-5277 
            if (feedback.IndexOf("<br>") > 0)
            {
                feedback = feedback.Substring(0,feedback.LastIndexOf("<br>"));
            }
            return feedback;
        }

        public int GetNullCountinAnswerText(List<string> AnswerTexts)
        { 
            int count = 0;

            foreach (string str in AnswerTexts)
            {
                if (str.Equals("null"))
                    count++;
            }

            return count;
        }

        public int GetEmptyCountinAnswerText(List<string> AnswerTexts)
        {
            int count = 0;

            foreach (string str in AnswerTexts)
            {
                if (str.Equals(""))
                    count++;
            }

            return count;
        }


        private bool OnlyDefaultFeedback(ICPAssessmentService.AssessmentItem[] assessmentItems, int questionIndex)
        {
            for (int i = 0; i < assessmentItems[questionIndex].AssessmentAnswers.Length; i++)
            {
                if (!assessmentItems[questionIndex].AssessmentAnswers[i].Usedefaultfeedbacktf)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method sets the question pointer to appropriate index and sends the first question which was skipped.
        /// </summary>
        /// <returns>ShowQuestion command</returns>
        public object StartAskingSkippedQuestion()
        {
            //Set pointer to first skipped question
            int questionIndex;
            questionIndex = GetIndexOfFirstSkippedQuestion();
            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = questionIndex;
            return GetNextQuestion();
        }

        /// <summary>
        /// This method sets the question pointer to appropriate index based on client request and sends the question which was requested.
        /// </summary>
        /// <param name="assessmentItemID"></param>
        /// <returns>ShowQuestion command</returns>
        public object AskSpecifiedQuestion(int assessmentItemID)
        {
            //Set pointer to specifed question
            int questionIndex;
            questionIndex = GetIndexOfQuestion(assessmentItemID);
            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = questionIndex;
            return GetNextQuestion();
        }


        /// <summary>
        /// This method create ShowIndividualQuestionScore command
        /// </summary>
        /// <returns>ShowIndividualQuestionScore command</returns>

        public object CreateIndividualQuestionScoreCommandObject()
        {
            return CreateIndividualQuestionScoreCommandObject(null);
        }

        public object CreateIndividualQuestionScoreCommandObject(List<ShowTopicScoreSummary> showTopicScoreSummaries)
        {

            ShowIndividualQuestionScore showIndividualQuestionScore = null;
            IndividualQuestionScore individualQuestionScore = null;

            if (showTopicScoreSummaries == null)
            {
                showIndividualQuestionScore = new ShowIndividualQuestionScore();
                showIndividualQuestionScore.CommandName = CommandNames.ShowIndividualQuestionScore;
                showIndividualQuestionScore.IndividualQuestionScores = new List<IndividualQuestionScore>();
            }
            
            
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            int i = 0;
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            string assessmentItemText = string.Empty;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                assessmentItemText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentQuestion, brandCode, variant);
            }
            foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                individualQuestionScore = new IndividualQuestionScore();

                individualQuestionScore.AssessmentItemID = questionInfo.QuestionID;
                individualQuestionScore.AssessmentItemStem = assessmentItemText;
                individualQuestionScore.QuestionNo = i + 1;
                individualQuestionScore.IsCorrect = questionInfo.IsCorrectlyAnswered;
                individualQuestionScore.AssessmentItemType = questionInfo.QuestionType;


                if (questionInfo.AnswerTexts.Count > 0)
                {
                    //  string encodedAnswer = string.Empty;
                    foreach (string anserText in questionInfo.AnswerTexts)
                    {
                        //encodedAnswer = System.Web.HttpContext.Current.Server.HtmlEncode(anserText);
                        individualQuestionScore.Answers.Add(anserText);
                    }
                }
                if (questionInfo.AnswerIDs.Count > 0)
                {
                    ICPAssessmentService.AssessmentItem[] assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
                    int assessmentItemIndex = GetAssessmentItemIndexByID(questionInfo.QuestionID, assessmentItems);
                    int assessmentItemAnswerIndex = 0;
                    string answerAlphabetCode = string.Empty;
                    foreach (int answerID in questionInfo.AnswerIDs)
                    {
                        assessmentItemAnswerIndex = GetAssessmentItemAnswerIndex(answerID, assessmentItems[assessmentItemIndex]);
                        //answerAlphabetCode = Convert.ToChar(assessmentItemAnswerIndex + 65).ToString();
                        if (questionInfo.QuestionType != QuestionType.TrueFalse)
                            answerAlphabetCode = Convert.ToChar(assessmentItemAnswerIndex + 65).ToString();
                        else
                        {
                            // LCMS-9665
                            // -------------------------------------------------------------------------------------------------------------------
                            //answerAlphabetCode = GetAssessmentItemAnswer(answerID, assessmentItems[assessmentItemIndex]).Value;
                            ICP4.BusinessLogic.ICPAssessmentService.AssessmentItemAnswer aia = GetAssessmentItemAnswer(answerID, assessmentItems[assessmentItemIndex]);
                            if (aia != null)
                            {
                                answerAlphabetCode = aia.Value;
                            }
                            // -------------------------------------------------------------------------------------------------------------------
                            
                        }
                        individualQuestionScore.Answers.Add(answerAlphabetCode);
                    }
                }
                if (showTopicScoreSummaries != null)
                {
                    for (int j=0; j<showTopicScoreSummaries.Count;j++)
                    {
                        if (showTopicScoreSummaries[j].TopicScoreSummary.TopicID == questionInfo.AssessmentBinderID)
                        {
                            if (showTopicScoreSummaries[j].TopicScoreSummary.ShowIndividualQuestionScore == null)
                            {
                                showTopicScoreSummaries[j].TopicScoreSummary.ShowIndividualQuestionScore = new ShowIndividualQuestionScore();
                                showTopicScoreSummaries[j].TopicScoreSummary.ShowIndividualQuestionScore.IndividualQuestionScores = new List<IndividualQuestionScore>();
                            }
                            showTopicScoreSummaries[j].TopicScoreSummary.ShowIndividualQuestionScore.IndividualQuestionScores.Add(individualQuestionScore);
                            break;
                        }
                    }
                    
                }
                else
                {
                    showIndividualQuestionScore.IndividualQuestionScores.Add(individualQuestionScore);
                }
                i++;
            }
            return showIndividualQuestionScore;
        }


        /// <summary>
        /// This method send the ShowQuestion command in remidiation mode 
        /// </summary>
        /// <returns>ShowQuestion command of remidiation mode</returns>
        public object ShowAnswers()
        {
            return GetNextBackRemidiationQuestion(1);
        }


        /// <summary>
        /// This method send ShowQuestion command according to NEXT/BACK request.
        /// </summary>
        /// <param name="direction">Direction integer value, 1 represent means NEXT and -1 means BACK</param>
        /// <returns>ShowQuestion command of remidiation mode</returns>
        public object GetNextBackRemidiationQuestion(int direction)
        {
            //create next question command
            object returnObject = new object();

            int assessmentSequenceNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]);
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            SelectedQuestion selectedQuestion = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
            selectedQuestion = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            
            int examID = 0;
            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }

            bool showQuestionRemidiation = true;
            if (direction == 1)
            {
                if (assessmentItems !=null && assessmentSequenceNo > assessmentItems.Length - 1)
                {
                    showQuestionRemidiation = false;
                }

            }
            else if (direction == -1)
            {
                if (assessmentSequenceNo < 0)
                {
                    showQuestionRemidiation = false;
                }
            }

            if (showQuestionRemidiation)
            {
                ICPAssessmentService.AssessmentItem assessmentItem = new ICPAssessmentService.AssessmentItem();
                assessmentItem = assessmentItems[assessmentSequenceNo];

                //Setup variable based on polices.
                #region Get Policy variables
                bool showQuestionFeedBack = false;
                bool contentRemidiationAllowed = false;
                ICPCourseService.CourseConfiguration courseConfiguration = null;
                ICPCourseService.SequenceItem sequenceItem = null;
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {

                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                    string itemType = "";
                    if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
                    {
                        itemType = sequenceItem.ExamType;
                    }
                    else
                    {
                        itemType = sequenceItem.SequenceItemType;
                    }

                    if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                    {
                        #region Pre Assessment

                        if (courseConfiguration.PreAssessmentConfiguration.QuestionLevelResult)
                        {
                            showQuestionFeedBack = true;
                        }

                        if (courseConfiguration.PreAssessmentConfiguration.ContentRemediation)
                        {
                            contentRemidiationAllowed = false;
                        }



                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                    {
                        #region Quiz

                        if (courseConfiguration.QuizConfiguration.QuestionLevelResult)
                        {
                            showQuestionFeedBack = true;
                        }

                        if (courseConfiguration.QuizConfiguration.ContentRemediation)
                        {
                            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                            {
                                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                                string[] sceneGUIDs = assessmentService.GetAssessmentItemScene(assessmentItem.AssessmentItemGuid, courseID);
                                if (sceneGUIDs != null && sceneGUIDs.Length > 0)
                                    contentRemidiationAllowed = true;
                            }
                        }


                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                    {
                        #region Post Assessment

                        if (courseConfiguration.PostAssessmentConfiguration.QuestionLevelResult)
                        {
                            showQuestionFeedBack = true;
                        }
                        if (courseConfiguration.PostAssessmentConfiguration.ContentRemediation)
                        {
                            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                            {
                                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                                string[] sceneGUIDs = assessmentService.GetAssessmentItemScene(assessmentItem.AssessmentItemGuid, courseID);
                                if (sceneGUIDs != null && sceneGUIDs.Length > 0)
                                    contentRemidiationAllowed = true;
                            }
                        }
                        #endregion
                    }
                    else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                    {
                        ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                        assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];

                        if (assessmentConfiguration.QuestionLevelResult)
                        {
                            showQuestionFeedBack = true;
                        }

                        if (assessmentConfiguration.ContentRemediation)
                        {
                            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                            {
                                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                                string[] sceneGUIDs = assessmentService.GetAssessmentItemScene(assessmentItem.AssessmentItemGuid, courseID);
                                if (sceneGUIDs != null && sceneGUIDs.Length > 0)
                                    contentRemidiationAllowed = true;
                            }
                        }
                    }
                }


                #endregion

                string audioURL = string.Empty;
                string templateType = string.Empty;
                string visualTopType = string.Empty;
                string assessmentItemTemplate = GetAssessmentItemTemplate(assessmentItem.AssessmentItemID, out audioURL, out templateType, out visualTopType);
                string feedback = this.GetFeedback(assessmentItems, selectedQuestion.QuestionInfos[assessmentSequenceNo].AnswerIDs, selectedQuestion.QuestionInfos[assessmentSequenceNo].AnswerTexts, assessmentSequenceNo, selectedQuestion.QuestionInfos[assessmentSequenceNo].IsCorrectlyAnswered);

                returnObject = CreateQuestionCommandObject(assessmentItem, assessmentSequenceNo + 1, assessmentItems.Length, selectedQuestion.QuestionInfos[assessmentSequenceNo], showQuestionFeedBack, contentRemidiationAllowed, true, sequenceItem.SequenceItemType, assessmentItemTemplate, audioURL, templateType, visualTopType, feedback);
            }
            else
            {
                //returnObject = CreateIndividualQuestionScoreCommandObject();                
                //=======================================================================================

                int masteryScore = 80;
                bool showPercentageScore = false;
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                bool scoreAsYouGo = false;
                bool allowSkipping = false;

                #region Get Policy variables

                ICPCourseService.CourseConfiguration courseConfiguration = null;
                ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
                ICPCourseService.SequenceItem sequenceItem = null;
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                }

                string itemType = "";
                if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
                {
                    itemType = sequenceItem.ExamType;
                }
                else
                {
                    itemType = sequenceItem.SequenceItemType;
                }

                if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                {
                    #region Pre Assessment
                    assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;
                    if (courseConfiguration.PreAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }

                    masteryScore = courseConfiguration.PreAssessmentConfiguration.MasteryScore;
                    scoreAsYouGo = courseConfiguration.PreAssessmentConfiguration.ScoreAsYouGo;
                    allowSkipping = courseConfiguration.PreAssessmentConfiguration.AllowSkippingQuestion;
                    #endregion
                }
                else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                {
                    #region Quiz
                    assessmentConfiguration = courseConfiguration.QuizConfiguration;
                    if (courseConfiguration.QuizConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }

                    masteryScore = courseConfiguration.QuizConfiguration.MasteryScore;
                    scoreAsYouGo = courseConfiguration.QuizConfiguration.ScoreAsYouGo;
                    allowSkipping = courseConfiguration.QuizConfiguration.AllowSkippingQuestion;
                    #endregion
                }
                else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                {
                    assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                    #region Post Assessment
                    if (courseConfiguration.PostAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }

                    masteryScore = courseConfiguration.PostAssessmentConfiguration.MasteryScore;
                    scoreAsYouGo = courseConfiguration.PostAssessmentConfiguration.ScoreAsYouGo;
                    allowSkipping = courseConfiguration.PostAssessmentConfiguration.AllowSkippingQuestion;
                    #endregion
                }
                else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                {
                    ICPCourseService.AssessmentConfiguration assessmentConfigurationPE = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                    assessmentConfigurationPE = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];

                    assessmentConfiguration = assessmentConfigurationPE;
                    if (assessmentConfiguration.ScoreType == ScoreType.PassFail)
                    {
                        showPercentageScore = false;
                    }
                    else
                    {
                        showPercentageScore = true;
                    }

                    masteryScore = assessmentConfiguration.MasteryScore;
                    scoreAsYouGo = assessmentConfiguration.ScoreAsYouGo;
                    allowSkipping = assessmentConfiguration.AllowSkippingQuestion;

                }
                #endregion

                int numberOfCorrect = 0;
                int numberOfInCorrect = 0;
                bool isAssessmentPass = false;
                if (CheckIsThereAnySkippedQuestion())
                {
                    returnObject = CreateSkippedQuestionCommandObject();
                }
                else
                {

                    selectedQuestion = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

                  //  if (scoreAsYouGo)
                   // {
                        DataTable dt = null;
                        double score = -1;
                        bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
                        int currentAttemptNo = 1;

                        using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                        {
                            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                            courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                            sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        }

                        ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                        trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                        if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
                        { 
                            assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;

                            if (!isPreview)
                            {
                                ICPTrackingService.LearnerStatistics[] learnerStatistics = null;
                                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                                learnerStatistics = trackingService.GetPreAssessmentResult(learnerSessionID);
                                currentAttemptNo = learnerStatistics[learnerStatistics.Length - 1].AssessmentAttemptNumber;
                            }
                        }

                        else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
                        { 
                            assessmentConfiguration = courseConfiguration.QuizConfiguration;
                            if (!isPreview)
                            {
                                ICPTrackingService.LearnerStatistics[] learnerStatistics = null;
                                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                                int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                                sequenceItem = new CourseManager.CourseManager().GetSequenceItem(courseID, seqNo);
                                learnerStatistics = trackingService.GetQuizResult(learnerSessionID, sequenceItem.Item_GUID);
                                currentAttemptNo = learnerStatistics[learnerStatistics.Length - 1].AssessmentAttemptNumber;
                            }
                        }

                        else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
                        { 
                            assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                            if (!isPreview)
                            {
                                ICPTrackingService.LearnerStatistics[] learnerStatistics = null;
                                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                                learnerStatistics = trackingService.GetPostAssessmentResult(learnerSessionID);
                                currentAttemptNo = learnerStatistics[learnerStatistics.Length - 1].AssessmentAttemptNumber;
                            }
                        }

                        else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
                        {
                            ICPCourseService.AssessmentConfiguration assessmentConfigurationPE = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                            assessmentConfigurationPE = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];

                            assessmentConfiguration = assessmentConfigurationPE;
                            if (!isPreview)
                            {
                                ICPTrackingService.LearnerStatistics[] learnerStatistics = null;
                                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                                int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                                sequenceItem = new CourseManager.CourseManager().GetSequenceItem(courseID, seqNo);
                                learnerStatistics = trackingService.GetPracticeExamResult(learnerSessionID, sequenceItem.Item_GUID);
                                currentAttemptNo = learnerStatistics[learnerStatistics.Length - 1].AssessmentAttemptNumber;
                            }
                        }

                        if (assessmentConfiguration != null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
                        {
                            dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                            try
                            {
                                score = (double)dt.Compute("SUM(WeightedScore)", "");
                            }
                            catch (Exception exp)
                            {
                                score = 0.0;
                            }
                            isAssessmentPass = (score >= masteryScore);
                        }
                        else
                        {
                            if (examID > 0 && (assessmentConfiguration !=null && assessmentConfiguration.ScoreType != ScoreType.NoResults)) // calculate the data table for exam to summarize score topic wise
                            {
                                int temp1 = 0, temp2 = 0;
                                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref temp1, ref temp2);
                            }

                            CalculateAssessmentResult(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                            score = (Convert.ToDouble(numberOfCorrect) / Convert.ToDouble(numberOfCorrect + numberOfInCorrect)) * 100.00;
                            if (score >= masteryScore)
                                isAssessmentPass = true;
                            else
                                isAssessmentPass = false;
                        }

                        //                            isAssessmentPass = CalculateAssessmentResult(masteryScore, ref numberOfCorrect, ref numberOfInCorrect);
                        
                        
                        if (isPreview)
                        {
                            ICPTrackingService.LearnerStatistics[] learnerStatisticsPreview = null;
                            learnerStatisticsPreview = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                            if (learnerStatisticsPreview != null && learnerStatisticsPreview.Length > 0)
                            {
                                currentAttemptNo = learnerStatisticsPreview[learnerStatisticsPreview.Length - 1].AssessmentAttemptNumber;
                            }
                        }
                        
                        returnObject = CreateAssessmentScoreSummaryCommandObject(showPercentageScore, isAssessmentPass, numberOfCorrect, assessmentItems.Length, currentAttemptNo, score, dt);
                        
                        //=======================================================================================
                   // }
                    
                }
            }
            return returnObject;
        }
            
        /// <summary>
        /// This method send ShowQuestion command according to client request.
        /// </summary>
        /// <param name="assessmentItemID">AssessmentItemID integer value</param>
        /// <returns>ShowQuestion command of remidiation mode</returns>
        public object ShowSpecifiedRemidationQuestion(int assessmentItemID)
        {
            //Set pointer to specifed question
            object returnObject = new object();
            int questionIndex;
            questionIndex = GetIndexOfQuestion(assessmentItemID);
            System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = questionIndex;
            returnObject = GetNextBackRemidiationQuestion(1);

            return returnObject;
        }

        /// <summary>
        /// This method ends the assessment forcefully thus marks the unasnwered question as wrong and track them.
        /// </summary>
        /// <param name="masteryScore">MasteryScore integer value, policy variable</param>
        /// <returns></returns>
        public bool EndAssessmentForcefully(int masteryScore)
        {
            int noOfCorrect = 0;
            int noOfIncorrect = 0;
            bool isAssessmentPass = false;
            bool isSuccessfullySaved = false;
            SelectedQuestion selectedQuestions = null;

            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            MarkUnAnsweredQuestionsIncorrect(selectedQuestions);
            DataTable dt = null;
            double score = -1;
            


            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);                    
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            ICPCourseService.AssessmentConfiguration assessmentConfiguration = null;
            ICPCourseService.SequenceItem sequenceItem = null;
            string itemType = null;
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
                {
                    itemType = sequenceItem.ExamType;
                }
                else
                {
                    itemType = sequenceItem.SequenceItemType;
                }
            }


            if (itemType == CourseManager.SequenceItemTypeName.PreAssessment)
            {assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;}

            else if (itemType == CourseManager.SequenceItemTypeName.Quiz)
            {assessmentConfiguration = courseConfiguration.QuizConfiguration;}

            else if (itemType == CourseManager.SequenceItemTypeName.PostAssessment)
            {assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;}

            else if (itemType == CourseManager.SequenceItemTypeName.PracticeExam)
            { assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"]; }

            if (assessmentConfiguration !=null && assessmentConfiguration.UseWeightedScore && assessmentConfiguration.AdvanceQuestionSelectionType != ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE) // if weighted scrore policy is on
            {
                dt = CalculateAssessmentResultAsWeightedScore(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                try
                {
                    score = (double)dt.Compute("SUM(WeightedScore)", "");
                }
                catch (Exception exp)
                {
                    score = 0.0;
                }
                isAssessmentPass = (score >= masteryScore);
            }
            else
            {
                CalculateAssessmentResult(masteryScore, ref noOfCorrect, ref noOfIncorrect);
                score = (Convert.ToDouble(noOfCorrect) / Convert.ToDouble(selectedQuestions.QuestionInfos.Count)) * 100.00;
                if (score >= masteryScore)
                    isAssessmentPass = true;
                else
                    isAssessmentPass = false;
            }

            int currAttemptNo = 1;
            int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);
            if ((System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True" || courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment) && (System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True" || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE ||
                                                                                                   courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS) )
            {
                if (Convert.ToBoolean(HttpContext.Current.Session["AssessmentAllQuestionsAttempted"]) == false && Convert.ToBoolean(HttpContext.Current.Session["AssessmentAllQuestionsDisplayed"]) == false)
                {
                    score = -2;
                    isAssessmentPass = false;
                }
            }
            long learnerStatistic_Id = SaveAssessmentEndTrackingInfo(selectedQuestions, noOfCorrect, noOfIncorrect, currAttemptNo, score, isAssessmentPass, masteryScore);

            
            // LCMS-9213
            //----------------------------------------------------------------------------
            if (assessmentConfiguration.AllowPauseResumeAssessment)
            {
                //for (int questionIndex = 0; questionIndex < selectedQuestions.QuestionInfos.Count; questionIndex++)
                //{
                //    SelectedQuestion singleSelectedQuestions_single = new SelectedQuestion();
                //    singleSelectedQuestions_single.AssessmentParentId = selectedQuestions.AssessmentParentId;
                //    singleSelectedQuestions_single.AssessmentType = selectedQuestions.AssessmentType;
                //    singleSelectedQuestions_single.QuestionInfos.Add(selectedQuestions.QuestionInfos[questionIndex]);

                    

                //    isSuccessfullySaved = SaveQuestionTrackingInfo(singleSelectedQuestions_single, learnerStatistic_Id, true);
                //}

            }
            else
            {
                isSuccessfullySaved = SaveQuestionTrackingInfo(selectedQuestions, learnerStatistic_Id, false);
            }
            //----------------------------------------------------------------------------

            return isSuccessfullySaved;
        }


        /// <summary>
        /// This method create ShowTimerExpiredMessage command when assessment timer get expired at client
        /// </summary>
        /// <returns>ShowTimerExpiredMessage command</returns>
        public object AssessmentTimerExpired()
        {
            ShowTimerExpiredMessage showTimerExpiredMessage = new ShowTimerExpiredMessage();
            showTimerExpiredMessage.CommandName = CommandNames.ShowTimerExpiredMessage;
            showTimerExpiredMessage.TimerExpiredMessage = new TimerExpiredMessage();
            TimerExpiredMessage timerExpiredMessage = new TimerExpiredMessage();
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                timerExpiredMessage.TimerExpiredMessageHeading = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingTimeExpired, brandCode, variant);
                timerExpiredMessage.TimerExpiredMessageText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentTimeExpiredAssessment, brandCode, variant);
                timerExpiredMessage.TimerExpiredMessageImageUrl = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageTimeExpired, brandCode, variant);
                timerExpiredMessage.TimerExpiredMessageButtonText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentTimeExpiredContinueButton, brandCode, variant);
            }

            showTimerExpiredMessage.TimerExpiredMessage = timerExpiredMessage;

            return showTimerExpiredMessage;
        }

        /// <summary>
        /// This method check whether there is any skipped question
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool CheckIsThereAnySkippedQuestion()
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            for (int i = 0; i < selectedQuestions.QuestionInfos.Count; i++)
                if (selectedQuestions.QuestionInfos[i].IsSkipped)
                {
                    return true;

                }
            return false;
        }


        /// <summary>
        /// This method get index of first skipped question
        /// </summary>
        /// <returns>Intger value</returns>
        private int GetIndexOfFirstSkippedQuestion()
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            int firstOccuranceOfSkippedQuestionIndex = 0;
            for (int i = 0; i < selectedQuestions.QuestionInfos.Count; i++)
                if (selectedQuestions.QuestionInfos[i].IsSkipped)
                {
                    firstOccuranceOfSkippedQuestionIndex = i;
                    break;
                }

            return firstOccuranceOfSkippedQuestionIndex;

        }

        /// <summary>
        /// This method searches for question index on SelectedQuestion list
        /// </summary>
        /// <returns>Integer value</returns>
        private int GetIndexOfQuestion(int assessmentItemID)
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            for (int i = 0; i < selectedQuestions.QuestionInfos.Count; i++)
                if (selectedQuestions.QuestionInfos[i].QuestionID == assessmentItemID)
                {
                    return i;

                }

            return -1;

        }

        /// <summary>
        /// This method calculate index for next skipped question
        /// </summary>
        /// <returns>Integer value represent the index of skipped question</returns>
        private int GetIndexOfNextSkippedQuestion(int previousIndex)
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            int i = 0;
            bool found = false;
            for (i = 0; i < selectedQuestions.QuestionInfos.Count; i++)
                if (selectedQuestions.QuestionInfos[i].IsSkipped && i > previousIndex)
                {
                    found = true;
                    break;
                }

            if (found)
                return i;
            else
                //force player to end the assessment so that system can continue
                return selectedQuestions.QuestionInfos.Count;

        }

        /// <summary>
        /// This method create ShowSkippedQuestion command
        /// </summary>
        /// <returns>ShowSkippedQuestion command</returns>
        private object CreateSkippedQuestionCommandObject()
        {
            HttpContext.Current.Session["AssessmentAllQuestionsDisplayed"] = true;

            ShowSkippedQuestion showSkippedQuestion = new ShowSkippedQuestion();
            showSkippedQuestion.CommandName = CommandNames.ShowSkippedQuestion;
            showSkippedQuestion.SkippedQuestions = new List<SkippedQuestion>();
            SkippedQuestion skippedQuestion = new SkippedQuestion();

            string logOuttext = string.Empty;
            string AssessmentType = "";
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                ICPCourseService.SequenceItem sequenceItem = null;
                int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);                
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                AssessmentType = sequenceItem.SequenceItemType;
                
                if (AssessmentType == _360Training.BusinessEntities.SequenceItemType.Exam)
                {
                    AssessmentType = sequenceItem.ExamType;
                }
            }



            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();

            if (AssessmentType.Equals("PracticeExam"))
            {
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    if (cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutTextPractiseExam, brandCode, variant) != null)
                    {
                        logOuttext = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutPauseResumeOffPractiseExam, brandCode, variant);
                    }

                }
            }
            else
            {
                using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    if (cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutText, brandCode, variant) != null)
                    {
                        logOuttext = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutText, brandCode, variant);
                    }
                }
            }
            showSkippedQuestion.LogOutText = logOuttext;

            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            int i = 0;

            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {

                foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
                {
                    skippedQuestion = new SkippedQuestion();
                    if (questionInfo.IsSkipped)
                    {
                        skippedQuestion.AssessmentItemID = questionInfo.QuestionID;
                        skippedQuestion.AssessmentItemStem = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentQuestion, brandCode, variant);
                        skippedQuestion.AssessmentItemType = "";
                        skippedQuestion.AssessmentToogleFlag = questionInfo.ToogleFlag; //Abdus Samad LCMS-12105
                        skippedQuestion.QuestionNo = i + 1;

                        if (AssessmentType.Equals("PostAssessment")) 
                            System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"] = "True";

                        showSkippedQuestion.SkippedQuestions.Add(skippedQuestion);
                    }
                    i++;
                }
            }

            return showSkippedQuestion;

        }

        /// <summary>
        /// This method create ShowAnswerReview command
        /// </summary>
        /// <returns>ShowAnswerReview command</returns>
        private object CreateAnswerReviewCommandObject()
        {
            //LCMS-11066 Answer review screen reached 
            HttpContext.Current.Session["AssessmentAllQuestionsAttempted"] = true;
            
            ShowAnswerReview showAnswerReview = new ShowAnswerReview();
            showAnswerReview.CommandName = CommandNames.ShowAnswerReview;
            showAnswerReview.AnswerReviews = new List<AnswerReview>();
            AnswerReview answerReview = new AnswerReview();
            answerReview.Answers = new List<string>();

            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
            int i = 0;
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();

            string logOuttext = string.Empty;
            string AssessmentType = "";
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                ICPCourseService.SequenceItem sequenceItem = null;
                int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                AssessmentType = sequenceItem.SequenceItemType;

                if (AssessmentType == _360Training.BusinessEntities.SequenceItemType.Exam)
                {
                    AssessmentType = sequenceItem.ExamType;
                }
            }

            if (AssessmentType.Equals("PracticeExam"))
            {
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    if (cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutTextPractiseExam, brandCode, variant) != null)
                    {
                        logOuttext = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutPauseResumeOffPractiseExam, brandCode, variant);
                    }

                }
            }
            else
            {
                using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    if (cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutText, brandCode, variant) != null)
                    {
                        logOuttext = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentLogOutText, brandCode, variant);
                    }
                }
            }
            showAnswerReview.LogOutText = logOuttext;
            
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {

                foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
                {
                    answerReview = new AnswerReview();
                    answerReview.Answers = new List<string>();

                    answerReview.AssessmentItemID = questionInfo.QuestionID;
                    answerReview.AssessmentItemStem = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentQuestion, brandCode, variant);
                    answerReview.AssessmentItemType = questionInfo.QuestionType;
                    answerReview.AssessmentToogleFlag = questionInfo.ToogleFlag; //Abdus Samad LCMS-12105

                    if (AssessmentType.Equals("PostAssessment"))
                        System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"] = "True";
                    

                    answerReview.QuestionNo = i + 1;
                    if (questionInfo.AnswerTexts.Count > 0)
                    {
                        //string encodedAnswer = string.Empty;
                        foreach (string anwserText in questionInfo.AnswerTexts)
                        {
                            //encodedAnswer = System.Web.HttpContext.Current.Server.HtmlEncode(anwserText);
                            answerReview.Answers.Add(anwserText);
                        }
                    }
                    if (questionInfo.AnswerIDs.Count > 0)
                    {
                        ICPAssessmentService.AssessmentItem[] assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
                        int assessmentItemIndex = GetAssessmentItemIndexByID(questionInfo.QuestionID, assessmentItems);
                        int assessmentItemAnswerIndex = 0;
                        string answerAlphabetCode = string.Empty;
                        foreach (int answerID in questionInfo.AnswerIDs)
                        {
                            assessmentItemAnswerIndex = GetAssessmentItemAnswerIndex(answerID, assessmentItems[assessmentItemIndex]);
                            if (questionInfo.QuestionType != QuestionType.TrueFalse)
                                answerAlphabetCode = Convert.ToChar(assessmentItemAnswerIndex + 65).ToString();
                            else
                            {
                                // LCMS-9213
                                // ------------------------------------------------------------------------------------
                                //answerAlphabetCode = GetAssessmentItemAnswer(answerID, assessmentItems[assessmentItemIndex]).Value;
                                ICPAssessmentService.AssessmentItemAnswer aai = GetAssessmentItemAnswer(answerID, assessmentItems[assessmentItemIndex]);
                                if (aai != null)
                                {
                                    answerAlphabetCode = aai.Value;
                                }   
                                // ------------------------------------------------------------------------------------
                            }
                            answerReview.Answers.Add(answerAlphabetCode);
                        }
                    }

                    showAnswerReview.AnswerReviews.Add(answerReview);
                    i++;
                }
            }

            return showAnswerReview;

        }

        /// <summary>
        /// This method searches for AssessmentItemIndex on AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemID">AssessmentItemID integer value</param>
        /// <param name="assessmentItems">AssessmentItems, ICPAssessmentService.AssessmentItem[]</param>
        /// <returns>Integer value</returns>
        private int GetAssessmentItemIndexByID(int assessmentItemID, ICPAssessmentService.AssessmentItem[] assessmentItems)
        {
            for (int index = 0; index <= assessmentItems.Length - 1; index++)
            {
                if (assessmentItems[index].AssessmentItemID == assessmentItemID)
                    return index;

            }
            return -1;
        }

        /// <summary>
        /// This method searches for AssessmentItemAnswerIndex on AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemAnswerID">AssessmentItemAnswerID integer value</param>
        /// <param name="assessmentItem">AssessmentItem, ICPAssessmentService.AssessmentItem</param>
        /// <returns>Integer value</returns>
        private int GetAssessmentItemAnswerIndex(int assessmentItemAnswerID, ICPAssessmentService.AssessmentItem assessmentItem)
        {
            for (int index = 0; index <= assessmentItem.AssessmentAnswers.Length - 1; index++)
            {
                if (assessmentItem.AssessmentAnswers[index].AssessmentItemAnswerID == assessmentItemAnswerID)
                    return index;
            }

            return -1;
        }

        private ICPAssessmentService.AssessmentItemAnswer GetAssessmentItemAnswer(int assessmentItemAnswerID, ICPAssessmentService.AssessmentItem assessmentItem)
        {
            for (int index = 0; index <= assessmentItem.AssessmentAnswers.Length - 1; index++)
            {
                if (assessmentItem.AssessmentAnswers[index].AssessmentItemAnswerID == assessmentItemAnswerID)
                    return assessmentItem.AssessmentAnswers[index];
            }

            return null;
        }



        /// <summary>
        /// This method calculate assessment result.
        /// </summary>
        /// <param name="masteryScore">MasteryScore integer vale, policy variable</param>
        /// <param name="numberOfCorrect">NumberOfCorrect integer value, represent the number of question which was correctly answered</param>
        /// <param name="numberOfInCorrect">NumberOfInCorrect integer value, represent the number of question which was incorrectly answered</param>
        /// <returns>Boolean value</returns>
        private bool CalculateAssessmentResult(int masteryScore, ref int numberOfCorrect, ref int numberOfInCorrect)
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                if (questionInfo.IsCorrectlyAnswered)
                    numberOfCorrect++;
            }
            numberOfInCorrect = selectedQuestions.QuestionInfos.Count - numberOfCorrect;
            return true;

        }




        /// <summary>
        /// This method calculate assessment result when weighted score policy is on.
        /// </summary>
        /// <param name="masteryScore">MasteryScore integer vale, policy variable</param>
        /// <param name="numberOfCorrect">NumberOfCorrect integer value, represent the number of question which was correctly answered</param>
        /// <param name="numberOfInCorrect">NumberOfInCorrect integer value, represent the number of question which was incorrectly answered</param>
        /// <returns>DataTable</returns>
        private DataTable CalculateAssessmentResultAsWeightedScore(int masteryScore, ref int numberOfCorrect, ref int numberOfInCorrect)
        {
            SelectedQuestion selectedQuestions = null;
            selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];

            // Logic for weighted score LCMS-7085
            //----------------------------------------------
            Hashtable learningObjectives = new Hashtable();
            DataTable dt = new DataTable();

            dt.Columns.Add("LOID", System.Type.GetType("System.Int32"));
            
            /**LCMS-7422 added following fields for exam prep - start*/
            dt.Columns.Add("AssessmentBinderName", System.Type.GetType("System.String"));            
            /**LCMS-7422 added following fields for exam prep - end*/

            dt.Columns.Add("LOWeightage", System.Type.GetType("System.Double"));
            dt.Columns.Add("NumberOfCorrectAnswers", System.Type.GetType("System.Double"));
            dt.Columns.Add("TotalNumberOfQuestions", System.Type.GetType("System.Double"));
            dt.Columns.Add("WeightedScore", System.Type.GetType("System.Double"));
            dt.TableName = "WeightedScoreTable";

            DataRow dr = null;
            //----------------------------------------------

            foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                // Logic for weighted score LCMS-7085
                //----------------------------------------------
                if (!learningObjectives.ContainsKey(questionInfo.AssessmentBinderID.ToString()))
                {

                    learningObjectives.Add(questionInfo.AssessmentBinderID.ToString(), new Object[] { 0, 0, questionInfo.ScoreWeight, questionInfo.AssessmentBinderName});
                }

                Object[] values = (Object[])learningObjectives[questionInfo.AssessmentBinderID.ToString()];
                int correctlyAnsweredInLO = (int)values[0];
                int totalInLO = (int)values[1];
                totalInLO++;                
                //----------------------------------------------

                if (questionInfo.IsCorrectlyAnswered)
                {
                    numberOfCorrect++;
                    correctlyAnsweredInLO++;
                }
                else
                {
                    numberOfInCorrect++;
                }

                // Logic for weighted score LCMS-7085
                //----------------------------------------------

                //correctlyAnsweredInLO.ToString() + "/" + totalInLO.ToString() + "/" + questionInfo.ScoreWeight;                
                values[0] = correctlyAnsweredInLO;
                values[1] = totalInLO;                
                learningObjectives[questionInfo.AssessmentBinderID.ToString()] = values;//correctlyAnsweredInLO.ToString() + "/" + totalInLO.ToString() + "/" + questionInfo.ScoreWeight;                
                //----------------------------------------------                      
            }


            // WeightedScore LCMS-7085
            //----------------------------------------------
            foreach (System.Collections.DictionaryEntry dictEntry in learningObjectives)
            {
                string val = dictEntry.Key.ToString();
                Object[] values = (Object[])dictEntry.Value;
                dr = dt.NewRow();
                dr["LOID"] = Convert.ToInt32(dictEntry.Key.ToString());
                dr["LOWeightage"] = (double)values[2]; // temporarily hard-coded
                
                dr["AssessmentBinderName"] = (String)values[3];

                dr["NumberOfCorrectAnswers"] = (int)values[0];
                dr["TotalNumberOfQuestions"] = (int)values[1];
                dt.Rows.Add(dr);                
            }
            dt.AcceptChanges();

            GetWeightedScore(dt);
            //----------------------------------------------


            return dt;
            //numberOfInCorrect = selectedQuestions.QuestionInfos.Count - numberOfCorrect;
            //if ((numberOfCorrect / Convert.ToDecimal(selectedQuestions.QuestionInfos.Count)) * 100 >= masteryScore)
            //    return true;
            //else
            //    return false;

        }


        /// <summary>
        /// This method calculates weighted score by taking datatable as parameter and updates 'Weighted Score' column of same datatable
        /// </summary>
        /// <param name="dt">DataTable</param>
        public void GetWeightedScore(DataTable dt)
        {
            double totalWeightage = (double)dt.Compute("SUM(LOWeightage)", "");
            bool totalWeightageTobeAdjusted = false;
            double totalWeightageTobeAdjustedSum = 0.0;

            if (totalWeightage != 1)
                totalWeightageTobeAdjusted = true;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double currentLOWeightage = 0.0;
                if (Convert.ToDouble(dt.Rows[i]["LOWeightage"]) == 0)
                    continue;

                if (totalWeightageTobeAdjusted)
                {
                    if (i == (dt.Rows.Count - 1))
                    {
                        //currentLOWeightage = (Convert.ToDouble(dt.Rows[i]["LOWeightage"].ToString()) / totalWeightage) * 100;
                        //totalWeightageTobeAdjustedSum = totalWeightageTobeAdjustedSum + currentLOWeightage;
                        dt.Rows[i]["LOWeightage"] = 1 - totalWeightageTobeAdjustedSum;
                        currentLOWeightage = 1 - totalWeightageTobeAdjustedSum;
                        totalWeightageTobeAdjustedSum = totalWeightageTobeAdjustedSum + currentLOWeightage;
                    }
                    else
                    {
                        currentLOWeightage = (Convert.ToDouble(dt.Rows[i]["LOWeightage"].ToString()) / totalWeightage);
                        currentLOWeightage = Math.Round(currentLOWeightage,2);
                        totalWeightageTobeAdjustedSum = totalWeightageTobeAdjustedSum + currentLOWeightage;
                        dt.Rows[i]["LOWeightage"] = currentLOWeightage;
                    }
                }
                else
                {
                    currentLOWeightage = Convert.ToDouble(dt.Rows[i]["LOWeightage"].ToString());
                }
                double userScorePercentageInCurrentLO = (Convert.ToDouble(dt.Rows[i]["NumberOfCorrectAnswers"].ToString()) / Convert.ToDouble(dt.Rows[i]["TotalNumberOfQuestions"].ToString()));
                //string wScore = String.Format("{0:0.00}", ((currentLOWeightage) * userScorePercentageInCurrentLO * 100));
                dt.Rows[i]["WeightedScore"] = ((currentLOWeightage) * userScorePercentageInCurrentLO * 100);//Convert.ToDouble(wScore);

                //System.Diagnostics.Trace.WriteLine("GetWeightedScore : currentLOWeightage=" + currentLOWeightage + " userScorePercentageInCurrentLO="+userScorePercentageInCurrentLO + " WeightedScore=" + dt.Rows[i]["WeightedScore"] + " LOID=" + dt.Rows[i]["LOID"]);
                //System.Diagnostics.Trace.Flush();

            }
            dt.AcceptChanges();
        }


        /// <summary>
        /// This method create ShowQuestionResult command
        /// </summary>
        /// <param name="correctlyAnswered">CorrectlyAnswered boolean value</param>
        /// <param name="feedback">Feedback string value</param>
        /// <returns>ShowQuestionResult command</returns>
        public object CreateQuestionResultCommandObject(bool correctlyAnswered, string feedback, bool endAssessment)
        {
            int assessmentSequenceNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]);
            ICPAssessmentService.AssessmentItem[] assessmentItems = null;
            assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
            if (assessmentItems !=null && assessmentItems.Length == assessmentSequenceNo)
            {
                HttpContext.Current.Session["AssessmentAllQuestionsAttempted"] = true;
            }

            ShowQuestionResult showQuestionResult = new ShowQuestionResult();
            QuestionResult questionResult = new QuestionResult();
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {

                questionResult.IsCorrectlyAnswered = correctlyAnswered;
                questionResult.QuestionFeedBack = feedback;
                questionResult.HeadingQuestionFeedback = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingQuestionFeedback, brandCode, variant);
                questionResult.ImageQuestionFeedbackUrl = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageQuestionFeedback, brandCode, variant);
                questionResult.EndAssessment = endAssessment;
                if (correctlyAnswered)
                {
                    questionResult.ImageCorrectIncorectUrl = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageCorrectAnswer, brandCode, variant);
                    questionResult.ContentCorrectIncorrect = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentCorrectAnswer, brandCode, variant);
                }
                else
                {
                    questionResult.ImageCorrectIncorectUrl = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageInCorrectAnswer, brandCode, variant);
                    questionResult.ContentCorrectIncorrect = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentIncorrectAnswer, brandCode, variant);
                }
            }
            showQuestionResult.CommandName = CommandNames.ShowQuestionResult;
            showQuestionResult.QuestionResult = questionResult;

            return showQuestionResult;
        }



        /// <summary>
        /// This method create ShowExamScoreSummary command
        /// </summary>
        /// <param name="showPercentageScore">ShowPercentageScore boolean variable, policy variable</param>
        /// <param name="isAssessmentPass">IsAssessmentPass boolean variable, represent whether the assessment was passed or not</param>
        /// <param name="numberOfCorrectQuestion">NumberOfCorrectQuestion integer value, represent the number of question which was correctly answered</param>
        /// <param name="totalNumberOfQuestion">TotalNumberOfQuestion integer value, represent the number of total question</param>
        /// <returns>ShowAssessmentScoreSummary command</returns>


        private ShowTopicScoreSummary CreateTopicScoreSummaryCommandObject(int topicId, String topicName, double topicScoreWeight, double achivedScore, int totalQuestionsAsked, int correctAnswersCount)
        {
            ShowTopicScoreSummary showTopicScoreSummary = new ShowTopicScoreSummary();
            showTopicScoreSummary.TopicScoreSummary = new TopicScoreSummary();

            if (topicScoreWeight < 0)
            {
                showTopicScoreSummary.TopicScoreSummary.AchievedScore = Math.Round(Convert.ToDouble(((correctAnswersCount * 100) / Convert.ToDecimal(totalQuestionsAsked))), 2);
            }
            else
            {
                if (topicScoreWeight > 0)
                {
                    showTopicScoreSummary.TopicScoreSummary.AchievedScore = Math.Round((achivedScore * 100) / (topicScoreWeight * 100), 2);
                }
                else
                {
                    showTopicScoreSummary.TopicScoreSummary.AchievedScore = 0.0;
                }
            }

            
            showTopicScoreSummary.TopicScoreSummary.CorrectAnswersCount = correctAnswersCount;
            showTopicScoreSummary.TopicScoreSummary.InCorrectAnswersCount = totalQuestionsAsked - correctAnswersCount;
            showTopicScoreSummary.TopicScoreSummary.NumberOfQuestionsAsked = totalQuestionsAsked;
            showTopicScoreSummary.TopicScoreSummary.TopicName = topicName;
            showTopicScoreSummary.TopicScoreSummary.TopicID = topicId;
            //showTopicScoreSummary.TopicScoreSummary.TopicNumber = topicNumber;
            showTopicScoreSummary.TopicScoreSummary.WeightedScore = topicScoreWeight;

            return showTopicScoreSummary;
        }





        /// <summary>
        /// This method create ShowAssessmentScoreSummary command
        /// </summary>
        /// <param name="showPercentageScore">ShowPercentageScore boolean variable, policy variable</param>
        /// <param name="isAssessmentPass">IsAssessmentPass boolean variable, represent whether the assessment was passed or not</param>
        /// <param name="numberOfCorrectQuestion">NumberOfCorrectQuestion integer value, represent the number of question which was correctly answered</param>
        /// <param name="totalNumberOfQuestion">TotalNumberOfQuestion integer value, represent the number of total question</param>
        /// <returns>ShowAssessmentScoreSummary command</returns>
        private object CreateAssessmentScoreSummaryCommandObject(bool showPercentageScore, bool isAssessmentPass, int numberOfCorrectQuestion, int totalNumberOfQuestion, int currAttemptNo, double weightedScore, DataTable assessmentResultWeightedScoreDataTable)
        {
            // LCMS-9213
            // --------------------------------------------------------------------
            HttpContext.Current.Session.Remove("askedAssessmentItemsAttributes");
            HttpContext.Current.Session.Remove("SelectedQuestionSequenceNo");
            HttpContext.Current.Session.Remove("Current_Assessment_LearnerStatistics_ID");
            // --------------------------------------------------------------------
            //LCMS-10266
            HttpContext.Current.Session.Remove("RandomAlternateWithPauseResume");
            //End LCMS-10266
            //Fix for LCMS-11066
            HttpContext.Current.Session.Remove("AssessmentAllQuestionsAttempted");
            ///
            HttpContext.Current.Session.Remove("AssessmentAllQuestionsDisplayed");
            int examID = 0;

            if (System.Web.HttpContext.Current.Session["ExamID"] != null)
            {
                examID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ExamID"]);
            }
            

            ShowAssessmentScoreSummary showAssessmentScoreSummary = new ShowAssessmentScoreSummary();
            AssessmentScoreSummary assessmentScoreSummary = new AssessmentScoreSummary();
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            string remainingAttemptText = "";
            int MaxAttemptNo;

            bool isHideScore = false;
            bool showExamGraph = true;
            String noScoreText = "";

            //Abdus Samad
            //LCMS 8147 Start
            string AssessmentType = System.Web.HttpContext.Current.Session["AssessmentType"].ToString();
          
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {

                if (System.Web.HttpContext.Current.Session["AssessmentType"] != null)
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);                    
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                 
                    switch (AssessmentType)
                    {
                        case CourseManager.LearnerStatisticsType.PreAssessment:
                            {

                                assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PreAssessmentType, brandCode, variant);
                                isHideScore = courseConfiguration.PreAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                noScoreText = courseConfiguration.PreAssessmentConfiguration.NoResultText;

                                if (noScoreText.Trim() == string.Empty && isHideScore)
                                {
                                    using (CourseManager.CourseManager courseManager = new CourseManager.CourseManager())
                                    {
                                        object courseManagerObject = courseManager.ContinueAfterAssessmentScore(courseID);
                                        return courseManagerObject;
                                    }                                  
                                }
                                break;
                            }
                        case CourseManager.LearnerStatisticsType.PostAssessment:
                            {
                                assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PostAssessmentType, brandCode, variant);
                                isHideScore = courseConfiguration.PostAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                noScoreText = courseConfiguration.PostAssessmentConfiguration.NoResultText;

                                if (noScoreText.Trim() == string.Empty && isHideScore)
                                {
                                    using (CourseManager.CourseManager courseManager = new CourseManager.CourseManager())
                                    {
                                        object courseManagerObject = courseManager.ContinueAfterAssessmentScore(courseID);
                                        return courseManagerObject;
                                    }
                                }
                                break;
                            }
                        case CourseManager.LearnerStatisticsType.Quiz:
                            {
                                assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.QuizType, brandCode, variant);
                                isHideScore = courseConfiguration.QuizConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                noScoreText = courseConfiguration.QuizConfiguration.NoResultText;

                                if (noScoreText.Trim() == string.Empty && isHideScore)
                                {
                                    using (CourseManager.CourseManager courseManager = new CourseManager.CourseManager())
                                    {
                                        object courseManagerObject = courseManager.ContinueAfterAssessmentScore(courseID);
                                        return courseManagerObject;
                                    }
                                }
                                break;
                            }
                        case CourseManager.LearnerStatisticsType.PracticeExam:
                            {
                                assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PracticeExamType, brandCode, variant);

                                ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                                isHideScore = assessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                noScoreText = assessmentConfiguration.NoResultText;

                                if (noScoreText.Trim() == string.Empty && isHideScore)
                                {
                                    using (CourseManager.CourseManager courseManager = new CourseManager.CourseManager())
                                    {
                                        object courseManagerObject = courseManager.ContinueAfterAssessmentScore(courseID);
                                        return courseManagerObject;
                                    }
                                }
                                break;
                            }

                    }
                }

            }

            //Abdus Samad
            //LCMS 8147 Stop            
                       
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                                
                if (AssessmentType.Equals(CourseManager.LearnerStatisticsType.PostAssessment) && courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)
                {
                    showExamGraph = false;
                }                

                if (examID > 0 && showExamGraph)
                {
                    assessmentScoreSummary.IsShowGraph = true;
                    assessmentScoreSummary.IsExam = true;
                    assessmentScoreSummary.HeadingTopicScoreChart = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingExamTopicScoreSummary, brandCode, variant);
                    assessmentScoreSummary.IndividualScoreHeaderText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeaderTextIndividualQuestionScore, brandCode, variant);
                }
                else
                {
                    if (showExamGraph == false)
                    {
                        assessmentScoreSummary.IsShowGraph = true;
                        assessmentScoreSummary.IsExam = true;
                        string HeadingExamTopicScoreSummary = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingExamTopicScoreSummary, brandCode, variant);
                        HeadingExamTopicScoreSummary = HeadingExamTopicScoreSummary.Replace("topic", "assessment item bank");
                        assessmentScoreSummary.HeadingTopicScoreChart = HeadingExamTopicScoreSummary;
                        assessmentScoreSummary.IndividualScoreHeaderText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeaderTextIndividualQuestionScore, brandCode, variant);                        
                    }
                }

                if (AssessmentType.Equals(CourseManager.LearnerStatisticsType.PostAssessment) && courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS && courseConfiguration.PostAssessmentConfiguration.ScoreType == ScoreType.PassFail)
                {
                    assessmentScoreSummary.IsShowGraph = false;                    
                }
                else
                {
                    assessmentScoreSummary.IsShowGraph = true;                    
                }

                
                //int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                if (!isAssessmentPass) // Check only in case of failure
                {
                    remainingAttemptText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentRemainingAttempts, brandCode, variant);

                    //remainingAttemptText = remainingAttemptText.Replace("{0}", NumberToWordConvertor.NumberToText(currAttemptNo, true).ToLower());
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);

                    if (System.Web.HttpContext.Current.Session["AssessmentType"] != null)
                    {
                        //string AssessmentType = System.Web.HttpContext.Current.Session["AssessmentType"].ToString();
                        switch (AssessmentType)
                        {
                            case CourseManager.LearnerStatisticsType.PreAssessment:
                                {
                                    assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PreAssessmentType, brandCode, variant); ;
                                    MaxAttemptNo = courseConfiguration.PreAssessmentConfiguration.MaximumNOAttempt;
                                    /*if (MaxAttemptNo < currAttemptNo && courseConfiguration.PreAssessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = currAttemptNo % MaxAttemptNo;
                                    }
//                                  if (MaxAttemptNo == currAttemptNo)
                                    if (MaxAttemptNo == currAttemptNo && courseConfiguration.PreAssessmentConfiguration.MaxAttemptHandlerEnabled) 
                                    {
                                        currAttemptNo = 0;
                                    }
                                    */
                                    //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false).ToLower());
                                    if (!courseConfiguration.PreAssessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        //remainingAttemptText = remainingAttemptText.Replace("This is your $ATTEMPTNUMBER attempt. You are allowed a maximum of $ATTEMPTEDNUMBER attempts after which $ACTIONMESSAGE", "");
                                        //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", "unlimited");
                                        remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentRemainingAttempts, brandCode, variant), "");
                                        
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());
                                        remainingAttemptText = AssessmentMaxAttemptText(remainingAttemptText, MaxAttemptNo);
                                    }

                                    remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTNUMBER", NumberToWordConvertor.NumberToText(currAttemptNo, true, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());

                                    if (courseConfiguration.PreAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Go To Next Lesson"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PreAssessmentGoToNextLesson, brandCode, variant));
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PreAssessmentLockCourse, brandCode, variant));
                                    }

                                    isHideScore = courseConfiguration.PreAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                    noScoreText = courseConfiguration.PreAssessmentConfiguration.NoResultText;


                                    // LCMS-8147
                                    //------------------------------------
                                   // if (noScoreText.Trim() == "" && isHideScore)
                                   // {
                                   //     return new CourseManager.CourseManager().ContinueAfterAssessmentScore(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]));
                                   // }
                                    //------------------------------------

                                    break;
                                }
                            case CourseManager.LearnerStatisticsType.PostAssessment:
                                {
                                    assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PostAssessmentType, brandCode, variant); ;
                                    MaxAttemptNo = courseConfiguration.PostAssessmentConfiguration.MaximumNOAttempt;
                                    /*if (MaxAttemptNo < currAttemptNo && courseConfiguration.PostAssessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = currAttemptNo % MaxAttemptNo;
                                    }
                                    if (MaxAttemptNo == currAttemptNo && courseConfiguration.PostAssessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = 0;
                                    }
                                    */
                                    //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false).ToLower());

                                    if (!courseConfiguration.PostAssessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        //remainingAttemptText = remainingAttemptText.Replace("This is your $ATTEMPTNUMBER attempt. You are allowed a maximum of $ATTEMPTEDNUMBER attempts after which $ACTIONMESSAGE", "");
                                        remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentRemainingAttempts, brandCode, variant), "");
                                        //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", "unlimited");
                                        //remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentAttemptTextToRemove, brandCode, variant), ".");
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());
                                        remainingAttemptText = AssessmentMaxAttemptText(remainingAttemptText, MaxAttemptNo);
                                    }

                                    remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTNUMBER", NumberToWordConvertor.NumberToText(currAttemptNo, true, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());

                                    if (courseConfiguration.PostAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Lock Course"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PostAssessmentLockCourse, brandCode, variant));
                                    }
                                    else if (courseConfiguration.PostAssessmentConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Continue Course"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.POSTAssessmentContinueCourse, brandCode, variant)) + ".";
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PostAssessmentRetakeContent, brandCode, variant));
                                    }
                                    isHideScore = courseConfiguration.PostAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                    noScoreText = courseConfiguration.PostAssessmentConfiguration.NoResultText;

                                    // LCMS-8147
                                    //------------------------------------
                                    //if (noScoreText.Trim() == "" && isHideScore)
                                    //{
                                    //    return new CourseManager.CourseManager().ContinueAfterAssessmentScore(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]));
                                    //}
                                    //------------------------------------

                                    break;
                                }
                            case CourseManager.LearnerStatisticsType.Quiz:
                                {
                                    assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.QuizType, brandCode, variant); ;
                                    MaxAttemptNo = courseConfiguration.QuizConfiguration.MaximumNOAttempt;
                                    /*if (MaxAttemptNo < currAttemptNo && courseConfiguration.QuizConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = currAttemptNo % MaxAttemptNo;
                                    }
                                    if (MaxAttemptNo == currAttemptNo && courseConfiguration.QuizConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = 0;
                                    }
                                   */
                                    
                                    //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false).ToLower());
                                    if (!courseConfiguration.QuizConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        //remainingAttemptText = remainingAttemptText.Replace("This is your $ATTEMPTNUMBER attempt. You are allowed a maximum of $ATTEMPTEDNUMBER attempts after which $ACTIONMESSAGE", "");
                                        remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentRemainingAttempts, brandCode, variant), "");
                                        //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", "unlimited");
                                        //remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentAttemptTextToRemove, brandCode, variant), ".");
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());
                                        remainingAttemptText = AssessmentMaxAttemptText(remainingAttemptText, MaxAttemptNo);
                                    }

                                    remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTNUMBER", NumberToWordConvertor.NumberToText(currAttemptNo, true, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());

                                    if (courseConfiguration.QuizConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Go To Next Lesson"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.LessonAssessmentGoToNextLesson, brandCode, variant));
                                    }
                                    else if (courseConfiguration.QuizConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Lock Course"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.LessonAssessmentLockCourse, brandCode, variant));
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.LessonAssessmentRetakeLesson, brandCode, variant));
                                    }
                                    isHideScore = courseConfiguration.QuizConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                    noScoreText = courseConfiguration.QuizConfiguration.NoResultText;

                                    // LCMS-8147
                                    //------------------------------------
                                    //if (noScoreText.Trim() == "" && isHideScore)
                                    //{
                                    //    return new CourseManager.CourseManager().ContinueAfterAssessmentScore(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]));
                                    //}
                                    //------------------------------------

                                    break;
                                }
                            case CourseManager.LearnerStatisticsType.PracticeExam:
                                {
                                    assessmentScoreSummary.AssessmentType = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PracticeExamType, brandCode, variant); ;
                                    ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                                    assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                                    MaxAttemptNo = assessmentConfiguration.MaximumNOAttempt;
                                    /*if (MaxAttemptNo < currAttemptNo && assessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = currAttemptNo % MaxAttemptNo;
                                    }
                                    if (MaxAttemptNo == currAttemptNo && assessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        currAttemptNo = 0;
                                    }
                                    */
                                    
                                    //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false).ToLower());
                                    if (!assessmentConfiguration.MaxAttemptHandlerEnabled)
                                    {
                                        //remainingAttemptText = remainingAttemptText.Replace("This is your $ATTEMPTNUMBER attempt. You are allowed a maximum of $ATTEMPTEDNUMBER attempts after which $ACTIONMESSAGE", "");
                                        remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentRemainingAttempts, brandCode, variant), "");
                                        //remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", "unlimited");
                                        //remainingAttemptText = remainingAttemptText.Replace(cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.AssessmentAttemptTextToRemove, brandCode, variant), ".");
                                    }
                                    else
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTEDNUMBER", NumberToWordConvertor.NumberToText(MaxAttemptNo, false, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());
                                        remainingAttemptText = AssessmentMaxAttemptText(remainingAttemptText, MaxAttemptNo);
                                    }

                                    remainingAttemptText = remainingAttemptText.Replace("$ATTEMPTNUMBER", NumberToWordConvertor.NumberToText(currAttemptNo, true, cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words0, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words1, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words2, brandCode, variant), cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.NumberToText_words3, brandCode, variant)).ToLower());


                                    if (assessmentConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Go To Next Lesson"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PracticeExamGoToNextLesson, brandCode, variant));
                                    }
                                    else if (assessmentConfiguration.ActionToTakeAfterFailingMaxAttempt.Equals("Lock Course"))
                                    {
                                        remainingAttemptText = remainingAttemptText.Replace("$ACTIONMESSAGE", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PracticeExamLockCourse, brandCode, variant));
                                    }
                                    isHideScore = assessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                                    noScoreText = assessmentConfiguration.NoResultText;

                                    // LCMS-8147
                                    //------------------------------------
                                    //if (noScoreText.Trim() == "" && isHideScore)
                                    //{
                                    //    return new CourseManager.CourseManager().ContinueAfterAssessmentScore(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]));
                                    //}
                                    //------------------------------------

                                    break;
                                }
                        }

                    }
                }

                // -----------------------------------------

                //if (System.Web.HttpContext.Current.Session["AssessmentType"] != null)
                //{
                //    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                //    //int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                //    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                //    //string AssessmentType = System.Web.HttpContext.Current.Session["AssessmentType"].ToString();
                //    switch (AssessmentType)
                //    {
                //        case CourseManager.LearnerStatisticsType.PreAssessment:
                //            {
                //                assessmentScoreSummary.AssessmentType = "Pre Assessment";
                //                isHideScore = courseConfiguration.PreAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                //                noScoreText = courseConfiguration.PreAssessmentConfiguration.NoResultText;
                //                break;
                //            }
                //        case CourseManager.LearnerStatisticsType.PostAssessment:
                //            {
                //                assessmentScoreSummary.AssessmentType = "Post Assessment";
                //                isHideScore = courseConfiguration.PostAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                //                noScoreText = courseConfiguration.PostAssessmentConfiguration.NoResultText;
                //                break;
                //            }
                //        case CourseManager.LearnerStatisticsType.Quiz:
                //            {
                //                assessmentScoreSummary.AssessmentType = "Quiz";
                //                isHideScore = courseConfiguration.QuizConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                //                noScoreText = courseConfiguration.QuizConfiguration.NoResultText;
                //                break;
                //            }
                //        case CourseManager.LearnerStatisticsType.PracticeExam:
                //            {
                //                assessmentScoreSummary.AssessmentType = "Practice Exam";

                //                ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                //                assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];

                //                isHideScore = assessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()) ? true : false;
                //                noScoreText = assessmentConfiguration.NoResultText;

                //                break;
                //            }

                //    }
                //}

                //------------------------------------------





                //if (examID > 0 && !(courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS))
                if (examID > 0 && showExamGraph)
                {
                    assessmentScoreSummary.HeadingAssesmentScoreSummaryText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingExamScoreSummary, brandCode, variant);
                    // LCMS-10316
                    //Abdus Samad Start
                     //assessmentScoreSummary.ContentAssesmentScoreSummaryText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentExamScoreSummary, brandCode, variant);
                    assessmentScoreSummary.ContentAssesmentScoreSummaryText = String.Empty;
                    //Abdus Samad End
                      
                }
                else
                {
                    if (showExamGraph)
                    {
                        assessmentScoreSummary.HeadingAssesmentScoreSummaryText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingAssesmentScoreSummary, brandCode, variant);
                        assessmentScoreSummary.ContentAssesmentScoreSummaryText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentAssessmentScoreSummary, brandCode, variant);
                    }
                    else                    
                    {
                        string ContentExamScoreSummary = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentExamScoreSummary, brandCode, variant);
                        ContentExamScoreSummary=ContentExamScoreSummary.Replace("topic","assessment item bank"); 
                        assessmentScoreSummary.HeadingAssesmentScoreSummaryText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingExamScoreSummary, brandCode, variant);
                        assessmentScoreSummary.ContentAssesmentScoreSummaryText = ContentExamScoreSummary;                        
                    }
                }

                
                assessmentScoreSummary.ImageAssessmentSummaryScoreUrl = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageAssessmentSummaryScore, brandCode, variant);

                if (isAssessmentPass && !showPercentageScore)
                {
                    assessmentScoreSummary.ImagePassFailScoreURL = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImagePassScore, brandCode, variant);
                    assessmentScoreSummary.PassFailScoreText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentPassScore, brandCode, variant);
                    assessmentScoreSummary.PassFailScoreText = assessmentScoreSummary.PassFailScoreText.Replace("$ICO_CORRECT", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageCorrectAnswer, brandCode, variant));
                }
                else if (!showPercentageScore)
                {
                    assessmentScoreSummary.ImagePassFailScoreURL = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageFailScore, brandCode, variant);
                    assessmentScoreSummary.PassFailScoreText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentFailScore, brandCode, variant);
                    assessmentScoreSummary.PassFailScoreText = assessmentScoreSummary.PassFailScoreText.Replace("$ICO_INCORRECT", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageInCorrectAnswer, brandCode, variant));
                    assessmentScoreSummary.PassFailScoreText = assessmentScoreSummary.PassFailScoreText + " " + remainingAttemptText;

                }
                if (showPercentageScore)
                {
                    string precentageScoreText = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentPercentageScore, brandCode, variant);

                    if (isAssessmentPass == true)
                    {
                        precentageScoreText = precentageScoreText.Replace("$PASSFAIL", examID > 0 ? cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PassedText, brandCode, variant) : cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.PassedExamText, brandCode, variant));
                        precentageScoreText = precentageScoreText.Replace("$ICO_QUESTIONFEEDBACK", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageCorrectAnswer, brandCode, variant));
                    }
                    else
                    {
                        precentageScoreText = precentageScoreText.Replace("$PASSFAIL", examID > 0 ? cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.FailedText, brandCode, variant) : cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.FailedExamText, brandCode, variant));
                        precentageScoreText = precentageScoreText.Replace("$ICO_QUESTIONFEEDBACK", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageInCorrectAnswer, brandCode, variant));
                    }
                    precentageScoreText = precentageScoreText.Replace("$NUMEROFCORRECT", numberOfCorrectQuestion.ToString());
                    precentageScoreText = precentageScoreText.Replace("$NUMBEROFASKED", totalNumberOfQuestion.ToString());

                    decimal precentage = Convert.ToDecimal(weightedScore);

                    if (weightedScore == -1)
                    {
                        precentage = ((numberOfCorrectQuestion / Convert.ToDecimal(totalNumberOfQuestion)) * 100);
                    }
                    precentageScoreText = precentageScoreText.Replace("$PERCENTAGE", Math.Round(precentage, 2).ToString());

                    assessmentScoreSummary.PercentageScoreText = precentageScoreText;
                    if (!isAssessmentPass)
                    {
                        assessmentScoreSummary.PercentageScoreText = assessmentScoreSummary.PercentageScoreText + " " + remainingAttemptText;
                    }
                    else
                    {
                        assessmentScoreSummary.PercentageScoreText = assessmentScoreSummary.PercentageScoreText.Replace("<br><br>", "");
                    }


                }
            };



            //Load Topics incase of Exam - Start ==============================================
            //if (examID > 0 && assessmentResultWeightedScoreDataTable != null && !(courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS))
            if (examID > 0 && assessmentResultWeightedScoreDataTable != null && showExamGraph)
            {
                
                //assessmentScoreSummary.ShowTopicScoreSummaries = CreateTopicScoreSummaryCommandObjects(assessmentResultWeightedScoreDataTable);
                assessmentScoreSummary.ShowTopicScoreSummaries = new List<ShowTopicScoreSummary>();
                DataRow[] rows = assessmentResultWeightedScoreDataTable.Select("", "LOID asc");
                for (int index = 0; index < rows.Length; index++)
                {
                    DataRow dr = rows[index];
                    assessmentScoreSummary.ShowTopicScoreSummaries.Add(CreateTopicScoreSummaryCommandObject(
                        Convert.ToInt32(dr["LOID"]),
                        dr["AssessmentBinderName"].ToString(),
                        weightedScore == -1 ? -1 : Convert.ToDouble(dr["LOWeightage"]),
                        dr["WeightedScore"] != System.DBNull.Value ? Math.Round(Convert.ToDouble(dr["WeightedScore"]), 2):-1,
                        Convert.ToInt32(dr["TotalNumberOfQuestions"]),
                        Convert.ToInt32(dr["NumberOfCorrectAnswers"])                        
                        ));
                    
                }

                    
    
                //assessmentScoreSummary.ShowTopicScoreSummaries.Add(CreateTopicScoreSummaryCommandObject("OSHA Construction", 90));
                //assessmentScoreSummary.ShowTopicScoreSummaries.Add(CreateTopicScoreSummaryCommandObject("OSHA General", 80));
                //assessmentScoreSummary.ShowTopicScoreSummaries.Add(CreateTopicScoreSummaryCommandObject("OSHA Industry", 85));
            }

            if (assessmentResultWeightedScoreDataTable != null && showExamGraph==false)
            {
                assessmentScoreSummary.ShowTopicScoreSummaries = new List<ShowTopicScoreSummary>();
                DataRow[] rows = assessmentResultWeightedScoreDataTable.Select("", "LOID asc");
                for (int index = 0; index < rows.Length; index++)
                {
                    DataRow dr = rows[index];
                    assessmentScoreSummary.ShowTopicScoreSummaries.Add(CreateTopicScoreSummaryCommandObject(
                        Convert.ToInt32(dr["LOID"]),
                        dr["AssessmentBinderName"].ToString(),
                        weightedScore == -1 ? -1 : Convert.ToDouble(dr["LOWeightage"]),
                        dr["WeightedScore"] != System.DBNull.Value ? Math.Round(Convert.ToDouble(dr["WeightedScore"]), 2) : -1,
                        Convert.ToInt32(dr["TotalNumberOfQuestions"]),
                        Convert.ToInt32(dr["NumberOfCorrectAnswers"])
                        ));

                }                
            }

            //Load Topics incase of Exam - End   ==============================================


            //=================================================================================

            object showIndivialQuestionResultCommand = new object();
            bool showIndivialQuestionResult = false;
            string itemType = "";
            int seqNo = 0;
            int course_id = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            //ICPCourseService.CourseConfiguration courseConfiguration = new ICP4.BusinessLogic.ICPCourseService.CourseConfiguration();
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                itemType = cacheManager.GetSequenceItemType(course_id, seqNo, source, courseConfigurationID);               

                if (itemType.Equals(SequenceItemTypeName.Exam))
                {
                    ICPCourseService.SequenceItem sequenceItem = null;
                    sequenceItem = cacheManager.GetRequestedItemFromQueue(course_id, seqNo, source, courseConfigurationID);
                    itemType = sequenceItem.ExamType; 

                }
                switch (itemType)
                {
                    case SequenceItemTypeName.PreAssessment:
                        showIndivialQuestionResult = courseConfiguration.PreAssessmentConfiguration.ShowQuestionAnswerSummary;
                        break;
                    case SequenceItemTypeName.PostAssessment:
                        showIndivialQuestionResult = courseConfiguration.PostAssessmentConfiguration.ShowQuestionAnswerSummary;

                        break;
                    case SequenceItemTypeName.Quiz:
                        showIndivialQuestionResult = courseConfiguration.QuizConfiguration.ShowQuestionAnswerSummary;
                        break;
                    case SequenceItemTypeName.PracticeExam:
                        ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                        assessmentConfiguration = (ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                        showIndivialQuestionResult = assessmentConfiguration.ShowQuestionAnswerSummary;
                        break;
                }

                assessmentScoreSummary.IndividualScoreHeading = "";

                if (showIndivialQuestionResult)
                {
                    using (AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
                    {
                        //if (examID > 0 && !(courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS))
                        if (examID > 0 && showExamGraph)
                        {                            
                            //foreach (ShowTopicScoreSummary showTopicSummary in assessmentScoreSummary.ShowTopicScoreSummaries)
                            //{
                            CreateIndividualQuestionScoreCommandObject(assessmentScoreSummary.ShowTopicScoreSummaries);
                                //showTopicSummary.TopicScoreSummary.ShowIndividualQuestionScore = (ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore)showIndivialQuestionResultCommand;
                            //}
                        }
                        else
                        {
                            if (showExamGraph)
                            {
                                showIndivialQuestionResultCommand = CreateIndividualQuestionScoreCommandObject();
                                assessmentScoreSummary.ShowIndividualQuestionScore = (ICP4.CommunicationLogic.CommunicationCommand.ShowIndividualQuestionScore.ShowIndividualQuestionScore)showIndivialQuestionResultCommand;
                            }
                            else
                            {
                                CreateIndividualQuestionScoreCommandObject(assessmentScoreSummary.ShowTopicScoreSummaries);
                            }
                        }
                    }
                    
                    
                    string indScoreHeading = cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingIndividualQuestionScore, brandCode, variant);
                    indScoreHeading = indScoreHeading.Replace("$CORRECTICON", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageCorrectAnswer, brandCode, variant));
                    indScoreHeading = indScoreHeading.Replace("$INCORRECTICON", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageInCorrectAnswer, brandCode, variant));
                    assessmentScoreSummary.IndividualScoreHeading = indScoreHeading;
                }
            }

            //=================================================================================


            /**For No Score Part*****/
            if (isHideScore)
            {

                assessmentScoreSummary.IndividualScoreHeading = "";
                 assessmentScoreSummary.ImagePassFailScoreURL = "";
                 assessmentScoreSummary.PassFailScoreText = "";
                 assessmentScoreSummary.PercentageScoreText = "";

                assessmentScoreSummary.HeadingAssesmentScoreSummaryText = "" ;
                assessmentScoreSummary.ContentAssesmentScoreSummaryText = noScoreText == null ? "" : noScoreText;
                assessmentScoreSummary.ContentAssesmentScoreSummaryText = assessmentScoreSummary.ContentAssesmentScoreSummaryText + "<br><br><br><br><br>";
                assessmentScoreSummary.HeadingTopicScoreChart = "";
                if (assessmentScoreSummary.ShowTopicScoreSummaries != null)
                {
                    foreach (ShowTopicScoreSummary summary in assessmentScoreSummary.ShowTopicScoreSummaries)
                    {

                        summary.TopicScoreSummary.AchievedScore = 0.0;
                        summary.TopicScoreSummary.ShowIndividualQuestionScore = null;
                        summary.TopicScoreSummary.WeightedScore = 0.0;
                        summary.TopicScoreSummary.TopicName = "";
                    }
                }


            }

            /**END For No Score Part*****/




            showAssessmentScoreSummary.CommandName = CommandNames.ShowAssessmentScoreSummary;
            showAssessmentScoreSummary.AssessmentScoreSummary = assessmentScoreSummary;
            System.Web.HttpContext.Current.Session["AssessmentStage"] = "AssessmentCompleted";
            System.Web.HttpContext.Current.Session.Remove("AssessmentRemediatonCount");

            return showAssessmentScoreSummary;
        }





        /// <summary>
        /// This method create ShowQuestion command based on the provided data.
        /// </summary>
        /// <param name="assessmentItem">AssessmentItem object, represent the question which is to be asked</param>
        /// <param name="questionNo">QuestionNo integer value, represent the question no</param>
        /// <param name="totalQuestion">TotalQuestion integer value, represent the total number of question</param>
        /// <param name="questionInfo">QuestionInfo object, this variable will be used when question remidiation will be on</param>
        /// <param name="allowQuestionLevelFeedBack">AllowQuestionLevelFeedBack boolean value, this variable will be used when question remidiation will be on</param>
        /// <param name="contentRemidiationAvailable">ContentRemidiationAvailable boolean value, this variable will be used when question remidiation will be on</param>
        /// <returns>ShowQuestion command</returns>
        public object CreateQuestionCommandObject(ICPAssessmentService.AssessmentItem assessmentItem, int questionNo, int totalQuestion, QuestionInfo questionInfo, bool allowQuestionLevelFeedBack, bool contentRemidiationAvailable, bool remediationMode, string assessmentType, string templateHTML, string audioURL, string templateType, string visualTopType, string feedBack)
        {
            ShowQuestion showQuestion = new ShowQuestion();
            AssessmentItem item = null;
            List<AssessmentItemAnswer> answers = new List<AssessmentItemAnswer>();

            showQuestion.CommandName = CommandNames.ShowQuestion;
            showQuestion.AssessmentType = assessmentType;
            item = GetAssessmentItemEntity(assessmentItem);
            answers = GetAssessmentItemAnswers(assessmentItem, assessmentItem.QuestionType);
            item.AssessmentAnswers = answers;
            item.QuestionNo = questionNo;
            item.TotalQuestion = totalQuestion;
            item.TemplateHTML = templateHTML;
            item.AudioURL = audioURL;
            item.TemplateType = templateType;          
            if (item.QuestionType != QuestionType.ImageTarget)
                item.VisualTopType = visualTopType;
            if (questionInfo != null)
            {
                StudentAnswer studentAnswer = new StudentAnswer();
                studentAnswer.AnswerIDs = questionInfo.AnswerIDs;
                studentAnswer.AnswerTexts = questionInfo.AnswerTexts;
                studentAnswer.IsCorrectlyAnswered = questionInfo.IsCorrectlyAnswered;
                studentAnswer.ToogleFlag = questionInfo.ToogleFlag;//Added By Abdus Samad LCMS-12105
                if (allowQuestionLevelFeedBack/* && !studentAnswer.IsCorrectlyAnswered*/ && feedBack != string.Empty)
                    studentAnswer.QuestionFeedBack = feedBack;
                item.RemidiationMode = remediationMode;
                item.StudentAnswer = studentAnswer;
                if (contentRemidiationAvailable)
                    item.ContentRemidiationAvailable = true;
            }
            else
            {
                item.ContentRemidiationAvailable = false;
                item.RemidiationMode = false;
                item.StudentAnswer = new StudentAnswer();
            }

            showQuestion.AssessmentItem = item;

            return showQuestion;
        }

        /// <summary>
        /// This method searches for all skipped questions and mark them as incomplete
        /// </summary>
        /// <param name="selectedQuestions">SelectedQuestion object, represent the list of all question which was aksed</param>
        public void MarkUnAnsweredQuestionsIncorrect(SelectedQuestion selectedQuestions)
        {

            foreach (QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                if (questionInfo.IsSkipped)
                {
                    questionInfo.IsSkipped = false;
                    questionInfo.IsCorrectlyAnswered = false;
                }
            }
        }

        #region Assessment Translation

        /// <summary>
        /// This method is used to get <c>AssessmentItem</c> object
        /// </summary>
        /// <param name="row">data row</param>
        /// <returns>returns <c>AssessmentItem</c> object</returns>
        private AssessmentItem GetAssessmentItemEntity(ICPAssessmentService.AssessmentItem assessmentItem)
        {
            AssessmentItem item = null;
            try
            {
               
                string AssessmentType = "";
                bool policyEachQuestionAnswered = false;
                ICPCourseService.CourseConfiguration courseConfiguration = null;
                using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    ICPCourseService.SequenceItem sequenceItem = null;
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);          
                    if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
                    {
                        AssessmentType = sequenceItem.ExamType;
                    }
                    else
                    {
                        AssessmentType = sequenceItem.SequenceItemType;
                    }
                   
                }
               

                string type = assessmentItem.QuestionType;
                item = GetAssessmentItemForType(type);

                item.AssessmentItemID = assessmentItem.AssessmentItemID;
                item.QuestionStem = assessmentItem.QuestionStem;
                item.AssessmentItemGuid = assessmentItem.AssessmentItemGuid;
                item.QuestionType = type;

                if (AssessmentType == CourseManager.SequenceItemTypeName.PreAssessment && courseConfiguration.PreAssessmentConfiguration.GradeQuestions == "AfterEachQuestionIsAnswered")
                {
                    item.PolicyEachQuestionAnswered = true;
                }

                else if (AssessmentType == CourseManager.SequenceItemTypeName.Quiz && courseConfiguration.QuizConfiguration.GradeQuestions == "AfterEachQuestionIsAnswered")
                {

                    item.PolicyEachQuestionAnswered = true;
                }

                else if (AssessmentType == CourseManager.SequenceItemTypeName.PostAssessment && courseConfiguration.PostAssessmentConfiguration.GradeQuestions == "AfterEachQuestionIsAnswered")
                {
                    item.PolicyEachQuestionAnswered = true;
                }

                else if (AssessmentType == CourseManager.SequenceItemTypeName.PracticeExam && courseConfiguration.PracticeAssessmentConfiguration.GradeQuestions == "AfterEachQuestionIsAnswered")
                {
                    item.PolicyEachQuestionAnswered = true;
                }

                
                // Optional parameters based on question type
                switch (item.QuestionType)
                {
                    case QuestionType.TextInputFITB:
                    case QuestionType.NumericInputFITB:
                        //((FillInTheBlankQuestion)item).IsAnswerCaseSensitive = ((ICPAssessmentService.FillInTheBlankQuestion)assessmentItem).IsAnswerCaseSensitive; 
                        break;
                    case QuestionType.Rating:
                        ((RatingQuestion)item).HighValueLabel = ((ICPAssessmentService.RatingQuestion)assessmentItem).HighValueLabel;

                        ((RatingQuestion)item).LowValueLabel = ((ICPAssessmentService.RatingQuestion)assessmentItem).LowValueLabel;

                        ((RatingQuestion)item).Rating = ((ICPAssessmentService.RatingQuestion)assessmentItem).Rating;

                        break;
                    case QuestionType.ImageTarget:
                        string icpFileSystem = ConfigurationManager.AppSettings["ICPFileSystem"];
                        if (((ICPAssessmentService.ImageTargetQuestion)assessmentItem).ImageURL.IndexOf("http") == -1)
                        {
                            ((ImageTargetQuestion)item).ImageURL = icpFileSystem + ((ICPAssessmentService.ImageTargetQuestion)assessmentItem).ImageURL;
                        }
                        else
                        {
                            ((ImageTargetQuestion)item).ImageURL = ((ICPAssessmentService.ImageTargetQuestion)assessmentItem).ImageURL;
                        }
                        if (((ICPAssessmentService.ImageTargetQuestion)assessmentItem).ImageURL.IndexOf('.') != -1)
                        {
                            ((ImageTargetQuestion)item).VisualTopType = System.IO.Path.GetExtension(((ICPAssessmentService.ImageTargetQuestion)assessmentItem).ImageURL).Remove(0, 1);
                        }
                        break;
                }


            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return item;
        }




        /// <summary>
        /// This method is used to create child assessment objects
        /// </summary>
        /// <param name="assetType">assessment item type</param>
        /// <returns>returns assessment item</returns>
        private AssessmentItem GetAssessmentItemForType(string type)
        {
            AssessmentItem item = null;

            try
            {
                switch (type)
                {
                    case QuestionType.TrueFalse:
                        {
                            item = new TrueFalseQuestion();
                        }
                        break;
                    case QuestionType.SingleSelectMCQ:
                        {
                            item = (MultipleChoiceQuestion)new SingleSelectQuestion();
                        }
                        break;
                    case QuestionType.MultipleSelectMCQ:
                        {
                            item = (MultipleChoiceQuestion)new MultipleSelectQuestion();
                        }
                        break;
                    case QuestionType.Matching:
                        {
                            item = new MatchingQuestion();
                        }
                        break;
                    case QuestionType.TextInputFITB:
                        {
                            item = (FillInTheBlankQuestion)new TextInputQuestion();
                        }
                        break;
                    case QuestionType.NumericInputFITB:
                        {
                            item = (FillInTheBlankQuestion)new NumericInputQuestion();
                        }
                        break;
                    case QuestionType.Ordering:
                        {
                            item = new OrderingQuestion();
                        }
                        break;
                    case QuestionType.ImageTarget:
                        {
                            item = new ImageTargetQuestion();
                        }
                        break;
                    case QuestionType.Rating:
                        {
                            item = new RatingQuestion();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return item;
        }

        /// <summary>
        /// This method is used to create child assessment item answer objects
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private AssessmentItemAnswer GetAssessmentItemAnswerForType(string type)
        {
            AssessmentItemAnswer answer = null;

            try
            {
                switch (type)
                {
                    case QuestionType.Matching:
                        {
                            answer = new MatchingAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.TextInputFITB:
                    case QuestionType.NumericInputFITB:
                        {
                            answer = new FITBAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.Ordering:
                        {
                            answer = new OrderingAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.ImageTarget:
                        {
                            answer = new ImageTargetAssessmentItemAnswer();
                        }
                        break;
                    default:
                        {
                            answer = new AssessmentItemAnswer();
                        }

                        break;

                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return answer;

        }


        /// <summary>
        /// This method is used to get assessment item answers list
        /// </summary>
        /// <param name="assessmentItemID">assessment item Id</param>
        /// <param name="questionType">question type</param>
        /// <returns>returns list of assessment item answer</returns>
        private List<AssessmentItemAnswer> GetAssessmentItemAnswers(ICPAssessmentService.AssessmentItem assessmentItem, string questionType)
        {
            List<AssessmentItemAnswer> answers = new List<AssessmentItemAnswer>();
            try
            {
                if (questionType != QuestionType.TextInputFITB)
                {
                    AssessmentItemAnswer answer = null;
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        answer = GetAssessmentItemAnswerEntity(assessmentItemAnswer, questionType);
                        answers.Add(answer);
                    }
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return answers;

        }


        /// <summary>
        /// This method is used to get <c>AssessmentItemAnswer</c> object
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="questionType">question type</param>
        /// <returns>returns an <c>AssessmentItemAnswer</c> object</returns>
        private AssessmentItemAnswer GetAssessmentItemAnswerEntity(ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer, string questionType)
        {

            AssessmentItemAnswer answer = null;
            List<ImageTargetCoordinate> coordinates = new List<ImageTargetCoordinate>();
            try
            {
                answer = GetAssessmentItemAnswerForType(questionType);
                answer.AssessmentItemAnswerID = assessmentItemAnswer.AssessmentItemAnswerID;
                answer.Label = assessmentItemAnswer.Label;
                answer.Value = assessmentItemAnswer.Value;
                //answer.IsCorrect = false;
                answer.DisplayOrder = assessmentItemAnswer.DisplayOrder;
                answer.AssessmentItemAnswerGuid = assessmentItemAnswer.AssessmentItemAnswerGuid;

                // Optional parameters based on question type
                switch (questionType)
                {
                    case QuestionType.Matching:
                        ((MatchingAssessmentItemAnswer)answer).RightItemText = ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItemAnswer).RightItemText;

                        //((MatchingAssessmentItemAnswer)answer).RightItemOrder = ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItemAnswer).RightItemOrder;

                        ((MatchingAssessmentItemAnswer)answer).LeftItemText = ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItemAnswer).LeftItemText;

                        //((MatchingAssessmentItemAnswer)answer).LeftItemOrder = ((ICPAssessmentService.MatchingAssessmentItemAnswer)assessmentItemAnswer).LeftItemOrder;

                        break;
                    case QuestionType.Ordering:
                        //((OrderingAssessmentItemAnswer)answer).CorrectOrder = ((ICPAssessmentService.OrderingAssessmentItemAnswer)assessmentItemAnswer).CorrectOrder;
                        break;
                }

                // Add Image Target Coordinates
                if (questionType == QuestionType.ImageTarget)
                {

                    coordinates = GetAssessmentItemAnswersImageTargetCoordinates(((ICPAssessmentService.ImageTargetAssessmentItemAnswer)assessmentItemAnswer).ImageTargetCoordinates);
                    ((ImageTargetAssessmentItemAnswer)answer).ImageTargetCoordinates = coordinates;
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return answer;
        }



        /// <summary>
        /// This method is used to get Assessment item answer image target coordinates
        /// </summary>
        /// <param name="answerID">answer Id</param>
        /// <returns>returns list of image target coordinates for answer</returns>
        private List<ImageTargetCoordinate> GetAssessmentItemAnswersImageTargetCoordinates(ICPAssessmentService.ImageTargetCoordinate[] imageTargetCoordinate)
        {
            List<ImageTargetCoordinate> coordinates = new List<ImageTargetCoordinate>();
            try
            {
                ImageTargetCoordinate coordinate = null;
                foreach (ICPAssessmentService.ImageTargetCoordinate singleImageTargetCoordinate in imageTargetCoordinate)
                {
                    coordinate = GetImageTargetCoordinateEntity(singleImageTargetCoordinate);
                    coordinates.Add(coordinate);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return coordinates;
        }

        public string GetAssessmentItemGUID(int assessmentItemID, ICPAssessmentService.AssessmentItem[] assessmentItems)
        {
            string asssessmentItemGUID = string.Empty;
            foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
            {
                if (assessmentItemID == assessmentItem.AssessmentItemID)
                {
                    asssessmentItemGUID = assessmentItem.AssessmentItemGuid;
                    break;
                }
            }
            return asssessmentItemGUID;
        }


        public string GetAssessmentAnswerItemGUID(int assessmentItemID, int studentAnswerID)
        {
            ICPAssessmentService.AssessmentItem[] assessmentItems = (ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];
            string asssessmentAnswerItemGUID = string.Empty;
            foreach (ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
            {
                if (assessmentItemID == assessmentItem.AssessmentItemID)
                {
                    foreach (ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
                    {
                        if (assessmentItemAnswer.AssessmentItemAnswerID == studentAnswerID)
                        {
                            asssessmentAnswerItemGUID = assessmentItemAnswer.AssessmentItemAnswerGuid;
                            break;
                        }
                    }
                    break;
                }
            }
            return asssessmentAnswerItemGUID;
        }
        /// <summary>
        /// This method is used to get image target coordinate
        /// </summary>
        /// <param name="row">date row</param>
        /// <returns>returns image target coordinate</returns>
        private ImageTargetCoordinate GetImageTargetCoordinateEntity(ICPAssessmentService.ImageTargetCoordinate singleImageTargetCoordinate)
        {
            ImageTargetCoordinate coordinate = null;
            try
            {
                coordinate = new ImageTargetCoordinate();
                coordinate.XPos = singleImageTargetCoordinate.XPos;
                coordinate.YPos = singleImageTargetCoordinate.YPos;
                coordinate.Width = singleImageTargetCoordinate.Width;
                coordinate.Height = singleImageTargetCoordinate.Height;
                coordinate.CoordinateOrder = singleImageTargetCoordinate.CoordinateOrder;
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }
            return coordinate;
        }
        /// <summary>
        /// This method gets and processes the assesmentitem template
        /// </summary>
        /// <param name="assessmentItemID">int assessmentitemID</param>
        /// <param name="audioURL">string audiourl</param>
        /// <returns>string questionhtml</returns>
        public string GetAssessmentItemTemplate(int assessmentItemID, out string audioURL, out string templateType, out string visualTopType)
        {
            ICPAssessmentService.AssessmentItemTemplate assessmentItemTemplate = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentItemTemplate();
            ICPAssessmentService.AssessmentItemAsset[] assessmentItemAssets = null;
            audioURL = string.Empty;
            templateType = string.Empty;
            visualTopType = string.Empty;
            string questionTemplate = string.Empty;
            string icpFileSystemURL = ConfigurationManager.AppSettings["ICPFileSystem"];
            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
            {
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                assessmentItemTemplate = assessmentService.GetAssessmentItemTemplate(assessmentItemID, out assessmentItemAssets);
                questionTemplate = ProcessQuestionTemplate(assessmentItemTemplate, assessmentItemAssets, icpFileSystemURL, ref audioURL, ref visualTopType);
                templateType = assessmentItemTemplate.Name;
            }
            return questionTemplate;
        }
        /// <summary>
        /// This method processes the template,e.g replaces placeholders etc.
        /// </summary>
        /// <param name="assessmentItemTemplate"></param>
        /// <param name="assessmentItemAssets"></param>
        /// <param name="icpFileSystemURL"></param>
        /// <param name="audioURL"></param>
        /// <returns></returns>
        private string ProcessQuestionTemplate(ICPAssessmentService.AssessmentItemTemplate assessmentItemTemplate, ICPAssessmentService.AssessmentItemAsset[] assessmentItemAssets, string icpFileSystemURL, ref string audioURL, ref string visualTopType)
        {
            string questionHTML = string.Empty;
            StringBuilder stringBuilder = new StringBuilder(assessmentItemTemplate.TemplateHTML);

            foreach (ICPAssessmentService.AssessmentItemAsset assessmentItemAsset in assessmentItemAssets)
            {
                if (assessmentItemAsset.IsVisualTF == true)
                {
                    if (assessmentItemTemplate.IsVisualAssetTF == true)
                    {
                        if (assessmentItemAsset.Assets.URL.IndexOf('.') != -1)
                            visualTopType = System.IO.Path.GetExtension(assessmentItemAsset.Assets.URL).Remove(0, 1);

                        if (assessmentItemAsset.Assets.URL.IndexOf("http://") == -1)
                            stringBuilder.Replace("$VisualTop", icpFileSystemURL + assessmentItemAsset.Assets.URL);
                        else
                            stringBuilder.Replace("$VisualTop", assessmentItemAsset.Assets.URL);
                        continue;
                    }
                }
                else if (assessmentItemAsset.IsAudioTF == true)
                {
                    if (assessmentItemTemplate.IsAudioAssesTF == true)
                    {
                        if (assessmentItemAsset.Assets.URL.IndexOf("http://") == -1)
                            audioURL = icpFileSystemURL + assessmentItemAsset.Assets.URL;
                        else
                            audioURL = assessmentItemAsset.Assets.URL;
                        continue;
                    }
                }
            }
            stringBuilder.Replace("$VisualTop", "");//case when asset not found
            questionHTML = stringBuilder.ToString();

            return questionHTML;
        }

        /// <summary>
        /// This method get the last learner statistics
        /// </summary>
        /// <param name="AttemptNo"></param>
        /// <param name="MaxAttemptNo"></param>        
        /// <returns></returns>
        private void GetAssessmentStatistics(out int AttemptNo, out int MaxAttemptNo)
        {
            AttemptNo = 0;
            MaxAttemptNo = 0;

            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

            CacheManager.CacheManager cacheManagerCC = new ICP4.BusinessLogic.CacheManager.CacheManager();
            ICPCourseService.CourseConfiguration courseConfiguration = new ICP4.BusinessLogic.ICPCourseService.CourseConfiguration();
            ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
            courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
            courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
            courseConfiguration = cacheManagerCC.GetIFConfigurationExistInCache(courseConfigurationID);

            if (courseConfiguration == null)
            {                
                courseConfiguration = courseService.GetCourseConfiguaration(courseConfigurationID);
                cacheManagerCC.CreateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
            }

            // Preview Case
            bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
            if (isPreview)
            {
                ICPTrackingService.LearnerStatistics[] learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[1];
                ICPTrackingService.LearnerStatistics learnerStatistic = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();

                learnerStatistics = (ICPTrackingService.LearnerStatistics[])System.Web.HttpContext.Current.Session["AssessmentEndStats"];
                if (learnerStatistics != null && learnerStatistics.Length > 0)
                {
                    AttemptNo = learnerStatistics[learnerStatistics.Length - 1].AssessmentAttemptNumber;
                }

                if (System.Web.HttpContext.Current.Session["AssessmentType"] != null)
                {
                    string AssessmentType = System.Web.HttpContext.Current.Session["AssessmentType"].ToString();
                    switch (AssessmentType)
                    {
                        case CourseManager.LearnerStatisticsType.PreAssessment:
                            {
                                MaxAttemptNo = courseConfiguration.PreAssessmentConfiguration.MaximumNOAttempt;
                                break;
                            }
                        case CourseManager.LearnerStatisticsType.PostAssessment:
                            {
                                MaxAttemptNo = courseConfiguration.PostAssessmentConfiguration.MaximumNOAttempt;
                                break;
                            }
                        case CourseManager.LearnerStatisticsType.Quiz:
                            {
                                MaxAttemptNo = courseConfiguration.QuizConfiguration.MaximumNOAttempt;
                                break;
                            }
                    }

                }
                return;

            }

            if (System.Web.HttpContext.Current.Session["AssessmentType"] != null)
            {
                string AssessmentType = System.Web.HttpContext.Current.Session["AssessmentType"].ToString();
                string learenrSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();


                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                ICPTrackingService.LearnerStatistics[] previousLearnerStatistics = null;

                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                switch (AssessmentType)
                {
                    case CourseManager.LearnerStatisticsType.PreAssessment:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.PreAssessmentEnd;
                            previousLearnerStatistics = trackingService.GetPreAssessmentResult(learenrSessionID);
                            MaxAttemptNo = courseConfiguration.PreAssessmentConfiguration.MaximumNOAttempt;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.PostAssessment:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.PostAssessmentEnd;
                            previousLearnerStatistics = trackingService.GetPostAssessmentResult(learenrSessionID);
                            MaxAttemptNo = courseConfiguration.PostAssessmentConfiguration.MaximumNOAttempt;
                            break;
                        }
                    case CourseManager.LearnerStatisticsType.Quiz:
                        {
                            learnerStatistics.Statistic_Type = CourseManager.LearnerStatisticsType.QuizEnd;
                            int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                            ICPCourseService.SequenceItem sequenceItem = null;
                            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                                learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                            }
                            previousLearnerStatistics = trackingService.GetQuizResult(learenrSessionID, sequenceItem.Item_GUID);
                            MaxAttemptNo = courseConfiguration.QuizConfiguration.MaximumNOAttempt;
                            break;
                        }
                }

                using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                    bool isPass = false;
                    bool actionTaken = false;
                    long learnerStatisticID = 0;
                    int remediationCount = 0;
                    courseManager.GetLastAssessmentResult(previousLearnerStatistics, 1, out isPass, out AttemptNo, out actionTaken, out learnerStatisticID, out remediationCount);
                }
            }
        }
        #endregion

        public string GetTextOnlyTemplate()
        {
            //Abdus Samad
            //LCMS-12105
            //Start

            //string template = @"" +
            //"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"padding: 0px; width: 98%; margin-top:-10px;height:auto;margin:right:auto;margin:left:0px;\"id=\"assessment-table\"><tbody><tr><td valign=\"top\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"padding: 0px;\"><tbody><tr><td valign=\"top\"><div style=\"margin-top: -10px;width:570px\" id=\"divQuizColumn1\"></div></td></tr><tr><td><div id=\"divQuestionDescription\" style=\"width:640px\"></div></td></tr></tbody></table></td><td valign=\"top\" style=\"margin-top: -50px;\"><div id=\"divIncorrect\" style=\"margin-top: -20px; margin-right: 8px; float:right\"></div><div id=\"divCorrect\" style=\"margin-top: -20px; margin-right: 8px; float:right\"></div></td></tr></tbody></table>";

            string template = @"" +
                "<section class=\"scene-wrapper visual-left\" id=\"assessment-table\"><div class=\"scene-body\"><div id=\"divQuizColumn1\"></div><div id=\"divQuestionDescription\"></div></div></section>";
            //"<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"padding: 0px; width: 98%; margin-top:-10px;height:auto;margin:right:auto;margin:left:0px;\"id=\"assessment-table\"><tbody><tr><td valign=\"top\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"padding: 0px;\"><tbody><tr><td valign=\"top\"><div style=\"margin-top: -10px;width:570px\" id=\"divQuizColumn1\"></div></td></tr><tr><td><div id=\"divQuestionDescription\" style=\"width:640px\"></div></td></tr></tbody></table></td><td valign=\"top\" style=\"margin-top: -50px;\"><div id=\"divIncorrect\" style=\"margin-top: -19px; margin-right: 8px; float:right\"></div><div id=\"divCorrect\" style=\"margin-top: -19px; margin-right: 8px; float:right\"></div></td></tr></tbody></table>";
            return template;
            
            //Abdus Samad
            //LCMS-12105
            //Stop

        }

        public void SessionAbandonOnAssessment() 
        {
            System.Web.HttpContext.Current.Session.Remove("AssessmentItemList");
            System.Web.HttpContext.Current.Session.Remove("SelectedQuestionSequence");
            System.Web.HttpContext.Current.Session.Remove("SelectedQuestionSequenceNo");
            System.Web.HttpContext.Current.Session.Remove("AssessmentRemediatonCount");
            System.Web.HttpContext.Current.Session.Remove("RemidiationIndexSequence");
            System.Web.HttpContext.Current.Session.Remove("AssessmentFlow");
            System.Web.HttpContext.Current.Session.Remove("RemidiationSequenceNumber");
            System.Web.HttpContext.Current.Session.Remove("AssessmentEndStats");
            System.Web.HttpContext.Current.Session.Remove("AssessmentStartTime");
            System.Web.HttpContext.Current.Session.Remove("ContentRemidiationAssessmentID");
            System.Web.HttpContext.Current.Session.Remove("AssessmentType");
            System.Web.HttpContext.Current.Session.Remove("AssessmentStage");
            System.Web.HttpContext.Current.Session.Remove("AssessmentAllQuestionsAttempted");
            System.Web.HttpContext.Current.Session.Remove("AssessmentAllQuestionsDisplayed");
            System.Web.HttpContext.Current.Session.Abandon();
        }


        // LCMS-9213
        public Hashtable GetAssessmentAnswerItemIDsByGuid(string answerGuids)
        {
            using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
            {
                assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                DataTable dt = assessmentService.GetAssessmentAnswerItemIDsByGuid(answerGuids);


                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ht.Add(dt.Rows[i]["ASSESSMENTITEMANSWER_GUID"].ToString(), dt.Rows[i]["ID"].ToString());
                    }
                }


                return ht;
            }
        }




        public ICPAssessmentService.AssessmentItem[] GetAssessmentItemsByGUIDs(string assessmentItemGuids)
        {
            try
            {
                using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                {
                    assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                    assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    return assessmentService.GetAssessmentItemsByGUIDs(assessmentItemGuids);
                }
            }
            catch (Exception ex) 
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }
        }




        public ICPAssessmentService.AssessmentItemBank[] GetAssessmentItemsByAssessmentBankIDs(string assessmentBankIDs)
        {
            try
            {
                using (ICPAssessmentService.AssessmentService assessmentService = new ICP4.BusinessLogic.ICPAssessmentService.AssessmentService())
                {
                    assessmentService.Url = ConfigurationManager.AppSettings["ICPAssessmentService"];
                    assessmentService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    return assessmentService.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }
        }
        public bool ResetRandomAlternateAssessmentItemStatsByAttempt()
        {
            try
            {
                ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                string learningSessionGuid = Convert.ToString(System.Web.HttpContext.Current.Session["LearnerSessionID"]);
                string assessmentType = LearnerStatisticsType.PostAssessment;
                string statisticsType = LearnerStatisticsType.PostAssessmentEnd;
                int remediationCount = Convert.ToInt32(System.Web.HttpContext.Current.Session["AssessmentRemediatonCount"]);
                int attemptNumber = Convert.ToInt32(HttpContext.Current.Session["LastAssessmentAttemptNo"]);

                trackingService.ResetAssessmentItemStatistics(learningSessionGuid, statisticsType, assessmentType, "", attemptNumber, remediationCount);
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }
            return true;
        }

        private string AssessmentMaxAttemptText(string attemptMessge, int attemptNumber)
        {
            if (attemptNumber == 1)
            {
                attemptMessge = attemptMessge.Replace("attempts", "attempt");
            }
            return attemptMessge;

        }
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}

