<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoursePlayerPreviewer.aspx.cs" Inherits="ICP4.CoursePlayer.CoursePlayerPreviewer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Course Player Previewer</title>
    
    <script language="javascript" type="text/javascript">
    var launchWindow;
    function openWin(url)
    {        
        var isResizable = 1;
        if(navigator.appName == 'Microsoft Internet Explorer')
        {
           if(navigator.appVersion.indexOf('MSIE 6') != -1)
           {
                if(navigator.appMinorVersion.indexOf('SP3') == -1)
                {
                    isResizable = 0;
                }   
           }
        }
        
        if (isResizable == 0)
        {
         if ( launchWindow != null ) {
            launchWindow.close();
        }
            /*
          var popup = window.open(url, "popup", "fullscreen");
          if (popup.outerWidth < screen.availWidth || popup.outerHeight < screen.availHeight)
          {
            popup.moveTo(0,0);
            popup.resizeTo(screen.availWidth, screen.availHeight);
          }      
          */  
        launchWindow=window.open(url,'','menubar=0,scroll bars=no,width=1024,height=660,top=0,left=0,resizable=0,location=0,toolbar=0,directories=0');
        //launchWindow=window.open(url,'','');
        }
        else
        {
        if ( launchWindow != null ) {
            launchWindow.close();
            }
            /*
          var popup = window.open(url, "popup", "fullscreen");
          if (popup.outerWidth < screen.availWidth || popup.outerHeight < screen.availHeight)
          {
            popup.moveTo(0,0);
            popup.resizeTo(screen.availWidth, screen.availHeight);
          } 
          */            
        launchWindow=window.open(url,'','menubar=0,scroll bars=no,width=1024,height=660,top=0,left=0,resizable=1,location=0,toolbar=0,directories=0');
        //launchWindow=window.open(url,'','');
        }
        
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    <center>Click on Preview Course button to preview
        <br /><br /><input runat="server" id="buttonPreview" type="button" value="Preview Course" onclick=""/> </center>        
    </div>
    </form>
</body>
</html>
