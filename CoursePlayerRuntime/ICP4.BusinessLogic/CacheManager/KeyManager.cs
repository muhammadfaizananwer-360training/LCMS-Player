using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;

namespace ICP4.BusinessLogic.CacheManager
{
    public static class KeyManager
    {

        public static string GetCourseKey()
        {
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"].ToString());
            int sourceID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString()); 
            return "COURSE" + "_" + courseID.ToString() + "_" + sourceID.ToString();
        }

        public static string GetCourseConfigurationKey()
        {
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"].ToString());
            int sourceID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString());
            int courseApprovalID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseApprovalID"].ToString());
            if (courseApprovalID > 0)
            {
                return "COURSECONFIGURATION" + "_" + courseID.ToString() + "_" + sourceID.ToString() + "_" + courseApprovalID.ToString();
            }
            else
            {
                return "COURSECONFIGURATION" + "_" + courseID.ToString() + "_" + sourceID.ToString();
            }
        }

        public static string GetCourseSequenceKey()
        {
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"].ToString());
            int sourceID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString());
            return "COURSESEQUENCE" + "_" + courseID.ToString() + "_" + sourceID.ToString();
        }

        public static string GetDemoCourseSequenceKey()
        {
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"].ToString());
            int sourceID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString());
            return "DEMOCOURSESEQUENCE" + "_" + courseID.ToString();
        }

        public static string GetCourseTOCKey()
        {
            int courseID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseID"].ToString());
            int sourceID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"].ToString());
            return "COURSETOC" + "_" + courseID.ToString() + "_" + sourceID.ToString();
        }

        public static string GetBrandKey()
        {
            string brandCode = System.Web.HttpContext.Current.Session["BrandCode"].ToString();
            string variant = System.Web.HttpContext.Current.Session["Variant"].ToString();
            return "COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString();
        }
    }
}
