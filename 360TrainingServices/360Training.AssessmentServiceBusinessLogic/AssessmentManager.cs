using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using _360Training.BusinessEntities;
using _360Training.AssessmentServiceDataLogic.AssessmentDA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections;
using System.Data;

namespace _360Training.AssessmentServiceBusinessLogic
{
    public class AssessmentManager:IAssessmentManager,IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IAssessmentManger Members

        //LCMS-9213
        public List<AssessmentItem> GetAssessmentItemsByGUIDs(string assessmentItemGuids)
        {
            using (AssesmentDA assesmentDA = new AssesmentDA())
            {
                return assesmentDA.GetAssessmentItemsByGUIDs(assessmentItemGuids);
            }
        }



        /// <summary>
        /// This method returns the list of assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetPreAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID)
        {
            try
            {
                if (previouslyAskedQuestionsGUIDs == null)
                    previouslyAskedQuestionsGUIDs = new List<string>();

                 List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                 if (courseConfiguration.PreAssessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX || courseConfiguration.PreAssessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE)
                 {
                     assessmentItems = GetAssessmentAssessmentItemsForAdvanceQuestionSelection(courseID, courseConfiguration.PreAssessmentConfiguration.RandomizeQuestion,
                         courseConfiguration.PreAssessmentConfiguration.RandomizeAnswers,
                         courseConfiguration.PreAssessmentConfiguration.NOQuestion,
                         previouslyAskedQuestionsGUIDs, courseConfiguration.PreAssessmentConfiguration.AdvanceQuestionSelectionType,
                         AssessmentConfiguration.ASSESSMENTYPE_PREASSESSMENT, 0, examID);
                     return assessmentItems;
                     
                 }

                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    List<AssessmentItem> deletedAssessmentItems=new List<AssessmentItem>();
                    if (examID > 0)
                        assessmentItems = assesmentDA.GetExamAssessmentItems(courseID, !courseConfiguration.PreAssessmentConfiguration.UseWeightedScore, examID);
                    else
                        assessmentItems = assesmentDA.GetPreAssessmentAssessmentItems(courseID, !courseConfiguration.PreAssessmentConfiguration.UseWeightedScore);

                    if (courseConfiguration.PreAssessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
                    {
                        deletedAssessmentItems= DeleteQuestionsByGUID(assessmentItems, previouslyAskedQuestionsGUIDs);
                        //Fix for LCMS-3135
                        if (deletedAssessmentItems.Count > 1)
                        {
                            RandomizeQuestions(ref deletedAssessmentItems);
                        }
                    }
                    
                    int numberOfQuestion = 0;
                    if (courseConfiguration.PreAssessmentConfiguration.NOQuestion > 0)
                    {
                        numberOfQuestion = courseConfiguration.PreAssessmentConfiguration.NOQuestion;
                    }
                    else
                    {
                        numberOfQuestion = Convert.ToInt32(ConfigurationSettings.AppSettings["PreAssessmentNOQuestion"]);
                    }
                    if (courseConfiguration.PreAssessmentConfiguration.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }
                    //if (courseConfiguration.PreAssessmentRandomizeAnswers == true)
                    //{
                    
                    //}

                    ReduceToNumberOfQuestionsAllowed(assessmentItems,numberOfQuestion); 
                    if (assessmentItems.Count < numberOfQuestion)
                        AddBackQuestions(assessmentItems, deletedAssessmentItems, numberOfQuestion - assessmentItems.Count);
                    
                    if (courseConfiguration.PreAssessmentConfiguration.RandomizeAnswers == true) //LCMS-7282
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }
                    
                }
                return assessmentItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        


        /// <summary>
        /// This method returns the arraylist containing current bank ID on one index and assessmentitem list on second index
        /// </summary>
        /// <param name="askedBanks">string askedBanks</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>        
        /// <returns>ArrayList</returns>
        public ArrayList GetAssessmentItemsForRandomAlternateInPreviewMode(string askedBanks, AssessmentConfiguration assessmentConfig)
        {
            try
            {
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                ArrayList arrToReturn = new ArrayList();

                using (AssesmentDA assesmentDA = new AssesmentDA())
                {                    
                    /*
                     * This dataset has two datatables in itself. One contains all the banks associated with 
                     * assessment configuration and the other contains previously asked banks
                     */
                    System.Data.DataSet ds = assesmentDA.GetAssessmentItemsForRandomAlternateInPreviewMode(askedBanks, assessmentConfig);

                    string[] askedBanksArr = askedBanks.Split(',');
                    bool hasListExhausted = (ds.Tables[0].Rows.Count <= askedBanksArr.Length) && (askedBanks != "");
                    int countBanks = ds.Tables[0].Rows.Count;
                    int bankIndexToAsk = 0;
                    string currentBankID = "";
                    if (hasListExhausted) // if list has been exhausted
                    {                        
                        bankIndexToAsk = askedBanksArr.Length % ds.Tables[0].Rows.Count;
                        currentBankID = askedBanksArr[bankIndexToAsk];
                        /*
                         * This statement picks a bank ID from previously asked banks and gets the child assessment items 
                         * This banks selection would be done according to the last sequence in which banks were asked (before the list was exhausted)
                         */                        
                        assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(Convert.ToInt32(askedBanksArr[bankIndexToAsk]));
                     
                    }
                    else // if list has not been exhausted
                    {
                        
                        // Previously asked banks are getting deleted here (from the total banks)
                        // ------------------------------------------------------------------------------------------
                        for (int i = 0; i < askedBanksArr.Length ; i++)
                        {
                            if (askedBanksArr[i] != "")
                            {
                                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ASSESSMENTITEMBANK_ID=" + askedBanksArr[i])[0]);
                            }
                        }
                        ds.Tables[0].AcceptChanges();
                        // ------------------------------------------------------------------------------------------
                        int remainingBanksCount = ds.Tables[0].Rows.Count;
                        bankIndexToAsk = new Random().Next(0, remainingBanksCount);
                        currentBankID = ds.Tables[0].Rows[bankIndexToAsk]["ASSESSMENTITEMBANK_ID"].ToString();

                        // This statement gets the child assessment items of a randomly selected bank (but not from those banks that are previously asked)
                        assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(Convert.ToInt32(ds.Tables[0].Rows[bankIndexToAsk]["ASSESSMENTITEMBANK_ID"].ToString()));
                     
                    }
                    arrToReturn.Add(currentBankID);
                    
                    if (assessmentConfig.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }

                    if (assessmentConfig.RandomizeAnswers == true)
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }

                    arrToReturn.Add(assessmentItems);


                }
                return arrToReturn;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }







        /// <summary>
        /// This method returns the list of post assessmentitems
        /// </summary>
        /// <param name="learnerSessionID">string learningSessionID</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>        
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetAssessmentItemsForRandomAlternate(string learnerSessionID, AssessmentConfiguration assessmentConfig,bool isPauseResumeCase)
        {
            try
            {
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {

                    
                    /*
                     * This dataset has two datatables in itself. One contains all the banks associated with 
                     * assessment configuration and the other contains previously asked banks
                     */
                 
                    System.Data.DataSet ds = assesmentDA.GetAssessmentItemBanksForRandomAlternate(learnerSessionID, assessmentConfig);
                    

                    bool hasListExhausted = (ds.Tables[0].Rows.Count <= ds.Tables[1].Rows.Count);
                    int countBanks = ds.Tables[0].Rows.Count;
                    int bankIndexToAsk = 0;
                    //LCMS-10266
                    if (isPauseResumeCase)
                    {
                        bankIndexToAsk = (ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows.Count - 1 : ds.Tables[1].Rows.Count);
                                  
                        assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(Convert.ToInt32(ds.Tables[1].Rows[bankIndexToAsk]["ASSESSMENTITEMBANK_ID"].ToString()));
                    }
                        //END
                    else if (hasListExhausted) // if list has been exhausted
                    {
                        bankIndexToAsk = ds.Tables[1].Rows.Count % ds.Tables[0].Rows.Count;

                        /*
                         * This statement picks a bank ID from previously asked banks and gets the child assessment items 
                         * This banks selection would be done according to the last sequence in which banks were asked (before the list was exhausted)
                         */
                        assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(Convert.ToInt32(ds.Tables[1].Rows[bankIndexToAsk]["ASSESSMENTITEMBANK_ID"].ToString()));
                        
                    }
                    else // if list has not been exhausted
                    {
                           
                        // Previously asked banks are getting deleted here (from the total banks)
                        // ------------------------------------------------------------------------------------------
                        for(int i=0 ; i < ds.Tables[1].Rows.Count ; i++)
                        {
                            ds.Tables[0].Rows.Remove(ds.Tables[0].Select("ASSESSMENTITEMBANK_ID=" + ds.Tables[1].Rows[i]["ASSESSMENTITEMBANK_ID"].ToString())[0]);
                        }
                        ds.Tables[0].AcceptChanges();
                        // ------------------------------------------------------------------------------------------
                        
                        int remainingBanksCount = ds.Tables[0].Rows.Count;
                        bankIndexToAsk = new Random().Next(0, remainingBanksCount);

                        // This statement gets the child assessment items of a randomly selected bank (but not from those banks that are previously asked)
                        assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(Convert.ToInt32(ds.Tables[0].Rows[bankIndexToAsk]["ASSESSMENTITEMBANK_ID"].ToString()));
                        
                    }

                    
                    if (assessmentConfig.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }

                    if (assessmentConfig.RandomizeAnswers == true)
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }

                }
                return assessmentItems;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        /// <summary>
        /// This method returns the list of post assessmentitems
        /// </summary>
        /// <param name="learnerSessionID">string learningSessionID</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>        
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetTestForRandomAlternateMultipleItemBanks(string learnerSessionID, AssessmentConfiguration assessmentConfig, bool isPauseResumeCase)
        {
            try
            {
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {


                    /*
                     * This dataset has two datatables in itself. One contains all the banks associated with 
                     * assessment configuration and the other contains previously asked banks
                     */

                    System.Data.DataSet ds = assesmentDA.GetTestForRandomAlternate(learnerSessionID, assessmentConfig);


                    bool hasListExhausted = (ds.Tables[0].Rows.Count <= ds.Tables[1].Rows.Count);
                    int countTests = ds.Tables[0].Rows.Count;
                    int testIndexToAsk = 0;
                    //LCMS-10266
                    if (isPauseResumeCase)
                    {
                        testIndexToAsk = (ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows.Count - 1 : ds.Tables[1].Rows.Count);

                        assessmentItems = assesmentDA.GetAssessmentItemsByTestID(Convert.ToInt32(ds.Tables[1].Rows[testIndexToAsk]["TEST_ID"].ToString()));
                    }
                        //End
                    else if (hasListExhausted) // if list has been exhausted
                    {
                        testIndexToAsk = ds.Tables[1].Rows.Count % ds.Tables[0].Rows.Count;

                        /*
                         * This statement picks a bank ID from previously asked banks and gets the child assessment items 
                         * This banks selection would be done according to the last sequence in which banks were asked (before the list was exhausted)
                         */
                        assessmentItems = assesmentDA.GetAssessmentItemsByTestID(Convert.ToInt32(ds.Tables[1].Rows[testIndexToAsk]["TEST_ID"].ToString()));

                    }
                    else // if list has not been exhausted
                    {

                        // Previously asked banks are getting deleted here (from the total banks)
                        // ------------------------------------------------------------------------------------------
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows.Remove(ds.Tables[0].Select("TEST_ID=" + ds.Tables[1].Rows[i]["TEST_ID"].ToString())[0]);
                        }
                        ds.Tables[0].AcceptChanges();
                        // ------------------------------------------------------------------------------------------

                        int remainingBanksCount = ds.Tables[0].Rows.Count;
                        testIndexToAsk = new Random().Next(0, remainingBanksCount);

                        // This statement gets the child assessment items of a randomly selected bank (but not from those banks that are previously asked)
                        assessmentItems = assesmentDA.GetAssessmentItemsByTestID(Convert.ToInt32(ds.Tables[0].Rows[testIndexToAsk]["TEST_ID"].ToString()));

                    }


                    if (assessmentConfig.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }

                    if (assessmentConfig.RandomizeAnswers == true)
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }

                }
                return assessmentItems;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        /// <summary>
        /// This method returns the arraylist containing current bank ID on one index and assessmentitem list on second index
        /// </summary>
        /// <param name="askedBanks">string askedTests</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>        
        /// <returns>ArrayList</returns>        
        public ArrayList GetTestForRandomAlternateMultipleItemBankInPreviewMode(string askedTests, AssessmentConfiguration assessmentConfig)
        {
            try
            {
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                ArrayList arrToReturn = new ArrayList();

                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    /*
                     * This dataset has two datatables in itself. One contains all the banks associated with 
                     * assessment configuration and the other contains previously asked banks
                     */
                    System.Data.DataSet ds = assesmentDA.GetTestForRandomAlternateMultipleItemBanksInPreviewMode(askedTests, assessmentConfig);

                    string[] askedTestsArr = askedTests.Split(',');
                    bool hasListExhausted = (ds.Tables[0].Rows.Count <= askedTestsArr.Length) && (askedTests != "");
                    int countTests = ds.Tables[0].Rows.Count;
                    int testIndexToAsk = 0;
                    string currentTestID = "";
                    if (hasListExhausted) // if list has been exhausted
                    {
                        testIndexToAsk = askedTestsArr.Length % ds.Tables[0].Rows.Count;
                        currentTestID = askedTestsArr[testIndexToAsk];
                        /*
                         * This statement picks a bank ID from previously asked banks and gets the child assessment items 
                         * This banks selection would be done according to the last sequence in which banks were asked (before the list was exhausted)
                         */
                        assessmentItems = assesmentDA.GetAssessmentItemsByTestID(Convert.ToInt32(askedTestsArr[testIndexToAsk]));

                    }
                    else // if list has not been exhausted
                    {

                        // Previously asked banks are getting deleted here (from the total banks)
                        // ------------------------------------------------------------------------------------------
                        for (int i = 0; i < askedTestsArr.Length; i++)
                        {
                            if (askedTestsArr[i] != "")
                            {
                                ds.Tables[0].Rows.Remove(ds.Tables[0].Select("TEST_ID=" + askedTestsArr[i])[0]);
                            }
                        }
                        ds.Tables[0].AcceptChanges();
                        // ------------------------------------------------------------------------------------------
                        int remainingBanksCount = ds.Tables[0].Rows.Count;
                        testIndexToAsk = new Random().Next(0, remainingBanksCount);
                        currentTestID = ds.Tables[0].Rows[testIndexToAsk]["TEST_ID"].ToString();

                        // This statement gets the child assessment items of a randomly selected bank (but not from those banks that are previously asked)
                        assessmentItems = assesmentDA.GetAssessmentItemsByTestID(Convert.ToInt32(ds.Tables[0].Rows[testIndexToAsk]["TEST_ID"].ToString()));

                    }
                    arrToReturn.Add(currentTestID);

                    if (assessmentConfig.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }

                    if (assessmentConfig.RandomizeAnswers == true)
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }

                    arrToReturn.Add(assessmentItems);


                }
                return arrToReturn;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public List<AssessmentItem> TestGetAssessmentItemsForBankID(int BankID)
        {
            try
            {
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {


                    assessmentItems = assesmentDA.GetAssessmentItemsByAssessmentBankID(BankID);

                }
                return assessmentItems;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        // LCMS-9882
        public List<AssessmentItemBank> GetAssessmentItemsByAssessmentBankIDs(string assessmentBankIDs)
        {
            try
            {
                List<AssessmentItemBank> assessmentItemBanks = new List<AssessmentItemBank>();
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    assessmentItemBanks = assesmentDA.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
                }
                return assessmentItemBanks;
            }
            catch (Exception exp)
            {

                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }




        /// <summary>
        /// This method returns the list of post assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetPostAssessmentAssessmentItems(int courseID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID)
        {
            try
            {
                if (previouslyAskedQuestionsGUIDs == null)
                    previouslyAskedQuestionsGUIDs = new List<string>();

                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                if (courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE)
                {
                    assessmentItems = GetAssessmentAssessmentItemsForAdvanceQuestionSelection(courseID, courseConfiguration.PostAssessmentConfiguration.RandomizeQuestion,
                        courseConfiguration.PostAssessmentConfiguration.RandomizeAnswers,
                        courseConfiguration.PostAssessmentConfiguration.NOQuestion,
                        previouslyAskedQuestionsGUIDs, courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType,
                        AssessmentConfiguration.ASSESSMENTYPE_POSTASSESSMET, 0, examID);
                    return assessmentItems;

                }
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    List<AssessmentItem> deletedAssessmentItems = new List<AssessmentItem>();
                    if (examID > 0)
                        assessmentItems = assesmentDA.GetExamAssessmentItems(courseID, !courseConfiguration.PostAssessmentConfiguration.UseWeightedScore, examID);
                    else
                        assessmentItems = assesmentDA.GetPostAssessmentAssessmentItems(courseID, !courseConfiguration.PostAssessmentConfiguration.UseWeightedScore);
                    if (courseConfiguration.PostAssessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
                    {
                        deletedAssessmentItems= DeleteQuestionsByGUID(assessmentItems, previouslyAskedQuestionsGUIDs);
                        //Fix for LCMS-3135
                        if (deletedAssessmentItems.Count > 1)
                        {
                            RandomizeQuestions(ref deletedAssessmentItems);
                        }
                    }
                    int numberOfQuestion = 0;
                    if (courseConfiguration.PostAssessmentConfiguration.NOQuestion > 0)
                    {
                        numberOfQuestion = courseConfiguration.PostAssessmentConfiguration.NOQuestion;
                    }
                    else
                    {
                        numberOfQuestion = Convert.ToInt32(ConfigurationSettings.AppSettings["PostAssessmentNOQuestion"]);
                    }
                    if (courseConfiguration.PostAssessmentConfiguration.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }
                    //if (courseConfiguration.PostAssessmentRandomizeAnswers == true) //LCMS-4061
                    //{
                    
                    //}
                    ReduceToNumberOfQuestionsAllowed(assessmentItems, numberOfQuestion); 
                    if (assessmentItems.Count < numberOfQuestion)
                        AddBackQuestions(assessmentItems, deletedAssessmentItems, numberOfQuestion - assessmentItems.Count);
                    
                    if (courseConfiguration.PostAssessmentConfiguration.RandomizeAnswers == true) //LCMS-7282
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }
                    
                }
                return assessmentItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the list of quiz assessmentitems
        /// </summary>
        /// <param name="courseID">int contentObjectID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetQuizAssessmentAssessmentItems(int courseID,int contentObjectID, CourseConfiguration courseConfiguration, List<string> previouslyAskedQuestionsGUIDs,int examID)
        {
            try
            {
                if (previouslyAskedQuestionsGUIDs == null)
                    previouslyAskedQuestionsGUIDs = new List<string>();
                
                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                if (courseConfiguration.QuizConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX || courseConfiguration.QuizConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE)
                {
                    assessmentItems = GetAssessmentAssessmentItemsForAdvanceQuestionSelection(courseID, courseConfiguration.QuizConfiguration.RandomizeQuestion,
                        courseConfiguration.QuizConfiguration.RandomizeAnswers,
                        courseConfiguration.QuizConfiguration.NOQuestion,
                        previouslyAskedQuestionsGUIDs, courseConfiguration.QuizConfiguration.AdvanceQuestionSelectionType,
                        AssessmentConfiguration.ASSESSMENTYPE_QUIZ, contentObjectID, examID);
                    return assessmentItems;

                }
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    List<AssessmentItem> deletedAssessmentItems = new List<AssessmentItem>();
                    if (examID > 0)
                    {
                        assessmentItems = assesmentDA.GetExamAssessmentItems(courseID, !courseConfiguration.QuizConfiguration.UseWeightedScore, examID);
                        Logger.Write("GetQuizAssessmentAssessmentItems() ASSESSMENTITEM COUNT :" + assessmentItems.Count.ToString() + " EXAMID=" + examID.ToString() , "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                        
                    }
                    else
                    {
                        assessmentItems = assesmentDA.GetQuizAssessmentItems(contentObjectID, !courseConfiguration.QuizConfiguration.UseWeightedScore);
                        Logger.Write("GetQuizAssessmentAssessmentItems() ASSESSMENTITEM COUNT :" + assessmentItems.Count.ToString() + " CONTENTOBJECTID=" + contentObjectID.ToString(), "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    }

                    if (courseConfiguration.QuizConfiguration.EnforceUniqueQuestionsOnRetake == true)
                    {
                        deletedAssessmentItems =DeleteQuestionsByGUID(assessmentItems, previouslyAskedQuestionsGUIDs);
                        //Fix for LCMS-3135
                        if (deletedAssessmentItems.Count > 1)
                        {
                            RandomizeQuestions(ref deletedAssessmentItems);
                        }
                    }
                    int numberOfQuestion = 0;
                    if (courseConfiguration.QuizConfiguration.NOQuestion > 0)
                    {
                        numberOfQuestion = courseConfiguration.QuizConfiguration.NOQuestion;
                    }
                    else
                    {
                        numberOfQuestion = Convert.ToInt32(ConfigurationSettings.AppSettings["QuizNOQuestion"]);
                    }
                    if (courseConfiguration.QuizConfiguration.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }
                    //if (courseConfiguration.QuizRandomizeAnswers == true)
                    //{
                    
                    //}
                    ReduceToNumberOfQuestionsAllowed(assessmentItems, numberOfQuestion); 
                    if (assessmentItems.Count < numberOfQuestion)
                        AddBackQuestions(assessmentItems, deletedAssessmentItems, numberOfQuestion - assessmentItems.Count);
                    
                    if (courseConfiguration.QuizConfiguration.RandomizeAnswers == true) //LCMS-7282
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }
                }

                Logger.Write("GetQuizAssessmentAssessmentItems() RETURN ASSESSMENTITEM COUNT :" + assessmentItems.Count.ToString() + " CONTENTOBJECTID=" + contentObjectID.ToString(), "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                return assessmentItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        /// <summary>
        /// This method returns the list of Practice Exam assessmentitems
        /// </summary>
        /// <param name="courseID">int contentObjectID</param>
        /// <param name="courseConfiguration">CourseConfiguration object</param>
        /// <param name="previouslyAskedQuestionsGUIDs">List of string previouslyAskedQuestionsGUIDs</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetPracticeExamAssessmentItems(int courseID, int contentObjectID, AssessmentConfiguration assessmentConfiguration, List<string> previouslyAskedQuestionsGUIDs, int examID)
        {
            try
            {
                if (previouslyAskedQuestionsGUIDs == null)
                    previouslyAskedQuestionsGUIDs = new List<string>();

                List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
                if (assessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX || assessmentConfiguration.AdvanceQuestionSelectionType == AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE)
                {
                    assessmentItems = GetAssessmentAssessmentItemsForAdvanceQuestionSelection(courseID, assessmentConfiguration.RandomizeQuestion,
                        assessmentConfiguration.RandomizeAnswers,
                        assessmentConfiguration.NOQuestion,
                        previouslyAskedQuestionsGUIDs, assessmentConfiguration.AdvanceQuestionSelectionType,
                        AssessmentConfiguration.ASSESSMENTYPE_PRACTICEEXAM, contentObjectID, examID);
                    return assessmentItems;

                }
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    List<AssessmentItem> deletedAssessmentItems = new List<AssessmentItem>();
                    assessmentItems = assesmentDA.GetExamAssessmentItems(courseID, !assessmentConfiguration.UseWeightedScore, examID);

                    if (assessmentConfiguration.EnforceUniqueQuestionsOnRetake == true)
                    {
                        deletedAssessmentItems = DeleteQuestionsByGUID(assessmentItems, previouslyAskedQuestionsGUIDs);
                        //Fix for LCMS-3135
                        if (deletedAssessmentItems.Count > 1)
                        {
                            RandomizeQuestions(ref deletedAssessmentItems);
                        }
                    }
                    int numberOfQuestion = 0;
                    if (assessmentConfiguration.NOQuestion > 0)
                    {
                        numberOfQuestion = assessmentConfiguration.NOQuestion;
                    }
                    else
                    {
                        numberOfQuestion = Convert.ToInt32(ConfigurationSettings.AppSettings["PracticeExamNOQuestion"]);
                    }
                    if (assessmentConfiguration.RandomizeQuestion == true)
                    {
                        RandomizeQuestions(ref assessmentItems);
                    }
                    
                    ReduceToNumberOfQuestionsAllowed(assessmentItems, numberOfQuestion);
                    if (assessmentItems.Count < numberOfQuestion)
                        AddBackQuestions(assessmentItems, deletedAssessmentItems, numberOfQuestion - assessmentItems.Count);

                    if (assessmentConfiguration.RandomizeAnswers == true) //LCMS-7282
                    {
                        RandomizeAnswers(ref assessmentItems);
                    }

                }
                return assessmentItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        private List<AssessmentItem> DeleteQuestionsByGUID(List<AssessmentItem> assessmentItems, List<string> previouslyAskedQuestionsGUIDs)
        {
            List<AssessmentItem> deletedAssessmentItems=new List<AssessmentItem>();
            foreach (string item in previouslyAskedQuestionsGUIDs)
            {
                for(int index=0;index<=assessmentItems.Count-1;index++)
                {
                    if (assessmentItems[index].AssessmentItemGuid==item)
                    {
                        deletedAssessmentItems.Add(assessmentItems[index]);
                        assessmentItems.RemoveAt(index); 
                    }
                }
            }
            return deletedAssessmentItems;
        }
        private void ReduceToNumberOfQuestionsAllowed(List<AssessmentItem> assessmentItems, int noOfQuestionsAllowed)
        {
            if (assessmentItems.Count > noOfQuestionsAllowed)
                assessmentItems.RemoveRange(noOfQuestionsAllowed, assessmentItems.Count - noOfQuestionsAllowed);
        }
        private void RandomizeQuestions(ref List<AssessmentItem> assessmentItems)
        {
            List<AssessmentItem> randomAssessmentItems=new List<AssessmentItem>();
            List<int> randomNumbers = new List<int>();
            int randomNumber=-1;
            foreach (AssessmentItem assessmentItem in assessmentItems)
            {
                Random random = new Random(System.DateTime.Now.Millisecond);
                while (true)
                {
                    randomNumber = random.Next(0, assessmentItems.Count);
                    if (randomNumbers.Contains(randomNumber) == true)
                        continue;
                    else
                    {
                        randomNumbers.Add(randomNumber);
                        randomAssessmentItems.Add(assessmentItems[randomNumber]);
                        break;
                    }
                }
            }
            assessmentItems = randomAssessmentItems;
        }
        public void RandomizeAnswers(ref List<AssessmentItem> assessmentItems)
        {
            List<AssessmentItemAnswer> randomAssessmentItemAnswers=new List<AssessmentItemAnswer>(); 
            List<int> randomNumbers ;
            int randomNumber = -1;
            Random random = new Random(System.DateTime.Now.Millisecond);
            foreach (AssessmentItem assessmentItem in assessmentItems)
            {
                //LCMS-4061
                if (assessmentItem.Disablerandomizeanswerchoicetf == true || assessmentItem.QuestionType == QuestionType.TextInputFITB)
                    continue;
                //LCMS-4061

                randomNumbers = new List<int>();
                randomAssessmentItemAnswers=new List<AssessmentItemAnswer>();
                int IsSameOrderCounter = 0;

                for (int index = 0; index < assessmentItem.AssessmentAnswers.Count;index++ )
                {
                    while (true)
                    {
                        randomNumber = random.Next(0, assessmentItem.AssessmentAnswers.Count);
                        if (randomNumbers.Contains(randomNumber) == true)
                            continue;
                        else
                        {
                            randomNumbers.Add(randomNumber);
                            randomAssessmentItemAnswers.Add(assessmentItem.AssessmentAnswers[randomNumber]);
                            if (index + 1 == assessmentItem.AssessmentAnswers[randomNumber].DisplayOrder)
                            {
                                IsSameOrderCounter++;
                            }

                            break;
                        }
                    }
                }
                if (randomAssessmentItemAnswers.Count >0 && IsSameOrderCounter == randomAssessmentItemAnswers.Count)//Swapp
                {
                    AssessmentItemAnswer assessmentItemAnswer = randomAssessmentItemAnswers[0];
                    randomAssessmentItemAnswers[0] = randomAssessmentItemAnswers[randomAssessmentItemAnswers.Count - 1];
                    randomAssessmentItemAnswers[randomAssessmentItemAnswers.Count - 1] = assessmentItemAnswer;

                }
                assessmentItem.AssessmentAnswers=randomAssessmentItemAnswers;
            }
        }
        private void AddBackQuestions(List<AssessmentItem> assessmentItems, List<AssessmentItem> deletedAssessmentItems,int noOfAddBackQuestions)
        {
            AddtoAssessmentItemList(assessmentItems, deletedAssessmentItems,noOfAddBackQuestions);
        }
        private void AddtoAssessmentItemList(List<AssessmentItem> targetAssessmentItems, List<AssessmentItem> sourceAssessmentItems,int noOfItems)
        {
            for(int index=0;index<sourceAssessmentItems.Count;index++)
            {
                if (noOfItems == index)
                    break;
                else
                    targetAssessmentItems.Add(sourceAssessmentItems[index]);
            }
        }
        /// <summary>
        /// This method returns the sceneGUID for the given AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemGUID">string assessmentItemGUID</param>
        /// <returns>string sceneGUID</returns>
        public List<string> GetAssessmentItemScene(string assessmentItemGUID,int courseID)
        {
            try
            {
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    return assesmentDA.GetAssessmentItemScene(assessmentItemGUID,courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        private AssessmentItemTemplate GetAssessmentItemAssessmentItemTemplate(int assessmentItemTemplateID)
        {
            AssessmentItemTemplate assessmentItemTemplate = new AssessmentItemTemplate();
            try
            {
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    assessmentItemTemplate= assesmentDA.GetAssessmentItemAssessmentItemTemplate(assessmentItemTemplateID);
                }
                return assessmentItemTemplate;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return assessmentItemTemplate;
            }
        }
        /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        private List<AssessmentItemAsset> GetAssessmentItemAssets(int assessmentItemID)
        {
            List<AssessmentItemAsset> assessmentItemAssets = new List<AssessmentItemAsset>();
            try
            {
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    assessmentItemAssets=assesmentDA.GetAssessmentItemAssets(assessmentItemID);
                }
                return assessmentItemAssets;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return assessmentItemAssets;
            }
        }
         /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID and the assets associated with them
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        public AssessmentItemTemplate GetAssessmentItemTemplate(int assessmentItemID, out List<AssessmentItemAsset> assessmentItemAssets)
        {
            assessmentItemAssets = new List<AssessmentItemAsset>();
            try
            {
                assessmentItemAssets = GetAssessmentItemAssets(assessmentItemID);
                return GetAssessmentItemAssessmentItemTemplate(assessmentItemID);
                
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                assessmentItemAssets = new List<AssessmentItemAsset>();
                return new AssessmentItemTemplate();
            }
        }
        /// <summary>
        /// This mehtid gets the knowledge check assessment items by scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of assessmentitems object</returns>
        public List<AssessmentItem> GetKnowledgeCheckAssessmentItems(int sceneID)
        {
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            try
            {
                using (AssesmentDA assesmentDA = new AssesmentDA())
                {
                    assessmentItems = assesmentDA.GetSceneAssessmentItems(sceneID);
                    //LCMS-4007 
                    RandomizeQuestions(ref assessmentItems);
                    RandomizeAnswers(ref assessmentItems);
                }
                return assessmentItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return new List<AssessmentItem>();
            }
        }
        #endregion




        public List<AssessmentItem> GetAssessmentAssessmentItemsForAdvanceQuestionSelection(int courseID, bool randomizeQuestion, bool randomizeAnswer, int noOfQuestions, List<string> previouslyAskedQuestionsGUIDs, string advanceQuestionSelectionMode, string assessmentType, int contentObjectID,int examID)
        {
            try
            {
                List<AssessmentItem> completeAssessmentItemList = new List<AssessmentItem>();
                AssesmentDA assesmentDA = new AssesmentDA();
                DataTable dtLoList = new DataTable();

                if (examID > 0)
                {
                    dtLoList = assesmentDA.GetExamTopicForAdvacne(examID);
                }
                else
                {

                    if (assessmentType == SequenceItemType.PostAssessment)
                    {
                        dtLoList = assesmentDA.GetPostAssessmentLearningObjectiveForCourse(courseID);
                    }
                    else if (assessmentType == SequenceItemType.Quiz)
                    {
                        dtLoList = assesmentDA.GetQuizAssessmentLearningObjectiveForCourse(courseID, contentObjectID);
                    }
                    else if (assessmentType == SequenceItemType.PreAssessment)
                    {
                        dtLoList = assesmentDA.GetPreAssessmentLearningObjectiveForCourse(courseID);
                    }
                }


                int totalNumberOfQuestions = 0;
                int totalNumberOfQuestionsSumofMin = 0;

                DataSet dsAssessmentItems = new DataSet();
                //foreach(DataRow rowLO in dtLoList.Rows)
                for (int index = 0; index < dtLoList.Rows.Count; index++)
                {
                    //add new datatable in dsAssessmentItems
                    DataTable dtAssessmentItemID = null;

                    if (examID > 0)
                    {
                        dtAssessmentItemID = assesmentDA.GetAssessmentItemIdForTopic(Convert.ToInt32(dtLoList.Rows[index]["ID"]));
                    }
                    else
                    {
                        dtAssessmentItemID = assesmentDA.GetAssessmentItemIdForLearningObjective(Convert.ToInt32(dtLoList.Rows[index]["ID"]), assessmentType);
                    }
                    
                    if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX) && dtAssessmentItemID.Rows.Count < Convert.ToInt32(dtLoList.Rows[index]["MINIMUMNUMBERQUESTIONSTOASK"]))
                    {
                        dtLoList.Rows[index]["MINIMUMNUMBERQUESTIONSTOASK"] = dtAssessmentItemID.Rows.Count;
                    }
                    else if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE) && dtAssessmentItemID.Rows.Count < Convert.ToInt32(dtLoList.Rows[index]["NUMBERQUESTIONSTOASK"]))
                    {
                        dtLoList.Rows[index]["NUMBERQUESTIONSTOASK"] = dtAssessmentItemID.Rows.Count;
                        //dtLoList.Rows[index]["ACTUALMAXIMUMNUMBERQUESTIONSTOASK"] = dtAssessmentItemID.Rows.Count;
                    }
                    totalNumberOfQuestions = totalNumberOfQuestions + dtAssessmentItemID.Rows.Count;

                    if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX))
                    {
                        totalNumberOfQuestionsSumofMin = totalNumberOfQuestionsSumofMin + Convert.ToInt32(dtLoList.Rows[index]["MINIMUMNUMBERQUESTIONSTOASK"]);
                        if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_QUIZ)
                        {
                            if (dtLoList.Columns.Contains("OVERRIDEMAXQUIZQUESTIONSTOASKTF") && dtLoList.Columns.Contains("MAXQUIZQUESTIONSTOASK"))
                            {
                                if (dtLoList.Rows[index]["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] != DBNull.Value && Convert.ToBoolean(dtLoList.Rows[index]["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]) == true && dtLoList.Rows[index]["MAXQUIZQUESTIONSTOASK"] != DBNull.Value)
                                    dtLoList.Rows[index]["MAXIMUMNUMBERQUESTIONSTOASK"] = dtLoList.Rows[index]["MAXQUIZQUESTIONSTOASK"];
                            }
                        }
                    }
                    else if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE))
                    {
                        totalNumberOfQuestionsSumofMin = totalNumberOfQuestionsSumofMin + Convert.ToInt32(dtLoList.Rows[index]["NUMBERQUESTIONSTOASK"]);
                    }
                    dtLoList.AcceptChanges();
                    dsAssessmentItems.Tables.Add(dtAssessmentItemID);
                }

                if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE))
                {
                    /*
                    foreach (DataRow rowLO in dtLoList.Rows)
                    {
                        //get total questions
                        List<AssessmentItem> assessmentItemList = assesmentDA.GetAssessmentItemBySplit(GetAssessmentItemByDataTable(dsAssessmentItems.Tables[rowLO["ID"].ToString()]));
                        int ActualMaxnQuestionToAsk = Convert.ToInt32(rowLO["NUMBERQUESTIONSTOASK"]);
                        if (assessmentItemList.Count > ActualMaxnQuestionToAsk)
                            assessmentItemList.RemoveRange(0, assessmentItemList.Count - ActualMaxnQuestionToAsk);
                        completeAssessmentItemList.AddRange(assessmentItemList);
                    }*/
                    completeAssessmentItemList = GetFinalAssessmentItems(previouslyAskedQuestionsGUIDs, dtLoList, dsAssessmentItems, assesmentDA, "NUMBERQUESTIONSTOASK", randomizeQuestion);
                }
                else
                {
                    if (noOfQuestions > totalNumberOfQuestions)
                    {
                        noOfQuestions = totalNumberOfQuestions;
                    }

                    if (totalNumberOfQuestionsSumofMin > noOfQuestions)
                    {
                        /*
                        //foreach (DataRow rowLO in dtLoList.Rows)
                        for (int index = 0; index < dtLoList.Rows.Count; index++)
                        {
                            //get total questions
                            List<AssessmentItem> assessmentItemList = assesmentDA.GetAssessmentItemBySplit(GetAssessmentItemByDataTable(dsAssessmentItems.Tables[dtLoList.Rows[index]["ID"].ToString()]));
                            int ActualMaxNoQuestionToAsk = 0;
                            if (AdvanceQuestionSelectionMode.Equals(ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_MINMAX))
                            {
                                ActualMaxNoQuestionToAsk = Convert.ToInt32(dtLoList.Rows[index]["MINIMUMNUMBERQUESTIONSTOASK"]);
                            }
                            
                            if (assessmentItemList.Count > ActualMaxNoQuestionToAsk)
                                assessmentItemList.RemoveRange(0, assessmentItemList.Count - ActualMaxNoQuestionToAsk);
                            completeAssessmentItemList.AddRange(assessmentItemList);
                        }
                        */
                        //GetFinalAssessmentItems
                        completeAssessmentItemList = GetFinalAssessmentItems(previouslyAskedQuestionsGUIDs, dtLoList, dsAssessmentItems, assesmentDA, "MINIMUMNUMBERQUESTIONSTOASK", randomizeQuestion);

                    }
                    else
                    {
                        if (advanceQuestionSelectionMode.Equals(AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_MINMAX))
                        {
                            while (totalNumberOfQuestionsSumofMin < noOfQuestions)
                            {
                                int MaxReachedCount = 0;
                                //foreach (DataRow rowLO in dtLoList.Rows)
                                for (int index = 0; index < dtLoList.Rows.Count; index++)
                                {
                                    if (Convert.ToInt32(dtLoList.Rows[index]["ACTUALMAXIMUMNUMBERQUESTIONSTOASK"]) >= Convert.ToInt32(dtLoList.Rows[index]["MAXIMUMNUMBERQUESTIONSTOASK"]))
                                        MaxReachedCount++;
                                    else
                                    {
                                        dtLoList.Rows[index]["ACTUALMAXIMUMNUMBERQUESTIONSTOASK"] = Convert.ToInt32(dtLoList.Rows[index]["ACTUALMAXIMUMNUMBERQUESTIONSTOASK"]) + 1;
                                        totalNumberOfQuestionsSumofMin++;
                                        dtLoList.AcceptChanges();
                                    }

                                    if (totalNumberOfQuestionsSumofMin >= noOfQuestions)
                                        break;
                                }

                                if (MaxReachedCount >= dtLoList.Rows.Count)
                                    break;
                            }
                        }
                        //GetFinalAssessmentItems
                        completeAssessmentItemList = GetFinalAssessmentItems(previouslyAskedQuestionsGUIDs, dtLoList, dsAssessmentItems, assesmentDA, "ACTUALMAXIMUMNUMBERQUESTIONSTOASK", randomizeQuestion);

                    }

                }

                assesmentDA = null;

                //if (randomizeQuestion == true)
                //{
                //    this.RandomizeQuestions(ref completeAssessmentItemList);
                //}

                if (randomizeAnswer == true)
                {
                    this.RandomizeAnswers(ref completeAssessmentItemList);
                }


                return completeAssessmentItemList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public List<AssessmentItem> GetFinalAssessmentItems(List<string> previouslyAskedQuestionsGUIDs, DataTable dtLoList, DataSet dsAssessmentItems, AssesmentDA assesmentDA, string QuestingToAskSTR, bool randomizeQuestion)
        {
            List<AssessmentItem> completeAssessmentItemList = new List<AssessmentItem>();
            try
            {
                string previouslyAskedQuestionsGUID_STR = ConvertToCommaSeperatedStr(previouslyAskedQuestionsGUIDs);
                foreach (DataRow rowLO in dtLoList.Rows)
                {
                    int ActualMaxnQuestionToAsk = Convert.ToInt32(rowLO[QuestingToAskSTR]);
                    int LearningObjective_Id = Convert.ToInt32(rowLO["ID"].ToString());
                    string assessmentItemBinderName = rowLO["NAME"].ToString();
                    double scoreWeight = Convert.ToDouble(rowLO["SCOREWEIGHT"].ToString());
                    //get total questions
                    DataTable dtAssessmentItems = dsAssessmentItems.Tables[LearningObjective_Id.ToString()];
                    //DataRow[] dataRowExludedPrev = dtAssessmentItems.Select();
                    //DataRow[] dataRowExludedPrev = new DataRow[ActualMaxnQuestionToAsk];
                    for (int i = 0; i < dtAssessmentItems.Rows.Count; i++)
                    {
                        if (dtAssessmentItems.Rows.Count == ActualMaxnQuestionToAsk)
                            break;

                        DataRow dataRow = dtAssessmentItems.Rows[i];
                        if (previouslyAskedQuestionsGUIDs.Contains(dataRow["ASSESSMENTITEM_GUID"].ToString()))
                        {
                            dtAssessmentItems.Rows.RemoveAt(i--);
                        }
                    }
                    DataRow[] dataRowExludedPrev = dtAssessmentItems.Select();
                    /*
                    if (previouslyAskedQuestionsGUID_STR.Length > 0)
                    {
                        //Here we need remove the Previously Asked Questions:
                        dataRowExludedPrev = dtAssessmentItems.Select("ASSESSMENTITEM_GUID NOT IN(" + previouslyAskedQuestionsGUID_STR + ")");
                        int extraRowsToIncludeCount = ActualMaxnQuestionToAsk - dataRowExludedPrev.Length;
                        if (extraRowsToIncludeCount > 0)
                        {
                            DataRow[] dataRowReincluded = dtAssessmentItems.Select("SELECT TOP " + extraRowsToIncludeCount + " FROM " + dtAssessmentItems.TableName+ " WHERE ASSESSMENTITEM_GUID IN(" + previouslyAskedQuestionsGUID_STR + ")");
                            
                            
                            previouslyAskedQuestionsGUID_STR = ExcludeCommaSeperatedStr(previouslyAskedQuestionsGUID_STR, extraRowsToIncludeCount);
                            if (previouslyAskedQuestionsGUID_STR != null && previouslyAskedQuestionsGUID_STR.Length > 0)
                            {
                                dataRowExludedPrev = dtAssessmentItems.Select("ASSESSMENTITEM_GUID NOT IN(" + previouslyAskedQuestionsGUID_STR + ")");
                            }
                            else
                            {
                                dataRowExludedPrev = dtAssessmentItems.Select();
                            }


                            //DataRow[] dataRowInludedPrev = dtAssessmentItems.Select("ASSESSMENTITEM_GUID IN(" + previouslyAskedQuestionsGUID_STR + ")");
                            //for (int index = 0; index < extraRowsToIncludeCount; index++)
                            //{
                            //    previouslyAskedQuestionsGUID_STR = previouslyAskedQuestionsGUID_STR.Replace("'" + dataRowInludedPrev[index]["ASSESSMENTITEM_GUID"].ToString() + "',", "");
                            //}
                            //dataRowExludedPrev = dtAssessmentItems.Select("ASSESSMENTITEM_GUID NOT IN(" + previouslyAskedQuestionsGUID_STR + ")");
                            ////dtAssessmentItems.Rows.Clear();

                            //foreach(DataRow dataRow in dataRowExludedPrev)
                            //{
                            //    dtAssessmentItems.Rows.Add(dataRow);
                            //}
                            //dtAssessmentItems.AcceptChanges();
                        }
                    
                    }
                    */
                    List<AssessmentItem> assessmentItemList = assesmentDA.GetAssessmentItemBySplit(GetAssessmentItemByDataRowArray(dataRowExludedPrev, true), LearningObjective_Id, scoreWeight, assessmentItemBinderName);
                    if (randomizeQuestion)
                        RandomizeQuestions(ref assessmentItemList);
                    //Logic here

                    for(int i=0; i<assessmentItemList.Count; i++)
                    {
                        AssessmentItem ai = assessmentItemList[i];
                        if (completeAssessmentItemList.Contains(ai))
                        {
                            assessmentItemList.RemoveAt(i--);
                        }
                    }


                    if (randomizeQuestion)
                    {
                        if (assessmentItemList.Count > ActualMaxnQuestionToAsk)
                            assessmentItemList.RemoveRange(0, assessmentItemList.Count - ActualMaxnQuestionToAsk);
                    }
                    else
                    {
                        List<AssessmentItem> tempAssessmentItemList = new List<AssessmentItem>();
                        for (int i = 0; i < ActualMaxnQuestionToAsk; i++)
                        {
                            tempAssessmentItemList.Add(assessmentItemList[i]);
                        }
                        assessmentItemList.Clear();
                        assessmentItemList = tempAssessmentItemList;
                    }


                    completeAssessmentItemList.AddRange(assessmentItemList);//container
                }

                return completeAssessmentItemList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public string ExcludeCommaSeperatedStr(string strList, int count)
        {
            StringBuilder CommaSeperatedStr = new StringBuilder("");

            try
            {
                string[] strListArr = strList.Split(new char[] { ',' });

                count = strListArr.Length - count;

                if (strListArr != null)
                {
                    for (int index = 0; index < count; index++)
                    {
                        if (index == count - 1)
                        {
                            CommaSeperatedStr.Append(strListArr[index]);
                        }
                        else
                        {
                            CommaSeperatedStr.Append(strListArr[index]);
                            CommaSeperatedStr.Append(",");
                        }
                    }
                }

                return CommaSeperatedStr.ToString();
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string ConvertToCommaSeperatedStr(List<string> strList)
        {
            StringBuilder CommaSeperatedStr = new StringBuilder("");

            try
            {
                if (strList != null)
                {
                    for (int index = 0; index < strList.Count; index++)
                    {
                        if (index == strList.Count - 1)
                        {
                            CommaSeperatedStr.Append("'");
                            CommaSeperatedStr.Append(strList[index]);
                            CommaSeperatedStr.Append("'");
                        }
                        else
                        {
                            CommaSeperatedStr.Append("'");
                            CommaSeperatedStr.Append(strList[index]);
                            CommaSeperatedStr.Append("'");
                            CommaSeperatedStr.Append(",");
                        }

                    }
                }

                return CommaSeperatedStr.ToString();
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }


        public string GetAssessmentItemByDataRowArray(DataRow[] dataRow, bool distinctFilter)
        {
            try
            {
                string assessmentItemIds = string.Empty;

                for (int i = 0; i < dataRow.Length; i++)
                {
                    DataRow drassessmentItemId = dataRow[i];
                    if (distinctFilter == true)
                    {
                        if (assessmentItemIds.IndexOf(drassessmentItemId["ASSESSMENTITEM_ID"].ToString() + ",") > 0)
                        {
                            continue;
                        }
                        else if (assessmentItemIds.IndexOf("," + drassessmentItemId["ASSESSMENTITEM_ID"].ToString()) > 0)
                        {
                            continue;
                        }
                        else if (assessmentItemIds.EndsWith(drassessmentItemId["ASSESSMENTITEM_ID"].ToString()) == true)
                        {
                            continue;
                        }
                    }

                    if (dataRow.Length - 1 == i)
                        assessmentItemIds = assessmentItemIds + drassessmentItemId["ASSESSMENTITEM_ID"];
                    else
                        assessmentItemIds = assessmentItemIds + drassessmentItemId["ASSESSMENTITEM_ID"] + ",";
                }

                return assessmentItemIds;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }

        public string GetAssessmentItemByDataTable(DataTable dtAssessmentItemID)
        {
            try
            {
                string assessmentItemIds = string.Empty;

                for (int i = 0; i < dtAssessmentItemID.Rows.Count; i++)
                {
                    DataRow drassessmentItemId = dtAssessmentItemID.Rows[i];
                    if (dtAssessmentItemID.Rows.Count - 1 == i)
                        assessmentItemIds = assessmentItemIds + drassessmentItemId["ASSESSMENTITEM_ID"];
                    else
                        assessmentItemIds = assessmentItemIds + drassessmentItemId["ASSESSMENTITEM_ID"] + ",";
                }

                return assessmentItemIds;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }


        // LCMS-9213
        public DataTable GetAssessmentAnswerItemIDsByGuid(string answerGuids)
        {
            try
            {
                return new AssesmentDA().GetAssessmentAnswerItemIDsByGuid(answerGuids);
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the list of assessment Item result
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <param name="assessmentType">int assessmentType</param>
        /// <returns>list of course approvals objects</returns>
        public List<AssessmentItemResult> GetLearnerAssessmentItemResults(int enrollmentID, string assessmentType)
        {
            using (AssesmentDA assessmentDA = new AssesmentDA())
            {
                return assessmentDA.GetLearnerAssessmentItemResults(enrollmentID, assessmentType);
            }
        }

    }
}
