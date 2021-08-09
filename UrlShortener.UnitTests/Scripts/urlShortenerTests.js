/// <reference path="../../UrlShortener/wwwroot/lib/jquery/jquery.js"/>
/// <reference path="../../UrlShortener/wwwroot/lib/jquery-validation/jquery.validate.js"/>
/// <reference path="../../UrlShortener/wwwroot/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"/>
/// <reference path="../../UrlShortener/wwwroot/js/utilities.js"/>
/// <reference path="../../UrlShortener/wwwroot/js/urlShortener.js"/>
/// <reference path="../Lib/Jasmine/jasmine.js"/>
/// <reference path="../Lib/Jasmine/jasmine-html.js"/>

//These tests should be run using chutzpah


describe("urlShortener.urlShortener", function () {

    var utilitiesModule = urlShortener.utilities;
    var urlShortenerModule = urlShortener.urlShortener;
    var form, shortUrlField, copyToClipboardButton, errorMessage, outputGroup;

    beforeEach(function () {
        var mockHtml = getMockHtml();
        var mockHtmlWithContainer = addMockHtmlContainer(mockHtml);
        appendToDocumentBody(mockHtmlWithContainer);
        form = $("#url-shortener");
        shortUrlField = $("#short-url");
        copyToClipboardButton = $("#copy-to-clipboard-button");
        errorMessage = $("#error-message");
        outputGroup = $("#short-url-output-group");

    });

    afterEach(function () {
        removeMockHtml();
    });

    it("should call urlShortener.utilities.ajaxFormInit when init is called", function () {
        // arrange
        spyOn(utilitiesModule, 'ajaxFormInit');

        // act
        urlShortenerModule.init();

        // assert
        expect(utilitiesModule.ajaxFormInit).toHaveBeenCalledWith(form, urlShortenerModule.sucessCallback, urlShortenerModule.errorCallback);
    });

    it("should call document.executeCommand(copy) when copy to clickboard button is clicked", function () {
        // arrange
        spyOn(document, 'execCommand');
        urlShortenerModule.init();

        // act
        copyToClipboardButton.click()

        // assert
        expect(document.execCommand).toHaveBeenCalledWith("Copy");
    });

    it("should hide error message when successCallback is called", function () {
        // arrange
        errorMessage.removeClass("hide");

        // act
        urlShortenerModule.sucessCallback("")

        // assert
        expect(errorMessage.hasClass("hide"))
    });

    it("should set value of shortUrlField when successCallback is called", function () {
        // act
        urlShortenerModule.sucessCallback("test");

        // assert
        expect(shortUrlField.val()).toBe("test");
    });

    it("should show shortUrlField when successCallback is called", function () {
        // act
        urlShortenerModule.sucessCallback("test");

        // assert
        expect(outputGroup.hasClass("hide")).toBe(false);
    });

    it("should show error message when erroCallback is called", function () {
        // act
        urlShortenerModule.errorCallback();

        // assert
        expect(errorMessage.hasClass("hide")).toBe(false);
    });


    function getMockHtml() {
        return '<form id="url-shortener" action="exampleURl" method="POST">' +
            '<p id="error-message" class="text-danger hide">Opps something went wrong. Make sure to use a valid URL and try again.</p>' +
            '<input id="long-url" name="LongUrl" type="text" value="">' +
            '<button type="submit" class="btn btn-std btn-primary mt-4">Get Short URL</button>' +
            '<div id="short-url-output-group">' +
            '<input id="short-url" name="shortUrl" for="long=url">' +
            '<button type="button" id="copy-to-clipboard-button">Copy</button>' +
            '</div>' +
            '</form>';
    }

    function addMockHtmlContainer(html) {
        return '<div id="mock-html-container">' +
            html +
            '</div>';
    }

    function appendToDocumentBody(html) {
        $(document.body).append(html);
    }

    function removeMockHtml() {
        $("#mock-html-container").remove();
    }
});