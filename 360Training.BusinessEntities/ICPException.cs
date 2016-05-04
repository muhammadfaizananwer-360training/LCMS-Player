using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;



/// <summary>
/// Summary description for ICPException
/// </summary>
class ICPException : Exception
{
	public ICPException(string message, string sTrace) : base(message)
	{
        this._stackTrace = sTrace;
	}


    private System.Collections.IDictionary _data;
    public new System.Collections.IDictionary Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }



    private Exception _innerException;
    public new Exception InnerException
    {
        get
        {
            return _innerException;
        }
        set
        {
            _innerException = value;
        }
    }


    private string _stackTrace;
    public override string StackTrace
    {
        get
        {
            return _stackTrace;
        }        
    }



    private System.Reflection.MethodBase _targetSite;
    public new System.Reflection.MethodBase TargetSite
    {
        get
        {
            return _targetSite;
        }
        set
        {
            _targetSite = value;
        }
    }


}
