$(document).ready(function(){


    $(document).keypress(function(e){
        if((e.which || e.keyCode) == 13){
            console.log("Enter pressed");
            $("#login").click();
            //return false;
        }
    });

    $("#login").click(function () {
        console.log("login clicked");

        if (is_valid()) {

            var aData = new Object();

            $("input").each(function(){
                aData[$(this).data("fieldname")] = $(this).val();
            });



            aData['password'] = Sha256.hash(aData['password']);

            var oUsername = new Object();
            oUsername['username'] = aData['username'];
            validateUsername(oUsername);


            sendInfo("http://87.253.157.240/login", aData, "success.html", "Login succesvol u wordt geredirect");

        }
    });
});
