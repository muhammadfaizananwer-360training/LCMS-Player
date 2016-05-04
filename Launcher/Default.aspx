<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<!--<domain -->
<title>POC</title>
<script>
function SFSubmit()
{
window.open('SFSubmit.aspx', '_blank');
}

function CourseLauncher()
{
window.open('CourseLauncher.aspx', '_blank');
}

function ProctorLauncher()
{
window.open('ProctorLauncher.aspx', '_blank');
}
</script>
</head>
<body>
<form id="form1" runat="server">
<p>Step 1 : The course has been purchased and now click <A href="#" onclick="javascript:SFSubmit();">here</A> to make Schedule </p>

<p>Step 2 : Now Launch the course on LMS <!--<A href="#" onclick="javascript:CourseLauncher();">LMS</A>--></p>

<p>Step 3 : Now Open the Proctor Screen  <A href="#" onclick="javascript:ProctorLauncher();">ProctorLauncher</A></p>


</form>
</body>

</html>
