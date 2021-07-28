var urlShortener = window.urlShortener || {};

urlShortener.urlShortener = (function () {
    function init() {
        var form = getForm();
        urlShortener.utilities.ajaxFormInit(form, sucessCallback, errorCallback);
    }

    function getForm() {
        return $("#url-shortener");
    }

    function sucessCallback() {
        console.log("Success");
    }

    function errorCallback() {
        console.log("Error");

    }

    return {
        init: init,
        sucessCallback: sucessCallback,
        errorCallback: errorCallback
    }
})();