<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LegacyRedirectPage.aspx.cs"
    Inherits="ICP4.CoursePlayer.LegacyRedirectPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Course Not Available
    </title>
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
    <form id="form1" runat="server">
    <div id="divContainer" runat="server">
        <div class="roundcont">
            <div class="roundtop">
                <img src="images/tl.gif" alt="" width="19" height="15" class="corner" />
                <img src="images/tr.gif" alt="" width="19" height="15" style="float: right" />
            </div>
            <div class="dataHead">
                <div class="rowleft">
                </div>
                <div class="rowright">
                </div>
                <div id="header" class="info-header" runat="server">
                    <img src="images/warning.png" id="imgLogoLeft" style="height: 30px; float: left;
                        margin-right: 10px;" alt="warning message">
                    <h2 style="padding-top: 4px;" id="H2heading" runat=server>
                       
                    </h2>
                </div>
            </div>
            <div class="roundbottom">
                <img src="images/bl.gif" alt="" width="19" height="15" class="corner" style="display: none" />
                <img src="images/br.gif" alt="" width="19" height="15" style="float: right" />
            </div>
        </div>
        <div class="roundcont">
            <div class="roundtop">
                <img src="images/tl.gif" alt="" width="19" height="15" class="corner" />
                <img src="images/tr.gif" alt="" width="19" height="15" style="float: right" />
            </div>
            <div class="datacontainer contentHeight" runat="server">
                <div class="rowleft">
                </div>
                <div class="rowright">
                </div>
                <div class="content">
                   
                    <br />
                    <div class="contentScroll" id="instructorInformationText" runat="server">
                      
                    </div>
                </div>
            </div>
            <div class="roundbottom">
                <img src="images/bl.gif" alt="" width="19" height="15" class="corner" style="display: none" />
                <img src="images/br.gif" alt="" width="19" height="15" style="float: right" />
            </div>
        </div>
    </div>
    <div id="divEmpty" runat="server">
        &nbsp;</div>
    </form>
</body>
</html>
