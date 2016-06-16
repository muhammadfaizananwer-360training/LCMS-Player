/* 
Communication Engine updated October 06, 2008;
*/
function CommunicationEngine() {
	
	//this.InitializeCoursePlayer = function(learnerSessionID,courseID,demo,isPreview)
	//this.InitializeCoursePlayer = function(learnerSessionID,brandCode,variant,courseID,demo,isPreview,stateVertical,sceneID,assetID)  
	this.InitializeCoursePlayer = function(learnerSessionID,brandCode,variant,courseID,demo,isRedirect,isPreview,stateVertical,sceneID,assetID,courseGUID) 
	
	{
	
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/InitializeCoursePlayer",
					  //data: "{'learnerSessionID':'"+learnerSessionID+"','brandCode':'"+brandCode+"','variant':'"+variant+"','courseID':'"+courseID+"','isdemo':'"+demo+"','isPreview':'"+isPreview+"','svId':'"+stateVertical+"','sceneID':'"+sceneID+"','assetID':'"+assetID+"'}",
					  data: "{'learnerSessionID':'"+learnerSessionID+"','brandCode':'"+brandCode+"','variant':'"+variant+"','courseID':'"+courseID+"','isdemo':'"+demo+"','isRedirect':'"+isRedirect+"','isPreview':'"+isPreview+"','svId':'"+stateVertical+"','sceneID':'"+sceneID+"','assetID':'"+assetID+"','courseGUID':'"+courseGUID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var resourceInfo = eval('(' + msg.d + ')'); 
						returnPacket = resourceInfo;
						//cp.CommandHelper(resourceInfo);
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
		return returnPacket;
		
	}


	this.GetResourceValues = function() 
	{
		jQuery().ready(function(){
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetResourceValues",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var myObject = eval('(' + msg.d + ')');
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		});
			
	}



    this.AuthenticateProctor = function(proctorLogin, proctorPassword,onComplete,OnBeforeSend)
    {
    
        var returnPacket = null;
        $.ajax({
        
             type: "POST",
			 async:true, cache:false,
			 url: "CoursePlayer.aspx/AuthenticateProctor",
			 data: "{'proctorLogin':'"+proctorLogin+"','proctorPassword':'" + proctorPassword + "'}",
			 contentType: "application/json; charset=utf-8",
			 dataType: "json",
			 success: function(msg) {
			 //var courseInfo = eval('(' + msg.d + ')'); 
			  returnPacket = eval('(' + msg.d + ')');    
			  //cp.CommandHelper(courseInfo); 					  
			 },
			 error: function(msg) {
			// Replace t
			},
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
       });
              
       return returnPacket ;
    
    }

	this.LoadCourse = function() 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/LoadCourse",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var courseInfo = eval('(' + msg.d + ')'); 
						  returnPacket = courseInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket ;
	}

	this.StartAssessment = function(onComplete,OnBeforeSend) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/StartAssessment",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var commandInfo = eval('(' + msg.d + ')'); 
						  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
			return returnPacket ;
	}
	
	
	this.ResumeAssessment = function(onComplete,OnBeforeSend) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ResumeAssessment",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var commandInfo = eval('(' + msg.d + ')'); 
						  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
			return returnPacket ;
	}
	
	this.SkipPracticeExam = function(onComplete,OnBeforeSend) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/SkipPracticeExam",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var commandInfo = eval('(' + msg.d + ')'); 
						  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
			return returnPacket ;
	}	


	this.GetStartingItem = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetStartingItem",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}

	this.GetTOC = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/GetTOC",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {
	            var tocObject = eval('(' + msg.d + ')');
	            returnPacket = tocObject;
	            //cp.CommandHelper(tocObject); 
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}

	this.GetGlossary = function(sceneID) 
	{
	var returnPacket = null;
	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetGlossary",
					  data: "{'sceneID':'"+sceneID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var glossaryObject = eval('('+msg.d+')');
						  returnPacket = glossaryObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
	
		return returnPacket;	
	}
	
	this.LoadGlossaryItem = function(glossaryID) 
	{
	var returnPacket = null;
	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/LoadGlossaryItem",
					  data: "{'glossaryID':'"+glossaryID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var glossaryItemObject = eval('('+msg.d+')');
						  returnPacket = glossaryItemObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
	
		return returnPacket;	
	}	
	

	this.GetCourseMaterial = function() 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetCourseMaterial",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var courseMaterialPacket = eval('(' + msg.d + ')'); 
					  	returnPacket =   courseMaterialPacket;
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
					return returnPacket;
		
			
	}

	this.GetBookMark = function() 
	{
			var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetBookMark",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var bookmarkPacket = eval('(' + msg.d + ')'); 
					  	returnPacket =   bookmarkPacket;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
	
			return returnPacket;
	}

	this.LoadBookmark = function(bookMarkID,onComplete,OnBeforeSend) 
	{
			var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/LoadBookMark",
					  data: "{'bookMarkID':'"+bookMarkID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var bookmarkObject = eval('(' + msg.d + ')'); 
					  	returnPacket =   bookmarkObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
	
			return returnPacket;
	}

	this.SaveBookMark = function(title, sID,sGUID, frameNum, scence, movieEnd, nextBstate,firstSceneName) 
	{
			var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/SaveBookMark",
					  data: "{'bookMarkTitle':'"+title+"','itemGUID':'" + sID + "','sceneGUID':'"+sGUID+"','flashSceneNo':'"+frameNum+"','lastScene':'"+scence+"','IsMovieEnded':'"+movieEnd+"','nextButtonState':'"+nextBstate+"','firstSceneName':'"+firstSceneName+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
			
					  var bookmarkObject = eval('(' + msg.d + ')'); 
					  	returnPacket =   bookmarkObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
			return returnPacket;
    }
    
	this.DeleteBookMark = function(bookmarkID) 
	{
			var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/DeleteBookMark",
					  data: "{'bookmarkID':'"+bookmarkID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
			
					  var bookmarkObject = eval('(' + msg.d + ')'); 
					  	returnPacket =   bookmarkObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
			return returnPacket;
    }    

    this.SaveCourseRatingNPS = function(NPS_RATING,USER_REVIEW_TEXT,
    RATING_SHOPPINGEXP,RATING_COURSE,RATING_LEARNINGTECH,RATING_CS,RATING_SHOPPINGEXP_SECONDARY,RATING_COURSE_SECONDARY,
    RATING_LEARNINGTECH_SECONDARY,RATING_CS_SECONDARY,   RATING_SHOPPINGEXP_SECONDARY_Q, RATING_COURSE_SECONDARY_Q,RATING_LEARNINGTECH_SECONDARY_Q,RATING_CS_SECONDARY_Q) {
       
        var isSuccess = true;
        var dataString = JSON.stringify({
            NPS_RATING: NPS_RATING,
            USER_REVIEW_TEXT: USER_REVIEW_TEXT,
            RATING_COURSE: RATING_COURSE,
            RATING_SHOPPINGEXP: RATING_SHOPPINGEXP,
            RATING_LEARNINGTECH: RATING_LEARNINGTECH,
            RATING_CS: RATING_CS,
            RATING_SHOPPINGEXP_SECONDARY: RATING_SHOPPINGEXP_SECONDARY,
            RATING_COURSE_SECONDARY: RATING_COURSE_SECONDARY,
            RATING_LEARNINGTECH_SECONDARY: RATING_LEARNINGTECH_SECONDARY,
            RATING_CS_SECONDARY: RATING_CS_SECONDARY,

            RATING_SHOPPINGEXP_SECONDARY_Q: RATING_SHOPPINGEXP_SECONDARY_Q,
            RATING_COURSE_SECONDARY_Q: RATING_COURSE_SECONDARY_Q,
            RATING_LEARNINGTECH_SECONDARY_Q: RATING_LEARNINGTECH_SECONDARY_Q,
            RATING_CS_SECONDARY_Q: RATING_CS_SECONDARY_Q
            
            
        });
        $.ajax({
            async: true,
            url: "CoursePlayer.aspx/SaveCourseRatingNPS",
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            data: dataString,
            success: function(data) {
                //$("#ratingresult").show();
                //$("#CourseLevelRatingButton").find("a").unbind('click.namespace');
                //$("#CourseLevelRatingButton").fadeTo('slow', .4);
                //$("#SubmitRating").find("a").unbind('click.namespace');
                //$("#SubmitRatingBtn").attr("disabled", "disabled");
                //$("#btnstart").attr("disabled", "disabled");
                //$("#btnend").attr("disabled", "disabled");
                //$jq('input[type=radio]', document).rating('readOnly');
                if (data.hasOwnProperty("d")) {

                }
                isSuccess = true;
            },
            error: function(xhr, textStatus, errorThrown) {
                isSuccess = false;
            }
        });
        
        return isSuccess;
    }

    this.SaveCourseRating = function(courseRating) {
       
        var dataString = JSON.stringify({
            Rating: courseRating
        });
        $.ajax({
            async: true,
            url: "CoursePlayer.aspx/SaveCourseRating",
            type: "POST",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            data: dataString,
            success: function(data) {
                $("#ratingresult").show();
                $("#CourseLevelRatingButton").find("a").unbind('click.namespace');
                $("#CourseLevelRatingButton").fadeTo('slow', .4);
                $("#SubmitRating").find("a").unbind('click.namespace');
                $("#SubmitRating").attr("disabled", "disabled");
                $("#btnstart").attr("disabled", "disabled");
                $("#btnend").attr("disabled", "disabled");
                $jq('input[type=radio]', document).rating('readOnly');
                if (data.hasOwnProperty("d")) {

                }
            },
            error: function(xhr, textStatus, errorThrown) {

            }
        });
    }


    //LCMS-12532 Yasin
    this.SaveValidationIdentityQuestion = function(QS1, txtAnswerSet1, QS2, txtAnswerSet2, QS3, txtAnswerSet3, QS4, txtAnswerSet4, QS5, txtAnswerSet5,onComplete,OnBeforeSend) {

    var returnPacket = null;

        var dataString = JSON.stringify({           
           QS1: QS1,
           Answer1:txtAnswerSet1,
           QS2:QS2,
           Answer2:txtAnswerSet2,
           QS3:QS3,
           Answer3:txtAnswerSet3,
           QS4:QS4,
           Answer4:txtAnswerSet4,
           QS5:QS5,
           Answer5:txtAnswerSet5
        });        

        $.ajax({
            type: "POST",
            async: false, cache: false,
            
            url: "CoursePlayer.aspx/SaveValidationIdendityQuestions",
            data: dataString,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) { 
                        var nextSlide = eval('(' + msg.d + ')'); 
					  	returnPacket =   nextSlide;
					    //alert(returnPacket);
            },
            error: function(msg) {
                // Replace the div's content with the page method's return.
            },
             beforeSend:function(){
               OnBeforeSend();
			},
					  complete: function() {
						onComplete(returnPacket);
					  }
        });
        return returnPacket;


    }
 
            /*
            * Code Review : Now Course Intro command will be sent or other command will be sent here.
            */            
