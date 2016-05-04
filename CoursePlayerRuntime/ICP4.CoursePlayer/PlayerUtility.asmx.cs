using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ICP4.BusinessLogic.CacheManager;
using ICP4.DataLogic.PlayerServerDA;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ICP4.CoursePlayer
{
    /// <summary>
    /// Summary description for PlayerUtility
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PlayerUtility : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

/*
        [WebMethod]
        public bool AddUpdateCache(
                    int publishedCourseId,

                    ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration,
                    ICP4.BusinessLogic.ICPCourseService.Sequence courseSequence,
                    ICP4.BusinessLogic.ICPCourseService.Sequence courseDemoSequence,
                    ICP4.BusinessLogic.ICPCourseService.TableOfContent courseTOC)
        {
            
            using (CacheManager cacheManager = new CacheManager())
            {
                return cacheManager.AddUpdateCourseCache(publishedCourseId, courseConfiguration, courseSequence, courseDemoSequence, courseTOC);                
            }            

        }
*/

        [WebMethod]
        public bool InvalidateCache(int publishedCourseId)
        {
            return InvalidateCacheAndNotifyToAllRemainingServers(publishedCourseId, false);
        }

        [WebMethod]
        public bool InvalidateCacheAndNotifyToAllRemainingServers(int publishedCourseId, bool notifytoAllRemainingServers)
        {
            PlayerServerDA playerServerDA = new PlayerServerDA(ConfigurationManager.AppSettings["PlayerServerSettingsFilePath"].ToString());

            try
            {
                using (CacheManager cacheManager = new CacheManager())
                {
                    cacheManager.InvalidateCache(publishedCourseId, notifytoAllRemainingServers, 0);
                    return cacheManager.InvalidateCache(publishedCourseId, notifytoAllRemainingServers, 1);
                    //throw new Exception("Hi test exception email");
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "ICPException");
                playerServerDA.AddPlayerServerCacheLog(System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2].ToString(), publishedCourseId, 0, ex.ToString());
                EmailUtility emailUtility = new EmailUtility();
                String fromEmail = ConfigurationManager.AppSettings["FromEmailCacheInvalidation"].ToString();
                String toEmail = ConfigurationManager.AppSettings["ToEmailCacheInvalidation"].ToString();
                String subject = "Exception (" + ex.Message + ") while cache invalidating - " + System.Net.Dns.GetHostName() + " : " + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2];
                String body = "<b> Host Name : </b>" + System.Net.Dns.GetHostName();
                body += "<br><b> Host Address : </b>" + System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2];
                body += "<br><b> DateTime : </b>" + System.DateTime.Now;
                body += "<br><br><b> Exception : </b><br>" + ex.ToString();


                String smtpServer = ConfigurationManager.AppSettings["SMTPAddress"].ToString();
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());

                emailUtility.sendMail(toEmail, fromEmail, "", "", subject, body, smtpServer, smtpPort);
                return false;
            }
        }

        [WebMethod]
        public bool InvalidateCourseConfigurationCache(int courseConfigurationID)
        {
            using (CacheManager cacheManager = new CacheManager())
            {
                return cacheManager.InvalidateCourseConfigurationCache(courseConfigurationID); 
            }
        }

        [WebMethod]
        public bool InvalidateCourseApprovalCache(int courseApprovalID, int courseID)
        {
            using (CacheManager cacheManager = new CacheManager())
            {
                cacheManager.InvalidateCourseApprovalCache(courseApprovalID);
                return InvalidateCacheAndNotifyToAllRemainingServers(courseID, false);
            }
        }

        [WebMethod]
        public int GetSessionTimeOutKeyValue()
        {
            System.Web.Configuration.SessionStateSection sessionStateSection = (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
            return sessionStateSection.Timeout.Minutes;
        }

        [WebMethod]
        public string GetAppSettingKeyValue(string Key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Key].ToString();
        }

        [WebMethod]
        public bool InvalidateBrandCache(string brandCode, string variant)
        {
            BusinessLogic.BrandManager.BrandManager brandManager = new BusinessLogic.BrandManager.BrandManager();

            return brandManager.UpdateBrand(variant, brandCode);
        }

    }
}
