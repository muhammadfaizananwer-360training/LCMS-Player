using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

using System.Collections;
using System.Collections.Generic;
using _360Training.BusinessEntities;
using ICP4.BusinessLogic.CourseManager;
using ICP4.BusinessLogic.DocuSignAPI;
using System;
using System.Configuration;
using ICP4.BusinessLogic.CredentialAPI;


namespace ICP4.BusinessLogic.DocuSignManager
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
                credentials.ApiUrl =  ConfigurationManager.AppSettings["APIUrl"];

            }
            else
            {
                //this.GoToErrorPage("Please make sure your credentials are entered in web.config");
                DoLogin();
                // Changed by Waqas Zakai
                // LCMS-11515, LCMS-11530
                // START
                credentials.AccountId = DocuSignStateHelper.DocuSignData.APIAccountID;
                credentials.UserName = "[" + DocuSignStateHelper.DocuSignData.APIIKey + "]";
                credentials.UserName += DocuSignStateHelper.DocuSignData.APIUserID;
                credentials.Password = DocuSignStateHelper.DocuSignData.APIPassword;
                credentials.ApiUrl = ConfigurationManager.AppSettings["APIUrl"];                
                // LCMS-11515, LCMS-11530
                // END
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
            using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
            {
                envelopID = courseManager.GetEnvelopeId();
            }
                     
            if (string.IsNullOrEmpty(envelopID))
            {
                      
                template = client.RequestTemplate(dsld.TemplateId, true);

                envelope = template.Envelope;              

                DocuSignAPI.Tab tb1 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "address");
                if (tb1 != null)
                {
                    tb1.Value = dsld.Address;
                }


                DocuSignAPI.Tab tb2 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "approval_number");
                if (tb2 != null)
                {
                    tb2.Value = dsld.ApprovalNumber;
                }



                DocuSignAPI.Tab tb3 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "approved_course_name");
                if (tb3 != null)
                {
                    tb3.Value = dsld.ApprovedCourseName;
                }


                DocuSignAPI.Tab tb4 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "approved_credit_hours");
                if (tb4 != null)
                {
                    tb4.Value = dsld.ApprovedCreditHours;
                }


                DocuSignAPI.Tab tb5 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "cert_exp_date");
                if (tb5 != null)
                {
                    tb5.Value = dsld.CertificateExpiryDate;
                }


                DocuSignAPI.Tab tb6 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "cert_num");
                if (tb6 != null)
                {
                    tb6.Value = dsld.CertificateNumber;
                }


                DocuSignAPI.Tab tb7 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "city");
                if (tb7 != null)
                {
                    tb7.Value = dsld.City;
                }


                DocuSignAPI.Tab tb8 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "comp_date");
                if (tb8 != null)
                {
                    tb8.Value = dsld.LastPostTestDate;
                }


                DocuSignAPI.Tab tb9 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "course_name");
                if (tb9 != null)
                {
                    tb9.Value = dsld.CourseName;
                }


                DocuSignAPI.Tab tb10 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "credit_hours");
                if (tb10 != null)
                {
                    tb10.Value = dsld.CreditHours;
                }



                DocuSignAPI.Tab tb11 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "date_of_birth");
                if (tb11 != null)
                {
                    tb11.Value = dsld.DateofBirth;
                }


                DocuSignAPI.Tab tb12 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "final_exam_score");
                if (tb12 != null)
                {
                    tb12.Value = dsld.FinalExamScore;
                }


                DocuSignAPI.Tab tb13 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "license_expiration_date");
                if (tb13 != null)
                {
                    tb13.Value = dsld.LicenseExpirationDate;
                }

                DocuSignAPI.Tab tb14 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "license_number");
                if (tb14 != null)
                {
                    tb14.Value = dsld.LicenseNumber;
                }

                DocuSignAPI.Tab tb15 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "license_type");
                if (tb15 != null)
                {
                    tb15.Value = dsld.LicenseType;
                }


                DocuSignAPI.Tab tb16 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "name");
                if (tb16 != null)
                {
                    tb16.Value = dsld.LearnerName;
                }

                DocuSignAPI.Tab tb17 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "nerc_cert_num");
                if (tb17 != null)
                {
                    tb17.Value = dsld.NERCCertificateNumber;
                }


                DocuSignAPI.Tab tb18 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "NPN");
                if (tb18 != null)
                {
                    tb18.Value = dsld.NationProducerNumber;
                }

                DocuSignAPI.Tab tb19 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "reg_date");
                if (tb19 != null)
                {
                    tb19.Value = dsld.RegistrationDate;
                }


                DocuSignAPI.Tab tb20 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "ssn");
                if (tb20 != null)
                {
                    tb20.Value = dsld.SocialSecurityNumber;
                }


                DocuSignAPI.Tab tb21 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "start_date");
                if (tb21 != null)
                {
                    tb21.Value = dsld.StartDate;
                }


                DocuSignAPI.Tab tb22 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "state");
                if (tb22 != null)
                {
                    tb22.Value = dsld.State;
                }


                DocuSignAPI.Tab tb23 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "zip");
                if (tb23 != null)
                {
                    tb23.Value = dsld.ZipCode;
                }



                DocuSignAPI.Tab tb24 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "phone");
                if (tb24 != null)
                {
                    tb24.Value = dsld.Phone;
                }
            

                
                DocuSignAPI.Tab tb25 = envelope.Tabs.FirstOrDefault(o => o.TabLabel == "Learner Email");
                if (tb25 != null)
                {
                    tb25.Value = dsld.LearnerEmail;
                }
                
                //Create the recipient(s)
                envelope.Recipients = ConstructRecipients();

                // Send the envelope and temporarily store the status in the session
                status = client.CreateAndSendEnvelope(envelope);
                if (status != null)
                {
                    using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                    {
                        courseManager.SaveEnvelopeId(status.EnvelopeID);                                
                    }

                 InsertEnvelopRoleAgainstEnrollmentID(envelope.Recipients, status.EnvelopeID);

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
                if (status.Status != EnvelopeStatusCode.Completed)
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
            bool rollName = false;

            if (dsld != null)
            {
                // Construct the recipients          
                rollName = GetRoleName();
                DocuSignAPI.Recipient r1 = new DocuSignAPI.Recipient();
                r1.UserName = dsld.LearnerName;
                r1.Email = dsld.LearnerEmail;
                r1.RoleName = ConfigurationManager.AppSettings["RollNameForLearner"];
                r1.ID = "1";
                r1.RoutingOrder = 1;
                r1.RoutingOrderSpecified = true;

                //LCMS-12345
                //Abdus Samad
                //Start                
                if (rollName == true)
                {
                    r1.Type = DocuSignAPI.RecipientTypeCode.InPersonSigner;
                    r1.SignerName = ConfigurationManager.AppSettings["SignerNameForProctor"]; //"Proctor";  
                    r1.RoleName = ConfigurationManager.AppSettings["RollNameForProctor"];
                   
                }
                else
                {
                    r1.Type = DocuSignAPI.RecipientTypeCode.Signer;
                }
                //Stop

                //r1.Type = DocuSignAPI.RecipientTypeCode.Signer;
                r1.CaptiveInfo = new DocuSignAPI.RecipientCaptiveInfo();
                if (System.Web.HttpContext.Current.Session["LearnerID"] != null)
                {
                                   
                    
                    //LCMS-12345
                    //Abdus Samad
                    //Start    
                    if (rollName == true)
                    {
                        r1.CaptiveInfo.ClientUserId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        r1.CaptiveInfo.ClientUserId = System.Web.HttpContext.Current.Session["LearnerID"].ToString(); //"1";
                    }
                    //Stop
                }
                else
                {
                    //LCMS-12345
                    //Abdus Samad
                    //Start    
                    if (System.Web.HttpContext.Current.Request.QueryString["learnerID"] != null)
                    {
                        if (rollName == true)
                        {

                            r1.CaptiveInfo.ClientUserId = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            r1.CaptiveInfo.ClientUserId = System.Web.HttpContext.Current.Request.QueryString["learnerID"].ToString();
                        }
                    }
                    //Stop
                }
                runningList.Add(r1);

                if (CheckIfRoleNameForProviderAndApproverExists() == true)
                {


                    // -----------------------------------------FOR PROVIDER---------------------------------------------------------
                    DocuSignAPI.Recipient r2 = new DocuSignAPI.Recipient();
                    r2.UserName = ConfigurationManager.AppSettings["UserNameForProvider"]; //"Provider";
                    r2.Email = ConfigurationManager.AppSettings["ProviderEmailAddress"];   //"abdus.qureshi@360training.com";
                    r2.RoleName = ConfigurationManager.AppSettings["RollNameForProvider"]; //"Provider";
                    r2.ID = "2";
                    r2.RoutingOrder = 2;
                    r2.RoutingOrderSpecified = true;
                    r2.Type = DocuSignAPI.RecipientTypeCode.Signer;
                    runningList.Add(r2);


                   // -----------------------------------------FOR APPROVER--------------------------------------------------------- 
                    DocuSignAPI.Recipient r3 = new DocuSignAPI.Recipient();
                    r3.UserName = ConfigurationManager.AppSettings["UserNameForApprover"]; // "Approver";
                    r3.Email = ConfigurationManager.AppSettings["ApproverEmailAddress"];   //"abdussamadqureshi@yahoo.com";
                    r3.RoleName = ConfigurationManager.AppSettings["RollNameForApprover"]; //"Approver";
                    r3.RoutingOrderSpecified = true;                    
                    r3.ID = "3";
                    r3.RoutingOrder = 3;
                    r3.Type = DocuSignAPI.RecipientTypeCode.Signer;                                                 
                    runningList.Add(r3);


               }
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
            urls.OnException = urlBase + "&event=" + DocuSignEvents.Exception;
            urls.OnFaxPending = urlBase + "&event=" + DocuSignEvents.FaxPending;

            DocuSignAPI.APIServiceSoapClient client = this.CreateAPIProxy();

            //LCMS-12345
            //Abdus Samad
            //Start 
            //The reason for adding this piece of code was when we set the sign proctor to proctor so 
            //it automatically rename the recipient User Name to Protor due to this an exception is thrown which
            //tells that the the learner user name and password are different from one another
            //So in proctor case we have fetched the data from the session and place that data 

            if (GetRoleName() == true)
            {

                if (System.Web.HttpContext.Current.Session["DocuSignLearnerData"] != null)
                {
                    DocuSignLearner dsld = System.Web.HttpContext.Current.Session["DocuSignLearnerData"] as DocuSignLearner;

                    token = client.RequestRecipientToken(status.EnvelopeID, recipient.ClientUserId, dsld.LearnerName, recipient.Email, assertion, urls);
                }
            }
            //Stop
            else
            {

               /// Request the token for a specific recipient
                token = client.RequestRecipientToken(status.EnvelopeID, recipient.ClientUserId, recipient.UserName, recipient.Email, assertion, urls);                

            }
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception();
            }

        }

       
        public string GetEnvelopeStatus()
        {
            DocuSignAPI.EnvelopeStatus status = null;
            DocuSignAPI.APIServiceSoapClient client = null;
            string envelopID = string.Empty;
            string docuSignEnvelopeStatus = string.Empty;
            if (DoLogin())
            {
                client = this.CreateAPIProxy();

                using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {

                    envelopID = courseManager.GetEnvelopeId();
                    if (!string.IsNullOrEmpty(envelopID))
                    {
                        status = client.RequestStatus(envelopID);
                        if (status.Status == EnvelopeStatusCode.Completed)
                        {
                            docuSignEnvelopeStatus = status.Status.ToString();
                        }
                    }
                }
            }
            
            
            return docuSignEnvelopeStatus;
        }

        //LCMS-12345
        //Abdus Samad
        //Start    
        public bool GetRoleName()
        {
        
         DocuSignAPI.APIServiceSoapClient client = null;
         bool proctorStatus;
         client = this.CreateAPIProxy();
         if (client.RequestTemplate(dsld.TemplateId, false).Envelope.Recipients[0].RoleName.Contains(ConfigurationManager.AppSettings["RollNameForProctor"]))
         {
             proctorStatus = true;
         }
         else
         {
             proctorStatus = false;
         }

         return proctorStatus;

        }
        //Stop

        public bool CheckIfRoleNameForProviderAndApproverExists()
        {
            bool approverAndProviderStatus;
            DocuSignAPI.APIServiceSoapClient client = null;          
            client = this.CreateAPIProxy();
            if (client.RequestTemplate(dsld.TemplateId, false).Envelope.Recipients.Count() > 1)
            {
                if (client.RequestTemplate(dsld.TemplateId, false).Envelope.Recipients[1].RoleName.Contains(ConfigurationManager.AppSettings["RollNameForProvider"]) && client.RequestTemplate(dsld.TemplateId, false).Envelope.Recipients[2].RoleName.Contains(ConfigurationManager.AppSettings["RollNameForApprover"]))
                {
                    approverAndProviderStatus = true;
                }

                else
                {
                    approverAndProviderStatus = false;
                }
            }
            else
            {
                approverAndProviderStatus = false;
            }
            return approverAndProviderStatus;
        }



        protected void InsertEnvelopRoleAgainstEnrollmentID(DocuSignAPI.Recipient[] recpList, string envelopID)
        {
            
            if (recpList.Count() > 0)
            {

                DocuSignAPI.Recipient[] recipientList = recpList;

                foreach (DocuSignAPI.Recipient recipientinfo in recipientList)
                {
                    using (ICPTrackingService.TrackingService trackingService = new ICP4.BusinessLogic.ICPTrackingService.TrackingService())
                    {
                        int enrollmentID = Convert.ToInt32(System.Web.HttpContext.Current.Session["EnrollmentID"]);
                        trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                        trackingService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                        
                        trackingService.SaveDocuSignRoleAgainstLearnerEnrollment(envelopID, enrollmentID, recipientinfo.RoleName);                   
                                              
                    }              

                }
            
            }

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

    public class DocuSignEvents
    {
        public const string Connect = "connect";
        public const string SignComplete = "SignComplete1";
        public const string ViewComplete = "ViewComplete1";
        public const string Cancel = "Cancel1";
        public const string Decline = "Decline1";
        public const string Timeout = "Timeout1";
        public const string TTLExpired = "TTLExpired1";
        public const string IDCheckFailed = "IDCheckFailed1";
        public const string AccessCodeFailed = "AccessCodeFailed1";
        public const string Exception = "Exception1";
        public const string FaxPending = "FaxPending1";


    }
}
