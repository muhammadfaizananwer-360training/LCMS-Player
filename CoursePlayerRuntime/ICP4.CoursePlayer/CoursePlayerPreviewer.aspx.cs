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
    public partial class CoursePlayerPreviewer : System.Web.UI.Page
    {       
        /// <summary>
        /// Javascript code to open the player window
        /// </summary>
        
//        private static string jsOpenWindow = "window.open('$$URL$$','','menubar=0,scroll bars=no,width=1024,height=660,top=0,left=0,resizable=1,location=0,toolbar=0,directories=0')";
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["COURSEID"] != null)
            {
                string queryString = Request.RawUrl.Split('?')[1];
                string url = this.ResolveUrl("~/CoursePlayer.aspx?" + queryString);
                //buttonPreview.Attributes.Add("onclick", jsOpenWindow.Replace("$$URL$$", url)); 
                buttonPreview.Attributes.Add("onclick", "openWin('" + url + "');");

            }
            //else
            //{
            //    string queryString = "COURSEID=7469&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=4db463f3-5115-4d89-ad68-15abde86c0aa";
            //    string url = this.ResolveUrl("~/CoursePlayer.aspx?" + queryString);
            //    buttonPreview.Attributes.Add("onclick", "openWin('" + url + "');");
            //}
        }
    }
}
