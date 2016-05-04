using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using _360Training.BusinessEntities;
using _360Training.BrandingServiceBusinessLogic;

namespace _360Training.BrandingService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.360training.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BrandingService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetCourseInfo()
        {
            return "Hello World";
        }
        /// <summary>
        /// This method gets the localInfo for a particular brand and locale. It also fills
        /// the resources collection with default values if non default are not available 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="brandID">int brandID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>brandLocaleInfo object</returns>
        [WebMethod]
        public BrandLocaleInfo GetLocaleInfo(int courseID, int brandID, int localeID)
        {
            using (BrandManager brandManager = new BrandManager())
            {
                return brandManager.GetLocaleInfo(brandID, localeID);
            }
            //BrandLocaleInfo brandLocaleInfo = new BrandLocaleInfo(); 
            //LocaleResource localResource = new LocaleResource();
            //List<LocaleResource> localResourceList = new List<LocaleResource>();


            //if (localeID == 1)
            //{
            //    localResource.ResourceKey = ResourceKeyNames.PlayerCSS;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/CSS/style.css";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.BGContainer;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/Images/bg_container.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.BGTitle;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/Images/bg_title.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.ImageLogoutButton;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/Images/btn_logout.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.ImageComanyLogo;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/Images/logo_360.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.MenuStrip;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/1/Images/menu_btn.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingTOC;
            //    localResource.ResourceValue = "Table of Content";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingGlossary;
            //    localResource.ResourceValue = "Glossary";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingCourseMaterial;
            //    localResource.ResourceValue = "CourseMaterial";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingBookmark;
            //    localResource.ResourceValue = "Bookmark";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingFooterNote;
            //    localResource.ResourceValue = "LCMS 1.x Course Player © 2006 - 2007 360training.com™ All Rights Reserved. Powered by 360training.com";
            //    localResourceList.Add(localResource);
            //}
            //else if (localeID == 2)
            //{
            //    localResource.ResourceKey = ResourceKeyNames.PlayerCSS;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/2/CSS/style.css";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.BGContainer;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/2/Images/bg_containerPink.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.BGTitle;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/2/Images/bg_title.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.ImageLogoutButton;
            //    localResource.ResourceValue = "http://www.rebootitsmagic.com/rim/images/logoutButton.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.ImageComanyLogo;
            //    localResource.ResourceValue = "http://www.codeproject.com/SiteRes/CP/Img/Std/logo225x90.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.MenuStrip;
            //    localResource.ResourceValue = "http://10.0.100.250/ICPBrands/2/Images/menu_btn.gif";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingTOC;
            //    localResource.ResourceValue = "Tabe ksdf mjkhsdf";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingGlossary;
            //    localResource.ResourceValue = "Glodfetr ";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingCourseMaterial;
            //    localResource.ResourceValue = "Coufdrse Masdfdrial";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingBookmark;
            //    localResource.ResourceValue = "Boofd f kmark";
            //    localResourceList.Add(localResource);

            //    localResource = new LocaleResource();
            //    localResource.ResourceKey = ResourceKeyNames.HeadingFooterNote;
            //    localResource.ResourceValue = ",djbfsd jf sdufgsdifj";
            //    localResourceList.Add(localResource);
            
            
            //}
            //brandLocaleInfo.BrandName = "asdasdas";
            //brandLocaleInfo.LocaleID = localeID;
            //brandLocaleInfo.LocaleResourceList = localResourceList;
            //return brandLocaleInfo;

  
        }
        /// <summary>
        /// This method returns the brandlocaleinfo entity by branding code and language variant
        /// </summary>
        /// <param name="brandCode">string brandcode</param>
        /// <param name="variant">string variant</param>
        /// <returns>BrandLocaleInfo object</returns>
        [WebMethod]
        public BrandLocaleInfo GetBrandLocaleInfo(string brandCode, string variant)
        {
            using (BrandManager brandManager = new BrandManager())
            {
                return brandManager.GetBrandLocaleInfo(brandCode, variant);
            }
        }
    }
}
