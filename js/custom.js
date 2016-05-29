$(document).ready(function(){
	$("#submit").click(function(){
		console.log("submit clicked");


        var aData = new Object();

        $("input").each(function(){
            //Waarschijnlijk niet handig om uit te gaan van ID op html elementen
           aData[$(this).attr("id")] = $(this).val();
        });

        console.log(aData);

		var sUsername = aData['username'];
		var sPassword = aData['password'];


        if(sUsername != '' && sPassword != ''){

            $.post("index.php", aData,
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