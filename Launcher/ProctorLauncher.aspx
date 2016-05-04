<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProctorLauncher.aspx.cs" Inherits="ProctorLauncher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Now copy paste the Learning Session Guid here : <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        
        <asp:Button ID="Button1" runat="server" Text="Lock Course" 
            onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="UnLock Course" 
            onclick="Button2_Click" />
    </div>
    </form>
</body>
</html>
