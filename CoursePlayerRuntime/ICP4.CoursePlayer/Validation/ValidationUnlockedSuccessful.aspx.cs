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
using System.Xml.Linq;

namespace ICP4.CoursePlayer.Validation
{
    public partial class ValidationUnlockedSuccessful : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BusinessLogic.CacheManager.CacheManager cacheManager = new ICP4.BusinessLogic.CacheManager.CacheManager();
            if (HttpContext.Current.Session["BrandCode"] != null && HttpContext.Current.Session["Variant"] != null)
            {
                string imageURL = cacheManager.GetResourceValueByResourceKey(BusinessLogic.BrandManager.ResourceKeyNames.ImageComanyLogo, HttpContext.Current.Session["BrandCode"].ToString(), HttpContext.Current.Session["Variant"].ToString());
                imgLogo.Src = imageURL;
            }
        }
    }
}
