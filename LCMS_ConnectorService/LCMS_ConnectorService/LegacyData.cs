using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LCMS_ConnectorService
{
    public class LegacyData
    {
        /// <summary>
        /// private object for database
        /// </summary>
        private Database db = null;

        public LegacyData(){
            db = DatabaseFactory.CreateDatabase("360CoursewareTrainingServiceDB");
        }

        public DataTable Get_LCMS_StudentCourse()
        { 
            string strQuery = ""+
            " SELECT learningSession_id,course_id,status,student_id,epoch," +
            " (select top 1 playerVersion from VUCouseMapping_LCMS where vuCourseId=LSC.Course_Id) as playerVersion " +
            " FROM LCMS_StudentCourse LSC" +
            " inner join LCMS..LearningSession LS on LS.LearningSessionGuid = LSC.learningSession_id" +
            " WHERE 	status = 0 and lockBit = 0 " +
            " and LS.STARTTIME >= DATEADD (day , -1 , GETDATE() )" +
            " order by LSC.id desc";

            
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = db.ExecuteDataSet(CommandType.Text, strQuery);
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }

            }

            catch (Exception exp)
            {
                Trace.WriteLine(exp.ToString());
                Trace.Flush();

                return null;
            } 

            return null;
        }
    }
}
