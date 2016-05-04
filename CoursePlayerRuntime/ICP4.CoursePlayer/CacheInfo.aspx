<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheInfo.aspx.cs" Inherits="ICP4.CoursePlayer.CacheInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PnlAuthentication" runat="server">
            <asp:Label ID="lblUserName" runat="server">User Name</asp:Label>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblPassword" runat="server">Password</asp:Label>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnAuthentication" runat="server" Text="Login" onclick="btnAuthentication_Click" />   
        </asp:Panel> 
        <asp:Panel ID="PnlCacheInfo" runat="server">
            <asp:Label ID="lblCourseID" runat="server">Publish Course ID</asp:Label>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="CourseID" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSearchCourse" runat="server" Text="Search Course Cache" onclick="btnSearchCourse_Click" />   
            <br />
            <br />
            <asp:Literal ID="ltrlCacheTable" runat="server"></asp:Literal>  
            <asp:Literal ID="ltrlCacheDetails" runat="server"></asp:Literal>  
        </asp:Panel>
        <asp:HiddenField ID="hdnAuthentication" Value="false"  runat="server" />   
    </div>
    </form>
</body>
</html>
