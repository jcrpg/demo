/*
    created: 13/05/2017
    creating javascript that will make Ajax call on behalf of other parts of the application so it can be reused
*/
var sendPetRequest = function (url, verb, data, successCallback, errorCallback, options) {
    var requestOptions = options || {};
    requestOptions.type = verb;
    requestOptions.success = successCallback;
    requestOptions.error = errorCallback;

    if (!url || !verb) {
        errorCallback(401, "URL and HTTP verb required");
    }

    if (data) {
        requestOptions.data = data;
    }
    $.ajax(url, requestOptions);
}

var setDefaultCallbacks = function(successCallback, errorCallback) {
    $.ajaxSetup({
        complete: function(jqXHR, status) {
            //status 2xx : successful
            //status 3xx : redirect
            if (jqXHR.status >= 200 && jqXHR.status < 300) {
                successCallback(jqXHR.responseJSON);
            } else {
                errorCallback(jqXHR.status, jqXHR.statusText);
            }
        }
    });
}