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
    public partial class CourseDemoPreviewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This will be the course guid
            if (Request.QueryString["courseGUID"] != null)
            {
                //Get the course type first and check whether legacy or any other
                string courseType = string.Empty;
                //Get the course guid from querystring
                string courseGUID = Request.QueryString["courseGUID"];
                using (ICP4.BusinessLogic.CourseManager.CourseManager courseManager = new ICP4.BusinessLogic.CourseManager.CourseManager())
                {
                 courseType = courseManager.GetCourseTypeByGUID(Request.QueryString["courseGUID"]);
                }
                //if legace course 
                if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.LegacyCourse)
                {
                    string url = ConfigurationManager.AppSettings["VUCourseDemoURL"];
                    Response.Redirect(url + "?courseGUID=" + courseGUID);                    
                }
                else if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.SelfPacedCourse)
                {                                        
                    //add it to courseplayer with demoable bit
                    string url = this.ResolveUrl("~/CoursePlayer.aspx?courseGUID=" + courseGUID + "&DEMO=True");
                    Response.Redirect(url);
                }
                else if (courseType == ICP4.BusinessLogic.CourseManager.CourseType.WebLinkCourse)
                {
                    // Don't know the implementation
                    Response.Write("Preview not available.");
                }
                else
                {
                    Response.Write("Preview not available.");
                }
            }
        }
    }
}
