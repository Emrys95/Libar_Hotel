let validationMessage = function(message, input) {
    
    if(input || message) 
    {
        //ukoliko ne postoji labela sa odgovarajucim id za input gresku kreiraj novu labelu i dodelu joj poruku
        if(!document.getElementById("error-" + input.id)){
            let label = document.createElement("label");
            label.textContent = message;
            label.id= "error-" + input.id;
            label.classList.add("text-danger");
            console.log("dodaj poruku");
            input.parentNode.insertBefore(label, input.nextSibling);
            

           
            
        }

        //ukoliko vec postoji labela sa id gde se pojavljuje greska za input samo mu postavi poruku
        else if(document.getElementById("error-"+input.id)){
            console.log("izmeni poruku");
            document.getElementById("error-" + input.id).textContent = message;
            
        }
        
    }

   

    else {
        console.log("Error - input ili poruka nije definisana");
    }
    
}