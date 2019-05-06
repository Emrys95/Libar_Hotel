let roomForm = document.getElementById("room");

if (roomForm) {
    console.log(roomForm);
    roomForm.onsubmit = function (form) {

        let floorInput = form.target["Room.Floor"];
        let isValid = true;


        if (!floorInput.value) {
            validationMessage("Morate uneti broj sprata.", floorInput);
            isValid = false;
        }

        else if (!/^\d{1,1}$/.test(floorInput.value)) {
            validationMessage("Broj sprata mora da bude jednocifren broj", floorInput)
            isValid = false;
        }

        else {
            let floor = parseInt(floorInput.value);

            if (floor < 1) {
                validationMessage("Broj sprata ne može biti manji od 1", floorInput);
                isValid = false;
            }

        }

        return isValid;
    };
}