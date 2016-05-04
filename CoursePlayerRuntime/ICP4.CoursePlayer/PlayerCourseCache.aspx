<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayerCourseCache.aspx.cs" Inherits="ICP4.CoursePlayer.PlayerCourseCache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .error
        {
            color:red;
        }
        .message
        {
            color:blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="140px" Font-Names="Verdana">
        <asp:Label ID="Message" runat="server"></asp:Label>
        <br />
        <br />
        Please Enter Course ID to reset the Cache<br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="10px" Text="Course ID"></asp:Label>
        &nbsp;<asp:TextBox ID="CourseID" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
    </asp:Panel>
    </form>
</body>
</html>
