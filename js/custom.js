$(document).ready(function(){
	$("#submit").click(function(){
		console.log("submit clicked");


        // var aData = new Object();

        // $("input").each(function(){
        //    aData[$(this).data("id") = $(this).val();
        // });
        var sUsername = "tralalalal";
        var sPassword = "tralalalal";

        // console.log(aData);

		// var sUsername = aData['username'];
		// var sPassword = aData['password'];


        if(sUsername != '' && sPassword != ''){

            $.get("http://87.253.157.240/getmatches", sPassword,
                function(returnData){
                    console.log(returnData['password_matches']);
                    if(returnData['password_matches']){
                        $(".msg").css('color', 'red');
                    }else{
                        $(".msg").css('color', 'pink');
                    }
                }, "json" );
        } else{

            $("input").each(function(){
                if($(this).val() == ''){
                    $(this).addClass("validation-failed");
                }
            });

            setTimeout(function(){
                $("input").removeClass("validation-failed");
            }, 4000)

        }


	})


		
});