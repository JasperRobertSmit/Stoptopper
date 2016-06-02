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

$(document).ready(function(){
    $("input").blur(function(){
        if($(this).hasClass('validation-failed') && $(this).val() != ''){
            $(this).removeClass('validation-failed');
            $(this).parent().find('.validation-message').text('');
        }
    });
});

