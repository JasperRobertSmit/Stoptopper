function is_valid() {
    var bValidationResult = true;

    $("input").each(function () {
        if ($(this).hasClass('required') && $(this).val() == '') {
            $(this).addClass('validation-failed');
            $(this).parent().find('.validation-message').text('Dit veld is verplicht');
            bValidationResult = false;
        }
    });

    return bValidationResult;
}

function updateFields(oResult, redirectUrl, redirectMessage){

    if(!oResult['Success']){
        //Er is een fout
        if(oResult['Message'] == "Username already exists!"){
            var username = $("#username");
            username.addClass('validation-failed');
            username.parent().find('.validation-message').text(oResult['Message']);
        }

        if(oResult['Message'] == "Invalid credentials!"){
            var usernameAndPassword = $("#username, #password");
            usernameAndPassword.addClass('validation-failed');
            usernameAndPassword.parent().find('.validation-message').text(oResult['Message']);
        }
    }else{
        if(redirectUrl != null){
            console.log("Redirected: " + redirectUrl);


            //Als er een redirect bericht is toon het
            if(redirectMessage != null){
                $(".msg").text(redirectMessage);    
            }
            

            setTimeout(function(){
                $(location).attr('href', redirectUrl);
            }, 4000);
            
        }    
    }
}



function sendInfo(url, aData, redirectUrl, redirectMessage){
    $.post(url, aData, function(returnData){
        updateFields(returnData, redirectUrl, redirectMessage);
    }, "json");
}


$(document).ready(function(){
    $("input").blur(function(){
        if($(this).hasClass('validation-failed') && $(this).val() != ''){
            $(this).removeClass('validation-failed');
            $(this).parent().find('.validation-message').text('');
        }
    });
});

