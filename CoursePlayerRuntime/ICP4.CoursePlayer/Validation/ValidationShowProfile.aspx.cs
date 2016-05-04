using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ICP4.BusinessLogic.ValidationManager;
using _360Training.BusinessEntities;

namespace ICP4.CoursePlayer.Validation
{
    public partial class ValidationShowProfile : System.Web.UI.Page
    {
        LearnerProfile learner;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string transactionId = Request.QueryString["GUID"];                
                ValidationUnlockManager validationManager = new ValidationUnlockManager();                
                learner = validationManager.GetUserProfile(transactionId);
                FillProfileFields(learner);                

                //set the branding code and variant in session and load the LOGO
                if (learner.BrandCode  != string.Empty && learner.Variant != string.Empty)
                {                                        
                    HttpContext.Current.Session["BrandCode"] = learner.BrandCode;
                    HttpContext.Current.Session["Variant"] = learner.Variant;

                    #region Test Purpose only Comment this code
                    ////Load the Cache with resources 
                    //// this is not required once the cache is filled. TO BE REMOVED!!
                    //BusinessLogic.BrandManager.BrandManager brandManager = new ICP4.BusinessLogic.BrandManager.BrandManager(); 
                    //brandManager.GetLocalResource(HttpContext.Current.Session["Variant"].ToString(),HttpContext.Current.Session["BrandCode"].ToString());            
                    ////End Load
                    #endregion

                    LoadLogoURLFromBrand();
                }
            }
        }
        private void FillProfileFields(LearnerProfile learner)
        {
            TxtAddress1.Text = learner.Address1;
            TxtAddress2.Text = learner.Address2;
            TxtAddress3.Text = learner.Address3;
            TxtCity.Text = learner.City;
            TxtCountry.Text = learner.Country;
            TxtEmail.Text = learner.EmailAddress;
            TxtZipcode.Text = learner.ZipCode;
            TxtFirstName.Text = learner.FirstName;
            TxtLastName.Text = learner.LastName;
            TxtPhone.Text = learner.MobilePhone;
            TxtState.Text = learner.State;
        }
        private void LoadLogoURLFromBrand()
        {
            //load url
            BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
            string imageURL = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
            imgLogo.Src = imageURL;
        }
       
        protected void BtnUpdate_Click1(object sender, EventArgs e)
        {
            LearnerProfile learner = new LearnerProfile();
            learner.Address1 = TxtAddress1.Text;
            learner.Address2 = TxtAddress2.Text;
            learner.Address3 = TxtAddress3.Text;
            learner.City = TxtCity.Text;
            learner.Country = TxtCountry.Text;
            learner.EmailAddress = TxtEmail.Text;
            learner.ZipCode = TxtZipcode.Text;
            learner.FirstName = TxtFirstName.Text;
            learner.LastName = TxtLastName.Text;
            learner.MobilePhone = TxtPhone.Text;
            learner.State = TxtState.Text;
            learner.LearningSessionID = Request.QueryString["GUID"];//"660136e1-6048-4d67-baaa-d65596b19875";//= Request.QueryString["learningSessionId"]

            ValidationUnlockManager validationManager = new ValidationUnlockManager();            
            bool isUpdate = validationManager.UpdateLearnerProfile(learner);
            if (isUpdate)
            {
                Response.Redirect("ValidationUnlockedSuccessful.aspx");
            }
            else
            {
                LabelMessage.Text = "Profile not updated!";
                LabelMessage.Visible = true;
            }
        }
    }
}
