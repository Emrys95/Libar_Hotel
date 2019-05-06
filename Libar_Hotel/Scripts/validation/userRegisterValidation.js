let userRegisterForm = document.getElementById("user-register");

if (userRegisterForm) {

    userRegisterForm.onsubmit = function (form) {
        let firstNameInput = form.target["FirstName"];
        let lastNameInput = form.target["LastName"];
        let isValid = true;

        if (!firstNameInput.value) {
            validationMessage("Morate uneti ime.", firstNameInput);
            isValid = false;

        }
        //^([A-Z]{1}[a-z]{2,20})( [A-Z]{1}[a-z]{2,20})?$
       
        else if (!/^([A-Z]{1}[a-zA]{2,20})( [A-Z]{1}[a-z]{2,20})?$/.test(firstNameInput.value)) {
            validationMessage("Ime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.", firstNameInput);
            isValid = false;
        }

        if (!lastNameInput.value) {
            validationMessage("Morate uneti prezime.", lastNameInput);
            isValid = false;
        }

        else if (!/^([A-Z]{1}[a-zA]{2,20})( [A-Z]{1}[a-z]{2,20})?$/.test(lastNameInput.value)) {
            validationMessage("Prezime mora da počinje velikim slovom i mora minimum da ima 3 slova do 20 slova.", lastNameInput);
            isValid = false;
        }


        return isValid;

        
    }
}