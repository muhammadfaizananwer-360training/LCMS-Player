<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoursePlayer.aspx.cs" Inherits="ICP4.CoursePlayer.CoursePlayer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- for debugging
<script type="text/javascript" src="https://getfirebug.com/firebug-lite.js"></script>
-->
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
    <title>LCMS Course Player</title>
    <%-- <% string g = System.Guid.NewGuid().ToString(); %>--%>
    <% string g = "course player testing"; %>
    <!-------------- Added by Mustafa for LCMS-2050 ------------------>
    <!-- ========================================================== -->

    <script language="javascript" type="text/javascript">
        if (typeof Image == 'undefined') {
            (function() {
                function createImage() {
                    var iframe = document.createElement('iframe');
                    iframe.style.display = 'none';
                    document.body.appendChild(iframe);
                    window.frames[window.frames.length - 1].document.write("<script>window.parent.Image = Image;<\/script>");
                    document.body.removeChild(iframe);
                }
            })();
        }       
      

        function action() {


            if (document.getElementById("ImageButton1").src.indexOf("Flag1.png") > -1) {
                document.getElementById("ImageButton1").src = "\Images\\Flag2.png";
                document.getElementById("ImageButton1").title = assessment_toggle_flag1_tx; //"Click to remove the flag on this question.";
            }
            else {
                document.getElementById("ImageButton1").src = "\Images\\Flag1.png";
                document.getElementById("ImageButton1").title = assessment_toggle_flag2_tx; //"Click to flag this question for review.";
            }
        }
    </script>

    <!-- ========================================================= -->

    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <!-- Start Loading JQuery -->

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

    <script type="text/javascript" src='<%="JSPlayer/j360player.js"%>'></script>

    <script type="text/javascript" src='<%="JSPlayer/init.js"%>'></script>
    
    <script src="Scripts/JSON3.js" type="text/javascript"></script>
    <!-- End Loading JQuery -->
    <!-- Start Loading Course Player Frame Work -->
    <!-- LCMS-11870 starts -->

    <script type='text/javascript'>
        function javascriptPlayerMp4(fileName, fileType) {
            j360Player.PlayerCall(fileType, fileName);
        }
    </script>

    <script type='text/javascript'>
        function javascriptPlayer(fileName, fileType, StreamingEngine, stream) {
            j360Player.PlayerCall(fileType, fileName, StreamingEngine, stream);
        }
    </script>

    <!-- LCMS-11870 ends -->

    <script type="text/javascript" src="Scripts/AC_RunActiveContent.js"></script>

    <!-- LCMS-11870 starts 

    <script type="text/javascript" src='<%="Scripts/com/j360player.js"%>'></script>

    <script type="text/javascript" src='<%="Scripts/com/init.js"%>'></script> -->
    <!-- LCMS-11870 ends -->

    <script type="text/javascript" src='<%="Scripts/com/CoursePlayerEngine.js?g=" + g %>'></script>

    <script type="text/javascript" src='<%="Scripts/com/CommunicationEngine.js?g=" + g %>'></script>

    <script type="text/javascript" src='<%="Scripts/com/RenderEngine.js?g=" + g %>'></script>

    <script src="Scripts/jquery/jquery.treeview.js" type="text/javascript"></script>

    <script src="Scripts/jquery/ui.progressbar.js" type="text/javascript"></script>

    <script language="Javascript">

        var blinkingCheck = false;
        var ISCourseLocked = false;
        // Added by Mustafa for LCMS-2502
        //--------------------------------------
        function refreshTitle() {
            document.title = "LCMS Course Player";
        }
        //--------------------------------------



        // Supporting script for flash detection (partial script for cancelling the timeout behavior is in flashplugin.swf)
        //---------------------------------------------------------

        function checkFrameUrl() {
            var pluginUrl = "https://get.adobe.com/flashplayer";
            var message = "The scene you are about to access contains flash content, but your flash plugin does not seem to be installed or updated. It is recommended that you download the latest version of flash player from " + pluginUrl + ". Do you want to download the latest player now?";
            var isUserAgreed = confirm(message);

            if (isUserAgreed) {
                window.open(pluginUrl, "_blank");
            }
        }

        var flashPluginTimerId;
        var flashPluginTimeOutDuration = 3000; // 3 seconds


        // This line is placed in RenderEngine.js to start the timer specifically for a scene that contains flash content.
        //flashPluginTimerId = window.setTimeout("checkFrameUrl();", flashPluginTimeOutDuration);

        function clearFlashPluginTimeOut() {
            clearTimeout(flashPluginTimerId);
        }
        //---------------------------------------------------------



        // Added by Mustafa for LCMS-2107
        // ------------------------------------------------------------
        var flashContentLoadingDuration = 3000; // 3 seconds
        function makeFlashVisible(w, h) {
            document.getElementById("divFlashLoading").style.display = 'none';
            document.getElementById("divFlashContent").style.display = 'none';
            document.getElementById("divFlashContent").style.display = '';
            document.getElementById("divFlashContent").style.width = w + 'px';
            document.getElementById("divFlashContent").style.height = h + 'px';
        }
        // ------------------------------------------------------------




        // functions to prevent holding keys while mouse click
        //----------------------------------------------------------
        var isKeyPressed = false;
        //fix for LCMS-10870
        var lastKeyCode = 0;
        var lastKeyIsCtrl = true;
        function storeKeyPress(e) {
            //        if (e.keyCode == 9 && (!e.ctrlKey || !e.altKey) && is_chrome) //If TAB is pressed on Chrome
            //        {
            //        e.preventDefault();//prevent tab press effect.
            //        return;
            //        }

            //fix for LCMS-10871 avoid Ctrl+P
            if (e.keyCode == 80 && e.ctrlKey) //If Ctrl+P is pressed
            {
                if (isLockoutClickAwayToActiveWindowStart == true && ISCourseLocked == false) {
                    //cp.CourseLockDueToInActiveCurrentWindow(); //lock course LCMS-12554 Yasin
                    ModelPopUpForClickAwayFunctionality(); //LCMS-12554 Yasin

                }
                return;
            }

            lastKeyCode = e.keyCode;
            lastKeyIsCtrl = e.ctrlKey;

            if (e.keyCode == 122) //if F-11 is pressed, flush it after 1 sec
            {
                setTimeout(function() { lastKeyCode = 0; }, 1000)
            }

            isKeyPressed = true;
        }

        function onKeyUp(e) {
            isKeyPressed = false;
            //fix for LCMS-10871-Avoid print-screen
            if (e.keyCode == 44) //If Print Screen pressed. This only works in firefox
            {
                if (isLockoutClickAwayToActiveWindowStart == true && ISCourseLocked == false) {
                    //cp.CourseLockDueToInActiveCurrentWindow(); //lock course LCMS-12554 Yasin
                    ModelPopUpForClickAwayFunctionality(); //LCMS-12554 Yasin

                }
                //window.clipboardData.clearData();              
                return;
            }
        }
        function storeKeyUp() { isKeyPressed = false; }
        function checkPressedKeys() {
            lastKeyCode = 0;
            if (isKeyPressed) {
                alert(holding_key_tx);
                isKeyPressed = false;
                return false;
            }
        }
        //---------------------------------------------------------- 

  
    </script>

    <script type="text/javascript">



        // For identifying IE8 and giving alert for downloading security patch
        //-----------------------------------------------------------

        //if(navigator.appName == "Microsoft Internet Explorer")
        //{
        //    if(navigator.appVersion.indexOf("MSIE 8") != -1)
        //    {
        //        if(confirm("It seems you are using Microsoft Internet Explorer 8. It is recommended to download and install \"IE June Security Update for IE8 Beta 1\" or higher. Click OK to download the security update or CANCEL if you have it already."))
        //        {window.open("http://support.microsoft.com/kb/951804", "_blank");}
        //    }
        //}
        //-----------------------------------------------------------


        function popUp(divId) {
            //Updated By Rahil - To show Popup in center 
            //Start LCMS-2412
            var popupWidth = 600;
            var popupHeight = 200;
            var left = (screen.width - popupWidth) / 2;
            var top = (screen.height - popupHeight) / 2;
            var featurelist = "width=" + popupWidth + ",height=" + popupHeight + ",left=" + left + ",top=" + top + ",scrollbars=1,resizable=1,menubar=no";
            var frog = window.open("", "PopUp", featurelist)
            //var frog = window.open("","PopUp","width=600,height=200,scrollbars=1,resizable=1,menubar=no")
            //END LCMS-2412

            //             var html = document.getElementById(divId).innerHTML;
            var html = document.getElementById(divId).innerHTML.replace("<PRE>", "").replace("</PRE>", "").replace("<pre>", "").replace("</pre>", "");
            var oldfontfamily = $('#' + divId).parents('div').css('font-family');
            var oldfontsize = $('#' + divId).parents('div').css('font-size');
            var oldtextalign = $('#' + divId).parents('div').css('text-align');

            //variable name of window must be included for all three of the following methods so that
            //javascript knows not to write the string to this window, but instead to the new window

            frog.document.open()
            //Fix for LCMS-1799--Modified by Umer     

            //frog.document.write('<style type="text/css" media="screen">body,pre{font-family:'+oldfontfamily+';font-size:'+oldfontsize+';text-align:'+oldtextalign+';}</style>'+ replaceSpacesAndLineBreaks(html)); //Modified by Mustafa On Sep 2nd 2009
            frog.document.write('<style type="text/css" media="screen">body,pre{font-family:' + oldfontfamily + ';font-size:' + oldfontsize + ';text-align:left;}</style>' + replaceSpacesAndLineBreaks(html)); //Modified by Mustafa On Oct 27th 2009 (for LCMS-2404)
            //            frog.document.write('<style type="text/css" media="screen">body,pre{font-family:'+oldfontfamily+';font-size:10pt;text-align:'+oldtextalign+';}</style>'+ replaceSpacesAndLineBreaks(html)); //Modified by Mustafa On Sep 14th 2009
            frog.document.close()

        }

        //    function testFunction(para1, para2, para3){
        //    getMovieName('player1').SetVariable("StageWidth",document.getElementById("txtWidth").value);
        //    getMovieName('player1').SetVariable("StageHeight",document.getElementById("txtHeight").value);
        //    getMovieName('player1').SetVariable("movieClipName",document.getElementById("txtURL").value);
        //      }


        function getMovieName(movieName) {
            if (navigator.appVersion.indexOf("10.0") != -1) {
                return document[movieName];
            } else if (navigator.appName.indexOf("Microsoft") != -1) {
                return window[movieName];

            } else {
                return document.embeds[movieName];
            }
        }

        function initializeIt(val) {
            return val;
        }


        function playerLoaded() {
            if (playerFileName == "player_as2") {
                getMovieName("NewPlayer").SetVariable("MovieClipName", document.getElementById("txtURL").value.replace("https://", "http://"));
                //getMovieName ("NewPlayer").initializeIt(document.embeds.getElementById("txtURL").value.replace("https://","http://"));
                //getMovieName("NewPlayer").SetVariable("StageWidth",document.getElementById("txtWidth").value);
                //getMovieName("NewPlayer").SetVariable("StageHeight",document.getElementById("txtHeight").value);
                //getMovieName(playerFileName).SetVariable("MovieClipName",document.getElementById("txtURL").value);            
            }
            else if (playerFileName == "videoplayer") {
                //Set video player volume            
                getMovieName("NewPlayer").setVolume(0.5);
                getMovieName("NewPlayer").sendVariables(null, null, document.getElementById("txtURL").value.replace("https://", "http://"));
                //getMovieName(playerFileName).sendToActionScript(document.getElementById("txtURL").value);
            }





            //    getMovieName("player1").SetVariable("StageWidth", 750);
            //    getMovieName("player1").SetVariable("StageHeight", 450);
            //    getMovieName("player1").SetVariable("MovieClipName", "BBT.flv");
        }

        function alertfun() {
            alert("drag");
        }


    </script>

    <script language="javascript" type="text/javascript">

        var statusUpdateId;
        function process() {
            var process = document.getElementById("process");
            var loadText = document.getElementById("loadText");
            var status_bar = document.getElementById("status_bar");
            status_bar.style.width = "1px";
            var process_text = document.getElementById("process_text");
            process_text.innerHTML = processing_text_tx; //"The contents of your course are being loaded.";
            status_bar.style.width = "1px";
            if (process.style.display == "none") {
                process.style.display = "block"
                statusUpdateId = setInterval("updateStatus()", 200);
                //alert(statusUpdateId);
            } else {
                process.style.display = "none"
                clearInterval(statusUpdateId);
                // alert(statusUpdateId);
            }
        }
        function updateStatus() {
            var status_bar = document.getElementById("status_bar");
            if (parseInt(status_bar.style.width) < 375) {
                status_bar.style.width = (parseInt(status_bar.style.width) + 15) + "px"
            } else {
                clearInterval(statusUpdateId);
                var process_text = document.getElementById("process_text");
                process_text.innerHTML = processed_text_tx; //"The content has been loaded.";

                //$(preloader).hide();

            }
        }
            
                              
    </script>

    <script language="javascript" type="text/javascript">
        /*
        $(function(){

 $(document).keyup(function (e){
        if(e.which == 17 || e.which == 122){
        jsFun();
        }
        });

window.onblur = jsFun;


 function jsFun(){
        var secureDiv = document.getElementById('security');
        if(secureDiv.style.display != "none")
        thisMovie("securityAPI").jsFun();
        }

 function thisMovie(movieName) {
        if (navigator.appName.indexOf("Microsoft") != -1) {
        return window[movieName];
        } else {
        return document[movieName];
        }
        }
        }); */

    </script>

    <style type="text/css">
        #flashContent
        {
            width: 100%;
            height: 100%;
            overflow: visible;
        }
    </style>    
    <link href="css/Courseloading.css" rel="stylesheet" type="text/css" />   
    <link href='<%="css/style.css?g=" + g %>' rel="stylesheet" type="text/css" />    
    <link href="css/jquery.treeview.css" rel="stylesheet" type="text/css" />

