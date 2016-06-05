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

            var oResult = sendInfo("http://87.253.157.240/login", aData);

            console.log("oResult" + oResult);
        }
    });


    function myCallback(result){

    }

    function foo(callback){
        $.post("asdsd", aData, function(data){
            myCallback(data);
        })
    }
});
