using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.CommunicationLogic.CommunicationCommand;
using System.Net;



namespace ICP4.BusinessLogic.BrandManager
{
    public class BrandManager
    {
        public BrandManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }

        /// <summary>
        /// This method creates ShowResourceInfo command by calling Branding Service and getting Resourceinfo
        /// </summary>
        /// <param name="variant">Variant string value</param>
        /// <param name="brandCode">BrandCode  string value</param>
        /// <returns>ShowResourceInfo command</returns>
        public ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ShowResourceInfo GetLocalResource(string variant, string brandCode)
        {


            if (System.Configuration.ConfigurationManager.AppSettings["ClearCache"] == "Yes")
            {
                System.Web.HttpContext.Current.Cache.Remove(brandCode + variant + "CourseBrandedLocale");
            }

            ICPBrandingService.BrandLocaleInfo brandLocaleInfo = GetCourseBrand(variant, brandCode);
            ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ShowResourceInfo showResourceInfo = new ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ShowResourceInfo();
            ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ResourceInfo resourceInfo = new ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ResourceInfo();
            List<ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ResourceInfo> resourceInfoList = new List<ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ResourceInfo>();

            string[] resources = System.Configuration.ConfigurationManager.AppSettings["ResourcesOnInit"].ToString().Split(',');
            List<string> resourceslist = new List<string>(resources.Length);
            resourceslist.AddRange(resources); 
            foreach (ICPBrandingService.LocaleResource localeResource in brandLocaleInfo.LocaleResourceList)
            {
                if (resourceslist.Contains(localeResource.ResourceKey))
                {
                    resourceInfo = new ICP4.CommunicationLogic.CommunicationCommand.ShowResourceInfo.ResourceInfo();

                    resourceInfo.ResourceKey = localeResource.ResourceKey;
                    resourceInfo.ResourceValue = localeResource.ResourceValue;

                    resourceInfoList.Add(resourceInfo);
                }
            }

            showResourceInfo.CommandName = ICP4.CommunicationLogic.CommunicationCommand.CommandNames.ShowResourceInfo;
            showResourceInfo.ResourceInfo = resourceInfoList;

            return showResourceInfo;





        }

        /// <summary>
        /// This method calls BrandingService and get branding information
        /// </summary>
        /// <param name="variant">Variant string value</param>
        /// <param name="brandCode">BrandCode  string value</param>
        /// <returns>ICPBrandingService.BrandLocaleInfo object</returns>
        private ICPBrandingService.BrandLocaleInfo GetCourseBrand(string variant, string brandCode)
        {

            CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
            ICPBrandingService.BrandLocaleInfo brandLocaleInfo = new ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo();
            ICPBrandingService.BrandingService brandingService = new ICP4.BusinessLogic.ICPBrandingService.BrandingService();
            brandingService.Url = System.Configuration.ConfigurationManager.AppSettings["ICPBrandingService"];
            brandingService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]); 

            brandLocaleInfo = cacheManager.GetIFBrandInfoExistInCache(brandCode, variant);

            if (brandLocaleInfo == null)
            {
                brandLocaleInfo = brandingService.GetBrandLocaleInfo(brandCode, variant);
                cacheManager.CreateCourseBrandInfoInCache(brandCode, variant, brandLocaleInfo);
            }

            return brandLocaleInfo;
        }

        public bool UpdateBrand(string variant, string brandCode)
        {
            try
            {
                CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
                ICPBrandingService.BrandLocaleInfo brandLocaleInfo = new ICP4.BusinessLogic.ICPBrandingService.BrandLocaleInfo();
                ICPBrandingService.BrandingService brandingService = new ICP4.BusinessLogic.ICPBrandingService.BrandingService();
                brandingService.Url = System.Configuration.ConfigurationManager.AppSettings["ICPBrandingService"];
                brandingService.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                brandLocaleInfo = brandingService.GetBrandLocaleInfo(brandCode, variant);
                if (brandLocaleInfo != null)
                    cacheManager.CreateCourseBrandInfoInCache(brandCode, variant, brandLocaleInfo);

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "ICPException");
                return false;
            }
        }


    }
}
