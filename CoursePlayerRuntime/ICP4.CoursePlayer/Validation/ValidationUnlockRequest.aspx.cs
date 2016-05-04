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

namespace ICP4.CoursePlayer.Validation
{
    public partial class ValidationUnlockRequest : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            string learningSessionId = Request.QueryString["lsId"];            

            #region Test Purpose Only Comment this Code
            //learningSessionId = "6C050491-DF22-8E2B-6759C8F012459098";
            //// Test only
            //// add brandCode and variant
            //HttpContext.Current.Session["BrandCode"] = "DEFAULT";
            //HttpContext.Current.Session["Variant"] = "En-US";
            ////Load the Cache with resources 
            //BusinessLogic.BrandManager.BrandManager brandManager = new ICP4.BusinessLogic.BrandManager.BrandManager(); 
            //brandManager.GetLocalResource(HttpContext.Current.Session["Variant"].ToString(),HttpContext.Current.Session["BrandCode"].ToString());
            //// End test            
            #endregion


            //Get Brand Code and Varaint
            ValidationUnlockManager validationManager = new ValidationUnlockManager();
            validationManager.GetLearningSessionBrandcodeVariant(learningSessionId);
           if (HttpContext.Current.Session["BrandCode"] != null && HttpContext.Current.Session["Variant"] != null)
           {
                LoadLogoURLFromBrand();
                SendValidationRequest(learningSessionId);                
            }
        }
        #endregion

        #region Method/Functions
        private void SendValidationRequest(string learningSessionId)
        {            
            ValidationUnlockManager validationManager = new ValidationUnlockManager();
            // send request
            validationManager.CreateValidationUnlockRequest(learningSessionId, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());                           
        }
        private void LoadLogoURLFromBrand()
        {
            //load url
            BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();            
            string imageURL = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
            imgLogo.Src = imageURL;
        }
        #endregion
    }
}
