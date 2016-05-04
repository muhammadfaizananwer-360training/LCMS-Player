using System;
using System.Web;
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
using _360Training.AssessmentServiceDataLogic.Common;


namespace _360Training.AssessmentServiceDataLogic.AssessmentDA
{
    public class AssesmentDA:IAssessmentDA,IDisposable
    {
        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        public AssesmentDA()
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

        #region IAssessmentDAMembers
        /// <summary>
        /// This method returns the list of assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetPreAssessmentAssessmentItems(int courseID, bool use_Individual_AssessmentItem)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems=new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_PREASSESSMENT_ASSESSMENTITEM);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@USE_INDIVIDUAL_ASSESSMENTITEM", DbType.Boolean, use_Individual_AssessmentItem);

                result = db.ExecuteDataSet(dbCommand);

                foreach(DataRow row in result.Tables[0].Rows)
                {
                   item = new AssessmentItem();
                   string type = Convert.ToString(row["QUESTIONTYPE"]);
                   item = GetAssessmentItemEntity(row);
                   assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                   item.AssessmentAnswers = assessmentItemAnswers;
                   assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }




        // LCMS-9213
        public List<AssessmentItem> GetAssessmentItemsByGUIDs(string assessmentItemGuids)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand("ICP_GETASSESSMENTITEMS_BY_GUIDS");
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_GUIDS", DbType.String, assessmentItemGuids);                

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }







