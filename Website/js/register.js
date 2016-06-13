$(document).ready(function () {

    $(document).keypress(function(e){
        if((e.which || e.keyCode) == 13){
            console.log("Enter pressed");
            $("#submit").click();
            //return false;
        }
    });

    $("#submit").click(function () {

        console.log("submit clicked");

        if (is_valid()) {

            var aData = new Object();

            $("input").each(function(){
                aData[$(this).data("fieldname")] = $(this).val();
            });


            sendInfo("http://87.253.157.240/register", aData, "login.html", "Registratie succesvol, u wordt naar de login pagina gestuurd.");

        }
    });

});