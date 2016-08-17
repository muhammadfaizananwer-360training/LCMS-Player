using System;
using System.Configuration ;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Net;
using ICP4.BusinessLogic.ICPCourseService;
using ICP4.BusinessEntities;
using ICP4.DataLogic.PlayerServerDA;
using ICP4.BusinessLogic;

namespace ICP4.BusinessLogic.CacheManager
{
    public class CacheManager : IDisposable
    {
        public CacheManager()
        {
            ServicePointManager.CertificatePolicy = new CommonAPI.AcceptAllCertificatePolicy();
        }

        /// <summary>
        /// This method get Course Configuration from cache if exist
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <returns>ICPCourseService.CourseConfiguration object</returns>
        public ICPCourseService.CourseConfiguration GetIFConfigurationExistInCache(int courseConfigurationID)
        {
            ICPCourseService.CourseConfiguration courseConfiguration = new ICP4.BusinessLogic.ICPCourseService.CourseConfiguration(); 
            try
            {
                if (HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()] == null)
                {
                    ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    courseConfiguration = courseService.GetCourseConfiguaration(courseConfigurationID);
                    CreateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
                    return courseConfiguration;
                   
                }
                else
                {
                    courseConfiguration = (ICPCourseService.CourseConfiguration)HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()];                    
                }
                return courseConfiguration;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }


        /// <summary>
        /// This method get Course approval from cache if exist
        /// </summary>        
        /// <returns>Dictionary<int,DateTime> object</returns>
        public Dictionary<int, DateTime> GetCourseApprovalInCache()
        {
            Dictionary<int, DateTime> courseapprovals = null;  
            try
            {
                if (HttpRuntime.Cache["COURSEAPPROVAL"] != null)
                {
                    courseapprovals =(Dictionary<int,DateTime>)HttpRuntime.Cache["COURSEAPPROVAL"];
                }                
                return courseapprovals;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }
       
        /// <summary>
        /// This method get Course Sequence from cache if exist
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <returns>ICPCourseService.Sequence object</returns>
        public ICPCourseService.Sequence GetIFSequenceExistInCache(int courseID, int source, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = new ICP4.BusinessLogic.ICPCourseService.Sequence();
            try
            {
                if (HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()] == null)
                {
                    ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    sequence = courseService.GetSequence(courseID, courseService.GetCourseConfiguaration(courseConfigurationID));
                    CreateCourseSequenceInCache(courseID, sequence, source, courseConfigurationID);
                    return sequence;                    
                }
                else
                {
                   // sequence = null;
                    sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];

                }
                return sequence;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }

