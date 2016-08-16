using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;



namespace ICP4.BusinessLogic.CourseManager
{
    public class CourseLockingManagement : IDisposable
    {

        public CourseLockingManagement()
        {
        }       
        
        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        public _360Training.BusinessEntities.CourseConfiguration GetCourseConfiguration(int courseConfigurationID)
        {
            DbCommand dbCommand = null;

          
            try
            {
                Database db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
                //This SP gets a courseconfiguration record
                DateTime lastModifiedDate = DateTime.Now;
                dbCommand = db.GetStoredProcCommand("ICP_SELECT_COURSECONFIGURATION");
                db.AddInParameter(dbCommand, "@COURSECONFIGURATION_ID", DbType.Int32, courseConfigurationID);
                _360Training.BusinessEntities.CourseConfiguration courseConfiguration = null;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        lastModifiedDate = dataReader["LASTMODIFIEDDATE"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dataReader["LASTMODIFIEDDATE"]);
                    }



                    if (dataReader.NextResult())
                    {
                        courseConfiguration = GetCourseConfiguration(dataReader);
                        courseConfiguration.LastModifiedDateTime = lastModifiedDate;
                    }
                }
                return courseConfiguration;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        private _360Training.BusinessEntities.CourseConfiguration GetCourseConfiguration(IDataReader dataReader)
        {

            _360Training.BusinessEntities.CourseConfiguration courseConfiguration = new _360Training.BusinessEntities.CourseConfiguration();

            while (dataReader.Read())
            {

                courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit = Convert.ToInt32(dataReader["COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT"]);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay = Convert.ToInt32(dataReader["COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEDAY"]);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute = Convert.ToInt32(dataReader["COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEMINUTE"]);
                courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime = dataReader["COMPLETION_UNITOFMUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIME"].ToString();
                courseConfiguration.CompletionPostAssessmentAttempted = Convert.ToBoolean(dataReader["COMPLETION_POSTASSESSMENTATTEMPTED"]);
                courseConfiguration.CompletionPostAssessmentMastery = Convert.ToBoolean(dataReader["COMPLETION_POSTASSESSMENTMASTERY"]);
                courseConfiguration.CompletionPreAssessmentMastery = Convert.ToBoolean(dataReader["COMPLETION_PREASSESSMENTMASTERY"]);
                courseConfiguration.CompletionQuizMastery = Convert.ToBoolean(dataReader["COMPLETION_QUIZMASTERY"]);
                courseConfiguration.CompletionSurvey = Convert.ToBoolean(dataReader["COMPLETION_SURVEY"]);
                courseConfiguration.CompletionViewEverySceneInCourse = Convert.ToBoolean(dataReader["COMPLETION_VIEWEVERYSCENEINCOURSE"]);
                //DBNull.Value ? false: DBNull.Value ? string.Empty :
                courseConfiguration.CompletionRespondToCourseEvaluation = dataReader["COMPLETION_RESPONDTOCOURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETION_RESPONDTOCOURSEEVALUATION"]);
                courseConfiguration.CertificateEnabled = dataReader["COMPLETIONCERTIFICATEENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETIONCERTIFICATEENABLEDTF"]);
                courseConfiguration.CertificateAssetID = dataReader["COMPLETIONCERTIFICATEASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COMPLETIONCERTIFICATEASSET_ID"]);

                courseConfiguration.CourseConfigurationID = Convert.ToInt32(dataReader["ID"]);
                courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion = Convert.ToBoolean(dataReader["PLAYER_ALLOWUSERTOREVIEWCOURSEAFTERCOMPLETION"]);
                courseConfiguration.PlayerCourseFlow = dataReader["PLAYER_COURSEFLOW"] == DBNull.Value ? "" : dataReader["PLAYER_COURSEFLOW"].ToString();
                courseConfiguration.PlayerEnableContent = Convert.ToBoolean(dataReader["PLAYER_ENABLECONTENT"]);
                courseConfiguration.PlayerEnableEndOfCourseScene = Convert.ToBoolean(dataReader["PLAYER_ENABLEENDOFCOURSESCENE"]);
                courseConfiguration.PlayerEnableIntroPage = Convert.ToBoolean(dataReader["PLAYER_ENABLEINTROPAGE"]);
                courseConfiguration.PlayerEnforceTimedOutline = Convert.ToBoolean(dataReader["PLAYER_ENFORCETIMEDOUTLINE"]);
                courseConfiguration.PlayerIdleUserTimeout = Convert.ToInt32(dataReader["PLAYER_IDLEUSERTIMEOUT"]);

