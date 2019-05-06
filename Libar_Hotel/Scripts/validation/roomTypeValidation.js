let roomType = document.getElementById("room-type");


if (roomType) {

    roomType.onsubmit = function (form) {

        let nameInput = form.target['Name'];
        //provera ako ima hiddenPolje zato sto vraca niz od dva elementa pa uzmi vrednost drugog elementa koji sadrzi value property
        if (nameInput.length > 1) {
            nameInput = nameInput[1];
        }

        console.log(nameInput.value);

        let priceInput = form.target['Price'];


        let isValid = true;

        //^([A-Z]{1}[a-z ]{3,30})(-[a-z ]{3,30})?$

        if (!nameInput.value) {
            validationMessage("Morate uneti naziv tipa sobe.", nameInput)
            isValid = false;
        }
        //Naziv usluge mora poceti sa velikim slovom i mora imati min 3 karaktera, max 20 karaktera
        else if (!/^([A-Z]{1}[a-z ]{3,30})(-[a-z ]{3,30})?$/.test(nameInput.value)) {
            validationMessage("Naziv tipa sobe mora početi velikim slovom i mora sadržati od 3 do 30 slova.", nameInput);
            isValid = false;
        }


        if (!priceInput.value) {
            validationMessage("Morate uneti cenu tipa sobe", priceInput)
            isValid = false;
        }

        // \d{2,3}
        else if (!/^\d{2,3}$/.test(priceInput.value)) {
            validationMessage("Cena tipa sobe mora biti broj i mora sadržati od 2 do 3 cifre.", priceInput);
            isValid = false;
        }

        else {
            let price = parseFloat(priceInput.value);

            if (price < 100 || price >= 1000) {
                validationMessage("Cena tipa sobe mora biti u rasponu od 100 do 999.", priceInput);
                isValid = false;

            }
        }



        return isValid;
    };
}




