using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ICP4.CoursePlayer.DocuSignAPI;
using ICP4.CoursePlayer.CredentialAPI;
using System.Collections;
using System.Collections.Generic;
using _360Training.BusinessEntities;
using ICP4.BusinessLogic.CourseManager;

namespace ICP4.CoursePlayer
{
    public class DocuSignHelper
    {
        DocuSignLearner dsld = new DocuSignLearner();
        string token = string.Empty;
        public DocuSignHelper()
        {

        }
        public APIServiceSoapClient CreateAPIProxy()
        {
            AccountCredentials creds = GetAPICredentials();
            APIServiceSoapClient apiClient = new APIServiceSoapClient("APIServiceSoap", creds.ApiUrl);
            apiClient.ClientCredentials.UserName.UserName = creds.UserName;
            apiClient.ClientCredentials.UserName.Password = creds.Password;

            return apiClient;
        }

        public AccountCredentials GetAPICredentials()
        {
            AccountCredentials credentials = new AccountCredentials();
            if (SettingIsSet("APIUrl") && DocuSignStateHelper.DocuSignData.APIAccountID != null && DocuSignStateHelper.DocuSignData.APIEmail != null && DocuSignStateHelper.DocuSignData.APIPassword != null)
            {
                credentials.AccountId = DocuSignStateHelper.DocuSignData.APIAccountID;
                credentials.UserName = "[" + DocuSignStateHelper.DocuSignData.APIIKey + "]";
                credentials.UserName += DocuSignStateHelper.DocuSignData.APIUserID;
                credentials.Password = DocuSignStateHelper.DocuSignData.APIPassword;
                credentials.ApiUrl = ConfigurationManager.AppSettings["APIUrl"];

            }
            else
            {
                //this.GoToErrorPage("Please make sure your credentials are entered in web.config");
                DoLogin();
            }
            return credentials;
        }
        public bool DoLogin()
        {
            // Log in with Credential API
            String login = String.Format("[{0}]{1}", ConfigurationManager.AppSettings["IntegratorsKey"], ConfigurationManager.AppSettings["APIUserEmail"]);
            CredentialSoapClient credential = new CredentialSoapClient();
            LoginResult result = credential.Login(login, ConfigurationManager.AppSettings["Password"], true);

            // If we could log the user in, go to the main page
            if (result.Success)
            {
                // Grab the info from the form, even if it is already stored in the Session
                DocuSignStateHelper.DocuSignData.APIEmail = ConfigurationManager.AppSettings["APIUserEmail"];//Request.Form["DevCenterEmail"];
                DocuSignStateHelper.DocuSignData.APIPassword = ConfigurationManager.AppSettings["Password"]; //Request.Form["DevCenterPassword"];
                DocuSignStateHelper.DocuSignData.APIIKey = ConfigurationManager.AppSettings["IntegratorsKey"];// Request.Form["DevCenterIKey"];


                DocuSignStateHelper.DocuSignData.APIAccountID = result.Accounts[0].AccountID;
                DocuSignStateHelper.DocuSignData.APIUserID = result.Accounts[0].UserID;
                DocuSignStateHelper.DocuSignData.APIUserName = result.Accounts[0].UserName;
                return true;

            }

            else
            {
                return false;
            }
        }
        //public void GoToErrorPage(string errorMessage)
        //{
        //    Session["errorMessage"] = errorMessage;
        //    Response.Redirect("error.aspx", true);
        //}

        public bool SettingIsSet(string settingName)
        {
            // check if a value is specified in the config file
            return (ConfigurationManager.AppSettings[settingName] != null && ConfigurationManager.AppSettings[settingName].Length > 0);
        }



        public void AddEnvelopeID(string id)
        {
            if (DocuSignStateHelper.DocuSignData.EnvelopeIDs == null)
            {
                DocuSignStateHelper.DocuSignData.EnvelopeIDs = id;
            }
            else
            {
                DocuSignStateHelper.DocuSignData.EnvelopeIDs += "," + id;
            }
        }

        public string[] GetEnvelopeIDs()
        {
            if (DocuSignStateHelper.DocuSignData.EnvelopeIDs == null)
            {
                return new string[0];
            }
            string ids = DocuSignStateHelper.DocuSignData.EnvelopeIDs;
            return ids.Split(',');
        }

