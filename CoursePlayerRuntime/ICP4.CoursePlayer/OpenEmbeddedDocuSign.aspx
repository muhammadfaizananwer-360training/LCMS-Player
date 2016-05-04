<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenEmbeddedDocuSign.aspx.cs" Inherits="ICP4.CoursePlayer.OpenEmbeddedDocuSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LCMS Course Player</title>
    
    <%--<script type="text/javascript" src="Scripts/jquery/jquery-1.2.6.js"></script>--%>
    <style type="text/css">
        .ui-widget
        {
            font-family: Verdana,Arial,sans-serif;
            font-size: 85% !important;
        }
    </style>
    
    <link rel="stylesheet" href="Scripts/DocuSign/themes/base/jquery.ui.all.css">
    <script src="Scripts/DocuSign/jquery-1.9.1.js"></script>
    <script src="Scripts/DocuSign/jquery-ui-1.10.1.custom.min.js"></script>
     
     <script type="text/javascript"> 
     
        function DocuSignDownMesage_old()
        {
            alert("The system is experiencing a high volume of requests at this time.  Please try again to sign your affidavit.");
           
            setTimeout(function()
                { 
                    $('[id$=btnTryAgain]').click();
                }
                ,100);
        }
       function DocuSignDownMesage()
        {
            $(function() {
		        $( "#dialog-confirm" ).dialog({
			        resizable: false,
        			width: 500,
			        modal: true,
			        buttons: {
				        "Continue": function() {
				            $('[id$=btnTryAgain]').click();
					        setTimeout(function(){ 
                                $('[id$=btnTryAgain]').click();
                            },100);
				        },
				        Cancel: function() {
				            self.close();
					        $( this ).dialog( "close" );
					        
				        }
			        }
		        });
	        });
        }
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Button ID="btnTryAgain" runat="server" Text="Button"  style="display:none;"
        onclick="btnTryAgain_Click"  />
    </div>
     <!-- dialogue -->
    <div id="dialog-confirm" title="DocuSign alert">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            The system is experiencing a high volume of requests at this time.  Please try again to sign your affidavit.</p>
    </div>
    <!-- dialogue end -->
    </form>
    <div class="signerMessage" id="messagediv" name="messagediv" runat="server" visible="false"><%=signerMessage %></div>
    
</body>
</html>
