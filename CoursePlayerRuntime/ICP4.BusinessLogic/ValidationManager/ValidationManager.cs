using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CommunicationLogic.CommunicationCommand;
using ICP4.CommunicationLogic.CommunicationCommand.ShowValidationQuestion;
using ICP4.CommunicationLogic.CommunicationCommand.OpenCommand;
using System.Net;        

namespace ICP4.BusinessLogic.ValidationManager
{
    public class ValidationManager : IDisposable
    {
        public ValidationManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }
        public List<ValidationQuestionData> GetValidationQuestion(int courseID, int studentID)
        {
            List<ValidationQuestionData> validationQuestionDatum = new List<ValidationQuestionData>();
            ValidationQuestionData validationQuestionData = null;
            ICP4.BusinessLogic.ICPCourseService.ValidationQuestion[] validationQuestions = null;
            int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            try
            {

                using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                {
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];                    
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    validationQuestions = courseService.GetValidationQuestions(learnerID, variant);
                }
                for (int index = 0; index < validationQuestions.Length; index++)
                {
                    validationQuestionData = new ValidationQuestionData();
                    validationQuestionData.ValidationQuestionID = validationQuestions[index].ValiditionQuestionId;
                    validationQuestionData.QuestionType = validationQuestions[index].QuestionType; 
                    validationQuestionData.AnswerText = validationQuestions[index].Answer;
                    validationQuestionData.QuestionText = validationQuestions[index].QuestionStem;
                    validationQuestionData.ValidationQuestionOption = GetValidationQuestionOption(validationQuestions[index].ValidationQuestionOption); 
                    validationQuestionDatum.Add(validationQuestionData);
                }   
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
            return validationQuestionDatum;
        }

