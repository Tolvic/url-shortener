var urlShortener = window.urlShortener || {};

urlShortener.utilities = (function () {
    function ajaxFormInit(form, successCallback, errorCallback, finallyCallback) {
        form.on("submit", function (event) {
            event.preventDefault();

            if (validateForm(form)) {
                $.ajax({
                    url: form.attr("action"),
                    type: form.attr("method"),
                    data: form.serialize(),
                    timeout: 30000,
                    success: function () {
                        if (isFunction(successCallback)) {
                            successCallback();
                        }
                    },
                    error: function () {
                        if (isFunction(errorCallback)) {
                            errorCallback();
                        }
                    }
                });

                if (isFunction(finallyCallback)) {
                    finallyCallback();
                }
            }
        });
    }

    function validateForm(form) {
        form.validate();
        return form.valid();
    }

    function isFunction(possibleFunction) {
        return typeof (possibleFunction) === typeof (Function);
    }

    return {
        ajaxFormInit: ajaxFormInit,
    }
})();