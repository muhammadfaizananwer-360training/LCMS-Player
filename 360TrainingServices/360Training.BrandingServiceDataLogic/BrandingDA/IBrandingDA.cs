using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities; 

namespace _360Training.BrandingServiceDataLogic.BrandingDA
{
    public interface IBrandingDA
    {
        CourseInfo GetCourseInfo(int courseID);
        /// <summary>
        /// Thi mehod gets the branding  
        /// </summary>
        /// <param name="brandingID">int brandingid</param>
        /// <returns>BrandLocaleInfo object</returns>
        BrandLocaleInfo GetBranding(int brandingID);
        /// <summary>
        /// This method gets the resources of the specified branding locale
        /// </summary>
        /// <param name="brandingID">int brandingID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>List of LocaleResource object</returns>
        List<LocaleResource> GetBrandingLocaleResources(int brandingID, int localeID);
        /// <summary>
        /// Thi method returns the languageid of the given variant 
        /// </summary>
        /// <param name="variant">string variant</param>
        /// <returns>languageID if suceessfull, else 0</returns>
        int GetVariantLanguage(string variant);
         /// <summary>
        /// This metho gets the branding record of code
        /// </summary>
        /// <param name="code">strin code</param>
        /// <returns>BrandLocaleInfo  object</returns>
        BrandLocaleInfo GetCodeBranding(string code);
    }
}
