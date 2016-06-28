/* 
Course Player Engine updated October 06, 2008;
*/
var fullWidthAccordian = 1234;

/*$(window).bind('resize', function() {
var windowHeight = document.documentElement.clientHeight - 165;
$("#content_container").height(windowHeight);
	
var panelHeight = $("#content").height();
$("#panel").height(panelHeight);
$("#panel div").height(panelHeight-147);
$("#panelbutton").css('margin-top', (panelHeight/2 -20)+'px');
		
if (navigator.appName.indexOf("Microsoft") != -1) {
fullWidthAccordian = $("#content").width()-15;	
}else {
fullWidthAccordian = $("#content").width();
}

});*/


//setInterval("moreSnow()", 500);
//	function moreSnow() {
//		content_resize();
//	} 


var htmlTimer = null;


function content_resize() {
    //LCMS-10329
//    var otherElementHeight = 165;
//    if ($('#contentWrapper').length > 0) {
//        otherElementHeight += $('#contentWrapper').height();
//    }

    //Abdus Samad
    //LCMS-11878  
    //Start 
//    if ($('#contentWrapperReco').length > 0) {
//        otherElementHeight += $('#contentWrapperReco').height();
//    }
    //Stop


    //var w = $(window);
    //var H = w.height() - otherElementHeight;
    ////END LCMS-10329		
    //var W = w.width();


    //globalinitheight = H;

//    if (IsAmazonAffiliatePanel == false && bIsContentResize == true) {
//        H = globalinitheight + $('#contentWrapper').height();
//    }

    //Abdus Samad
    //LCMS-11878  
    //Start 
//    if (bIsContentResize == true && IsRecommendationCoursePanel == false) {
//        H = globalinitheight + $('#contentWrapperReco').height();
//    }
    //Stop


    //$("#content_container").height(H);

    //for html overlay div
//    var ccWidth = $("#masked_div").width() / 2;
//    var ccHeight = $("#masked_div").height() / 2;
//    var rWidth = Math.round(ccWidth) - 50;
//    var rHeight = Math.round(ccHeight) - 50;
//    $("#divLoading").css('margin-top', rHeight);
//    $("#divLoading").css('margin-left', rWidth);
    ///////////////////////


    //var panelHeight = $("#content").height();
    //var panelHeight = $("#content_container").height();
    //$("#content").height(panelHeight);
    //$("#panel").height(panelHeight);
    //$("#panel > div").height(panelHeight - 147);
    //alert(panelHeight);
    //$("#toc div").removeAttr('style');
    //$("#panelbutton").css('margin-top', (panelHeight / 2 - 20) + 'px');
    //$("#panelholder").css('width', '10px');
    //if (navigator.appName.indexOf("Microsoft") != -1) {
     //   fullWidthAccordian = $("#content").width() - 15;
    //} else {
     //   fullWidthAccordian = $("#content").width() - 10;
    //}

    //AdjustingWidthAndHeightOfTOC();

   //$( '#content' ).css( {width: W-20, height: H-20} ); 
}


/////////////Class to display loading overlays and images 
function AdjustingWidthAndHeightOfTOC() {
 
    var panelHeight =  $("#panel > div").height();

   if($('#spMaterialh3').css('display') == 'none'  && $('#glossh3').css('display') == 'none' && $('#bookmarksh3').css('display') == 'none'  )
	{
		$("#panel > div").height(panelHeight + 107);
	}	
	else 
	if($('#spMaterialh3').css('display') == 'block'  && $('#glossh3').css('display') == 'none' && $('#bookmarksh3').css('display') == 'none'  )
	{
		$("#panel > div").height(panelHeight + 67);
	}
	else
	if($('#spMaterialh3').css('display') == 'none'  && $('#glossh3').css('display') == 'block' && $('#bookmarksh3').css('display') == 'none'  )
	{
		$("#panel > div").height(panelHeight + 67);
	}
	else
	if($('#spMaterialh3').css('display') == 'none'  && $('#glossh3').css('display') == 'none' && $('#bookmarksh3').css('display') == 'block'  )
	{
		$("#panel > div").height(panelHeight + 67);
	}
	else
	if($('#spMaterialh3').css('display') == 'block'  && $('#glossh3').css('display') == 'block' && $('#bookmarksh3').css('display') == 'none'  )
	{
		$("#panel > div").height(panelHeight + 32);
	}
	else
	if($('#spMaterialh3').css('display') == 'block'  && $('#glossh3').css('display') == 'none' && $('#bookmarksh3').css('display') == 'block'  )
	{
		$("#panel > div").height(panelHeight + 32);
	}
	else
	if($('#spMaterialh3').css('display') == 'none'  && $('#glossh3').css('display') == 'block' && $('#bookmarksh3').css('display') == 'block'  )
	{
		$("#panel > div").height(panelHeight + 32);
	}
	else
	if($('#spMaterialh3').css('display') == 'block'  && $('#glossh3').css('display') == 'block' && $('#bookmarksh3').css('display') == 'block'  )
	{
		$("#panel > div").height(panelHeight - 6);
	}				
}

//use the onAfterRestore and onBeforeRestore events.

// private variables
var preloader = "#preloaderOLD";
var logoutbutton = "#logoutbutton";
var player = "#player";
var swfContainer = "#swfContainer";
var htmlContentContainer = "#htmlContentContainer";
var quiz_container = "#quiz_container";
var header = "#header";
var main_container = "#main_container";
var heading = "#heading";
var panel = "#panel";
var panelbutton = "#panelbutton";
//var toc = "#toc";
var toc = "#course-outline .items";
var spMaterial = "#spMaterial";
var gloss = "#course-glossary .items";
var course_material = "#course-material .items";
var bookmarks = "#bookmarks";
var content_container = "#content_container";
var controlPanel = "#player-options";
var assessmentControlPanel = "#assessmentControlPanel"
var IcoOptions = "#IcoOptions";
var IcoBookMark = "#modal-trigger-bookmark";
var IcoBookMarkDs = "#modal-trigger-bookmark-Ds";
var IcoHelp = "#IcoHelp";
var IcoConfigure = "#IcoConfigure";
var NextQuestionButtonEn = "#NextQuestionButtonEn";
var BackQuestionButtonEn = "#BackQuestionButtonEn";
var PlaybuttonEn = "#PlaybuttonEn";
var Redirectbutton = "#Redirectbutton";
var PlaybuttonDs = "#PlaybuttonDs";
var BackbuttonEn = "#BackbuttonEn";
var BackbuttonDs = "#BackbuttonDs";
var timer = "#timer";
var assessmentTimer = "#aTimer";
var footer = "#footer";
var overlay = "#overlay";
var dialog = "#dialog";
var glossaryDialogue = "#glossaryDialogue";
var bookmarkDialogue = "#bookmarkDialogue";
var bookmarkTitle = "#bookmarkTitle"
var configrationDialogue = "#configrationDialogue";
var studentAuthentication = "#studentAuthentication";
var lockCourseDialogue = "#lockCourseDialogue";
var timerExpireDialogue = "#timerExpireDialogue";
var RemediationControlPanel = "#RemediationControlPanel";
var NextRemediationButtonEn = "#NextRemediationButtonEn";
var BackRemediationButtonEn = "#BackRemediationButtonEn";
var CourseMenuIcons = "#CourseMenuIcons";
var ShowQuestionButton = "#ShowQuestionButton";
var disableScreen = "#disableScreen";
var demo = false;
var NextQuestionButtonDs = "#NextQuestionButtonDs";
var isPreview = false;
var isRedirect = false;
var stateVertical = 0;
var sceneID = 0;
var assetID = 0;
var courseGID = "";
var gradeAssessment = "#GradeAssessment";
var ValidationPlaybuttonEn = "#ValidationPlaybuttonEn";
var ValidationTimer = "#ValidationTimer";
var ValidationControlBar = "#ValidationControlBar";
var ProgressBarContainer = "#progressBarContainter";
var ProgressBar = "#progressbar";
var ProgressBarText = "#progressBarTxt";
var ImageProgressBarGlass = ""; //we need these two images as we are destroying and reconstructing progress bar 
var ImageProgressBarHighlight = "";
var ProgressBarTxt = "#progressBarTxt";
var AnswerReviewContainer = "#AnswerReviewContainer";
var ImageIncorrect = "";
var ImageCorrect = "";
var IAgreeButton = "#IAgreeButton";
var ContinueButton = "#continueButton";
var endCourse = "#endCourse";
var courseEvaluationInProgressTF = false;
var evalQuestions = "#EvalQuestions";
var errorMessageCloseMessage = "#close_message";
var errorMessageBox = "#message_box";
var IcoCourseCompletion = "#IcoCourseCompletion";
var IcoCourseCompletionDs = "#IcoCourseCompletionDs";
var IcoAmazonAffiliatePanel = "#IcoAmazonAffiliatePanel";
var IcoAmazonAffiliatePanelDs = "#IcoAmazonAffiliatePanelDs";
var IcoRecommendationCoursePanel = "#IcoRecommendationCoursePanel";  //Abdus Samad LCMS-11878
var IcoRecommendationCoursePanelDs = "#IcoRecommendationCoursePanelDs"; //Abdus Samad LCMS-11878	

var logOutText = "Your assessment is not complete. If you logout your assessment will be graded. All unanswered questions will be marked as incorrect";

