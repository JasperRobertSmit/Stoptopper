$(document).ready(function(){

    $("#login").click(function () {
        console.log("login clicked");

        if($("#username").val() == ''){
            console.log("Username empty string");
        }

        if (is_valid()) {

            var aData = new Object();

            $("input").each(function(){
                aData[$(this).data("fieldname")] = $(this).val();
            });



            $.get("http://87.253.157.240/getteams", aData,
                function (returnData) {
                    console.log("TeamId: " + returnData[0]['TeamId']);
                }, "json");

        }
    });
});
