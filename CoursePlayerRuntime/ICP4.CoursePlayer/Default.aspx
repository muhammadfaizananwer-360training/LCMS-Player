<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ICP4.CoursePlayer._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>External Interface example @ deconcept</title>

<!-- FlashObject embed by Geoff Stearns geoff@deconcept.com http://blog.deconcept.com/flashobject/ -->
<script type="text/javascript" src="Scripts/swfobject.js"></script>
<script type="text/javascript">
   // <![CDATA[
   
      // get our flash movie object
      var flashMovie;
      function init() {
         if (document.getElementById) {
            flashMovie = document.getElementById("ei_test");
            flashMovie2 = document.getElementById("player");
         }
		 clickHand('lesson_2.swf');
      }
      
      // wait for the page to fully load before initializing
      window.onload = init;      
      function clickHand(url) {
		 if (flashMovie) {
		    flashMovie.callLoadSWF(url);
		    flashMovie2.callLoadSWF(url);
		    }
		}
      
   // ]]>
</script>
</head>
<body>

	<div id="flashcontent">
		
	</div>

    <div id="playercontent">
		
	</div>
	<script type="text/javascript">
		// <![CDATA[

      var fo = new SWFObject("test.swf", "ei_test", "300", "350", "8.0.15", "#ffffff", true);
      // need this next line for local testing, it's optional if your swf is on the same domain as your html page
      fo.addParam("allowScriptAccess", "always");
      fo.write("flashcontent");
      
      var fo = new SWFObject("test.swf", "player", "300", "350", "8.0.15", "#ffffff", true);
      // need this next line for local testing, it's optional if your swf is on the same domain as your html page
      fo.addParam("allowScriptAccess", "always");
      fo.write("playercontent");
	  
	 
		// ]]>
	</script>

	<input type="button" value="Send Data" />
</body>
</html>


