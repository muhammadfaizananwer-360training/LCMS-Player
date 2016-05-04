using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.IO;

public static class ExceptionPolicyForLCMS
{


    public static bool HandleException(Exception exceptionToHandle, string policyName)
    {

        bool returnValue = false;
        string stateObjects = "";

        try
        {
            CollectionDumpUtility c = new CollectionDumpUtility();
            stateObjects += c.GetCacheContents();
            stateObjects += c.GetSessionContents();
        }
        catch (Exception ex) { }

 

        ICPException myException = new ICPException(exceptionToHandle.Message + stateObjects, exceptionToHandle.StackTrace);

        myException.Data = exceptionToHandle.Data;
        myException.Source = exceptionToHandle.Source;
        myException.HelpLink = exceptionToHandle.HelpLink;
        myException.InnerException = exceptionToHandle.InnerException;
        myException.TargetSite = exceptionToHandle.TargetSite;

        returnValue = ExceptionPolicy.HandleException(myException, policyName);
        return returnValue;
    }
}
