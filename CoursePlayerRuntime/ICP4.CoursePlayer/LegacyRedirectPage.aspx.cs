using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICP4.BusinessLogic.CacheManager;
using ICP4.BusinessLogic.BrandManager;

namespace ICP4.CoursePlayer
{
    public partial class LegacyRedirectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string HeadingLegacyRetired = string.Empty;

            string LegacyRetiredText = string.Empty;
            string brandCode = "DEFAULT";
            string variant = "En-US";

            BrandManager brandManager = new ICP4.BusinessLogic.BrandManager.BrandManager();

            brandManager.GetLocalResource(variant, brandCode);


            using (CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager())
            {
                HeadingLegacyRetired = cacheManager.GetResourceValueByResourceKey(ResourceKeyNames.HeadingLegacyRetired, brandCode, variant);
                
                H2heading.InnerText = HeadingLegacyRetired;
                LegacyRetiredText = cacheManager.GetResourceValueByResourceKey(ResourceKeyNames.LegacyRetiredText, brandCode, variant);
              
                instructorInformationText.InnerHtml = LegacyRetiredText;
            }
        }
    }
}
