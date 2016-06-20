var Browser = navigator.userAgent.substr(25,8);
if(navigator.userAgent.indexOf("BOIE9") !== "-1"){
	//document.documentMode = "9"
}

var j360Player = {
	Browser : navigator.userAgent.substr(25,8),
	Browser2: navigator.userAgent.indexOf("Firefox") !== -1,
	isAnyError: String(navigator.mimeTypes.toString()) === "undefined",
	Width: '100%',
	Height: '100%',
	PlayerCall: function(videoType,hLink,strmer,provide){
			
			hLink = hLink.replace("https","http");			
			hLink = hLink.replace("mp4:","");
			
			if(!(j360Player.isAnyError)&&(String(strmer) !== "undefined") && (String(provide) !== "undefined")){
			hLink = "http://"+strmer.split("/")[strmer.split("/").length-2]+"/vod/" + strmer.split("/")[strmer.split("/").length-1]+"/"+hLink;
				provide = strmer = "undefined";
				
			}
			/*if(videoType=="youtube"){
				j360player("container").setup({
						height: j360Player.Height,
						width: j360Player.Width,					
						file:"https://www.youtube.com/watch?v=fT28TRORLwE"
					});
			}else */if((String(strmer) !== "undefined") && (String(provide) !== "undefined")){
					j360player("container").setup({
						height: j360Player.Height,
						width: j360Player.Width,
						autostart: "true",
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
			}else if(j360Player.isAnyError||(videoType == "swf") || (videoType == "flv")/*|| (videoType == "mp4")*/){
				j360player("container").setup({
					height: j360Player.Height,
					width: j360Player.Width,
					autostart: "true",
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
			}else {
				j360player("container").setup({
					height: j360Player.Height,
					width: j360Player.Width,
					autostart: "true",
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
				
				if(videoType==="mp3"){
					j360player().play();
				}
		}
	
}
