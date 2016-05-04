using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ICP4.BusinessLogic.CourseManager;
using System.Web.Script.Services;



namespace ICP4.CoursePlayer
{
    /// <summary>
    /// Summary description for ExternalCourseActions
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//     [System.Web.Script.Services.ScriptService]
    public class ExternalCourseActions : System.Web.Services.WebService
    {

        [WebMethod]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool LockCourse(String LearningSessionGuid, String Mode, string SecurityCode)
        {
            System.Diagnostics.Trace.WriteLine("LockCourse : 1" + LearningSessionGuid + ":" + Mode + ":" + SecurityCode);
            System.Diagnostics.Trace.Flush();

            using (ProctorManagement proctorManagement = new ProctorManagement())
            {
                return proctorManagement.LockCourse(LearningSessionGuid, Mode, SecurityCode);
            }
            
        }

        [WebMethod]
        public bool UnLockCourse(String LearningSessionGuid, String Mode, string SecurityCode)
        {
            using (ProctorManagement proctorManagement = new ProctorManagement())
            {
                return proctorManagement.UnLockCourse(LearningSessionGuid, Mode, SecurityCode);
            }
        }
    }
}
