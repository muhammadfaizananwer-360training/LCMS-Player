var Browser = navigator.userAgent.substr(25,8);
if(navigator.userAgent.indexOf("BOIE9") != "-1"){
	//document.documentMode = "9"
}

var j360Player = {
	Browser : navigator.userAgent.substr(25,8),
	Browser2: navigator.userAgent.indexOf("Firefox") != -1,
	isAnyError: String(navigator.mimeTypes.toString()) == "undefined",
	Width: '97%',
	Height: '97%',
	PlayerCall: function(videoType,hLink,strmer,provide){
		
			//alert(videoType+hLink+strmer+provide)
			hLink = hLink.replace("https","http");
			//alert(navigator.userAgent.match(/Android/i) == "Android");
			if((/*(videoType == "mp4")||*/(navigator.userAgent.match(/Android/i) == "Android") || navigator.userAgent.match(/webOS/i) || navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || (navigator.userAgent.search("iPad") != -1) || (videoType == "swf")) && (String(provide) != "undefined")){
				hLink = hLink.replace("mp4:","");
				hLink = "http://"+strmer.split("/")[strmer.split("/").length-2]+"/vod/" + strmer.split("/")[strmer.split("/").length-1]+"/"+hLink;
				alert(hLink)
				provide = strmer = "undefined";
			}
			if((String(strmer) != "undefined") && (String(provide) != "undefined")){
//alert("con"+1);
					j360player("container").setup({
						height: j360Player.Height,
						width: j360Player.Width,
						autostart: "true",
						//image: "preview.jpg",
						modes: [
							{ 	type: "flash",
								src: "jsplayer/player.swf",
								config: {
									file: hLink,
									streamer: strmer,
									provider: provide
								}
							},
							{ type: "download" }
						]
						});
				}else{
					if(hLink.indexOf(".mp3")!="-1"){
						
						if(j360Player.isAnyError){
							//alert("con"+3);
							j360player("container").setup({
								height: 0,
								width: 0,
								autostart: "true",
								wmode: 'transparent',	
								//image: "preview.jpg",
								modes: [
									{ 	type: "flash",
										src: "jsplayer/player.swf",
										config: {
											file: hLink
										}
									},
									{ type: "download" }
								]
							});
						}else{//alert("con"+3);
							j360player("container").setup({
								height: 0,
								width: 0,
								controlbar: "hidden",
								autostart: "true",
								wmode: 'transparent',						
								//image: "preview.jpg",
								modes: [
								{ 	type: "html5",
									config: {
										file: hLink
									}
								},
								{ type: "download" }
								]
							});
						}
					}else if((j360Player.Browser == "MSIE 8.0") || (j360Player.Browser == "MSIE 7.0") || (hLink.indexOf(".swf")!= -1) || j360Player.isAnyError || ((navigator.platform.indexOf("Win") != -1)&&(hLink.indexOf(".mov") != -1)||(hLink.indexOf(".flv") != -1)||(hLink.indexOf(".f4v") != -1))){
						
//alert((j360Player.Browser == "MSIE 8.0") +" "+ (navigator.userAgent) +" "+ (j360Player.Browser2) +" "+ j360Player.isAnyError +" "+ (hLink.indexOf("swf")!="-1")); 

						j360player("container").setup({
							height: j360Player.Height,
							width: j360Player.Width,
							autostart: "true",
							//image: "preview.jpg",
							modes: [
								{ 	type: "flash",
									src: "jsplayer/player.swf",
									config: {
										file: hLink
									}
								},
								{ type: "download" }
							]
						});
					} else{
//alert("con"+3);
						//alert()
						j360player("container").setup({
							height: j360Player.Height,
							width: j360Player.Width,
							autostart: "true",						
							//image: "preview.jpg",
							modes: [
							{ 	type: "html5",
								config: {
									file: hLink
								}
							},
							{ type: "download" }
							]
						});
					}
				}
				if(videoType=="mp3"){
					j360player().play();
				}
		}
	
}
