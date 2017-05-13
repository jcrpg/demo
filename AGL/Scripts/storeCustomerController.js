var setCategory=function(category) {
    customerModel.selectedCategory(category);
    filterPetsByCategory();
}

var setView=function(view) {
    customerModel.currentView(view);
}


//when there are changes to the observable data items the subscribe function defined those functions will be called 
model.pets.subscribe(function (newPets) {
    filterPetsByCategory();
    customerModel.petCategories.removeAll();
    customerModel.petCategories.push.apply(customerModel.petCategories,
        model.pets().map(function (p) {
        return p.gender;
    }).filter(function(value, index, self) {
        return self.indexOf(value) === index;
    }).sort());

});

var filterPetsByCategory = function () {
    var category = customerModel.selectedCategory();
    customerModel.filteredPets.removeAll();
    customerModel.filteredPets.push.apply(customerModel.filteredPets, model.pets().filter(function(p) {
        return category == null || p.gender == category;
    }));
}

$(document).ready(function() {
    getPets();
})