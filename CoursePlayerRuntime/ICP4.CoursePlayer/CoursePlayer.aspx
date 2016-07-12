<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoursePlayer.aspx.cs" Inherits="ICP4.CoursePlayer.CoursePlayer" %>

<!DOCTYPE html>
<html lang="en" class="no-js" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0">
    
    <title>LCMS Course Player</title>
	<!-- BEGIN CSS -->
	<% string g = "course player testing"; %>
    <link rel="stylesheet" type="text/css" href="assets/plugins/bootstrap-3.3.5-dist/css/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="assets/plugins/tour-guide/css/style.css" />
	<link rel="stylesheet" href="assets/plugins/bootstrap-switch/static/stylesheets/bootstrap-switch-metro.css" />
	<link rel="stylesheet" type="text/css" href="assets/css/circle.css" />
	<link rel="stylesheet" type="text/css" href='<%="assets/css/style.css?g=" + g %>'/>
	
    <link rel="shortcut icon" href="favicon.ico" />
    <!-- END CSS -->
	
    <!-- BEGIN SCRIPTS -->
    <script type="text/javascript" src="assets/plugins/jquery.min.js"></script>  
    <script type="text/javascript" src="assets/plugins/jquery-ui.min.js"></script>	
    <script type="text/javascript" src="assets/plugins/jquery.ui.touch-punch.min.js"></script>	
    <script type="text/javascript" src="assets/plugins/html5shiv.js"></script>
    <script type="text/javascript" src="assets/plugins/respond.min.js"></script>
	<script type="text/javascript" src="assets/plugins/modernizr.js"></script>
	<script type="text/javascript" src="assets/plugins/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>
	<script type="text/javascript" src="assets/plugins/bootstrap-switch/static/js/bootstrap-switch.js"></script>
	<script type="text/javascript" src="assets/plugins/jquery-slimscroll/jquery.slimscroll.js"></script>	
    <script type="text/javascript" src="assets/plugins/pdfobject.js"></script>
    <script type="text/javascript" src="assets/plugins/platform.js" async defer></script>
	
	<script type="text/javascript" src="assets/plugins/embedly-platform.js" async charset="UTF-8"></script>
	<script type="text/javascript" src="assets/plugins/jwplayer-7.1.4/jwplayer.js"></script>
	<script type="text/javascript" src="assets/plugins/snap-svg/snap.svg-min.js"></script>
	<script type="text/javascript" src="assets/plugins/bootstrap-slider/js/bootstrap-slider.js"></script>
	<script type="text/javascript" src="assets/plugins/isotope.pkgd.min.js"></script>
	<script type="text/javascript" src="assets/plugins/BrowserDetect.js"></script>	
    <script type="text/javascript" src="https://www.dropbox.com/static/api/2/dropins.js" id="dropboxjs" data-app-key="5ep2lw0rah3qmbk"></script>
	<script type="text/javascript" src="assets/scripts/JSON.js"></script>	
	<script type="text/javascript" src="assets/scripts/json2.js"></script>
	
	
    <script type='text/javascript' src='<%="assets/scripts/ui.js?g=" + g %>'></script>
    <script type="text/javascript" src='<%="JSPlayer/j360player.js?g=" + g%>'></script>
    <script type="text/javascript" src='<%="JSPlayer/init.js?g=" + g%>'></script>    
    <script type="text/javascript" src="assets/scripts/Chart.js"></script>
    <script type="text/javascript" src="assets/scripts/AC_RunActiveContent.js"></script>
    <script type="text/javascript" src='<%="assets/scripts/com/CoursePlayerEngine.js?g=" + g %>'></script>
    <script type="text/javascript" src='<%="assets/scripts/com/CommunicationEngine.js?g=" + g %>'></script>
    <script type="text/javascript" src='<%="assets/scripts/com/RenderEngine.js?g=" + g %>'></script>
    <script type="text/javascript" src='<%="assets/scripts/cptop.js?g=" + g %>'></script>
	<!-- END SCRIPTS -->
