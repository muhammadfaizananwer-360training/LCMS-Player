using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        int course_id = Convert.ToInt32(Request.QueryString["course_id"]);
        int student_id = Convert.ToInt32(Request.QueryString["student_id"]);
        int epoch = Convert.ToInt32(Request.QueryString["epoch"]);
        String learningSessionGUID = Request.QueryString["learningSessionGUID"];

        PlayerUtil.debugMessage(Session.SessionID +  " : - Request Recieved ** : course_id =" + course_id + ", student_id=" + student_id + ", epoch" + epoch);        
                
        

        if (Session["course_id"] == null)
        {
            Session["course_id"] = course_id;
            Session["student_id"] = student_id;
            Session["epoch"] = epoch;

            PlayerUtil.debugMessage(Session.SessionID + " : - Request Recieved : course_id =" + course_id + ", student_id=" + student_id + ", epoch=" + epoch);
            
        }

    }
}
