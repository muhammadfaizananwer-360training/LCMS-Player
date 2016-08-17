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
    var dynamicContent={Support:{}};
    var InstructorContent={Information:{}};
    var CourseExitButton={Course:{}};
    var IsSoundOnOff;
    var CourseDescription;
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
                $("#focusOutLockedOutMessage").find("#warning").text(assessment_focusOutLockedOutMessage_warning);
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutMessageText").html(assessment_focusOutLockedOutMessage_text);
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutMessageTimer").html(assessment_focusOutLockedOutMessage_timer);
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonNo").text(assessment_focusOutLockedOutButtonNo_text);
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonYes").text(assessment_focusOutLockedOutButtonYes_text);
                ui.svgModal.open($("<a data-group='modal-ClickAway'></a>"));

                //$("#focusOutLockedOutMessage").fadeIn("slow");
                //$(overlay).css({ "opacity": "0.7" });
                //$(overlay).fadeIn("slow");

                document.getElementById("spanClickAwayTimer").innerHTML = (clickAwayTimeOutinMilliSeconds / 1000);

                blinkingCheck = true;
                StartBlinking(assessment_focusOutLockedOutMessage_warning);

                lockoutTimeOutVar = setInterval(function() {

                    document.getElementById("spanClickAwayTimer").innerHTML = (parseInt(document.getElementById("spanClickAwayTimer").innerHTML) - 1);

                    if (document.getElementById("spanClickAwayTimer").innerHTML == "0") {
                        //$("#focusOutLockedOutMessage").fadeOut("slow");
                        //$(overlay).fadeOut("slow");
                        
                        window.clearInterval(lockoutTimeOutVar);                        
                        lockoutTimeOutVar = null;
                        ui.slide.next(function()
                        {
                            StopBlinking();
                            cp.CourseLockDueToInActiveCurrentWindow();
                        });                        
                        
                        ISCourseLocked = true;
                        ClearAfterCourseLockOut();
                    }
                }, 1000);


                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonYes").unbind('click.namespace');                
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonYes").bind('click.namespace', function() {                                    
                    window.clearInterval(lockoutTimeOutVar);
                    ui.svgModal.close('modal-ClickAway');
                    lockoutTimeOutVar = null;
                    ui.slide.next(function()
                    {            
                        StopBlinking();            
                        cp.CourseLockDueToInActiveCurrentWindow();                        
                    });
                    
                    ISCourseLocked = true;
                    ClearAfterCourseLockOut();
                    return false;
                });


                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonNo").unbind('click.namespace'); 
                $("#focusOutLockedOutMessage").find("#focusOutLockedOutButtonNo").bind('click.namespace', function() {                    
                    window.clearInterval(lockoutTimeOutVar);
                    StopBlinking();                    
                    lockoutTimeOutVar = null;
                    return false;
                });
                
                $("#focusOutLockedOutMessage").find("#ClickAwayClose").unbind('click.namespace'); 
                $("#focusOutLockedOutMessage").find("#ClickAwayClose").bind('click.namespace', function() {                    
                    window.clearInterval(lockoutTimeOutVar);
                    StopBlinking();
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
            if ($("#focusOutLockedOutMessage").hasClass("modal-is-visible"))
            {        
                ui.svgModal.close('modal-ClickAway');
            }             
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
        //$(overlay).fadeOut("slow");

        var re = cp.getRenderEngineInstance();
        re.isIdleTimerReset = true;
        re.lastActivityTimeStamp = new Date();
        resetCPIdleTimer();
        cp.ResetSessionTime();

    }
    // Waqas Zakai
    // LCMS-10318
    // END
    
    function ProctorLockCourseDialog()
    {
     var communicationEng = new CommunicationEngine();
     var coursePlayerEng = new CoursePlayerEngine();
     var IsLocked = communicationEng.IsProctorLockedCourse();
          if (IsLocked != null) {           
           coursePlayerEng.CommandHelper(IsLocked);
    
    }
    
  }  
    