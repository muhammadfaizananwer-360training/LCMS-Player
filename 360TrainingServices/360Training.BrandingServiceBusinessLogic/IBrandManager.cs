using System;
using System.Collections.Generic;
using System.Text;
using _360Training.BusinessEntities; 

namespace _360Training.BrandingServiceBusinessLogic
{
    public interface IBrandManager
    {
        CourseInfo GetCourseInfo(int courseID);
        /// <summary>
        /// This method gets the localInfo for a particular brand and locale. It also fills
        /// the resources collection with default values if non default are not available 
        /// </summary>
        /// <param name="brandID">int brandID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>brandLocaleInfo object</returns>
        BrandLocaleInfo GetLocaleInfo(int brandID, int localeID);
        /// <summary>
        /// This method returns the brandlocaleinfo entity by branding code and language variant
        /// </summary>
        /// <param name="brandCode">string brandcode</param>
        /// <param name="variant">string variant</param>
        /// <returns>BrandLocaleInfo object</returns>
        BrandLocaleInfo GetBrandLocaleInfo(string brandCode, string variant);
        
    }
}