</head>
<!---onunload="windowClosed();"--->
<body onload="completed()" onunload="windowClosed();" onmousedown="checkPressedKeys();"
    onkeydown="storeKeyPress(event);" onkeyup="onKeyUp(event);" onfocus="storeKeyUp();StopBlinking();">
    <style type="text/css">
        div.MaskedDiv
        {
            background-color: #FFFFFF;
            display: none;
            font-family: verdana;
            font-weight: bold;
            height: 100%;
            left: 0;
            opacity: 0.5; /*padding: 40px;*/
            position: absolute;
            top: 0; /*visibility: hidden;*/
            width: 100%;
            z-index: 999999;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=55); /* this works in IE6, IE7, and IE8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=55)"; /* this works in IE8 only */ /* ieWin only stuff */
            _background-image: none;
            _backgroud-color: none;
        }
    </style>
    <div id="security" style="overflow: hidden; width: 5px; height: 5px; float: left;">
        <object id="securityAPI" height="5" align="middle" width="5" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0"
            classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000">
            <param value="always" name="allowScriptAccess" />
            <param value="false" name="allowFullScreen" />
            <param value="securityAPI.swf" name="movie" />
            <param value="high" name="quality" />
            <param value="#EAEAEA" name="bgcolor" />
            <embed height="5" align="middle" width="5" pluginspage="https://www.adobe.com/go/getflashplayer"
                swliveconnect="true" type="application/x-shockwave-flash" allowfullscreen="false"
                allowscriptaccess="always" name="securityAPI" bgcolor="#EAEAEA" quality="high"
                src="securityAPI.swf" />
        </object>
    </div>
    <form id="form1" runat="server">
    <div id="timerCancelling">
    </div>
    <asp:HiddenField ID="txtWidth" runat="server" />
    <asp:HiddenField ID="txtHeight" runat="server" />
    <asp:HiddenField ID="txtURL" runat="server" />
    <div id="preloader" visible="false">
        <table align="center" width="100%" height="100%">
            <tr align="center" valign="top">
                <td align="center" valign="top">
                    <%--<img id="preloaderimg" runat="server" alt="Loading Image" />--%>
                    <div id="process" style="display: none;">
                        <div class="process_window">
                            <div class="icon_clock">
                            </div>
                            <div class="process_heading">
                                Please wait a moment...</div>
                            <div id="process_text" class="processing_text">
                                The contents of your course are being loaded.</div>
                            <div class="bars_container">
                                <div class="progress_bar">
                                </div>
                                <div id="status_bar" class="status_bar">
                                </div>
                            </div>
                        </div>
                        <div class="process_window_shadow">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        //Translation Start
        processSetUp();

        var jsLink = $("<script type='text/javascript' src='Scripts/Language/" + variant + ".js'>");
        $("head").append(jsLink);
        ChangeCourseLoadingMessages();

        //Translation End
        process();
    </script>

    <%--src="images/ajax-loader.gif"
