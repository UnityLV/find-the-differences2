mergeInto(LibraryManager.library, {  	


  	SetToLeaderboard: function(value){
  		ysdk.getLeaderboards()
	  	.then(lb => {		    
		    lb.setLeaderboardScore('TestValues', value);		    
  		});
  	},

  	ShowFullscreenAdv: function(){

  		ysdk.adv.showFullscreenAdv({
          callbacks: {
          onClose: function(wasShown) {
            console.log("feedbackSent")
	        },
	          onError: function(error) {
	            // some action on error
	        }
    	}
	})
  	},


  	ShowRewardedVideo: function(){

  		ysdk.adv.showRewardedVideo({
          callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
            myGameInstance.SendMessage('Game', 'OnRewardADOpen');
        },
          onRewarded: () => {
            console.log('Rewarded!');
            myGameInstance.SendMessage('Game', 'OnRewardADRewarded');

        },
          onClose: () => {
            console.log('Video ad closed.');
            myGameInstance.SendMessage('Game', 'OnRewardADClose');
        }, 
          onError: (e) => {
            console.log('Error while open video ad:', e);
        }
    }
	})
  	}, 	


	CheckIsAvalableForShortcut: function(){

		YaGames.init().then(ysdk => ysdk.shortcut.canShowPrompt()).then(prompt => {
			  console.log('Shortcut is allowed?:', prompt);
			  if (prompt.canShow) {
			    // Здесь можно показать кнопку для добавления ярлыка
			    myGameInstance.SendMessage('Game', 'TakeIsAvalableForShortcutInfo', prompt.canShow);
			    
			}
		});
	},


	CreateShortcut: function(){
		YaGames.init().then(ysdk => ysdk.shortcut.showPrompt()).then(result => {
			  console.log('Shortcut created?:', result);
			  if (result.outcome === 'accepted') {
			    // А здесь — начислить награду за добавление ярлыка
			    myGameInstance.SendMessage('Game', 'ShortcutHasBeenCreate');
			  }
			});
	},


	GetLanguage: function(){
		var lang = ysdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang,buffer,bufferSize);
		return buffer;
	},


  	SetPlayerPersonalInformation: function () {

  		myGameInstance.SendMessage('Game', 'SetName', player.getName());
  		myGameInstance.SendMessage('Game', 'SetPhoto', player.getPhoto("medium"));    	
  	},	

  	RateForGame: function() {

		ysdk.feedback.canReview()
	        .then(({ value, reason }) => {
	            if (value) {
	                ysdk.feedback.requestReview()
	                    .then(({ feedbackSent }) => {
	                        console.log(feedbackSent);
	                    })
	            } else {
	                console.log(reason)
	            }
	        })
  	},

  	

  	LoadStatsExtern: function () {
  		player.getData().then(_data => {
  			const myJSON = JSON.stringify(_data);
  			myGameInstance.SendMessage('Game', 'SetPlayerStats', myJSON);
  		});
    	
  	},	

  	SaveStatsExtern: function(stats){

  		const statsDataJson = UTF8ToString(stats);
		var statsData = JSON.parse(statsDataJson);
  		player.setData(statsData);  
  		console.log(statsData);

  		
  		//const myJSONStats = JSON.stringify(statsData);
		//myGameInstance.SendMessage('Game', 'SetPlayerStats', myJSONStats);

  	},

  	


  	ResetStats: function(){
  		player.setStats({});
  	},
























  });