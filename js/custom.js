$(document).ready(function(){
	$("#submit").click(function(){
		console.log("submit clicked");

		var sUsername = $("#username").val();
		var sPassword = $("#password").val();


        if(sUsername != '' && sPassword != ''){
            console.log(sUsername + sPassword);
            var data = { username: sUsername, password: sPassword };

            $.post("index.php", data,
                function(returnData){
                    console.log(returnData['password_matches']);
                    if(returnData['password_matches']){
                        $(".msg").css('color', 'red');
                    }else{
                        $(".msg").css('color', 'pink');
                    }
                }, "json" );
        }


	})


		
});