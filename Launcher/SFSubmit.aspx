<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFSubmit.aspx.cs" Inherits="SFSubmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="SFSubmit" action="SFSubmit.aspx" method="POST">
<input type="hidden" id="LearnerEnrollment_Id" />
<input type="hidden" id="Mode" />
<input type="hidden" id="SecurityCode" />
<input type="hidden" id="ExamDuration" />
</form>
<script>


document.getElementById("LearnerEnrollment_Id").value = "123123";
document.getElementById("Mode").value = "Schedule";
document.getElementById("SecurityCode").value = "secure360";
document.getElementById("ExamDuration").value = "10";
//document.getElementById("SFSubmit").submit();
	
</script>
</body>
</html>
