"use strict"

document.addEventListener("DOMContentLoaded", function () {
    const serverError = document.querySelector('#js-server-error').value;
    if (serverError != '') {
        alert(serverError);
    }

    const form = document.getElementById('form');
    form.addEventListener('submit', formSend);

    async function formSend(e) {
        let error = formValidate(form);
        if (error != '') {
            e.preventDefault();
            alert(error);
        }
    }

    function formValidate(form) {
        let error = '';
        let formReq = document.querySelectorAll('._req');

        for (let index = 0; index < formReq.length; index++) {
            const input = formReq[index];
            formRemoveError(input);

            if (input.value === '') {
                formAddError(input);
                error = "Заполните все поля!";
            }
            else if (input.classList.contains('_email')) {
                if (!emailIsCorrect(input)) {
                    formAddError(input);
                    error = "Почта некорректна!";
                }
            }
            else if (input.classList.contains('_password')) {
                //if ()
                if (!passwordIsCorrect(input)) {
                    formAddError(input);
                    error = "Пароль должен содержать не меньше 10 символов: цифры, заглавные и строчные латинские буквы!";
                }
            }
        }
        return error;
    }

    function formAddError(input) {
        input.parentElement.classList.add('_error');
        input.classList.add('_error');
    }
    function formRemoveError(input) {
        input.parentElement.classList.remove('_error');
        input.classList.remove('_error');
    }
    function emailIsCorrect(input) {
        return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,8})+$/.test(input.value);
    }
    function passwordIsCorrect(input) {
        return input.value.length >= 10 && /(?=.*\d)(?=.*[a-z])/i.test(input.value);
    }
});