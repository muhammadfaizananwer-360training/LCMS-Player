<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAssessmentResult.aspx.cs" Inherits="ICP4.CoursePlayer.ViewAssessmentResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LCMS Course Player :: View Assessment Result</title>
    <script type="text/javascript" src="Scripts/jquery/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="Scripts/jquery/ui.core.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.dimensions.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.accordion.js"></script>
    <script type="text/javascript" src="Scripts/jquery/ui.mouse.js"></script>
    <script type="text/javascript" src="Scripts/jquery/ui.slider.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.event.drop-1.0.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.event.drag-1.2.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.wresize.js"></script>
    <script type="text/javascript" src="Scripts/JSON.js"></script>
    <script type="text/javascript" src="Scripts/jquery/jquery.multicolselect.js"></script>        
    <script type="text/javascript" src="Scripts/Chart.js"></script>    
    
    <style type="text/css">

    .assessmentResultText {
        font-family: Tahoma,Arial,Verdana,Helvetica,sans-serif;
        font-size: 16px;
    }
    .assessmentResultborder {
        border-color: #eaeaea;
        border-style: solid;
        border-width: 1px;
        font-size: 16px;
    }
    .assessmentResultText {
        font-family: Tahoma,Arial,Verdana,Helvetica,sans-serif;
        font-size: 16px;
    }
    .assessmentResultborder {
        border-color: #eaeaea;
        border-style: solid;
        border-width: 1px;
        font-size: 16px;
    }
    
    .table {
        height: 100%;
        margin: auto;
        padding: 10px 10px 5px;
        width: 100%;
    }
    
    .body {
        font-family: Tahoma,Arial,Verdana,Helvetica,sans-serif;
        font-size: 1em;
    }    
    </style>          
</head>
<body class="body">
    <form id="form1" runat="server">
        <div id="assessmentresultdatatable" style="width:100%;">							
		     <asp:Literal ID="ltrlAssessmentResult" runat="server"></asp:Literal>						
		</div> 
		<asp:HiddenField ID="hdnEnrollmentID" Value="0" runat="server" />   
    </form>
</body>
</html>
