let serviceTypeForm = document.getElementById("service-type");


if (serviceTypeForm) {
    
    serviceTypeForm.onsubmit = function (form) {

        let nameInput = form.target['Name'];
        //provera ako ima hiddenPolje zato sto vraca niz od dva elementa pa uzmi vrednost drugog elementa koji sadrzi value property
        if (nameInput.length > 1)
        {
            nameInput = nameInput[1];
        }

        console.log(nameInput.value);

        let priceInput = form.target['Price'];

        
        let isValid = true;

        //^[A-Z]{1}[a-z ]{3,20}$

        if (!nameInput.value) {
            validationMessage("Morate uneti naziv usluge", nameInput)
            isValid = false;
        }
        //Naziv usluge mora poceti sa velikim slovom i mora imati min 3 karaktera, max 20 karaktera
        else if (!/^[A-Z]{1}[a-z ]{3,20}$/.test(nameInput.value)) {
            validationMessage("Naziv usluge mora početi velikim slovom i mora sadržati od 3 do 20 slova", nameInput);
            isValid = false;
        }


        if (!priceInput.value) {
            validationMessage("Morate uneti cenu usluge", priceInput)
            isValid = false;
        }

        // \d{2,3}
        else if (!/^\d{2,3}$/.test(priceInput.value)) {
            validationMessage("Cena usluge mora biti dvocifren ili trocifren broj, manji od 1000", priceInput);
            isValid = false;
        }

        else {
            let price = parseFloat(priceInput.value);

            if (price < 10 || price >= 1000) {
                validationMessage("Cena usluge mora biti u rasponu od 10 do 999", priceInput);
                isValid = false;
               
            }
        }

      
       
        return isValid;
    };
}