        /// <summary>
        /// This method returns the list of assessmentitems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem object</returns>
        public List<AssessmentItem> GetExamAssessmentItems(int courseID, bool use_Individual_AssessmentItem,int examID)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_COURSE_EXAM_ASSESSMENTITEM);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@USE_INDIVIDUAL_ASSESSMENTITEM", DbType.Boolean, use_Individual_AssessmentItem);
                db.AddInParameter(dbCommand, "@EXAM_ID", DbType.Int32, examID);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        /// <summary>
        /// This method returns the two lists of Assessment Item Banks i.e.
        /// 1) All the banks bound to the Assessment Configuration
        /// 2) Previously asked Banks
        /// </summary>
        /// <param name="learnerSessionID">int learnerSessionID</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>
        /// <returns>DataSet contianing two datatables</returns>
        public DataSet GetAssessmentItemBanksForRandomAlternate(string learnerSessionID, AssessmentConfiguration assessmentConfig)
        {

            DbCommand dbCommand = null;
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMBANKS_FOR_RANDOMALTERNATE);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionID);
                db.AddInParameter(dbCommand, "@ASSESSMENTCONFIGURATION_ID", DbType.Int32, assessmentConfig.ID);
                db.AddInParameter(dbCommand, "@ASKEDBANKS", DbType.String, System.DBNull.Value);
                return db.ExecuteDataSet(dbCommand); 
            }

            catch (Exception exp)
            {               
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        /// <summary>
        /// This method returns all the banks bound to the Assessment Configuration        
        /// </summary>
        /// <param name="askedBanks">string askedBanks</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>
        /// <returns>DataSet</returns>
        public DataSet GetAssessmentItemsForRandomAlternateInPreviewMode(string askedBanks, AssessmentConfiguration assessmentConfig)
        {

            DbCommand dbCommand = null;
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMBANKS_FOR_RANDOMALTERNATE);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, null);
                db.AddInParameter(dbCommand, "@ASSESSMENTCONFIGURATION_ID", DbType.Int32, assessmentConfig.ID);
                db.AddInParameter(dbCommand, "@ASKEDBANKS", DbType.String, askedBanks);
                
                        
                return db.ExecuteDataSet(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        /// <summary>
        /// This method returns the two lists of Tests i.e.
        /// 1) All the tests bound to the Assessment Configuration
        /// 2) Previously asked tests
        /// </summary>
        /// <param name="learnerSessionID">int learnerSessionID</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>
        /// <returns>DataSet contianing two datatables</returns>
        public DataSet GetTestForRandomAlternate(string learnerSessionID, AssessmentConfiguration assessmentConfig)
        {

            DbCommand dbCommand = null;
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMBANKS_FOR_RANDOMALTERNATEMULTIPLEITEMBANK);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, learnerSessionID);
                db.AddInParameter(dbCommand, "@ASSESSMENTCONFIGURATION_ID", DbType.Int32, assessmentConfig.ID);
                db.AddInParameter(dbCommand, "@ASKEDTESTS", DbType.String, System.DBNull.Value);
                return db.ExecuteDataSet(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }


        /// <summary>
        /// This method returns all the banks bound to the Assessment Configuration        
        /// </summary>
        /// <param name="askedBanks">string askedBanks</param>
        /// <param name="assessmentConfig">AssessmentConfiguration assessmentConfig</param>
        /// <returns>DataSet</returns>
        public DataSet GetTestForRandomAlternateMultipleItemBanksInPreviewMode(string askedTests, AssessmentConfiguration assessmentConfig)
        {

            DbCommand dbCommand = null;
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMBANKS_FOR_RANDOMALTERNATEMULTIPLEITEMBANK);
                db.AddInParameter(dbCommand, "@LEARNINGSESSION_GUID", DbType.String, null);
                db.AddInParameter(dbCommand, "@ASSESSMENTCONFIGURATION_ID", DbType.Int32, assessmentConfig.ID);
                db.AddInParameter(dbCommand, "@ASKEDTESTS", DbType.String, askedTests);


                return db.ExecuteDataSet(dbCommand);
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        /// <summary>
        /// This method returns the list of assessmentItems against asessment item bank ID
        /// </summary>
        /// <param name="assessmentBankID">int assessmentBankID</param>
        /// <returns>List of assessmentItem objects</returns>
        public List<AssessmentItem> GetAssessmentItemsByAssessmentBankID(int assessmentBankID)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMS_BY_ASSESSMENTITEMBANKID);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEMBANK_ID", DbType.Int32, assessmentBankID);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        // LCMS-9882
        /// <summary>
        /// This method returns the list of assessmentItems against asessment item bank IDs (comma separated)
        /// </summary>
        /// <param name="assessmentBankID">string assessmentBankIDs</param>
        /// <returns>List of assessmentItem objects</returns>
        public List<AssessmentItemBank> GetAssessmentItemsByAssessmentBankIDs(string assessmentBankIDs)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;

            List<AssessmentItemBank> assessmentItemBanks = new List<AssessmentItemBank>();
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {






                // Sorting by parameter values
                //------------------------------------------------------------
                string[] bankIds = assessmentBankIDs.Trim().Split(',');
                DataTable dt = null;// result.Tables[0].Clone();

                for (int i = 0; i < bankIds.Length; i++)
                {
                    //This SP gets assessmentItems
                    dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMS_BY_ASSESSMENTITEMBANKIDS);
                    db.AddInParameter(dbCommand, "@ASSESSMENTITEMBANK_IDS", DbType.String, bankIds[i].ToString());

                    result = db.ExecuteDataSet(dbCommand);
                    if (dt == null)
                        dt = result.Tables[0].Clone();

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        DataRow r = dt.NewRow();
                        r.ItemArray = row.ItemArray;
                        r["ASSESSMENTITEMBANK_ID"] = bankIds[i];
                        dt.Rows.Add(r);
                    }
                }
                //------------------------------------------------------------

                AssessmentItemBank currentBank = new AssessmentItemBank();
                int lastBankID = 0;

                //foreach (DataRow row in result.Tables[0].Rows)
                foreach (DataRow row in dt.Rows)
                {
                    int currentBankId = Convert.ToInt32(row["ASSESSMENTITEMBANK_ID"]);


                    if (lastBankID == 0)
                    {
                        lastBankID = currentBankId;
                    }

                    if (currentBankId != lastBankID)
                    {
                        currentBank.AssessmentItems = assessmentItems;
                        assessmentItemBanks.Add(currentBank);
                        assessmentItems = new List<AssessmentItem>();
                        currentBank = new AssessmentItemBank();
                        lastBankID = currentBankId;
                    }

                    currentBank.AssessmentBankID = currentBankId;
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);

                }


                // to enter the last bank's info
                //------------------------------------------------
                currentBank.AssessmentItems = assessmentItems;
                assessmentItemBanks.Add(currentBank);
                //------------------------------------------------

                return assessmentItemBanks;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        /// <summary>
        /// This method returns the list of assessmentItems against test ID
        /// </summary>
        /// <param name="assessmentBankID">int testID</param>
        /// <returns>List of assessmentItem objects</returns>
        public List<AssessmentItem> GetAssessmentItemsByTestID(int testID)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMS_BY_TESTID);
                db.AddInParameter(dbCommand, "@TEST_ID", DbType.Int32, testID);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);                    
                    item.TestID = Convert.ToInt32(row["TESTID"].ToString());
                    item.AssessmentBinderID = Convert.ToInt32(row["ASSESSMENTBINDERID"].ToString());
                    item.AssessmentBinderName = Convert.ToString(row["ASSESSMENTBINDERNAME"]);                  
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }




        /// <summary>
        /// This method returns the list of post assessmentItems
        /// </summary>
        /// <param name="courseID">int courseID</param>
        /// <returns>List of assessmentItem objects</returns>
        public List<AssessmentItem> GetPostAssessmentAssessmentItems(int courseID, bool use_Individual_AssessmentItem)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_COURSE_POSTASSESSMENT_ASSESSMENTITEM);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                db.AddInParameter(dbCommand, "@USE_INDIVIDUAL_ASSESSMENTITEM", DbType.Boolean, use_Individual_AssessmentItem);

                result = db.ExecuteDataSet(dbCommand);
                
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method returns the list of quiz assessmentItems
        /// </summary>
        /// <param name="contentObjectID">int contentObjectID</param>
        /// <returns>list of assessmentItem object</returns>
        public List<AssessmentItem> GetQuizAssessmentItems(int contentObjectID, bool use_Individual_AssessmentItem)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets quiz assessmentItems
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_CONTENTOBJECT_ASSESSMENTITEMS);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, contentObjectID);
                db.AddInParameter(dbCommand, "@USE_INDIVIDUAL_ASSESSMENTITEM", DbType.Boolean, use_Individual_AssessmentItem);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method is used to get assessment item answers list
        /// </summary>
        /// <param name="assessmentItemID">assessment item Id</param>
        /// <param name="questionType">question type</param>
        /// <returns>returns list of assessment item answer</returns>
        private List<AssessmentItemAnswer> GetAssessmentItemAnswers(int assessmentItemID, string questionType)
        {
            List<AssessmentItemAnswer> answers = new List<AssessmentItemAnswer>();
            try
            {
                DataSet result = null;
                DbCommand command = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEM_ASSESSMENTITEMANSWER);

                db.AddInParameter(command, "@ASSESSMENTITEM_ID", DbType.Int32, assessmentItemID);
                result = db.ExecuteDataSet(command);

                AssessmentItemAnswer answer = null;
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    answer = GetAssessmentItemAnswerEntity(row, questionType); 
                    answers.Add(answer);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return answers;

        }
        /// <summary>
        /// This method is used to get <c>AssessmentItemAnswer</c> object
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="questionType">question type</param>
        /// <returns>returns an <c>AssessmentItemAnswer</c> object</returns>
        private AssessmentItemAnswer GetAssessmentItemAnswerEntity(DataRow row, string questionType)
        {
            AssessmentItemAnswer answer = null;
            List<ImageTargetCoordinate> coordinates = new List<ImageTargetCoordinate>();
            try
            {
                answer = GetAssessmentItemAnswerForType(questionType);
                answer.AssessmentItemAnswerID = Convert.ToInt32(row["ID"]); ;
                answer.Label = HttpUtility.HtmlDecode(Convert.ToString(row["LABEL"]));
                answer.Label = answer.Label.Replace("<p>", "");
                answer.Label = answer.Label.Replace("</p>", ""); 
                answer.Value = Convert.ToString(row["VALUE"]);
                answer.IsCorrect = Convert.ToBoolean(row["ISCORRECTTF"]);
                answer.DisplayOrder = Convert.ToInt32(row["DISPLAYORDER"]);
                answer.AssessmentItemAnswerGuid = row["ASSESSMENTITEMANSWER_GUID"].ToString();
                answer.Feedback = row["FEEDBACK"] == DBNull.Value ? string.Empty : row["FEEDBACK"].ToString();
                answer.Correctfeedback = row["CORRECTFEEDBACK"] == DBNull.Value ? string.Empty : row["CORRECTFEEDBACK"].ToString();
                answer.Incorrectfeedback = row["INCORRECTFEEDBACK"] == DBNull.Value ? string.Empty : row["INCORRECTFEEDBACK"].ToString();
                answer.Usedefaultfeedbacktf = row["USEDEFAULTFEEDBACKTF"] == DBNull.Value ? false : Convert.ToBoolean(row["USEDEFAULTFEEDBACKTF"]);
                // Optional parameters based on question type
                switch (questionType)
                {
                    case QuestionType.Matching:
                        ((MatchingAssessmentItemAnswer)answer).RightItemText =
                        row["RIGHTITEMTEXT"] == DBNull.Value ? "" : Convert.ToString(row["RIGHTITEMTEXT"]);
                        ((MatchingAssessmentItemAnswer)answer).RightItemOrder = 
                        row["RIGHTITEMORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["RIGHTITEMORDER"]);
                        ((MatchingAssessmentItemAnswer)answer).LeftItemText = 
                        row["LEFTITEMTEXT"] == DBNull.Value ? "" : Convert.ToString(row["LEFTITEMTEXT"]);
                        ((MatchingAssessmentItemAnswer)answer).LeftItemOrder = 
                        row["LEFTITEMORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["LEFTITEMORDER"]);
                        break;
                    case QuestionType.Ordering:
                        ((OrderingAssessmentItemAnswer)answer).CorrectOrder =
                        row["CORRECTORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["CORRECTORDER"]);
                        break;
                }

                // Add Image Target Coordinates
                if (questionType == QuestionType.ImageTarget)
                {
                    coordinates = GetAssessmentItemAnswersImageTargetCoordinates(answer.AssessmentItemAnswerID);
                    ((ImageTargetAssessmentItemAnswer)answer).ImageTargetCoordinates = coordinates;
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return answer;
        }
        /// <summary>
        /// This method is used to create child assessment item answer objects
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private AssessmentItemAnswer GetAssessmentItemAnswerForType(string type)
        {
            AssessmentItemAnswer answer = null;

            try
            {
                switch (type)
                {
                    case QuestionType.Matching:
                        {
                            answer = new MatchingAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.TextInputFITB:
                    case QuestionType.NumericInputFITB:
                        {
                            answer = new FITBAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.Ordering:
                        {
                            answer = new OrderingAssessmentItemAnswer();
                        }
                        break;
                    case QuestionType.ImageTarget:
                        {
                            answer = new ImageTargetAssessmentItemAnswer();
                        }
                        break;
                    default:
                        {
                            answer = new AssessmentItemAnswer();
                        }

                        break;

                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return answer;
            
        }
        /// <summary>
        /// This method is used to get Assessment item answer image target coordinates
        /// </summary>
        /// <param name="answerID">answer Id</param>
        /// <returns>returns list of image target coordinates for answer</returns>
        private List<ImageTargetCoordinate> GetAssessmentItemAnswersImageTargetCoordinates(int answerID)
        {
            List<ImageTargetCoordinate> coordinates = new List<ImageTargetCoordinate>();
            try
            {
                DataSet result = null;
                DbCommand command = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEMANSWER_IMAGETARGETCOORDINATE);

                db.AddInParameter(command, "@ASSESSMENTITEMANSWER_ID", DbType.Int32, answerID);
                result = db.ExecuteDataSet(command);

                ImageTargetCoordinate coordinate = null;
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    coordinate = GetImageTargetCoordinateEntity(row);
                    coordinates.Add(coordinate);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return coordinates;
        }
        /// <summary>
        /// This method is used to get image target coordinate
        /// </summary>
        /// <param name="row">date row</param>
        /// <returns>returns image target coordinate</returns>
        private ImageTargetCoordinate GetImageTargetCoordinateEntity(DataRow row)
        {
            ImageTargetCoordinate coordinate = null;
            try
            {
                coordinate = new ImageTargetCoordinate();
                coordinate.XPos = Convert.ToInt32(row["XPOS"]);
                coordinate.YPos = Convert.ToInt32(row["YPOS"]); ;
                coordinate.Width = Convert.ToInt32(row["WIDTH"]); ;
                coordinate.Height = Convert.ToInt32(row["HEIGHT"]); ;
                coordinate.CoordinateOrder = Convert.ToInt32(row["COORDINATEORDER"]); ;
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return coordinate;
        }
        /// <summary>
        /// This method is used to get <c>AssessmentItem</c> object
        /// </summary>
        /// <param name="row">data row</param>
        /// <returns>returns <c>AssessmentItem</c> object</returns>
        private AssessmentItem GetAssessmentItemEntity(DataRow row)
        {
            AssessmentItem item = null;
            try
            {
                string type = Convert.ToString(row["QUESTIONTYPE"]);
                item = GetAssessmentItemForType(type);

                item.AssessmentItemID = Convert.ToInt32(row["ID"]);
                if (row.Table.Columns.Contains("LEARNINGOBJECTIVE_ID"))
                    item.AssessmentBinderID = row["LEARNINGOBJECTIVE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["LEARNINGOBJECTIVE_ID"].ToString());
                if (row.Table.Columns.Contains("scoreWeight"))
                    item.ScoreWeight = row["scoreWeight"] == DBNull.Value ? 0 : Convert.ToDouble(row["scoreWeight"].ToString());
                
                if (row.Table.Columns.Contains("EXAMTOPIC_ID"))
                    item.AssessmentBinderID = row["EXAMTOPIC_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["EXAMTOPIC_ID"].ToString());

                if (row.Table.Columns.Contains("EXAMTOPICscoreWeight"))
                    item.ScoreWeight = row["EXAMTOPICscoreWeight"] == DBNull.Value ? 0 : Convert.ToDouble(row["EXAMTOPICscoreWeight"].ToString());

                if (row.Table.Columns.Contains("EXAMTOPICNAME"))
                    item.AssessmentBinderName = row["EXAMTOPICNAME"] == DBNull.Value ? "" : Convert.ToString(row["EXAMTOPICNAME"].ToString());

                item.QuestionStem =  Convert.ToString(row["QUESTIONSTEM"]);
                item.Status = row["STATUS"] == DBNull.Value ? "" : row["STATUS"].ToString();
                item.AssessmentItemGuid = row["ASSESSMENTITEM_GUID"] == DBNull.Value ? "" : row["ASSESSMENTITEM_GUID"].ToString();
                item.QuestionType = type;
                item.Feedback = row["FEEDBACK"] == DBNull.Value ? "" : row["FEEDBACK"].ToString();
                item.Correctfeedback = row["CORRECTFEEDBACK"] == DBNull.Value ? "" : row["CORRECTFEEDBACK"].ToString();
                item.Incorrectfeedback = row["INCORRECTFEEDBACK"] == DBNull.Value ? "" : row["INCORRECTFEEDBACK"].ToString();
                item.Disablerandomizeanswerchoicetf = row["DISABLERANDOMIZEANSWERCHOICETF"] == DBNull.Value ? true : Convert.ToBoolean(row["DISABLERANDOMIZEANSWERCHOICETF"]);
                item.AssessmentItemTemplateType = row["ASSESSMENTITEMTEMPLATE_NAME"] == DBNull.Value ? "" : row["ASSESSMENTITEMTEMPLATE_NAME"].ToString();
                item.Feedbacktype = row["FEEBACKTYPE"] == DBNull.Value ? "" : row["FEEBACKTYPE"].ToString();
                
                // Optional parameters based on question type
                switch (item.QuestionType)
                {
                    case QuestionType.TextInputFITB:
                    case QuestionType.NumericInputFITB:
                        ((FillInTheBlankQuestion)item).IsAnswerCaseSensitive =
                            row["ISANSWERCASESENSITIVETF"] == DBNull.Value ? false : Convert.ToBoolean(row["ISANSWERCASESENSITIVETF"]); 
                        break;
                    case QuestionType.Rating:
                        ((RatingQuestion)item).HighValueLabel = 
                        row["HIGHVALUELABEL"] == DBNull.Value ? "" : Convert.ToString(row["HIGHVALUELABEL"]); 
                        ((RatingQuestion)item).LowValueLabel = 
                        row["LOWVALUELABEL"] == DBNull.Value ? "" : Convert.ToString(row["LOWVALUELABEL"]); 
                        ((RatingQuestion)item).Rating = 
                        row["RATING"] == DBNull.Value ? 0 : Convert.ToInt32(row["RATING"]); 
                        break;
                    case QuestionType.ImageTarget:
                        int assetID = row["IMAGE_ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IMAGE_ASSET_ID"]); 
                        ((ImageTargetQuestion)item).ImageURL= GetAssetURL(assetID);
                    break;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return item;
        }
        private string GetAssetURL(int assetID)
        {
            DbCommand dbCommand = null;
            try
            {
                //This SP gets the URL of the asset
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSET_LOCATION);
                db.AddInParameter(dbCommand, "@ASSET_ID", DbType.Int32, assetID);

                string url= string.Empty;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        url=dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                    }
                }
                return url;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return string.Empty;
            }
        }
        /// <summary>
        /// This method is used to create child assessment objects
        /// </summary>
        /// <param name="assetType">assessment item type</param>
        /// <returns>returns assessment item</returns>
        private AssessmentItem GetAssessmentItemForType(string type)
        {
            AssessmentItem item = null;

            try
            {
                switch (type)
                {
                    case QuestionType.TrueFalse:
                        {
                            item = new TrueFalseQuestion();
                        }
                        break;
                    case QuestionType.SingleSelectMCQ:
                        {
                            item = (MultipleChoiceQuestion)new SingleSelectQuestion();
                        }
                        break;
                    case QuestionType.MultipleSelectMCQ:
                        {
                            item = (MultipleChoiceQuestion)new MultipleSelectQuestion();
                        }
                        break;
                    case QuestionType.Matching:
                        {
                            item = new MatchingQuestion();
                        }
                        break;
                    case QuestionType.TextInputFITB:
                        {
                            item = (FillInTheBlankQuestion)new TextInputQuestion();
                        }
                        break;
                    case QuestionType.NumericInputFITB:
                        {
                            item = (FillInTheBlankQuestion)new NumericInputQuestion();
                        }
                        break;
                    case QuestionType.Ordering:
                        {
                            item = new OrderingQuestion();
                        }
                        break;
                    case QuestionType.ImageTarget:
                        {
                            item = new ImageTargetQuestion();
                        }
                        break;
                    case QuestionType.Rating:
                        {
                            item = new RatingQuestion();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                ExceptionPolicyForLCMS.HandleException(ex, "Exception Policy");
            }
            return item;
        }
        /// <summary>
        /// This method returns the sceneGUIDs for the given AssessmentItemID
        /// </summary>
        /// <param name="assessmentItemGUID">string assessmentItemGUID</param>
        /// <returns>string sceneGUID</returns>
        public List<string> GetAssessmentItemScene(string assessmentItemGUID,int courseID)
        {
            DbCommand dbCommand = null;
            List<string> sceneGUIDs = new List<string>();
            try
            {
                //This SP gets the sceneGUID for the given assessmentItemGUID
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEM_SCENE);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_GUID", DbType.String, assessmentItemGUID);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);


                string sceneGUID = string.Empty;

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        sceneGUID = dataReader["SCENE_GUID"] == DBNull.Value ? "" : dataReader["SCENE_GUID"].ToString();
                        sceneGUIDs.Add(sceneGUID);
                    }
                }
                return sceneGUIDs;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        /// <summary>
        /// This method gets the assessmentitem template by assessmenttiemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>AssessmentItemTemplate object</returns>
        public AssessmentItemTemplate GetAssessmentItemAssessmentItemTemplate(int assessmentItemID)
        {
            DbCommand dbCommand = null;
            AssessmentItemTemplate assessmentItemTemplate = new AssessmentItemTemplate();
            try
            {
                //This SP gets assessmenttiemtemplate by assessmentitem
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEM_ASSESSMENTITEMTEMPLATE);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.Int32, assessmentItemID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assessmentItemTemplate.Name = dataReader["NAME"] == DBNull.Value ? "" : dataReader["NAME"].ToString();
                        assessmentItemTemplate.TemplateHTML= dataReader["TEMPLATEHTML"] == DBNull.Value ? "" : dataReader["TEMPLATEHTML"].ToString();
                        assessmentItemTemplate.IsAudioAssesTF= dataReader["ISAUDIOASSETTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISAUDIOASSETTF"]);
                        assessmentItemTemplate.IsVisualAssetTF= dataReader["ISVISUALASSETTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISVISUALASSETTF"]);
                        assessmentItemTemplate.AssessmentItemTemplateID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]);
                    }
                }
                return assessmentItemTemplate;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return assessmentItemTemplate;
            }
        }
        /// <summary>
        /// This method gets the assessmentitem assets by assessmentitemID
        /// </summary>
        /// <param name="assessmentItemTemplateID">int assesmentitemID</param>
        /// <returns>List of AssessmentItemAsset object</returns>
        public List<AssessmentItemAsset> GetAssessmentItemAssets(int assessmentItemID)
        {
            DbCommand dbCommand = null;
            List<AssessmentItemAsset> assessmentItemAssets= new List<AssessmentItemAsset>();
            AssessmentItemAsset assessmentItemAsset = null; 
            try
            {
                //This SP gets assessmenttiemassets by assessmentitemID
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_ASSESSMENTITEM_ASSETS);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.Int32, assessmentItemID);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        assessmentItemAsset = new AssessmentItemAsset();
                        assessmentItemAsset.Assets = new Asset();
                        assessmentItemAsset.Assets.URL = dataReader["LOCATION"] == DBNull.Value ? "" : dataReader["LOCATION"].ToString();
                        assessmentItemAsset.IsAudioTF = dataReader["ISAUDIOTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISAUDIOTF"]);
                        assessmentItemAsset.IsVisualTF= dataReader["ISVISUALTF"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["ISVISUALTF"]);
                        assessmentItemAsset.Assets.AssetType = dataReader["ASSETTYPE"] == DBNull.Value ? "" : dataReader["ASSETTYPE"].ToString();
                        assessmentItemAsset.Assets.AssetID = dataReader["ASSET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ASSET_ID"]);
                        assessmentItemAssets.Add(assessmentItemAsset);
                    }
                }
                return assessmentItemAssets;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return assessmentItemAssets;
            }
        }
        /// <summary>
        /// This mehtid gets the knowledge check assessment items by scene
        /// </summary>
        /// <param name="sceneID">int sceneID</param>
        /// <returns>list of assessmentitems object</returns>
        public List<AssessmentItem> GetSceneAssessmentItems(int sceneID)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems fro scene
                dbCommand = db.GetStoredProcCommand(StoredProcedures.SELECT_SCENE_ASSESSMENTITEMS);
                db.AddInParameter(dbCommand, "@SCENE_ID", DbType.Int32, sceneID);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }
        #endregion



        public DataTable GetPostAssessmentLearningObjectiveForCourse(int CourseId)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtLO = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_LEARNINGOBJECTIVE_POST);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseId);

                dtLO = db.ExecuteDataSet(dbCommand).Tables[0];


                return dtLO;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public DataTable GetExamTopicForAdvacne(int ExamID)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtLO = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_EXAM_TOPIC_FORADVANCE);
                db.AddInParameter(dbCommand, "@EXAM_ID", DbType.Int32, ExamID);

                dtLO = db.ExecuteDataSet(dbCommand).Tables[0];


                return dtLO;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public DataTable GetPreAssessmentLearningObjectiveForCourse(int CourseId)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtLO = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_LEARNINGOBJECTIVE_PRE);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseId);

                dtLO = db.ExecuteDataSet(dbCommand).Tables[0];


                return dtLO;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public DataTable GetQuizAssessmentLearningObjectiveForCourse(int CourseId, int ContnetObjectId)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtLO = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_LEARNINGOBJECTIVE_QUIZ);
                db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, CourseId);
                db.AddInParameter(dbCommand, "@CONTENTOBJECT_ID", DbType.Int32, ContnetObjectId);

                dtLO = db.ExecuteDataSet(dbCommand).Tables[0];


                return dtLO;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public DataTable GetAssessmentItemIdForLearningObjective(int LearningObjectiveId, string assessmentType)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtAssessmentItemID = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_ASSESSMENTITEMID_LEARNINGOBJECTIVE);
                db.AddInParameter(dbCommand, "@LEARNINGOBJECTIVE_ID", DbType.Int32, LearningObjectiveId);
                db.AddInParameter(dbCommand, "@ASSESSMENTTYPE", DbType.String, assessmentType);

                dtAssessmentItemID = db.ExecuteDataSet(dbCommand).Tables[0];
                dtAssessmentItemID.TableName = LearningObjectiveId.ToString();

                return dtAssessmentItemID.Copy();
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public DataTable GetAssessmentItemIdForTopic(int TopicId)
        {
            DbCommand dbCommand = null;
            try
            {
                DataTable dtAssessmentItemID = new DataTable();
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_ASSESSMENTITEMID_TOPIC);
                db.AddInParameter(dbCommand, "@EXAMTOPIC_ID", DbType.Int32, TopicId);

                dtAssessmentItemID = db.ExecuteDataSet(dbCommand).Tables[0];
                dtAssessmentItemID.TableName = TopicId.ToString();

                return dtAssessmentItemID.Copy();
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        public List<AssessmentItem> GetAssessmentItemBySplit(string assessmentIds, int learningObjective_Id, double scoreWeight,string assessmentItemBinderName)
        {
            DataSet result = null;
            AssessmentItem item = null;
            DbCommand dbCommand = null;
            List<AssessmentItem> assessmentItems = new List<AssessmentItem>();
            List<AssessmentItemAnswer> assessmentItemAnswers = new List<AssessmentItemAnswer>();
            try
            {
                //This SP gets assessmentItems fro scene
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_ASSESSMENTITEM_SPLT);
                db.AddInParameter(dbCommand, "@ASSESSMENTITEM_ID", DbType.String, assessmentIds);

                result = db.ExecuteDataSet(dbCommand);

                foreach (DataRow row in result.Tables[0].Rows)
                {
                    item = new AssessmentItem();
                    string type = Convert.ToString(row["QUESTIONTYPE"]);
                    item = GetAssessmentItemEntity(row);
                    item.AssessmentBinderID = learningObjective_Id;
                    item.ScoreWeight = scoreWeight;
                    item.AssessmentBinderName = assessmentItemBinderName;
                    assessmentItemAnswers = GetAssessmentItemAnswers(item.AssessmentItemID, type);
                    item.AssessmentAnswers = assessmentItemAnswers;
                    assessmentItems.Add(item);
                }
                return assessmentItems;
            }

            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }



        // LCMS-9213
        public DataTable GetAssessmentAnswerItemIDsByGuid(string answerGuids)
        {
            DbCommand dbCommand = null;
            
            try
            {
                DataTable dtAssessmentItemID = new DataTable();
                dbCommand = db.GetStoredProcCommand("GET_ASSESSMENTITEMANSWER_ID_LIST_BY_GUIDS");
                db.AddInParameter(dbCommand, "@GUIDLIST", DbType.String, answerGuids);


                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count == 0)
                { return null; }

                return ds.Tables[0];

               
                
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        
        }

        /// <summary>
        /// This method returns the list of assessment Item result
        /// </summary>
        /// <param name="enrollmentID">int enrollmentID</param>
        /// <param name="assessmentType">int assessmentType</param>
        /// <returns>list of course approvals objects</returns>
        public List<AssessmentItemResult> GetLearnerAssessmentItemResults(int enrollmentID, string assessmentType)
        {
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand(StoredProcedures.ICP_SELECT_ASSESSMENTITEMID_RESULT);
                db.AddInParameter(dbCommand, "@LEARNERENROLLMENT_ID", DbType.Int32, enrollmentID);
                db.AddInParameter(dbCommand, "@ASSESSMENT_TYPE", DbType.String, assessmentType);

                List<AssessmentItemResult> assessmentItemResult = new List<AssessmentItemResult>();
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    FillAssessmentItemResultEntityList(dataReader, assessmentItemResult);
                }
                return assessmentItemResult;

            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        /// <summary>
        /// Thi method fills the Course Approval list with the data from IDataReader 
        /// </summary>
        /// <param name="dataReader">IDataReader datareader</param>
        /// <param name="glossaryItems">List CourseApproval object</param>
        private void FillAssessmentItemResultEntityList(IDataReader dataReader, List<AssessmentItemResult> assessmentItemResults)
        {
            AssessmentItemResult assessmentItemResult = null;
            int iCount = 1;
            int approvalID = 0;
            while (dataReader.Read())
            {
                assessmentItemResult = new AssessmentItemResult();
                assessmentItemResult.AssessmentItemResultlID = Convert.ToInt32(dataReader["ID"]);
                assessmentItemResult.MajorCategory = dataReader["MAJORCATEGORY"].ToString();
                assessmentItemResult.TotalAssessment = Convert.ToInt32(dataReader["TOTALASSESSMENT"]);
                assessmentItemResult.TotalAnswerCorrect = Convert.ToInt32(dataReader["ANSWEREDCORRECT"]);
                assessmentItemResult.TotalAnswerInCorrect = Convert.ToInt32(dataReader["ANSWEREDINCORRECT"]);
                assessmentItemResult.AnswerCorrectPercentage = Convert.ToDouble(dataReader["ANSWEREDCORRECTPERCENTAGE"]);
                assessmentItemResult.AnswerInCorrectPercentage = Convert.ToDouble(dataReader["ANSWEREDINCORRECTPERCENTAGE"]);
                assessmentItemResults.Add(assessmentItemResult);
            }
        }
    }
}