                courseConfiguration.PlayerCourseEvaluation = dataReader["PLAYER_COURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_COURSEEVALUATION"]);
                courseConfiguration.PlayerMustCompleteCourseEvaluatio = dataReader["PLAYER_MUSTCOMPLETECOURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_MUSTCOMPLETECOURSEEVALUATION"]);
                courseConfiguration.PlayerDisplayCourseEvaluation = dataReader["PLAYER_DISPLAYCOURSEEVALUATION"] == DBNull.Value ? _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment : Convert.ToString(dataReader["PLAYER_DISPLAYCOURSEEVALUATION"]);
                courseConfiguration.PlayerCourseEvaluationInstructions = dataReader["PLAYER_COURSEEVALUATIONINSTRUCTIONS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["PLAYER_COURSEEVALUATIONINSTRUCTIONS"]);

                //LCMS-10392
                courseConfiguration.PlayerShowAmazonAffiliatePanel = dataReader["PLAYER_SHOW_AMAZON_AFFILIATE_PANEL"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_AMAZON_AFFILIATE_PANEL"]);

                //Abdus Samad
                //LCMS-11878
                //Start     

                courseConfiguration.PlayerShowCoursesRecommendationPanel = dataReader["PLAYER_SHOW_COURSE_SUGGESTED_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_COURSE_SUGGESTED_TF"]);

                //Stop

                //Waqas Zakai
                //LCMS-
                //Start     

                courseConfiguration.PlayerRestrictIncompleteJSTemplate = dataReader["RESTRICT_INCOMPLETE_JS_TEMPLATE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICT_INCOMPLETE_JS_TEMPLATE"]);

                //Stop

                courseConfiguration.PlayerAllowTOCDisplaySlides = dataReader["PLAYER_SHOW_DISPLAY_SLIDES_TOC"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_DISPLAY_SLIDES_TOC"]);

                courseConfiguration.SpecialQuestionnaire = dataReader["PLAYER_SPECIALQUESTIONNAIRE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SPECIALQUESTIONNAIRE"]);
                courseConfiguration.DisplaySpecialQuestionnaire = dataReader["PLAYER_DISPLAYSPECIALQUESTIONNAIRE"] == DBNull.Value ? _360Training.BusinessEntities.DisplayCourseEvaluation.BeforeAndAfterPostAssessment : Convert.ToString(dataReader["PLAYER_DISPLAYSPECIALQUESTIONNAIRE"]);
                courseConfiguration.MustCompleteSpecialQuestionnaire = dataReader["PLAYER_MUSTCOMPLETESPECIALQUESTIONNAIRE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_MUSTCOMPLETESPECIALQUESTIONNAIRE"]);
                courseConfiguration.SpecialQuestionnaireInstructions = dataReader["PLAYER_SPECIALQUESTIONNAIREINSTRUCTIONS"] == DBNull.Value ? String.Empty : Convert.ToString(dataReader["PLAYER_SPECIALQUESTIONNAIREINSTRUCTIONS"]);


                courseConfiguration.ValidationNOMissedQuestionsAllowed = Convert.ToInt32(dataReader["VALIDATION_NOMISSEDQUESTIONSALLOWED"]);
                courseConfiguration.ValidationNOValidationQuestion = Convert.ToInt32(dataReader["VALIDATION_NOVALIDATIONQUESTION"]);
                courseConfiguration.ValidationRequireIdentityValidation = Convert.ToBoolean(dataReader["VALIDATION_REQUIREIDENTITYVALIDATION"]);
                courseConfiguration.ValidationTimeBetweenQuestion = Convert.ToInt32(dataReader["VALIDATION_TIMEBETWEENQUESTION"]);
                courseConfiguration.ValidationTimeToAnswerQuestion = Convert.ToInt32(dataReader["VALIDATION_TIMETOANSWERQUESTION"]);
                courseConfiguration.PlayerEnableOrientaionScenes = dataReader["PLAYER_ORIENTATIONKEY"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_ORIENTATIONKEY"]);
                //Embedded ACK
                //Embedded ACK
                courseConfiguration.EmbeddedAcknowledgmentEnabled = dataReader["EMBEDDED_ACKNOWLEDGMENT_ENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["EMBEDDED_ACKNOWLEDGMENT_ENABLEDTF"]);
                courseConfiguration.EmbeddedAcknowledgmentText = dataReader["EMBEDDED_ACKNOWLEDGMENT_TEXT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["EMBEDDED_ACKNOWLEDGMENT_TEXT"]);
                // Added by Waqas Zakai 
                // LCMS-5337
                courseConfiguration.SeatTimeEnabled = dataReader["SEATTIMEENABLED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SEATTIMEENABLED"]);
                courseConfiguration.SeatTimeInHour = Convert.ToInt32(dataReader["MAXSEATTIMEPERDAYHOUR"]);
                courseConfiguration.SeatTimeInMin = Convert.ToInt32(dataReader["MAXSEATTIMEPERDAYMINUTE"]);
                courseConfiguration.MessageSeatTimeExceeds = dataReader["MESSAGESEATTIMEEXCEEDS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["MESSAGESEATTIMEEXCEEDS"].ToString());
                courseConfiguration.MessageSeatTimeCourseLaunch = dataReader["MESSAGESEATTIMECOURSELAUNCH"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["MESSAGESEATTIMECOURSELAUNCH"].ToString());

                // Added by Waqas Zakai
                // LCMS-6092
                courseConfiguration.ActionToTakeUponIdleTimeOut = dataReader["ACTIONTOTAKEUPONIDLETIMEOUT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ACTIONTOTAKEUPONIDLETIMEOUT"].ToString());

                // Added by Waqas Zakai
                // LCMS-6461

                courseConfiguration.PlayerEndOfCourseInstructions = dataReader["ENDOFCOURSEINSTRUCTIONS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ENDOFCOURSEINSTRUCTIONS"].ToString());

                //LCMS-8422
                courseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF = dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE_TF"]);
                courseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"] == DBNull.Value ? 12 : Convert.ToInt32(dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"]);
                courseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = dataReader["UNIT_MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"] == DBNull.Value ? _360Training.BusinessEntities.TimeUnit.Months : dataReader["UNIT_MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"].ToString();

                //LCMS-10186
                courseConfiguration.IsRequireProctorValidation = dataReader["REQUIRE_PROCTOR_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["REQUIRE_PROCTOR_VALIDATION_TF"]); // LCMS-9455
                courseConfiguration.IsRequireLearnerValidation = dataReader["REQUIRE_LEARNER_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["REQUIRE_LEARNER_VALIDATION_TF"]);
                courseConfiguration.IsANSIValidation = dataReader["ANSI_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSI_VALIDATION_TF"]);
                courseConfiguration.IsNYInsuranceValidation = dataReader["NY_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["NY_VALIDATION_TF"]);
                courseConfiguration.IsCARealStateValidation = dataReader["CA_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CA_VALIDATION_TF"]);

                //LCMS-10536
                courseConfiguration.InstructorInfoEnabled = dataReader["INSTRUCTORINFO_ENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["INSTRUCTORINFO_ENABLEDTF"]);
                courseConfiguration.InstructorInfoText = dataReader["INSTRUCTORINFO_TEXT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["INSTRUCTORINFO_TEXT"]);

                //LCMS-11877
                courseConfiguration.AllowCourseRating = dataReader["ALLOW_RATING_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOW_RATING_TF"]);

            }

            if (dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    String assessmentType = dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();
                    _360Training.BusinessEntities.AssessmentConfiguration assessmentconfiguration = null;
                    if (assessmentType == "")
                    {
                        continue;
                    }
                    else if (assessmentType == _360Training.BusinessEntities.AssessmentConfiguration.ASSESSMENTYPE_PREASSESSMENT)
                    {
                        assessmentconfiguration = courseConfiguration.PreAssessmentConfiguration;
                    }
                    else if (assessmentType == _360Training.BusinessEntities.AssessmentConfiguration.ASSESSMENTYPE_POSTASSESSMET)
                    {
                        assessmentconfiguration = courseConfiguration.PostAssessmentConfiguration;
                    }
                    else if (assessmentType == _360Training.BusinessEntities.AssessmentConfiguration.ASSESSMENTYPE_QUIZ)
                    {
                        assessmentconfiguration = courseConfiguration.QuizConfiguration;
                    }
                    else if (assessmentType == _360Training.BusinessEntities.AssessmentConfiguration.ASSESSMENTYPE_PRACTICEEXAM)
                    {
                        assessmentconfiguration = courseConfiguration.PracticeAssessmentConfiguration;
                    }

                    assessmentconfiguration.ID = Convert.ToInt32(dataReader["ID"]);
                    assessmentconfiguration.Enabled = Convert.ToBoolean(dataReader["ENABLED"]);
                    assessmentconfiguration.MaximumNOAttempt = dataReader["MAXIMUMNOATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXIMUMNOATTEMPT"]);
                    assessmentconfiguration.NOQuestion = Convert.ToInt32(dataReader["NOQUESTION"]);
                    assessmentconfiguration.MasteryScore = Convert.ToInt32(dataReader["MASTERYSCORE"]);
                    assessmentconfiguration.ActionToTakeAfterFailingMaxAttempt = dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"] == DBNull.Value ? "" : dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"].ToString();
                    assessmentconfiguration.AllowPauseResumeAssessment = Convert.ToBoolean(dataReader["ALLOWPAUSERESUMEASSESSMENT"]);
                    assessmentconfiguration.AllowSkippingQuestion = Convert.ToBoolean(dataReader["ALLOWSKIPPINGQUESTION"]);
                    assessmentconfiguration.EnforceMaximumTimeLimit = Convert.ToInt32(dataReader["ENFORCEMAXIMUMTIMELIMIT"]);
                    assessmentconfiguration.EnforceUniqueQuestionsOnRetake = Convert.ToBoolean(dataReader["ENFORCEUNIQUEQUESTIONSONRETAKE"]);
                    assessmentconfiguration.MinimumTimeBeforeStart = dataReader["MINIMUMTIMEBEFORESTART"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MINIMUMTIMEBEFORESTART"]);
                    assessmentconfiguration.MinimumTimeBeforeStartUnit = dataReader["MINIMUMTIMEBEFORESTART_UNIT"] == DBNull.Value ? "" : dataReader["MINIMUMTIMEBEFORESTART_UNIT"].ToString();
                    assessmentconfiguration.ContentRemediation = dataReader["CONTENTREMEDIATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CONTENTREMEDIATION"]);
                    assessmentconfiguration.ProctoredAssessment = Convert.ToBoolean(dataReader["PROCTOREDASSESSMENT"]);
                    assessmentconfiguration.QuestionLevelResult = Convert.ToBoolean(dataReader["QUESTIONLEVELRESULT"]);
                    assessmentconfiguration.RandomizeAnswers = Convert.ToBoolean(dataReader["RANDOMIZEANSWERS"]);
                    assessmentconfiguration.RandomizeQuestion = Convert.ToBoolean(dataReader["RANDOMIZEQUESTION"]);
                    //assessmentconfiguration.ScoreAsYouGo = Convert.ToBoolean(dataReader["SCOREASYOUGO"]);
                    assessmentconfiguration.ScoreType = dataReader["SCORETYPE"] == DBNull.Value ? "" : dataReader["SCORETYPE"].ToString();
                    assessmentconfiguration.ShowQuestionAnswerSummary = Convert.ToBoolean(dataReader["SHOWQUESTIONANSWERSUMMARY"]);
                    assessmentconfiguration.StrictlyEnforcePolicyToBeUsed = Convert.ToBoolean(dataReader["STRICLYENFORCEPOLICYTOBEUSED"]);
                    assessmentconfiguration.RestrictiveMode = dataReader["RESTRICTIVEASSESSMENTMODETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICTIVEASSESSMENTMODETF"].ToString());
                    assessmentconfiguration.AdvanceQuestionSelectionType = dataReader["ADVANCEQUESTIONSELECTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["ADVANCEQUESTIONSELECTIONTYPE"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded
                    assessmentconfiguration.UseWeightedScore = dataReader["SCOREWEIGHTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SCOREWEIGHTTF"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded; // temporarily hard-coded
                    assessmentconfiguration.GradeQuestions = dataReader["GRADEQUESTIONS"] == DBNull.Value ? "" : Convert.ToString(dataReader["GRADEQUESTIONS"]);
                    assessmentconfiguration.DisplaySeatTimeSatisfiedMessageTF = dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"]);
                    assessmentconfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF = dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"]);
                    assessmentconfiguration.NoResultText = dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"] == DBNull.Value ? "" : dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"].ToString();
                    assessmentconfiguration.MaxAttemptHandlerEnabled = dataReader["ENABLEMAXATTEMPTHANDLER"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEMAXATTEMPTHANDLER"]);
                    assessmentconfiguration.LockoutFuntionalityClickAwayToActiveWindowEnable = dataReader["ENABLELOCKOUTCLICKAWAYFROMACTIVEWINDOWTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLELOCKOUTCLICKAWAYFROMACTIVEWINDOWTF"]);
                    assessmentconfiguration.AssessmentResultEnabled = dataReader["ENABLEVIEWASSESSMENTRESULT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEVIEWASSESSMENTRESULT"]);

                    if (assessmentconfiguration.GradeQuestions == _360Training.BusinessEntities.AssessmentConfiguration.GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED)
                    {
                        assessmentconfiguration.ScoreAsYouGo = false;
                    }
                    else if (assessmentconfiguration.GradeQuestions == _360Training.BusinessEntities.AssessmentConfiguration.GRADEQUESTION_AFTER_EACH_QUESTION_IS_ANSWERED)
                    {
                        assessmentconfiguration.ScoreAsYouGo = true;
                    }
                }
            }

            return courseConfiguration;
        }




        #region IDisposable Members


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
