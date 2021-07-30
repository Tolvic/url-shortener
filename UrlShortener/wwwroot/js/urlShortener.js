var urlShortener = window.urlShortener || {};

urlShortener.urlShortener = (function () {
    function init() {
        var form = getForm();
        urlShortener.utilities.ajaxFormInit(form, sucessCallback, errorCallback);
        setupCopyToClipboard()
    }

    function sucessCallback(shortUrl) {
        hide($("#error-message"));
        var outputField = getOutputField();
        outputField.val(shortUrl)

        var outputGroup = getOutputGroup();
        show(outputGroup);
    }

    function errorCallback() {
        show($("#error-message"))
    }

    function getForm() {
        return $("#url-shortener");
    }

    function getOutputGroup() {
        return $("#short-url-output-group");
    }

    function getOutputField() {
        return $("#short-url");
    }

    function show(element) {
        element.removeClass("hide");
    }

    function hide(element) {
        element.addClass("hide");
    }

    function setupCopyToClipboard() {
        var button = getCopyButton();

        button.on("click", function () {
            copyShortUrlToClipboard();
        })
    }

    function getCopyButton() {
        return $("#copy-to-clipboard-button");
    }

    function copyShortUrlToClipboard() {
        var outputField = getOutputField();
        outputField.select();
        document.execCommand("Copy");
    }

    return {
        init: init,
        sucessCallback: sucessCallback,
        errorCallback: errorCallback
    }
})();