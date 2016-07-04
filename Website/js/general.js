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

function updateFields(oResult, redirectUrl, redirectMessage) {

    if (!oResult['Success']) {
        //Er is een fout
        if (oResult['ErrorField'] != null) {
            var errorfield = '#' + oResult['ErrorField'];
            errorfield = errorfield.toLowerCase();

            $(errorfield).addClass('validation-failed');
            $(errorfield).parent().find('.validation-message').text(oResult['Message']);
        }
    } else {
        if (redirectUrl != null) {
            console.log("Redirected: " + redirectUrl);


            //Als er een redirect bericht is toon het
            if (redirectMessage != null) {
                $(".msg").text(redirectMessage);
            }

            redirectUrl = redirectUrl + "?access_token=" + oResult['Data'];


                $(location).attr('href', redirectUrl);


        }
    }
}


function sendInfo(url, aData, redirectUrl, redirectMessage) {
    $.post(url, aData, function (returnData) {
        updateFields(returnData, redirectUrl, redirectMessage);
    }, "json");
}

function validateUsername(aUsername) {

    $.post("http://87.253.157.240/ValidateUsername", aUsername, function (response) {
        if (!response['Success']) {
            var oUsername = $("#username");
            oUsername.addClass('validation-failed');
            oUsername.parent().find('.validation-message').text(response['Message']);
        }
    }, "json");
}

$(document).ready(function () {
    $("input").blur(function () {
        if ($(this).hasClass('validation-failed') && $(this).val() != '') {
            $(this).removeClass('validation-failed');
            $(this).parent().find('.validation-message').text('');
        }
    });


});
