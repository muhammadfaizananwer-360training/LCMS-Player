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

namespace ICP4.CoursePlayer
{
    public partial class DynamicPageLoader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["url"] != null && Request.QueryString["url"].ToString().Trim() != "")
                {
                    //Response.Redirect(Request.QueryString["url"].ToString());
                    ltCode.Text = "<iframe  frameborder=\"0\" border=\"0\" width=\"100%\" height=\"100%\" name=\"ifr\" src=\"" + Request.QueryString["url"].ToString() + "\"></iframe>";
                }
            }
        }
    }
}
