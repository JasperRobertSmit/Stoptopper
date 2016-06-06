$(document).ready(function(){

    $("#login").click(function () {
        console.log("login clicked");

        if (is_valid()) {

            var aData = new Object();

            $("input").each(function(){
                aData[$(this).data("fieldname")] = $(this).val();
            });

            sendInfo("http://87.253.157.240/login", aData);

        }
    });
});