var EndOfCSceneTF = false;
function CoursePlayerEngine() {
    var com = new CommunicationEngine();
    var renderEngine = new RenderEngine();
    var loadingEngine = new LoadingEngine();
    var cpEngine = this;



    var idleTimerObj = new IdleTimerClass();





    var isAccordianOpen = false;
    var isAccordianMax = false;
    var varient = "";
    var brandcode = "";
    var sessionID = "";
    var courseID = 0;


    var GlossaryTerm = "";


    this.resetTimer = function() {

        idleTimerObj.ResetTimer();

        //			    if( renderEngine.whenToShowIdleTimePopup >=0 );
        //			    {
        //			       renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
        //			    }
    }

    //Transaltion Start
    this.setBrandCode = function(brandcode) {
        brandcode = brandcode;
    }
    this.setVariant = function(variant) {
        varient = variant;
    }    
    //Transaltion End

    this.getRenderEngineInstance = function() {
        return renderEngine;
    }

    // Yasin LCMS-12519
    // -------------------------------------------------------------
    this.GetTimeoutValueForClickAwayLockout  = function() {
        return com.GetTimeoutValueForClickAwayLockout();
    }

    // -------------------------------------------------------------


    // Abdus Samad LCMS-12540
    //START   
    this.IsPreviewModeEnabled = function() {
    return com.IsPreviewModeEnabled();
    }  
   //STOP

    this.AuthenticateProctor = function(proctorLogin, proctorPassword) {
        var commandObject = com.AuthenticateProctor(proctorLogin, proctorPassword, function(commandObject) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(commandObject);                       
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        // cp.CommandHelper(commandObject);
    }

    this.CourseLockDueToInActiveCurrentWindow = function() {
        var commandObject = com.CourseLockDueToInActiveCurrentWindow();
		ui.slide.loader.hide(function()
		{				
		},'');           
        if (commandObject != null) {
            this.CommandHelper(commandObject);
        }
    }

  


    this.NoteTime = function() {
        // Added by Mustafa for LCMS-9278
        //----------------------------------------------------------------------------               
        com.NoteTime();
        //----------------------------------------------------------------------------
    }

    this.init = function() {
        //$("#loading").show();
        $('#swfContainer').show();
        $('#EOCText').show();
        $('#EOCInstructions').show();
        $('#content').show();
        //var windowHeight = document.documentElement.clientHeight - 165;
        //$("#content_container").height(windowHeight);

        //var panelHeight = $("#content_container").height();
        ////$("#content").height(panelHeight);
        //$("#panel").height(panelHeight);
        //$("#panel div").height(panelHeight - 147);
        //$("#panelbutton").css('margin-top', (panelHeight / 2 - 20) + 'px');

//        if (navigator.appName.indexOf("Microsoft") != -1) {
//            fullWidthAccordian = $("#content").width() - 15;

//        } else {
//            fullWidthAccordian = $("#content").width() - 10;
//        }




        $("#proctor_login_screen").hide(); // LCMS-9455
        $("#CARealStateValidation").hide();
        $("#NYInsuranceValidation").hide();

        // USED OLD COURSE PLAYER START
        //$(panel).accordion();
        //$("#panel").css('visibility', 'hidden'); //Added for 508 Compliance By Abdus Samad
        // USED OLD COURSE PLAYER END
        
        $(PlaybuttonEn).hide();
        $(BackbuttonDs).hide();
        $(configrationDialogue).hide();
        if (statusUpdateId != null || statusUpdateId != 0)
            clearInterval(statusUpdateId);

        $(preloader).hide();        
        $(disableScreen).hide();
        $(studentAuthentication).hide();
        $(assessmentControlPanel).hide();
        $(lockCourseDialogue).hide();
        //$(timerExpireDialogue).hide();
        $(ShowQuestionButton).hide();
        $(timer).hide();
        $(ValidationTimer).hide();
        $(ValidationControlBar).hide();
        $("#CourseApproval").hide();
        ////Progress bar//////////
        if (!demo)            
        {
            $(ProgressBar).attr('style','width: 0');            
        }
        else            
            $(ProgressBarContainer).hide();
            

        //$('#controlPanel ul li IcoBookMarkDs span').hide();

        //////////////////////////

        // });
        var packet = com.InitializeCoursePlayer(sessionID, brandcode, varient, courseID, demo, isRedirect, isPreview, stateVertical, sceneID, assetID, courseGID);
        this.CommandHelper(packet);

        if (courseApproval == false) {

            packet = com.CourseApprovalCheck(sessionID, brandcode, varient, courseID, demo, isRedirect, isPreview, courseGID);
            if (packet != null) {
                this.CommandHelper(packet);
            }
            else {
                courseApproval = true;
            }
            $("#loading").hide();
        }

        if (courseApproval == true) {
            packet = com.LoadCourse();

            this.CommandHelper(packet);

            //				        // LCMS-10535
            //				        var settingsPacket=com.LoadCourseSettings(); //new packet variable is used to not disturb the below code as former is being used below.
            //				         this.CommandHelper(settingsPacket);
            //				        /////End LCMS-10535 

            if (packet.CommandName != "ShowCourseLocked") {


                if (!demo) {
                    packet = com.GetTOC();
                    this.CommandHelper(packet);
                    packet = com.GetGlossary(0);
                    this.CommandHelper(packet);
                    packet = com.GetBookMark();
                    this.CommandHelper(packet);
                    packet = com.GetCourseMaterial();
                    this.CommandHelper(packet);
                    $(CourseMenuIcons).show();
                }
                else {
                    $(CourseMenuIcons).hide();
                }

                packet = com.BeginCourse();
                this.CommandHelper(packet);
            } else {
                //window.location = 'http://www.yahoo.com';

            }





            // To load Idle time Notification variables
            // ----------------------------------------------------------------
            var output = com.GetIdleTimeVariables();
            if (output != null) {
                var idleUserTime = output.split("||")[0];
                renderEngine.idleWarningTime = output.split("||")[1];
                renderEngine.idleTimeMsgHeading = output.split("||")[2];
                renderEngine.idleTimeMsgContent = output.split("||")[3];
                renderEngine.whenToShowIdleTimePopup = parseInt(idleUserTime) - parseInt(renderEngine.idleWarningTime);
                //renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
                this.resetTimer();
            }

            //   document.onmousedown=function(){if (   !$("#examIdleTimerDialogue").is(":visible")  ||  event.srcElement.id == "btnResume"){  cpEngine.resetTimer(); }};
            //     document.onmousemove=function(){if (   !$("#examIdleTimerDialogue").is(":visible")  ||  event.srcElement.id == "btnResume"){  cpEngine.resetTimer(); }};
            //document.onclick=function(){if (!$("#examIdleTimerDialogue").is(":visible")){ cpEngine.resetTimer(); }};
            document.onmousedown = function(e) { if (!$("#examIdleTimerDialogue").hasClass("modal-is-visible")) { if (navigator.appName == "Netscape") { if (e.which == 1) { renderEngine.isIdleTimerReset = true; cpEngine.resetTimer() } } else { if (event.button == 1) { renderEngine.isIdleTimerReset = true; cpEngine.resetTimer(); } } } };
            document.onkeypress = function() { if (!$("#examIdleTimerDialogue").hasClass("modal-is-visible") || event.srcElement.id == "btnResume") { renderEngine.isIdleTimerReset = true; cpEngine.resetTimer(); } };
            //document.onkeydown=function(){if (   !$("#examIdleTimerDialogue").is(":visible")  ||  event.srcElement.id == "btnResume"){  cpEngine.resetTimer(); }};
            window.onscroll = function() { if (!$("#examIdleTimerDialogue").hasClass("modal-is-visible")) { renderEngine.isIdleTimerReset = true; cpEngine.resetTimer(); } };

            // ----------------------------------------------------------------	

            $("#loading").hide();
            course_launch();
        }
    }




    this.getQueryString = function() {


         var _queryString = window.location.search.substring(1);
         //var _queryString = "SESSION=04c3e66a-2738-418c-929b-842d7f9fa727&ts=1465367461442";
         //var _queryString = "COURSEID=117960&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=ee9074c0-2f3b-4561-8c5b-ce9dbd2eaa2c"; //Yasin

        //  var _queryString = window.location.search.substring(1);
        //var _queryString = "COURSEID=104913&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=c1211f33-6531-46fe-839f-f35cc7de73b9&SCENEID=109637"; //Yasin
        //var _queryString = "SESSION=bd795340-7bde-4f62-85e5-521ee4461d79&ts=1457594082527#"; // Dual Signer

        //var _queryString = "SESSION=a3afe893-419b-4aac-acc5-75ab59c25dc4&ts=1460554096402#";


        //var _queryString = "COURSEID=96501&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=f59548f9-cdd9-458f-a0c2-71eb8e5d125d";
        //var _queryString = "COURSEID=90191&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=4f29b08c-dd35-4f30-a7df-7890f79f9b78";
        //var _queryString ="courseGUID=5e5b55a260eb48668c9f1c1c477f7dd0&DEMO=true";//zaheer new
        //var _queryString ="COURSEID=77518&VARIANT=En-US&BRANDCODE=DEFAULT&PREVIEW=true&SESSION=f993c5b3-6c1c-49d0-8e46-68d0608a80d9&svId=";//zaheer new


        if (_queryString) {
            //alert(window.location.search.substring(0));
            //alert(_queryString);
            var allParams = _queryString.split('&');

            for (var i = 0, index = -1; i < allParams.length; i++) {
                var keyValuePair = allParams[i];
                //alert(keyValuePair);
                if ((index = keyValuePair.indexOf("=")) > 0) {
                    var paramKey = keyValuePair.substring(0, index);
                    var paramValue = keyValuePair.substring(index + 1);
                    if (paramKey == "VARIANT") {
                        varient = paramValue;
                    }
                    if (paramKey == "BRANDCODE") {
                        brandcode = paramValue;
                    }

                    if (paramKey == "SESSION") {
                        sessionID = paramValue;
                    }
                    if (paramKey == "COURSEID") {
                        courseID = paramValue;
                    }
                    if (paramKey == "DEMO") {
                        demo = paramValue;
                    }
                    if (paramKey == "PREVIEW") {
                        isPreview = paramValue;

                    }

                    if (paramKey == "REDIRECT") {
                        //alert(paramKey);
                        isRedirect = paramValue;

                    }
                    if (paramKey == "svId") {
                        if (paramValue != "")
                            stateVertical = paramValue;

                    }
                    if (paramKey == "SCENEID") {
                        sceneID = paramValue;

                    }
                    if (paramKey == "ASSETID") {
                        assetID = paramValue;



                    }
                    if (paramKey == "courseGUID") {
                        courseGID = paramValue;




                    }

                }
            }
        }

    }


    this.initHandlers = function() {

        //logout button click
        //jQuery().ready(function(){	        
//        $(logoutbutton).bind('click.namespace', function() {
//            // LCMS-10928
//            //if( $('#assessmentControlPanel').is(':visible') && $('#AssessmentInProgress').is(':visible')) 
//            //LCMS-4803
//            if ($('#assessmentControlPanel').is(':visible') && $('#AssessmentInProgress').is(':visible')) {
//                // overlay
//                $(overlay).css({ "opacity": "0.7" });

//                // setting up dialog
//                $(dialog).find("h2").hide();
//                $(dialog).find("#dialogHeading h1").hide();
//                $(dialog).find("#Icon").hide();
//                $(dialog).find("p").html(logOutText);
//                $(dialog).find("button").html("OK"); //Yasin LCMS-12914

//                //$(dialog).find("button").after("&nbsp;<button class='button' type='button'>Cancel</button>");

//                $(overlay).fadeIn("slow");
//                $(dialog).fadeIn("slow");

//                //document.getElementById('dialog').focus(); //508 Complainance
//                $(dialog).find("button").html("OK").focus(); //508 Complainance
//                var confirmation = false;

//                // binding button events
//                $(dialog).find("button:contains('OK')").click(function() {
//                    confirm_dialog_cleanup();
//                    $(logoutbutton).find("a").hide();
//                    $("#logoutbuttonDS").css("display", "block");
//                    cp.SendLogoutMessage();
//                    return false;
//                });
//                //	                $(dialog).find("button:contains('Cancel')").click(function() {
//                //				        confirm_dialog_cleanup();
//                //				    });

//                function confirm_dialog_cleanup() {
//                    $(overlay).fadeOut("slow");
//                    $(dialog).fadeOut("slow", function() {
//                        $(dialog).find("h2").show();
//                        $(dialog).find("#dialogHeading h1").show();
//                        $(dialog).find("#Icon").show();
//                        $(dialog).find("p").html("");
//                        if ($(dialog).find("button:contains('Cancel')").length) {
//                            $(dialog).find("button:contains('Cancel')").remove();
//                        }
//                        $(dialog).find("button").html("");
//                    });
//                }

//                /*
//                if(!confirm("Your Assessment is not complete. If you logout your assessment will be graded. All unanswered questions will be marked as incorrect."))
//                {
//                return false;
//                }
//                */

//                if (!confirmation) return false;

//            }
//            // end
//            else {
//                //debugger;
//                //$(logoutbutton).hide();
//                $("#logoutbuttonDS").css("display", "block");
//                if (EndOfCSceneTF == true) {
//                    $("#CourseMessage").css("display", "block");
//                }
//                
//                ui.loader("show", function()
//			    {
//			        cp.SendLogoutMessage();				    			        
//			    },"saving");                
//                return false;
//            }
//        });
        
        //$(panel + " h3").eq(0).find('a').bind('click.namespace', function() { idleTimerObj.ResetTimer(); });
        //$(panel + " h3").eq(1).find('a').bind('click.namespace', function() { idleTimerObj.ResetTimer(); });
        //$(panel + " h3").eq(2).find('a').bind('click.namespace', function() { idleTimerObj.ResetTimer(); });
        //$(panel + " h3").eq(3).find('a').bind('click.namespace', function() { idleTimerObj.ResetTimer(); });
        // Play Button
        $(PlaybuttonEn).find('a').bind('click.namespace', function() {




            ////////////////////////////////////////////////////////////////////////////
            try {
                document.getElementById("Audio").innerHTML = "";
            }
            catch (e) { }
            ////////////////////////////////////////////////////////////////////////////




            //alert('PlaybuttonEn Bind()');
            idleTimerObj.ResetTimer();
            if (courseEvaluationInProgressTF == false)
            {            
                ui.slide.next(function()
			    {
			        playClick();
			    });
			}   
            else {
                ui.slide.next(function()
			    {
			        submitCourseEvaluation();
			    });  
            }

//            if (isAccordianMax) {
//                closeAccordian(panel);
//                $(panelbutton).css("display", "none");
//                isAccordianOpen = false;
//                isAccordianMax = false;
//            } else if (isAccordianOpen) {
//                offAccordian(panel);
//                $(panelbutton).css("display", "none");
//                isAccordianOpen = false;
//                isAccordianMax = false;
//            }
        });
        // Back Button
        $(BackbuttonEn).find('a').bind('click.namespace', function() {


            ////////////////////////////////////////////////////////////////////////////
            try {
                document.getElementById("Audio").innerHTML = "";
            }
            catch (e) { }
            ////////////////////////////////////////////////////////////////////////////


            //alert('PlaybuttonEn Bind()');
            idleTimerObj.ResetTimer();
            ui.slide.prev(function()
		    {
		        backClick();
		    });
//            if (isAccordianMax) {
//                closeAccordian(panel);
//                $(panelbutton).css("display", "none");
//                isAccordianOpen = false;
//                isAccordianMax = false;
//            } else if (isAccordianOpen) {
//                offAccordian(panel);
//                $(panelbutton).css("display", "none");
//                isAccordianOpen = false;
//                isAccordianMax = false;
//            }
        });


//        $(panelbutton).bind('click.namespace', function() {
//            idleTimerObj.ResetTimer();
//            if (isAccordianOpen) {
//                if (!isAccordianMax) {
//                    maxAccordian(panel);

//                    isAccordianMax = true;
//                } else {
//                    minAccordian(panel);

//                    isAccordianMax = false;
//                }
//            }
//        });

//        $(IcoOptions).bind('click.namespace', function() {
//            idleTimerObj.ResetTimer();
//            if (!isAccordianMax) {
//                if (!isAccordianOpen) {
//                    onAccordian(panel);

//                    $(panelbutton).css("display", "block");
//                    $(panelbutton).css("left", "0px");   //Yasin code

//                    $("#panel").css('visibility', ''); //Added For 508 Compliance by Abdus Samad 

//                    isAccordianOpen = true;
//                } else {
//                    offAccordian(panel);
//                    $(panelbutton).css("display", "none");
//                    isAccordianOpen = false;
//                    $(panelbutton).css("left", "0px");   //Yasin code

//                }
//            } else {
//                closeAccordian(panel);

//                $(panelbutton).css("display", "none");
//                isAccordianOpen = false;
//                isAccordianMax = false;
//            }
//        });
        
            $(bookmarkDialogue).find("button").eq(0).bind('click.namespace', function() {
                //$(bookmarkDialogue).fadeOut("slow");
                //$(overlay).fadeOut("slow");

                getSlideInfo();
                resetCPIdleTimer();


                //jQuery().ready(function(){	
                $(bookmarkTitle).val("");

                //});	



                //$(bookmarkDialogue).find("button").eq(0).unbind('click.namespace');
                return false; // added by mustafa for LCMS-2763
            });    
//        $(IcoBookMark).bind('click.namespace', function() {
//            //$(overlay).css({ "opacity": "0.7" });
//            //$(overlay).fadeIn("slow");
//            //$(bookmarkDialogue).fadeIn("slow");
//            resetCPIdleTimer();
//            //submit button
//            $(bookmarkDialogue).find("button").eq(0).bind('click.namespace', function() {
//                //$(bookmarkDialogue).fadeOut("slow");
//                //$(overlay).fadeOut("slow");

//                getSlideInfo();
//                resetCPIdleTimer();


//                //jQuery().ready(function(){	
//                $(bookmarkTitle).val("");

//                //});	



//                $(bookmarkDialogue).find("button").eq(0).unbind('click.namespace');
//                return false; // added by mustafa for LCMS-2763
//            }); //bookMark button end

//            //cancel button
////            $(bookmarkDialogue).find("button").eq(1).bind('click.namespace', function() {
////                $(bookmarkDialogue).fadeOut("slow");
////                $(overlay).fadeOut("slow");
////                resetCPIdleTimer();
////                //jQuery().ready(function(){	
////                $(bookmarkTitle).val("");

////                //});	
////                $(bookmarkDialogue).find("button").eq(1).unbind('click.namespace');
////                return false; // added by mustafa for LCMS-2763
////            }); 
//            //bookMark button end	


//        });

//        $(IcoCourseCompletion).bind('click.namespace', function() {
//            //$(overlay).css({"opacity": "0"});
//            //$(overlay).fadeIn("slow");	
//            //$("#CourseCompletionReport").fadeIn("slow");
//            //$(htmlContentContainer).html($("#CourseCompletionReport").html()); 
//            if ($("#CourseCompletionReport").is(":visible")) {
//                $("#content").show();
//                $("#CourseCompletionReport").hide();

//                //LCMS-11504
//                //$('#contentWrapper').show();
//                //$('#affiliatePanelWrapper').show();                           
//            }
//            else {
//                $("#content").hide();
//                $("#CourseCompletionReport").show();

//                $("#CourseCompletionReport").find("#CourseCompletionReportHeading").hide();
//                $("#CourseCompletionReport").find("#CourseCompletionReportDetail").hide();
//                $("#CourseCompletionReport").find("#CourseCompletionReportTableDiv").hide();
//                $("#CourseCompletionReport").find("#CourseCompletionReportLoading").show();
//                var CourseCompletionReportPacket = com.GetCourseCompletionReport();

//                $("#CourseCompletionReport").find("#CourseCompletionReportLoading").hide();
//                $("#CourseCompletionReport").find("#CourseCompletionReportHeading").show();
//                $("#CourseCompletionReport").find("#CourseCompletionReportDetail").show();
//                $("#CourseCompletionReport").find("#CourseCompletionReportTableDiv").show();

//                $("#CourseCompletionReport").find("#CourseCompletionHeading_ImgCorrect").attr("src", ImageCorrect); //.css('backgroundImage',"url('"+ImageCorrect+"')");
//                $("#CourseCompletionReport").find("#CourseCompletionHeading_ImgIncorrect").attr("src", ImageIncorrect); //.css('backgroundImage',"url('"+ImageIncorrect+"')");
//                //TEXT
//                $("#CourseCompletionReportHeading").text(CourseCompletionReportPacket.CourseCompletion.CourseCompletionReportText);
//                $("#CourseCompletionReportDetail").html(CourseCompletionReportPacket.CourseCompletion.CourseCompletionReportSummaryText);
//                $("#CourseCompletionReportGenericInfo").html(CourseCompletionReportPacket.CourseCompletion.CourseCompletionReportInfo1);
//                //TEXT   

//                //$("#CourseCompletionReport").find("tt1").remove();
//                //alert($("#CourseCompletionReport").find("IsViewEverySceneInCourseAchievedRow").hide());
//                //$("#CourseCompletionReport").find("IsViewEverySceneInCourseAchievedRow").addAttr('style','display:none;');

//                if (CourseCompletionReportPacket != null) {
//                    if (CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchieved == true) {
//                            //$("#CourseCompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitAchieved").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitAchieved").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsCompleteAfterNOUniqueCourseVisitAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                        { $('#CourseCompletionReport').find("#IsCompleteAfterNOUniqueCourseVisitAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')"); }

//                        /*if (isPreview) {
//                        CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchievedText = CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchievedText.replace(".", " (not applicable for preview mode).");
//                        }*/

//                        $("#CourseCompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchievedText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentAchieved == true) {
//                            //$("#CourseCompletionReport").find("#IsembeddedAcknowledgmentAchieved").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsembeddedAcknowledgmentAchieved").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsembeddedAcknowledgmentAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                            $('#CourseCompletionReport').find("#IsembeddedAcknowledgmentAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");

//                        $("#CourseCompletionReport").find("#IsembeddedAcknowledgmentAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentAchievedText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess == true) {
//                            //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                        { $('#CourseCompletionReport').find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess").css('backgroundImage', "url('" + ImageIncorrect + "')"); }

//                        /*if (isPreview) {
//                        CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText = CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText.replace(".", " (not applicable for preview mode).");
//                        }*/

//                        $("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText").html(CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate == true) {
//                            //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                        { $('#CourseCompletionReport').find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate").css('backgroundImage', "url('" + ImageIncorrect + "')"); }

//                        /*if (isPreview) {
//                        CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText = CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText.replace(".", " (not applicable for preview mode).");
//                        }*/

//                        $("#CourseCompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText").html(CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsPostAssessmentEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsPostAssessmentEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentAttempted == true) {
//                            //$("#CourseCompletionReport").find("#IsPostAssessmentAttempted").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsPostAssessmentAttempted").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsPostAssessmentAttempted").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                            $('#CourseCompletionReport').find("#IsPostAssessmentAttempted").css('backgroundImage', "url('" + ImageIncorrect + "')");

//                        $("#CourseCompletionReport").find("#IsPostAssessmentAttemptedText").html(CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentAttemptedText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsPostAssessmentMasteryEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsPostAssessmentMasteryEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryAchieved == true) {
//                            //$("#CourseCompletionReport").find("#IsPostAssessmentMasteryAchieved").removeAttr('class');
//                            //$("#CourseCompletionReport").find("#IsPostAssessmentMasteryAchieved").addClass('icon-correct');
//                            $('#CourseCompletionReport').find("#IsPostAssessmentMasteryAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                            $('#CourseCompletionReport').find("#IsPostAssessmentMasteryAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");
//                        $("#CourseCompletionReport").find("#IsPostAssessmentMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryAchievedText);
//                    }

//                    if (CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryEnabled == true) {
//                        //$("#CourseCompletionReport").find("#IsPreAssessmentMasteryEnabledRow").attr("style","display:block;");
//                        $("#CourseCompletionReport").find("#IsPreAssessmentMasteryEnabledRow").show();
//                        if (CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryAchieved == true) {
//                            $('#CourseCompletionReport').find("#IsPreAssessmentMasteryAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                        }
//                        else
//                            $('#CourseCompletionReport').find("#IsPreAssessmentMasteryAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");
//                    }

//                    $("#CourseCompletionReport").find("#IsPreAssessmentMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryAchievedText);
//                }

//                if (CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryEnabled == true) {
//                    //$("#CourseCompletionReport").find("#IsQuizMasteryEnabledRow").attr("style","display:block;");
//                    $("#CourseCompletionReport").find("#IsQuizMasteryEnabledRow").show();
//                    if (CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryAchieved == true) {
//                        //$("#CourseCompletionReport").find("#IsQuizMasteryAchieved").removeAttr('class');
//                        //$("#CourseCompletionReport").find("#IsQuizMasteryAchieved").addClass('icon-correct');
//                        $('#CourseCompletionReport').find("#IsQuizMasteryAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                    }
//                    else
//                        $('#CourseCompletionReport').find("#IsQuizMasteryAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");

//                    $("#CourseCompletionReport").find("#IsQuizMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryAchievedText);
//                }

//                if (CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationEnabled == true) {
//                    //$("#CourseCompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").attr("style","display:block;");
//                    $("#CourseCompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").show();
//                    if (CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationAchieved == true) {
//                        //$("#CourseCompletionReport").find("#IsRespondToCourseEvaluationAchieved").removeAttr('class');
//                        //$("#CourseCompletionReport").find("#IsRespondToCourseEvaluationAchieved").addClass('icon-correct');
//                        $('#CourseCompletionReport').find("#IsRespondToCourseEvaluationAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                    }
//                    else
//                        $('#CourseCompletionReport').find("#IsRespondToCourseEvaluationAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");

//                    $("#CourseCompletionReport").find("#IsRespondToCourseEvaluationAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationAchievedText);
//                }

//                if (CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseEnabled == true) {
//                    //$("#CourseCompletionReport").find("#IsViewEverySceneInCourseEnabledRow").attr("style","display:block;");
//                    $("#CourseCompletionReport").find("#IsViewEverySceneInCourseEnabledRow").show();
//                    if (CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseAchieved == true) {
//                        //$("#CourseCompletionReport").find("#IsViewEverySceneInCourseAchieved").removeAttr('class');
//                        //$("#CourseCompletionReport").find("#IsViewEverySceneInCourseAchieved").addClass('icon-correct');
//                        $('#CourseCompletionReport').find("#IsViewEverySceneInCourseAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");

//                    }
//                    else {
//                        $('#CourseCompletionReport').find("#IsViewEverySceneInCourseAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");

//                    }
//                    $("#CourseCompletionReport").find("#IsViewEverySceneInCourseAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseAchievedText);
//                }

//                // LCMS-11285
//                if (CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavitEnabled == true) {
//                    $("#CourseCompletionReport").find("#IsSubmitSignedAffidavitRow").show();
//                    if (CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavit == true) {
//                        $('#CourseCompletionReport').find("#IsSubmitSignedAffidavitAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                    }
//                    else {
//                        $('#CourseCompletionReport').find("#IsSubmitSignedAffidavitAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");
//                    }
//                    //Abdus SAMAD aDDED fOR TESTINGTHIS
//                    $("#CourseCompletionReport").find("#IsSubmitSignedAffidavitAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavitText);
//                }

//                if (CourseCompletionReportPacket.CourseCompletion.IsAcceptAffidavitAcknowledgmentEnabled == true) {
//                    $("#CourseCompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").show();
//                    if (CourseCompletionReportPacket.CourseCompletion.IsAcceptAffidavitAcknowledgment == true) {
//                        $('#CourseCompletionReport').find("#IsAcceptAffidavitAcknowledgmenAchieved").css('backgroundImage', "url('" + ImageCorrect + "')");
//                    }
//                    else {
//                        $('#CourseCompletionReport').find("#IsAcceptAffidavitAcknowledgmenAchieved").css('backgroundImage', "url('" + ImageIncorrect + "')");
//                    }
//                }

//                //LCMS-11504
//                renderEngine.HideAmazonAffiliatePanel();
//                IsAmazonAffiliatePanel = false;
//                //Abdus Samad LCMS-11878 Start
//                renderEngine.HideRecommendationCoursePanel();
//                IsRecommendationCoursePanel = false;
//                //Stop


//                bIsContentResize = true;
//                content_resize();
//                clearInterval(myPanel);
//                //End                         

//            }
//            //$(document.getElementById("IsViewEverySceneInCourseAchievedRow")).remove();



//            // This loop is to change
//            // ------------------------------------------						    
//            var table = document.getElementById('CourseCompletionReportTable');

//            var tbody = table.getElementsByTagName("tbody");
//            var tr = tbody[0].getElementsByTagName("tr");
//            var currentColor = "#ffffff";
//            var lastVisibleRowIndex = 0;
//            for (var i = 0; i < tr.length; i++) {
//                if (tr[i].style.display != "none") {
//                    lastVisibleRowIndex = i;
//                    tr[i].style.backgroundColor = currentColor;

//                    if (currentColor == "#ffffff") { currentColor = "#f0f0f0"; }
//                    else { currentColor = "#ffffff"; }
//                }
//            }

//            tr[lastVisibleRowIndex].getElementsByTagName("td")[0].style.borderBottom = "1px solid #CCCCCC";
//            tr[lastVisibleRowIndex].getElementsByTagName("td")[1].style.borderBottom = "1px solid #CCCCCC";
//            // ------------------------------------------


//        });

        // Amazon Affiliate Panel button start		    
        $(IcoAmazonAffiliatePanel).bind('click.namespace', function() {

            //if(IsAmazonAffiliatePanel==true)
            if ($("#contentWrapper").is(":visible")) {
                renderEngine.HideAmazonAffiliatePanel();
                IsAmazonAffiliatePanel = false;
                bIsContentResize = true;
                content_resize();
                clearInterval(myPanel);
                //alert('content_resize1');
            }
            else {
                if (!renderEngine.IsAmazonAffiliatePanelRendered()) {
                    renderEngine.HideRecommendationCoursePanel(); //This has been added to hide the Recommended Books Panel
                    IsRecommendationCoursePanel = false;          //This has been added to hide the Recommended Books Panel	
                    $(IcoAmazonAffiliatePanel).hide();
                    $(IcoAmazonAffiliatePanelDs).show();
                    var amazonAffiliatePanelData = com.GetAmazonAffiliatePanelData();
                    if (amazonAffiliatePanelData != null && amazonAffiliatePanelData.ShowPanel) {
                        var keywords = amazonAffiliatePanelData.CourseKeywords;
                        var affiliatePanelWSURL = amazonAffiliatePanelData.AffiliatePanelWSURL;
                        renderEngine.RenderAmazonAffiliatePanel(keywords, affiliatePanelWSURL);
                        IsAmazonAffiliatePanel = true;
                        bIsContentResize = false;
                        content_resize();
                        //alert('content_resize2');
                    }
                    //LCMS-11603 START    
                    if ($("#CourseCompletionReport").is(":visible")) {
                        $("#content").show();
                        $("#CourseCompletionReport").hide();
                        renderEngine.HideRecommendationCoursePanel(); //This has been added to hide the Recommended Books Panel
                        IsRecommendationCoursePanel = false;          //This has been added to hide the Recommended Books Panel	
                    }

                    //LCMS-11603 END

                }
                else {
                    if ($("#CourseCompletionReport").is(":visible")) {
                        $("#content").show();
                        $("#CourseCompletionReport").hide();
                    }
                    renderEngine.HideRecommendationCoursePanel(); //This has been added to hide the Recommended Books Panel
                    IsRecommendationCoursePanel = false;          //This has been added to hide the Recommended Books Panel	
                    renderEngine.ShowAmazonAffiliatePanel();
                    IsAmazonAffiliatePanel = true;
                    bIsContentResize = false;
                    content_resize();

                }


            }
        }); // Amazon Affiliate Panel button end


        // Recommandation Course Panel button start
        $(IcoRecommendationCoursePanel).bind('click.namespace', function() {
            //debugger;
            //if(IsAmazonAffiliatePanel==true)
            if ($("#contentWrapperReco").is(":visible")) {
                renderEngine.HideRecommendationCoursePanel();
                IsRecommendationCoursePanel = false;
                bIsContentResize = true;
                content_resize();
                clearInterval(myPanel);
                //alert('content_resize1');
            }

            else {
                //debugger;
                if (!renderEngine.IsRecommendationCoursePanelRendered()) {
                    renderEngine.HideAmazonAffiliatePanel(); //This has been added to hide the Amazon Affiliate Panel
                    IsAmazonAffiliatePanel = false;         //This has been added to hide the Amazon Affiliate Panel
                    $(IcoRecommendationCoursePanel).hide();
                    $(IcoRecommendationCoursePanelDs).show();
                    var recommendationCoursePanelData = com.GetCourseRecommendationPanelData();
                    if (recommendationCoursePanelData != null && recommendationCoursePanelData.ShowPanel) {
                        var keywords = recommendationCoursePanelData.CourseKeywords;
                        var affiliatePanelWSURL = recommendationCoursePanelData.AffiliatePanelWSURL;

                        renderEngine.RenderRecommendationCoursePanel(keywords, affiliatePanelWSURL);
                        IsRecommendationCoursePanel = true;
                        bIsContentResize = false;
                        content_resize();
                        //alert('content_resize2');
                    }
                    //LCMS-11603 START    
                    if ($("#CourseCompletionReport").is(":visible")) {
                        $("#content").show();
                        $("#CourseCompletionReport").hide();
                        renderEngine.HideAmazonAffiliatePanel(); //This has been added to hide the Amazon Affiliate Panel
                        IsAmazonAffiliatePanel = false;         //This has been added to hide the Amazon Affiliate Panel
                    }

                    //LCMS-11603 END

                }
                else {
                    //debugger;
                    if ($("#CourseCompletionReport").is(":visible")) {
                        $("#content").show();
                        $("#CourseCompletionReport").hide();
                    }
                    renderEngine.HideAmazonAffiliatePanel(); //This has been added to hide the Amazon Affiliate Panel
                    IsAmazonAffiliatePanel = false;         //This has been added to hide the Amazon Affiliate Panel    
                    renderEngine.ShowRecommendationCoursePanel();
                    IsRecommendationCoursePanel = true;
                    bIsContentResize = false;
                    content_resize();
                }
            }
        }); // Recommandation Course Panel button end




















//        $(IcoHelp).bind('click.namespace', function() {

//            $(overlay).css({ "opacity": "0.7" });
//            $(overlay).fadeIn("slow");
//            $("#helpDialogue").fadeIn("slow");

//            $("#helpDialogue").find("button").eq(0).bind('click.namespace', function() {
//                $("#helpDialogue").fadeOut("slow");
//                $(overlay).fadeOut("slow");
//                resetCPIdleTimer();
//                //jQuery().ready(function(){	
//                //});	
//                $("#helpDialogue").find("button").eq(0).unbind('click.namespace');
//                return false; // added by mustafa for LCMS-2763
//            }); //bookMark button end

//            resetCPIdleTimer();

//            //Abdus Samad
//            //LCMS-12526
//            //Start
//            var GetChatForHelpSupportPacket = com.GetChatForHelpSupport();

//            var ChatForHelpFirstName = GetChatForHelpSupportPacket.FirstName;

//            var ChatForHelpEmailAddress = GetChatForHelpSupportPacket.EmailAddress;

//            var ChatForHelpLastName = GetChatForHelpSupportPacket.LastName;

//            var ChatForHelpURL;

//            if (document.location.href.indexOf('/player') > 0) {

//                ChatForHelpURL = "https://livechat.boldchat.com/aid/3210241798524429162/bc.chat?resize=true&cbdid=688180042846874761&vn=" + ChatForHelpFirstName + "&ve=" + ChatForHelpEmailAddress + "&ln=" + ChatForHelpLastName;

//            }
//            else {

//                ChatForHelpURL = "https://livechat.boldchat.com/aid/449369304135401025/bc.chat?resize=true&cwdid=704598144649519636&vn=" + ChatForHelpFirstName + "&ve=" + ChatForHelpEmailAddress + "&ln=" + ChatForHelpLastName;
//            }

//            $("#ChatForHelp").attr("href", ChatForHelpURL);
//            //Stop         


//        }); // IcoHelp button end


//        $(IcoConfigure).bind('click.namespace', function() {
//            idleTimerObj.ResetTimer();
//            $(overlay).css({ "opacity": "0.7" });
//            $(overlay).fadeIn("slow");
//            $(configrationDialogue).fadeIn("slow");

//            oldSliderValue = setSlider();

//            //submit button
//            /*$(configrationDialogue).find("button").eq(0).bind('click.namespace', function(){
//            $(configrationDialogue).fadeOut("slow");
//            $(overlay).fadeOut("slow");
//            resetCPIdleTimer();
//            var temp = setSlider();
//            //alert(temp);
//            $(configrationDialogue).find("button").eq(0).unbind('click.namespace');
//            }); //configrationDialogue button end
//            */
//            //Ok button
//            $(configrationDialogue).find("button").bind('click.namespace', function() {
//                $(configrationDialogue).fadeOut("slow");
//                $(overlay).fadeOut("slow");
//                resetCPIdleTimer();

//                //alert(oldSliderValue);
//                $(configrationDialogue).find("button").unbind('click.namespace');
//            }); //configrationDialogue button end

//        });


        //});// jquery end

    }

    this.InitializeCoursePlayer = function(resourceInfo) {


        renderEngine.LocalizationRendering(resourceInfo);

    }
    this.LoadCourseRendering = function(courseInfo) {
        renderEngine.LoadCourseRendering(courseInfo, idleTimerObj);
    }

    this.ShowTOC = function(tableOfContent) {
        renderEngine.TableOfContentRendering(tableOfContent);
    }

    this.ShowGlossary = function(glossaryObject) {
        renderEngine.GlossaryRendering(glossaryObject);
    }

    this.ShowBookMark = function(bookmarkObject) {

        renderEngine.BookMarkRendering(bookmarkObject);
    }

    this.ShowCourseMaterial = function(courseMaterialObject) {

        renderEngine.CourseMaterialRendering(courseMaterialObject);
    }

    this.CustomMessageRendering = function(customMessage) {
        renderEngine.ShowCustomMessageRendering(customMessage);
    }

    this.StartAssessmentMessageRendering = function(startAssessmentMessage) {
        renderEngine.StartAssessmentMessageRendering(startAssessmentMessage);
    }
    this.ErrorMessageRendering = function(errorMessage) {
        renderEngine.ShowErrorMessageRendering(errorMessage);
    }
    this.StartAssessment = function() {
        var packet = com.StartAssessment(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
			{				
			},'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //			var packet = com.StartAssessment();
        //			//alert('StartAssessment'+packet.CommandName);
        //			this.CommandHelper(packet);
        //			//var packet = com.ShowCourseEvaluation();
        //			//this.CommandHelper(packet);

    }
    this.ResumeAssessment = function() {
        var packet = com.ResumeAssessment(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });

        //old sync code LCMS-10278
        //alert('StartAssessment'+packet.CommandName);
        //this.CommandHelper(packet);
        //var packet = com.ShowCourseEvaluation();
        //this.CommandHelper(packet);

    }

    this.SaveLearnerCourseApproval = function(courseApprovalID) {
        var courseApprovalArray = courseApprovalID.split('-');
        courseApprovalID = courseApprovalArray[1];
        var packet = com.SaveLearnerCourseApproval(sessionID, courseID, courseApprovalID);
        if (packet == -1) {
            courseApproval = false;
            alert("Due to Network issue, request cannot served by Server. Please try again");
        }
    }
    
    this.SaveLearnerCourseMessage = function(courseApprovalID) {        
        var packet = com.SaveLearnerCourseMessage(sessionID, courseID, function(packet) {
            loadingEngine.OnReadyContentArea();            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        /*var packet = com.SaveLearnerCourseMessage(sessionID, courseID);
        if (packet == -1) {
            courseApproval = false;
            alert("Due to Network issue, request cannot served by Server. Please try again");
        }*/
    }    

    this.ContinueAfterAffidavit = function(onComplete) {
        var packet = com.ContinueAfterAffidavit(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');               
            if (onComplete) {
                onComplete();
            }
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);	         
    }

    //LCMS-11281
    this.ContinueAfterDocuSignRequirementAffidavit = function(onComplete) {
        var packet = com.ContinueAfterDocuSignRequirementAffidavit(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
			{				
			},'');
            if (onComplete) {
                onComplete();
            }
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
    }
    //End

    //LCMS-11281
    this.ContinueAfterDocuSignProcess = function(onComplete) {
        var packet = com.ContinueAfterDocuSignProcess(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
            {
            },'');            
            if (onComplete) {
                onComplete();
            }
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
    }
    //End

    this.AuthenticateSpecialPostAssessmentValidation = function(DRELicenseNumber, DriverLicenseNumber) {
        var packet = com.AuthenticateSpecialPostAssessmentValidation(sessionID, courseID, DRELicenseNumber, DriverLicenseNumber, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278	        
        //  this.CommandHelper(packet);
    }

    this.CancelSpecialPostAssessmentValidation = function() {
        var packet = com.CancelSpecialPostAssessmentValidation(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);            
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);	         
    }

    this.AuthenticateNYInsuranceValidation = function(monitorNumber) {
        var packet = com.AuthenticateNYInsuranceValidation(sessionID, courseID, monitorNumber, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278        
        // this.CommandHelper(packet);
    }


    this.SkipPracticeExam = function() {
        var packet = com.SkipPracticeExam(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278		
        //this.CommandHelper(packet);		    		
    }

    this.CallGoto = function(showSlideObject, type) {
        var packet = com.Goto(showSlideObject, type, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
			{				
			},'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //alert('CallGoto:'+packet.CommandName);
        //old sync code LCMS-10278
        //this.CommandHelper(packet);
    }

    this.CallGetGlossary = function(glossaryID, glossaryTerm) {
        GlossaryTerm = glossaryTerm;
        var packet = com.LoadGlossaryItem(glossaryID);
        this.CommandHelper(packet);
    }

    this.ShowGlossaryInDetail = function(commandObject) {
        renderEngine.ShowGlossaryItem(commandObject, GlossaryTerm);
    }

    this.CallGetBookMark = function(bookMarkID) {
        var packet = com.LoadBookmark(bookMarkID, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
			{				
			},'');              
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //this.CommandHelper(packet);
    }
    
    this.reportClick = function(bookMarkID) {    
        var CourseCompletionReportPacket = com.GetCourseCompletionReport();
        
        
        if (CourseCompletionReportPacket != null) {
        
           if (CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitEnabled == true) {                       
               $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchieved == true) {     
                   $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").removeAttr('class');
                   $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").addClass('meet');
               }
               else
               {
                   $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").removeAttr('class');
                   $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitEnabledRow").addClass('not-meet');
               }
               $("#CompletionReport").find("#IsCompleteAfterNOUniqueCourseVisitAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsCompleteAfterNOUniqueCourseVisitAchievedText);
           }
           
           if (CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentEnabled == true) {
               
               $("#CompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentAchieved == true) {                           
                    $("#CompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").addClass('meet');
               }
               else
               {
                    $("#CompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsembeddedAcknowledgmentEnabledRow").addClass('not-meet');                       
               }
               $("#CompletionReport").find("#IsembeddedAcknowledgmentAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsembeddedAcknowledgmentAchievedText);
           }
           
           if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabled == true) {
               
               $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccess == true) {                       
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").addClass('meet');                           
               }
               else
               { 
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow").addClass('not-meet');
               }

               $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText").html(CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText);
           } 
           
           if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabled == true) {
               
               $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDate == true) {                       
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").addClass('meet');
               }
               else
               { 
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow").addClass('not-meet');                       
               }

               $("#CompletionReport").find("#IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText").html(CourseCompletionReportPacket.CourseCompletion.IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText);
           }  
           
           if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentEnabled == true) {
               
               $("#CompletionReport").find("#IsPostAssessmentEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentAttempted == true) {
                    $("#CompletionReport").find("#IsPostAssessmentEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPostAssessmentEnabledRow").addClass('meet');
               }
               else
               {
                    $("#CompletionReport").find("#IsPostAssessmentEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPostAssessmentEnabledRow").addClass('not-meet');                           
               }
               $("#CompletionReport").find("#IsPostAssessmentAttemptedText").html(CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentAttemptedText);
           }
           
           if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryEnabled == true) {                       
               $("#CompletionReport").find("#IsPostAssessmentMasteryEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryAchieved == true) {
                    $("#CompletionReport").find("#IsPostAssessmentMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPostAssessmentMasteryEnabledRow").addClass('meet');
               }
               else
               {
                    $("#CompletionReport").find("#IsPostAssessmentMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPostAssessmentMasteryEnabledRow").addClass('not-meet');                        
               }
               $("#CompletionReport").find("#IsPostAssessmentMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsPostAssessmentMasteryAchievedText);
           }    
           
           
           if (CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryEnabled == true) {
               
               $("#CompletionReport").find("#IsPreAssessmentMasteryEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryAchieved == true) {
                    $("#CompletionReport").find("#IsPreAssessmentMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPreAssessmentMasteryEnabledRow").addClass('meet');                           
               }
               else
               {
                    $("#CompletionReport").find("#IsPreAssessmentMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsPreAssessmentMasteryEnabledRow").addClass('not-meet');                         
               }                           
                $("#CompletionReport").find("#IsPreAssessmentMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsPreAssessmentMasteryAchievedText);                                             
           }
           
            if (CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryEnabled == true) {               
               $("#CompletionReport").find("#IsQuizMasteryEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryAchieved == true) {
                    $("#CompletionReport").find("#IsQuizMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsQuizMasteryEnabledRow").addClass('meet');                    
               }
               else
               {
                    $("#CompletionReport").find("#IsQuizMasteryEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsQuizMasteryEnabledRow").addClass('not-meet');                                   
               }
               $("#CompletionReport").find("#IsQuizMasteryAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsQuizMasteryAchievedText);
           }   
           
           if (CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationEnabled == true) {                   
               $("#CompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationAchieved == true) {
                    $("#CompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").addClass('meet');                          
               }                   
               else
               {
                    $("#CompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsRespondToCourseEvaluationEnabledRow").addClass('not-meet');
               }
               $("#CompletionReport").find("#IsRespondToCourseEvaluationAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsRespondToCourseEvaluationAchievedText);
           } 
           
           if (CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseEnabled == true) {                   
               $("#CompletionReport").find("#IsViewEverySceneInCourseEnabledRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseAchieved == true) {
                    $("#CompletionReport").find("#IsViewEverySceneInCourseEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsViewEverySceneInCourseEnabledRow").addClass('meet');
               }
               else {
                    $("#CompletionReport").find("#IsViewEverySceneInCourseEnabledRow").removeAttr('class');
                    $("#CompletionReport").find("#IsViewEverySceneInCourseEnabledRow").addClass('not-meet');
               }
               $("#CompletionReport").find("#IsViewEverySceneInCourseAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsViewEverySceneInCourseAchievedText);
           } 
           
           if (CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavitEnabled == true) {
               $("#CompletionReport").find("#IsSubmitSignedAffidavitRow").show();
               if (CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavit == true) {
                    $("#CompletionReport").find("#IsSubmitSignedAffidavitRow").removeAttr('class');
                    $("#CompletionReport").find("#IsSubmitSignedAffidavitRow").addClass('meet');                       
               }
               else {
                    $("#CompletionReport").find("#IsSubmitSignedAffidavitRow").removeAttr('class');
                    $("#CompletionReport").find("#IsSubmitSignedAffidavitRow").addClass('not-meet');                                              
               }                   
               $("#CompletionReport").find("#IsSubmitSignedAffidavitAchievedText").html(CourseCompletionReportPacket.CourseCompletion.IsSubmitSignedAffidavitText);
           }  
           
               if (CourseCompletionReportPacket.CourseCompletion.IsAcceptAffidavitAcknowledgmentEnabled == true) {
                   $("#CompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").show();
                   if (CourseCompletionReportPacket.CourseCompletion.IsAcceptAffidavitAcknowledgment == true) {
                        $("#CompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").removeAttr('class');
                        $("#CompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").addClass('meet');                       
                   }
                   else {
                        $("#CompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").removeAttr('class');
                        $("#CompletionReport").find("#IsAcceptAffidavitAcknowledgmentRow").addClass('not-meet');                       
                   }
               }
        }
        
        var completionReportHTML=$("#CompletionReport").html();        
        $('.cd-modal[data-modal="modal-dynamic"] > .cd-modal-content').removeClass('pre-loader please-wait')
	    .html(completionReportHTML);
    }    
    
    this.CallDeleteBookMark = function(bookMarkID) {    
        var packet = com.DeleteBookMark(bookMarkID);
        this.CommandHelper(packet);        
    }


    this.CallAnswerRemainingClick = function() {
        var packet = com.AnswerRemainingQuestion(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //			var packet = com.AnswerRemainingQuestion();
        //			this.CommandHelper(packet);
    }


    this.CallNextSlide = function() {            
        var packet = com.Next(function(packet) {        
            loadingEngine.OnReadyContentArea();
            if (packet != null) {
                cpEngine.CommandHelper(packet);
                //	hide slider loader                
				ui.slide.loader.hide(function()
				{	
				//	go ahead, template is ready....
				},'');

                
            }
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });

        //old sync code LCMS-10278
        //			var packet = com.Next();
        //			//alert( "<-- this.CallNextSlide -->" + packet);
        //		if(packet != null)		
        //			this.CommandHelper(packet);
    }
    this.CallBackSlide = function() {
        var packet = com.Back(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{	
			//	go ahead, template is ready....
			},'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //			var packet = com.Back();
        //			this.CommandHelper(packet);
    }

    this.ShowAnswers = function() {
        var packet = com.ShowAnswers(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);
    }


    this.ShowSlide = function(slide) {
        //debugger;
        //alert(slide.MediaAsset.MediaAssetType);		    
        $(IcoBookMark).show();
        $(controlPanel).find("#modal-trigger-bookmark").show();
        $(controlPanel).find("#cd-tour-trigger").show();
        $('.cd-nav-trigger').show();

        $('.CourseLevelRating div').remove();


        switch (slide.MediaAsset.MediaAssetType) {
            case "FlashAsset":
                {
                    $(ProgressBarContainer).show();
                    renderEngine.ShowSlideRendering(slide);
                    break;
                }
            case "ContentAsset":
                {
                    $(ProgressBarContainer).show();
                    
                    $(controlPanel).find("#IcoInstructorInformation").show();
                    $(controlPanel).find("#IcoInstructorInformationDs").hide();                    
                    
                    $(controlPanel).find("#IcoTOC").show();
                    $(controlPanel).find("#IcoTOCDs").hide();                    
                    
                    $(controlPanel).find("#IcoGlossary").show();
                    $(controlPanel).find("#IcoGlossaryDs").hide();                                        
                    
                    $(controlPanel).find("#IcoCourseMaterial").show();
                    $(controlPanel).find("#IcoCourseMaterialDs").hide();
                    
                    $('#cd-tour-trigger').show();
                    $('#modal-trigger-bookmark').show();  
        
                    $(controlPanel).find("#modal-trigger-bookmark").show();
                    $(controlPanel).find("#cd-tour-trigger").show();
                    
                    $(controlPanel).find("#IcoConfigure").show();
                    $(controlPanel).find("#IcoConfigureDs").hide();
                    
                    //$(controlPanel).find("#IcoHelp").show();
                    $(controlPanel).find("#IcoHelp").hide();
                    $(controlPanel).find("#IcoHelpDs").hide();

                    $(controlPanel).find("#IcoCourseCompletion").show();
                    $(controlPanel).find("#IcoCourseCompletionDs").hide();


                    $(PlaybuttonEn).show();
                    $(PlaybuttonDs).hide();
                    $(BackbuttonEn).show();
                    $(BackbuttonDs).hide();
                    renderEngine.ShowHTMLRendering(slide);                    
                    //LCMS-10392
                    //		                if (!renderEngine.IsAmazonAffiliatePanelRendered()) 
                    //		                    {		                        
                    //		                        var amazonAffiliatePanelData = com.GetAmazonAffiliatePanelData();
                    //		                            if (amazonAffiliatePanelData != null && amazonAffiliatePanelData.ShowPanel) 
                    //		                                {
                    //		                                    var keywords = amazonAffiliatePanelData.CourseKeywords;
                    //		                                    var affiliatePanelWSURL = amazonAffiliatePanelData.AffiliatePanelWSURL;
                    //		                                    renderEngine.RenderAmazonAffiliatePanel(keywords, affiliatePanelWSURL);		                                    
                    //		                                    
                    //		                                }
                    //		                               
                    //		                    } 
                    //		                else 
                    //		                    {
                    //		                        //renderEngine.ShowAmazonAffiliatePanel();
                    //		                    }

                    //		                Practice Exam
                    //debugger;
                    if (slide.MediaAsset.IsAssessmentStartMessage == true) {
                        renderEngine.HideAmazonAffiliatePanel();
                        IsAmazonAffiliatePanel = false;
                        //Abdus Samad Start LCMS-11878
                        renderEngine.HideRecommendationCoursePanel();
                        IsRecommendationCoursePanel = false;
                        //Stop					        
                        bIsContentResize = true;
                        content_resize();
                        $("#NYInsuranceValidation").hide();
                        $("#proctor_login_screen").hide();
                        $("#CARealStateValidation").hide();                        
                    }
                    break;
                }
            case "IntroPage":
                {
                    $(ProgressBarContainer).hide();
                    $(IcoBookMark).hide();
                    $('#security').empty();
                    renderEngine.ShowSlideRendering(slide);

                    break;
                }
            case "EndOfCourseScene":
                {
                    
                    cp.SynchToExternalSystem("EndOfCourseScene");
                    //Stop sound/video if playing
                    //LCMS-3459
                    if (document.getElementById('media') != null)
                        document.getElementById('media').innerHTML = "";
                    //$(htmlContentContainer).empty();
                    //End
                    $(IcoBookMark).hide();
                    $(ProgressBarContainer).hide();
                    renderEngine.ShowSlideRendering(slide);

                    // Added by Waqas Zakai LCMS-6575 START
                    $(controlPanel).find("#IcoInstructorInformation").show();
                    $(controlPanel).find("#IcoInstructorInformationDs").hide();                    
                    $(controlPanel).find("#IcoTOC").show();
                    $(controlPanel).find("#IcoTOCDs").hide();                    
                    $(controlPanel).find("#IcoGlossary").show();
                    $(controlPanel).find("#IcoGlossaryDs").hide();                    
                    $(controlPanel).find("#IcoCourseMaterial").show();
                    $(controlPanel).find("#IcoCourseMaterialDs").hide();                    
                    $(controlPanel).find("#modal-trigger-bookmark").hide();
                    $(controlPanel).find("#cd-tour-trigger").hide();
                    $(controlPanel).find("#IcoConfigure").show();
                    $(controlPanel).find("#IcoConfigureDs").hide();
                    //$(controlPanel).find("#IcoHelp").show();
                    $(controlPanel).find("#IcoHelp").hide();
                    $(controlPanel).find("#IcoHelpDs").hide();
                    $(controlPanel).find("#IcoCourseCompletion").show();
                    $(controlPanel).find("#IcoCourseCompletionDs").hide();

                    EndOfCSceneTF = true;
                    // LCMS-6575 END

                    $(PlaybuttonEn).hide();
                    $(PlaybuttonDs).show();
                    $(BackbuttonEn).show();
                    $(BackbuttonDs).hide();
                    $('#security').empty();
                    $(timer).hide(); // Added by Mustafa for LCMS-2617
                    if (globalshowInstructorInfo == true) {
                        $('#InstructorInformation').show();
                    }
                    break;
                }
        }
    }



    this.SendBookmarkInfo = function(slideID, sceneGUID, currentframeNumber, LastScene, isMovieEnded, nextButtonState, firstSceneName) {

        var bookMarkTitle = "";
        //jQuery().ready(function(){	
        bookMarkTitle = $(bookmarkTitle).val();

        //Replace ' with [^&*] so that query wont break .[^&*] is just a unique sequence.
        //on server side system will replace [^&*] with ' so that orignal value can be stored
        //This is important if user uses any ' in book mark name
        bookMarkTitle = bookMarkTitle.replace("'", "[^&*]");


        //});	
        if(bookMarkTitle.length >0)
        {
            var packet = com.SaveBookMark(bookMarkTitle, slideID, sceneGUID, currentframeNumber, LastScene, isMovieEnded, nextButtonState, firstSceneName);
            this.CommandHelper(packet);
        }
    }

    this.SendLogoutMessage = function() {
        com.logoutCoursePlayerIntegeration($(timer).html());
        //alert('1');
        var packet = com.logoutCoursePlayer($(timer).html());
        cp.CommandHelper(packet);
        
    }


    this.SendActionToTakeOnIdleUserTimeoutCommand = function() {
        var packet = com.GetCommandDueToIdleUserTimeout();
        cp.CommandHelper(packet);
    }


    this.GetNextQuestion = function() {
        packet = com.GetQuestion(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //this.CommandHelper(packet);	
    }
    //LCMS-11283
    //        this.OnSignCompleted = function() {
    //			 
    //			$(".DocuSignProcessText").text(sceneText3); 
    //			$('#PlaybuttonDocuSign').show();
    //			$(PlaybuttonDs).hide();

    //		}
    //End


    function HideControlBar() {
        $(controlPanel).find("#IcoInstructorInformation").show();
        $(controlPanel).find("#IcoInstructorInformationDs").hide();                    
        
        $(controlPanel).find("#IcoTOC").show();
        $(controlPanel).find("#IcoTOCDs").hide();                    
        
        $(controlPanel).find("#IcoGlossary").show();
        $(controlPanel).find("#IcoGlossaryDs").hide();                                        
        
        $(controlPanel).find("#IcoCourseMaterial").show();
        $(controlPanel).find("#IcoCourseMaterialDs").hide();
        
        $(controlPanel).find("#modal-trigger-bookmark").show();
        $(controlPanel).find("#cd-tour-trigger").show();
        
        $(controlPanel).find("#IcoConfigure").show();
        $(controlPanel).find("#IcoConfigureDs").hide();
        
        //$(controlPanel).find("#IcoHelp").show();
        $(controlPanel).find("#IcoHelp").hide();
        $(controlPanel).find("#IcoHelpDs").hide();

        $(controlPanel).find("#IcoCourseCompletion").show();
        $(controlPanel).find("#IcoCourseCompletionDs").hide();


        //$('#btnSubmit').attr("disabled", true);

        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();

        //$('#InstructorInformation').show();
    }


    //LCMS-11217
    this.OnDocuSignFail = function() {

        HideControlBar();
        $('.sceneTextArea').text("The system has made several attempts to access the affidavit system, but it is not responding at this time. Please contact our customer support team at 1-800-442-1149 for resolution.")

    }

    this.OnSignCancel = function() {

        HideControlBar();
        $('.sceneTextArea').text("Remember, you will not receive credit for this course, unless you sign the affidavit. Please be sure to return to this screen to sign it later.");

    }

    //Abdus Samad
    //LCMS-11974
    //START
    this.OnSignDecline = function() {

        HideControlBar();
        $('.sceneTextArea').text("Remember, you will not receive credit for this course, unless you sign the affidavit. Please be sure to return to this screen to sign it later.");
        $('input[type="button"]').attr('disabled', 'disabled');
        $('input[type="button"]').css('color', 'grey');
        $('input[type="checkbox"]').attr('disabled', 'disabled');
        $('input[type="checkbox"]').css('color', 'grey');

        $('.DeclineHeading').css("display", "block");
        $('.DeclineBody').css("display", "block");
        $('.DeclineFooter').css("display", "block");
    }
    //STOP

    this.OnDocuSignTimeout = function() {

        var result = confirm("Would you like to complete the session by signing the document?")
        if (result == true) {
            var myWindow = window.open('OpenEmbeddedDocuSign.aspx', 'myWindow', "height=700,width=1024");
            myWindow.opener = window.self;
        }
        else {
            HideControlBar();
            $('.sceneTextArea').text("You must submit a signed affidavit for course completion.");
        }

    }

    this.OnDocuSignExpired = function() {

        var result = confirm("It looks like your previous session time has expired, would you like to finish the document by starting a new session?")
        if (result == true) {
            var myWindow = window.open('OpenEmbeddedDocuSign.aspx', 'myWindow', "height=700,width=1024");
            myWindow.opener = window.self;
        }
        else {
            HideControlBar();
            $('.sceneTextArea').text("You must submit a signed affidavit for course completion.");
        }

    }

    this.OnSignExpired = function() {

        HideControlBar();
        $('.sceneTextArea').text("You must submit a signed affidavit for course completion.");

    }

    this.OnViewComplete = function() {

        HideControlBar();
        $('.sceneTextArea').text("You have completed the View Of the Document.");

    }

    //End


    String.prototype.replaceAll = function(
                                               strTarget, // The substring you want to replace
                                               strSubString // The string you want to replace in.

                                            ) {
        var strText = this;
        var intIndexOfMatch = strText.indexOf(strTarget);


        // Keep looping while an instance of the target string
        // still exists in the string.
        while (intIndexOfMatch != -1) {
            // Relace out the current instance.
            strText = strText.replace(strTarget, strSubString)

            // Get the index of any next matching substring.
            intIndexOfMatch = strText.indexOf(strTarget);
        }


        // Return the updated string with ALL the target strings
        // replaced out with the new substring.
        return (strText);
    }
    this.SubmitAssessmentResult = function(assessmentId, arrAnswerIds, arrAnswerStrings, isSkipping) {

        var packet = com.SubmitAssessmentResult(assessmentId, arrAnswerIds, arrAnswerStrings, isSkipping, toogleFlagSkipQuestion(),
                                function(packet) {
                                    loadingEngine.OnReadyContentArea();
                                    if (packet == null) {
                                        if (document.getElementById('single') != null)
                                            document.getElementById('single').disabled = false;
                                    }
                                    else
                                    {
                                        cpEngine.CommandHelper(packet);
                                        ui.slide.loader.hide(function()
			                            {				
			                            },'');                                         
                                    }
                                }, function() {
                                    loadingEngine.OnLoadContentArea('');
                                });
        //old sync code LCMS-10278
        //               packet = com.SubmitAssessmentResult(assessmentId, arrAnswerIds,arrAnswerStrings, isSkipping);
        //               if(packet == null)
        //               {
        //                    if(document.getElementById('single')!=null)          
        //                        document.getElementById('single').disabled=false;
        //               }
        //               else     
        //                this.CommandHelper(packet);	
    }

    //Added By Abdus Samad
    //LCMS-12105
    //Start
     function toogleFlagSkipQuestion() {
         var isflagSkipQuestion = false;
         /*var agent = navigator.userAgent.toLowerCase();

         if (agent.search("msie") > -1) {

             if (document.getElementById("ImageButton1").nameProp == "Flag2.png") {
                 isflagSkipQuestion = true;
             }
         }
         if (agent.search("firefox") > -1) {
             if (document.getElementById("ImageButton1").src.contains("Flag2.png") == true) {
                 isflagSkipQuestion = true;
             }
         }

         else {*/
         
//             var information = document.getElementById('ImageButton1').getAttribute('src').toString();
//             if (information.search("Flag2.png") > 0) {
//                 isflagSkipQuestion = true;
//             //}

//            }

                if($("#toogle-flag").hasClass('flagged'))             
                    isflagSkipQuestion = true;



         return isflagSkipQuestion;
     }
     
//    function toogleFlagSkipQuestion() {
//        var isflagSkipQuestion = false;

//        var agent = navigator.userAgent.toLowerCase();

//        if (agent.search("firefox") > -1) {
//            if (document.getElementById("ImageButton1").src.contains("Flag2.png") == true) {
//                isflagSkipQuestion = true;
//            }
//        }

//        // if (agent.search("msie") > -1) 
//        else {
//            if (document.getElementById("ImageButton1").nameProp == "Flag2.png") {
//                isflagSkipQuestion = true;
//            }
//        }
//      
//        
//        
//        return isflagSkipQuestion;
//    }
    //Stop


    this.GetQuestionRendering = function(questionObject) {
        renderEngine.ShowQuestion(questionObject);
        //			var thingsToCheck = com.CheckThingsBeforeCallingIdleTimeWarningPopup();			
        //			if(thingsToCheck == "true")
        //			{
        //			    renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
        //            }
    }

    this.QuestionRemediationRendering = function(questionObject) {
        renderEngine.QuestionRemediationRendering(questionObject);
    }

    this.GetQuestionResultRendering = function(objQuestionResult) {
        renderEngine.ShowQuestionResultRendering(objQuestionResult);
        //		    var thingsToCheck = com.CheckThingsBeforeCallingIdleTimeWarningPopup();			
        //			if(thingsToCheck == "true")
        //			{
        //		        renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
        //		    }
    }

    this.GetNextQuestionAfterFeedback = function() {
        packet = com.GetNextQuestionAfterFeedback(function(packet) {
            loadingEngine.OnReadyContentArea();
            if (packet != null) {
                cpEngine.CommandHelper(packet);
			    ui.slide.loader.hide(function()
			    {				
			    },'');                 
            }
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //	        packet = com.GetNextQuestionAfterFeedback();
        //	        if(packet != null)
        //	            this.CommandHelper(packet);
    }

    this.ShowSkippedQuestion = function(objectSkippedQuestion) {
        renderEngine.ShowSkippedQuestion(objectSkippedQuestion);
    }

    this.AskSpecifiedQuestion = function(assessmentItemID) {
        var packet = com.AskSpecifiedQuestion(assessmentItemID,
	                            function(packet) {
	                                loadingEngine.OnReadyContentArea();
	                                cpEngine.CommandHelper(packet);
			                        ui.slide.loader.hide(function()
			                        {				
			                        },'');  	                                
	                            }, function() {
	                                loadingEngine.OnLoadContentArea('');
	                            });
        //old sync code LCMS-10278
        //	        packet = com.AskSpecifiedQuestion(assessmentItemID);
        //	        this.CommandHelper(packet);
    }

    this.ShowSpecifiedQuestionScore = function(assessmentItemID) {
        packet = com.ShowSpecifiedQuestionScore(assessmentItemID, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');               
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);
    }

    this.ShowValidationQuestion = function(authenticationObject) {
        renderEngine.ShowValidationQuestionRendering(authenticationObject);
    }

    this.ShowAssessmentScoreSummary = function(objectScoreSummary) {
        //renderEngine.GetIdleTimeNotificationObj().ClearTimeOutForNotification();
        renderEngine.ShowAssessmentScoreSummary(objectScoreSummary);
    }


    this.ShowAnswerReview = function(objectAnswerReview) {
        renderEngine.ShowAnswerReview(objectAnswerReview);
        resetCPIdleTimer();
        //		    renderEngine.resetCPIdleTimer(); 
        //		    var thingsToCheck = com.CheckThingsBeforeCallingIdleTimeWarningPopup();			
        //			if(thingsToCheck == "true")
        //			{
        //		    renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
        //		    }
    }

    this.ShowIndividualQuestionScore = function(objectIndividualQuestionScore) {
        renderEngine.ShowIndividualQuestionScore(objectIndividualQuestionScore);
    }

    this.FinishGradingAssessment = function() {
        packet = com.FinishGradingAssessment(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);
    }

    this.ContinueAfterAssessmentScore = function() {
        packet = com.ContinueAfterAssessmentScore(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //  this.CommandHelper(packet);
    }

    this.SynchToExternalSystem = function(mileStone) {
        com.SynchToExternalSystem(mileStone);
		ui.slide.loader.hide(function()
		{				
		},'');         
    }

    //LCMS-11877
    this.SaveCourseRating = function() {
        //var Rating = jQuery('#starVin  .star:checked').val();
        var Rating = $('#divstarRating input[name="star1"]:checked').val();
        com.SaveCourseRating(Rating);
		ui.slide.loader.hide(function()
		{				
		},'');         
    }

    this.SaveCourseRatingNPS = function(NPS_RATING,USER_REVIEW_TEXT,RATING_SHOPPINGEXP,RATING_COURSE,RATING_LEARNINGTECH,RATING_CS,RATING_SHOPPINGEXP_SECONDARY,RATING_COURSE_SECONDARY,RATING_LEARNINGTECH_SECONDARY,RATING_CS_SECONDARY,   RATING_SHOPPINGEXP_SECONDARY_Q, RATING_COURSE_SECONDARY_Q,RATING_LEARNINGTECH_SECONDARY_Q,RATING_CS_SECONDARY_Q) {
        
        return com.SaveCourseRatingNPS(NPS_RATING,USER_REVIEW_TEXT,RATING_SHOPPINGEXP,RATING_COURSE,RATING_LEARNINGTECH,RATING_CS,RATING_SHOPPINGEXP_SECONDARY,RATING_COURSE_SECONDARY,RATING_LEARNINGTECH_SECONDARY,RATING_CS_SECONDARY,   RATING_SHOPPINGEXP_SECONDARY_Q,RATING_COURSE_SECONDARY_Q,RATING_LEARNINGTECH_SECONDARY_Q,RATING_CS_SECONDARY_Q);
        
    }
    
    //LCMS-12532 Yasin
    this.SaveValidationIdentityQuestion = function() {

        var txtAnswerSet1 = document.getElementById("txtAnswerSet1").value;
        var txtAnswerSet2 = document.getElementById("txtAnswerSet2").value;
        var txtAnswerSet3 = document.getElementById("txtAnswerSet3").value;
        var txtAnswerSet4 = document.getElementById("txtAnswerSet4").value;
        var txtAnswerSet5 = document.getElementById("txtAnswerSet5").value;

        var QuestionSet1 = document.getElementById("QuestionSet1");
        var QS1 = QuestionSet1.options[QuestionSet1.selectedIndex].value;        

        var QuestionSet2 = document.getElementById("QuestionSet2");
        var QS2 = QuestionSet2.options[QuestionSet2.selectedIndex].value;

        var QuestionSet3 = document.getElementById("QuestionSet3");
        var QS3 = QuestionSet3.options[QuestionSet3.selectedIndex].value;

        var QuestionSet4 = document.getElementById("QuestionSet4");
        var QS4 = QuestionSet4.options[QuestionSet4.selectedIndex].value;

        var QuestionSet5 = document.getElementById("QuestionSet5");
        var QS5 = QuestionSet5.options[QuestionSet5.selectedIndex].value;
        var errordiv = document.getElementById("RequiredValidationQuestion");



        if (QS1 === '0') {
            ui.slide.loader.hide(function()
            {	
            },'');
            $("#RequiredValidationQuestion").html(Validation_Message1);            
            return false;
        }
        if (QS2 === '0') {            
            ui.slide.loader.hide(function()
            {	
            },'');
            $("#RequiredValidationQuestion").html(Validation_Message2);
            return false;
        }
        if (QS3 === '0') {            
            ui.slide.loader.hide(function()
            {	
            },'');        
            $("#RequiredValidationQuestion").html(Validation_Message3);    
            return false;
        }
        if (QS4 === '0') {            
            ui.slide.loader.hide(function()
            {	
            },'');        
            $("#RequiredValidationQuestion").html(Validation_Message4);    
            return false;
        }
        if (QS5 === '0') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message5);
            return false;
        }


        if (txtAnswerSet1 === '') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message_Must);
            return false;
        }

        if (txtAnswerSet2 === '') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message_Must);
            return false;
        }

        if (txtAnswerSet3 === '') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message_Must);
            return false;
        }
        if (txtAnswerSet4 === '') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message_Must);
            return false;
        }
        if (txtAnswerSet5 === '') {            
            ui.slide.loader.hide(function()
            {	
            },'');            
            $("#RequiredValidationQuestion").html(Validation_Message_Must);
            return false;
        }



//        packet = com.SaveValidationIdentityQuestion(QS1, txtAnswerSet1, QS2, txtAnswerSet2, QS3, txtAnswerSet3, QS4, txtAnswerSet4, QS5, txtAnswerSet5);
//        $(htmlContentContainer).html("");
//           if (packet != null)
//            cp.CommandHelper(packet);


  packet = com.SaveValidationIdentityQuestion(QS1, txtAnswerSet1, QS2, txtAnswerSet2, QS3, txtAnswerSet3, QS4, txtAnswerSet4, QS5, txtAnswerSet5, function(packet) {
	                                loadingEngine.OnReadyContentArea();
	                                cpEngine.CommandHelper(packet);
                                    ui.slide.loader.hide(function()
                                    {	
                                    },'');	                                
	                            }, function() {
	                                loadingEngine.OnLoadContentArea('');
	                            });

        /*
        * Code Review : call the commandHelper here with the returned Packet. cp.CommandHelper(packet);
        */
    }

    this.SubmitValidationQuestionResult = function(questionId, questionText) {

        packet = com.SubmitValidationQuestionResult(questionId, questionText);
        ui.slide.loader.hide(function()
        {	            
        },'');           
        if (packet != null)
            cp.CommandHelper(packet);
    }


    this.CallOpenCommand = function(openCommandObject) {
        renderEngine.OpenCommand(openCommandObject);
        packet = com.ResumeCourseAfterValidation();
        // alert("ResumeCourseAfterValidation:"+packet.CommandName);
        cp.CommandHelper(packet);


    }

    this.ShowCourseLocked = function(lockCourseCommandObject) {
        renderEngine.LockCourse(lockCourseCommandObject);
    }

    this.ShowCourseApproval = function(CourseApprovalCommandObject) {
        renderEngine.CourseApproval(CourseApprovalCommandObject);
    }
    
    this.ShowAssessmentItemResult = function(AssessmentItemResultCommandObject) {
        renderEngine.AssessmentItemResult(AssessmentItemResultCommandObject);
    }
    


    this.ContinueAfterAssessment = function() {
        packet = com.ContinueAfterAssessment();
        cp.CommandHelper(packet);
		ui.slide.loader.hide(function()
		{				
		},'');         
    }

    this.AssessmentTimerExpire = function() {
        //renderEngine.AssessmentTimerExpire(timerExpireDialogue, overlay);
        packet = com.AssessmentTimerExpired();
        cp.CommandHelper(packet);
    }

    this.ContinueGradingWithoutAnswering = function() {        
        var packet = com.ContinueGradingWithoutAnswering(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });

        //old sync code LCMS-10278
        //		    packet = com.ContinueGradingWithoutAnswering();
        //		    this.CommandHelper(packet);

    }

    this.ReturnToAssessmentResults = function() {
        packet = com.ReturnToAssessmentResults(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);
    }

    this.GetNextRemidiationQuestion = function() {
        var packet = com.GetNextRemidiationQuestion(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');             
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // cp.CommandHelper(packet);
    }

    this.ShowContent = function(AssessmentItemID) {
        //alert(AssessmentItemID);
        var packet = com.ShowContent(AssessmentItemID, function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
            {
            },'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // cp.CommandHelper(packet);
    }

    this.GetPreviousRemidiationQuestion = function() {
        var packet = com.GetPreviousRemidiationQuestion(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');               
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // cp.CommandHelper(packet);
    }

    this.ShowTimerExpiredMessage = function(timeExpiredMessageObject) {
        renderEngine.AssessmentTimerExpire(timeExpiredMessageObject);
    }

    this.GoContentTOQuestion = function() {
        packet = com.GoContentTOQuestion(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
            ui.slide.loader.hide(function()
            {	            
            },'');            
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // this.CommandHelper(packet);
    }

    this.ShowProctorMessage = function(commandObject) {
        //alert("in cp.showproct");
        renderEngine.ShowProctorMessage(commandObject);
    }

    this.GradeAssessment = function(assesmentId, answerIds, answerStrings) {
        var packet;
        packet = com.GradeAssessment(assesmentId, answerIds, answerStrings);
        //alert("Packet Returned\n" + packet);
        this.CommandHelper(packet);
        ui.slide.loader.hide(function()
		{				
		},'');         
    }
    //zaheer code

    this.UnlockCourse = function(assesmentId, answerIds, answerStrings) {

        com.UnlockCourse();

    }


    this.AskValidationQuestion = function() {
        // alert('AskValidationQuestion checking');
        renderEngine.validationTimer("0");
        //renderEngine.validationTimerBit(false);
        packet = com.AskValidationQuestion();
        ui.slide.loader.hide(function()
        {	
        //	go ahead, template is ready....
        },'');
        
        if (packet != null)
            cp.CommandHelper(packet);


        //		$(PlaybuttonEn).find('a').unbind();
        //		
        //		// Play Button
        //				$(PlaybuttonEn).find('a').bind('click.namespace', function(){ 
        //					
        //					//alert('change code');
        //					 packet = com.AskValidationQuestion();
        //			         cp.CommandHelper(packet);	
        //		
        //		             $(PlaybuttonEn).find('a').unbind();
        //		
        //		            $(PlaybuttonEn).find('a').bind('click.namespace', function(){ 
        //						
        //						idleTimerObj.ResetTimer();
        //						playClick();
        //						
        //						if(isAccordianMax){
        //							closeAccordian(panel);
        //							$(panelbutton).css("display", "none");
        //							isAccordianOpen = false;
        //							isAccordianMax = false;
        //						}else if(isAccordianOpen){
        //							offAccordian(panel);
        //							$(panelbutton).css("display", "none");
        //							isAccordianOpen = false;
        //							isAccordianMax = false;
        //						}
        //				     });
        //			
        //						
        //				});

    }

    this.ValidationTimerExpired = function() {
        //stop 0
        //Resume 1
        //Pause 2
        //alert('ValidationTimerExpired');
        com.ValidationTimerExpired();
        // renderEngine.assessmentTimer("2");
        renderEngine.ResetValidationTimer();
        renderEngine.validationTimer("0");




        //  renderEngine.validationTimerBit(false);
        //pause validationTmer Here
        //cp.CommandHelper(packet);




    }

    this.StartValidation = function(commandObject) {
        //alert('IsOrientationScene '+commandObject.StartValidationMessage.IsOrientationScene);
        renderEngine.assessmentTimer("2");
//        if (isAccordianMax) {
//            closeAccordian(panel);
//            $(panelbutton).css("display", "none");
//            isAccordianOpen = false;
//            isAccordianMax = false;
//        } else if (isAccordianOpen) {
//            offAccordian(panel);
//            $(panelbutton).css("display", "none");
//            isAccordianOpen = false;
//            isAccordianMax = false;
//        }
        if (commandObject.StartValidationMessage.IsOrientationScene == true) {
            //  alert('IsOrientationScene');
            var packet;
            packet = com.GetValidationOrientationScene();
            // alert("GetValidationOrientationScene:"+packet);
            this.CommandHelper(packet);
            //bind askvalidation
        } else {

            var packet;
            ui.slide.next(function()
            {
	            packet = com.AskValidationQuestion();
            });
            //bind submit
            if (packet != null)
            {
            ui.slide.loader.hide(function()
            {	
                //	go ahead, template is ready....
            },'');                            
                this.CommandHelper(packet);
            }
                
            

        }

        //alert("Packet Returned\n" + packet);

    }



    this.ShowValidationOrientationScene = function(packet) {

        renderEngine.ShowValidationOrientationSceneHTMLRendering(packet);

    }
    //end of zaheer code
    this.RedirectCoursePlayer = function(redirectURL) {        
        if ($("#examIdleTimerDialogue").hasClass("modal-is-visible"))
        {
            ui.svgModal.close('modal-idle');
        }  
        if ($("#timerExpireDialogue").hasClass("modal-is-visible"))
        {
            ui.svgModal.close('modal-Expire');
        }               
        window.location = redirectURL;
    }
    this.ContinueAfterCourseEnd = function() {

        com.ContinueAfterCourseEnd();
    }
    this.windowClosed = function() {
        // com.ContinueAfterCourseEnd();
        //com.logoutCoursePlayer($(timer).html());
        com.logoutCoursePlayerIntegeration($(timer).html());
        //alert('1');
        var packet = com.logoutCoursePlayer($(timer).html());
    }

    this.DownloadCertificate = function() {
        com.DownloadCertificate();
        //cp.CommandHelper(packet);

    }

    this.DownloadCourseApprovalCertificate = function(certificateURL) {
        com.DownloadCourseApprovalCertificate(certificateURL);
    }

    this.BeginCourseEvaluation = function() {
        var packet;
        packet = com.BeginCourseEvaluation(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');               
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        // cp.CommandHelper(packet);
    }
    this.SkipCourseEvaluation = function() {
        var packet;
        packet = com.SkipCourseEvaluation(function(packet) {
            loadingEngine.OnReadyContentArea();
            cpEngine.CommandHelper(packet);
			ui.slide.loader.hide(function()
			{				
			},'');               
        }, function() {
            loadingEngine.OnLoadContentArea('');
        });
        //old sync code LCMS-10278
        //  cp.CommandHelper(packet);
    }
    this.PlayClick = function() {
        playClick();
    }



    this.ShowProctorLoginScreen = function() {

        return com.ShowProctorLoginScreen();

    }


    // LCMS-9882
    this.GetAssessmentItemsByAssessmentBankIDs = function(assessmentBankIDs) {
        return com.GetAssessmentItemsByAssessmentBankIDs(assessmentBankIDs);
    }

    // LCMS-9882
    this.SaveAssessmentEndTrackingInfo_ForGameTemplate = function(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount) {
        return com.SaveAssessmentEndTrackingInfo_ForGameTemplate(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount);
    }
    // LCMS-10535
    //		this.ShowCourseSettings = function(courseSettings) {	
    //			renderEngine.CourseSettingsRendering(courseSettings);
    //		}

    this.ResetSessionTime = function() {
        return com.NoteTime();
    }

    //Proctor Lock Screen
    //Added by Abdus Samad
    //Start
    this.LockCourseProctor = function(commandObject) {
        //debugger;
        $("#ProctorLockCourse").html(commandObject.ProctorLockCourseCommandMessage.LockCourseMessage);
        
                $("#ProctorLockCourse").find('a').bind('click.namespace', function() {
            var IsLocked = com.IsProctorLockedCourse();
            if (IsLocked != null) {
                //$(overlay).fadeOut("slow");
                //$("#ProctorLockCourse").fadeOut("slow");
                cpEngine.CommandHelper(IsLocked);

            }
            return false;
        });

        //$(overlay).css({ "opacity": "0.7" });
        //$(overlay).fadeIn("slow");
        //$("#ProctorLockCourse").fadeIn("slow");
        //$(glossaryDialogue).fadeIn("slow");

    }
//Stop



    this.CommandHelper = function(commandObject) {


        // LCMS-10928
        // ----------------------------------------------
        $('#assessmentControlPanel').hide();
        $('#AssessmentInProgress').hide();
        // ----------------------------------------------
        //LCMS-10392
        //debugger;
        renderEngine.HideAmazonAffiliatePanel();
        IsAmazonAffiliatePanel = false;
        //Abdus Samad LCMS-11878 Start
        renderEngine.HideRecommendationCoursePanel();
        IsRecommendationCoursePanel = false;
        //Stop



        bIsContentResize = true;
        content_resize();

        //renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
        //$(htmlContentContainer).find('span').empty(); 
        //alert(commandObject.CommandName);   
        try {
            if ($("#CourseCompletionReport").is(":visible")) {
                $("#content").show();
                $("#CourseCompletionReport").hide();
            }
        }
        catch (err) {
        }
        courseEvaluationInProgressTF = false;

        // alert('command name: '+commandObject.CommandName);
        switch (commandObject.CommandName) {

            case "ProctorLockCourseCommand":
                {
                    commandObject.ProctorLockCourseCommand
                    this.LockCourseProctor(commandObject);
                    break;
                }        
            case "ShowCourseInfo":
                {
                    this.LoadCourseRendering(commandObject);
                    break;
                }

            case "ShowBookMark":
                {
                    this.ShowBookMark(commandObject);
                    break;
                }
            case "ShowCourseMaterial":
                {
                    this.ShowCourseMaterial(commandObject);
                    break;
                }
            case "ShowErrorMessage":
                {

                    this.ErrorMessageRendering(commandObject);
                    break;
                }
            case "ShowGlossary":
                {
                    this.ShowGlossary(commandObject);
                    break;
                }
            case "ShowGlossaryInDetail":
                {
                    this.ShowGlossaryInDetail(commandObject);
                    break;
                }
            case "ShowLocalizedInfo":
                {

                    break;
                }

            case "ShowResourceInfo":
                {

                    this.InitializeCoursePlayer(commandObject);
                    break;
                }
            case "ShowSlide":
                {
                    stopSound();
                    this.ShowSlide(commandObject);
                    break;
                }
            case "ShowStartAssessment":
                {

                    stopSound();
                    this.StartAssessmentMessageRendering(commandObject)
                    break;
                }
            case "ShowTableOfContent":
                {

                    this.ShowTOC(commandObject);
                    break;
                }
            case "ShowCustomMessage":
                {

                    this.CustomMessageRendering(commandObject);
                    break;
                }

            case "ShowQuestion":
                {

                    // LCMS-10928
                    // ----------------------------------------------
                    $('#assessmentControlPanel').show();
                    $('#AssessmentInProgress').show();
                    // ----------------------------------------------
                    $("#proctor_login_screen").hide();
                    $("#CARealStateValidation").hide();
                    $("#NYInsuranceValidation").hide();
                    //alert('RemidiationMode'+commandObject.AssessmentItem.RemidiationMode);
                    stopSound();
                    if (commandObject.AssessmentItem.RemidiationMode) {
                        this.QuestionRemediationRendering(commandObject);
                    }
                    else {
                        this.GetQuestionRendering(commandObject);

                    }
                    break;
                }

            case "ShowQuestionResult":
                {
                    // LCMS-10928
                    // ----------------------------------------------
                    $('#assessmentControlPanel').show();
                    $('#AssessmentInProgress').show();
                    // ----------------------------------------------
                    stopSound();
                    this.GetQuestionResultRendering(commandObject);
                    break;
                }

            case "ShowAssessmentScoreSummary":
                {

                    stopSound();
                    this.SynchToExternalSystem("ShowAssessmentScoreSummary");
                    this.ShowAssessmentScoreSummary(commandObject);
                    break;
                }

            case "ShowSkippedQuestion":
                {
                    // LCMS-10928
                    // ----------------------------------------------
                    $('#assessmentControlPanel').show();
                    $('#AssessmentInProgress').show();
                    // ----------------------------------------------
                    stopSound();
                    this.ShowSkippedQuestion(commandObject);
                    break;
                }
            case "ShowValidationQuestion":
                {
                    //alert(commandObject.CommandName);
                    $("#proctor_login_screen").hide();
                    stopSound();
                    this.ShowValidationQuestion(commandObject);
                    break;
                }

            case "ShowAnswerReview":
                {
                    stopSound();
                    this.ShowAnswerReview(commandObject);

                    break;
                }

            case "ShowIndividualQuestionScore":
                {
                    stopSound();
                    this.ShowIndividualQuestionScore(commandObject);
                    break;
                }

            case "OpenCommand":
                {
                    // alert(commandObject.CommandName);
                    this.CallOpenCommand(commandObject);
                    break;
                }

            case "ShowCourseLocked":
                {
                    this.ShowCourseLocked(commandObject);
                    break;
                }

            case "ShowCourseApproval":
                {
                    this.ShowCourseApproval(commandObject);
                    break;
                }
            case "ShowAssessmentResult":
                {
                    this.ShowAssessmentItemResult(commandObject);
                    break;
                }                

            case "ShowCourseApprovalAffidavit":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowCourseApprovalAffidavit(commandObject);
                    break;
                }

            case "ShowTimerExpiredMessage":
                {
                    stopSound();
                    this.ShowTimerExpiredMessage(commandObject);
                    break;
                }

            case "ShowProctorMessage":
                {
                    //alert('ShowProctorMessage');
                    this.ShowProctorMessage(commandObject);
                    break;
                }
            case "StartValidation":
                {
                    //alert(commandObject.CommandName);
                    //commandObject.StartValidationMessage.IsOrientationScene
                    this.StartValidation(commandObject);
                    break;
                }
            case "ShowValidationOrientationScene":
                {
                    //alert(commandObject.CommandName);
                    //commandObject.StartValidationMessage.IsOrientationScene
                    stopSound();
                    this.ShowValidationOrientationScene(commandObject);
                    break;
                }
            case "ShowEOCInstructions":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowEOCInstructions(commandObject);
                    $(PlaybuttonEn).show();
                    $(PlaybuttonDs).hide();
                    break;
                }
            case "ShowCourseCertificate":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowCourseCertificate(commandObject);
                    $(PlaybuttonEn).show();
                    $(PlaybuttonDs).hide();
                    break;
                }
            case "ShowEmbeddedAcknowledgment":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowEmbeddedAcknowledgment(commandObject);
                    break;
                }
            case "ShowPlayerCourseReviewAfterCompletion":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowCourseEndTextForCourseReviewPolicy(commandObject);
                    $(controlPanel).find("#IcoCourseCompletion").show();
                    $(controlPanel).find("#IcoCourseCompletionDs").hide();
                    break;
                }
            case "ShowFinalExamLocked":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowFinalExamLocked(commandObject);
                    break;
                }
            case "ShowCourseEvaluation":
                {
                    //Stop sound if playing
                    //LCMS-3459
                    $('#security').empty();
                    stopSound();
                    renderEngine.ShowCourseEvaluation(commandObject);
                    break;
                }

            case "ShowCourseEvaluationQuestions":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowCourseEvaluationQuestions(commandObject);
                    courseEvaluationInProgressTF = true;
                    break;
                }
            case "ShowSeatTimeCourseLaunch":
                {
                    stopSound();
                    $('#security').empty();
                    $(htmlContentContainer).show();
                    renderEngine.ShowSeatTimeCourseLaunch(commandObject);
                    break;
                }

            case "ShowSeatTimeExceed":
                stopSound();
                $('#security').empty();
                renderEngine.ShowSeatTimeExceed(commandObject);

                break;
            case "ShowProctorLoginScreen":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowProctorLoginScreen(commandObject);
                    break;
                }
            case "ShowProctorAuthenticationResult":
                {                    
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowProctorAuthenticationResult(commandObject);
                    break;
                }
            case "ShowSpecialPostAssessmentValidation":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowSpecialPostAssessmentValidation(commandObject);
                    break;
                }
            case "ShowSpecialPostAssessmentAuthenticationResult":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowSpecialPostAssessmentAuthenticationResult(commandObject);
                    break;
                }
            case "ShowNYInsuranceValidation":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowNYInsuranceValidation(commandObject);
                    break;
                }
            case "ShowNYInsuranceAuthenticationResult":
                {
                    stopSound();
                    $('#security').empty();
                    renderEngine.ShowNYInsuranceAuthenticationResult(commandObject);
                    break;
                }
                //					// LCMS-10535
                //					case "ShowCourseSettings":
                //					{
                //						
                //						this.ShowCourseSettings(commandObject);
                //						break;
                //					}

            case "ShowDocuSignRequirementAffidavit":
                {
                    stopSound();
                    renderEngine.ShowDocuSignRequirementAffidavit(commandObject);
                    break;
                }
                //For LCMS-11283	
            case "ShowDocuSignProcess":
                {
                    stopSound();
                    renderEngine.ShowDocuSignProcess(commandObject);
                    break;
                }

                //For LCMS-11877
            case "ShowCourseLevelRating":
                {
                    stopSound();
                    renderEngine.ShowCourseLevelRating(commandObject);
                    break;
                }
                //LCMS-12532 Yasin
            case "ShowValidationIdentityQuestion":
                {                    
                    stopSound();
                    renderEngine.showValidationIdentityQuestion(commandObject);
                    break;
                }


        }
        //renderEngine.GetIdleTimeNotificationObj().SetTimeOutForNotification(renderEngine.whenToShowIdleTimePopup, renderEngine.idleWarningTime, renderEngine.idleTimeMsgHeading, renderEngine.idleTimeMsgContent);
    }

    function submitCourseEvaluation() {
        var len = $(evalQuestions).find('div').length;
        var questionIds = "";
        var questionTypes = "";
        var answerIds = "";
        var QuestionType = "";
        var alen = 0;
        var validation = true;

        var questionTypesArr = new Array();
        var questionIdsArr = new Array();
        var answeresArr = new Array();

        for (i = 0; i < len; i++) {
            //Question List
            /*
            if(i == 0)
            questionIds = $(evalQuestions).find('div').eq(i).attr('Id');
            else
            questionIds = questionIds + "," + $(evalQuestions).find('div').eq(i).attr('Id');
            
            QuestionType = $(evalQuestions).find('div').eq(i).attr('title');
            */

            QuestionType = $(evalQuestions).find('div').eq(i).attr('Id');
            /*
            if (i == 0)
            questionIds = QuestionType.split("_")[0];
            else
            questionIds = questionIds + "," + QuestionType.split("_")[0];
            */
            //alert(QuestionType);            
            if(QuestionType != undefined)
            {
               var questionInfo = QuestionType.split("_");
            
           

            questionIdsArr.push(questionInfo[0]);
            questionTypesArr.push(questionInfo[1]);

            //alert(QuestionType);
            if (QuestionType.indexOf("MSSQ") >= 0) {
                /*
                questionTypes = questionTypes + "MSSQ";
                */
                var acount = 0;
                alen = $(evalQuestions).find('div').eq(i).find('input').length;

                var mssqAnswerIDS = "";

                for (j = 0; j < alen; j++) {

                    if ($(evalQuestions).find('div').eq(i).find('input').eq(j).is(':checked')) {                        
                        /*
                        if(answerIds.length == 0)
                        answerIds = $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        else if(acount == 0)
                        answerIds = answerIds + $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        else
                        answerIds = answerIds + ";" + $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        */

                        if (acount == 0) {
                            mssqAnswerIDS = $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        } else {
                            mssqAnswerIDS = mssqAnswerIDS + ";" + $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id')
                        }

                        acount++;
                    }
                }               
                answeresArr.push(mssqAnswerIDS);

                if (QuestionType.indexOf("Required") > 0) {
                    if (acount < 1) {
                        validation = false;                        
                        //add the red color logic
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'red');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'block');
                        //break;/*LCMS-4020*/
                    }
                    else {
                        
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'black');
                        $(errorMessageBox).css('display', 'none');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'none');
                    }
                }

            }
            else if (QuestionType.indexOf("SSSQ") >= 0) {
                var acount = 0;
                alen = $(evalQuestions).find('div').eq(i).find('input').length;
                //questionTypes = questionTypes + "SSSQ";
                var ssqAnswerID = "";

                for (j = 0; j < alen; j++) {

                    if ($(evalQuestions).find('div').eq(i).find('input').eq(j).is(':checked')) {                        
                        /*
                        if(answerIds.length == 0)
                        answerIds = $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        else if(acount == 0)
                        answerIds = answerIds + $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                        else
                        answerIds = answerIds + ";" + $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id');
                            
                        
                        */
                        ssqAnswerID = $(evalQuestions).find('div').eq(i).find('input').eq(j).attr('Id')
                        acount++;
                        break;
                    }
                }

                answeresArr.push(ssqAnswerID);

                if (QuestionType.indexOf("Required") > 0) {
                    //alert('acount : '+acount);
                    if (acount < 1) {
                        validation = false;
                        //add the red color logic
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'red');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'block');
                        //break;/*LCMS-4020*/
                    }
                    else {
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'black');
                        $(errorMessageBox).css('display', 'none');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'none');
                    }
                }
            }
            else if (QuestionType.indexOf("FITB") >= 0) {
                var FITBValue = "";
                //FITBValue = $(evalQuestions).find('div').eq(i).find('input').attr('value');
                FITBValue = $(evalQuestions).find('div').eq(i).find('input').val();
                //questionTypes = questionTypes + "FITB";
                //LCMS-4064
                if(FITBValue != undefined)
                {
                    FITBValue = FITBValue.trim();
                              
                
                if (QuestionType.indexOf("Required") > 0) {
                    if (FITBValue.length < 1) {
                        validation = false;
                        //add the red color logic
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'red');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'block');
                        //break;/*LCMS-4020*/
                    }
                    else {
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'black');
                        $(errorMessageBox).css('display', 'none');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'none');
                    }
                }

                //answerIds = answerIds + FITBValue;
                answeresArr.push(FITBValue);
               } 
            }
            else if (QuestionType.indexOf("TEXT") >= 0) {
                var TEXTValue = "";
                // alert($(htmlContentContainer).find('div').eq(i).find('textarea').attr('value') );
                if ($(evalQuestions).find('div').eq(i).find('textarea').val() != undefined)
                    TEXTValue = $(evalQuestions).find('div').eq(i).find('textarea').val();
                else
                    TEXTValue = $(evalQuestions).find('div').eq(i).find('input').val();
                // alert(TEXTValue);
                //questionTypes = questionTypes + "TEXT";

                //LCMS-4064                
                if(TEXTValue != undefined)
                {
                    TEXTValue = TEXTValue.trim();                     
                
                if (QuestionType.indexOf("Required") > 0) {
                    if (TEXTValue.length < 1) {
                        validation = false;
                        //add the red color logic
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'red');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'block');
                        //break;/*LCMS-4020*/
                    }
                    else {
                        $(evalQuestions).find('div').eq(i).find('h3').css('color', 'black');
                        $(errorMessageBox).css('display', 'none');
                        $(evalQuestions).find('div').eq(i).find('img').css('display', 'none');
                    }
                }

                //answerIds = answerIds + TEXTValue;
                    answeresArr.push(TEXTValue);
               }
              
            }
           }

            /*
            if(i < len-1)
            {
            questionTypes = questionTypes + ",";
            answerIds = answerIds + ",";
                
            }
            */
        }

        if (validation == true) {
            $(errorMessageBox).css('display', 'none');
            var packet;
            //packet=com.GetAndSubmitCourseEvaluation(questionIds,answerIds,questionTypes);            
            packet = com.GetAndSubmitCourseEvaluation(JSON.encode(questionIdsArr), JSON.encode(answeresArr), JSON.encode(questionTypesArr), function(packet) {            
                loadingEngine.OnReadyContentArea();
                cpEngine.CommandHelper(packet);
			    ui.slide.loader.hide(function()
			    {				
			    },'');                   
                cpEngine.SynchToExternalSystem("SubmitCourseEval");
            }, function() {
                loadingEngine.OnLoadContentArea('');
            });
            // cp.CommandHelper(packet);
            // cp.SynchToExternalSystem("SubmitCourseEval");
        }
        else //show error message
        {
            ui.slide.loader.hide(function()
		    {				
		    },''); 
            $(errorMessageBox).css('display', 'block');
            $(errorMessageBox).css('opacity', '1');
        }
    }
}



