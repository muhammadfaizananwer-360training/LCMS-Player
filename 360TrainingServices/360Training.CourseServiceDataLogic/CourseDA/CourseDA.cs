using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using _360Training.BusinessEntities;
using _360Training.CourseServiceDataLogic.Common;  
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace _360Training.CourseServiceDataLogic.CourseDA
{
    public class CourseDA : ICourseDA, IDisposable
    {
        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        public CourseDA()
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

        #region ICourseDA Members

        #region Course Methods
        /// <summary>
        /// This method is used to get all contentobjects belong to certain course.
        /// </summary>
        /// <param name="courseID">CourseId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing contentobjects, null otherwise.</returns>
        public List<ContentObject> GetCourseContentObjects(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all content objects related to a particular course
                //This procedure will return all the content objects in child hierarchy 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_CONTENTOBJECTS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<ContentObject> contentObjects = new List<ContentObject>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    ContentObject contenObject;
                    while (dataReader.Read())
                    {
                        contenObject = new ContentObject();
                        contenObject.ContentObjectID = Convert.ToInt32(dataReader["ID"]);
                        contenObject.ParentContentObjectID = dataReader["PARENT_CO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["PARENT_CO_ID"]);
                        contenObject.AlowQuizTF = dataReader["ALLOWQUIZTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWQUIZTF"]);
                        contenObject.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        contenObject.ContentObject_GUID = dataReader["CONTENTOBJECT_GUID"] == DBNull.Value ? "" : dataReader["CONTENTOBJECT_GUID"].ToString();
                        contenObject.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        contenObject.MaxQuizQuestionsToAsk = dataReader["MAXQUIZQUESTIONSTOASK"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXQUIZQUESTIONSTOASK"]);
                        contenObject.OverrideMaxQuizQuestionsToAsk = dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]);
                        contentObjects.Add(contenObject);
                    }

                }
                return contentObjects;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method is used to get all contentobjects, Scenes and Exam belong to certain course.
        /// </summary>
        /// <param name="courseID">CourseId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items , null otherwise.</returns>
        public List<SequenceItem> GetCourseSequance(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all content objects related to a particular course
                //This procedure will return all the content objects in child hierarchy 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_SEQUANCE);
                db.AddInParameter(dbCommand, "@COURSEID", DbType.Int32, courseID);

                List<SequenceItem> sequanceItems = new List<SequenceItem>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    SequenceItem sequanceItem;
                    while (dataReader.Read())
                    {
                        sequanceItem = new SequenceItem();
                        if (dataReader["ITEMTYPE"].ToString().Equals(SequenceItemType.ContentObject))
                        {
                            sequanceItem.SequenceItemType = SequenceItemType.ContentObject;  
                            sequanceItem.SequenceItemID = Convert.ToInt32(dataReader["ITEM_ID"]);
                            sequanceItem.ParentID = Convert.ToInt32(dataReader["PARENT_CONTENTOBJECT_ID"]);
                            sequanceItem.SequenceItemType = SequenceItemType.ContentObject;
                            sequanceItem.Item_GUID = dataReader["ITEM_GUID"].ToString();
                            sequanceItem.ContentObjectName = dataReader["CONTENTOBJECTNAME"].ToString();
                            sequanceItem.IsAllowQuizInContentObject = dataReader["QUIZENABLED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["QUIZENABLED"]);
                            sequanceItem.IsNotActive = dataReader["ISNOTACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISNOTACTIVE"]);
                        }

                        if (dataReader["ITEMTYPE"].ToString().Equals(SequenceItemType.Scene))
                        {
                            if (dataReader["SCENETEMPLATETYPE"].ToString().Equals(SceneTemplateType.BF))
                            {
                                sequanceItem.SequenceItemType = SequenceItemType.FlashAsset;
                                sequanceItem.ParentID = Convert.ToInt32(dataReader["PARENT_CONTENTOBJECT_ID"]);
                                sequanceItem.ContentObjectName = dataReader["CONTENTOBJECTNAME"].ToString();
                                sequanceItem.Item_GUID = dataReader["ITEM_GUID"].ToString();
                                sequanceItem.SceneTemplateID = Convert.ToInt32(dataReader["SceneTemplateID"]);
                                sequanceItem.SceneGUID = dataReader["ITEM_GUID"].ToString();
                                sequanceItem.sceneID = Convert.ToInt32(dataReader["SCENE_ID"]);
                                sequanceItem.IsViewStreamingInScene = dataReader["IsViewStreamingInScene"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsViewStreamingInScene"]);
                                sequanceItem.IsPlayPauseFeatureInScene = dataReader["IsPlayPauseFeatureInScene"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsPlayPauseFeatureInScene"]); //Added By Abdus Samad For LCMS-12267
                                sequanceItem.SceneName = dataReader["SCENE_NAME"].ToString();
                                sequanceItem.IsNotActive = dataReader["ISNOTACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISNOTACTIVE"]);
                            }
                            else
                            {
                                if (dataReader["SCENETEMPLATETYPE"].ToString().Equals(SceneTemplateType.KC))
                                {
                                    sequanceItem.SequenceItemType = SequenceItemType.KnowledgeCheck;
                                }
                                else
                                {
                                    sequanceItem.SequenceItemType = SequenceItemType.ContentAsset;
                                }

                                sequanceItem.ParentID = Convert.ToInt32(dataReader["PARENT_CONTENTOBJECT_ID"]);
                                sequanceItem.ContentObjectName = dataReader["CONTENTOBJECTNAME"].ToString();
                                sequanceItem.Item_GUID = dataReader["ITEM_GUID"].ToString();
                                sequanceItem.SequenceItemID = Convert.ToInt32(dataReader["ITEM_ID"]);
                                sequanceItem.SceneTemplateID = Convert.ToInt32(dataReader["SceneTemplateID"]);
                                sequanceItem.SceneGUID = dataReader["ITEM_GUID"].ToString();
                                sequanceItem.sceneID = Convert.ToInt32(dataReader["SCENE_ID"]);
                                sequanceItem.SceneDuration = Convert.ToInt32(dataReader["SCENEDURATIONSECONDS"]);
                                sequanceItem.IsViewStreamingInScene = dataReader["IsViewStreamingInScene"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsViewStreamingInScene"]);
                                sequanceItem.IsPlayPauseFeatureInScene = dataReader["IsPlayPauseFeatureInScene"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsPlayPauseFeatureInScene"]); //Added By Abdus Samad For LCMS-12267
                                sequanceItem.SceneName = dataReader["SCENE_NAME"].ToString();
                                sequanceItem.isTitleVisible = dataReader["SCENENAMEVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SCENENAMEVISIBLE"]);
                                sequanceItem.isTopicTitleVisible = dataReader["TOPICTITLEVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["TOPICTITLEVISIBLE"]);

                                sequanceItem.MCSceneXml = dataReader["MC_SCENE_XML"] == DBNull.Value ? "" : Convert.ToString(dataReader["MC_SCENE_XML"]); // MC
                                sequanceItem.IsNotActive = dataReader["ISNOTACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISNOTACTIVE"]);

                                if (dataReader["SCENETEMPLATETYPE"].ToString().Equals(SceneTemplateType.VSC))
                                {
                                    if (dataReader["VideoFilename"] == null || dataReader["VideoFilename"].ToString() == string.Empty)
                                    {

                                        if (bool.Parse(dataReader["ISEMBEDCODE"].ToString()) == true)
                                        {
                                            sequanceItem.StreamingServerApplication = "freemium";
                                            sequanceItem.VideoHeight = 0;
                                            sequanceItem.VideoWidth = 0;
                                            sequanceItem.StartQueueHours = 0;
                                            sequanceItem.StartQueueMinutes = 0;
                                            sequanceItem.StartQueueSeconds = 0;
                                            sequanceItem.EndQueueHours = 0;
                                            sequanceItem.EndQueueMinutes = 0;
                                            sequanceItem.EndQueueSeconds = 0;
                                            sequanceItem.FullScreen = true;
                                            sequanceItem.DisplayStandardTF = true;
                                            sequanceItem.DisplayWideScreenTF = false;
                                            sequanceItem.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                                            sequanceItem.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609
                                        
                                        }
                                        else
                                        {
                                            dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_VSC_INFO);
                                            db.AddInParameter(dbCommand, "@SCENE_ID", DbType.Int32, sequanceItem.sceneID);

                                            using (IDataReader dataReaderVSC = db.ExecuteReader(dbCommand))
                                            {

                                                while (dataReaderVSC.Read())
                                                {
                                                    sequanceItem.VideoFilename = dataReaderVSC["VideoFilename"].ToString();
                                                    if (System.Configuration.ConfigurationSettings.AppSettings["FreemiumApp"] != null && System.Configuration.ConfigurationSettings.AppSettings["FreemiumApp"] != string.Empty)
                                                        sequanceItem.StreamingServerApplication = System.Configuration.ConfigurationManager.AppSettings["FreemiumApp"];
                                                    else
                                                        sequanceItem.StreamingServerApplication = "freemium";// dataReaderVSC["StreamingServerApplication"].ToString();
                                                    sequanceItem.VideoHeight = 0;// Convert.ToInt32(dataReaderVSC["VideoHeight"]);
                                                    sequanceItem.VideoWidth = 0;// Convert.ToInt32(dataReaderVSC["VideoWidth"]);

                                                    sequanceItem.StartQueueHours = 0;// Convert.ToInt32(dataReaderVSC["StartQueueHours"]);
                                                    sequanceItem.StartQueueMinutes = 0;// Convert.ToInt32(dataReaderVSC["StartQueueMinutes"]);
                                                    sequanceItem.StartQueueSeconds = 0;// Convert.ToInt32(dataReaderVSC["StartQueueSeconds"]);

                                                    sequanceItem.EndQueueHours = 0;// Convert.ToInt32(dataReaderVSC["EndQueueHours"]);
                                                    sequanceItem.EndQueueMinutes = 0;// Convert.ToInt32(dataReaderVSC["EndQueueMinutes"]);
                                                    sequanceItem.EndQueueSeconds = 0;// Convert.ToInt32(dataReaderVSC["EndQueueSeconds"]);
                                                    sequanceItem.FullScreen = true;// dataReaderVSC["FullScreen"] == DBNull.Value ? false : Convert.ToBoolean(dataReaderVSC["FullScreen"]);
                                                    sequanceItem.DisplayStandardTF = dataReader["DisplayStandardTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DisplayStandardTF"]); //Added By Abdus Samad For LCMS-12267
                                                    sequanceItem.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]); //Added By Abdus Samad For LCMS-12267
                                                    sequanceItem.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                                                    sequanceItem.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sequanceItem.VideoFilename = dataReader["VideoFilename"].ToString();
                                        sequanceItem.StreamingServerApplication = dataReader["StreamingServerApplication"].ToString();
                                        sequanceItem.VideoHeight = Convert.ToInt32(dataReader["VideoHeight"]);
                                        sequanceItem.VideoWidth = Convert.ToInt32(dataReader["VideoWidth"]);

                                        sequanceItem.StartQueueHours = Convert.ToInt32(dataReader["StartQueueHours"]);
                                        sequanceItem.StartQueueMinutes = Convert.ToInt32(dataReader["StartQueueMinutes"]);
                                        sequanceItem.StartQueueSeconds = Convert.ToInt32(dataReader["StartQueueSeconds"]);

                                        sequanceItem.EndQueueHours = Convert.ToInt32(dataReader["EndQueueHours"]);
                                        sequanceItem.EndQueueMinutes = Convert.ToInt32(dataReader["EndQueueMinutes"]);
                                        sequanceItem.EndQueueSeconds = Convert.ToInt32(dataReader["EndQueueSeconds"]);
                                        sequanceItem.FullScreen = dataReader["FullScreen"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["FullScreen"]);
                                        sequanceItem.DisplayStandardTF = dataReader["DisplayStandardTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DisplayStandardTF"]); //Added By Abdus Samad For LCMS-12267
                                        sequanceItem.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]); //Added By Abdus Samad For LCMS-12267

                                        sequanceItem.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                                        sequanceItem.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609
                                    }
                                }
                            }                        
                        }

                        if (dataReader["ITEMTYPE"].ToString().Equals(SequenceItemType.Exam))
                        {
                            if (bool.Parse(dataReader["ExamPolicy"].ToString())==true)
                            {
                                sequanceItem.SequenceItemType = SequenceItemType.Exam;
                                sequanceItem.SequenceItemID = Convert.ToInt32(dataReader["ITEM_ID"]);
                                sequanceItem.ParentID = Convert.ToInt32(dataReader["PARENT_CONTENTOBJECT_ID"]);
                                sequanceItem.Item_GUID = dataReader["ITEM_GUID"].ToString();
                                sequanceItem.ContentObjectName = dataReader["CONTENTOBJECTNAME"].ToString();
                                sequanceItem.ExamID = Convert.ToInt32(dataReader["ITEM_ID"]);
                                sequanceItem.ExamName = dataReader["EXAMNAME"].ToString();
                                sequanceItem.ExamType = dataReader["EXAMTYPE"].ToString();
                                if (sequanceItem.ExamType == SequenceItemType.Quiz)
                                    sequanceItem.IsValidQuiz = true;
                                sequanceItem.SceneTemplateID = Convert.ToInt32(dataReader["EXAMTemplateID"]);
                                sequanceItem.IsNotActive = dataReader["ISNOTACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISNOTACTIVE"]);
                            }
                        }
                        
                        //contenObject.ContentObjectID = Convert.ToInt32(dataReader["ID"]);
                        //contenObject.ParentContentObjectID = dataReader["PARENT_CO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["PARENT_CO_ID"]);
                        //contenObject.AlowQuizTF = dataReader["ALLOWQUIZTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWQUIZTF"]);
                        //contenObject.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        //contenObject.ContentObject_GUID = dataReader["CONTENTOBJECT_GUID"] == DBNull.Value ? "" : dataReader["CONTENTOBJECT_GUID"].ToString();
                        //contenObject.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        //contenObject.MaxQuizQuestionsToAsk = dataReader["MAXQUIZQUESTIONSTOASK"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXQUIZQUESTIONSTOASK"]);
                        //contenObject.OverrideMaxQuizQuestionsToAsk = dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]);
                        if(!sequanceItem.SequenceItemType.Equals(""))                         
                            sequanceItems.Add(sequanceItem);
                    }

                }
                return sequanceItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets the root contentobjects associated with the course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>returns list of contentobjects</returns>
        public List<ContentObject> GetCourseRootContentObject(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns root contentobjects associated with the course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_ROOT_CONTENTOBJECT);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<ContentObject> contentObjects = new List<ContentObject>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    ContentObject contenObject;
                    while (dataReader.Read())
                    {
                        contenObject = new ContentObject();
                        contenObject.ContentObjectID = Convert.ToInt32(dataReader["ID"]);
                        contenObject.ParentContentObjectID = dataReader["PARENT_CO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["PARENT_CO_ID"]);
                        contenObject.AlowQuizTF = dataReader["ALLOWQUIZTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWQUIZTF"]);
                        contenObject.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        contenObject.ContentObject_GUID = dataReader["CONTENTOBJECT_GUID"] == DBNull.Value ? "" : dataReader["CONTENTOBJECT_GUID"].ToString();
                        contenObject.MaxQuizQuestionsToAsk = dataReader["MAXQUIZQUESTIONSTOASK"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXQUIZQUESTIONSTOASK"]);
                        contenObject.OverrideMaxQuizQuestionsToAsk = dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]);
                        contentObjects.Add(contenObject);
                    }

                }
                return contentObjects;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public List<TOCItem> GetCourseContentObjectsInCourseOutline(int courseID,int contentobjectID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all content objects after the rootcontentobject related to a particular course
                //This procedure will return all the content objects in child hierarchy (of root contentobject)that are in course outline
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_CONTENTOBJECTINOUTLINE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentobjectID);

                List<TOCItem> tocItems = new List<TOCItem>();                
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    TOCItem tocitem;
                    while (dataReader.Read())
                    {
                        if (int.Parse(dataReader["ExamEnable"].ToString()) == 1)
                        {
                            tocitem = new TOCItem();
                            if (dataReader["TOCType"].ToString().Equals(TOCItemType.ContenObject))
                            {
                                tocitem.Type = TOCItemType.ContenObject;
                            }
                            else if (dataReader["TOCType"].ToString().Equals(TOCItemType.Exam))
                            {
                                tocitem.Type = TOCItemType.Exam;
                            }
                            else if (dataReader["TOCType"].ToString().Equals(TOCItemType.Scene))
                            {
                                tocitem.Type = TOCItemType.Scene;
                            }

                            tocitem.ID = Convert.ToInt32(dataReader["ID"]);
                            tocitem.ParentID = dataReader["PARENT_CO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["PARENT_CO_ID"]);
                            tocitem.AlowQuizTF = dataReader["ALLOWQUIZTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWQUIZTF"]);
                            tocitem.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                            tocitem.GUID = dataReader["CONTENTOBJECT_GUID"] == DBNull.Value ? "" : dataReader["CONTENTOBJECT_GUID"].ToString();
                            tocitem.MaxQuizQuestionsToAsk = dataReader["MAXQUIZQUESTIONSTOASK"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXQUIZQUESTIONSTOASK"]);
                            tocitem.OverrideMaxQuizQuestionsToAsk = dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]);
                            tocItems.Add(tocitem);
                        }
                    }

                }
                return tocItems;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        public CourseConfiguration GetCourseConfiguration(int courseConfigurationID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                DateTime lastModifiedDate = DateTime.Now;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSECONFIGURATION);
                db.AddInParameter(dbCommand, "@COURSECONFIGURATION_ID", DbType.Int32, courseConfigurationID);
                CourseConfiguration courseConfiguration = null;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        lastModifiedDate = dataReader["LASTMODIFIEDDATE"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dataReader["LASTMODIFIEDDATE"]);
                    }

                   

                    if (dataReader.NextResult())
                    {
                        courseConfiguration = GetCourseConfiguration(dataReader);
                        courseConfiguration.LastModifiedDateTime = lastModifiedDate;
                    }
                }
                return courseConfiguration;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns course configuration ID
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseID">int source</param>        
        /// <returns>courseconfigurationID int</returns>
        public int GetCourseConfigurationID(int courseID, int source, int courseApprovalID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                int courseConfigurationID=0;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSECONFIGURATION_ID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@SORUCE", DbType.Int32, source);
                db.AddInParameter(dbCommand, "@COURSEAPPROVALID", DbType.Int32, courseApprovalID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseConfigurationID = dataReader["COURSECONFIGURATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COURSECONFIGURATION_ID"]);
                    }
                }
                return courseConfigurationID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }


        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>courseconfiguration object</returns>
        public DateTime GetCourseLastPublishedDate(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                DateTime lastPublishedDate = new DateTime(1900, 1, 1);
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSELASTPUBLISHEDDATE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        lastPublishedDate = dataReader["LASTPUBLISHEDDATE"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dataReader["LASTPUBLISHEDDATE"]);
                    }
                }

                return lastPublishedDate;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return new DateTime(1900,1,1);
            }
        }

        /// <summary>
        /// Gets all course groups in the parent child hierarchy of A course group   
        /// </summary>
        /// <param name="courseGroupID"></param>
        /// <returns>Course Group object List</returns>
        public string GetCourseGroupsByCourse(int CourseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all coursegroups related to a particular coursegroup
                //This procedure will return all the related coursegroups in the current hierarchy 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_COURSEGROUPS_BY_COURSE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseID);
                string courseGroupGuid = string.Empty;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {                   

                    while (dataReader.Read())
                    {
                        if (Convert.ToBoolean(dataReader["CONTAINSCOURSE"]))
                        {
                            courseGroupGuid = dataReader["COURSEGROUP_GUID"] == DBNull.Value ? "" : dataReader["COURSEGROUP_GUID"].ToString();
                        }
                    }
                }
                return courseGroupGuid;

            }
            catch (Exception exp)
            {
                ExceptionPolicy.HandleException(exp, "Exception Policy");
                return null;
            }

        }

        /// <summary>
        /// This method returns Course Approval Affidavit
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseApprovalID">int courseApprovalID</param>        
        /// <returns>int</returns>
        public int GetCourseApprovalAffidavit(int courseID, int courseApprovalID)
        {
            DbCommand dbCommand = null;
            try
            {
                int affidavitID = 0;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSEAPPROVALAFFIDAVIT);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@COURSEAPPROVAL_ID", DbType.Int32, courseApprovalID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        affidavitID = dataReader["AFFIDAVIT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["AFFIDAVIT_ID"]);
                    }
                }

                return affidavitID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// This method returns CourseApprovalCertificate
        /// </summary>        
        /// <param name="courseApprovalID">int courseApprovalID</param>        
        /// <returns>CourseApprovalCertificate</returns>
        public CourseApprovalCertificate GetCourseApprovalCertificate(int courseApprovalID, int enrollmentID)
        {
            DbCommand dbCommand = null;
            try
            {                
                CourseApprovalCertificate courseApprovalCertificate = new CourseApprovalCertificate(); 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSEAPPROVAL_CERTIFICATE);
                db.AddInParameter(dbCommand, "@COURSEAPPROVALID", DbType.Int32, courseApprovalID);
                db.AddInParameter(dbCommand, "@ENROLLMENTID", DbType.Int32, enrollmentID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseApprovalCertificate.CertificateID = dataReader["ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSET_ID"]);
                        courseApprovalCertificate.CertificateEnabled = dataReader["CERTIFICATETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CERTIFICATETF"]);
                    }
                }

                return courseApprovalCertificate;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns course configuration
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <param name="courseID">int CourseApprovalID</param>
        /// <returns>courseconfiguration object</returns>
        public CourseConfiguration GetCourseApprovalCourseConfiguration(int courseID, int CourseApprovalID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                DateTime lastPublishedDate = DateTime.Now;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSEAPPROVAL_COURSECONFIGURATION);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@COURSEAPPROVAL_ID", DbType.Int32, CourseApprovalID);

                CourseConfiguration courseConfiguration = null;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        lastPublishedDate = dataReader["LASTPUBLISHEDDATE"] == DBNull.Value ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dataReader["LASTPUBLISHEDDATE"]);
                    }

                    

                    if (dataReader.NextResult())
                    {
                        courseConfiguration = GetCourseConfiguration(dataReader);
                        courseConfiguration.LastModifiedDateTime = lastPublishedDate;
                    }
                }
                return courseConfiguration;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public List<CourseConfiguration> GetCourseApprovalCourseConfigurationTemplates(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSECONFIGURATIONTEMPLATE_ID_BY_COURSEAPPROVAL);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                List<CourseConfiguration> courseApprovalCourseConfigurationTemplates = new List<CourseConfiguration>();

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {

                    

                    while (dataReader.Read())
                    {
                        if (dataReader["COURSECONFIGURATIONTEMPLATE_ID"] != DBNull.Value)
                        {
                            courseApprovalCourseConfigurationTemplates.Add(GetCourseConfigurationTemplate(Convert.ToInt32(dataReader["COURSECONFIGURATIONTEMPLATE_ID"])));
                        }
                    }
                }

                return courseApprovalCourseConfigurationTemplates;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        public CourseConfiguration GetCourseConfigurationTemplate(int courseConfigurationTemplateId)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                DateTime lastPublishedDate = DateTime.Now;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_COURSECONFIGURATIONTEMPLATE);

                db.AddInParameter(dbCommand, "@COURSECONFIGURATIONTEMPLATE_ID", DbType.Int32, courseConfigurationTemplateId);
                return GetCourseConfiguration(db.ExecuteReader(dbCommand));
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
            finally
            {
                if (dbCommand.Connection != null && dbCommand.Connection.State == ConnectionState.Open)
                    dbCommand.Connection.Close();
            }
        }

        private CourseConfiguration GetCourseConfiguration(IDataReader dataReader)
        {
            
            CourseConfiguration courseConfiguration = new CourseConfiguration();

            while (dataReader.Read())
            {

                courseConfiguration.CompletionCompleteAfterNOUniqueCourseVisit = Convert.ToInt32(dataReader["COMPLETION_COMPLETEAFTERNOUNIQUECOURSEVISIT"]);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeDay = Convert.ToInt32(dataReader["COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEDAY"]);
                courseConfiguration.CompletionMustCompleteWithinSpecifiedAmountOfTimeMinute = Convert.ToInt32(dataReader["COMPLETION_MUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIMEMINUTE"]);
                courseConfiguration.CompletionUnitOfMustCompleteWithInSpecifiedAmountOfTime = dataReader["COMPLETION_UNITOFMUSTCOMPLETEWITHINSPECIFIEDAMOUNTOFTIME"].ToString();
                courseConfiguration.CompletionPostAssessmentAttempted = Convert.ToBoolean(dataReader["COMPLETION_POSTASSESSMENTATTEMPTED"]);
                courseConfiguration.CompletionPostAssessmentMastery = Convert.ToBoolean(dataReader["COMPLETION_POSTASSESSMENTMASTERY"]);
                courseConfiguration.CompletionPreAssessmentMastery = Convert.ToBoolean(dataReader["COMPLETION_PREASSESSMENTMASTERY"]);
                courseConfiguration.CompletionQuizMastery = Convert.ToBoolean(dataReader["COMPLETION_QUIZMASTERY"]);
                courseConfiguration.CompletionSurvey = Convert.ToBoolean(dataReader["COMPLETION_SURVEY"]);
                courseConfiguration.CompletionViewEverySceneInCourse = Convert.ToBoolean(dataReader["COMPLETION_VIEWEVERYSCENEINCOURSE"]);
                //DBNull.Value ? false: DBNull.Value ? string.Empty :
                courseConfiguration.CompletionRespondToCourseEvaluation = dataReader["COMPLETION_RESPONDTOCOURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETION_RESPONDTOCOURSEEVALUATION"]);
                courseConfiguration.CertificateEnabled = dataReader["COMPLETIONCERTIFICATEENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["COMPLETIONCERTIFICATEENABLEDTF"]);
                courseConfiguration.CertificateAssetID = dataReader["COMPLETIONCERTIFICATEASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["COMPLETIONCERTIFICATEASSET_ID"]);

                courseConfiguration.CourseConfigurationID = Convert.ToInt32(dataReader["ID"]);
                courseConfiguration.PlayerAllowUserToReviewCourseAfterCompletion = Convert.ToBoolean(dataReader["PLAYER_ALLOWUSERTOREVIEWCOURSEAFTERCOMPLETION"]);
                courseConfiguration.PlayerCourseFlow = dataReader["PLAYER_COURSEFLOW"] == DBNull.Value ? "" : dataReader["PLAYER_COURSEFLOW"].ToString();
                courseConfiguration.PlayerEnableContent = Convert.ToBoolean(dataReader["PLAYER_ENABLECONTENT"]);
                courseConfiguration.PlayerEnableEndOfCourseScene = Convert.ToBoolean(dataReader["PLAYER_ENABLEENDOFCOURSESCENE"]);
                courseConfiguration.PlayerEnableIntroPage = Convert.ToBoolean(dataReader["PLAYER_ENABLEINTROPAGE"]);
                courseConfiguration.PlayerEnforceTimedOutline = Convert.ToBoolean(dataReader["PLAYER_ENFORCETIMEDOUTLINE"]);
                courseConfiguration.PlayerIdleUserTimeout = Convert.ToInt32(dataReader["PLAYER_IDLEUSERTIMEOUT"]);
                
                courseConfiguration.PlayerCourseEvaluation = dataReader["PLAYER_COURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_COURSEEVALUATION"]);
                courseConfiguration.PlayerMustCompleteCourseEvaluatio = dataReader["PLAYER_MUSTCOMPLETECOURSEEVALUATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_MUSTCOMPLETECOURSEEVALUATION"]);
                courseConfiguration.PlayerDisplayCourseEvaluation = dataReader["PLAYER_DISPLAYCOURSEEVALUATION"] == DBNull.Value ? DisplayCourseEvaluation.BeforeAndAfterPostAssessment : Convert.ToString(dataReader["PLAYER_DISPLAYCOURSEEVALUATION"]);
                courseConfiguration.PlayerCourseEvaluationInstructions = dataReader["PLAYER_COURSEEVALUATIONINSTRUCTIONS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["PLAYER_COURSEEVALUATIONINSTRUCTIONS"]);

                //LCMS-10392
                courseConfiguration.PlayerShowAmazonAffiliatePanel = dataReader["PLAYER_SHOW_AMAZON_AFFILIATE_PANEL"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_AMAZON_AFFILIATE_PANEL"]);

                //Abdus Samad
                //LCMS-11878
                //Start     

                courseConfiguration.PlayerShowCoursesRecommendationPanel = dataReader["PLAYER_SHOW_COURSE_SUGGESTED_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_COURSE_SUGGESTED_TF"]);

                //Stop

                //Waqas Zakai
                //LCMS-
                //Start     

                courseConfiguration.PlayerRestrictIncompleteJSTemplate = dataReader["RESTRICT_INCOMPLETE_JS_TEMPLATE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICT_INCOMPLETE_JS_TEMPLATE"]);

                //Stop

                courseConfiguration.PlayerAllowTOCDisplaySlides = dataReader["PLAYER_SHOW_DISPLAY_SLIDES_TOC"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SHOW_DISPLAY_SLIDES_TOC"]);

                courseConfiguration.SpecialQuestionnaire = dataReader["PLAYER_SPECIALQUESTIONNAIRE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_SPECIALQUESTIONNAIRE"]);
                courseConfiguration.DisplaySpecialQuestionnaire = dataReader["PLAYER_DISPLAYSPECIALQUESTIONNAIRE"] == DBNull.Value ? DisplayCourseEvaluation.BeforeAndAfterPostAssessment : Convert.ToString(dataReader["PLAYER_DISPLAYSPECIALQUESTIONNAIRE"]);
                courseConfiguration.MustCompleteSpecialQuestionnaire = dataReader["PLAYER_MUSTCOMPLETESPECIALQUESTIONNAIRE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_MUSTCOMPLETESPECIALQUESTIONNAIRE"]);
                courseConfiguration.SpecialQuestionnaireInstructions = dataReader["PLAYER_SPECIALQUESTIONNAIREINSTRUCTIONS"] == DBNull.Value ? String.Empty : Convert.ToString(dataReader["PLAYER_SPECIALQUESTIONNAIREINSTRUCTIONS"]);
                

                courseConfiguration.ValidationNOMissedQuestionsAllowed = Convert.ToInt32(dataReader["VALIDATION_NOMISSEDQUESTIONSALLOWED"]);
                courseConfiguration.ValidationNOValidationQuestion = Convert.ToInt32(dataReader["VALIDATION_NOVALIDATIONQUESTION"]);
                courseConfiguration.ValidationRequireIdentityValidation = Convert.ToBoolean(dataReader["VALIDATION_REQUIREIDENTITYVALIDATION"]);
                courseConfiguration.ValidationTimeBetweenQuestion = Convert.ToInt32(dataReader["VALIDATION_TIMEBETWEENQUESTION"]);
                courseConfiguration.ValidationTimeToAnswerQuestion = Convert.ToInt32(dataReader["VALIDATION_TIMETOANSWERQUESTION"]);
                courseConfiguration.PlayerEnableOrientaionScenes = dataReader["PLAYER_ORIENTATIONKEY"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYER_ORIENTATIONKEY"]);
                //Embedded ACK
                //Embedded ACK
                courseConfiguration.EmbeddedAcknowledgmentEnabled = dataReader["EMBEDDED_ACKNOWLEDGMENT_ENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["EMBEDDED_ACKNOWLEDGMENT_ENABLEDTF"]);
                courseConfiguration.EmbeddedAcknowledgmentText = dataReader["EMBEDDED_ACKNOWLEDGMENT_TEXT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["EMBEDDED_ACKNOWLEDGMENT_TEXT"]);
                // Added by Waqas Zakai 
                // LCMS-5337
                courseConfiguration.SeatTimeEnabled = dataReader["SEATTIMEENABLED"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SEATTIMEENABLED"]);
                courseConfiguration.SeatTimeInHour = Convert.ToInt32(dataReader["MAXSEATTIMEPERDAYHOUR"]);
                courseConfiguration.SeatTimeInMin = Convert.ToInt32(dataReader["MAXSEATTIMEPERDAYMINUTE"]);
                courseConfiguration.MessageSeatTimeExceeds = dataReader["MESSAGESEATTIMEEXCEEDS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["MESSAGESEATTIMEEXCEEDS"].ToString());
                courseConfiguration.MessageSeatTimeCourseLaunch = dataReader["MESSAGESEATTIMECOURSELAUNCH"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["MESSAGESEATTIMECOURSELAUNCH"].ToString());

                // Added by Waqas Zakai
                // LCMS-6092
                courseConfiguration.ActionToTakeUponIdleTimeOut = dataReader["ACTIONTOTAKEUPONIDLETIMEOUT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ACTIONTOTAKEUPONIDLETIMEOUT"].ToString());

                // Added by Waqas Zakai
                // LCMS-6461

                courseConfiguration.PlayerEndOfCourseInstructions = dataReader["ENDOFCOURSEINSTRUCTIONS"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["ENDOFCOURSEINSTRUCTIONS"].ToString());
                
                //LCMS-8422
                courseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDateTF = dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE_TF"]);
                courseConfiguration.MustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"] == DBNull.Value ? 12 : Convert.ToInt32(dataReader["MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"]);
                courseConfiguration.UnitMustStartCourseWithinSpecifiedAmountOfTimeAfterRegistrationDate = dataReader["UNIT_MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"] == DBNull.Value ? TimeUnit.Months : dataReader["UNIT_MUST_START_COURSE_WITHIN_SPECIFIED_AMOUNT_OF_TIME_AFTER_REGISTRATION_DATE"].ToString();               

                //LCMS-10186
                courseConfiguration.IsRequireProctorValidation = dataReader["REQUIRE_PROCTOR_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["REQUIRE_PROCTOR_VALIDATION_TF"]); // LCMS-9455
                courseConfiguration.IsRequireLearnerValidation = dataReader["REQUIRE_LEARNER_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["REQUIRE_LEARNER_VALIDATION_TF"]);
                courseConfiguration.IsANSIValidation = dataReader["ANSI_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ANSI_VALIDATION_TF"]);
                courseConfiguration.IsNYInsuranceValidation = dataReader["NY_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["NY_VALIDATION_TF"]);
                courseConfiguration.IsCARealStateValidation = dataReader["CA_VALIDATION_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CA_VALIDATION_TF"]);

                //LCMS-10536
                courseConfiguration.InstructorInfoEnabled = dataReader["INSTRUCTORINFO_ENABLEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["INSTRUCTORINFO_ENABLEDTF"]);
                courseConfiguration.InstructorInfoText = dataReader["INSTRUCTORINFO_TEXT"] == DBNull.Value ? string.Empty : Convert.ToString(dataReader["INSTRUCTORINFO_TEXT"]);

                //LCMS-11877
                 courseConfiguration.AllowCourseRating = dataReader["ALLOW_RATING_TF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOW_RATING_TF"]);

            }

            if (dataReader.NextResult())
            {
                while (dataReader.Read())
                {
                    String assessmentType = dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();
                    AssessmentConfiguration assessmentconfiguration = null;
                    if (assessmentType == "")
                    {
                        continue;
                    }
                    else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_PREASSESSMENT)
                    {
                        assessmentconfiguration = courseConfiguration.PreAssessmentConfiguration;
                    }
                    else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_POSTASSESSMET)
                    {
                        assessmentconfiguration = courseConfiguration.PostAssessmentConfiguration;
                    }
                    else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_QUIZ)
                    {
                        assessmentconfiguration = courseConfiguration.QuizConfiguration;
                    }
                    else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_PRACTICEEXAM)
                    {
                        assessmentconfiguration = courseConfiguration.PracticeAssessmentConfiguration;
                    }

                    assessmentconfiguration.ID = Convert.ToInt32(dataReader["ID"]);
                    assessmentconfiguration.Enabled = Convert.ToBoolean(dataReader["ENABLED"]);
                    assessmentconfiguration.MaximumNOAttempt = dataReader["MAXIMUMNOATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXIMUMNOATTEMPT"]);
                    assessmentconfiguration.NOQuestion = Convert.ToInt32(dataReader["NOQUESTION"]);
                    assessmentconfiguration.MasteryScore = Convert.ToInt32(dataReader["MASTERYSCORE"]);
                    assessmentconfiguration.ActionToTakeAfterFailingMaxAttempt = dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"] == DBNull.Value ? "" : dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"].ToString();
                    assessmentconfiguration.AllowPauseResumeAssessment = Convert.ToBoolean(dataReader["ALLOWPAUSERESUMEASSESSMENT"]);
                    assessmentconfiguration.AllowSkippingQuestion = Convert.ToBoolean(dataReader["ALLOWSKIPPINGQUESTION"]);
                    assessmentconfiguration.EnforceMaximumTimeLimit = Convert.ToInt32(dataReader["ENFORCEMAXIMUMTIMELIMIT"]);
                    assessmentconfiguration.EnforceUniqueQuestionsOnRetake = Convert.ToBoolean(dataReader["ENFORCEUNIQUEQUESTIONSONRETAKE"]);
                    assessmentconfiguration.MinimumTimeBeforeStart = dataReader["MINIMUMTIMEBEFORESTART"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MINIMUMTIMEBEFORESTART"]);
                    assessmentconfiguration.MinimumTimeBeforeStartUnit = dataReader["MINIMUMTIMEBEFORESTART_UNIT"] == DBNull.Value ? "" : dataReader["MINIMUMTIMEBEFORESTART_UNIT"].ToString();
                    assessmentconfiguration.ContentRemediation = dataReader["CONTENTREMEDIATION"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["CONTENTREMEDIATION"]);
                    assessmentconfiguration.ProctoredAssessment = Convert.ToBoolean(dataReader["PROCTOREDASSESSMENT"]);
                    assessmentconfiguration.QuestionLevelResult = Convert.ToBoolean(dataReader["QUESTIONLEVELRESULT"]);
                    assessmentconfiguration.RandomizeAnswers = Convert.ToBoolean(dataReader["RANDOMIZEANSWERS"]);
                    assessmentconfiguration.RandomizeQuestion = Convert.ToBoolean(dataReader["RANDOMIZEQUESTION"]);
                    //assessmentconfiguration.ScoreAsYouGo = Convert.ToBoolean(dataReader["SCOREASYOUGO"]);
                    assessmentconfiguration.ScoreType = dataReader["SCORETYPE"] == DBNull.Value ? "" : dataReader["SCORETYPE"].ToString();
                    assessmentconfiguration.ShowQuestionAnswerSummary = Convert.ToBoolean(dataReader["SHOWQUESTIONANSWERSUMMARY"]);
                    assessmentconfiguration.StrictlyEnforcePolicyToBeUsed = Convert.ToBoolean(dataReader["STRICLYENFORCEPOLICYTOBEUSED"]);
                    assessmentconfiguration.RestrictiveMode = dataReader["RESTRICTIVEASSESSMENTMODETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICTIVEASSESSMENTMODETF"].ToString());
                    assessmentconfiguration.AdvanceQuestionSelectionType = dataReader["ADVANCEQUESTIONSELECTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["ADVANCEQUESTIONSELECTIONTYPE"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded
                    assessmentconfiguration.UseWeightedScore = dataReader["SCOREWEIGHTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SCOREWEIGHTTF"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded; // temporarily hard-coded
                    assessmentconfiguration.GradeQuestions = dataReader["GRADEQUESTIONS"] == DBNull.Value ? "" : Convert.ToString(dataReader["GRADEQUESTIONS"]);
                    assessmentconfiguration.DisplaySeatTimeSatisfiedMessageTF = dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"]);
                    assessmentconfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF = dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"]);
                    assessmentconfiguration.NoResultText = dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"] == DBNull.Value ? "" : dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"].ToString();
                    assessmentconfiguration.MaxAttemptHandlerEnabled = dataReader["ENABLEMAXATTEMPTHANDLER"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEMAXATTEMPTHANDLER"]);
                    assessmentconfiguration.LockoutFuntionalityClickAwayToActiveWindowEnable = dataReader["ENABLELOCKOUTCLICKAWAYFROMACTIVEWINDOWTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLELOCKOUTCLICKAWAYFROMACTIVEWINDOWTF"]);
                    assessmentconfiguration.AssessmentResultEnabled = dataReader["ENABLEVIEWASSESSMENTRESULT"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEVIEWASSESSMENTRESULT"]);

                    if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED)
                    {
                        assessmentconfiguration.ScoreAsYouGo = false;
                    }
                    else if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_EACH_QUESTION_IS_ANSWERED)
                    {
                        assessmentconfiguration.ScoreAsYouGo = true;
                    }
                }
            }

            return courseConfiguration;
        }
        /// <summary>
        /// This method returns Practice Exam course configuration
        /// </summary>
        /// <param name="courseID">int ExamID</param>
        /// <returns>AssessmentConfiguration object</returns>
        public AssessmentConfiguration GetPraceticeExamAssessmentConfiguration(int examID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_PRACTICEEXAM_ASSESSMENTCONFIGURATION);
                db.AddInParameter(dbCommand, "@EXAM_ID", DbType.Int32, examID);

                AssessmentConfiguration assessmentconfiguration = new AssessmentConfiguration();

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        String assessmentType = dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();                        
                        if (assessmentType == "")
                        {
                            continue;
                        }                        
                        else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_PRACTICEEXAM)
                        {
                            assessmentconfiguration.AssessmentType = assessmentType;
                        }

                        assessmentconfiguration.ID = Convert.ToInt32(dataReader["ID"]);
                        assessmentconfiguration.Enabled = Convert.ToBoolean(dataReader["ENABLED"]);
                        assessmentconfiguration.MaximumNOAttempt = dataReader["MAXIMUMNOATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXIMUMNOATTEMPT"]);
                        assessmentconfiguration.NOQuestion = Convert.ToInt32(dataReader["NOQUESTION"]);
                        assessmentconfiguration.MasteryScore = Convert.ToInt32(dataReader["MASTERYSCORE"]);
                        assessmentconfiguration.ActionToTakeAfterFailingMaxAttempt = dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"] == DBNull.Value ? "" : dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"].ToString();
                        assessmentconfiguration.AllowPauseResumeAssessment = Convert.ToBoolean(dataReader["ALLOWPAUSERESUMEASSESSMENT"]);
                        assessmentconfiguration.AllowSkippingQuestion = Convert.ToBoolean(dataReader["ALLOWSKIPPINGQUESTION"]);
                        assessmentconfiguration.EnforceMaximumTimeLimit = Convert.ToInt32(dataReader["ENFORCEMAXIMUMTIMELIMIT"]);
                        assessmentconfiguration.EnforceUniqueQuestionsOnRetake = Convert.ToBoolean(dataReader["ENFORCEUNIQUEQUESTIONSONRETAKE"]);
                        assessmentconfiguration.MinimumTimeBeforeStart = dataReader["MINIMUMTIMEBEFORESTART"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MINIMUMTIMEBEFORESTART"]);
                        assessmentconfiguration.MinimumTimeBeforeStartUnit = dataReader["MINIMUMTIMEBEFORESTART_UNIT"] == DBNull.Value ? "" : dataReader["MINIMUMTIMEBEFORESTART_UNIT"].ToString();
                        assessmentconfiguration.ContentRemediation = Convert.ToBoolean(dataReader["CONTENTREMEDIATION"]);
                        assessmentconfiguration.ProctoredAssessment = Convert.ToBoolean(dataReader["PROCTOREDASSESSMENT"]);
                        assessmentconfiguration.QuestionLevelResult = Convert.ToBoolean(dataReader["QUESTIONLEVELRESULT"]);
                        assessmentconfiguration.RandomizeAnswers = Convert.ToBoolean(dataReader["RANDOMIZEANSWERS"]);
                        assessmentconfiguration.RandomizeQuestion = Convert.ToBoolean(dataReader["RANDOMIZEQUESTION"]);
                        //assessmentconfiguration.ScoreAsYouGo = Convert.ToBoolean(dataReader["SCOREASYOUGO"]);
                        assessmentconfiguration.ScoreType = dataReader["SCORETYPE"] == DBNull.Value ? "" : dataReader["SCORETYPE"].ToString();
                        assessmentconfiguration.ShowQuestionAnswerSummary = Convert.ToBoolean(dataReader["SHOWQUESTIONANSWERSUMMARY"]);
                        assessmentconfiguration.StrictlyEnforcePolicyToBeUsed = Convert.ToBoolean(dataReader["STRICLYENFORCEPOLICYTOBEUSED"]);
                        assessmentconfiguration.RestrictiveMode = dataReader["RESTRICTIVEASSESSMENTMODETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICTIVEASSESSMENTMODETF"].ToString());
                        assessmentconfiguration.AdvanceQuestionSelectionType = dataReader["ADVANCEQUESTIONSELECTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["ADVANCEQUESTIONSELECTIONTYPE"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded
                        assessmentconfiguration.UseWeightedScore = dataReader["SCOREWEIGHTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SCOREWEIGHTTF"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded; // temporarily hard-coded
                        assessmentconfiguration.GradeQuestions = dataReader["GRADEQUESTIONS"] == DBNull.Value ? "" : Convert.ToString(dataReader["GRADEQUESTIONS"]);
                        assessmentconfiguration.DisplaySeatTimeSatisfiedMessageTF = dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"]);
                        assessmentconfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF = dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"]);
                        assessmentconfiguration.NoResultText = dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"] == DBNull.Value ? "" : dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"].ToString();
                        assessmentconfiguration.MaxAttemptHandlerEnabled = dataReader["ENABLEMAXATTEMPTHANDLER"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEMAXATTEMPTHANDLER"]);
                        assessmentconfiguration.AssessmentResultEnabled = false;


                        if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED)
                        {
                            assessmentconfiguration.ScoreAsYouGo = false;
                        }
                        else if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_EACH_QUESTION_IS_ANSWERED)
                        {
                            assessmentconfiguration.ScoreAsYouGo = true;
                        }                        
                    }
                }
                return assessmentconfiguration;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns Practice Exam course configuration
        /// </summary>
        /// <param name="courseID">int ExamID</param>
        /// <returns>AssessmentConfiguration object</returns>
        public AssessmentConfiguration GetQuizExamAssessmentConfiguration(int contentObjectID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets a courseconfiguration record
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_QUIZEXAM_ASSESSMENTCONFIGURATION);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);

                AssessmentConfiguration assessmentconfiguration = null;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                                       
                    while (dataReader.Read())
                    {
                        assessmentconfiguration = new AssessmentConfiguration();
                        
                        String assessmentType = dataReader["ASSESSMENTTYPE"] == DBNull.Value ? "" : dataReader["ASSESSMENTTYPE"].ToString();
                        if (assessmentType == "")
                        {
                            continue;
                        }
                        else if (assessmentType == AssessmentConfiguration.ASSESSMENTYPE_QUIZ)
                        {
                            assessmentconfiguration.AssessmentType = assessmentType;
                        }

                        assessmentconfiguration.ID = Convert.ToInt32(dataReader["ID"]);
                        assessmentconfiguration.Enabled = Convert.ToBoolean(dataReader["ENABLED"]);
                        assessmentconfiguration.MaximumNOAttempt = dataReader["MAXIMUMNOATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXIMUMNOATTEMPT"]);
                        assessmentconfiguration.NOQuestion = Convert.ToInt32(dataReader["NOQUESTION"]);
                        assessmentconfiguration.MasteryScore = Convert.ToInt32(dataReader["MASTERYSCORE"]);
                        assessmentconfiguration.ActionToTakeAfterFailingMaxAttempt = dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"] == DBNull.Value ? "" : dataReader["ACTIONTOTAKEAFTERFAILINGMAXATTEMPT"].ToString();
                        assessmentconfiguration.AllowPauseResumeAssessment = Convert.ToBoolean(dataReader["ALLOWPAUSERESUMEASSESSMENT"]);
                        assessmentconfiguration.AllowSkippingQuestion = Convert.ToBoolean(dataReader["ALLOWSKIPPINGQUESTION"]);
                        assessmentconfiguration.EnforceMaximumTimeLimit = Convert.ToInt32(dataReader["ENFORCEMAXIMUMTIMELIMIT"]);
                        assessmentconfiguration.EnforceUniqueQuestionsOnRetake = Convert.ToBoolean(dataReader["ENFORCEUNIQUEQUESTIONSONRETAKE"]);
                        assessmentconfiguration.MinimumTimeBeforeStart = dataReader["MINIMUMTIMEBEFORESTART"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MINIMUMTIMEBEFORESTART"]);
                        assessmentconfiguration.MinimumTimeBeforeStartUnit = dataReader["MINIMUMTIMEBEFORESTART_UNIT"] == DBNull.Value ? "" : dataReader["MINIMUMTIMEBEFORESTART_UNIT"].ToString();
                        assessmentconfiguration.ContentRemediation = Convert.ToBoolean(dataReader["CONTENTREMEDIATION"]);
                        assessmentconfiguration.ProctoredAssessment = Convert.ToBoolean(dataReader["PROCTOREDASSESSMENT"]);
                        assessmentconfiguration.QuestionLevelResult = Convert.ToBoolean(dataReader["QUESTIONLEVELRESULT"]);
                        assessmentconfiguration.RandomizeAnswers = Convert.ToBoolean(dataReader["RANDOMIZEANSWERS"]);
                        assessmentconfiguration.RandomizeQuestion = Convert.ToBoolean(dataReader["RANDOMIZEQUESTION"]);
                        //assessmentconfiguration.ScoreAsYouGo = Convert.ToBoolean(dataReader["SCOREASYOUGO"]);
                        assessmentconfiguration.ScoreType = dataReader["SCORETYPE"] == DBNull.Value ? "" : dataReader["SCORETYPE"].ToString();
                        assessmentconfiguration.ShowQuestionAnswerSummary = Convert.ToBoolean(dataReader["SHOWQUESTIONANSWERSUMMARY"]);
                        assessmentconfiguration.StrictlyEnforcePolicyToBeUsed = Convert.ToBoolean(dataReader["STRICLYENFORCEPOLICYTOBEUSED"]);
                        assessmentconfiguration.RestrictiveMode = dataReader["RESTRICTIVEASSESSMENTMODETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["RESTRICTIVEASSESSMENTMODETF"].ToString());
                        assessmentconfiguration.AdvanceQuestionSelectionType = dataReader["ADVANCEQUESTIONSELECTIONTYPE"] == DBNull.Value ? "" : Convert.ToString(dataReader["ADVANCEQUESTIONSELECTIONTYPE"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded
                        assessmentconfiguration.UseWeightedScore = dataReader["SCOREWEIGHTTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SCOREWEIGHTTF"]);//AssessmentConfiguration.ADVANCEQUESTIONSELECTIONTYPE_DISCRETE; // temporarily hard-coded; // temporarily hard-coded
                        assessmentconfiguration.GradeQuestions = dataReader["GRADEQUESTIONS"] == DBNull.Value ? "" : Convert.ToString(dataReader["GRADEQUESTIONS"]);
                        assessmentconfiguration.DisplaySeatTimeSatisfiedMessageTF = dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSEATTIMESATISFIEDMESSAGETF"]);
                        assessmentconfiguration.AllowPostAssessmentAfterSeatTimeSatisfiedTF = dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWPOSTASSESSMENTAFTERSEATTIMESATISFIEDTF"]);
                        assessmentconfiguration.NoResultText = dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"] == DBNull.Value ? "" : dataReader["TURNOFFASSESSMENTSCORINGCUSTOMMESSAGE"].ToString();
                        assessmentconfiguration.MaxAttemptHandlerEnabled = dataReader["ENABLEMAXATTEMPTHANDLER"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ENABLEMAXATTEMPTHANDLER"]);
                        assessmentconfiguration.AssessmentResultEnabled = false;


                        if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_ASSESSMENT_IS_SUBMITTED)
                        {
                            assessmentconfiguration.ScoreAsYouGo = false;
                        }
                        else if (assessmentconfiguration.GradeQuestions == AssessmentConfiguration.GRADEQUESTION_AFTER_EACH_QUESTION_IS_ANSWERED)
                        {
                            assessmentconfiguration.ScoreAsYouGo = true;
                        }
                    }
                }
                return assessmentconfiguration;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        public string GetCourseName(int courseID)
        {
            DbCommand dbCommand = null;
            string courseName = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_NAME);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                    }
                }
                return courseName;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        /// <summary>
        /// This method gets the course name
        /// </summary>
        /// <param name="courseID">int courseid</param>
        /// <returns>string courseName</returns>
        public string[] GetCourseNameAndDescription(int courseID)
        {
            DbCommand dbCommand = null;
            string[] coursenameanddescription=new string[2];
            string courseName = string.Empty;
            string courseDescription = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_NAME);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        courseDescription = dataReader["DESCRIPTION"] == DBNull.Value ? "" : dataReader["DESCRIPTION"].ToString();
                    }
                }
                coursenameanddescription[0] = courseName;
                coursenameanddescription[1] = courseDescription;
                return coursenameanddescription;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return coursenameanddescription;
            }
        }  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetCourseGUID(int courseID)
        {
            DbCommand dbCommand = null;
            string courseGUID = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_GUID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseGUID = dataReader["COURSE_GUID"] == DBNull.Value ? "" : dataReader["COURSE_GUID"].ToString();
                    }
                }
                return courseGUID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetOriginalCourseID(int offeredcourseID)
        {
            DbCommand dbCommand = null;
            int originalCourseID = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ORIGINAL_COURSE_ID);
                db.AddInParameter(dbCommand, "@OFFERED_COURSE_ID", DbType.Int32, offeredcourseID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        originalCourseID = dataReader["ORIGINAL_COURSE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ORIGINAL_COURSE_ID"]);                        
                    }
                }
                return originalCourseID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetCourseImageAsset(int courseID)
        {
            DbCommand dbCommand = null;
            int AssetID = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_IMAGE_ASSET);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        AssetID = dataReader["ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSET_ID"]);
                    }
                }
                return AssetID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        /// <summary>
        /// Gets the course type by course guid
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetCourseTypeByGUID(string courseGUID)
        {
            DbCommand dbCommand = null;
            string courseStatus = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSETYPE_BY_GUID);
                db.AddInParameter(dbCommand, "@COURSE_GUID", DbType.String, courseGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseStatus = dataReader["COURSETYPE"] == DBNull.Value ? "" : dataReader["COURSETYPE"].ToString();
                    }
                }
                return courseStatus;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

       
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        /// 

        public string GetContentObjectName(int contentObjectID)
        {
            DbCommand dbCommand = null;
            string name = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_NAME);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                    }
                }
                return name;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string GetContentObjectName(string ContentObjectGUID)
        {
            DbCommand dbCommand = null;
            string name = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_NAME_BY_GUID);
                db.AddInParameter(dbCommand, "@ipCONTENTOBJECTGUID", DbType.String, ContentObjectGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        name = dataReader["CONTENTOBJECTNAME"] == DBNull.Value ? "" : dataReader["CONTENTOBJECTNAME"].ToString();
                    }
                }
                return name;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public string GetContentObjectNameByExamGUID(string ExamGUID)
        {
            DbCommand dbCommand = null;
            string name = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_NAME_BY_EXAM_GUID);
                db.AddInParameter(dbCommand, "@ipEXAMGUID", DbType.String, ExamGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        name = dataReader["CONTENTOBJECTNAME"] == DBNull.Value ? "" : dataReader["CONTENTOBJECTNAME"].ToString();
                    }
                }
                return name;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        /// <summary>
        /// This method is used to get all demoable contentobjects belonging to certain course.
        /// </summary>
        /// <param name="courseID">CourseId non-zero integer value.</param>
        /// <returns>This method will return list of sequence items representing contentobjects, null otherwise.</returns>
        public List<ContentObject> GetCourseDemoableContentObjects(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all demoable content objects related to a particular course
                //This procedure will return all the content objects in child hierarchy 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_DEMOABLECONTENTOBJECTS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<ContentObject> contentObjects = new List<ContentObject>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    ContentObject contenObject;
                    while (dataReader.Read())
                    {
                        contenObject = new ContentObject();
                        contenObject.ContentObjectID = Convert.ToInt32(dataReader["ID"]);
                        contenObject.ParentContentObjectID = dataReader["PARENT_CO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["PARENT_CO_ID"]);
                        contenObject.AlowQuizTF = dataReader["ALLOWQUIZTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ALLOWQUIZTF"]);
                        contenObject.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        contenObject.ContentObject_GUID = dataReader["CONTENTOBJECT_GUID"] == DBNull.Value ? "" : dataReader["CONTENTOBJECT_GUID"].ToString();
                        contenObject.MaxQuizQuestionsToAsk = dataReader["MAXQUIZQUESTIONSTOASK"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MAXQUIZQUESTIONSTOASK"]);
                        contenObject.OverrideMaxQuizQuestionsToAsk = dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OVERRIDEMAXQUIZQUESTIONSTOASKTF"]); 
                        contentObjects.Add(contenObject);
                    }

                }
                return contentObjects;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public int GetCourseID(string courseGUID)
        {
            DbCommand dbCommand = null;
            int courseID = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_ID);
                db.AddInParameter(dbCommand, "@COURSE_GUID", DbType.String, courseGUID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                    }
                }
                return courseID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        #endregion

        #region ContentObject Methods
        /// <summary>
        /// This method is used to get all scenes belong to certain contentobject.
        /// </summary>
        /// <param name="contentObjectID">ContentObjectId non-zero integer value.</param>
        /// <returns>This method will return scenes, null otherwise.</returns>
        public List<Scene> GetContentObjectScenes(int contentObjectID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all scenes associated with a given cntent object
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_SCENES);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);

                List<Scene> scenes = new List<Scene>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Scene scene;
                    while (dataReader.Read())
                    {
                        scene = new Scene();
                        scene.SceneID = Convert.ToInt32(dataReader["ID"]);
                        scene.SceneName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        scene.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();
                        scene.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        scene.SceneTemplateID = dataReader["SCENETEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENETEMPLATE_ID"]);
                        scene.Duration = dataReader["DURATION"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DURATION"]);
                        scene.IsTitleVisible = dataReader["NAMEVISIBLETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["NAMEVISIBLETF"]);
                        scene.IsViewStreaming = dataReader["VIEWSTREAMINGTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIEWSTREAMINGTF"]);
                        scene.IsTopicTileVisible = dataReader["TOPICTITLEVISIBLEVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["TOPICTITLEVISIBLEVISIBLE"]);
                        scene.IsPlayPauseFeature = dataReader["PLAYPAUSEFEATURETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYPAUSEFEATURETF"]);
                        scene.DisplayStandardTF = dataReader["DISPLAYSTANDARDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSTANDARDTF"]);
                        scene.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]);

                        scene.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                        scene.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609
                        scenes.Add(scene);

                    }
                }
                return scenes;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public List<Scene> GetContentObjectDemoableScenes(int contentObjectID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns demoable scenes associated with a given cntent object
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_DEMOABLESCENES);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);

                List<Scene> scenes = new List<Scene>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Scene scene;
                    while (dataReader.Read())
                    {
                        scene = new Scene();
                        scene.SceneID = Convert.ToInt32(dataReader["ID"]);
                        scene.SceneName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        scene.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();
                        scene.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        scene.SceneTemplateID = dataReader["SCENETEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENETEMPLATE_ID"]);
                        scene.IsViewStreaming = dataReader["VIEWSTREAMINGTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIEWSTREAMINGTF"]);
                        scene.IsPlayPauseFeature = dataReader["PLAYPAUSEFEATURETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["PLAYPAUSEFEATURETF"]);
                        scene.DisplayStandardTF = dataReader["DISPLAYSTANDARDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSTANDARDTF"]);
                        scene.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]);

                        scene.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                        scene.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609

                        scenes.Add(scene);

                    }
                }
                return scenes;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion

        #region Scene Methods
        /// <summary>
        /// This method returns all scenes related to a course via contentobjects
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of Scene objects</returns>
        public List<Scene> GetCourseScenes(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all scenes of a course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_SCENES);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<Scene> scenes = new List<Scene>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Scene scene;
                    while (dataReader.Read())
                    {
                        scene = new Scene();
                        scene.SceneID = Convert.ToInt32(dataReader["ID"]);
                        scene.SceneName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        scene.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();
                        scene.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        scene.SceneTemplateID = dataReader["SCENETEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENETEMPLATE_ID"]);
                        scene.Duration = dataReader["DURATION"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DURATION"]);
                        scene.IsTitleVisible = dataReader["NAMEVISIBLETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["NAMEVISIBLETF"]);
                        scene.IsViewStreaming = dataReader["VIEWSTREAMINGTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIEWSTREAMINGTF"]);
                        scene.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        scene.ContenObjectID = dataReader["SCENEPARENTID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENEPARENTID"]);
                        scene.ContentObjectName = dataReader["CONTENTOBJECTNAME"] == DBNull.Value ? "" : dataReader["CONTENTOBJECTNAME"].ToString();
                        scene.IsTopicTileVisible = dataReader["TOPICTITLEVISIBLEVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["TOPICTITLEVISIBLEVISIBLE"]);
                        
                        scene.VideoFilename = dataReader["VIDEOFILENAME"] == DBNull.Value ? "" : dataReader["VIDEOFILENAME"].ToString();
                        scene.StreamingServerApplication = dataReader["STREAMINGSERVERAPPLICATION"] == DBNull.Value ? "" : dataReader["STREAMINGSERVERAPPLICATION"].ToString();
                        
                        scene.StartQueueHours = dataReader["STARTQUEUEHOUR"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUEHOUR"]);
                        scene.StartQueueMinutes = dataReader["STARTQUEUEMINUTE"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUEMINUTE"]);                        
                        scene.StartQueueSeconds = dataReader["STARTQUEUESECOND"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUESECOND"]);
                        
                        scene.EndQueueHours = dataReader["ENDQUEUEHOUR"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUEHOUR"]);
                        scene.EndQueueMinutes = dataReader["ENDQUEUEMINUTE"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUEMINUTE"]);
                        scene.EndQueueSeconds = dataReader["ENDQUEUESECOND"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUESECOND"]);
                        
                        scene.VideoHeight = dataReader["VIDEOHEIGHT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["VIDEOHEIGHT"]);
                        scene.VideoWidth = dataReader["VIDEOWIDTH"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["VIDEOWIDTH"]);
                        
                        scene.FullScreen = dataReader["VIDEOFULLSCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIDEOFULLSCREENTF"]);

                        scene.DisplayStandardTF = dataReader["DISPLAYSTANDARDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSTANDARDTF"]);

                        scene.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]);

                        scene.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                        scene.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609


                        scenes.Add(scene);

                    }
                }
                return scenes;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns all demoable scenes related to a course 
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of Scene objects</returns>
        public List<Scene> GetCourseDemoableScenes(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all scenes of a course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_DEMOABLESCENES);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<Scene> scenes = new List<Scene>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Scene scene;
                    while (dataReader.Read())
                    {
                        scene = new Scene();
                        scene.SceneID = Convert.ToInt32(dataReader["ID"]);
                        scene.SceneName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        scene.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();
                        scene.Scene_GUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        scene.SceneTemplateID = dataReader["SCENETEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENETEMPLATE_ID"]);
                        scene.Duration = dataReader["DURATION"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DURATION"]);
                        scene.IsTitleVisible = dataReader["NAMEVISIBLETF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["NAMEVISIBLETF"]);
                        scene.IsViewStreaming = dataReader["VIEWSTREAMINGTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIEWSTREAMINGTF"]);
                        scene.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        scene.ContenObjectID = dataReader["SCENEPARENTID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SCENEPARENTID"]);
                        scene.ContentObjectName = dataReader["CONTENTOBJECTNAME"] == DBNull.Value ? "" : dataReader["CONTENTOBJECTNAME"].ToString();
                        scene.IsTopicTileVisible = dataReader["TOPICTITLEVISIBLEVISIBLE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["TOPICTITLEVISIBLEVISIBLE"]);
                        scene.VideoFilename = dataReader["VIDEOFILENAME"] == DBNull.Value ? "" : dataReader["VIDEOFILENAME"].ToString();
                        scene.StreamingServerApplication = dataReader["STREAMINGSERVERAPPLICATION"] == DBNull.Value ? "" : dataReader["STREAMINGSERVERAPPLICATION"].ToString();
                        scene.StartQueueHours = dataReader["STARTQUEUEHOUR"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUEHOUR"]);
                        scene.StartQueueMinutes = dataReader["STARTQUEUEMINUTE"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUEMINUTE"]);
                        scene.StartQueueSeconds = dataReader["STARTQUEUESECOND"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["STARTQUEUESECOND"]);
                        scene.EndQueueHours = dataReader["ENDQUEUEHOUR"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUEHOUR"]);
                        scene.EndQueueMinutes = dataReader["ENDQUEUEMINUTE"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUEMINUTE"]);
                        scene.EndQueueSeconds = dataReader["ENDQUEUESECOND"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ENDQUEUESECOND"]);
                        scene.VideoHeight = dataReader["VIDEOHEIGHT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["VIDEOHEIGHT"]);
                        scene.VideoWidth = dataReader["VIDEOWIDTH"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["VIDEOWIDTH"]);
                        scene.FullScreen = dataReader["VIDEOFULLSCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["VIDEOFULLSCREENTF"]);
                        scene.DisplayStandardTF = dataReader["DISPLAYSTANDARDTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYSTANDARDTF"]);
                        scene.DisplayWideScreenTF = dataReader["DISPLAYWIDESCREENTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DISPLAYWIDESCREENTF"]);

                        scene.IsEmbedCode = dataReader["ISEMBEDCODE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISEMBEDCODE"]); //Added by Abdus Samad //Embeded Code WLCMS-2609
                        scene.EmbedCode = dataReader["EMBEDCODE"] == DBNull.Value ? "" : dataReader["EMBEDCODE"].ToString(); ; //Added by Abdus Samad //Embeded Code WLCMS-2609

                        scenes.Add(scene);

                    }
                }
                return scenes;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method is used to get all assets belong to certain Scene.
        /// </summary>
        /// <param name="sceneID">SceneId non-zero integer value.</param>
        /// <returns>This method will return list of assets, null otherwise.</returns>
        public List<Asset> GetSceneAssets(int sceneID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all assets associated with a given scene
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENE_ASSETS);
                db.AddInParameter(dbCommand, "@SCENE_ID", DbType.Int32, sceneID);

                List<Asset> assets = new List<Asset>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Asset asset;
                    while (dataReader.Read())
                    {
                        asset = new Asset();
                        asset.AssetID = Convert.ToInt32(dataReader["ID"]);
                        asset.ContentText= dataReader["CONTENT"] == DBNull.Value ? "" : dataReader["CONTENT"].ToString();
                        asset.URL = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        asset.AssetType = dataReader["ASSETTYPE"] == DBNull.Value ? "" : dataReader["ASSETTYPE"].ToString();
                        asset.Asset_GUID = dataReader["ASSET_GUID"] == DBNull.Value ? "" : dataReader["ASSET_GUID"].ToString();
                        asset.AssetSceneOrientation = dataReader["ORIENTATIONKEY"] == DBNull.Value ? "" : dataReader["ORIENTATIONKEY"].ToString();
                        asset.ActionScriptVersion = dataReader["ACTIONSCRIPTVERSION"] == DBNull.Value ? "" : dataReader["ACTIONSCRIPTVERSION"].ToString();
                        assets.Add(asset);
                    }
                }

                if (assets.Count == 0)
                {
                    Asset asset;
                    //Add empty Asset
                    asset = new Asset();
                    asset.AssetID = 1;
                    asset.ContentText = "";
                    asset.URL = "";
                    asset.AssetType = "Text";
                    asset.Asset_GUID = "";
                    asset.AssetSceneOrientation = "$Text";
                    assets.Add(asset);
                }

                return assets;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets the scene template HTMLURL
        /// </summary>
        /// <param name="sceneTemplateID"></param>
        /// <returns></returns>
        public string GetSceneTemplateHtmlURL(int sceneTemplateID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the scene template HTML URL 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENETEMPLATE_HTMLURL);
                db.AddInParameter(dbCommand, "@SCENETEMPLATE_ID", DbType.Int32, sceneTemplateID);

                string sceneTempalteHTMLURL = string.Empty;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneTempalteHTMLURL = dataReader["TEMPLATEHTMLURL"] == DBNull.Value ? "" : dataReader["TEMPLATEHTMLURL"].ToString();
                        
                    }
                }
                return sceneTempalteHTMLURL;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        public string GetSceneTemplateTypeHtmlURL(string sceneTemplateType)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the scene template HTML URL 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENETEMPLATETYPE_HTMLURL);
                db.AddInParameter(dbCommand, "@SCENETEMPLATETYPE", DbType.String, sceneTemplateType);

                string sceneTempalteHTMLURL = string.Empty;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneTempalteHTMLURL = dataReader["TEMPLATEHTMLURL"] == DBNull.Value ? "" : dataReader["TEMPLATEHTMLURL"].ToString();
                        
                    }
                }
                return sceneTempalteHTMLURL;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }

        }
        /// <summary>
        /// This method gets the scene template HTML variant according to the parameters
        /// </summary>
        /// <param name="sceneTemplateID">int sceneTemplateID</param>
        /// <returns>SceneTemplate object</returns>
        public SceneTemplate GetSceneTemplateWithHTMLVariant(int sceneTemplateID, bool isText, bool isVOText, bool isVisualTop,bool isHeading)
        {
            DbCommand dbCommand = null;
            SceneTemplate sceneTemplate = new SceneTemplate();
            try
            {
                //This SP returns the scene template HTML URL 
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENETEMPLATEVARIANT);
                db.AddInParameter(dbCommand, "@SCENETEMPLATE_ID", DbType.String, sceneTemplateID);
                db.AddInParameter(dbCommand, "@ISTEXT", DbType.Boolean, isText);
                db.AddInParameter(dbCommand, "@ISVOTEXT", DbType.Boolean, isVOText);
                db.AddInParameter(dbCommand, "@ISVISUALTOP", DbType.Boolean, isVisualTop);
                db.AddInParameter(dbCommand, "@ISHEADING", DbType.Boolean, isHeading);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneTemplate.TemplateHTMLURL= dataReader["TEMPLATEHTMLURL"] == DBNull.Value ? "" : dataReader["TEMPLATEHTMLURL"].ToString();

                        sceneTemplate.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();

                        // MC
                        //-------------------------------
                        if (sceneTemplate.SceneTemplateType == "MC")
                        {
                            sceneTemplate.TemplateHTMLURL = System.Web.HttpContext.Current.Server.MapPath(".." + sceneTemplate.TemplateHTMLURL);
                        }
                        //-------------------------------
                        
                    }
                }
                return sceneTemplate;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This method selects scene templates by category
        /// </summary>
        /// <param name="sceneTemplateCategory">string sceneTemplateCategory</param>
        /// <returns>List of SceneTemplate object</returns>
        public List<SceneTemplate> GetCategorySceneTemplates(string sceneTemplateCategory)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns the scene templates by category
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENETEMPLATECATEGORY_SCENETEMPLATE);
                db.AddInParameter(dbCommand, "@SCENETEMPLATECATEGORY", DbType.String, sceneTemplateCategory);

                List<SceneTemplate> sceneTemplates = new List<SceneTemplate>();
                SceneTemplate sceneTemplate = null;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneTemplate = new SceneTemplate();
                        sceneTemplate.SceneTemplateID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                        sceneTemplate.SceneTemplateType = dataReader["SCENETEMPLATETYPE"] == DBNull.Value ? "" : dataReader["SCENETEMPLATETYPE"].ToString();
                        sceneTemplates.Add(sceneTemplate);
                    }
                }
                return sceneTemplates;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetSceneGUID(int sceneID)
        {
            DbCommand dbCommand = null;
            string sceneGUID = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENE_GUID);
                db.AddInParameter(dbCommand, "@SCENE_ID", DbType.Int32, sceneID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneGUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                    }
                }
                return sceneGUID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        #endregion

        #region Asset Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetAssetGUID(int assetID)
        {
            DbCommand dbCommand = null;
            string assetGUID = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSET_GUID);
                db.AddInParameter(dbCommand, "@ASSET_ID", DbType.Int32, assetID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assetGUID = dataReader["ASSET_GUID"] == DBNull.Value ? "" : dataReader["ASSET_GUID"].ToString();
                    }
                }
                return assetGUID;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        public Asset GetAsset(int assetID)
        {
            DbCommand dbCommand = null;            
            Asset asset = null;
            try
            {
                asset = new Asset();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_ASSET);
                db.AddInParameter(dbCommand, "@ASSET_ID", DbType.Int32, assetID);
                db.AddInParameter(dbCommand, "@VERSION_ID", DbType.Int32, 0);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        asset.URL = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        asset.AssetType = dataReader["ASSETTYPE"] == DBNull.Value ? "" : dataReader["ASSETTYPE"].ToString();
                        asset.ContentText = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();                        

                    }
                }
                return asset;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return asset;
            }
        }

        public Asset GetAffidavitAsset(int affidativeID)
        {
            DbCommand dbCommand = null;
            Asset asset = null;
            try
            {
                asset = new Asset();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.GET_ASSET);
                db.AddInParameter(dbCommand, "@ASSET_ID", DbType.Int32, affidativeID);
                db.AddInParameter(dbCommand, "@VERSION_ID", DbType.Int32, 0);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        asset.URL = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        asset.AssetType = dataReader["ASSETTYPE"] == DBNull.Value ? "" : dataReader["ASSETTYPE"].ToString();
                        asset.AssetName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        asset.ContentText = dataReader["CONTENT"] == DBNull.Value ? "" : dataReader["CONTENT"].ToString();
                        //LCMS-11217
                        asset.AffidavitTemplateId=(dataReader["AFFIDAVITTEMPLATE_ID"]==DBNull.Value?0: Convert.ToInt64(dataReader["AFFIDAVITTEMPLATE_ID"]));
                        asset.DisplayText1=dataReader["DISPLAYTEXT1"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT1"].ToString();
                        asset.DisplayText2=dataReader["DISPLAYTEXT2"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT2"].ToString();
                        asset.DisplayText3 = dataReader["DISPLAYTEXT3"] == DBNull.Value ? string.Empty : dataReader["DISPLAYTEXT3"].ToString();
                        //End
                    }
                }
                return asset;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return asset;
            }
        }


        #endregion
        
        #region GlossaryItem Methods
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of glossaryitem objects</returns>
        public List<GlossaryItem> GetCourseGlossaryItems(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all glossary items associated with  given course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_GLOSSARYITEMS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<GlossaryItem> glossaryItems = new List<GlossaryItem>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    FillGlossaryItemEntityList(dataReader, glossaryItems);
                }
                return glossaryItems;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the list of glossary items
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of glossaryitem objects</returns>
        public List<GlossaryItem> GetCourseSceneGlossaryItems(int courseID, int sceneID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all glossary items associated with  given course
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_SCENE_GLOSSARYITEMS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@SCENE_ID", DbType.Int32, sceneID);
                List<GlossaryItem> glossaryItems = new List<GlossaryItem>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    FillGlossaryItemEntityList(dataReader, glossaryItems);
                }
                return glossaryItems;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// Thi method fills the glossaryitems list with the data from IDataReader 
        /// </summary>
        /// <param name="dataReader">IDataReader datareader</param>
        /// <param name="glossaryItems">List GlossaryItem object</param>
        private void FillGlossaryItemEntityList(IDataReader dataReader, List<GlossaryItem> glossaryItems)
        {
            GlossaryItem glossaryItem;
            while (dataReader.Read())
            {
                glossaryItem = new GlossaryItem();
                glossaryItem.GlossaryItemID = Convert.ToInt32(dataReader["ID"]);
                glossaryItem.Term = dataReader["TERM"] == DBNull.Value ? "" : dataReader["TERM"].ToString();
                glossaryItems.Add(glossaryItem);
            }
        }
        /// <summary>
        /// This method returns the glossary item definition
        /// </summary>
        /// <param name="glossaryItemID">int glossaryitemID</param>
        /// <returns>GlossaryItem object</returns>
        public GlossaryItem GetGlossaryItemDefinition(int glossaryItemID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns glossary item definition
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_GLOSSARYITEM_DEFINITION);
                db.AddInParameter(dbCommand, "@GLOSSARYITEM_ID", DbType.Int32, glossaryItemID);
                GlossaryItem glossaryItem = new GlossaryItem();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        glossaryItem.Definition = dataReader["DEFINITION"] == DBNull.Value ? "" : dataReader["DEFINITION"].ToString();
                    }
                }
                return glossaryItem;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion

        #region CourseMaterial Methods
        /// <summary>
        /// This method gets the CourseMaterialInfo objects associated to a Course
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of CourseMaterialInfo object</returns>
        public List<CourseMaterialInfo> GetCourseMaterialInfo(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns all assets associated with a given scene
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_COURSEMATERIAL);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<CourseMaterialInfo> courseMaterialInfos = new List<CourseMaterialInfo>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    CourseMaterialInfo courseMaterialInfo = null;
                    while (dataReader.Read())
                    {
                        courseMaterialInfo = new CourseMaterialInfo();
                        courseMaterialInfo.CourseMaterialAssetLocation = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        courseMaterialInfo.CourseMaterialAssetName = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        courseMaterialInfos.Add(courseMaterialInfo);
                    }
                }
                return courseMaterialInfos;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion

        #region Intro/End Page Methods
        /// <summary>
        /// This method gets the CourseIntroPage
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>CourseIntroPage object</returns>
        public CourseIntroPage GetIntroPage(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns course intro page
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_INTROPAGE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                CourseIntroPage courseIntroPage = new CourseIntroPage();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseIntroPage.Url = dataReader["INTROPAGEURL"] == DBNull.Value ? "" : dataReader["INTROPAGEURL"].ToString();
                    }
                }
                if (courseIntroPage.Url == "")
                {
                    
                    dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_INTROPAGE);
                    db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, 0);
                    courseIntroPage = new CourseIntroPage();
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            courseIntroPage.Url = dataReader["INTROPAGEURL"] == DBNull.Value ? "" : dataReader["INTROPAGEURL"].ToString();
                        }
                    }
                
                }

                return courseIntroPage;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the course end page
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>CourseEndPage object</returns>
        public CourseEndPage GetEndPage(int courseID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP returns course end page
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_ENDPAGE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                CourseEndPage courseEndPage = new CourseEndPage();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseEndPage.Url = dataReader["ENDPAGEURL"] == DBNull.Value ? "" : dataReader["ENDPAGEURL"].ToString();
                    }
                }
                if (courseEndPage.Url == "")
                {

                    dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_ENDPAGE);
                    db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, 0);
                    courseEndPage = new CourseEndPage();
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            courseEndPage.Url = dataReader["ENDPAGEURL"] == DBNull.Value ? "" : dataReader["ENDPAGEURL"].ToString();
                        }
                    }

                }

                return courseEndPage;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public EOCInstructions GetEOCInstructions(int courseID)
        {

            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_GET_EOCINSTRUCTIONS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                EOCInstructions endOfCourseInstructions = new EOCInstructions();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dataReader.Read();
                    endOfCourseInstructions.CourseID = courseID;
                    endOfCourseInstructions.BrandingEOCInstructions = dataReader["RESOURCEVALUE"] == DBNull.Value ? "" : dataReader["RESOURCEVALUE"].ToString();
                    endOfCourseInstructions.CourseConfigurationEOCInstructions = dataReader["COURSECONFIGURATION_ENDOFCOURSEINSTRUCTIONS"] == DBNull.Value ? "" : dataReader["COURSECONFIGURATION_ENDOFCOURSEINSTRUCTIONS"].ToString();
                    endOfCourseInstructions.CourseEOCInstructions = dataReader["COURSE_ENDOFCOURSEINSTRUCTIONS"] == DBNull.Value ? "" : dataReader["COURSE_ENDOFCOURSEINSTRUCTIONS"].ToString();
                    
                }

                return endOfCourseInstructions;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public String GetCourseEOCInstructions(int courseID)
        {

            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.LMS_GET_COURSE_EOCINSTRUCTIONS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dataReader.Read();                    
                    return dataReader["ENDOFCOURSEINSTRUCTIONS"] == DBNull.Value ? "" : dataReader["ENDOFCOURSEINSTRUCTIONS"].ToString();

                }
                
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        #endregion

        #region Validation Questions
        /// <summary>
        /// This method gets the validation questions and their answers
        /// </summary>
        /// <param name="variant">string variant</param>
        /// <returns>list of object validation questions</returns>
        public List<ValidationQuestion> GetValidationQuestions(int learnerID,string variant)
        {
            List<ValidationQuestion> validationQuestions = new List<ValidationQuestion>();
            ValidationQuestion validationQuestion = null;
            DbCommand dbCommand = null;
            string answerQuery=string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNER_VALIDATIONQUESTIONS);
                db.AddInParameter(dbCommand, "@VARIANT", DbType.String, variant);
                db.AddInParameter(dbCommand, "@LEARNERID", DbType.Int32, learnerID);//LCMS-12532 Yasin;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        validationQuestion=new ValidationQuestion();
                        answerQuery = string.Empty;
                        validationQuestion.QuestionStem = dataReader["QUESTIONSTEM"] == DBNull.Value ? "" : dataReader["QUESTIONSTEM"].ToString();
                        validationQuestion.QuestionType = dataReader["QUESTIONTYPE"] == DBNull.Value ? ValidationQuestionType.FillInTheBlank : dataReader["QUESTIONTYPE"].ToString();
                        validationQuestion.LanguageID = dataReader["LANGUAGE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["LANGUAGE_ID"]);
                        validationQuestion.ValiditionQuestionId = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"].ToString());
                        answerQuery = dataReader["ANSWERQUERY"] == DBNull.Value ? "" : dataReader["ANSWERQUERY"].ToString();
                        validationQuestion.Answer=GetValidationQuestionAnswer(answerQuery,learnerID);
                        if (validationQuestion.Answer == string.Empty)
                            continue;
                        if (validationQuestion.QuestionType == ValidationQuestionType.SingleSelect)
                        {
                            validationQuestion.ValidationQuestionOption = GetValidationQuestionOption(validationQuestion.ValiditionQuestionId);
                        }
                        else
                        {
                            validationQuestion.ValidationQuestionOption = new List<ValidationQuestionOption>();
                        }
                        validationQuestions.Add(validationQuestion);
                    }
                }
                return validationQuestions;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }

        }
        /// <summary>
        /// This method gets the validation questions' answers
        /// </summary>
        private string GetValidationQuestionAnswer(string answerQuery, int learnerID)
        {
            DbCommand dbCommand = null;
            string answerText = string.Empty;
            try
            {
                dbCommand = db.GetSqlStringCommand(answerQuery);
                db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, learnerID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        answerText = dataReader["ANSWERTEXT"] == DBNull.Value ? "" : dataReader["ANSWERTEXT"].ToString();
                    }
                }
                return answerText;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }

        /// <summary>
        /// This method gets the validation questions Options List
        /// </summary>
        private List<ValidationQuestionOption> GetValidationQuestionOption(int ValidationQuestionID)
        {
            List<ValidationQuestionOption> validationQuestionOptionList = new List<ValidationQuestionOption>();
            ValidationQuestionOption validationQuestionOption = null;
            DbCommand dbCommand = null;
            string answerQuery = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_LEARNER_VALIDATIONQUESTIONOPTIONS);
                db.AddInParameter(dbCommand, "@VALIDATIONQUESTIONID", DbType.Int32, ValidationQuestionID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        validationQuestionOption = new ValidationQuestionOption();
                        validationQuestionOption.ValiditionQuestionOptionId = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"].ToString());
                        validationQuestionOption.OptionLabel = dataReader["OPTIONLABEL"] == DBNull.Value ? "" : dataReader["OPTIONLABEL"].ToString();
                        validationQuestionOption.OptionValue = dataReader["OPTIONVALUE"] == DBNull.Value ? "" : dataReader["OPTIONVALUE"].ToString();
                        validationQuestionOption.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"].ToString()); ;
                        validationQuestionOptionList.Add(validationQuestionOption);
                    }
                }
                return validationQuestionOptionList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        public string CreateValidationUnlockRequest(string learningSessionId, string strGUID,string brandCode,string variant)
        {
            DbCommand dbCommand = null;
            string emailAddress = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.CREATE_VALIDATION_UNLOCK_REQUEST);
                db.AddInParameter(dbCommand, "@learningSessionId", DbType.String, learningSessionId);
                db.AddInParameter(dbCommand, "@GUID", DbType.String, strGUID);
                db.AddInParameter(dbCommand, "@brandCode", DbType.String, brandCode);
                db.AddInParameter(dbCommand, "@variant", DbType.String, variant);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                        emailAddress = dataReader[0].ToString();

                }

                return emailAddress;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return "";
            }

        }

        #endregion

        #region Course Evaluation
        public CourseEvaluation GetCourseEvaluationByCourseID(int courseID, string surveyType)
        {            
            CourseEvaluation courseEvaluation = null;
            DbCommand dbCommand = null;
            string answerQuery = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_SURVEY);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@SURVEYTYPE", DbType.String, surveyType);
                
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseEvaluation = new CourseEvaluation();
                        courseEvaluation.ContentOwnerID = dataReader["OWNER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["OWNER_ID"]);
                        courseEvaluation.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        courseEvaluation.ID = Convert.ToInt32(dataReader["ID"]);
                        courseEvaluation.IsLockedTF = dataReader["ISLOCKEDTF"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ISLOCKEDTF"]);
                        courseEvaluation.Event = dataReader["EVENT"] == DBNull.Value ? "" : dataReader["EVENT"].ToString();
                        courseEvaluation.Status = dataReader["STATUS"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        courseEvaluation.ShowAllTF = dataReader["SHOWALLTF"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SHOWALLTF"]);
                        courseEvaluation.QuestionsPerPage = dataReader["QUESTIONSPERPAGE"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["QUESTIONSPERPAGE"]);                        
                    }
                }
                return courseEvaluation;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public List<CourseEvaluationQuestion> GetCourseEvaluationQuestions(int courseEvaluationID)
        {
            List<CourseEvaluationQuestion> courseEvaluationQuestions = new List<CourseEvaluationQuestion>();
            CourseEvaluationQuestion courseEvaluationQuestion = null;
            DbCommand dbCommand = null;            
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SURVEY_QUESTIONS);
                db.AddInParameter(dbCommand, "@SURVEY_ID", DbType.Int32, courseEvaluationID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseEvaluationQuestion = GetSubTypeCourseEvaluationQuestion(dataReader["QUESTION_TYPE"].ToString());
                        courseEvaluationQuestion.QuestionID = Convert.ToInt32(dataReader["ID"]);
                        courseEvaluationQuestion.QuestionText = dataReader["TEXT"] == DBNull.Value ? "" : dataReader["TEXT"].ToString();
                        courseEvaluationQuestion.Required =  dataReader["required"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["required"]);
                        courseEvaluationQuestion.UnLimitedTF = dataReader["UNLIMITEDTF"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["UNLIMITEDTF"]);
                        courseEvaluationQuestion.DropDownTF = dataReader["DROPDOWNTF"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DROPDOWNTF"]);
                        courseEvaluationQuestion.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        courseEvaluationQuestion.Alignment = dataReader["ALIGNMENT"] == DBNull.Value ? "" : dataReader["ALIGNMENT"].ToString();
                        courseEvaluationQuestions.Add(courseEvaluationQuestion);
                    }
                }
                return courseEvaluationQuestions;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        private CourseEvaluationQuestion GetSubTypeCourseEvaluationQuestion(string courseEvaluationType)
        {
            switch (courseEvaluationType)
            {
                case CourseEvaluationQuestionType.FillInTheBlank:
                    return new FillInTheBlankCourseEvaluationQuestion();                    
                case CourseEvaluationQuestionType.MultiSelect:
                    return new MultipleSelectCourseEvaluationQuestion();                    
                case CourseEvaluationQuestionType.SingleSelect:
                    return new SingleSelectCourseEvaluationQuestion();                    
                case CourseEvaluationQuestionType.Text:
                    return new TextCourseEvaluationQuestion();                    
                default:
                    return null;                    
            } 
        }
        public List<CourseEvaluationAnswer> GetCourseEvaluationAnswerByCourseEvaluation(int courseEvaluationID)
        {
            List<CourseEvaluationAnswer> courseEvaluationAnswers = new List<CourseEvaluationAnswer>();
            CourseEvaluationAnswer courseEvaluationAnswer = null;
            DbCommand dbCommand = null;
            string answerQuery = string.Empty;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SURVEY_ANSWERS);
                db.AddInParameter(dbCommand, "@SURVEY_ID", DbType.Int32,courseEvaluationID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseEvaluationAnswer = new CourseEvaluationAnswer();
                        courseEvaluationAnswer.DisplayOrder = dataReader["DISPLAYORDER"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DISPLAYORDER"]);
                        courseEvaluationAnswer.ID = Convert.ToInt32(dataReader["ID"]);
                        courseEvaluationAnswer.Label = dataReader["Label"] == DBNull.Value ? "" : dataReader["Label"].ToString(); ;
                        courseEvaluationAnswer.QuestionID = dataReader["SURVEYQUESTION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SURVEYQUESTION_ID"]);
                        courseEvaluationAnswer.Value = dataReader["Value"] == DBNull.Value ? "" : dataReader["Value"].ToString(); ;                            
                        courseEvaluationAnswers.Add(courseEvaluationAnswer);
                    }
                }
                return courseEvaluationAnswers;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        public int GetCourseEvaluationQuestionsCount(int courseID, string surveyType)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_SURVEYQUESTIONS_COUNT);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@SURVEYTYPE", DbType.String, surveyType);
                int courseEvaluationsQuestionsCount = 0;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseEvaluationsQuestionsCount = dataReader["SURVEYQUESTIONSCOUNT"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SURVEYQUESTIONSCOUNT"]);
                    }
                }
                return courseEvaluationsQuestionsCount;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        #endregion

        #region Course Approval
        /// <summary>
        /// This method returns the list of course approvals
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of course approvals objects</returns>
        public List<CourseApproval> GetCourseCourseApproval(int courseID, string learningSessionGUID)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SELECT_COURSE_COURSEAPPROVAL);
                db.AddInParameter(dbCommand, "@LearningSessionGUID", DbType.String, learningSessionGUID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);

                List<CourseApproval> courseApprovals = new List<CourseApproval>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    FillCourseApprovalEntityList(dataReader, courseApprovals);
                }
                return courseApprovals;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// This method returns the list of course approvals
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>list of course approvals objects</returns>
        public bool CheckLearnerCourseCourseApproval(int courseID, string learningSessionGuid)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.CHECK_LEARNER_COURSE_COURSEAPPROVAL);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                int selectedCourseApprovalID=0;
                bool isCoursehaveApproval=false;
                bool isLearnerStartCourse = false;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        selectedCourseApprovalID = dataReader["SELECTEDCOURSEAPPROVALID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SELECTEDCOURSEAPPROVALID"]);
                        isCoursehaveApproval = dataReader["COURSEHAVECOURSEAPPROVAL"] == DBNull.Value ? false  : Convert.ToBoolean(dataReader["COURSEHAVECOURSEAPPROVAL"]);
                        isLearnerStartCourse = dataReader["HASLEARNERSTARTCOURSE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["HASLEARNERSTARTCOURSE"]);
                    }
                }

                if (isCoursehaveApproval == true)
                {
                    if (selectedCourseApprovalID > 0)
                    {
                        return true;
                    }

                    if (isLearnerStartCourse == true)
                    {
                        return true;
                    }

                    if (selectedCourseApprovalID <= 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                return false;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return true;
            }
        }

        public int SaveLearnerCourseApproval(int courseID, string learningSessionGuid, int CourseApprovalID)
        {
            DbCommand dbCommand = null;
            int courseapprovalid = 0;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_LEARNERCOURSEAPPROVAL);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@COURSEAPPROVALID", DbType.Int32, CourseApprovalID);
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    courseapprovalid = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);

            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                courseapprovalid = 0;
            }
            return courseapprovalid;
        }

        public bool SaveLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            DbCommand dbCommand = null;
            bool isCourseMessageDisplay = false;
            try
            {
                //This SP saves the learnerstatistics
                dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4_LEARNERCOURSEMESSAGE);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);                
                db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 4);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                    isCourseMessageDisplay = Convert.ToBoolean(dbCommand.Parameters["@NEWID"].Value);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                isCourseMessageDisplay = false;
            }
            return isCourseMessageDisplay;
        }

        /// <summary>
        /// This method returns the course message displyed or not
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>return bool</returns>
        public bool CheckLearnerCourseMessage(int courseID, string learningSessionGuid)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_CHECK_LEARNER_COURSE_MESSAGE);
                db.AddInParameter(dbCommand, "@LEARNERSESSION_GUID", DbType.String, learningSessionGuid);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                
                bool isMessageDisplayed = false;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isMessageDisplayed = dataReader["SELECTEDCOURSMESSAGE"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["SELECTEDCOURSMESSAGE"]);
                    }
                }

                return isMessageDisplayed;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return true;
            }
        }


        /// <summary>
        /// Thi method fills the Course Approval list with the data from IDataReader 
        /// </summary>
        /// <param name="dataReader">IDataReader datareader</param>
        /// <param name="glossaryItems">List CourseApproval object</param>
        private void FillCourseApprovalEntityList(IDataReader dataReader, List<CourseApproval> courseapprovals)
        {
            CourseApproval courseApproval=null;
            int iCount = 1;
            int approvalID = 0;
            while (dataReader.Read())
            {
                if (approvalID != Convert.ToInt32(dataReader["CourseApprovalID"]))
                {
                    courseApproval = new CourseApproval();
                    courseApproval.CourseApprovalID = Convert.ToInt32(dataReader["CourseApprovalID"]);
                    approvalID = courseApproval.CourseApprovalID;
                    courseApproval.CourseApprovalCode = iCount.ToString() + "-" + Convert.ToInt32(dataReader["CourseApprovalID"]).ToString();
                    courseApproval.HoldingRegulator = dataReader["HoldingRegulator"].ToString();
                    courseApproval.CreditType = dataReader["CREDITTYPE"].ToString();
                    courseApproval.ApprovedCourseHour = Convert.ToDouble(dataReader["APPROVEDCREDITHOURS"]);
                    courseApproval.CourseName = dataReader["COURSE_NAME"].ToString();
                    courseApproval.CredentialName = dataReader["CREDENTIALNAME"].ToString();
                    courseapprovals.Add(courseApproval);
                }
                else
                {
                    if (!courseApproval.CreditType.ToString().Contains(dataReader["CREDITTYPE"].ToString())) 
                    {
                        courseApproval.CreditType = courseApproval.CreditType + ",&nbsp;" + dataReader["CREDITTYPE"].ToString(); 
                    }

                    if (!courseApproval.CredentialName.ToString().Contains(dataReader["CREDENTIALNAME"].ToString()))
                    {
                        courseApproval.CredentialName = courseApproval.CredentialName + ",&nbsp;" + dataReader["CREDENTIALNAME"].ToString();
                    }
                }
                iCount++;
                
            }
        }

        #endregion


        public bool GetQuizAssessmentItems(int contentObjectID)
        {

            DbCommand dbCommand = null;

            try
            {
                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_CONTENTOBJECT_ASSESSMENTITEMS_COUNT);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);
                db.AddOutParameter(dbCommand, "@COUNT_ASSESSMENTITEM", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);
                if (dbCommand.Parameters["@COUNT_ASSESSMENTITEM"].Value != DBNull.Value)
                {
                    int COUNT_ASSESSMENTITEM = Convert.ToInt32(dbCommand.Parameters["@COUNT_ASSESSMENTITEM"].Value);
                    if (COUNT_ASSESSMENTITEM == 0)
                        return false;
                }

                return true;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return false;
            }
        }

      
        #endregion

        #region Sub Content Owner
        /// <summary>
        /// This method gets the Original Course ID into Sub Content Owner
        /// </summary>
        /// <param name="courseID">int courseid</param>        
        /// <returns>int</returns>
        public int GetOriginalCourseIDFromSubContentOwner(int offerCourseID)
        {
            DbCommand dbCommand = null;
            try
            {
                int originalCourseID = 0;
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_ID_FROM_SUB_CONTENTOWNER);
                db.AddInParameter(dbCommand, "@OFFER_COURSE_ID", DbType.Int32, offerCourseID);
                db.AddOutParameter(dbCommand, "ORIGINAL_COURSE_ID", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);
                if (dbCommand.Parameters["@ORIGINAL_COURSE_ID"].Value != DBNull.Value)
                {
                    originalCourseID = Convert.ToInt32(dbCommand.Parameters["@ORIGINAL_COURSE_ID"].Value);
                    
                }
                return originalCourseID;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return 0;
            }
        }
        #endregion

		//LCMS-10392
        public string GetCourseKeywords(int courseID)
        {

            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_COURSE_KEYWORDS);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                string courseKeywords = null;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dataReader.Read();
                    courseKeywords = dataReader["KEYWORDS"] == DBNull.Value ? "" : Convert.ToString(dataReader["KEYWORDS"]);
                }

                return courseKeywords;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        #region DocuSign LCMS-11217

        public DocuSignLearner GetLearnerData(int CourseID, string LearnerSessionID, int LearnerID, int EnrollmentID)
        {
            DbCommand dbCommand = null;
            
            DocuSignLearner docuSignLearnerData = new DocuSignLearner();
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_DOCUSIGN_LEARNER_DATA);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseID);
                db.AddInParameter(dbCommand, "@EnrollmentID", DbType.Int32, EnrollmentID);
                db.AddInParameter(dbCommand, "@LearnerID", DbType.Int64, LearnerID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {

                    while (dataReader.Read())
                    {

                        docuSignLearnerData.CourseName = dataReader["COURSENAME"] == DBNull.Value ? string.Empty : dataReader["COURSENAME"].ToString();    
                    
                        docuSignLearnerData.CreditHours = dataReader["CREDITHOUR"] == DBNull.Value ? string.Empty : dataReader["CREDITHOUR"].ToString();

                        docuSignLearnerData.TemplateId = dataReader["TEMPLATE_GUID"] == DBNull.Value ? string.Empty : dataReader["TEMPLATE_GUID"].ToString();
                        
                        docuSignLearnerData.LearnerName= dataReader["USERNAME"] == DBNull.Value ? string.Empty : dataReader["USERNAME"].ToString();
                       
                        docuSignLearnerData.LearnerEmail = dataReader["EMAILADDRESS"] == DBNull.Value ? string.Empty : dataReader["EMAILADDRESS"].ToString();

                        docuSignLearnerData.CertificateNumber = dataReader["CERTIFICATENUMBER"] == DBNull.Value ? string.Empty : dataReader["CERTIFICATENUMBER"].ToString();
                        
                        docuSignLearnerData.StartDate =  dataReader["STARTDATE"] == DBNull.Value ? string.Empty : dataReader["STARTDATE"].ToString();

                        docuSignLearnerData.RegistrationDate = dataReader["REGISTRATIONDATE"] == DBNull.Value ? string.Empty : dataReader["REGISTRATIONDATE"].ToString();

                        docuSignLearnerData.ApprovalNumber = dataReader["COURSEAPPROVALNUMBER"] == DBNull.Value ? string.Empty : dataReader["COURSEAPPROVALNUMBER"].ToString();

                        docuSignLearnerData.ApprovedCourseName = dataReader["APPROVEDCOURSENAME"] == DBNull.Value ? string.Empty : dataReader["APPROVEDCOURSENAME"].ToString();
                                                
                        docuSignLearnerData.ApprovedCreditHours = dataReader["APPROVEDCREDITHOURS"] == DBNull.Value ? string.Empty : dataReader["APPROVEDCREDITHOURS"].ToString();

                        docuSignLearnerData.LastPostTestDate = dataReader["LASTPOSTTESTDATE"] == DBNull.Value ? string.Empty : dataReader["LASTPOSTTESTDATE"].ToString();
                                                
                        docuSignLearnerData.FinalExamScore = dataReader["FINALEXAMSCORE"] == DBNull.Value ? string.Empty : dataReader["FINALEXAMSCORE"].ToString();
                    }

                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            
                            if (CheckFieldInDataReader(dataReader, "Date of Birth") == true) docuSignLearnerData.DateofBirth = dataReader["Date of Birth"] == DBNull.Value ? string.Empty : dataReader["Date of Birth"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "License Number") == true) docuSignLearnerData.LicenseNumber = dataReader["License Number"] == DBNull.Value ? string.Empty : dataReader["License Number"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "License Type") == true) docuSignLearnerData.LicenseType = dataReader["License Type"] == DBNull.Value ? string.Empty : dataReader["License Type"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "National Producer Number (NPN)") == true) docuSignLearnerData.NationProducerNumber = dataReader["National Producer Number (NPN)"] == DBNull.Value ? string.Empty : dataReader["National Producer Number (NPN)"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "NERC Certification Number") == true) docuSignLearnerData.NERCCertificateNumber = dataReader["NERC Certification Number"] == DBNull.Value ? string.Empty : dataReader["NERC Certification Number"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "Social Security Number") == true) docuSignLearnerData.SocialSecurityNumber = dataReader["Social Security Number"] == DBNull.Value ? string.Empty : dataReader["Social Security Number"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "License Expiration Date") == true) docuSignLearnerData.LicenseExpirationDate = dataReader["License Expiration Date"] == DBNull.Value ? string.Empty : dataReader["License Expiration Date"].ToString();
                           
                            if (CheckFieldInDataReader(dataReader, "City") == true) docuSignLearnerData.City = dataReader["City"] == DBNull.Value ? string.Empty : dataReader["City"].ToString();
                                                   
                            if (CheckFieldInDataReader(dataReader, "State") == true) docuSignLearnerData.State = dataReader["State"] == DBNull.Value ? string.Empty : dataReader["State"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "Zip Code") == true) docuSignLearnerData.ZipCode = dataReader["Zip Code"] == DBNull.Value ? string.Empty : dataReader["Zip Code"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "Telephone Number") == true) docuSignLearnerData.Phone = dataReader["Telephone Number"] == DBNull.Value ? string.Empty : dataReader["Telephone Number"].ToString();
                            
                            if (CheckFieldInDataReader(dataReader, "Street Address") == true) docuSignLearnerData.Address = dataReader["Street Address"] == DBNull.Value ? string.Empty : dataReader["Street Address"].ToString();
                        
                        }
                    }
                }
                return docuSignLearnerData;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return docuSignLearnerData;
            }

        }

        public Boolean CheckFieldInDataReader(IDataReader dataReader, String sFieldName)
        {
            Boolean bReturn = false;
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetName(i).Equals(sFieldName, StringComparison.InvariantCultureIgnoreCase))
                {
                    bReturn = true;
                    break;
                }
            }
            return bReturn;

        }
           
        public int SaveEnvelopeId(int EnrollmentID, string EnvelopeId)
        {
            DbCommand dbCommand = null;
            int effectedRowsCount = 0;
            try
            {
                
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SAVE_ENVELOPE_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);
                db.AddInParameter(dbCommand, "@ENVELOPE_ID", DbType.String, EnvelopeId);

                effectedRowsCount = db.ExecuteNonQuery(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                
            }
            return effectedRowsCount;
        }

        public string GetEnvelopeId(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            string envelopeId = string.Empty;
            try
            {
                
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_ENVELOPE_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);
                

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        envelopeId = dataReader["EVELOPE_ID"] == DBNull.Value ? string.Empty : dataReader["EVELOPE_ID"].ToString();
                        

                    }
                }
                return envelopeId;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return envelopeId;
        }

        public int SaveStatusAfterDocuSignProcessComplete(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            int effectedRowsCount = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETE);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);
                
                effectedRowsCount = db.ExecuteNonQuery(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return effectedRowsCount;
        }

        public int SaveStatusAfterDocuSignProcessComplete(string EnvelopeId)
        {
            DbCommand dbCommand = null;
            int enrollmentId = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SET_STATUS_AFTER_DOCUSIGN_PROCESS_COMPLETED);
                db.AddInParameter(dbCommand, "@EnvelopeId", DbType.String, EnvelopeId);
                db.AddOutParameter(dbCommand, "@EnrollId", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);

                enrollmentId = Convert.ToInt32(dbCommand.Parameters["@EnrollId"].Value);


                 
                return enrollmentId;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return enrollmentId;
        }

        public bool GetDocuSignedAffidavitStatus(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            bool docuSignedAffidavitStatus = false;

            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_LEARNERCOURSESTATUS_BY_ENROLLMENT_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);


                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        docuSignedAffidavitStatus = dataReader["DocuSignedAffidavitStatus"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["DocuSignedAffidavitStatus"]);


                    }
                }
                return docuSignedAffidavitStatus;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return docuSignedAffidavitStatus;
        }

        public string GetCourseStatusByEnrollmentId(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            string courseStatus = string.Empty;

            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_CORUSE_STATUS_BY_LEARNERENROLLMENT_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);


                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        courseStatus = dataReader["STATUS"] == DBNull.Value ? string.Empty : dataReader["STATUS"].ToString();


                    }
                }
                return courseStatus;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return courseStatus;
        }

        public int SaveAffidavitAcknowledgmentStatus(bool courseApprovalAffidavitStatus, int EnrollmentID)
        {
            DbCommand dbCommand = null;
            int effectedRowsCount = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SET_CORUSE_APPROVAL_AFFIDAVIT_STATUS_BY_LEARNERENROLLMENT_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);
                db.AddInParameter(dbCommand, "COURSE_APPROVAL_AFFIDAVIT_STATUS", DbType.Boolean, courseApprovalAffidavitStatus);

                effectedRowsCount = db.ExecuteNonQuery(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return effectedRowsCount;
        }
        
        public bool GetAffidavitAcknowledgmentStatus(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            bool docuSignAffidavitAcknowledgmentStatus = false;

            try
            {

                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_AFFIDAVIT_ACKNOWLDGEMENT_BY_LEARNERENROLLMENT_ID);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);


                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        docuSignAffidavitAcknowledgmentStatus = dataReader["AffidavitAcknowledgmentStatus"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["AffidavitAcknowledgmentStatus"]);


                    }
                }
                return docuSignAffidavitAcknowledgmentStatus;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return docuSignAffidavitAcknowledgmentStatus;
        }

        public CourseInfo GetCourseInformation(int EnrollmentID)
        {
            DbCommand dbCommand = null;

            CourseInfo courseInformation = new CourseInfo();
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_COURSEINFORMATION_BY_ENROLLMENT_ID);
                db.AddInParameter(dbCommand, "@EnrollmentID", DbType.Int32, EnrollmentID);
                db.AddOutParameter(dbCommand, "@COURSE_ID", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSECONFIGTEMPLATE_ID", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@COURSEAPPROVAL_ID", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@LEARNER_ID", DbType.Int32, 4);
                db.AddOutParameter(dbCommand, "@LEARNINGSESSIONGUID", DbType.String, 255);

                db.ExecuteNonQuery(dbCommand);

                courseInformation.CourseConfigId = Convert.ToInt32(dbCommand.Parameters["@COURSECONFIGTEMPLATE_ID"].Value);

                courseInformation.CourseApprovalId = Convert.ToInt32(dbCommand.Parameters["@COURSEAPPROVAL_ID"].Value);

                courseInformation.CourseId = Convert.ToInt32(dbCommand.Parameters["@COURSE_ID"].Value);

                courseInformation.LearnerId = Convert.ToInt32(dbCommand.Parameters["@LEARNER_ID"].Value);

                courseInformation.LearnerSessionGuid = dbCommand.Parameters["@LEARNINGSESSIONGUID"].Value.ToString();

                return courseInformation;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return courseInformation;
            }

        }

        #endregion

        //Suggested Course Panel LCMS-11878
        public List<BusinessEntities.SuggestedCourse> GetCourseNameAgainstCourseGuids(List<string> courseGuids)
        {

            DbCommand dbCommand = null;
            try
            {
             
                List<BusinessEntities.SuggestedCourse> CourseNameList = null;

                CourseNameList = new List<BusinessEntities.SuggestedCourse>();

                string listCN = string.Empty; // Initialize a string to hold the comma-delimited data as empty

                for (int i = 0; i <= courseGuids.Count - 1 ; i++)
                {
                    if (listCN.Length > 0)
                    {
                        listCN += ","; // Add a comma if data already exists
                    }
                    
                    listCN += courseGuids[i];                
                
                }


                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_COURSE_NAME_COURSEGUIDS);


                db.AddInParameter(dbCommand, "@COURSEGUIDS", DbType.String, listCN);
                           
                              
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        SuggestedCourse suggestedCourse = new SuggestedCourse();

                        suggestedCourse.CourseName = dataReader["NAME"] == DBNull.Value ? string.Empty : dataReader["NAME"].ToString();
                        suggestedCourse.CourseGuid = dataReader["GUID"] == DBNull.Value ? string.Empty : dataReader["GUID"].ToString();

                        CourseNameList.Add(suggestedCourse); 
                    }                   
                }
                return CourseNameList;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

       public string GetCourseStoreId(int courseID)
        {

            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_COURSE_STOREID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                string courseStoreId = null;
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dataReader.Read();
                    courseStoreId = dataReader["STOREID"] == DBNull.Value ? "" : Convert.ToString(dataReader["STOREID"]);
                }

                return courseStoreId;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

       //LCMS-11974 DocuSign Decline
       //Abdus Samad 
       //Start
       public int SaveStatusAfterDocuSignDecline(int EnrollmentID)
        {
            DbCommand dbCommand = null;
            int effectedRowsCount = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SET_STATUS_AFTER_DOCUSIGN_DECLINE);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, EnrollmentID);
                
                effectedRowsCount = db.ExecuteNonQuery(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

            }
            return effectedRowsCount;
        }
       //Stop

       public int GetEnrollmentIdAgainstEnvelopeId(string EnvelopeId)
       {
           DbCommand dbCommand = null;
           int enrollmentId = 0;
           try
           {
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_GET_ENROLLMENTID_AGAINST_ENVELOPID);
               db.AddInParameter(dbCommand, "@EnvelopeId", DbType.String, EnvelopeId);
               db.AddOutParameter(dbCommand, "@EnrollId", DbType.Int32, 4);
               db.ExecuteNonQuery(dbCommand);

               enrollmentId = Convert.ToInt32(dbCommand.Parameters["@EnrollId"].Value);



               return enrollmentId;
           }

           catch (Exception exp)
           {
               ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

           }
           return enrollmentId;
       }

       public bool SelectCourseApprovalMessage(string CourseID, string LearnerID, string CourseApprovalID, string learnerSessionGUID)
       {
           DbCommand dbCommand = null;
           bool courseApprovalMessage = false;
           try
           {
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_COURSE_COURSEAPPROVALMESSAGE);
               db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseID);
               db.AddInParameter(dbCommand, "@LEARNER_ID", DbType.Int32, LearnerID);
               db.AddInParameter(dbCommand, "@COURSEAPPROVAL_ID", DbType.Int32, CourseApprovalID);
               db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionGUID);

               db.AddOutParameter(dbCommand, "@IS_NEWAPPROVAL", DbType.Boolean, 50);
               db.ExecuteNonQuery(dbCommand);

               courseApprovalMessage = Convert.ToBoolean(dbCommand.Parameters["@IS_NEWAPPROVAL"].Value);

               return courseApprovalMessage;
           }

           catch (Exception exp)
           {
               ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");

           }
           return courseApprovalMessage;
       }

       //LCMS-13475
       //Abdus Samad
       //Start
       public bool GetMultipleQuizConfigurationCount(int courseID)
       {

           DbCommand dbCommand = null;

           try
           {
               //This SP gets quiz assessmentItems
               dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_CHECK_COURSE_MULTIPLEQUIZCONFIGURATION);
               db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
               db.AddOutParameter(dbCommand, "@COUNT_ASSESSMENTITEM", DbType.Int32, 4);
               db.ExecuteNonQuery(dbCommand);
               if (dbCommand.Parameters["@COUNT_ASSESSMENTITEM"].Value != DBNull.Value)
               {
                   int COUNT_ASSESSMENTITEM = Convert.ToInt32(dbCommand.Parameters["@COUNT_ASSESSMENTITEM"].Value);
                   if (COUNT_ASSESSMENTITEM == 0)
                       return false;
               }

               return true;
           }

           catch (Exception exp)
           {
               ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
               return false;
           }
       }
        //Stop

      

    }
}
