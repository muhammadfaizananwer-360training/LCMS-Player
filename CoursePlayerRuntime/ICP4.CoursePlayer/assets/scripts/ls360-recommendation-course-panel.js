/**
 * ...........................................................................................
 * LS360 Suggested Courses Product API: jQuery Plugin Author: Abdus Samad 
 * June 26 2013
 * ...........................................................................................
 */

function f_clientHeight() {
    return $(window).height();
}

!function(exports, $, undefined) {
    /** LS360 Suggested Courses Product API Plugin */
    var LS360RecommendationCoursePanel = function() {
        /** LS360 Suggested Courses Product API : Plugin Variables */
        var ls360RecommendationCourseProducts = {}, // private API
		LS360RecommendationCoursePanel = {}, // public API
        /** LS360 Suggested Courses Product API : Default Variables */
		defaults = {
		    amazonAffiliateProducts: "",
		    recommendationPanelWidth: 800,
		    recommendationPanelHeight: 140,
		    recommendationItemWidth: 302,
		    defaultDisplay: true,
		    recommendationPanelHTML: '<!-- LS360 Amazon Affiliate Product API : Affiliate Panel --><div id="recommendationPanelHeader" class="recommendation-panel-header" ></div><div id="recommendationPanelWrapper" class="recommendation-panel-wrapper" style="display:none;"><div id="recommendationPanel" class="recommendation-panel recommendation-panel-hidden"><div id="loadingGraphic" class="loading-graphic" style="display:none;">Loading Graphic</div></div></div><!-- End Amazon Product API : Affiliate Panel -->',
		    recommendationPanelWSURL: "http://10.0.100.250:8080/lms/aws_product.do?jsoncallback=?&keyword="

		};
        /**
        * PUBLIC : LS360 Suggested Courses Product API : Public Visibility Value
        */
        LS360RecommendationCoursePanel.visible = false;

        /**
        * PUBLIC : LS360 Suggested Courses Product API : Hide Affiliate Panel
        */
        LS360RecommendationCoursePanel.hideAffiliatePanel = function() {
            ls360RecommendationCourseProducts.hideAffiliatePanel();
        }

        /**
        * PUBLIC : LS360 Suggested Courses Product API : Show Affiliate Panel
        */
        LS360RecommendationCoursePanel.showAffiliatePanel = function() {
            ls360RecommendationCourseProducts.showAffiliatePanel();
        }

        /** PRIVATE : LS360 Suggested Courses Product API: Options Variables */
        ls360RecommendationCourseProducts.options = {};

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Select Product Results
        */

        function CheckingAmazonPanel() {
            $('#IcoRecommendationCoursePanel').show();
            $('#IcoRecommendationCoursePanelDs').hide();
            if ($("#recommendationPanelWrapper").is(":visible")) {
                StopcheckingAmazonPanel();
            }
            else {
                $('#recommendationPanelHeader').hide();
                ls360RecommendationCourseProducts.displayNoRecommendedMessage();
                StopcheckingAmazonPanel();
                ls360RecommendationCourseProducts.hideAffiliatePanel()
                IsRecommendationCoursePanel = false;
                bIsContentResize = true;
            }
        }

        function StopcheckingAmazonPanel() {
            clearInterval(myPanel);
            $('#IcoRecommendationCoursePanel').show();
            $('#IcoRecommendationCoursePanelDs').hide();
        }

        ls360RecommendationCourseProducts.selectAffiliateProducts = function(keywordParameter) {
            // Retrieve JSON Object from LMS AWS Integration Controller
            try {
                myPanel = setInterval(function() { CheckingAmazonPanel() }, 10000);

                //$.getJSON(ls360RecommendationCourseProducts.options.affiliatePanelWSURL + keywordParameter, //LCMS-10392
                //function(data) { 


                information = eval("(" + keywordParameter + ")");
                data = information[0];
                if (data != null && data.affiliateItems && data.affiliateItems.length > 0) {
                    // Assign Product Results to Plugins Private Variable
                    StopcheckingAmazonPanel();
                    ls360RecommendationCourseProducts.amazonAffiliateProducts = data;
                    // Load Affiliate Panel Products
                    ls360RecommendationCourseProducts.addAffiliatePanelItems();
                    // Display Affiliate Panel
                    ls360RecommendationCourseProducts.showAffiliatePanel();
                    // Add Toggle Affiliate Panel Click Event
                    ls360RecommendationCourseProducts.addAffiliatePanelClickEvent();
                    // Set Default Display State
                    if (ls360RecommendationCourseProducts.options.defaultDisplay) {
                        // Set the Affiliate Panel Wrapper to the width of the Course Players Course Title
                        $('#recommendationPanelHeader').show();
                        $('#recommendationPanelWrapper').css("width", $('#course-title').width());
                        $('#contentWrapperReco').css('width', $('#content-frame').width());
                        $('#recommendationPanel').removeClass('recommendation-panel-hidden');
                    }
                }
                else {//Don't show affiliate panel if no book is returned
                    ls360RecommendationCourseProducts.hideAffiliatePanel();
                    $('#contentWrapperReco').hide();
                }

                // });

            }
            catch (Error) {
            }

        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Create Affiliate Panel
        */
        ls360RecommendationCourseProducts.addAffiliatePanelItems = function() {
            // Retrieve Affiliate Products from Plugin Object
            var affiliateProducts = ls360RecommendationCourseProducts.amazonAffiliateProducts.affiliateItems;
            // Load Each of the Affiliate Panel Books
            $.each(affiliateProducts, function(index, value) {
                ls360RecommendationCourseProducts.buildAffiliatePanelBook(value, index);
            });

            // Set Affiliate Panel Item Width
            var affiliateItemWidth = (affiliateProducts.length * ls360RecommendationCourseProducts.options.recommendationItemWidth);
            $('#recommendationPanel').css("width", affiliateItemWidth + "px");
            // Add Scrolling Elements
            $('#recommendationPanelWrapper').css('overflow', 'scroll');
            $('#recommendationPanelWrapper').css('overflow-y', 'hidden');
            $('#recommendationPanelWrapper').css('overflow-x', 'scroll');
            // Remove Loading Graphic.
            $('#recommendationPanel').find("#loadingGraphic").remove();
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Show Affiliate Panel
        */
        ls360RecommendationCourseProducts.showAffiliatePanel = function() {
            // -- N3Player Specific -- 
            // Resize Course Player Content
            $("#content-frame").height(f_clientHeight() - 222 - 154);
            // Show Affiliate Panel
            $('#recommendationPanelWrapper').css('display', 'block');
            // Set Affiliate Panel Visibility value
            LS360RecommendationCoursePanel.visible = true;
            // Set Affiliate Panel Header Text
            $('#recommendationPanelHeader').text("");

            //Abdus New Stuff
            $('#recommendationPanelHeader').css('display', 'block');
            
            //Added By Abdus Samad For Image In Header Panel
            //START
            $('#recommendationPanelHeader').prepend('<img  src="images/cross-icon.png" title="Close" />')
            //END
           
            content_resize();
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Hide Affiliate Panel
        */
        ls360RecommendationCourseProducts.hideAffiliatePanel = function() {
            // -- N3Player Specific -- 
            // Resize Course Player Content
            $("#content-frame").height(f_clientHeight() - 150);
            // Hide Affiliate Panel
            $('#recommendationPanelWrapper').css('display', "none");
            // Set Affiliate Panel Visibility value
            LS360RecommendationCoursePanel.visible = false;
            // Set Affiliate Panel Header Text
            $('#recommendationPanelHeader').text("");

            //Abdus New Stuff
            $('#recommendationPanelHeader').css('display', 'none');
            
            content_resize();
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Build Affiliate Panel
        * HTML
        */
        ls360RecommendationCourseProducts.createAffiliatePanelHTML = function() {
            // Append Affiliate Panel to HTML
            $('#contentWrapperReco').append(ls360RecommendationCourseProducts.options.recommendationPanelHTML);
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Add Toggle Button
        * Click Event
        */
        ls360RecommendationCourseProducts.addAffiliatePanelClickEvent = function() {
            $('#recommendationPanelHeader').bind("click", function() {

            //Added By Abdus Samad For Image In Header Panel
            //START
            
            ls360RecommendationCourseProducts.hideAffiliatePanel();
            $("#contentWrapperReco").hide();
            $("#recommendationItemTemplate").hide();
            content_resize();       
            //STOP   
            });
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Handle redirect to Amazon site
        * Click Event
        */
        ls360RecommendationCourseProducts.redirectToAmazon = function(detailUrl) {
            if ($("#recommendationPanelRedirectDialog").length == 0) {

                var htmlRedirectConfirmaton = '<div id="recommendationPanelRedirectDialog" class="recommendation-panel-redirect-dialog">' +
                                                    '<h2>Question</h2>' +
                                                    '<div class="ap-redirect-dialog-message">' +
                                                        recomendedCourseDialogMsg +
                                                    '</div>' +
                                                    '<div class="ap-redirect-dialog-buttons">' +
                                                        '<button id="recommendationPanelCancelButton" class="button">'+cancel_text+'</button>' +
                                                        '<button id="recommendationPanelOkButton" class="button">'+ok_text+'</button>' +
                                                    '</div>' +
                                                '</div>';

                $(htmlRedirectConfirmaton).appendTo('body');
                $("#recommendationPanelCancelButton").bind("click", function() {
                    $("#recommendationPanelOkButton").unbind("click");
                    $("#recommendationPanelRedirectDialog").fadeOut("slow");
                    $("#overlay").fadeOut("slow");
                    //detailnew = null;
                });
            }
            $("#recommendationPanelOkButton").bind("click.ap", function() {
                $("#recommendationPanelOkButton").unbind("click.ap");
                $("#recommendationPanelRedirectDialog").fadeOut("slow");
                $("#overlay").fadeOut("slow");
                //alert(detailUrl);
                window.open(detailUrl);
            });

            $("#overlay").css({ "opacity": "0.7" });
            $("#overlay").fadeIn("slow");
            $("#recommendationPanelRedirectDialog").fadeIn("slow");
        }



        /**
        * PRIVATE : 
        * Message display no recommended found
        */
        ls360RecommendationCourseProducts.displayNoRecommendedMessage = function() {
            if ($("#recommendationPanelRedirectDialog").length == 0) {
                var htmlRedirectConfirmaton = '<div id="recommendationPanelRedirectDialog" class="recommendation-panel-redirect-dialog">' +
                                                    '<h2>' + InformationText + '</h2>' +
                                                    '<div class="ap-redirect-dialog-message">' +
                                                        recomendedCourseDialogMsg_NotFound +
                                                    '</div>' +
                                                    '<div class="ap-redirect-dialog-buttons">' +
                                                        '<button id="recommendationPanelCancelButton" class="button">'+ok_text+'</button>' +
                                                    '</div>' +
                                                '</div>';

                $(htmlRedirectConfirmaton).appendTo('body');
                $("#recommendationPanelCancelButton").bind("click", function() {
                    $("#recommendationPanelRedirectDialog").fadeOut("slow");
                    $("#overlay").fadeOut("slow");
                });
            }

            $("#overlay").css({ "opacity": "0.7" });
            $("#overlay").fadeIn("slow");
            $("#recommendationPanelRedirectDialog").fadeIn("slow");
        }

        /**
        * PRIVATE : LS360 Suggested Courses Product API : Build Product Item
        */
        ls360RecommendationCourseProducts.buildAffiliatePanelBook = function(affiliateItemJSON, index) {
            // Build and assign product to results container.
            // Assign Values for Book Template            
            var title = affiliateItemJSON.CourseName;
            var author = "";                                 //affiliateItemJSON.author;
            var rating = "";                                 //affiliateItemJSON.rating;
            var price = "";                                  // affiliateItemJSON.price;
            var imageUrl = affiliateItemJSON.CourseImageUrl;
            var detailUrl = affiliateItemJSON.StoreAddress;  //affiliateItemJSON.redirectToUrl;
            var affiliateItemId = "recommendationItem" + index;
            // Clone Book Template Object
            // TODO: Replace with Object Creation
            var amazonItemTemplate = $('#recommendationItemTemplate').clone();
            // Alter Cloned Book Object
            // - Complete Book Id
            $(amazonItemTemplate).attr('id', affiliateItemId);
            // Adjust Affiliate Item Title Length
            var truncLen = 20;
            var newTitle = "";
            var oldTitle = title;
            if (title.length > truncLen) {
                newTitle = title.substring(0, truncLen);
                newTitle = newTitle.replace(/w+$/, '');
                if (title != newTitle) {
                    newTitle = newTitle + "...";
                    title = newTitle;
                }
            }

            // - Complete Course Title
            //if (title.length > truncLen)
            //{
            //LCMS-12104 (The tool tip for the Course Title has been added)
            $(amazonItemTemplate).find('#recommendationItemTitle').attr('title', oldTitle);
            //}
            $(amazonItemTemplate).find('#recommendationItemTitle').html(title);
            $(amazonItemTemplate).find('#recommendationItemTitle').bind("click", function() {
                ls360RecommendationCourseProducts.redirectToAmazon(detailUrl);
            });
            $(amazonItemTemplate).find('#recommendationItemTitle').css("cursor", "pointer");
            // - Complete Course Image   

            var defaultImageUrl = 'images/ico_default_course.png';
            if (!imageUrl || imageUrl.length == 0) {
                imageUrl = defaultImageUrl;
            }

            $(amazonItemTemplate).find('#imgRecommendationItemMedium').
					error(function() {
					    if (!imageUrl || imageUrl.length == 0) {
					        $(this).attr('src', defaultImageUrl);
					    }
					    else {
					        $(this).attr('src', imageUrl);
					    }
					}).
					attr('src', imageUrl);
            $(amazonItemTemplate).find('#imgRecommendationItemMedium').attr('id',
					"imgRecommendationItemMedium" + index);
            // }
            // - Complete Course Author
            $(amazonItemTemplate).find('#recommendationItemAuthor').html(author);
            // - Complete Course Price
            $(amazonItemTemplate).find('#recommendationItemPrice').html(price);
            // - Complete Course Image Link
            $(amazonItemTemplate).find('#imgRecommendationItemMedium' + index).attr('url', detailUrl);
            $(amazonItemTemplate).find('#imgRecommendationItemMedium' + index).bind("click",
				function() {
				    ls360RecommendationCourseProducts.redirectToAmazon(detailUrl);
				});

            // - Complete Course Detail Url
            $(amazonItemTemplate).find('#recommendationItemDetail').html(detailUrl);
            // Append Cloned Book Template Object to Affiliate Panel
            $('#recommendationPanel').append(amazonItemTemplate);
            // Display Cloned Book Template
            $(amazonItemTemplate).removeClass('recommendation-item-hidden');
        }

        /** PRIVATE : Player Course Evaluation : Initialize Course Evaluation */

        LS360RecommendationCoursePanel.init = function(options) {
            // Initialize LS360RecommendationCoursePanel
            if (options != null) {
                // Define and Initialize Amazon Product API Integration.
                $.extend(ls360RecommendationCourseProducts.options, defaults, options);
                // Determine Keyword
                var keywordParameter = options.keyword;
                // Create Affiliate Panel
                ls360RecommendationCourseProducts.createAffiliatePanelHTML();
                // Adjust Affiliate Panel Width
                $('#recommendationPanelWrapper').css("width", $('#course-title').width());
                $('#contentWrapperReco').css('width', $('#content-frame').width());
                // Select Affiliate Panel Products                
                ls360RecommendationCourseProducts.selectAffiliateProducts(keywordParameter);
                // Assign Window Resize Method
                $(window).resize(function() {
                    $("#recommendationPanelHeader").css('width', $("#content-frame").width());
                    $("#recommendationPanelWrapper").css('width', $("#content-frame").width());
                });
            }
        };
        /** Return LS360RecommendationCoursePanel Plugin */
        return LS360RecommendationCoursePanel;
    }
    /** Assign LS360RecommendationCoursePanel to exports */
    exports.LS360RecommendationCoursePanel = LS360RecommendationCoursePanel;
} (this, jQuery);