</head>
<body class="pre-loader launching" onunload="windowClosed();" onmousedown="checkPressedKeys();"
    onkeydown="storeKeyPress(event);" onkeyup="onKeyUp(event);" onfocus="storeKeyUp();">        
    <form id="form1" runat="server">	
    <div id="security" style="overflow: hidden; width: 5px; height: 5px; display:none">
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
    <div id="timerCancelling">
    </div>
    <asp:HiddenField ID="txtWidth" runat="server" />
    <asp:HiddenField ID="txtHeight" runat="server" />
    <asp:HiddenField ID="txtURL" runat="server" />
    <div id="preloaderOLD" visible="false" style="display:none">
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

        var jsLink = $("<script type='text/javascript' src='assets/scripts/Language/" + variant + ".js'>");
        $("head").append(jsLink);
        ChangeCourseLoadingMessages();

        //Translation End
        process();
    </script>    
    <div id="disableScreen">
    </div>  
    
    <!--
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
    -->
    
    <div id="ProctorLockCourse" style="display: none">
        <br />
       Hello, Your course is locked.And click
        <button class="button">
            Here</button>
        to Unlock.
        <br />
        <br />
    </div>   
    <div id="dialog"  role="alert" style="display:none;" >
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
                          
	<!-- BEGIN WRAPPER -->
    <div id="wrapper" class="options">
	
		<!-- BEGIN SLIDE LOADER OVERLAY -->
		<div id="svg-loader-bg" class="cd-svg-cover" data-step1="M1402,800h-2V0.6c0-0.3,0-0.3,0-0.6h2v294V800z" data-step2="M1400,800H383L770.7,0.6c0.2-0.3,0.5-0.6,0.9-0.6H1400v294V800z" data-step3="M1400,800H0V0.6C0,0.4,0,0.3,0,0h1400v294V800z" data-step4="M615,800H0V0.6C0,0.4,0,0.3,0,0h615L393,312L615,800z" data-step5="M0,800h-2V0.6C-2,0.4-2,0.3-2,0h2v312V800z" data-step6="M-2,800h2L0,0.6C0,0.3,0,0.3,0,0l-2,0v294V800z" data-step7="M0,800h1017L629.3,0.6c-0.2-0.3-0.5-0.6-0.9-0.6L0,0l0,294L0,800z" data-step8="M0,800h1400V0.6c0-0.2,0-0.3,0-0.6L0,0l0,294L0,800z" data-step9="M785,800h615V0.6c0-0.2,0-0.3,0-0.6L785,0l222,312L785,800z" data-step10="M1400,800h2V0.6c0-0.2,0-0.3,0-0.6l-2,0v312V800z">
			<svg height='100%' width="100%" preserveAspectRatio="none" viewBox="0 0 1400 800">
				<title>SVG cover layer</title>
				<desc>an animated layer to switch from one slide to the next one</desc>
				<path id="cd-changing-path" d="M1402,800h-2V0.6c0-0.3,0-0.3,0-0.6h2v294V800z"/>
			</svg>
		</div>
		<!-- END SLIDE LOADER OVERLAY -->

        <!-- BEGIN MODALS -->
		<div class="modals">			
			<!-- BEGIN DYNAMIC -->
			<div class="modal fade" id="dynamicModal">
			  <div class="modal-dialog modal-md">
				<div class="modal-content">
				  <div class="modal-header">
					<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>
					<h3 class="modal-title">
						<!-- Dynamic Title -->						
					</h3>
				  </div>
				  <div class="modal-body">				
					<!-- Dynamic Message -->					
				  </div>
				  <div class="modal-footer">				  
					<!-- Dynamic Action Buttons -->					
				  </div>
				</div>
			  </div>
			</div>
			<!-- END DYNAMIC -->			
		</div>
		<!-- END MODALS -->
		
		<!-- BEGIN TOUR GUIDE -->
		<ul class="cd-tour-wrapper">
			<li id="guide-menu" class="cd-single-step">
				<span></span>
				<div class="cd-more-info right">
					<h2>The Main Menu</h2>
					<p>This button allows for you to expand and collapse the main menu of your course, which includes the course outline, glossary, downloads, and other important features of your course.</p>
					<img src="assets/img/step-1.png" alt="step 1">					
				</div>
			</li>
			<li id="guide-menu-expend" class="cd-single-step">
				<span></span>
				<div class="cd-more-info right">
					<h2>Expanding The Menu</h2>
					<p>When clicked or tapped, the main areas of your course become visible. You can expand and collapse this menu at any time during your learning experience.</p>
					<img src="assets/img/step-2.png" alt="step 2">
				</div>
			</li>
			<li id="guide-prev" class="cd-single-step">
				<span></span>
				<div class="cd-more-info top">
					<h2>Previous</h2>
					<p>This allows for you to navigate backwards in the course, one slide or module at a time.</p>
					<img src="assets/img/step-3.png" alt="step 3">
				</div>
			</li>
			<li id="guide-next" class="cd-single-step">
				<span></span>
				<div class="cd-more-info top">
					<h2>Next</h2>
					<p>This allows for you to navigate forward in the course, one slide or module at a time.</p>
					<img src="assets/img/step-4.png" alt="step 4">
				</div>
			</li>
			<li id="guide-timeline" class="cd-single-step">
				<span></span>
				<div class="cd-more-info top">
					<h2>Course Time Line</h2>
					<p>The course time line gives you a visual of how much progress you have made through the course. Use the course outline in the main menu to easily access lessons, quizzes, and final exams.</p>
					<img src="assets/img/step-5.png" alt="step 5">
				</div>
			</li>
			<li id="guide-config" class="cd-single-step">
				<span></span>
				<div class="cd-more-info top">
					<h2>Volume Settings</h2>
					<p>This menu gives you access to the volume controls for the course, including the ability to adjust the volume or turn it completely off. It also allows for the ability to show or hide closed captioning support.</p>
					<img src="assets/img/step-6.png" alt="step 6">
				</div>
			</li>
			<li id="guide-save" class="cd-single-step">
				<span></span>
				<div class="cd-more-info left">
					<h2>Saving Your Course</h2>
					<p>This button allows you to save your course progress. You can also exit the course with this option. If you save and exit your course while taking assessments and exams, progress will not be saved.</p>
					<img src="assets/img/step-7.png" alt="step 7">
				</div>
			</li>
			<li id="guide-bookmarks" class="cd-single-step">
				<span></span>
				<div class="cd-more-info bottom">
					<h2>Bookmarks</h2>
					<p>This allows you to bookmark sections of the course  you may need to reference at a later point. You can customize all of your bookmarks by giving them a title. Bookmarks are organized by order.</p>
					<img src="assets/img/step-8.png" alt="step 8">
				</div>
			</li>
			<li id="guide-time-spent" class="cd-single-step">
				<span></span>
				<div class="cd-more-info bottom">
					<h2>Time Spent</h2>
					<p>This area shows you the total amount of time spent on this course, from start to finish. You must hit the save and close menu in order to record the latest amount of time spent.</p>
					<img src="assets/img/step-9.png" alt="step 9">
				</div>
			</li>
			
			
		</ul>
		<!-- END TOUR GUIDE -->
		
        <!-- BEGIN CD MODALS -->
        <div class="cd-modals">
		
			<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-dynamic">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-1" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-2" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-3" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a href="javascript:ui.svgModal.close('modal-dynamic');" class="modal-close" title="Close"></a>
				<div class="cd-modal-content">
					
				</div>
			</div>
			<!-- END CD MODAL -->
			
			<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-trigger-report">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-7" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-8" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-9" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a href="javascript:ui.svgModal.close('modal-trigger-report');" class="modal-close" title="Close Modal"></a>
				<div class="cd-modal-content" id="CompletionReport">
					<h1><span id="CourseCompletionModalHeading"></span></h1>
					<div class="row cHolders text-center">
						<div class="col-sm-4 cHolder" style="display:none;">
						  <div class="cHead">
							  <div class="c100 p60 pass">
									<span class="perc">60</span>
									<div class="slice">
										<div class="bar"></div>
										<div class="fill"></div>
									</div>
							  </div>
						  </div>
						  <h2>Pre Assessment</h2>
						  <span>You must pass with a mastery of 40%.</span>
						</div>
						<div class="col-sm-4 cHolder" style="display:none;">
						  <div class="cHead">
							  <div class="c100 p12 progs">
									<span class="perc">12</span>
									<div class="slice">
										<div class="bar"></div>
										<div class="fill"></div>
									</div>
							  </div>
						  </div>
						  <h2>Quiz</h2>
						  <span>You must pass with a mastery of 60%.</span>
						</div>
						<div class="col-sm-4 cHolder" style="display:none;">
						  <div class="cHead">
							  <div class="c100 p40 fail">
									<span class="perc">40</span>
									<div class="slice">
										<div class="bar"></div>
										<div class="fill"></div>
									</div>
							  </div>
						  </div>
						  <h2>Post Assessment</h2>
						  <span>You must pass with a mastery of 80%.</span>
						</div>
					</div>
					<h2><span id="CourseCompletionModalText"></span></h2>
					<div class="blockquote-list">
						<blockquote class="not-meet" id="IsViewEverySceneInCourseEnabledRow" style="display: none">
						    <span id="IsViewEverySceneInCourseAchievedText">You must view every scene in the course.</span>
                        </blockquote>                        
						<blockquote class="not-meet" id="IsCompleteAfterNOUniqueCourseVisitEnabledRow" style="display: none">
						    <span id="IsCompleteAfterNOUniqueCourseVisitAchievedText">The course can only be completed after at least 1 course launch</span>
                        </blockquote>  
						<blockquote class="not-meet" id="IsPostAssessmentEnabledRow" style="display: none">
						    <span id="IsPostAssessmentAttemptedText">You must attempt post assessment in order to complete this course.</span>
                        </blockquote> 
						<blockquote class="not-meet" id="IsPostAssessmentMasteryEnabledRow" style="display: none">
						    <span id="IsPostAssessmentMasteryAchievedText">You must pass the post assessment with a mastery of 80%. You have not attempted the post assessment yet.</span>
                        </blockquote> 
						<blockquote class="not-meet" id="IsQuizMasteryEnabledRow" style="display: none">
						    <span id="IsQuizMasteryAchievedText">You must pass the quiz with a mastery of 70%.</span>
                        </blockquote>
	                    <blockquote class="not-meet" id="IsPreAssessmentMasteryEnabledRow" style="display: none">
						    <span id="IsPreAssessmentMasteryAchievedText">You must pass the pre assessment with a mastery of 70%.</span>
                        </blockquote> 
                        <blockquote class="not-meet" id="IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessEnabledRow" style="display: none">
						    <span id="IsMustCompleteWithinSpecifiedAmountOfTimeaAfterFirstAccessText">You must complete the course within 48 hours after first course launch.</span>
                        </blockquote> 
                        <blockquote class="not-meet" id="IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateEnabledRow" style="display: none">
						    <span id="IsMustCompleteWithinSpecifiedAmountOfTimeAfterRegDateText">You must complete the course within 3651 days After registration.</span>
                        </blockquote>
                        <blockquote class="not-meet" id="IsembeddedAcknowledgmentEnabledRow" style="display: none">
						    <span id="IsembeddedAcknowledgmentAchievedText">Must Accept Embedded Acknowledgement.</span>
                        </blockquote>
                        <blockquote class="not-meet" id="IsRespondToCourseEvaluationEnabledRow" style="display: none">
						    <span id="IsRespondToCourseEvaluationAchievedText">You must complete all evaluations.</span>
                        </blockquote>  
                        <blockquote class="not-meet" id="IsAcceptAffidavitAcknowledgmentRow" style="display: none">
						    <span id="IsAcceptAffidavitAcknowledgmenAchievedText">You must accept affidavit acknowledgment.</span>
                        </blockquote> 
                        <blockquote class="not-meet" id="IsSubmitSignedAffidavitRow" style="display: none">
						    <span id="IsSubmitSignedAffidavitAchievedText">You must submit a signed affidavit.</span>
                        </blockquote> 
					</div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
			<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-trigger-feeds">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-10" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-11" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-12" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a href="javascript:ui.svgModal.close('modal-trigger-feeds');" class="modal-close" title="Close Modal"></a>
				<div class="cd-modal-content">
					<h1>The Feeds</h1>
					<h2>Heading 1</h2>
					<div class="blockquote-list">
						<blockquote>The course can only be completed after at least 9 course launches.<br><small>(not applicable for preview mode).</small></blockquote>
						<blockquote>You must view every scene in the course.</blockquote>
						<blockquote>You must attempt post assessment in order to complete this course.</blockquote>
						<blockquote>The course can only be completed after at least 9 course launches.<br><small>(not applicable for preview mode).</small></blockquote>
					</div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
			<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-trigger-rec">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-13" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-14" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-15" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a href="javascript:ui.svgModal.close('modal-trigger-rec');" class="modal-close" title="Close Modal"></a>
				<div class="cd-modal-content">
					<h1>Recommendations</h1>
					<p>Based on the course, you may be interested in some of the following:</p>
					<div class="isotope-filter-btns">
					  <button type="button">All</button>
					  <button type="button">Popular</button>
					  <button type="button">New Releases</button>
					</div>
					<div class="isotope">
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					  <div class="element-item" data-category="transition">
						<h4>Mercury</h4>
						<p>Hg</p>
						<p>80</p>
						<p>200.59</p>
					  </div>
					</div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
			<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-trigger-bookmark">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-16" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-17" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-18" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a href="javascript:ui.svgModal.close('modal-trigger-bookmark');" class="modal-close" title="Close Modal"></a>
				<div class="cd-modal-content">
					<h1><span id="bookmarkModalHeading"></span></h1>
					<div class="input-group" id="bookmarkDialogue">
						<input type="text" class="form-control cd-input" id="bookmarkTitle" name="bookmarkTitle" placeholder="" />
						<span class="input-group-btn">
							<button class="cd-btn main-action" type="button"></button>
						</span>
					</div>
					<h2><span id="bookmarkDialogHeading"></span></h2>
					<p style="margin-top:0"><span id="bookmarkContent"></span></p>
					<div id="bookmarks" class="blockquote-list interactive">						
					</div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
            <!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-idle" id="examIdleTimerDialogue">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-19" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-20" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-21" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a class="hide" data-group="modal-idle" data-trg="idle" data-type="cd-modal-trigger"></a>
				
				<a href="javascript:ui.svgModal.close('modal-idle');IdleTimerReset();" class="modal-close" title="Close"></a>
				
				<div class="cd-modal-content">    
                    <h1><span id="idleTimeHeading"></span></h1>                                           
                        <p><span id="idleTimeContent"></span>
                            <strong class="primary-font"><span id="spnMinutes"></span>:<span id="spnSeconds"></span>&nbsp;<span id="idleMinutesOrSecondsLabel">minutes</span>.</strong>
                        </p> 
                        <div>
					        <a id="btnResume" href="javascript:;" class="cd-btn main-action">Stay & Continue</a>
					    </div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
            <!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-Expire" id="timerExpireDialogue">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-22" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-23" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-24" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a class="hide" data-group="modal-Expire" data-trg="Expire" data-type="cd-modal-trigger"></a>
				
				<a href="javascript:ui.svgModal.close('modal-Expire');IdleTimerReset();" class="modal-close" id="expireClose" title="Close"></a>
				
				<div class="cd-modal-content">    
                    <h1><span id="timerExpireHeading"></span></h1>                                           
                    <p></p> 
                    <div>
					    <a id="expButton" href="javascript:;" class="cd-btn main-action">Stay & Continue</a>
					</div>
				</div>
			</div>
			<!-- END CD MODAL -->
			
