using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json;


public class CollectionDumpUtility
{
	public CollectionDumpUtility()
	{   

	}

    private string linebreak = "\r\n";

    // Extracts Session Contents in a string variable. 
    public string GetSessionContents()
    {
        string str = "";

        if (System.Configuration.ConfigurationManager.AppSettings["LogAllSessionObjects"].ToLower() == "true")
        {
            for (int i = 0; i < HttpContext.Current.Session.Count; i++)
            {
                try
                {
                    str += (str == "" ? "" : linebreak); // Json items separator
                    object obj = HttpContext.Current.Session[i];
                    str += "\"" + HttpContext.Current.Session.Keys[i].ToString() + "\":";
                    str += GetObjectAsJsonString(obj);
                }
                catch (Exception ex) { }
            }
        }
        else
        {
            str += GetSessionContents(System.Configuration.ConfigurationManager.AppSettings["ListOfSessionKeys"]);
        }

        return linebreak + linebreak + "SESSION CONTENTS:" + linebreak + str + linebreak;
    }


    // Extracts Session Contents in a string variable. The list of desired session keys (comma-separated) may be passed as string 
    // parameter to print only specific keys. 
    private string GetSessionContents(string listOfKeys)
    {
        string str = "";

        #region Check For List Of Keys
        if (listOfKeys != null && listOfKeys.Trim() != "")
        {
            string[] sArr = listOfKeys.Split(',');

            for (int i=0 ; i < sArr.Length ; i++)
            {
                try
                {
                    str += (str == "" ? "" : linebreak); // Json items separator
                    object obj = HttpContext.Current.Session[sArr[i]];
                    str += "\"" + sArr[i] + "\":";
                    str += GetObjectAsJsonString(obj);
                }
                catch (Exception ex) { }
            }

        }
        #endregion
        
        return str;
    }

    
    // Extracts Cache Contents in a string variable. The list of desired cache keys (comma-separated) may be passed as string 
    // parameter to print only specific keys. Pass null as parameter if entire cache content is to be extracted
    public string GetCacheContents()
    {

        string str = "";

        if (System.Configuration.ConfigurationManager.AppSettings["LogAllCacheObjects"].ToLower() == "true")
        {

            IDictionaryEnumerator en = HttpContext.Current.Cache.GetEnumerator();

            while (en.MoveNext())
            {
                try
                {
                    string cacheKey = en.Entry.Key.ToString();
                    object cacheValue = en.Entry.Value;

                    str += (str == "" ? "" : linebreak + linebreak); // Json items separator                    
                    object obj = HttpContext.Current.Cache[cacheKey];
                    str += "\"" + cacheKey + "\":";
                    str += GetObjectAsJsonString(obj);
                    
                }
                catch (Exception ex) { }
            }
        }
        else
        {
            str += GetCacheContents(System.Configuration.ConfigurationManager.AppSettings["ListOfCacheKeys"]);
        }

        if (str.Trim() == "")
        {
            return str;
        }
        
        return linebreak + linebreak + "CACHE CONTENTS:" + linebreak + str;
    }


    private string GetCacheContents(string listOfKeys)
    {
        string str = "";

        #region Check For List Of Keys
        if (listOfKeys != null && listOfKeys.Trim() != "")
        {
            string[] cArr = listOfKeys.Split(',');

            for (int i = 0; i < cArr.Length; i++)
            {
                try
                {

                    if (cArr[i].ToLower() == "courseconfiguration" || cArr[i].ToLower() == "coursesequence")
                    {
                        cArr[i] = (System.Web.HttpContext.Current.Session["CourseID"] == null ? "" : System.Web.HttpContext.Current.Session["CourseID"].ToString()) + cArr[i];
                        cArr[i] = cArr[i] + (System.Web.HttpContext.Current.Session["Source"] == null ? "" : System.Web.HttpContext.Current.Session["Source"].ToString());
                    }
                    str += (str == "" ? "" : linebreak); // Json items separator
                    object obj = HttpContext.Current.Cache[cArr[i]];
                    str += "\"" + cArr[i] + "\":";
                    str += GetObjectAsJsonString(obj);
                    str += linebreak;
                }
                catch (Exception ex) { }
            }

        }
        #endregion

        return str;
    }



    // Converts the objects to a readable Json serialized string
    public string GetObjectAsJsonString(object obj)
    {
        string str = "";

        if (obj == null)
        {
            str += System.Configuration.ConfigurationManager.AppSettings["StateObjectNotFoundMessage"];
        }
        else
        {
            str += JavaScriptConvert.SerializeObject(obj);
        }
        
        return str;
    }


}
