/* 
Render Engine updated October 06, 2008;
*/
var atTOC = 0;
var whenToShowIdleTimePopup = null;
var idleWarningTime = null;
var runningIdleWarningTime = null;
var idleTimeMsgHeading = null;
var idleTimeMsgContent = null;
var lastActivityTimeStamp = new Date();
var isIdleTimerReset = false;
var idleTimeNotification = new IdleTimeNotificationClass();
var isIdleTimeElapsed = false;
var playerFileName = "player_as2";
var JSplayerEnabled = false;
var playPauseFeature = false;
var xmlSampleData_ForCustomTemplate = "";

//LCMS-10392
var ls360AmazonProducts = null;

var Hourtxt;
var Hourstxt;
var Minutetxt;
var Minutestxt;
var Secondtxt;
var Secondstxt;

//Abdus Samad 
//LCMS-11878
//Start
var ls360RecommendationCourseProducts = null;
//Stop


//LCMS-8188 - Start
var isBrowserVersionIE6 = false;

if(BrowserDetect.browser == 'Explorer'){
    if (BrowserDetect.version == 6) {
        isBrowserVersionIE6 = true;
    }
}
//LCMS-8188 - End

// LCMS-5926
// -------------------------------------------
var minimumTimeBeforeStartingPostAssessment = 0;
var minimumTimeBeforeStartingPostAssessmentUnit = "";
var minimumTimeMetMessage = "";
// -------------------------------------------

function noRightClick() {
    return false;
}

//LCMS-12532 Yasin
function disableNext() 
{
    if ($('#PlaybuttonDs').css('display') == 'none') {
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
    }
}

function convertJSString(str) {
    // Convert problem characters to JavaScript Escaped values
    if (str == null || str == "") {
        return "";
    }

    str = str.replace(/&/g, '&amp;')
    str = str.replace(/"/g, '&quot;')
    str = str.replace(/'/g, '&#39;')
    str = str.replace(/</g, '&lt;')
    str = str.replace(/>/g, '&gt;');
    str = str.replace(/\\/g, '&#92;');
    return str;

}


function disableSelection(target) {
    if (typeof target.onselectstart != "undefined") //IE route
        target.onselectstart = function() { return false }
    else if (typeof target.style.MozUserSelect != "undefined") //Firefox route
        target.style.MozUserSelect = "none"
    else //All other route (ie: Opera)
        target.onmousedown = function() { return false }
    target.style.cursor = "default"
}
function replaceSpacesAndLineBreaks(val) {
    // To replace multiple line-breaks
    var reNewLines = /[\n\r]/g;
    var val = val.replace(reNewLines, "<br />");

    // To replace multiple spaces
    for (var j = 0; j < val.length; j++) {
        var newVal = val.replace("  ", "&nbsp;&nbsp;");
        if (val == newVal) {
            break;
        }
        val = newVal;
    }

    return val;
}

function StripTagsCharArray(source) {
    if (source != null) {
        var array = new Array();
        //var sarray = new sArray(source);
        var val = "";
        var arrayIndex = 0;
        var inside = false;

        for (var i = 0; i < source.length; i++) {
            var let = (source.charAt(i));

            if (let == '<') {
                inside = true;
                continue;
            }
            if (let == '>') {
                inside = false;
                continue;
            }
            if (!inside) {
                val = val.concat(let);
                arrayIndex++;
            }
        }
        return val
    }
    return null;

}

function loadjscssfile(filename) {
    var fileref = document.createElement("link");
    fileref.setAttribute("rel", "stylesheet");
    fileref.setAttribute("type", "text/css");
    fileref.setAttribute("href", filename + "?t=" + Number(new Date()));
    document.getElementsByTagName("head")[0].appendChild(fileref);
}

function initialize(server, application, video,displayratio ) {
    try {
        //thisMovie("videoplayer").SetVariable("uri", "rtmp://"+server+"/"+application);
        //thisMovie("videoplayer").SetVariable("file", video);       
        thisMovie("videoplayer").sendVariables(server, application, video , displayratio );
    } catch (e) {    //alert(e.Message);
        var funcName = "initialize('" + server + "','" + application + "','" + video + "','"+ displayratio +"')";
        setTimeout(funcName, 500);
    }
}

function thisMovie(movieName) {
    if (navigator.appName.indexOf("Microsoft") != -1) {
        return window[movieName];
    }
    else {
        return document.embeds[movieName];
    }
}



var lockCourseTimer = new LockCourseTimerClass();
var validationTimerObj = new ValidationTimerClass();
var assessmentTimerObj = new AssessmentTimerClass();
var odometerTimerObj = new OdometerTimerClass();
var courseTimerObj = new CourseTimerClass();
var validationQuestionID = null;
var validationAnswerText = null;
var validationQuestionType = null;
var validationQuestionOptions = null;
var sceneTimer = new SceneTimer();
var isValidationStarted = true;
var validationTimerValue = null;

function RenderEngine() {
    var coursePlayerEngine = null;
    var isDialogueOpen = false;
    var isGlossaryDialogueOpen = false;
    var allowSkipping = false;
    var isSoundOff = true;
    var showGradeAssessment = false;
    //LCMS-10392
    var ls360AmazonProducts = null;

    //Abdus Samad 
    //LCMS-11878
    //Start
    var ls360RecommendationCourseProducts = null;
    //Stop



    this.GetIdleTimeNotificationObj = function() {
        return idleTimeNotification;
    }



    this.ShowSeatTimeExceed = function(ShowSeatTimeExceed) {
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        $(htmlContentContainer).html(ShowSeatTimeExceed.SeatTimeExceedMessage.TemplateHtml);
        
        $(controlPanel).find("#IcoInstructorInformation").hide();
        $(controlPanel).find("#IcoInstructorInformationDs").show();
        
        $(controlPanel).find("#IcoTOC").hide();
        $(controlPanel).find("#IcoTOCDs").show();        
        
        $(controlPanel).find("#IcoGlossary").hide();
        $(controlPanel).find("#IcoGlossaryDs").show();                
        
        $(controlPanel).find("#IcoCourseMaterial").hide();
        $(controlPanel).find("#IcoCourseMaterialDs").show();                        

        $(controlPanel).find("#modal-trigger-bookmark").hide();
        $(controlPanel).find("#cd-tour-trigger").hide();

        $(controlPanel).find("#IcoConfigure").hide();
        $(controlPanel).find("#IcoConfigureDs").show();

        $(controlPanel).find("#IcoHelp").hide();
        $(controlPanel).find("#IcoHelpDs").show();

        $(controlPanel).find("#IcoCourseCompletion").hide();
        $(controlPanel).find("#IcoCourseCompletionDs").show();

        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $(NextQuestionButtonEn).hide();
        //progress bar
        //progress bar
        //$("#progressbar").progressbar('destroy');
        $("#progressbar").attr('style','width: 100%');
        //$("#progressbar").progressbar({ value: 100 });
        //To set from branding////////
        //$('ProgressBar').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
        //$('progressBarValueDiv').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');

        $("#progressbar").attr("title", "");
        //$(ProgressBarTxt).attr("title", "");

        $('#assessmentControlPanel').hide();
    }

    this.LocalizationRendering = function(resourceInfo) {

        //jQuery().ready(function(){
        for (var index = 0; index < resourceInfo.ResourceInfo.length; index++) {
            switch (resourceInfo.ResourceInfo[index].ResourceKey) {
                case "PlayerCSS":
                    {
                        //dynamically load and add this .css file
                        loadjscssfile(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "HeadingFooterNote":
                    {
                        // alert(resourceInfo.ResourceInfo[index].ResourceValue);
                        $('#footer .copyRight').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "HeadingTOC":
                    {

                        //$(panel + " h3").eq(0).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingTOC").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingTOCDs").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "HeadingCourseMaterial":
                    {
                        //$(panel + " h3").eq(1).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingCourseMaterial").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingCourseMaterialDs").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "HeadingGlossary":
                    {
                        //$(panel + " h3").eq(2).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingGlossary").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingGlossaryDs").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "HeadingBookmark":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#HeadingBookmark").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "HeadingBookmarkDialog":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#bookmarkModalHeading").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "ContentBookmarkDialogTitle":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#bookmarkDialogHeading").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    } 
                case "ContentBookmark":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#bookmarkContent").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
               case "BookmarkAddButtonText":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $(bookmarkDialogue).find('button').eq(0).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
               case "BookmarkTextboxTitle":
                    {
                        //$(panel + " h3").eq(3).find('a').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $(bookmarkDialogue).find('input').eq(0).attr('placeholder', resourceInfo.ResourceInfo[index].ResourceValue);                        
                        break;
                    }                    
                case "ImageComanyLogo":
                    {
                        $(header + " img").attr('src', resourceInfo.ResourceInfo[index].ResourceValue);
                        //$(header+" img").attr('width', 177);
                        $(header + " img").attr('height', 25);
                        break;
                    }
                case "ImageLogoutButton":
                    {
                        $(logoutbutton + " a").css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');
                        $(logoutbutton + " a").css('backgroundPosition', '0 0');
                        $("#logoutbuttonDS").css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');
                        break;
                    }
                case "MenuStrip":
                    {
                        //$(logoutbutton+" a").css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue +')');
                        //$(logoutbutton+" a").css('backgroundPosition', '-552 0');
                        break;
                    }
                case "BGContainer":
                    {
                        //$(main_container).css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue +')');
                        break;
                    }
                case "BGCourseTitle":
                    {
                        $('#heading').css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');

                        break;
                    }
                case "BGControlPanel":
                    {
                        //$('#controlBar').css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');
                        break;
                    }

                case "HeadingValidateYourIdentity":
                    {
                        $(studentAuthentication).find('h2').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $(studentAuthentication).find('h1').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentValidateYourIdentity":
                    {
                        $(studentAuthentication).find('p').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentValidateYourIdentityButton":
                    {
                        $(studentAuthentication).find('button').eq(0).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentValidateYourIdentityTimerRemaining":
                    {
                        $(studentAuthentication).find('#authenticationTimer strong').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentForgetMyIdentityButton":
                    {
                        $(studentAuthentication).find('button').eq(1).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ImageValidateYourIdentity":
                    {
                        $(studentAuthentication).find('#authenticationIcon').css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');
                        break;
                    }


                case "HeadingBookmarkDialog":
                    {
                        $(bookmarkDialogue).find('h2').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentBookmarkDialogSubmitButton":
                    {
                        $(bookmarkDialogue).find('button').eq(0).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentBookmarkDialogCancelButton":
                    {
                        $(bookmarkDialogue).find('button').eq(1).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentBookmarkDialogTitle":
                    {
                        $(bookmarkDialogue).find('span').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "HeadingAnswerReview":
                    {
                        $('#AnswerReviewHeading h1').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentAnswerReview":
                    {
                        $('#AnswerReviewMessage p').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentFinishGradingButton":
                    {
                        $('#AnswerReviewButtons').find("button").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }


                case "HeadingIndividualQuestionScore":
                    {

                        //var headingIndividual = resourceInfo.ResourceInfo[index].ResourceValue;
                        //headingIndividual = headingIndividual.replace("$CORRECTICON", "ImageCorrectAnswer").replace("$INCORRECTICON", "ImageInCorrectAnswer");			        
                        //$('#IndividualScoreHeading h4').html(headingIndividual);
                        $('#IndividualScoreHeading').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                    //case "ImageIndividualQuestionScore":
                    //{
                    //    $('#AnswerReviewMessage p').html(resourceInfo.ResourceInfo[index].ResourceValue);
                    //    break;
                    //}

                case "ContentIndividualQuestionSHowAnswerButton":
                    {
                        $('#IndividualScoreButtons').find("button").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }



                case "HeadingAssessmentIncomplete":
                    {
                        $('#assessmentcontent h1').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentAssessmentIncomplete":
                    {
                        $('#assessmentcontent p').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ContentAnswerRemainingButton":
                    {
                        $('#buttoncontainer').find('div').eq(0).find("button").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }


                case "ContentContinueGradingButton":
                    {
                        $('#buttoncontainer').find('div').eq(1).find("button").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "HeadingQuestionFeedback":
                    {

                        $('#QuestionRemediationFeedback').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }

                case "ImageProgressBarGlass":
                    {
                        //ImageProgressBarGlass = resourceInfo.ResourceInfo[index].ResourceValue;
                        //$('ProgressBar').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
                        break;
                    }
                case "ImageProgressBarHighlight":
                    {
                        ImageProgressBarGlass = resourceInfo.ResourceInfo[index].ResourceValue;
                        $('progressBarValueDiv').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
                        break;
                    }
                case "ContentProgressBar":
                    {
                        //$(ProgressBarTxt).html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "ImageBGProgressBar":
                    {
                        //$('#progressBarContainter').css('backgroundImage', 'url(' + resourceInfo.ResourceInfo[index].ResourceValue + ')');
                        break;
                    }
                case "ImageInCorrectAnswer":
                    {
                        ImageIncorrect = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "ImageCorrectAnswer":
                    {
                        ImageCorrect = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "HeadingOdometer":
                    {
                        $('#odometerTxt').html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }
                case "MinimumSeatTimeMetMessage":
                    {
                        minimumTimeMetMessage = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Hour":
                    {
                        Hourtxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Hour":
                    {
                        Hourtxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Hours":
                    {
                        Hourstxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Minute":
                    {
                        Minutetxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Minutes":
                    {
                        Minutestxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Second":
                    {
                        Secondtxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }
                case "Seconds":
                    {
                        Secondstxt = resourceInfo.ResourceInfo[index].ResourceValue;
                        break;
                    }

                case "LoadingImage":
                    {
                        
                          $("#divLoading")[0].children[0].src = resourceInfo.ResourceInfo[index].ResourceValue;
                         //$("#divLoading")[0].children[0].attr('src', resourceInfo.ResourceInfo[index].ResourceValue);
                        break;

                    }
                case "NextkbuttonText":
                    {                           
                        $("#PlaybuttonEnText").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#PlaybuttonDsText").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#VNextQuestionButtonEnText").html(resourceInfo.ResourceInfo[index].ResourceValue);                        
                        break;
                    }
                case "PreviouskbuttonText":
                    {   
                        $("#BackbuttonEnText").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        $("#BackbuttonDsText").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }   
                case "SaveCloseHeading":
                    {                           
                        $("#SaveCloseHeading").html(resourceInfo.ResourceInfo[index].ResourceValue);                         
                        break;
                    }  
                case "SaveCloseText":
                    {   
                        $("#SaveCloseText").html(resourceInfo.ResourceInfo[index].ResourceValue);                         
                        break;
                    }
                case "ExitCourseButtonText":
                    {   
                        CourseExitButton["Course"]["ExitCourseButton"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }   
                case "StayContinueButtonText":
                    {   
                        CourseExitButton["Course"]["StayContinueButton"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }  
                case "AssessmentLogOutHeadingText":
                    {   
                        CourseExitButton["Course"]["AssessmentLogOutHeadingText"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }   
                case "LogOutHeadingText":
                    {   
                        CourseExitButton["Course"]["LogOutHeadingText"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }     
                case "LogOutText":
                    {   
                        CourseExitButton["Course"]["LogOutText"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }                                                       
                case "CourseCompletionReportHeading":
                    {   
                        $("#HeadingCourseCompletionReport").html(resourceInfo.ResourceInfo[index].ResourceValue); 
                        $("#HeadingCourseCompletionReportDs").html(resourceInfo.ResourceInfo[index].ResourceValue);
                        break;
                    }  
                case "CourseCompletionReportModalHeading":
                    {   
                        $("#CourseCompletionModalHeading").html(resourceInfo.ResourceInfo[index].ResourceValue);                         
                        break;
                    }
                case "CourseCompletionReportModalText":
                    {   
                        $("#CourseCompletionModalText").html(resourceInfo.ResourceInfo[index].ResourceValue);                         
                        break;
                    }
               case "SupportHeadingLeftNavigation":
                    {   
                        $("#HeadingSupport").html(resourceInfo.ResourceInfo[index].ResourceValue);  
                        $("#HeadingSupportDs").html(resourceInfo.ResourceInfo[index].ResourceValue);  
                        break;
                    }                    
               case "SupportHeading":
                    {   
                        dynamicContent["Support"]["Heading"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }
               case "SupportContent":
                    {   
                        dynamicContent["Support"]["Content"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }  
               case "SupportButtonText":
                    {   
                        dynamicContent["Support"]["ButtonText"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    } 
               case "LiveChatButtonText":
                    {   
                        dynamicContent["Support"]["LiveChatButtonText"]=resourceInfo.ResourceInfo[index].ResourceValue;                        
                        break;
                    }                                                                                                                                                                                                                                                                          
            }
        }
        //})
    }

    this.LoadCourseRendering = function(obj, idleTimerObj) {
        //alert("load course call"+obj.CourseInfo.ValidationQuestionAskingTimer);


        $("#ValidationControlBar").hide();
        var CourseName = obj.CourseInfo.CourseName;
        CourseDescription = obj.CourseInfo.CourseDescription;
        var IdleTimeOut = obj.CourseInfo.IdleTimeOut;
        var ExpireTimeout = obj.CourseInfo.ExpireTimeout;
        var CourseTimer = obj.CourseInfo.CourseTimer;
        validationTimerValue = obj.CourseInfo.ValidationQuestionAskingTimer;
        var initialvalidationTimerValue = obj.CourseInfo.InitialValidationQuestionAskingTimer;
        var totaltimespent = obj.CourseInfo.TotalTimeSpent;
        minimumTimeBeforeStartingPostAssessment = obj.CourseInfo.MinimumTimeBeforeStartingPostAssessment;
        minimumTimeBeforeStartingPostAssessmentUnit = obj.CourseInfo.MinimumTimeBeforeStartingPostAssessmentUnit;
        var showInstructorInfo = obj.CourseInfo.ShowInstructorInfo;
        globalshowInstructorInfo = obj.CourseInfo.ShowInstructorInfo;
        var showAmazonAffiliatePanel = obj.CourseInfo.ShowAmazonAffiliatePanel;
        $('#coursetitle').html(CourseName);               
        if(CourseDescription.length > 149)
        {            
            $('#CourseDescriptionLeftNavigation').html(CourseDescription.substring(0, 150));
            $('#read-more-1').show();
        }
        else
        {            
            $('#CourseDescriptionLeftNavigation').html(CourseDescription);
            $('#read-more-1').hide();
        }
        
        //Waqas Zakai
        //LCMS-14012
        //Start     
        restrictIncompletJSTemplate = obj.CourseInfo.RestrictIncompleteJSTemplate;        
        //END
                
        //Abdus Samad
        //LCMS-11878
        //Start     
        var showCoursesRecommendationPanel = obj.CourseInfo.ShowCoursesRecommendationPanel;
        //Stop

        if (showInstructorInfo) {
            $('#InstructorInformation').show();
            InstructorContent["Information"]["Heading"]=$('#InstructorInformation a').html();
            InstructorContent["Information"]["Content"]=obj.CourseInfo.ShowInstructorText;
            InstructorContent["Information"]["Image"]=obj.CourseInfo.ShowInstructorImage;
            InstructorContent["Information"]["DefaultImage"]='assets/img/author_86x86.png';
            
        }
        else {
            $('#InstructorInformation').hide();
        }

        if (showAmazonAffiliatePanel) {
            $('#IcoAmazonAffiliatePanel').show();
            $('#IcoAmazonAffiliatePanelDs').hide();
        }
        else {
            $('#IcoAmazonAffiliatePanel').hide();
            $('#IcoAmazonAffiliatePanelDs').hide();
        }

        //Abdus Samad
        //LCMS-11878
        //Start

        if (showCoursesRecommendationPanel) {
            $('#IcoRecommendationCoursePanel').show();
            $('#IcoRecommendationCoursePanelDs').hide();
        }
        else {
            $('#IcoRecommendationCoursePanel').hide();
            $('#IcoRecommendationCoursePanelDs').hide();
        }
        //Stop





        //Fix for LCMS-11085
        if (obj.CourseInfo.logOutText != null && obj.CourseInfo.logOutText != "") {
            logOutText = obj.CourseInfo.logOutText;
        }
        // End fix for LCMS-11085

        /*
        var hours=0;
        var minutes=0;
        var seconds=0;
		
		if(totaltimespent==0)
        {
        hours=0;
        minutes=0;
        seconds=0;
        }
		
		if( (totaltimespent / 3600) >=1)
        {		   
        hours=Math.floor(totaltimespent /3600);
        minutes=Math.floor(((totaltimespent /3600) - hours) * 100);           
        }
        else
        {		   
        hours=0;
        minutes=Math.floor((totaltimespent/60));           
        }		
        */
        var totaltimespentDateTime = obj.CourseInfo.TotalTimeSpentStr; //hours+":"+minutes+":"+seconds;         
        odometerTimerObj.TimerContainer($("#odometer"));        
        odometerTimerObj.InitializeTimer(totaltimespentDateTime);
        //alert("ValidationQuestionAskingTimer"+obj.CourseInfo.ValidationQuestionAskingTimer+"initialvalidationTimerValue:"+obj.CourseInfo.InitialValidationQuestionAskingTimer);

        //jQuery().ready(function(){
        $(heading).find("#courseName").html(CourseName);
        //$(heading).html(CourseName);
        //});	


        //$("#preloader").css('display', 'none');
        //courseTimerObj.TimerContainer(timer); 
        //courseTimerObj.InitializeTimer("0:00");

        if (IdleTimeOut != -1) {            
            idleTimerObj.InitializeTimer(IdleTimeOut);
        }

        //if(validationTimerValue != -1)

        if (initialvalidationTimerValue != -1) {

            //validationTimerObj.InitializeTimer(validationTimerValue);
            //alert(initialvalidationTimerValue);
            validationTimerObj.InitializeTimer(initialvalidationTimerValue);

        }
    }

    this.TableOfContentRendering = function(tableOfContent) {

        var tableOfContentList = parseTocJson(tableOfContent);
        //alert(tableOfContentList);
        //jQuery().ready(function(){	
        
        $(toc).html(tableOfContentList);
        ui.nav.nestedBtn();
//        $(document).ready(function() {
//            //$("#browser").treeview();
//        });
        //});

    }

    this.GlossaryRendering = function(glossaryObject) {
        var glossaryList = parseGlossaryJson(glossaryObject);
        //jQuery().ready(function(){
        if(glossaryList == undefined)
        {
            //$(glossh3).attr('style','display:none');        	
            $("#Glossary").attr('style','display:none');            
        }
        else
        {	
            $(gloss).html(glossaryList);
        }
    }

    this.BookMarkRendering = function(bookmarkObject) {
        var bookmarksList = parseBookMarkJson(bookmarkObject);
        //jQuery().ready(function(){
        
        if(bookmarksList == undefined)
        {
            //$(bookmarksh3).attr('style','display:none');             
        }
        else
        {	
            //$(bookmarksh3).attr('style','display:block');
            $(bookmarks).html(bookmarksList);            
        }
        //});
    }

    this.CourseMaterialRendering = function(courseMaterialObject) {
        var courseMaterialList = parseCourseMaterialJson(courseMaterialObject);
        //jQuery().ready(function(){
        if(courseMaterialList == undefined)
        {
             $("#Material").attr('style','display:none');
        }
        else
        {	
             $(course_material).html(courseMaterialList);
        }
       
    }

    this.StartAssessmentMessageRendering = function(startAssessmentObj) {
        
//        //alert('StartAssessmentMessageRendering');
//        // To check either skipping of Question is allowed or not in assessment
//        //startAssessmentObj.StartAssessmentMessage.AssessmentTimer = 120;
//        allowSkipping = startAssessmentObj.StartAssessmentMessage.AllowSkipping;
//        isLockoutClickAwayToActiveWindowStart = startAssessmentObj.LockoutClickAwayToActiveWindow;        
//        //courseTimerObj.StopTimer();
//        //jQuery().ready(function(){	
//        //$(overlay).css({ "opacity": "0.7" });
//        //$(overlay).css("display", "block");
//        //$(dialog).css("display", "block");


//        $(dialog).find("h2").html(startAssessmentObj.StartAssessmentMessage.StartupHeading);
//        $(dialog).find("#dialogHeading h1").html(startAssessmentObj.StartAssessmentMessage.StartupHeading);
//        $(dialog).find("#Icon").css('backgroundImage', 'url(' + startAssessmentObj.StartAssessmentMessage.StartupImageUrl + ')');
//        $(dialog).find("p").html(startAssessmentObj.StartAssessmentMessage.StartupMessage);
//        $(dialog).find("button").html(startAssessmentObj.StartAssessmentMessage.ButtonText);


//        $(dialog).find("button").bind('click.namespace', function() {
//            $(dialog).find("button").unbind('click.namespace');
//            //$(overlay).css("display", "none");
//            //$(dialog).css("display", "none");

//            if (startAssessmentObj.StartAssessmentMessage.AssessmentTimer != -1) {
//                $(assessmentTimer).show();
//                assessmentTimerObj.TimerContainer(assessmentTimer);
//                //alert('AssessmentTimer:'+startAssessmentObj.StartAssessmentMessage.AssessmentTimer);
//                assessmentTimerObj.InitializeTimer(startAssessmentObj.StartAssessmentMessage.AssessmentTimer);
//            } else {
//                // alert('hide');
//                $(assessmentTimer).hide();

//            }


//            startAssessmentMessageClick();
//            resetCPIdleTimer();


//            return false;
//        });


    }  // StartAssessmentMessageRendering end...
    this.ShowSlideRendering = function(showSlideObject) {

        //alert("dsfsdfsdfsd" + showSlideObject);
        //courseTimerObj.StartTimer();
        $(htmlContentContainer).find('span').empty();



        if (showSlideObject.MediaAsset != null) {
            //jQuery().ready(function(){	

            if (!demo) {
                $("#progressbar").attr('style','width: '+ showSlideObject.MediaAsset.CourseProgressPercentage +'%');
                // USED BY OD COURSE PLAYER START
                //$("#progressbar").progressbar('destroy');
                
                //$("#progressbar").progressbar({ value: showSlideObject.MediaAsset.CourseProgressPercentage });
                // USED BY OD COURSE PLAYER END
                //To set from branding
                //$('ProgressBar').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
                //$('progressBarValueDiv').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');

                $("#progressbar").attr("title", showSlideObject.MediaAsset.CourseProgressToolTip);
                //$(ProgressBarTxt).attr("title", showSlideObject.MediaAsset.CourseProgressToolTip);
            }
            else {
                $(ProgressBarContainer).hide();
            }
            $(assessmentControlPanel).hide();
            $(controlPanel).show();
            $("#controlPanel").show();
            //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
            //$('#odometerContainter').css('margin-top', '0px');
            $(swfContainer).css("display", "block");
            $(htmlContentContainer).css("display", "none");
            if (showSlideObject.MediaAsset.RemidiationMode) {


                $(controlPanel).find("#ShowQuestionButton").show();
                $(controlPanel).find("#ShowQuestionButton").css('margin-left', '325px');
                //$('#odometerContainter').css('margin-top', '-27px');
                $(controlPanel).find("#CourseMenuIcons").hide();
                $("#timer").hide();
                //$(controlPanel).find("#timer").hide();
                $(ProgressBarContainer).hide();
            } else {
                $(controlPanel).find("#ShowQuestionButton").hide();

                if (!demo) {
                    $(controlPanel).find("#CourseMenuIcons").show();
                }

                // $(controlPanel).find("#timer").show();
            }

            //})

            

            $(controlPanel).find("#ShowQuestionButton").unbind('click.namespace');
            $(controlPanel).find("#ShowQuestionButton").bind('click.namespace', function() {
                $(controlPanel).find("#ShowQuestionButton").unbind('click.namespace');
                cp.GoContentTOQuestion();
                return false;
            });




            slideID = showSlideObject.MediaAsset.MediaAssetID;
            sceneGUID = showSlideObject.MediaAsset.MediaAssetSceneID;


            if (showSlideObject.MediaAsset.ContentObjectID != 0) {
                findToc(showSlideObject.MediaAsset);
                // $("#contentObjectName").html(showHTMLObject.MediaAsset.TitleBreadCrumb);
                //Fix for LCMS-2296				   
                $("#contentObjectName").html("");
            }
            if (showSlideObject.MediaAsset.EnableAllTOC == true) {
                EnableAllToc();
            }



            currentframeNumber = showSlideObject.MediaAsset.FlashSceneNo;
            LastScene = showSlideObject.MediaAsset.LastScene;
            nextButtonState = showSlideObject.MediaAsset.NextButtonState;
            isMovieEnded = showSlideObject.MediaAsset.IsMovieEnded;
            FirstSceneName = showSlideObject.MediaAsset.FlashFirstSceneName;


            FirstScene = true;


            //alert(showSlideObject.MediaAsset.FlashURL);
            //http://localhost/player/prequiz.swf
            
            loadMovie(showSlideObject.MediaAsset.FlashURL, showSlideObject.MediaAsset.FlashSceneNo);
            
            //loadMovie("lesson_1.swf", showSlideObject.MediaAsset.FlashSceneNo);
        }

    }
    //// LCMS-2857
    this.ShowEOCInstructions = function(EOCInstructionObj) {


        var htmlData = "";
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;


        htmlData += "<section class='scene-wrapper visual-right'><div class='scene-body'><div class='EOCInstructions'><h2>" + EOCInstructionObj.EOCInstructions.HeadingEOCInstructions + "</h2></div>";
        // FOR LMS			<h3>Course</h3> <h3>Course Configuration</h3> <h3>End Of Course Instruction</h3>class='EOCInstructions'
        if (EOCInstructionObj.EOCInstructions.LMS_VU == 0) {
            if (EOCInstructionObj.EOCInstructions.CourseEOCInstructions != '') {
                htmlData += "<div class='EOCInstructions'>" + EOCInstructionObj.EOCInstructions.CourseEOCInstructions + "</div>";
                if (navigator.appName.indexOf("Microsoft") != -1) {
                    htmlData += "<br/>";
                }
            }
            if (EOCInstructionObj.EOCInstructions.CourseConfigurationEOCInstructions != '') {
                htmlData += "<div class='EOCInstructions'>" + EOCInstructionObj.EOCInstructions.CourseConfigurationEOCInstructions + "</div>";
                if (navigator.appName.indexOf("Microsoft") != -1) {
                    htmlData += "<br/>";
                }
            }
            if (EOCInstructionObj.EOCInstructions.BrandingEOCInstructions != '') {
                htmlData += "<div class='EOCInstructions'>" + EOCInstructionObj.EOCInstructions.BrandingEOCInstructions + "</div><br/>";
            }
        }
        // FOr VU
        else if (EOCInstructionObj.EOCInstructions.LMS_VU == 1) {
            htmlData += "<div class='EOCInstructions'>" + EOCInstructionObj.EOCInstructions.CourseEOCInstructions + "</div><br/>";
        }
        htmlData +="</div></section>";

        $(htmlContentContainer).html(htmlData);
        isMovieEnded = true;


        // LCMS-3164
        // -------------------------------------
        $("#NextQuestionButtonEn").hide();
        $(controlPanel).show();
        $("#controlPanel").show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        //$('#odometerContainter').css('margin-top', '0px');
        $(PlaybuttonEn).show();
        $(PlaybuttonDs).hide();
        $(BackbuttonEn).show();
        $(BackbuttonDs).hide();

        // -------------------------------------
        if (globalshowInstructorInfo == true) {
            $('#InstructorInformation').show();
        }


    }


    this.ShowCourseEndTextForCourseReviewPolicy = function(ShowCourseEndTextForCourseReviewPolicyObj) {


        var htmlData = "";
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        var txtMsg = ShowCourseEndTextForCourseReviewPolicyObj.CourseEndTextOnRelaunch.CourseEndText;

        //htmlData += '<style>table {height:100%;width:100%;margin:auto;padding : 10px 10px 5px 10px}.mainimage {max-width: 350px;vertical-align:top;}.sceneTitle {font-family : Tahoma, Arial, Verdana, Helvetica, sans-serif; font-size : 30px; text-decoration : bold;vertical-align:top;}.sceneTextArea {font-family : Arial;font-size : 16px;font:bold;text-align: left;vertical-align:top;}</style><table width="95%" height="100" cellspacing="0" cellpadding="0" border="0" align="center"style="height: 100px ! important;" valign="center"><tbody><tr><td valign="top" style="height: 30px ! important;"><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td width="10%" valign="top"><img src="Images/course_certificate.png" class="mainimage" /></td><td width="90%" valign="top" style="padding-top: 30px;"><span class="sceneTitle">'+CourseReviewnotAllowedText+'</span></td></tr></tbody></table></td></tr><tr><td valign="top" height="10%" colspan="3"><span class="sceneTextArea">' + txtMsg + '</span></td></tr></tbody></table>';
        htmlData += '<section class="scene-wrapper visual-right"><div class="scene-body"><img id="image" class="img-responsive"  src="assets/img/course_certificate.png" /><h1 class="scene-title">'+CourseReviewnotAllowedText+'</h1><div class="scene-cell"><div class="scene-content"><p>' + txtMsg + '</p></div></div></div></section>';

        $(htmlContentContainer).html(htmlData);
        isMovieEnded = true;

        $(controlPanel).find("#IcoInstructorInformation").hide();
        $(controlPanel).find("#IcoInstructorInformationDs").show();        

        $(controlPanel).find("#IcoTOC").hide();
        $(controlPanel).find("#IcoTOCDs").show();        

        $(controlPanel).find("#IcoGlossary").hide();
        $(controlPanel).find("#IcoGlossaryDs").show();        

        $(controlPanel).find("#IcoCourseMaterial").hide();
        $(controlPanel).find("#IcoCourseMaterialDs").show();        
                
        $(controlPanel).find("#modal-trigger-bookmark").hide();
        $(controlPanel).find("#cd-tour-trigger").hide();

        $(controlPanel).find("#IcoConfigure").hide();
        $(controlPanel).find("#IcoConfigureDs").show();

        $(controlPanel).find("#IcoHelp").hide();
        $(controlPanel).find("#IcoHelpDs").show();

        $(controlPanel).find("#IcoCourseCompletion").hide();
        $(controlPanel).find("#IcoCourseCompletionDs").show();

        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();

        //progress bar
        // USED BY OD COURSE PLAYER START
        //$("#progressbar").progressbar('destroy');
        $("#progressbar").attr('style','width: 100%');
        //$("#progressbar").progressbar({ value: 100 });
        // USED BY OD COURSE PLAYER END
        //To set from branding////////
        //$('ProgressBar').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
        //$('progressBarValueDiv').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');

        $("#progressbar").attr("title", "");
        //$(ProgressBarTxt).attr("title", "");
        //

    }



    this.ShowCourseCertificate = function(CourseCertificateObj) {


        var htmlData = "";
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        htmlData = CourseCertificateObj.Certificates.TemplateHtml;

        if (CourseCertificateObj.Certificates.DownloadButtonEnabled == false) {
            htmlData = htmlData.replace('<div class="btn-start"></div>', '<div class="btn-start-ds"></div>');
            htmlData = htmlData.replace('<div class="btn-end"></div>', '<div class="btn-end-ds"></div>');
        }
        $(htmlContentContainer).html(htmlData);

        isMovieEnded = true;

        if (CourseCertificateObj.Certificates.DownloadButtonEnabled == true) {
            $(IAgreeButton).find("a").bind('click.namespace', function() {
                if (CourseCertificateObj.Certificates.CertificateURL != "") {
                    cp.DownloadCourseApprovalCertificate(CourseCertificateObj.Certificates.CertificateURL);
                }
                else {
                    cp.DownloadCertificate();
                }
                return false;
            });
        }
        else {
            $(IAgreeButton).find("a").unbind('click.namespace');
            $(IAgreeButton).find("a").attr("class", "btn-stem-ds");
        }

        $("#NextQuestionButtonEn").hide();
        $(controlPanel).show();
        $("#controlPanel").show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        $(PlaybuttonEn).show();
        $(PlaybuttonDs).hide();
        $(BackbuttonEn).show();
        $(BackbuttonDs).hide();


    }

    this.ShowEmbeddedAcknowledgment = function(EmbeddedAcknowledgmentObj) {

        $('#PlaybuttonDocuSign').remove(); //Added By Abdus Samad to avoid dublicate buttons  

        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        var htmlData = "";
        htmlData = EmbeddedAcknowledgmentObj.EmbeddedAcknowledgment.TemplateHtml;
        $(htmlContentContainer).html(htmlData);
        $(NextQuestionButtonEn).hide();
        $(controlPanel).show();
        $("#controlPanel").show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        //$('#odometerContainter').css('margin-top', '0px');
        $(PlaybuttonEn).hide();
        $(BackbuttonEn).show();
        $(PlaybuttonDs).show();
        $(BackbuttonDs).hide();
        $(IAgreeButton).find("a").bind('click.namespace', function() {
            cp.PlayClick();
            ui.slide.next(function()
            {   
                cp.SynchToExternalSystem("ShowEmbeddedAcknowledgment");
            });	        
        });

    }
    // LCMS-11877

    this.ShowCourseLevelRating = function(courseRatingObj) {

    $('#PlaybuttonDocuSign').remove(); //Added By Abdus Samad to avoid dublicate buttons
        
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        var htmlData = "";


        userRating = Number(courseRatingObj.CourseLevelRating.CurrentUserCourseRating);
        CourseLevelRating = courseRatingObj.CourseLevelRating;

        htmlData = courseRatingObj.CourseLevelRating.TemplateHtml;
        $(htmlContentContainer).html(htmlData);
        $('#CourseLevelRating').show();


        $('#ratingresult').hide();

        if (courseRatingObj.CourseLevelRating.CurrentUserCourseRating > 0) {
            $("#CourseLevelRatingButton").find("a").unbind('click.namespace');
            $("#CourseLevelRatingButton").fadeTo('slow', .4);
            $("#SubmitRating").find("a").unbind('click.namespace');
            $('#SubmitRating').attr("disabled", "disabled");
            $('#btnstart').attr("disabled", "disabled");
            $('#btnend').attr("disabled", "disabled");
        }

        $(NextQuestionButtonEn).hide();
        $(controlPanel).show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        //$('#odometerContainter').css('margin-top', '0px');

        $("#controlPanel").show();


        $(PlaybuttonEn).show();
        $(PlaybuttonDs).hide();

        $(BackbuttonEn).show();
        $(BackbuttonDs).hide();

        if (courseRatingObj.CourseLevelRating.CurrentUserCourseRating <= 0) {
            $('#CourseLevelRatingButton').find("a").bind('click.namespace', function() {
                ui.slide.next(function()
                {
                    cp.SaveCourseRating();
                });	            
                
            });
        }

    }
    //LCMS-12532 Yasin
    this.showValidationIdentityQuestion = function(validationQuestionObj) {

        $('#PlaybuttonDocuSign').remove(); //Added By Abdus Samad to avoid dublicate buttons

        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        var htmlData = "";

        htmlData = validationQuestionObj.ValidationIdentityQuestion.TemplateHtml;
        $(NextQuestionButtonEn).hide();

        $(controlPanel).find("#IcoCourseCompletion").hide();
        $(controlPanel).find("#IcoCourseCompletionDs").show();

        $(controlPanel).find("#IcoInstructorInformation").hide();
        $(controlPanel).find("#IcoInstructorInformationDs").show();        

        $(controlPanel).find("#IcoTOC").hide();
        $(controlPanel).find("#IcoTOCDs").show();        

        $(controlPanel).find("#IcoGlossary").hide();
        $(controlPanel).find("#IcoGlossaryDs").show();        

        $(controlPanel).find("#IcoCourseMaterial").hide();
        $(controlPanel).find("#IcoCourseMaterialDs").show(); 

        $(controlPanel).find("#modal-trigger-bookmark").hide();
        $(controlPanel).find("#cd-tour-trigger").hide();

        $(controlPanel).find("#IcoConfigure").hide();
        $(controlPanel).find("#IcoConfigureDs").show();

        $(controlPanel).find("#IcoHelp").hide();
        $(controlPanel).find("#IcoHelpDs").show();



        $(ValidationPlaybuttonEn).hide();
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $("#contentObjectName").html("");
        $(htmlContentContainer).html(htmlData);
        $('#ValidationQuestion').show();
        $("#btnSaveValidityIdentityQuestions").find("a").unbind('click.namespace');
        $("#btnSaveValidityIdentityQuestions").find("a").bind('click.namespace', function() {
        /*
        * Code Review : Remove the trigger, it is not working and make sure the loading overlay DIV is showing.
        */        
        //$(NextQuestionButtonEn).trigger("click");
            ui.slide.next(function()
            {
	            cp.SaveValidationIdentityQuestion();
            });        
        
      //  $('#ValidationQuestion').hide();  
            return false;
        });


    }
   

    
    this.ShowFinalExamLocked = function(FinalExamLockedObj) {

        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        //var htmlData = '<style>table {  height:100%;  width:100%;  margin:auto;  padding : 10px 10px 5px 10px}.mainimage {  max-width: 350px;  vertical-align:top;}.sceneTitle {  font-family : Tahoma, Arial, Verdana, Helvetica, sans-serif;   font-size : 30px;   text-decoration : bold;  vertical-align:top;}.sceneTextArea {  font-family : Arial;   font-size : 16px;  font:bold;  text-align: left;  vertical-align:top;  }</style><!--style="text-align:justify;"--><table border="0" width="95%" height="100" align="center" valign="center" cellpadding="0" cellspacing="0" style="height:100px !important;"><tr><td valign="top" style="height:30px !important;"><table border="0" cellpadding="0" cellspacing="0">  <tr>    <td width="10%"  valign="top"><img  class="mainimage" src="' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedImage + '" /></td><td width="90%"  valign="top" style="padding-top:30px;"><span class="sceneTitle">' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedHeading + '</span></td></tr></table></td></tr><tr><td colspan="3" valign="top" height="10%"><span class="sceneTextArea">' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedHelpText + '</span></td></tr><tr><td colspan="3" valign="top"></td></tr><tr><td colspan="3" valign="top"></td></tr></table>';
        var htmlData = '<section class="scene-wrapper visual-left"><div class="scene-body"><div class="scene-cell"><img class="img-responsive mainimage"  src="' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedImage + '" /></div><div class="scene-cell"><h1 class="scene-title">' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedHeading + '</h1><div class="scene-content"><p> ' + FinalExamLockedObj.FinalExamLocked.FinalExamLockedHelpText + '</p></div></div></div></section>';
        $(htmlContentContainer).html(htmlData);
        $(NextQuestionButtonEn).hide();
        $(controlPanel).show();
        $("#controlPanel").show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        //$('#odometerContainter').css('margin-top', '0px');
        $(PlaybuttonEn).hide();
        $(BackbuttonEn).show();
        $(PlaybuttonDs).show();
        $(BackbuttonDs).hide();

    }
    this.ShowCourseEvaluation = function(showCourseEvaluationObj) {

    $('#PlaybuttonDocuSign').remove(); //Added By Abdus Samad to avoid dublicate buttons
        
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        isMovieEnded = true;
        //var htmlData = '<style>table {  height:100%;  width:100%;  margin:auto;  padding : 10px 10px 5px 10px}.mainimage {  max-width: 350px;  vertical-align:top;}.sceneTitle {  font-family : Tahoma, Arial, Verdana, Helvetica, sans-serif;   font-size : 30px;   text-decoration : bold;  vertical-align:top;}.sceneTextArea {  font-family : Arial;   font-size : 16px;  font:bold;  text-align: left;  vertical-align:top;  }</style><!--style="text-align:justify;"--><table border="0" width="95%" height="100" align="center" valign="center" cellpadding="0" cellspacing="0" style="height:100px !important;"><tr><td valign="top" style="height:30px !important;"><table border="0" cellpadding="0" cellspacing="0"><tr><td width="10%"  valign="top"><img  class="mainimage" src="' + showCourseEvaluationObj.CourseEvaluation.ImageURL + '"/></td><td width="90%"  valign="top" style="padding-top:30px;"><span class="sceneTitle">' + showCourseEvaluationObj.CourseEvaluation.Heading + '</span></td></tr></table></td></tr><tr><td colspan="3" valign="top" height="10%"><span class="sceneTextArea">' + showCourseEvaluationObj.CourseEvaluation.ContentText + '</span></td></tr><tr><td colspan="3" valign="top"><div id="divStartCourseEvaluation"style="float:right; vertical-align:top; padding-left:8px; padding-right:12px; padding-top:30px;"><div class="btn-start"></div><span id="btnStartCourseEvaluation"><a class="btn-stem" href="#">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationStartButton + '</a></span><div class="btn-end"></div></div><div id="divSkipCourseEvaluation"style="float:right; vertical-align:top; padding-left:8px; padding-top:30px;"><div class="btn-start"></div><span id="btnSkipEvaluation"><a class="btn-stem" href="#">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationSkipButton + '</a></span><div class="btn-end"></div></div></td></tr><tr><td colspan="3" valign="top"></td></tr><tr><td colspan="3" valign="top"></td></tr></table>';
        //var htmlData = '<section class="scene-wrapper visual-right"><div class="scene-body"><div class="scene-cell"><img id="image" class="img-responsive"  src="' + showCourseEvaluationObj.CourseEvaluation.ImageURL + '" /></div><div class="scene-cell"><h1 class="scene-title">' + showCourseEvaluationObj.CourseEvaluation.Heading + '</h1><div class="scene-content"><p>' + showCourseEvaluationObj.CourseEvaluation.ContentText + '</p><p><div id="divStartCourseEvaluation"style="float:right; vertical-align:top; padding-left:8px; padding-right:12px; padding-top:30px;"><div id="btnStartCourseEvaluation"><a href="#" class="cd-btn main-action button btn-stem">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationStartButton + '</a></div></div></p><p><div id="divSkipCourseEvaluation"style="float:right; vertical-align:top; padding-left:8px; padding-top:30px;"><div id="btnSkipEvaluation"><a class="cd-btn main-action button btn-stem" href="#">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationSkipButton + '</a></div></div></p></div></div></div></section>';
        var htmlData = '<section class="scene-wrapper visual-right"><div class="scene-body"><div class="scene-cell"><h1 class="scene-title"><img id="image" class="img-responsive" style="display:inline;"  src="' + showCourseEvaluationObj.CourseEvaluation.ImageURL + '" />' + showCourseEvaluationObj.CourseEvaluation.Heading + '</h1><div class="scene-content"><p></p><p>' + showCourseEvaluationObj.CourseEvaluation.ContentText + '</p><div id="divStartCourseEvaluation" style="float: left;margin-top:20px;"><div id="btnStartCourseEvaluation"><a href="#" class="cd-btn button btn-stem">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationStartButton + '</a></div></div><div id="divSkipCourseEvaluation" style="float: left;margin-top:20px;"><div id="btnSkipEvaluation"><a class="cd-btn main-action button btn-stem" href="#">' + showCourseEvaluationObj.CourseEvaluation.CourseEvaluationSkipButton + '</a><div></div></div></div></div></div></section>';
        $(htmlContentContainer).html(htmlData);
        $(NextQuestionButtonEn).hide();
        $(controlPanel).show();
        $("#controlPanel").show();
        //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
        //$('#odometerContainter').css('margin-top', '0px');
        $(PlaybuttonEn).hide();
        $(BackbuttonEn).show();
        $(PlaybuttonDs).show();
        $(BackbuttonDs).hide();



        if (showCourseEvaluationObj.CourseEvaluation.IsSkippable == true) {
            $("#btnSkipEvaluation").find("a").unbind('click.namespace');
            $("#btnSkipEvaluation").find("a").bind('click.namespace', function() {
                ui.slide.next(function()
                {
                    cp.SkipCourseEvaluation();
                });		            
                return false;
            });
        }
        else {
            $("#btnSkipEvaluation").find("a").unbind('click.namespace');
            $("#divSkipCourseEvaluation").hide();
        }

        $("#btnStartCourseEvaluation").find("a").unbind('click.namespace');
        $("#btnStartCourseEvaluation").find("a").bind('click.namespace', function() {
            ui.slide.next(function()
            {
                cp.BeginCourseEvaluation();
            });
            
            return false;
        });
    }
    //	this.CourseSettingsRendering = function(courseSettingsObj) {
    //		var showInstructorInfo= courseSettingsObj.CourseSettings.ShowInstructorInfo;
    //		if (showInstructorInfo)
    //		{
    //		    $('#InstructorInformation').show();
    //		}
    //		else
    //		{
    //		    $('#InstructorInformation').hide();
    //		}
    //		
    //	}
    ///

    function getMovieName(movieName) {

        if (navigator.appVersion.indexOf("10.0") != -1) {
            return document[movieName];
        }

        if (navigator.appName.indexOf("Microsoft") != -1) {
            return window[movieName];
        } else {
            return document.embeds[movieName];
        }

    }

    function initializeIt(val) {
        return val;
    }


    function debug(msg) {
        thisMovie("videoplayer").debug(msg);
    }

    function thisMovie(movieName) {
        if (navigator.appName.indexOf("Microsoft") != -1) {
            return window[movieName];
        }
        else {
            return document[movieName];
        }
    }

    //LCMS-11870 starts
    function isIE() {
        var myNav = navigator.userAgent.toLowerCase();
        return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;

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

    function RenderNewPlayerForMobile(url) {
        var strHtml = "";
        //        strHtml =	'<embed'+
        //							' href="'+url+'"'+
        //							' type="video/x-mp4"'+
        //							' target="myself"'+
        //							' scale="1"'+
        //							' controller="false"'+
        //							' loop="false"'+
        //							' autoplay="true"'+
        //							' allowembedtagoverrides="true"'+
        //							' height="100%"'+
        //							' width="100%"'+
        //							' />"';
        //url=url.replace('https://10.0.100.250/','http://124.29.242.233/');
        url = url.replace('https://', 'http://');
        strHtml = "<video preoload='auto' width='100%' height='100%' controls><source type='video/mp4; codecs='avc1.42E01E, mp4a.40.2' src='" + url + "'></video>";

        return strHtml;

    }

    function RenderNewMP3PlayerForMobile(url) {
        var strHtml = "";
        //url=url.replace('https://10.0.100.250/','http://124.29.242.233/');
        url = url.replace('https://', 'http://');
        //strHtml="<audio controls='controls' autoplay='autoplay' ><source src='"  + url + "' style='left:68%; position:fixed; bottom:60px;'></audio>";
        strHtml = "<div id='mp3audioplay' style='display:none;'><a href='#' title='play audio'></a></div><div id='audioMP3div' style='display:none;'><audio id='audioMP3' autoplay='autoplay' controls='controls'><source src='" + url + "' style='display;none;'></audio></div>";
        return strHtml;

    }

    function RenderNewPlayer(url) {

        var strHtml = "";

        //alert(playerFileName);
        if (playerFileName == "player_as2") {


            strHtml += "<div id=\"flashContent\">";
            strHtml += "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"100%\" height=\"100%\" id=\"NewPlayer\">";
            strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            strHtml += "<param name=\"quality\" value=\"high\" />";
            strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            strHtml += "<param name=\"play\" value=\"true\" />";
            strHtml += "<param name=\"loop\" value=\"false\" />";
            strHtml += "<param name=\"wmode\" value=\"window\" />";
            strHtml += "<param name=\"scale\" value=\"noscale\" />";
            strHtml += "<param name=\"menu\" value=\"false\" />";
            strHtml += "<param name=\"devicefont\" value=\"false\" />";
            strHtml += "<param name=\"salign\" value=\"lt\" />";
            strHtml += "<param name=\"allowScriptAccess\" value=\"sameDomain\" />";
            strHtml += "<!--[if !IE]>-->";
            //strHtml += "<embed type=\"application/x-shockwave-flash\" data=\"" + playerFileName + ".swf\" width=\"100%\" height=\"100%\" name=\"NewPlayer\">";
            strHtml += "<embed src=\"" + playerFileName + ".swf\" type=\"application/x-shockwave-flash\" data=\"" + playerFileName + ".swf\" width=\"100%\" height=\"100%\" name=\"NewPlayer\">";
            strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            strHtml += "<param name=\"quality\" value=\"high\" />";
            strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            strHtml += "<param name=\"play\" value=\"true\" />";
            strHtml += "<param name=\"loop\" value=\"false\" />";
            strHtml += "<param name=\"wmode\" value=\"window\" />";
            strHtml += "<param name=\"scale\" value=\"noscale\" />";
            strHtml += "<param name=\"menu\" value=\"false\" />";
            strHtml += "<param name=\"devicefont\" value=\"false\" />";
            strHtml += "<param name=\"salign\" value=\"lt\" />";
            strHtml += "<param name=\"allowScriptAccess\" value=\"sameDomain\" />";
            strHtml += "<!--<![endif]-->";
            strHtml += "<a href=\"http://www.adobe.com/go/getflash\">";
            //strHtml += "<img src=\"http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif\" alt=\"Get Adobe Flash player\" />";
            strHtml += "</a>";
            strHtml += "<!--[if !IE]>-->";
            strHtml += "</embed>";
            strHtml += "<!--<![endif]-->";
            strHtml += "</object>";
            strHtml += "</div>";






            //                        strHtml += "<div id=\"flashContent\">";
            //                        strHtml += "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"100%\" height=\"100%\" id=\"NewPlayer\">";
            //                        strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            //                        strHtml += "<param name=\"quality\" value=\"high\" />";
            //                        strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            //                        strHtml += "<param name=\"play\" value=\"true\" />";
            //                        strHtml += "<param name=\"wmode\" value=\"window\" />";
            //                        strHtml += "<param name=\"scale\" value=\"noscale\" />";
            //                        strHtml += "<param name=\"loop\" value=\"false\" />";
            //                        strHtml += "<param name=\"menu\" value=\"false\" />";
            //                        strHtml += "<param name=\"devicefont\" value=\"false\" />";
            //                        strHtml += "<param name=\"salign\" value=\"lt\" />";
            //                        strHtml += "<param name=\"allowScriptAccess\" value=\"sameDomain\" />";
            //                        strHtml += "<!--[if !IE]>-->";
            //                        strHtml += "<object type=\"application/x-shockwave-flash\" data=\"" + playerFileName + ".swf\" width=\"100%\" height=\"100%\">";
            //                        strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            //                        strHtml += "<param name=\"quality\" value=\"high\" />";
            //                        strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            //                        strHtml += "<param name=\"play\" value=\"true\" />";
            //                        strHtml += "<param name=\"loop\" value=\"false\" />";
            //                        strHtml += "<param name=\"wmode\" value=\"window\" />";
            //                        strHtml += "<param name=\"scale\" value=\"noscale\" />";
            //                        strHtml += "<param name=\"menu\" value=\"false\" />";
            //                        strHtml += "<param name=\"devicefont\" value=\"false\" />";
            //                        strHtml += "<param name=\"salign\" value=\"lt\" />";
            //                        strHtml += "<param name=\"allowScriptAccess\" value=\"sameDomain\" />";
            //                        strHtml += "<!--<![endif]-->";
            //                        strHtml += "<a href=\"http://www.adobe.com/go/getflash\">";
            //                        strHtml += "<img src=\"http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif\" alt=\"Get Adobe Flash player\" />";
            //                        strHtml += "</a>";
            //                        strHtml += "<!--[if !IE]>-->";
            //                        strHtml += "</object>";
            //                        strHtml += "<!--<![endif]-->";
            //                        strHtml += "</object>";
            //                        strHtml += "</div>";


        }
        else if (playerFileName == "videoplayer") {

            strHtml += "<div id=\"flashContent\">";
            strHtml += "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"100%\" height=\"100%\" id=\"NewPlayer\">";
            strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            strHtml += "<param name=\"quality\" value=\"high\" />";
            strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            strHtml += "<param name=\"play\" value=\"true\" />";
            strHtml += "<param name=\"loop\" value=\"true\" />";
            strHtml += "<param name=\"wmode\" value=\"window\" />";
            strHtml += "<param name=\"scale\" value=\"showall\" />";
            strHtml += "<param name=\"menu\" value=\"true\" />";
            strHtml += "<param name=\"devicefont\" value=\"false\" />";
            strHtml += "<param name=\"salign\" value=\"\" />";
            strHtml += "<param name=\"allowScriptAccess\" value=\"always\" />";
            strHtml += "<param name=\"allowFullScreen\" value=\"true\" />";
            strHtml += "<!--[if !IE]>-->";
            //strHtml += "<embed type=\"application/x-shockwave-flash\" data=\"" + playerFileName + ".swf\" width=\"100%\" height=\"100%\" name=\"NewPlayer\">";
            strHtml += "<embed src=\"" + playerFileName + ".swf\" type=\"application/x-shockwave-flash\" data=\"" + playerFileName + ".swf\" width=\"100%\" height=\"100%\" name=\"NewPlayer\">";
            strHtml += "<param name=\"movie\" value=\"" + playerFileName + ".swf\" />";
            strHtml += "<param name=\"quality\" value=\"high\" />";
            strHtml += "<param name=\"bgcolor\" value=\"#ffffff\" />";
            strHtml += "<param name=\"play\" value=\"true\" />";
            strHtml += "<param name=\"loop\" value=\"true\" />";
            strHtml += "<param name=\"wmode\" value=\"window\" />";
            strHtml += "<param name=\"scale\" value=\"showall\" />";
            strHtml += "<param name=\"menu\" value=\"true\" />";
            strHtml += "<param name=\"devicefont\" value=\"false\" />";
            strHtml += "<param name=\"salign\" value=\"\" />";
            strHtml += "<param name=\"allowScriptAccess\" value=\"always\" />";
            strHtml += "<param name=\"allowFullScreen\" value=\"true\" />";
            strHtml += "<!--<![endif]-->";
            strHtml += "<a href=\"http://www.adobe.com/go/getflash\">";
            //strHtml += "<img src=\"http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif\" alt=\"Get Adobe Flash player\" />";
            strHtml += "</a>";
            strHtml += "<!--[if !IE]>-->";
            strHtml += "</embed>";
            strHtml += "<!--<![endif]-->";
            strHtml += "</object>";
            strHtml += "</div>";

        }
        return strHtml;

    }








    this.ShowHTMLRendering = function(showHTMLObject) {
        //debugger;
        JSplayerEnabled = showHTMLObject.MediaAsset.IsJSPlayerEnabled;
        playPauseFeature = showHTMLObject.MediaAsset.PlayPauseFeature;
        $("#controlPanel").show();

        xmlSampleData_ForCustomTemplate = showHTMLObject.MediaAsset.MCSceneXml; // MC
        
        //Changed By Waqas Zakai at 22nd FEB 2016
        //LCMS-14012
        //START
        if(xmlSampleData_ForCustomTemplate !=null)
        {        
            if (xmlSampleData_ForCustomTemplate.length > 0 && restrictIncompletJSTemplate=='true')
            {
                $(PlaybuttonEn).hide();
                $(PlaybuttonDs).show();
            }
            else
            {
                $(PlaybuttonEn).show();
                $(PlaybuttonDs).hide();        
            }
        }
        else
        {
            $(PlaybuttonEn).show();
            $(PlaybuttonDs).hide();        
        }        
        //END        

        var isCourseApproval = showHTMLObject.MediaAsset.IsCourseApproval;

        if (isCourseApproval == true) {

            var informationLogOut = showHTMLObject.MediaAsset.CourseApprovalMessage;

            var overlay = "#overlay";
            var dialog = "#dialog";

            // overlay
            //$(overlay).css({ "opacity": "0.7" });
            //$(dialog).css({ "height": "335px" });
            //$(dialog).css({ "text-align": "justify" });
            //$(dialog).css({ "border": "10px solid #DBD8D8" });

            // setting up dialog            
            $(dialog).find("#dialogHeading").remove();
            $(dialog).find("h2").remove();
            $(dialog).find("#Icon").remove();
            $(dialog).find("p").html(informationLogOut);
            $(dialog).find("button").wrap("<div style='text-align:center'></div>");
            $(dialog).find("button").html("OK");
            $(dialog).find("button").after("&nbsp;<button class='button' type='button'>Exit Course</button>");

            //$(overlay).fadeIn("slow");
            //$(dialog).fadeIn("slow");

            $(dialog).find("button:contains('OK')").click(function() {
                dialogCleanupForCourseApprovalMessage();
                cp.SaveLearnerCourseMessage();
                return false;
            });

            $(dialog).find("button:contains('Exit Course')").click(function() {
                dialogCleanupForCourseApprovalMessage();
                //alert("Hello");              
                cp.SendLogoutMessage();
                return false;
            });


            function dialogCleanupForCourseApprovalMessage() {
                // debugger;
                //$(overlay).fadeOut("slow");
//                $(dialog).fadeOut("slow", function() {
//                    $(dialog).find("h2").show();
//                    $(dialog).find("#dialogHeading h1").show();
//                    $(dialog).find("#Icon").show();
//                    $(dialog).find("p").html("");
//                    $(dialog).css({ "text-align": "center" });
//                    $(dialog).css({ "height": "" });
//                    if ($(dialog).find("button:contains('Exit Course')").length) {
//                        $(dialog).find("button:contains('Exit Course')").remove();
//                    }
//                    $(dialog).css({ "border": "0px" });
//                    $(dialog).find("button").html("");
//                });
            }
        }


        $('#toggle').show(); //By Yasin for LCMS-12165

        if (document.getElementById('media') != null)
            document.getElementById('media').innerHTML = "";

        $(IcoBookMarkDs).hide();

        if (!demo) {

            // USED BY OD COURSE PLAYER START
            //$("#progressbar").progressbar('destroy');
            $("#progressbar").attr('style','width: '+ showHTMLObject.MediaAsset.CourseProgressPercentage +'%');
            //$("#progressbar").progressbar({ value: showHTMLObject.MediaAsset.CourseProgressPercentage });
            // USED BY OD COURSE PLAYER END
            //To set from branding////////
            //$('ProgressBar').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');
            //$('progressBarValueDiv').css('backgroundImage', 'url(' + ImageProgressBarGlass + ')');

            $("#progressbar").attr("title", showHTMLObject.MediaAsset.CourseProgressToolTip);
            //$(ProgressBarTxt).attr("title", showHTMLObject.MediaAsset.CourseProgressToolTip);
        }
        else {

            $(ProgressBarContainer).hide();
        }
        $(htmlContentContainer).find('span').empty();


        $('#timer').html('00:00');

        //alert("show assessment start message: "+showHTMLObject.MediaAsset.IsAssessmentStartMessage);

        var isRestrictiveMechansimEnabled = false;
        if (showHTMLObject.MediaAsset.IsRestrictiveAssessmentEngine) {
            isRestrictiveMechansimEnabled = true;
        }
        else {
            document.body.oncontextmenu = new Function("return true");
            document.body.onselectstart = new Function("return true");
            document.body.ondragstart = new Function("return true");
            document.body.style.MozUserSelect = '';
            //var secureDiv = document.getElementById('security');
            //secureDiv.style.display = "none";

            $('#security').empty();

        }

        if (showHTMLObject.MediaAsset.DisableBackButton) {
            $(BackbuttonEn).hide();
            $(BackbuttonDs).show();
        } else if (!showHTMLObject.MediaAsset.DisableBackButton) {
            $(BackbuttonEn).show();
            $(BackbuttonDs).hide();
        }


        if (!showHTMLObject.MediaAsset.ShowBookMark) {
            // alert('s');
            $(IcoBookMark).hide();
        } else if (showHTMLObject.MediaAsset.ShowBookMark) {
            $(IcoBookMark).show();
        }


        if (showHTMLObject.MediaAsset != null) {


            if (document.getElementById('media') != null)
                document.getElementById('media').innerHTML = "";

            $(assessmentControlPanel).hide();
            $(controlPanel).show();
            $("#controlPanel").show();
            //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
            //$('#odometerContainter').css('margin-top', '0px');
            $(PlaybuttonEn).show();
            $(PlaybuttonDs).hide();
            $(htmlContentContainer).show();
            $('#NYInsuranceValidation').hide();
            $('#CARealStateValidation').hide();
            $(swfContainer).show();
            $("#timer").hide();
            //$(controlPanel).find("#timer").hide();

            sceneTimer.TimerContainer(timer);

            if (eval(showHTMLObject.MediaAsset.SceneDurationTimer) != 0) {

                $(PlaybuttonEn).hide();
                $(PlaybuttonDs).show();
                $("#timer").show();
                //$(controlPanel).find("#timer").show();
                sceneTimer.InitializeTimer(showHTMLObject.MediaAsset.SceneDurationTimer);
            }

            if (showHTMLObject.MediaAsset.RemidiationMode) {

                $(controlPanel).find("#ShowQuestionButton").show();
                $(controlPanel).find("#ShowQuestionButton").css('margin-left', '325px');
                //$('#odometerContainter').css('margin-top', '-27px');
                $(controlPanel).find("#CourseMenuIcons").hide();
                $("#timer").hide();
                //$(controlPanel).find("#timer").hide();
                $(ProgressBarContainer).hide();
            }
            else {
                $(controlPanel).find("#ShowQuestionButton").hide();

                if (!demo) {

                    $(controlPanel).find("#CourseMenuIcons").show();
                }

            }

            $(controlPanel).find("#ShowQuestionButton").unbind('click.namespace');
            $(controlPanel).find("#ShowQuestionButton").bind('click.namespace', function() {
                $(controlPanel).find("#ShowQuestionButton").unbind('click.namespace');
                //$('#odometerContainter').css('margin-top', '0px');
                cp.GoContentTOQuestion();
                return false;
            });
            findToc(showHTMLObject.MediaAsset);

            //this has been commented to as now contentobject title will come from Scene rendering Command


            //$("#contentObjectName").html(showHTMLObject.MediaAsset.TitleBreadCrumb);	
            //Fix for LCMS-2296

            $("#contentObjectName").html("");

            if (showHTMLObject.MediaAsset.EnableAllTOC == true) {
                EnableAllToc();
            }

            var htmlString = showHTMLObject.MediaAsset.TemplateHtml;

            //if(showHTMLObject.MediaAsset.ViewStreaming)

            if (showHTMLObject.MediaAsset.ViewStreaming) {


                if (String(showHTMLObject.MediaAsset.ActionScriptVersion) == "3") {
                    playerFileName = "videoplayer";
                }
                else {
                    playerFileName = "player_as2";
                }

                if (showHTMLObject.MediaAsset.VisualTopType == "swf") {

                    // LCMS-12341 Waqas Zakai-21Nov2013 START
                    //var timerCancellingSWF = AC_FL_RunContentSWF('codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0', 'width', '1', 'height', '1', 'src', 'flashplugin', 'quality', 'high', 'pluginspage', 'https://www.macromedia.com/go/getflashplayer', 'salign', '', 'play', 'true', 'loop', 'true', 'scale', 'noborder', 'wmode', 'transparent', 'devicefont', 'false', 'id', 'flashplugin', 'bgcolor', '#ffffff', 'name', 'flashplugin', 'menu', 'true', 'allowFullScreen', 'false', 'allowScriptAccess', 'sameDomain', 'movie', 'flashplugin', 'salign', '');                    
                    var timerCancellingSWF = AC_FL_RunContentSWF('width', '1', 'height', '1', 'movie', 'flashplugin');
                    // LCMS-12341 Waqas Zakai-21Nov2013 END
                    flashPluginTimerId = window.setTimeout("checkFrameUrl();", flashPluginTimeOutDuration);
                    $("#timerCancelling").html(timerCancellingSWF);


                    $(htmlContentContainer).html(htmlString);

                    var url = $('#media').find('embed').attr("src");
                    var width = $('#media').find('embed').attr("width");
                    var height = $('#media').find('embed').attr("height");






                    document.getElementById("txtHeight").value = height;
                    document.getElementById("txtWidth").value = width;
                    document.getElementById("txtURL").value = url;



                    var max_height = document.getElementById("txtHeight").value;
                    var max_width = document.getElementById("txtWidth").value;

                    if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true && $('#container').length > 0) {


                        $('#container').show();
                        //$('#media').find('toggle').hide();
                        $('#media').find('#toggle').remove(); // LCMS-12568 Yasin
                        // $('.pause').removeAttr("id"); 
                        // $('#media').removeClass("pause");

                        $('#media').find('embed').hide();
                        javascriptPlayer(url, "swf");

                    }
                    else {
                        var swfhtml = RenderNewPlayer(playerFileName);
                        document.getElementById("media").innerHTML = '';
                        document.getElementById("media").innerHTML = swfhtml;
                    }


                } else {

                    $(htmlContentContainer).html(htmlString);
                }

            }
            else if (showHTMLObject.MediaAsset.VisualTopType == "mp4" && (isMobile.iOS() || isMobile.Android() || isMobile.BlackBerry())) {
                if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true && $('#container').length > 0) {


                    $(htmlContentContainer).html(htmlString);
                    var url = $('#media').find('embed').attr("src");
                    $('#container').show();
                    $('#media').find('embed').hide();
                    javascriptPlayer(url, "mp4");
                }
                else {

                    $(htmlContentContainer).html(htmlString);
                    var url = $('#media').find('embed').attr("src");
                    var swfhtml = RenderNewPlayerForMobile(url);
                    document.getElementById("media").innerHTML = '';
                    document.getElementById("media").innerHTML = swfhtml;


                }





            }

            else if (showHTMLObject.MediaAsset.VisualTopType == "flv") {

                playerFileName = "videoplayer";

                // LCMS-12341 Waqas Zakai-21Nov2013 START
                //var timerCancellingSWF = AC_FL_RunContentSWF('codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0', 'width', '1', 'height', '1', 'src', 'flashplugin', 'quality', 'high', 'pluginspage', 'https://www.macromedia.com/go/getflashplayer', 'salign', '', 'play', 'true', 'loop', 'true', 'scale', 'noborder', 'wmode', 'transparent', 'devicefont', 'false', 'id', 'flashplugin', 'bgcolor', '#ffffff', 'name', 'flashplugin', 'menu', 'true', 'allowFullScreen', 'false', 'allowScriptAccess', 'sameDomain', 'movie', 'flashplugin', 'salign', '');                
                var timerCancellingSWF = AC_FL_RunContentSWF('width', '1', 'height', '1', 'movie', 'flashplugin');
                // LCMS-12341 Waqas Zakai-21Nov2013 END
                flashPluginTimerId = window.setTimeout("checkFrameUrl();", flashPluginTimeOutDuration);
                $("#timerCancelling").html(timerCancellingSWF);

                $(htmlContentContainer).html(htmlString);
                var url = $('#media').find('embed').attr("src");
                var width = $('#media').find('embed').attr("width");
                var height = $('#media').find('embed').attr("height");

                document.getElementById("txtHeight").value = height;
                document.getElementById("txtWidth").value = width;
                document.getElementById("txtURL").value = url;


                var max_height = document.getElementById("txtHeight").value;
                var max_width = document.getElementById("txtWidth").value;


                if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true && $('#container').length > 0) {

                    $('#container').show();
                    // $('#media').find('#toggle').hide();
                    $('#media').find('#toggle').remove();
                    $('#media').find('embed').hide();
                    javascriptPlayer(url, "flv");
                }

                else {
                    var swfhtml = RenderNewPlayer(playerFileName);
                    document.getElementById("media").innerHTML = '';
                    document.getElementById("media").innerHTML = swfhtml;
                }


            }

            else {

                if (showHTMLObject.MediaAsset.VisualTopType == "swf") {

                    //                    if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true) {
                    //                        $(htmlContentContainer).html(htmlString);
                    //                        var url = $('#media').find('embed').attr("src");
                    //                        var max_height = $(htmlString).find('embed').attr("height;")
                    //                        var max_width = $(htmlString).find('embed').attr("width;")
                    //                        $('#media').find('container').show();
                    //                        $('#media').find('#toggle').hide();
                    //                        $('#media').find('embed').hide();
                    //                        javascriptPlayer(url, "swf");
                    //                    }
                    //                    else {
                    var timerCancellingSWF = AC_FL_RunContentSWF('width', '1', 'height', '1', 'movie', 'flashplugin');
                    flashPluginTimerId = window.setTimeout("checkFrameUrl();", flashPluginTimeOutDuration);
                    $("#timerCancelling").html(timerCancellingSWF);

                    // }

                }

				if(xmlSampleData_ForCustomTemplate == null)
				{
					$(htmlContentContainer).html(htmlString);
				}
				else
				{
					//	Only for Game and Custom Activity Templates
					$(htmlContentContainer).html("<section class='scene-wrapper'><div class='scene-body'>"+htmlString+"</div></section>");
				}                


                if (showHTMLObject.MediaAsset.VisualTopType == "mp4") // By Yasin for LCMS-12165
                {
                    //LCMS-12333 Implemented checked by Waqas Zakai 19-Nov-2013 Start

                    if (document.getElementById("toggle") != null) {
                        var element = document.getElementById("toggle");
                        element.parentNode.removeChild(element);
                    }
                    if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true && $('#container').length > 0) {

                        var url = $('#media').find('embed').attr("src");
                        $('#container').show();
                        $('#media').find('embed').hide();
                        javascriptPlayer(url, "mp4");
                    }
                    //LCMS-12333 Implemented checked by Waqas Zakai 19-Nov-2013 Start

                }
            }

            if (showHTMLObject.MediaAsset.SceneTemplateType == "VSC") {

				var isEmbedCode = showHTMLObject.MediaAsset.IsEmbedCode;
				var EmbedCode  = showHTMLObject.MediaAsset.EmbedCode;
				
				if(isEmbedCode == true){
				    $("#media").removeAttr("style").html(EmbedCode).attr("style","display:block;width:100%;text-align:center;padding-top:20px");				  
							 
				}
				else
				{
                var streamingServerURL = showHTMLObject.MediaAsset.StreamingServerURL;
                var streamingApplication = showHTMLObject.MediaAsset.StreamingServer;
                var videoFileName = showHTMLObject.MediaAsset.VideoFileName;
                var videoHeight = showHTMLObject.MediaAsset.VideoHeight;
                var videoWidth = showHTMLObject.MediaAsset.VideoWidth;
                var fullScreen = showHTMLObject.MediaAsset.FullScreen;
                var displayStandard = showHTMLObject.MediaAsset.DisplayStandard;
                var displayWideScreen = showHTMLObject.MediaAsset.DisplayWideScreen;
                //LCMS-12264 By Waqas Zakai.....START
                var re = /(?:\.([^.]+))?$/;
                var ext = re.exec(videoFileName)[1];

                if (showHTMLObject.MediaAsset.IsJSPlayerEnabled == true && $('#container').length > 0) {

                    $('#container').show();
                    $('#media').find('#toggle').hide();

                    $('#media').find('embed').hide();
                    javascriptPlayer(videoFileName, ext, "rtmp://" + streamingServerURL + "/" + streamingApplication, "rtmp");
                    if (fullScreen == true) {
                        $('#media').attr('style', 'width:100%; height:100%; overflow:hidden;');

                    }
                    else {
                        $('#media').attr('style', 'width:' + videoWidth + 'px; height:' + videoHeight + 'px ;overflow:hidden');

                    }
                }

                else {

                    if (isMobile.iOS() || isMobile.Android() || isMobile.BlackBerry()) {

                        var streamingURL = 'http://' + streamingServerURL + '/vod/' + streamingApplication + '/' + videoFileName;
                        $('#container').show();
                        $('#media').find('embed').hide();
                        javascriptPlayerMp4(streamingURL, ext);

                    }
                    else {
                        if (fullScreen == true) {
                            $('#media').attr('style', 'width:100%; height:100%; overflow:hidden;');
                        }
                        else {
                            $('#media').attr('style', 'width:' + videoWidth + 'px; height:' + videoHeight + 'px ;overflow:hidden');
                        }

                        var streaminghtml = AC_FL_RunContentSWF('codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0', 'id', 'videoplayer', 'name', 'videoplayer', 'width', '100%', 'height', '100%', 'allowScriptAccess', 'sameDomain', 'allowFullScreen', fullScreen, 'movie', 'videoplayer', 'quality', 'high', 'salign', '', 'bgcolor', '#FFFFFF', 'wmode', 'transparent', 'devicefont', 'false', 'scale', 'noborder');
                        document.getElementById("media").innerHTML = streaminghtml;
                       
                        var displayratio;
                        if(displayStandard == true)
                        {
                        displayratio = 0;
                        }
                        else
                        {
                        displayratio = 1;
                        }

                        //setTimeout(initialize,500,streamingServerURL,streamingApplication,videoFileName);
                        var funcName = "initialize('" + streamingServerURL + "','" + streamingApplication + "','" + videoFileName + "','" + displayratio + "')";
                        setTimeout(funcName, 500);
                    }
					}	
                }
                //LCMS-12264 By Waqas Zakai.....END
            }

            if (navigator.appName == "Netscape") {
                $(htmlContentContainer).find('.sceneTextArea').find('a').each(function(i) {


                    // --------------------------------------------------------------------------
                    // OLD CONDITION COMMITTED OUT AND REPLACED BY NEW CODE  (by Mustafa)
                    // --------------------------------------------------------------------------						
                    /*if($(this).attr('href') != null && $(this).attr('href') != '#')
                    $(this).attr('target', '_blank');*/

                    if (($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") != -1) || ($(this).attr('href') != null && $(this).attr('href').indexOf("#") != -1))
                        $(this).attr('target', '_self');
                    else if ($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") == -1)
                        $(this).attr('target', '_blank');

                    // --------------------------------------------------------------------------

                })






                // --------------------------------------------------------------------------
                // HYPERLINK TARGET HANDLING FOR CLOSED CAPTIONING (by Mustafa)
                // --------------------------------------------------------------------------						
                $(htmlContentContainer).find('.sceneClosedCaptioningText').find('a').each(function(i) {
                    /*if($(this).attr('href') != null && $(this).attr('href') != '#')
                    $(this).attr('target', '_blank');*/

                    if (($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") != -1) || ($(this).attr('href') != null && $(this).attr('href').indexOf("#") != -1))
                        $(this).attr('target', '_self');
                    else if ($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") == -1)
                        $(this).attr('target', '_blank');

                    // --------------------------------------------------------------------------

                })


            }
            else if (navigator.appName == "Microsoft Internet Explorer") {

                $(htmlContentContainer).find('.sceneTextArea').find('a').each(function(i) {


                    // --------------------------------------------------------------------------
                    // OLD CONDITION COMMITTED OUT AND REPLACED BY NEW CODE  (by Mustafa)
                    // --------------------------------------------------------------------------						

                    if (($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") != -1) || ($(this).attr('href') != null && $(this).attr('href').indexOf("#") != -1))
                        $(this).attr('target', '_self');
                    else if ($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") == -1)
                        $(this).attr('target', '_blank');

                    /*
                    alert($(this).attr('href'));
                    if($(this).attr('href') != null && $(this).attr('href').toLowerCase().startsWith('javascript:'))
                    $(this).attr('target', '_self');
                    else if($(this).attr('href') != null && $(this).attr('href').toLowerCase().startsWith('javascript:') == false)
                    $(this).attr('target', '_blank');*/
                    // --------------------------------------------------------------------------

                })





                // --------------------------------------------------------------------------
                // HYPERLINK TARGET HANDLING FOR CLOSED CAPTIONING (by Mustafa)
                // --------------------------------------------------------------------------						
                $(htmlContentContainer).find('.sceneClosedCaptioningText').find('a').each(function(i) {
                    if (($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") != -1) || ($(this).attr('href') != null && $(this).attr('href').indexOf("#") != -1))
                        $(this).attr('target', '_self');
                    else if ($(this).attr('href') != null && $(this).attr('href').toLowerCase().indexOf("javascript:") == -1)
                        $(this).attr('target', '_blank');

                    /*
                    alert($(this).attr('href'));
                    if($(this).attr('href') != null && $(this).attr('href').toLowerCase().startsWith('javascript:'))
                    $(this).attr('target', '_self');
                    else if($(this).attr('href') != null && $(this).attr('href').toLowerCase().startsWith('javascript:') == false)
                    $(this).attr('target', '_blank');*/
                    // --------------------------------------------------------------------------

                })
            }

            try {
                setMediaType(showHTMLObject.MediaAsset.VisualTopType, '#image', '#media');
            } catch (e) {

            }


            if (showHTMLObject.MediaAsset.IsAssessmentStartMessage) {
            
                $('#BeginPreAssessmentButton').text(BeginPreAssessmentButtonText);
                $('#BeginLessonAssessmentButton').text(BeginLessonAssessmentButtonText);
                $('#BeginPostAssessmentButton').text(BeginPostAssessmentButtonText);
                $('#btnBeginPracticeExam').text(btnBeginPracticeExamText);
                $('#btnSkipPracticeExam').attr('value', btnSkipPracticeExamText);
                $("#assessmentItemResult").hide();
                $("#controlPanel").hide();
                $("#cd-tour-trigger").hide();

                if (isRestrictiveMechansimEnabled) {
                    document.body.oncontextmenu = new Function("return false");
                    document.body.onselectstart = new Function("return false");
                    document.body.ondragstart = new Function("return false");
                    document.body.style.MozUserSelect = "none"
                    //var secureDiv = document.getElementById('security');
                    //secureDiv.style.display = "";
                    //var secHTML= AC_FL_RunContentSWF('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0','width','5px','height','5px','allowScriptAccess','always','allowFullScreen',false,'movie','securityAPI.swf','quality','high','salign','','bgcolor','#EAEAEA','wmode', 'transparent','devicefont', 'false','scale', 'noborder');
                    var secHTML = AC_FL_RunContentSWF('codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0', 'width', '5px', 'height', '5px', 'align', 'middle', 'allowScriptAccess', 'always', 'allowFullScreen', false, 'movie', 'securityAPI', 'quality', 'high', 'id', 'securityAPI', 'bgcolor', '#EAEAEA');
                    $('#security').html(secHTML);
                }
                //LCMS-11085 
                if (showHTMLObject.MediaAsset.LogOutText != null && showHTMLObject.MediaAsset.LogOutText != "") {
                    logOutText = showHTMLObject.MediaAsset.LogOutText;
                }
                //End LCMS-11085


                //alert('sss'+showHTMLObject.MediaAsset.IsAssessmentStartMessage);
                //$(controlPanel).hide();
                $(BackbuttonEn).hide();
                $(PlaybuttonEn).hide();
                $(PlaybuttonDs).show();
                $(BackbuttonDs).show();
                $(IcoBookMarkDs).show();
                //$('#assessmentTimer').show();
                allowSkipping = showHTMLObject.MediaAsset.AllowSkipping;
                showGradeAssessment = showHTMLObject.MediaAsset.ShowGradeAssessment;
                isLockoutClickAwayToActiveWindowStart = showHTMLObject.MediaAsset.LockoutClickAwayToActiveWindow;

                if ($(htmlContentContainer).find("input") != null) {
                    // Waqas Zakai 
                    // LCMS-10384
                    // START
                    if ($(htmlContentContainer).find("input").val() != undefined) {
                        $(BackbuttonEn).show();
                        $(PlaybuttonEn).hide();
                        $(PlaybuttonDs).show();
                        $(BackbuttonDs).hide();
                    }
                    // LCMS-10384 End Here

                    $(IcoBookMarkDs).show();
                    $(htmlContentContainer).find("input").bind('click.namespace', function() {
                        $(htmlContentContainer).find("input").unbind('click.namespace');
                        skipPracticeExam();
                    });
                }

                // LCMS-9213
                //--------------------------------------------------------------------------					

                var butttonCount = 1;
                if (showHTMLObject.MediaAsset.IsConfigurationModified.toString().toLowerCase() == "true") {
                    $(htmlContentContainer).find("button").html("&nbsp;&nbsp;&nbsp;Continue&nbsp;&nbsp;&nbsp;");
                }
                else if (showHTMLObject.MediaAsset.IsAssessmentResumeMessage.toString().toLowerCase() == "true") {


                    butttonCount = 2;
                    var resumeButtonPlacementPoint = htmlString.toLowerCase().indexOf("<button");


                    var beforeButton = htmlString.substr(0, resumeButtonPlacementPoint) + "<button class=\"button\">" + btnResumeAssessmentText + "</button>&nbsp;&nbsp;&nbsp;";
                    var afterButton = htmlString.substr(resumeButtonPlacementPoint);

                    htmlString = beforeButton + afterButton;
                    //alert("pehlay");
                    $(htmlContentContainer).html(htmlString);
                    //alert("bad mein");

                    $(htmlContentContainer).find("button").eq(0).bind('click.namespace', function() {
                        $(htmlContentContainer).find("button").eq(0).unbind('click.namespace');

                        if (showHTMLObject.MediaAsset.AssessmentTimer != -1) {
                            $(assessmentTimer).show();
                            assessmentTimerObj.TimerContainer(assessmentTimer);
                            assessmentTimerObj.InitializeTimer(showHTMLObject.MediaAsset.AssessmentTimer);
                        }
                        else {

                            $(assessmentTimer).hide();

                        }


                        resumeAssessmentMessageClick();
                        resetCPIdleTimer();

                        return false;
                    });





                    $(htmlContentContainer).find("button").eq(1).html(StartAssessmentOverText);
                    $(htmlContentContainer).find("button").eq(1).bind('click.namespace', function() {
                        $(htmlContentContainer).find("button").eq(1).unbind('click.namespace');

                        if (showHTMLObject.MediaAsset.AssessmentTimer != -1) {
                            $(assessmentTimer).show();
                            assessmentTimerObj.TimerContainer(assessmentTimer);
                            assessmentTimerObj.InitializeTimer(showHTMLObject.MediaAsset.InitialAssessmentTimerValue);
                        } else {

                            $(assessmentTimer).hide();

                        }


                        startAssessmentMessageClick();
                        resetCPIdleTimer();

                        return false;
                    });

                    //Abdus Samad
                    //LCMS-11998
                    //Start
                    $(htmlContentContainer).find("input").eq(1).html(btnSkipPracticeExamText);
                    $(htmlContentContainer).find("input").unbind('click.namespace');
                    $(htmlContentContainer).find("input").bind('click.namespace', function() {
                        //  $(htmlContentContainer).find("input").unbind('click.namespace');
                        skipPracticeExam();

                        return false;
                    });


                    //Stop

                } // else if	




            }
            //--------------------------------------------------------------------------
            // LCMS-9213	




            if (butttonCount == 1) {
                $(htmlContentContainer).find("button").bind('click.namespace', function() {
                    $(htmlContentContainer).find("button").unbind('click.namespace');

                    if (showHTMLObject.MediaAsset.AssessmentTimer != -1) {
                        $(assessmentTimer).show();
                        assessmentTimerObj.TimerContainer(assessmentTimer);
                        assessmentTimerObj.InitializeTimer(showHTMLObject.MediaAsset.AssessmentTimer);
                    } else {

                        $(assessmentTimer).hide();

                    }


                    startAssessmentMessageClick();
                    resetCPIdleTimer();

                    return false;
                });
            } // if

            //// }



            /*if($('#htmlContentContainer').find('.closeCaption').html() == ""){
            $('#htmlContentContainer').find('.closeCaption').hide();
            alert($('#htmlContentContainer').find('.closeCaption').html());
            }else {
            alert($('#htmlContentContainer').find('.closeCaption').html());
            }*/



            //var samplevar = (($("#content").width()/2 ) / $(htmlContentContainer).find('img').eq(0).width() *100 );

            //alert(Math.ceil(samplevar));
            //var newpercent = Math.ceil(samplevar) - 100;

            //$(htmlContentContainer).find('img').eq(0).css("width", Math.ceil(samplevar)+"%");
            //$(htmlContentContainer).find('img').eq(0).css("width", "50%");

            //$('#image').find('img').eq(0).css('width', '50%');
            //alert($('#image').find('img').eq(0).width());

            /*if($('#image').find('img').eq(0).width() > ($("#content").width()/2 )) {
			
			$('#image').find('img').eq(0).css("width", "50%");
			
		  }else if($('#image').find('img').eq(0).width() < ($("#content").width()/2 )) {
			
			
		  }*/
            slideID = showHTMLObject.MediaAsset.MediaAssetID;
            sceneGUID = showHTMLObject.MediaAsset.MediaAssetSceneID;
            currentframeNumber = showHTMLObject.MediaAsset.FlashSceneNo;
            LastScene = showHTMLObject.MediaAsset.LastScene;



            nextButtonState = true;
            isMovieEnded = true;
            //LCMS-2592
            //Reset Idle Timer
            resetCPIdleTimer();

            try {
                //alert(showHTMLObject.MediaAsset.AudioURL);

                // var isSoundOn =  $("input[@id='SoundOn']").attr("checked");
                //var isSoundOff = $("input[@id='SoundOff']").attr("checked");                
                var isSoundOff=$("#speakerToggle > div").hasClass("switch-off");
                
                //Audio Asset Start
                if ((showHTMLObject.MediaAsset.AudioURL.indexOf(".mp3") > 0) && (isMobile.iOS() || isMobile.Android() || isMobile.BlackBerry())) {
                    var swfhtml = RenderNewMP3PlayerForMobile(showHTMLObject.MediaAsset.AudioURL);

                    document.getElementById("AudioMobile").innerHTML = '';
                    IsSoundPlaying = true;
                    document.getElementById("AudioMobile").innerHTML = swfhtml;

                    if (!isMobile.iOS()) {
                        $('#AudioMobile').hide();
                    }
                    else {
                        $('#AudioMobile').show();
                        $('#mp3audioplay').show();
                        $('#audioMP3div').hide();
                    }
                    $('#audioMP3').hide();

                    $('#mp3audioplay').find('a').bind('click.namespace', function() {
                        $("#audioMP3").get(0).play();
                    });
                }
                else {
                    loadSound(showHTMLObject.MediaAsset.AudioURL);
                    IsSoundPlaying = true;
                    if (isSoundOff) {
                        this.setVolumeValue(0);

                    }
                    else {
                        var volume = this.getSliderValue();
                        this.setVolumeValue(volume)
                    }
                }
            }
            catch (Error) {
                //alert(Error);
            }
        }
        window.setTimeout("refreshTitle();", 0); // Added by Mustafa for LCMS-2502
        window.setInterval("refreshTitle();", 500); // Added by Mustafa for LCMS-2502
        
        // Added by Waqas Zakai LCMS-13630 START        
        if(showHTMLObject.MediaAsset.IsHTML5Content)
        {
            var html5datamessage='<div style="height:15px"><img height="15" src="images/pixel.gif" /></div><div id="HTML5Compatibility" class="sceneTextArea" style="text-align:center">'+ showHTMLObject.MediaAsset.HTML5Message +'</div>'
            var isHtml5Compatible = document.createElement('canvas').getContext != undefined;            
            if(!isHtml5Compatible)
            {                
                $(htmlContentContainer).html('');
                $(htmlContentContainer).html(html5datamessage);                
            }
        }
        // Added by Waqas Zakai LCMS-13630 END
    }




    this.getSliderValue = function() {
        return $('.slider_bar').slider("value");
    }
    this.setVolumeValue = function(Volume) {
        getMovieName("movie").callSetVolumeInfo(Volume);

    }
    this.ShowGlossaryItem = function(glossaryItemObject, glossaryTerm)
	{	
		ui.nav.gIndex = parseInt($('#course-glossary').find("a[data-id='" + glossaryItemObject.GlossaryDetail.GlossaryID + "']").attr('data-index'));
		//ui.nav.gIndex = glossaryItemObject.GlossaryDetail.GlossaryID;
		
		var bodyHtml =	'<h1>'+replaceSpacesAndLineBreaks(glossaryTerm)+'</h1>'+
						'<p>' + InsertEscapeSequence(replaceSpacesAndLineBreaks(glossaryItemObject.GlossaryDetail.GlossaryDefinition),"'") + '</p>'+
						'<div>';
		
		if(ui.nav.gIndex <= 0)
		{
			bodyHtml += '<a href="javascript:;" class="cd-btn disabled" disabled="disabled">Prev</a>';
		}
		else
		{
			bodyHtml += '<a href="javascript:;" onclick="ui.nav.switchGlossary(this,false);" class="cd-btn main-action" data-id="'+ui.nav.gTerms[ui.nav.gIndex-1][0]+'" data-term="'+ui.nav.gTerms[ui.nav.gIndex-1][1]+'" data-group="modal-dynamic" data-trg="glossary" data-type="cd-modal-trigger">Prev</a>';
		}
		
		if(ui.nav.gIndex >= (ui.nav.gTerms.length-1))
		{
			bodyHtml += '<a href="javascript:;" class="cd-btn disabled" disabled="disabled">Next</a>';
		}
		else
		{
			bodyHtml += '<a href="javascript:;" onclick="ui.nav.switchGlossary(this,true);" class="cd-btn main-action" data-id="'+ui.nav.gTerms[ui.nav.gIndex+1][0]+'" data-term="'+ui.nav.gTerms[ui.nav.gIndex+1][1]+'" data-group="modal-dynamic" data-trg="glossary" data-type="cd-modal-trigger">Next</a>';
		}
		
		bodyHtml += '</div>';
		$('.cd-modal[data-modal="modal-dynamic"] > .cd-modal-content')
			.removeClass('pre-loader please-wait')
			.html(bodyHtml);
			
            /*$('.cd-modal[data-modal="modal-dynamic"] > .cd-modal-content').removeClass('pre-loader please-wait')
		    .html('<h1>'+replaceSpacesAndLineBreaks(glossaryTerm)+'</h1><p>'+ InsertEscapeSequence(replaceSpacesAndLineBreaks(glossaryItemObject.GlossaryDetail.GlossaryDefinition),"'") +'</p>');*/
    } // showGlossaryItem end
    
    this.ShowMaterialItem = function(courseMaterialObject) {        
    var material_content = '<div class="jwmovie">'+
						'<div id="Div1"></div>' +
					'</div>' +
					'<div>' +
						'<a href="assets/uploads/fields.mp4" target="_blank" class="cd-btn main-action" download="Video">Download File</a>' +
						'<a href="assets/uploads/fields.mp4" class="cd-btn dropbox-saver">Save To Dropbox</a>' +
					'</div>';
            $('.cd-modal[data-modal="modal-material"] > .cd-modal-content').removeClass('pre-loader please-wait')
		    .html('<h1>'+replaceSpacesAndLineBreaks()+'</h1><p>'+ InsertEscapeSequence(replaceSpacesAndLineBreaks(courseMaterialObject.GlossaryDetail.GlossaryDefinition),"'") +'</p>'+ material_content);
		    alert("contetn" + material_content);
    } // showMaterialItem end

    this.ShowErrorMessageRendering = function(errorObj) {
        $(htmlContentContainer).find('span').empty();

        //if(!isDialogueOpen){
        jQuery().ready(function() {
            //$(overlay).css({ "opacity": "0.7" });
            //$(overlay).fadeIn("slow");
            //$(dialog).fadeIn("slow");


            $(dialog).find("h2").html(errorObj.ErrorMessage.ErrorMessageHeading);
            $(dialog).find("#dialogHeading h1").html(errorObj.ErrorMessage.ErrorMessageHeading);
            $(dialog).find("#Icon").css('backgroundImage', 'url(' + errorObj.ErrorMessage.ErrorMessageImageURL + ')');
            $(dialog).find("p").html(errorObj.ErrorMessage.ErrorMessageText);
            $(dialog).find("button").html(errorObj.ErrorMessage.ErrorMessageButtonText);
            //isDialogueOpen = true;

            $(dialog).find("button").bind('click.namespace', function() {
                $(dialog).find("button").unbind('click.namespace');
                //$(overlay).fadeOut("slow");
                //$(dialog).fadeOut("slow");

                errorMessageClick();
                resetCPIdleTimer();
                isDialogueOpen = false;

                return false;
            });

        });

        //}// if end
    } // ShowErrorMessageRendering end...

    this.ShowCustomMessageRendering = function(customObj) {
        if (customObj.CustomMessage.CustomMessageType == "SessionEnd") {

            //$(disableScreen).show();
            if (customObj.CustomMessage.RedirectURL != "")
            {
                cp.RedirectCoursePlayer(customObj.CustomMessage.RedirectURL);
            }
            else
            {
                if ($("#examIdleTimerDialogue").hasClass("modal-is-visible"))
                {
                    ui.svgModal.close('modal-idle');
                }
                
                if ($("#timerExpireDialogue").hasClass("modal-is-visible"))
                {
                    ui.svgModal.close('modal-Expire');
                }                
                window.open("CoursePlayerExit.aspx","_self");
            }
            return;
            //window.close();
        }
        else if (customObj.CustomMessage.CustomMessageType == "CourseEnd") {
            var html = "<section class='scene-wrapper visual-left'><div class='scene-body'><div class='scene-cell'><img class='img-responsive mainimage'  src='" + customObj.CustomMessage.MessageImageURL + "'/></div><div class='scene-cell'><h1 class='scene-title'>'"+ customObj.CustomMessage.MessageHeading +"'</h1><div class='scene-content'><p> '"+ customObj.CustomMessage.MessageHeading +"'</p></div><div class='scene-content'><p>&copy; 2006 - 2010 360training.com &trade; All Rights Reserved</p></div></div></div></section>";
//            html += "<section class='scene-wrapper visual-left'>";
//            html += "<div class='scene-body'>";
//            html += "<div class='scrollable'>";
//            html += "<table id='session-error-table' border='0' cellpadding='0' cellspacing='0' align='center' style='margin-top: 82px;'>";
//            html += "<tr>";
//            html += "<td valign='top' width='60' height='70'>";
//            html += "<div id='icon-warning'>";
//            html += "<img src='" + customObj.CustomMessage.MessageImageURL + "'/>";
//            html += "</div>";
//            html += "</td>";
//            html += "<td valign='top'>";
//            html += "<div id='error-heading'>";
//            html += customObj.CustomMessage.MessageHeading;
//            html += "</div>";
//            html += "</td>";
//            html += "</tr>";
//            html += "<tr>";
//            html += "<td height='300'>";
//            html += "&nbsp;";
//            html += "</td>";
//            html += "<td valign='top'>";
//            html += "<div id='error-message'>";
//            html += customObj.CustomMessage.MessageText;
//            html += "</div>";
//            html += "</td>";
//            html += "</tr>";
//            html += "</table>";
//            html += "</div>";
//            html += "</div>";
//            html += "<div id='frame_footer'>";
//            html += "<div class='copyright_text'>&copy; 2006 - 2010 360training.com &trade; All Rights Reserved</div>";
//            html += "</div>";
//            html += "</div>";
            // - <a href='#' style='color:#999999;text-decoration:underline;'>About Us</a> | <a href='#' style='color:#999999; text-decoration:underline;'>Contact Us</a> | <a href='#' style='color:#999999;text-decoration:underline;'>Online Privacy Practices</a>
            document.body.innerHTML = html;
            /*
            //$(disableScreen).show();
            if (customObj.CustomMessage.RedirectURL != "")
            cp.RedirectCoursePlayer(customObj.CustomMessage.RedirectURL);
            else
            window.close();
            */
            return;
            //window.close();
        }

        $(htmlContentContainer).find('span').empty();

        if (document.getElementById('media') != null)
            document.getElementById('media').innerHTML = "";

        //$(htmlContentContainer).empty();
        $(swfContainer).empty();

        //if(!isDialogueOpen){
        jQuery().ready(function() {
            //$(overlay).css({ "opacity": "0.7" });
            //$(overlay).fadeIn("slow");
            //$(dialog).fadeIn("slow");


            $(dialog).find("h2").html(customObj.CustomMessage.MessageHeading);
            $(dialog).find("#dialogHeading h1").html(customObj.CustomMessage.MessageHeading);
            $(dialog).find("#Icon").css('backgroundImage', 'url(' + customObj.CustomMessage.MessageImageURL + ')');
            $(dialog).find("p").html(customObj.CustomMessage.MessageText);
            $(dialog).find("button").html(customObj.CustomMessage.ButtonText);
            isDialogueOpen = true;

            $(dialog).find("button").bind('click.namespace', function() {
                $(dialog).find("button").unbind('click.namespace');
                $(dialog).fadeOut("slow");
                //$(overlay).fadeOut("slow");

                if (customObj.CustomMessage.CustomMessageType == "SessionEnd") {

                    $(disableScreen).show();
                    if (customObj.CustomMessage.RedirectURL != "")
                        cp.RedirectCoursePlayer(customObj.CustomMessage.RedirectURL);
                    //window.close();
                }
                else if (customObj.CustomMessage.CustomMessageType == "CourseEnd") {

                    //$(PlaybuttonEn).hide();
                    //$(PlaybuttonDs).show();
                    //$(PlaybuttonEn).hide();
                    //$(PlaybuttonDs).show();
                    $(disableScreen).show();
                    cp.ContinueAfterCourseEnd();
                    if (customObj.CustomMessage.RedirectURL != "") {

                        cp.RedirectCoursePlayer(customObj.CustomMessage.RedirectURL);
                    }
                    else {
                        if ($("#examIdleTimerDialogue").hasClass("modal-is-visible"))
                        {
                            ui.svgModal.close('modal-idle');
                        }         
                        if ($("#timerExpireDialogue").hasClass("modal-is-visible"))
                        {
                            ui.svgModal.close('modal-Expire');
                        }                                        
                        window.open("CoursePlayerExit.aspx","_self");
                    }
                }
                else {

                    customMessageClick();
                    resetCPIdleTimer();
                    isDialogueOpen = false;

                    return false;
                }
            });

        });

        //}// if end

    }   // ShowCustomMessageRendering end...




    this.ShowQuestion = function(questionObject) {

        //LCMS-8188 - Start
        if (isBrowserVersionIE6 == true) {
            var mainCont = $('#quiz_container');
            if (mainCont.css('position') != 'relative') {
                mainCont.css('position', 'relative');
            }
        }
        //LCMS-8188 - End
        $('#buttoncontainerAnswerReview').hide();
        $('#buttoncontainerAnswerReviewPage').hide();
        //seting the template        
        $('#AssessmentInProgress').show();        
        $(quiz_container).find('#assessmentQuestionTemplate').empty();
        $('#QuestionRemediationContainer').find('#questionRemediationTemplate').empty();
        $('#assessmentQuestionTemplate').html(questionObject.AssessmentItem.TemplateHTML);        
        //inserting divs for answer checking logic

        //document.getElementById('divQuizColumn1').innerHTML="<div id='quizcolumn1'><!-- firstColumn --><h3></h3><div id='question'></div></div>";

        //LCMS-5770 - Start
        document.getElementById('divQuizColumn1').innerHTML = "<div id='quizcolumn1' tabindex = '-1' ><!-- firstColumn --><div id='questionStem'></div><div id='question' ></div></div>";
        //LCMS-5770 - End

        document.getElementById('divIncorrect').innerHTML = '<div id="incorrect">' + INCORRECT + '</div>';
        document.getElementById('divCorrect').innerHTML = '<div id="correct">' + CORRECT + '</div>';
        document.getElementById('divQuestionDescription').innerHTML = '<div id="questiondescription" tabindex = "-1" role="alert"></div>';
        //set the div for either media or image
        try {
            setMediaType(questionObject.AssessmentItem.VisualTopType, '#assessmentImage', '#assessmentMedia');
        } catch (e) {

        }


        // alert('ShowQuestion');
        closeAccordian(panel);
        $(panelbutton).css("display", "none");
        $(NextQuestionButtonEn).unbind('click.namespace');
        $(gradeAssessment).unbind('click.namespace');

        jQuery().ready(function() {
            $(assessmentControlPanel).show();
            $(NextQuestionButtonEn).show();
//            $(controlPanel).find("#IcoInstructorInformation").hide();
//            $(controlPanel).find("#IcoInstructorInformationDs").show();        

//            $(controlPanel).find("#IcoTOC").hide();
//            $(controlPanel).find("#IcoTOCDs").show();        

//            $(controlPanel).find("#IcoGlossary").hide();
//            $(controlPanel).find("#IcoGlossaryDs").show();        

//            $(controlPanel).find("#IcoCourseMaterial").hide();
//            $(controlPanel).find("#IcoCourseMaterialDs").show();        
//                    
//            $(controlPanel).find("#modal-trigger-bookmark").hide();
//            $(controlPanel).find("#cd-tour-trigger").hide();

//            $(controlPanel).find("#IcoConfigure").hide();
//            $(controlPanel).find("#IcoConfigureDs").show();

//            $(controlPanel).find("#IcoHelp").hide();
//            $(controlPanel).find("#IcoHelpDs").show();

//            $(controlPanel).find("#IcoCourseCompletion").hide();
//            $(controlPanel).find("#IcoCourseCompletionDs").show();	
//            
//            $(controlPanel).find("#IcoRecommendationCoursePanel").hide();
//            $(controlPanel).find("#IcoRecommendationCoursePanelDs").show();            
            if ($('#controlPanel').is(':hidden') == true) {
                //$('#odometerContainter').css('background', 'url()');
            }
            //$(assessmentTimer).show();
            //$(timer).hide();


            // Hide Other div's
            //$(swfContainer).hide(); // Fix for LCMS-7761
            $('#QuestionRemediationContainer').hide();
            $('#assesmentcontainer').hide();
            $('#quiz_container').hide();
            $('#assesmentcontainer').hide();
            $('#AnswerReviewContainer').hide();
            $('#IndividualScoreContainer').hide();
            $('#correct').hide();
            $('#incorrect').hide();
            $('#questiondescription').hide();
            $('#questionfeedback').hide();
            $('#toogle-flag').addClass('hide'); //Added By Abdus Samad LCMS-12105
            $(htmlContentContainer).hide();
            $('#QuestionRemediationButtons').hide();
            $('#buttoncontainerAnswerReview').hide();
            $('#buttoncontainerAnswerReviewPage').hide();


            $('#incorrect').css('backgroundImage', 'url(\'\')');
            $('#correct').css('backgroundImage', 'url(\'\')');
            //$(PlaybuttonEn).hide();
            //$(PlaybuttonDs).hide();
            //alert("this is here");
            $(quiz_container).show();

        });

        //   alert("Incoming assessmentId" + questionObject.AssessmentItem.AssessmentItemID);

        //LCMS-12105
        //Abdus Samad Start
        if (allowSkipping == true) {
            if (questionObject.AssessmentItem.PolicyEachQuestionAnswered != true) {                
                $('#toogle-flag').removeClass('hide');
                //$('#toogle-flag').css('display', 'block'); //Added By Abdus Samad LCMS-12105
                //document.getElementById("ImageButton1").src = "/Images/Flag1.png"
            }
        }


        var assesmentId = questionObject.AssessmentItem.AssessmentItemID;
        var questionNo = questionObject.AssessmentItem.QuestionNo;
        var totalQuestions = questionObject.AssessmentItem.TotalQuestion;
        var questionCounter = Question_text + " " + questionNo + " " + of + " " + totalQuestions;
        var questionStem = replaceSpacesAndLineBreaks(questionObject.AssessmentItem.QuestionStem);
        var questionType = questionObject.AssessmentItem.QuestionType;
        var arrAnswers = questionObject.AssessmentItem.AssessmentAnswers;
        var arrStudentAnswers = questionObject.AssessmentItem.StudentAnswer.AnswerIDs;
        var arrStudentAnswersText = questionObject.AssessmentItem.StudentAnswer.AnswerTexts;
        var assessmentItemToogled = questionObject.AssessmentItem.StudentAnswer.ToogleFlag;


        if (assessmentItemToogled == true) {
            $("#toogle-flag").addClass('flagged');   
            //document.getElementById("ImageButton1").src = '/assets/img/Flag2.png';
            //document.getElementById("ImageButton1").title = assessment_toggle_flag1_tx; //"Click to remove the flag on this question.";
        }
        else {
            $("#toogle-flag").removeClass('flagged');   
            //document.getElementById("ImageButton1").src = '/assets/img/Flag1.png';
            //document.getElementById("ImageButton1").title = assessment_toggle_flag2_tx; //"Click to flag this question for review.";
        }



        if (arrStudentAnswers.length > 0 || arrStudentAnswersText.length > 0) {
            if (IsAssessmentIncomplete == false) {
                $('#buttoncontainerAnswerReview').show();                
                $('#buttoncontainerAnswerReviewPage').show();
                $('#buttoncontainerAnswerReviewPage').find("button").text(ReturntoAnswerReviewText);
                $('#buttoncontainerAnswerReview').find("div").eq(0).find("button").bind('click.namespace', function() {
                    $('#buttoncontainerAnswerReview').find("div").eq(0).find("button").unbind('click.namespace');                    
                    $(NextQuestionButtonEn).trigger("click");
                    return false;
                });
            }
            else {
                $('#buttoncontainerAnswerReview').hide();
                $('#buttoncontainerAnswerReviewPage').hide();
            }
        }



        //	   alert(arrStudentAnswers.length);
        //	   
        //	   for(var i=0; i<arrStudentAnswers.length; i++)
        //	   {
        //	        alert(arrStudentAnswers[i]);
        //	   }



        //$(quiz_container).find('h1').html(questionCounter);
        $('#assessmentControlPanel').find('.question-counter').html(questionCounter);

        switch (questionType) {

            case "True False": // Generate radio buttons	        
                //$('#quiz_container').find('#question').removeAttr('disabled');
                //alert($('#quiz_container').find('#question').find('#single').removeAttr('disabled'));
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');                   
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;    

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');

                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End


                this.RenderTrueFalse(arrAnswers, arrStudentAnswers);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;
                break;

            case "Single Select MCQ":
                //$('#quiz_container').find('#question').removeAttr('disabled');	            
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');                   
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;                           

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');

                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End


                this.RenderSingleSelectMcq(arrAnswers, arrStudentAnswers);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;

                document.getElementById('quizcolumn1').focus(); //508 Complainance
                //document.getElementById('question').focus(); //508 Complainance

                break;

            case "Multiple Select MCQ":
                //$('#quiz_container').find('#question').removeAttr('disabled');	            
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');

                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End

                this.RenderMultiSelectMcq(arrAnswers, arrStudentAnswers);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;

                document.getElementById('quizcolumn1').focus(); //508 Complainance  
                break;

            case "Image Selection":
                //$('#quiz_container').find('#question').removeAttr('disabled');	            
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End


                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');
                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End


                this.RenderImageTargetQuestion(questionObject.AssessmentItem.ImageURL, arrAnswers, arrStudentAnswers, questionObject.AssessmentItem.TemplateType);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;
                break;


            case "Fill in the Blanks":
                //$('#quiz_container').find('#question').removeAttr('disabled');	            
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');
                this.RenderFillInTheBlanks(questionStem, arrAnswers, arrStudentAnswersText);
                if (document.getElementById('A1') != null)
                    document.getElementById('A1').disabled = false;

                break;


            case "Ordering":
                //$('#quiz_container').find('#question').removeAttr('disabled');	            
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').removeAttr('disabled');
                // $('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                // $('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');
                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End

                this.RenderOrdering(arrAnswers, arrStudentAnswers);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;
                break;


            case "Matching":
                $//('#quiz_container').find('#question').removeAttr('disabled');	         
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').removeAttr('disabled');
                // $('#quiz_container').find('#quizcolumn1').find('#question').find('#multi').disabled=false;               
                //$('#quiz_container').find('#quizcolumn1').find('#question').find('#single').disabled=false;

                //LCMS-5770 - Start
                //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                $('#quiz_container').find('#quizcolumn1').find('questionStem').find('#A1').removeAttr('disabled');
                //LCMS-5770 - End

                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                $('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');
                //LCMS-5770 - Start
                //$(quiz_container).find('h3').html(questionStem);
                InsertQustionStem(questionStem);
                //LCMS-5770 - End

                this.RenderMatching(arrAnswers, arrStudentAnswers, arrStudentAnswersText);
                if (document.getElementById('single') != null)
                    document.getElementById('single').disabled = false;
                if (document.getElementById('multi') != null)
                    document.getElementById('multi').disabled = false;
                break;

        } // Switch case end


        $(NextQuestionButtonEn).unbind('click.namespace');
        $(NextQuestionButtonEn).bind('click.namespace', function() {
            //$(NextQuestionButtonEn).unbind('click.namespace');

            //stop sound
            if (questionObject.AssessmentItem.AudioURL != "")
                stopSound();

            $('#assessmentcontent ul').find("ul").empty();

            switch (questionType) {
                case "True False":
                    //$('#quiz_container').find('#question').removeAttr('disabled');
                    if (document.getElementById('single') != null)
                        document.getElementById('single').disabled = false;
                    if (document.getElementById('multi') != null)
                        document.getElementById('multi').disabled = false;
                    SubmitTrueFalse(assesmentId, "submitAssessment");
                    for (var i = 0; i < arrAnswers.length; i++) {
                        $('#quiz_container').find('#question').find('#' + arrAnswers[i].AssessmentItemAnswerID).attr('disabled', 'disabled');
                    }

                    //RenderTrueFalseDisabled(arrAnswers);            

                    break;

                case "Fill in the Blanks":
                    //$('#quiz_container').find('#question').removeAttr('disabled');	
                    //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                    if (document.getElementById('A1') != null)
                        document.getElementById('A1').disabled = true;
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');	                        
                    SubmitFillInTheBlank(assesmentId, "submitAssessment");
                    //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').attr('disabled','disabled');

                    break;

                case "Ordering":
                    //$('#quiz_container').find('#question').removeAttr('disabled');	
                    if (document.getElementById('ListBox') != null)
                        document.getElementById('ListBox').disabled = true;

                    if (document.getElementById('btnA') != null)
                        document.getElementById('btnA').disabled = true;

                    if (document.getElementById('btnB') != null)
                        document.getElementById('btnB').disabled = true;

                    //$('#quiz_container').find('#quizcolumn1').find('h3').find('#A1').removeAttr('disabled');
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').removeAttr('disabled');
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').removeAttr('disabled');	                        
                    SubmitOrdering(assesmentId, "submitAssessment");
                    //$('#quiz_container').find('#question').find('#orderselect').find('#ListBox').attr('disabled', 'disabled');	             
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnA').attr('disabled', 'disabled');
                    //$('#quiz_container').find('#question').find('#orderbuttons').find('#btnB').attr('disabled', 'disabled');	                    

                    break;

                case "Matching":
                    //$('#quiz_container').find('#question').removeAttr('disabled');	             


                    SubmitMatching(assesmentId, arrAnswers, "submitAssessment");
                    //	            $('.drag').unbind('dragstart.namespace');
                    //	            $('.drag').unbind('drag.namespace');
                    //	            $('.drag').unbind('dragend.namespace');            	
                    //	            $('.drop').unbind('dropstart.namespace');
                    //	            $('.drop').unbind('drop.namespace');
                    //	            $('.drop').unbind('dropend.namespace'); 
                    break;

                case "Image Selection":
                    //$('#quiz_container').find('#question').removeAttr('disabled');	
                    $('.hotspotanswer').unbind('click.namespace');
                    SubmitImageTarget(assesmentId, "submitAssessment");

                    break;

                case "Single Select MCQ":

                    if (document.getElementById('single') != null)
                        document.getElementById('single').disabled = true;
                    //$('#quiz_container').find('#question').removeAttr('disabled');

                    SubmitSingleSelectMCQ(assesmentId, "submitAssessment");
                    break;

                case "Multiple Select MCQ":

                    if (document.getElementById('multi') != null)
                        document.getElementById('multi').disabled = true;

                    SubmitMultiSelectMCQ(assesmentId, "submitAssessment");
                    //$('#quiz_container').find('#question').find('#multi').attr('disabled','disabled');        	   
                    //document.getElementById('multi').disabled=true;
                    break;
            }

        }); // End NextQuestionButtonEn Handler

        if (showGradeAssessment == true) {
            //LCMS-7791
            //$(gradeAssessment).show();
        }
        else {
            $(gradeAssessment).hide();
        }

        $(gradeAssessment).bind('click.namespace', function() {
            $(gradeAssessment).unbind('click.namespace');
            // $('#assessmentcontent ul').find("ul").empty();

            //stop sound
            if (questionObject.AssessmentItem.AudioURL != "")
                stopSound();

            switch (questionType) {
                case "True False":
                    SubmitTrueFalse(assesmentId, "gradeAssessment");
                    break;

                case "Fill in the Blanks":
                    SubmitFillInTheBlank(assesmentId, "gradeAssessment");
                    break;

                case "Ordering":
                    SubmitOrdering(assesmentId, "gradeAssessment");
                    break;

                case "Matching":
                    SubmitMatching(assesmentId, arrAnswers, "gradeAssessment");
                    break;

                case "Image Selection":
                    SubmitImageTarget(assesmentId, "gradeAssessment");
                    break;

                case "Single Select MCQ":
                    SubmitSingleSelectMCQ(assesmentId, "gradeAssessment");
                    break;

                case "Multiple Select MCQ":
                    SubmitMultiSelectMCQ(assesmentId, "gradeAssessment");
                    break;
            }
        });


        $(NextQuestionButtonDs).hide();
        //alert(allowSkipping);
        //LCMS-11739 Waqas Zakai START
        if (allowSkipping == false) {
            if ((arrStudentAnswers != null && arrStudentAnswers.length > 0) || (arrStudentAnswersText != null && arrStudentAnswersText.length > 0)) {
                //LCMS-12010 Waqas Zakai Start
                //assessmentTimerObj.StopTimer();	            
                //$('#assessmentTimer').hide();
                //LCMS-12010 Waqas Zakai Start
                $(NextQuestionButtonEn).show();
                $(NextQuestionButtonDs).hide();

            }
            else {
                // alert('Hiding En Showing DS');
                $(NextQuestionButtonEn).hide();
                $(NextQuestionButtonDs).show();
            }
        }
        else if (allowSkipping == true) {
            if ((arrStudentAnswers != null && arrStudentAnswers.length > 0) || (arrStudentAnswersText != null && arrStudentAnswersText.length > 0)) {
                //LCMS-12010 Waqas Zakai Start
                //assessmentTimerObj.StopTimer();	            
                //$('#assessmentTimer').hide();
                //LCMS-12010 Waqas Zakai Start	            
                $(NextQuestionButtonEn).show();
                $(NextQuestionButtonDs).hide();
            }
            else {
                $(NextQuestionButtonEn).show();
                $(NextQuestionButtonDs).hide();
            }
        }
        //LCMS-11739 Waqas Zakai END
        //LCMS-2592
        //Reset the Idle Timer 
        resetCPIdleTimer();

        //sound playing

        if (questionObject.AssessmentItem.AudioURL != "") {
            try {
                $('#swfContainer').show();
                //var isSoundOff = $("input[@id='SoundOff']").attr("checked");
                var isSoundOff=$("#speakerToggle > div").hasClass("switch-off");
                loadSound(questionObject.AssessmentItem.AudioURL);
                if (isSoundOff) {
                    this.setVolumeValue(0);

                } else {
                    var volume = this.getSliderValue();
                    this.setVolumeValue(volume)
                }
            }
            catch (Error) {
                //alert(Error);
            }
        }
    }         // function ShowQuestion end	

    this.ShowCourseEvaluationQuestions = function(questionObject) {
        $(PlaybuttonEn).show();
        $(PlaybuttonDs).hide();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).hide();

        var questionList = questionObject.CourseEvaluation.CourseEvaluationQuestions;
        var courseEvalHTML = "";
        courseEvalHTML = courseEvalHTML + '<section class="scene-wrapper"><div class="scene-body"><div class="scene-cell visual-element"><div class="scene-content"><div style="display:none;" id="message_box" class="bg-danger" ><img class="img-responsive" src="assets/img/close.png" style="float: right;cursor: pointer;" id="close_message"><div class="width40pct"><div class="message_box_align" style="float:left"></div><div class="message_box_align" style="float:left">&nbsp;' + courseEval_Req_Validation + '</div></div></div><div id="EvalQuestions" style="margin-top:10px;"><h1 style="padding-left: 20px; padding-top: 30px;">' + questionObject.CourseEvaluation.CourseEvaluationName + '</h1>';
        //alert(questionObject);
        //alert(questionList.length);
        for (i = 0; i < questionList.length; i++) {
            switch (questionList[i].Quetiontype) {
                case "MSSQ":
                    courseEvalHTML = courseEvalHTML + this.ShowCourseEvaluationQuestionsMSSQ(questionList[i], questionList[i].QuestionNo);
                    break;
                case "SSSQ":
                    courseEvalHTML = courseEvalHTML + this.ShowCourseEvaluationQuestionsSSSQ(questionList[i], questionList[i].QuestionNo);
                    break;
                case "FITB":
                    courseEvalHTML = courseEvalHTML + this.ShowCourseEvaluationQuestionsFITB(questionList[i], questionList[i].QuestionNo);
                    break;
                case "TEXT":
                    courseEvalHTML = courseEvalHTML + this.ShowCourseEvaluationQuestionsTEXT(questionList[i], questionList[i].QuestionNo);
                    break;
            }
        }
        var errorDiv = "";
        courseEvalHTML = courseEvalHTML + "</div></div></div></section>";
        courseEvalHTML = courseEvalHTML;
        $(htmlContentContainer).html(courseEvalHTML);
        //Add Event 	     
        $('#close_message').click(function() {
            $('#message_box').hide();
        });        
    }


    this.ShowProctorAuthenticationResult = function(proctorAuthenticationResultObject) {
        var msg = proctorAuthenticationResultObject.ProctorAuthenticationResult.ErrorMessage;       
        //$("#proctor_login_screen").hide(); 
        if (msg == "") {            
            ui.slide.next(function()
            {
                cp.CallNextSlide();
            });	        
        }
        else {                
                $("#proctor_login_screen").find("#proctorErrorLabel").html('<i class="glyphicon glyphicon-warning-sign"></i> ' +  msg);
                return false;
            }
    }


    this.ShowProctorLoginScreen = function(proctorLoginScreenObject) {
        $("#proctor_login_screen").find("#proctorErrorLabel").html("");
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $("#proctorLoginErrorMessage").addClass('hide');
        $("#proctorPasswordErrorMessage").addClass('hide');

        $("#proctor_login_screen").show();
        //$("#proctor_login_screen").find("#icoAutheticating").html("<img src='" + proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenProgressAnimation + "' /> <font size='2'>Authenticating...</font>");
        //$("#proctor_login_screen").find("#icoAutheticating").hide();
        $("#htmlContentContainer").hide();
        $(swfContainer).hide();



        $("#proctor_login_screen").find("#btnProctorLoginSubmit").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenSubmitButtonText);
        $("#proctor_login_screen").find("#proctorLoginScreenMessage").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenContent);
        $("#proctor_login_screen").find("#proctorLoginHeading").html("<h2>" + proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenHeading + "</h2>");
        $("#proctor_login_screen").find("#attemptMessage").html("");
        //$("#proctor_login_screen").find("#attemptMessage").html("(You will have " + proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenAttempts + " attempts to enter the correct Proctor ID and Password.)");
        $("#proctor_login_screen").find("#attemptMessage").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorattemptMessageText);
        $("#proctor_login_screen").find("#ProctorLoginTableText").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginTableText);
        $("#proctor_login_screen").find("#ProctorLoginIDText").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginIDText + '<span class="red-font">*</span>');
        $("#proctor_login_screen").find("#proctorLoginErrorMessage").text(proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginIDRequiredValidationText);
        
        $("#proctor_login_screen").find("#ProctorPasswordText").html(proctorLoginScreenObject.ProctorLoginScreen.ProctorPasswordText + '<span class="red-font">*</span>');
        $("#proctor_login_screen").find("#proctorPasswordErrorMessage").text(proctorLoginScreenObject.ProctorLoginScreen.ProctorPasswordRequiredValidationText);
        
        $("#proctor_login_screen").find("#proctorLoginIcon").css("background", "url(" + proctorLoginScreenObject.ProctorLoginScreen.ProctorLoginScreenHeadingImage + ") no-repeat");
        $("#proctor_login_screen").find("#proctorLoginIcon").css("width", "80px");
        $("#proctor_login_screen").find("#proctorLoginIcon").css("height", "80px");
        document.getElementById("proctorLogin").value = "";
        document.getElementById("proctorPassword").value = "";

        $("#proctor_login_screen").find("#btnProctorLoginSubmit").unbind('click.namespace');

        $("#proctor_login_screen").find("#btnProctorLoginSubmit").bind('click.namespace', function() {
            $("#proctor_login_screen").find("#proctorErrorLabel").html("");

            $("#proctorLoginErrorMessage").addClass('hide');
            $("#proctorPasswordErrorMessage").addClass('hide');

            if ((document.getElementById("proctorLogin").value.trim() == "") || (document.getElementById("proctorPassword").value.trim() == "")) {
                if ((document.getElementById("proctorLogin").value.trim() == "")) {
                    $("#proctorLoginErrorMessage").removeClass('hide');
                    $("#proctorLogin").parent().addClass('has-error');
                }
                else
                {
                    $("#proctorLogin").parent().removeClass('has-error');
                }

                if ((document.getElementById("proctorPassword").value.trim() == "")) {
                    $("#proctorPasswordErrorMessage").removeClass('hide');
                    $("#proctorPassword").parent().addClass('has-error');
                }
                else
                {
                    $("#proctorPassword").parent().removeClass('has-error');
                }

                return false;
            }
            else
            {
                if ((document.getElementById("proctorLogin").value.trim() != "")) {
                     $("#proctorLogin").parent().removeClass('has-error');
                }
                
                if ((document.getElementById("proctorPassword").value.trim() != "")) {
                    $("#proctorPassword").parent().removeClass('has-error');
                }
            }
            //$("#proctor_login_screen").find("#icoAutheticating").show();            
            AuthenticateProctor(convertJSString(document.getElementById("proctorLogin").value), convertJSString(document.getElementById("proctorPassword").value));            
            return false;
        });

    }


    this.ShowSpecialPostAssessmentValidation = function(showSpecialPostAssessmentValidationObject) {
        $("#CARealStateValidation").find("#CARealStateErrorLabel").html("");
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $('#controlPanel').show();
        $('#assessmentControlPanel').hide();
        $('#timer').hide();



        $("#CARealStateValidation").show();
        $("#CARealStateValidation").html(showSpecialPostAssessmentValidationObject.SpecialPostAssessmentValidation.Template);
        $("#CAREALSTATEFIELD1ErrorMessage").html(showSpecialPostAssessmentValidationObject.SpecialPostAssessmentValidation.CARealStateRequiredValidationMessage1);
        $("#CAREALSTATEFIELD2ErrorMessage").html(showSpecialPostAssessmentValidationObject.SpecialPostAssessmentValidation.CARealStateRequiredValidationMessage2);


        $("#htmlContentContainer").hide();
        $(swfContainer).hide();

        document.getElementById("DRELicenseNumber").value = "";
        document.getElementById("DriverLicenseNumber").value = "";


        $("#CAREALSTATEFIELD1ErrorMessage").addClass('hide');
        $("#CAREALSTATEFIELD2ErrorMessage").addClass('hide');

        $("#btnCARealStateValidationContinue").unbind('click.namespace');
//        $("#btnContinueStart").attr("class", "btn-start");
//        $("#btnCARealStateValidationContinue").find("a").attr("class", "btn-stem");
//        $("#btnContinueEnd").attr("class", "btn-end");

        document.getElementById("DRELicenseNumber").focus();
        document.getElementById("DRELicenseNumber").select();

        //$('#CARealStateValidation').find('input[type=text]').bind('click.namespace', function() {

        /*if ((document.getElementById("DRELicenseNumber").value.trim() == "") && (document.getElementById("DriverLicenseNumber").value.trim() == "")) {
        return;
        }*/
        //if ((document.getElementById("DRELicenseNumber").value.trim() != "") && (document.getElementById("DriverLicenseNumber").value.trim() != "")) {
        /*
        $("#btnCARealStateValidationContinue").find("a").unbind('click.namespace');	    
        $("#btnContinueStart").attr("class","btn-start");
        $("#btnCARealStateValidationContinue").find("a").attr("class","btn-stem");
        $("#btnContinueEnd").attr("class","btn-end");
        */
        $("#btnCARealStateValidationContinue").bind('click.namespace', function() {
            $("#CARealStateValidation").find("#CARealStateErrorLabel").html("");
            $("#CAREALSTATEFIELD1ErrorMessage").addClass('hide').parent().removeClass('has-error');
            $("#CAREALSTATEFIELD2ErrorMessage").addClass('hide').parent().removeClass('has-error');                        
            $("#CAREALSTATEFIELD1ErrorMessage").addClass('hide');
            $("#CAREALSTATEFIELD2ErrorMessage").addClass('hide');
            if ((document.getElementById("DRELicenseNumber").value.trim() == "") || (document.getElementById("DriverLicenseNumber").value.trim() == "")) {
                if (document.getElementById("DRELicenseNumber").value.trim() == "") {                    
                     $("#CAREALSTATEFIELD1ErrorMessage").removeClass('hide').parent().addClass('has-error');
                }

                if (document.getElementById("DriverLicenseNumber").value.trim() == "") {                    
                    $("#CAREALSTATEFIELD2ErrorMessage").removeClass('hide').parent().addClass('has-error');
                }
                return false;
            }
            cp.AuthenticateSpecialPostAssessmentValidation(convertJSString(document.getElementById("DRELicenseNumber").value), convertJSString(document.getElementById("DriverLicenseNumber").value));
            return false;
        });
        //}




        $("#btnCARealStateValidationCancel").unbind('click.namespace');
        $("#btnCARealStateValidationCancel").bind('click.namespace', function() {
            ui.slide.prev(function()
		    {
		        cp.CancelSpecialPostAssessmentValidation();
		    }); 
		    return false;       
        });

    }

    this.ShowSpecialPostAssessmentAuthenticationResult = function(specialPostAssessmentAuthenticationResultObject) {
        var msg = specialPostAssessmentAuthenticationResultObject.SpecialPostAssessmentAuthenticationResult.ErrorMessage;
        if (msg == "") {
            cp.CallNextSlide();
        }
        else {
            $("#CARealStateValidation").find("#CARealStateErrorLabel").html('<i class="glyphicon glyphicon-warning-sign"></i> ' + msg);
        }
    }

    //NY Insurance

    this.ShowNYInsuranceValidation = function(showSpecialPostAssessmentValidationObject) {
        $("#NYInsuranceValidation").find("#NYInsuranceErrorLabel").html("");
        $("#NYInsuranceValidation").html("");
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $('#controlPanel').show();
        $('#assessmentControlPanel').hide();
        $('#timer').hide();

        $("#NYInsuranceValidation").show();
        $("#NYInsuranceValidation").html(showSpecialPostAssessmentValidationObject.SpecialPostAssessmentValidation.Template);
        $("#htmlContentContainer").hide();
        $(swfContainer).hide();

        document.getElementById("MonitorNumber").value = "";
        $("#NYInsuranceFIELDErrorMessage").hide();

        $("#btnNYInsuranceValidationContinue").unbind('click.namespace');
        //$("#btnContinueStart").attr("class", "btn-start");
        //$("#btnNYInsuranceValidationContinue").find("a").attr("class", "btn-stem");
        //$("#btnContinueEnd").attr("class", "btn-end");

        document.getElementById("MonitorNumber").focus();
        document.getElementById("MonitorNumber").select();
        $("#NYInsuranceFIELDErrorMessage").html("<font color='red'>"+showSpecialPostAssessmentValidationObject.SpecialPostAssessmentValidation.NYInsuranceRequiredValidationMessage+"</font>");
        //$('#NYInsuranceValidation').find('input[type=text]').bind('click.namespace', function() {

        //$("#btnNYInsuranceValidationContinue").find("a").unbind('click.namespace');	    
        //$("#btnContinueStart").attr("class","btn-start");
        //$("#btnNYInsuranceValidationContinue").find("a").attr("class","btn-stem");
        //$("#btnContinueEnd").attr("class","btn-end"); 	    

        $("#btnNYInsuranceValidationContinue").bind('click.namespace', function() {
            $("#NYInsuranceValidation").find("#NYInsuranceErrorLabel").html("");
            $("#NYInsuranceFIELDErrorMessage").addClass('hide').parent().removeClass('has-error');
            if (document.getElementById("MonitorNumber").value.trim() == "") {
                $("#NYInsuranceFIELDErrorMessage").removeClass('hide').parent().addClass('has-error');
                return false;
            }
            cp.AuthenticateNYInsuranceValidation(convertJSString(document.getElementById("MonitorNumber").value));
            return false;
        });
        //});


        $("#btnNYInsuranceValidationCancel").unbind('click.namespace');
        $("#btnNYInsuranceValidationCancel").bind('click.namespace', function() {
            ui.slide.prev(function()
		    {
		        cp.CancelSpecialPostAssessmentValidation();
		    });
            return false;
        });

    }

    this.ShowNYInsuranceAuthenticationResult = function(specialPostAssessmentAuthenticationResultObject) {
        var msg = specialPostAssessmentAuthenticationResultObject.SpecialPostAssessmentAuthenticationResult.ErrorMessage;
        if (msg == "") {
            cp.CallNextSlide();
        }
        else {
            $("#NYInsuranceValidation").find("#NYInsuranceErrorLabel").html('<i class="glyphicon glyphicon-warning-sign"></i> ' + msg);
        }
    }


    this.ShowSeatTimeCourseLaunch = function(seattimecourselaunch) {
        $(NextQuestionButtonEn).hide();
        $(controlPanel).find("#IcoInstructorInformation").hide();
        $(controlPanel).find("#IcoInstructorInformationDs").show();        
        $(controlPanel).find("#IcoTOC").hide();
        $(controlPanel).find("#IcoTOCDs").show();        
        $(controlPanel).find("#IcoGlossary").hide();
        $(controlPanel).find("#IcoGlossaryDs").show();
        $(controlPanel).find("#IcoCourseMaterial").hide();
        $(controlPanel).find("#IcoCourseMaterialDs").show();          
        $(controlPanel).find("#modal-trigger-bookmark").hide();
        $(controlPanel).find("#cd-tour-trigger").hide();
        $(controlPanel).find("#IcoConfigure").hide();
        $(controlPanel).find("#IcoConfigureDs").show();
        $(controlPanel).find("#IcoHelp").hide();
        $(controlPanel).find("#IcoHelpDs").show();
        $(controlPanel).find("#IcoCourseCompletion").hide();
        $(controlPanel).find("#IcoCourseCompletionDs").show();

        $("#ValidationTimer").hide();
        $(ValidationPlaybuttonEn).hide();
        $(PlaybuttonEn).show();
        $(PlaybuttonDs).hide();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $("#contentObjectName").html("");
        $(htmlContentContainer).html(seattimecourselaunch.SeatTimeCourseLaunch.TemplateHtml);
        //Add Event 	     
        $(document).ready(function() {
            $('#close_message').click(function() {
                $('#message_box').animate({ opacity: 0 }, "slow");
            });
        });
        isMovieEnded = true;
        courseEvaluationInProgressTF = false;
    }

    function RenderTrueFalseString(arrAnswer) {      
        var arrAnswernew= arrAnswer.trim();
        if (arrAnswernew == "True")
            return true_text;
        else
            return false_text;
        
    }
    //Code by Danish Khan
    function RenderTrueFalseDisabled(arrAnswers) {
        var dynamicHtml = "";

        for (var i = 0; i < arrAnswers.length; i++) {
            //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li> 

            if (arrAnswers[i].Value == "True") {
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio'  id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' checked='checked' disabled='disabled' onclick='ShowHideNextQuestionButton();'  /><span>"+ RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
            }
            else {
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio'  id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' disabled='disabled' onclick='ShowHideNextQuestionButton();' /><span>"+ RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
            }
        }
        $('#question').html("<div class='scene-content'>" + dynamicHtml + "</div>");

    } // function RenderTrueFalseDisabled end


    this.RenderTrueFalse = function(arrAnswers, arrStudentAnswers) {
        var dynamicHtml = "";
        isSkipping = true;        
        $('toggle-flag').removeClass('hide');
        if (arrStudentAnswers.length > 0) {
            for (var i = 0; i < arrAnswers.length; i++) {
                //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                if (arrStudentAnswers[0] == arrAnswers[i].AssessmentItemAnswerID) {
                    dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' checked='checked' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /><span>" + RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
                }
                else {
                    dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /><span>" + RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
                }
            }
        }
        else {            
            for (var i = 0; i < arrAnswers.length; i++) {
                //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /><span>" + RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
            }
        }


        //LCMS-8188 - Start
        //$('#question').html("<ul>" + dynamicHtml + "</ul>");
        var questionCont = $('#question');
        questionCont.html("<div class='scene-content'>" + dynamicHtml + "</div>");
        questionCont.find("input[type=radio]").bind('click.namespace', function() {
            ShowHideNextQuestionButton();
        });
        //LCMS-8188 - End

    } // function RenderTrueFalse end

    this.RenderSingleSelectMcq = function(arrAnswers, arrStudentAnswers) {
        var dynamicHtml = "";
        isSkipping = true;
        $('toggle-flag').removeClass('hide');

        if (arrStudentAnswers.length > 0) {            
            for (i = 0; i < arrAnswers.length; i++) {
                if (arrStudentAnswers[0] == arrAnswers[i].AssessmentItemAnswerID) {
                    //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                    //dynamicHtml += "<li><table style='padding:0px;margin:0px;width:auto;height:auto;'><tr><td valign='top' style='padding-top:5px;'><input type='radio' checked='checked' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /></td><td><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Label + "</label></td></tr></table></li>";
                    dynamicHtml+="<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' checked='checked' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /><span>" + arrAnswers[i].Label + "  </span><i></i></label></div>"
                }
                else {
                    //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                    dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "'  /><span>" + arrAnswers[i].Label + " </span><i></i></label></div>";
                }
            }
        }
        else {
            for (i = 0; i < arrAnswers.length; i++) {
                //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "'  /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
            }
        }

        //LCMS-8188 - Start
        //$('#question').html("<ul id='single'>" + dynamicHtml + "</ul>");
        var questionCont = $('#question');
        questionCont.html("<div class='scene-content'>" + dynamicHtml + "</div>");
        questionCont.find("input[type=radio]").bind('click.namespace', function() {
            ShowHideNextQuestionButton();
        });
        //LCMS-8188 - End


    } // Function RenderSingleSelectMcq end

    this.RenderMultiSelectMcq = function(arrAnswers, arrStudentAnswers) {
        var dynamicHtml = "";
        var checked = "";
        isSkipping = true;
        $('toggle-flag').removeClass('hide');
        if (arrStudentAnswers.length > 0) {           
            for (var i = 0; i < arrAnswers.length; i++) {
                for (var x = 0; x < arrAnswers.length; x++) {
                    if (arrStudentAnswers[x] == arrAnswers[i].AssessmentItemAnswerID) {
                        //alert(arrStudentAnswers[x]+"\n"+arrAnswers[i].AssessmentItemAnswerID);
                        checked = "checked = 'checked'";
                    }
                }
                dynamicHtml += "<div class='scene-option checkbox'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='checkbox' " + checked + " id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
                checked = "";
            }
        }
        else {
            for (var i = 0; i < arrAnswers.length; i++) {
                //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
                dynamicHtml += "<div class='scene-option checkbox'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
            }
        }

        //LCMS-8188 - Start
        //$('#question').html("<ul id='multi'>" + dynamicHtml + "</ul>");
        var questionCont = $('#question');
        questionCont.html("<div class='scene-content' id='multi'>" + dynamicHtml + "</div>");
        questionCont.find("input[type=checkbox]").bind('click.namespace', function() {
            ShowHideNextQuestionButton();
        });
        //LCMS-8188 - End

    }

    this.ShowCourseEvaluationQuestionsMSSQ = function(courseEvalQuestion, questionNo) {
        var dynamicHtml = "";
        var checked = "";
        var isRequiredHtml = "";

        if (courseEvalQuestion.Required == true) {
            dynamicHtml += "<DIV style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_MSSQ_Required'>";
            isRequiredHtml = "<font color='red'>*</font>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "_Required'>" + '<img style="float:left;display:none" class="img-responsive" height="14" width="14" alt="" src="assets/img/exclamation.gif" />' + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "&nbsp;" + isRequiredHtml + "</h3>";
        }
        else {
            dynamicHtml += "<DIV style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_MSSQ'>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "'>" + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "</h3>";
        }


        for (var i = 0; i < courseEvalQuestion.CourseEvaluationAnswer.length; i++) {
            //dynamicHtml += "<table style='padding-left:20px;margin:0px;width:auto;height:auto;' title='" + convertJSString(courseEvalQuestion.CourseEvaluationAnswer[i].Label) + "'><tr><td valign='top' style='padding-top:5px;'><input type='checkbox' id='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "' name='checkbox_" + courseEvalQuestion.Id + "' value='" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "' ' /></td><td><label for='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "'>" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "</label></td></tr></table>";
            dynamicHtml += "<div class='checkbox' title='" + convertJSString(courseEvalQuestion.CourseEvaluationAnswer[i].Label) + "'><label for='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "'><input type='checkbox' id='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "' name='checkbox_" + courseEvalQuestion.Id + "' value='" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "' ' />" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "</label></div>";
        }

        dynamicHtml += "</DIV>";
        //$('#question').html("<ul id='multi'>" + dynamicHtml + "</ul>");
        return dynamicHtml;
        //alert(dynamicHtml);
    }


    this.ShowCourseEvaluationQuestionsSSSQ = function(courseEvalQuestion, questionNo) {
        var dynamicHtml = "";
        var checked = "";
        var isRequiredHtml = "";


        if (courseEvalQuestion.Required == true) {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_SSSQ_Required'>";
            isRequiredHtml = "<font color='red'>*</font>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "_Required'>" + '<img style="float:left;display:none" class="img-responsive" height="14" width="14" alt="" src="assets/img/exclamation.gif" />' + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "&nbsp;" + isRequiredHtml + "</h3>";
        }
        else {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_SSSQ'>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "'>" + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "</h3>";
        }


        for (var i = 0; i < courseEvalQuestion.CourseEvaluationAnswer.length; i++) {
            //dynamicHtml += "<table style='padding-left:20px;margin:0px;width:auto;height:auto;' title='" + convertJSString(courseEvalQuestion.CourseEvaluationAnswer[i].Label) + "'><tr><td valign='top' style='padding-top:5px;'><input type='radio' id='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "' name='radio_" + courseEvalQuestion.Id + "' value='" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "' ' /></td><td><label for='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "'>" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "</label></td></tr></table>";
            dynamicHtml += "<div class='radio' title='" + convertJSString(courseEvalQuestion.CourseEvaluationAnswer[i].Label) + "'><label for='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "'><input type='radio' id='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "' name='radio_" + courseEvalQuestion.Id + "' value='" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "' ' />" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "</label></div>";
        }

        dynamicHtml += "</DIV>";
        //$('#question').html("<ul id='multi'>" + dynamicHtml + "</ul>");
        return dynamicHtml;
        //alert(dynamicHtml);
    }

    this.ShowCourseEvaluationQuestionsFITB = function(courseEvalQuestion, questionNo) {
        var dynamicHtml = "";
        var checked = "";
        var isRequiredHtml = "";


        if (courseEvalQuestion.Required == true) {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_FITB_Required'>";
            isRequiredHtml = "<font color='red'>*</font>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "_Required'>" + '<img style="float:left;display:none" class="img-responsive" height="14" width="14" alt="" src="assets/img/exclamation.gif" />' + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "&nbsp;" + isRequiredHtml + "</h3>";
        }
        else {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_FITB'>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "'>" + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "</h3>";
        }


        //	        for(var i=0;i<courseEvalQuestion.CourseEvaluationAnswer.length;i++)
        //            {
        //                dynamicHtml +=  "<li><table style='padding:0px;margin:0px;width:auto;height:auto;'><tr><td valign='top' style='padding-top:5px;'><input type='radio' id='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "' name='radio' value='" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "' ' /></td><td><label for='" + courseEvalQuestion.CourseEvaluationAnswer[i].Id + "'>" + courseEvalQuestion.CourseEvaluationAnswer[i].Label + "</label></td></tr></table></li>";
        //            }   
        dynamicHtml += "<div class='form-group'><input type='text' class='form-control' maxlength='256' id='textbox_" + courseEvalQuestion.Id + "' name='textbox' value='' /></div>";

        dynamicHtml += "</div>";
        //$('#question').html("<ul id='multi'>" + dynamicHtml + "</ul>");
        return dynamicHtml;
        //alert(dynamicHtml);
    }
    this.ShowCourseEvaluationQuestionsTEXT = function(courseEvalQuestion, questionNo) {
        var dynamicHtml = "";
        var checked = "";
        var isRequiredHtml = "";


        if (courseEvalQuestion.Required == true) {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_TEXT_Required'>";
            isRequiredHtml = "<font color='red'>*</font>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "_Required'>" + '<img style="float:left;display:none" class="img-responsive" height="14" width="14" alt="" src="assets/img/exclamation.gif" />' + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "&nbsp;" + isRequiredHtml + "</h3>";
        }
        else {
            dynamicHtml += "<div style='padding-left:20px;padding-top:20px' id='" + courseEvalQuestion.Id + "_TEXT'>";
            dynamicHtml += "<h3 title='" + convertJSString(courseEvalQuestion.Text) + "'>" + questionNo + ".&nbsp;" + courseEvalQuestion.Text + "</h3>";
        }

        if (courseEvalQuestion.UnlimitedTF == 1)
            dynamicHtml += "<div class='form-group'><textarea class='form-control' id=textbox_'" + courseEvalQuestion.Id + "' cols='60' rows='6'></textarea></div>";
        else
            dynamicHtml += "<div class='form-group'><input type='text' class='form-control' maxlength='256' id='textbox_" + courseEvalQuestion.Id + "' name='textbox' value='' /></div>";

        dynamicHtml += "</div>";
        return dynamicHtml;
    }

    this.RenderImageTargetQuestion = function(ImageURL, arrAnswers, arrStudentAnswers, templateType) {
        selectedHotSpot = null;
        isSkipping = true;
        var img = new Image();
        img.src = ImageURL;

        $('.hotspotanswer').unbind('click.namespace');

        var dynamicHtml = "<div id=\"ImageDiv\" style=\"width:" + img.width + "px;height:" + img.height + "px;\">";
        $('toggle-flag').removeClass('hide');
        if (arrStudentAnswers.length > 0) {            
            for (var i = 0; i < arrAnswers.length; i++) {
                if (arrStudentAnswers[0] == arrAnswers[i].AssessmentItemAnswerID) {
                    selectedHotSpot = arrAnswers[i].AssessmentItemAnswerID;
                    dynamicHtml += "<div class=\"HotSpot\" id=\"" + arrAnswers[i].AssessmentItemAnswerID + "\" style=\"left:" + arrAnswers[i].ImageTargetCoordinates[0].XPos + "px; top:" + arrAnswers[i].ImageTargetCoordinates[0].YPos + "px;width:" + arrAnswers[i].ImageTargetCoordinates[0].Width + "px;height:" + arrAnswers[i].ImageTargetCoordinates[0].Height + "px;\">";
                    dynamicHtml += "<div class=\"redBorder\" id=\"hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "\">";
                    dynamicHtml += "<a style=\"left:0px; top:0;\" href=\"javascript:void(0);\" class=\"hotspotanswer\"><img src='" + ImageURL + "' style=\"left:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].XPos + 3) + "px; top:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].YPos + 3) + "px;\"/> </a>";
                    dynamicHtml += "</div>";
                    dynamicHtml += "</div>";
                }
                else {
                    dynamicHtml += "<div class=\"HotSpot\" id=\"" + arrAnswers[i].AssessmentItemAnswerID + "\" style=\"left:" + arrAnswers[i].ImageTargetCoordinates[0].XPos + "px; top:" + arrAnswers[i].ImageTargetCoordinates[0].YPos + "px;width:" + arrAnswers[i].ImageTargetCoordinates[0].Width + "px;height:" + arrAnswers[i].ImageTargetCoordinates[0].Height + "px;\">";
                    dynamicHtml += "<div class=\"yellowBorder\" id=\"hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "\">";
                    dynamicHtml += "<a style=\"left:0px; top:0;\" href=\"javascript:void(0);\" class=\"hotspotanswer\"><img src='" + ImageURL + "' style=\"left:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].XPos + 3) + "px; top:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].YPos + 3) + "px;\"/> </a>";
                    dynamicHtml += "</div>";
                    dynamicHtml += "</div>";
                }

            }
        }
        else {
            for (var i = 0; i < arrAnswers.length; i++) {
                dynamicHtml += "<div class=\"HotSpot\" id=\"" + arrAnswers[i].AssessmentItemAnswerID + "\" style=\"left:" + arrAnswers[i].ImageTargetCoordinates[0].XPos + "px; top:" + arrAnswers[i].ImageTargetCoordinates[0].YPos + "px;width:" + arrAnswers[i].ImageTargetCoordinates[0].Width + "px;height:" + arrAnswers[i].ImageTargetCoordinates[0].Height + "px;\">";
                dynamicHtml += "<div class=\"yellowBorder\" id=\"hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "\">";
                dynamicHtml += "<a style=\"left:0px; top:0;\" href=\"javascript:void(0);\" class=\"hotspotanswer\"><img src='" + ImageURL + "' style=\"left:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].XPos + 3) + "px; top:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].YPos + 3) + "px;\"/> </a>";
                dynamicHtml += "</div>";
                dynamicHtml += "</div>";

            }
        }

        dynamicHtml += "<img src='" + ImageURL + "' class=\"imageClass\" / >";
        dynamicHtml += "</div>";

        //For assessmentitem template change

        if (templateType != "Text Only")
            $('#quiz_container').find('#assessmentImage').replaceWith(dynamicHtml);
        else
            $('#question').html(dynamicHtml);


        $('.hotspotanswer').bind('click.namespace', function() {

            imageHotSpotClick(this.parentNode.parentNode.id, arrAnswers);
            ShowHideNextQuestionButton();
        });



        /////////////////////////////////
        img.onload = function() {
            //if (templateType =="Visual Top")
            //{
            //$('#assessment-table').find('tr').eq(0).find('td').attr('height',img.height);
            jQuery().ready(function() {
                $('#ImageDiv').css("width", img.width);
                $('#ImageDiv').css("height", img.height);

            })
            //}

        };

        img.onerror = function() {
            //if (templateType =="Visual Top")
            //{
            //$('#assessment-table').find('tr').eq(0).find('td').attr('height',img.height);
            jQuery().ready(function() {
                $('#ImageDiv').css("height", 350);
                $('#ImageDiv').css("width", 350);
            })
            //}
        };
        /////////////////////////

    } // End ImageTargetQuestion

    this.RenderFillInTheBlanks = function(questionStem, arrAnswers, arrStudentAnswersText) {
        var dynamicHtml = questionStem;
        isSkipping = true;
        $('toggle-flag').removeClass('hide');
        if (arrStudentAnswersText.length > 0) {             
            for (var index = 0; index < arrStudentAnswersText.length; index++) {
                var value = "";
                value = arrStudentAnswersText[index];

                var dynamicHtml = dynamicHtml.replace("&lt;blank&gt;", "<input type='textbox' class='form-control' id='A1' value='" + value + "' >");
            }
            //  var dynamicHtml = questionStem.replace(/<blank>/g,"<input type='textbox' id='1' value='" + value + "' onclick='ShowHideNextQuestionButton();'>");

        }
        else {
            var dynamicHtml = questionStem.replace(/&lt;blank&gt;/g, "<input type='textbox' class='form-control' id='A1' >");
            // var dynamicHtml = questionStem.replace(/<blank>/g,"<input type='textbox' id='1' onclick='ShowHideNextQuestionButton();'>");
        }

        //alert(dynamicHtml);
        //LCMS-5770 - Start
        //$('#quiz_container').find('h3').html(dynamicHtml);
        //$('#quiz_container').find('questionStem').html(dynamicHtml);
        InsertQustionStem(dynamicHtml);
        //LCMS-5770 - End


        //LCMS-8188 - Start
        $('#quiz_container').find('#questionStem').find('input[type=textbox]').bind('click.namespace', function() {           
            ShowHideNextQuestionButton();
        });
        //LCMS-8188 - End
        $('#question').empty();

    } // End Render Fill In The Blanks


    this.RenderOrdering = function(arrAnswers, arrStudentAnswers) {
        var dynamicHtml = null;
        isSkipping = true;
        $('toggle-flag').removeClass('hide');
        if (arrStudentAnswers.length > 0) {            
            dynamicHtml = "<div class='scene-content scene-ordering'>";            
            for (var x = 0; x < arrAnswers.length; x++) {
                var studentOrder = arrStudentAnswers[x];
                for (var i = 0; i < arrAnswers.length; i++) {
                    //alert("x = " + i + " Display Order= " + arrAnswers[i].DisplayOrder);
                    if (arrAnswers[i].AssessmentItemAnswerID == studentOrder) {
                        dynamicHtml += '<div class="scene-option"><div class="ordering-btn" data-value="' + arrAnswers[i].AssessmentItemAnswerID + '"><a href="javascript:;" title="Move Up" class="up"><i class="glyphicon glyphicon-arrow-up"></i></a><a href="javascript:;" title="Move Down" class="down"><i class="glyphicon glyphicon-arrow-down"></i></a></div><span>'+ arrAnswers[i].Label +'</span></div>';
                        //dynamicHtml += "<option value='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Label + "</option>";
                        //alert(dynamicHtml);
                    }
                }
            }

            dynamicHtml += "</div>";
            //dynamicHtml += "<div id='orderbuttons'><p><button disabled='disabled' class='button' id='btnA'>▲</button></p>";
            //dynamicHtml += "<p><button class='button' disabled='disabled' id='btnB'>▼</button></p></div>";
        }
        else {
            dynamicHtml = "<div class='scene-content scene-ordering'>";

            for (var i = 0; i < arrAnswers.length; i++) {
                dynamicHtml += '<div class="scene-option"><div class="ordering-btn" data-value="' + arrAnswers[i].AssessmentItemAnswerID + '"><a href="javascript:;" title="Move Up" class="up"><i class="glyphicon glyphicon-arrow-up"></i></a><a href="javascript:;" title="Move Down" class="down"><i class="glyphicon glyphicon-arrow-down"></i></a></div><span>'+ arrAnswers[i].Label +'</span></div>';
                //dynamicHtml += "<option value='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Label + "</option>";
            }

            dynamicHtml += "</div>";
            //dynamicHtml += "<div id='orderbuttons'><p><button class='button' id='btnA' disabled='disabled'>▲</button></p>";
            //dynamicHtml += "<p><button class='button' id='btnB' disabled='disabled'>▼</button></p></div>";

        }

        //alert(dynamicHtml);	    
        //LCMS-8188 - Start
        //$('#question').html(dynamicHtml);
        var questionCont = $('#question');
        questionCont.html(dynamicHtml);
        
        $('.ordering-btn > .up').click(function(){
		    var parent = $(this).parent().parent();
		    parent.insertBefore(parent.prev());
		    ShowHideNextQuestionButton();
	    });
	    $('.ordering-btn > .down').click(function(){
		    var parent = $(this).parent().parent();
		    $(this).parent().parent().slideDown();
		    parent.insertAfter(parent.next());
		    ShowHideNextQuestionButton();
	    });        
        
//        questionCont.find("#btnA").bind('click.namespace', function() {
//            ListElementsMoveUp();
//            ShowHideNextQuestionButton();
//            return false;
//        });
//        questionCont.find("#btnB").bind('click.namespace', function() {
//            ListElementsMoveDown();
//            ShowHideNextQuestionButton();            
//            return false;
//        });
        //LCMS-8188 - End

    } // End Render Ordering
    
    
//    this.RenderOrdering = function(arrAnswers, arrStudentAnswers) {
//        var dynamicHtml = null;
//        isSkipping = true;

//        if (arrStudentAnswers.length > 0) {
//            dynamicHtml = "<div id='orderselect'><select onchange='javascript:HandleOrderingUpDownButtonsState();' name='ListBox' id='ListBox' size='5'>";

//            for (var x = 0; x < arrAnswers.length; x++) {
//                var studentOrder = arrStudentAnswers[x];
//                for (var i = 0; i < arrAnswers.length; i++) {
//                    //alert("x = " + i + " Display Order= " + arrAnswers[i].DisplayOrder);
//                    if (arrAnswers[i].AssessmentItemAnswerID == studentOrder) {
//                        dynamicHtml += "<option value='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Label + "</option>";
//                        //alert(dynamicHtml);
//                    }
//                }
//            }

//            dynamicHtml += "</select></div>";
//            dynamicHtml += "<div id='orderbuttons'><p><button disabled='disabled' class='button' id='btnA'>▲</button></p>";
//            dynamicHtml += "<p><button class='button' disabled='disabled' id='btnB'>▼</button></p></div>";
//        }
//        else {
//            dynamicHtml = "<div id='orderselect'><select onchange='javascript:HandleOrderingUpDownButtonsState();' name='ListBox' multiple id='ListBox' size='5'>";

//            for (var i = 0; i < arrAnswers.length; i++) {
//                dynamicHtml += "<option value='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Label + "</option>";
//            }

//            dynamicHtml += "</select></div>";
//            dynamicHtml += "<div id='orderbuttons'><p><button class='button' id='btnA' disabled='disabled'>▲</button></p>";
//            dynamicHtml += "<p><button class='button' id='btnB' disabled='disabled'>▼</button></p></div>";

//        }

//        //alert(dynamicHtml);	    
//        //LCMS-8188 - Start
//        //$('#question').html(dynamicHtml);
//        var questionCont = $('#question');
//        questionCont.html(dynamicHtml);
//        questionCont.find("#btnA").bind('click.namespace', function() {
//            ListElementsMoveUp();
//            ShowHideNextQuestionButton();
//            return false;
//        });
//        questionCont.find("#btnB").bind('click.namespace', function() {
//            ListElementsMoveDown();
//            ShowHideNextQuestionButton();            
//            return false;
//        });
//        //LCMS-8188 - End

//    } // End Render Ordering    

//    this.RenderMatching = function(arrAnswers, arrStudentAnswers, arrStudentAnswersText) {
//        // alert("arrStudentAnswers.length="+arrStudentAnswers.length+" :arrAnswers.length "+arrAnswers.length+" :arrAnswers.length "+arrAnswers.length);
//        isSkipping = true;
//        if (arrStudentAnswers.length > 0) {

//            //alert("in if");
//            //alert(arrAnswers+" : "+arrStudentAnswers+" : "+arrStudentAnswersText);
//            $('.drag').unbind('dragstart.namespace');
//            $('.drag').unbind('drag.namespace');
//            $('.drag').unbind('dragend.namespace');

//            $('.drop').unbind('dropstart.namespace');
//            $('.drop').unbind('drop.namespace');
//            $('.drop').unbind('dropend.namespace');

//            $('#question').empty();

//            // omair code
//            var dynamicHtml = "<div id=\"matching\"><div id=\"dropContainer\">";
//            for (var i = 0; i < arrAnswers.length; i++) {
//                dynamicHtml += "<div class='drop' title='" + arrAnswers[i].LeftItemText + "' id='drop-" + i + "'>";
//                dynamicHtml += "<span id='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].LeftItemText + "</span>";

//                if (arrStudentAnswersText.length > 0) { // LCMS - 12416  Abdus Samad
//                    
//                    if (arrStudentAnswersText[i] != "null") {

//                        dynamicHtml += "<div class=\"drag\"><span>" + arrStudentAnswersText[i] + "</span></div>";

//                    }
//                }
//                dynamicHtml += "</div>";
//            }

//            dynamicHtml += "</div><div id=\"dragContainer\">";

//            arrAnswers.sort(function() { return 0.7 - Math.random() });
//            for (var x = 0; x < arrAnswers.length; x++) {
//                if (arrStudentAnswersText[x] == "null") {
//                    dynamicHtml += "<div class=\"drag\" title='" + arrAnswers[x].RightItemText + "' id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
//                    dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
//                    dynamicHtml += "</div>";
//                }
//                //LCMS - 12416
//                //Abdus Samad
//                //Start
//                if (arrStudentAnswersText[x] == undefined) {
//                    dynamicHtml += "<div class=\"drag\" title='" + arrAnswers[x].RightItemText + "' id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
//                    dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
//                    dynamicHtml += "</div>";
//                }
//                //Stop

//            }

//            dynamicHtml += "</div></div>";

//            $('#question').append(dynamicHtml);

//            $('.drag').bind('dragstart.namespace', function(event) {

//                var $drag = $(this);
//                var $proxy = $drag.clone();
//                $drag.addClass("outline");
//                $.dropManage();
//                return $proxy.appendTo(document.body).addClass("ghost");

//            });


//            $('.drag').bind('drag.namespace', function(event) {
//                $(event.dragProxy).css({ left: event.offsetX, top: event.offsetY });
//            });

//            $('.drag').bind('dragend.namespace', function(event) {
//                $(event.dragProxy).fadeOut("normal", function() { $(this).remove(); });
//                if (!event.dropTarget && $(this).parent().is(".drop")) {
//                    //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                    $('#dragContainer').append(this);
//                }
//                $(this).removeClass("outline");
//                ShowHideNextQuestionButton();
//                $(".ghost").each(function(i) {
//                    $(this).hide();
//                    // alert('if');
//                });
//            });
//            /* binding functions to the drag div end */
//            /* binding functions to the droptarget div */
//            $('.drop').bind('dropstart.namespace', function(event) {
//                if (this == event.dragTarget.parentNode) return false;
//                $(this).addClass("active");
//            });

//            $('.drop').bind('drop.namespace', function(event) {

//                $(this).find('div').each(function(i) {

//                    $('#' + this.id).remove();
//                    $('#dragContainer').append(this);
//                    $('#dragContainer').findoverride(this).bind('dragstart.namespace', function(event) {
//                        var $drag = $(this);
//                        var $proxy = $drag.clone();
//                        $drag.addClass("outline");
//                        return $proxy.appendTo(document.body).addClass("ghost");
//                    });

//                    $('#dragContainer').findoverride(this).bind('drag.namespace', function(event) {
//                        $(event.dragProxy).css({ left: event.offsetX, top: event.offsetY });
//                    });

//                    $('#dragContainer').findoverride(this).bind('dragend.namespace', function(event) {
//                        $(event.dragProxy).fadeOut("normal", function() { $(this).remove(); });
//                        if (!event.dropTarget && $(this).parent().is(".drop")) {
//                            //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                            $('#dragContainer').append(this);

//                        }
//                        $(this).removeClass("outline");

//                        ShowHideNextQuestionButton();

//                    });



//                });






//                /*
//                $(this).find('div').each(function (i)
//                {
//                $('#'+this.id).remove();
//                $('#dragContainer').append(this);
//                                
//                // Irfan Start 
//                                    
//                $('.drag').bind('dragstart.namespace', function(event){ 
//                                    		
//                var $drag = $( this );
//                var $proxy = $drag.clone();
//                $drag.addClass("outline");
//                return $proxy.appendTo( document.body ).addClass("ghost");
//                                    			
//                });
//                                    		
//                $('.drag').bind('drag.namespace', function(event){ 
//                //alert(event.offsetX + "\r\n" + event.offsetY);
//                $( event.dragProxy ).css({left: event.offsetX, top: event.offsetY});		
//			                                    
//                });
//                                    		
//                $('.drag').bind('dragend.namespace', function(event){ 
//                $( event.dragProxy ).fadeOut( "normal", function(){$( this ).remove();});
//                if ( !event.dropTarget && $(this).parent().is(".drop") ){
//                //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                $('#dragContainer').append( this );
//                        							
//                }
//                $( this ).removeClass("outline");
//			                                    			                                    
//                ShowHideNextQuestionButton();
//                });
//		                                    
//                // Irfan End
//                });
//    	                
//    	                
//                       
//    	                
//                */



//                $(this).append(event.dragTarget);


//                $(".ghost").each(function(i) {
//                    $(this).hide();
//                    // alert('if');
//                });
//            });

//            $('.drop').bind('dropend.namespace', function(event) {


//                $(this).removeClass("active");

//            });
//            /* binding functions to the droptarget div end */

//        }
//        else {
//            //alert("in else");
//            /* unbinding functions to the drag and drop div */
//            $('.drag').unbind('dragstart.namespace');
//            $('.drag').unbind('drag.namespace');
//            $('.drag').unbind('dragend.namespace');

//            $('.drop').unbind('dropstart.namespace');
//            $('.drop').unbind('drop.namespace');
//            $('.drop').unbind('dropend.namespace');

//            $('#question').empty();

//            var dynamicHtml = "<div id=\"matching\"><div id=\"dropContainer\"  >";
//            for (var x = 0; x < arrAnswers.length; x++) {
//                //dynamicHtml += "<div class=\"drop\" title=\"Div 1\" id='drop-" + x + "'>";
//                dynamicHtml += "<div class=\"drop\" title='" + arrAnswers[x].LeftItemText + "' id='drop-" + x + "'>";
//                dynamicHtml += "<span id='" + arrAnswers[x].AssessmentItemAnswerID + "'>" + arrAnswers[x].LeftItemText + "</span>";
//                dynamicHtml += "</div>";
//            }

//            dynamicHtml += "</div><div id=\"dragContainer\">";

//            arrAnswers.sort(function() { return 0.7 - Math.random() });
//            for (var i = 0; i < arrAnswers.length; i++) {
//                //dynamicHtml += "<div class=\"drag\" title=\"Target A\" id='" + arrAnswers[i].AssessmentItemAnswerID + "'>";
//                dynamicHtml += "<div class=\"drag\" title='" + arrAnswers[i].RightItemText + "' id='drag-" + i + "'>";
//                dynamicHtml += "<span>" + arrAnswers[i].RightItemText + "</span>";
//                dynamicHtml += "</div>";
//            }
//            dynamicHtml += "</div></div>";

//            $('#question').append(dynamicHtml);


//            /* binding functions to the drag div */

//            $('.drag').bind('dragstart.namespace', function(event) {

//                var $drag = $(this), $proxy = $drag.clone();
//                $drag.addClass("outline");
//                $.dropManage();
//                return $proxy.appendTo(document.body).addClass("ghost");

//            });


//            $('.drag').bind('drag.namespace', function(event) {
//                //alert(event.offsetX + "\r\n" + event.offsetY);
//                $(event.dragProxy).css({ left: event.offsetX, top: event.offsetY });
//            });

//            $('.drag').bind('dragend.namespace', function(event) {
//                //alert(event.offsetX + "\r\n" + event.offsetY);
//                $(event.dragProxy).fadeOut("normal", function() { $(this).remove(); });
//                if (!event.dropTarget && $(this).parent().is(".drop")) {
//                    //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                    $('#dragContainer').append(this);

//                }
//                $(this).removeClass("outline");
//                ShowHideNextQuestionButton();
//                //alert('d');
//            });

//            /* binding functions to the drag div end */
//            /* binding functions to the droptarget div */
//            $('.drop').bind('dropstart.namespace', function(event) {
//                if (this == event.dragTarget.parentNode) return false;
//                $(this).addClass("active");
//            });

//            $('.drop').bind('drop.namespace', function(event) {






//                $(this).find('div').each(function(i) {

//                    $('#' + this.id).remove();
//                    $('#dragContainer').append(this);
//                    $('#dragContainer').findoverride(this).bind('dragstart.namespace', function(event) {
//                        var $drag = $(this);
//                        var $proxy = $drag.clone();
//                        $drag.addClass("outline");
//                        return $proxy.appendTo(document.body).addClass("ghost");
//                    });

//                    $('#dragContainer').findoverride(this).bind('drag.namespace', function(event) {
//                        $(event.dragProxy).css({ left: event.offsetX, top: event.offsetY });
//                    });

//                    $('#dragContainer').findoverride(this).bind('dragend.namespace', function(event) {
//                        $(event.dragProxy).fadeOut("normal", function() { $(this).remove(); });
//                        if (!event.dropTarget && $(this).parent().is(".drop")) {
//                            //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                            $('#dragContainer').append(this);

//                        }
//                        $(this).removeClass("outline");

//                        ShowHideNextQuestionButton();

//                    });



//                });






//                /*
//                $(this).find('div').each(function (i)
//                {
//                $('#'+this.id).remove();
//                $('#dragContainer').append(this);
//                                
//                // Irfan Start 
//                                    
//                $('.drag').bind('dragstart.namespace', function(event){ 
//                                    		
//                var $drag = $( this );
//                var $proxy = $drag.clone();
//                $drag.addClass("outline");
//                return $proxy.appendTo( document.body ).addClass("ghost");
//                                    			
//                });
//                                    		
//                $('.drag').bind('drag.namespace', function(event){ 
//                //alert(event.offsetX + "\r\n" + event.offsetY);
//                $( event.dragProxy ).css({left: event.offsetX, top: event.offsetY});		
//			                                    
//                });
//                                    		
//                $('.drag').bind('dragend.namespace', function(event){ 
//                $( event.dragProxy ).fadeOut( "normal", function(){$( this ).remove();});
//                if ( !event.dropTarget && $(this).parent().is(".drop") ){
//                //$('#log').append('<div>Removed <b>'+ this.id +'</b> from <span>'+ this.parentNode.id +'</span></div>');
//                $('#dragContainer').append( this );
//                        							
//                }
//                $( this ).removeClass("outline");
//			                                    			                                    
//                ShowHideNextQuestionButton();
//                });
//		                                    
//                // Irfan End
//                });
//    	                
//    	                
//                $(".ghost").each(function (i)
//                {
//                $(this).hide();
//                });
//    	                
//                */


//                $(this).append(event.dragTarget);

//                $(".ghost").each(function(i) {

//                    $(this).hide();
//                    // alert('else');
//                });

//            });

//            $('.drop').bind('dropend.namespace', function(event) {
//                //alert("drop end");
//                $(this).removeClass("active");

//            });
//            /* binding functions to the droptarget div end */

//        } // End else 


//    }   // End Render matching

    this.RenderMatching = function(arrAnswers, arrStudentAnswers, arrStudentAnswersText) {
        // alert("arrStudentAnswers.length="+arrStudentAnswers.length+" :arrAnswers.length "+arrAnswers.length+" :arrAnswers.length "+arrAnswers.length);
        isSkipping = true;
        $('toggle-flag').removeClass('hide');
        var checkAnswer={};
        var arrayAnswers2={};
        
        for (var i=0; i<arrAnswers.length; i++)
        {
            arrayAnswers2[arrAnswers[i].AssessmentItemAnswerID]=arrAnswers[i].RightItemText;
        }
        
        //console.log(arrayAnswers2);
        if (arrStudentAnswers.length > 0)
		{	
			$('#question').empty();
			var dynamicHtml = "<div class='scene-cell' id='dropable-holders'>";
			
            for (var i=0; i<arrAnswers.length; i++)
			{
                dynamicHtml += "<div class='cd-drop-holder' title='" + arrAnswers[i].LeftItemText + "' data-trg='drag-" + i + "'>";
                dynamicHtml += 	"<span id='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].LeftItemText + "</span>";                                
                
                if (arrStudentAnswersText.length > 0)
				{
                    if (typeof arrStudentAnswersText[i] != "undefined")
					{
					    checkAnswer[arrAnswers[i].AssessmentItemAnswerID]=arrAnswers[i].RightItemText;
					    var catchid;
					    $.each(arrayAnswers2, function(k,e)
					    {
					        if(e==arrStudentAnswersText[i])
					        {
					            catchid=k;
					        }					        
					    });
                        dynamicHtml += "<div class='cd-drag-btn' title='"+  arrStudentAnswersText[i] +"' id='drag-" + catchid + "'><span>" + arrStudentAnswersText[i] + "</span></div>";
                    }
                }
                dynamicHtml += "</div>";
            }

            dynamicHtml += 	'</div>'+
							'<div class="scene-cell" id="draggable-btns">'+
								'<div class="scene-content">';
				
			console.log(checkAnswer + ' ' + 'CHECK ANSWER');	
			console.log(arrStudentAnswers + ' ' + 'STUDENT ARRAY');	
			console.log(arrStudentAnswersText + ' ' + 'STUDENT ANSWER TEXT');	
			
            arrAnswers.sort(function(){return 0.7-Math.random()});
            for (var x=0; x<arrAnswers.length; x++)
			{   
                //if (typeof arrStudentAnswersText[x] == "undefined")
				//{
				    //console.log(checkAnswer[arrAnswers[x].AssessmentItemAnswerID]);
				    //console.log(arrAnswers[x].AssessmentItemAnswerID + ' ' + arrAnswers[x].RightItemText);
				    if(!checkAnswer[arrAnswers[x].AssessmentItemAnswerID])
				    {
                        dynamicHtml += "<div class='cd-drag-btn' title='" + arrAnswers[x].RightItemText + "' id='drag-" + arrAnswers[x].AssessmentItemAnswerID + "'>";
                        dynamicHtml += 	"<span>" + arrAnswers[x].RightItemText + "</span>";
                        dynamicHtml += "</div>";
                    }
                //}
//                if (arrStudentAnswersText[x] == undefined)
//				{
//                    dynamicHtml += "<div class='cd-drag-btn' title='" + arrAnswers[x].RightItemText + "' id='drag-" + arrAnswers[x].AssessmentItemAnswerID + "'>";
//                    dynamicHtml += 	"<span>" + arrAnswers[x].RightItemText + "</span>";
//                    dynamicHtml += "</div>";
//                }
            }
            dynamicHtml += "</div></div>";
            $('#question').append(dynamicHtml);
			
			$('.cd-drag-btn').draggable({
				revert: false,
				start: function(event, ui) {
					ui.helper.data('dropped', false);
				},
				stop: function(event, ui)
				{
					if(!ui.helper.data('dropped'))
					{
						$(this).css({"top":0,"left":0,"right":0,"bottom":0}).removeClass('hide');
						$("#draggable-btns > .scene-content").append($(this));
					}
					ShowHideNextQuestionButton();
					$(".cd-drop-holder").removeClass("highlight");
				}
			});
			$('.cd-drop-holder').droppable({
				accept: '.cd-drag-btn',
				over: function(event,ui){
					$(this).addClass("highlight");
				},
				out: function(event,ui){
					$(this).removeClass("highlight");
				},
				drop: function(event,ui){
					
					ui.draggable.data('dropped', true);
					if(ui.draggable.context.id == $(this).attr("data-trg"))
					{
						//console.log('correct');
					}
					else
					{
						//console.log('incorrect');
					}
					
					var $existDragItem = $(this).find('.cd-drag-btn');
					if(typeof $existDragItem.html() != "undefined")
					{
						$("#draggable-btns > .scene-content").append($existDragItem);
					}
					$(this).append($("#"+ui.draggable.context.id).css({"top":0,"left":0,"right":0,"bottom":0}));
				}
			});
        }
        else
		{
			/* New UI by Haris Mairaj
				<div class="scene-cell" id="dropable-holders">
					<div class="scene-content">
						<div class="cd-drop-holder" title="Placeholder" data-trg="drag-0"><span>A Based on the course, you may be interested in some of the following:</span></div>
						<div class="cd-drop-holder" title="Placeholder" data-trg="drag-1"><span>B Based on the course, you may be interested in some of the following: Based on the course, you may be interested in some of the following:</span></div>
						<div class="cd-drop-holder" title="Placeholder" data-trg="drag-2"><span>C Based on the course, you may be interested in some of the following:</span></div>
					</div>
				</div>
				<div class="scene-cell" id="draggable-btns">
					<div class="scene-content">
						<div class="cd-drag-btn" title="Draggable item" id="drag-0"><span>A Based on the course, you may be interested in some of the following:</span></div>
						<div class="cd-drag-btn" title="Draggable item" id="drag-1"><span>B Based on the course, you may be interested in some of the following: Based on the course, you may be interested in some of the following:</span></div>
						<div class="cd-drag-btn" title="Draggable item" id="drag-2"><span>C Based on the course, you may be interested in some of the following:</span></div>
					</div>
				</div>*/
			
			$('#question').empty();
			var dynamicHtml = "<div class='scene-cell' id='dropable-holders'>";
            for (var x = 0; x < arrAnswers.length; x++)
			{
				dynamicHtml += "<div class='cd-drop-holder' title='" + arrAnswers[x].LeftItemText + "' data-trg='drag-" + x + "'>";
				dynamicHtml += 	"<span id='" + arrAnswers[x].AssessmentItemAnswerID + "'>" + arrAnswers[x].LeftItemText + "</span>";
                dynamicHtml += "</div>";
            }
			dynamicHtml += 	'</div>'+
							'<div class="scene-cell" id="draggable-btns">'+
								'<div class="scene-content">';
			
            arrAnswers.sort(function(){return 0.7-Math.random()});
            for (var i = 0; i < arrAnswers.length; i++)
			{
                dynamicHtml += "<div class='cd-drag-btn' title='" + arrAnswers[i].RightItemText + "' id='drag-" + i + "'>";
                dynamicHtml += 	"<span>" + arrAnswers[i].RightItemText + "</span>";
                dynamicHtml += "</div>";
            }
			dynamicHtml += "</div></div>";
            $('#question').append(dynamicHtml);
			
			$('.cd-drag-btn').draggable({
				revert: false,
				start: function(event, ui) {
					ui.helper.data('dropped', false);
				},
				stop: function(event, ui)
				{
					if(!ui.helper.data('dropped'))
					{
						$(this).css({"top":0,"left":0,"right":0,"bottom":0}).removeClass('hide');
						$("#draggable-btns > .scene-content").append($(this));
					}
					ShowHideNextQuestionButton();
					$(".cd-drop-holder").removeClass("highlight");
				}
			});
			$('.cd-drop-holder').droppable({
				accept: '.cd-drag-btn',
				over: function(event,ui){
					$(this).addClass("highlight");
				},
				out: function(event,ui){
					$(this).removeClass("highlight");
				},
				drop: function(event,ui){
					
					ui.draggable.data('dropped', true);
					if(ui.draggable.context.id == $(this).attr("data-trg"))
					{
						//console.log('correct');
					}
					else
					{
						//console.log('incorrect');
					}
					
					var $existDragItem = $(this).find('.cd-drag-btn');
					if(typeof $existDragItem.html() != "undefined")
					{
						$("#draggable-btns > .scene-content").append($existDragItem);
					}
					$(this).append($("#"+ui.draggable.context.id).css({"top":0,"left":0,"right":0,"bottom":0}));
				}
			});
        } // End else 


    }   // End Render matching



    this.ShowQuestionResultRendering = function(objQuestionResult) {
        $(quiz_container).show();
        $('.drag').unbind('dragstart.namespace');
        $('.drag').unbind('drag.namespace');
        $('.drag').unbind('dragend.namespace');
        $('.drop').unbind('dropstart.namespace');
        $('.drop').unbind('drop.namespace');
        $('.drop').unbind('dropend.namespace');
        //Abdus Samad 
        //LCMS-10142 Start
        //assessmentTimerObj.PauseTimer();
        //LCMS-10142 Stop

        var feedbackDescription = objQuestionResult.QuestionResult.QuestionFeedBack;

        $(gradeAssessment).hide();
        $('#questiondescription').empty();
        $('#questiondescription').hide();

        if (objQuestionResult.QuestionResult.IsCorrectlyAnswered == true) {
            $('#correct').css('backgroundImage', "url('" + ImageCorrect + "')");
            $('#correct').show();
            if (feedbackDescription != "") {
                            
                $('#questiondescription').html("<strong>" + FeedbackICP4 + ":</strong> <br />" + replaceSpacesAndLineBreaks(feedbackDescription));  // Modified by Mustafa On Sep 2nd 2009 //Yasin LCMS-12913      
                               
                $('#questiondescription').show();
            }
        }
        else if (objQuestionResult.QuestionResult.IsCorrectlyAnswered == false) {
            $('#incorrect').css("backgroundImage", "url('" + ImageIncorrect + "')");
            $('#incorrect').show();
            if (feedbackDescription != "") {            
            
                $('#questiondescription').html("<strong>" + FeedbackICP4 + ":</strong> <br />" + replaceSpacesAndLineBreaks(feedbackDescription)); // Modified by Mustafa On Sep 2nd 2009 //Yasin LCMS-12913

                $('#questiondescription').show();
                //Abdus Samad LCMS-13470
                //Start
                document.getElementById('questiondescription').focus(); //508 Complainance
                //Stop
            }
        }
      
        $(NextQuestionButtonEn).unbind('click.namespace');
        $(NextQuestionButtonEn).bind('click.namespace', function() {
            //$(NextQuestionButtonEn).unbind('click.namespace');   

            GetNextQuestionAfterFeedback();
            //Abdus Samad 
            //LCMS-10142 Start      
            // assessmentTimerObj.ResumeTimer();   
            //LCMS-10142 Stop              
            if (objQuestionResult.QuestionResult.EndAssessment == true) {
                $('#quiz_container').hide();
                $('#assesmentcontainer').hide();
                $('#AnswerReviewContainer').hide();
            }
        }); // End NextQuestionButtonEn Handler

    }


    this.ShowProctorMessage = function(objectData) {
        //alert('ShowProctorMessage');
        var content = objectData.ProctorMessage.ProctorMessageText;
        var imageUrl = objectData.ProctorMessage.ProctorMessageImageUrl;
        var heading = objectData.ProctorMessage.ProctorMessageHeading;
        allowSkipping = objectData.ProctorMessage.AllowSkipping;
        var timer = objectData.ProctorMessage.AssessmentTimer;
        showGradeAssessment = objectData.ProctorMessage.ShowGradeAssessment;
        isLockoutClickAwayToActiveWindowStart = objectData.ProctorMessage.LockoutClickAwayToActiveWindow;

        //courseTimerObj.StopTimer();
        //alert("In Show Proctor");
        $(htmlContentContainer).hide();
        $(assessmentControlPanel).hide();
        $("#quiz_container").hide();
        $("#QuestionRemediationContainer").hide();
        $("#assesmentcontainer").hide();
        $("#AnswerReviewContainer").hide();
        $("#IndividualScoreContainer").hide();
        $("#assessment_result_container").hide();
        $("#CARealStateValidation").hide();
        $("#NYInsuranceValidation").hide();
        $("#assessmentItemResult").hide();
        

        // Ticket ID: LCMS-10358
        // Description: Navigation buttons bar is hidden 
        // in the scene to start a proctored exam. 
        // Learners can not go back and review the content.
        // Resolution: The below line of code requires to 
        // comment out as it hide the control
        // panel in case of Proctor
        //_________________________________________________
        //$("#controlPanel").hide(); 

        // After enabling the course,
        // Back and Play buttons will also
        // be visible. They should be disabled
        // in case of Proctor Exam 
        $(BackbuttonEn).hide();
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();
        $(BackbuttonDs).show();
        //_________________________________________________

        $("#assessmentItemResult").hide();
        $("#controlPanel").hide();
        $("#cd-tour-trigger").hide();
        $(gradeAssessment).hide();


        //   $(BackbuttonEn).hide();
        //	   $(PlaybuttonEn).hide();
        //	   $(PlaybuttonDs).show();
        //	   $(BackbuttonDs).show();
        //	   $(IcoBookMark).hide();
        //	   $(IcoBookMarkDs).show();

        if ($('#controlPanel').is(':hidden') == true) {
            //$('#odometerContainter').css('background', 'url()');
        }


        $("#ShowProctorMessageHeading").find("h1").empty();
        $("#ShowProctorMessageHeading").find("h1").html(heading);

        $("#ShowProctorMessageContent").find("p").empty();
        $("#ShowProctorMessageContent").find("p").html(content);

        //Fix for LCMS-10546
        //Reset the buttons to initial state before executing further  
        $('#ShowProctorMessageButtons').find("div").eq(0).empty();
        $('#ShowProctorMessageButtons').find("div").eq(0).html('<div class="floatright"><button class="cd-btn main-action button">Begin Assessment</button></div>');
        //End Fix for LCMS-10546

        $('#ShowProctorMessageButtons').find("div").eq(0).find("button").html(objectData.ProctorMessage.ProctorMessageButtonText);

        $("#ShowProctorMessageIcon").css("backgroundImage", "url('" + imageUrl + "')");

        $("#ShowProctorMessageContainer").show();
        $("#proctor_login_screen").hide();

        if (objectData.ProctorMessage.IsRestrictiveAssessmentEngine) {
            /*document.body.oncontextmenu =  new Function("return false");
            document.body.onselectstart = new Function("return false");
            document.body.ondragstart  = new Function("return false");
            document.body.style.MozUserSelect="none"
            var secureDiv = document.getElementById('security');
            secureDiv.style.display = "";*/
            document.body.oncontextmenu = new Function("return false");
            document.body.onselectstart = new Function("return false");
            document.body.ondragstart = new Function("return false");
            document.body.style.MozUserSelect = "none"
            var secHTML = AC_FL_RunContentSWF('codebase', 'https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0', 'width', '5px', 'height', '5px', 'align', 'middle', 'allowScriptAccess', 'always', 'allowFullScreen', false, 'movie', 'securityAPI', 'quality', 'high', 'id', 'securityAPI', 'bgcolor', '#EAEAEA');
            $('#security').html(secHTML);
            $('#security').show();
        } else {
            /*document.body.oncontextmenu =  new Function("return true");
            document.body.onselectstart = new Function("return true");
            document.body.ondragstart  = new Function("return true");
            document.body.style.MozUserSelect = '';
            var secureDiv = document.getElementById('security');
            secureDiv.style.display = "none";*/
            document.body.oncontextmenu = new Function("return true");
            document.body.onselectstart = new Function("return true");
            document.body.ondragstart = new Function("return true");
            document.body.style.MozUserSelect = '';
            //var secureDiv = document.getElementById('security');
            //secureDiv.style.display = "none";			
            $('#security').empty();
            $('#security').hide();
        }




        // Ticket ID: LCMS-9834
        // Description: LCMS - Pause Resume Functionality 
        // not working with "Enable Proctored Assessment 
        // Enforced by Affidavit"
        //_________________________________________________

        // If pause/resume enable
        if (!objectData.ProctorMessage.IsConfigurationModified && objectData.ProctorMessage.IsAssessmentResumeMessage) {
            // Change button text from 'Begin Assessment'
            // to 'Resume Assessment from Previous Session'
            $('#ShowProctorMessageButtons').find("div").eq(0).find("button").eq(0).text("Resume Assessment from Previous Session");
        } else if (objectData.ProctorMessage.IsConfigurationModified) {
            // Change button text from 'Begin Assessment'
            // to 'Continue'
            $('#ShowProctorMessageButtons').find("div").eq(0).find("button").eq(0).text("Continue");
        }

        $('#ShowProctorMessageButtons').find("div").eq(0).find("button").bind('click.namespace', function() {
            $('#ShowProctorMessageButtons').find("div").eq(0).find("button").unbind('click.namespace');

            //$(overlay).css("display", "none");
            $(dialog).css("display", "none");

            if (objectData.ProctorMessage.AssessmentTimer != -1) {
                $(assessmentTimer).show();
                assessmentTimerObj.TimerContainer(assessmentTimer);
                assessmentTimerObj.InitializeTimer(objectData.ProctorMessage.AssessmentTimer);
            } else {
                $(assessmentTimer).hide();


            }

            // If resumable
            if (objectData.ProctorMessage.IsAssessmentResumeMessage) {

                // In case of resumable assessment
                // the button text will be changed to
                // 'Resume Assessment from Previous Session'
                // therefore the operation will be resume
                // assessment instead of begin assessment
                resumeAssessmentMessageClick();
                resetCPIdleTimer();

            } else {

                // Otherwise, the course is non-resumable
                // or user hasn't started assessment yet then it
                // will only be showing 'Begin Assessment' button
                // and its operation will be start the assessment
                startAssessmentMessageClick();
                resetCPIdleTimer();

            }

            // Hide Proctor container (div) to visible
            // questionnaire
            $("#ShowProctorMessageContainer").hide();
            // alert('Hello');
            //$("#NYInsuranceValidation").hide(); // temporarily added by yasin
            return false;
        });

        // If pause/resume enable
        if (objectData.ProctorMessage.IsAssessmentResumeMessage) {

            // As this is a resumable assessment
            // and user can 'Start Assessment Over'
            // We need to add a button here for that
            var startOverButton = $("<button class='button'>Start Assessment Over</button>");

            // Define operation for 'Start Assessment Over'
            startOverButton.click(function() {

                if (objectData.ProctorMessage.AssessmentTimer != -1) {

                    $(assessmentTimer).show();
                    assessmentTimerObj.TimerContainer(assessmentTimer);
                    assessmentTimerObj.InitializeTimer(objectData.ProctorMessage.AssessmentTimer);

                } else {

                    $(assessmentTimer).hide();

                }

                $("#ShowProctorMessageContainer").hide();
                startAssessmentMessageClick();
                resetCPIdleTimer();

                return false;


            });

            // Add 'Start Assessment Over' button after 'Resume Assessment from Previous Session'
            $('#ShowProctorMessageButtons').find("div").eq(0).find("button").after(startOverButton);

            // Aligned buttons similar to non-proctor exams
            $('#ShowProctorMessageButtons').find("div").eq(0).find("button").eq(0).after("   ");
            $('#ShowProctorMessageButtons').find("div").removeClass("floatright");

        }

        // p.sceneTextArea shows text in bold 
        // and in bigger font which needs to be
        // corrected to normal and uniform 
        // font size
        $("#ShowProctorMessageContent p.sceneTextArea").removeClass("sceneTextArea").addClass("sceneTextArea1");

        //Fix for LCMS-11085
        if (objectData.ProctorMessage.LogOutText != null && objectData.ProctorMessage.LogOutText != "") {
            logOutText = objectData.ProctorMessage.LogOutText;
        }

    };


    this.ShowSkippedQuestion = function(objectSkippedQuestion) {
        IsAssessmentIncomplete = true;
        //LCMS-8188 - Start
        if (isBrowserVersionIE6 == true) {
            var mainCont = $('#assesmentcontainer');
            if (mainCont.css('position') != 'relative') {
                mainCont.css('position', 'relative');
            }
        }
        //LCMS-8188 - End
        //alert('In Assessment Result');
        $('#quiz_container').hide();
        $("#toogle-flag").addClass('hide');
        $(gradeAssessment).hide();
        assessmentTimerObj.PauseTimer();

        if (objectSkippedQuestion.LogOutText.length > 0) {
            logOutText = objectSkippedQuestion.LogOutText;
        }

        //var description = "You have not answered all of the questions in this assessment. To go back to a question click on one of                  the questions below or the 'Answer Remaining Questions' button. To submit your answers for grading without answering these questions click on the 'Continue Without Answering' button. NOTE: If you continue without answering the questions they will be scored and marked as incorrect.";
        //$('#assessmentcontent').find('p').html(description);
        $('#assesmentcontainer').show();
        $('#coursecontent').show();
        $('#assessmentincomplete').show();
        $('#assessmentincomplete h1').text(AssessmentIncompleteText);

        $('#assessmentcontent').show();
        $(assessmentControlPanel).hide();


        //Binding Answer Remaining Button	 

        $('#buttoncontainer').find("div").eq(0).find("button").bind('click.namespace', function() {
            $('#buttoncontainer').find("div").eq(0).find("button").unbind('click.namespace');
            $('#assessmentcontent').find("ul").empty();
            $('#assesmentcontainer').hide();

            assessmentTimerObj.ResumeTimer();

            answerRemainingClick();
            resetCPIdleTimer();
            return false;
        });

        //Binding Continue Grading Button 
        $('#buttoncontainer').find("div").eq(1).find("button").bind('click.namespace', function() {
            $('#buttoncontainer').find("div").eq(1).find("button").unbind('click.namespace');
            $('#assesmentcontainer').hide();
            assessmentTimerObj.StopTimer();
            continueGradingClick();
            resetCPIdleTimer();
            return false;
        });

        //LCMS-8188 - Start
        var assessmentCont = $('#assessmentcontent')
        //$('#assessmentcontent').empty();
        assessmentCont.empty();
        //LCMS-8188 - End

        var skippedQuestionLenght = objectSkippedQuestion.SkippedQuestions.length;
        var listSize = 3; // No of Questions to display in 1 column
        var totalNoUl = Math.ceil(skippedQuestionLenght / listSize); // Total No of columns

        var columnCounter = 0;

        var dynamicHtml = "<p>";

        while (columnCounter < totalNoUl) {
            var start = 0;
            var end = listSize;
            if (columnCounter > 0) {
                start = (listSize * columnCounter);
                end = start + listSize;
            }

            dynamicHtml += "<ul style='list-style-type:none; width:150px'>"; //Added By Abdus Samad For LCMS-12105

            for (i = start; i < skippedQuestionLenght; i++) {
                if (i >= end) {
                    continue;
                }


                dynamicHtml += "<li style='list-style-type:none;'>"; //Added By Abdus Samad For LCMS-12105


                //Added By Abdus Samad For LCMS-12105
                //START

                if (objectSkippedQuestion.SkippedQuestions[i].AssessmentToogleFlag == true) {
                    //$("#toogle-flag").addClass('flagged'); 
                    //dynamicHtml += "<img src='/assets/img/smallFlag2.png' width='20px' id='ImageButton1'></img>";
                    dynamicHtml += "<i class='glyphicon glyphicon-flag filled'></i>";

                }

                if (objectSkippedQuestion.SkippedQuestions[i].AssessmentToogleFlag == false) {
                    //$("#toogle-flag").removeClass('flagged'); 
                    dynamicHtml += "<i class='glyphicon glyphicon-flag unfilled'></i>";
                    //dynamicHtml += "<img src='/assets/img/smallFlag1.png' width='20px' id='ImageButton1'></img>";
                }

                //STOP

                //LCMS-8188 - Start
                //dynamicHtml += "<li> <a href='#' onclick='javascript:AskSpecifiedQuestion(" + objectSkippedQuestion.SkippedQuestions[i].AssessmentItemID + ");'> Question " + objectSkippedQuestion.SkippedQuestions[i].QuestionNo + "</a></li>";
                dynamicHtml += "<a href='#' id='ai_" + objectSkippedQuestion.SkippedQuestions[i].AssessmentItemID + "'> " + Question_text + " " + objectSkippedQuestion.SkippedQuestions[i].QuestionNo + "</a></li>";
                //LCMS-8188 - End

            }
            dynamicHtml += "</ul>";
            //alert("Dynamic HTML\n" + dynamicHtml);
            columnCounter++;
        }
        dynamicHtml += "</p>";
        //alert(dynamicHtml);
        //LCMS-8188 - Start
        //$('#assessmentcontent').append(dynamicHtml);
        assessmentCont.html(dynamicHtml);
        assessmentCont.find('li a').bind('click.namespace', function() {
            AskSpecifiedQuestion(this.id.split("_")[1]);
        });
        //LCMS-8188 - End	    
    }       // End ShowSkippedQuestions


    this.ShowAnswerReview = function(objectAnswerReview) {
        IsAssessmentIncomplete = false;
        //LCMS-8188 - Start	    
        if (isBrowserVersionIE6 == true) {
            var mainCont = $('#AnswerReviewContainer');
            if (mainCont.css('position') != 'relative') {
                mainCont.css('position', 'relative');
            }
        }
        //LCMS-8188 - End
        $('#toogle-flag').addClass('hide');
        assessmentTimerObj.PauseTimer();
        $(assessmentControlPanel).hide();
        $(gradeAssessment).hide();

        if (objectAnswerReview.LogOutText.length > 0) {
            logOutText = objectAnswerReview.LogOutText;
        }

        $(NextQuestionButtonEn).unbind('click.namespace');
        $('#quiz_container').hide();

        var description = "Below is how you answered each question. If you would like to change your answer please click on the question. To return to this screen click on the 'Grade Assessment' button. To continue with grading this assessment click on the 'Finish Grading My Assessment' button.";
        //$('#AnswerReviewHeading').find('h1').html('Answer Review');
        //$('#AnswerReviewMessage').find('p').html(description);
        //$('#AnswerReviewButtons').find("div").eq(0).find("button").html();
        $('#AnswerReviewHeading').show();

        //$('#coursecontent').show();
        //$('#AnswerReviewMessage').find('p').html(description);
        $('#AnswerReviewContent').empty();
        $('#AnswerReviewContent').show();

        $('#AnswerReviewContainer').show();

        //Binding Continue Grading Button 
        $('#AnswerReviewButtons').find("div").eq(0).find("button").bind('click.namespace', function() {
            $('#AnswerReviewButtons').find("div").eq(0).find("button").unbind('click.namespace');
            $('#AnswerReviewContainer').hide();
            //		continueGradingClick();
            //		resetCPIdleTimer();        
            assessmentTimerObj.StopTimer();
            FinishGradingAssessment();
            return false;
        });

        var AnswerReviewsLenght = objectAnswerReview.AnswerReviews.length;

        var listSize = 0; // No of Questions to display in 1 column
        var totalNoUl = 0; //Math.ceil(AnswerReviewsLenght / listSize); // Total No of columns
        if (AnswerReviewsLenght > 10) {
            totalNoUl = 2;
            listSize = Math.ceil(AnswerReviewsLenght / 2);

            //Button Positioning//
            // $('#AnswerReviewButtons').css('position','relative'); 
        }
        else if (AnswerReviewsLenght > 0) {
            totalNoUl = 1;
            listSize = 10;

            //Button Positioning//
            // $('#AnswerReviewButtons').css('position','absolute');
        }

        //Button Positioning//
        if (AnswerReviewsLenght > 26) {
            $('#AnswerReviewButtons').css('position', 'relative');
        }
        else if (AnswerReviewsLenght > 0) {
            $('#AnswerReviewButtons').css('position', 'absolute');
        }

        var columnCounter = 0;

        var dynamicHtml = null;
        while (columnCounter < totalNoUl) {
            var start = 0;
            var end = listSize;
            if (columnCounter > 0) {
                start = (listSize * columnCounter);
                end = start + listSize;
            }

            //Added By Abdus Samad For LCMS-12105
            //Start
            if (allowSkipping == true) {
                dynamicHtml = "<ul style='list-style-type:none;'>";
            }
            else {
                dynamicHtml = "<ul>";
            }
            //Stop

            for (i = start; i < AnswerReviewsLenght; i++) {
                if (i >= end) {
                    continue;
                }

                //var answers = objectAnswerReview.AnswerReviews[i].Answers;
                var answers = "";
                var answersLength = objectAnswerReview.AnswerReviews[i].Answers.length;


                //if (answersLength > 1) // condition replace by Mustafa for LCMS-1193
                if ((objectAnswerReview.AnswerReviews[i].AssessmentItemType == "Fill in the Blanks") && (answersLength > 0)) {
                    var answerCount = 0;
                    for (x = 0; x < answersLength; x++) {
                        if (objectAnswerReview.AnswerReviews[i].Answers[x] == "null" || objectAnswerReview.AnswerReviews[i].Answers[x] == "") {
                            continue;
                        }
                        answers += objectAnswerReview.AnswerReviews[i].Answers[x] + ",";
                        answerCount++;
                    }

                    if (answerCount == 0) {
                        answers = Not_Completed; //"Not&nbsp;CompletedTest1";
                    }
                    else if (answersLength > answerCount) {
                        answers = "Incomplete";
                    }
                    else if (answerCount > 1) {
                        answers = answers.substring(0, answers.lastIndexOf(","));
                        answers = answers.substring(0, answers.lastIndexOf(",")) + " " + and_text + " " + answers.substring(answers.lastIndexOf(",") + 1, answers.length);
                    }
                    else {
                        answers = answers.substring(0, answers.lastIndexOf(","));
                    }

                }
                else if ((objectAnswerReview.AnswerReviews[i].AssessmentItemType != "Image Selection") && (objectAnswerReview.AnswerReviews[i].AssessmentItemType != "Ordering") && (objectAnswerReview.AnswerReviews[i].AssessmentItemType != "Matching") && (answersLength > 1)) {
                    for (x = 0; x < answersLength; x++) {
                        if (objectAnswerReview.AnswerReviews[i].Answers[x] == "null") {
                            continue;
                        }
                        answers += objectAnswerReview.AnswerReviews[i].Answers[x] + ", ";
                    }

                    subStrLenght = null;
                    subStrLength = (answers.length) - 2;

                    answers = answers.substring(0, subStrLength);

                    //alert(answers);
                    //answers = answers.substring(0,answers.lastIndexOf(","));
                    answers = answers.substring(0, answers.lastIndexOf(",")) + " " + and_text + " " + answers.substring(answers.lastIndexOf(",") + 1, answers.length);

                }
                else {

                    if (objectAnswerReview.AnswerReviews[i].Answers.length == 0 && objectAnswerReview.AnswerReviews[i].AssessmentItemType != "Ordering") {
                        answers = Not_Completed; //"Not&nbsp;CompletedTest2";
                    }
                    else {

                        if (objectAnswerReview.AnswerReviews[i].AssessmentItemType == "Image Selection") {
                            answers = Response_Recorded; //"Response RecordedTest3";
                        }
                        else if (objectAnswerReview.AnswerReviews[i].AssessmentItemType == "Ordering") // Condition put by Mustafa for LCMS-1193
                        {
                            answers = Response_Recorded; //"Response RecordedTest4";
                        }
                        else if (objectAnswerReview.AnswerReviews[i].AssessmentItemType == "Matching") {
                            var nullcounter = 0
                            var anscounter = 0
                            for (x = 0; x < answersLength; x++) {
                                if (objectAnswerReview.AnswerReviews[i].Answers[x] == "null") {
                                    nullcounter++;
                                    continue;
                                }
                                anscounter++;
                            }
                            if (anscounter == nullcounter) {
                                answers = Not_Completed; //"Not&nbsp;CompletedTest5";
                            }
                            else {
                                answers = Response_Recorded; //"Response RecordedTest6";
                            }

                        }
                        else if (objectAnswerReview.AnswerReviews[i].AssessmentItemType == "True False") {
                            answers = RenderTrueFalseString(objectAnswerReview.AnswerReviews[i].Answers[0]);
                        }
                        else {
                            answers = objectAnswerReview.AnswerReviews[i].Answers[0];
                        }

                    }
                }

                //Added By Abdus Samad For LCMS-12105
                //Start
                if (allowSkipping == true) {
                    dynamicHtml += "<li style='list-style-type:none;'>";
                }
                else {
                    dynamicHtml += "<li>";
                }
                //Stop

                //Added By Abdus Samad For LCMS-12105
                //START
                if (allowSkipping == true) {
                    if (objectAnswerReview.AnswerReviews[i].AssessmentToogleFlag == true) {
                        //$("#toogle-flag").addClass('flagged'); 
                        dynamicHtml += "<i class='glyphicon glyphicon-flag filled'></i>";
                        //dynamicHtml += "<img src='/assets/img/smallFlag2.png' width='20px' id='ImageButton1'></img>";

                    }

                    if (objectAnswerReview.AnswerReviews[i].AssessmentToogleFlag == false) {
                        //$("#toogle-flag").removeClass('flagged'); 
                        dynamicHtml += "<i class='glyphicon glyphicon-flag unfilled'></i>";
                        //dynamicHtml += "<img src='/assets/img/smallFlag1.png' width='20px' id='ImageButton1'></img>";
                    }
                }
                //STOP


                //LCMS-8188 - Start
                //dynamicHtml += "<li> <a href='#' onclick='AskSpecifiedQuestion(" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + ");'> Question " + objectAnswerReview.AnswerReviews[i].QuestionNo + "</a> &nbsp; <a href='#' onclick='AskSpecifiedQuestion(" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + ");'>" + answers + "</a></li>";
                //dynamicHtml += "<li> <a href='#' id='ai_" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + "' > Question " + objectAnswerReview.AnswerReviews[i].QuestionNo + "</a> &nbsp; <a href='#' id='ai_" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + "'>" + answers + "</a></li>";
                //LCMS-8188 - End
                dynamicHtml += " <a href='#' id='ai_" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + "' > " + Question_text + " " + objectAnswerReview.AnswerReviews[i].QuestionNo + "</a> &nbsp; <a href='#' id='ai_" + objectAnswerReview.AnswerReviews[i].AssessmentItemID + "'>" + answers + "</a></li>";


            }
            dynamicHtml += "</ul>";
            //alert("Dynamic HTML\n" + dynamicHtml);
            //	        alert("First\n"+$('#AnswerReviewContent').html());
            //	        $('#AnswerReviewContent').append(dynamicHtml);
            //	        alert("Second\n"+$('#AnswerReviewContent').html());

            //$('#AnswerReviewContent').append(dynamicHtml);

            //LCMS-8188 - Start
            var assessmentCont = $('#AnswerReviewContent');
            assessmentCont.append(dynamicHtml);
            assessmentCont.find('li a').bind('click.namespace', function() {
                AskSpecifiedQuestion(this.id.split("_")[1]);
            });
            //LCMS-8188 - End
            columnCounter++;
        }





        $(NextQuestionButtonEn).bind('click.namespace', function() {
            $(NextQuestionButtonEn).unbind('click.namespace');
            FinishGradingAssessment();
        });

    }                    // End Show Answer Review	


    this.ShowIndividualQuestionScore = function(objectIndividualScores) {
        this.ShowSimpleIndividualQuestionScore(objectIndividualScores, true);
    }

    this.ShowTopicIndividualQuestionScore = function(topicScoreSummaries) {
        $('#IndividualScoreContent').empty();
        for (var i = 0; i < topicScoreSummaries.length; i++) {

            $('#IndividualScoreContent').append("<div id='TopicIndividualScoreContainer'><span><b>" + topicScoreSummaries[i].TopicScoreSummary.TopicName + "</b></span><div id='TopicIndividualScoreContent'>");
            this.ShowSimpleIndividualQuestionScore(topicScoreSummaries[i].TopicScoreSummary.ShowIndividualQuestionScore, false);
            $('#IndividualScoreContent').append("</div></div>");
        }
    }

    this.ShowSimpleIndividualQuestionScore = function(objectIndividualScores, emptyCotent) {


        //Hide other divs
        $(gradeAssessment).hide();
        $('#quiz_container').hide();
        $('#assesmentcontainer').hide();
        $('#AnswerReviewContainer').hide();
        $('#QuestionRemediationContainer').hide();
        $('#QuestionRemediationButtons').find("div").eq(0).find("button").hide();
        $('#QuestionRemediationButtons').find("div").eq(1).find("button").hide();
        //$('#BackQuestionButtonEn').hide();
        $("#BackQuestionButtonEn").addClass("hide");
        $(NextQuestionButtonEn).show();
        $(NextQuestionButtonDs).hide();





        //	    var description = "Below is how you answered each question. If you would like to change your answer please click on the question. To return to this screen click on the 'Grade Assessment' button. To continue with grading this assessment click on the 'Finish Grading My Assessment' button.";
        //	    $('#AnswerReviewContent').find('p').html(description);
        $('#IndividualScoreContainer').show();
        $('#coursecontent').show();
        //$('#IndividualScoreHeading').find('h1').html('Individual Question Score');
        $('#IndividualScoreHeading').show();

        if (emptyCotent) {
            $('#IndividualScoreContent').empty();
        }

        $('#IndividualScoreContent').show();

        //Binding Continue Grading Button 

        $('#IndividualScoreButtons').find("button").bind('click.namespace', function() {
            $('#IndividualScoreButtons').find("button").unbind('click.namespace');
            $('#assessment_result_container').hide();

            //$('#IndividualScoreContainer').hide();

            ShowAnswers();
            return false;
        });

        //		$('#IndividualScoreButtons').find("div").eq(0).find("button").bind('click.namespace', function(){
        //		$('#IndividualScoreButtons').find("div").eq(0).find("button").unbind('click.namespace');
        //		$('#IndividualScoreContainer').hide();
        //		
        //        ShowAnswers();
        //        return false;
        //		}); 

        var QstScoresLenght = objectIndividualScores.IndividualQuestionScores.length;
        var listSize = 0; // No of Questions to display in 1 column
        var totalNoUl = 0; //Math.ceil(QstScoresLenght / listSize); // Total No of columns
        if (QstScoresLenght > 8) {
            totalNoUl = 2;
            listSize = Math.ceil(QstScoresLenght / 2);

            //Button Positioning//
            //  $('#IndividualScoreButtons').css('position','relative'); 
        }
        else if (QstScoresLenght > 0) {
            totalNoUl = 1;
            listSize = 8;

            //Button Positioning//
            // $('#IndividualScoreButtons').css('position','absolute'); 
        }

        //Button Positioning//
        /*LCMS-7783 - Start - no need to set position as absolute*/
        /*
        if (QstScoresLenght > 26) {
        $('#IndividualScoreButtons').css('position', 'relative');
        }
        else if (QstScoresLenght > 0) {
        $('#IndividualScoreButtons').css('position', 'absolute');
        }
        */
        /*LCMS-7783 =- End*/
        var columnCounter = 0;

        //var dynamicHtml = "<ul>";
        var dynamicHtml = "<table cellspacing='2' cellpadding=2' class='answers-table'><tbody>";
        var questionsCountToShow = 2;

        var html = "";

        for (var i = 0; i < objectIndividualScores.IndividualQuestionScores.length; i++) {


            if (i % questionsCountToShow == 0) {
                html += "<tr>";
            }



            var answers = "";

            // LCMS-3121
            //------------------------------------
            //var preAnswers = "Click to Preview";
            //var preAnswers = "Click&nbsp;to&nbsp;Preview";
            var preAnswers = Response_Recorded; //"Yasin10"; //"Response&nbsp;Recorded";
            //------------------------------------

            var answersLength = objectIndividualScores.IndividualQuestionScores[i].Answers.length;

            if (answersLength > 1) {
                for (x = 0; x < answersLength; x++) {
                    if (objectIndividualScores.IndividualQuestionScores[i].Answers[x] == "null") {
                        continue;
                    }

                    // LCMS-3121
                    //------------------------------------

                    if (objectIndividualScores.IndividualQuestionScores[i].Answers[x].replace(/^\s+|\s+$/g, "") != "") {
                        var ans = objectIndividualScores.IndividualQuestionScores[i].Answers[x] + ", ";
                        answers += ans;
                    }
                    //------------------------------------

                }

                subStrLenght = null;
                // LCMS-3121
                //------------------------------------
                //subStrLength = (answers.length) - 2;
                subStrLength = (answers.length) - 7;
                //------------------------------------

                //answers = answers.substring(0,subStrLength);
            }
            else {

                if (objectIndividualScores.IndividualQuestionScores[i].Answers.length == 0) {
                    answers = "&nbsp;";
                }
                else {

                    answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0];

                    // LCMS-3121
                    //------------------------------------------------------------------------
                    //answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0];
                    //answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0].replace(new RegExp(" ","g"),"&nbsp;").replace(new RegExp("-","g"),"&#8209;");
                    //------------------------------------------------------------------------
                }
            }

            // start LCMS-5276
            if (answers.indexOf(",") != -1) {
                answers = answers.substring(0, answers.lastIndexOf(","));
            }
            // below answercopy for fitb
            var AnswersCopy = answers;

            if (answers.replace(new RegExp(",", "gi"), "").replace("&nbsp;", "").replace(/^\s+|\s+$/g, "").length == 0) {
                answers = Not_Completed; //"Yasin4"; //"Not&nbsp;Completed";
            }

            if (answers != Not_Completed /*"Not&nbsp;Completed"*/ && answers.indexOf(",") != -1) {
                answers = answers.substring(0, answers.lastIndexOf(",")) + " " + and_text + " " + answers.substring(answers.lastIndexOf(",") + 1, answers.length);
            }

            html += "<td width='25%' nowrap><ul><li>";
            // end LCMS-5276
            if (objectIndividualScores.IndividualQuestionScores[i].IsCorrect) {
                html += "<div class='icon-correct'></div>";
                // Condition added by Mustafa for LCMS-734
                if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Image Selection") {
                    //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "' >&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                    //LCMS-8188 - End
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Matching") {
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "' >&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                    //LCMS-8188 - End
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Ordering") {
                    //Naveed - LCMS-3730
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "' >&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                    //LCMS-8188 - End
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "True False") {
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>True</a>";

                html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + RenderTrueFalseString(objectIndividualScores.IndividualQuestionScores[i].Answers[0]) + "</a>";
                    //LCMS-8188 - End
                }
                else {
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a>";
                    //LCMS-8188 - End
                }
            }
            else {
                html += "<div class='icon-incorrect'></div>";
                // Condition added by Mustafa for LCMS-734
                if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Image Selection") {

                    if (answers == Not_Completed /*"Not&nbsp;Completed"*/) {
                        //LCMS-8188 - Start
                        //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>Not&nbsp;Completed</a>";
                        html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + Not_Completed + "</a>"; //Not&nbsp;CompletedYasin9
                        //LCMS-8188 - End
                    } else {
                        //LCMS-8188 - Start
                        //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                        //LCMS-8188 - End
                    }
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Matching") {
                    var nullcounter = 0;
                    var anscounter = 0;
                    for (x = 0; x < objectIndividualScores.IndividualQuestionScores[i].Answers.length; x++) {
                        if (objectIndividualScores.IndividualQuestionScores[i].Answers[x] == "null") {
                            nullcounter++;
                            continue;
                        }
                        anscounter++;
                    }
                    if (nullcounter == anscounter) {
                        //LCMS-8188 - Start
                        //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>Not&nbsp;Completed</a>";
                        html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + Not_Completed + "</a>"; //Not&nbsp;Completed
                        //LCMS-8188 - End
                    } else {
                        //LCMS-8188 - Start
                        //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                        //LCMS-8188 - End
                    }
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Ordering") {
                    //LCMS-3730
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a>";
                html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a>";
                    //LCMS-8188 - End
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "True False") {
                if (answers != Not_Completed /*"Not&nbsp;Completed"*/) {
                        answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0];
                    }
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + RenderTrueFalseString(answers) + "</a>";
                    //LCMS-8188 - End
                }
                else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Fill in the Blanks") {
                if (answersLength > AnswersCopy.split(",").length && answers != Not_Completed /*"Not&nbsp;Completed"*/) {
                        answers = "Incomplete";
                    }
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a>";
                    //LCMS-8188 - End
                }
                else {
                    //LCMS-8188 - Start
                    //html += "<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a>";
                    html += "<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a>";
                    //LCMS-8188 - End
                }
            }
            html += "</li></ul></td>";

            if ((i + 1) % questionsCountToShow == 0) {
                html += "</tr>";
            }

        }

        //alert(html);
        var reamainingCol = objectIndividualScores.IndividualQuestionScores.length % questionsCountToShow;
        //alert("reamainingCol : "+ reamainingCol);

        if (reamainingCol > 0) {
            for (var i = 0; i < (questionsCountToShow - reamainingCol); i++) {
                html += "<td class='empty' width='25%' nowrap>&nbsp;</td>";
            }
            html += "</tr>";
        }

        //alert(html);

        dynamicHtml += html;

        while (false && columnCounter < totalNoUl) {
            var start = 0;
            var end = listSize;
            if (columnCounter > 0) {
                start = (listSize * columnCounter);
                end = start + listSize;
            }


            for (i = start; i < QstScoresLenght; i++) {
                if (i >= end) {
                    continue;
                }

                //var answers = objectAnswerReview.AnswerReviews[i].Answers;
                var answers = "";

                // LCMS-3121
                //------------------------------------
                //var preAnswers = "Click to Preview";
                //var preAnswers = "Click&nbsp;to&nbsp;Preview";
                var preAnswers = Response_Recorded; //"Yasin11"; "Response&nbsp;Recorded";
                //------------------------------------

                var answersLength = objectIndividualScores.IndividualQuestionScores[i].Answers.length;

                if (answersLength > 1) {
                    for (x = 0; x < answersLength; x++) {
                        if (objectIndividualScores.IndividualQuestionScores[i].Answers[x] == "null") {
                            continue;
                        }

                        // LCMS-3121
                        //------------------------------------

                        if (objectIndividualScores.IndividualQuestionScores[i].Answers[x].replace(/^\s+|\s+$/g, "") != "") {
                            var ans = objectIndividualScores.IndividualQuestionScores[i].Answers[x] + ", ";
                            answers += ans;
                        }
                        //------------------------------------

                    }

                    subStrLenght = null;
                    // LCMS-3121
                    //------------------------------------
                    //subStrLength = (answers.length) - 2;
                    subStrLength = (answers.length) - 7;
                    //------------------------------------

                    //answers = answers.substring(0,subStrLength);
                }
                else {

                    if (objectIndividualScores.IndividualQuestionScores[i].Answers.length == 0) {
                        answers = "&nbsp;";
                    }
                    else {

                        answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0];

                        // LCMS-3121
                        //------------------------------------------------------------------------
                        //answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0];
                        //answers = objectIndividualScores.IndividualQuestionScores[i].Answers[0].replace(new RegExp(" ","g"),"&nbsp;").replace(new RegExp("-","g"),"&#8209;");
                        //------------------------------------------------------------------------
                    }
                }

                // start LCMS-5276
                if (answers.indexOf(",") != -1) {
                    answers = answers.substring(0, answers.lastIndexOf(","));
                }
                // below answercopy for fitb
                var AnswersCopy = answers;

                if (answers.replace(new RegExp(",", "gi"), "").replace("&nbsp;", "").replace(/^\s+|\s+$/g, "").length == 0) {
                    answers = Not_Completed; //"Yasin6"; //"Not&nbsp;Completed";
                }

                if (answers != Not_Completed /*"Not&nbsp;Completed"*/ && answers.indexOf(",") != -1) {
                    answers = answers.substring(0, answers.lastIndexOf(",")) + " " + and_text + " " + answers.substring(answers.lastIndexOf(",") + 1, answers.length);
                }

                // end LCMS-5276
                if (objectIndividualScores.IndividualQuestionScores[i].IsCorrect) {
                    // Condition added by Mustafa for LCMS-734
                    if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Image Selection") {
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'> Question " + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a></li>";
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                        dynamicHtml += "<li class='correct'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End

                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Matching") {
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                    dynamicHtml += "<li class='correct'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Ordering") {
                        //LCMS-8188 - Start
                        //Naveed - LCMS-3730
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>Click to Review Ordering Question</a></li>";
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                    dynamicHtml += "<li class='correct'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "True False") {
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>True</a></li>";
                    dynamicHtml += "<li class='correct'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>True</a></li>";
                        //LCMS-8188 - End
                    }
                    else {
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='correct'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a></li>";
                        dynamicHtml += "<li class='correct'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a></li>";
                        //LCMS-8188 - End
                    }
                }
                else {
                    // Condition added by Mustafa for LCMS-734
                    if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Image Selection") {
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'> Question " + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a></li>";
                        if (answers == Not_Completed /*"Not&nbsp;Completed"*/)
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>Not&nbsp;Completed</a></li>";
                            dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + Not_Completed + "</a></li>"; //Not&nbsp;CompletedYasin7
                        //LCMS-8188 - End
                        else
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                            dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Matching") {
                        var nullcounter = 0;
                        var anscounter = 0;
                        for (x = 0; x < objectIndividualScores.IndividualQuestionScores[i].Answers.length; x++) {
                            if (objectIndividualScores.IndividualQuestionScores[i].Answers[x] == "null") {
                                nullcounter++;
                                continue;
                            }
                            anscounter++;
                        }
                        if (nullcounter == anscounter)
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>Not&nbsp;Completed</a></li>";
                            dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + Not_Completed + "</a></li>"; //Not&nbsp;Completedyasin8
                        //LCMS-8188 - End
                        else
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                            dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Ordering") {
                        //LCMS-8188 - Start
                        //LCMS-3730
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + preAnswers + "</a></li>";
                    dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;" + "&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + preAnswers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "True False") {
                    if (answers != Not_Completed /*"Not&nbsp;Completed"*/) {
                            answers = "False";
                        }
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a></li>";
                        dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else if (objectIndividualScores.IndividualQuestionScores[i].AssessmentItemType == "Fill in the Blanks") {
                    if (answersLength > AnswersCopy.split(",").length && answers != Not_Completed /*"Not&nbsp;Completed"*/) {
                            answers = "Incomplete";
                        }
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a></li>";
                        dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a></li>";
                        //LCMS-8188 - End
                    }
                    else {
                        //LCMS-8188 - Start
                        //dynamicHtml += "<li class='incorrect'><a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>&nbsp;Question&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' onclick='ShowSpecifiedQuestionScore(" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + ");'>" + answers + "</a></li>";
                        dynamicHtml += "<li class='incorrect'><a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>&nbsp;" + Question_text + "&nbsp;" + objectIndividualScores.IndividualQuestionScores[i].QuestionNo + "</a>&nbsp;&nbsp;<a href='#' id='ai_" + objectIndividualScores.IndividualQuestionScores[i].AssessmentItemID + "'>" + answers + "</a></li>";
                        //LCMS-8188 - End
                    }
                }
            }


            // alert("Dynamic HTML\n" + dynamicHtml);



            columnCounter++;
        } // end of while loop
        //dynamicHtml += "</ul>";
        dynamicHtml += "</tbody></table>";
        //LCMS-8188 - Start
        //$('#IndividualScoreContent').append(dynamicHtml);
        var indScoreCont = $('#IndividualScoreContent');
        //indScoreCont.append(dynamicHtml);
        indScoreCont.append(dynamicHtml);

        indScoreCont.find('li a').bind('click.namespace', function() {
            ShowSpecifiedQuestionScore(this.id.split("_")[1]);
        });
        //LCMS-8188 - End	    

        //$('#IndividualScoreContainer').css("z-index","10");
        //$('#IndividualScoreContainer').css("top","250px");
        //$('#IndividualScoreContainer').css("left","0px");
        // Commented out because Individual scores are now part of score sumary screen
        //	    $(NextQuestionButtonEn).bind('click.namespace', function()
        //	    {
        //            $(NextQuestionButtonEn).unbind('click.namespace');   
        //            $('#IndividualScoreContainer').hide();
        //            ContinueAfterAssessment();
        //	    });


    }                               // End Show Individual Question Score


    //zaheer code

    this.lockCourseTimers = function(status) {
        //stop 0
        //Resume 1
        //Pause 2

        switch (status) {

            case "0":
                {

                    lockCourseTimer.StopTimer();
                    break;
                }
            case "1":
                {

                    lockCourseTimer.ResumeTimer();
                    break;
                }
            case "2":
                {
                    lockCourseTimer.PauseTimer();
                    break;
                }
        }

    }


    this.assessmentTimer = function(status) {
        //stop 0
        //Resume 1
        //Pause 2

        switch (status) {

            case "0":
                {

                    assessmentTimerObj.StopTimer();
                    // assessmentTimerObj.
                    break;
                }
            case "1":
                {

                    assessmentTimerObj.ResumeTimer();
                    break;
                }
            case "2":
                {
                    assessmentTimerObj.PauseTimer();
                    break;
                }
        }

    }
    this.validationTimer = function(status) {
        //stop 0
        //Resume 1
        //Pause 2

        //alert('validationTimer Status:'+status);
        switch (status) {
            case "0":
                {
                    validationTimerObj.StopTimer();
                    break;
                }
            case "1":
                {
                    validationTimerObj.ResumeTimer();
                    break;
                }
            case "2":
                {
                    validationTimerObj.PauseTimer();
                    break;
                }
        }

    }


    this.ResetValidationTimer = function() {

        //alert('ResetValidationTimer Out Bound:'+isValidationStarted);

        if (isValidationStarted) {
            if (validationTimerValue != -1) {

                // alert("ResetValidationTimer Inbound:"+validationTimerValue);
                //validationTimerObj.seconds=(validationTimerValue*1000);
                // validationTimerObj.InitializeTimer(validationTimerValue);
                validationTimerObj.ReInitializeTimer(validationTimerValue);
                isValidationStarted = false;

            }


        }
        //visible tools buttons and hide validation button
        //ResumeCourseAfterValidation call this method 
    }
    this.ShowValidationOrientationSceneHTMLRendering = function(showHTMLObject) {

        //alert('ShowValidationOrientationSceneHTMLRendering');
        this.EnableValidationControlBar(true);

        if (document.getElementById('media') != null)
            document.getElementById('media').innerHTML = "";
        //$(htmlContentContainer).empty();
        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();
        $("#ValidationTimer").hide();
        $('#controlPanel').hide();
        //$(quiz_container).find('#assessmentQuestionTemplate').empty();
        $(quiz_container).hide();
        //$("#ImageDiv").hide();

        var htmlString = showHTMLObject.ValidationOrientationScene.TemplateHtml;
        $(htmlContentContainer).html(htmlString);
        //alert(htmlString);

        $('#ValidationPlaybuttonEn').unbind('click.namespace');
        $('#ValidationPlaybuttonEn').bind('click.namespace', function() {
            //$('#ValidationPlaybuttonEn').unbind('click.namespace');
            //alert('ValidationPlaybuttonEn');
            ui.slide.next(function()
            {
	            cp.AskValidationQuestion();
            });            
            
            //this.EnableValidationControlBar(false);

        }); //Validate Button Click end

        //$(htmlContentContainer).find('.sceneTextArea').find('a').attr('target', '_blank');





    }


    this.EnableValidationControlBar = function(status) {

        //alert('EnableValidationControlBar:'+status);
        if (status == true) {
            //validation start
            $('#ValidationPlaybuttonEn').show();
            $("#ValidationControlBar").show();
            $("#ValidationTimer").show();
            $("#controlBar").hide();
            //  $("#quiz_container").hide();





        }
        else {
            //validation stop

            $("#ValidationControlBar").hide();
            $("#studentAuthentication").hide();
            //$('#ValidationPlaybuttonEn').hide()
            $("#controlBar").show();
        }
    }

    this.ShowValidationQuestionRendering = function(authenticationObject) {

        //authenticationObject.ValidationQuestion.InitialValidationQuestionAskingTimer
        //IsValidationStarted
        //alert('ShowValidationQuestionRendering');
        //$(studentAuthentication).empty();
        $("#assesmentcontainer").hide();
        $("#IndividualScoreContainer").hide();
        $("#QuestionRemediationContainer").hide();
        $("#CARealStateValidation").hide();
        $("#NYInsuranceValidation").hide();
        $("#proctor_login_screen").hide();
        $("#assessmentItemResult").hide();
        // $("#quiz_container").hide();
        $("#AnswerReviewContainer").hide();
        $("#assessmentincomplete").hide();

        if (document.getElementById('media') != null)
            document.getElementById('media').innerHTML = "";
        //$(htmlContentContainer).empty();	

        $(htmlContentContainer).hide();
        //$(quiz_container).find('#assessmentQuestionTemplate').empty();
        $(quiz_container).hide();

        this.EnableValidationControlBar(true);
        $(studentAuthentication).show();

        lockCourseTimer.TimerContainer($("#vTimer"));
        lockCourseTimer.InitializeTimer(authenticationObject.ValidationQuestion.ValidationQuestionTimer);
        validationTimerObj.StopTimer();

        this.EnableValidationControlBar(true);
        validationQuestionID = authenticationObject.ValidationQuestion.ValidationQuestionID; //global variable
        $(studentAuthentication).find('h3').html(authenticationObject.ValidationQuestion.ValidationQuestionText);
        validationQuestionType = authenticationObject.ValidationQuestion.ValidationQuestionType;
        var validationQuestionOptions = authenticationObject.ValidationQuestion.ValidationQuestionOptions;

        if (validationQuestionType == 'Single Select') {
            $(studentAuthentication).find("#authenticateText").hide();
            $(studentAuthentication).find("#authenticateSelect").show();
            if (validationQuestionOptions != null && validationQuestionOptions.length > 0) {

                var select = document.getElementById("authenticateSelect");

                if (select.options.length > 0) {
                    for (i = select.options.length - 1; i >= 0; i--) {
                        select.remove(i);
                    }
                }

                var arrayValidationQuestionOption = validationQuestionOptions.split(",");
                for (i = 0; i < arrayValidationQuestionOption.length; i++) {
                    var subarrayValidationQuestionOption = arrayValidationQuestionOption[i].split(":");
                    select.options[select.options.length] = new Option(subarrayValidationQuestionOption[1].replace('\'', '').replace('\'', '').replace('};', ''), subarrayValidationQuestionOption[0]);
                }
            }
        }
        else {
            $(studentAuthentication).find("#authenticateText").show();
            $(studentAuthentication).find("#authenticateSelect").hide();
            $(studentAuthentication).find("#authenticateText").val("");
        }

        //lockCourseTimer.TimerContainer($("#authenticationTimer span"));
        //$("#ValidationTimer").empty();
        //alert(authenticationObject.ValidationQuestion.ValidationQuestionTimer);
        //$("#ValidationTimer").html(authenticationObject.ValidationQuestion.ValidationQuestionTimer);

        //authenticationObject.ValidationQuestion.ValidationQuestionTimer
        //alert('this.EnableValidationControlBar(true)');


        if (validationQuestionType == 'Single Select') {
            validationAnswerText = $(studentAuthentication).find("select#authenticateSelect").val();

        }
        else {
            validationAnswerText = $(studentAuthentication).find("#authenticateText").val(); //global variable	
        }

        $("#ValidationPlaybuttonEn").unbind('click.namespace');
        $("#ValidationPlaybuttonEn").bind('click.namespace', function() {



            if (validationQuestionType == 'Single Select') {
                validationAnswerText = $(studentAuthentication).find("select#authenticateSelect").val();

            }
            else {
                validationAnswerText = $(studentAuthentication).find("#authenticateText").val(); //global variable	   
            }
            lockCourseTimer.StopTimer();
            ui.slide.next(function()
            {
                cp.SubmitValidationQuestionResult(validationQuestionID, validationAnswerText);
            });            
            return false;
        }); //Validate My Identity Button Click end

        //				//Forgot My Answers Button Click
        //				$(studentAuthentication).find("button").eq(1).bind('click.namespace', function(){
        //					$(studentAuthentication).find("button").eq(1).unbind('click.namespace');
        //					$(overlay).fadeOut("slow");
        //					$(studentAuthentication).fadeOut("slow");
        //					//alert("Forgot My Answers");
        //					
        //						return false;
        //				});//Forgot My Answers Button Click end 


        //}); 

    }   // ShowStudentAuthenticationRendering end...

    //	this.ShowValidationQuestionRendering = function(authenticationObject) {	
    //			
    //			//alert('hide button');
    //			$(studentAuthentication).find("#authenticateText").val("");
    //		
    //				//lockCourseTimer.TimerContainer($("#authenticationTimer span"));
    //				lockCourseTimer.TimerContainer($("#ValidationTimer"));
    //				lockCourseTimer.InitializeTimer(authenticationObject.ValidationQuestion.ValidationQuestionTimer);
    //				validationTimerObj.StopTimer();
    //				
    //				
    //			//authenticationObject.ValidationQuestion.ValidationQuestionTimer
    //			
    //			    $("#htmlContentContainer").hide();
    //				$(studentAuthentication).show();
    //				$("#CourseMenuIcons").hide();
    //				$("#PlaybuttonEn").hide();
    //				$("#BackbuttonDs").hide();
    //				$("#GradeAssessment").hide();
    //				$("#assessmentTimer").hide();
    //				
    //				$("#NextQuestionButtonEn").hide();
    //				$("#ValidationPlaybuttonEn").show();
    //				
    //				
    //					
    //				validationQuestionID = authenticationObject.ValidationQuestion.ValidationQuestionID; //global variable
    //				validationAnswerText = $(studentAuthentication).find("#authenticateText").val(); //global variable	
    //				
    //				$(studentAuthentication).find('h3').html(authenticationObject.ValidationQuestion.ValidationQuestionText);
    //				
    //				//Validate My Identity Button Click	
    //				$("#ValidationPlaybuttonEn").bind('click.namespace', function(){
    //					$("#ValidationPlaybuttonEn").unbind('click.namespace');
    //					
    //				validationQuestionID = authenticationObject.ValidationQuestion.ValidationQuestionID; //global variable
    //				validationAnswerText = $(studentAuthentication).find("#authenticateText").val(); //global variable	
    //				
    //				lockCourseTimer.StopTimer();
    //				cp.SubmitValidationQuestionResult(validationQuestionID, validationAnswerText);
    //					
    //						return false;
    //				});//Validate My Identity Button Click end
    //			
    //				//Forgot My Answers Button Click
    //				$(studentAuthentication).find("button").eq(1).bind('click.namespace', function(){
    //					$(studentAuthentication).find("button").eq(1).unbind('click.namespace');
    //					$(overlay).fadeOut("slow");
    //					$(studentAuthentication).fadeOut("slow");
    //					//alert("Forgot My Answers");
    //					
    //						return false;
    //				});//Forgot My Answers Button Click end 

    //			
    //				
    //			//}); 
    //		
    //	}   // ShowStudentAuthenticationRendering end...
    //	
    //end of zaheer code

    
    this.ShowAssessmentScoreSummary = function(objectScoreSummary) {
        //LCMS-8188 - Start
        if (isBrowserVersionIE6 == true) {
            var mainCont = $('#assessment_result_container');
            if (mainCont.css('position') != 'relative') {
                mainCont.css('position', 'relative');
            }
        }
        //LCMS-8188 - End

        if (/*@cc_on!@*/false) { // check if it is Internet Explorer the double c on is jsript that only ie has	    
            document.onfocusout = ClearOnBlur;
        } else {
            window.onblur = ClearOnBlur;
        }
        IsShowAssessmentScoreSummary=true;

        $(gradeAssessment).hide();
        assessmentTimerObj.StopTimerFromAssessmentScoreSummary();
        $(assessmentControlPanel).show();
        //$(BackQuestionButtonEn).hide();
        $(BackQuestionButtonEn).addClass("hide");
        $(assessmentTimer).hide();
        $(NextQuestionButtonDs).hide();
        $(NextQuestionButtonEn).show();
        $('#AssessmentInProgress').hide();
        $('#buttoncontainerAnswerReview').hide();
        $('#buttoncontainerAnswerReviewPage').hide();
        $('#assessmentTimer').removeClass('slideIn');
        $('#assessmentControlPanel').find(".question-counter").empty();

        var isExam = objectScoreSummary.AssessmentScoreSummary.IsExam;
        var isShowGraph=objectScoreSummary.AssessmentScoreSummary.IsShowGraph;

        $(NextQuestionButtonEn).unbind('click.namespace');
        // Hide other Div's
        $('#quiz_container').hide();
        $('#assesmentcontainer').hide();
        $('#AnswerReviewContainer').hide();


        // First empty divs so there will be no previous data
        $('#assessment_result').find('h1').html('');
        $('#exam_assessment_result').find('h1').html('');

        $('#assessment_result_content').find('h2').html('');
        $('#assessment_result_content').find('h3').html('');
        $('#exam_assessment_result_content').find('h2').html('');
        $('#exam_assessment_result_content').find('h3').html('');

        // Show Score Summary Div
        $('#assessment_result_container').show();

        if (isExam) {
            $('#exam_assessment_result').find('h1').html(objectScoreSummary.AssessmentScoreSummary.HeadingAssesmentScoreSummaryText);
            $('#exam_assessment_result').find('h4').html(objectScoreSummary.AssessmentScoreSummary.ContentAssesmentScoreSummaryText);
            $('#exam_assessment_result').css('backgroundImage', 'url(' + objectScoreSummary.AssessmentScoreSummary.ImageAssessmentSummaryScoreUrl + ')');
            $('#exam_assessment_result_content').find('h2').html(objectScoreSummary.AssessmentScoreSummary.PercentageScoreText);
            if (objectScoreSummary.AssessmentScoreSummary.PassFailScoreText != null) {
                $('#exam_assessment_result_content').find('h3').html("<div style='margin-left: 10px;'>" + objectScoreSummary.AssessmentScoreSummary.PassFailScoreText + "</div>");
                $('#exam_assessment_result_content').find('h3').show();
            } else {
                $('#exam_assessment_result_content').find('h3').hide();
            }

            $('#exam_assessment_result').show();
            $('#exam_assessment_result_content').show();

            $('#assessment_result_content').hide();
            $('#assessment_result').hide();

        } else {
            $('#assessment_result').find('h1').html(objectScoreSummary.AssessmentScoreSummary.HeadingAssesmentScoreSummaryText);
            $('#assessment_result').find('h4').html(objectScoreSummary.AssessmentScoreSummary.ContentAssesmentScoreSummaryText);
            $('#assessment_result').css('backgroundImage', 'url(' + objectScoreSummary.AssessmentScoreSummary.ImageAssessmentSummaryScoreUrl + ')');
            $('#assessment_result_content').find('h2').html(objectScoreSummary.AssessmentScoreSummary.PercentageScoreText);
            if (objectScoreSummary.AssessmentScoreSummary.PassFailScoreText != null) {
                $('#assessment_result_content').find('h3').html("<div style='margin-left: 10px;'>" + objectScoreSummary.AssessmentScoreSummary.PassFailScoreText + "</div>");
                $('#assessment_result_content').find('h3').show();
            } else {
                $('#assessment_result_content').find('h3').hide();
            }
            $('#exam_assessment_result').hide();
            $('#exam_assessment_result_content').hide();
            $('#assessment_result_content').show();
            $('#assessment_result').show();
        }



        //$('#assessment_result_content').find('h3').css('backgroundImage', 'url(' + objectScoreSummary.AssessmentScoreSummary.ImagePassFailScoreURL + ')');



        $('#assessment_result_container').find('#assessment_score_Summary_subheading').html('');
        $('#assessment_result_container').find('#assessment_score_Summary_subheading').html(objectScoreSummary.AssessmentScoreSummary.AssessmentType);


        /*
        if (objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore != null && objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore != undefined) {
        $('#IndividualScoreHeading').html(objectScoreSummary.AssessmentScoreSummary.IndividualScoreHeading);
        $('#IndividualScoreButtons').show();
        this.ShowIndividualQuestionScore(objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore);
        }
        else {
        $('#IndividualScoreHeading').show();
        $('#IndividualScoreContent').show();
        $('#IndividualScoreButtons').show();
        $('#IndividualScoreContainer').show();


	        $('#IndividualScoreHeading').hide();
        $('#IndividualScoreContent').hide();
        $('#IndividualScoreButtons').hide();
        $('#IndividualScoreContainer').hide();

	    }
        */

        if (isExam) {
            $('#TopicScoreChartHeading').html(objectScoreSummary.AssessmentScoreSummary.HeadingTopicScoreChart);
            $('#TopicScoreChartFooter').hide();

            // remove all rows

            $('#TopicScoreChartContentTable tbody').empty();
            var tableTRs = "";

            if (objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries != null) {
                for (var i = 0; i < objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries.length; i++) {

                    var topicScoreSummary = objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries[i].TopicScoreSummary;

                    var topicName = topicScoreSummary.TopicName;
                    var newTopicName = "";

                    if (topicName.length > 60) {
                        for (var j = 0; j < topicName.length; ) {

                            if ((topicName.length - j) <= 6) {
                                newTopicName = newTopicName + topicName.substring(j);
                                break;
                            }
                            var spaceFound = topicName.indexOf(' ', j + (60 - 10));
                            if (spaceFound < 0) {
                                newTopicName = newTopicName + (j > 0 ? "<BR>" : "") + topicName.substring(j);
                                break;
                            } else {
                                newTopicName = newTopicName + (j > 0 ? "<BR>" : "") + topicName.substring(j, spaceFound);
                                j = spaceFound;
                            }

                        }
                    } else {
                        newTopicName = topicName;
                    }

                    tableTRs += "<tr>";
                    tableTRs += "<td class='topic' nowrap='true'><label>" + newTopicName + "</label></td>";
                    tableTRs += "<td class='bar'><div class='barchart-container'><div style='width:" + topicScoreSummary.AchievedScore + "%;' class='barchart'></div></div><div class='barchart-text'>" + topicScoreSummary.AchievedScore + "%</div></td>";
                    tableTRs += "</tr>";
                }
            }
            $('#TopicScoreChartContentTable tbody').append(tableTRs);


            if (topicName != null) {
                if (topicName.length > 0) {
                    $('#TopicScoreChartContainer').show();
                }
            }
            else {
                $('#TopicScoreChartContainer').empty();
                $('#TopicScoreChartContainer').hide();
            }
            
            if(isShowGraph==false)
            {
                $('#TopicScoreChartContainer').hide();
            }
            // LCMS-7839
            // -----------------------------------------------
            setTimeout(function() { $('#TopicScoreChartHeading').show(); $('#TopicScoreChartContent').show(); }, 0);
            // -----------------------------------------------

            /*Start Adjust Topic chart table width - start*/
            /*
            var tableOffsetWidth = document.getElementById("TopicScoreChartContentTable").offsetWidth;
            var fiftyPercentOfTableWidth = tableOffsetWidth / 2;
            var topicCSSWidth = 10;

	        $('#TopicScoreChartContentTable tbody tr td[class="topic"] label').each(function(i, o) {
            var percentWidth = (($(o).width() * 100) / tableOffsetWidth);

	            if (percentWidth >= 45) {
            topicCSSWidth = 45;
            } else if (percentWidth > topicCSSWidth) {
            topicCSSWidth = percentWidth;
            }

	        });
            
            //$('#TopicScoreChartContentTable tbody tr td[class="topic"]').css("width", topicCSSWidth + "%");

	        if (topicCSSWidth >= 45) {
            $('#TopicScoreChartContentTable tbody tr td[class="topic"]').css("whiteSpace", "pre-line");
            }
            */
            /*Start Adjust Topic chart table width - end*/
        } else {

            $('#TopicScoreChartContainer').hide();
            $('#IndividualScoreHeaderText').empty();
            $('#IndividualScoreHeaderText').hide();
            $('#TopicScoreChartFooter').hide();

            // LCMS-7839
            // -----------------------------------------------
            setTimeout(function() { $('#TopicScoreChartHeading').hide(); $('#TopicScoreChartContent').hide(); }, 0);
            // -----------------------------------------------       

        }
        if (objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries != null) {
            var showIndividualQuestionsScore = isExam == true ? objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries[0].TopicScoreSummary.ShowIndividualQuestionScore : objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore;
        }
        else {
            var showIndividualQuestionsScore = isExam == true ? objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore : objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore;
        }
        //check the policy is on for Show Individual Questions and Answer, if present it means policy is ON.

        if (isExam && showIndividualQuestionsScore != null && showIndividualQuestionsScore != undefined) {
            $('#IndividualScoreHeaderText').html(objectScoreSummary.AssessmentScoreSummary.IndividualScoreHeaderText);
            $('#IndividualScoreHeaderText').hide();
        } else {
            $('#IndividualScoreHeaderText').hide();
        }

        if (showIndividualQuestionsScore != null && showIndividualQuestionsScore != undefined) {

            $('#IndividualScoreHeading').html(objectScoreSummary.AssessmentScoreSummary.IndividualScoreHeading);
            $('#IndividualScoreButtons').show();
            if (isExam) {
                if (objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries != null) {
                    this.ShowTopicIndividualQuestionScore(objectScoreSummary.AssessmentScoreSummary.ShowTopicScoreSummaries);
                }
            }
            else {
                this.ShowIndividualQuestionScore(objectScoreSummary.AssessmentScoreSummary.ShowIndividualQuestionScore);
            }
        }
        else {

            $('#IndividualScoreHeading').show();
            $('#IndividualScoreContent').show();
            $('#IndividualScoreButtons').show();
            $('#IndividualScoreContainer').show();

            setTimeout(function() {
                $('#IndividualScoreContainer').hide();
                $('#IndividualScoreHeading').hide();
                $('#IndividualScoreContent').hide();
                $('#IndividualScoreButtons').hide();
            }, 2);


        }

        //alert(showIndividualQuestionsScore.length);
        $(NextQuestionButtonEn).bind('click.namespace', function() {
            $('#assessment_result_container').hide();
            $(NextQuestionButtonEn).unbind('click.namespace');                       
            ContinueAfterAssessmentScore();
            //continueWithCourse();

        }); // End NextQuestionButtonEn Handler
    }


    // Rendering Methods For Remediation Start

    this.RemediationRenderTrueFalse = function(arrAnswers, arrStudentAnswers) {
        var dynamicHtml = "";
        for (i = 0; i < arrAnswers.length; i++) {
            //<li><input type="radio" id="one" name="q1" /><label for="one">One</label></li>
            if (arrAnswers[i].AssessmentItemAnswerID == arrStudentAnswers.AnswerIDs[0]) {
                //dynamicHtml += "<li><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' checked='checked' disabled='disabled' /><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + RenderTrueFalseString(arrAnswers[i].Value) + "</label></li>";
                  dynamicHtml +="<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' checked='checked' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' /><span>" + RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>" ;
            }
            else {
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' disabled='disabled' /><span>" + RenderTrueFalseString(arrAnswers[i].Value) + "</span><i></i></label></div>";
            }
        }
        //$('#question').find('ul').html(dynamicHtml);
        $('#QuestionRemediationQuestion').html("<div class='scene-content'>" + dynamicHtml + "</div>");

    } // function RenderTrueFalse end

    this.RemediationRenderSingleSelectMcq = function(arrAnswers, arrStudentAnswers) {
        var selectedMcq = arrStudentAnswers.AnswerIDs[0];
        var dynamicHtml = "";

        for (i = 0; i < arrAnswers.length; i++) {
            if (arrAnswers[i].AssessmentItemAnswerID == selectedMcq) {
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' disabled='disabled' checked='checked' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
            }
            else {
                dynamicHtml += "<div class='scene-option radio'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='radio' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='radioButton' value='" + arrAnswers[i].Value + "' disabled='disabled' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
            }
        }
        //$('#question').find('ul').html(dynamicHtml);
        $('#QuestionRemediationQuestion').html("<div class='scene-content'>" + dynamicHtml + "</div>");

    } // Function RenderSingleSelectMcq end

    this.RemediationRenderMultiSelectMcq = function(arrAnswers, arrStudentAnswers) {
        /*var dynamicHtml = "";
       
	
		for(i=0; i<arrAnswers.length; i++){
			
			dynamicHtml +=  "<li><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' disabled='disabled' checked='checked'/><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Value + "</label></li>";
			
		}*/



        var dynamicHtml = "";
        var drawn = false;
        for (i = 0; i < arrAnswers.length; i++) {
            drawn = false;
            for (n = 0; n < arrStudentAnswers.AnswerIDs.length; n++) {
                if (arrAnswers[i].AssessmentItemAnswerID == arrStudentAnswers.AnswerIDs[n]) {
                    dynamicHtml += "<div class='scene-option checkbox'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' disabled='disabled' checked='checked' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
                    drawn = true;
                    break;
                }

            }
            if (drawn == false)
                dynamicHtml += "<div class='scene-option checkbox'><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' disabled='disabled' /><span>" + arrAnswers[i].Label + "</span><i></i></label></div>";
        }


        /*for(i=0;i<arrAnswers.length;i++)
        {
        if(arrAnswers[i].AssessmentItemAnswerID == arrStudentAnswers.AnswerIDs[n])
        {
				
				dynamicHtml +=  "<li><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' disabled='disabled' checked='checked' /><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Value + "</label></li>";
				
				}else {
				
				dynamicHtml +=  "<li><input type='checkbox' id='" + arrAnswers[i].AssessmentItemAnswerID + "' name='checkbox' value='" + arrAnswers[i].Value + "' disabled='disabled' /><label for='" + arrAnswers[i].AssessmentItemAnswerID + "'>" + arrAnswers[i].Value + "</label></li>";
				
				}
		
			
        }  */
        //$('#question').find('ul').html(dynamicHtml);
        $('#QuestionRemediationQuestion').html("<div class='scene-content'>" + dynamicHtml + "</div>");


    }

    this.RemediationRenderImageTargetQuestion = function(ImageURL, arrAnswers, arrStudentAnswers, templateType) {

        var img = new Image();
        img.src = ImageURL;

        var dynamicHtml = "<div id=\"ImageDiv\" style=\"width:" + img.width + "px;height:" + img.height + "px;\">";

        for (i = 0; i < arrAnswers.length; i++) {
            if (arrStudentAnswers.AnswerIDs[0] == arrAnswers[i].AssessmentItemAnswerID) {
                dynamicHtml += "<div class=\"HotSpot\" id=\"" + arrAnswers[i].AssessmentItemAnswerID + "\" style=\"left:" + arrAnswers[i].ImageTargetCoordinates[0].XPos + "px; top:" + arrAnswers[i].ImageTargetCoordinates[0].YPos + "px;width:" + arrAnswers[i].ImageTargetCoordinates[0].Width + "px;height:" + arrAnswers[i].ImageTargetCoordinates[0].Height + "px;\">";
                dynamicHtml += "<div class=\"redBorder\" id=\"hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "\">";
                dynamicHtml += "<a href=\"javascript:void(0)\" );\"><img src='" + ImageURL + "' style=\"left:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].XPos + 3) + "px; top:-" + eval(arrAnswers[i].ImageTargetCoordinates[0].YPos + 3) + "px;\"/> </a>";
                dynamicHtml += "</div>";
                dynamicHtml += "</div>";
            }
        }

        dynamicHtml += "<img src='" + ImageURL + "' class=\"imageClass\" / >";
        dynamicHtml += "</div>";

        //For assessmentitem template change
        //$('#question').html(dynamicHtml);
        if (templateType != "Text Only")
            $('#QuestionRemediationContainer').find('#assessmentImage').replaceWith(dynamicHtml);
        else
            $('#QuestionRemediationQuestion').html(dynamicHtml);
    } // End ImageTargetQuestion

    this.RemediationRenderFillInTheBlanks = function(questionStem, arrAnswers, arrStudentAnswers) {
        /*
        var value = "";
        if(arrStudentAnswers.AnswerTexts.length > 0)
        {
        value = arrStudentAnswers.AnswerTexts[0];
        }
        */
        if (arrStudentAnswers.AnswerTexts.length > 0) {
            for (var index = 0; index < arrStudentAnswers.AnswerTexts.length; index++) {
                questionStem = questionStem.replace("&lt;blank&gt;", "<input type='textbox' class='form-control' id='" + (index + 1) + "' value='" + arrStudentAnswers.AnswerTexts[index] + "' disabled='disabled'>");
            }
        }
        else {
            questionStem = questionStem.replace(/&lt;blank&gt;/g, "<input type='textbox' class='form-control' id='A1' value=''>");
        }

        var dynamicHtml = questionStem;
        //var dynamicHtml = questionStem.replace("&lt;blank&gt;","<input type='textbox' id='1' value='" + value + "' disabled='disabled'>");

        //LCMS-5770 - Start
        //$('#QuestionRemediationContainerColumn1').find('h3').html(dynamicHtml);
        $('#QuestionRemediationContainerColumn1').find('#QuestionRemediationQuestionStem').html(dynamicHtml);
        //LCMS-5770 - End

        $('#QuestionRemediationQuestion').empty();
    }

    this.RemediationRenderOrdering = function(arrAnswers, arrStudentAnswers) {


        //var dynamicHtml = "<div id='orderselect'><select name='ListBox' id='ListBox' size='5'>";
        var dynamicHtml = "<div class='scene-content scene-ordering'>";

        if (arrStudentAnswers.AnswerIDs.length > 0) {
            for (x = 0; x < arrAnswers.length; x++) {
                var studentOrder = arrStudentAnswers.AnswerIDs[x];

                for (i = 0; i < arrAnswers.length; i++) {
                    //alert("x = " + i + " Display Order= " + arrAnswers[i].DisplayOrder);
                    if (arrAnswers[i].AssessmentItemAnswerID == studentOrder) {                        
                        dynamicHtml += '<div class="scene-option"><div class="ordering-btn" data-value="' + arrAnswers[i].AssessmentItemAnswerID + '"><a href="javascript:;" title="Move Up" class="up disabled"><i class="glyphicon glyphicon-arrow-up"></i></a><a href="javascript:;" title="Move Down" class="down disabled"><i class="glyphicon glyphicon-arrow-down"></i></a></div><span>'+ arrAnswers[i].Label +'</span></div>';
                        //dynamicHtml += "<option value='" + arrAnswers[i].AssessmentItemAnswerID + "' disabled='disabled'>" + arrAnswers[i].Label + "</option>";
                        //alert(dynamicHtml);
                    }
                }
            }
        }
        else {
            for (var i = 0; i < arrAnswers.length; i++) {
                dynamicHtml += '<div class="scene-option"><div class="ordering-btn" data-value="' + arrAnswers[i].AssessmentItemAnswerID + '"><a href="javascript:;" title="Move Up" class="up disabled"><i class="glyphicon glyphicon-arrow-up"></i></a><a href="javascript:;" title="Move Down" class="down disabled"><i class="glyphicon glyphicon-arrow-down"></i></a></div><span>'+ arrAnswers[i].Label +'</span></div>';
            }
        }
        dynamicHtml += "</div>";
        //dynamicHtml += "<div id='orderbuttons'><p><button class='button' disabled='disabled' onclick='ListElementsMoveUp();'>▲</button></p>";
        //dynamicHtml += "<p><button class='button' disabled='disabled' onclick='ListElementsMoveDown();'>▼</button></p></div>";



        //alert(dynamicHtml);

        $('#QuestionRemediationQuestion').html(dynamicHtml);

    } // End Render Ordering


//    this.RemediationRenderMatching = function(arrAnswers, arrStudentAnswers, arrStudentAnswersText) {
//        //alert(arrAnswers+" : "+arrStudentAnswers+" : "+arrStudentAnswersText);

//        $('#QuestionRemediationQuestion').empty();


//        // omair code
//        var dynamicHtml = "<div id=\"matchingRemediation\"><div id=\"dropContainerRemediation\">";
//        for (var i = 0; i < arrAnswers.length; i++) {
//            dynamicHtml += "<div class=\"dropRemediation\" title=\"Target A\" id='drop-" + i + "'>";
//            dynamicHtml += "<span>" + arrAnswers[i].LeftItemText + "</span>";

//            if (arrStudentAnswersText.length > 0) { // LCMS-12442  Abdus Samad

//            if (arrStudentAnswers.AnswerTexts[i] != "null") {
//             
//                    dynamicHtml += "<div class=\"dragRemediation\"><span>" + arrStudentAnswers.AnswerTexts[i] + "</span></div>";
//                }

//            }      
//                          
//            dynamicHtml += "</div>";

//        }

//        dynamicHtml += "</div><div id=\"dragContainerRemediation\">";

//        for (var x = 0; x < arrAnswers.length; x++) {
//            if (arrStudentAnswers.AnswerTexts[x] == "null") {
//                dynamicHtml += "<div class=\"dragRemediation\" title=\"Div 1\" id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
//                dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
//                dynamicHtml += "</div>";
//            }
//            //LCMS-12442
//            //Abdus Samad
//            //Start
//            if (arrStudentAnswersText[x] == undefined) {
//                dynamicHtml += "<div class=\"dragRemediation\" title=\"Div 1\" id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
//                dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
//                dynamicHtml += "</div>";
//            }
//            //Stop


//        }

//        dynamicHtml += "</div></div>";

//        $('#QuestionRemediationQuestion').append(dynamicHtml);

//    }   // End Render matching

    this.RemediationRenderMatching = function(arrAnswers, arrStudentAnswers, arrStudentAnswersText) {
        //alert(arrAnswers+" : "+arrStudentAnswers+" : "+arrStudentAnswersText);
        $('#QuestionRemediationQuestion').empty();
        var dynamicHtml = "<div class='scene-cell' id='dropable-holders'>";
        for (var i = 0; i < arrAnswers.length; i++)
        {
	        dynamicHtml += "<div class=\"cd-drop-holder\" title=\"Target A\" id='drop-" + i + "'>";
	        dynamicHtml += "<span>" + arrAnswers[i].LeftItemText + "</span>";

	        if (arrStudentAnswersText.length > 0) { // LCMS-12442  Abdus Samad

	        if (arrStudentAnswers.AnswerTexts[i] != "null") {        	 
			        dynamicHtml += "<div class=\"cd-drag-btn\"><span>" + arrStudentAnswers.AnswerTexts[i] + "</span></div>";		        
			        }
	        }
	        dynamicHtml += "</div>";
        }
        dynamicHtml += 	'</div>'+
				        '<div class="scene-cell" id="draggable-btns">'+
					        '<div class="scene-content">';
        					
        for (var x = 0; x < arrAnswers.length; x++)
        {
	        if (arrStudentAnswers.AnswerTexts[x] == "null") {
		        dynamicHtml += "<div class=\"cd-drag-btn\" title=\"Div 1\" id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
		        dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
		        dynamicHtml += "</div>";
	        }
	        //LCMS-12442
	        //Abdus Samad
	        //Start
	        if (arrStudentAnswersText[x] == undefined) {
		        dynamicHtml += "<div class=\"cd-drag-btn\" title=\"Div 1\" id='" + arrAnswers[x].AssessmentItemAnswerID + "'>";
		        dynamicHtml += "<span>" + arrAnswers[x].RightItemText + "</span>";
		        dynamicHtml += "</div>";
	        }
	        //Stop
        }
        dynamicHtml += "</div></div>";
        $('#QuestionRemediationQuestion').append(dynamicHtml);

    }   // End Render matching

    this.QuestionRemediationRendering = function(objectQuestions) {
        //LCMS-8188 - Start
        if (isBrowserVersionIE6 == true) {
            var mainCont = $('#QuestionRemediationContainer');
            if (mainCont.css('position') != 'relative') {
                mainCont.css('position', 'relative');
            }
        }
        //LCMS-8188 - End

        //setting template
        $('#QuestionRemediationContainer').show();
        $('#AssessmentInProgress').hide();

        $('#QuestionRemediationContainer').find('#questionRemediationTemplate').empty();
        $(quiz_container).find('#assessmentQuestionTemplate').empty();
        $('#questionRemediationTemplate').html(objectQuestions.AssessmentItem.TemplateHTML);
        $('#questionRemediationTemplate').find("#assessment-table").removeClass("scene-wrapper");
        $('#questionRemediationTemplate').find("#assessment-table").find('.scene-body').removeClass("scene-body");
        $('#questionRemediationTemplate').find("#assessment-table").removeClass("visual-left");
        //setting div for question logic
        //document.getElementById('divQuizColumn1').innerHTML='<div id="QuestionRemediationContainerColumn1"><!-- firstColumn --><h3>this is text</h3><div id="QuestionRemediationQuestion"></div></div>';
        document.getElementById('divQuizColumn1').innerHTML = '<div id="QuestionRemediationContainerColumn1"><!-- firstColumn --><div id="QuestionRemediationQuestionStem">this is text</div><div id="QuestionRemediationQuestion"></div></div>';
        document.getElementById('divIncorrectRemediation').innerHTML = '<div id="QuestionRemediationIncorrect">' + INCORRECT + '</div>';
        document.getElementById('divCorrectRemediation').innerHTML = '<div id="QuestionRemediationCorrect">' + CORRECT + '</div>';
        document.getElementById('divQuestionDescription').innerHTML = ' <div id="QuestionRemediationDescription"></div>';
        //set the div for either media or image
        $('#buttoncontainerAnswerReview').hide();
        $('#buttoncontainerAnswerReviewPage').hide();

        try {
            setMediaType(objectQuestions.AssessmentItem.VisualTopType, $('#QuestionRemediationContainer').find('#assessmentImage'), $('#QuestionRemediationContainer').find('#assessmentMedia'));
        }
        catch (e) {

        }
        $(NextQuestionButtonEn).unbind('click.namespace');

        jQuery().ready(function() {
            //alert('Showing divs');
            $(assessmentControlPanel).show();
            $(NextQuestionButtonEn).show();
            //$(BackQuestionButtonEn).show();
            $(BackQuestionButtonEn).removeClass("hide");


            $(controlPanel).find("#IcoInstructorInformation").hide();
            $(controlPanel).find("#IcoInstructorInformationDs").show();        

            $(controlPanel).find("#IcoTOC").hide();
            $(controlPanel).find("#IcoTOCDs").show();        

            $(controlPanel).find("#IcoGlossary").hide();
            $(controlPanel).find("#IcoGlossaryDs").show();        

            $(controlPanel).find("#IcoCourseMaterial").hide();
            $(controlPanel).find("#IcoCourseMaterialDs").show();        
                    
            $(controlPanel).find("#modal-trigger-bookmark").hide();
            $(controlPanel).find("#cd-tour-trigger").hide();

            $(controlPanel).find("#IcoConfigure").hide();
            $(controlPanel).find("#IcoConfigureDs").show();

            $(controlPanel).find("#IcoHelp").hide();
            $(controlPanel).find("#IcoHelpDs").show();

            $(controlPanel).find("#IcoCourseCompletion").hide();
            $(controlPanel).find("#IcoCourseCompletionDs").show();	
            
            $(controlPanel).find("#IcoRecommendationCoursePanel").hide();
            $(controlPanel).find("#IcoRecommendationCoursePanelDs").show();	
                    
            if ($('#controlPanel').is(':hidden') == true) {
                //$('#odometerContainter').css('background', 'url()');
            }

            //alert('Hide divs');
            // Hide Other div's
            $(gradeAssessment).hide();
            $('#assesmentcontainer').hide();
            $('#controlPanel').hide();
            $('#assessment_result_container').hide();
            $('#quiz_container').hide();
            $('#assesmentcontainer').hide();
            $('#AnswerReviewContainer').hide();
            $('#IndividualScoreContainer').hide();
            $('#correct').hide();
            $('#incorrect').hide();
            $('#questiondescription').hide();
            $('#questionfeedback').hide();
            $('#toogle-flag').addClass('hide'); //Added By Abdus Samad LCMS-12105
            $("#htmlContentContainer").hide();
            $("#ShowQuestionButton").hide();
            $("#timer").show();
            $("#QuestionRemediationButtons").show();
            $('#QuestionRemediationButtons').find("div").eq(1).find("button").show();


            $('#incorrect').css('backgroundImage', 'url("")');
            $('#correct').css('backgroundImage', 'url("")');

            if (objectQuestions.AssessmentItem.ContentRemidiationAvailable) {
                if (objectQuestions.AssessmentType == "PreAssessment")
                    $('#QuestionRemediationButtons').find("div").eq(0).find("button").hide();
                else
                    $('#QuestionRemediationButtons').find("div").eq(0).find("button").show();
            }
            else
                $('#QuestionRemediationButtons').find("div").eq(0).find("button").hide();

        });

        $('#QuestionRemediationContainer').show();

        var arrItems = objectQuestions.AssessmentItem;
        var arrAnswers = objectQuestions.AssessmentItem.AssessmentAnswers;
        var arrStudentAnswers = arrItems.StudentAnswer;
        var arrStudentAnswersText = objectQuestions.AssessmentItem.StudentAnswer.AnswerTexts;


        var assessmentItemId = arrItems.AssessmentItemID;

        var questionNo = arrItems.QuestionNo;
        var totalQuestions = arrItems.TotalQuestion;
        var questionType = arrItems.QuestionType;
        var questionStem = replaceSpacesAndLineBreaks(arrItems.QuestionStem);
        var questionFeedback = arrStudentAnswers.QuestionFeedBack;
        var answersLength = arrItems.AssessmentAnswers.length;
        var isCorrectlyAnswered = arrStudentAnswers.IsCorrectlyAnswered;
        var queCountStr = Question_text + " " + questionNo + " " + of + " " + totalQuestions;

        // Empty & Hide useless Div's
        $('#assessmentControlPanel').find('.question-counter').empty();

        //LCMS-5770 - Start
        //$('#QuestionRemediationContainerColumn1').find('h3').empty();
        $('#QuestionRemediationContainerColumn1').find('#QuestionRemediationQuestionStem').empty();
        //LCMS-5770 - End

        $('#QuestionRemediationDescription').empty();



        // Fill the Div's with data & show the div


        $('#assessmentControlPanel').find('.question-counter').html(queCountStr);

        //LCMS-5770 - Start
        //$('#QuestionRemediationContainerColumn1').find('h3').html(questionStem);
        $('#QuestionRemediationContainerColumn1').find('#QuestionRemediationQuestionStem').html(questionStem);
        //LCMS-5770 - End




        if (questionFeedback == "") {
            $('#QuestionRemediationDescription').hide();
        }
        else {
            $('#QuestionRemediationDescription').html("<strong>" + FeedbackICP4 + ":</strong> <br />" + questionFeedback);
            $('#QuestionRemediationDescription').show();
            
            document.getElementById('QuestionRemediationDescription').focus(); //508 Complainance      
        }

        if (isCorrectlyAnswered) {
            $('#QuestionRemediationCorrect').show();
            $('#QuestionRemediationIncorrect').hide();
        }
        else {
            $('#QuestionRemediationIncorrect').show();
            $('#QuestionRemediationCorrect').hide();
        }

        // Answers will go in this Div --> QuestionRemediationQuestion
        switch (questionType) {
            case "True False": // Generate radio buttons
                this.RemediationRenderTrueFalse(arrAnswers, arrStudentAnswers);
                break;

            case "Single Select MCQ":
                this.RemediationRenderSingleSelectMcq(arrAnswers, arrStudentAnswers);
                break;

            case "Multiple Select MCQ":
                this.RemediationRenderMultiSelectMcq(arrAnswers, arrStudentAnswers);
                break;

            case "Image Selection":
                this.RemediationRenderImageTargetQuestion(objectQuestions.AssessmentItem.ImageURL, arrAnswers, arrStudentAnswers, objectQuestions.AssessmentItem.TemplateType);
                break;

            case "Fill in the Blanks":
                this.RemediationRenderFillInTheBlanks(questionStem, arrAnswers, arrStudentAnswers);
                break;

            case "Ordering":
                this.RemediationRenderOrdering(arrAnswers, arrStudentAnswers);
                break;

            case "Matching":
                this.RemediationRenderMatching(arrAnswers, arrStudentAnswers, arrStudentAnswersText);
                break;

        }

        $(NextQuestionButtonEn).bind('click.namespace', function() {
            $(NextQuestionButtonEn).unbind('click.namespace');
            GetNextRemidiationQuestion();
        }); // End NextQuestionButtonEn Handler
        
        $(BackQuestionButtonEn).unbind('click.namespace');
        $(BackQuestionButtonEn).bind('click.namespace', function() {
            $(BackQuestionButtonEn).unbind('click.namespace');
            GetPreviousRemidiationQuestion();
            return false;
        }); // End NextQuestionButtonEn Handler

        // alert(assessmentItemId);
        //Binding Continue Grading Button 
        $('#QuestionRemediationButtons').find("div").eq(0).find("button").unbind('click.namespace');
        $('#QuestionRemediationButtons').find("div").eq(0).find("button").bind('click.namespace', function() {
            $('#QuestionRemediationButtons').find("div").eq(0).find("button").unbind('click.namespace');
            $(NextQuestionButtonEn).unbind('click.namespace');
            $('#QuestionRemediationButtons').hide();
            $('#QuestionRemediationContainer').hide();

            ShowContent(assessmentItemId);
            //$('#odometerContainter').css('background', 'url()');
            return false;
        });

        $('#QuestionRemediationButtons').find("div").eq(1).find("button").text(ReturntoAssessmentResultsText);
        $('#QuestionRemediationButtons').find("div").eq(1).find("button").bind('click.namespace', function() {
            $('#QuestionRemediationButtons').find("div").eq(1).find("button").unbind('click.namespace');
            $(NextQuestionButtonEn).unbind('click.namespace');
            $('#QuestionRemediationButtons').hide();
            $('#QuestionRemediationContainer').hide();
            //ContinueAfterAssessmentScore();   
            ui.slide.next(function()
            {
                cp.ReturnToAssessmentResults();
            });	                 
            return false;
        });
    }



    this.OpenCommand = function(openCommandObject) {

        var bool = openCommandObject.OpenCommandMessage.ContuniueAskingValidationQuestion;
        // ContuniueAskingValidationQuestion is true  than reset timer
        //alert('OpenCommand Timer:ContuniueAskingValidationQuestion'+bool);
        if (bool) {
            //alert('OpenCommand ResetTimer:'+bool);

            validationTimerObj.ResetTimer();
        } else {
            //alert('OpenCommand StopTimer:'+bool);

            validationTimerObj.StopTimer();
        }
        //alert('validationTimer and EnableValidationControlBar:'+bool);
        this.assessmentTimer("1");
        // assessmentTimerObj.ResumeTimer(); 
        this.EnableValidationControlBar(false);
        //alert('coming here or not');
        //visible tools buttons and hide validation button
        //ResumeCourseAfterValidation call this method 
    }







    this.LockCourse = function(lockCourseCommandObject) {
        jQuery().ready(function() {

            //$(lockCourseDialogue).fadeIn("slow");
            //$(overlay).css({"opacity": "0.7"});
            //$(overlay).fadeIn("slow");
            //$(lockCourseDialogue).find('h3').html(lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageHeading);
            //$(lockCourseDialogue).find('p').html(lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageText);

            //LCMS-10534
            $('#quiz_container').hide();
            $('#QuestionRemediationContainer').hide();
            //$(lockCourseDialogue).text(course_locked_title);




            //LCMS-LCMS-10572
            $("#AnswerReviewContainer").hide();
            $('#assesmentcontainer').hide();
            validationTimerObj.StopTimer();
            //validationTimerObj.PauseTimer();
            //lockCourseTimer.StopTimer();
            //this.EnableValidationControlBar(false);
            $("#ValidationControlBar").hide();
            $("#studentAuthentication").hide();

            // Changed made by Waqas Zakai 08-June-2010
            // LCMS-4147
            $(htmlContentContainer).show();
            $("#CARealStateValidation").hide();
            $("#NYInsuranceValidation").hide();
            $("#InstructorInformation").hide();
            $("#assessmentItemResult").hide();



            isMovieEnded = true;

            // Commented out for LCMS-10320
            // -------------------------------------------
            //			    var sCourseLockedMessageShortText=lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageText;			    
            //			    sCourseLockedMessageShortText=sCourseLockedMessageShortText.substring(0,sCourseLockedMessageShortText.indexOf('.') + 1);
            // -------------------------------------------

            //var htmlData = '<style>table {  height:100%;  width:100%;  margin:auto;  padding : 10px 10px 5px 10px}.mainimage {  max-width: 350px;  vertical-align:top;}.sceneTitle {  font-family : Tahoma, Arial, Verdana, Helvetica, sans-serif;   font-size : 30px;   text-decoration : bold;  vertical-align:top;}.sceneTextArea1 {  font-family : Arial;   font-size : 16px;  font:bold;  text-align: left; overflow: hidden;   }</style><!--style="text-align:justify;"--><table border="0" width="95%" height="100" align="center" valign="center" cellpadding="0" cellspacing="0" style="height:100px !important;"><tr><td valign="top" style="height:30px !important;"><table border="0" cellpadding="0" cellspacing="0"><tr><td width="10%"  valign="top"><img  class="mainimage" src="' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageImageUrl + '"/></td><td width="90%"  valign="top" style="padding-top:30px;"><span class="sceneTitle">' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageHeading + '</span>';
            
            var htmlData = '<section class="scene-wrapper visual-left"><div class="scene-body"><div class="scene-cell"><img class="img-responsive mainimage"  src="' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageImageUrl + '" /></div><div class="scene-cell"><h1 class="scene-title">' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageHeading + '</h1><div class="scene-content"><p>' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageText + '</p><p><div id="divStartCourseEvaluation"style="float:left; vertical-align:top; padding-left:8px; padding-top:30px;"><div id="btnUnlockMyCourse"><a href="#" class="cd-btn main-action button btn-stem">' + lockCourseCommandObject.CourseLockedMessage.ContentUnlockCourseButton + '</a></div></div></p></div></div></div></section>';



            // Commented out for LCMS-10320
            // -------------------------------------------
            //		        if ( (lockCourseCommandObject.CourseLockedMessage.LockType != 'ProctorNotPartOfCredential') && (lockCourseCommandObject.CourseLockedMessage.LockType != 'ProctorAccountNotActive') && (lockCourseCommandObject.CourseLockedMessage.LockType != 'ProctorLoginFailed') )
            //		        {
            //		            htmlData += '<br /><span class="sceneTextArea">'+sCourseLockedMessageShortText+'</span>';
            //		        }
            // -------------------------------------------

            //htmlData += '</td></tr></table></td></tr><tr><td colspan="3" valign="top" height="10%"><span class="sceneTextArea">' + lockCourseCommandObject.CourseLockedMessage.CourseLockedMessageText + '</span></td></tr><tr><td colspan="3" valign="top"><div id="divCourseLocked"style="float:left; vertical-align:top; padding-left:8px; padding-top:30px;"><div class="btn-start"></div><span id="btnUnlockMyCourse"><a class="btn-stem" href="#">' + lockCourseCommandObject.CourseLockedMessage.ContentUnlockCourseButton + '</a></span><div class="btn-end"></div></div></td></tr><tr><td colspan="3" valign="top"></td></tr><tr><td colspan="3" valign="top"></td></tr></table>';
            $(htmlContentContainer).html('');
            $(htmlContentContainer).html(htmlData);
            $(NextQuestionButtonEn).hide();

            $(controlPanel).find("#IcoInstructorInformation").hide();
            $(controlPanel).find("#IcoInstructorInformationDs").show();        

            $(controlPanel).find("#IcoTOC").hide();
            $(controlPanel).find("#IcoTOCDs").show();        

            $(controlPanel).find("#IcoGlossary").hide();
            $(controlPanel).find("#IcoGlossaryDs").show();        

            $(controlPanel).find("#IcoCourseMaterial").hide();
            $(controlPanel).find("#IcoCourseMaterialDs").show();             

            $(controlPanel).find("#modal-trigger-bookmark").hide();
            $(controlPanel).find("#cd-tour-trigger").hide();

            $(controlPanel).find("#IcoConfigure").hide();
            $(controlPanel).find("#IcoConfigureDs").show();

            $(controlPanel).find("#IcoHelp").hide();
            $(controlPanel).find("#IcoHelpDs").show();


            $(controlPanel).find("#IcoCourseCompletion").hide();
            $(controlPanel).find("#IcoCourseCompletionDs").show();

            $(PlaybuttonEn).hide();
            $(PlaybuttonEn).hide();
            $(BackbuttonEn).hide();
            $(PlaybuttonDs).show();
            $(BackbuttonDs).show();
            //$(logoutbutton+" a").hide();
            //$("#logoutbuttonDS").show();
            $("#ValidationTimer").hide();
            $(ValidationPlaybuttonEn).hide();

            if ((lockCourseCommandObject.CourseLockedMessage.LockType == 'MonitorFieldMisMatch') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'CourseApprovalNotAttachedWithCourse') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'ReportingFieldMisMatch') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'ReportingFieldNotAttachedWithCourseApproval') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'ProctorNotPartOfCredential') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'ProctorAccountNotActive') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'ProctorLoginFailed') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'MaxAttemptReach') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'MaxAttemptReachPostAssessment') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'MaxAttemptReachLessonAssessment') || (lockCourseCommandObject.CourseLockedMessage.LockType == 'MaxAttemptReachPreAssessment')) {
                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
            }

            if (lockCourseCommandObject.CourseLockedMessage.LockType == 'FailedCompletionMustCompleteWithinSpecificAmountOfTimeMinute' || lockCourseCommandObject.CourseLockedMessage.LockType == 'FailedCompletionMustCompleteWithinSpecificAmountOfTimeAfterRegistration') {
                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
                $(gradeAssessment).hide();
                $(assessmentTimer).hide();
                //$("#odometer").html('00 minute');
            }

            if (lockCourseCommandObject.CourseLockedMessage.LockType == 'CoursePublishedScene' || lockCourseCommandObject.CourseLockedMessage.LockType == 'CoursePublishedAssessment') {
                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
                $(gradeAssessment).hide();
                $(assessmentTimer).hide();
                $("#NextQuestionButtonEn").hide();
                //$("#BackQuestionButtonEn").hide();
                $("#BackQuestionButtonEn").addClass("hide");
                $("#QuestionRemediationButtons").hide();
                $(logoutbutton + " a").show();
                $("#logoutbuttonDS").hide();
                $('#assessmentControlPanel').hide();

                //$("#odometer").html('00 minute');
            }

            if (lockCourseCommandObject.CourseLockedMessage.LockType == 'ClickingAwayFromActiveWindow') {
                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
                $("#assessmentControlPanel").hide();
                $(gradeAssessment).hide();
                $(assessmentTimer).hide();
                $("#NextQuestionButtonEn").hide();
                //$("#BackQuestionButtonEn").hide();
                $("#BackQuestionButtonEn").addClass("hide");
                $("#QuestionRemediationButtons").hide();
                $(logoutbutton + " a").show();
                $("#logoutbuttonDS").hide();
                $("#assessmentControlPanel").hide();
                $("#assessmentTimer").hide();
                $("#controlBar").show();
                $("#controlPanel").show();
            }

            //LCMS-8422
            if (lockCourseCommandObject.CourseLockedMessage.LockType == 'MustStartCourseWithinSpecificAmountOfTimeAfterRegistration') {
                $(logoutbutton + " a").show();
                $("#logoutbuttonDS").hide();
                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
                $(gradeAssessment).hide();
                $(assessmentTimer).hide();
                //$("#odometer").html('00 minute');
            }

            //CourseLockedMessageImageUrl
            //cp.UnlockCourse();

            // Added by Mustafa for LCMS-2045
            // --------------------------------------------
            //   if(lockCourseCommandObject.CourseLockedMessage.RedirectURL!='')
            //  {   
            //     $("#lnkUnlock").attr("href", "javascript:cp.UnlockCourse();location.replace('" + lockCourseCommandObject.CourseLockedMessage.RedirectURL + "');");			        			        		        
            //}
            // --------------------------------------------
            //$(lockCourseDialogue).find("button").bind('click.namespace', function(){
            //	$(lockCourseDialogue).find("button").unbind('click.namespace');
            $("#btnUnlockMyCourse").find("a").unbind('click.namespace');
            $("#btnUnlockMyCourse").find("a").bind('click.namespace', function() {


                if (lockCourseCommandObject.CourseLockedMessage.LockType == 'ValidationFailed') {
                    if (lockCourseCommandObject.CourseLockedMessage.RedirectURL != '') {
                        cp.UnlockCourse();

                        // Replaced by Mustafa for LCMS-2045
                        //--------------------------------------------------------------------------
                        //window.location = lockCourseCommandObject.CourseLockedMessage.RedirectURL;
                        window.location = "DynamicPageLoader.aspx?url=" + lockCourseCommandObject.CourseLockedMessage.RedirectURL;
                        //--------------------------------------------------------------------------

                    }

                }

                if (lockCourseCommandObject.CourseLockedMessage.LockType == 'MaxAttemptReach') {
                    var courseDiv = document.getElementById('lockCourseDialogue');
                    courseDiv.style.display = "none";
                    //$(overlay).fadeOut("slow");
                    continueWithCourse();
                }





                //cp.SendLogoutMessage();
                return false;
            });


            if (lockCourseCommandObject.CourseLockedMessage.LockType == 'IdleUserTimeElapsed') {

                $("#btnUnlockMyCourse").hide();
                $("#divCourseLocked").hide();
                $(gradeAssessment).hide();
                $(assessmentTimer).hide();

            }

        });
    }

    this.AssessmentItemResult = function(AssessmentItemResultCommandObject) {
        jQuery().ready(function() {          
            
            var htmlData = AssessmentItemResultCommandObject.AssessmentResultMessage.TemplateassessmentResult;
            var chartData=AssessmentItemResultCommandObject.AssessmentResultMessage.TemplateChartData;
            htmlData=htmlData.replace("<CHARTDATA />",chartData);
            
            $("#assessmentItemResult").show();
            $("#assessmentItemResult").html('');
            $("#assessmentItemResult").html(htmlData);  
            
            $("#assessmentControlPanel").show();
            $("#NextQuestionButtonEn").show();
            $("#htmlContentContainer").hide();
            
            $(NextQuestionButtonEn).bind('click.namespace', function() {                        
                        $('#assessmentItemResult').hide();
                        $(NextQuestionButtonEn).unbind('click.namespace');
                        ContinueAfterAssessmentScore();
            });
        });
    }


    this.CourseApproval = function(CourseApprovalCommandObject) {
        jQuery().ready(function() {

            $("#CourseApproval").show();
            isMovieEnded = true;
            var htmlData = CourseApprovalCommandObject.CourseApprovalMessage.TemplatecourseApproval;

            var CourseName = CourseApprovalCommandObject.CourseApprovalMessage.CourseName;
            $(heading).find("#courseName").html(CourseName);

            $("#CourseApproval").html(htmlData);
            $(NextQuestionButtonEn).hide();

            $(controlPanel).find("#IcoInstructorInformation").hide();
            $(controlPanel).find("#IcoInstructorInformationDs").show();        

            $(controlPanel).find("#IcoTOC").hide();
            $(controlPanel).find("#IcoTOCDs").show();        

            $(controlPanel).find("#IcoGlossary").hide();
            $(controlPanel).find("#IcoGlossaryDs").show();        

            $(controlPanel).find("#IcoCourseMaterial").hide();
            $(controlPanel).find("#IcoCourseMaterialDs").show();               
                
            $(controlPanel).find("#modal-trigger-bookmark").hide();
            $(controlPanel).find("#cd-tour-trigger").hide();

            $(controlPanel).find("#IcoConfigure").hide();
            $(controlPanel).find("#IcoConfigureDs").show();

            $(controlPanel).find("#IcoHelp").hide();
            $(controlPanel).find("#IcoHelpDs").show();


            $(controlPanel).find("#IcoCourseCompletion").hide();
            $(controlPanel).find("#IcoCourseCompletionDs").show();

            $(PlaybuttonEn).hide();
            $(PlaybuttonEn).hide();
            $(BackbuttonEn).hide();
            $(PlaybuttonDs).show();
            $(BackbuttonDs).show();
            $("#ValidationTimer").hide();
            $(ValidationPlaybuttonEn).hide();
            $("#btnUnlockMyCourse").hide();
            $("#divCourseLocked").hide();
            $(gradeAssessment).hide();
            $(assessmentTimer).hide();
            $("#NextQuestionButtonEn").hide();
            //$("#BackQuestionButtonEn").hide();
            $("#BackQuestionButtonEn").addClass("hide");
            $("#QuestionRemediationButtons").hide();
            $(logoutbutton + " a").show();
            $("#logoutbuttonDS").hide();
            $('#assessmentControlPanel').hide();
            $('#swfContainer').hide();
            $('#EOCText').hide();
            $('#EOCInstructions').hide();
            $('#content').hide();

            $("#divCourseapproval").find("a").unbind('click.namespace');
            $("#divCourseapproval").find("a").attr("class", "btn-stem-ds");
            $("#btnstart").attr("class", "btn-start-ds");
            $("#btnend").attr("class", "btn-end-ds");
            /*
            $("#CourseApproval").find("a").bind('click.namespace', function(){				

                      courseApproval=true;
            cp.init();
            return false;
            });
            */

            $("#courseapprovaldatatable").multicolselect({
                buttonImage: "images/selectbutton.gif",
                valueCol: 4,
                hideCol: 4
            });

        });
    }

    this.ShowCourseApprovalAffidavit = function(CourseApprovalAffidavitCommandObject) {
        //For LCMS-11217
        // debugger;
        $(controlPanel).show();
        $("#controlPanel").show();
        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();
        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();

        $(controlPanel).find("#IcoBookMark").hide();
        $(controlPanel).find("#IcoBookMarkDs").show();



        if (!CourseApprovalAffidavitCommandObject.Affidavit.IsDocuSignAffidavit) {
            $(htmlContentContainer).show();
            $('#NYInsuranceValidation').hide();
            $('#CARealStateValidation').hide();

            var htmlData = "";
            var checkResult = false;
            var downloadResult = false;

            isMovieEnded = true;
            htmlData = CourseApprovalAffidavitCommandObject.Affidavit.TemplateHtml;

            $(htmlContentContainer).html(htmlData);

            isMovieEnded = true;


            $(IAgreeButton).find("a").attr("target", "_blank");
            $('<div id="PlaybuttonDocuSign"><a href="#" title="Next"></a></div>').insertAfter(PlaybuttonEn);
            $('#PlaybuttonDocuSign').bind('click.namespace', function() {
                ui.slide.next(function()
                {
                   cp.ContinueAfterAffidavit(function() {                
                    $('#PlaybuttonDocuSign').remove();
                    });
                });	
            });
            $('#PlaybuttonDocuSign').hide();

            //$(ContinueButton).find("a").unbind('click.namespace');
            //$(ContinueButton).find("a").attr("class","btn-stem-ds");
            //$("#btnstart").attr("class","btn-start-ds");
            //$("#btnend").attr("class","btn-end-ds");
            //			    $(PlaybuttonEn).hide();


            //$(IAgreeButton).find("a").attr("class","btn-stem-ds");
            $(ContinueButton).hide();
            $('#btnstart').hide();
            $("#btnend").hide();
            $(".affidavitCheckbox #Checkbox1").bind('click.namespace', function() {
                // debugger;
                if ($(".affidavitCheckbox #Checkbox1").is(":checked")) {
                    checkResult = true;
                    if (downloadResult == true) {

                        $('#PlaybuttonDocuSign').show();
                        $(PlaybuttonDs).hide();

                    }

                }
                else {
                    checkResult = false;
                    $('#PlaybuttonDocuSign').hide();
                    $(PlaybuttonDs).show();
                }

            });
            $(IAgreeButton).find("a").bind('click.namespace', function() {
                courseApprovalAffidavitClick(CourseApprovalAffidavitCommandObject.Affidavit.AffidavitURL);
                downloadResult = true;

                if (checkResult == true) {
                    //					    $(PlaybuttonEn).show();
                    //                        $(PlaybuttonDs).hide();
                    //                        $(BackbuttonEn).hide();
                    //                        $(BackbuttonDs).show();
                    $('#PlaybuttonDocuSign').show();
                    $(PlaybuttonDs).hide();

                }
                //$(ContinueButton).find("a").unbind('click.namespace');
                // $(ContinueButton).find("a").attr("class","btn-stem");
                //$("#btnstart").attr("class","btn-start");
                // $("#btnend").attr("class","btn-end");	


                //			        $(ContinueButton).find("a").bind('click.namespace', function()
                //			        {
                //                        cp.ContinueAfterAffidavit();
                //					    return false;
                //				    });			    
                return false;
            });
            $("#NextQuestionButtonEn").hide();
            $(controlPanel).show();
            $("#controlPanel").show();
            //$('#odometerContainter').css('background', 'url(images/ctrl_ver_devider.gif) no-repeat');
            //                $(PlaybuttonEn).hide();
            //                $(PlaybuttonEn).hide();
            //                $(PlaybuttonDs).show();
            //                $(BackbuttonEn).hide();
            //                $(BackbuttonDs).show();	
        }
        else {
            $(htmlContentContainer).show();
            $('#NYInsuranceValidation').hide();
            $('#CARealStateValidation').hide();

            var htmlData = "";

            isMovieEnded = true;
            htmlData = CourseApprovalAffidavitCommandObject.Affidavit.TemplateHtml;

            $(htmlContentContainer).html(htmlData);



            $('<div id="PlaybuttonDocuSign"><a href="#" title="Next"></a></div>').insertAfter(PlaybuttonEn);
            $('#PlaybuttonDocuSign').bind('click.namespace', function() {
                cp.ContinueAfterAffidavit(function() {
                    $('#PlaybuttonDocuSign').remove();
                });
            });
            $('#PlaybuttonDocuSign').hide();

            //For LCMS-11217 (LCMS-11282)
            $(".affidavitCheckbox #Checkbox1").bind('click.namespace', function() {
                // debugger;
                if ($(".affidavitCheckbox #Checkbox1").is(":checked")) {
                    $('#PlaybuttonDocuSign').show();
                    $(PlaybuttonDs).hide();
                    //cp.ContinueAfterAffidavit();

                }
                else {
                    $('#PlaybuttonDocuSign').hide();
                    $(PlaybuttonDs).show();

                }
            });
            //End
        }
        //End	
    }

    //LCMS-11281
    this.ShowDocuSignRequirementAffidavit = function(DocuSignRequirementAffidavitCommandObject) {

        $(htmlContentContainer).show();
        $('#NYInsuranceValidation').hide();
        $('#CARealStateValidation').hide();

        var htmlData = "";

        isMovieEnded = true;
        htmlData = DocuSignRequirementAffidavitCommandObject.Affidavit.TemplateHtml;

        $(htmlContentContainer).html(htmlData);

        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();

        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).hide();

        $('<div id="PlaybuttonDocuSign"><a href="#" title="Next"></a></div>').insertAfter(PlaybuttonEn);
        $('#PlaybuttonDocuSign').bind('click.namespace', function() {
            $('#PlaybuttonDocuSign').remove();
            cp.ContinueAfterDocuSignRequirementAffidavit(function() {

            });
        });
    }
    //End

    //LCMS-11283
    this.ShowDocuSignProcess = function(ShowDocuSignProcessCommandObject) {
        //debugger;

        $(htmlContentContainer).show();
        $('#InstructorInformation').hide();

        var htmlData = "";

        isMovieEnded = true;
        htmlData = ShowDocuSignProcessCommandObject.Affidavit.TemplateHtml;
        var sceneText = (ShowDocuSignProcessCommandObject.Affidavit.SceneText + '').split('|');
        var sceneText1 = sceneText[0];
        var sceneText2 = sceneText[1];
        var sceneText3 = sceneText[2];
        var sceneText4 = sceneText[3];

        $(htmlContentContainer).html(htmlData);

        $(controlPanel).show();
        $("#controlPanel").show();

        $(BackbuttonEn).hide();
        $(BackbuttonDs).show();

        $(PlaybuttonEn).hide();
        $(PlaybuttonDs).show();

        $(controlPanel).find("#IcoInstructorInformation").hide();
        $(controlPanel).find("#IcoInstructorInformationDs").show();        

        $(controlPanel).find("#IcoTOC").hide();
        $(controlPanel).find("#IcoTOCDs").show();        

        $(controlPanel).find("#IcoGlossary").hide();
        $(controlPanel).find("#IcoGlossaryDs").show();        

        $(controlPanel).find("#IcoCourseMaterial").hide();
        $(controlPanel).find("#IcoCourseMaterialDs").show();

        $('#btnSubmit').attr("disabled", 'disabled');

        $('<div id="PlaybuttonDocuSign"><a href="#" title="Next"></a></div>').insertAfter(PlaybuttonEn);
        $('#PlaybuttonDocuSign').bind('click.namespace', function() {
            // debugger;
            // ------------------------------------
            cp.ContinueAfterDocuSignProcess(function() {
                $('#PlaybuttonDocuSign').remove();
                //$('#btnStartDocuSignProcess').attr('disabled', 'disabled');
                //$(controlPanel).hide();
            });
        });
        $('#PlaybuttonDocuSign').hide();

        //For LCMS-11217 (LCMS-11282)
        $(".startDocuSignProcess").bind('click.namespace', function() {
            //debugger;         
            $(controlPanel).find("#IcoInstructorInformation").hide();
            $(controlPanel).find("#IcoInstructorInformationDs").show();        

            $(controlPanel).find("#IcoTOC").hide();
            $(controlPanel).find("#IcoTOCDs").show();        

            $(controlPanel).find("#IcoGlossary").hide();
            $(controlPanel).find("#IcoGlossaryDs").show();        

            $(controlPanel).find("#IcoCourseMaterial").hide();
            $(controlPanel).find("#IcoCourseMaterialDs").show();   

            $(controlPanel).find("#modal-trigger-bookmark").hide();
            $(controlPanel).find("#cd-tour-trigger").hide();

            $(controlPanel).find("#IcoConfigure").hide();
            $(controlPanel).find("#IcoConfigureDs").show();

            $(controlPanel).find("#IcoHelp").hide();
            $(controlPanel).find("#IcoHelpDs").show();


            $(controlPanel).find("#IcoCourseCompletion").hide();
            $(controlPanel).find("#IcoCourseCompletionDs").show();


            var myWindow = window.open('OpenEmbeddedDocuSign.aspx' + ShowDocuSignProcessCommandObject.Affidavit.URL, 'myWindow', "height=700,width=1024");
            myWindow.opener = window.self;
            $(".DocuSignProcessText").text(sceneText2);

            window.OnSignCompleted = function(signedStatus) {
                $('#btnStartDocuSignProcess').attr('disabled', 'disabled');

                if (signedStatus == 'True') {
                    $(".DocuSignProcessText").html(sceneText4);
                    $('#btnSubmit').css("display", "none");
                    $(BackbuttonEn).show();
                    $(BackbuttonDs).hide();

                }
                else {                             
                    $(".DocuSignProcessText").text(sceneText3);
                    $('#btnSubmit').css("display", "none");
                }

                $('#PlaybuttonDocuSign').show();

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

                $(controlPanel).find("#IcoHelp").show();
                $(controlPanel).find("#IcoHelpDs").hide();

                $(controlPanel).find("#IcoCourseCompletion").show();
                $(controlPanel).find("#IcoCourseCompletionDs").hide();

                $(PlaybuttonDs).hide();

                //LCMS - 12201 S.M.Yasin
                $(".DeclineHeading").hide();
                $(".DeclineBody").hide();
                $(".DeclineFooter").hide();

                // LCMS - 12206
                $('#btnSubmit').attr("disabled", true);


            }
        });

        if (ShowDocuSignProcessCommandObject.Affidavit.IsDocuSignDualSigner == true) {

            $(BackbuttonEn).show();
            $(BackbuttonDs).hide();

            $(PlaybuttonDs).hide();

            $('#PlaybuttonDocuSign').show();

			$(controlPanel).find("#IcoInstructorInformation").show();
            $(controlPanel).find("#IcoInstructorInformationDs").hide();                    
            
            $(controlPanel).find("#IcoTOC").show();
            $(controlPanel).find("#IcoTOCDs").hide();                    
            
            $(controlPanel).find("#IcoGlossary").show();
            $(controlPanel).find("#IcoGlossaryDs").hide();                                        
            
            $(controlPanel).find("#IcoCourseMaterial").show();
            $(controlPanel).find("#IcoCourseMaterialDs").hide();            

        }
        else {
            $(".DocuSignProcessText").text(sceneText1);
        }

    }
    //End

    this.AssessmentTimerExpire = function(timeExpiredMessageObject) {
        ui.svgModal.open($("<a data-group='modal-Expire'></a>"));
        $(timerExpireDialogue).find('#timerExpireHeading').html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageHeading);
        $(timerExpireDialogue).find('p').html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageText);        
        $(timerExpireDialogue).find("#expButton").text(Continue_text);
        $(timerExpireDialogue).find("#expButton").focus();
        
            $(timerExpireDialogue).find("#expButton").unbind('click.namespace');
            $(timerExpireDialogue).find("#expButton").bind('click.namespace', function() {
                $(timerExpireDialogue).find("#expButton").unbind('click.namespace');                
                ui.slide.next(function()
                {   
                    IdleTimerReset();
                    cp.ContinueGradingWithoutAnswering();
                });
                return false;
            });
            
            $(timerExpireDialogue).find("#expireClose").unbind('click.namespace');
            $(timerExpireDialogue).find("#expireClose").bind('click.namespace', function() {
                $(timerExpireDialogue).find("expireClose").unbind('click.namespace');                
                ui.slide.next(function()
                {
                    IdleTimerReset();
                    cp.ContinueGradingWithoutAnswering();
                });
                return false;
            });            
        
//        jQuery().ready(function() {

//            $(timerExpireDialogue).fadeIn("slow");
//            $(overlay).css({ "opacity": "0.7" });
//            $(overlay).fadeIn("slow");

//            $(timerExpireDialogue).find('h3').html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageHeading);
//            $("#timerExpireIcon").html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageHeading);
//            $(timerExpireDialogue).find('p').html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageText);
//            $("#expButton").find("button").text(Continue_text);
//            //CourseLockedMessageImageUrl				
//            $(timerExpireDialogue).find("button").html(timeExpiredMessageObject.TimerExpiredMessage.TimerExpiredMessageButtonText);
//            $(timerExpireDialogue).find("button").focus();

//            $(timerExpireDialogue).find("button").unbind('click.namespace');
//            $(timerExpireDialogue).find("button").bind('click.namespace', function() {
//                $(timerExpireDialogue).find("button").unbind('click.namespace');
//                $(timerExpireDialogue).fadeOut("slow");
//                $(overlay).fadeOut("slow");
//                cp.ContinueGradingWithoutAnswering();
//                return false;
//            });
//        });
    }

    //LCMS-10392
    this.RenderAmazonAffiliatePanel = function(keywords, affiliatePanelWSURL) {
        jQuery().ready(function() {

            if (typeof LS360AmazonAffiliatePanel == "undefined") {
                $("<link rel='stylesheet' type='text/css'  href='css/ls360-amazon-affiliate-panel.css'/>").appendTo("head");
                $.getScript("Scripts/ls360-amazon-affiliate-panel.js", function(data, textStatus, jqxhr) {
                    //Amazon Affiliate Panel Container
                    var htmlContainer = '<div id="contentWrapper" class="content-wrapper"></div>';
                    //HTML for templating
                    var htmlTemplate = '<div id="affiliateItemTemplate" class="affiliate-item-container affiliate-item-hidden">' +
	                                        '<div id="affiliateItemImage" class="affiliate-item-image">' +
                    //LCMS-11541
		                                        '<img id="imgAffiliateItemMedium" width="85" height="110" src="" />' +
	                                        '</div>' +
	                                        '<div id="affiliateItemTitle" class="affiliate-item-title"></div>' +
	                                        '<div id="affiliateItemAuthor" class="affiliate-item-author affiliate-item-text-field"></div>' +
	                                        '<div id="affiliateItemPrice" class="affiliate-item-price affiliate-item-text-field"></div>' +
                                        '</div>';

                    $(htmlContainer).insertAfter('#content_container');
                    $(htmlTemplate).appendTo('body');

                    //Initialize AmazonAffiliatePanel plugin
                    ls360AmazonProducts = new LS360AmazonAffiliatePanel();
                    ls360AmazonProducts.init({ keyword: keywords, affiliatePanelWSURL: affiliatePanelWSURL });
                    content_resize();


                    //LCMS - 12271 By Abdus Samad
                    //Start

                    //                    if (!$("#proctor_login_screen").css('display') === 'block') {
                    //                        //LCMS-12064 by Yasin
                    //                        $("#NYInsuranceValidation").hide();
                    //                        $("#proctor_login_screen").hide();
                    //                        $("#CARealStateValidation").hide();
                    //                    }
                    //                    if (!$("#NYInsuranceValidation").css('display') === 'block') {
                    //                        //LCMS-12064 by Yasin
                    //                        $("#NYInsuranceValidation").hide();
                    //                        $("#proctor_login_screen").hide();
                    //                        $("#CARealStateValidation").hide();
                    //                    }

                    //                    if (!$("#CARealStateValidation").css('display') === 'block') {
                    //                        //LCMS-12064 by Yasin
                    //                        $("#NYInsuranceValidation").hide();
                    //                        $("#proctor_login_screen").hide();
                    //                        $("#CARealStateValidation").hide();
                    //                    }
                    //Stop

                });
            } else {
                //alert('AmazonAffiliatePanel has already been loaded!');
            }
        });
    }

    //LCMS-10392
    this.HideAmazonAffiliatePanel = function() {
        if (ls360AmazonProducts) {
            ls360AmazonProducts.hideAffiliatePanel();
            $("#contentWrapper").hide();
            $("#affiliateItemTemplate").hide();
            content_resize();
        }
    }

    //LCMS-10392
    this.ShowAmazonAffiliatePanel = function() {
        //LCMS-10392, Don't show affiliate panel if no book is present
        if (ls360AmazonProducts && $('#contentWrapper .affiliate-item-container').length > 0) {
            $("#contentWrapper").show();
            //$("#affiliateItemTemplate").show();
            ls360AmazonProducts.showAffiliatePanel();
            content_resize();
        }
        else {
            //ls360AmazonProducts.CheckingAmazonPanel();
        }
    }

    //LCMS-10392
    this.IsAmazonAffiliatePanelRendered = function() {
        return ls360AmazonProducts != null;
    }




    //Abdus Samad 
    //LCMS-11878
    //Start  

    this.RenderRecommendationCoursePanel = function(keywords) {
        jQuery().ready(function() {
            if (typeof LS360RecommendationCoursePanel == "undefined") {
                $("<link rel='stylesheet' type='text/css'  href='css/ls360-recommendation-course-panel.css'/>").appendTo("head");
                $.getScript("Scripts/ls360-recommendation-course-panel.js", function() {
                    //Amazon Affiliate Panel Container
                    var htmlContainer = '<div id="contentWrapperReco" class="content-wrapper"></div>';
                    //HTML for templating
                    var htmlTemplate = '<div id="recommendationItemTemplate" class="recommendation-item-container recommendation-item-hidden">' +
	                                        '<div id="recommendationItemImage" class="recommendation-item-image">' +
                    //LCMS-11541
		                                        '<img id="imgRecommendationItemMedium" width="85" height="110" src="" />' +
	                                        '</div>' +
	                                        '<div id="recommendationItemTitle" class="recommendation-item-title"></div>' +
	                                        '<div id="recommendationItemAuthor" class="recommendation-item-author recommendation-item-text-field"></div>' +
	                                        '<div id="recommendationItemPrice" class="recommendation-item-price recommendation-item-text-field"></div>' +
                                        '</div>';

                    $(htmlContainer).insertAfter('#content_container');
                    $(htmlTemplate).appendTo('body');

                    //Initialize AmazonAffiliatePanel plugin
                    ls360RecommendationCourseProducts = new LS360RecommendationCoursePanel();
                    //ls360RecommendationCourseProducts.init({ keyword: keywords, affiliatePanelWSURL: affiliatePanelWSURL });
                    ls360RecommendationCourseProducts.init({ keyword: keywords });
                    content_resize();
                });
            } else {
                //alert('AmazonAffiliatePanel has already been loaded!');
            }
        });
    }

    //LCMS-10392
    this.HideRecommendationCoursePanel = function() {
        if (ls360RecommendationCourseProducts) {
            ls360RecommendationCourseProducts.hideAffiliatePanel();
            $("#contentWrapperReco").hide();
            $("#recommendationItemTemplate").hide();
            content_resize();
        }
    }

    //LCMS-10392
    this.ShowRecommendationCoursePanel = function() {
        //debugger;
        //LCMS-10392, Don't show affiliate panel if no book is present
        if (ls360RecommendationCourseProducts && $('#contentWrapperReco .recommendation-item-container').length > 0) {
            $("#contentWrapperReco").show();
            //$("#affiliateItemTemplate").show();
            ls360RecommendationCourseProducts.showAffiliatePanel();
            content_resize();
        }
        //        else {
        //            ls360RecommendationCourseProducts.CheckingAmazonPanel();
        //        }
    }

    //LCMS-10392
    this.IsRecommendationCoursePanelRendered = function() {
        return ls360RecommendationCourseProducts != null;
    }

    //STOP   
    //Abdus Samad 
    //LCMS-11878       


}   // render engine end...

function findToc(mediaAsset) {
//debugger;
   if (mediaAsset.PlayerAllowTOCDisplaySlides == true)
   {
     AllowTOCDisplaySlidesTrue(mediaAsset);
   }
   else
   {
     AllowTOCDisplaySlidesFalse(mediaAsset);
   }

  
}
/*NEW TOC*/
function AllowTOCDisplaySlidesTrue(mediaAsset) {

  var tocID = mediaAsset.ExamID > 0 ? mediaAsset.ExamID : mediaAsset.SceneSequenceID;

    if (tocID <= 0) {
        return false;
    }

    var isExistsinTOC = false;
    var index = 0;
    for (x = 0; x < tocArray.length; x++) {
       if(mediaAsset.ContentObjectID == tocArray[x][0])
        {
          index = x;
          isExistsinTOC = true;
        }
         if (tocID == tocArray[x][0]) {
            index = x;
            isExistsinTOC = true;
        }
        
    }

    if (tocID != 0 && isExistsinTOC == true) {


        var x = 0;



        for (x = 0; x < tocArray.length; x++) {
            /*
            var arrclass = $("#" + tocArray[x][0]).attr("class");
            if (arrclass == "disable expandable")
                arrclass = "enable expandable"
         
            if (arrclass == "disable collapsable lastCollapsable")
                arrclass = "enable collapsable lastCollapsable"

            if (arrclass == "disable last")
                arrclass = "enable last";

            if (arrclass == "disable")
                arrclass = "enable";
                
            if (arrclass == "disable expandable lastExpandable")
                arrclass = "enable expandable lastExpandable"


            //alert(arrclass);
            $("#" + tocArray[x][0]).removeAttr("class");
            //$("#"+tocArray[x]).addClass("enable");
            $("#" + tocArray[x][0]).addClass(arrclass);
            */
            $("#" + tocArray[x][0]).addClass("active").removeClass("at");
            $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
            $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

            //this has been commented to as now contentobject title will come from Scene rendering Command

            //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());
        
            if (tocID == tocArray[x][0]) {
            atTOC = tocID;
             $("#" + tocArray[x][0]).addClass("at");
               	//var container = $('#toc');
				var scrollTo = $('#' + tocID +'');
				ui.nav.move("outline",scrollTo.offset().top - toc.offset().top + toc.scrollTop() - (toc.height()/2));
				
                /*toc.scrollTop(0); 
				 
			    toc.scrollTop(
				    scrollTo.offset().top - toc.offset().top + toc.scrollTop() - (toc.height()/2)
				); 
				  
				//container.scrollTop(
				//	(scrollTo.offset().top) - container.offset().top + container.scrollTop()
				//);				  						
			*/
			  break;
            }
            
            

        }
    }
}
/*OLD TOC*/
//function AllowTOCDisplaySlidesTrue(mediaAsset) {
//  var tocID = mediaAsset.ExamID > 0 ? mediaAsset.ExamID : mediaAsset.SceneSequenceID;

//    if (tocID <= 0) {
//        return false;
//    }

//    var isExistsinTOC = false;
//    var index = 0;
//    for (x = 0; x < tocArray.length; x++) {
//       if(mediaAsset.ContentObjectID == tocArray[x][0])
//        {
//          index = x;
//          isExistsinTOC = true;
//        }
//         if (tocID == tocArray[x][0]) {
//            index = x;
//            isExistsinTOC = true;
//        }
//        
//    }

//    if (tocID != 0 && isExistsinTOC == true) {


//        var x = 0;



//        for (x = 0; x < tocArray.length; x++) {

//            var arrclass = $("#" + tocArray[x][0]).attr("class");
//            if (arrclass == "disable expandable")
//                arrclass = "enable expandable"
//         
//            if (arrclass == "disable collapsable lastCollapsable")
//                arrclass = "enable collapsable lastCollapsable"

//            if (arrclass == "disable last")
//                arrclass = "enable last";

//            if (arrclass == "disable")
//                arrclass = "enable";
//                
//            if (arrclass == "disable expandable lastExpandable")
//                arrclass = "enable expandable lastExpandable"


//            //alert(arrclass);
//            $("#" + tocArray[x][0]).removeAttr("class");
//            //$("#"+tocArray[x]).addClass("enable");
//            $("#" + tocArray[x][0]).addClass(arrclass);

//            $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
//            $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

//            //this has been commented to as now contentobject title will come from Scene rendering Command

//            //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());

//            if (tocID == tocArray[x][0]) {
//               	var container = $('#toc');
//				var scrollTo = $('#' + tocID +'');
//				  
//                container.scrollTop(0); 
//				 
//			    container.scrollTop(
//				scrollTo.offset().top - container.offset().top + container.scrollTop() - (container.height()/2)
//				); 
//				  
//				//container.scrollTop(
//				//	(scrollTo.offset().top) - container.offset().top + container.scrollTop()
//				//);				  						
//			
//			  break;
//            }

//        }
//    }


//}
///*NEW TOC*/
function AllowTOCDisplaySlidesFalse(mediaAsset) {

  var tocID = mediaAsset.ExamID > 0 ? mediaAsset.ExamID : mediaAsset.ContentObjectID;   //mediaAsset.SceneSequenceID;

    if (tocID <= 0) {
        return false;
    }

    var isExistsinTOC = false;
    var index = 0;
    for (x = 0; x < tocArray.length; x++) {
//       if(mediaAsset.ContentObjectID == tocArray[x][0])
//        {
         // index = x;
          //  isExistsinTOC = true;
//        }
         if (tocID == tocArray[x][0]) {
            index = x;
            isExistsinTOC = true;
        }
        
    }

    if (tocID != 0 && isExistsinTOC == true) {


        var x = 0;



        for (x = 0; x < tocArray.length; x++) {

            /*var arrclass = $("#" + tocArray[x][0]).attr("class");
            if (arrclass == "disable expandable")
                arrclass = "active hasChildren";
                //arrclass = "enable expandable"
                
                

          if (arrclass == "disable collapsable")
                arrclass = "active hasChildren"
                //arrclass = "enable collapsable"


            if (arrclass == "disable collapsable lastCollapsable")
                arrclass = "active hasChildren"
                //arrclass = "enable collapsable lastCollapsable"

            if (arrclass == "disable last")
                arrclass = "active";
                //arrclass = "enable last";

            if (arrclass == "disable")
                arrclass = "active";
                //arrclass = "enable";
                
            if (arrclass == "disable expandable lastExpandable")
                arrclass = "active hasChildren expand"
                //arrclass = "enable expandable lastExpandable"
*/

            //alert(arrclass);
           // $("#" + tocArray[x][0]).removeAttr("class");
            //$("#"+tocArray[x]).addClass("enable");
            //$("#" + tocArray[x][0]).addClass(arrclass);
            
            $("#" + tocArray[x][0]).addClass("active").removeClass("at");

            $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
            $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

            //this has been commented to as now contentobject title will come from Scene rendering Command

            //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());
            
            if (tocID == tocArray[x][0]) {
            atTOC = tocID;
            $("#" + tocArray[x][0]).addClass("at").parent().parent().addClass("expand");
               	//var container = $('#toc');
               	/*
				var scrollTo = $('#' + tocID +'');
				  
                toc.scrollTop(0); 
				 
			    toc.scrollTop(
				scrollTo.offset().top - toc.offset().top + toc.scrollTop() - (toc.height()/2)
				); 
				  */
				//container.scrollTop(
				//	(scrollTo.offset().top) - container.offset().top + container.scrollTop()
				//);				  						
			
			  break;
            }
            
        }
    }


}

/*OLD TOC*/
//function AllowTOCDisplaySlidesFalse(mediaAsset) {
//  var tocID = mediaAsset.ExamID > 0 ? mediaAsset.ExamID : mediaAsset.ContentObjectID;   //mediaAsset.SceneSequenceID;

//    if (tocID <= 0) {
//        return false;
//    }

//    var isExistsinTOC = false;
//    var index = 0;
//    for (x = 0; x < tocArray.length; x++) {
////       if(mediaAsset.ContentObjectID == tocArray[x][0])
////        {
//         // index = x;
//          //  isExistsinTOC = true;
////        }
//         if (tocID == tocArray[x][0]) {
//            index = x;
//            isExistsinTOC = true;
//        }
//        
//    }

//    if (tocID != 0 && isExistsinTOC == true) {


//        var x = 0;



//        for (x = 0; x < tocArray.length; x++) {

//            var arrclass = $("#" + tocArray[x][0]).attr("class");
//            if (arrclass == "disable expandable")
//                arrclass = "enable expandable"

//          if (arrclass == "disable collapsable")
//                arrclass = "enable collapsable"


//            if (arrclass == "disable collapsable lastCollapsable")
//                arrclass = "enable collapsable lastCollapsable"

//            if (arrclass == "disable last")
//                arrclass = "enable last";

//            if (arrclass == "disable")
//                arrclass = "enable";
//                
//            if (arrclass == "disable expandable lastExpandable")
//                arrclass = "enable expandable lastExpandable"


//            //alert(arrclass);
//            $("#" + tocArray[x][0]).removeAttr("class");
//            //$("#"+tocArray[x]).addClass("enable");
//            $("#" + tocArray[x][0]).addClass(arrclass);

//            $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
//            $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

//            //this has been commented to as now contentobject title will come from Scene rendering Command

//            //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());

//            if (tocID == tocArray[x][0]) {
//               	var container = $('#toc');
//				var scrollTo = $('#' + tocID +'');
//				  
//                container.scrollTop(0); 
//				 
//			    container.scrollTop(
//				scrollTo.offset().top - container.offset().top + container.scrollTop() - (container.height()/2)
//				); 
//				  
//				//container.scrollTop(
//				//	(scrollTo.offset().top) - container.offset().top + container.scrollTop()
//				//);				  						
//			
//			  break;
//            }

//        }
//    }


//}

function EnableAllToc() {

    var x = 0;

    for (x = 0; x < tocArray.length; x++) {
        /*
        var arrclass = $("#" + tocArray[x][0]).attr("class");
        if (arrclass == "disable collapsable")
            arrclass = "enable collapsable"

        if (arrclass == "disable collapsable lastCollapsable")
            arrclass = "enable collapsable lastCollapsable"

        if (arrclass == "disable last")
            arrclass = "enable last";

        if (arrclass == "disable")
            arrclass = "enable";

        //alert(arrclass);
        $("#" + tocArray[x][0]).removeAttr("class");
        //$("#"+tocArray[x][0]).addClass("enable");
        $("#" + tocArray[x][0]).addClass(arrclass);
            */
           
        $("#" + tocArray[x][0]).addClass("active").removeClass("at");
        $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
        $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

        //this has been commented to as now contentobject title will come from Scene rendering Command

        //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());
    }
}

/*OLD TOC*/
//function EnableAllToc() {

//    var x = 0;

//    for (x = 0; x < tocArray.length; x++) {

//        var arrclass = $("#" + tocArray[x][0]).attr("class");
//        if (arrclass == "disable collapsable")
//            arrclass = "enable collapsable"

//        if (arrclass == "disable collapsable lastCollapsable")
//            arrclass = "enable collapsable lastCollapsable"

//        if (arrclass == "disable last")
//            arrclass = "enable last";

//        if (arrclass == "disable")
//            arrclass = "enable";

//        //alert(arrclass);
//        $("#" + tocArray[x][0]).removeAttr("class");
//        //$("#"+tocArray[x][0]).addClass("enable");
//        $("#" + tocArray[x][0]).addClass(arrclass);

//        $("#" + tocArray[x][0]).find("a").eq(0).removeAttr("href");
//        $("#" + tocArray[x][0]).find("a").eq(0).attr("href", "javascript:tocClick('" + tocArray[x][0] + "', '" + tocArray[x][1] + "');resetCPIdleTimer();");

//        //this has been commented to as now contentobject title will come from Scene rendering Command

//        //$("#contentObjectName").html($("#"+tocArray[x]).find("span").eq(0).html());
//    }
//}

function parseBookMarkJson(json) {
    if (json.Bookmarks.length > 0) {
        var bookMarkStr = "";
        for (var x in json.Bookmarks) {            
            bookMarkStr = bookMarkStr + "<blockquote><a href=\"javascript:bookmarkClick('" + json.Bookmarks[x].BookMarkID + "');resetCPIdleTimer();\">" + json.Bookmarks[x].BookMarkTitle + "<br><small>"+ json.Bookmarks[x].BookMarkDate +" | " + json.Bookmarks[x].BookMarkTime + "</small></a><span id='btnDeleteBookmark' onclick=\"javascript:DeletebookmarkClick('" + json.Bookmarks[x].BookMarkID + "');resetCPIdleTimer();\" class='delete-item' title='Delete this bookmark'></span></blockquote>";
        }

        bookMarkStr = bookMarkStr + "";
        return bookMarkStr;
    }
}

function parseCourseMaterialJson(json) {
    if (json.CourseMaterials.length > 0) {
        var courseMaterialStr = "";
        for (var x in json.CourseMaterials)
		{
			var icon_type,link_attr;
			var url = json.CourseMaterials[x].CourseMaterialURL.replace("'", "\\'");
			switch(json.CourseMaterials[x].CourseMaterialIconUrl)
			{
				case 'images/ico_gif.png' :
				case 'images/ico_jpg.gif' :
						//image
						link_attr = ' data-source="'+url+'" data-group="modal-dynamic" data-trg="image" data-type="cd-modal-trigger" data-term="'+ json.CourseMaterials[x].CourseMaterialTitle +'"';
						icon_type = '<i class="glyphicon glyphicon-picture"></i>';
				break;
				
				case 'images/ico_xlsm.png' :
				case 'images/ico_pptx.png' :
				case 'images/ico_docM.png' :
				case 'images/ico_file.png' :
					//doc
					link_attr = ' data-source="'+url+'" data-group="modal-dynamic" data-trg="doc" data-type="cd-modal-trigger" data-term="'+ json.CourseMaterials[x].CourseMaterialTitle +'"';
					icon_type = '<i class="glyphicon glyphicon-list-alt"></i>';
				break;
				
				case 'images/ico_html.png' :
					//link
					link_attr = ' target="_blank" href="' + url + '")';
					icon_type = '<i class="glyphicon glyphicon-hand-up"></i>';
				break;
				
				case 'images/ico_pdf.png' :
					//pdf
					link_attr = ' data-source="'+url+'" data-group="modal-dynamic" data-trg="pdf" data-type="cd-modal-trigger" data-term="'+ json.CourseMaterials[x].CourseMaterialTitle +'"';
					icon_type = '<i class="glyphicon glyphicon-file"></i>';
				break;
			}
            courseMaterialStr = courseMaterialStr + "<li title='" + json.CourseMaterials[x].CourseMaterialTitle + "' class=\"active\"><div><a" + link_attr + "id='modalMaterial'>" + icon_type +" "+ json.CourseMaterials[x].CourseMaterialTitle + "</a></div></li>";
            //courseMaterialStr = courseMaterialStr + "<li title='" + json.CourseMaterials[x].CourseMaterialTitle + "' class=\"enable\"><img src=\"" + json.CourseMaterials[x].CourseMaterialIconUrl + "\" border=\"0\"><a href=\"javascript:courseMaterialClick('" + json.CourseMaterials[x].CourseMaterialURL.replace("'", "\\'") + "');resetCPIdleTimer();\">" + json.CourseMaterials[x].CourseMaterialTitle + "</a></li>";
        }
		
        courseMaterialStr = courseMaterialStr;
        return courseMaterialStr;
    }
}

function parseGlossaryJson(json) {
    if (json.Glossaries.length > 0) {
        var glossaryStr = "";
		ui.nav.gTerms = [];
		
        for (var x in json.Glossaries) {

            // replace method implmented on title for LCMS-2562
            //.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;")
             
             //;
			ui.nav.gTerms[x] = [json.Glossaries[x].GlossaryID,InsertEscapeSequence(json.Glossaries[x].Term, "'")];
            glossaryStr = glossaryStr + "<li title='" + StripTagsCharArray(json.Glossaries[x].Term) + "' class=\"active\"><div><a data-group='modal-dynamic' data-trg='glossary' data-type='cd-modal-trigger' href=\"javascript:;\" data-index='"+x+"' data-id='" +  json.Glossaries[x].GlossaryID +  "' data-term='"+ InsertEscapeSequence(json.Glossaries[x].Term, "'") +"'>" + StripTagsCharArray(json.Glossaries[x].Term.replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;")) + "</a></div></li>";
            //glossaryStr = glossaryStr + "<li title='" + StripTagsCharArray(json.Glossaries[x].Term) + "' class=\"active\"><div><a id='modalGlossary' href=\"javascript:glossaryClick('" + json.Glossaries[x].GlossaryID + "','" + InsertEscapeSequence(json.Glossaries[x].Term, "'") + " ');resetCPIdleTimer();\">" + StripTagsCharArray(json.Glossaries[x].Term.replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;")) + "</div></a></li>";
            
            
            //glossaryStr  = glossaryStr + "<li title='"+json.Glossaries[x].Term.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;")+"' class=\"enable\"><a href=\"javascript:glossaryClick('"+json.Glossaries[x].GlossaryID+"','"+InsertEscapeSequence(json.Glossaries[x].Term,"'")+" ');resetCPIdleTimer();\">"+json.Glossaries[x].Term.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;")+"</a></li>";			

            //glossaryStr  = glossaryStr + "<li title='"+json.Glossaries[x].Term.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;")+"' class=\"enable\"><a href=\"javascript:glossaryClick('"+ json.Glossaries[x].GlossaryID+"','"+ json.Glossaries[x].Term.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;").replace("(","&#40;") + "');resetCPIdleTimer();\">"+json.Glossaries[x].Term.replace(new RegExp("<","g"),"&lt;").replace(new RegExp(">","g"),"&gt;").replace(new RegExp("'","g"),"&#39").replace(new RegExp("\"","g"),"&quot;")+"</a></li>";			

        }
		
        glossaryStr = glossaryStr;
        //alert(glossaryStr);
        return glossaryStr;
    }
}
/*NEW TOC*/
function parseJsonChild(json) {
    if (json.length > 0) {
       
        
        htmlstr = htmlstr + "<ul>";
        for (var x in json) {
            var nested_spanStr = "";
            var nested_hasChild = "";
       
                
                  
        if( json[x].TOCItems.length > 0 ){ nested_spanStr = "<span></span>"; nested_hasChild = "hasChildren"; }
            //alert(json[x].ContentObjectID);			
            tocArray.push([json[x].ID, json[x].Type]);
            if (json[x].Type == 'ContentObject') {
                if (!json[x].IsDisabled) {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"active " + nested_hasChild +"\"><div><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+ nested_spanStr + "</div>";
                } else {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\"class=\"" + nested_hasChild +"\" ><div><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") +"</a>"+ nested_spanStr + "</div>";
                }
            }
            if (json[x].Type == 'Exam') {
                if (!json[x].IsDisabled) {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"exam active " + nested_hasChild +"\"><div><a data-type=\"EXAM\" title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") +"</a>"+ nested_spanStr + "</div>";
                } else {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"exam" + nested_hasChild +"\" ><div><a data-type=\"EXAM\" title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;")+"</a>"+ nested_spanStr + "</div>";
                }
            }
          if (json[x].Type == 'Scene') {
                if (!json[x].IsDisabled) {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"active " + nested_hasChild +"\"><div><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+ nested_spanStr + "</div>";
                } else {
                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"" + nested_hasChild +"\"><div><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;")+"</a>"+ nested_spanStr + "</div>";
                }
            }       
            parseJsonChild(json[x].TOCItems);
        }   
        htmlstr = htmlstr + "</li></ul>";
        
        
        return htmlstr;

    } else {
        return "";
    }
}

var htmlstr = "";
/*OLD TOC*/
//function parseJsonChild(json) {
//    if (json.length > 0) {
//        htmlstr = htmlstr + "<ul>";
//        for (var x in json) {
//            //alert(json[x].ContentObjectID);			
//            tocArray.push([json[x].ID, json[x].Type]);
//            if (json[x].Type == 'ContentObject') {
//                if (!json[x].IsDisabled) {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"enable \"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=folder href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=folder>" + json[x].BreadCrumb + "</span></a>";
//                } else {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"disable\"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=folder href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=folder>" + json[x].BreadCrumb + "</span></a>";
//                }
//            }
//            if (json[x].Type == 'Exam') {
//                if (!json[x].IsDisabled) {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"enable \"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=exam href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=exam>" + json[x].BreadCrumb + "</span></a>";
//                } else {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"disable\"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=exam href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=folder>" + json[x].BreadCrumb + "</span></a>";
//                }
//            }
//          if (json[x].Type == 'Scene') {
//                if (!json[x].IsDisabled) {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"enable \"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=file href=\"javascript:tocClick('" + json[x].ID + "', '" + json[x].Type + "');resetCPIdleTimer();\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=folder>" + json[x].BreadCrumb + "</span></a>";
//                } else {
//                    htmlstr = htmlstr + "<li id=\"" + json[x].ID + "\" class=\"disable\"><a title=\"" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" class=file href=\"javascript:void(0);\">" + json[x].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "<span class=folder>" + json[x].BreadCrumb + "</span></a>";
//                }
//            }       
//            parseJsonChild(json[x].TOCItems);
//        }   
//        htmlstr = htmlstr + "</li></ul>";
//        return htmlstr;

//    } else {
//        return "";
//    }
//}

/*NEW TOC*/
function parseTocJson(json) {

    //var g="filetree treeview";
    //var classvar = "filetree treeview-famfamfam";
  //  var classvar = "side-menu-nav items";
    var tocStr = "";//"<ul id=browser class=\"" + classvar + "\">";
    for (var x in json) {
    //console.log(json[x]);
        if (typeof (json[x]) == 'object' && json[x].TOCItems != null) {
            if (json[x].TOCItems.length != 0) {

                for (var i = 0; i < json[x].TOCItems.length; i++) {
                    htmlstr = "";
                    tocArray.push([json[x].TOCItems[i].ID, json[x].TOCItems[i].Type]);
                    
                    var spanStr = "";
                    var hasChild = "";
                    /*if(json[x].TOCItems[i].Type == 'Exam'){
                     console.log(json[x].TOCItems[i]);
                    }*/
                    if(json[x].TOCItems[i].TOCItems.length > 0 /*&& json[x].TOCItems[i].Type == 'Exam')
                    || (json[x].TOCItems[i].TOCItems.length > 0 && json[x].TOCItems[i].Type == 'ContentObject')
                    || (json[x].TOCItems[i].TOCItems.length > 0 && json[x].TOCItems[i].Type == 'Scene')*/)
                    {
                    spanStr = "<span></span>"; 
                    hasChild = " hasChildren";
                    }
                    
                    //if(json[x].TOCItems[i].TOCItems.length > 0 & json[x].TOCItems[i].Type == 'ContentObject'){spanStr = "<span></span>"; hasChild = "hasChildren"}
                    //if(json[x].TOCItems[i].TOCItems.length > 0 & json[x].TOCItems[i].Type == 'Scene'){spanStr = "<span></span>"; hasChild = "hasChildren"}
                    
                    
                    if (json[x].TOCItems[i].Type == 'ContentObject') {
                    
                    
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"active" + hasChild +"\"><div><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //"<span>" + json[x].TOCItems[i].BreadCrumb + "</span>"
                            
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"enable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=folder>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"" + hasChild +"\"><div><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=folder>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                    if (json[x].TOCItems[i].Type == 'Exam') {
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"exam active" + hasChild +"\"><div><a data-type=\"EXAM\" title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"active\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=exam>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"exam" + hasChild +"\"><div><a data-type=\"EXAM\" title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=exam>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                     if (json[x].TOCItems[i].Type == 'Scene') {
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"active" + hasChild +"\"><div><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"active\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=file>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"" + hasChild +"\"><div><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\">" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</a>"+spanStr+"</div>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                            //tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=file>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                }
            }
        }
    }
  //  tocStr = tocStr + "</ul>";

    //var TOS="<ul id=browser class=\""+g+"\"><li><span class=folder>Folder 1</span><ul><li><span class=folder>Item 1.1</span><ul><li><span class=file>Item 1.1.1</span></li></ul></li><li><span class=folder>Folder 2</span><ul><li><span class=folder>Subfolder 2.1</span><ul id=folder21><li><span class=file>File 2.1.1</span></li><li><span class=file>File 2.1.2</span></li></ul></li><li><span class=folder>Subfolder 2.2</span><ul><li><span class=file>File 2.2.1</span></li><li><span class=file>File 2.2.2</span></li></ul></li></ul></li><li class=closed><span class=folder>Folder 3 (closed at start)</span><ul><li><span class=file>File 3.1</span></li></ul></li><li><span class=file>File 4</span></li></ul></li></ul>";	
    //Test Code
    var TOS = "" +
	"<ul id=\"browser\" class=\"filetree\">" +
     "   <li><span class=\"folder\" onclick=\"alert('Folder 1')\">Folder 1</span>" +
      "      <ul>" +
       "         <li><span class=\"folder\" onclick=\"alert('Item 1.1')\">Item 1.1</span></li>" +
        "    </ul>" +
        "</li>" +
        "<li><span class=\"folder\">Folder 2</span>" +
         "   <ul>" +
          "      <li><a><span class=\"folder\">Subfolder 2.1</span></a>" +
           "         <ul id=\"folder21\">" +
            "            <li><span class=\"folder\">File 2.1.1</span></li>" +
             "           <li><span class=\"folder\">File 2.1.2</span></li>" +
              "      </ul>" +
               " </li>" +
                "<li><span class=\"folder\">File 2.2</span></li>" +
            "</ul>" +
        "</li>" +
        "<li class=\"folder\"><span class=\"folder\">Folder 3 (closed at start)</span>" +
         "   <ul>" +
          "      <li><span class=\"folder\">File 3.1</span></li>" +
           " </ul>" +
        "</li>" +
        "'<li><span class=\"folder\">File 4</span></li>" +
    "</ul>";
    //Test Code
    //return TOS;

    return tocStr;

}

/*
OLD TOC
function parseTocJson(json) {

    //var g="filetree treeview";
    var classvar = "filetree treeview-famfamfam";
    var tocStr = "<ul id=browser class=\"" + classvar + "\">";
    for (var x in json) {
        if (typeof (json[x]) == 'object' && json[x].TOCItems != null) {
            if (json[x].TOCItems.length != 0) {

                for (var i = 0; i < json[x].TOCItems.length; i++) {
                    htmlstr = "";
                    tocArray.push([json[x].TOCItems[i].ID, json[x].TOCItems[i].Type]);
                    if (json[x].TOCItems[i].Type == 'ContentObject') {
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"enable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=folder>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=folder>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                    if (json[x].TOCItems[i].Type == 'Exam') {
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"enable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=exam>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=exam>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                     if (json[x].TOCItems[i].Type == 'Scene') {
                        if (!json[x].TOCItems[i].IsDisabled) {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"enable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\" href=\"javascript:tocClick('" + json[x].TOCItems[i].ID + "', '" + json[x].TOCItems[i].Type + "');resetCPIdleTimer();\"><strong class=file>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        } else {
                            tocStr = tocStr + "<li id=\"" + json[x].TOCItems[i].ID + "\" class=\"disable\"><a title=\"" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "\"  href=\"javascript:void(0);\"><strong class=file>" + json[x].TOCItems[i].Title.replace(new RegExp("<", "g"), "&lt;").replace(new RegExp(">", "g"), "&gt;").replace(new RegExp("'", "g"), "&#39").replace(new RegExp("\"", "g"), "&quot;") + "</strong><span>" + json[x].TOCItems[i].BreadCrumb + "</span></a>" + parseJsonChild(json[x].TOCItems[i].TOCItems) + "</li>";
                        }
                    }
                }
            }
        }
    }
    tocStr = tocStr + "</ul>";

    //var TOS="<ul id=browser class=\""+g+"\"><li><span class=folder>Folder 1</span><ul><li><span class=folder>Item 1.1</span><ul><li><span class=file>Item 1.1.1</span></li></ul></li><li><span class=folder>Folder 2</span><ul><li><span class=folder>Subfolder 2.1</span><ul id=folder21><li><span class=file>File 2.1.1</span></li><li><span class=file>File 2.1.2</span></li></ul></li><li><span class=folder>Subfolder 2.2</span><ul><li><span class=file>File 2.2.1</span></li><li><span class=file>File 2.2.2</span></li></ul></li></ul></li><li class=closed><span class=folder>Folder 3 (closed at start)</span><ul><li><span class=file>File 3.1</span></li></ul></li><li><span class=file>File 4</span></li></ul></li></ul>";	
    //Test Code
    var TOS = "" +
	"<ul id=\"browser\" class=\"filetree\">" +
     "   <li><span class=\"folder\" onclick=\"alert('Folder 1')\">Folder 1</span>" +
      "      <ul>" +
       "         <li><span class=\"folder\" onclick=\"alert('Item 1.1')\">Item 1.1</span></li>" +
        "    </ul>" +
        "</li>" +
        "<li><span class=\"folder\">Folder 2</span>" +
         "   <ul>" +
          "      <li><a><span class=\"folder\">Subfolder 2.1</span></a>" +
           "         <ul id=\"folder21\">" +
            "            <li><span class=\"folder\">File 2.1.1</span></li>" +
             "           <li><span class=\"folder\">File 2.1.2</span></li>" +
              "      </ul>" +
               " </li>" +
                "<li><span class=\"folder\">File 2.2</span></li>" +
            "</ul>" +
        "</li>" +
        "<li class=\"folder\"><span class=\"folder\">Folder 3 (closed at start)</span>" +
         "   <ul>" +
          "      <li><span class=\"folder\">File 3.1</span></li>" +
           " </ul>" +
        "</li>" +
        "'<li><span class=\"folder\">File 4</span></li>" +
    "</ul>";
    //Test Code
    //return TOS;

    return tocStr;

}
*/
function tocClick(ID, type) {
    //closeAccordian(panel); // added by Musafa (for LCMS-2315)
    $(panelbutton).css("display", "none"); //added by Umer for (LCMS-2022 )
    $('#' + atTOC).removeClass("at");
    $('#PlaybuttonDocuSign').remove(); //added by Abdus Samad (Since 2 Buttons were appearing)
    ui.slide.next(function()
    {
        cp.CallGoto(ID, type);
    });    
}



function glossaryClick(glossaryID, glossaryTerm) {    
    cp.CallGetGlossary(glossaryID, glossaryTerm);
}

function bookmarkClick(bookMarkID) {    
    
    ui.svgModal.close('modal-trigger-bookmark');
    //DELAY
    setTimeout(function()
    {
        ui.slide.next(function()
        {   
            cp.CallGetBookMark(bookMarkID);
        })
    },800); 
    
}

function reportClick()
{
    cp.reportClick();    
}

function DeletebookmarkClick(bookMarkID) {            
    cp.CallDeleteBookMark(bookMarkID);
}

function courseMaterialClick(courseMaterialURL) {
    if (courseMaterialURL != "") {
        window.open(courseMaterialURL);
    }
}

function courseApprovalAffidavitClick(affidavitURL) {
    if (affidavitURL != "") {
        //alert(affidavitURL);
        window.open(affidavitURL);
    }
}


function resumeAssessmentMessageClick() {
    cp.ResumeAssessment();
    if (isLockoutClickAwayToActiveWindowStart) {
        initiateSelfLockingDueToClickingAwayToActvieWindow();
    }
}


function startAssessmentMessageClick() {
    ui.slide.next(function()
    {
        $('#wrapper').removeClass('toggled-left')        
        $('.cd-nav-trigger').hide();
        $("#toogle-flag").removeClass('hide');
        cp.StartAssessment();
    });	
    if (isLockoutClickAwayToActiveWindowStart) {
        initiateSelfLockingDueToClickingAwayToActvieWindow();
    }
    //cp.CallNextSlide();
}

function skipPracticeExam() {

    cp.SkipPracticeExam();
}

function errorMessageClick() {

    //alert("asdasdas");
    //
}

function continueWithCourse() {
    cp.CallNextSlide();
}

function answerRemainingClick() {
    ui.slide.next(function()
    {
        cp.CallAnswerRemainingClick();
    });		
}


function continueGradingClick() {
    ui.slide.next(function()
    {
        cp.ContinueGradingWithoutAnswering();
    });
}


function customMessageClick() {

    //cp.CallNextSlide();
}


function resetCPIdleTimer() {
    cp.resetTimer();
}

// Irfan Calling CP Manager Get Questions
function GetNextQuestion() {
    cp.GetNextQuestion();
}
// Irfan


// Irfan's Function Start

function GetNextQuestionAfterFeedback() {

    ui.slide.next(function()
    {
        cp.GetNextQuestionAfterFeedback();
    });	
}

function AskSpecifiedQuestion(assessmentItemId) {
    assessmentTimerObj.ResumeTimer();
    ui.slide.next(function()
    {
        cp.AskSpecifiedQuestion(assessmentItemId);
    });	    
}


function ShowSpecifiedQuestionScore(assessmentItemId) {
    //alert(assessmentItemId);
    ui.slide.next(function()
    {
        cp.ShowSpecifiedQuestionScore(assessmentItemId);
    });		    
}

var selectedHotSpot = null;
function imageHotSpotClick(hotspotID, arrAnswers) {


    for (var i = 0; i < arrAnswers.length; i++) {

        $("#hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "").removeClass("redBorder");
        $("#hotSpot" + arrAnswers[i].AssessmentItemAnswerID + "").addClass("yellowBorder");

        if (hotspotID == arrAnswers[i].AssessmentItemAnswerID) {

            $("#hotSpot" + hotspotID + "").removeClass("yellowBorder");
            $("#hotSpot" + hotspotID + "").addClass("redBorder");

            selectedHotSpot = hotspotID;
        }


    }


}

// Irfan's Function End


function CourseTimerClass() {
    var seconds;
    var minutes;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    var temp = new Array();
    var timerDiv = "";

    this.TimerContainer = function(divName) {
        timerDiv = divName;

    }

    this.InitializeTimer = function(value) {
        temp = value.split(':');
        minutes = eval(temp[0]);
        seconds = eval(temp[1]);

        this.StopTimer();
        this.StartTimer();

    }

    this.StartTimer = function() {
        if (!timerRunning) {

            timerID = setInterval(function() {
                seconds++;
                if (seconds == 60) {
                    seconds = 0;
                    minutes++;
                }
                var str = FormatZero(minutes) + ":" + FormatZero(seconds);
                jQuery().ready(function() {
                    $(timerDiv).html(str);
                });
            }, delay);

            timerRunning = true;
        }

    }

    this.StopTimer = function() {
        if (timerRunning) {
            clearInterval(timerID);
            timerRunning = false;


        }
    }

}


function OdometerTimerClass() {
    var seconds;
    var minutes;
    var hours;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    var temp = new Array();
    var timerDiv = "";
    var initializeDate = null;
    var initseconds;
    var initminutes;
    var inithours;


    this.TimerContainer = function(divName) {
        timerDiv = divName;

    }

    this.InitializeTimer = function(value) {
        if (value == null) {
            value = '00:00:00';
        }

        temp = value.split(':');
        hours = eval(temp[0]);
        minutes = eval(temp[1]);
        seconds = eval(temp[2]);

        inithours = hours;
        initminutes = minutes;
        initseconds = seconds;

        this.StopTimer();
        initializeDate = new Date();


        this.StartTimer();

    }

    this.StartTimer = function() {
        if (!timerRunning) {

            timerID = setInterval(function() {

                /*seconds++;
			
				if(minutes == 60) {
                seconds = 0;
                minutes=0;
                hours++;
                }
                if(seconds == 60) {
                seconds = 0;
                minutes++;
                }*/
                //


                var nowDate = new Date();
                var nowDateForTimer = new Date();
                var re = cp.getRenderEngineInstance();
                var diffSeconds;

                //var prevDate = new Date(nowDate.getYear(),nowDate.getMonth(),nowDate.getDay(),inithours,initminutes,initseconds);
                //alert(inithours);


                var differenceForTimer = nowDateForTimer.getTime() - initializeDate.getTime();
                diffSeconds = Math.floor(differenceForTimer / 1000);



                nowDate.setHours(nowDate.getHours() + inithours);
                nowDate.setMinutes(nowDate.getMinutes() + initminutes);
                nowDate.setSeconds(nowDate.getSeconds() + initseconds);



                if (nowDate.getDate() == initializeDate.getDate()) {
                    var difference = nowDate.getTime() - initializeDate.getTime();
                    //diffSeconds = Math.floor(difference/1000);
                    var daysDifference = Math.floor(difference / 1000 / 60 / 60 / 24);
                    difference -= daysDifference * 1000 * 60 * 60 * 24

                    var hoursDifference = Math.floor(difference / 1000 / 60 / 60);
                    difference -= hoursDifference * 1000 * 60 * 60

                    var minutesDifference = Math.floor(difference / 1000 / 60);
                    difference -= minutesDifference * 1000 * 60

                    var secondsDifference = Math.floor(difference / 1000);

                }
                else {
                    var difference = nowDate.getTime() - initializeDate.getTime();
                    // diffSeconds = Math.floor(difference/1000);
                    var daysDifference = Math.floor(difference / 1000 / 60 / 60 / 24);
                    difference -= daysDifference * 1000 * 60 * 60 * 24

                    var hoursDifference = Math.floor(difference / 1000 / 60 / 60);
                    difference -= hoursDifference * 1000 * 60 * 60

                    var minutesDifference = Math.floor(difference / 1000 / 60);
                    difference -= minutesDifference * 1000 * 60

                    var secondsDifference = Math.floor(difference / 1000);

                    //hoursDifference = hoursDifference+(24 * daysDifference);
                }

                hours = hoursDifference;
                minutes = minutesDifference;
                seconds = secondsDifference;
                if (daysDifference > 0) {
                    hours = hours + (daysDifference * 24);
                }



                // IDLE COUNTER LOGIC    
                // -----------------------------------------------------------------   


                //            if(!re.isIdleTimerReset)
                //			{
                //			    diffSeconds = difference/1000;
                //			    //re.lastActivityTimeStamp = initializeDate;
                //			}
                //			else
                //			{
                //			//var diffSeconds = nowDate.getSeconds() - re.lastActivityTimeStamp.getSeconds();
                //			    diffSeconds = Math.floor((nowDate.getTime() - re.lastActivityTimeStamp.getTime()) / 1000);
                //			}

                if (re.isIdleTimerReset) {
                    //diffSeconds = nowDate.getSeconds() - re.lastActivityTimeStamp.getSeconds();
                    diffSeconds = Math.floor((nowDateForTimer - re.lastActivityTimeStamp) / 1000);

                }

                if (!re.isIdleTimeElapsed) {
                    if ((diffSeconds >= re.whenToShowIdleTimePopup) && (!$("#examIdleTimerDialogue").hasClass("modal-is-visible"))) // if timestamp difference reaches to warning time, then show the countdown
                    {
                        re.GetIdleTimeNotificationObj().ShowIdleTimeDialogue(re.idleTimeMsgHeading, re.idleTimeMsgContent);
                     
                    }

                    if (diffSeconds >= re.whenToShowIdleTimePopup) // if timestamp difference is greater than Idle time and warning time
                    {

                        re.GetIdleTimeNotificationObj().proceedSeconds(re.runningIdleWarningTime - (diffSeconds - re.whenToShowIdleTimePopup));

                        //re.runningIdleWarningTime = re.runningIdleWarningTime - (diffSeconds - re.whenToShowIdleTimePopup);
                        //re.runningIdleWarningTime-- ;
                    }
                }
                // -----------------------------------------------------------------  





                // LCMS-5926
                //-----------------------------------------------

                if (minimumTimeBeforeStartingPostAssessmentUnit != "") {

                    var df = nowDate.getTime() - initializeDate.getTime();
                    var timeDiff;

                    if (minimumTimeBeforeStartingPostAssessmentUnit == "Hours") {
                        timeDiff = Math.floor(df / 1000 / 60 / 60);
                    }
                    else if (minimumTimeBeforeStartingPostAssessmentUnit == "Minutes") {
                        timeDiff = Math.floor(df / 1000 / 60);
                    }

                    if (timeDiff >= minimumTimeBeforeStartingPostAssessment) {                        
                        $("#odometer").html(minimumTimeMetMessage);
                        return;
                    }

                }
                //-----------------------------------------------





                if (hours == 0) {                    
                    var str = ""
                    if (minutes <= 1) {
                        str = str + FormatZero(minutes) + " " + Minutetxt;
                    }
                    else {
                        str = str + FormatZero(minutes) + " " + Minutestxt;
                    }
                }
                if (hours == 1) {
                    var str = FormatZero(hours) + " " + Hourtxt + " ";
                    if (minutes <= 1) {
                        str = str + FormatZero(minutes) + " " + Minutetxt + "";
                    }
                    else {
                        str = str + FormatZero(minutes) + " " + Minutestxt + "";
                    }
                }
                if (hours > 1) {
                    var str = FormatZero(hours) + " " + Hourstxt + " ";
                    if (minutes <= 1) {
                        str = str + FormatZero(minutes) + " " + Minutetxt + "";
                    }
                    else {
                        str = str + FormatZero(minutes) + " " + Minutestxt;
                    }
                }                
                $(timerDiv).html(str);
            }, delay);

            timerRunning = true;
        }

    }

    this.StopTimer = function() {
        if (timerRunning) {
            clearInterval(timerID);
            timerRunning = false;


        }
    }

}




function IdleTimerClass() {
    var seconds;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;

    this.InitializeTimer = function(value) {
        seconds = (value * delay);
        //alert(value);
        this.StopTimer();
        this.StartTimer();



    }

    this.ResetTimer = function() {

        if (timerRunning == true) {
            this.StopTimer();
            this.StartTimer();
        }

        var re = cp.getRenderEngineInstance();
        re.lastActivityTimeStamp = new Date();
        re.runningIdleWarningTime = re.idleWarningTime;

    }

    this.StartTimer = function() {
        if (!timerRunning) {
            //timerID = setTimeout(cp.SendLogoutMessage, seconds);		
            //timerID = setTimeout(cp.SendActionToTakeOnIdleUserTimeoutCommand, seconds);		
            timerRunning = true;
        }

    }

    this.StopTimer = function() {
        if (timerRunning) {
            clearTimeout(timerID);
            timerRunning = false;
        }
    }

}

function FormatZero(number) {
    return (number >= 0 && number <= 9) ? "0" + number : number;
}

// Irfan Ashraf 14 Nov 2008  Assessment Timer Class Start

function AssessmentTimerClass() {
    var seconds;
    var minutes;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    var temp = new Array();
    var timerDiv = "";
    var pausedSeconds;
    var pausedMinutes;
    var dontRunTimer = false;
    var IsStop = true;
    var assessmentDuration=0; 


    this.TimerContainer = function(divName) {
        timerDiv = divName;
    }
    this.InitializeTimer = function(value) {
        IsStop = true;
        //alert('AssessmentTimer  InitializeTimer:'+IsStop);
        if (value > 0) {
            if (value >= 60) {
                minutes = Math.floor(value / 60);
                seconds = value % 60;
            } else {
                minutes = 0;
                seconds = value;
            }

            dontRunTimer = true;
            assessmentDuration=value;
            this.StopTimer();
            this.StartTimer();


        }
        else {
            dontRunTimer = true;
        }

    }

    this.StartTimer = function() {
        IsStop = true;
        //alert('AssessmentTimer  StartTimer:'+IsStop);
        //alert('AssessmentTimer  StartTimer');
        if (dontRunTimer) {
            if (!timerRunning) {
            
                var $sd = $("#assessmentTimer");
	            /*var $bar = $sd.find(".progress-bar");        
		        $bar.css("width","100%");        */
		        $sd.removeClass('hide');           
                var counter = 0;            

                timerID = setInterval(function() {
                    seconds--;
                    if (seconds < 0) {
                        seconds = 59;
                        minutes--;
                    }
                    if ((minutes <= 0) && (seconds <= 0)) {
                        assessmentTimerObj.StopTimer();
                        //Send the request to server to forcefully mark all the unanswered question as false and show AssessmentScoreSummary page
                        // alert(" AssessmentTimerExpire  seconds"+seconds+"minutes"+minutes); 
                        $sd.addClass('hide');
                        cp.AssessmentTimerExpire();
                        //return;
                    }

                    var str = FormatZero(minutes) + ":" + FormatZero(seconds);
                    counter++;
                    jQuery().ready(function() {                        
                        /*$bar.css("width",((((assessmentDuration-counter) / assessmentDuration)) * 100)+"%");*/
                        $(timerDiv).html(str);
                    });
                }, delay);

                timerRunning = true;
            }
        }
    }
    this.StopTimer = function() {
        //alert('AssessmentTimer  StopTimer:'+IsStop);	    
        if (dontRunTimer) {
            if (timerRunning) {
                // alert('AssessmentTimer  StopTimer:'+timerRunning);
                clearInterval(timerID);
                timerRunning = false;
                IsStop = false;
                $(timerDiv).empty();
            }
        }
    }

    this.StopTimerFromAssessmentScoreSummary = function() {
        if (dontRunTimer) {
            clearInterval(timerID);
            timerRunning = false;
            IsStop = false;
            $(timerDiv).empty();
        }
    }

    this.PauseTimer = function() {
        // alert('AssessmentTimer  PauseTimer:'+IsStop);
        if (dontRunTimer) {
            pausedSeconds = seconds;
            pausedMinutes = minutes;


            // this.StopTimer();
            if (timerRunning) {
                // alert('AssessmentTimer  StopTimer:'+timerRunning);
                clearInterval(timerID);
                timerRunning = false;
            }


            if (timerRunning) {
                timerID = setInterval(function() {

                    var str = FormatZero(pausedMinutes) + ":" + FormatZero(pausedSeconds);
                    jQuery().ready(function() {
                        $(timerDiv).html(str);
                    });
                }, delay);
            }

            timerRunning = false;
        }
    }


    this.ResumeTimer = function() {
        //alert('AssessmentTimer  ResumeTimer:'+IsStop);

        if (IsStop) {
            //alert('AssessmentTimer  Has Resumed Again:'+IsStop);
            if (dontRunTimer) {
                seconds = pausedSeconds;
                minutes = pausedMinutes;

                if (!timerRunning) {
                    //  alert(" Resume Assesesment Timer seconds:"+pausedSeconds+"minutes"+pausedMinutes); 
                    timerID = setInterval(function() {

                        //seconds--;
                        if (seconds == 00) {
                            seconds = 59;
                            --minutes;
                        }
                        --seconds;

                        if ((minutes <= 0) && (seconds <= 0)) {
                            assessmentTimerObj.StopTimer();
                            cp.AssessmentTimerExpire();
                        }

                        var str = FormatZero(minutes) + ":" + FormatZero(seconds);
                        jQuery().ready(function() {
                            $(timerDiv).html(str);
                        });
                    }, delay);

                    timerRunning = true;
                }
            }
        }
    }

}

// Irfan Ashraf 14 Nov 2008 Assessment Timer Class End


// Irfan Ashraf List Box Element UP DOWN Start
function ListElementsMoveUp() {
    var lst = $('#question').find('select')[0];

    if (lst.selectedIndex == -1) {
        //alert('Please select an Item to move up.');


    }
    else {
        if (lst.selectedIndex == 0) {
            alert('First element cannot be moved up');
            return false
        }
        else {
            var tempValue = lst.options[lst.selectedIndex].value;
            var tempIndex = lst.selectedIndex - 1;
            lst.options[lst.selectedIndex].value = lst.options[lst.selectedIndex - 1].value;
            lst.options[lst.selectedIndex - 1].value = tempValue;
            var tempText = lst.options[lst.selectedIndex].text;
            lst.options[lst.selectedIndex].text = lst.options[lst.selectedIndex - 1].text;
            lst.options[lst.selectedIndex - 1].text = tempText;
            lst.selectedIndex = tempIndex;


        }


        // LCMS-10853 (START)
        //------------------------------------------------------------
        HandleOrderingUpDownButtonsState();
        //------------------------------------------------------------
        // LCMS-10853 (END)


    }

    return false;
}

// LCMS-10853 (START)
//------------------------------------------------------------
function HandleOrderingUpDownButtonsState() {

    var lst = $('#question').find('select')[0];

    if (lst.selectedIndex == 0) {
        document.getElementById('orderbuttons').getElementsByTagName('button')[0].disabled = true;
    }
    else {
        document.getElementById('orderbuttons').getElementsByTagName('button')[0].disabled = false;
    }

    if (lst.selectedIndex == lst.options.length - 1) {
        document.getElementById('orderbuttons').getElementsByTagName('button')[1].disabled = true;
    }
    else {
        document.getElementById('orderbuttons').getElementsByTagName('button')[1].disabled = false;
    }
    return false;
}
//------------------------------------------------------------
// LCMS-10853 (END)

function ListElementsMoveDown() {
    var lst = $('#question').find('select')[0];

    if (lst.selectedIndex == -1) {
        //alert('Please select an Item to move down');

    }
    else {
        if (lst.selectedIndex == lst.options.length - 1) {
            alert('Last element cannot be moved down');
        }
        else {
            var tempValue = lst.options[lst.selectedIndex].value;
            var tempIndex = lst.selectedIndex + 1;
            lst.options[lst.selectedIndex].value = lst.options[lst.selectedIndex + 1].value;
            lst.options[lst.selectedIndex + 1].value = tempValue;
            var tempText = lst.options[lst.selectedIndex].text;
            lst.options[lst.selectedIndex].text = lst.options[lst.selectedIndex + 1].text;
            lst.options[lst.selectedIndex + 1].text = tempText;
            lst.selectedIndex = tempIndex;


        }

        // LCMS-10853 (START)
        //------------------------------------------------------------
        HandleOrderingUpDownButtonsState();
        //------------------------------------------------------------
        // LCMS-10853 (END)

    }
    return false;
}
// Irfan Ashraf List Box Element UP DOWN End





function ValidationTimerClass() {
    var seconds;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    //var getvalue=null;

    this.InitializeTimer = function(value) {
        //alert("value:"+value);
        //getvalue=(value * delay);
        seconds = (value * delay);
        this.StopTimer();
        this.StartTimer();

    }
    this.ReInitializeTimer = function(value) {
        //alert("value:"+value);
        //getvalue=(value * delay);
        seconds = (value * delay);
        clearInterval(timerID);
        timerRunning = false;
        this.StartTimer();
        //this.StopTimer();
        //this.StartTimer();

    }
    this.ResetTimer = function() {
        this.StopTimer();
        this.StartTimer();
    }

    this.StartTimer = function() {
        if (!timerRunning) {
            //alert("setInterval(cp.ValidationTimerExpired):"+seconds);
            //timerID = setInterval(cp.AskValidationQuestion, seconds);
            //
            // alert('ValidationTimerExpired:'+seconds);
            timerID = setInterval(cp.ValidationTimerExpired, seconds);

            timerRunning = true;
        }

    }

    this.StopTimer = function() {
        if (timerRunning) {
            clearInterval(timerID);
            timerRunning = false;
            //alert("StopTimer:"+seconds);
            //$(timerDiv).empty();
        }
    }

}



// Irfan Ashraf FinishGrading() Start

function FinishGradingAssessment() {
    ui.slide.next(function()
    {
        cp.FinishGradingAssessment();
    });
}

// Irfan Ashraf FinishGrading() End

// Irfan AShraf ContinueAfterAssessmentScore(); Start

function ContinueAfterAssessmentScore() {
    ui.slide.next(function()
    {    
        $('.cd-nav-trigger').show();
        cp.ContinueAfterAssessmentScore();
    });		
}

// Irfan Ashraf ContinueAfterAssessmentScore(); End


function ContinueAfterAssessment() {
    ui.slide.next(function()
    {
        cp.ContinueAfterAssessment();
    });	
}

function ShowAnswers() {
    ui.slide.next(function()
    {
        cp.ShowAnswers();
    });	
}

function GetNextRemidiationQuestion() {			
    ui.slide.next(function()
    {
        cp.GetNextRemidiationQuestion();
    });
}

function GetPreviousRemidiationQuestion() {
    ui.slide.prev(function()
    {
        cp.GetPreviousRemidiationQuestion();
    });	    
}

function ShowContent(AssessmentItemID) {
    cp.ShowContent(AssessmentItemID);
}

function ShowHideNextQuestionButton() {
    $(NextQuestionButtonEn).show();
    $(NextQuestionButtonDs).hide();
    isSkipping = false;
}

function GradeAssessment(gradeAssessmentId, gradeArrayAnswerIds, gradeArrayAnswerStrings) {

    ui.slide.next(function()
    {
        cp.GradeAssessment(gradeAssessmentId, gradeArrayAnswerIds, gradeArrayAnswerStrings);
    });
}

function SubmitAssessmentResult(submitAssessmentId, submitArrAnswerIds, submitAnswerStrings) {

    //alert(submitAssessmentId+ "\n" + submitArrAnswerIds + "\n" + submitAnswerStrings); 
    ui.slide.next(function()
    {
        cp.SubmitAssessmentResult(submitAssessmentId, submitArrAnswerIds, submitAnswerStrings, isSkipping);
    });
}




function LockCourseTimerClass() {
    var seconds;
    var minutes;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    var temp = new Array();
    var timerDiv = "";
    var validationDuration=0; 


    this.TimerContainer = function(divName) {
        timerDiv = divName;
    }
    this.InitializeTimer = function(value) {
        //alert("lock timer init");
        if (value >= 60) {
            minutes = Math.floor(value / 60);
            seconds = value % 60;
        } else {
            minutes = 0;
            seconds = value;
        }
        validationDuration=value; 

        lockCourseTimer.StopTimer();
        lockCourseTimer.StartTimer();

    }
    this.StartTimer = function() {
        //alert("lock timer Start Timer");
        if (!timerRunning) {
        
            var $sd = $("#ValidationTimer");
	        var $bar = $sd.find(".progress-bar");        
		    $bar.css("width","100%");        
            $sd.addClass('slideIn');            
            var counter = 0;        

            timerID = setInterval(function() {
                seconds--;
                if (seconds < 0) {
                    seconds = 59;
                    minutes--;
                }
                if ((minutes <= 0) && (seconds <= 0)) {

                    lockCourseTimer.StopTimer();
                    jQuery().ready(function() {
                        $("#studentAuthentication").find("button").eq(0).unbind('click.namespace');
                        $sd.removeClass('slideIn');
                    })
                    cp.SubmitValidationQuestionResult(validationQuestionID, validationAnswerText);


                }

                var str = FormatZero(minutes) + ":" + FormatZero(seconds);
                counter++;
                jQuery().ready(function() {
                    $bar.css("width",((((validationDuration-counter) / validationDuration)) * 100)+"%");
                    $(timerDiv).html(str);
                });
            }, delay);

            timerRunning = true;
        }

    }
    this.StopTimer = function() {
        if (timerRunning) {
            //alert('lock timer stop');
            clearInterval(timerID);
            timerRunning = false;
            $(timerDiv).html("00:00");

        }
    }

}




function SceneTimer() {
    var seconds;
    var minutes;
    var timerID = null;
    var timerRunning = false;
    var delay = 1000;
    var temp = new Array();
    var timerDiv = "";
    var sceneDuration=0;   


    this.TimerContainer = function(divName) {
        timerDiv = divName;
    }
    this.InitializeTimer = function(value) {
        if (value >= 60) {
            minutes = Math.floor(value / 60);
            seconds = value % 60;
        } else {
            minutes = 0;
            seconds = value;
        }
		sceneDuration=value;
        sceneTimer.StopTimer();
        sceneTimer.StartTimer();

    }
    this.StartTimer = function() {
        if (!timerRunning) {
            var $sd = $("#scene-duration");
	        var $bar = $sd.find(".progress-bar");        
		    $bar.css("width","100%");        
            $sd.addClass('slideIn');            
            var counter = 0;
            
            timerID = setInterval(function() {
                seconds--;
                if (seconds < 0) {
                    seconds = 59;
                    minutes--;
                }
                if ((minutes <= 0) && (seconds <= 0)) {

                    sceneTimer.StopTimer();

                    jQuery().ready(function() {
                        $(PlaybuttonEn).show();
                        $(PlaybuttonDs).hide();
                        $sd.removeClass('slideIn');
                    })

                }

                var str = FormatZero(minutes) + ":" + FormatZero(seconds);
                counter++;
                jQuery().ready(function() {
                    $bar.css("width",((((sceneDuration-counter) / sceneDuration)) * 100)+"%");
                    $(timerDiv).html(str);                    
                });
            }, delay);

            timerRunning = true;
        }

    }
    this.StopTimer = function() {
        if (timerRunning) {
            clearInterval(timerID);
            timerRunning = false;

        }
    }

}





// Irfan Submit Logic Start

SubmitTrueFalse = function(assesmentId, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    $(quiz_container).find('input').each(function(i) {
        if (this.checked) {
            arrAnswerIds[i] = this.id;
        }
    });

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {
        //alert(newAssessmentId + "\n" + answerIds 
        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }



} // End Submit True False

SubmitFillInTheBlank = function(assesmentId, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();    
    $(quiz_container).find('input').each(function(i) {
        //  alert(this.id);
        //  alert(this.value);
        //arrAnswerIds[i] = this.id;
        arrAnswerStrings[i] = this.value;
    });

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {

        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }

} // End SubmitFillInTheBlank

SubmitSingleSelectMCQ = function(assesmentId, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    $(quiz_container).find('input').each(function(i) {
        if (this.checked) {
            //alert(this.id);
            //alert(this.value);
            arrAnswerIds[i] = this.id;
            //arrAnswerStrings[i] = this.value;
        }
    });

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {
        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }

} // End SubmitSingleSelectMCQ



SubmitMultiSelectMCQ = function(assesmentId, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    $(quiz_container).find('input').each(function(i) {
        if (this.checked) {
            //alert(this.id);
            //alert(this.value);
            arrAnswerIds[i] = this.id;
            //arrAnswerStrings[i] = this.value;
        }
    });

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {
        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }

} // End SubmitMultiSelectMCQ

SubmitOrdering = function(assesmentId, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    var listBox = $('#question').find('.scene-ordering');
    
    $.each($('#question').find('.scene-ordering').children(), function( k, v ) {
        arrAnswerIds[k] = $(v).find(".ordering-btn").attr("data-value");        
    });    

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {
        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }

} // End SubmitOrdering



SubmitMatching = function(assesmentId, arrAnswers, methodToCall) {
    var newAssessmentId = assesmentId;
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    var totalMatchingBox = arrAnswers.length;

    for (i = 0; i < totalMatchingBox; i++) {
        arrAnswerIds[i] = $('div[data-trg=drag-' + i + ']').find('span').attr('id');
        arrAnswerStrings[i] = $('div[data-trg=drag-' + i + ']').find('div').find('span').html();

        // arrAnswerStrings[i] = arrAnswerStrings[i].replace(/\'/g,"╦");


    }


    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);
    
    if (methodToCall == "submitAssessment") {
        SubmitAssessmentResult(newAssessmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(newAssessmentId, answerIds, answerStrings);
    }

} // End Submit Matching


SubmitImageTarget = function(assesmentId, methodToCall) {
    var arrAnswerIds = new Array();
    var arrAnswerStrings = new Array();

    //alert(selectedHotSpot);
    arrAnswerIds[0] = selectedHotSpot;

    var answerIds = JSON.encode(arrAnswerIds);
    var answerStrings = JSON.encode(arrAnswerStrings);

    if (methodToCall == "submitAssessment") {
        SubmitAssessmentResult(assesmentId, answerIds, answerStrings);
    }
    else if (methodToCall == "gradeAssessment") {
        GradeAssessment(assesmentId, answerIds, answerStrings);
    }

} // End SubmitIMageTarget


function setMediaType(mediaType, imageDiv, mediaDiv) {

    //alert("mediaType"+mediaType+"---"+mediaType.toLowerCase());
    switch (mediaType.toLowerCase()) {
        case "jpg":
            {
                $(imageDiv).show();
                $(mediaDiv).hide();
                break;
            }
        case "jpeg":
            {
                $(imageDiv).show();
                $(mediaDiv).hide();
                break;
            }

        case "gif":
            {
                $(imageDiv).show();
                $(mediaDiv).hide();
                break;
            }
        case "png":
            {
                $(imageDiv).show();
                $(mediaDiv).hide();
                break;
            }
        case "bmp":
            {
                $(imageDiv).show();
                $(mediaDiv).hide();
                break;
            } case "avi":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "mov":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "wmv":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "flv":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "mpeg":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "mpg":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }

        case "swf":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        case "mp4":
            {
                $(mediaDiv).show();
                $(imageDiv).hide();
                break;
            }
        default:
            {

                $(imageDiv).hide();
                $(mediaDiv).hide();
            }

    }
}


// Irfan Submit Logic End
function InsertEscapeSequence(str, ch) {
    str = str.replace(new RegExp(ch, 'g'), "\\" + ch);
    return str;
}

function InsertQustionStem(questionStem) {
    $(quiz_container).find('#questionStem').html(questionStem);
}

function AuthenticateProctor(proctorLogin, proctorPassword) {		
    cp.AuthenticateProctor(proctorLogin, proctorPassword)    
    return false;
}

function IdleTimerReset()
{
    var re = cp.getRenderEngineInstance();
    re.isIdleTimerReset = true;
    re.lastActivityTimeStamp = new Date();

    resetCPIdleTimer();

    cp.ResetSessionTime(); //LCMS-12072 by Yasin
    if ($("#examIdleTimerDialogue").hasClass("modal-is-visible"))
    {       
        ui.svgModal.close('modal-idle');
    }  
    if ($("#timerExpireDialogue").hasClass("modal-is-visible"))
    {        
        ui.svgModal.close('modal-Expire');
    }  
}

var IsShowAmazonAffiliatePanel = false;

//Abdus Samad
//LCMS-11878
//Start
var IsShowRecommendationCoursePanel = false;
//Stop

function IdleTimeNotificationClass() {

    this.ShowIdleTimeDialogue = function(idleTimeMsgHeading, idleTimeMsgContent) {
        
            if ($("#affiliatPanelRedirectDialog").is(":visible")) {
                IsShowAmazonAffiliatePanel = true;
                //LCMS-11878
                //Start
                var IsShowRecommendationCoursePanel = true;
                //Stop
                $('#affiliatPanelRedirectDialog').hide();
            }

            /*
            $(overlay).css({ "opacity": "0.7" });
            $(overlay).fadeIn("slow");
            $("#examIdleTimerDialogue").fadeIn("slow");
            */            
            ui.svgModal.open($("<a data-group='modal-idle'></a>"));
            $("#examIdleTimerDialogue").find("#idleTimeHeading").html(idleTimeMsgHeading);
            $("#examIdleTimerDialogue").find("#idleTimeContent").html(idleTimeMsgContent);            
            $("#examIdleTimerDialogue").find("#btnResume").text(btnResume_text);
            $("#examIdleTimerDialogue").find("#btnResume").focus();
        
            

        $("#examIdleTimerDialogue").find("#btnResume").bind('click.namespace', function() {


            $("#examIdleTimerDialogue").find("#btnResume").unbind('click.namespace');

            /*
            $("#examIdleTimerDialogue").fadeOut("slow");


            if (IsShowAmazonAffiliatePanel) {
                $('#affiliatPanelRedirectDialog').show();
                $("#affiliatPanelRedirectDialog").fadeIn("slow");
                IsShowAmazonAffiliatePanel = false;
            }
            else {
                $(overlay).fadeOut("slow");
            }
            //Abdus Samad
            //LCMS-11878
            //Start

            if (IsShowRecommendationCoursePanel) {
                $('#affiliatPanelRedirectDialog').show();
                $("#affiliatPanelRedirectDialog").fadeIn("slow");
                IsShowRecommendationCoursePanel = false;
            }
            else {
                $(overlay).fadeOut("slow");
            }
            //Stop
            */

//            var re = cp.getRenderEngineInstance();
//            re.isIdleTimerReset = true;
//            re.lastActivityTimeStamp = new Date();

//            resetCPIdleTimer();

//            cp.ResetSessionTime(); //LCMS-12072 by Yasin
//            ui.svgModal.close('modal-idle');
            IdleTimerReset();
            return false;
        });



    }
    
    




    this.proceedSeconds = function(scnds) {

        var numMinutes = Math.floor(eval(scnds / 60));
        var numSeconds = eval(scnds % 60);


        if (numMinutes < 0) { return false; }

        if (numMinutes == 0) {
            $("#examIdleTimerDialogue").find("#idleMinutesOrSecondsLabel").html(idleMinutesOrSecondsLabel_text_sec);
        }
        else {
            $("#examIdleTimerDialogue").find("#idleMinutesOrSecondsLabel").html(idleMinutesOrSecondsLabel_text_min);
        }

        document.getElementById("spnMinutes").innerHTML = numMinutes;
        document.getElementById("spnSeconds").innerHTML = (numSeconds < 10 ? '0' : '') + numSeconds;



        if (numMinutes == 0 && numSeconds == 0) {
            cp.SendActionToTakeOnIdleUserTimeoutCommand();
            var re = cp.getRenderEngineInstance();            
            re.isIdleTimeElapsed = true;
            ui.svgModal.close('modal-idle');
        }

        if (numSeconds == 0) {

            document.getElementById("spnSeconds").innerHTML = (numSeconds < 10 ? '0' : '') + numSeconds;


            if (numMinutes > 0) {
                document.getElementById("spnMinutes").innerHTML = numMinutes;
                --numMinutes;
            }
            else if ((numMinutes == 0) && (numSeconds == 0)) {
                // Time Up
                $("#examIdleTimerDialogue").find("#btnResume").unbind('click.namespace');
                //$("#examIdleTimerDialogue").fadeOut("slow");
                //$(overlay).fadeOut("slow");


            }
        }
        else {
            document.getElementById("spnSeconds").innerHTML = (numSeconds < 10 ? '0' : '') + numSeconds;
        }


    } // proceedSeconds End    







    // -----------------------------------------------------------------------------

}


