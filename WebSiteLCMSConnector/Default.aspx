<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%
    /*Response.Expires = -1, by this every time new response is generate*/    
    Response.Expires = -1;
    
    bool fromAjax = Request.QueryString["hb_source"] != null && Request.QueryString["hb_source"].Equals("ajax");
    
    if (fromAjax)
    {
        Response.Write("Last Updated On : " + System.DateTime.Now + " : " + Session.SessionID);        
    }
    else
    {
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">

<%
    
    
    int course_id = Convert.ToInt32(Request.QueryString["course_id"]);
    int student_id = Convert.ToInt32(Request.QueryString["student_id"]);
    int epoch = Convert.ToInt32(Request.QueryString["epoch"]);
    String learningSessionGUID = Request.QueryString["learningSessionGUID"];
%>

<head runat="server">
    <title>Untitled Page</title>
</head>
<body onload="setTimeout('_hb_connectedWithSession()', 2000);">
    
    <div id="debugDiv" style="font-family: Verdana; font-size: 12px;">
    
    <br />
    Date : <%= System.DateTime.Now %>
    <br />
    Session ID : <%=Session.SessionID   %>
    <br />
    Student ID : <%=student_id %>
    <br />
    Course ID : <%=course_id %>
    <br />
    Epoch : <%=epoch %>
    <br />
    Learning Session GUID : <%=learningSessionGUID %>
    
    </div>

    <script type="text/javascript">

        var _hb_xmlhttp = null;
        var _hb_responseText = "";        

        function _hb_connectedWithSession() {
            
            if (_hb_xmlhttp == null) {
                if (window.XMLHttpRequest) {
                    _hb_xmlhttp = new XMLHttpRequest();
                } else if (window.ActiveXObject) {
                    if (new ActiveXObject("Microsoft.XMLHTTP")) {
                        _hb_xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                    } else {
                        _hb_xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
                    }
                }
            }
            _hb_xmlhttp.onreadystatechange = function() {
                if (_hb_xmlhttp.readyState == 4) {
                    if (_hb_xmlhttp.status == 200) {
                        _hb_responseText = _hb_xmlhttp.responseText;
                        //alert(_hb_xmlhttp.responseText);
                        document.getElementById("debugDiv").innerHTML += "<BR>" + _hb_responseText;
                        setTimeout("_hb_connectedWithSession()", 2000);
                    } else {
                        setTimeout("_hb_connectedWithSession()", 2000);
                    }
                    _hb_responseText = "";
                }
            };

                var url = "Default.aspx?course_id=<%=course_id %>&student_id=<%=student_id %>&epoch=<%=epoch %>&learningSessinGUID=<%=learningSessionGUID %>&hb_source=ajax";
                _hb_xmlhttp.open("POST", url, true); //false means synchronous
                _hb_xmlhttp.setRequestHeader("Cache-Control", "no-cache"); //"application/json; charset=utf-8");
                _hb_xmlhttp.send(null);

        } // end database call

    </script>

</body>

</html>

<%
    }
%>
