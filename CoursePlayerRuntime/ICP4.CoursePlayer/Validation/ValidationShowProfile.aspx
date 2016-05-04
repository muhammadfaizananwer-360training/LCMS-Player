<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationShowProfile.aspx.cs" Inherits="ICP4.CoursePlayer.Validation.ValidationShowProfile" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Update Profile</title>
<style>
h1,h2,h3,h4,h5,h6,h7{ margin:0px; padding:0px;}

body{ font-family:Tahoma, Arial, Verdana, Helvetica, sans-serif;
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
 
 
 
 
 
 
 
 
 
 
 
 
    <form id="form1" runat="server">
 
 
 
 
 
 
 
 
 
 
 
 
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
     <h2>Please re-submit your profile details:</h2>
    <br />
    <br />
     <table width="59%" border="0" cellspacing="0" cellpadding="0">

              
       
       
       <tr>
         <td class="style8">First Name:</td>
         <td class="style5"><asp:TextBox ID="TxtFirstName" runat="server"></asp:TextBox></td>
       </tr>

       
       
       
       <tr>
         <td class="style11">Last Name:</td>
         <td class="style12"><asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox></td>
       </tr>

       
       
       
       <tr>
         <td class="style8">Address 1:</td>
         <td class="style5"><asp:TextBox ID="TxtAddress1" runat="server"></asp:TextBox></td>
       </tr>

       
       
       
       <tr>
         <td class="style8">Address 2:</td>
         <td class="style5"><asp:TextBox ID="TxtAddress2" runat="server"></asp:TextBox></td>
       </tr>

       
       
       
       <tr>
         <td class="style8">Address 3: </td>
         <td class="style5"><asp:TextBox ID="TxtAddress3" runat="server"></asp:TextBox>

         </td>
       </tr>

       
       
       
       <tr>
         <td class="style8">City:</td>
         <td class="style5"><asp:TextBox ID="TxtCity" runat="server" Height="21px" ></asp:TextBox>
                    &nbsp;</td>
       </tr>

       
       
       
       <tr>
         <td class="style8">State/ Province:</td>
         <td class="style5">
                    <asp:TextBox ID="TxtState" runat="server"></asp:TextBox>
                </td>
       </tr>

       
       <tr>
         <td class="style8">Country:</td>
         <td class="style5">
                    <asp:TextBox ID="TxtCountry" runat="server"></asp:TextBox>
                </td>
       </tr>

       
       <tr>
         <td class="style8">ZIp code:</td>
         <td class="style5">
                    <asp:TextBox ID="TxtZipcode" runat="server"></asp:TextBox>
                </td>
       </tr>

       
       <tr>
         <td class="style8">Phone:</td>
         <td class="style5">
                    <asp:TextBox ID="TxtPhone" runat="server"></asp:TextBox>
                            </td>
       </tr>

       
       <tr>
         <td class="style8">Email Address:</td>
         <td class="style5">
                    <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                            </td>
       </tr>
       <tr>
         <td class="style6">
             <asp:Label ID="LabelMessage" runat="server" Visible="False" ForeColor="#FF3300"></asp:Label>      </td>
         <td>

             <asp:Button ID="BtnUpdate" runat="server" onclick="BtnUpdate_Click1" 
                 Text="Update " />

      </td>
       </tr>
     </table>
    
   
     
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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style4
        {
            height: 26px;
        }
        .style5
        {
            height: 20px;
        }
        .style6
        {
            width: 255px;
        }
        .style7
        {
            height: 26px;
            width: 255px;
        }
        .style8
        {
            height: 20px;
            width: 255px;
        }
        .style9
        {
            width: 255px;
            height: 11px;
        }
        .style10
        {
            height: 11px;
        }
        .style11
        {
            height: 24px;
            width: 255px;
        }
        .style12
        {
            height: 24px;
        }
    </style>
</head>
<body>
    </form>
</body>
</html>
