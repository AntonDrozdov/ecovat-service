$(document).ready(function (event) {
    //$('#contactsFeedbackFormBtn').on('click', function () {
    //    showFeedbackForm();
    //});

    $('#askQuestionIcon').on('click', function () {
        //showFeedbackForm();
        //$('#exampleModal').modal({ show:'true', backdrop: 'static', keyboard: false });
    });

    showFeedbackForm = function () {
        $("#ecovat-formfeedbackInputs-successPost").hide();
        $("#ecovat-formfeedbackInputs-failPost").hide();
        $("#ecovat-formfeedbackInputs").show();
    };

    $("#kc-feedbackForm-validatePart").submit(function (event) {
        event.preventDefault();
    });

    sendFeedbackForm = function () {

        var isValid = validateFeedbackForm();
        if (!isValid) {
            return;
        }

        //var formData = new FormData($("form").get(0));
        var formData = new FormData();        
        formData.append("Name", $('#ecovat-feedbackName').val());
        formData.append("Phone", $('#ecovat-feedbackPhone').val());
        formData.append("Email", $('#ecovat-feedbackEmail').val());
        formData.append("Message", $('#ecovat-feedbackMessage').val());

        //var request = new XMLHttpRequest();
        //request.open("POST", "FeedbackForm/FeedbackForm");
        
        //request.send(formData);

        $.ajax({
            type: 'POST',
            url: 'FeedbackForm/FeedbackForm',
            async: true,
            data: formData,
            dataType: 'json',
            contentType: false,
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            processData: false,
            success: function (response) {
                if (response.status == "success") {
                    showPostFeedbackFormResult("formfeedbackInputs-successPost");
                } else {
                    showPostFeedbackFormResult("formfeedbackInputs-failPost");
                }

            },
            error: function (response) {
                showPostFeedbackFormResult("formfeedbackInputs-failPost");
            }
        }).then(function (response) {
        });

    };

    showPostFeedbackFormResult = function (elementName) {
        $("#formfeedbackInputs").hide();

        $("#" + elementName).show();

        setTimeout(function () {
            $("#" + elementName).hide();
            $("#feedbackCloseBtn").click();
        },
            5000);
    };

    validateFeedbackForm = function () {
        $("#ecovat-feedbackForm").validate({
            rules: {
                name: {
                    required: true,
                    minlength: 2
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    digits: true,
                    minlength: 3,
                    maxlength: 20
                },
                message: {
                    required: true,
                    minlength: 10,
                    maxlength: 3000
                }

            },
            messages: {
                name: {
                    required: "*Как мы можем к Вам обращаться",
                    minlength: "*Не менее 2х символов"
                },
                email: {
                    required: "*Введите адрес электронной почты",
                    email: "*Неккоректный адрес"
                },
                phone: {
                    required: "*Введите номер телефона",
                    digits: "*Допустимы только цифры",
                    minlength: "*Не менее 3х символов",
                    maxlength: "*Не более 20ти символов"
                },
                message: {
                    required: "*Поле не может быть пустым",
                    minlength: "*Не менее 10ти символов",
                    maxlength: "*Не более 3000 символов"
                }

            }
        });

        return $("#ecovat-feedbackForm").valid();
    };
});