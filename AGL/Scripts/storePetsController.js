/*
    Defining the Pets Controller
    which will send an Ajax call to the corresponding call to the server site Pet controller
*/
var petUrl = "/api/pet/";
var getPets = function() {
    sendPetRequest(petUrl, "GET", {category:"cat"}, function(data) {
        model.pets.removeAll();
        model.pets.push.apply(model.pets,data);
    });
};


