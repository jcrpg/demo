/*
    customer model is observable so it will automaticallly updates the client interface 
*/
var customerModel = {
    petCategories: ko.observableArray([]),
    filteredPets: ko.observableArray([]),
    selectedCategory: ko.observable(null),
    currentView:ko.observable("list")

}