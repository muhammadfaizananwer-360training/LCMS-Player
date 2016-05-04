<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Heartbeat.aspx.cs" Inherits="ICP4.CoursePlayer.Heartbeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <script language="javascript" type="text/javascript">
          
            var setTimerForSessionReset;  
		    function SetParentTimerForSessionReset(parent)
            {                 
                setTimerForSessionReset=setInterval(function(){parent.ExecuteTimer()},60000);
            }
            
            function ClearExecuteTimer()
            {                
                clearInterval(setTimerForSessionReset);
                setTimerForSessionReset = null;
            }
          
          window.onload = function ()
          {     
            var top = parent.opener;           
            SetParentTimerForSessionReset(top);
          };
          
          // For IE6/IE7/IE8/IE9
          window.onunload = function ()
          {    
            ClearExecuteTimer();      
          };
          
          // For FireFox/Chrome/Safari
          window.onbeforeunload = function() 
          {
            ClearExecuteTimer();
          }; 
          
                          
      </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>
</body>
</html>
