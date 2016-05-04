using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ICP4.BusinessLogic.AssessmentManager;
using ICP4.BusinessLogic.CourseManager;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Net;
using ICP4.BusinessLogic.ICPCourseService;
using ICP4.BusinessLogic.ICPTrackingService;
using ICP4.BusinessLogic.IntegerationManager;
//using _360Training.BusinessEntities;

namespace ICP4.CoursePlayer
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Trace.WriteLine("EndSessionOnServer : " + Session["LearnerSessionID"].ToString());
                //System.Diagnostics.Trace.Flush();

                if (Session["isAbondoned"] == null)
                {
                    //System.Diagnostics.Trace.WriteLine("isAbondoned : null");
                    //System.Diagnostics.Trace.Flush();

                    using (CourseManager courseManager = new CourseManager())
                    {
                        int courseID = Convert.ToInt32(Session["CourseID"]);
                        //System.Diagnostics.Trace.WriteLine("CourseID : " + courseID.ToString());
                        //System.Diagnostics.Trace.Flush();
                        if (courseID > 0)
                        {
                            string learnerSessionID = Session["LearnerSessionID"].ToString();
                            EndSessionOnServer(courseID, learnerSessionID, DateTime.Now);
                        }
                    }
                }
                else
                {
                    //System.Diagnostics.Trace.WriteLine("isAbondoned : " + Session["isAbondoned"].ToString());
                    //System.Diagnostics.Trace.Flush();
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                
            }
            
        }
        private void EndSessionOnServer(int courseID, string learnerSessionID, DateTime endTime)
        {
            bool TimeloggedOnServer = false;
            try
            {
                //System.Diagnostics.Trace.WriteLine("EndSessionOnServer : " + learnerSessionID);
                //System.Diagnostics.Trace.Flush();
                double percentageCourseProgress = 0.0;
                if (Convert.ToBoolean(Session["isPreview"]) == false
                    && Convert.ToBoolean(Session["IsDemoable"]) == false)
                {
                    string itemType;
                    string itemGUID = Session["ItemGUID"].ToString();
                    string sceneGUID = Session["SceneGUID"].ToString();
                    int seqNo = Convert.ToInt32(Session["CurrentIndex"]);
                    int learnerSessionIDPrimary = Convert.ToInt32(Session["LearnerSessionIDPrimary"]);
                    string learnerSessioGUID = Convert.ToString(Session["LearnerSessionID"]);
                    int enrollmentID = Convert.ToInt32(Session["EnrollmentID"]);
                    int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                    //DateTime assetStartTime = Convert.ToDateTime(Session["AssetStartTime"]);
                    //Getting the locked status from Server
                    string lockingReason = LockingReason.GeneralCase;
                    bool courseLocked = IsCourseLocked(enrollmentID, out lockingReason);
                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration = null;
                    using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = -1;
                        if (Session["Source"] != null)
                        {
                            source = Convert.ToInt32(Session["Source"]);
                        }
                        else
                        {
                            ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                            source = trackingService.GetSource(learnerSessioGUID);
                        }
                        
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCacheOnApplicationLevel(courseConfigurationID);
                    }

                    if (!courseLocked)
                    {
                        using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                        {
                            int source = -1;
                            if (Session["Source"] != null)
                            {
                                source = Convert.ToInt32(Session["Source"]);
                            }
                            else
                            {
                                ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                                source = trackingService.GetSource(learnerSessioGUID);
                            }
                            itemType = cacheManager.GetSequenceItemTypeFromApplicationLevel(courseID, seqNo, source, courseConfigurationID);
                            SequenceItem sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, seqNo, source, courseConfigurationID);

                            if (itemType == SequenceItemTypeName.Exam)
                            {
                                itemType = sequenceItem.ExamType;
                            }
                        }



                        ////////Incase of any Assessment////////////////////
                        if (itemType == SequenceItemTypeName.PreAssessment ||
                            itemType == SequenceItemTypeName.PostAssessment ||
                            itemType == SequenceItemTypeName.Quiz
                            || itemType == SequenceItemTypeName.PracticeExam
                            )
                        {


                            string assessmentStage = Convert.ToString(Session["AssessmentStage"]);
                            if (assessmentStage == "AssessmentIsInProgress")
                            {
                                int masteryScore = 0;
                                switch (itemType)
                                {
                                    case SequenceItemTypeName.PreAssessment:
                                        masteryScore = courseConfiguration.PreAssessmentConfiguration.MasteryScore;
                                        break;
                                    case SequenceItemTypeName.PostAssessment:
                                        masteryScore = courseConfiguration.PostAssessmentConfiguration.MasteryScore;
                                        break;
                                    case SequenceItemTypeName.Quiz:
                                        masteryScore = courseConfiguration.QuizConfiguration.MasteryScore;
                                        break;
                                    case SequenceItemTypeName.PracticeExam:
                                        AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();
                                        assessmentConfiguration = (AssessmentConfiguration)Session["PracticeExamAssessmentConfiguration"];
                                        masteryScore = assessmentConfiguration.MasteryScore;
                                        break;

                                }
                                EndAssessmentForcefully(masteryScore);

                            }
                            else
                            {

                                if (itemType != null && itemType != string.Empty)
                                {
                                    DateTime assetStartTime = Convert.ToDateTime(Session["AssetStartTime"]);
                                    SaveCourseStudentTrack(itemGUID, sceneGUID, learnerSessionIDPrimary, itemType, assetStartTime);
                                }
                            }
                        }
                        else ////////Incase of anything other then assessment i.e any scene
                        {
                            using (CourseManager courseManager = new CourseManager())
                            {
                                DateTime assetStartTime = Convert.ToDateTime(Session["AssetStartTime"]);
                                SaveCourseStudentTrack(itemGUID, sceneGUID, learnerSessionIDPrimary, itemType, assetStartTime);
                            }
                        }
                        // in case of course evaluation we need to save evaluation stats as well 
                        if (itemType == SequenceItemTypeName.CourseEvaluation)
                        {
                            SaveCourseEvaluationStatsIfCompleted();
                        }

                    }

                    using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                    {
                        DateTime courseStartTime = Convert.ToDateTime(Session["CourseStartTime"]);
                        int learnerID = Convert.ToInt32(Session["LearnerID"]);


                        int totalTimeSpent = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, courseStartTime, endTime, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                        totalTimeSpent = totalTimeSpent - (Session.Timeout * 60);
                        if (totalTimeSpent < 0)
                            totalTimeSpent = 0;
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                        int visitedSceneCount;
                        int totalSceneCount;                        
                        percentageCourseProgress = CalculateCourseProgressPercentageFromSequence(courseID, seqNo, Convert.ToInt32(Session["TotalNoOfViewableScenes"]), out visitedSceneCount, out totalSceneCount);

                        // LCMS-9070 START 
                        int totalTimeSpentinDay = 0;
                        int days = Math.Abs(endTime.DayOfYear - courseStartTime.DayOfYear);
                        if (days > 0)
                        {
                            DateTime courseEndTimeinDay = new DateTime(courseStartTime.Year, courseStartTime.Month, courseStartTime.Day, 23, 59, 59);
                            totalTimeSpentinDay = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, courseStartTime, courseEndTimeinDay, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                            trackingService.EndSession(learnerSessionID, courseEndTimeinDay, totalTimeSpentinDay, percentageCourseProgress, enrollmentID);
                        }

                        if (totalTimeSpentinDay > 0)
                            totalTimeSpent = totalTimeSpent - totalTimeSpentinDay;
                        // LCMS-9070 END

                        trackingService.EndSession(learnerSessionID, endTime, totalTimeSpent, percentageCourseProgress, enrollmentID);
                        TimeloggedOnServer = true;
                        int source = trackingService.GetSource(learnerSessioGUID);

                        ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[] learnerStatistics = null;

                        double postAssessmentScore = -1;
                        bool isPostAssessmentPassed = false;

                        if (courseConfiguration.PostAssessmentConfiguration.ScoreType.ToUpper().Equals(ICP4.BusinessLogic.AssessmentManager.ScoreType.NoResults.ToUpper()))
                        {
                            postAssessmentScore = -1;
                            isPostAssessmentPassed = false;
                        }
                        else
                        {
                            learnerStatistics = trackingService.GetPostAssessmentResult(learnerSessionID);
                            if (learnerStatistics.Length > 0)
                            {
                                postAssessmentScore = learnerStatistics[learnerStatistics.Length - 1].RawScore;
                                isPostAssessmentPassed = learnerStatistics[learnerStatistics.Length - 1].IsPass;
                            }
                        }

                        //int QuizCount = courseManager.GetValidQuizCount(courseID);
                        Integeration integeration = IntegerationFactory.GetObject(source);
                        IntegerationStatistics integerationStatistics = new IntegerationStatistics();

                        if (source == 1)
                        {
                            using (ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                            {
                                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                                string courseGUID = string.Empty;
                                courseGUID = courseService.GetCourseGUID(courseID);
                                //Getting the total Course time spent by the learner
                                int totalPreviouslyTimeSpent =  trackingService.GetLearnerTimeSpent(enrollmentID, learnerSessionID);
                                int courseTimeSpent = totalPreviouslyTimeSpent;//totalTimeSpent


                                integerationStatistics.PercentageCourseProgress = percentageCourseProgress;
                                integerationStatistics.IsCourseLocked = courseLocked;
                                integerationStatistics.LockReason = lockingReason;
                                integerationStatistics.LearningSessionGuid = learnerSessioGUID;
                                integerationStatistics.IsAssessmentPassed = isPostAssessmentPassed;
                                integerationStatistics.AssessmentScore = postAssessmentScore;
                                integerationStatistics.Enrollment_Id = enrollmentID;
                                integerationStatistics.CourseTimeSpent = courseTimeSpent;
                                integerationStatistics.CourseGuid = courseGUID;
                                integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.CourseCompletion;
                            }
                        }
                        else
                        {
                            integerationStatistics = (IntegerationStatistics)Session["IntegerationStatistics"];
                            //Integeration integeration = IntegerationFactory.GetObject(source);
                            Session["IntegerationStatistics"] = null;
                            if (integerationStatistics != null && integerationStatistics.IntegerationStatisticsType == IntegerationStatisticsType.AssessmentCompletion)
                            {
                                integeration.SynchStatsToExternalSystem(integerationStatistics);
                            }

                            if (integerationStatistics == null)
                                integerationStatistics = new IntegerationStatistics();
                            integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.ProgressCompletion;
                            integerationStatistics.CourseTimeSpent = totalTimeSpent;
                            integerationStatistics.PercentageCourseProgress = percentageCourseProgress;
                            integerationStatistics.LearningSessionGuid = learnerSessionID;
                        }
                        #region Course Completion
                        ICP4.BusinessLogic.ICPTrackingService.LearnerCourseCompletionStatus learnerCourseCompletionStatus = (ICP4.BusinessLogic.ICPTrackingService.LearnerCourseCompletionStatus)Session["LearnerCourseCompletionStatus"];

                        //if (learnerCourseCompletionStatus == null)
                        //{
                        DateTime registrationDate = DateTime.Now;
                        if (Session["CourseRegAccessDateTime"] != null)
                            DateTime.TryParse(Session["CourseRegAccessDateTime"].ToString(), out registrationDate);
                        CourseManager courseManager = new CourseManager();
                        int QuizCount = GetValidQuizCount(courseID);

                        int courseApprovalID = 0;
                        if (System.Web.HttpContext.Current.Session["CourseApprovalID"] != null)
                        {
                            courseApprovalID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseApprovalID"]);
                        }

                        learnerCourseCompletionStatus = trackingService.GetCourseCompletionStatus(courseID, courseManager.CourseCompletionPolicyEntity2BizTranslator(courseConfiguration), learnerID, enrollmentID, QuizCount, registrationDate, percentageCourseProgress, courseApprovalID,source);                       

                        //}
                        #endregion

                        integerationStatistics.IsCourseCompleted = learnerCourseCompletionStatus.IsCourseCompleted;
                        integeration.SynchStatsToExternalSystem(integerationStatistics);

                    }

                }
            }
            catch(Exception exp)
            {
                if (TimeloggedOnServer == false)
                {
                    using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                    {
                        DateTime courseStartTime = Convert.ToDateTime(Session["CourseStartTime"]);
                        int learnerID = Convert.ToInt32(Session["LearnerID"]);

                        int totalTimeSpent = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, courseStartTime, endTime, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                        totalTimeSpent = totalTimeSpent - (Session.Timeout * 60);
                        if (totalTimeSpent < 0)
                            totalTimeSpent = 0;
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                        int visitedSceneCount;
                        int totalSceneCount;
                        CourseManager courseManager = new CourseManager();
                        double percentageCourseProgress = 0.0;
                        int seqNo = Convert.ToInt32(Session["CurrentIndex"]);
                        int enrollmentID = Convert.ToInt32(Session["EnrollmentID"]);
                        percentageCourseProgress = CalculateCourseProgressPercentageFromSequence(courseID, seqNo, Convert.ToInt32(Session["TotalNoOfViewableScenes"]), out visitedSceneCount, out totalSceneCount);

                        // LCMS-9070 START 
                        int totalTimeSpentinDay = 0;
                        int days = Math.Abs(endTime.DayOfYear - courseStartTime.DayOfYear);
                        if (days > 0)
                        {
                            DateTime courseEndTimeinDay = new DateTime(courseStartTime.Year, courseStartTime.Month, courseStartTime.Day, 23, 59, 59);
                            totalTimeSpentinDay = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, courseStartTime, courseEndTimeinDay, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                            trackingService.EndSession(learnerSessionID, courseEndTimeinDay, totalTimeSpentinDay, percentageCourseProgress, enrollmentID);
                        }

                        if (totalTimeSpentinDay > 0)
                            totalTimeSpent = totalTimeSpent - totalTimeSpentinDay;
                        // LCMS-9070 END

                        trackingService.EndSession(learnerSessionID, endTime, totalTimeSpent, percentageCourseProgress, enrollmentID);
                    }
                }
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + exp.Message, exp.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
            }
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
            selectedQuestions = (SelectedQuestion)Session["SelectedQuestionSequence"];

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

                    learningObjectives.Add(questionInfo.AssessmentBinderID.ToString(), new Object[] { 0, 0, questionInfo.ScoreWeight, questionInfo.AssessmentBinderName });
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
            double totalWeightage = 0.0;
            
            try
            {
                totalWeightage = (double)dt.Compute("SUM(LOWeightage)", "");
            }
            catch (Exception exp)
            {
                totalWeightage = 0.0;
            }

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
                        currentLOWeightage = Math.Round(currentLOWeightage, 2);
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

                //System.Diagnostics.Trace.WriteLine("GetWeightedScore : currentLOWeightage=" + currentLOWeightage + " userScorePercentageInCurrentLO=" + userScorePercentageInCurrentLO + " WeightedScore=" + dt.Rows[i]["WeightedScore"] + " LOID=" + dt.Rows[i]["LOID"]);
                //System.Diagnostics.Trace.Flush();

            }
            dt.AcceptChanges();
        }

        private bool IsCourseLocked(int enrollmentID, out string lockingReason)
        {
            bool isPreview = Convert.ToBoolean(Session["isPreview"]);
            if (isPreview)//as course is never locked in preview mode
            {
                lockingReason = string.Empty;
                return false;
            }
            ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
            trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

            return trackingService.IsCourseLocked(enrollmentID, out  lockingReason);
        }
        private void SaveCourseStudentTrack_(string itemGUID, string sceneGUID, int ID, string statisticType)
        {
            bool isPreview = Convert.ToBoolean(Session["isPreview"]);
            if (isPreview)//as we do not want tracking in preview mode
            {
                return;
            }

            using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
            {
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();

                learnerStatistics.Item_GUID = itemGUID;
                learnerStatistics.Scene_GUID = sceneGUID;
                learnerStatistics.LearningSession_ID = ID;
                learnerStatistics.Statistic_Type = statisticType;

                //Now calculate time difference
                DateTime assetStartTime = Convert.ToDateTime(Session["AssetStartTime"]);
                learnerStatistics.TimeInSeconds = Convert.ToInt32(DateTime.Now.Subtract(assetStartTime).TotalSeconds) - (Session.Timeout*60);
                if (learnerStatistics.TimeInSeconds < 0)
                  learnerStatistics.TimeInSeconds = 0;
                //LCMS-7169
                //START
                /*
                if (Session["LearningSessionTimeSpentOnScene"] != null)
                {
                  learnerStatistics.TimeInSeconds = learnerStatistics.TimeInSeconds + int.Parse(Session["LearningSessionTimeSpentOnScene"].ToString());
                }
                */ 
                //END
                //System.Diagnostics.Trace.WriteLine("TimeInSeconds: " + Session.Timeout);
                //System.Diagnostics.Trace.Flush();
                trackingService.SaveLearnerStatistics(learnerStatistics);
                return;
            }


        }

        public bool SaveCourseStudentTrack(string itemGUID, string sceneGUID, int ID, string statisticType, DateTime assetStartTime)
        {

            try
            {
                //Now calculate time difference

                /*LCMS-8972 - Start*/
                int timeInSeconds = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assetStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                /*LCMS-8972 - End*/

                if (statisticType == _360Training.BusinessEntities.SequenceItemType.PreAssessment || statisticType == _360Training.BusinessEntities.SequenceItemType.Quiz || statisticType == _360Training.BusinessEntities.SequenceItemType.PostAssessment || statisticType == _360Training.BusinessEntities.SequenceItemType.PracticeExam || statisticType == _360Training.BusinessEntities.SequenceItemType.Exam)
                {

                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration = null;
                    ICP4.BusinessLogic.ICPCourseService.SequenceItem sequenceItem = null;
                    string itemType = "";
                    int seqNo = Convert.ToInt32(Session["CurrentIndex"]);
                    int courseID = Convert.ToInt32(Session["CourseID"]);
                    using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, seqNo, source, courseConfigurationID);
                        itemType = cacheManager.GetSequenceItemType(courseID, seqNo, source, courseConfigurationID);
                    }

                    if (itemType == SequenceItemTypeName.Exam)
                    {
                        itemType = sequenceItem.ExamType;
                    }

                    statisticType = itemType;
                    sceneGUID = sequenceItem.Item_GUID;

                }
                else if (statisticType != LearnerStatisticsType.ContentAsset && statisticType != LearnerStatisticsType.KnowledgeCheck)
                {
                    sceneGUID = statisticType;
                }

                /*
                 *  Waqas Zakai
                 *  LCMS-10974
                 *  START
                 *  Subtracted the SessionTimeOut values into Scene's time.
                 */
                timeInSeconds = Convert.ToInt32(DateTime.Now.Subtract(assetStartTime).TotalSeconds) - (Session.Timeout * 60);
                if (timeInSeconds < 0)
                    timeInSeconds  = 0;
                /*
                 *  LCMS-10974
                 *  END
                 */

                //Get visitedSceneList from session and update time for the scene
                List<_360Training.BusinessEntities.VisitedScene> visitedSceneList = (List<_360Training.BusinessEntities.VisitedScene>)Session["VisitedSceneList"];
                for (int index = 0; index < visitedSceneList.Count; index++)
                {
                    _360Training.BusinessEntities.VisitedScene scene = visitedSceneList[index];
                    if (scene.SceneGUID == sceneGUID)
                    {
                        visitedSceneList.RemoveAt(index);
                        /*LCMS-8972 - Start*/
                        scene.TimeSpent += timeInSeconds;
                        /*LCMS-8972 - End*/
                        visitedSceneList.Add(scene);
                        break;
                    }


                }
                Session["VisitedSceneList"] = visitedSceneList;

                //if (statisticType == SequenceItemType.PreAssessment || statisticType == SequenceItemType.Quiz || statisticType == SequenceItemType.PostAssessment || statisticType == SequenceItemType.PracticeExam || statisticType == SequenceItemType.Exam)
                //{
                //    return true;
                //}

                bool isPreview = Convert.ToBoolean(Session["isPreview"]);
                if (isPreview)//as we do not want tracking in preview mode
                {
                    return true;
                }

                using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();

                    //LCMS-5883: no need for Item_Guid because we are not saving ContentObject Guid now in every Scene
                    //learnerStatistics.Item_GUID = itemGUID;
                    learnerStatistics.Scene_GUID = sceneGUID;
                    learnerStatistics.LearningSession_ID = ID;
                    learnerStatistics.Statistic_Type = statisticType;
                    learnerStatistics.LearnerEnrollment_ID = long.Parse(Session["EnrollmentID"].ToString());
                    //LCMS-5883: no need to send Time
                    /*LCMS-8972 -Start*/
                    learnerStatistics.TimeInSeconds = timeInSeconds;
                    /*LCMS-8972 -End*/

                    return trackingService.SaveLearnerStatistics_Scene(learnerStatistics);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }



        public bool EndAssessmentForcefully(int masteryScore)
        {
            int noOfCorrect = 0;
            int noOfIncorrect = 0;
            bool isAssessmentPass = false;
            bool isSuccessfullySaved = false;
            SelectedQuestion selectedQuestions = null;

            selectedQuestions = (SelectedQuestion)Session["SelectedQuestionSequence"];

            MarkUnAnsweredQuestionsIncorrect(selectedQuestions);
            DataTable dt = null;
            double score = -1;



            int courseID = Convert.ToInt32(Session["CourseID"]);
            int courseSequenceIndex = Convert.ToInt32(Session["CurrentIndex"]);
            CourseConfiguration courseConfiguration = null;
            AssessmentConfiguration assessmentConfiguration = null;
            SequenceItem sequenceItem = null;
            string itemType = null;
            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
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


            if (itemType == SequenceItemTypeName.PreAssessment)
            { assessmentConfiguration = courseConfiguration.PreAssessmentConfiguration; }

            else if (itemType == SequenceItemTypeName.Quiz)
            { assessmentConfiguration = courseConfiguration.QuizConfiguration; }

            else if (itemType == SequenceItemTypeName.PostAssessment)
            { assessmentConfiguration = courseConfiguration.PostAssessmentConfiguration; }

            else if (itemType == SequenceItemTypeName.PracticeExam)
            { assessmentConfiguration = (AssessmentConfiguration)Session["PracticeExamAssessmentConfiguration"]; }

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
                //score = (noOfCorrect / selectedQuestions.QuestionInfos.Count) * 100;
                if (score >= masteryScore)
                    isAssessmentPass = true;
                else
                    isAssessmentPass = false;
            }

            int currAttemptNo = 1;
            int.TryParse(Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);

            if (assessmentConfiguration.AllowPauseResumeAssessment && (assessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE ||
                                                                                                   assessmentConfiguration.AdvanceQuestionSelectionType == ICP4.BusinessLogic.AssessmentManager.ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS))
            {
                if (Convert.ToBoolean(Session["AssessmentAllQuestionsAttempted"]) == false && Convert.ToBoolean(Session["AssessmentAllQuestionsDisplayed"]) == false)
                {
                    score = -2;
                    isAssessmentPass = false;
                }
            }
            long learnerStatistic_Id = SaveAssessmentEndTrackingInfo(selectedQuestions, noOfCorrect, noOfIncorrect, currAttemptNo, score, isAssessmentPass, masteryScore);


            // LCMS-9213
            //------------------------------------------------------------------------------------------
            //isSuccessfullySaved = SaveQuestionTrackingInfo(selectedQuestions, learnerStatistic_Id);
            if (!assessmentConfiguration.AllowPauseResumeAssessment)
            {
                isSuccessfullySaved = SaveQuestionTrackingInfo(selectedQuestions, learnerStatistic_Id);
            }
            //------------------------------------------------------------------------------------------

            return isSuccessfullySaved;
        }

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

        private bool CalculateAssessmentResult(int masteryScore, ref int numberOfCorrect, ref int numberOfInCorrect)
        {
            ICP4.BusinessLogic.AssessmentManager.SelectedQuestion selectedQuestions = null;
            selectedQuestions = (ICP4.BusinessLogic.AssessmentManager.SelectedQuestion)Session["SelectedQuestionSequence"];

            foreach (ICP4.BusinessLogic.AssessmentManager.QuestionInfo questionInfo in selectedQuestions.QuestionInfos)
            {
                if (questionInfo.IsCorrectlyAnswered)
                    numberOfCorrect++;
            }
            numberOfInCorrect = selectedQuestions.QuestionInfos.Count - numberOfCorrect;
            if ((numberOfCorrect / Convert.ToDecimal(selectedQuestions.QuestionInfos.Count)) * 100 >= masteryScore)
                return true;
            else
                return false;

        }
        public bool SaveQuestionTrackingInfo(SelectedQuestion selectedQuestion, long learnerStatistic_Id)
        {
            bool isPreview = Convert.ToBoolean(Session["isPreview"]);
            if (isPreview)//as we do not want tracking in preview mode
            {
                return true; 
            }
            try
            {
                bool isSaved = false;
                TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                LearnerStatistics learnerStatistics = null;

                foreach (QuestionInfo questionInfo in selectedQuestion.QuestionInfos)
                {
                    learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                    learnerStatistics.AssessmentItemID = questionInfo.QuestionGuid;
                    learnerStatistics.AssessmentType = selectedQuestion.AssessmentType;
                    learnerStatistics.CorrectTF = questionInfo.IsCorrectlyAnswered;
                    learnerStatistics.Item_GUID = questionInfo.QuestionGuid;
                    learnerStatistics.LearningSession_ID = Convert.ToInt32(Session["LearnerSessionIDPrimary"]);
                    learnerStatistics.Statistic_Type = ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.Question;
                    learnerStatistics.RemediationCount = Convert.ToInt32(Session["AssessmentRemediatonCount"]);

                    int arrayLength = questionInfo.AnswerIDs.Count < questionInfo.AnswerTexts.Count ? questionInfo.AnswerTexts.Count : questionInfo.AnswerIDs.Count;

                    learnerStatistics.LearnerStatisticsAnswers = new LearnerStatisticsAnswer[arrayLength];

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
                        learnerStatistics.LearnerStatisticsAnswers[studentAnswerTextIndex].Value += questionInfo.AnswerTexts[studentAnswerTextIndex] + ",";
                    }
                    learnerStatistics.CorrectAnswerGuids = questionInfo.CorrectAssessmentItemGuids;
                    learnerStatistics.LearnerStatisticsID = learnerStatistic_Id;

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

        public string GetAssessmentAnswerItemGUID(int assessmentItemID, int studentAnswerID)
        {
            ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[] assessmentItems = (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[])Session["AssessmentItemList"];
            string asssessmentAnswerItemGUID = string.Empty;
            foreach (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem assessmentItem in assessmentItems)
            {
                if (assessmentItemID == assessmentItem.AssessmentItemID)
                {
                    foreach (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItemAnswer assessmentItemAnswer in assessmentItem.AssessmentAnswers)
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

        public long SaveAssessmentEndTrackingInfo(SelectedQuestion selectedQuestion, int noOfAnswersCorrect, int noOfAnswersInCorrect, int currentAttemptNo, double weightedScore, bool isCurrentAssessmentPassed, int masteryScore)
        {
            bool isPreview = Convert.ToBoolean(Session["isPreview"]);
            //currentAttemptNo = 1;
            if (isPreview)
            {
                LearnerStatistics[] learnerStatisticsPreview = null;
                learnerStatisticsPreview = (LearnerStatistics[])Session["AssessmentEndStats"];
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
                string learenrSessionID = Session["LearnerSessionID"].ToString();
                int courseID = Convert.ToInt32(Session["CourseID"]);

                //int attemptNo = 0;
                DateTime assessmentStartTime = Convert.ToDateTime(Session["AssessmentStartTime"]);

                TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService();
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                LearnerStatistics learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                
                CourseConfiguration courseConfiguration = null;
                
                using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                    courseConfiguration = cacheManager.GetIFConfigurationExistInCacheOnApplicationLevel(courseConfigurationID);
                }

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
                learnerStatistics.AssessmentAttemptNumber = currentAttemptNo;
                //currentAttemptNo = attemptNo;// learnerStatistics.AssessmentAttemptNumber;
                learnerStatistics.AssessmentType = selectedQuestion.AssessmentType;
                learnerStatistics.LearningSession_ID = Convert.ToInt32(Session["LearnerSessionIDPrimary"]);
                learnerStatistics.NumberAnswersCorrect = noOfAnswersCorrect;
                learnerStatistics.NumberAnswersIncorrect = noOfAnswersInCorrect;
                learnerStatistics.RawScore = weightedScore;
                learnerStatistics.MaxAtemptActionTaken = false;
                learnerStatistics.IsPass = isCurrentAssessmentPassed;
                learnerStatistics.RemediationCount = Convert.ToInt32(Session["AssessmentRemediatonCount"]);
                learnerStatistics.TimeInSeconds = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assessmentStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));
                learnerStatistics.LearnerEnrollment_ID = Convert.ToInt32(Session["EnrollmentID"]);
                //LCMS-10266
                if (selectedQuestion.AssessmentType == LearnerStatisticsType.PostAssessment
                    && (courseConfiguration.PostAssessmentConfiguration.AllowPauseResumeAssessment || System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString() == "True")
                    && (courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE || courseConfiguration.PostAssessmentConfiguration.AdvanceQuestionSelectionType == ExamPrepConstants.ADVANCEQUESTIONSELECTIONTYPE_RANDOMALTERNATE_MULTIPLEITEMBANKS)
                    )
                {
                    //// As in pause resume case it never considers pausing on question 1 to be pause/resume scenario and never logs its time
                    //if (Convert.ToInt32(Session["SelectedQuestionSequenceNo"]) <= 1 && Session["OriginalAttemptNumber"]!=null)
                    //{
                    //    learnerStatistics.AssessmentAttemptNumber = Convert.ToInt32(Session["OriginalAttemptNumber"]);
                    //}
                        learnerStatistics.IsRepeatedAssessmentAttempt = (Session["RandomAlternateWithPauseResume"] != null ? true : false);
                }
                //End LCMS-10266/////////
                if (selectedQuestion.AssessmentType == ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.Quiz || selectedQuestion.AssessmentType == ICP4.BusinessLogic.CourseManager.LearnerStatisticsType.PracticeExam)
                {
                    learnerStatistics.Statistic_Type = selectedQuestion.AssessmentType;// CourseManager.LearnerStatisticsType.QuizEnd;

                    int courseSequenceIndex = Convert.ToInt32(Session["CurrentIndex"]);
                    SequenceItem sequenceItem = null;
                    using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                        sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                        learnerStatistics.Item_GUID = sequenceItem.Item_GUID;
                    }
                }
                //isSaved = trackingService.SaveLearnerStatistics(learnerStatistics);
                long learnerStatistics_ID = trackingService.SaveAssessmentScore(learnerStatistics, masteryScore);

                AssessmentConfiguration currentAssessmentConfiguration = null;

                if (selectedQuestion.AssessmentType == LearnerStatisticsType.PreAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PreAssessmentConfiguration;
                }
                else if (selectedQuestion.AssessmentType == LearnerStatisticsType.Quiz)
                {
                    currentAssessmentConfiguration = courseConfiguration.QuizConfiguration;
                }
                else if (selectedQuestion.AssessmentType == LearnerStatisticsType.PostAssessment)
                {
                    currentAssessmentConfiguration = courseConfiguration.PostAssessmentConfiguration;
                }

                //LCMS-8469 - In Case of No Result, LMS Web Service will not be called to update assessment score
                if (currentAssessmentConfiguration.ScoreType != ScoreType.NoResults)
                {
                    //LMS Integeration
                    int source = Convert.ToInt32(Session["Source"]);
                    if (source == 0)
                    {
                        IntegerationStatistics integerationStatistics = new IntegerationStatistics();
                        integerationStatistics.AssessmentType = selectedQuestion.AssessmentType;
                        integerationStatistics.AssessmentScore = weightedScore;
                        integerationStatistics.IsAssessmentPassed = isCurrentAssessmentPassed;
                        integerationStatistics.IntegerationStatisticsType = IntegerationStatisticsType.AssessmentCompletion;
                        integerationStatistics.LearningSessionGuid = learenrSessionID;
                        Session["IntegerationStatistics"] = integerationStatistics;
                        //Integeration integeration = IntegerationFactory.GetObject(source);
                        //integeration.SynchStatsToExternalSystem(integerationStatistics);
                    }
                }


                Session["AssessmentType"] = selectedQuestion.AssessmentType;

                return learnerStatistics_ID;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return 0;

            }
        }
        
        public bool SaveAssessmentEndTrackingInfoInSession(ICP4.BusinessLogic.AssessmentManager.SelectedQuestion selectedQuestion, int noOfAnswersCorrect, int noOfAnswersInCorrect, double weightedScore, bool isCurrentAssessmentPassed)
        {
            try
            {
                int attemptNo = 0;
                LearnerStatistics[] learnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[1];
                LearnerStatistics learnerStatistic = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics();
                LearnerStatistics[] previousLearnerStatistics = null;
                switch (selectedQuestion.AssessmentType)
                {
                    case LearnerStatisticsType.PreAssessment:
                        {
                            learnerStatistic.Statistic_Type = LearnerStatisticsType.PreAssessmentEnd;
                            break;
                        }
                    case LearnerStatisticsType.PostAssessment:
                        {
                            learnerStatistic.Statistic_Type = LearnerStatisticsType.PostAssessmentEnd;
                            break;
                        }
                    case LearnerStatisticsType.Quiz:
                        {
                            learnerStatistic.Statistic_Type = LearnerStatisticsType.QuizEnd;
                            int courseID = Convert.ToInt32(Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(Session["CurrentIndex"]);
                            SequenceItem sequenceItem = null;
                            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                int source = Convert.ToInt32(Session["Source"]);
                                int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                                learnerStatistic.Item_GUID = sequenceItem.Item_GUID;
                            }
                            break;
                        }
                    case LearnerStatisticsType.PracticeExam:
                        {
                            learnerStatistic.Statistic_Type = LearnerStatisticsType.PracticeExamEnd;
                            int courseID = Convert.ToInt32(Session["CourseID"]);
                            int courseSequenceIndex = Convert.ToInt32(Session["CurrentIndex"]);
                            SequenceItem sequenceItem = null;
                            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                            {
                                int source = Convert.ToInt32(Session["Source"]);
                                int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                                sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                                learnerStatistic.Item_GUID = sequenceItem.Item_GUID;
                            }
                            break;
                        }
                }
                if (Session["AssessmentEndStats"] != null)
                {
                    previousLearnerStatistics = (LearnerStatistics[])Session["AssessmentEndStats"];
                }
                if (previousLearnerStatistics == null)
                    previousLearnerStatistics = new ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics[0];

                using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                    bool isPass = false;
                    bool actionTaken = false;
                    long learnerStatisticID = 0;
                    int remediationCount = 0;
                    courseManager.GetLastAssessmentResult(previousLearnerStatistics, 1, out isPass, out attemptNo, out actionTaken, out learnerStatisticID, out remediationCount);
                }

                learnerStatistic.AssessmentAttemptNumber = attemptNo + 1;
                learnerStatistic.AssessmentType = selectedQuestion.AssessmentType;
                learnerStatistic.LearningSession_ID = Convert.ToInt32(Session["LearnerSessionIDPrimary"]);
                learnerStatistic.NumberAnswersCorrect = noOfAnswersCorrect;
                learnerStatistic.NumberAnswersIncorrect = noOfAnswersInCorrect;
                learnerStatistic.MaxAtemptActionTaken = false;
                learnerStatistic.RawScore = weightedScore;
                learnerStatistic.IsPass = isCurrentAssessmentPassed;
                learnerStatistics[0] = learnerStatistic;
                Session["AssessmentEndStats"] = learnerStatistics;
                Session["AssessmentType"] = selectedQuestion.AssessmentType;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;

            }
        }


        private bool SaveCourseEvaluationStatsIfCompleted()
        {
            bool isCourseEvaluationCompleted = false;
            bool isCourseEvaluationSaved = false;

            try
            {
                if (Session["CourseEvaluation"] != null &&
                    Session["CourseEvaluationResult"] != null)
                {
                    isCourseEvaluationCompleted = IsCourseEvaluationCompleted((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)Session["CourseEvaluationResult"],
                                                (ICP4.BusinessLogic.ICPCourseService.CourseEvaluation)Session["CourseEvaluation"]);

                    if (isCourseEvaluationCompleted)
                    {
                        using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                        {
                            trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                            isCourseEvaluationSaved = trackingService.SaveCourseEvaluationResult((ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult)Session["CourseEvaluationResult"]);
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
        private bool IsCourseEvaluationCompleted(ICP4.BusinessLogic.ICPTrackingService.CourseEvaluationResult courseEvaluationResult,ICP4.BusinessLogic.ICPCourseService.CourseEvaluation courseEvaluation)
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

        public double CalculateCourseProgressPercentageFromSequence(int courseID, int seqNo, int total, out int visitedSceneNo, out int totalSceneNo)
        {
            try
            {
                if (total <= 0)
                {
                    //visitedSceneNo = 0;
                    //totalSceneNo = 0;
                    //return 100;    

                    // LCMS-9157 START HERE
                    using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);                        
                        ICP4.BusinessLogic.ICPCourseService.Sequence sequencefortotal = cacheManager.GetIFSequenceExistInCache(courseID, source, courseConfigurationID);
                        total = CalculateTotalViewableScenes(sequencefortotal);
                    }
                    if (seqNo <= 0)
                    {
                        string itemGUID = Session["ItemGUID"].ToString();
                        string sceneGUID = Session["SceneGUID"].ToString();
                        string statisticsType = Session["StatisticsType"].ToString();
                        seqNo = GetSequenceNoByID(courseID, itemGUID, sceneGUID, statisticsType);
                    }
                    // LCMS-9157 END HERE
                }

                ICP4.BusinessLogic.ICPCourseService.Sequence sequence = new ICP4.BusinessLogic.ICPCourseService.Sequence();
                using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                {
                    int source = Convert.ToInt32(Session["Source"]);
                    int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);                    
                    sequence = cacheManager.GetIFSequenceExistInCache(courseID, source, courseConfigurationID);
                }
                int visitedSceneCount = 0;
                for (int index = 0; index <= seqNo; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == SequenceItemTypeName.ContentAsset ||
                        sequence.SequenceItems[index].SequenceItemType == SequenceItemTypeName.FlashAsset)
                    {
                        visitedSceneCount++;
                    }
                }
                totalSceneNo = total;
                visitedSceneNo = visitedSceneCount;
                if (visitedSceneCount > 0 && total > 0)
                {
                    return (visitedSceneCount / Convert.ToDouble(total)) * 100;
                }
                else
                {
                    return 0.0;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");                
                visitedSceneNo = -1;
                totalSceneNo = -1;
                return -1;
            }
        }

        private int CalculateTotalViewableScenes(ICP4.BusinessLogic.ICPCourseService.Sequence sequence)
        {
            int totalViewableScenes = 0;
            for (int index = 0; index <= sequence.SequenceItems.Length - 1; index++)
            {
                if (sequence.SequenceItems[index].SequenceItemType == SequenceItemTypeName.ContentAsset ||
                    sequence.SequenceItems[index].SequenceItemType == SequenceItemTypeName.FlashAsset)
                {
                    totalViewableScenes++;
                }
            }
            return totalViewableScenes;
        }

        private int GetSequenceNoByID(int courseID, string itemGUID, string sceneGUID, string statisticsType)
        {
            int seqeunceNo = -1;
            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);
                seqeunceNo = cacheManager.GetRequestedItemNoFromQueue(courseID, itemGUID, sceneGUID, statisticsType, source, courseConfigurationID);
            }
            return seqeunceNo;
        }

        public int GetValidQuizCount(int CourseId)
        {
            using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(Session["CourseConfigurationID"]);                
                ICP4.BusinessLogic.ICPCourseService.Sequence Sequence = cacheManager.GetIFSequenceExistInCache(CourseId, source, courseConfigurationID);
                int QuizCount = 0;

                foreach (ICP4.BusinessLogic.ICPCourseService.SequenceItem sequnceItem in Sequence.SequenceItems)
                {
                    if (sequnceItem.SequenceItemType == _360Training.BusinessEntities.SequenceItemType.Quiz && sequnceItem.IsValidQuiz == true)
                        QuizCount++;
                    else if (sequnceItem.SequenceItemType == _360Training.BusinessEntities.SequenceItemType.Exam && sequnceItem.ExamType == _360Training.BusinessEntities.SequenceItemType.Quiz && sequnceItem.IsValidQuiz == true)
                        QuizCount++;

                }

                return QuizCount;
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }
    }
}