        private List<ValidationQuestionOption> GetValidationQuestionOption(ICP4.BusinessLogic.ICPCourseService.ValidationQuestionOption[] validationQuestionOptions) 
        {
            List<ValidationQuestionOption> validationQuestionOptionDatum = new List<ValidationQuestionOption>();
            ValidationQuestionOption validationQuestionOptionData = null;
            //ICP4.BusinessLogic.ICPCourseService.ValidationQuestionOption[] validationQuestionOptionsA = null;
            try 
            {
                for (int index = 0; index < validationQuestionOptions.Length; index++)
                {
                    validationQuestionOptionData = new ValidationQuestionOption();
                    validationQuestionOptionData.ValiditionQuestionOptionId = validationQuestionOptions[index].ValiditionQuestionOptionId;
                    validationQuestionOptionData.OptionLabel = validationQuestionOptions[index].OptionLabel;
                    validationQuestionOptionData.OptionValue = validationQuestionOptions[index].OptionValue;
                    validationQuestionOptionData.DisplayOrder = validationQuestionOptions[index].DisplayOrder;                    
                    validationQuestionOptionDatum.Add(validationQuestionOptionData);
                } 
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
            return validationQuestionOptionDatum;
        } 

        public object AskValidationQuestion(int courseID)
        {
            ShowValidationQuestion showValidationQuestion = new ShowValidationQuestion();
            ValidationQuestion validationQuestion = new ValidationQuestion();
            ValidationQuestionData validationQuestionData = new ValidationQuestionData();
            int learnervalidationStatisticsID = 0;
            string ValitionQuestionOptionJavaScriptArray = "";
            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
            try
            {
                showValidationQuestion.CommandName = CommandNames.ShowValidationQuestion;
                validationQuestionData = ProvideQuestionData();
                if (validationQuestionData != null)
                {
                    validationQuestion.ValidationQuestionID = validationQuestionData.ValidationQuestionID;
                    validationQuestion.ValidationQuestionText = validationQuestionData.QuestionText;
                    validationQuestion.ValidationQuestionType = validationQuestionData.QuestionType;

                    if (validationQuestion.ValidationQuestionType == _360Training.BusinessEntities.ValidationQuestionType.SingleSelect)
                    {                        
                        foreach(ValidationQuestionOption validationquestionoption in validationQuestionData.ValidationQuestionOption)
                        {
                            if (!ValitionQuestionOptionJavaScriptArray.Equals(""))
                                ValitionQuestionOptionJavaScriptArray += ",";
                            ValitionQuestionOptionJavaScriptArray += validationquestionoption.OptionValue + ":" + "'" + validationquestionoption.OptionLabel + "'";
                        }                        
                    }
                    validationQuestion.ValidationQuestionOptions = ValitionQuestionOptionJavaScriptArray;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        ICPCourseService.CourseConfiguration courseConfiguration = null;
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                        validationQuestion.ValidationQuestionTimer = courseConfiguration.ValidationTimeToAnswerQuestion;
                    }

                    showValidationQuestion.ValidationQuestion = validationQuestion;

                    #region Tracking
                    learnervalidationStatisticsID = SaveValidationQuestionStatistics(validationQuestionData, new ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics(), enrollmentID);
                    System.Web.HttpContext.Current.Session["currentLearnerValidationStatisticsID"] = learnervalidationStatisticsID;
                    #endregion
                }
                else
                {
                    OpenCommand openCommand = new OpenCommand();
                    OpenCommandMessage openCommandMessage = new OpenCommandMessage();
                    openCommand.CommandName = CommandNames.OpenCommand;
                    openCommandMessage.ContuniueAskingValidationQuestion = false;
                    openCommand.OpenCommandMessage = openCommandMessage;
                    return openCommand;
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
            return  showValidationQuestion;
        
        }

        public bool CheckValidationQuestionAnswer(int validationQuestionID, string studentAnswer)
        {
            try
            {
                bool isCorrectlyAnswered = false;
                List<ValidationQuestionData> validationQuestionList = new List<ValidationQuestionData>();
                validationQuestionList = (List<ValidationQuestionData>)System.Web.HttpContext.Current.Session["ValidationQuestions"];
                int validationQuestionIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["ValidationQuestionIndex"]);
                int learnerValidationStatisticsID=Convert.ToInt32(System.Web.HttpContext.Current.Session["currentLearnerValidationStatisticsID"]);
                #region Answer Checking
                if (((ValidationQuestionData)validationQuestionList[validationQuestionIndex]).ValidationQuestionID == validationQuestionID && ((ValidationQuestionData)validationQuestionList[validationQuestionIndex]).AnswerText.ToLower()  == studentAnswer.ToLower() )
                    isCorrectlyAnswered=true;
                #endregion

                #region Tracking
                validationQuestionList[validationQuestionIndex].IsAnswered = true;
                validationQuestionList[validationQuestionIndex].IsCorrect = isCorrectlyAnswered;
                validationQuestionList[validationQuestionIndex].StudentAnswerText = studentAnswer;
                System.Web.HttpContext.Current.Session["ValidationQuestions"] = validationQuestionList;
                UpdateValidationQuestionStatisticsAnswer(studentAnswer, isCorrectlyAnswered, true, learnerValidationStatisticsID,DateTime.Now);
                #endregion
                
                return isCorrectlyAnswered;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
        }

        public object SetNextQuestion(int courseID,bool correctlyAnswer)
        {
            object returnCommand = new object(); 
            try
            {
                int validationQuestionIndex = 0; 
                int validationAttemptNo = 0;

                validationQuestionIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["ValidationQuestionIndex"]);
                
                //exhaust case for current user session
                if (validationQuestionIndex + 1 >= ((List<ValidationQuestionData>)System.Web.HttpContext.Current.Session["ValidationQuestions"]).Count)
                {
                    List<ValidationQuestionData> ResettedValidationQuestionData = ResetValidationQuestions((List<ValidationQuestionData>)System.Web.HttpContext.Current.Session["ValidationQuestions"]);
                    System.Web.HttpContext.Current.Session["ValidationQuestionIndex"] = 0;
                   // System.Web.HttpContext.Current.Session["ValidationQuestionFailAttemptNo"] = 0;
                    System.Web.HttpContext.Current.Session["ValidationQuestions"] = ResettedValidationQuestionData;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["ValidationQuestionIndex"] = validationQuestionIndex + 1;
                }

                if (correctlyAnswer)
                {
                    
                   // validationQuestionIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["ValidationQuestionIndex"]);
                   // System.Web.HttpContext.Current.Session["ValidationQuestionIndex"] = validationQuestionIndex + 1;
                    System.Web.HttpContext.Current.Session["ValidationQuestionFailAttemptNo"] = 0;
                    OpenCommand openCommand = new OpenCommand();
                    OpenCommandMessage openCommandMessage = new OpenCommandMessage();
                    openCommand.CommandName = CommandNames.OpenCommand;

                    //if( validationQuestionIndex + 1 >= ((List<ValidationQuestionData>)System.Web.HttpContext.Current.Session["ValidationQuestions"]).Count)
                    //    openCommandMessage.ContuniueAskingValidationQuestion = false;
                    //else
                        openCommandMessage.ContuniueAskingValidationQuestion = true;

                    openCommand.OpenCommandMessage = openCommandMessage;
                    returnCommand = openCommand;
                }
                else
                {
                    validationAttemptNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["ValidationQuestionFailAttemptNo"]);
                    System.Web.HttpContext.Current.Session["ValidationQuestionFailAttemptNo"] = validationAttemptNo + 1;
                    ICPCourseService.CourseConfiguration courseConfiguration = null;
                    using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    }
                    if (validationAttemptNo + 1 > courseConfiguration.ValidationNOMissedQuestionsAllowed)
                    {
                        using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                        {
                            int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);

                            //Start
                            string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                            if (System.Web.HttpContext.Current.Session["AssessmentStage"]!=null && System.Web.HttpContext.Current.Session["AssessmentStage"].ToString().Equals("AssessmentIsInProgress"))
                            {
                                System.Web.HttpContext.Current.Session["AssessmentStageDuringLock"] = "true";
                            }
                            courseManager.EndSession(courseID, learnerSessionID, DateTime.Now, DateTime.Now, false);
                            //End

                            courseManager.LockCourse(courseID, learnerID,enrollmentID,ICP4.BusinessLogic.CourseManager.LockingReason.ValidationFailed);
                            
                            if (System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"] != null && System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"].ToString().Equals("True"))
                                courseManager.UpdateCourseStatusDuringAssessment(courseID, enrollmentID);

                            returnCommand = courseManager.CreateCourseLockedCommandObject(courseID, CourseManager.LockingReason.ValidationFailed);
                        }
                    }
                    else
                        returnCommand = AskValidationQuestion(courseID);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
            return returnCommand;
        }
        public object GetValidationOrientationScene()
        {
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();

            return CreateValidationOrientationSceneCommand( brandCode, variant);
        }
        public bool LoadValidationQuestions(int courseID, int learnerID, int enrollmentID, out int secondsSinceLastValidaiton, int source, out int unansweredQuestionID)
        {
            try
            {
                int noOfMissedQuestions;
                unansweredQuestionID = 0;
                int validationQuestionIndex = 0;
                int validationQuestionFailAttemptNo = 0;
                secondsSinceLastValidaiton =0;

                List<ValidationQuestionData> validationQuestionList=new List<ValidationQuestionData>();
                switch (source)
                {
                    case 0:
                        {
                            validationQuestionList = GetValidationQuestion(courseID, learnerID);
                            break;
                        }
                    case 1:
                        {
                            int stateVertiCal = Convert.ToInt32(System.Web.HttpContext.Current.Session["StateVertical"]);                            
                            validationQuestionList = GetQuestionsFromLegacyVU(stateVertiCal, learnerID);
                            break;
                        }
                }
                
                if (validationQuestionList.Count > 0)
                {
					ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics[] learnerValidationStatistics = GetLearnerValidationStatistics(enrollmentID, courseID, learnerID, source, out secondsSinceLastValidaiton);

                    if (learnerValidationStatistics.Length > 0)
                        validationQuestionList = RearrangeValidationQuestions(validationQuestionList, learnerValidationStatistics);

                    unansweredQuestionID = GetPreviousValidationInfo(learnerValidationStatistics, out noOfMissedQuestions);

                    if (noOfMissedQuestions > 0)
                        validationQuestionFailAttemptNo = noOfMissedQuestions;

                    if (unansweredQuestionID > 0)
                    {
                        validationQuestionIndex = GetValidationQuestionIndexByID(validationQuestionList, unansweredQuestionID);
                        using (CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                        {
                            courseManager.ValidationTimerExpired();
                        }
                    }

                    System.Web.HttpContext.Current.Session["ValidationQuestions"] = validationQuestionList;
                    System.Web.HttpContext.Current.Session["ValidationQuestionIndex"] = validationQuestionIndex;
                    System.Web.HttpContext.Current.Session["ValidationQuestionFailAttemptNo"] = validationQuestionFailAttemptNo;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                secondsSinceLastValidaiton = 0;
                unansweredQuestionID = 0;
                return false;
            }

        }

        private List<ValidationQuestionData> GetQuestionsFromLegacyVU(double stateVertical,double learnerID)
        {
           
            object[] arr=null;
            List<ValidationQuestionData> validationQuestionDatum = new List<ValidationQuestionData>();
            using (LegacyVUValidationQuestionService.ValidationQuestionsService validationQuestionsService = new ICP4.BusinessLogic.LegacyVUValidationQuestionService.ValidationQuestionsService())
            {
                validationQuestionsService.Url = ConfigurationManager.AppSettings["LegacyVUValidationQuestionService"];
                validationQuestionsService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]); 
                ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
                arr = validationQuestionsService.GetValidationQuestions(stateVertical, learnerID);
            }
            validationQuestionDatum=TranslateLegacyValidationQuestionsIntoValidationQuestionData(arr) ;
            if (validationQuestionDatum!=null)
                return validationQuestionDatum;
            else
                return new List<ValidationQuestionData>();
        }

        private List<ValidationQuestionData> TranslateLegacyValidationQuestionsIntoValidationQuestionData(object[] arr)
        {

            try
            {
                ValidationQuestionData validationQuestionData = null;
                List<ValidationQuestionData> validationQuestionDatum = new List<ValidationQuestionData>();                
                List<ValidationQuestionOption> validationQuestionOptionList= null;  
                Object[] st;

                System.Xml.XmlElement xmlObj1;
                System.Xml.XmlElement xmlObj2;
                System.Xml.XmlElement xmlObj3;
                System.Xml.XmlElement xmlObj4;
                System.Xml.XmlElement xmlObj5;
                System.Xml.XmlElement xmlObj6;
                String qid, ques, ans, err, defaultanswer, inputfield;
                String str, str1, str2, str3, str4, str5;
                if (arr.Length > 0 && arr.Length > 1)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        validationQuestionData = new ValidationQuestionData();
                        validationQuestionOptionList = new List<ValidationQuestionOption>(); 
                        st = (Object[])arr[i];

                        if (st.Length == 8)
                        {
                            xmlObj1 = (System.Xml.XmlElement)st[2]; // QID
                            xmlObj2 = (System.Xml.XmlElement)st[4]; // ANSWER
                            xmlObj3 = (System.Xml.XmlElement)st[5]; // ERROR
                            xmlObj4 = (System.Xml.XmlElement)st[6]; // QUESTION
                            xmlObj5 = (System.Xml.XmlElement)st[7]; // DEFAULTANSWER
                            xmlObj6 = (System.Xml.XmlElement)st[3]; // ISINPUTFIELD
                            // ============================================================================
                            str = xmlObj1.InnerText.Trim();  // QID
                            qid = str.Substring(0, 3).Trim();
                            if (qid.Equals("qid"))
                            {
                                int strLength = str.Length;
                                int length = strLength - 3;
                                String remain = str.Substring(3, length).Trim();
                                int validationQuestionID;
                                if (!Int32.TryParse(remain, out validationQuestionID))
                                    return null;
                                else
                                {
                                    if (validationQuestionID > 0)
                                    {
                                        validationQuestionData.ValidationQuestionID = validationQuestionID;
                                    }
                                    else
                                    {
                                        continue; 
                                    }
                                }
                            }

                            str1 = xmlObj2.InnerText.Trim(); // ANSWER
                            ans = str1.Substring(0, 6).Trim();
                            if (ans.Equals("answer"))
                            {
                                int strLength = str1.Length;
                                int length = strLength - 6;
                                String remain = str1.Substring(6, length).Trim();
                                validationQuestionData.AnswerText = remain;
                            }

                            str2 = xmlObj3.InnerText.Trim(); // ERROR
                            err = str2.Substring(0, 5).Trim();
                            if (err.Equals("error"))
                            {
                                int strLength = str2.Length;
                                int length = strLength - 5;
                                String remain = str2.Substring(5, length).Trim();
                            }

                            str3 = xmlObj4.InnerText.Trim();  // QUESTION
                            ques = str3.Substring(0, 8).Trim();
                            if (ques.Equals("question"))
                            {
                                int strLength = str3.Length;
                                int length = strLength - 8;
                                String remain = str3.Substring(8, length).Trim();
                                validationQuestionData.QuestionText = remain;
                            }

                            str4 = xmlObj5.InnerText.Trim();  // DEFAULTANSWER
                            defaultanswer = str4.Substring(0, 8).Trim();
                            if (defaultanswer.Equals("defaultA"))
                            {
                                
                                int strLength = str4.Length;
                                int length = strLength - 8;                                
                                String remain = str4.Substring(8, length).Trim();
                                string[] defaultanswerArray = remain.Split('\n');
                                if (defaultanswerArray.Length > 0)
                                {
                                    for (int k = 0; k < defaultanswerArray.Length; k++)
                                    {
                                        if (!defaultanswerArray[k].Trim().Equals(""))
                                        {
                                            ValidationQuestionOption validationQuestionOption = new ValidationQuestionOption();
                                            validationQuestionOption.DisplayOrder = k + 1;
                                            validationQuestionOption.OptionLabel = defaultanswerArray[k].Trim().ToString();
                                            validationQuestionOption.OptionValue = defaultanswerArray[k].Trim().ToString();
                                            validationQuestionOption.ValiditionQuestionOptionId = validationQuestionData.ValidationQuestionID;
                                            validationQuestionOptionList.Add(validationQuestionOption); 
                                        }
                                    }
                                }
                                validationQuestionData.ValidationQuestionOption  = validationQuestionOptionList;
                            }

                            str5 = xmlObj6.InnerText.Trim();  // INPUTFIELD
                            inputfield = str5.Substring(0, 10).Trim();
                            if (inputfield.Equals("inputfield"))
                            {
                                int strLength = str5.Length;
                                int length = strLength - 10;
                                String remain = str5.Substring(10, length).Trim();
                                if (remain.Equals("1"))
                                {
                                    validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.FillInTheBlank;
                                }
                                else
                                {
                                    if (validationQuestionOptionList.Count > 0)
                                    {
                                        validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.SingleSelect;
                                    }
                                    else
                                    {
                                        validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.FillInTheBlank;
                                    }
                                }
                            }
                        }
                        //validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.FillInTheBlank;
                        validationQuestionDatum.Add(validationQuestionData);
                    }
                }
                else if (arr.Length > 0 && arr.Length == 1) // it may be an error
                {
                    validationQuestionData = new ValidationQuestionData();
                    validationQuestionOptionList = new List<ValidationQuestionOption>(); 

                    st = (Object[])arr[0];
                    if (st.Length == 8)
                    {
                        xmlObj1 = (System.Xml.XmlElement)st[2]; // QID
                        xmlObj2 = (System.Xml.XmlElement)st[4]; // ANSWER
                        xmlObj3 = (System.Xml.XmlElement)st[5]; // ERROR
                        xmlObj4 = (System.Xml.XmlElement)st[6]; // QUESTION
                        xmlObj5 = (System.Xml.XmlElement)st[7]; // DEFAULTANSWER
                        xmlObj6 = (System.Xml.XmlElement)st[3]; // ISINPUTFIELD
                        // ========================================================

                        str = xmlObj1.InnerText.Trim();  // QID
                        qid = str.Substring(0, 3).Trim();
                        if (qid.Equals("qid"))
                        {
                            int strLength = str.Length;
                            int length = strLength - 3;
                            String remain = str.Substring(3, length).Trim();
                            int validationQuestionID;
                            if (!Int32.TryParse(remain, out validationQuestionID))
                                return null;
                            else
                            {
                                if (validationQuestionID > 0)
                                {
                                    validationQuestionData.ValidationQuestionID = validationQuestionID;
                                }                                
                            }
                        }

                        if (validationQuestionData.ValidationQuestionID > 0)
                        {
                            str1 = xmlObj2.InnerText.Trim(); // ANSWER
                            ans = str1.Substring(0, 6).Trim();
                            if (ans.Equals("answer"))
                            {
                                int strLength = str1.Length;
                                int length = strLength - 6;
                                String remain = str1.Substring(6, length).Trim();
                                validationQuestionData.AnswerText = remain;
                            }

                            str2 = xmlObj3.InnerText.Trim(); // ERROR
                            err = str2.Substring(0, 5).Trim();
                            if (err.Equals("error"))
                            {
                                int strLength = str2.Length;
                                int length = strLength - 5;
                                String remain = str2.Substring(5, length).Trim();
                            }

                            str3 = xmlObj4.InnerText.Trim();  // QUESTION
                            ques = str3.Substring(0, 8).Trim();
                            if (ques.Equals("question"))
                            {
                                int strLength = str3.Length;
                                int length = strLength - 8;
                                String remain = str3.Substring(8, length).Trim();
                                validationQuestionData.QuestionText = remain;
                            }

                            str4 = xmlObj5.InnerText.Trim();  // DEFAULTANSWER
                            defaultanswer = str4.Substring(0, 8).Trim();
                            if (defaultanswer.Equals("defaultA"))
                            {

                                int strLength = str4.Length;
                                int length = strLength - 8;
                                String remain = str4.Substring(8, length).Trim();
                                string[] defaultanswerArray = remain.Split('\n');
                                if (defaultanswerArray.Length > 0)
                                {
                                    for (int k = 0; k < defaultanswerArray.Length; k++)
                                    {
                                        if (!defaultanswerArray[k].Trim().Equals(""))
                                        {
                                            ValidationQuestionOption validationQuestionOption = new ValidationQuestionOption();
                                            validationQuestionOption.DisplayOrder = k + 1;
                                            validationQuestionOption.OptionLabel = defaultanswerArray[k].Trim().ToString();
                                            validationQuestionOption.OptionValue = defaultanswerArray[k].Trim().ToString();
                                            validationQuestionOption.ValiditionQuestionOptionId = validationQuestionData.ValidationQuestionID;
                                            validationQuestionOptionList.Add(validationQuestionOption);
                                        }
                                    }
                                }
                                validationQuestionData.ValidationQuestionOption = validationQuestionOptionList;
                            }

                            str5 = xmlObj6.InnerText.Trim();  // INPUTFIELD
                            inputfield = str5.Substring(0, 10).Trim();
                            if (inputfield.Equals("inputfield"))
                            {
                                int strLength = str5.Length;
                                int length = strLength - 10;
                                String remain = str5.Substring(10, length).Trim();
                                if (remain.Equals("1"))
                                {
                                    validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.FillInTheBlank;
                                }
                                else
                                {
                                    if (validationQuestionOptionList.Count > 0)
                                    {
                                        validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.SingleSelect;
                                    }
                                    else
                                    {
                                        validationQuestionData.QuestionType = _360Training.BusinessEntities.ValidationQuestionType.FillInTheBlank;
                                    }
                                }
                            }
                        }
                    }
                }
                return validationQuestionDatum;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }
        private int GetPreviousValidationInfo(ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics[] learnerValidationStatistics, out int previousNoOfMissedQuestions)
        {
            int unansweredQuestionID = 0;
            previousNoOfMissedQuestions = 0;
            for (int index = 0; index < learnerValidationStatistics.Length; index++)
            {
               // if (learnerValidationStatistics[index].IsAnswered == false)
               //     unansweredQuestionID = learnerValidationStatistics[index].QuestionID;

                if (learnerValidationStatistics[index].IsCorrect == true)
                    previousNoOfMissedQuestions = 0;
                else if (learnerValidationStatistics[index].IsAnswered == true)
                    previousNoOfMissedQuestions++;
            }
            if (learnerValidationStatistics.Length > 0)
            {
                if (learnerValidationStatistics[0].IsAnswered == false)
                    unansweredQuestionID = learnerValidationStatistics[0].QuestionID;
            }

            return unansweredQuestionID;
        }
        private object CreateValidationOrientationSceneCommand(string brandCode,string variant)
        {
            
            ICP4.CommunicationLogic.CommunicationCommand.ShowValidationOrientationScene.ShowValidationOrientationScene showValidationOrientationScene = new ICP4.CommunicationLogic.CommunicationCommand.ShowValidationOrientationScene.ShowValidationOrientationScene();
            ICP4.CommunicationLogic.CommunicationCommand.ShowValidationOrientationScene.ValidationOrientationScene validationOrientationScene = new ICP4.CommunicationLogic.CommunicationCommand.ShowValidationOrientationScene.ValidationOrientationScene();

            string HTML = "";
            using (ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
            {
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                HTML = courseService.GetValidationOrientaionSceneTemplate().TemplateHTML;
            }
            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                StringBuilder sb = new StringBuilder(HTML);
                sb.Replace("$Heading", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.HeadingValidationAndIdentityCheckScene, brandCode, variant));
                sb.Replace("$VisualTop", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ImageValidationAndIdentityCheckScene, brandCode, variant));
                sb.Replace("$Text", cacheManager.GetResourceValueByResourceKey(BrandManager.ResourceKeyNames.ContentValidationAndIdentityCheckScene, brandCode, variant));
                HTML = sb.ToString();
            }


            validationOrientationScene.TemplateHtml= HTML;

            showValidationOrientationScene.ValidationOrientationScene= validationOrientationScene;
            showValidationOrientationScene.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowValidationOrientationScene;
            return showValidationOrientationScene;
        }
        public object StartValidation()
        {
            bool isOrientationSceneEnabled=false;
            ICPCourseService.CourseConfiguration courseConfiguration = null;
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

            using (CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
            }
            if (courseConfiguration.PlayerEnableOrientaionScenes == true)
                isOrientationSceneEnabled = true;

            return CreateStartValidationCommandObject(isOrientationSceneEnabled);
        }
        private object CreateStartValidationCommandObject(bool isOrientationScene)
        {
            ICP4.CommunicationLogic.CommunicationCommand.StartValidation.StartValidation startValidation = new ICP4.CommunicationLogic.CommunicationCommand.StartValidation.StartValidation();
            ICP4.CommunicationLogic.CommunicationCommand.StartValidation.StartValidationMessage startValidationMessage = new ICP4.CommunicationLogic.CommunicationCommand.StartValidation.StartValidationMessage();

            startValidation.CommandName = CommandNames.StartValidation;
            startValidationMessage.IsOrientationScene = isOrientationScene;
            startValidation.StartValidationMessage = startValidationMessage;

            return startValidation;

        }
        private ValidationQuestionData ProvideQuestionData()
        {
            List<ValidationQuestionData> validationQuestionList = new List<ValidationQuestionData>();
            int validationQuestionIndex = 0;
            try
            {
                validationQuestionList = (List<ValidationQuestionData>)System.Web.HttpContext.Current.Session["ValidationQuestions"];
                if (validationQuestionList == null) //Added By Abdus Samad LCMS-13044 //START
                    return null;  //STOP
                else if (validationQuestionList.Count == 0)
                    return null;
                validationQuestionIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["ValidationQuestionIndex"]);
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
            return validationQuestionList[validationQuestionIndex];
        }
        private int SaveValidationQuestionStatistics(ValidationQuestionData validationQuestionData, ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics learnerValidationStatistics,int enrollMentID)
        {
            int returnValue = 0;

            learnerValidationStatistics.AnswerText = validationQuestionData.StudentAnswerText;
            learnerValidationStatistics.EnrollmentID = enrollMentID;
            learnerValidationStatistics.QuestionID = validationQuestionData.ValidationQuestionID;
            learnerValidationStatistics.SaveTime = DateTime.Now;
            learnerValidationStatistics.IsAnswered = validationQuestionData.IsAnswered;
            learnerValidationStatistics.IsCorrect = validationQuestionData.IsCorrect;
            using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
            {
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                returnValue=trackingService.SaveLearnerValidationStatistics(learnerValidationStatistics);
            }
            return returnValue;
        }
        private bool UpdateValidationQuestionStatisticsAnswer(string answerText,bool isCorrect,bool isAnswered,int learnerValidationStatisticsID,DateTime saveTime)
        {
            bool returnValue = false;
            using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
            {
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                returnValue = trackingService.UpdateValidationLearnerStatisticsAnswer(learnerValidationStatisticsID,answerText,isCorrect,isAnswered,saveTime);
            }
            return returnValue;
        }
        private List<ValidationQuestionData> ResetValidationQuestions(List<ValidationQuestionData> validationQuestionDatum)
        {
            foreach (ValidationQuestionData validationQuestionData in validationQuestionDatum)
            {
                validationQuestionData.StudentAnswerText= string.Empty;
                validationQuestionData.IsAnswered = false;
                validationQuestionData.IsCorrect = false;
            }
            return validationQuestionDatum;
        }
		public ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics[] GetLearnerValidationStatistics(int enrollmentID, int courseId, int learnerId, int source, out int minutesSinceLastValidation)
        {
            minutesSinceLastValidation = 0;
            ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics[] learnerValidationStatistics=null;
            using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
            {
                trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                learnerValidationStatistics = trackingService.GetCurrentLearnerValidationStatistics(enrollmentID, courseId, learnerId, source, out minutesSinceLastValidation);
            }
            return learnerValidationStatistics;
        }
        private List<ValidationQuestionData> RearrangeValidationQuestions(List<ValidationQuestionData> validationQuestionDatum, ICP4.BusinessLogic.ICPTrackingService.LearnerValidationStatistics[] learnerValidationStatistics)
        {
            bool isFound = false;
            List<ValidationQuestionData> arrangedValidationQuestionData = new List<ValidationQuestionData>();
            List<ValidationQuestionData> alreadyAskedValidationQuestionData = new List<ValidationQuestionData>();
            foreach (ValidationQuestionData validationQuestionData in validationQuestionDatum)
            {
                isFound = false;
                for (int index = 0; index < learnerValidationStatistics.Length; index++)
                {
                    if (validationQuestionData.ValidationQuestionID == learnerValidationStatistics[index].QuestionID)
                    {
                        alreadyAskedValidationQuestionData.Add(validationQuestionData);
                        isFound = true;
                        break;
                    }
                }
                if (isFound == false)
                    arrangedValidationQuestionData.Add(validationQuestionData);

            }
            if (alreadyAskedValidationQuestionData.Count > 0)
                arrangedValidationQuestionData.AddRange(alreadyAskedValidationQuestionData);

            return arrangedValidationQuestionData;
        }
        public int GetValidationQuestionIndexByID(List<ValidationQuestionData> validationQuestionDatum, int validationQuestionID)
        {
            for(int index=0;index<validationQuestionDatum.Count;index++)
            {
                if (validationQuestionDatum[index].ValidationQuestionID == validationQuestionID)
                    return index;
            }
            return 0;
        }
    
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}