--%><div id="disableScreen">
    </div>
    <!-- lock Course -->
    <div id="lockCourseDialogue">
        <div id="lockCourseHeading">
            <!-- head -->
            <div id="lockCourseIcon">
                Course Locked</div>
            <h1>
                <a href="#">&nbsp;</a></h1>
        </div>
        <p>
        </p>
        <asp:HiddenField ID="hdnUnlock" runat="server" />
        <!-- <a id="lnkUnlock"><img border="0" src="images/btnUnlock.gif" /></a> -->
        <button class="button">
            OK</button>
    </div>
    <!-- lock Course End -->
    <!-- dialogue -->
    <div id="glossaryDialogue">
        <h2>
            360training.com - Glossary</h2>
        <dl>
            <dt>Term</dt>
            <dd>
            </dd>
            <dt>Definition</dt>
            <dd class="deflist">
            </dd>
        </dl>
        <button class="button" style="margin-top: 50px;">
            Close</button>
    </div>
    <%-- Yasin LCMS-12519 Start --%>
    <div id="focusOutLockedOutMessage" style="display: none;">
        <div class="warning-border">
            <img src="images/warning.png" alt="Warning" border="0" />
            <div class="warning" style="text-align: left; padding: 10px 0px 5px 5px; font-size: 20px;">
                Warning!</div>
            <div id="focusOutLockedOutMessageText">
                <p>
                    If you leave the examination window for any reason during the exam, the course will
                    lock and you will fail this exam attempt.
                </p>
                <p>
                    You have clicked in a way that will cause you to leave the examination window.</p>
                <p>
                    Do you want to continue? Click Cancel to stay in the exam.</p>
            </div>
            <div style="background-color: #f4f4f4; height: 45px; width: 100%;">
                <table cellpadding="0" cellspacing="0" width="100%" class="modal-table">
                    <tr>
                        <td style="text-align: left;">
                            <div id="focusOutLockedOutMessageTimer">
                                Course automatically locks in <span id="spanClickAwayTimer"></span>&nbsp;seconds.</div>
                        </td>
                        <td>
                            <div style="float: right; padding-right: 0px;">
                                <button id="focusOutLockedOutButtonNo" class="button" style="margin-top: 0px;">
                                    Cancel</button>&nbsp;
                                <button id="focusOutLockedOutButtonYes" class="button" style="margin-top: 0px;">
                                    Leave Exam</button></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%-- Yasin LCMS-12519 End --%>
    <div id="orderingAssessmentDialogue" style="position: absolute; top: 30%; left: 25%;
        width: 550px; background: url(images/bg-dialog2.gif) repeat-x bottom; border: 1px #DBD8D8 solid;
        text-align: center; padding-bottom: 20px; z-index: 50; display: none; font-size: .9em;">
        <br />
        <div style="padding-left: 10px;">
        </div>
        <button class="button" style="margin-top: 50px;">
            OK</button>
    </div>
    <div id="helpDialogue">
        <h2>
            Help</h2>
        <br />
        <div style="padding-left: 10px;">
            <a id="help_linkstudent_text" href="http://www.360trainingsupport.com" target="_blank">
                Individual Students Click Here</a>
            <br />
            <br />
            <a id="help_linkcoporate_text" href="http://www.360trainingsupport.com" target="_blank">
                Corporate Clients Click Here</a>
            <br />
            <br />
            <a id="ChatForHelp" name="ChatForHelp" target="_blank" onclick="window.open((window.pageViewer &amp;&amp; pageViewer.link || function(link){return link;})(this.href + (this.href.indexOf('?')>=0 ? '&amp;' : '?') + 'url=' + escape(document.location.href)), 'Chat1589595132079595734', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=640,height=480');return false;">
                For Live Chat Click Here</a>

            <script language="JavaScript" type="text/javascript">

                document.write(unescape('%3Cscript language="JavaScript" type="text/javascript" src="' + (('https:' == document.location.protocol) ? 'https:' : 'http:') + '//cbi.boldchat.com/aid/365233055248581397/bc.cbhs"%3E%3C/script%3E')); 
  
            </script>

        </div>
        <div style="width: 100%; text-align: center;" id="ChatForHelpClose">
            <button class="button" style="margin-top: 50px;">
                Close</button>
        </div>
    </div>
    <div id="examIdleTimerDialogue">
        <h2>
            <span id="idleTimeHeading"></span>
        </h2>
        <br />
        <br />
        <div id="idleTimeMessageBody">
            <center>
                <span id="idleTimeContent"></span><b><span id="spnMinutes"></span>:<span id="spnSeconds"></span>&nbsp;<span
                    id="idleMinutesOrSecondsLabel">minutes</span></b>
                <br />
                <br />
                <button id="btnResume" class="button" style="width: 200px;">
                    Continue&nbsp;Course</button>
            </center>
        </div>
    </div>
    <!-- dialogue -->
    <div id="bookmarkDialogue">
        <h2>
            Bookmarks</h2>
        <label for="bookmarkTitle">
            <span>Add Bookmark:</span><input type="text" id="bookmarkTitle" name="bookmarkTitle" /></label>
        <button class="button">
            Submit</button>
        <button class="button">
            Cancel</button>
    </div>
    <!-- dialogue end -->
    <!-- Assessment Timer Expires -->
    <div id="ProctorLockCourse" style="display: none">
        <br />
       Hello, Your course is locked.And click
        <button class="button">
            Here</button>
        to Unlock.
        <br />
        <br />
    </div>
    <div id="timerExpireDialogue">
        <div id="timerExpireHeading">
            <!-- head -->
            <div id="timerExpireIcon">
                Assessment Timer has Expired
            </div>
        </div>
        <p>
            The time alloted to take complete this course has expired. Click on the 'Logout'
            button to continue.</p>
        <!-- DIV tag has been put for LCMS-727 -->
        <div id="expButton" style="padding-right: 40px; text-align: right;">
            <button class="button">
                Continue</button>
        </div>
    </div>
    </form>
    <!-- Assessment Timer Expires End -->
    <!-- messages dialogue Start Assessment -->
    <div id="dialog"  role="alert" >
        <h2>
            </h2>
        <div id="dialogHeading" >
            <div id="Icon">
                &nbsp;</div>
            <h1>
               </h1>
        </div>
        <p>
            In order to continue in this course you must validate your identity. Please answer
            the question below and click on the 'Validate My Identify' button to continue.
        </p>
        <button class="button">
            Start Assessment</button>
    </div>
    <!-- messages dialogue end -->
    <!-- configuration dialogue -->
    <div id="configrationDialogue">
        <p class="iconHeading">
            &nbsp;</p>
        <h1>
            Player Configuration</h1>
        <div id="configrater">
            <ul>
                <li><span>Audio:</span></li>
                <li>
                    <label for="SoundOn">
                        On
                    </label>
                    <input type="radio" checked="checked" name="Sound" id="SoundOn" onclick="soundOnOffHandler('On');" />
                    <label for="SoundOff">
                        Off
                    </label>
                    <input type="radio" name="Sound" id="SoundOff" onclick="soundOnOffHandler('Off');" />
                </li>
            </ul>
            <ul>
                <li><span>Volume:</span></li>
                <li>
                    <div class="minusButton">
                    </div>
                    <div class="slider_bar">
                        <div style="display: none;" id="slider_callout">
                        </div>
                        <a href="javascript:void(0)">
                            <div id="slider1_handle" class="slider_handle">
                            </div>
                        </a>
                    </div>
                    <div class="plusButton">
                    </div>
                </li>
            </ul>
            <ul>
                <li><span>Captioning:</span></li>
                <li>
                    <label for="CaptioningOn">
                        On
                    </label>
                    <input type="radio" checked="checked" name="Captioning" id="CaptioningOn" />
                    <label for="CaptioningOff">
                        Off
                    </label>
                    <input type="radio" name="Captioning" id="CaptioningOff" />
                </li>
            </ul>
            <%--<ul>
                <li><span>Video:</span></li>
                <li>
                    <label for="VideoOn">
                        On
                    </label>
                    <input type="radio" checked="checked" name="Video" id="VideoOn" />
                    <label for="VideoOff">
                        Off
                    </label>
                    <input type="radio" name="Video" id="VideoOff" />
                </li>
            </ul>--%>
            <%--<ul>
                <li><span>Bandwidth:</span></li>
                <li>
                    <label for="BandwidthOn">
                        High
                    </label>
                    <input type="radio" name="Bandwidth" id="BandwidthOn" />
                    <label for="BandwidthOff">
                        Low
                    </label>
                    <input type="radio" checked="checked" name="Bandwidth" id="BandwidthOff" />
                </li>
            </ul>--%>
        </div>
        <!-- <button class="button">Save</button> -->
        <button class="button">
            OK</button>
    </div>
    <!-- configuration dialogue end -->
    <div id="overlay">
    </div>
    <!-- transparent background -->
    <div id="player">
        <div id="header">
            <div id="logos" >
                <img src="images/logo_3601.gif" alt="" border="0" title="360training.com" /></div>
                
            <span id="logoutbutton" title="Save and Close" ><a href="#" tabindex="1" title="Save and Close" ></a></span>
            <div style="margin: 0px;" ><!-- height: 30px; -->
            <span id="CourseMessage" style="display:none; text-align:right; margin-right:210px;">Please wait while your course is being saved...</span>
            </div>
            <span id="logoutbuttonDS" title="Save and Close" ></span>
        </div>
        <div id="heading" style="clear: both">
            <span id="courseName"></span><span id="contentObjectName">&nbsp;</span>
        </div>
        <div class="MaskedDiv" id="masked_div">
            <div style="position: absolute; text-align: center;" id="divLoading">
                <img src="" alt="loading.." id="LoadingImage">
            </div>
        </div>
        <div id="main_container">
            <!-- accordian panel start src="images/mask_loader.gif"-->
            <div id="panelholder" >
                <div id="panel" >
                    <h3>
                        <a href="#" >Table of Content</a></h3>
                    <div id="toc" >
                    </div>
                    <h3 id="spMaterialh3">
                        <a href="#">Course Material</a></h3>
                    <div id="spMaterial">
                    </div>
                    <h3 id="glossh3">
                        <a href="#">Glossaries</a></h3>
                    <div id="gloss">
                    </div>
                    <h3 id="bookmarksh3">
                        <a href="#">Bookmarks</a></h3>
                    <div id="bookmarks">
                    </div>
                </div>
                <div id="panelbutton" class="arrowLeft">
                    <%--Yasin code--%>
                </div>
            </div>
            <!-- accordian panel end -->
            <div id="content_container">
                <div id="content">
                    <!-- Irfan Question Start .find('h3').html()eq(0). -->
                    <div id="quiz_container">
                        <div id="quizheader">
                            <!-- head -->
                            <h1>
                                Question 1 of 10</h1>
                            <div id="questionfeedback">
                                Question Feedback</div>
                            <div align="right" id="toogleFlag" style="padding-right: 20px">
                                <a href="#" style="background-color: White; border-bottom: none; border-right: none;
                                    border-left: none; border-top: none; outline: none; ie-dummy: expression(this.hideFocus=true);"
                                    onclick="action();">
                                    <img title="Click to flag this question for review." src="\Images\\Flag1.png" width="28px"
                                        id="ImageButton1" alt='NoImage' style="background-color: White; border-bottom: none;
                                        border-right: none; border-left: none; border-top: none; outline: none; ie-dummy: expression(this.hideFocus=true);" />
                                </a>
                            </div>
                        </div>
                        <div id="assessmentQuestionTemplate">
                        </div>
                    </div>
                    <!-- Irfan Question End -->
                    <!-- Student Authentication dialogue -->
                    <div id="studentAuthentication">
                        <%--<h2>Validation</h2>--%>
                        <div id="authenticationHeading">
                            <!-- head -->
                            <div id="authenticationIcon">
                                &nbsp;</div>
                            <h1>
                                Validation Azhar Question</h1>
                        </div>
                        <p>
                            In order to continue in this course you must validate your identity. Please answer
                            the question below and click on the 'Validate My Identify' button to continue. If
                            you have forgotten the answer to the question please click on the 'Forgot My Answers'
                            button below.
                        </p>
                        
                        <span id="validationNote" style="padding:15px">Your answer must exactly match the answer you entered in your profile.</span>
                        
                        <h3>
                            What is the last four digits of your home telephone number?</h3>
                        <label for="authenticateText" style="padding-left: 30px;">
                            <span id="SpanAnswer"> Answer:</span>
                            <input type="text" id="authenticateText" value="Answer here the above question" /><select
                                id="authenticateSelect"></select></label>
                        <%-- <button class="button">Validate My Identity</button><button class="button">Forgot My Answers</button>--%>
                        <%--<div id="authenticationTimer"><span></span><strong>Time Remaining to Answer:</strong></div>--%>
                    </div>
                    <!-- Student Authentication dialogue -->
                    <div id="QuestionRemediationContainer">
                        <div id="QuestionRemediationContainerHeader" style="clear: both">
                            <!-- head -->
                            <h1>
                                Question 1 of 10</h1>
                            <div id="QuestionRemediationFeedback">
                                &nbsp;</div>
                        </div>
                        <div id="questionRemediationTemplate">
                        </div>
                    </div>
                    <!-- Irfan Assessment Result Start -->
                    <div id="assesmentcontainer">
                        <div id="assessmentincomplete">
                            <h1>
                                Assessment Incomplete</h1>
                        </div>
                        <div id="assessmentcontent">
                            <p>
                            </p>
                        </div>
                        <div id="buttoncontainer">
                            <div class="floatleft">
                                <button class="button">
                                    Answer Remaining Questions</button></div>
                            <div class="floatright">
                                <button class="button">
                                    Continue Grading Without Answering</button></div>
                        </div>
                    </div>
                    <!-- Irfan Assessment Result End -->
                    <!-- Irfan ShowProctorMessage Start -->
                    <div id="ShowProctorMessageContainer">
                        <div id="ShowProctorMessageHeading">
                            <h1>
                            </h1>
                        </div>
                        <div id="ShowProctorMessageContent">
                            <p>
                            </p>
                        </div>
                        <div id="ShowProctorMessageButtons" style="position: relative; bottom: 3px;">
                            <div class="floatright">
                                <button class="button">
                                    Begin Assessment</button></div>
                        </div>
                    </div>
                    <!-- Irfan ShowProctorMessage End -->
                    <div id="AnswerReviewContainer">
                        <div id="AnswerReviewHeading">
                            <h1>
                            </h1>
                        </div>
                        <div id="AnswerReviewMessage">
                            <p>
                            </p>
                        </div>
                        <div id="AnswerReviewContent">
                        </div>
                        <div id="AnswerReviewButtons">
                            <div class="floatright">
                                <button class="button">
                                    Finish Grading My Assessment</button>
                            </div>
                        </div>
                    </div>
                    <!--
                    <div id="IndividualScoreContainer">
                        <div id="IndividualScoreHeading">
                            <h4>
                            </h4>
                        </div>
                        <div id="IndividualScoreContent">
                        </div>
                        <div id="IndividualScoreButtons">
                            <div class="floatright">
                                <button class="button">
                                    Show Answer</button>
                            </div>
                        </div>
                    </div>
                    -->
                    <!-- Irfan Start Score Summary -->
                    <!-- Irfan Assessment Result End -->
                    <!-- Irfan Assessment Result End -->
                    <div id="assessment_result_container">
                        <div id="assessment_score_Summary_subheading">
                        </div>
                        <div id="assessment_result">
                            <h1>
                            </h1>
                            <h4>
                                Below is the assessment score summary
                            </h4>
                        </div>
                        <div id="assessment_result_content">
                            <div style="margin-left: 10px;">
                                <br />
                                <h2>
                                </h2>
                                <h3>
                                </h3>
                            </div>
                        </div>
                        <div id="exam_assessment_result">
                            <h1>
                            </h1>
                            <h4>
                                Below is the assessment score summary
                            </h4>
                        </div>
                        <div id="exam_assessment_result_content">
                            <div style="margin-left: 10px;">
                                <h2>
                                </h2>
                                <h3>
                                </h3>
                            </div>
                        </div>
                        <!--LCMS-7422 Topic Score Summary Chart Container - Start -->
                        <div id="TopicScoreChartContainer">
                            <div id="TopicScoreChartHeading">
                            </div>
                            <div id="TopicScoreChartContent">
                                <table id="TopicScoreChartContentTable" cellpadding="2" cellspacing="10">
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            <div id="TopicScoreChartFooter">
                            </div>
                        </div>
                        <!--LCMS-7422 Topic Score Summary Chart Container - End -->
                        <div id="IndividualScoreContainer">
                            <div id="IndividualScoreHeaderText">
                            </div>
                            <br />
                            <div id="IndividualScoreHeading">
                            </div>
                            <div id="IndividualScoreContent">
                            </div>
                            <br />
                            <div id="IndividualScoreButtons">
                                <!-- <div>class="floatright"> -->
                                <button class="button">
                                    Show Answer</button>
                                <!--</div>-->
                            </div>
                        </div>
                    </div>
                    <!-- Irfan Start Score Summary -->
                    <!-- Irfan Start Score Summary -->
                    <!-- Irfan End Scorey -->
                    <!-- PROCTOR LOGIN SCREEN (START) -->
                    <div id="proctor_login_screen" style="left: 0px; position: absolute;">
                        <table cellspacing="0" cellpadding="0" border="0" class="ProctorLoginScreenTable">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="10" border="0" class="ProctorLoginScreenTable">
                                        <tr>
                                            <td>
                                                <div id="proctorLoginIcon">
                                                </div>
                                            </td>
                                            <td>
                                                <div id="proctorLoginHeading">
                                                    <h2>
                                                        ---
                                                    </h2>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="10" border="0" class="ProctorLoginScreenTable">
                                        <tr>
                                            <td>
                                                <div id="proctorLoginScreenMessage">
                                                    ...</div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <br />
                                        <br />
                                        <div style="background-color: #EAEAEA; width: 600px; text-align: left; border: 1px solid #eaeaea;">
                                            <!-- <table cellspacing="0" cellpadding="5" border="0" class="ProctorLoginScreenTable"> -->
                                            <table cellspacing="0" cellpadding="5" border="0" style="padding: 0px 0px 0px;">
                                                <tr>
                                                    <td>
                                                        <div id="ProctorLoginTableText">
                                                            PROCTOR LOGIN
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="width: 600px; border-left: 1px solid #eaeaea; border-right: 1px solid #eaeaea;
                                            border-bottom: 1px solid #eaeaea; border-top: 0px solid #eaeaea;">
                                            <!-- <table cellspacing="0" cellpadding="5" border="0" class="ProctorLoginScreenTable"> -->
                                            <table cellspacing="0" cellpadding="5" border="0" style="padding: 0px 0px 0px;" width="500px">
                                                <tr>
                                                    <td colspan="4" style="height: 10px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 20%">
                                                        <div id="ProctorLoginIDText">
                                                            Proctor ID
                                                        </div>
                                                    </td>
                                                    <td align="left">
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td align="left">
                                                        <input type="text" id="proctorLogin" style="width: 220px;" name="proctorLogin" />
                                                    </td>
                                                    <td align="left">
                                                        <font color="red" size="1px">
                                                            <div id="proctorLoginErrorMessage" style="width: 150px;">
                                                                The Proctor ID is a required field.
                                                            </div>
                                                        </font>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td style="height: 5px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 5px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 5px;" align="left" valign="top">
                                                        <font color="red" size="1px">(e.g. PROC-XXXXXXXX-CODE)</font>
                                                    </td>
                                                    <td style="height: 5px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 20%">
                                                        <div id="ProctorPasswordText">
                                                            Password
                                                        </div>
                                                    </td>
                                                    <td align="left">
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td align="left">
                                                        <input type="password" style="width: 220px;" id="proctorPassword" name="proctorPassword" />
                                                    </td>
                                                    <td align="left">
                                                        <font color="red" size="1px">
                                                            <div id="proctorPasswordErrorMessage" style="width: 150px;">
                                                                The Password is a required field.</div>
                                                        </font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2" style="font-size: 10px" align="left">
                                                        <b id="attemptMessage"></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 10px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <%-- <div style="float:left;" id="icoAutheticating"></div>--%>
                                                        <div style="vertical-align: top; padding-top: 10px; margin: 0 auto; overflow: hidden;
                                                            width: 80px;">
                                                            <span class="btn-start"></span><span id="btnProctorLoginSubmit"></span><span class="btn-end">
                                                            </span>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <div id="proctorErrorLabel">
                                            Invalid Login name
                                        </div>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- PROCTOR LOGIN SCREEN (END) -->
                    <div id="CARealStateValidation">
                    </div>
                    <div id="NYInsuranceValidation">
                    </div>
                    <div id="assessmentItemResult">
                    </div>                    
                    <!-- LCMS-11870 starts -->
                    <div id="JSPlayerContainer">
                    </div>
                    <!-- LCMS-11870 ends -->
                    <div id="htmlContentContainer">
                    </div>
                    <!-- LCMS-2857 -->
                    <div id="EOCInstructions">
                        <div id="EOCDiv1">
                        </div>
                        <div id="EOCDiv2">
                        </div>
                        <div id="EOCDiv3">
                        </div>
                    </div>
                    <!--LCMS-3385 PlayerEnableCourseReview -->
                    <div id="EOCText">
                    </div>
                    <div id="swfContainer">

                        <script type="text/javascript">
                            if (AC_FL_RunContent == 0) {
                                //alert("This page requires AC_RunActiveContent.js.");
                            } else {


                                // LCMS-3061
                                //---------------------------------------------
                                var flashHeight = '100%';
                                if (navigator.appName == "Microsoft Internet Explorer") {
                                    flashHeight = '100%';
                                }
                                //---------------------------------------------

                                AC_FL_RunContent(
			'codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0',
			'width', '100%',
			'height', flashHeight,
			'src', 'player',
			'quality', 'high',
			'pluginspage', 'https://www.macromedia.com/go/getflashplayer',
			'align', 'middle',
			'play', 'true',
			'loop', 'true',
			'scale', 'showall',
			'wmode', 'transparent',
			'devicefont', 'false',
			'id', 'movie',
			'bgcolor', '#ffffff',
			'name', 'movie',
			'menu', 'true',
			'allowFullScreen', 'false',
			'allowScriptAccess', 'sameDomain',
			'movie', 'player',
			'salign', 't'
			); //end AC code
                            }
                        </script>

                        <!-- <noscript>
	<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="100%" height="100%" id="movie" align="middle">
	<param name="allowScriptAccess" value="sameDomain" />
	<param name="allowFullScreen" value="false" />
	<param name="movie" value="player.swf" />
    <param name="quality" value="high" />
    <param name="bgcolor" value="#ffffff" />
    <embed src="player.swf" quality="high" bgcolor="#ffffff" width="100%" height="100%" name="movie" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
	</object>
