using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for PlayerUtil
/// </summary>
    public class PlayerUtil
    {
        public PlayerUtil()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void debugMessage(String message)
        {
            debug(System.DateTime.Now + " : Debug : " + message);
        }

        public static void debugError(String error)
        {
            debug(System.DateTime.Now + " : Error : " + error);
        }

        public static void debug(String msg)
        {
            System.Diagnostics.Trace.Listeners[0].WriteLine(msg);
            System.Diagnostics.Trace.Listeners[0].Flush();

            System.IO.File.WriteAllText(@"d:\WebSiteLCMSConnector\test.log", msg);
        }

    }