        public bool LoggedIn()
        {
            if (DocuSignStateHelper.DocuSignData.APIAccountID != null && DocuSignStateHelper.DocuSignData.APIEmail != null && DocuSignStateHelper.DocuSignData.APIPassword != null && DocuSignStateHelper.DocuSignData.APIIKey != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string DocuSignURL()
        {
            if (HttpContext.Current.Session["DocuSignLearnerData"] != null)
            {
                dsld = HttpContext.Current.Session["DocuSignLearnerData"] as DocuSignLearner;
            }

            DocuSignAPI.EnvelopeStatus status = null;
            //buttonTable.Visible = false;


            // Request the template and populate the recipient table
            DocuSignAPI.APIServiceSoapClient client = null;
            DocuSignAPI.EnvelopeTemplate template = null;
            DocuSignAPI.Envelope envelope = new DocuSignAPI.Envelope();

            client = this.CreateAPIProxy();
            
            string envelopID = string.Empty;
            using (CourseManager courseManager = new CourseManager())
            {
                envelopID= courseManager.GetEnvelopeId();
            }

            if (string.IsNullOrEmpty(envelopID))
            {
                template = client.RequestTemplate(dsld.TemplateId, true);
                envelope = template.Envelope;

                DocuSignAPI.Tab tb1 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "Learner Name");
                if (tb1 != null)
                {
                    tb1.Value = dsld.LearnerName;
                }

                DocuSignAPI.Tab tb2 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "Course Name");
                if (tb2 != null)
                {
                    tb2.Value = dsld.CourseName;
                }

                DocuSignAPI.Tab tb3 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "Completion Date");
                if (tb3 != null)
                {
                    tb3.Value = dsld.CourseCompletionDate;
                }
                //Create the recipient(s)
                envelope.Recipients = ConstructRecipients();


                // Send the envelope and temporarily store the status in the session
                status = client.CreateAndSendEnvelope(envelope);
                if (status != null)
                {
                    using (CourseManager courseManager = new CourseManager())
                    {
                        courseManager.SaveEnvelopeId(status.EnvelopeID);
                    }
                }
            }
            else
            {
                status = client.RequestStatus(envelopID);
            }
            if (status.SentSpecified)
            {
                ////HttpContext.Current.Session["EnvelopeStatus"] = status;
                ////this.AddEnvelopeID(status.EnvelopeID);
                if (status.Status!=EnvelopeStatusCode.Completed)
                {
                    // Start the first signer
                    SignFirst(status);
                }
            }


            return token;




        }
        protected DocuSignAPI.Recipient[] ConstructRecipients()
        {
            List<DocuSignAPI.Recipient> runningList = new List<DocuSignAPI.Recipient>();

            if (dsld != null)
            {
                // Construct the recipients

                DocuSignAPI.Recipient r1 = new DocuSignAPI.Recipient();
                r1.UserName = dsld.LearnerName;
                r1.Email = dsld.LearnerEmail;
                r1.ID = "1";
                r1.Type = DocuSignAPI.RecipientTypeCode.Signer;
                r1.CaptiveInfo = new DocuSignAPI.RecipientCaptiveInfo();
                r1.CaptiveInfo.ClientUserId = System.Web.HttpContext.Current.Session["LearnerID"].ToString(); //"1";
                runningList.Add(r1);

            }
            return runningList.ToArray();

        }

