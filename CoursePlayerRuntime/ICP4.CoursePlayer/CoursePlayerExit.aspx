<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoursePlayerExit.aspx.cs" Inherits="ICP4.CoursePlayer.CoursePlayerExit" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    
    <title>360training | Course Exit</title>

	<!-- BEGIN CSS -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/bootstrap-3.3.5-dist/css/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="assets/css/style.css" />
    <link rel="shortcut icon" href="favicon.ico" />
    <!-- END CSS -->
	
    <!-- BEGIN SCRIPTS -->
    <script type="text/javascript" src="assets/plugins/jquery.min.js"></script>  
    <script type="text/javascript" src="assets/plugins/html5shiv.js"></script>
    <script type="text/javascript" src="assets/plugins/respond.min.js"></script>
	<script type="text/javascript" src="assets/plugins/modernizr.js"></script>
	<script type="text/javascript" src="assets/plugins/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>
	
	<script type='text/javascript' src="assets/scripts/ui.js"></script>
	<!-- END SCRIPTS -->

</head>

<body class="pre-loader saving">
	
	<!-- BEGIN WRAPPER -->
    <div id="wrapper">
		
		<div class="cd-modal modal-is-visible">
			<div class="cd-modal-content">
				<h1>Course Exit</h1>
				<p style="color:#777">Your progress has been saved.</p>
				<div>
					<a href="#" class="hide">Retake Course</a>
				</div>
			</div>
		</div>
		
		<div class="cd-cover-layer modal-is-visible"></div>
		
    </div>
    <!-- END WRAPPER -->
	
	<script>
		$(document).ready(function ()
		{
			ui.loader("hide", function()
			{
				
			},"saving");
		});
		
		function retakeCourse()
		{
			ui.loader("show", function()
			{
				window.open("index.html","_self");
			},"launching");
		}
	</script>
</body>
</html>
