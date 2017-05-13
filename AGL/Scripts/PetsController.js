var petUrl = "/api/pet/";
var getPets=function() {
    filterByCat(petUrl,"GET", {category:"cat"},function() {
        
    })
}