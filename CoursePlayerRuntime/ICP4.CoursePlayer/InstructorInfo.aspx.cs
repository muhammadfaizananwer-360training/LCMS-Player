using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICP4.BusinessLogic;  

namespace ICP4.CoursePlayer
{
    public partial class InstructorInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadLogoURLFromBrand();
            LoadData();

        }
        private void LoadLogoURLFromBrand()
        {
            //load url
            try
            {
                if (HttpContext.Current.Session["BrandCode"] != null && HttpContext.Current.Session["Variant"] != null)
                {
                    BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
                    string imageURL1=string.Empty;
                    string imageURL2 = string.Empty;
                    //this.Title = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.InstructorInfoPageTitle, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                    InstructorInfoh2.InnerText = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.InstructorInfoPageH2, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                    imageURL1= cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                   // this.Title = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.InstructorInfoPageTitle, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                    this.instructorInformationText.InnerText = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                    if (Convert.ToString(HttpContext.Current.Session["BrandCode"]).ToLower() == "default" && Convert.ToString(HttpContext.Current.Session["Variant"]).ToLower() == "en-us")
                    {
                        imgLogoLeft.Src = imageURL1;
                        imgLogoRight.Visible = false;
                    }
                    else
                    {
                        //imageURL2 = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, "DEFAULT", "En-US");
                        ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo brandLocaleInfo = new ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo();
                        ICP4.BusinessLogic.ICPBrandingService.BrandingService brandingService = new ICP4.BusinessLogic.ICPBrandingService.BrandingService();
                        brandingService.Url = System.Configuration.ConfigurationManager.AppSettings["ICPBrandingService"];
                        brandingService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                        brandLocaleInfo = brandingService.GetBrandLocaleInfo("DEFAULT", "En-US");

                        foreach (ICP4.BusinessLogic.ICPBrandingService.LocaleResource localeResource in brandLocaleInfo.LocaleResourceList)
                        {
                            if (localeResource.ResourceKey.Equals("ImageComanyLogo"))
                            {
                                imageURL2 = localeResource.ResourceValue.ToString();
                                break;
                            }
                        }
                        imgLogoRight.Src = imageURL2;
                        imgLogoLeft.Src = imageURL1;
                    }
                }
                else
                {
                    divContainer.Visible = false;
                    divEmpty.Visible = true;
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");

            }
        }
        private void LoadData()
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["CourseConfigurationID"] != null && Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]) > 0)
                {
                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration = null;
                    using (ICP4.BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
                    {
                        int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
                        courseConfiguration = cacheManager.GetIFConfigurationExistInCache(courseConfigurationID);
                    }
                    if (courseConfiguration != null && courseConfiguration.InstructorInfoEnabled)
                    {
                        instructorInformationText.InnerHtml = courseConfiguration.InstructorInfoText;
                    }
                    else
                    {
                        divContainer.Visible = false;
                        divEmpty.Visible = true;
                    }
                }
                else
                {
                    divContainer.Visible = false;
                    divEmpty.Visible = true;
                }
            }
            catch (Exception ex1)
            {
                ExceptionPolicyForLCMS.HandleException(ex1, "ICPException");
                
            }

        }
    }
}
