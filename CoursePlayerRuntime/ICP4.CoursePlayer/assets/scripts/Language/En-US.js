//On Load
process_heading_tx = "Please wait a moment ..."
processing_text_tx = "The course content is loading."
processed_text_tx = "The course content is loaded"
assessment_toggle_flag1_tx = "Click to remove the flag on this question.";
assessment_toggle_flag2_tx = "Click to flag this question for review.";
holding_key_tx = "Holding key with a mouse click is not allowed";
course_locked_title = "Course Closed";
cancel_text = "Cancel";

//On Load

assessment_focusOutLockedOutMessage_warning = "Warning!";
assessment_focusOutLockedOutMessage_text = "<p> If you leave the examination window for any reason during the exam, the course will lock and you will fail this exam attempt. </p><p>  You have clicked in a way that will cause you to leave the examination window.</p><p> Do you want to continue? Click Cancel to stay in the exam.</p>";
assessment_focusOutLockedOutMessage_timer = "Course automatically locks in <strong class=\"primary-font\"><span id=\"spanClickAwayTimer\"></span>&nbsp;seconds.</strong>";
assessment_focusOutLockedOutButtonYes_text = "Exit Course";
assessment_focusOutLockedOutButtonNo_text = "Stay & Continue";

Not_Completed = "Not Completed"; // Yasin LCMS-12764
Response_Recorded = "Response Recorded"; // Yasin LCMS-12764

ToolTipMenuButton = "Menu Button";
ToolTipMenuButtonDisables = "Button is disabled";
ToolTipMenuButtonBookmark = "Bookmark Button";
ToolTipMenuButtonConfig = "Configuration Button";
ToolTipMenuButtonCourseCompletionReport = "Course Completion Report";
ToolTipMenuButtonAffiliatePanel = "Click to see / close Amazon book suggestions";
ToolTipMenuButtonRecomended = "Click to see / close Recommended Courses";
ToolTipMenuButtonHelp = "Help Button";
InstructorInfoLinkText = "About The Author";
NextText = "Next";
BackText = "Previous";
BeginPreAssessmentButtonText = "Begin Pre Assessment";
Question_text = "Question";
of = "of";
INCORRECT = "INCORRECT";
CORRECT = "CORRECT";
FeedbackICP4 = "Feedback";
ReturntoAssessmentResultsText = "Return to Assessment Results";
ReturntoAnswerReviewText = "Return to Answer Review Page";
AssessmentIncompleteText = "Assessment Incomplete";
BeginLessonAssessmentButtonText = "Begin Lesson Assessment";
BeginPostAssessmentButtonText = "Begin Final Exam";
NotCompletedText = "Not&nbsp;Completed";
CourseReviewnotAllowedText = "Course Review Not Allowed";
InformationText = 'Information';
NoRecomendedBookText = 'No recommended books found.';

function ChangeCourseLoadingMessages() {
    $("#process .process_heading").text(process_heading_tx);
    //  $("#process_text").text(processing_text);
    //    alert('ChangeCourseLoadingMessages');
}
help_text = "Help";
help_linkstudent_text = "Individual Students Click Here";
help_linkcoporate_text = "Corporate Clients Click Here";
Close_text = "Close";
ChatForHelp_text = "For Live Chat Click Here";
idleMinutesOrSecondsLabel_text_sec = "second";
idleMinutesOrSecondsLabel_text_min = "minutes";
btnResume_text = "Stay & Continue";
AddBookmark_text = "Add Bookmark";
Submit_text = "submit";
Continue_text = "Continue";
Player_Configuration_text = "Player Configuration";
audio_text = "Speaker:";
on_text = "On";
off_text = "Off";
volume_text = "Volume:";
Captioning_text = "Captioning:";
ok_text = "OK";
glossaryTerms="Glossary Terms & Definitions";
aboutcourse="About This Course";
coursematerial="Course Materials & Files";
glossaryTermsText='This section provides you with different terms found within the course, and exposes you to the definitions and meanings behind them. Use the quick search.';
coursematerialText='This section provides you with the core assets you need to create a complete hands-on experience with this course. Take these materials and follow along with the lessons and instructions to recreate the same solutions and scenarios on your own computer. It\'s more effective than written or even lab exercises.';
readmoreText='Read more';

progressBarTxt = "Course Progress";
courseEval_Req_Validation = "Please provide the answers for the questions appearing in red.";

