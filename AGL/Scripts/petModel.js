var model = {
    pets: ko.observableArray([]),
    error: ko.observable(""),
    dddd:ko.observableArray([]),
    gotError: ko.observable(false)
}


//A set of properties of the handleError function
$(document).ready(function () {
    ko.applyBindings();
    setDefaultCallbacks(function (data) {
        if (data) {
            console.log("---Begin Success---");
            console.log(JSON.stringify(data));
            console.log("---End Success---");
        } else {
            console.log("Success (no data)");
        }
        model.gotError(false);
    },
        function (statusCode, statusText) {
            console.log("Error: " + statusCode + " (" + statusText + ")");
            model.error(statusCode + " (" + statusText + ")");
            model.gotError(true);
        });
});
