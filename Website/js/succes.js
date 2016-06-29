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


$(document).ready(function(){

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

	$("#send").click(function(){
	
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



		$.post("http://87.253.157.240/event", aData, function(data){
			console.log(data);
			var url = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + guid;
			var img = "<img src=" + url + " alt='QR image here' >";
			$("main").prepend(img);
		}, "json");
	});


	//var url = "https://chart.googleapis.com/chart?cht=qr&chs=20x20&chl=" + qrCode;

});