btnBeginPracticeExamText = 'Begin Practice Exam';
btnSkipPracticeExamText = 'Skip Practice Exam';
StartAssessmentOverText = 'Start Assessment Over';
btnResumeAssessmentText = 'Resume Assessment from Previous Session';
recomendedCourseDialogMsg = 'The course you selected will open in a new window. Do you want to continue?';
recomendedCourseDialogMsg_NotFound = 'No recommended courses found.';
true_text = "True";
false_text = "False";
and_text = "and";


answer_text = "Answer:";

Validation_Message1 = 'Please Select Question from Question Set 1.';
Validation_Message2 = 'Please Select Question from Question Set 2.';
Validation_Message3 = 'Please Select Question from Question Set 3.';
Validation_Message4 = 'Please Select Question from Question Set 4.';
Validation_Message5 = 'Please Select Question from Question Set 5.';

Validation_Message_Must = 'You must provide an answer for each of the five questions. Please fill in the remaining selections.';
validationNoteText = 'Your answer must exactly match the answer you entered in your profile.';


$(document).ready(function() {
    //Chat for Help
    $("#helpDialogue h2").text(help_text);
    $("#help_linkstudent_text").text(help_linkstudent_text);
    $("#help_linkcoporate_text").text(help_linkcoporate_text);
    $("#ChatForHelp").text(ChatForHelp_text);
    $("#ChatForHelpClose button").text(Close_text);
    //$("#bookmarkDialogue button").text(Close_text);

    //Configuration
    $("#btnPlayerConfiguration").attr("title",Player_Configuration_text);
    $("#volumeText").html(volume_text);
    $("#speakerText").html(audio_text);
    $("#captioningText").html(Captioning_text);
    
    $("#CourseMaterialFileHeadingText").html(coursematerial);
    $("#GlossaryTermsHeadingText").html(glossaryTerms);
    $("#AboutCourseHeadingText").html(aboutcourse);
    $("#GlossaryTermsText").html(glossaryTermsText);
    $("#readmoreText").html(readmoreText);        
    $("#coursereadMoreText").html(readmoreText);
    if(coursematerialText.length > 140)
    {
        $('#CourseMaterialFileText').html(coursematerialText.substring(0, 140));
    }
    else
    {
        $('#CourseMaterialFileText').html(coursematerialText);
    }        
    
    //$("#configrationDialogue").find("h1").text(Player_Configuration_text);
    //$("#configrater").find("li").eq(0).find("span").text(audio_text);
    //$("#configrater").find("label").eq(0).text(on_text);
    //$("#configrater").find("label").eq(1).text(off_text);

    //$("#configrater").find("li").eq(2).find("span").text(volume_text);
    //$("#configrater").find("li").eq(4).find("span").text(Captioning_text);
    //$("#configrater").find("li").eq(5).find("label").eq(0).text(on_text);
    //$("#configrater").find("li").eq(5).find("label").eq(1).text(off_text);
    //$("#configrationDialogue").find("button").text(ok_text);
    //$("#configrationDialogue").find("button").css({'margin-top': '30px'});
     

    //Tooltip
    $('#IcoOptions a').attr('title', ToolTipMenuButton);
    $('#IcoOptionsDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoBookMark a').attr('title', ToolTipMenuButtonBookmark);
    $('#IcoBookMarkDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoConfigure a').attr('title', ToolTipMenuButtonConfig);
    $('#IcoConfigureDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoCourseCompletion a').attr('title', ToolTipMenuButtonCourseCompletionReport);
    $('#IcoCourseCompletionDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoAmazonAffiliatePanel a').attr('title', ToolTipMenuButtonAffiliatePanel);
    $('#IcoAmazonAffiliatePanelDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoRecommendationCoursePanel a').attr('title', ToolTipMenuButtonRecomended);
    $('#IcoRecommendationCoursePanelDs a').attr('title', ToolTipMenuButtonDisables);

    $('#IcoHelp a').attr('title', ToolTipMenuButtonHelp);
    $('#IcoHelpDs a').attr('title', ToolTipMenuButtonDisables);

    //Instructor Info:
    $('#InstructorInformation a').text(InstructorInfoLinkText);
    $('#InstructorInformationDs a').text(InstructorInfoLinkText);

    $('#PlaybuttonEn a').attr('title', NextText);
    $('#BackbuttonEn a').attr('title', BackText);

    $('#PlaybuttonDs span').attr('title', ToolTipMenuButtonDisables);
    $('#BackbuttonDs span').attr('title', ToolTipMenuButtonDisables);

    $('#NextQuestionButtonEn a').attr('title', NextText);
    $('#BackQuestionButtonEn a').attr('title', BackText);

    $("#studentAuthentication").find("label").eq(0).find("span").text(answer_text);
    $("#validationNote").text(validationNoteText);


});

