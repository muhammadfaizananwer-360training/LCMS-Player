using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Web.Services;
using ICP4.BusinessLogic.CourseManager;
using ICP4.BusinessLogic.AssessmentManager;
using ICP4.BusinessLogic.ValidationManager;
using ICP4.BusinessLogic.CourseEvaluation;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CoursePlayer.SuggestedCourses;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace ICP4.CoursePlayer
{
    public partial class CoursePlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
            string brandCode = null;
            string variant = null;

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetNoStore();
            //Response.AppendHeader("Pragma", "no-cache");
            //if (isPageExpired())
            //{
            //    Response.Redirect("CoursePlayerExit.aspx");
            //}
            //else
            //{
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //    Response.Cache.SetNoStore();
            //    Response.AppendHeader("Pragma", "no-cache");
            //}
            

            if (Request.QueryString["REDIRECT"] == null)
            {

                string learnerSessionID = Request.QueryString["SESSION"];
                string courseType = string.Empty;
                using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    string weblinkUrl = string.Empty;
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    if (Convert.ToBoolean(Request.QueryString["PREVIEW"]) == true || Convert.ToBoolean(Request.QueryString["DEMO"]) == true)
                    {
                        
                        courseType = ICP4.BusinessLogic.CourseManager.CourseType.SelfPacedCourse;
                    }
                    else
                    {
                       
                        courseType = trackingService.GetLearningSessionCourseTypeAndUrl(learnerSessionID, out weblinkUrl);
                    }
                    #region Legacy Course
                    if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.LegacyCourse)
                    {
                        //As per planned we are shutting down the legacy so we have created a redirect page
                        using (CourseManager courseManager = new CourseManager())
                        {
                            if (courseManager.CheckForLegacyRedirectionStatus() == true)
                            {

                                Response.Redirect("~/LegacyRedirectPage.aspx", false);

                            }
                            else
                            {
                                string url = ConfigurationManager.AppSettings["LMSConnector_AdapterURL"];
                                string emailAddress;
                                string firstName;
                                string lastName;
                                string courseGUID;
                                int epoch;
                                int learnerID;
                                string phone;
                                string officePhone;
                                string streetAddress;
                                string city;
                                string zipCode;
                                string state;
                                string country;
                                string middleName;
                                string userName;
                                emailAddress = trackingService.GetInfoForLMSVUConnector(learnerSessionID, out firstName, out lastName, out courseGUID, out epoch, out learnerID, out phone, out officePhone, out streetAddress, out city, out zipCode, out state, out country, out middleName, out userName);
                                string queryString = "email=" + HttpUtility.UrlEncode(emailAddress) + "&fname=" + HttpUtility.UrlEncode(firstName) + "&lname=" + HttpUtility.UrlEncode(lastName) + "&lmscourseId=" + HttpUtility.UrlEncode(courseGUID) + "&learnerSessionId=" + HttpUtility.UrlEncode(learnerSessionID) + "&epoch=" + epoch + "&learnerId=" + learnerID + "&phone=" + HttpUtility.UrlEncode(phone) + "&office=" + HttpUtility.UrlEncode(officePhone) + "&address=" + HttpUtility.UrlEncode(streetAddress) + "&city=" + HttpUtility.UrlEncode(city) + "&zip=" + HttpUtility.UrlEncode(zipCode) + "&state=" + HttpUtility.UrlEncode(state) + "&country=" + HttpUtility.UrlEncode(country) + "&mi=" + HttpUtility.UrlEncode(middleName) + "&uname=" + HttpUtility.UrlEncode(userName);
                                Response.Redirect(url + "?" + queryString, false);

                            }


                        }                                

                        HttpContext.Current.Session.Add("isAbondoned", "true");
                        HttpContext.Current.Session.Abandon();
                    }
                    #endregion

                    #region WebLinkCourse
                    else if (courseType.ToLower() == ICP4.BusinessLogic.CourseManager.CourseType.WebLinkCourse.ToLower())
                    {
                        if (weblinkUrl.IndexOf("http") == -1)
                            weblinkUrl = "http://" + weblinkUrl;
                        Response.Redirect(weblinkUrl, false);
                        HttpContext.Current.Session.Add("isAbondoned", "true");
                        HttpContext.Current.Session.Abandon();
                    }
                    #endregion WebLinkCourse

                    #region Self Paced Course
                    else if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.SelfPacedCourse)
                    {

                        bool isPreview = Convert.ToBoolean(Request.QueryString["PREVIEW"]);
                        if (isPreview == true)
                        {
                            brandCode = Request.QueryString["BRANDCODE"].ToString();
                            variant = Request.QueryString["VARIANT"].ToString();
                            // preloaderimg.Src = trackingService.GetResourceValueOfResourceKey(brandCode, variant, ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ImagePlayerLoading);
                        }
                        else
                        {
                            //Multilingual Start
                            brandCode = trackingService.GetLearningSessionBrandcodeVariant(learnerSessionID, out variant);
                            //Multilingual End    
                        }

                        //Multilingual Start
                        StringBuilder loadingScriptTextScriptSB = new StringBuilder();
                        loadingScriptTextScriptSB.Append(@"<script type=""text/javascript"" language=""javascript"">");
                        //loadingScriptTextScriptSB.Append(@"  var myVar =" & MyVariable & ";  //use a server-side statement to pull\n");
                        loadingScriptTextScriptSB.Append(@"  function processSetUp(){      ");
                        loadingScriptTextScriptSB.Append(@"  brandCode = '" + brandCode + "';");
                        loadingScriptTextScriptSB.Append(@"  variant = '" + variant + "';}");
                        loadingScriptTextScriptSB.Append(@"</script>");

                        this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "processSetUp", loadingScriptTextScriptSB.ToString());
                        //Multilingual End


                    }
                    #endregion

                    #region External Course
                    else //if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.ExternalCourse)
                    {
                        string url = ConfigurationManager.AppSettings["ExternalCourseHandlerURL"] + "?learnerSessionID=" + learnerSessionID;
                        Response.Redirect(url, false);
                        HttpContext.Current.Session.Add("isAbondoned", "true");
                        HttpContext.Current.Session.Abandon();
                    }
                    #endregion
                }

            }
        }

        //private bool isPageExpired()
        //{
        //    if (Session["IsPageExpired"] != null && Convert.ToBoolean(Session["IsPageExpired"])==true)
        //        return true;            
        //    else
        //        return false;
        //}

        [WebMethod]
        public static string InitializeCoursePlayer(string learnerSessionID, string brandCode, string variant, int courseID, bool isdemo, bool isRedirect, bool isPreview, int svId, int sceneID, int assetID, string courseGUID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.InitializedCoursePlayer(learnerSessionID, brandCode, variant, courseID, isdemo,isRedirect, isPreview, svId,sceneID,assetID,courseGUID);
                   
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }


        [WebMethod]
        public static string LoadCourse()
        {
            try
            {          
                using (CourseManager courseManager = new CourseManager())
                {                   
                    string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
					bool isRedirect = Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsRedirect"]);
                    object commandObject = courseManager.LoadCourse(learnerSessionID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);
            }
        }
        // LCMS-10535
        //[WebMethod]
        //public static string LoadCourseSettings()
        //{
        //    int courseID = -1;
        //    try
        //    {
        //        courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
        //        using (CourseManager courseManager = new CourseManager())
        //        {
        //            object commandObject = courseManager.LoadCourseSettings(courseID);
        //            return JavaScriptConvert.SerializeObject(commandObject);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception ex1 = new Exception("CourseID : " + courseID + " " + ex.Message, ex.InnerException);
        //        ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
        //        return CreateExceptionCommandMessage(ex1);
        //    }
        //}

        [WebMethod]
        public static string BeginCourse()
        {
            try
            {
                object commandObject = null;
 

                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID, studentID;
                    string sceneGUID, itemGUID, statisticsType = "";
                    string endSession = string.Empty;
					bool isRedirect = Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsRedirect"]);
                    //Logger.append(Convert.ToString(isRedirect), Logger.ALL);
                    courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    studentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                    itemGUID = Convert.ToString(System.Web.HttpContext.Current.Session["ItemGUID"]);
                    sceneGUID = Convert.ToString(System.Web.HttpContext.Current.Session["SceneGUID"]);
                    statisticsType = Convert.ToString(System.Web.HttpContext.Current.Session["StatisticsType"]);                                        
                               
                    commandObject = courseManager.BeginCourse(courseID, studentID, itemGUID, sceneGUID, statisticsType, isRedirect);

                    #region Maximum Seat Time
                    //Check For Maximum Seat Time:LCMS-5263:Starts
                    object commandObjectSeatTime = courseManager.CheckSeatTime(1);
                    if (commandObjectSeatTime != null)
                        return JavaScriptConvert.SerializeObject(commandObjectSeatTime);
                    //Check For Maximum Seat Time:LCMS-5263:Ends
                    #endregion


                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
					}
					if (isRedirect)
                    {
                        System.Web.HttpContext.Current.Session["IsRedirect"] = false;
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }
        }

        [WebMethod]
        public static string Next()
        {
            try
            {

                /*
                 * LCMS-6247 - Start
                 * Abandon the session as it's assumed that the session would be already created.
                 * This case can be re-produced when IIS server is reset or restart and user click Next/Back
                 */
                if (System.Web.HttpContext.Current.Session.IsNewSession)
                {
                    System.Web.HttpContext.Current.Session.Abandon();
                }

                /*
                 * LCMS-6247 - End
                 */

                if (System.Web.HttpContext.Current.Session["CurrentCommand"] == null)
                {
                    using (CourseManager courseManager = new CourseManager())
                    {
                        object commandObject = new object();

                        #region Maximum Seat Time
                        //Check For Maximum Seat Time:LCMS-5263:Starts
                        commandObject = courseManager.CheckSeatTime(1);
                        if(commandObject!=null)
                           return JavaScriptConvert.SerializeObject(commandObject);
                        //Check For Maximum Seat Time:LCMS-5263:Ends
                        #endregion

                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                        if (Convert.ToString(System.Web.HttpContext.Current.Session["AssessmentFlow"]) == "QuestionContentRemidiation")
                        {
                            commandObject = RemidationNextScene();
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["IsNormalDirection"] = "true";
                            commandObject = courseManager.NextBack(courseID, 1);
                        }

                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                }
                else
                {
                    object commandObject = new object();
                    using (CourseManager courseManager = new CourseManager())
                    {
                        commandObject = courseManager.CheckSeatTime(1);
                    }
                    if (commandObject != null)
                    {
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                    else
                    {
                        commandObject = System.Web.HttpContext.Current.Session["CurrentCommand"];
                        System.Web.HttpContext.Current.Session["CurrentCommand"] = null;
                        System.Web.HttpContext.Current.Session.Remove("CurrentCommand");
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START  
                        using (CourseManager courseManager = new CourseManager())
                        {
                            if (courseManager.IsCoursePublished() == true)
                            {
                                object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                                courseManager.SessionAbandonOnScene();
                                return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                            }
                        }
                        //END
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static object NextValidation()
        {
            try
            {

                /*
                 * LCMS-6247 - Start
                 * Abandon the session as it's assumed that the session would be already created.
                 * This case can be re-produced when IIS server is reset or restart and user click Next/Back
                 */
                if (System.Web.HttpContext.Current.Session.IsNewSession)
                {
                    System.Web.HttpContext.Current.Session.Abandon();
                }

                /*
                 * LCMS-6247 - End
                 */

                if (System.Web.HttpContext.Current.Session["CurrentCommand"] == null)
                {
                    using (CourseManager courseManager = new CourseManager())
                    {
                        object commandObject = new object();

                        #region Maximum Seat Time
                        //Check For Maximum Seat Time:LCMS-5263:Starts
                        commandObject = courseManager.CheckSeatTime(1);
                        if (commandObject != null)
                            return JavaScriptConvert.SerializeObject(commandObject);
                        //Check For Maximum Seat Time:LCMS-5263:Ends
                        #endregion

                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                        if (Convert.ToString(System.Web.HttpContext.Current.Session["AssessmentFlow"]) == "QuestionContentRemidiation")
                        {
                            commandObject = RemidationNextScene();
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["IsNormalDirection"] = "true";
                            commandObject = courseManager.NextBack(courseID, 1);
                        }

                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                }
                else
                {
                    object commandObject = new object();
                    using (CourseManager courseManager = new CourseManager())
                    {
                        commandObject = courseManager.CheckSeatTime(1);
                    }
                    if (commandObject != null)
                    {
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                    else
                    {
                        commandObject = System.Web.HttpContext.Current.Session["CurrentCommand"];
                        System.Web.HttpContext.Current.Session["CurrentCommand"] = null;
                        System.Web.HttpContext.Current.Session.Remove("CurrentCommand");
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START  
                        using (CourseManager courseManager = new CourseManager())
                        {
                            if (courseManager.IsCoursePublished() == true)
                            {
                                object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                                courseManager.SessionAbandonOnScene();
                                return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                            }
                        }
                        //END
                        return commandObject;
                    }
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string Back()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = new object();

                    #region Maximum Seat Time
                    //Check For Maximum Seat Time:LCMS-5263:Starts
                    commandObject = courseManager.CheckSeatTime(-1);
                    if (commandObject != null)
                      return JavaScriptConvert.SerializeObject(commandObject);
                    //Check For Maximum Seat Time:LCMS-5263:Ends
                    #endregion

                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                    if (Convert.ToString(System.Web.HttpContext.Current.Session["AssessmentFlow"]) == "QuestionContentRemidiation")
                    {
                        commandObject = RemidationBackScene();
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["IsNormalDirection"] = "true";
                        commandObject = courseManager.NextBack(courseID, -1);
                    }

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);
            }
        }

        [WebMethod]
        public static string Goto(int id, String type)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = new object();

                    #region Maximum Seat Time
                    //Check For Maximum Seat Time:LCMS-5263:Starts
                    commandObject = courseManager.CheckSeatTime(0);
                    if (commandObject != null)
                        return JavaScriptConvert.SerializeObject(commandObject);
                    //Check For Maximum Seat Time:LCMS-5263:Ends
                    #endregion

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }                   
                    //END
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    System.Web.HttpContext.Current.Session["CancelSpecialPostAssessmentValidation"] = true;
                    //commandObject = courseManager.Goto(courseID, contentObjectID);
                    commandObject = courseManager.Goto(courseID, id, type);
                    System.Web.HttpContext.Current.Session["CancelSpecialPostAssessmentValidation"] = false;

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }


        [WebMethod]
        public static string GetTOC()
        {
            int enrollmentID = 0;
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int studentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                    enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                    object commandObject = courseManager.GetTOC(courseID, studentID,enrollmentID);

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }

            catch (Exception ex)
            {
                Exception ex1 = new Exception("enrollmentID : " + enrollmentID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string GetGlossary(int sceneID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = courseManager.GetGlossary(courseID, sceneID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }


        [WebMethod]
        public static string GetBookMark()
        {
            int enrollmentID = 0;
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int studentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                    enrollmentID=Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]); 
                    object commandObject = courseManager.GetCourseStudentBookmark(courseID, studentID,enrollmentID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("enrollmentID : " + enrollmentID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }


        [WebMethod]
        public static string SaveBookMark(string bookMarkTitle, string itemGUID, string sceneGUID, string flashSceneNo, string lastScene, bool IsMovieEnded, bool nextButtonState, string firstSceneName)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int studentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                    int enrollmentID=Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]); 
                    bookMarkTitle = bookMarkTitle.Replace("[^&*]", "'");
                    object commandObject = courseManager.SaveCourseStudentBookmark(courseID, studentID,enrollmentID, itemGUID, sceneGUID, flashSceneNo, bookMarkTitle, lastScene, IsMovieEnded, nextButtonState, firstSceneName);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string DeleteBookMark(string bookmarkID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int studentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                    int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);                    
                    object commandObject = courseManager.DeleteStudentBookmark(courseID, studentID, enrollmentID, Convert.ToInt32(bookmarkID));
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string LoadBookMark(int bookMarkID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = courseManager.LoadBookmark(courseID, bookMarkID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }
        }

        [WebMethod]
        public static string LoadGlossaryItem(int glossaryID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.GetGlossaryItem(glossaryID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string GetCourseMaterial()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = courseManager.GetCourseMaterial(courseID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }
        }






        [WebMethod]
        public static string CourseIdleTimeOut()
        {
            return "";
        }

        [WebMethod]
        public static string CourseExpired()
        {
            return "";
        }

        [WebMethod]
        public static string logoutCoursePlayerIntegeration(string timer)
        {
            try
            {
                //System.Threading.Thread.Sleep(60 * 1000);

                //Abdus Samad LCMS-13553
                //Start 
                try
                {
                    SaveStudentSpendTimeInCookie();

                }
                catch (Exception exp)
                {

                }
                //Stop                               
                
                    using (CourseManager courseManager = new CourseManager())
                    {
                        if (System.Web.HttpContext.Current.Session["LearnerSessionID"] != null)
                        {
                            string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                            //if (System.Web.HttpContext.Current.Session["CourseID"] != null)
                            //{

                            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                            //string itemGUID = System.Web.HttpContext.Current.Session["ItemGUID"].ToString();
                            //string sceneGUID = System.Web.HttpContext.Current.Session["SceneGUID"].ToString();
                            int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);

                            object commandObject = null;
                            //if (seqNo != -1)
                            //{
                            commandObject = courseManager.EndSession(courseID, learnerSessionID, DateTime.Now, DateTime.Now, false);
                            //}


                        }
                    }
            }
            catch (Exception ex1)
            {
                if (System.Web.HttpContext.Current.Session["LearnerSessionID"] != null)
                    ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
            }
            System.Web.HttpContext.Current.Session.Add("IsLearningSessionEnded", true); 
            return "";
        }       

        private static void ProctorCleanUp()
        {
            try
            {
                Logger.Write("LMS ProctorCleanUp : " + DateTime.Now.ToString(), "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                //Now we can call Proctor
                if (ConfigurationManager.AppSettings["OnlineProctorCourseId"] != null || ConfigurationManager.AppSettings["OnlineProctorCourseId"] != String.Empty)
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    if (courseID == Convert.ToInt32(ConfigurationManager.AppSettings["OnlineProctorCourseId"]))
                    {
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ConfigurationManager.AppSettings["OnlineProctorCourseExtSaveAndClose"] + "/" + System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString());
                        request.Method = "GET";
                        String test = String.Empty;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            Stream dataStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(dataStream);
                            test = reader.ReadToEnd();
							Logger.Write("LMS ProctorCleanUp : " + DateTime.Now.ToString(), "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                            reader.Close();
                            dataStream.Close();
                        }
                    }

                }
            }
            catch (Exception ex1)
            {

                    ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
            }
        }

        [WebMethod]
        public static string LogoutCoursePlayer(string timer)
        {
            try
            {
               
                //Abdus Samad LCMS-13553
                //Start 
                try
                {
                    SaveStudentSpendTimeInCookie();
                }
                catch (Exception exp)
                {

                }
                //Stop
                              
                //System.Diagnostics.Trace.WriteLine("LogoutCoursePlayer : 1" + System.Web.HttpContext.Current.Session.SessionID);
                //System.Diagnostics.Trace.Flush();
                try
                {

                    ProctorCleanUp();
                }
                   catch (Exception exp)
                {

                }


                if (System.Web.HttpContext.Current.Session["IsLearningSessionEnded"] == null || Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsLearningSessionEnded"]) == false)
                {
                    logoutCoursePlayerIntegeration(null);
                }

                using (CourseManager courseManager = new CourseManager())
                {
                    if (System.Web.HttpContext.Current.Session["LearnerSessionID"] != null)
                    {
                        string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();

                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                        int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                        object commandObject = null;
                        if (seqNo != -1)
                        {
                            commandObject = courseManager.EndSessionWithReturn(courseID, learnerSessionID, DateTime.Now);
                        }
                        else
                        {

                            commandObject = courseManager.CreateCustomeMessageForSessionEndWhenRelaunch();
                        }

                        if (commandObject == null)
                        {
                            ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage showCustomMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage();
                            ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.CustomMessage customMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.CustomMessage();

                            customMessage.MessageHeading = "";
                            customMessage.MessageText = "";
                            customMessage.MessageImageURL = "";
                            customMessage.ButtonText = "";
                            customMessage.CustomMessageType = "SessionEnd";
                            customMessage.RedirectURL = string.Empty;
                            showCustomMessage.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCustomMessage;
                            showCustomMessage.CustomMessage = customMessage;
                            //System.Diagnostics.Trace.WriteLine("Turning isAbondoned to true at LogoutcoursePlayer if (commandObject == null)");
                            //System.Diagnostics.Trace.Flush();
                            HttpContext.Current.Session.Add("isAbondoned", "true");
                            HttpContext.Current.Session.Abandon();

                            return JavaScriptConvert.SerializeObject(showCustomMessage);

                        }
                        else
                        {
                            //System.Diagnostics.Trace.WriteLine("Turning isAbondoned to true at LogoutcoursePlayer (else)");
                            //System.Diagnostics.Trace.Flush();
                            HttpContext.Current.Session.Add("isAbondoned", "true");
                            HttpContext.Current.Session.Abandon();
                            return JavaScriptConvert.SerializeObject(commandObject);
                        }

                    }
                    else
                    {
                        ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage showCustomMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage();
                        ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.CustomMessage customMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.CustomMessage();

                        customMessage.MessageHeading = "";
                        customMessage.MessageText = "";
                        customMessage.MessageImageURL = "";
                        customMessage.ButtonText = "";
                        customMessage.CustomMessageType = "SessionEnd";
                        customMessage.RedirectURL = string.Empty;
                        showCustomMessage.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCustomMessage;
                        showCustomMessage.CustomMessage = customMessage;
                        //System.Diagnostics.Trace.WriteLine("Turning isAbondoned to true at LogoutcoursePlayer (outer else)");
                        //System.Diagnostics.Trace.Flush();
                        HttpContext.Current.Session.Add("isAbondoned", "true");
                        HttpContext.Current.Session.Abandon();

                        return JavaScriptConvert.SerializeObject(showCustomMessage);
                    }
                }
            }
            catch (Exception ex1)
            {
                HttpContext.Current.Session.Add("isAbondoned", "true");
                HttpContext.Current.Session.Abandon();

                if (System.Web.HttpContext.Current.Session["LearnerSessionID"] != null)
                    ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }    
    

        [WebMethod]
        public static void UnlockCourse()
        {
            string learnerSessionID = string.Empty;
            try
            {
                //This check determines wether the lock course message appeared at start or middle of course
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]) != 0)
                {
                    using (CourseManager courseManager = new CourseManager())
                    {
                        learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                        object commandObject = courseManager.EndSession(courseID, learnerSessionID, DateTime.Now, DateTime.Now, false);
                        HttpContext.Current.Session.Add("isAbondoned", "true");
                        HttpContext.Current.Session.Abandon();

                    }
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
            }
        }
        [WebMethod]
        public static string StartAssessment()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = null;
                    if (System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] != null)
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START
                        using (CourseManager courseManager = new CourseManager())
                        {
                            if (courseManager.IsCoursePublished() == true)
                            {
                                object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                                courseManager.SessionAbandonOnScene();
                                return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                            }
                        }
                        //END
                        using (ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager knowledgeCheckManager = new ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager())
                        {
                            commandObject = knowledgeCheckManager.GetNextKnowledgeCheckQuestion();
                        }
                    }
                    else
                    {
                        //LCMS-10266-- if this is either a start assessment over or continue case for random alternate then reset the assessment items stats
                        if (HttpContext.Current.Session["RandomAlternateWithPauseResume"] != null
                            && Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) > 0)
                        {
                            assessmentManager.ResetRandomAlternateAssessmentItemStatsByAttempt();
                        }
                        ////End LCMS-10266///////
                        System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = 0;
                        HttpContext.Current.Session.Remove("askedAssessmentItemsAttributes");
                        System.Web.HttpContext.Current.Session["AssessmentFlow"] = "NormalSequentialFlow";
                        System.Web.HttpContext.Current.Session["AssessmentStage"] = "AssessmentIsInProgress";
                        System.Web.HttpContext.Current.Session["AssessmentStartTime"] = DateTime.Now;
                        System.Web.HttpContext.Current.Session["IsAssessmentStarting"] = false;
                                                
                        commandObject = assessmentManager.GetNextQuestion();
                    }
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    using (CourseManager courseManager = new CourseManager())
                    {
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
            //Since we havent implement any assessment, so just call Next method and skip the assessment :)
            //return Next(); 
        }




        // LCMS-9213
        [WebMethod]
        public static string ResumeAssessment()
        {
            try
            {


                ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);                    
                int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                ICP4.BusinessLogic.ICPCourseService.SequenceItem sequenceItem = cacheManager.GetRequestedItemFromQueue(courseID, courseSequenceIndex, source, courseConfigurationID);
                string itemType = string.Empty;

                ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration assessmentConfiguration = new ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration();

                if (sequenceItem.SequenceItemType == SequenceItemTypeName.Exam)
                {
                    itemType = sequenceItem.ExamType;
                }
                else
                    itemType = sequenceItem.SequenceItemType;

                if (itemType == SequenceItemTypeName.PreAssessment)
                {
                    assessmentConfiguration = courseConfig.PreAssessmentConfiguration;
                }
                else if (itemType == SequenceItemTypeName.Quiz)
                {             
                    assessmentConfiguration = courseConfig.QuizConfiguration;                   
                }
                else if (itemType == SequenceItemTypeName.PostAssessment)
                {
                    assessmentConfiguration = courseConfig.PostAssessmentConfiguration;                   
                }
                else if (itemType == SequenceItemTypeName.PracticeExam)
                {
                    assessmentConfiguration = (ICP4.BusinessLogic.ICPCourseService.AssessmentConfiguration)System.Web.HttpContext.Current.Session["PracticeExamAssessmentConfiguration"];
                }

                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = null;

                    System.Web.HttpContext.Current.Session["AssessmentFlow"] = "NormalSequentialFlow";
                    System.Web.HttpContext.Current.Session["AssessmentStage"] = "AssessmentIsInProgress";
                    System.Web.HttpContext.Current.Session["AssessmentStartTime"] = DateTime.Now;
                    System.Web.HttpContext.Current.Session["IsAssessmentStarting"] = false;                    

                    // LCMS-9213
                    //--------------------------------------------------------
                    //List<string> previouslyAskedQuestionsWithAttr = new List<string>();
                    List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics> previouslyAskedQuestionsWithAttr = new List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>();

                    if (HttpContext.Current.Session["askedAssessmentItemsAttributes"] != null)
                    {
                        // previouslyAskedQuestionsWithAttr = (List<string>)HttpContext.Current.Session["askedAssessmentItemsAttributes"];
                        previouslyAskedQuestionsWithAttr = (List<ICP4.BusinessLogic.ICPTrackingService.LearnerStatistics>)HttpContext.Current.Session["askedAssessmentItemsAttributes"];                        
                    }

                    SelectedQuestion selectedQuestions = (SelectedQuestion)System.Web.HttpContext.Current.Session["SelectedQuestionSequence"];
                    //--------------------------------------------------------




                    // LCMS-9213 (getting assessment item answer IDs against guids in a hashtable)
                    // -----------------------------------------------------------------
                    string correctAnswerGuids = "";
                    
                    
                    for (int i = 0; i < previouslyAskedQuestionsWithAttr.Count; i++)
                    {
                        //string correctAnswers = previouslyAskedQuestionsWithAttr[i].Split('|')[5];  // guid,blank
                        //string answerTexts = previouslyAskedQuestionsWithAttr[i].Split('|')[4]; // guid,blank

                        string correctAnswers = previouslyAskedQuestionsWithAttr[i].CorrectAnswerGuids;  // guid,blank
                        string answerTexts = previouslyAskedQuestionsWithAttr[i].AnswerTexts; // guid,blank


                        if (correctAnswers.EndsWith(","))
                        {
                            correctAnswers = correctAnswers.Substring(0, correctAnswers.Length - 1);
                        }

                        if (answerTexts.EndsWith(","))
                        {
                            answerTexts = answerTexts.Substring(0, answerTexts.Length - 1);
                        }


                        correctAnswers += "," + answerTexts;

                        if (correctAnswerGuids != "" && !correctAnswerGuids.EndsWith(","))
                        {
                            correctAnswerGuids += ",";
                        }

                        correctAnswerGuids += correctAnswers;                      
                    }

                    if (correctAnswerGuids.EndsWith(",") || correctAnswerGuids.EndsWith("|"))
                    {
                        correctAnswerGuids = correctAnswerGuids.Substring(0, correctAnswerGuids.Length - 1);
                    } 

                    correctAnswerGuids = "\'" + correctAnswerGuids.Replace(",", "\',\'") + "\'";
                    Hashtable ht = new Hashtable();
                    ht = new AssessmentManager().GetAssessmentAnswerItemIDsByGuid(correctAnswerGuids);
                    // -----------------------------------------------------------------




                    //// for answerreview fix
                    ////------------------------------------------------------
                    ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[] assessmentItems = (ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[])System.Web.HttpContext.Current.Session["AssessmentItemList"];                    
                    ////-------------------------------------------------------
                    

                    // LCMS-9213 (sorting & merging)
                    //-------------------------------------------------------------------------

                   List<string> notFoundQuestionGuids = new List<string>();
                    List<int> notFoundQuestionIndexes = new List<int>();




                    for (int i = 0; i < previouslyAskedQuestionsWithAttr.Count; i++)
                    {
                        //string assessmentItemGuid = previouslyAskedQuestionsWithAttr[i].Split('|')[0];
                        //string isCorrect = previouslyAskedQuestionsWithAttr[i].Split('|')[1];
                        //string isAnswered = previouslyAskedQuestionsWithAttr[i].Split('|')[2];

                        //string questionType = previouslyAskedQuestionsWithAttr[i].Split('|')[3];
                        //string[] answerTexts = previouslyAskedQuestionsWithAttr[i].Split('|')[4].Split(','); // guid,blank
                        //string[] correctAnswers = previouslyAskedQuestionsWithAttr[i].Split('|')[5].Split(',');  // guid,blank


                        string assessmentItemGuid = previouslyAskedQuestionsWithAttr[i].AssessmentItemID;
                        bool isCorrect = previouslyAskedQuestionsWithAttr[i].CorrectTF;
                        bool isAnswered = previouslyAskedQuestionsWithAttr[i].AnswerProvided;
                        bool isAssessmentItemToogled = previouslyAskedQuestionsWithAttr[i].IsAssessmentItemToogled;  //Abdus Samad LCMS-12105
                        

                        string questionType = previouslyAskedQuestionsWithAttr[i].QuestionType;
                        string[] answerTexts = previouslyAskedQuestionsWithAttr[i].AnswerTexts.Split(','); // guid,blank
                        string[] correctAnswers = previouslyAskedQuestionsWithAttr[i].CorrectAnswerGuids.Split(',');  // guid,blank
                        

                        bool questionFound = false;
                        for (int j = i; j < selectedQuestions.QuestionInfos.Count; j++)
                        {
                            if (selectedQuestions.QuestionInfos[j].QuestionGuid == assessmentItemGuid)
                            {
                                questionFound = true;
                                QuestionInfo q_temp = selectedQuestions.QuestionInfos[i];
                                selectedQuestions.QuestionInfos[i] = selectedQuestions.QuestionInfos[j];
                                selectedQuestions.QuestionInfos[j] = q_temp;

                                // For synch'ing assessmentItems with selectedQuestions
                                // --------------------------------------------
                                ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem a_temp = assessmentItems[i];
                                assessmentItems[i] = assessmentItems[j];
                                assessmentItems[j] = a_temp;
                                // --------------------------------------------



                            }
                        }

                        if (!questionFound)
                        { 
                            // replace question info with prev
                          //  QuestionInfo q_temp = new QuestionInfo();
                           // q_temp.QuestionGuid = assessmentItemGuid;

                            selectedQuestions.QuestionInfos[i].QuestionGuid = assessmentItemGuid;                            
                      

                            // adding it to collection for AssessmentItemList
                            //---------------------------
                            notFoundQuestionGuids.Add(assessmentItemGuid);
                            notFoundQuestionIndexes.Add(i);
                            //---------------------------

                        }

                        //selectedQuestions.QuestionInfos[i].IsCorrectlyAnswered = Convert.ToBoolean(isCorrect);
                        selectedQuestions.QuestionInfos[i].IsCorrectlyAnswered = isCorrect;
//                        selectedQuestions.QuestionInfos[i].IsSkipped = !Convert.ToBoolean(isAnswered);
                        
                        //Abdus Samad
                        //LCMS-12105
                        //Start
                        selectedQuestions.QuestionInfos[i].ToogleFlag = isAssessmentItemToogled;
                        //Stop


                        //if (!Convert.ToBoolean(isAnswered) && assessmentConfiguration.AllowSkippingQuestion && !assessmentConfiguration.ScoreAsYouGo)
                        if (!isAnswered && assessmentConfiguration.AllowSkippingQuestion && !assessmentConfiguration.ScoreAsYouGo)
                        {
                            selectedQuestions.QuestionInfos[i].IsSkipped = true;
                        }
                        else
                        {
                            selectedQuestions.QuestionInfos[i].IsSkipped = false;
                        }
                                                
                        for (int t = 0; t < answerTexts.Length; t++)
                        {
                            //object value = ht[answerTexts[t]];

                            bool isGuid = false;
                            try
                            {
                                //Guid g = (Guid)ht[answerTexts[t]];
                                Guid g = new Guid(answerTexts[t].ToString());
                                isGuid = true;
                            }
                            catch (Exception ex)
                            {
                                isGuid = false;
                            }

                            if (isGuid) // if (value != null)
                            {
                                // string ans_text = value.ToString();
                               // selectedQuestions.QuestionInfos[i].AnswerTexts.Add(ans_text);
                                //selectedQuestions.QuestionInfos[i].AnswerTexts.Add(ht[answerTexts[t]].ToString().Split('|')[1]);
                                selectedQuestions.QuestionInfos[i].AnswerIDs.Add(Convert.ToInt32(ht[answerTexts[t]].ToString().Split('|')[0]));
                                
                            }
                            else
                            {
                                selectedQuestions.QuestionInfos[i].AnswerTexts.Add(answerTexts[t]);
                            }
                        }
                

                    } //for

                    System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;
                    System.Web.HttpContext.Current.Session["AssessmentItemList"] = assessmentItems;



                    
                        // repalcing assessment item collection if question not found
                        //---------------------------------------------------------
                        if (notFoundQuestionGuids.Count > 0)
                        {
                            string questionGuids = "";
                            for (int i = 0; i < notFoundQuestionGuids.Count; i++)
                            {
                                if (questionGuids != "")
                                {
                                    questionGuids += ",";
                                }
                                questionGuids += "'" + notFoundQuestionGuids[i] + "'" ;
                            }

                            ICP4.BusinessLogic.ICPAssessmentService.AssessmentItem[] aItems = new AssessmentManager().GetAssessmentItemsByGUIDs(questionGuids);


                            for (int i = 0; i < notFoundQuestionGuids.Count; i++)
                            {

                                for (int j = 0; j < aItems.Length; j++)
                                {
                                    if (aItems[j].AssessmentItemGuid == notFoundQuestionGuids[i])
                                    {
                                        assessmentItems[notFoundQuestionIndexes[i]] = aItems[j];
                                        selectedQuestions.QuestionInfos[notFoundQuestionIndexes[i]].QuestionID = aItems[j].AssessmentItemID;
                                        selectedQuestions.QuestionInfos[notFoundQuestionIndexes[i]].QuestionType = aItems[j].QuestionType;                                        
                                        break;
                                    }
                                }
                                //notFoundQuestionGuids
                            }
                            
                            System.Web.HttpContext.Current.Session["SelectedQuestionSequence"] = selectedQuestions;
                            System.Web.HttpContext.Current.Session["AssessmentItemList"] = assessmentItems;
                        }
                      //---------------------------------------------------------   




                    // LCMS-9213 (for adding up all the last resume attempts in terms of individual questions and time)
                    //------------------------------------------------------------------------------------
                    if((System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"].ToString()=="True" || assessmentConfiguration.AllowPauseResumeAssessment) && (Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) > 0))
                    {
                        
                        AssessmentManager assessmentMgr = new AssessmentManager();
                        bool isPreview = true;
                        bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);

                        if (!isPreview)
                        {
                            int currAttemptNo = 1;
                            int.TryParse(HttpContext.Current.Session["LastAssessmentAttemptNo"].ToString(), out currAttemptNo);

                            long learnerStatistics_Id = assessmentMgr.SaveAssessmentEndTrackingInfo_PauseResume(selectedQuestions, currAttemptNo, 0, previouslyAskedQuestionsWithAttr[0].TimeInSeconds);
                            
                            for (int questionIndex = 0; questionIndex < Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]); questionIndex++)
                            {
                                SelectedQuestion singleSelectedQuestions = new SelectedQuestion();
                                singleSelectedQuestions.AssessmentParentId = selectedQuestions.AssessmentParentId;
                                singleSelectedQuestions.AssessmentType = selectedQuestions.AssessmentType;
                                singleSelectedQuestions.QuestionInfos.Add(selectedQuestions.QuestionInfos[questionIndex]);
                                assessmentMgr.SaveQuestionTrackingInfo(singleSelectedQuestions, learnerStatistics_Id, true);
                            }
                            

                        }
                    }
                    //-----------------------------------------------------------------------------------

                    int enrollmentId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                    CourseManager CM = new CourseManager();
                    bool isSuccess = CM.UpdateCourseStatusDuringAssessment(courseID, enrollmentId);

                    commandObject = assessmentManager.GetNextQuestion();

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    using (CourseManager courseManager = new CourseManager())
                    {
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {

                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
            //Since we havent implement any assessment, so just call Next method and skip the assessment :)
            //return Next(); 
        }

        [WebMethod]
        public static string IsProctorLockedCourse()
        {
            using (ProctorManagement proctorManagement = new ProctorManagement())
            {
                if (proctorManagement.IsLockCourse(System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString()))
                {
                    return JavaScriptConvert.SerializeObject(null);
                }
                else
                {
                    object commandObject = System.Web.HttpContext.Current.Session["CurrentCommand"];
                    System.Web.HttpContext.Current.Session.Remove("CurrentCommand");
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
        }


        [WebMethod]
        public static string GetNextQuestion()
        {
            try
            {
                object commandObject = null;

                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    if (System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] == null)
                    {
                        commandObject = assessmentManager.GetNextQuestion();
                    }
                    else
                    {
                        using (ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager knowledgeCheckManager = new ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager())
                        {
                            //Change Made by Waqas Zakai 1st March 2011
                            // LCMS-6461
                            // START
                            using (CourseManager courseManager = new CourseManager())
                            {
                                if (courseManager.IsCoursePublished() == true)
                                {
                                    object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                                    courseManager.SessionAbandonOnScene();
                                    return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                                }
                            }
                            //END
                            commandObject = knowledgeCheckManager.GetNextKnowledgeCheckQuestion();
                        }
                    }

                    using (ProctorManagement proctorManagement = new ProctorManagement())
                    {
                        commandObject = proctorManagement.IsLockCourse(commandObject, System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString());
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                   // return JavaScriptConvert.SerializeObject(commandObject);

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    using (CourseManager courseManager = new CourseManager())
                    {
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }
            //Since we havent implement any assessment, so just call Next method and skip the assessment :)
            //return Next();
        }


        [WebMethod]
        //public static string SubmitQuestionResult(int assessmentItemID,string assessmentAnswerIDs, string assessmentAnswerStrings)
        public static string SubmitQuestionResult(int assessmentItemID,string[] assessmentAnswerIDs, string[] assessmentAnswerStrings, string isSkiping, bool toogleFlag)
        {
            try
            {
                

                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);

                    //object studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerIDs);
                    //Newtonsoft.Json.Linq.JArray answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    List<int> studentAnswerIDArray = new List<int>();

                    for (int i = 0; i < assessmentAnswerIDs.Length; i++)
			       {

                         int value;
                        try
                        {
                            value = Convert.ToInt32(assessmentAnswerIDs[i].ToString().Replace("\"", ""));
                        }
                        catch
                        {
                            value = 0;
                        }
                        if (value != 0)
                            studentAnswerIDArray.Add(value);
			 
			       }
                    
                    //assessmentAnswerStrings= System.Web.HttpContext.Current.Server.HtmlEncode(assessmentAnswerStrings);
                   // studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerStrings);
                    //answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    List<string> studentAnswerStringArray = new List<string>();

                                       
                    for (int i = 0; i <assessmentAnswerStrings.Length; i++)
                    {
                        if (assessmentAnswerStrings[i]==null)
                        {
                            assessmentAnswerStrings[i] ="null";
                        }
                             string studentAnswerString = assessmentAnswerStrings[i].ToString().Replace("\"", "&quot;").Trim();
                            
                            studentAnswerString=HttpUtility.HtmlDecode(studentAnswerString);
                            studentAnswerString=HttpUtility.HtmlEncode(studentAnswerString);
                            
                        //if (studentAnswerString != "")
                            studentAnswerStringArray.Add(studentAnswerString);
                
                       
                            
                    }


                    object commandObject;
                    if (System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] == null)
                    {
                        int askedQuestionIndex = 0;
                        bool IsCorrectlyAnswered = assessmentManager.CheckQuestionResult(assessmentItemID, studentAnswerIDArray, studentAnswerStringArray, toogleFlag, ref askedQuestionIndex);

                        commandObject = assessmentManager.GetNextAvailableItem(courseID, courseSequenceIndex, askedQuestionIndex, Convert.ToBoolean(isSkiping));
                    }
                    else
                    {
                        using (ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager knowledgeCheckManager=new ICP4.BusinessLogic.KnowledgeCheckManager.KnowledgeCheckManager())
                        {
                            //Change Made by Waqas Zakai 1st March 2011
                            // LCMS-6461
                            // START
                            using (CourseManager courseManager = new CourseManager() )
                            {
                                if (courseManager.IsCoursePublished() == true)
                                {
                                    object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                                    courseManager.SessionAbandonOnScene(); 
                                    return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                                }
                            }
                            //END
                            commandObject= knowledgeCheckManager.SubmitQuestionResult(assessmentItemID, studentAnswerIDArray, studentAnswerStringArray);
                        }
                    }
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    using (CourseManager courseManager = new CourseManager())
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedAssessment);
                            //assessmentManager.SessionAbandonOnAssessment();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                        else
                        {
                            if (valiadtionTimerExpired == true)
                            {
                                commandObject = courseManager.StartValidation(commandObject);
                            }
                        }
                        //END
                    }
                    using (ProctorManagement proctorManagement = new ProctorManagement())
                    {
                        commandObject = proctorManagement.IsLockCourse(commandObject, System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString());
                        //return JavaScriptConvert.SerializeObject(commandObject);
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
                
                #region old Code
                /*
                //assessmentAnswerStrings = assessmentAnswerStrings.Replace("╦", "'");
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);

                    object studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerIDs);
                    Newtonsoft.Json.Linq.JArray answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    List<int> studentAnswerIDArray = new List<int>();
                    foreach (Newtonsoft.Json.Linq.JToken jToken in answerArray)
                    {
                        int value;
                        try
                        {
                            value = Convert.ToInt32(jToken.ToString().Replace("\"", ""));
                        }
                        catch
                        {
                            value = 0;
                        }
                        if (value != 0)
                            studentAnswerIDArray.Add(value);
                    }
                    //assessmentAnswerStrings= System.Web.HttpContext.Current.Server.HtmlEncode(assessmentAnswerStrings);
                    studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerStrings);
                    answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    List<string> studentAnswerStringArray = new List<string>();
                    foreach (Newtonsoft.Json.Linq.JToken jToken in answerArray)
                    {
                        string studentAnswerString = jToken.ToString().Replace("\"", "").Trim();
                        if (studentAnswerString != "")
                            studentAnswerStringArray.Add(studentAnswerString);
                    }


                    int askedQuestionIndex = 0;
                    bool IsCorrectlyAnswered = assessmentManager.CheckQuestionResult(assessmentItemID, studentAnswerIDArray, studentAnswerStringArray, ref askedQuestionIndex);

                    object commandObject = assessmentManager.GetNextAvailableItem(courseID, courseSequenceIndex, askedQuestionIndex);
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    using (CourseManager courseManager = new CourseManager())
                    {
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                return CreateExceptionCommandMessage(ex);
            }
              */ 
            #endregion 
        }

        [WebMethod]
        public static string AnswerRemainingQuestion()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    System.Web.HttpContext.Current.Session["AssessmentFlow"] = "AnswerRemainingQuestion";
                    //int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    //int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                    {
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedAssessment);
                            assessmentManager.SessionAbandonOnAssessment();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                    }
                    //END
                    object commandObject = assessmentManager.StartAskingSkippedQuestion(); 
                    using (CourseManager courseManager = new CourseManager())
                    {
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }


        }
        [WebMethod]
        public static string GradeAssessment(int assessmentItemID, string[] assessmentAnswerIDs, string[] assessmentAnswerStrings)
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int courseSequenceIndex = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    using (ICP4.BusinessLogic.CourseManager.CourseManager coursemanager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                    {
                        if (coursemanager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = coursemanager.CreateCourseLockedCommandObject(courseID, LockingReason.CoursePublishedAssessment);
                            //assessmentManager.SessionAbandonOnAssessment();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                    }
                    //END

                   // object studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerIDs);
                    //Newtonsoft.Json.Linq.JArray answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    List<int> studentAnswerIDArray = new List<int>();

                    for (int i = 0; i < assessmentAnswerIDs.Length; i++)
                    {
                        int value;
                        try
                        {
                            value = Convert.ToInt32(assessmentAnswerIDs[i].ToString().Replace("\"", ""));
                        }
                        catch
                        {
                            value = 0;
                        }
                        if (value != 0)
                            studentAnswerIDArray.Add(value);

                    }
                    //foreach (Newtonsoft.Json.Linq.JToken jToken in answerArray)
                    //{
                    //    int value;
                    //    try
                    //    {
                    //        value = Convert.ToInt32(jToken.ToString().Replace("\"", ""));
                    //    }
                    //    catch
                    //    {
                    //        value = 0;
                    //    }
                    //    if (value != 0)
                    //        studentAnswerIDArray.Add(value);
                    //}


                    List<string> studentAnswerStringArray = new List<string>();


                    for (int i = 0; i < assessmentAnswerStrings.Length; i++)
                    {
                        if (assessmentAnswerStrings[i] == null)
                        {
                            assessmentAnswerStrings[i] = "null";
                        }

                        string studentAnswerString = assessmentAnswerStrings[i].ToString().Replace("\"", "&quot;").Trim();
                        
                        studentAnswerString = HttpUtility.HtmlDecode(studentAnswerString);
                        studentAnswerString = HttpUtility.HtmlEncode(studentAnswerString);
                        
                        //if (studentAnswerString != "")
                                studentAnswerStringArray.Add(studentAnswerString);
                       
                    }


                    //studentAnswers = JavaScriptConvert.DeserializeObject(assessmentAnswerStrings);
                    //answerArray = (Newtonsoft.Json.Linq.JArray)studentAnswers;

                    //List<string> studentAnswerStringArray = new List<string>();
                    //foreach (Newtonsoft.Json.Linq.JToken jToken in answerArray)
                    //{
                    //    string studentAnswerString = jToken.ToString().Replace("\"", "").Trim();
                    //    if (studentAnswerString != "")
                    //        studentAnswerStringArray.Add(studentAnswerString);
                    //}


                    int askedQuestionIndex = 0;
                    bool IsCorrectlyAnswered = assessmentManager.CheckQuestionResult(assessmentItemID, studentAnswerIDArray, studentAnswerStringArray, false, ref askedQuestionIndex);
                    object commandObject = assessmentManager.GradeAssessment(courseID);

                    using (CourseManager courseManager = new CourseManager())
                    {
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }

        }
        [WebMethod]
        public static string AskSpecifiedQuestion(int assessmentItemID)
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    System.Web.HttpContext.Current.Session["AssessmentFlow"] = "AskSpecifiedQuestion";
                    object commandObject = assessmentManager.AskSpecifiedQuestion(assessmentItemID);

                    using (CourseManager courseManager = new CourseManager())
                    {
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                return CreateExceptionCommandMessage(ex);
            }
        }

        public static string CreateExceptionCommandMessage(Exception ex)
        {
            //This handling needs to be refactored
            if (System.Web.HttpContext.Current.Session["CourseID"] == null)
            {
                ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage showCustomMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage();
                using (CourseManager courseManager = new CourseManager())
                {
                    showCustomMessage = (ICP4.CommunicationLogic.CommunicationCommand.ShowCustomMessage.ShowCustomMessage)courseManager.CreateCustomeMessageForSessionEnd();
                }
                return JavaScriptConvert.SerializeObject(showCustomMessage);
            }
            else
            {
                ICP4.CommunicationLogic.CommunicationCommand.ShowErrorMessage.ShowErrorMessage showErrorMessage = new ICP4.CommunicationLogic.CommunicationCommand.ShowErrorMessage.ShowErrorMessage();
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    showErrorMessage = (ICP4.CommunicationLogic.CommunicationCommand.ShowErrorMessage.ShowErrorMessage)courseManager.CreateErrorMessage(courseID, ex.StackTrace);
                }
                return JavaScriptConvert.SerializeObject(showErrorMessage);
            }

        }

        [WebMethod]
        public static string AskValidationQuestion()
        {
            try
            {
                using (ValidationManager validationManager = new ValidationManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = validationManager.AskValidationQuestion(courseID);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string SubmitValidationQuestionResult(int validationQuestionId, string validationQuestionAnswer)
        {
            try
            {
                using (ValidationManager validationManager = new ValidationManager())
                {

                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    bool correctlyAnswer = validationManager.CheckValidationQuestionAnswer(validationQuestionId, validationQuestionAnswer);
                    object commandObject = validationManager.SetNextQuestion(courseID, correctlyAnswer);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ContinueAfterAssessment()
        {
            try
            {
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.ContinueAfterAssessment(courseID);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene(); 
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                    //END
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ContinueAfterAssessmentScore()
        {
            try
            {
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.ContinueAfterAssessmentScore(courseID);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                    //END
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ContinueGradingWithoutAnswering()
        {
            try
            {
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.ContinueGradingWithoutAnswering(courseID);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    using (ICP4.BusinessLogic.AssessmentManager.AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
                    {
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(courseID, LockingReason.CoursePublishedAssessment);
                            assessmentManager.SessionAbandonOnAssessment();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                    }
                    //END
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }




        [WebMethod]
        public static string ReturnToAssessmentResults()
        {
            try
            {
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.ReturnToAssessmentResults(courseID);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    using (ICP4.BusinessLogic.AssessmentManager.AssessmentManager assessmentManager = new ICP4.BusinessLogic.AssessmentManager.AssessmentManager())
                    {
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(courseID, LockingReason.CoursePublishedAssessment);
                            //assessmentManager.SessionAbandonOnAssessment();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                    }
                    //END
                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }



        [WebMethod]
        public static string FinishGradingAssessment()
        {
            try
            {
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.FinishGrading(courseID);

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }
                    
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ShowAnswers()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {

                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = 0;
                    object commandObject = assessmentManager.ShowAnswers();
                    using (CourseManager courseManager = new CourseManager())
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START    
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                            courseManager.SessionAbandonOnScene(); 
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                        //END
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }

        }

        [WebMethod]
        public static string GetNextRemidiationQuestion()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {

                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) + 1;
                    object commandObject = assessmentManager.GetNextBackRemidiationQuestion(1);

                    using (CourseManager courseManager = new CourseManager())
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START    
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                            courseManager.SessionAbandonOnScene();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                        //END

                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }

        }

        [WebMethod]
        public static string GetPreviousRemidiationQuestion()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"] = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedQuestionSequenceNo"]) - 1;
                    object commandObject = assessmentManager.GetNextBackRemidiationQuestion(-1);

                    using (CourseManager courseManager = new CourseManager())
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START    
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                            courseManager.SessionAbandonOnScene();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                        //END
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ShowSpecifiedRemidationQuestion(int assessmentItemID)
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = assessmentManager.ShowSpecifiedRemidationQuestion(assessmentItemID);

                    using (CourseManager courseManager = new CourseManager())
                    {
                        //Change Made by Waqas Zakai 1st March 2011
                        // LCMS-6461
                        // START    
                        if (courseManager.IsCoursePublished() == true)
                        {
                            object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                            courseManager.SessionAbandonOnScene();
                            return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                        }
                        //END
                        bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                        if (valiadtionTimerExpired == true)
                        {
                            commandObject = courseManager.StartValidation(commandObject);
                        }
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string ShowContent(int assessmentItemID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    //Assessmnet flow session is reused as it is no longer used when assessmnet is finished
                    System.Web.HttpContext.Current.Session["AssessmentFlow"] = "QuestionContentRemidiation";
                    System.Web.HttpContext.Current.Session["ContentRemidiationAssessmentID"] = assessmentItemID;
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = courseManager.ShowContent(assessmentItemID, courseID);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene(); 
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                    //END

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string GoContentTOQuestion()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int contentRemidiationAssessmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ContentRemidiationAssessmentID"]);
                    return ShowSpecifiedRemidationQuestion(contentRemidiationAssessmentID);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }


        public static object RemidationNextScene()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int contentRemidiationAssessmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ContentRemidiationAssessmentID"]);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return CoursePublishCommand;
                    }
                    //END

                    object commandObject = courseManager.NextBackRemidationScene(courseID, 1, contentRemidiationAssessmentID);

                    //bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    //if (valiadtionTimerExpired == true)
                    //{
                    //    commandObject = courseManager.StartValidation(commandObject);
                    //}

                    return commandObject;
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }


        public static object RemidationBackScene()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    int contentRemidiationAssessmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ContentRemidiationAssessmentID"]);

                    //Change Made by Waqas Zakai 1st March 2011
                    // LCMS-6461
                    // START    
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return CoursePublishCommand;
                    }
                    //END

                    object commandObject = courseManager.NextBackRemidationScene(courseID, -1, contentRemidiationAssessmentID);

                    bool valiadtionTimerExpired = Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]);
                    if (valiadtionTimerExpired == true)
                    {
                        commandObject = courseManager.StartValidation(commandObject);
                    }

                    return commandObject;
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string AssessmentTimerExpired()
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = assessmentManager.AssessmentTimerExpired();
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }
        [WebMethod]
        public static void ValidationTimerExpired()
        {

            using (CourseManager courseManager = new CourseManager())
            {
                courseManager.ValidationTimerExpired();
            }

        }
        [WebMethod]
        public static string GetValidationOrientationScene()
        {
            try
            {
                using (ValidationManager validationManager = new ValidationManager())
                {
                    object commandObject = validationManager.GetValidationOrientationScene();
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }
        [WebMethod]
        public static string ResumeCourseAfterValidation()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.ResumeCourseAfterValidation();
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }
        [WebMethod]
        public static void ContinueAfterEndOfCourse()
        {
            using (CourseManager courseManager = new CourseManager())
            {
                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

                object commandObject = courseManager.EndSession(courseID, learnerSessionID, DateTime.Now, DateTime.Now, false);

            }
            HttpContext.Current.Session.Add("isAbondoned", "true");
            HttpContext.Current.Session.Abandon();


        }
        [WebMethod]
        public static string BeginCourseEvaluation()
        {
            try
            {
                //Change Made by Waqas Zakai 1st March 2011
                // LCMS-6461
                // START
                using (CourseManager courseManager = new CourseManager())
                {
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                }
                //END
                object commandObject = null;
                //Get the command object of Course Evaluation Question
                using (CourseEvaluationManager courseEvalManager = new CourseEvaluationManager())
                {
                    commandObject = courseEvalManager.LoadCourseEvaluationInSession();
                }
                return JavaScriptConvert.SerializeObject(commandObject);
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string GetAndSubmitCourseEvaluation(string[] questiongIds, string[] answerIds, string[] questionTypes)
        {
            try
            {
                //Change Made by Waqas Zakai 1st March 2011
                // LCMS-6461
                // START    
                using (ICP4.BusinessLogic.CourseManager.CourseManager coursemanager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                    if (coursemanager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = coursemanager.CreateCourseLockedCommandObject(Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]), LockingReason.CoursePublishedScene);
                        coursemanager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                }
                //END

                object commandObject = null;
                //Get the command object of Course Evaluation Question
                using (CourseEvaluationManager courseEvalManager = new CourseEvaluationManager())
                {
                    commandObject = courseEvalManager.GetNextCourseEvaluationQuestionsList(questiongIds, answerIds,questionTypes);
                }
                return JavaScriptConvert.SerializeObject(commandObject);
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string SkipCourseEvaluation()
        {
            try
            {
                //Change Made by Waqas Zakai 1st March 2011
                // LCMS-6461
                // START
                using (CourseManager courseManager = new CourseManager())
                {
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                }
                //END

                using (CourseEvaluationManager courseEvaluationManager = new CourseEvaluationManager())
                {
                    object commandObject= courseEvaluationManager.SkipCourseEvaluation();
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string SkipPracticeExam()
        {
            try
            {
                //Change Made by Waqas Zakai 1st March 2011
                // LCMS-6461
                // START
                using (CourseManager courseManager = new CourseManager())
                {
                    if (courseManager.IsCoursePublished() == true)
                    {
                        object CoursePublishCommand = courseManager.CreateCourseLockedCommandObject(int.Parse(System.Web.HttpContext.Current.Session["CourseID"].ToString()), LockingReason.CoursePublishedScene);
                        courseManager.SessionAbandonOnScene();
                        return JavaScriptConvert.SerializeObject(CoursePublishCommand);
                    }
                }
                //END

                using (CourseManager courseManager = new CourseManager())
                {
                    object commandObject = courseManager.SkipPracticeExam();
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }
        ////////Ueeless code below///////////
        ////////Ueeless code below///////////
        ////////Ueeless code below///////////

        //[WebMethod]
        //public static string GetQuestion()
        //{
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion showQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion();
        //    showQuestion.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowQuestion;
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.TrueFalseQuestion trueFalseQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.TrueFalseQuestion();
        //    trueFalseQuestion.AssessmentItemGuid = "sdfsdf-3sdfs23423-234234";
        //    trueFalseQuestion.AssessmentItemID = 1;
        //    trueFalseQuestion.QuestionStem = "Are you pakistani?";
        //    trueFalseQuestion.AssessmentAnswers = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer> answerlist = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 1;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = true;
        //    answer.Label = "Yes";
        //    answer.Value = "Yes";




        //    answerlist.Add(answer);
        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 2;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "No";
        //    answer.Value = "No";
        //    answerlist.Add(answer);

        //    trueFalseQuestion.AssessmentAnswers = answerlist;



        //    showQuestion.AssessmentItem = trueFalseQuestion;

        //    string questionJSON = JavaScriptConvert.SerializeObject(showQuestion);

        //    return questionJSON;
        //}


        /*  Irfan Get Question Start*/



        //[WebMethod]
        //public static string GetQuestion4()
        //{
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion showQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion();
        //    showQuestion.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowQuestion;
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.MultipleChoiceQuestion multipleChoiceQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.MultipleChoiceQuestion();
        //    multipleChoiceQuestion.AssessmentItemGuid = "sdfsdf-3sdfs23423-234234";
        //    multipleChoiceQuestion.AssessmentItemID = 2;
        //    multipleChoiceQuestion.QuestionStem = "Who are pakistani presidents?";
        //    multipleChoiceQuestion.QuestionType = ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.QuestionType.MultipleSelectMCQ;



        //    multipleChoiceQuestion.AssessmentAnswers = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer> answerlist = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "kjsdbfkjsdbfkjsd";
        //    answer.AssessmentItemAnswerID = 1;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Pervaiz";
        //    answer.Value = "Pervaiz";
        //    answerlist.Add(answer);


        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "lishdddfoisdhfo";
        //    answer.AssessmentItemAnswerID = 2;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Zardari";
        //    answer.Value = "Zardari";
        //    answerlist.Add(answer);


        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "lishdfogdesisdhfo";
        //    answer.AssessmentItemAnswerID = 3;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Bush";
        //    answer.Value = "Bush";
        //    answerlist.Add(answer);


        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "lishdfofgfgisdhfo";
        //    answer.AssessmentItemAnswerID = 4;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Man Mohan";
        //    answer.Value = "Man Mohan";
        //    answerlist.Add(answer);

        //    multipleChoiceQuestion.AssessmentAnswers = answerlist;



        //    showQuestion.AssessmentItem = multipleChoiceQuestion;

        //    string questionJSON = JavaScriptConvert.SerializeObject(showQuestion);

        //    return questionJSON;
        //}

        //[WebMethod]
        //public static string GetQuestion()
        //{
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion showQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.ShowQuestion();
        //    showQuestion.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowQuestion;
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.SingleSelectQuestion singleSelectQuestion = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.SingleSelectQuestion();
        //    singleSelectQuestion.AssessmentItemGuid = "sdfsdf-3sdfs23423-23434";
        //    singleSelectQuestion.AssessmentItemID = 1;
        //    singleSelectQuestion.QuestionStem = "What is your nationality?";
        //    singleSelectQuestion.QuestionType = ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.QuestionType.SingleSelectMCQ;


        //    singleSelectQuestion.AssessmentAnswers = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer> answerlist = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer>();
        //    ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 1;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Pakistani";
        //    answer.Value = "Pakistani";
        //    answerlist.Add(answer);

        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 2;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Indian";
        //    answer.Value = "Indian";
        //    answerlist.Add(answer);


        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 3;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "American";
        //    answer.Value = "American";
        //    answerlist.Add(answer);


        //    answer = new ICP4.CommunicationLogic.CommunicationCommand.ShowQuestion.AssessmentItemAnswer();
        //    answer.AssessmentItemAnswerGuid = "asdasdas";
        //    answer.AssessmentItemAnswerID = 4;
        //    answer.DisplayOrder = 0;
        //    answer.IsCorrect = false;
        //    answer.Label = "Irani";
        //    answer.Value = "Irani";
        //    answerlist.Add(answer);

        //    singleSelectQuestion.AssessmentAnswers = answerlist;



        //    showQuestion.AssessmentItem = singleSelectQuestion;

        //    string questionJSON = JavaScriptConvert.SerializeObject(showQuestion);

        //    return questionJSON;
        //}

        //[WebMethod]
        //public static string SubmitMCQResult(int assessmentID, string assessmentAnswerIDs)
        //{
        //    string[] arrAnswerIds = assessmentAnswerIDs.Split(',');

        //    string data = "AnswerFeedBack";
        //    return data;
        //}

        [WebMethod]
        public static void SubmitQuestion(int questionId, string age)
        {
            object dd = new object();

            try
            {
                Newtonsoft.Json.Linq.JArray gg = (Newtonsoft.Json.Linq.JArray)JavaScriptConvert.DeserializeObject(age);

            }
            catch (Exception ex)
            {

            }

            string name1 = string.Empty;


            return;
        }

        [WebMethod]
        public static void SubmitQuestion1()
        {
            string name = string.Empty;
            return;
        }


        /* Irfan Get Question End*/






        [WebMethod]
        public static string GetDate()
        {
            return DateTime.Now.ToString();
        }


        [WebMethod]
        public static string MutliUser(int userID)
        {
            string mycontent;
            if (userID == 7)
            {
                System.Threading.Thread.Sleep(5000);
                mycontent = "now you are allowed to go";
            }
            else
            {
                mycontent = "OK you can go";
            }
            return mycontent;
        }

        [WebMethod] // adnan method.
        public static string ReceiveCommand(string command)
        {
            string url = "";
            if (command == "LOADME")
            {
                string myXMLString = "<Command CommandName=\"CourseLoad\"><CommandData><LoadMovie>lesson_2.swf</LoadMovie><IdleTimer>10</IdleTimer></CommandData></Command>";


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(myXMLString);

                string jsonText = JavaScriptConvert.SerializeXmlNode(doc);

                url = jsonText;

            }

            else if (command == "LOADTOCCONTENT")
                url = "lesson_2.swf";
            else if (command == "IDLETIMEOUT")
            {
                string myXMLString = "<Command CommandName=\"CourseEnd\"><CommandData><LoadMovie>1bc.swf</LoadMovie></CommandData></Command>";


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(myXMLString);

                string jsonText = JavaScriptConvert.SerializeXmlNode(doc);

                url = jsonText;

            }

            return url;
        }


        [WebMethod] // adnan method.
        public static string Ddothe(int myNumber)
        {
            return "";

        }


        //[WebMethod] // adnan method.
        //public static string Ddothe(int myNumber, string myJavaScript)
        //{
        //    string url = "";

        //        person myObject = new person();
        //        myObject.Age = 9;
        //        myObject.FirstName = "abc";

        //        myObject.LastName = "sdfsdf";
        //        myObject.MyChild = new System.Collections.Generic.List<person>();


        //        person myChildObject = new person();
        //        myChildObject.Age = 1;
        //        myChildObject.FirstName = "sss";
        //        myChildObject.LastName = "fff";
        //        myObject.MyChild = new System.Collections.Generic.List<person>();
        //        myObject.MyChild.Add(myChildObject);


        //        string ff = JavaScriptConvert.SerializeObject(myObject); 

        //        person myJavaObject = (person)JavaScriptConvert.DeserializeObject(myJavaScript, typeof(person));

        //    if (myJavaScript == "LOADME")
        //    {
        //        string myXMLString = "<Command CommandName=\"CourseLoad\"><CommandData><LoadMovie>lesson_2.swf</LoadMovie><IdleTimer>10</IdleTimer></CommandData></Command>";


        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(myXMLString);

        //        string jsonText = JavaScriptConvert.SerializeXmlNode(doc);

        //        url = jsonText;

        //    }
        //    else if (myJavaScript == "LOADTOCCONTENT")
        //        url = "lesson_2.swf";
        //    else if (myJavaScript == "IDLETIMEOUT")
        //    {
        //        string myXMLString = "<Command CommandName=\"CourseEnd\"><CommandData><LoadMovie>1bc.swf</LoadMovie></CommandData></Command>";


        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(myXMLString);

        //        string jsonText = JavaScriptConvert.SerializeXmlNode(doc);

        //        url = jsonText;

        //    }

        //    return url;
        //}

        /*
        [WebMethod]
        public static string RecieveCommand(string command)
        {
            string url = "";
            if (command == "LOADME")
                url = "lesson_2.swf";
            else if (command == "LOADTOCCONTENT")
                url = "lesson_2.swf";

            return url;
        }*/

        [WebMethod]
        public static string SendData(string hello, string world)
        {
            return "<p style=\"border:1px SOLID #FF0000;\"><h2>HEllow World</h2></p>";
        }

        [WebMethod]
        public static string SendXMLData()
        {
            /*string xmlString = "<Command CommandName=\"ShowSlide\"><CommandData><SlideMediaAsset MediaAssetID=\"3\" LessonID=\"1\">";
                   xmlString += "<MediaAssetURL>http://abc.com/info.swf</MediaAssetURL><LessonNo>1</LessonNo><SceneNo>1</SceneNo>";
                   xmlString += "<LastScene>1</LastScene><IsMovieEnded>1</IsMovieEnded><NextButtonState>1</NextButtonState></SlideMediaAsset></CommandData></Command>";
             */
            string xmlString = "<Command CommandName=\"ShowTableOfContent\"><CommandData><TableContentLesson LessonID=\"1\"><LessonName>lesson_2.swf</LessonName>";
            xmlString += "</TableContentLesson><TableContentLesson LessonID=\"2\"><LessonName>Falto Cocepts</LessonName></TableContentLesson><TableContentLesson LessonID=\"3\">";
            xmlString += "<LessonName>Marketing Rules</LessonName></TableContentLesson></CommandData></Command>";

            return xmlString;
        }

        [WebMethod]
        public static string LoadMovie(string command)
        {
            return command;
        }


        [WebMethod]
        public static void EmptyRequest()
        { }

        [WebMethod]
        public static string GetJSONString()
        {

            string myXMLString = "<Command CommandName=\"ShowTableOfContent\"><CommandData><TableContentLesson LessonID=\"1\"><LessonName>lesson_2.swf</LessonName>";
            myXMLString += "</TableContentLesson><TableContentLesson LessonID=\"2\"><LessonName>lesson_1.swf</LessonName></TableContentLesson><TableContentLesson LessonID=\"3\">";
            myXMLString += "<LessonName>Marketing Rules</LessonName></TableContentLesson></CommandData></Command>";

            //string myXMLString = "<Command CommandName=\"ShowTableOfContent\"><CommandData><TableContentLesson LessonID=\"1\"><LessonName>Marketing Concepts</LessonName></TableContentLesson><TableContentLesson LessonID=\"2\"><LessonName>Falto Concepts</LessonName></TableContentLesson></CommandData></Command>";


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(myXMLString);

            string jsonText = JavaScriptConvert.SerializeXmlNode(doc);

            return jsonText;

            //return DateTime.Now.ToString();
        }


        [WebMethod]
        public static string ShowCourseEvaluation()
        {
            try
            {

                ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationRoot
                    courseEvaluationRoot = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationRoot();

                ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationQuestions
                    CourseEvaluationQuestion1 = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationQuestions();
                CourseEvaluationQuestion1.Alignment = "horizontal";
                CourseEvaluationQuestion1.Id = 100;
                CourseEvaluationQuestion1.Text = "What is the color of Sun and Flower?";
                CourseEvaluationQuestion1.Required = false;
                CourseEvaluationQuestion1.Quetiontype = "MSSQ";
                CourseEvaluationQuestion1.DisplayOrder = 1;

                CourseEvaluationQuestion1.CourseEvaluationAnswer = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationAnswer>();
                ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationAnswer courseEvaluationAnswer1
                    = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationAnswer();
                courseEvaluationAnswer1.Id = 1001;
                courseEvaluationAnswer1.Label = "Green";
                courseEvaluationAnswer1.Displayorder = "0";

                ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationAnswer courseEvaluationAnswer2
                                    = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationAnswer();
                courseEvaluationAnswer2.Id = 1002;
                courseEvaluationAnswer2.Label = "Blue";
                courseEvaluationAnswer2.Displayorder = "0";
                
                CourseEvaluationQuestion1.CourseEvaluationAnswer.Add(courseEvaluationAnswer1);
                CourseEvaluationQuestion1.CourseEvaluationAnswer.Add(courseEvaluationAnswer2);

                courseEvaluationRoot.CourseEvaluationQuestions = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.CourseEvaluationQuestions>();
                courseEvaluationRoot.CourseEvaluationQuestions.Add(CourseEvaluationQuestion1);


                ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.ShowCourseEvaluationQuestions
                    showCourseEvaluationQuestions = new ICP4.CommunicationLogic.CommunicationCommand.ShowCourseEvaluationQuestions.ShowCourseEvaluationQuestions();
                showCourseEvaluationQuestions.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowCourseEvaluationQuestions;
                showCourseEvaluationQuestions.CourseEvaluation = courseEvaluationRoot;


                object commandObject = showCourseEvaluationQuestions;

                return JavaScriptConvert.SerializeObject(commandObject);
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);

            }
        }

        [WebMethod]
        public static string GetIdleTimeVariables()
        {

            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
            string[] s = new string[4];

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
            ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);

          
            s[0] = courseConfig.PlayerIdleUserTimeout.ToString();
            s[1] = (Convert.ToInt32(ConfigurationManager.AppSettings["IdleUserTimeWarningDurationInSeconds"])).ToString();
            s[2] = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.HeadingIdleTime, brandCode, variant);


            if (courseConfig.ActionToTakeUponIdleTimeOut.ToLower().StartsWith("lock"))
            {
                s[3] = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ContentIdleTimeLockCourse, brandCode, variant);
            }
            else
            {
                s[3] = cacheManager.GetResourceValueByResourceKey(ICP4.BusinessLogic.BrandManager.ResourceKeyNames.ContentIdleTime, brandCode, variant);
            }

            


            return s[0] + "||" + s[1] + "||" + s[2] + "||" + s[3];
        }



        [WebMethod]
        public static string CheckThingsBeforeCallingIdleTimeWarningPopup()
        {
            if (System.Web.HttpContext.Current.Session["KnowledgeCheckInProgress"] == null)
            { return "true"; }


            if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["ValidationTimerExpired"]) == null) 
            { return "true"; }

            return "false";
        }


        [WebMethod]
        public static void SynchToExternalSystem(string mileStone)
        {
            CourseManager courseManager = new CourseManager();
            courseManager.SynchToExternalSystem(mileStone);

        }

        [WebMethod]
        public static string GetCourseCompletionReport()
        {
            CourseManager courseManager = new CourseManager();
            object commandObject = courseManager.GetCourseCompletionReport();
            return JavaScriptConvert.SerializeObject(commandObject);
        }

        [WebMethod]
        public static string GetCommandDueToIdleUserTimeout()
        {
            ICP4.BusinessLogic.CacheManager.CacheManager ch = new ICP4.BusinessLogic.CacheManager.CacheManager();
         
            CourseManager c = new CourseManager();
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);

            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
            string actionToTakeOnIdleUserTimeout = ch.GetIFConfigurationExistInCache(courseConfigurationID).ActionToTakeUponIdleTimeOut;            
            object commandObject = null;
            string assessmentStage = Convert.ToString(System.Web.HttpContext.Current.Session["AssessmentStage"]);
            
            if (actionToTakeOnIdleUserTimeout == "Lock Course") 
            {
                string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();                
                int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
                int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                c.EndSessionForAssessment(courseID, learnerSessionID, DateTime.Now);
                commandObject = c.CreateCourseLockedCommandObject(courseID, LockingReason.IdleUserTimeElapsed);
                c.LockCourse(courseID, learnerID, enrollmentID, LockingReason.IdleUserTimeElapsed);
                logoutCoursePlayerIntegeration(null);
                LogoutCoursePlayer(null);
                return JavaScriptConvert.SerializeObject(commandObject);
            }
            else // No Action or Assessment is not in progress
            {
                logoutCoursePlayerIntegeration(null);
                return LogoutCoursePlayer(null);
            }
            return null;
        }



        [WebMethod]
        public static void NoteTime()
        {
            System.Web.HttpContext.Current.Session["LastConnectedTime"] = DateTime.Now;            
        }

        [WebMethod]
        public static string CourseApprovalCheck(string learnerSessionID, string brandCode, string variant, int courseID, bool isdemo, bool isRedirect, bool isPreview, string courseGUID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString());
                    if (source == 0)
                    {
                        if (courseManager.CheckLearnerCourseApproval(courseID, learnerSessionID) == false)
                        {
                            object commandObject = courseManager.CreateCourseApprovalCommandObject(courseID, learnerSessionID);
                            if (commandObject != null)
                            {
                                System.Web.HttpContext.Current.Session["IsCourseApprovalSelection"] = true;
                                return JavaScriptConvert.SerializeObject(commandObject);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        [WebMethod]
        public static string SaveLearnerCourseApproval(string learnerSessionID, int courseID, int courseApprovalID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    courseManager.SaveLearnerCourseApproval(courseID, learnerSessionID, courseApprovalID);
                    //System.Web.HttpContext.Current.Session["IsCourseApprovalSelection"] = false;
                    return null;
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        [WebMethod]
        public static string ContinueAfterAffidavit()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    //int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    //int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    //System.Web.HttpContext.Current.Session["CurrentIndex"] = seqNo - 1;
                    //System.Web.HttpContext.Current.Session["AskedCourseApprovalAffidavit"] = true;
                    //object commandObject = new object();
                    //commandObject = courseManager.NextBack(courseID, 1); 
                    object assessmentStartCommand = System.Web.HttpContext.Current.Session["CurrentCommandProctor"];
                    //System.Web.HttpContext.Current.Session.Remove("CurrentCommandProctor");                    
                    //return JavaScriptConvert.SerializeObject(assessmentStartCommand);
                    System.Web.HttpContext.Current.Session["isAffidavitChecked"] = true;
                    courseManager.SaveAffidavitAcknowledgmentStatus(true);
                    return JavaScriptConvert.SerializeObject(courseManager.SpecialPostAssessmentValidation(assessmentStartCommand)); 

                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception(ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        [WebMethod]
        public static string SaveLearnerCourseMessage(string learnerSessionID, int courseID)
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    courseManager.SaveLearnerCourseMessage(courseID, learnerSessionID);                    
                    return null;
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        //LCMS-11281

        [WebMethod]
        public static string ContinueAfterDocuSignRequirementAffidavit()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {
                    /*
                     
                    //int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    //int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    //System.Web.HttpContext.Current.Session["CurrentIndex"] = seqNo - 1;
                    //System.Web.HttpContext.Current.Session["AskedCourseApprovalAffidavit"] = true;
                    //object commandObject = new object();
                    //commandObject = courseManager.NextBack(courseID, 1); 
                    object assessmentStartCommand = System.Web.HttpContext.Current.Session["CurrentCommandDocuSign"];
                    //System.Web.HttpContext.Current.Session.Remove("CurrentCommandProctor");                    
                    //return JavaScriptConvert.SerializeObject(assessmentStartCommand);
                    return JavaScriptConvert.SerializeObject(courseManager.SpecialPostAssessmentValidation(assessmentStartCommand));
                   
                     */
                    System.Web.HttpContext.Current.Session["isDisplayTextCrossed"] = true;
                    int seqNo = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentIndex"]);
                    int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                    object commandObject = new object();
                    commandObject=courseManager.GetCommandObjectIfEndofCoruse(courseID,seqNo);
                    if (commandObject == null)
                    {
                        commandObject = courseManager.NextBack(courseID, 1);
                    }
                    return JavaScriptConvert.SerializeObject(commandObject);

                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception(ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        //End

        #region Course Player Rating 11877
        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SaveCourseRatingNPS(short NPS_RATING, string USER_REVIEW_TEXT, short RATING_SHOPPINGEXP, short RATING_COURSE, short RATING_LEARNINGTECH, short RATING_CS, string RATING_SHOPPINGEXP_SECONDARY, string RATING_COURSE_SECONDARY, string RATING_LEARNINGTECH_SECONDARY, string RATING_COURSE_SECONDARY_Q, string RATING_CS_SECONDARY, string RATING_SHOPPINGEXP_SECONDARY_Q, string RATING_LEARNINGTECH_SECONDARY_Q, string RATING_CS_SECONDARY_Q)
        {

            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int iResult = 0;
            string CourseGuid = "";
            double AvgRating = 0;
            int TotalRating = 0;

            try
            {
                using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    ICP4.BusinessLogic.ICPTrackingService.CourseRating courseRating = new ICP4.BusinessLogic.ICPTrackingService.CourseRating();
                    
                    courseRating.NPS_RATING=NPS_RATING;
                    courseRating.USER_REVIEW_TEXT = USER_REVIEW_TEXT;
                    courseRating.RATING_COURSE=RATING_COURSE;
                    courseRating.RATING_SHOPPINGEXP = RATING_SHOPPINGEXP;
                    courseRating.RATING_LEARNINGTECH =RATING_LEARNINGTECH;
                    courseRating.RATING_CS = RATING_CS;
                    courseRating.RATING_COURSE_SECONDARY = RATING_COURSE_SECONDARY;
                    courseRating.RATING_SHOPPINGEXP_SECONDARY = RATING_SHOPPINGEXP_SECONDARY;
                    courseRating.RATING_LEARNINGTECH_SECONDARY = RATING_LEARNINGTECH_SECONDARY;
                    courseRating.RATING_CS_SECONDARY = RATING_CS_SECONDARY;

                    courseRating.RATING_COURSE_SECONDARY_Q = RATING_COURSE_SECONDARY_Q;
                    courseRating.RATING_SHOPPINGEXP_SECONDARY_Q = RATING_SHOPPINGEXP_SECONDARY_Q;
                    courseRating.RATING_LEARNINGTECH_SECONDARY_Q = RATING_LEARNINGTECH_SECONDARY_Q;
                    courseRating.RATING_CS_SECONDARY_Q = RATING_CS_SECONDARY_Q;
                    courseRating.CourseID = courseID;
                    courseRating.EnrollmentID = enrollmentID;

                    courseRating = trackingService.SaveCourseRatingNPS(courseRating);

                    /*CourseLevelRatingWeb.CourseLevelRatingService clrService = new ICP4.CoursePlayer.CourseLevelRatingWeb.CourseLevelRatingService();
                    CourseLevelRatingWeb.CLRRequestType request = new ICP4.CoursePlayer.CourseLevelRatingWeb.CLRRequestType();
                    request.avgRating = AvgRating;
                    request.courseGuid = CourseGuid;
                    request.ratingCount = TotalRating;
                    */
                    //CourseLevelRatingWeb.CourseLevelRatingResponse response = clrService.NewOperation(request);
                    CourseGuid = courseRating.CourseGuid;
                    AvgRating = courseRating.AvgRating;
                    TotalRating = courseRating.TotalRating;

                    SyncCourseRatingWithSF(AvgRating, CourseGuid, TotalRating);

                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }

            return new JavaScriptSerializer().Serialize(iResult.ToString());

        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SaveCourseRating(int Rating)
        {
            //in actual both will be taken from session
            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int iResult = 0;
            string CourseGuid = "";
            double AvgRating = 0;
            int TotalRating = 0;

            try
            {
                using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    iResult = trackingService.SaveCourseRating(courseID, Rating, enrollmentID, out CourseGuid, out AvgRating, out TotalRating);

                    /*CourseLevelRatingWeb.CourseLevelRatingService clrService = new ICP4.CoursePlayer.CourseLevelRatingWeb.CourseLevelRatingService();
                    CourseLevelRatingWeb.CLRRequestType request = new ICP4.CoursePlayer.CourseLevelRatingWeb.CLRRequestType();
                    request.avgRating = AvgRating;
                    request.courseGuid = CourseGuid;
                    request.ratingCount = TotalRating;
                    */
                    //CourseLevelRatingWeb.CourseLevelRatingResponse response = clrService.NewOperation(request);                   
                    SyncCourseRatingWithSF(AvgRating, CourseGuid, TotalRating);

                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
            }

            return new JavaScriptSerializer().Serialize(iResult.ToString());
        }

        public static bool SyncCourseRatingWithSF(double AvgRating, string CourseGuid, int TotalRating)
        {
            try
            {
                string responseStatus = "";
                    string userName = System.Configuration.ConfigurationSettings.AppSettings["SFUserNameForPublishing"];

                    string password = System.Configuration.ConfigurationSettings.AppSettings["SFPasswordForPublishing"];
                    string soap = "" +
                    @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cour=""http://www.threesixtytraining.com/CourseCatalog.xsd"">" +
                    @"<soapenv:Header xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">" +
                    @"<wsse:Security soap:mustUnderstand=""1"" xmlns:soap=""http://schemas.xmlsoap.org/wsdl/soap/"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"">" +
                    "<wsse:UsernameToken>" +
                    "<wsse:Username>" + userName + "</wsse:Username>" +
                    @"<wsse:Password Type= ""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">" + password + "</wsse:Password>" +
                    "</wsse:UsernameToken>" +
                    "</wsse:Security>" +
                    "</soapenv:Header>" +
                    @"<soapenv:Body xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">" +
                    @"<CourseLevelRatingRequest xmlns=""http://www.example.org/CourseLevelRatingService"">" +
                    "<courseGuid>" + CourseGuid + "</courseGuid>" +
                    "<ratingCount>" + TotalRating + "</ratingCount>" +
                    "<avgRating>" + AvgRating + "</avgRating>" +
                    "</CourseLevelRatingRequest>" +
                    "</soapenv:Body>" +
                    "</soapenv:Envelope>";

                    HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["StoreFrontServiceCallsURL_Rating"]);
                    web_request.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    web_request.Headers.Add("SOAPAction", "\"NewOperation\"");
                    web_request.ContentType = "text/xml;charset=\"utf-8\"";
                    web_request.Accept = "text/xml";
                    web_request.Method = "POST";

                    using (Stream stm = web_request.GetRequestStream())
                    {
                        using (StreamWriter stmw = new StreamWriter(stm))
                        {
                            stmw.Write(soap);
                        }
                    }

                    WebResponse web_response = null;
                    StreamReader reader = null;
                    web_response = web_request.GetResponse();
                    Stream responseStream = web_response.GetResponseStream();
                    XmlDocument xml = new XmlDocument();

                    reader = new StreamReader(responseStream);
                    xml.LoadXml(reader.ReadToEnd());

                    return true;

                }
                catch (Exception exp)
                {
                    ExceptionPolicyForLCMS.HandleException(exp, "ICPException");
                    return false;
                }
            }
        

        //LCMS-12532 Yasin
        [WebMethod]
        public static string SaveValidationIdendityQuestions(int QS1, string Answer1, int QS2, string Answer2,int QS3, string Answer3,int QS4, string Answer4,int QS5, string Answer5)
        {
            string sReturn = "";
            int QuestionSet1 = 0;
            int QuestionSet2 = 0;
            int QuestionSet3 = 0;
            int QuestionSet4 = 0;
            int QuestionSet5 = 0;
            bool iResult = false;
            int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);
            int CourseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
            int secondsSinceLastValidation = 0;
            int unansweredQuestionID = 0;
            object commandObject = null;
            if (QS1 == 101 || QS1 == 102 || QS1 == 103)
            {
                QuestionSet1 = 1;
            }
            if (QS2 == 104 || QS2 == 105 || QS2 == 106)
            {
                QuestionSet2 = 2;
            }
            if (QS3 == 107 || QS3 == 108 || QS3 == 109)
            {
                QuestionSet3 = 3;
            }
            if (QS4 == 110 || QS4 == 111 || QS4 == 112)
            {
                QuestionSet4 = 4;
            }
            if (QS5 == 113 || QS5 == 114 || QS5 == 115)
            {
                QuestionSet5 = 5;
            }
            try
            {
                using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    iResult = trackingService.SaveValidationIdendityQuestions(QS1, Answer1, QS2, Answer2, QS3, Answer3, QS4, Answer4, QS5, Answer5, learnerID, QuestionSet1, QuestionSet2, QuestionSet3, QuestionSet4, QuestionSet5);
                    using (ValidationManager validationManager = new ICP4.BusinessLogic.ValidationManager.ValidationManager())
                    {
                        if (validationManager.LoadValidationQuestions(CourseID, learnerID, enrollmentID, out secondsSinceLastValidation, 0, out unansweredQuestionID) == true)
                        {
                        
                        }
                    }

                
                    if (iResult == true)
                    {
                        return  Next();
                    }
                    /*
                     * Code Review : Call next method here , this automatically load the current command.
                     */

                }
                return new JavaScriptSerializer().Serialize(commandObject);
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);
            }

          
            //return new JavaScriptSerializer().Serialize(iResult.ToString());

           //  return null;
        }
        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetCourseRatingCount()
        {
            //in actual both will be taken from session
            //int CourseID = 623;
            //int EnrollmentID = 138674;
             int iResult = 0;

             //using (ICP4.BusinessLogic.ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
             //{
                 
             //}

            //TrackingService t = new TrackingService();
            //int iResult = t.GetCourseRatingCount(CourseID, EnrollmentID);
            return iResult.ToString();

            // return new JavaScriptSerializer().Serialize(iResult.ToString());
        }
        #endregion

        //LCMS-11283

        [WebMethod]
        public static string ContinueAfterDocuSignProcess()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {

                    object docuSignProcessCommand = System.Web.HttpContext.Current.Session["CurrentCommandDocuSignProcess"];

                    return JavaScriptConvert.SerializeObject(courseManager.SpecialLoadCourseCertificateCommand(docuSignProcessCommand));

                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception(ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        //End

        [WebMethod]
        public static string CourseLockDueToInActiveCurrentWindow()
        {
            try
            {
                using (CourseManager courseManager = new CourseManager())
                {   
                    bool isPreview = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isPreview"]);
                    if (System.Web.HttpContext.Current.Session["AssessmentStage"] != null && System.Web.HttpContext.Current.Session["AssessmentStage"].ToString().Equals("AssessmentIsInProgress") && isPreview == false)
                    {
                        object commandObject = null;                        
                        int learnerID = Convert.ToInt32(System.Web.HttpContext.Current.Session["LearnerID"]);                
                        int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                        //LCMS-11614 Abdus Samad 
                        //Start
                        string learnerSessionID = System.Web.HttpContext.Current.Session["LearnerSessionID"].ToString();
                        if (System.Web.HttpContext.Current.Session["AssessmentStage"] !=null && System.Web.HttpContext.Current.Session["AssessmentStage"].ToString().Equals("AssessmentIsInProgress"))
                        {
                            System.Web.HttpContext.Current.Session["AssessmentStageDuringLock"] = "true";
                            System.Web.HttpContext.Current.Session["CourseLockedDuringAssessment"]= "True";

                        }
                        courseManager.EndSession(courseID, learnerSessionID, DateTime.Now, DateTime.Now, false);
                        //End

                        courseManager.LockCourse(courseID, learnerID, enrollmentID, LockingReason.ClickingAwayFromActiveWindow);

                        if (System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"]!=null && System.Web.HttpContext.Current.Session["AssessmentEndStageDuringLock"].ToString().Equals("True"))
                            courseManager.UpdateCourseStatusDuringAssessment(courseID, enrollmentID);

                        commandObject = courseManager.CreateCourseLockedCommandObject(courseID, LockingReason.ClickingAwayFromActiveWindow);
                        return JavaScriptConvert.SerializeObject(commandObject);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception(ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }


        [WebMethod]
        public static string AuthenticateSpecialPostAssessmentValidation(string learnerSessionID, int courseID, string DRELicenseNumber, string DriverLicenseNumber)
        {
            try
            {
                object commandObject = null;
                using (CourseManager courseManager = new CourseManager())
                {
                    commandObject = courseManager.AuthenticateSpecialPostAssessmentValidation(learnerSessionID, HttpUtility.HtmlDecode(DRELicenseNumber), HttpUtility.HtmlDecode(DriverLicenseNumber));
                    return JavaScriptConvert.SerializeObject(commandObject);                   
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }

        [WebMethod]
        public static string CancelSpecialPostAssessmentValidation()
        {
            object commandObject = null;
            using (CourseManager courseManager = new CourseManager())
            {
                System.Web.HttpContext.Current.Session["CancelSpecialPostAssessmentValidation"] = true;
                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                commandObject = courseManager.NextBack(courseID, -1);
                System.Web.HttpContext.Current.Session["CancelSpecialPostAssessmentValidation"] = false;
                return JavaScriptConvert.SerializeObject(commandObject);
            }
        }

        [WebMethod]
        public static string AuthenticateNYInsuranceValidation(string learnerSessionID, int courseID, string MonitorNumber)
        {
            try
            {
                object commandObject = null;
                using (CourseManager courseManager = new CourseManager())
                {
                    commandObject = courseManager.AuthenticateNYInsuranceValidation(learnerSessionID, HttpUtility.HtmlDecode(MonitorNumber));
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {
                Exception ex1 = new Exception("learnerSessionID : " + learnerSessionID + " " + ex.Message, ex.InnerException);
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                return CreateExceptionCommandMessage(ex1);
            }
        }


        [WebMethod]
        public static string AuthenticateProctor(string proctorLogin, string proctorPassword)
        {
            object commandObject = null;
            CourseManager courseManager = new CourseManager();

            commandObject = courseManager.AuthenticateProctor(HttpUtility.HtmlDecode(proctorLogin), HttpUtility.HtmlDecode(proctorPassword));
            return JavaScriptConvert.SerializeObject(commandObject);

        }







        // LCMS-9882
        [WebMethod]
        public static string GetAssessmentItemsByAssessmentBankIDs(string assessmentBankIDs)
        {
            try
            {
                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = null;
                    commandObject = assessmentManager.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {

                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }


        }


        // LCMS-9882
        [WebMethod]
        public static string SaveAssessmentEndTrackingInfo_ForGameTemplate(string assessmentType, int noOfAnswersCorrect, int noOfAnswersInCorrect, int currentAttemptNo, double weightedScore, bool isCurrentAssessmentPassed, int masteryScore, int assessmentTimeInSeconds, int remediationCount)
        {
            try
            {

               bool isPreview = false;
               bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);
                if (isPreview)
               {return ""; }


                using (AssessmentManager assessmentManager = new AssessmentManager())
                {
                    object commandObject = null;
                    commandObject = assessmentManager.SaveAssessmentEndTrackingInfo_ForGameTemplate(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount);
                    return JavaScriptConvert.SerializeObject(commandObject);
                }
            }
            catch (Exception ex)
            {

                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }
        }

        // LCMS-10392
        [WebMethod]
        public static string GetAmazonAffiliatePanelData()
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["CourseConfigurationID"] != null)
                {
                    ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    string AffiliatePanelWSURL = ConfigurationManager.AppSettings["AffiliatePanelWSURL"];
                    bool isPreview = false;
                    bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);
                    if (courseConfig.PlayerShowAmazonAffiliatePanel && !isPreview)
                    {
                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                        using (CourseManager courseManager = new CourseManager())
                        {
                            string courseKeywords = courseManager.GetCourseKeywords(courseID);
                            bool showAffiliatePanel = !string.IsNullOrEmpty(courseKeywords);
                            courseKeywords = System.Web.HttpUtility.UrlEncode(courseKeywords);  //LCMS-11490
                            var data = new { ShowPanel = showAffiliatePanel, CourseKeywords = courseKeywords, AffiliatePanelWSURL = AffiliatePanelWSURL };
                            return JavaScriptConvert.SerializeObject(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return CreateExceptionCommandMessage(ex);

            }

            var dummyData = new { ShowPanel = false, CourseKeywords = string.Empty, AffiliatePanelWSURL = string.Empty };
            return JavaScriptConvert.SerializeObject(dummyData);
        }


        // Yasin LCMS-12519
        [WebMethod]
        public static string GetTimeoutValueForClickAwayLockout()
        {
            return System.Configuration.ConfigurationManager.AppSettings["timeoutValueForClickAwayLockout"];
        }

        // LCMS-11878
        [WebMethod]
        public static string GetCourseRecommendationPanelData()
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["CourseConfigurationID"] != null)
                {
                    ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
                    int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);


                    if (courseConfig.PlayerShowCoursesRecommendationPanel)
                    {
                        int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
                        string courseGUID = string.Empty;
                        string storeID = string.Empty;

                        using (ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                        {
                            courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                            courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                            courseGUID = courseService.GetCourseGUID(courseID);

                            storeID = courseService.GetCourseStoreId(courseID);

                        }

                        using (CourseManager courseManager = new CourseManager())
                        {

                            SuggestedCoursesDisplayService displayService = new SuggestedCoursesDisplayService();
                            SuggestedCoursesDisplayRequestType requestType = new SuggestedCoursesDisplayRequestType();

                            string[] arrayOfCourseGuid = new string[1];

                            arrayOfCourseGuid[0] = courseGUID;
                            //arrayOfCourseGuid[0] = "0-15-15206001CD";

                            requestType.courseGuids = arrayOfCourseGuid;
                            // String Array

                            requestType.storeId = storeID;
                            //requestType.storeId = "0";
                            //int

                            SuggestedCoursesDisplayRequest displayRequest = new SuggestedCoursesDisplayRequest();
                            displayRequest.@in = requestType;


                            SuggestedCoursesDisplayResponseType lsts = displayService.GetSuggestedCourses(displayRequest);

                            CourseInfo[] response = lsts.CourseList;


                            string myCodes = string.Empty; // Initialize a string to hold the comma-delimited data as empty

                            List<string> courseGuids = new List<string>();
                            foreach (CourseInfo courseinfo in response)
                            {
                                courseGuids.Add(courseinfo.courseGuidTo.ToString());
                            }


                            ICP4.BusinessLogic.ICPCourseService.SuggestedCourse[] suggestedCourseList = null;

                            using (ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
                            {
                                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                                
                                suggestedCourseList = courseService.GetCourseNameAgainstCourseGuids(courseGuids.ToArray());
                            }

                            if (suggestedCourseList != null)
                            {

                                foreach (ICP4.BusinessLogic.ICPCourseService.SuggestedCourse suggestedCourse in suggestedCourseList)
                                {
                                    CourseInfo courseinfo = GetCourseInfo(response, suggestedCourse.CourseGuid);
                                    suggestedCourse.CourseImageUrl = courseinfo.imageUrl;
                                    suggestedCourse.StoreAddress = courseinfo.orderItemURL;
                                }
                            }

                            string pInfo1 = JavaScriptConvert.SerializeObject(suggestedCourseList);

                            string pInfo2 = pInfo1.Replace("[","[{affiliateItems:[");

                            string pInfo3 = pInfo2.Replace("]", "]}]");
                                                        
                            var data = new { ShowPanel = true, CourseKeywords = pInfo3 };

                            return JavaScriptConvert.SerializeObject(data);

                        }

                    }

                   // return JavaScriptConvert.SerializeObject(null);
                }
            }
            catch (Exception ex)
            {

                //ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                //return CreateExceptionCommandMessage(ex);

                var dummyData1 = new { ShowPanel = true, CourseKeywords = string.Empty };
                return JavaScriptConvert.SerializeObject(dummyData1);

            }

            var dummyData2 = new { ShowPanel = false, CourseKeywords = string.Empty, AffiliatePanelWSURL = string.Empty };
            return JavaScriptConvert.SerializeObject(dummyData2);

        }

        private static CourseInfo GetCourseInfo(CourseInfo[] courseinfolist, string courseguid)
        {
            foreach (CourseInfo courseinfo in courseinfolist)
            {
                if (courseinfo.courseGuidTo == courseguid)
                    return courseinfo;
            }
            return null;
        }

        //Abdus Samad
        //LCMS-12526
        //Start

        [WebMethod]
        public static string GetChatForHelpSupport()
        {
            int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);

            CourseManager courseManager = new CourseManager();
            object commandObject = courseManager.GetLearnerInformationForChatForHelp(enrollmentID);
            return JavaScriptConvert.SerializeObject(commandObject);
        }

        [WebMethod]
        public static bool IsPreviewModeEnabled()
        {
            bool isPreview = false;
            bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);

            return isPreview;
        }
     
        //Abdus Samad LCMS-13553
        //Start
        private static void SaveStudentSpendTimeInCookie()
        {
            try
            {

                if (System.Web.HttpContext.Current.Session["AssetStartTime"].ToString() != null && System.Web.HttpContext.Current.Session["AssetStartTime"].ToString() != String.Empty)
                {
                    DateTime assetStartTime = Convert.ToDateTime(System.Web.HttpContext.Current.Session["AssetStartTime"]);

                    int timeInSeconds = Convert.ToInt32(Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Second, assetStartTime, DateTime.Now, Microsoft.VisualBasic.FirstDayOfWeek.System, Microsoft.VisualBasic.FirstWeekOfYear.System));

                    HttpCookie StudentCookies = new HttpCookie("StudentTimeSpendCookies");
                    StudentCookies.Value = Convert.ToString(timeInSeconds);
                    StudentCookies.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(StudentCookies);
                }

            }
            catch (Exception exp)
            {

            }

        }
        //Stop


      
        //Stop

        //public static string GetCourseRecommendationPanelData()
        //{
        //    try
        //    {
        //        if (System.Web.HttpContext.Current.Session["CourseConfigurationID"] != null)
        //        {
        //            ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
        //            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
        //            ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfig = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
        //            //string AffiliatePanelWSURL = ConfigurationManager.AppSettings["AffiliatePanelWSURL"];
        //            //bool isPreview = false;
        //            //bool.TryParse(System.Web.HttpContext.Current.Session["isPreview"].ToString(), out isPreview);
        //            if (courseConfig.PlayerShowCoursesRecommendationPanel)
        //            {
        //                int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"]);
        //                using (CourseManager courseManager = new CourseManager())
        //                {
        //                    //string courseKeywords = courseManager.GetCourseKeywords(courseID);
        //                    //bool showAffiliatePanel = !string.IsNullOrEmpty(courseKeywords);
        //                    //courseKeywords = System.Web.HttpUtility.UrlEncode(courseKeywords);  //LCMS-11490
        //                    //var data = new { ShowPanel = showAffiliatePanel, CourseKeywords = courseKeywords, AffiliatePanelWSURL = AffiliatePanelWSURL };
        //                    //return JavaScriptConvert.SerializeObject(data);

        //                    Service webservice = new Service();

        //                    object commandObject = webservice.ListAllCourses();

        //                    var data = new { ShowPanel = true, CourseKeywords = JavaScriptConvert.SerializeObject(commandObject) };
        //                    return JavaScriptConvert.SerializeObject(data);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
        //        return CreateExceptionCommandMessage(ex);

        //    }

        //    var dummyData1 = new { ShowPanel = false, CourseKeywords = string.Empty, AffiliatePanelWSURL = string.Empty };
        //    return JavaScriptConvert.SerializeObject(dummyData1);

        //}



      



    }
}


