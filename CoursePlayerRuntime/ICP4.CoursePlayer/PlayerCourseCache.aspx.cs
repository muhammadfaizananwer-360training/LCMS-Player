using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICP4.CoursePlayer
{
    public partial class PlayerCourseCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String errorMessage = "";
            String message = "";
            if (CourseID.Text.Trim().Length == 0)
            {
                errorMessage = "Course ID is required.";
            }
            else
            {
                int courseId = 0;
                try
                {
                    courseId = Convert.ToInt32(CourseID.Text.Trim());
                }
                catch (FormatException ex)
                {
                    errorMessage = "Please enter valid Course ID.";
                }

                if (courseId > 0)
                {
                    PlayerUtility playerUtility = new PlayerUtility();
                    if (playerUtility.InvalidateCacheAndNotifyToAllRemainingServers(courseId, true))
                    {
                        message = "Player course cache reset successfully.";
                    }
                    else
                    {
                        errorMessage = "Player course cache reset successfully.";
                    }
                }
                
            }
            if (!errorMessage.Equals(""))
            {
                Message.Text = errorMessage;
                Message.CssClass = "error";
            }
            else
            {
                Message.Text = message;
                Message.CssClass = "message";
            }

            
            
        }
    }
}