//             $(PlaybuttonEn).show();
//             $(PlaybuttonDs).hide();
//             $("#btnSaveValidityIdentityQuestions").find("a").unbind('click.namespace');
//             $("#btnSaveValidityIdentityQuestions").attr("disabled", "disabled");
	
	
	this.GetGlossaryItem = function(glossaryID) 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetGlossaryItem",
					  data: "{'glossaryID':'"+glossaryID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}

	this.Next = function(onComplete,OnBeforeSend) 
	{
	
		 var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/Next",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var nextSlide = eval('(' + msg.d + ')'); 
					  	returnPacket =   nextSlide;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});

			return  returnPacket;
	}


	this.Back = function(onComplete,OnBeforeSend) 
	{
		 var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/Back",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					
					 var backSlide = eval('(' + msg.d + ')'); 
						returnPacket =   backSlide;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});

			return  returnPacket;
			
	}

	this.Goto = function(ID, Type,onComplete,OnBeforeSend) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/Goto",
					  data: "{'id':'" + ID + "', 'type':'" + Type + "'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var showSlideObject = eval('(' + msg.d + ')'); 
					  	  returnPacket =   showSlideObject;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
		return returnPacket;	
	}

	this.CourseIdleTimeOut = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/CourseIdleTimeOut",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}

	this.CourseExpired = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/CourseExpired",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}

	this.UnlockCourse = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/UnlockCourse",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}
	
	
	this.BeginCourse = function() 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/BeginCourse",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					    var someCommand = eval('(' + msg.d + ')'); 
						returnPacket = someCommand;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket;
	}
	
	this.logoutCoursePlayer = function(timer) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/LogoutCoursePlayer",
					  data: "{'timer':'"+timer+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					    
						var endSessionCommand = eval('(' + msg.d + ')'); 
						returnPacket = endSessionCommand;
						
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket;
	}
	
	this.logoutCoursePlayerIntegeration = function(timer) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/logoutCoursePlayerIntegeration",
					  data: "{'timer':'"+timer+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					    window.open("CoursePlayerExit.aspx","_self");
						//var endSessionCommand = eval('(' + msg.d + ')'); 
						//returnPacket = endSessionCommand;
						
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket;
	}

