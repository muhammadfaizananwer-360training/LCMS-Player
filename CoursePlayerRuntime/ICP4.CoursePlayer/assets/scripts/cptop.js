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
            if($("#toogle-flag").hasClass('flagged'))
            {
                $("#toogle-flag").removeClass('flagged'); 
            }
            else
            {
                $("#toogle-flag").addClass('flagged'); 
            }

//        if (document.getElementById("ImageButton1").src.indexOf("Flag1.png") > -1) {
//            document.getElementById("ImageButton1").src = "assets/img/Flag2.png";
//            document.getElementById("ImageButton1").title = assessment_toggle_flag1_tx; //"Click to remove the flag on this question.";
//        }
//        else {
//            document.getElementById("ImageButton1").src = "assets/img/Flag1.png";
//            document.getElementById("ImageButton1").title = assessment_toggle_flag2_tx; //"Click to flag this question for review.";
//        }
    }
        
    function javascriptPlayerMp4(fileName, fileType) {
        j360Player.PlayerCall(fileType, fileName);
    }    
        
    function javascriptPlayer(fileName, fileType, StreamingEngine, stream) {
        j360Player.PlayerCall(fileType, fileName, StreamingEngine, stream);
    }   
    
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

/*
    function getMovieName(movieName) {
        if (navigator.appVersion.indexOf("10.0") != -1) {
            return document[movieName];
        } else if (navigator.appName.indexOf("Microsoft") != -1) {
            return window[movieName];

        } else {
            return document.embeds[movieName];
        }
    }*/
    
	function getMovieName(movieName) { 	
		if(String(document[movieName])!="undefined")
		{ 
	    	return document[movieName];
	    }
		else if(String(document.embeds[movieName]) != "undefined")
		{ 
	    	return document.embeds[movieName];
	    }
		else
		{ 
	    	return window[movieName]; 
	    }; 
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
        //alert("drag");
    }    
    
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
    
    
                