<!-- BEGIN CD MODAL -->
			<div class="cd-modal" data-modal="modal-ClickAway" id="focusOutLockedOutMessage">
				<div class="cd-svg-bg" 
				data-step1="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z" 
				data-step2="M33.8,690l-188.2-300.3c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1L959.6,110c0.1,0.1,0,0.3-0.1,0.3 L34.1,690.1C34,690.2,33.9,690.1,33.8,690z" 
				data-step3="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z" 
				data-step4="M-329.3,504.3l-272.5-435c-0.1-0.1,0-0.3,0.1-0.3l925.4-579.8c0.1-0.1,0.3,0,0.3,0.1l272.5,435c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-329,504.5-329.2,504.5-329.3,504.3z" 
				data-step5="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z" 
				data-step6="M476.4,1013.4L205,580.3c-0.1-0.1,0-0.3,0.1-0.3L1130.5,0.2c0.1-0.1,0.3,0,0.3,0.1l271.4,433.1c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C476.6,1013.6,476.5,1013.5,476.4,1013.4z">
					<svg height="100%" width="100%" preserveAspectRatio="none" viewBox="0 0 800 500">
						<title>SVG Modal background</title>
						<path id="cd-changing-path-25" d="M-59.9,540.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L864.8-41c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L-59.5,540.6 C-59.6,540.7-59.8,540.7-59.9,540.5z"/>
						<path id="cd-changing-path-26" d="M-465.1,287.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L459.5-294c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3 l-925.4,579.8C-464.9,287.7-465,287.7-465.1,287.5z"/>
						<path id="cd-changing-path-27" d="M341.1,797.5l-0.9-1.4c-0.1-0.1,0-0.3,0.1-0.3L1265.8,216c0.1-0.1,0.3,0,0.3,0.1l0.9,1.4c0.1,0.1,0,0.3-0.1,0.3L341.5,797.6 C341.4,797.7,341.2,797.7,341.1,797.5z"/>
					</svg>
				</div>
				<a class="hide" data-group="modal-ClickAway" data-trg="ClickAway" data-type="cd-modal-trigger"></a>
				
				<a href="javascript:;" class="modal-close" id="ClickAwayClose" title="Close"></a>
				
				<div class="cd-modal-content">    
                    <h1><span id="warning"></span></h1>
                        <p><span id="focusOutLockedOutMessageText"></span></p>
                        <p><span id="focusOutLockedOutMessageTimer"></span></p>                    
                    <div>
	                    <a id="focusOutLockedOutButtonNo" href="javascript:;" class="cd-btn main-action"></a>
	                    <a id="focusOutLockedOutButtonYes" href="javascript:;" type="button" class="cd-btn"></a>
                    </div>
			    </div>
			 </div>   
			<!-- END CD MODAL -->							
						
			<div class="cd-cover-layer"></div>
			
		</div>		
		<!-- END CD MODALS -->
		
	    <!-- BEGIN TOP -->
		<div class="top-wrapper">
			<div class="wrapper-body">
				<a href="javascript:ui.nav.options();" class="cd-nav-trigger" title="Menu">
					<span class="cd-nav-icon"></span>
					<svg x="0px" y="0px" width="54px" height="54px" viewBox="0 0 54 54">
						<circle fill="transparent" stroke="#EAEAEA" stroke-width="1" cx="27" cy="27" r="25" stroke-dasharray="157 157" stroke-dashoffset="157"></circle>
					</svg>
				</a>
				<a href="javascript:;" id="modal-trigger-save" data-group="modal-dynamic" data-trg="save" data-type="cd-modal-trigger" class="btn" title="Save"><span class="glyphicon glyphicon-floppy-disk"></span></a>				
				<a href="javascript:;" style="ie-dummy: expression(this.hideFocus=true);" onclick="action();" id="toogle-flag" class="for-assessment btn hide" title="Click to flag this question for review"><span class="glyphicon glyphicon-flag"></span></a>
				<a href="javascript:;" id="cd-tour-trigger" class="btn" title="Tour Guide"><span class="glyphicon glyphicon-exclamation-sign"></span></a>
				<a href="javascript:;" id="modal-trigger-bookmark" class="btn" data-type="cd-modal-trigger" title="Bookmark This Page"><span class="glyphicon glyphicon-heart-empty"></span></a>				
				<div class="timer" id="odometerContainter">
				     <div>
				        <i class="glyphicon glyphicon-time"></i> 
				        <span id="odometerTxt"></span>
				        <span id="odometer">00:00</span>
				     </div>
				     <div id="assessmentTimer" class="for-assessment hide">
				        <i class="glyphicon glyphicon-hourglass"></i>
				        Time Left:<span id="aTimer">00:00</span>
				    </div>				    				   
				</div>				
			</div>
		</div>
		<!-- END TOP -->
		
        <!-- BEGIN LEFT MENU -->
        <div class="left-menu menu-wrapper">
			<div id="coursetitle" class="side-menu-brand"></div>
			<div class="side-menu-container">
				<div id="player-options" class="menu-body">
					<ul class="side-menu-nav items">									
						<li class="active" id="IcoTOC">
							<div id="TOC">
								<a href="javascript:ui.nav.outline();"><span id="HeadingTOC">Course Outline</span></a>
							</div>
						</li>
						<li id="IcoTOCDs" style="display: none;">
							<div>
								<a href="javascript:;"><span id="HeadingTOCDs">Course Outline</span></a>
							</div>
						</li>
						<li class="active" id="IcoGlossary">
							<div id="Glossary">
								<a href="javascript:ui.nav.glossary();"><span id="HeadingGlossary">Glossary</span></a>
							</div>
						</li>
						<li id="IcoGlossaryDs" style="display: none;">
							<div>
								<a href="javascript:;"><span id="HeadingGlossaryDs">Glossary</span></a>
							</div>
						</li>
						<li class="active" id="IcoCourseMaterial">
							<div id="Material">
								<a href="javascript:ui.nav.material();"><span id="HeadingCourseMaterial">Materials & Files</span></a>
							</div>
						</li>
						<li id="IcoCourseMaterialDs" style="display: none;">
							<div>
								<a href="javascript:;"><span id="HeadingCourseMaterialDs">Materials & Files</span></a>
							</div>
						</li>																							
						<li class="active" id="IcoRecommendationCoursePanel">
							<div>
								<a href="#rec" id="modal-trigger-rec" data-type="cd-modal-trigger"><span id="HeadingCourseRecommendation">Recommendations</span></a>
							</div>
						</li>
						<li id="IcoRecommendationCoursePanelDs" style="display: none;">
							<div>
								<a href="javascript:;"><span id="HeadingCourseRecommendationDs">Recommendations</span></a>
							</div>
						</li>						
                        <li class="active" id="IcoCourseCompletion">
							<div>								
								<a href="javascript:;" id="modal-trigger-report" data-group="modal-dynamic" data-trg="reports" data-type="cd-modal-trigger"><span id="HeadingCourseCompletionReport">Reports</span></a>
							</div>
						</li>
						<li id="IcoCourseCompletionDs" style="display: none;">
							<div>
								<a href="javascript:;" disabled="disabled"><span id="HeadingCourseCompletionReportDs">Reports</span></a>
							</div>
						</li>
                        <li class="active" id="IcoInstructorInformation">
							<div id="InstructorInformation">
								<a href="javascript:;" data-group="modal-dynamic" data-trg="about-author" data-type="cd-modal-trigger">About The Author</a>
							</div>
						</li>
						<li id="IcoInstructorInformationDs" style="display: none;">
							<div id="InstructorInformationDs">
								<a href="javascript:;">About The Author</a>
							</div>
						</li>													
						<li class="active" id="IcoHelp">
							<div>
								<a href="javascript:;" data-group="modal-dynamic" data-trg="support" data-type="cd-modal-trigger"><span id="HeadingSupport">Support</span></a>
							</div>
						</li>
						<li id="IcoHelpDs" style="display: none;">
							<div>
								<a href="javascript:;" disabled="disabled"><span id="HeadingSupportDs">Support</span></a>
							</div>
						</li>
					</ul>
				</div>
				<div id="course-material" class="menu-body">
					<ul class="side-menu-nav">
						<li class="active">
							<div class="secondary-menu-description"><span class="title" id="CourseMaterialFileHeadingText"></span><p><span id="CourseMaterialFileText"></span><a href="javascript:;" id="read-more-0" data-group="modal-dynamic" data-trg="read-more" data-type="cd-modal-trigger"><span id="readmoreText"></span></a></p> <a href="javascript:ui.nav.home();" class="btn menu-back-secondary"></a></div>
						</li>
					</ul>
					<ul class="side-menu-nav items">
						<!--
						<li class="active">
							<div>
								<a href="#movie" id="modal-trigger-movie" data-type="cd-modal-trigger"><i class="glyphicon glyphicon-film"></i>movie_name.mp4</a>
							</div>
						</li>
						<li class="active">
							<div>
								<a href="#image" id="modal-trigger-image" data-type="cd-modal-trigger"><i class="glyphicon glyphicon-picture"></i>image_name.jpg</a>
							</div>
						</li>
						<li class="active">
							<div>
								<a href="#doc" id="modal-trigger-doc" data-type="cd-modal-trigger"><i class="glyphicon glyphicon-file"></i>instructional_set.pdf</a>
							</div>
						</li>
						<li class="active">
							<div>
								<a href=""><i class="glyphicon glyphicon-hand-up"></i>External Resource Link</a>
							</div>
						</li>
						<li class="active">
							<div>
								<a href="#project" id="modal-trigger-project" data-type="cd-modal-trigger"><i class="glyphicon glyphicon-list-alt"></i>XCode_Project.zip</a>
							</div>
						</li>
						-->
					</ul>
				</div>
				<div id="course-glossary" class="menu-body">
					<ul class="side-menu-nav">
						<li class="active">
							<div class="secondary-menu-description"><span class="title" id="GlossaryTermsHeadingText"></span><p id="GlossaryTermsText"></p><a href="javascript:ui.nav.home();" class="btn menu-back-secondary"></a></div>
						</li>
					</ul>
					<ul class="side-menu-nav items">											
					</ul>
				</div>
				<div id="course-outline" class="menu-body">
					<ul class="side-menu-nav">
						<li class="active">
							<div class="secondary-menu-description"><span class="title" id="AboutCourseHeadingText"></span><p><span id="CourseDescriptionLeftNavigation"></span><a href="javascript:;" id="read-more-1" data-group="modal-dynamic" data-trg="course-read-more" data-type="cd-modal-trigger"><span id="coursereadMoreText"></span></a></p><a href="javascript:ui.nav.home();" class="btn menu-back-secondary"></a></div>
						</li>
					</ul>
					<ul class="side-menu-nav items">
					</ul>
				</div>
			</div>
			<div class="side-menu-social">
				<div class="social-label">Share Course</div>
				<ul>
					<li><a href="javascript:ui.social.fb.share(1);"" title="Share on Facebook" class="fb-symbol"></a></li>
					<li><a href="javascript:ui.social.in.share(1)" title="Share on LindeIn" class="ln-symbol"></a></li>
					<li><a href="#" title="Share on Twitter" class="hide tr-symbol"></a></li>
					<li><a href="#" title="Share on Google Plus" class="hide gp-symbol"></a></li>
					<li><a href="#" title="Share on 360Connect" class="hide ts-symbol"></a></li>
				</ul>
			</div>
			<div class="side-menu-logo">
				<img id="side-menu-inside-logo" src="" alt="360training"/>
				<div class="copyright">Copyright 2016. All Rights Reserved.</div>
			</div>
		</div>		
		<!-- END LEFT MENU -->
		
        <!-- BEGIN PAGE CONTENT -->
        <div id="page-content-wrapper">
			<div class="wrapper-body">
                <div class="MaskedDiv" id="masked_div" style="display:none;">
                    <div style="position: absolute; text-align: center;" id="divLoading">
                        <img src="" alt="loading.." id="LoadingImage">
                    </div>
                </div>	               
			    <div id="main_container">			        		    
                    <div id="content_container">
                        <div id="content">
                            <!-- Irfan Question Start .find('h3').html()eq(0). -->                            
                            <div id="quiz_container" style="display:none">
                                <div id="quizheader">
                                    <!-- head -->                                    
                                    <div id="questionfeedback">
                                        Question Feedback</div>                                   
                                   <div class="quizresult"><div id="divIncorrect"></div><div id="divCorrect"></div></div>
                                </div>
                                <div id="assessmentQuestionTemplate">
                                </div>
                            </div>
                            <!-- Irfan Question End -->
                            <!-- Student Authentication dialogue -->
                            <section id="studentAuthentication" class="scene-wrapper">
                                <div class="scene-body">                                    
                                     <div class="scene-title">  
                                        <div id="authenticationHeading">
                                            <!-- head -->
                                            <div id="authenticationIcon">
                                                &nbsp;
                                            </div>
                                                <h1>
                                                Validation Azhar Question</h1>
                                         </div>    
                                      </div>
                                     <div class="scene-content">  
                                        <p>
                                            In order to continue in this course you must validate your identity. Please answer
                                            the question below and click on the 'Validate My Identify' button to continue. If
                                            you have forgotten the answer to the question please click on the 'Forgot My Answers'
                                            button below.
                                        </p>
                                        <span id="validationNote">
                                            Your answer must exactly match the answer you entered in your profile.
                                        </span> 
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h3>What is the last four digits of your home telephone number?</h3>
                                             </div>
                                        </div>     
                                         <div class="row">
                                            <div class="col-sm-12"> 
                                                <div class="form-group form-group-md">
                                                    <label class="col-sm-1 control-label" for="authenticateText" id="SpanAnswer">Answer:</label>
                                                    <div class="col-sm-11">
                                                        <input class="form-control" type="text" id="authenticateText" placeholder="Answer here the above question">
                                                        <select id="authenticateSelect" class="form-control"></select>
                                                    </div>
                                                </div>
                                            </div>                                 
                                        </div>                                     
                                </div>
                            </section>
                            <!-- Student Authentication dialogue -->
                            <section id="QuestionRemediationContainer" class="scene-wrapper">
                                <div class="scene-body">
                                    <div id="QuestionRemediationContainerHeader">                                        
                                        <h1 id="QuestionRemediationFeedback" class="scene-title">&nbsp;</h1>
                                    </div>
                                    <div class="quizresultRemediation"><div id="divIncorrectRemediation"></div><div id="divCorrectRemediation"></div></div>
                                    <div id="questionRemediationTemplate">
                                    </div>
                                </div>
                            </section>
                            <!-- Irfan Assessment Result Start -->
                            <section id="assesmentcontainer" class="scene-wrapper visual-left">
                                <div class="scene-body">
                                    <div id="assessmentincomplete">
                                        <h1>
                                            Assessment Incomplete</h1>
                                    </div>
                                    <div id="assessmentcontent">
                                        <p>
                                        </p>
                                    </div>
                                    <div id="buttoncontainer">
										<div><button class="cd-btn button">Answer Remaining Questions</button></div>
										<div><button class="cd-btn main-action button">Continue Grading Without Answering</button></div>
                                    </div>
                                </div>
                            </section>
                            <!-- Irfan Assessment Result End -->
                            <!-- Irfan ShowProctorMessage Start -->
                            <section id="ShowProctorMessageContainer" class="scene-wrapper">
                                <div id="ShowProctorMessageHeading" class="scene-body">
                                    <h1 class="scene-title"></h1>                                    
                                    <div id="ShowProctorMessageContent" class="scene-content">
                                        <p>
                                        </p>
                                    </div>
                                    <div id="ShowProctorMessageButtons">
                                        <button class="cd-btn main-action button">Begin Assessment</button>
                                    </div>
                                </div>
                            </section>
                            <!-- Irfan ShowProctorMessage End -->
                            <section id="AnswerReviewContainer" class="scene-wrapper visual-left">
                                <div class="scene-body">
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
                                            <button class="cd-btn main-action button">
                                                Finish Grading My Assessment</button>
                                        </div>
                                    </div>
                                </div>
                            </section>
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
                            <section id="assessment_result_container" class="scene-wrapper visual-left">
                                <div class="scene-body">
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
                                            <button class="cd-btn main-action button">
                                                Show Answer</button>
                                            <!--</div>-->
                                        </div>
                                    </div>
                                </div>
                            </section>
                            
                            <!-- Irfan Start Score Summary -->
                            <!-- Irfan Start Score Summary -->
                            <!-- Irfan End Scorey -->
                            <!-- PROCTOR LOGIN SCREEN (START) -->
                            <section class="scene-wrapper" id="proctor_login_screen">
                                 <div class="scene-body">
                                    <h1 class="scene-title"><span id="proctorLoginIcon"></span> <span id="proctorLoginHeading"></span></h1>
                                    <div class="scene-cell">
                                        <div class="scene-content">
                                            <p id="proctorLoginScreenMessage"></p>
                                            <br>
                                            <div>
                                                <h1 id="ProctorLoginTableText" class="scene-title">Proctor Login</h1>
                                                <small id="attemptMessage" class="help-block"></small>
                                                    <div class="form-group">
                                                        <label id="ProctorLoginIDText" class="control-label">Proctor ID<span class="red-font">*</span></label>
                                                        <input type="text" class="form-control" id="proctorLogin" name="proctorLogin" placeholder="e.g. PROC-XXXXXXXX-CODE"/>
                                                        <small id="proctorLoginErrorMessage" class="help-block hide">The Proctor ID is a required field.</small>
                                                    </div>
                                                    <div class="form-group">
                                                        <label id="ProctorPasswordText" class="control-label">Password<span class="red-font">*</span></label>
                                                            <input type="password" class="form-control" id="proctorPassword" name="proctorPassword" placeholder="Enter Your Password"/>
                                                            <small id="proctorPasswordErrorMessage" class="help-block hide">The Password is a required field.</small>
                                                    </div>
                                                    <small id="proctorErrorLabel" class="help-block red-font"></small>
                                                    <button class="cd-btn main-action" id="btnProctorLoginSubmit">Login</button>
                                             </div>
                                        </div>
                                    </div>
                                </div>
                            </section>

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
                            <section id="swfContainer"  class="scene-wrapper">
                            <div class="scene-body">
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
        </noscript> -->         </div>
                            </section>
                        </div> 
                        <div>
                            <div id="loading" style="padding-left: 400px; padding-top: 200px;display:none;">
                                <p>
                                    <img src="images/loading.gif" />Please Wait</p>
                            </div>
                            <div id="CourseApproval">
                            </div>
                        </div>
                        <div id="AudioMobile" style="display: none; left: 68%; position: fixed; bottom: 60px;">
                        </div>
                    </div>
                    		    
			    </div>			
			</div>
			<div id="content-overlay"></div>
			<div id="overlay" style="display: none;"></div>
		</div>
        <!-- END PAGE CONTENT -->
		
		<!-- BEGIN BOTTOM -->
        <div id="assessmentControlPanel" style="display:none" class="bottom-wrapper for-assessment">
            <div class="wrapper-body">
		        <div class="t-row">  
		            <div class="t-col option-pre hide" id="BackQuestionButtonEn">
                        <a href="javascript:;" title="" class="btn ctrl"><i class="glyphicon glyphicon-triangle-left"></i><span id="BackQuestionButtonEnText">PREV</span></a>                            
                    </div>
                    <div class="t-col timeline">
						<span class="question-counter"></span>						
					</div>
                    <div class="t-col configures">
                        <div id="buttoncontainerAnswerReview" style="display: none;">
							<div id="buttoncontainerAnswerReviewPage" style="display: none;">
								<button class="btn ctrl button">Return to Answer Review Page</button>
							</div>
						</div>
						<div id="QuestionRemediationButtons">
						    <div style="float:left;margin-left:-105px;">
							    <button class="btn ctrl button" id="ShowContentRemediation">Show Contents</button>
							</div>
							<div>
							    <button class="btn ctrl button" id="ReturnToAssessmentResult">Return to Assessment Results</button>
							</div>
						</div>
						<div id="GradeAssessment">
							<button class="btn ctrl button">Grade Assessment</button>
						</div>			            
		            </div>					                   
		            <div class="t-col option-next">
			            <span id="NextQuestionButtonEn"><a href="javascript:;" title="" class="btn ctrl"><span id="NextQuestionButtonEnText">NEXT</span><i class="glyphicon glyphicon-triangle-right"></i></a></span>
						<span id="NextQuestionButtonDs"><a href="javascript:;" disabled="disabled"  title="" class="btn ctrl disabled"><span id="NextQuestionButtonDsText">NEXT</span><i class="glyphicon glyphicon-triangle-right"></i></a></span>
		            </div>
			    </div>
	    </div>                             
		</div> 	
		<!-- END BOTTOM -->
	
		<div id="ValidationControlBar" class="bottom-wrapper for-assessment">
			<div class="wrapper-body">
				<div id="ValidationControlPanel" class="t-row" style="float:right;">
					<div id="ValidationPlaybuttonEn" class="t-col option-next assessmentNext">
						<a  href="javascript:;" title="" class="btn ctrl"><span id="VNextQuestionButtonEnText">NEXT</span><i class="glyphicon glyphicon-triangle-right"></i></a>                            
					</div>
					<div id="ValidationTimer" class="timer-bar assessmentTimer" style="float:right;">
						<div class="progress"><div class="progress-bar" style="width:100%"></div></div>
						<label><span id="vTimer">00:00</span></label>
					</div>
				</div>
			 </div>
		</div>
                
                
    
    	
		<!-- BEGIN BOTTOM -->
		<div class="bottom-wrapper" id="controlPanel">
			<div class="wrapper-body">
				<div class="t-row" >
					<div class="t-col option-pre">
						<span id="BackbuttonEn"><a href="javascript:;" title="" class="btn ctrl"><i class="glyphicon glyphicon-triangle-left"></i><span id="BackbuttonEnText"></span></a></span>
						<span id="BackbuttonDs"><a href="javascript:;" title="" disabled="disabled" class="btn ctrl disabled"><i class="glyphicon glyphicon-triangle-left"></i><span id="BackbuttonDsText"></<span></a></span>
					</div>
					<div class="t-col timeline">
						<div class="progress" id="progressBarContainter">
						  <div id="progressbar" class="progress-bar"></div>
						  <span></span>
						</div>
						<div id="progressBarTxt"></div>
					</div>
					<div class="t-col configures">
						<div id="IcoConfigure" class="btn-group dropup">
						  <button id="btnPlayerConfiguration" type="button" title="Show Hidden Options" class="btn ctrl" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
							<span class="glyphicon glyphicon-cog"></span>
						  </button>
						  <ul class="dropdown-menu dropdown-menu-right">
							<li>
								<div class="title"><span id="volumeText"></span><span id="vol-label"></span></div>
								<div class="volume-ctrl">
									<div id="volume-slider"></div>
								</div>
							</li>
							<li>
								<div class="title"><span id="speakerText"></span></div>
								<div>
									<div id="speakerToggle" class="make-switch" data-text-label="<i class='glyphicon glyphicon-headphones'></i>" data-on-label="ON" data-off-label="OFF">
										<input type="checkbox" checked>
									</div>
								</div>
							</li>
							<li>
								<div class="title"><span id="captioningText"></span></div>
								<div>
									<div id="ccToggle" class="make-switch" data-text-label="<i class='glyphicon glyphicon-subtitles'></i>" data-on-label="SHOW" data-off-label="HIDE">
										<input type="checkbox" checked>
									</div>
								</div>
							</li>
						  </ul>
						</div>
						<div id="IcoConfigureDs" class="btn-group dropup" style="display:none">
							<button id="Button1" type="button" disabled="disabled" title="Show Hidden Options" class="btn ctrl" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
								<span class="glyphicon glyphicon-cog"></span>
							</button>					    
						</div>
					</div>
					<div class="t-col" id="ShowQuestionButton" style="display:none;">
						<button style="display: inline-block;padding:5px" class="btn ctrl button">Show Question</button>
					</div>
					<div class="t-col option-next">
						<span id="PlaybuttonEn"><a  href="javascript:;" title="" class="btn ctrl"><span id="PlaybuttonEnText"></span><i class="glyphicon glyphicon-triangle-right"></i></a></span>
						<span id="PlaybuttonDs"> 
							<div id="scene-duration" class="timer-bar">
								<div class="progress"><div class="progress-bar" style="width:100%"></div></div>
								<label><span id="timer">00:00</span></label>
							</div>					
							<a href="javascript:;" disabled="disabled"  title="" class="btn ctrl disabled"><span id="PlaybuttonDsText"></span><i class="glyphicon glyphicon-triangle-right"></i></a>
						</span>
					</div>       	
				</div>
			</div>
		</div>
		<!-- END BOTTOM -->
		
    </div>
    <!-- END WRAPPER -->
    
    <span id="AssessmentInProgress" style="height: 0px; width: 0px; display: none">
    </span>	
    <script language="javascript" type="text/javascript">
    
        var courseOutline = [];
        $(document).ready(function ()
		{
			completed();
			
			
			
			
            var labelTrg = $("#vol-label");
			$("#volume-slider").slider({
				min:0,
				max:100,
				value:100,
				tooltip:'hide',
				formater:function()
				{
					labelTrg.html($(this)[0].percentage[0] +"%");
				}	
			}).on('slideStop', function(ev){
			    setVolume($("#volume-slider").slider('getValue')[0].value);
            });
			
            $('#speakerToggle').on('switch-change', function(e, data) {
			
			    var $trg = $("#vol-label");
			    if(data.value)
			    {
				    $trg.parent().parent().show();				    
				    setVolume(parseInt($trg.text()));
			    }
			    else
			    {
				    $trg.parent().parent().hide();
				    setVolume(0);
			    }
				
			}).bootstrapSwitch('setSizeClass', 'switch-small');
			
			$('#ccToggle').on('switch-change', function(e, data) {
				//console.log(e +" - "+ data.value);
				var $trg = $(".sceneClosedCaptioningText");
				
				if(data.value)
				{
					$trg.show();
					
				}
				else
				{
					$trg.hide();
					
				}
				
			}).bootstrapSwitch('setSizeClass', 'switch-small');
		});
		
		function course_launch()
		{
		    $(".social-share").click(function(){
				ui.social.specificTitle = $(this).data("title");
				ui.svgModal.open($("<a data-group='modal-dynamic' data-trg='social-sharing'></a>"));
			});
			
			ui.init();			
			ui.loader("hide", function()
			{
				setTimeout(function()
				{
					$("#wrapper").addClass("toggled-top toggled-bottom toggled-left");
				},1000);
			},"launching");			
		}
		
		function course_exit()
		{
			//	this should be call when all information saved
			ui.loader("show", function()
			{
			    window.close();
				//window.open("CoursePlayerExit.aspx","_self");
			},"saving");
		}	
		
		function svg_modal_open($trg)
		{
			var attr = $trg.attr("data-group");
			var trg = $trg.attr("data-trg");
			var heading = $trg.attr("data-term");
			$(".cd-modals").removeClass("cd-modal-with-nav");
			var url = $trg.attr("data-source");
			switch(trg)
			{
				case 'save':
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					var bodyHtml;
					var isAssessmentStart=false;
					if ($('#assessmentControlPanel').is(':visible') && $('#AssessmentInProgress').is(':visible')) {
					    isAssessmentStart=true;
					}					
					if(isAssessmentStart)
					{
						bodyHtml =	'<h1>'+ CourseExitButton["Course"]["AssessmentLogOutHeadingText"] +'</h1>'+
										'<p>'+ logOutText +'</p>'+
										'<div>'+
											'<a href="javascript:course_exit();" type="button" class="cd-btn main-action">'+ CourseExitButton["Course"]["ExitCourseButton"] +'</a>'+
											'<a href="javascript:ui.svgModal.close(\'modal-dynamic\');" type="button" class="cd-btn">'+ CourseExitButton["Course"]["StayContinueButton"] +'</a>'+
										'</div>';
					}
					else
					{
						bodyHtml =	'<h1>'+ CourseExitButton["Course"]["LogOutHeadingText"] +'</h1>'+
									'<p>'+ CourseExitButton["Course"]["LogOutText"] +'</p>'+
									'<div>'+
										'<a href="javascript:course_exit();" type="button" class="cd-btn main-action">'+ CourseExitButton["Course"]["ExitCourseButton"] + '</a>'+
										'<a href="javascript:ui.svgModal.close(\'modal-dynamic\');" type="button" class="cd-btn">'+ CourseExitButton["Course"]["StayContinueButton"] +'</a>'+
									'</div>';
					}
					$thisModal.html(bodyHtml);
				break;
				case "glossary":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var id = $trg.attr("data-id");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
					glossaryClick(id,heading);
					resetCPIdleTimer();
				break;
				case "reports":
				    $(".cd-modals").addClass("cd-modal-with-nav");
				    var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
					reportClick();
					resetCPIdleTimer();
				break;
				case "image":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1><i class="glyphicon glyphicon-picture"></i> '+heading+'</h1>'+
											'<p></p>'+
											'<div class="image-asset">'+
												'<img class="img-responsive" src="'+url+'" alt="'+heading+'">'+
											'</div>'+
											'<div>'+
												'<a href="'+url+'" target="_blank" class="cd-btn main-action" download="image">Download File</a>'+
												'<a id="dropbox-saver-0" href="javascript:;" class="cd-btn dropbox-saver">Save To Dropbox</a>'+
											'</div>';
							
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							ui.dropBox('dropbox-saver-0',url,heading);
						//},100);
				break;
				case "pdf":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1><i class="glyphicon glyphicon-file"></i> '+heading+'</h1>'+
											'<p></p>'+
											'<div id="cd-modal-pdf">It appears you don\'t have Adobe Reader or PDF support in this web browser. Click below to download the PDF</div>'+
											'<div>'+
												'<a href="'+url+'" target="_blank" class="cd-btn main-action" download="pdf">Download File</a>'+
												'<a id="dropbox-saver-0" href="javascript:;" class="cd-btn dropbox-saver">Save To Dropbox</a>'+
											'</div>';
					
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							ui.dropBox('dropbox-saver-0',url,heading);
							ui.pdf(url);
						//},100);
				break;
				case "doc":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1><i class="glyphicon glyphicon-list"></i> '+heading+'</h1>'+
											'<p></p>'+
											'<div>'+
												'<a href="'+url+'" target="_blank" class="cd-btn main-action" download="doc">Download File</a>'+
												'<a id="dropbox-saver-0" href="javascript:;" class="cd-btn dropbox-saver">Save To Dropbox</a>'+
											'</div>';
							
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							ui.dropBox('dropbox-saver-0',url,heading);
						//},100);
				break;
				case "video":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1><i class="glyphicon glyphicon-list"></i> '+heading+'</h1>'+
											'<p></p>'+
											'<div class="jwmovie">'+
												'<div id="material-video-player"></div>'+
											'</div>'+
											'<div>'+
												'<a href="'+url+'" target="_blank" class="cd-btn main-action" download="video">Download File</a>'+
												'<a id="dropbox-saver-0" href="javascript:;" class="cd-btn dropbox-saver">Save To Dropbox</a>'+
											'</div>';
							
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							ui.dropBox('dropbox-saver-0',url,heading);
							ui.video.player(url,'material-video-player');
						//},100);
				break;
				case "read-more":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1>'+ coursematerial + '</h1>'+
											'<p>'+ coursematerialText +'</p>';							
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
						//},100);
				break;
				case "course-read-more":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1>'+ aboutcourse + '</h1>'+
											'<p>'+ CourseDescription +'</p>';							
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
						//},100);
				break;				
                case "about-author":
                    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
				    var authorImage;
				    if(InstructorContent["Information"]["Image"].toString().length <=0)
				    {
				        authorImage=InstructorContent["Information"]["DefaultImage"];
				    }
				    else
				    {
				        authorImage=InstructorContent["Information"]["Image"];
				    }				    
					var bodyHtml =	'<h1>'+ InstructorContent["Information"]["Heading"] + '</h1>'+
									'<p><img class="img-circle author-avatar" src="' + authorImage + '" />' +  InstructorContent["Information"]["Content"] + '</p>';
					$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);					
				break;
				case "support":
				    $(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
					var bodyHtml =	'<h1>'+ dynamicContent["Support"]["Heading"] +'</h1>'+
									'<p>'+ dynamicContent["Support"]["Content"] +'</p>'+
									'<div>'+
										'<a href="http://www.360training.com/support" target="_blank" class="cd-btn main-action">'+ dynamicContent["Support"]["ButtonText"] +'</a>'+										
										//'<a href="https://livechat.boldchat.com/aid/449369304135401025/bc.chat?resize=true&cwdid=704598144649519636&vn=&ve=&ln=&url=https%3A//10.0.100.179/ICP4/CoursePlayer.aspx%3FCOURSEID%3D95749%26VARIANT%3DEn-US%26BRANDCODE%3DDEFAULT%26PREVIEW%3Dtrue%26SESSION%3Dd3e2c154-ff7e-411b-9b40-7bc292238cd6%23" target="_blank" class="cd-btn main-action">'+ dynamicContent["Support"]["LiveChatButtonText"] +'</a>'+
									'</div>';
					$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);						
				break;
                case "assessment-expired":
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
					$thisModal.addClass('pre-loader please-wait');
						//setTimeout(function(){
							var bodyHtml =	'<h1>Assessment Timer Has Expired</h1>'+
											'<p>Your assessment has been graded. All unanswered questions were counted incorrect.'+
											'<br>Click "Stay & Continue" to view the results of your assessment.</p>'+
											'<div>'+
												'<a href="javascript:;" class="cd-btn main-action">Stay & Continue</a>'+
											'</div>';
							$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
						//},100);
				break;
				case "assessment-click-away":
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
						$thisModal.addClass('pre-loader please-wait');
							//setTimeout(function(){
								var bodyHtml =	'<h1>Warning</h1>'+
												'<p>If you leave the examination window for any reason during the exam, the course will lock and you will fail this exam attempt.'+
												' You have clicked in a way that will cause you to leave the examination window.'+
												' Do you want to continue? Click "Stay & Continue" to stay in the exam.</p>'+
												'<p>Course automatically locks in <strong class="primary-font">22 seconds.</strong></p>'+
												'<div>'+
													'<a href="javascript:ui.svgModal.close(\'modal-dynamic\');" type="button" class="cd-btn main-action">Stay & Continue</a>'+
													'<a href="javascript:course_exit();" type="button" class="cd-btn">Exit Course</a>'+
												'</div>';
								$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							//},100);
				break;
				case "idle":
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
						$thisModal.addClass('pre-loader please-wait');
							//setTimeout(function(){
								var bodyHtml =	'<h1>Your session is about to expire.</h1>'+
												'<p>If you are still working in your course simply click the "Stay & Continue" button.'+
												'<br>Your session will expire in <strong class="primary-font">4:56 minutes.</strong></p>'+
												'<div>'+
													'<a href="javascript:ui.svgModal.close(\'modal-dynamic\');" type="button" class="cd-btn main-action">Stay & Continue</a>'+
												'</div>';
								$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							//},100);
				break;
				case "social-sharing":
					$(".cd-modals").addClass("cd-modal-with-nav");
					var $thisModal = $('.cd-modal[data-modal="'+ attr +'"] > .cd-modal-content');
						$thisModal.addClass('pre-loader please-wait');
							//setTimeout(function(){
								var bodyHtml =	'<h1>Social Sharing</h1>'+
												'<h2>What\'s on your mind?</h2>'+
												'<div class="blockquote-list interactive">'+
													'<blockquote class="hide">'+
														'<a href="javascript:;" class="three-sixty-social">Share on 360Connect</a>'+
													'</blockquote>'+
													'<blockquote>'+
														'<a href="javascript:ui.social.fb.share(2);" class="fb-social">Share on Facebook</a>'+
													'</blockquote>'+
													'<blockquote>'+
														'<a href="javascript:ui.social.in.share(2);" class="ln-social">Share on Linkedin</a>'+
													'</blockquote>'+
													'<blockquote class="hide">'+
														'<a href="javascript:;" class="tr-social">Share on Twitter</a>'+
													'</blockquote>'+
													'<blockquote class="hide">'+
														'<a href="javascript:;" class="gp-social">Share on Google Plus</a>'+
													'</blockquote>'+
												'</div>';
								$thisModal.removeClass('pre-loader please-wait').html(bodyHtml);
							//},100);
				break;
			}
		}
		
		function svg_modal_close($trg)
		{
			var $trg = $("#"+$trg);
			var attr = $trg.parent().attr("data-modal");
			switch(attr)
			{
				case "modal-dynamic":
					$trg.parent().find(".cd-modal-content").removeClass('pre-loader please-wait').html(" ");
				break;
			}
		}
	</script>
	<% string gb = "course player testing"; %>
	<script type="text/javascript" src='<%="assets/scripts/cpbottom.js?g=" + gb %>'></script>
</form>	
</body>
</html>