        protected void SignFirst(DocuSignAPI.EnvelopeStatus status)
        {
            // Create the assertion using the current time, password and demo information
            DocuSignAPI.RequestRecipientTokenAuthenticationAssertion assertion = new DocuSignAPI.RequestRecipientTokenAuthenticationAssertion();
            assertion.AssertionID = new Guid().ToString();
            assertion.AuthenticationInstant = DateTime.Now;
            assertion.AuthenticationMethod = DocuSignAPI.RequestRecipientTokenAuthenticationAssertionAuthenticationMethod.Password;
            assertion.SecurityDomain = "DocuSignSample";

            DocuSignAPI.RecipientStatus recipient = status.RecipientStatuses[0];

            // Construct the URLs to which the iframe will redirect upon every event
            DocuSignAPI.RequestRecipientTokenClientURLs urls = new DocuSignAPI.RequestRecipientTokenClientURLs();

            String urlBase = HttpContext.Current.Request.Url.AbsoluteUri.Replace("OpenEmbeddedDocuSign.aspx", "DocuSignHandler.ashx") + "?source=embed&envelopeId=" + status.EnvelopeID;
            
            urls.OnSigningComplete = urlBase + "&event=" + DocuSignEvents.SignComplete;
            urls.OnViewingComplete = urlBase + "&event=" + DocuSignEvents.ViewComplete;
            urls.OnCancel = urlBase + "&event=" + DocuSignEvents.Cancel;
            urls.OnDecline = urlBase + "&event=" + DocuSignEvents.Decline;
            urls.OnSessionTimeout = urlBase + "&event=" + DocuSignEvents.Timeout;
            urls.OnTTLExpired = urlBase + "&event=" + DocuSignEvents.TTLExpired;
            urls.OnIdCheckFailed = urlBase + "&event=" + DocuSignEvents.IDCheckFailed;
            urls.OnAccessCodeFailed = urlBase + "&event=" + DocuSignEvents.AccessCodeFailed;
            urls.OnException = urlBase + "&event="+DocuSignEvents.Exception;
            urls.OnFaxPending = urlBase + "&event=" + DocuSignEvents.FaxPending;

            DocuSignAPI.APIServiceSoapClient client = this.CreateAPIProxy();

            // Request the token for a specific recipient
            token = client.RequestRecipientToken(status.EnvelopeID, recipient.ClientUserId, recipient.UserName, recipient.Email, assertion, urls);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception();
            }

        }

        //public DocuSignLearner GetLearnerData()
        //{
        //    DocuSignLearner docuSignLearnerData = new DocuSignLearner();

        //    using (ICP4.BusinessLogic.ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService())
        //    {
        //        docuSignLearnerData = courseService.GetLearnerData();
        //    }



        //    return dsld; 
        //}

        public string GetEnvelopeStatus()
        {
            DocuSignAPI.EnvelopeStatus status = null;
            DocuSignAPI.APIServiceSoapClient client = null;
            client = this.CreateAPIProxy();
            string envelopID = string.Empty;
            string docuSignEnvelopeStatus = string.Empty;
            using (CourseManager courseManager = new CourseManager())
            {

                envelopID = courseManager.GetEnvelopeId();
                if (!string.IsNullOrEmpty(envelopID))
                {
                    status = client.RequestStatus(envelopID);
                    if (status.Status == EnvelopeStatusCode.Completed)
                    {
                       docuSignEnvelopeStatus= status.Status.ToString();
                    }
                }
            }
            return docuSignEnvelopeStatus;            
        }

    }

    public struct AccountCredentials
    {
        public string ApiUrl; // url endpoint of hte api
        public string AccountId; // billing account for sending envelopes
        public string UserName; // email address used for DocuSign login, prefixed with integrator key in square brackets
        public string Password; // password for DocuSign login
    }
    public static class DocuSignStateHelper
    {
        public static DocuSignDataHelper DocuSignData
        {
            get
            {
                if (HttpContext.Current.Session["DocuSignData"] == null)
                {
                    HttpContext.Current.Session["DocuSignData"] = new DocuSignDataHelper();
                }
                return (HttpContext.Current.Session["DocuSignData"]) as DocuSignDataHelper;
            }
            set
            {
                HttpContext.Current.Session["DocuSignData"] = value;
            }
        }

        public static int TryCount
        {
            get
            {
                if (HttpContext.Current.Session["TryCount"] == null)
                {
                    HttpContext.Current.Session["TryCount"] = 0;
                }
                return Convert.ToInt32(HttpContext.Current.Session["TryCount"]);
            }
            set
            {
                HttpContext.Current.Session["TryCount"] = value;
            }
        }

    }
    public class DocuSignDataHelper
    {
        public string EnvelopeIDs { get; set; }
        public string APIAccountID { get; set; }
        public string APIIKey { get; set; }
        public string APIEmail { get; set; }
        public string APIPassword { get; set; }
        public string APIUserID { get; set; }
        public string APIUserName { get; set; }
    }

}
