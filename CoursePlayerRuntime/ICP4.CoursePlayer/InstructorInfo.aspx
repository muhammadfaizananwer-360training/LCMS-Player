<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorInfo.aspx.cs" Inherits="ICP4.CoursePlayer.InstructorInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Instructor Information</title>
<link href="css/instructorStyle.css" rel="stylesheet" type="text/css" />
<!--[if lt IE 7]>
<style ... >
.headingHeight{
height:50px;
}
.contentHeight{
height:220px;
}
</style>
<![endif]-->
</head>

<body>
<form runat="server">
<div id="divContainer" runat="server">
<div class="roundcont">
   <div class="roundtop">
	 <img src="images/tl.gif" alt="" 
	 width="19" height="15" class="corner" 
	  />  
      <img src="images/tr.gif" alt="" 
	 width="19" height="15" style="float:right" />       </div>

    <div class="dataHead"> 
   <div class="rowleft"></div>
   <div class="rowright"></div>
   <div id="header" class="info-header">
     <img id="imgLogoLeft" runat="server" style="width:150px;height:30px" /> <img id="imgLogoRight" runat="server" style="width:150px;height:30px" /> </div>
   </div>
  
   <div class="roundbottom">
	 <img src="images/bl.gif" alt="" 
	 width="19" height="15" class="corner" 
	 style="display: none" /> 
     
    <img src="images/br.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  /> </div>
</div>	
 
 
 <div class="roundcont">
   <div class="roundtop">
	 <img src="images/tl.gif" alt="" 
	 width="19" height="15" class="corner" 
	  />  
      <img src="images/tr.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  />
   </div>

   <div class="datacontainer contentHeight">
   <div class="rowleft"></div>
   <div class="rowright"></div>
   <div class="content">
    <h2 id="InstructorInfoh2" runat="server">Instructor Information:</h2><br />
	
    <div class="contentScroll" id="instructorInformationText" runat="server">
		<p>For questions related to OSHA regulations or course content, please contact your trainer:<br /></p>
		<p>Marie Athey<br /> <a href="mailto:outreach-trainer@360training.com">outreach-trainer@360training.com</a><br /> Your Outreach Trainer will contact you within 24 hours of receipt of your email. <br /></p>
		<p style="padding-bottom:10px;">For questions related to your customer account or software-related support, please contact our 24/7 support team:<br /> <a href="mailto:support@360training.com">support@360training.com</a><br /> 1-800-442-1149</p>
		<%--<p>&nbsp;  </p>--%>
	</div>
   </div>
    
   </div>
  
   <div class="roundbottom">
	 <img src="images/bl.gif" alt="" 
	 width="19" height="15" class="corner" 
	 style="display: none" /> 
     
    <img src="images/br.gif" alt="" 
	 width="19" height="15" style="float:right" /> </div>
</div>
 
</div>
 <div id="divEmpty" runat="server">&nbsp;</div>
 </form>
</body>
</html>