var currentframeNumber = null;
var LastScene = null;
var FirstScene = false;
var FirstSceneName = null;
var nextButtonState = null;
var slideID = null;
var sceneGUID = null;
var isMovieEnded = false;
var tocArray = [];
//sound variable
var IsSoundPlaying = false;

/*
function getMovieName(movieName) {
    if (navigator.appVersion.indexOf("10.0") != -1) {
        return document[movieName];
    }

    if (navigator.appName.indexOf("Microsoft") != -1) {

        return window[movieName];
    } else {
        // for Chrome, FF and Safari.
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

//sending to flash...
function loadMovie(url, frame) {    

    // LCMS-3096    
    //------------------------------
    try {			 
        getMovieName("movie").callLoadSWF(url, frame);
    }
    catch (ex) {        
        setTimeout("loadMovie('" + url + "', '" + frame + "');", 1000);
    }
    //------------------------------
}
//sending to flash...
function playClick() {

    if (isMovieEnded) {
        cp.CallNextSlide();

    } else {
		ui.slide.loader.hide(function()
		{	
		//	go ahead, template is ready....
		},'');
        getMovieName("movie").callPlayMovie();
        document.getElementById('PlaybuttonEn').style.display = 'none';
        document.getElementById('PlaybuttonDs').style.display = '';
        nextButtonState = "false";
    }
    //resetCPIdleTimer();
}
//calling from flash...
function getSlideInfo() {
    getMovieName("movie").callGetSldeInfo();
}
//message from flash...
function setSlideInfo(frame) {
    currentframeNumber = frame;
    //alert("setSlideinfo");
    cp.SendBookmarkInfo(slideID, sceneGUID, currentframeNumber, LastScene, isMovieEnded, nextButtonState, FirstSceneName);
}

//calling from flash...
function playNextLesson(num) {
    //alert(num);
    document.getElementById('PlaybuttonEn').style.display = '';
    document.getElementById('PlaybuttonDs').style.display = 'none';
    nextButtonState = "true";
    if (num == 1) {
        isMovieEnded = true;
    }

}

function setScene(scene) {
    //alert("<--setScene:scene-->" + scene);
    LastScene = scene;

    if (FirstScene) {

        FirstSceneName = scene;
        //alert("<--setScene:FirstScene-->" + FirstSceneName);
        FirstScene = false;
    }
    //alert(scene);
}

//sending to flash...
function backClick() {
    isMovieEnded = false;

    //alert("<--backClick:LastScene-->" + LastScene);
    var scene = LastScene;
    if (scene == FirstSceneName) {
        //alert("<--backClick:backClick-->" + FirstSceneName);
        //alert("<--backClick:scene-->" + scene);
        //alert("scene is null I am Sending requeste to the server");	
        scene = "";
        cp.CallBackSlide();
    }
    else if (scene == null || scene == "") {
        //alert("scene is null I am Sending requeste to the server");	
        scene = "";
        //alert("<--backClick:HTMLRENDERING BACK-->" + FirstSceneName);
        cp.CallBackSlide();
    }
    else {
        getMovieName("movie").callBackButtonHandler(scene);
        //resetCPIdleTimer();
        //getMovieName("movie").callStopMovie(); 
    }
}
// function for flash volume
function getVolume(vol) {
    //getMovieName("movie").callGetVolumeInfo(); 
    //alert(vol);
}

function setVolume(volume) {       
    getMovieName("movie").callSetVolumeInfo(volume);
    //getMovieName("soundPlayer").callSetVolumeInfo(volume); 
    //getMovieName("movie").callSetVolumeInfo(volume); 
}

// functions for accordian
function onAccordian(panel) {
//    if (navigator.appName == "Microsoft Internet Explorer") {
//        document.getElementById("panelholder").style.removeAttribute('width');
//    }
//    else {
//        document.getElementById("panelholder").style.removeProperty('width');
//    }


    // $("#panelholder").animate({width: "234"}, 1200);
    //$("#panelholder").dequeue();

    //$(panel).animate({ width: "234" }, 1200);
    //$(panelbutton).animate({ left: "234" }, 1200); //Yasin code

    //$(panel).dequeue();
    // $("#panelholder").dequeue();

    //$("#panelholder").css('width','270px');


}

function offAccordian(panel) {




//    $(panel).animate({ width: "0" }, 1200);
//    $(panel).dequeue();

//    $("#panelholder").css('width', '10px');


}

function maxAccordian(panel) {
//    if (navigator.appName == "Microsoft Internet Explorer") {
//        document.getElementById("panelholder").style.removeAttribute('width');
//    }
//    else {
//        document.getElementById("panelholder").style.removeProperty('width');
//    }




//    $(panel).animate({ width: fullWidthAccordian }, 1200);
//    $("#panelbutton").removeAttr('class');
//    $("#panelbutton").addClass('arrowRight');
//    $("#panelbutton").animate({ left: fullWidthAccordian + "px" }, 1200); 		//Yasin code

//    //$("#panelbutton").css('backgroundImage', 'url(../images/panel_button_off.gif)');
//    $(panel).dequeue();


}

function minAccordian(panel) {


//    if (navigator.appName == "Microsoft Internet Explorer") {
//        document.getElementById("panelholder").style.removeAttribute('width');
//    }
//    else {
//        document.getElementById("panelholder").style.removeProperty('width');
//    }




//    $(panel).animate({ width: "234" }, 1200);
//    $("#panelbutton").removeAttr('class');
//    $("#panelbutton").addClass('arrowLeft');
//    $(panelbutton).animate({ left: "234" }, 1200); 					//Yasin code

//    //$("#panelbutton").css('backgroundImage', 'url(../images/panel_button_on.gif)');
//    $(panel).dequeue();
//    //alert("minAccordian");




}

function closeAccordian(panel) {

//    $(panel).animate({ width: "0" }, 1200);
//    $("#panelbutton").removeAttr('class');
//    $("#panelbutton").addClass('arrowLeft');
//    $(panelbutton).animate({ left: "0" }, 1200); 						//Yasin code

//    //$("#panelbutton").css('backgroundImage', 'url(../images/panel_button_on.gif)');
//    $(panel).dequeue();

//    $("#panelholder").css('width', '10px');

}



//  $("#block").animate({ width: "90%", fontSize: "10em", borderWidth: 10 }, 1000 );




var oldSliderValue = 50;
var currentSlideValue = oldSliderValue;
// function for slider
// USED FOR OLD COURSE PLAYER START
//$(function() {
//    $('#slider_callout').hide();
//    var calloutVisible = false;

//    $('.slider_bar').slider({
//        handle: '.slider_handle',
//        minValue: 0,
//        maxValue: 100,
//        steps: 10,
//        start: function(e, ui) {

//            //if(Math.round(ui.value)!=0){
//            $('#slider_callout').fadeIn('fast', function() { calloutVisible = true; });
//            //}

//        },
//        stop: function(e, ui) {
//            if (calloutVisible == false) {
//                //if(Math.round(ui.value)!=0){
//                $('#slider_callout').fadeIn('fast', function() { calloutVisible = true; });
//                //}
//                $('#slider_callout').css('left', ui.handle.css('left')).text(Math.round(ui.value));
//            }
//            $('#slider_callout').fadeOut('fast', function() { calloutVisible = false; });
//            currentSlideValue = Math.round(ui.value);

//            //alert(Math.round(ui.value)); //here I place the flash function
//        },
//        slide: function(e, ui) {
//            $('#slider_callout').css('left', ui.handle.css('left')).text(Math.round(ui.value));
//            setVolume(Math.round(ui.value));

//            //alert("slide"); here I place the flash function
//        }
//    });

//    $('.minusButton').bind('click.namespace', function() {
//        SliderMinus();
//        //alert("asdd");
//    });
//    $('.plusButton').bind('click.namespace', function() {
//        SilderPlus();
//    });
//    initSlider(100);
//});
// USED FOR OLD COURSE PLAYER END
//initSlider(100);


//function SilderPlus() {

//    //
//    var isSoundOn = $("input[@id='SoundOn']").attr("checked");
//    // var var_SoundOff =  $("input[@id='SoundOff']").attr("checked");
//    if (isSoundOn) {

//        var val = setSlider();

//        if (val != 100) {
//            initSlider(val + 10);
//            setVolume(val + 10)
//        }
//        else {
//            initSlider(100);
//            setVolume(100);
//        }

//    }
//}


//function SliderMinus() {

//    var isSoundOn = $("input[@id='SoundOn']").attr("checked");
//    //var var_SoundOff =  $("input[@id='SoundOff']").attr("checked");

//    if (isSoundOn) {

//        var val = setSlider();
//        if (val != 0) {
//            initSlider(val - 10);
//            setVolume(Math.round(val - 10));
//        } else {
//            initSlider(0);
//            setVolume(0);

//        }
//    }
//}


//function initSlider(val) {
//    $('.slider_bar').slider("moveTo", val, null, false);
//}

function initSetSlider(val) {
    $('.slider_bar').slider("option", 'value', val);
}

function setSlider() {
    //$('.slider_bar').slider("moveTo", "+="+num+"");
    return $('.slider_bar').slider("value");
}


//function soundOnOffHandler(val) {
//    if (val == 'Off') {
//        $('.volume-ctrl').parent().addClass('hide');
//        setVolume(0);
//    } else if (val == 'On') {
//        $('.volume-ctrl').parent().removeClass('hide');
//        setVolume(80);
//    }
//}
// centring the windows...
function centerDialogueWindow(dialogueWindow) {
    var windowWidth = document.documentElement.clientWidth;
    var windowHeight = document.documentElement.clientHeight;

    var popupHeight = dialogueWindow.height();
    var popupWidth = dialogueWindow.width();

    dialogueWindow.css({
        "position": "absolute",
        "top": windowHeight / 2 - popupHeight / 2,
        "left": windowWidth / 2 - popupWidth / 2
    });
}

var isMobile = {
    Android: function() {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function() {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function() {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function() {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function() {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function() {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

function loadSound(url) {
    //alert("soundplayer"+getMovieName("soundPlayer").callLoadSound(url));
    //getMovieName("soundPlayer").callLoadSound(url);
    getMovieName("movie").callLoadSound(url);

    IsSoundPlaying = true;
}
function stopSound() {

    if (IsSoundPlaying == true) {
        //getMovieName("soundPlayer").callStopSound();
        if ((isMobile.iOS() || isMobile.Android() || isMobile.BlackBerry())) {
            document.getElementById("AudioMobile").innerHTML = '';
            $('#AudioMobile').hide();
            IsSoundPlaying = false;
        }
        else {
            getMovieName("movie").callStopSound();
            IsSoundPlaying = false;
        }
    }
}
function startSound() {
    //getMovieName("soundPlayer").callPlaySound();
    getMovieName("movie").callPlaySound();

    IsSoundPlaying = true;
}

String.prototype.trim = function() {
    return this.replace(/^\s*/, "").replace(/\s*$/, "");
}

function CourseIdleTimeOut() {
    var com = new CommunicationEngine();
    com.CourseIdleTimeOut();
}

/////////////Class to display loading overlays and images 
function LoadingEngine() {

    this.OnLoadContentArea = function(caption) {
        //$('#masked_div').show();

    }
    this.OnReadyContentArea = function() {
        //$('#masked_div').hide();

    }
}


function enable_disable_btn(isTrue, trg)
{
    if(isTrue)
    {
         $(trg).attr("disabled","disabled").addClass("disabled");
    }
    else
    {
        $(trg).removeAttr("disabled","disabled").removeClass("disabled");
    }
}
////////////////////////////////////////