        /// <summary>
        /// This method get Course Sequence from cache if exist in DEMO mode.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <returns>ICPCourseService.Sequence object</returns>
        public ICPCourseService.Sequence GetIFDemoSequenceExistInCache(int courseID)
        {
            ICPCourseService.Sequence sequence = new ICP4.BusinessLogic.ICPCourseService.Sequence();
            try
            {
                if (HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseID.ToString()] == null)
                {

                    ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    sequence = courseService.GetCourseDemoSequence(courseID);
                    CreateDemoCourseSequenceInCache(courseID, sequence);
                    return sequence;                    
                }
                else
                {
                    // sequence = null;
                    sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseID.ToString()];

                }
                return sequence;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }


        /// <summary>
        /// This method finds resource value from ICPBrandingService.BrandLocaleInfo saved in cache
        /// </summary>
        /// <param name="resourceKey">ResourceKey string value</param>
        /// <param name="brandCode">BrandCode string value</param>
        /// <param name="variant">Variant string valye</param>
        /// <returns>Resource value</returns>
        public string GetResourceValueByResourceKey(string resourceKey,string brandCode,string variant)
        {
            ICPBrandingService.BrandLocaleInfo brandLocaleInfo = GetIFBrandInfoExistInCache(brandCode, variant);
            foreach (ICPBrandingService.LocaleResource localeResource in brandLocaleInfo.LocaleResourceList)
            {
                if (localeResource.ResourceKey == resourceKey)
                    return localeResource.ResourceValue;
            }
            return null;
        }


        /// <summary>
        /// This method get Course Brading information from cache if exist
        /// </summary>
        /// <param name="brandCode">BrandCode string value, represent the key for cache</param>
        /// <param name="variant">Variant string value, represent the key for cache </param>
        /// <returns>ICPBrandingService.BrandLocaleInfo object</returns>
        public ICPBrandingService.BrandLocaleInfo GetIFBrandInfoExistInCache(string brandCode,string variant)
        {
            ICPBrandingService.BrandLocaleInfo brandLocaleInfo = new ICPBrandingService.BrandLocaleInfo();
            try
            {
                if (HttpRuntime.Cache["COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString()] == null)
                {
                    brandLocaleInfo = null;
                }
                else
                {
                    brandLocaleInfo = (ICPBrandingService.BrandLocaleInfo)HttpRuntime.Cache["COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString()];
                }


                return brandLocaleInfo;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }

        /// <summary>
        /// This method get Course Image from cache if exist
        /// </summary>
        /// <param name="CourseID">CourseID int value, represent the key for cache</param>        
        /// <returns>ICPBrandingService.BrandLocaleInfo object</returns>
        public string GetIFCourseImageExistInCache(int CourseID)
        {
            string courseImage = null;
            try
            {
                if (HttpRuntime.Cache["COURSEIMAGE" + "_" + CourseID.ToString()] == null)
                {
                    courseImage = null;
                }
                else
                {
                    courseImage = (string)HttpRuntime.Cache["COURSEIMAGE" + "_" + CourseID.ToString()];
                }
                return courseImage;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }
        }

        /// <summary>
        /// This method get Course Product Page from cache if exist
        /// </summary>
        /// <param name="CourseID">CourseID int value, represent the key for cache</param>        
        /// <returns>ICPBrandingService.BrandLocaleInfo object</returns>
        public string GetIFCourseProductPageExistInCache(int CourseID)
        {
            string courseImage = null;
            try
            {
                if (HttpRuntime.Cache["COURSEPRODUCTPAGE" + "_" + CourseID.ToString()] == null)
                {
                    courseImage = null;
                }
                else
                {
                    courseImage = (string)HttpRuntime.Cache["COURSEPRODUCTPAGE" + "_" + CourseID.ToString()];
                }
                return courseImage;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }
        }


        /// <summary>
        /// This method create Course Configuration in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="courseConfiguration">CourseConfiguration, ICPCourseService.CourseConfiguration</param>
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseConfigurationInCache(int courseConfigurationID, ICPCourseService.CourseConfiguration courseConfiguration)
        {
            
            try
            {
                if (HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()] == null)
                {
                    HttpRuntime.Cache.Add("COURSECONFIGURATION" + "_" + courseConfigurationID.ToString(), courseConfiguration, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }               
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }


        /// <summary>
        /// This method create Course Sequence in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="courseSequence">CourseSequence, ICPCourseService.Sequence</param>
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseSequenceInCache(int courseID, ICPCourseService.Sequence courseSequence, int source, int courseConfigurationID)
        {

            try
            {
                if (HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()] == null)
                {

                    HttpRuntime.Cache.Add("COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString(), courseSequence, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }

        /// <summary>
        /// This method create Course Image in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>        
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseImageInCache(int courseID, string courseImageURL)
        {

            try
            {
                if (HttpRuntime.Cache["COURSEIMAGE" + "_" + courseID.ToString()] == null)
                {

                    HttpRuntime.Cache.Add("COURSEIMAGE" + "_" + courseID.ToString(), courseImageURL, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }
        }

        /// <summary>
        /// This method create Course Product Page URL in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>        
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseProductPageInCache(int courseID, string courseProductPageURL)
        {

            try
            {
                if (HttpRuntime.Cache["COURSEPRODUCTPAGE" + "_" + courseID.ToString()] == null)
                {

                    HttpRuntime.Cache.Add("COURSEPRODUCTPAGE" + "_" + courseID.ToString(), courseProductPageURL, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }
        }

        /// <summary>
        /// This method create Course Sequence in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="courseSequence">CourseSequence, ICPCourseService.Sequence</param>
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseTOCInCache(int courseID, ICPCourseService.TableOfContent TOC, int source)
        {
            try
            {
                if (HttpRuntime.Cache["COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString()] == null)
                {
                    HttpRuntime.Cache.Add("COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString(), TOC, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
             
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }

        public ICPCourseService.TableOfContent GetCourseTOCInCache(int courseID, int source)
        {
            try
            {
                if (HttpRuntime.Cache["COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString()] == null)
                {
                    ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
                    courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                    courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

                    ICPCourseService.TableOfContent serviceTableOfContent = courseService.GetTableOfContent(courseID);
                    CreateCourseTOCInCache(courseID, serviceTableOfContent, source);
                    return serviceTableOfContent;
                }
                else
                {
                    return (ICPCourseService.TableOfContent)HttpRuntime.Cache["COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString()];
                }
                
              
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }

        public bool RemoveCourseTOCInCache(int courseID, int source)
        {
            try
            {
                if (HttpRuntime.Cache["COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString()] == null)
                {

                    HttpRuntime.Cache.Remove("COURSETOC" + "_" + courseID.ToString() + "_" + source.ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }
        }

        /// <summary>
        /// This method create Course Sequence in cache in DEMO mode
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="courseSequence">CourseSequence, ICPCourseService.Sequence</param>
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateDemoCourseSequenceInCache(int courseID, ICPCourseService.Sequence courseSequence)
        {
            try
            {
                if (HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseID.ToString()] == null)
                {
                    HttpRuntime.Cache.Add("COURSEDEMOSEQUENCE" + "_" + courseID.ToString(), courseSequence, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }
        }

        /// <summary>
        /// This method create Course Branding information in cache
        /// </summary>
        /// <param name="brandCode">BrandCode string value, represnt key in for cache</param>
        /// <param name="variant">Variant string value, represnt key in for cache</param>
        /// <param name="brandLocaleInfo">BrandLocaleInfo, ICPBrandingService.BrandLocaleInfo</param>
        /// <returns></returns>
        public bool CreateCourseBrandInfoInCache(string brandCode,string variant, ICPBrandingService.BrandLocaleInfo brandLocaleInfo)
        {

            try
            {
                if (HttpRuntime.Cache["COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString()] == null)
                {

                    HttpRuntime.Cache.Add("COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString(), brandLocaleInfo, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
                else
                {
                    HttpRuntime.Cache["COURSEBRANDEDLOCALE" + "_" + brandCode.ToString() + "_" + variant.ToString()] = brandLocaleInfo;
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }

  
        /// <summary>
        /// This method get item from Sequence stored on cache according to NEXT/Back signal
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="direction">Direction interger value, 1 means NEXT/ -1 means BACK</param>
        /// <param name="seqNo">SeqNo integer value, represent the index of sequence</param>
        /// <returns>ICPCourseService.SequenceItem object</returns>
        public ICPCourseService.SequenceItem GetNextBackItem(int courseID,int direction,int seqNo)
        {
            int currentIndex = seqNo;

            /// This IF check wehther the move is valid or not.
            /// For suppose, if user is already at the first slide and press BACK, then this move is not valid 
            /// because we cant take him/her back from this position. Similary if user is already present at the end of 
            /// the course and presses NEXT then system will not allow him to go further.
            int source = Convert.ToInt32(System.Web.HttpContext.Current.Session["Source"]);
            int courseConfigurationID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CourseConfigurationID"]);
            if (CheckIsMoveValid(courseID, direction, currentIndex, source, courseConfigurationID))
            {
                currentIndex += direction;
                return GetRequestedItemFromQueue(courseID, currentIndex, source, courseConfigurationID);
            }

            return null;
        }

        /// <summary>
        /// This method get item from Sequence stored on cache according to NEXT/Back signal in DEMO mode
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="direction">Direction interger value, 1 means NEXT/ -1 means BACK</param>
        /// <param name="seqNo">SeqNo integer value, represent the index of sequence</param>
        /// <returns>ICPCourseService.SequenceItem object</returns>
        public ICPCourseService.SequenceItem GetDemoNextBackItem(int courseID, int direction, int seqNo)
        {
            int currentIndex = seqNo;

            /// This IF check wehther the move is valid or not.
            /// For suppose, if user is already at the first slide and press BACK, then this move is not valid 
            /// because we cant take him/her back from this position. Similary if user is already present at the end of 
            /// the course and presses NEXT then system will not allow him to go further.
            if (CheckIsMoveValidInDemo(courseID, direction, currentIndex))
            {
                currentIndex += direction;
                return GetRequestedDemoItemFromQueue(courseID, currentIndex);
            }

            return null;
        }

        /// <summary>
        /// This method get item from Sequence stored on cache according to specified index
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="specificIndex">SpecificIndex integer value, represent the index of sequence</param>
        /// <param name="source">source integer value, represent the source</param>
        /// <returns>ICPCourseService.SequenceItem object</returns>
        public ICPCourseService.SequenceItem GetRequestedItemFromQueue(int courseID, int specificIndex, int source, int courseConfigurationID)
        {
            int currentIndex = specificIndex;            
            ICPCourseService.SequenceItem sequenceItem = null;
            ICPCourseService.Sequence sequence = ((ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()]);
           
            if (sequence.SequenceItems.Length > 0)
            {
                sequenceItem = sequence.SequenceItems[currentIndex];
            }
            return sequenceItem; 

        }

        /// <summary>
        /// This method get item from Sequence stored on cache according to specified index in DEMO mode
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="specificIndex">SpecificIndex integer value, represent the index of sequence</param>
        /// <returns>ICPCourseService.SequenceItem object</returns>
        public ICPCourseService.SequenceItem GetRequestedDemoItemFromQueue(int courseID, int specificIndex)
        {
            int currentIndex = specificIndex;            
            ICPCourseService.SequenceItem sequenceItem = null;
            ICPCourseService.Sequence sequence = ((ICPCourseService.Sequence)HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseID.ToString()]);
            
            if (sequence.SequenceItems.Length > 0)
            {
                sequenceItem = sequence.SequenceItems[currentIndex];
            }
            return sequenceItem;

        }

        /// <summary>
        /// This method searches for SequenceItem no. It will search in Sequence stored in cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="itemGUID">ItemGUID string value, represent the ITEMGUID of sequenceItem</param>
        /// <param name="sceneGUID">SceneGUID string value, represent the ITEMGUID of sequenceItem</param>
        /// <param name="statisticsType">StatisticsType string value, represent the type of sequenceItem</param>
        /// <returns>Integer value returns -1 if item exists but not found</returns>
        public int GetRequestedItemNoFromQueue(int courseID, string itemGUID, string sceneGUID, string statisticsType, int source, int courseConfigurationID)
        {

            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];

            if (statisticsType == CourseManager.LearnerStatisticsType.PreAssessmentEnd || statisticsType == ICP4.BusinessLogic.CourseManager.SequenceItemTypeName.PreAssessment)//|| statisticsType == "")
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsTypeTranslator.ConvertLearnerStatisticsTypeToSequenceType(CourseManager.LearnerStatisticsType.PreAssessment))
                    {
                        return index;
                    }
                    else if (sequence.SequenceItems[index].ExamType == "PreAssessment" && sequence.SequenceItems[index].SequenceItemType == "Exam")
                    {
                        return index;
                    } 
                }
           
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.PostAssessmentEnd || statisticsType == ICP4.BusinessLogic.CourseManager.SequenceItemTypeName.PostAssessment)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsTypeTranslator.ConvertLearnerStatisticsTypeToSequenceType(CourseManager.LearnerStatisticsType.PostAssessment))
                    {
                        return index;
                    }
                    else if (sequence.SequenceItems[index].ExamType == "PostAssessment" && sequence.SequenceItems[index].SequenceItemType == "Exam")
                    {
                        return index;
                    } 
                }
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.QuizEnd || statisticsType == ICP4.BusinessLogic.CourseManager.SequenceItemTypeName.Quiz)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].Item_GUID == itemGUID && sequence.SequenceItems[index].SceneGUID == sceneGUID && sequence.SequenceItems[index].SequenceItemType == ICP4.BusinessLogic.CourseManager.LearnerStatisticsTypeTranslator.ConvertLearnerStatisticsTypeToSequenceType(CourseManager.LearnerStatisticsType.Quiz))                    
                    {
                        return index;
                    }
                    else if (sequence.SequenceItems[index].Item_GUID == itemGUID && sequence.SequenceItems[index].SequenceItemType == "Exam")
                    {                      
                        return index;
                    }                    
                }
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.IntroPage)
            {
               return 0;
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.EndOfCourseScene)
            {
                return sequence.SequenceItems.Length - 1;
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.LessonIntroductionScene)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].Item_GUID == itemGUID && sequence.SequenceItems[index].SceneGUID == sceneGUID)
                    {
                        return index;
                    }
                }
                return -1;
            }
            else if (itemGUID != "" || sceneGUID != "")
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].Item_GUID == itemGUID && sequence.SequenceItems[index].SceneGUID == sceneGUID)
                    {
                        return index;
                    }
                }
                return -1;
            }
            /*
                Add Logic to find the squence number for EOCInstructions.
                Changes made by Waqas Zakai 25-Feb-2010
                LCMS-3508
             */
            else if (statisticsType == CourseManager.LearnerStatisticsType.EOCInstructions)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.EOCInstructions)
                    {
                        return index;
                    }
                }
            }
            /*
                Add Logic to find the squence number for CourseCertificateScene.
                Changes made by Waqas Zakai 25-Feb-2010
                LCMS-3508
             */
            else if (statisticsType == CourseManager.LearnerStatisticsType.CourseCertificate)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.CourseCertificate)
                    {
                        return index;
                    }
                }
            }
            /*
                Add Logic to find the squence number for EmbeddedAcknowledgmentScene.
                Changes made by Waqas Zakai 25-Feb-2010
                LCMS-3508
             */
            else if (statisticsType == CourseManager.LearnerStatisticsType.EmbeddedAcknowledgmentScene || statisticsType == CourseManager.LearnerStatisticsType.EmbeddedAcknowledgmentAgreedScene)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.EmbeddedAcknowledgmentScene || sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.EmbeddedAcknowledgmentAgreedScene)
                    {
                        return index;
                    }
                }
            }
            //LCMS-11877
            // Need to check with Waqas Z, on do we need this check here
            else if (statisticsType == CourseManager.LearnerStatisticsType.CourseRating)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.CourseRating)
                    {
                        return index;
                    }
                }
            }

            else if (statisticsType == CourseManager.LearnerStatisticsType.CourseEvaluation)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.CourseEvaluation)
                    {
                        return index;
                    }
                }
                return -1;
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.SpecialQuestionnaire)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.SpecialQuestionnaire)
                    {
                        return index;
                    }
                }
                return -1;
            }
            else if (statisticsType == CourseManager.LearnerStatisticsType.KnowledgeCheck)
            {
                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].SequenceItemType == CourseManager.LearnerStatisticsType.KnowledgeCheck)
                    {
                        return index;
                    }
                }
                return -1;
            }
            return 0;
        }


        /// <summary>
        /// This method searches for SequenceItem no. It will search in Sequence stored in cache based on ITEMGUID and SCENEGUID
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="itemGUID">ItemGUID string value, represent the ITEMGUID of sequenceItem</param>
        /// <param name="sceneGUID">SceneGUID string value, represent the ITEMGUID of sequenceItem</param>
        /// <returns>Integer value, returns -1 if item exists but not found</returns>
        public int GetRequestedItemNoFromQueueOnItemGUIDAndScenGUID(int courseID, string itemGUID, string sceneGUID, int source, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];

            if (itemGUID != "" || sceneGUID != "")
            {

                for (int index = 0; index < sequence.SequenceItems.Length; index++)
                {
                    if (sequence.SequenceItems[index].Item_GUID == itemGUID && sequence.SequenceItems[index].SceneGUID == sceneGUID)
                    {
                        return index;
                    }
                }
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// This method gets the sequenceitem index of SequenceItem stored in cached Sequence according to SequenceItemID
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="sequenceItemID">SequenceItemID integer value, represent the ID of sequenceItem</param>
        /// <returns>Integer value</returns>
        public int GetRequestedItemNoFromQueueOnSequenceItemID(int courseID, int sequenceItemID, String type, int source, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];
            for (int index = 0; index < sequence.SequenceItems.Length; index++)
            {
                if (sequence.SequenceItems[index].SequenceItemID == sequenceItemID && (type == null || type == sequence.SequenceItems[index].SequenceItemType))
                {
                    return index;
                }
            }
            return 0;
        }

        /// <summary>
        /// This method gets the sequenceitem type from Sequence stored on cache according by searching on sequenceItemID
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="sequenceItemID">SequenceItemID integer value, represent the ID of sequenceItem</param>
        /// <returns>String value</returns>
        public string GetSequenceItemType(int courseID, int sequenceItemID, int source, int courseConfigurationID)
        {
            try
            {
                ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];

                return sequence.SequenceItems[sequenceItemID].SequenceItemType;
            }
            catch (Exception exp)
            {
                return "";
            }

        }
        /// <summary>
        /// This function finds the previous asset
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="seqNo"></param>
        /// <param name="sequenceItem"></param>
        /// <returns>index>=0 if finds else -1</returns>
        public int GetSceneOrAssetBeforeContentObject(int courseID, int seqNo, int source, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];
            for (int index = seqNo; index >= 0; index--)
            {
                //if (sequence.SequenceItems[index].SequenceItemType == "ContentAsset" || sequence.SequenceItems[index].SequenceItemType == "FlashAsset")
                if (sequence.SequenceItems[index].SequenceItemType != "ContentObject" && sequence.SequenceItems[index].SequenceItemType != "Quiz"
                    && sequence.SequenceItems[index].SequenceItemType != "PreAssessment" && sequence.SequenceItems[index].SequenceItemType != "PostAssessment" && sequence.SequenceItems[index].ExamType != "PreAssessment" && sequence.SequenceItems[index].ExamType != "Quiz" && sequence.SequenceItems[index].ExamType != "PostAssessment" && sequence.SequenceItems[index].IsNotActive==false)
                {
                    return index;
                }
            }
            return -1;
        }
        /// <summary>
        /// This method searches Sequence to find first occurance of scene,asset or quiz based on contentObjectID.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="seqNo">SeqNo integer value, represent the sequenceno for contentobject</param>
        /// <returns>Integer value</returns>
        public int GetFirstChildSceneAssetOrQuizOfContentObject(int courseID, int seqNo, int sourse, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + sourse.ToString()];
            for(int index=seqNo+1;index<sequence.SequenceItems.Length;index++)
            {
                if (sequence.SequenceItems[index].SequenceItemType != "ContentObject" && sequence.SequenceItems[index].IsNotActive==false)
                {
                    return index;
                }
            }
            return -1;   
        }

        /// <summary>
        /// This method searches Sequence to find active content object.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="seqNo">SeqNo integer value, represent the sequenceno for contentobject</param>
        /// <returns>Integer value</returns>
        public int GetNextActiveContentObject(int courseID, int seqNo, int sourse, int courseConfigurationID, int parentContentObjectID, bool isRootContentObject)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + sourse.ToString()];
            ArrayList parentCOID = new ArrayList();
            if (isRootContentObject == false)
            {
                parentCOID.Add(parentContentObjectID);
            }
            for (int index = seqNo + 1; index < sequence.SequenceItems.Length; index++)
            {
                if (sequence.SequenceItems[index].SequenceItemType == "ContentObject" && sequence.SequenceItems[index].IsNotActive == false)
                {
                    if (!parentCOID.Contains(sequence.SequenceItems[index].ParentID))
                    {
                        return index;
                    }
                    else
                    {
                        if(!parentCOID.Contains(sequence.SequenceItems[index].SequenceItemID))
                        {
                            parentCOID.Add(sequence.SequenceItems[index].SequenceItemID);
                        }
                    }
                }

                if (!parentCOID.Contains(sequence.SequenceItems[index].SequenceItemID) && sequence.SequenceItems[index].SequenceItemType == "ContentObject")
                {
                    parentCOID.Add(sequence.SequenceItems[index].SequenceItemID);
                }
            }
            return GetFirstChildSceneAssetOrQuizOfContentObject(courseID,seqNo,sourse,courseConfigurationID);
        }


        /// <summary>
        /// This method gets a list of contentObjectIDs by comparing tracking data and sequence in cache
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="learnerCourseTrackInfo">LearnerCourseTrackInfo, ICPTrackingService.LearnerCourseTrackInfo[]</param>
        /// <returns>List of integer</returns>
        public List<int> GetTraversedContentObjectSequenceItemIDs(int courseID, ICPTrackingService.LearnerCourseTrackInfo[] learnerCourseTrackInfo, int source, int courseConfigurationID)
        {
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()];
            List<int> traversedContentObjectIDs = new List<int>();

            for (int index = 0; index < sequence.SequenceItems.Length; index++)
            {  

                if (sequence.SequenceItems[index].SequenceItemType == "FlashAsset" || sequence.SequenceItems[index].SequenceItemType == "ContentAsset")             
                {
                    foreach (ICPTrackingService.LearnerCourseTrackInfo singleLearnerCourseTrackInfo in learnerCourseTrackInfo)
                    {
                        if (sequence.SequenceItems[index].Item_GUID == singleLearnerCourseTrackInfo.ItemGUID && sequence.SequenceItems[index].SceneGUID == singleLearnerCourseTrackInfo.SceneGUID)
                        {
                            traversedContentObjectIDs.Add(sequence.SequenceItems[index].ParentID);
                            break;
                        }
                    }
                }
            }

            return traversedContentObjectIDs;

        }


        /// <summary>
        /// This method check wehther the move is valid or not.
        /// For suppose, if user is already at the first slide and press BACK, then this move is not valid 
        /// because we cant take him/her back from this position. Similary if user is present at the end of 
        /// the course and presses NEXT then system will not allow him to go further and fires Course 
        /// Minimum Seat Time policy if applicable.
        /// </summary>
        /// <param name="direction">Direction, 1 means NEXT, -1 means BACK</param>
        /// <param name="currentIndex">CurrentIndex</param>
        /// <returns>True or false</returns>
        private bool CheckIsMoveValid(int courseID, int direction, int currentIndex, int source, int courseConfigurationID)
        {
            bool valid = true;           

            switch (direction)
            {
                case 1:
                    {
                        if (currentIndex >= ((ICPCourseService.Sequence)HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()]).SequenceItems.Length - 1)
                        {
                            valid = false;
                            //PolicyManager.BAL.PolicyManagerLogic policyManager = new PolicyManager.BAL.PolicyManagerLogic();
                            //policyManager.ApplyPolicy("OnCourseMaterialComplete");
                        }

                        break;
                    }
                case -1:
                    {
                        if (currentIndex <= 0)
                        {
                            valid = false;

                        }
                        break;
                    }

            }
            return valid;

        }

        /// <summary>
        /// This method check wehther the move is valid or not.
        /// For suppose, if user is already at the first slide and press BACK, then this move is not valid 
        /// because we cant take him/her back from this position. Similary if user is present at the end of 
        /// the course and presses NEXT then system will not allow him to go further and fires Course 
        /// Minimum Seat Time policy if applicable.
        /// </summary>
        /// <param name="direction">Direction, 1 means NEXT, -1 means BACK</param>
        /// <param name="currentIndex">CurrentIndex</param>
        /// <returns>True or false</returns>
        private bool CheckIsMoveValidInDemo(int courseID, int direction, int currentIndex)
        {
            bool valid = true;            

            switch (direction)
            {
                case 1:
                    {
                        if (currentIndex >= ((ICPCourseService.Sequence)HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseID.ToString()]).SequenceItems.Length - 1)
                        {
                            valid = false;
                            //PolicyManager.BAL.PolicyManagerLogic policyManager = new PolicyManager.BAL.PolicyManagerLogic();
                            //policyManager.ApplyPolicy("OnCourseMaterialComplete");
                        }

                        break;
                    }
                case -1:
                    {
                        if (currentIndex <= 0)
                        {
                            valid = false;

                        }
                        break;
                    }

            }
            return valid;

        }

        public bool notifyInvalidateCacheAllRemainingServers(int publishedCourseId)
        {

            List<PlayerServer> playerServerList = null;

            using (PlayerServerDA serverDA = new PlayerServerDA(ConfigurationManager.AppSettings["PlayerServerSettingsFilePath"].ToString()))
            {
                playerServerList = serverDA.GetPlayerServers();
            }

            if (playerServerList != null && playerServerList.Count > 0)
            {

                foreach (PlayerServer playerServer in playerServerList)
                {

                    if (System.Net.Dns.GetHostName().Equals(playerServer.HostName))
                    {
                        continue;
                    }




                    ICP4PlayerUtility.PlayerUtility playerUtility = new ICP4PlayerUtility.PlayerUtility();
                    //playerUtility.Url = "http://" + playerServer.IPAddress + "/icp4/PlayerUtility.asmx";
                    playerUtility.Url = playerServer.PlayerWebServiceURL;


                    //playerUtility.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                    playerUtility.InvalidateCache(publishedCourseId);
                }
            }
            return true;


        }

        public bool InvalidateCache(int publishedCourseId, int source)
        {
            return InvalidateCache(publishedCourseId, false, source);
        }
        public bool InvalidateCache(int publishedCourseId, bool notifyToAllRemainingServers, int source)
        {

            //Creating PlayerServerDA for logging of cache invalidation
            PlayerServerDA playerServerDA = new PlayerServerDA(ConfigurationManager.AppSettings["PlayerServerSettingsFilePath"].ToString());           
            
            // Fix for LCMS-5801 and LCMS-5798 (start)
            // ------------------------------------------------------------------------
            // Initialization of Course Service Object
            ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
            courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"]; 
            courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);

            // This condition would check if the course is currently in cache (i.e. being run currently by any user).
            // If yes, then cache items would be replaced, otherwise removed


            //LCMS-5559
            // Getting fresh course items from database to add or update in cache 
            Sequence courseDemoSequence = courseService.GetCourseDemoSequence(publishedCourseId);
            TableOfContent courseTOC = courseService.GetTableOfContent(publishedCourseId);
            int status = 0;
            String msg = "";


            // Course Demo Sequence
            if (HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + publishedCourseId.ToString()] != null)
            {
                UpdateDemoCourseSequenceInCache(publishedCourseId, courseDemoSequence);
            }
            else
            {
                CreateDemoCourseSequenceInCache(publishedCourseId, courseDemoSequence);
            }

            // Course TOC
            if (HttpRuntime.Cache["COURSETOC" + "_" + publishedCourseId.ToString() + "_" + source.ToString()] != null)
            {
                UpdateTOCInCache(publishedCourseId, courseTOC, source);
            }
            else
            {
                CreateCourseTOCInCache(publishedCourseId, courseTOC, source);
            }

            // Course Sequence
            // Getting fresh course items from database to add or update in cache 

            foreach (DictionaryEntry cacheDictionaryItem in HttpContext.Current.Cache)
            {
                if (HttpContext.Current.Cache[cacheDictionaryItem.Key.ToString()] != null)
                {
                    if (cacheDictionaryItem.Key.ToString().Contains("COURSESEQUENCE" + "_" + publishedCourseId.ToString() + "_"))
                    {
                        //int courseConfigurationID = courseService.GetCourseConfiguarationID(publishedCourseId, source);
                        int courseConfigurationID = Convert.ToInt32(GetCourseConfigurationID(cacheDictionaryItem.Key.ToString()));
                        InvalidateCourseConfigurationCache(courseConfigurationID);
                        CourseConfiguration courseConfiguration = courseService.GetCourseConfiguaration(courseConfigurationID);
                        Sequence courseSequence = courseService.GetSequence(publishedCourseId, courseConfiguration);

                        if (HttpRuntime.Cache[cacheDictionaryItem.Key.ToString()] != null)
                        {
                            status = 2;
                            // Updating Cache
                            //UpdateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
                            UpdateCourseSequenceInCache(publishedCourseId, courseSequence, source, courseConfigurationID);


                            //msg += "Server/Host : "+ System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2] +"/"+System.Net.Dns.GetHostName();
                            msg += "Server/Host : " + System.Net.Dns.GetHostName();
                            msg += " Cache Updated - Course ID : " + publishedCourseId;


                        }
                        else
                        {
                            status = 1;
                            //Adding Cache
                            //CreateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
                            CreateCourseSequenceInCache(publishedCourseId, courseSequence, source, courseConfigurationID);


                            //msg += "Server/Host : "+ System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[2] +"/"+System.Net.Dns.GetHostName();
                            msg += "Server/Host : " + System.Net.Dns.GetHostName();
                            msg += " Cache Added - Course ID : " + publishedCourseId;
                        }

                    }
                }
            }

            Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");

            //InvalidateCourseConfigurationCache(courseConfigurationID);

            if (notifyToAllRemainingServers)
            {
                notifyInvalidateCacheAllRemainingServers(publishedCourseId);    
            }
                
            
            // ------------------------------------------------------------------------
            // Fix for LCMS-5801 and LCMS-5798 (end)

            
            return true;
        }


        private string GetCourseConfigurationID(string cacheKey)
        {
            string[] aCacheKey = cacheKey.Split('_');
            return aCacheKey[2].ToString();
        }

        public bool InvalidateCourseConfigurationCache(int courseConfigurationID)
        {
            try
            {
                ICPCourseService.CourseService courseService = new ICP4.BusinessLogic.ICPCourseService.CourseService();
                courseService.Url = ConfigurationManager.AppSettings["ICPCourseService"];
                courseService.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ICPCourseServiceTimeout"]);
                int status = 0;
                String msg = "";

                if (courseConfigurationID > 0)
                {
                    CourseConfiguration courseConfiguration = courseService.GetCourseConfiguaration(courseConfigurationID);
                    //courseConfiguration.LastModifiedDateTime = DateTime.Now;
                    if (HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()] != null)
                    {
                        status = 2;
                        UpdateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
                        msg += "Server/Host : " + System.Net.Dns.GetHostName();
                        msg += " Cache Updated - CourseConfiguration ID : " + courseConfigurationID.ToString();

                    }
                    else
                    {
                        status = 1;
                        CreateCourseConfigurationInCache(courseConfigurationID, courseConfiguration);
                        msg += "Server/Host : " + System.Net.Dns.GetHostName();
                        msg += " Cache Added - CourseConfiguration ID : " + courseConfigurationID.ToString();
                    }

                    Logger.Write(msg, "PlayerCourseCache", 2, 2000, System.Diagnostics.TraceEventType.Information, "");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exp)
            {
                return false;
            }
        }

        public bool InvalidateCourseApprovalCache(int courseApprovalID)
        {
            try
            {
                if (courseApprovalID > 0)
                {
                    int status = 0;
                    String msg = "";

                    if (HttpRuntime.Cache["COURSEAPPROVAL"] != null)
                    {
                        Dictionary<int, DateTime> courseApprovals = (Dictionary<int, DateTime>)HttpRuntime.Cache["COURSEAPPROVAL"];
                        if (courseApprovals.ContainsKey(courseApprovalID))
                        {
                            courseApprovals[courseApprovalID] = DateTime.Now;
                            UpdateCourseApprovalInCache(courseApprovals);
                            msg += "Server/Host : " + System.Net.Dns.GetHostName();
                            msg += " Cache Added - CourseApproval ID : " + courseApprovalID.ToString();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exp)
            {
                return false;
            }
        }

        /// <summary>
        /// This method create Course Approval object into Cache.
        /// </summary>
        /// <param name="courseID">CourseID integer value, represent the key for cache</param>
        /// <param name="courseConfiguration">CourseConfiguration, ICPCourseService.CourseConfiguration</param>
        /// <returns>Boolean value to represent whether the object is stored in cache</returns>
        public bool CreateCourseApprovalInCache(int courseApprovalID, DateTime learningSessionStartDateTime)
        {
            try
            {
                if (HttpRuntime.Cache["COURSEAPPROVAL"] == null)
                {
                    Dictionary<int, DateTime> courseApprovals = new Dictionary<int, DateTime>();
                    courseApprovals.Add(courseApprovalID, learningSessionStartDateTime);
                    HttpRuntime.Cache.Add("COURSEAPPROVAL", courseApprovals, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 6, 0, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
                else
                {
                    Dictionary<int, DateTime> courseApprovals = (Dictionary<int, DateTime>)HttpRuntime.Cache["COURSEAPPROVAL"];
                    if (!courseApprovals.ContainsKey(courseApprovalID)) 
                    {
                        courseApprovals.Add(courseApprovalID, learningSessionStartDateTime);
                        UpdateCourseApprovalInCache(courseApprovals);
                    } 
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return false;
            }

        }



        public void UpdateCourseConfigurationInCache(int courseConfigurationID, ICPCourseService.CourseConfiguration courseConfiguration)
        {
            HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()] = courseConfiguration;
        }

        public void UpdateCourseSequenceInCache(int courseId, ICPCourseService.Sequence courseSequence, int source, int courseConfigurationID)
        {
            HttpRuntime.Cache["COURSESEQUENCE" + "_" + courseId.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString()] = courseSequence;
        }

        public void UpdateDemoCourseSequenceInCache(int courseId, ICPCourseService.Sequence courseDemoSequence)
        {
            HttpRuntime.Cache["COURSEDEMOSEQUENCE" + "_" + courseId.ToString()] = courseDemoSequence;
        }

        public void UpdateTOCInCache(int courseId, ICPCourseService.TableOfContent courseTOC, int source)
        {
            HttpRuntime.Cache["COURSETOC" + "_" + courseId.ToString() + "_" + source.ToString()] = courseTOC;
        }

        public void UpdateCourseApprovalInCache(Dictionary<int,DateTime> courseapprovals)
        {
            HttpRuntime.Cache["COURSEAPPROVAL"] = courseapprovals;
        }

   

        #region Application Level Cache Access
        public ICPCourseService.CourseConfiguration GetIFConfigurationExistInCacheOnApplicationLevel(int courseConfigurationID)
        {
            ICP4.BusinessLogic.ICPCourseService.CourseConfiguration courseConfiguration = new ICP4.BusinessLogic.ICPCourseService.CourseConfiguration();
            try
            {
                if (HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()] == null)
                {
                    courseConfiguration = null;
                }
                else
                {
                    courseConfiguration = (ICP4.BusinessLogic.ICPCourseService.CourseConfiguration)HttpRuntime.Cache["COURSECONFIGURATION" + "_" + courseConfigurationID.ToString()];

                }
                return courseConfiguration;
            }
            catch (Exception ex)
            {
                ExceptionPolicyForLCMS.HandleException(ex, "ICPException");
                return null;
            }

        }
        public ICPCourseService.SequenceItem GetRequestedItemFromQueueOnAppliationLevel(int courseID, int specificIndex, int source, int courseConfigurationID)
        {
            int currentIndex = specificIndex;
            string cacheKey = "COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString();

            ICP4.BusinessLogic.ICPCourseService.SequenceItem sequenceItem = ((ICP4.BusinessLogic.ICPCourseService.Sequence)HttpRuntime.Cache[cacheKey]).SequenceItems[currentIndex];

            return sequenceItem;

        }
        public string GetSequenceItemTypeFromApplicationLevel(int courseID, int sequenceItemID, int source, int courseConfigurationID)
        {

            string cacheKey = "COURSESEQUENCE" + "_" + courseID.ToString() + "_" + courseConfigurationID.ToString() + "_" + source.ToString();
            ICPCourseService.Sequence sequence = (ICPCourseService.Sequence)HttpRuntime.Cache[cacheKey];

            return sequence.SequenceItems[sequenceItemID].SequenceItemType;

        }
        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
