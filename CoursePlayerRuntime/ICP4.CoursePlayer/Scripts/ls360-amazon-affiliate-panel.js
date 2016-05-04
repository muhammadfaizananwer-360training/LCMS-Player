/**
 * ...........................................................................................
 * LS360 Amazon Affiliate Product API: jQuery Plugin Author: Niles White
 * December 2012
 * ...........................................................................................
 */

//LCMS-10329
function f_clientHeight() {
    return $(window).height();
}

!function(exports, $, undefined) {
    /** LS360 Amazon Affiliate Product API Plugin */
    var LS360AmazonAffiliatePanel = function() {
        /** LS360 Amazon Affiliate Product API : Plugin Variables */
        var ls360AmazonProducts = {}, // private API
		LS360AmazonAffiliatePanel = {}, // public API
        /** LS360 Amazon Affiliate Product API : Default Variables */
        //For LCMS-11508
		defaults = {
		    amazonAffiliateProducts: "",
		    affiliatePanelWidth: 800,
		    affiliatePanelHeight: 140,
		    affiliateItemWidth: 302,
		    defaultDisplay: true,
		    affiliatePanelHTML: '<!-- LS360 Amazon Affiliate Product API : Affiliate Panel --><div id="affiliatePanelHeader" class="affiliate-panel-header" ></div><div id="affiliatePanelWrapper" class="affiliate-panel-wrapper" style="display:none;"><div id="affiliatePanel" class="affiliate-panel affiliate-panel-hidden"><div id="loadingGraphic" class="loading-graphic" style="display:none;">Loading Graphic</div></div></div><!-- End Amazon Product API : Affiliate Panel -->',
		    affiliatePanelWSURL: "http://10.0.100.250:8080/lms/aws_product.do?jsoncallback=?&keyword="//LCMS-10392		    

		};
        /**
        * PUBLIC : LS360 Amazon Affiliate Product API : Public Visibility Value
        */
        LS360AmazonAffiliatePanel.visible = false;

        /**
        * PUBLIC : LS360 Amazon Affiliate Product API : Hide Affiliate Panel
        */
        LS360AmazonAffiliatePanel.hideAffiliatePanel = function() {
            ls360AmazonProducts.hideAffiliatePanel();
        }

        /**
        * PUBLIC : LS360 Amazon Affiliate Product API : Show Affiliate Panel
        */
        LS360AmazonAffiliatePanel.showAffiliatePanel = function() {
            ls360AmazonProducts.showAffiliatePanel();
        }

        /** PRIVATE : LS360 Amazon Affiliate Product API: Options Variables */
        ls360AmazonProducts.options = {};

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Select Product Results
        */

        function CheckingAmazonPanel() {
            $('#IcoAmazonAffiliatePanel').show();             
		    $('#IcoAmazonAffiliatePanelDs').hide();
		     
            if ($("#affiliatePanelWrapper").is(":visible")) {
            
                StopcheckingAmazonPanel();
            }
            else {
                $('#affiliatePanelHeader').hide();
                ls360AmazonProducts.displayNoRecommendedMessage();
                StopcheckingAmazonPanel();
                ls360AmazonProducts.hideAffiliatePanel();
                IsAmazonAffiliatePanel=false;
		        bIsContentResize=true;
            }
        }

        function StopcheckingAmazonPanel() {
            clearInterval(myPanel);
            $('#IcoAmazonAffiliatePanel').show();
		    $('#IcoAmazonAffiliatePanelDs').hide();
        }

        ls360AmazonProducts.selectAffiliateProducts = function(keywordParameter) {
            // Retrieve JSON Object from LMS AWS Integration Controller
            try {
                myPanel = setInterval(function() { CheckingAmazonPanel() }, 10000);

                $.getJSON(ls360AmazonProducts.options.affiliatePanelWSURL + keywordParameter, //LCMS-10392
				    function(data) {
				        if (data != null && data.affiliateItems && data.affiliateItems.length > 0) {
				            // Assign Product Results to Plugins Private Variable
				            StopcheckingAmazonPanel();
				            ls360AmazonProducts.amazonAffiliateProducts = data.affiliateItems;
				            // Load Affiliate Panel Products
				            ls360AmazonProducts.addAffiliatePanelItems();
				            // Display Affiliate Panel
				            ls360AmazonProducts.showAffiliatePanel();
				            // Add Toggle Affiliate Panel Click Event
				            ls360AmazonProducts.addAffiliatePanelClickEvent();
				            // Set Default Display State
				            if (ls360AmazonProducts.options.defaultDisplay) {
				                // Set the Affiliate Panel Wrapper to the width of the Course Players Course Title
				                $('#affiliatePanelHeader').show();
				                $('#affiliatePanelWrapper').css("width", $('#course-title').width());
				                $('#contentWrapper').css('width', $('#content-frame').width());
				                $('#affiliatePanel').removeClass('affiliate-panel-hidden');
				            }
				        } else {//Don't show affiliate panel if no book is returned
				            ls360AmazonProducts.hideAffiliatePanel();
				            $('#contentWrapper').hide();
				        }

				    });

            }
            catch (Error) {
            }

        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Create Affiliate Panel
        */
        ls360AmazonProducts.addAffiliatePanelItems = function() {
            // Retrieve Affiliate Products from Plugin Object            
            var affiliateProducts = ls360AmazonProducts.amazonAffiliateProducts;
            // Load Each of the Affiliate Panel Books
            $.each(affiliateProducts, function(index, value) {
                ls360AmazonProducts.buildAffiliatePanelBook(value, index);
            });

            // Set Affiliate Panel Item Width
            var affiliateItemWidth = (affiliateProducts.length * ls360AmazonProducts.options.affiliateItemWidth);
            $('#affiliatePanel').css("width", affiliateItemWidth + "px");
            // Add Scrolling Elements
            $('#affiliatePanelWrapper').css('overflow', 'scroll');
            $('#affiliatePanelWrapper').css('overflow-y', 'hidden');
            $('#affiliatePanelWrapper').css('overflow-x', 'scroll');
            // Remove Loading Graphic.
            $('#affiliatePanel').find("#loadingGraphic").remove();
        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Show Affiliate Panel
        */
        ls360AmazonProducts.showAffiliatePanel = function() {
            // -- N3Player Specific -- 
            // Resize Course Player Content
            $("#content-frame").height(f_clientHeight() - 222 - 154);
            // Show Affiliate Panel
            $('#affiliatePanelWrapper').css('display', 'block');
            // Set Affiliate Panel Visibility value
            LS360AmazonAffiliatePanel.visible = true;
            // Set Affiliate Panel Header Text
            $('#affiliatePanelHeader').text("");
            //LCMS-10329
            content_resize();
        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Hide Affiliate Panel
        */
        ls360AmazonProducts.hideAffiliatePanel = function() {
            // -- N3Player Specific -- 
            // Resize Course Player Content
            $("#content-frame").height(f_clientHeight() - 150);
            // Hide Affiliate Panel
            $('#affiliatePanelWrapper').css('display', "none");
            // Set Affiliate Panel Visibility value
            LS360AmazonAffiliatePanel.visible = false;
            // Set Affiliate Panel Header Text
            $('#affiliatePanelHeader').text(
					"");
            //LCMS-10329
            content_resize();
        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Build Affiliate Panel
        * HTML
        */
        ls360AmazonProducts.createAffiliatePanelHTML = function() {
            // Append Affiliate Panel to HTML            
            $('#contentWrapper').append(ls360AmazonProducts.options.affiliatePanelHTML);
        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Add Toggle Button
        * Click Event
        */
        ls360AmazonProducts.addAffiliatePanelClickEvent = function() {
            $('#affiliatePanelHeader').bind("click", function() {
                //                if (!LS360AmazonAffiliatePanel.visible) {
                //                    ls360AmazonProducts.showAffiliatePanel();
                //                } else {
                //                    ls360AmazonProducts.hideAffiliatePanel();
                //                }
            });
        }

        //LCMS-10392
        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Handle redirect to Amazon site
        * Click Event
        */
        ls360AmazonProducts.redirectToAmazon = function(detailUrl) {
            if ($("#affiliatPanelRedirectDialog").length == 0) {
                var htmlRedirectConfirmaton = '<div id="affiliatPanelRedirectDialog" class="affiliate-panel-redirect-dialog">' +
                                                    '<h2>Question</h2>' + //LCMS-11488
                                                    '<div class="ap-redirect-dialog-message">' +
                                                        'The Amazon product you selected will open in a new window. Do you want to continue?' +
                                                    '</div>' +
                                                    '<div class="ap-redirect-dialog-buttons">' +
                                                        '<button id="affiliatePanelCancelButton" class="button">Cancel</button>&nbsp;' + //Added &nbsp; for the bug LCMS-13827
                                                        '<button id="affiliatePanelOkButton" class="button">OK</button>' +
                                                    '</div>' +
                                                '</div>';

                $(htmlRedirectConfirmaton).appendTo('body');
                $("#affiliatePanelCancelButton").bind("click", function() {
                    $("#affiliatPanelRedirectDialog").fadeOut("slow");
                    $("#overlay").fadeOut("slow");
                });
            }
            $("#affiliatePanelOkButton").bind("click.ap", function() {
                $("#affiliatePanelOkButton").unbind("click.ap");
                $("#affiliatPanelRedirectDialog").fadeOut("slow");
                $("#overlay").fadeOut("slow");
                //window.open(detailUrl, "Amazon : 360 Link");
                window.open(detailUrl);
            });

            $("#overlay").css({ "opacity": "0.7" });
            $("#overlay").fadeIn("slow");
            $("#affiliatPanelRedirectDialog").fadeIn("slow");
        }


        //LCMS-11592
        /**
        * PRIVATE : 
        * Message display no recommended found
        */
        ls360AmazonProducts.displayNoRecommendedMessage = function() {
            if ($("#affiliatPanelRedirectDialog").length == 0) {
                var htmlRedirectConfirmaton = '<div id="affiliatPanelRedirectDialog" class="affiliate-panel-redirect-dialog">' +
                                                    '<h2>' + InformationText + '</h2>' + //LCMS-11488
                                                    '<div class="ap-redirect-dialog-message">' +
                                                        NoRecomendedBookText +
                                                    '</div>' +
                                                    '<div class="ap-redirect-dialog-buttons">' +
                                                        '<button id="affiliatePanelCancelButton" class="button">' + ok_text + '</button>' +
                                                    '</div>' +
                                                '</div>';

                $(htmlRedirectConfirmaton).appendTo('body');
                $("#affiliatePanelCancelButton").bind("click", function() {
                    $("#affiliatPanelRedirectDialog").fadeOut("slow");
                    $("#overlay").fadeOut("slow");
                });
            }

            $("#overlay").css({ "opacity": "0.7" });
            $("#overlay").fadeIn("slow");
            $("#affiliatPanelRedirectDialog").fadeIn("slow");
        }

        /**
        * PRIVATE : LS360 Amazon Affiliate Product API : Build Product Item
        */
        ls360AmazonProducts.buildAffiliatePanelBook = function(affiliateItemJSON, index) {
            // Build and assign product to results container.
            // Assign Values for Book Template
            var title = affiliateItemJSON.title;
            var author = affiliateItemJSON.author;
            var rating = affiliateItemJSON.rating;
            var price = affiliateItemJSON.price;
            var imageUrl = affiliateItemJSON.bookImageUrl;
            var detailUrl = affiliateItemJSON.redirectToUrl;
            var affiliateItemId = "affiliateItem" + index;
            // Clone Book Template Object
            // TODO: Replace with Object Creation
            var amazonItemTemplate = $('#affiliateItemTemplate').clone();
            // Alter Cloned Book Object
            // - Complete Book Id
            $(amazonItemTemplate).attr('id', affiliateItemId);
            // Adjust Affiliate Item Title Length
            var truncLen = 35;
            var newTitle = "";
            if (title.length > truncLen) {
                newTitle = title.substring(0, truncLen);
                newTitle = newTitle.replace(/w+$/, '');
                if (title != newTitle) {
                    newTitle = newTitle + "...";
                    title = newTitle;
                }
            }

            // - Complete Book Title
            $(amazonItemTemplate).find('#affiliateItemTitle').html(title);
            $(amazonItemTemplate).find('#affiliateItemTitle').bind("click", function() {
                //LCMS-10392
                ls360AmazonProducts.redirectToAmazon(detailUrl);
            });
            $(amazonItemTemplate).find('#affiliateItemTitle').css("cursor", "pointer"); //LCMS-11483
            // - Complete Book Image
            // if (imageUrl.split('.')[4] == "jpg") {

            //10392, re Niles White Comment on 30/Jan/13 01:13 PM
            var defaultImageUrl = 'images/ico_default_book.png';
            if (!imageUrl || imageUrl.length == 0) {
                imageUrl = defaultImageUrl;
            }
            
            $(amazonItemTemplate).find('#imgAffiliateItemMedium').
					error(function() {
                        //For LCMS-11651, version 4.15.2
                        //$(this).attr('src', defaultImageUrl);
                        if (!imageUrl || imageUrl.length == 0) {
                            $(this).attr('src', defaultImageUrl);
                        }
                        else {
                            $(this).attr('src', imageUrl);
                        }
                        //End LCMS-11651
					}).
					attr('src', imageUrl);
            $(amazonItemTemplate).find('#imgAffiliateItemMedium').attr('id',
					"imgAffiliateItemMedium" + index);
            // }
            // - Complete Book Author
            $(amazonItemTemplate).find('#affiliateItemAuthor').html(author);
            // - Complete Book Price
            $(amazonItemTemplate).find('#affiliateItemPrice').html(price);
            // - Complete Book Image Link
            $(amazonItemTemplate).find('#imgAffiliateItemMedium' + index).attr('url', detailUrl); //LCMS-10392 for debugging
            $(amazonItemTemplate).find('#imgAffiliateItemMedium' + index).bind("click",
				function() {
				    //LCMS-10392
				    ls360AmazonProducts.redirectToAmazon(detailUrl);
				});
            // - Complete Book Detail Url
            $(amazonItemTemplate).find('#affiliateItemDetail').html(detailUrl);
            // Append Cloned Book Template Object to Affiliate Panel
            $('#affiliatePanel').append(amazonItemTemplate);
            // Display Cloned Book Template
            $(amazonItemTemplate).removeClass('affiliate-item-hidden');
        }

        /** PRIVATE : Player Course Evaluation : Initialize Course Evaluation */

        LS360AmazonAffiliatePanel.init = function(options) {
            // Initialize LS360AmazonAffiliatePanel
            if (options != null) {
                // Define and Initialize Amazon Product API Integration.
                $.extend(ls360AmazonProducts.options, defaults, options);
                // Determine Keyword
                var keywordParameter = options.keyword;
                // Create Affiliate Panel
                ls360AmazonProducts.createAffiliatePanelHTML();
                // Adjust Affiliate Panel Width
                $('#affiliatePanelWrapper').css("width", $('#course-title').width());
                $('#contentWrapper').css('width', $('#content-frame').width());
                // Select Affiliate Panel Products                
                ls360AmazonProducts.selectAffiliateProducts(keywordParameter);
                // Assign Window Resize Method
                $(window).resize(function() {
                    $("#affiliatePanelHeader").css('width', $("#content-frame").width());
                    $("#affiliatePanelWrapper").css('width', $("#content-frame").width());
                });
            }
        };
        /** Return LS360AmazonAffiliatePanel Plugin */
        return LS360AmazonAffiliatePanel;
    }
    /** Assign LS360AmazonAffiliatePanel to exports */
    exports.LS360AmazonAffiliatePanel = LS360AmazonAffiliatePanel;
} (this, jQuery);