/* Irfan getQuestion Start*/
this.GetQuestion = function(onComplete,OnBeforeSend)
	{	    
	    var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/GetQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
	}
/* Irfan getQuestion Ended*/

/* Irfan Submit Assessment Result Start */
	this.SubmitAssessmentResult = function(assessmentId, answerIds, answerStrings, isskipping, toogleFlag,onComplete, OnBeforeSend)
    {         
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/SubmitQuestionResult",
					  //data: "{'learnerSessionID':'"+learnerSessionID+"','courseID':'"+courseID+"','courseApprovalID':'"+courseApprovalID+"'}",
					  //data: "{'assessmentItemID':'" + assessmentId + "','assessmentAnswerIDs':'" + answerIds + "',assessmentAnswerStrings':'" + answerStrings + "','isSkipping':'" + isSkipping + "'}",
					  data: "{assessmentItemID:" + assessmentId + ",assessmentAnswerIDs:" + answerIds + ",assessmentAnswerStrings:" + answerStrings + ",isSkiping:" + isSkipping + ",toogleFlag:" + toogleFlag + "}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  //var resourceInfo = String(msg.d); 
					  var resourceInfo = eval('(' + msg.d + ')'); 
					  returnPacket = resourceInfo;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
						//alert(msg);
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
		 return returnPacket;
    }
    
    this.GetNextQuestionAfterFeedback = function(onComplete,OnBeforeSend)
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/GetNextQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    this.AnswerRemainingQuestion = function(onComplete,OnBeforeSend)
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/AnswerRemainingQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
// Irfan Submit Assessment Result End
    
    this.FinishGradingAssessment = function(onComplete,OnBeforeSend)
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/FinishGradingAssessment",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    this.ContinueAfterAssessmentScore = function(onComplete,OnBeforeSend)
    {
        //
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterAssessmentScore",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }

	this.AskValidationQuestion = function()
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/AskValidationQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }

    this.SubmitValidationQuestionResult = function(questionID, questionText) {

        //alert(questionText);
        var dataString = JSON.stringify({
            validationQuestionId:questionID,
            validationQuestionAnswer:questionText
        });

        var returnPacket = null;
        $.ajax({
            type: "POST",
            async: false, cache: false,
            url: "CoursePlayer.aspx/SubmitValidationQuestionResult",

            data:dataString,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var question = eval('(' + msg.d + ')');

                returnPacket = question;
            },
            error: function(msg) {
                // Replace the div's content with the page method's return.
            }
        });

        return returnPacket;
    }

    
    this.ContinueAfterAssessment = function()
    {
        
		 var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterAssessment",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 	
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }
    
    this.AskSpecifiedQuestion = function(assessmentItemId,onComplete,OnBeforeSend)
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/AskSpecifiedQuestion",
					  data: "{'assessmentItemID':'" + assessmentItemId + "'}",
					  //data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    
    this.ShowAnswers = function(onComplete,OnBeforeSend)
    {
		 var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ShowAnswers",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 	
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    
    this.GetNextRemidiationQuestion = function(onComplete,OnBeforeSend)
    {
            var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetNextRemidiationQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 	
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    

    this.GetPreviousRemidiationQuestion = function(onComplete,OnBeforeSend)
    {
            var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/GetPreviousRemidiationQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    this.ShowSpecifiedQuestionScore = function(assessmentItemId,onComplete,OnBeforeSend)
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ShowSpecifiedRemidationQuestion",
					  data: "{'assessmentItemID':'" + assessmentItemId + "'}",
					  //data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }

    this.ShowContent = function(AssessmentItemID,onComplete,OnBeforeSend)
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ShowContent",
					  data: "{'assessmentItemID':'"+AssessmentItemID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 	
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }



    this.ReturnToAssessmentResults = function(onComplete,OnBeforeSend)
    {

        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ReturnToAssessmentResults",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }


    this.ContinueGradingWithoutAnswering = function(onComplete,OnBeforeSend)
    {

        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ContinueGradingWithoutAnswering",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    this.AssessmentTimerExpired = function()
    {

        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/AssessmentTimerExpired",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var timerExpiredMessgae = eval('(' + msg.d + ')'); 
					  returnPacket = timerExpiredMessgae;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }
    this.GoContentTOQuestion = function(onComplete,OnBeforeSend)
    {

        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/GoContentTOQuestion",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }
    
    this.GradeAssessment = function(assesmentId, answerIds,answerStrings)
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GradeAssessment",
					  //data: "{}",
					  data: "{'assessmentItemID':'" + assesmentId + "',assessmentAnswerIDs:" + answerIds + ",assessmentAnswerStrings:" + answerStrings + "}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }
    
    
    
    this.ValidationTimerExpired = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/ValidationTimerExpired",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}
	
	
	this.GetValidationOrientationScene = function()
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetValidationOrientationScene",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }
	

this.ResumeCourseAfterValidation = function()
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/ResumeCourseAfterValidation",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }
	
this.ContinueAfterCourseEnd=function()	
 {
        
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterEndOfCourse",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					 
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		
    }
    
    
    this.UnlockCourse = function() 
	{
		
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/UnlockCourse",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			
	}

    this.ShowCourseEvaluation = function()
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/ShowCourseEvaluation",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var question = eval('(' + msg.d + ')'); 
					  returnPacket = question;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }


    this.DownloadCertificate = function() 
	{	
		window.open("ShowCourseCertificate.aspx");		
			
	}
	
    this.DownloadCourseApprovalCertificate = function(certificateURL) 
	{	
		window.open(certificateURL);		
			
	}	
	
	this.BeginCourseEvaluation=function(onComplete,OnBeforeSend)	
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/BeginCourseEvaluation",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					    var packet = eval('(' + msg.d + ')'); 
					    returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         return returnPacket;
		
    }
    
    this.SkipCourseEvaluation=function(onComplete,OnBeforeSend)	
    {
         var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/SkipCourseEvaluation",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					    var packet = eval('(' + msg.d + ')'); 
					    returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         return returnPacket;		
		
    }
    
    this.GetAndSubmitCourseEvaluation = function(assesmentId, answerIds, questionTypes,onComplete,OnBeforeSend)
    {   
      // answerIds = answerIds.replace(/'/g,"");
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/GetAndSubmitCourseEvaluation",
					  data: "{questiongIds:" + assesmentId + ",answerIds:" + answerIds + ",questionTypes:" + questionTypes + "}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
         		
		 return returnPacket;
    }





 this.GetIdleTimeVariables = function()
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetIdleTimeVariables",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = msg.d ; //eval('(' + msg + ')'); 
					  returnPacket = packet;
					
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }



this.GetCommandDueToIdleUserTimeout = function()
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetCommandDueToIdleUserTimeout",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
					  returnPacket = packet;
				
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }


 this.CheckThingsBeforeCallingIdleTimeWarningPopup = function()
    {
        var returnPacket = null;	
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/CheckThingsBeforeCallingIdleTimeWarningPopup",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = msg.d ; //eval('(' + msg + ')'); 
					  returnPacket = packet;
					
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
         		
		 return returnPacket;
    }


	this.SynchToExternalSystem = function(mileStone) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache: false,
					  url: "CoursePlayer.aspx/SynchToExternalSystem",					  
					  data: "{'mileStone':'" + mileStone + "'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					        
					  //var commandInfo = eval('(' + msg.d + ')'); 
						//  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket ;
	}


	this.GetCourseCompletionReport = function() 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false, 
					  url: "CoursePlayer.aspx/GetCourseCompletionReport",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					        
					    var packet = eval('(' + msg.d + ')'); 
					    returnPacket = packet;					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket ;
	}
	
	
	
	
	this.NoteTime = function() 
	{
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/NoteTime",
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
		
					  },
					   error:function (xhr, textStatus, q){
//                         alert(xhr.status);
//                         alert(xhr.responseText);
//                         alert(xhr.statusText);
//                         alert(textStatus);           
                        }   
					});
		

    }
    
	this.CourseApprovalCheck = function(learnerSessionID,brandCode,variant,courseID,demo,isRedirect,isPreview,courseGUID) 
	
	{
	
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/CourseApprovalCheck",					  
					  data: "{'learnerSessionID':'"+learnerSessionID+"','brandCode':'"+brandCode+"','variant':'"+variant+"','courseID':'"+courseID+"','isdemo':'"+demo+"','isRedirect':'"+isRedirect+"','isPreview':'"+isPreview+"','courseGUID':'"+courseGUID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
		return returnPacket;
		
	}  
	
	this.SaveLearnerCourseApproval = function(learnerSessionID,courseID, courseApprovalID) 	
	{
		var returnPacket = -1;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/SaveLearnerCourseApproval",					  
					  data: "{'learnerSessionID':'"+learnerSessionID+"','courseID':'"+courseID+"','courseApprovalID':'"+courseApprovalID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
		return returnPacket;
		
	}
	
	this.SaveLearnerCourseMessage = function(learnerSessionID,courseID,onComplete,OnBeforeSend) 	
	{

		var returnPacket = -1;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/SaveLearnerCourseMessage",
					  data: "{'learnerSessionID':'"+learnerSessionID+"','courseID':'"+courseID+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }, 
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
						}
					});
		
		return returnPacket;
	}	
	
	this.ContinueAfterAffidavit = function(onComplete,OnBeforeSend) 	
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterAffidavit",					  
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
		return returnPacket;
		
	}	 	
	
	//LCMS-11281
	this.ContinueAfterDocuSignRequirementAffidavit = function(onComplete,OnBeforeSend) 	
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterDocuSignRequirementAffidavit",					  
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
		return returnPacket;
		
	}
	//End
	
	
	//LCMS-11283
	this.ContinueAfterDocuSignProcess = function(onComplete,OnBeforeSend) 	
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/ContinueAfterDocuSignProcess",					  
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
					  }
					});
		
		return returnPacket;
		
	}
	//End

	//Proctor Lock Screen
	//Added by Abdus Samad
	//Start
	this.IsProctorLockedCourse = function() {
	   
	    var returnPacket = null;

	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/IsProctorLockedCourse",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {

	            var glossaryObject = eval('(' + msg.d + ')');
	            //alert(glossaryObject);
	            returnPacket = glossaryObject;

	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}
	
	this.AuthenticateSpecialPostAssessmentValidation = function(learnerSessionID, courseID, DRELicenseNumber, DriverLicenseNumber,onComplete,OnBeforeSend) 	
	{
		var returnPacket = -1;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/AuthenticateSpecialPostAssessmentValidation",					  
					  data: "{'learnerSessionID':'"+learnerSessionID+"','courseID':'"+courseID+"','DRELicenseNumber':'"+DRELicenseNumber+"','DriverLicenseNumber':'"+DriverLicenseNumber+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }, 
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
						}
					});
		
		return returnPacket;
		
	}
	
	this.CancelSpecialPostAssessmentValidation = function(onComplete,OnBeforeSend) 	
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/CancelSpecialPostAssessmentValidation",					  
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
						}
					});
		
		return returnPacket;
		
	}
	
	this.AuthenticateNYInsuranceValidation = function(learnerSessionID, courseID, monitorNumber,onComplete,OnBeforeSend) 	
	{	    
		var returnPacket = -1;
				$.ajax({
					  type: "POST",
					  async:true, cache:false,
					  url: "CoursePlayer.aspx/AuthenticateNYInsuranceValidation",					  
					  data: "{'learnerSessionID':'"+learnerSessionID+"','courseID':'"+courseID+"','MonitorNumber':'"+monitorNumber+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  },
					  beforeSend:function(){
					    OnBeforeSend();
					  },
					  complete: function() {
						onComplete(returnPacket);
						}
					});
		
		return returnPacket;
		
	}			
	
	// LCMS-9882
	this.GetAssessmentItemsByAssessmentBankIDs = function(assessmentBankIDs) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/GetAssessmentItemsByAssessmentBankIDs",
					  data: "{'assessmentBankIDs':'"+assessmentBankIDs+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var commandInfo = eval('(' + msg.d + ')'); 
						  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket ;
	}  


	this.CourseLockDueToInActiveCurrentWindow = function() 	
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/CourseLockDueToInActiveCurrentWindow",					  
					  data: "{}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  
					  var packet = eval('(' + msg.d + ')'); 
						returnPacket = packet;						
					//
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
		return returnPacket;
		
	}



	// LCMS-9882
	this.SaveAssessmentEndTrackingInfo_ForGameTemplate = function(assessmentType, noOfAnswersCorrect, noOfAnswersInCorrect, currentAttemptNo, weightedScore, isCurrentAssessmentPassed, masteryScore, assessmentTimeInSeconds, remediationCount) 
	{
		var returnPacket = null;
				$.ajax({
					  type: "POST",
					  async:false, cache:false,
					  url: "CoursePlayer.aspx/SaveAssessmentEndTrackingInfo_ForGameTemplate",
					  data: "{'assessmentType':'"+assessmentType+"','noOfAnswersCorrect':'"+noOfAnswersCorrect+"','noOfAnswersInCorrect':'"+noOfAnswersInCorrect+"','currentAttemptNo':'"+currentAttemptNo+"','weightedScore':'"+weightedScore+"','isCurrentAssessmentPassed':'"+isCurrentAssessmentPassed+"','masteryScore':'"+masteryScore+"','assessmentTimeInSeconds':'"+assessmentTimeInSeconds+"','remediationCount':'"+remediationCount+"'}",
					  contentType: "application/json; charset=utf-8",
					  dataType: "json",
					  success: function(msg) {
					  var commandInfo = eval('(' + msg.d + ')'); 
						  returnPacket = commandInfo;   
						  //cp.CommandHelper(courseInfo); 					  
					  },
					  error: function(msg) {
						// Replace the div's content with the page method's return.
					  }
					});
		
			return returnPacket ;
	}  
