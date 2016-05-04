using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProctorLauncher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        WebReference.ExternalCourseActions ws = new WebReference.ExternalCourseActions();
        bool bool1 = ws.LockCourse(TextBox1.Text.Trim(), "Course", "360training");
        if(bool1)
            Response.Write("Course has been locked successfully");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        WebReference.ExternalCourseActions ws = new WebReference.ExternalCourseActions();
        bool bool1 = ws.UnLockCourse(TextBox1.Text.Trim(), "Course", "360training");
        if (bool1)
            Response.Write("Course has been locked successfully");

    }
}