</noscript> -->
                    </div>
                </div>
                <!-- content -->
                <div id="CourseCompletionReport" style="display: none;">
                    <div id="CourseCompletionReportLoading">
                        Course Completion Report Loading...</div>
                    <div id="CourseCompletionReportHeading">
                        Course Completion Report</div>
                    <br />
                    <table cellspacing="0" cellpadding="0" style="left: 0px; padding-top: 0px; padding-bottom: 0px;
                        padding-right: 0px; padding-left: 10px; margin: auto; width: 95%; z-index: 12;">
                        <tr>
                            <td>
                                <div id="CourseCompletionReportDetail">
                                    <h3>
                                        Course Completion Report</h3>
                                    Below is the summary of the course completion report. You must meet all the requirements
                                    to successfully complete the course. You can view this report anytime during the
                                    course. Click on the "Completion Report" button at the bottom of the screen to continue
                                    your course.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <div id="CourseCompletionReportGenericInfo">
                                    <b>Note:</b> Requirements marked
                                    <img id="CourseCompletionHeading_ImgCorrect" src="../images/ico_correct.png" alt="">
                                    have been completed. Requirements marked
                                    <img id="CourseCompletionHeading_ImgIncorrect" src="../images/ico_incorrect.png"
                                        alt="">
                                    are pending completion.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                                <table style="width: 100%; padding: 0px;" align="left" cellspacing="0" cellpadding="0"
                                    border="0" id="CourseCompletionReportTable">
                                    <tbody>
                                        <tr id="IsViewEverySceneInCourseEnabledRow" bgcolor="#FFFFFF" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <!-- style="border-left: 1px solid rgb(221,221,221); border-top: 1px solid rgb(221,221,221);border-bottom: 1px solid rgb(221,221,221);padding-left: 7px;"> -->
                                                <div id="IsViewEverySceneInCourseAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 5px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsViewEverySceneInCourseAchievedText">
                                                    You must view every scene in the course.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#F0F0F0" id="IsCompleteAfterNOUniqueCourseVisitEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsCompleteAfterNOUniqueCourseVisitAchieved" style="width: 17px; height: 25px;
                                                    margin-top: 7px; margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsCompleteAfterNOUniqueCourseVisitAchievedText">
                                                    The course can only be completed after at least 1 course launch.</div>
                                                <%-- You launched the course 3 times.--%>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsPostAssessmentEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsPostAssessmentAttempted" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsPostAssessmentAttemptedText">
                                                    You must attempt post assessment in order to complete this course.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#F0F0F0" id="IsPostAssessmentMasteryEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsPostAssessmentMasteryAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsPostAssessmentMasteryAchievedText">
                                                    You must pass the post assessment with a mastery of 80%. You have not attempted
                                                    the post assessment yet.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsQuizMasteryEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsQuizMasteryAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsQuizMasteryAchievedText">
                                                    You must pass the quiz with a mastery of 70%.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsPreAssessmentMasteryEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsPreAssessmentMasteryAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsPreAssessmentMasteryAchievedText">
                                                    You must pass the pre assessment with a mastery of 70%.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#F0F0F0" id="IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow"
                                            style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess" style="width: 17px;
                                                    height: 25px; margin-top: 7px; margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText">
                                                    You must complete the course within 48 hours after first course launch.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow"
                                            style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate" style="width: 17px;
                                                    height: 25px; margin-top: 7px; margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText">
                                                    You must complete the course within 365 days After registration.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#F0F0F0" id="IsembeddedAcknowledgmentEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsembeddedAcknowledgmentAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsembeddedAcknowledgmentAchievedText">
                                                    Must Accept Embedded Acknowledgement.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsRespondToCourseEvaluationEnabledRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsRespondToCourseEvaluationAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsRespondToCourseEvaluationAchievedText">
                                                    You must complete all evaluations.</div>
                                            </td>
                                        </tr>
                                        <%--For LCMS-11284--%>
                                        <tr bgcolor="#FFFFFF" id="IsAcceptAffidavitAcknowledgmentRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsAcceptAffidavitAcknowledgmenAchieved" style="width: 17px; height: 25px;
                                                    margin-top: 7px; margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsAcceptAffidavitAcknowledgmenAchievedText">
                                                    You must accept affidavit acknowledgment.</div>
                                            </td>
                                        </tr>
                                        <tr bgcolor="#FFFFFF" id="IsSubmitSignedAffidavitRow" style="display: none">
                                            <td width="17" height="25" align="center" class="table-row">
                                                <div id="IsSubmitSignedAffidavitAchieved" style="width: 17px; height: 25px; margin-top: 7px;
                                                    margin-left: 3px; margin-right: 0px;" class="icon-incorrect">
                                                    &nbsp;</div>
                                            </td>
                                            <td style="width: 100%; border-right: 1px solid #CCCCCC;" class="table-row">
                                                <div id="IsSubmitSignedAffidavitAchievedText">
                                                    You must submit a signed affidavit.</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <div id="loading" style="padding-left: 400px; padding-top: 200px">
                        <p>
                            <img src="images/loading.gif" />Please Wait</p>
                    </div>
                    <div id="CourseApproval">
                    </div>
                </div>
                <div id="AudioMobile" style="display: none; left: 68%; position: fixed; bottom: 60px;">
                </div>
            </div>
            <div id="ValidationControlBar">
                <div id="ValidationControlPanel">
                    <div id="ValidationPlaybuttonEn">
                        <a href="#" title="Next"></a>
                    </div>
                    <div id="ValidationTimer">
                        00:00</div>
                </div>
            </div>
            <div id="controlBar">
                <div id="controlPanel">
                    <ul id="CourseMenuIcons">
                        <li id="IcoOptions"><a href="#" title="Menu Button" tabindex="2"></a></li>
                        <li id="IcoOptionsDs" style="display: none;"><a title="Button is disabled" tabindex="-1"></a></li>
                        <li id="IcoBookMark"><a href="#" title="Bookmark Button"  tabindex="3"></a></li>
                        <li id="IcoBookMarkDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                        <li id="IcoConfigure"><a href="#" title="Configuration Button"  tabindex="4"></a></li>
                        <li id="IcoConfigureDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                        <li id="IcoCourseCompletion"><a href="#" title="Course Completion Report"  tabindex="5"></a></li>
                        <li id="IcoCourseCompletionDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                        <li id="IcoAmazonAffiliatePanel" style="display: none"><a href="#" title="Click to see/close Amazon book suggestions."  tabindex="6"></a></li>
                        <li id="IcoAmazonAffiliatePanelDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                        <%-- Abdus Start LCMS-11878--%>
                        <li id="IcoRecommendationCoursePanel" style="display: none"><a href="#" title="Click to see/close course suggestions."  tabindex="7"></a></li>
                        <li id="IcoRecommendationCoursePanelDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                        <%-- Abdus Stop LCMS-11878--%>
                        <li id="IcoHelp"><a href="#" title="Help Button"  tabindex="8"></a></li>
                        <li id="IcoHelpDs" style="display: none;"><a title="Button is disabled"  tabindex="-1"></a></li>
                    </ul>
                    <div id="progressBarContainter">
                        <div id="progressbar">
                        </div>
                        <div id="progressBarTxt">
                           </div>
                    </div>
                    <div id="PlaybuttonEn">
                        <a href="#" title="Next" tabindex="9"></a>
                    </div>
                    <div id="PlaybuttonDs">
                        <span title="Button is disabled" ></span>
                    </div>
                    <div id="BackbuttonEn">
                        <a href="#" title="Previous" tabindex="10"></a>
                    </div>
                    <div id="BackbuttonDs">
                        <span title="Button is disabled" ></span>
                    </div>
                    <div id="timer">
                        00:00</div>
                    <div id="ShowQuestionButton">
                        <button class="button">
                            Show Question</button></div>
                </div>
                <div id="odometerContainter">
                    <div id="odometerTxt">
                    </div>
                    <div id="odometer">
                    </div>
                </div>
                <div id="assessmentControlPanel">
                    <div id="QuestionRemediationButtons">
                        <div class="floatleft">
                            <button class="button" id="ShowContentRemediation" style="margin-right:15px;">
                                Show Contents</button></div>
                        <div class="floatleft">
                            <button class="button" id="ReturnToAssessmentResult">
                                Return to Assessment Results</button></div>
                    </div>
                    <div id="buttoncontainerAnswerReview" style="display: none;">
                        <div id="buttoncontainerAnswerReviewPage" class="floatright" style="display: none;">
                            <button class="button">
                                Return to Answer Review Page</button>
                        </div>
                    </div>
                    <div id="NextQuestionButtonEn">
                        <a href="#" title="Next"></a>
                    </div>
                    <div id="NextQuestionButtonDs">
                        <span title="Button is disabled"></span>
                    </div>
                    <div id="BackQuestionButtonEn">
                        <a href="#" title="Previous"></a>
                    </div>
                    <div id="GradeAssessment">
                        <button class="button">
                            Grade Assessment</button></div>
                    <div id="assessmentTimer">
                        00:00</div>
                </div>
            </div>
        </div>
        <div id="footer" >
            <div class="copyRight" style="width: 75%; float: left">
                </div>
            <div id="InstructorInformation" style="width: 25%; float: right; text-align: right;
                display: none;">
                <a href="#" onclick='javascript:window.open("InstructorInfo.aspx", "Instructors", "location=1,toolbar=1,status=0,scrollbars=1,  width=800,height=370")'
                    style="font-family: Tahoma,Arial,Verdana,Helvetica,sans-serif; font-size: 14px">
                    Instructor Information</a>
            </div>
        </div>
    </div>
    <!-- End Loading Course Player Frame Work -->
    <span id="AssessmentInProgress" style="height: 0px; width: 0px; display: block">
    </span>
    <%-- <div id="blink">Hello World!</div>--%>

    <script type="text/javascript">

        var cp = new CoursePlayerEngine();
        var courseApproval = false;
        var isLockoutClickAwayToActiveWindowStart = false;
        var active_element; // using in Lockout Functionality Clicking Away to Active Window//
        var bIsMSIE; // using in Lockout Functionality Clicking Away to Active Window//
        var isSkipping = true;
        var courseApprovalSelectedvalue;
        var IsAmazonAffiliatePanel = false;
        var IsRecommendationCoursePanel = false; //Abdus Samad //LCMS-11878	 
        var myPanel;
        var globalshowInstructorInfo = false;
        var globalinitheight = 0;
        var bIsContentResize = false;
        var IsAssessmentIncomplete = false;
        var restrictIncompletJSTemplate=false;
        var overlay = "#overlay";
        var lockoutTimeOutVar;
        var blinkInterval;
        function completed() {
            cp.getQueryString();
            cp.init();
            cp.initHandlers();
        }

        function onBlur() {

            if (bIsMSIE && (active_element != document.activeElement)) {
                //LCMS-10870
                lastKeyIsCtrl = false;
                lastKeyCode = 0;
                active_element = document.activeElement;


            }
            else {
                if (lastKeyCode == 122)// || (lastKeyCode==9 && !lastKeyIsCtrl && !document.hasFocus ()) ) //F-11 OR TAB(Without Ctrl)
                {
                    lastKeyIsCtrl = false;
                    lastKeyCode = 0;
                }
                else {
                    ModelPopUpForClickAwayFunctionality();

                }
            }
        };

        function ModelPopUpForClickAwayFunctionality() {
            // Yasin LCMS-12519
            // ------------------------------------------------------
            if (cp.IsPreviewModeEnabled() == false) {

                var clickAwayTimeOutinMilliSeconds = cp.GetTimeoutValueForClickAwayLockout();


                if (lockoutTimeOutVar == null) {
                    $("#focusOutLockedOutMessage .warning").text(assessment_focusOutLockedOutMessage_warning);
                    $("#focusOutLockedOutMessageText").html(assessment_focusOutLockedOutMessage_text);
                    $("#focusOutLockedOutMessageTimer").html(assessment_focusOutLockedOutMessage_timer);
                    $("#focusOutLockedOutButtonNo").text(cancel_text);
                    $("#focusOutLockedOutButtonYes").text(assessment_focusOutLockedOutButtonYes_text);

                    $("#focusOutLockedOutMessage").fadeIn("slow");
                    $(overlay).css({ "opacity": "0.7" });
                    $(overlay).fadeIn("slow");

                    document.getElementById("spanClickAwayTimer").innerHTML = (clickAwayTimeOutinMilliSeconds / 1000);

                    blinkingCheck = true;
                    StartBlinking(assessment_focusOutLockedOutMessage_warning);

                    lockoutTimeOutVar = setInterval(function() {

                        document.getElementById("spanClickAwayTimer").innerHTML = (parseInt(document.getElementById("spanClickAwayTimer").innerHTML) - 1);

                        if (document.getElementById("spanClickAwayTimer").innerHTML == "0") {
                            $("#focusOutLockedOutMessage").fadeOut("slow");
                            $(overlay).fadeOut("slow");
                            window.clearInterval(lockoutTimeOutVar);
                            lockoutTimeOutVar = null;
                            cp.CourseLockDueToInActiveCurrentWindow();
                            ISCourseLocked = true;
                            ClearAfterCourseLockOut();

                        }

                    }, 1000);


                    $("#focusOutLockedOutButtonYes").unbind('click.namespace');

                    $("#focusOutLockedOutButtonNo").unbind('click.namespace');

                    $("#focusOutLockedOutButtonYes").bind('click.namespace', function() {
                        $("#focusOutLockedOutMessage").fadeOut("slow");
                        $(overlay).fadeOut("slow");
                        window.clearInterval(lockoutTimeOutVar);
                        lockoutTimeOutVar = null;
                        cp.CourseLockDueToInActiveCurrentWindow();
                        ISCourseLocked = true;
                        ClearAfterCourseLockOut();
                        return false;
                    });


                    $("#focusOutLockedOutButtonNo").bind('click.namespace', function() {
                        $("#focusOutLockedOutMessage").fadeOut("slow");
                        $(overlay).fadeOut("slow");
                        window.clearInterval(lockoutTimeOutVar);
                        lockoutTimeOutVar = null;
                        return false;
                    });
                }
                // ------------------------------------------------------
            }
        };

        function ClearOnBlur() {
            return true;
        };


        function ClearAfterCourseLockOut() {

            document.onfocusout = null;
            window.onblur = null;
        };



        var originalTitle;

        var blinkTitle;

        var blinkLogicState = false;

        //var blinkHandler;

        function StartBlinking(title) {
            originalTitle = document.title;

            blinkTitle = title;

            BlinkIteration();
        }

        function BlinkIteration() {
            if (blinkLogicState == false) {
                document.title = blinkTitle;
            }
            else {
                document.title = originalTitle;
            }

            blinkLogicState = !blinkLogicState;

            blinkHandler = setTimeout(BlinkIteration, 100);


        }

        function StopBlinking() {
            if (blinkingCheck == true) {
                if (blinkHandler) {
                    clearTimeout(blinkHandler);
                }

                document.title = originalTitle;
            }
        }


        function initiateSelfLockingDueToClickingAwayToActvieWindow() {

            if (navigator.appName == "Microsoft Internet Explorer") {
                active_element = document.activeElement;
                document.onfocusout = onBlur;
                bIsMSIE = true;

            }
            else {
                window.onblur = onBlur;
            }
        }


        function windowClosed() {
            cp.windowClosed();

        }     

        /*function flashDownloaded(){
        cp.getQueryString();
        cp.init();
        cp.initHandlers();
        }*/

        //LCMS-9882
        function GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs) {
            return cp.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
        }

        //LCMS-9882
        function SaveAssessmentEndTrackingInfo_ForGameTemplate(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount) {
            cp.SaveAssessmentEndTrackingInfo_ForGameTemplate(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount);
        }

        // Waqas Zakai
        // LCMS-10318
        // Start

        function ExecuteTimer() {

            $("#examIdleTimerDialogue").fadeOut("slow");
            $(overlay).fadeOut("slow");

            var re = cp.getRenderEngineInstance();
            re.isIdleTimerReset = true;
            re.lastActivityTimeStamp = new Date();
            resetCPIdleTimer();
            cp.ResetSessionTime();

        }
        // Waqas Zakai
        // LCMS-10318
        // END

         
    </script>

</body>
</html>