//    // LCMS-10535
//	this.LoadCourseSettings = function() 
//	{
//		var returnPacket = null;
//	    $.ajax({
//	        type: "POST",
//	        async: false, cache: false,
//	        url: "CoursePlayer.aspx/LoadCourseSettings",
//	        data: "{}",
//	        contentType: "application/json; charset=utf-8",
//	        dataType: "json",
//	        success: function(msg) {
//	            var setObject = eval('(' + msg.d + ')');
//	            returnPacket = setObject;
//	            
//	        },
//	        error: function(msg) {
//	            // Replace the div's content with the page method's return.
//	        }
//	    });

//	    return returnPacket;
//	}

	// LCMS-10392
	this.GetAmazonAffiliatePanelData = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/GetAmazonAffiliatePanelData",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {
	            var commandInfo = eval('(' + msg.d + ')');
	            returnPacket = commandInfo;
	            //cp.CommandHelper(courseInfo); 					  
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}


	// LCMS-10392
	this.GetCourseRecommendationPanelData = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/GetCourseRecommendationPanelData",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {
	            var commandInfo = eval('(' + msg.d + ')');
	            returnPacket = commandInfo;
	            //cp.CommandHelper(courseInfo); 					  
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}

	//Abdus Samad
	//LCMS-12526
	//Start
	this.GetChatForHelpSupport = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/GetChatForHelpSupport",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {

	            var packet = eval('(' + msg.d + ')');
	            returnPacket = packet;
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}
	//Stop

	//Abdus Samad
	//LCMS-12540
	//Start	
	this.IsPreviewModeEnabled = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/IsPreviewModeEnabled",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {

	            var packet = eval('(' + msg.d + ')');
	            returnPacket = packet;
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}
   //Stop

	// Yasin LCMS-12519
	//-----------------------------------------------------
	// LCMS-
	this.GetTimeoutValueForClickAwayLockout = function() {
	    var returnPacket = null;
	    $.ajax({
	        type: "POST",
	        async: false, cache: false,
	        url: "CoursePlayer.aspx/GetTimeoutValueForClickAwayLockout",
	        data: "{}",
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function(msg) {
	            var commandInfo = eval('(' + msg.d + ')');
	            returnPacket = commandInfo;
	            
	        },
	        error: function(msg) {
	            // Replace the div's content with the page method's return.
	        }
	    });

	    return returnPacket;
	}  
	
	// ----------------
  
}

