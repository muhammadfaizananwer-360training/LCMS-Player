<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationUnlockedSuccessful.aspx.cs" Inherits="ICP4.CoursePlayer.Validation.ValidationUnlockedSuccessful" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Course Unlocked</title>
<style>
h1,h2,h3,h4,h5,h6,h7{ margin:0px; padding:0px;}

body{ font-family:"Tahoma, Arial, Verdana, Helvetica, sans-serif;
font-size:11px; }

html> body> .datacontainer{ height:100%;}
.roundcont {
	
	background-color: #fff;
	
	margin-bottom:10px;
}

label{ margin:0px;
padding:0px; 
display:block;
font-size:14px;
padding-top:10px;

}
input{ background:#f4f4f4;
border:#999999 1px solid;}

select{ background:#f4f4f4;
border:#999999 1px solid;}

.roundcont p {
	margin: 0 10px;
}

.roundtop { 
	height:15px;
	background:url(../images/top_row.gif); 

}

.roundbottom {
	height:15px;
	background:url(../images/bot_row.gif); 
}

img.corner {
   width: 19px;
   height: 15px;
   border: none;
   display: block !important;
   float:left;
}

.datacontainer{ position:relative;}

.rowleft{
position:absolute; 
float:left; 
width:19px; 
height:100%; 
background:url(../images/bg_left.gif);
border:#000099 0px solid;
}


.rowright{
position:absolute; 
right:0px; 
width:19px; 
height:100%; 
background:url(../images/bg_right.gif);
}
</style>
</head>

<body>

<div class="roundcont">
   <div class="roundtop">
	 <img src="../images/tl.gif" alt="" 
	 width="19" height="15" class="corner" 
	  />  
      <img src="../images/tr.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  />       </div>

   <div class="datacontainer">
   <div class="rowleft"></div>
   <div class="rowright"></div>
   <div style="padding:10px;">
     <img id="imgLogo" runat="server" src=""  style="margin-left:10px;"/>   </div>
   </div>
  
   <div class="roundbottom">
	 <img src="../images/bl.gif" alt="" 
	 width="19" height="15" class="corner" 
	 style="display: none" /> 
     
    <img src="../images/br.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  /> </div>
</div>	
 
 
 
 
 
 
 
 
 
 
 
 
 <div class="roundcont">
   <div class="roundtop">
	 <img src="../images/tl.gif" alt="" 
	 width="19" height="15" class="corner" 
	  />  
      <img src="../images/tr.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  />
   </div>

   <div class="datacontainer">
   <div class="rowleft"></div>
   <div class="rowright"></div>
   <div style="padding-left:30px;">
     <h2></h2>
       <h2>
           Your course has been successfully unlocked</h2>
                <p>The validation questions have been rest and you can resume your course now.
                    <br />
                    Please log on to My Training to resume your course. An email has been sent to 
                    you containing the updated validation questions for your reference.  

  </p>
     <p>&nbsp;  </p>
   </div>
    
   </div>
  
   <div class="roundbottom">
	 <img src="../images/bl.gif" alt="" 
	 width="19" height="15" class="corner" 
	 style="display: none" /> 
     
    <img src="../images/br.gif" alt="" 
	 width="19" height="15" style="float:right" 
	  /> </div>
</div>
 
 
 
</body>
</html>
