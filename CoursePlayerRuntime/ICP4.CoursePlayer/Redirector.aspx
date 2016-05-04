<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Redirector.aspx.cs" Inherits="ICP4.CoursePlayer.Redirector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="Scripts/jquery/jquery-1.2.6.js"></script>
    <script type="text/javascript">

        function getParameterByName(name) {
            //debugger;
            var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);

            return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
        }

        function doRedirect() {
            //debugger;         
            var parentWindow = window.opener;            
            if(parentWindow!=null)
            {
                if (parentWindow) {
                    var paramValue = getParameterByName("op");
                    var paramRollname = getParameterByName("rollname");				
                    //alert('Before complete');
                    //alert('parameter value: '+ paramValue);
                    //debugger;
                    if(paramValue=="complete" && typeof parentWindow.OnSignCompleted != "undefined")
                    {
                       // alert('After complete');
                        try
						{
						    parentWindow.OnSignCompleted(paramRollname);
						}
						catch(err)
						{							
						}
                    }
                    else if (paramValue=="DocuSignDown")
                    {
                        try
						{							
							parentWindow["cp"].OnDocuSignFail();
						}
						catch(err)
						{							
						}                         
                    }
                    else if (paramValue=="timeout")
                    {
                        self.close();
                        parentWindow["cp"].OnDocuSignTimeout();
                    }
                    else if (paramValue=="expired")
                    {
                        self.close();
                        parentWindow["cp"].OnDocuSignExpired();
                    }
                    else if (paramValue=="cancel")
                    {
						try
						{							
							parentWindow["cp"].OnSignCancel();							
						}
						catch(err)
						{							
						}
                    }
                    else if (paramValue=="expired")
                    {
                        try
						{							
							parentWindow["cp"].OnSignExpired();
						}
						catch(err)
						{							
						}                        
                    }
                     else if (paramValue=="viewcomplete")
                    {
                       try
						{							
							parentWindow["cp"].OnViewComplete();
						}
						catch(err)
						{							
						}
                    }
                    else if (paramValue=="decline")
                     {
                         try 
                        {
                            parentWindow["cp"].OnSignDecline();
                        }
                        catch (err) 
                        {
                        }
                    }                            
                    
                }
            }
             // Changed by Waqas Zakai
            // LCMS-11515, LCMS-11530
            // START            
            if (navigator.appName == "Microsoft Internet Explorer")
            {                
                window.close(); 
            }
            else
            {             
                self.close();
            }
            // LCMS-11515, LCMS-11530
            // END            
        }
 
         
        $(function () {
            doRedirect();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
