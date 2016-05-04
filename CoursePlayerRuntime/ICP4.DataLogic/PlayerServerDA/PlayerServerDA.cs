using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ICP4.BusinessEntities;
using ICP4.DataLogic.Common;

namespace ICP4.DataLogic.PlayerServerDA
{
    public class PlayerServerDA : IDisposable
    {

        #region Properties
        /// <summary>
        /// private object for database
        /// </summary>
        private XmlDocument document = null;
        /// <summary>
        /// Class constructor
        /// </summary>
        public PlayerServerDA(String xmlFilePath)
        {
            document = new XmlDocument();
            document.Load(xmlFilePath);
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Player Server Methods
        /// <summary>
        /// This method is used to log player server cache invalidation in database
        /// </summary>
        /// <returns>N/A</returns>

        public int AddPlayerServerCacheLog(String ipAddress, int courseID, int cahceClearStatus, String exception)
        {
            /*
                        DbCommand dbCommand = null;
                        int returnValue = 0 ;
                        try
                        {
                            //This SP returns all player servers objects
                            dbCommand = db.GetStoredProcCommand(StoredProcedures.INSERT_ICP4PLAYERCOURSECACHELOG);
                            db.AddInParameter(dbCommand, "@IPADDRESS", DbType.String, ipAddress);
                            db.AddInParameter(dbCommand, "@COURSE_ID", DbType.Int32, courseID);
                            db.AddInParameter(dbCommand, "@TIMESTAMP", DbType.DateTime, System.DateTime.Now);
                            db.AddInParameter(dbCommand, "@CACHECLEARSTATUS", DbType.Int32, cahceClearStatus);
                            db.AddInParameter(dbCommand, "@EXCEPTION", DbType.String, exception);
                            db.AddOutParameter(dbCommand, "@NEWID", DbType.Int32, 0);
                            db.ExecuteNonQuery(dbCommand);
                            returnValue = Convert.ToInt32(dbCommand.Parameters["@NEWID"].Value);


                            return returnValue;
                        }
                        catch (Exception exp)
                        {
                            ExceptionPolicyForLCMS.HandleException(exp, "ICPException");
                            return returnValue;
                        }
            */
            return 0;
        }

        /// <summary>
        /// This method is used to get all player server list.
        /// </summary>
        /// <returns>This method will return list player servers, empty list otherwise.</returns>
        public List<PlayerServer> GetPlayerServers()
        {

            try
            {
                //This SP returns all player servers objects

                List<PlayerServer> playerServers = new List<PlayerServer>();
                XmlElement root = document.DocumentElement;
                XmlNode node = root.SelectSingleNode("/ServerSettings/Servers//Server");
                while (node != null)
                {
                    String hostName = node.Attributes["hostName"].Value;
                    String ipAddress = node.Attributes["localIPAddress"].Value;
                    String playerWebServiceURL = node.Attributes["playerWebServiceURL"].Value;

                    PlayerServer playerServer = new PlayerServer();
                    playerServer.HostName = hostName;
                    playerServer.LocalIPAddress = ipAddress;
                    playerServer.PlayerWebServiceURL = playerWebServiceURL;
                    playerServers.Add(playerServer);

                    node = node.NextSibling;
                }

                return playerServers;
            }
            catch (Exception exp)
            {
                ExceptionPolicyForLCMS.HandleException(exp, "Exception Policy");
                return null;
            }
        }

        #endregion
    }
}
