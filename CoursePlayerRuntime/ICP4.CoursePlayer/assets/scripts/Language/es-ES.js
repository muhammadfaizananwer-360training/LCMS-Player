//On Load
process_heading_tx = "Por favor, espere un momento…"
processing_text_tx = "Los contenidos de su curso se están cargando."
processed_text_tx = "Los contenidos de su curso se cargan."
assessment_toggle_flag1_tx = "Haga clic para quitar la marca sobre esta pregunta.";
assessment_toggle_flag2_tx = "Haga clic para marcar esta pregunta para su revisión.";
holding_key_tx = "Si mantiene presionada una tecla con un clic no se permite.";
course_locked_title = "Curso Cerrado";
cancel_text = "Cancelar";
glossaryTerms="Glossary Terms & Definitions";
aboutcourse="About This Course";
coursematerial="Course Materials & Files";
glossaryTermsText='This section provides you with different terms found within the course, and exposes you to the definitions and meanings behind them. Use the quick search.';
coursematerialText='This section provides you with the core assets you need to create a complete hands-on experience with this course. Take these materials and follow along with the lessons and instructions to recreate the same solutions and scenarios on your own computer. It\'s more effective than written or even lab exercises.';
readmoreText='Read more';

//On Load

assessment_focusOutLockedOutMessage_warning = "¡ Advertencia !";
assessment_focusOutLockedOutMessage_text = "<p>Si sale de la ventana de examen por cualquier razón durante el examen, el curso se bloqueará y usted fallará este intento de examen.</p><p> Ha hecho clic en una forma que hará que usted deje la ventana de examen.</p><p> ¿Quieres continuar? Haga clic en Cancelar para permanecer en el examen.</p>";
assessment_focusOutLockedOutMessage_timer = "Curso bloquea automáticamente en <strong class=\"primary-font\"><span id=\"spanClickAwayTimer\"></span>&nbsp;segundo.</strong>";
assessment_focusOutLockedOutButtonYes_text = "Deje el Examen";
assessment_focusOutLockedOutButtonNo_text = "Stay & Continue";

ToolTipMenuButton = "Botón de Menú";
ToolTipMenuButtonDisables = "Este botón está desactivado";
ToolTipMenuButtonBookmark = "Botón de Marcadores";
ToolTipMenuButtonConfig = "Botón de Configuración";
ToolTipMenuButtonCourseCompletionReport = "Informe de Finalización del Curso";
ToolTipMenuButtonAffiliatePanel = "Haga clic para ver / cerrar sugerencias sobre libros de Amazon.";
ToolTipMenuButtonRecomended = "Haga clic para ver / cerrar sugerencias respecto a otros cursos.";
ToolTipMenuButtonHelp = "Botón de Ayuda";
InstructorInfoLinkText = "La información sobre el instructor";
NextText = "Siguiente";
BackText = "Anterior";
BeginPreAssessmentButtonText = "Comenzar la Pre-evaluación";
Question_text = "Pregunta";
of = "de";
INCORRECT = "Incorrecto";
CORRECT = "Correcto";
FeedbackICP4 = "Retroalimentación";
ReturntoAssessmentResultsText = "Volver a los Resultados de la Evaluación";
ReturntoAnswerReviewText = "Volver a la página de revisión";
AssessmentIncompleteText = "Evaluación incompleta";
BeginLessonAssessmentButtonText = "Comenzar la Evaluación";
BeginPostAssessmentButtonText = "Comenzar el Examen";
NotCompletedText = "No&nbsp;Completado"; 
CourseReviewnotAllowedText = 'Curso de Revisión no permitida'

Not_Completed = "No se complete"; // Yasin LCMS-12764
Response_Recorded = "Respuesta registrada"; // Yasin LCMS-12764

Validation_Message1 = 'Por favor seleccione una pregunta de grupo la pregunta 1.';
Validation_Message2 = 'Por favor seleccione una pregunta de grupo la pregunta 2.';
Validation_Message3 = 'Por favor seleccione una pregunta de grupo la pregunta 3.';
Validation_Message4 = 'Por favor seleccione una pregunta de grupo la pregunta 4.';
Validation_Message5 = 'Por favor seleccione una pregunta de grupo la pregunta 5.';

Validation_Message_Must = 'Debe proporcionar una respuesta para cada una de las cinco preguntas. Por favor complete las selecciones restantes.';
validationNoteText = 'Su respuesta debe coincidir exactamente con la respuesta que ha escrito en su perfil.';

function ChangeCourseLoadingMessages() {
    $("#process .process_heading").text(process_heading_tx);
    //  $("#process_text").text(processing_text);
    //    alert('ChangeCourseLoadingMessages');
}
help_text = "Ayuda";
help_linkstudent_text = "Estudiantes individuales: Haga clic aquí.";
help_linkcoporate_text = "Clientes corporativos: Haga clic aquí.";
Close_text = "Cerrar";
ChatForHelp_text = "Por Live Chat (comunicar con un representante), haga clic aquí.";
idleMinutesOrSecondsLabel_text_sec = "segundos";
idleMinutesOrSecondsLabel_text_min = "minutos";
btnResume_text = "Continuar con el Curso";
AddBookmark_text = "Añadir Favorito";
Submit_text = "Enviar";
Continue_text = "Continuar";
Player_Configuration_text = "Configuración de Curso";
audio_text = "Audio";
on_text = "Activado";
off_text = "Desactivado";
volume_text = "Volumen";
Captioning_text = "Subtitulado";
ok_text = "OK";

answer_text = "Respuesta:";

progressBarTxt = "Avance del Curso";
courseEval_Req_Validation = "Por favor, dar las respuestas para las preguntas que aparecen en rojo.";

btnBeginPracticeExamText = 'Empezar el Examen de Práctica';
btnSkipPracticeExamText = 'Saltar el Examen de Práctica';
StartAssessmentOverText = 'Empezar de Nuevo en la Evaluación';
btnResumeAssessmentText = 'Reiniciar el Examen de la Sesión Anterior';
recomendedCourseDialogMsg = 'El curso que ha seleccionado se abrirá en una nueva ventana. ¿Quieres continuar?';
recomendedCourseDialogMsg_NotFound = 'No hay cursos recomendados encontrado.';
InformationText = 'Información';
NoRecomendedBookText = 'No hay libros recomendados que se encuentran';
true_text = "Verdadero";
false_text = "Falso";
and_text = "y";

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
//    $("#configrationDialogue").find("h1").text(Player_Configuration_text);
//    $("#configrater").find("li").eq(0).find("span").text(audio_text);
//    $("#configrater").find("label").eq(0).text(on_text);
//    $("#configrater").find("label").eq(1).text(off_text);

//    $("#configrater").find("li").eq(2).find("span").text(volume_text);
//    $("#configrater").find("li").eq(4).find("span").text(Captioning_text);
//    $("#configrater").find("li").eq(5).find("label").eq(0).text(on_text);
//    $("#configrater").find("li").eq(5).find("label").eq(1).text(off_text);
//    $("#configrationDialogue").find("button").text(ok_text);

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

