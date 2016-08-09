<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoursePlayerPreviewer.aspx.cs" Inherits="ICP4.CoursePlayer.CoursePlayerPreviewer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Course Player Previewer</title>
    
    <script language="javascript" type="text/javascript">
    var launchWindow;
    function ie_ver(){  
       var iev=0;
       var ieold = (/MSIE (\d+\.\d+);/.test(navigator.userAgent));
       var trident = !!navigator.userAgent.match(/Trident\/7.0/);
       var rv=navigator.userAgent.indexOf("rv:11.0");

       if (ieold) iev=new Number(RegExp.$1);
       if (navigator.appVersion.indexOf("MSIE 10") != -1) iev=10;
       if (trident&&rv!=-1) iev=11;

       return iev;         
   }
    
    function openWin(url)
    {        
	    if (ie_ver() > 0){ 
		    launchWindow = window.open(url,'','address=no,resizable=yes,toolbar=no,location=no,scrollbars=yes,menubar=no,status=yes,width=1366,height=660,left=0,top=0');
		    launchWindow.focus();
		    launchWindow.opener=this.window;
        }
        else{
		    launchWindow = window.open(url,'','address=no,resizable=yes,toolbar=no,location=no,scrollbars=yes,menubar=no,status=yes,width=launchWidth,height=launchHeight,left=0,top=0');
		    if (launchWindow.outerWidth < screen.availWidth || launchWindow.outerHeight < screen.availHeight)
		    {
			    launchWindow.moveTo(0,0);
			    launchWindow.resizeTo(screen.availWidth, screen.availHeight);
		    }                 
		    launchWindow.focus();
		    launchWindow.opener=this.window;
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
