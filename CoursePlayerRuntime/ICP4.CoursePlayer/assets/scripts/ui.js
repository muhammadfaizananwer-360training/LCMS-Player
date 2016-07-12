var ui = function () {

    return {
		
		init:function()
		{
			if($("html").hasClass("no-touch"))
			{
				//	Desktop Specific
				//ui.mouseAnalyzer(0);
				$('.left-menu .side-menu-nav.items').slimscroll({
					height: '100%',
					color: '#fff',
					wheelStep: 5,
					alwaysVisible: true
				});
			}
			else
			{
				//	Mobile Specific
				$('.left-menu .side-menu-nav.items').slimscroll({
					height: '100%',
					color: '#fff',
					alwaysVisible: true
				});
				/*$(".wrapper-body").on("touchmove", function(event) {
					event.preventDefault();
					event.stopPropagation();
				});*/
			}
			
			ui.guide.init();
			ui.svgModal.init();
			ui.slide.init();
			ui.social.fb.init();
			
			//ui.warning.init();
			
			/*$("#right-side").click(function(e) {
				e.preventDefault();
				$("#wrapper").toggleClass("toggled-right");
				$("#wrapper").removeClass("toggled-left outline glossary");
			});
			$("#bottom-toggle").click(function(e) {
				e.preventDefault();
				$("#wrapper").toggleClass("toggled-bottom");
			});
			$("#top-toggle").click(function(e) {
				e.preventDefault();
				$("#wrapper").toggleClass("toggled-top");
			});*/
			$("#content-overlay").click(function(e) {
				e.preventDefault();
				if($("#wrapper").hasClass("toggled-left"))
				{
					ui.nav.options();
				}
			});
			$('.configures').on('click', '.dropdown-menu', function(){
				$(this).closest('.dropup').addClass('dontClose');
			});
			$('.configures > .dropup').on('hide.bs.dropdown', function(e) {
				if ( $(this).hasClass('dontClose') ){
					e.preventDefault();
				}
				$(this).removeClass('dontClose');
			});
		},
		
		dropBox:function(id,path,title)
		{
			var options = {
				files: [
					// You can specify up to 100 files.
					{'url': path, 'filename': title}
				],

				// Success is called once all files have been successfully added to the user's
				// Dropbox, although they may not have synced to the user's devices yet.
				success: function () {
					// Indicate to the user that the files have been saved.
					console.log("Success! Files saved to your Dropbox.");
				},

				// Progress is called periodically to update the application on the progress
				// of the user's downloads. The value passed to this callback is a float
				// between 0 and 1. The progress callback is guaranteed to be called at least
				// once with the value 1.
				progress: function (progress) {
					console.log("progress: "+progress);
				},

				// Cancel is called if the user presses the Cancel button or closes the Saver.
				cancel: function () {
					console.log("cancel");
				},

				// Error is called in the event of an unexpected response from the server
				// hosting the files, such as not being able to find a file. This callback is
				// also called if there is an error on Dropbox or if the user is over quota.
				error: function (errorMessage) {
					console.log("errorMessage: "+errorMessage);
				}
			};
			var button = Dropbox.createSaveButton(options);
			$("#"+id).parent().append(button);
			$("#"+id).remove();
		},
		
		video:
		{
			instance:'',
			player:function(path,id,_w,_h,_auto)
			{
				if(typeof _auto == "undefined")
				{
					_auto = false;
				}
				if(typeof _h == "undefined")
				{
					_h = 260;
				}
				if(typeof _w == "undefined")
				{
					_w = "100%";
				}
				
				if(typeof id != "undefined")
				{
					jwplayer.key="NWa+NruTBASm39QxfCBvuv1UblvSsMtD+mrZiJgnxNI=";
					ui.video.instance = jwplayer(id);
				}
				
				ui.video.instance.setup({
					file: path,//assets/uploads/fields.mp4',
					/*aspectratio: "16:9",*/
					height: _h,
					repeat: false,
					skin: {"name": "bekle"},
					stagevideo: false,
					stretching: "uniform",
					width: _w,
					autostart: _auto
				});
			}
		},
		
		pdf:function(file)
		{
			new PDFObject({ url: file }).embed("cd-modal-pdf");
		},
		
		nav:{
			cache:'',
			gIndex:0,
			gTerms:["WMI","PowerShell","Networking","CIM Analogy","Certification"],
			
			move:function(cond,pos)
			{
				switch (cond)
				{
					case "outline":
						$("#course-outline .side-menu-nav.items").slimScroll({scrollTo: pos});
					break;
				}
			},
			
			switchGlossary:function(trg,isForward)
			{
				if(isForward)
				{
					ui.nav.gIndex++;
				}
				else
				{
					ui.nav.gIndex--;
				}
				
				$('.cd-modal[data-modal="modal-dynamic"] > .cd-modal-content').html(' ');
				svg_modal_open($(trg));
			},
			
			nestedBtn:function()
			{
				$("#course-outline .hasChildren span").click(function(e) {
					$(this).parent().parent().toggleClass("expand");
				});
			},
			
			options:function ()
			{
				$("#wrapper").toggleClass("toggled-left");
			},
			
			home:function ()
			{
				if($("#wrapper").hasClass("glossary") || $("#wrapper").hasClass("material") || $("#wrapper").hasClass("outline"))
				{
					$("#wrapper").toggleClass("options");
				}
				else
				{
					$("#wrapper").toggleClass("toggled-left").toggleClass("options");
				}
				$("#wrapper").removeClass("toggled-right glossary material outline");
			},
			
			outline:function ()
			{
				if($("#wrapper").hasClass("glossary") || $("#wrapper").hasClass("material") || $("#wrapper").hasClass("options"))
				{
					$("#wrapper").toggleClass("outline");
				}
				else
				{
					$("#wrapper").toggleClass("toggled-left").toggleClass("outline");
				}
				$("#wrapper").removeClass("toggled-right glossary material options");
			},
			
			glossary:function ()
			{
				if($("#wrapper").hasClass("outline") || $("#wrapper").hasClass("material") || $("#wrapper").hasClass("options"))
				{
					$("#wrapper").toggleClass("glossary");
				}
				else
				{
					$("#wrapper").toggleClass("toggled-left").toggleClass("glossary");
				}
				$("#wrapper").removeClass("toggled-right outline material options");
			},
			
			material:function ()
			{
				if($("#wrapper").hasClass("outline") || $("#wrapper").hasClass("glossary") || $("#wrapper").hasClass("options"))
				{
					$("#wrapper").toggleClass("material");
				}
				else
				{
					$("#wrapper").toggleClass("toggled-left").toggleClass("material");
				}
				$("#wrapper").removeClass("toggled-right outline glossary options");
			}
		},
		
		warning:
		{
			instance:'',
			count:0,
			step:0,
			duration:9,
			init:function()
			{
				ui.warning.interval(function()
				{
					ui.loader("show", function()
					{
						$("#wrapper").attr("data-remain","00:0"+ui.warning.duration).append("<div id='continue-course-btn'><a href='javascript:ui.warning.close();' class='cd-btn main-action'>CONTINUE</a></div>");
						ui.warning.step = 1;
						ui.warning.interval(function()
						{
							window.open("session_expire.html","_self");
						},ui.warning.duration);
					},"session");
				},5);
			},
			
			interval:function(callBack,duration)
			{
				ui.warning.clear();
				ui.warning.instance = setInterval(function()
				{
					if(ui.warning.step == 1)
					{
						//console.log(ui.warning.count + " > "+duration+" - "+(ui.warning.step == 1));
						$("#wrapper").attr("data-remain","00:0"+(ui.warning.duration-ui.warning.count));
					}
					
					if(ui.warning.count >= duration)
					{
						ui.warning.clear();
						if(typeof callBack == "function")
						{
							callBack();
						}
					}
					ui.warning.count++;
				},1000);
			},
			
			close:function()
			{
				if(ui.warning.step == 1)
				{
					ui.loader("hide", function()
					{
						
					},"session");
				}
				ui.warning.step = 0;
				$("#wrapper").removeAttr("data-remain");
				$("#wrapper #continue-course-btn").remove();
				
				ui.warning.clear();
			},
			
			clear:function()
			{
				ui.warning.count = 0;
				if(ui.warning.instance != '')
				{
					clearInterval(ui.warning.instance);
				}
			}
		},
		
		mouseAnalyzer:function (n)
		{
			switch(n){
				case 0:
					$('html').mouseleave(function() {
						//console.log('out');
						ui.nav.cache = $('#wrapper').attr('class');
						$('#wrapper').removeClass('toggled-top toggled-left toggled-bottom');
						ui.mouseAnalyzer(1);
					});
					ui.mouseAnalyzer(1);
				break;
				case 1:
					$('html').mouseover(function() {
						//console.log('over');
						$('#wrapper').addClass(ui.nav.cache);
						ui.nav.cache = '';
						$('html').unbind('mouseover');
					});
				break;
			}
		},
	
		svgModal:
		{
			coverLayer:'',
			duration:0,
			epsilon:0,
			aniObject:'',
			pathSteps:{},
			pathsArray:{},
			init: function (modalTrigger)
			{
				ui.svgModal.coverLayer = $('.cd-cover-layer');
				ui.svgModal.duration = 600;
				ui.svgModal.epsilon = (1000 / 60 / ui.svgModal.duration) / 4;
				ui.svgModal.aniObject = ui.svgModal.bezier(.63,.35,.48,.92, ui.svgModal.epsilon);
				
				$('a[data-type="cd-modal-trigger"]').each(function(){
					
					var modalTriggerId = (typeof $(this).attr('data-group') == "undefined"? $(this).attr('id'):$(this).attr('data-group'))/*$(this).attr('id')*/,
						modal = $('.cd-modal[data-modal="'+ modalTriggerId +'"]'),
						svgCoverLayer = modal.children('.cd-svg-bg'),
						paths = svgCoverLayer.find('path');
						ui.svgModal.pathsArray[modalTriggerId] = [];
					
					//store Snap objects
					ui.svgModal.pathsArray[modalTriggerId][0] = Snap('#'+paths.eq(0).attr('id')),
					ui.svgModal.pathsArray[modalTriggerId][1] = Snap('#'+paths.eq(1).attr('id')),
					ui.svgModal.pathsArray[modalTriggerId][2] = Snap('#'+paths.eq(2).attr('id'));

					//store path 'd' attribute values	
					ui.svgModal.pathSteps[modalTriggerId] = [];
					ui.svgModal.pathSteps[modalTriggerId][0] = svgCoverLayer.data('step1');
					ui.svgModal.pathSteps[modalTriggerId][1] = svgCoverLayer.data('step2');
					ui.svgModal.pathSteps[modalTriggerId][2] = svgCoverLayer.data('step3');
					ui.svgModal.pathSteps[modalTriggerId][3] = svgCoverLayer.data('step4');
					ui.svgModal.pathSteps[modalTriggerId][4] = svgCoverLayer.data('step5');
					ui.svgModal.pathSteps[modalTriggerId][5] = svgCoverLayer.data('step6');
					
					//open modal window
					$(this).on('click', function(event){
						event.preventDefault();
						ui.svgModal.open(this);
					});
				});
			},
			
			open:function(trg)
			{
				var $trg = $(trg)
				var id = (typeof $trg.attr('data-group') == "undefined"? $trg.attr('id'):$trg.attr('data-group'));
				var modal = $('.cd-modal[data-modal="'+ id +'"]');
				modal.addClass('modal-is-visible');
				ui.svgModal.coverLayer.addClass('modal-is-visible');
				ui.svgModal.animateModal(ui.svgModal.pathsArray[id], ui.svgModal.pathSteps[id], ui.svgModal.duration, 'open');
				svg_modal_open($trg);
			},
			
			close:function(id)
			{
				var modal = $('.cd-modal[data-modal="'+ id +'"]');
				modal.removeClass('modal-is-visible');
				ui.svgModal.coverLayer.removeClass('modal-is-visible');
				ui.svgModal.animateModal(ui.svgModal.pathsArray[id], ui.svgModal.pathSteps[id], ui.svgModal.duration, 'close');
				svg_modal_close(id,modal);
			},
			
			animateModal: function (paths, pathSteps, duration, animationType)
			{
				var path1 = ( animationType == 'open' ) ? pathSteps[1] : pathSteps[0],
					path2 = ( animationType == 'open' ) ? pathSteps[3] : pathSteps[2],
					path3 = ( animationType == 'open' ) ? pathSteps[5] : pathSteps[4];
				paths[0].animate({'d': path1}, duration, ui.svgModal.aniObject);
				paths[1].animate({'d': path2}, duration, ui.svgModal.aniObject);
				paths[2].animate({'d': path3}, duration, ui.svgModal.aniObject);
			},

			bezier: function (x1, y1, x2, y2, epsilon)
			{
				var curveX = function(t){
					var v = 1 - t;
					return 3 * v * v * t * x1 + 3 * v * t * t * x2 + t * t * t;
				};

				var curveY = function(t){
					var v = 1 - t;
					return 3 * v * v * t * y1 + 3 * v * t * t * y2 + t * t * t;
				};

				var derivativeCurveX = function(t){
					var v = 1 - t;
					return 3 * (2 * (t - 1) * t + v * v) * x1 + 3 * (- t * t * t + 2 * v * t) * x2;
				};

				return function(t){

					var x = t, t0, t1, t2, x2, d2, i;

					// First try a few iterations of Newton's method -- normally very fast.
					for (t2 = x, i = 0; i < 8; i++){
						x2 = curveX(t2) - x;
						if (Math.abs(x2) < epsilon) return curveY(t2);
						d2 = derivativeCurveX(t2);
						if (Math.abs(d2) < 1e-6) break;
						t2 = t2 - x2 / d2;
					}

					t0 = 0, t1 = 1, t2 = x;

					if (t2 < t0) return curveY(t0);
					if (t2 > t1) return curveY(t1);

					// Fallback to the bisection method for reliability.
					while (t0 < t1){
						x2 = curveX(t2);
						if (Math.abs(x2 - x) < epsilon) return curveY(t2);
						if (x > x2) t0 = t2;
						else t1 = t2;
						t2 = (t1 - t0) * .5 + t0;
					}

					// Failure
					return curveY(t2);
				};
			}
		
		},
		
		isoTope:
		{
			container:'',
			lastFilter:'*',
			init:function()
			{
				$("#iso-filter-btns").children().each(function(i){
					
					$(this).click(function(e){
						e.preventDefault();
						var filter = $(this).attr('data-filter');
						$(this).addClass("active");
						$("button[data-filter='"+ui.isoTope.lastFilter+"']").removeClass("active");
						ui.isoTope.lastFilter = filter;
						ui.isoTope.container.isotope({ filter: filter });
					});
				});
				
				ui.isoTope.container = $('#course_recomendations').isotope({
					layoutMode: 'fitRows',
					getSortData:{
						category: '[data-category]'
					}
				});
			},
			item:
			{
				add:function(elm){
					ui.isoTope.container.isotope('insert', elm);
				},
				close:function(elm){
					ui.isoTope.container.isotope( 'remove', $(elm).parent() ).isotope('layout');
				}
			}
			
		},
		
		slide:
		{
			intervalObj:'',
			sceneDuration:0,
			counter:0,
			coverLayer:'',
			duration:300,
			epsilon:0,
			aniObject1:'',
			aniObject2:'',
			svgPath:'',
			pathArray:[],
			path4:'',
			path5:'',
			init:function()
			{
				ui.slide.epsilon = (1000 / 60 / ui.slide.duration) / 4;
				ui.slide.aniObject1 = ui.svgModal.bezier(.42,.03,.77,.63, ui.slide.epsilon);
				ui.slide.aniObject2 = ui.svgModal.bezier(.27,.5,.6,.99, ui.slide.epsilon);
				
				ui.slide.coverLayer = $('#svg-loader-bg');
				var pathId = ui.slide.coverLayer.find('path').attr('id');
				ui.slide.svgPath = Snap('#'+pathId);
				
				ui.slide.pathArray[0] = ui.slide.coverLayer.data('step1');
				ui.slide.pathArray[1] = ui.slide.coverLayer.data('step6');
				ui.slide.pathArray[2] = ui.slide.coverLayer.data('step2');
				ui.slide.pathArray[3] = ui.slide.coverLayer.data('step7');
				ui.slide.pathArray[4] = ui.slide.coverLayer.data('step3');
				ui.slide.pathArray[5] = ui.slide.coverLayer.data('step8');
				ui.slide.pathArray[6] = ui.slide.coverLayer.data('step4');
				ui.slide.pathArray[7] = ui.slide.coverLayer.data('step9');
				ui.slide.pathArray[8] = ui.slide.coverLayer.data('step5');
				ui.slide.pathArray[9] = ui.slide.coverLayer.data('step10');
			},
			
			transition:function(template)
			{
				switch(template)
				{
					case "lesson_intro_typing":
						$('#page-content-wrapper .cd-intro-content').addClass("mask");
						$('#page-content-wrapper .content-wrapper').removeClass("fade");
					break;
				}
			},
			
			loader:{
				hide:function(callBack)
				{
					$('body').removeClass("static-loader");
					callBack();
					/*
					if(ui.slide.coverLayer.hasClass('loader'))
					{
						ui.slide.coverLayer.removeClass('loader');
						ui.slide.svgPath.animate({'d': ui.slide.path4}, ui.slide.duration, ui.slide.aniObject1, function(){
							ui.slide.svgPath.animate({'d': ui.slide.path5}, ui.slide.duration, ui.slide.aniObject2, function(){
								ui.slide.coverLayer.removeClass('is-animating');
								
								callBack();
								
							});
						});
					}*/
				},
				show:function(callBack)
				{
					$('body').addClass("static-loader");
					callBack();
				}
			},
			
			dummyLoad:function(template,callBack)
			{
				$("#page-content-wrapper > .wrapper-body").load("templates/"+template+".html", function()
				{
					if(typeof callBack == "function")
					{
						callBack();
					}
				});
			},
			
			prev:function(callBack)
			{
				ui.slide.loader.show(function(){
					callBack();
				});
				
				//ui.slide.coverLayer.addClass('loader');
				
				/*var path1 = ui.slide.pathArray[0],
					path2 = ui.slide.pathArray[2],
					path3 = ui.slide.pathArray[4];
				ui.slide.path4 = ui.slide.pathArray[6];
				ui.slide.path5 = ui.slide.pathArray[8];
				
				ui.slide.coverLayer.addClass('is-animating');
				
				ui.slide.svgPath.attr('d', path1);
				ui.slide.svgPath.animate({'d': path2}, ui.slide.duration, ui.slide.aniObject1, function(){
					ui.slide.coverLayer.addClass('loader');
					ui.slide.svgPath.animate({'d': path3}, ui.slide.duration, ui.slide.aniObject2, function(){
						
						callBack();
						
					});
				});*/
			},
			
			next:function(callBack)
			{
				ui.slide.loader.show(function(){
					callBack();
				});
				
				//ui.slide.coverLayer.addClass('loader');
				
				/*var path1 = ui.slide.pathArray[1],
					path2 = ui.slide.pathArray[3],
					path3 = ui.slide.pathArray[5];
				ui.slide.path4 = ui.slide.pathArray[7];
				ui.slide.path5 = ui.slide.pathArray[9];
				
				ui.slide.coverLayer.addClass('is-animating');
				
				ui.slide.svgPath.attr('d', path1);
				ui.slide.svgPath.animate({'d': path2}, ui.slide.duration, ui.slide.aniObject1, function(){
					ui.slide.coverLayer.addClass('loader');
					ui.slide.svgPath.animate({'d': path3}, ui.slide.duration, ui.slide.aniObject2, function(){
						
						callBack();
						
					});
				});*/
			},
			
			timer:function(LENGTH)
			{
				if(ui.slide.intervalObj != "")
				{
					clearInterval(ui.slide.intervalObj);
					ui.slide.intervalObj = "";
				}
				
				ui.slide.counter = 0;
				ui.slide.sceneDuration = LENGTH;
				
				var $sd = $("#scene-duration");
				var $bar = $sd.find(".progress-bar");
				var $label = $sd.find("label");
				$bar.css("width","100%");
				$label.text("00:0"+(ui.slide.sceneDuration-ui.slide.counter));
				
				$sd.addClass('slideIn');
				
				ui.slide.intervalObj = setInterval(function(){
					
					if(ui.slide.counter >= ui.slide.sceneDuration)
					{
						$sd.removeClass('slideIn');
						clearInterval(ui.slide.intervalObj);
						ui.slide.intervalObj = "";
					}
					else
					{
						ui.slide.counter++;
						$bar.css("width",((((ui.slide.sceneDuration-ui.slide.counter) / ui.slide.sceneDuration)) * 100)+"%");
						$label.text("00:0"+(ui.slide.sceneDuration-ui.slide.counter));
					}
					
				},1000);
			}
		},
		
		loader:function(cond,callBack,state)
		{
			var $body = $('body');
			if(cond == "show")
			{
				$body.addClass("pre-loader done up");
				
				setTimeout(function(){
					$body.removeClass("done up");
					setTimeout(function(){
						$body.addClass(state);
					},500);
					setTimeout(function(){
						callBack();
					},1500);
				},500);
			}
			else if(cond == "hide")
			{
				$body.addClass("done");
				setTimeout(function(){
					$body.addClass("up");
					setTimeout(function(){
						$body.removeClass("pre-loader done up "+state);
						callBack();
					},500);
				},500);
			}
		},
		
		guide:{
			trg:{tourWrapper:'',tourSteps:'',stepsNumber:''},
			init:function()
			{
				ui.guide.trg.tourWrapper = $('.cd-tour-wrapper');
				ui.guide.trg.tourSteps = ui.guide.trg.tourWrapper.children('li');
				ui.guide.trg.stepsNumber = ui.guide.trg.tourSteps.length;
				var tourStepInfo = $('.cd-more-info');

				//create the navigation for each step of the tour
				ui.guide.createNav(ui.guide.trg.tourSteps, ui.guide.trg.stepsNumber);
				
				$('#cd-tour-trigger').on('click', function(){
					
					//	BEGIN - MODAL BOX -------------------------------
					var $trgModal = $("#dynamicModal");
				
					//	BEGIN TITLE, MESSAGE AND BUTTONS
					var title = 'Welcome To The New Course Player';
					var msg = "<p class='guide-content'>This guided tour will walk you through all the new functionality, look, and feel included in the new course player experience.</p>";
					var btns = '<button type="button" class="cd-btn main-action" onclick="ui.guide.begin();" data-dismiss="modal">Start Tutorial</button>';
					//	END TITLE, MESSAGE AND BUTTONS
					
					$trgModal.find(".modal-title").html(title);
					$trgModal.find(".modal-body").html(msg);
					$trgModal.find(".modal-footer").html(btns);
					
					$trgModal.modal('show');
					//	END - MODAL BOX --------------------------------
					
				});

				//change visible step
				tourStepInfo.on('click', '.cd-prev', function(event){
					//go to prev step - if available
					( !$(event.target).hasClass('inactive') ) && ui.guide.changeStep(ui.guide.trg.tourSteps, 'prev');
				});
				tourStepInfo.on('click', '.cd-next', function(event){
					//go to next step - if available
					if($(event.target).hasClass('end'))
					{
						//	BEGIN - MODAL BOX -------------------------------
						var $trgModal = $("#dynamicModal");
					
						//	BEGIN TITLE, MESSAGE AND BUTTONS
						var title = "That's It. Start Learning.";
						var msg = "<p class='guide-content'>In an effort to keep the course player as simple as possible, we've created an easy to use interface for your learning experience. We hope you enjoy it.</p>";
						var btns = '<button type="button" class="cd-btn main-action" data-dismiss="modal">Close Tour</button>';
						//	END TITLE, MESSAGE AND BUTTONS
						
						$trgModal.find(".modal-title").html(title);
						$trgModal.find(".modal-body").html(msg);
						$trgModal.find(".modal-footer").html(btns);
						
						$trgModal.modal('show');
						ui.guide.close(ui.guide.trg.tourSteps, ui.guide.trg.tourWrapper);
						//	END - MODAL BOX --------------------------------
					}
					else
					{
						(!$(event.target).hasClass('inactive') ) && ui.guide.changeStep(ui.guide.trg.tourSteps, 'next');
					}
				});

				//close tour
				tourStepInfo.on('click', '.cd-close', function(event){
					ui.guide.close(ui.guide.trg.tourSteps, ui.guide.trg.tourWrapper);
				});
			},
			
			begin:function()
			{
				//start tour
				if(!ui.guide.trg.tourWrapper.hasClass('active')) {
					//in that case, the tour has not been started yet
					ui.guide.trg.tourWrapper.addClass('active');
					ui.guide.showStep(ui.guide.trg.tourSteps.eq(0));
				}
			},

			createNav:function(steps, n)
			{
				var tourNavigationHtml = '<div class="cd-nav"><span><b class="cd-actual-step">1</b> of '+n+'</span><ul class="cd-tour-nav"><li><a href="javascript:;" class="cd-prev">&#171; Previous</a></li><li><a href="javascript:;" class="cd-next">Next &#187;</a></li></ul></div><a href="javascript:;" class="cd-close"></a>';

				steps.each(function(index){
					var step = $(this),
						stepNumber = index + 1,
						nextClass = ( stepNumber < n ) ? '' : 'inactive',
						prevClass = ( stepNumber == 1 ) ? 'inactive' : '';
					var nav = $(tourNavigationHtml).find('.cd-next').addClass(nextClass).end().find('.cd-prev').addClass(prevClass).end().find('.cd-actual-step').html(stepNumber).end().appendTo(step.children('.cd-more-info'));
				});
			},

			showStep:function(step)
			{
				switch(step.attr("id")){
					case "guide-menu":
						$("#wrapper").removeClass("toggled-left");
					break;
					case "guide-menu-expend":
						$("#wrapper").addClass("toggled-left");
					break;
					case "guide-time-spent":
						step.find(".cd-next.inactive").removeClass("inactive").addClass("end");
					break;
				}
				step.addClass('is-selected').removeClass('move-left');
				ui.guide.smoothScroll(step.children('.cd-more-info'));
			},

			smoothScroll:function(element)
			{
				(element.offset().top < $(window).scrollTop()) && $('body,html').animate({'scrollTop': element.offset().top}, 100);
				(element.offset().top + element.height() > $(window).scrollTop() + $(window).height() ) && $('body,html').animate({'scrollTop': element.offset().top + element.height() - $(window).height()}, 100); 
			},

			changeStep:function(steps, bool)
			{
				var visibleStep = steps.filter('.is-selected'),
					delay = (ui.guide.viewportSize() == 'desktop') ? 300: 0; 
				visibleStep.removeClass('is-selected');

				(bool == 'next') && visibleStep.addClass('move-left');

				setTimeout(function(){
					( bool == 'next' )
						? ui.guide.showStep(visibleStep.next())
						: ui.guide.showStep(visibleStep.prev());
				}, delay);
			},

			close:function(steps, wrapper)
			{
				steps.removeClass('is-selected move-left');
				wrapper.removeClass('active');
			},

			viewportSize:function()
			{
				/* retrieve the content value of .cd-main::before to check the actua mq */
				return window.getComputedStyle(document.querySelector('.cd-tour-wrapper'), '::before').getPropertyValue('content').replace(/"/g, "").replace(/'/g, "");
			}
		},
		
		social:
		{
			title:"",
			specificTitle:"",
			desc:"",
			url:"",
			img:"",
			
			in:{
				init:function()
				{
					IN.Event.on(IN, "auth", ui.social.in.share);
                },
				
				share:function(caseNum)
				{
					var obj = {
						url: ui.social.url,
						title:(caseNum == 1?ui.social.title:ui.social.specificTitle+' of '+ui.social.title),
						summary: ui.social.desc,
						source: ui.social.url
					}
					window.open('http://www.linkedin.com/shareArticle?mini=true&url='+obj.url+'&title='+obj.title+'&summary='+obj.summary+'&source='+obj.source+'', "", "width=600,height=400");
					
					//IN.UI.Share().params(obj).place();
				}

			},
			
			fb:
			{
				/*
				ui = 1210481435650319
				localhost = 1226415294056933
				dev = 1226413724057090
				qa = 1219762514722211
				live = 1073273936037737
				*/
				
				key:'1226413724057090',
				init: function(){
				  window.fbAsyncInit = function() {
					FB.init({
					  appId      : ui.social.fb.key,
					  xfbml      : true,
					  version    : 'v2.6'
					});
				  };

				  (function(d, s, id){
					 var js, fjs = d.getElementsByTagName(s)[0];
					 if (d.getElementById(id)) {return;}
					 js = d.createElement(s); js.id = id;
					 js.src = "//connect.facebook.net/en_US/sdk.js";
					 fjs.parentNode.insertBefore(js, fjs);
				   }(document, 'script', 'facebook-jssdk'));
				},
				
				share:function(caseNum)
				{
					FB.ui({
						method: 'feed',
						app_id: ui.social.fb.key,
						name: (caseNum == 1?ui.social.title:ui.social.specificTitle+' of '+ui.social.title),
						link: ui.social.url,
						picture: ui.social.img,
						caption: "To find more details, click on this post",
						description: ui.social.desc
					},
					function(response){
						
					});
				}
				
			}
		}
	}
}();