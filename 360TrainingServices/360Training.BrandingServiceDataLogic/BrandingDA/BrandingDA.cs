using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using _360Training.BusinessEntities;
using _360Training.BrandingServiceDataLogic.Common;

namespace _360Training.BrandingServiceDataLogic.BrandingDA
{
    public class BrandingDA:IBrandingDA,IDisposable
    {
        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        public BrandingDA()
        {
            db = DatabaseFactory.CreateDatabase("360TrainingServiceDB");
        }
        #endregion 

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IBrandinDAMembers
        
        #region Course
        public CourseInfo GetCourseInfo(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_LANGUAGE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                CourseInfo courseInfo = new CourseInfo();

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseInfo.CourseName = Convert.ToString(dataReader["COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT"]);
                        courseInfo.Language = Convert.ToString(dataReader["COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT"]);
                    }
                }
                return courseInfo;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }


        }
    #endregion

        #region Branding
        /// <summary>
        /// This mehod gets the branding  
        /// </summary>
        /// <param name="brandingID">int brandingid</param>
        /// <returns>BrandLocaleInfo object</returns>
        public BrandLocaleInfo GetBranding(int brandingID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a branding record
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_BRANDING);
                db.AddInParameter(dbCommand, "@BRANDING_ID", DbType.Int32, brandingID);

                BrandLocaleInfo brandLocaleInfo = new BrandLocaleInfo();

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        brandLocaleInfo.BrandID = Convert.ToInt32(dataReader["ID"]);
                        brandLocaleInfo.BrandName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                    }
                }
                return brandLocaleInfo;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }


        }
        /// <summary>
        /// This method gets the resources of the specified branding locale
        /// </summary>
        /// <param name="brandingID">int brandingID</param>
        /// <param name="localeID">int localeID</param>
        /// <returns>List of LocaleResource object</returns>
        public List<LocaleResource> GetBrandingLocaleResources(int brandingID,int localeID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets the locale resources of a branding
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_BRANDING_LOCALE_RESOURCE);
                db.AddInParameter(dbCommand, "@BRANDING_ID", DbType.Int32, brandingID);
                db.AddInParameter(dbCommand, "@LANGUAGE_ID", DbType.Int32, localeID);

                List<LocaleResource> localeResources = new List<LocaleResource>();
                LocaleResource localeResource; 
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        localeResource = new LocaleResource();
                        localeResource.ResourceKey = dataReader["RESOURCEKEY"] == DBNull.Value ? "" : dataReader["RESOURCEKEY"].ToString(); ;
                        localeResource.ResourceValue = dataReader["RESOURCEVALUE"] == DBNull.Value ? "" : dataReader["RESOURCEVALUE"].ToString();
                        localeResources.Add(localeResource);
                    }
                }
                return localeResources;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }


        }
        /// <summary>
        /// This metho gets the branding record of code
        /// </summary>
        /// <param name="code">strin code</param>
        /// <returns>BrandLocaleInfo  object</returns>
        public BrandLocaleInfo GetCodeBranding(string code)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a branding record by code
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CODE_BRANDING);
                db.AddInParameter(dbCommand, "@CODE", DbType.String, code);

                BrandLocaleInfo brandLocaleInfo = new BrandLocaleInfo();

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        brandLocaleInfo.BrandID = Convert.ToInt32(dataReader["ID"]);
                        brandLocaleInfo.BrandName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                    }
                }
                return brandLocaleInfo;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// Thi method returns the languageid of the given variant 
        /// </summary>
        /// <param name="variant">string variant</param>
        /// <returns>languageID if suceessfull, else 0</returns>
        public int GetVariantLanguage(string variant)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a language record by variant
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_VARIANT_LANGUAGE);
                db.AddInParameter(dbCommand, "@VARIANT", DbType.String, variant);
                int languageID = 0;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        languageID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                    }
                }
                return languageID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }

        }
        #endregion
        #endregion

    }
}
