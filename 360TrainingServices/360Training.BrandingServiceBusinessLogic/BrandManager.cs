using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities;
using _360Training.BrandingServiceDataLogic.BrandingDA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace _360Training.BrandingServiceBusinessLogic
{
    public class BrandManager:IBrandManager,IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Course
        public CourseInfo GetCourseInfo(int courseID)
        {
            try
            {
                using (BrandingDA brandingDA = new BrandingDA())
                {
                    return brandingDA.GetCourseInfo(courseID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        #endregion

        #region BrandingMethods
        /// <summary>
        /// This mehod gets the branding  
        /// </summary>
        /// <param name="brandingID">int brandingid</param>
        /// <returns>BrandLocaleInfo object</returns>
        private BrandLocaleInfo GetBranding(int brandingID)
        {
            try
            {
                using (BrandingDA brandingDA = new BrandingDA())
                {
                    return brandingDA.GetBranding(brandingID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This mehod gets the branding by code 
        /// </summary>
        /// <param name="brandingID">string code</param>
        /// <returns>BrandLocaleInfo object</returns>
        private BrandLocaleInfo GetCodeBranding(string code)
        {
            try
            {
                using (BrandingDA brandingDA = new BrandingDA())
                {
                    return brandingDA.GetCodeBranding(code);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This method returns the language id of the variant passed as argument
        /// </summary>
        /// <param name="variant">string variant</param>
        /// <returns>int languageID if succesfull,else 0</returns>
        private int GetVariantLanguage(string variant)
        {
            try
            {
                using (BrandingDA brandingDA = new BrandingDA())
                {
                    return brandingDA.GetVariantLanguage(variant);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }

        }
        /// <summary>
        /// This method gets the resources of the specified branding locale
        /// </summary>
        /// <param name="brandingID">int brandingID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>List of LocaleResource object</returns>
        private List<LocaleResource> GetBrandingLocaleResources(int brandingID, int localeID)
        {
            try
            {
                using (BrandingDA brandingDA = new BrandingDA())
                {
                    return brandingDA.GetBrandingLocaleResources(brandingID,localeID);
                }
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This method gets the localInfo for a particular brand and locale. It also fills
        /// the resources collection with default values if non default are not available 
        /// </summary>
        /// <param name="brandID">int brandID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>brandLocaleInfo object</returns>
        public BrandLocaleInfo GetLocaleInfo(int brandID, int localeID)
        {
            try
            {
                BrandLocaleInfo brandLocaleInfo = GetBranding(brandID);
                List<LocaleResource> localeResources = GetBrandingLocaleResources(brandID, localeID);
                Dictionary<string, string> localeResourcesDictionary = new Dictionary<string, string>();
                foreach (LocaleResource localeResource in localeResources)
                {
                    localeResourcesDictionary.Add(localeResource.ResourceKey, localeResource.ResourceValue);
                }
                brandLocaleInfo.LocaleResourceList = GetBrandingLocaleResources(1, 1);//Get default values
                string keyValue =string.Empty;
                for (int resourceCount = 0; resourceCount <= brandLocaleInfo.LocaleResourceList.Count - 1;resourceCount++ )
                {
                    if (localeResourcesDictionary.ContainsKey(brandLocaleInfo.LocaleResourceList[resourceCount].ResourceKey))
                    {
                        keyValue = localeResourcesDictionary[brandLocaleInfo.LocaleResourceList[resourceCount].ResourceKey];
                        if (keyValue!= string.Empty)
                            brandLocaleInfo.LocaleResourceList[resourceCount].ResourceValue = keyValue;
                    }
                }
                return brandLocaleInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This method returns the brandlocaleinfo entity by branding code and language variant
        /// </summary>
        /// <param name="brandCode">string brandcode</param>
        /// <param name="variant">string variant</param>
        /// <returns>BrandLocaleInfo object</returns>
        public BrandLocaleInfo GetBrandLocaleInfo(string brandCode, string variant)
        {
            try
            {
                BrandLocaleInfo brandLocaleInfo = GetCodeBranding(brandCode);
                brandLocaleInfo.LocaleID = GetVariantLanguage(variant);
                //System.Diagnostics.Trace.WriteLine("BrandCode:" + brandLocaleInfo.BrandID + " Variant:" + brandLocaleInfo.LocaleID);
                //System.Diagnostics.Trace.Flush();

                List<LocaleResource> localeResources = GetBrandingLocaleResources(brandLocaleInfo.BrandID, brandLocaleInfo.LocaleID);
                Dictionary<string, string> localeResourcesDictionary = new Dictionary<string, string>();
                foreach (LocaleResource localeResource in localeResources)
                {
                    if (localeResourcesDictionary.ContainsKey(localeResource.ResourceKey) == false)
                    {
                        localeResourcesDictionary.Add(localeResource.ResourceKey, localeResource.ResourceValue);
                        //System.Diagnostics.Trace.WriteLine("ResourceKey:" + localeResource.ResourceKey + " ResourceValue:" + localeResource.ResourceValue);
                        //System.Diagnostics.Trace.Flush();
                    }
                   
                }
                //System.Diagnostics.Trace.WriteLine("*********************************************************");
                //    System.Diagnostics.Trace.Flush();
                brandLocaleInfo.LocaleResourceList = GetBrandingLocaleResources(1, 1);//Get default values
                string keyValue = string.Empty;
                for (int resourceCount = 0; resourceCount <= brandLocaleInfo.LocaleResourceList.Count - 1; resourceCount++)
                {
                    if (localeResourcesDictionary.ContainsKey(brandLocaleInfo.LocaleResourceList[resourceCount].ResourceKey))
                    {
                        keyValue = localeResourcesDictionary[brandLocaleInfo.LocaleResourceList[resourceCount].ResourceKey];
                        if (keyValue != string.Empty)
                        {
                            brandLocaleInfo.LocaleResourceList[resourceCount].ResourceValue = keyValue;
                            //System.Diagnostics.Trace.WriteLine("ResourceKey:" + brandLocaleInfo.LocaleResourceList[resourceCount].ResourceKey + " ResourceValue:" + brandLocaleInfo.LocaleResourceList[resourceCount].ResourceValue);
                            //System.Diagnostics.Trace.Flush();
                        }
                    }
                }
                return brandLocaleInfo;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        #endregion
    }
}
