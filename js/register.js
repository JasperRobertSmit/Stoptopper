$(document).ready(function () {
    $("#submit").click(function () {
        console.log("submit clicked");

        if($("#username").val() == ''){
            console.log("Username empty string");
        }

        if (is_valid()) {

            var aData = new Object();

            $("input").each(function(){
                aData[$(this).data("fieldname")] = $(this).val();
            });

            console.log("aData:" + aData);

            sendInfo("http://87.253.157.240/register", aData);

            //$.post("http://87.253.157.240/register", aData,
            //    function (returnData) {
            //        console.log("Success: " + returnData['Success']);
            //    }, "json");

        }
    });

});