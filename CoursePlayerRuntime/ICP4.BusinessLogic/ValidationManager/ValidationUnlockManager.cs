using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using _360Training.BusinessEntities;
using System.Web;
using System.Net;

namespace ICP4.BusinessLogic.ValidationManager
{
    public class ValidationUnlockManager : IDisposable
    {
        public ValidationUnlockManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }


        public bool CreateValidationUnlockRequest(string learningSessionId,string brandCode,string variant)
        {//validationTimerValue
            
            try 

            {
                using (ICPCourseService.CourseService courseService = new ICPCourseService.CourseService())
                {
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    return courseService.CreateValidationUnlockRequest(learningSessionId,brandCode,variant);
                }

              
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
        }

        public LearnerProfile GetUserProfile(string learningSessionId)
        {
            try
            {
                using (ICPTrackingService.TrackingService trackingService = new ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                    return LearnerProfileTranslator(trackingService.GetUserProfile(learningSessionId));
                }


               
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
        }
        private LearnerProfile LearnerProfileTranslator(ICPTrackingService.LearnerProfile serviceLearnerProfile)
        {
            LearnerProfile learnerProfile = new LearnerProfile();
            learnerProfile.Address1 = serviceLearnerProfile.Address1;
            learnerProfile.Address2 = serviceLearnerProfile.Address2;
            learnerProfile.Address3 = serviceLearnerProfile.Address3;
            learnerProfile.City = serviceLearnerProfile.City;
            learnerProfile.EmailAddress = serviceLearnerProfile.EmailAddress;
            learnerProfile.FirstName = serviceLearnerProfile.FirstName;
            learnerProfile.Id = serviceLearnerProfile.Id;
            learnerProfile.LastName = serviceLearnerProfile.LastName;
            learnerProfile.LearningSessionID = serviceLearnerProfile.LearningSessionID;
            learnerProfile.MobilePhone = serviceLearnerProfile.MobilePhone;
            learnerProfile.OfficePhone = serviceLearnerProfile.OfficePhone;
            learnerProfile.Country = serviceLearnerProfile.Country;
            learnerProfile.State = serviceLearnerProfile.State;
            learnerProfile.ZipCode = serviceLearnerProfile.ZipCode;
            learnerProfile.BrandCode = serviceLearnerProfile.BrandCode;
            learnerProfile.Variant = serviceLearnerProfile.Variant;
            return learnerProfile;
            
        }

        private ICPTrackingService.LearnerProfile LearnerProfileTranslator(LearnerProfile learnerProfile)
        {
            ICPTrackingService.LearnerProfile profile = new ICPTrackingService.LearnerProfile();
            profile.Address1 = learnerProfile.Address1;
            profile.Address2 = learnerProfile.Address2;
            profile.Address3 = learnerProfile.Address3;
            profile.City = learnerProfile.City;
            profile.EmailAddress = learnerProfile.EmailAddress;
            profile.FirstName = learnerProfile.FirstName;
            profile.Id = learnerProfile.Id;
            profile.LastName = learnerProfile.LastName;
            profile.LearningSessionID = learnerProfile.LearningSessionID;
            profile.MobilePhone = learnerProfile.MobilePhone;
            profile.OfficePhone = learnerProfile.OfficePhone;
            profile.Country = learnerProfile.Country;
            profile.State = learnerProfile.State;
            profile.ZipCode = learnerProfile.ZipCode;
            profile.BrandCode = learnerProfile.BrandCode;
            profile.Variant = learnerProfile.Variant;
            return profile;

        }

        public bool UpdateLearnerProfile(LearnerProfile profile)
        {
            try
            {
                using (ICPTrackingService.TrackingService trackingService = new ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];

                    return trackingService.UpdateLearnerProfile(profile.LearningSessionID, LearnerProfileTranslator(profile));
                }



            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }
         
        }

        public void GetLearningSessionBrandcodeVariant(string learningSessionId)
        {
            //GetLearningSessionBrandcodeVariant
            try
            {
                using (ICPTrackingService.TrackingService trackingService = new ICPTrackingService.TrackingService())
                {
                    trackingService.Url = ConfigurationManager.AppSettings["ICPTrackingService"];
                    string brandCode = string.Empty;
                    string variant = string.Empty;

                    brandCode = trackingService.GetLearningSessionBrandcodeVariant(learningSessionId, out variant);
                    HttpContext.Current.Session["BrandCode"] = brandCode;
                    HttpContext.Current.Session["Variant"] = variant;
                }



            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                throw ex;
            }

        }



        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
