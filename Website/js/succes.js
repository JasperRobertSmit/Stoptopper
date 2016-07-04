var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function generateUUID(){
    var d = new Date().getTime();
    if(window.performance && typeof window.performance.now === "function"){
        d += performance.now(); //use high-precision timer if available
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = (d + Math.random()*16)%16 | 0;
        d = Math.floor(d/16);
        return (c=='x' ? r : (r&0x3|0x8)).toString(16);
    });
    return uuid;
}

var veldid = 0;

// document ready function 
$(document).ready(function(){
	$(".PloegContainer").hide();
	var access_token = getUrlParameter("access_token");
	var startdatum = new Date();  // for example
	var einddatum  = new Date();

	einddatum.setDate(startdatum.getDate() + 5);
	
	// the number of .net ticks at the unix epoch
	var epochTicks = 621355968000000000;
	
	// there are 10000 .net ticks per millisecond
	var ticksPerMillisecond = 10000;
	
	// calculate the total number of .net ticks for your date
	var startDatumTicks = epochTicks + (startdatum.getTime() * ticksPerMillisecond);

	var eindDatumTicks = epochTicks + (einddatum.getTime() * ticksPerMillisecond);

	console.log("startDatumTicks: " + startDatumTicks);
	console.log("eindDatumTicks: " + eindDatumTicks);

	// add event button
	$(".addEventButton").click(function(){
		var afkorting = $("#afkorting").val();
		var guid = generateUUID();
		var afstand = $("#afstand").val();
		var naam = $("#naam").val();
		var plaats = $("#plaats").val();
		var orgid = $("#orgid").val();

		var aData = {
			"afkorting"  		: afkorting,
			"einddatum"  		: eindDatumTicks,
			"startdatum" 		: startDatumTicks,
			"guid" 		 		: guid,
			"afstand"    		: afstand,
			"naam"       		: naam,
			"plaats"     		: plaats,
			"organisatorid"     : orgid,
			"token" 			: access_token
		}

		// add evenement
		$.post("http://87.253.157.240/event", aData, function(data){
			var url = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + guid;
			var img = "<img src=" + url + " alt='QR image here' >";
			$(".imageContainer").html(img);
			
			var eventid = JSON.parse(data).Data;
			var aDataBlok = {
				"starttijd"  		: "633896886277130000",
				"eindtijd"  		: "633896886277130000",
				"evenementid" 		: eventid,
				"nummer" 		 	: "1",
				"token"    			: access_token
			}
			
			// add blok
			$.post("http://87.253.157.240/blok", aDataBlok, function(data2){
				var blokid = data2.Data;
				var aDataVeld = {
					"afkorting"  		: "TST",
					"naam"  		: "TestVeld",
					"blokid" 		: blokid,
					"token"    			: access_token
				}
			
				// add veld
				$.post("http://87.253.157.240/veld", aDataVeld, function(data){
					veldid = data.Data;
					$(".PloegContainer").show();
					
					setInterval(function(){
						// pollen tijden
						$.get("http://87.253.157.240/ploeg/all/veld/"+veldid+"/"+access_token, function(data){
							
							
							// reload ploegen
							var ploegenHtml = "";
							$.each(JSON.parse(JSON.parse(data).Data), function(i, item){
								$.ajax({
									url: "http://87.253.157.240/tijd/all/ploeg/"+item.Id+"/"+access_token, 
									success: function(data){
										var bootnaam = item.Bootnaam;
										var rugnummer = item.Rugnummer;
										$.each(JSON.parse(JSON.parse(data).Data), function(i, item){
											var tijd = item.Tijd1;
											ploegenHtml += "<tr><td>"+bootnaam+"</td><td>"+rugnummer+"</td><td>"+tijd.replace("T", " ")+"</td></tr>";
										});
									},
									async: false
								});
							});
							console.log(ploegenHtml);
							$(".TijdenOverzichtTable tbody").html(ploegenHtml);
						});
					}, 1000);
				}, "json");
			}, "json");
		});
	});
	
	// ad ploeg event
	$(".addPloegButton").click(function(){
		var bootnaam = $("#bootnaam").val();
		var naam = $("#naam").val();
		var rugnummer = $("#rugnummer").val();
		var aDataPloeg = {
			"bootnaam"  		: bootnaam,
			"naam"  			: naam,
			"rugnummer" 		: rugnummer,
			"veldid" 		 	: veldid,
			"verenigingsid"    	: "1",
			"token"       		: access_token
		}

		// add ploeg
		$.post("http://87.253.157.240/ploeg", aDataPloeg, function(data){
		
			// ploegen ophalen
			$.get("http://87.253.157.240/ploeg/all/veld/"+veldid+"/"+access_token, function(data){

					// reload ploegen
					var ploegenHtml = "";
					console.log(data);
					console.log(JSON.parse(data).Data);
					$.each(JSON.parse(JSON.parse(data).Data), function(i, item){
					console.log(item);
						var bootnaam = item.Bootnaam;
						var rugnummer = item.Rugnummer;
						ploegenHtml += "<tr><td>"+bootnaam+"</td><td>"+rugnummer+"</td></tr>";
					});
					$(".PloegenOverzichtTable tbody").html(ploegenHtml);
				
			});
		}, "json");
	});
});