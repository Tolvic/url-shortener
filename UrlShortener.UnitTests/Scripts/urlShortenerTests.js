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
    var form, submitButton, urlField;

    beforeEach(function () {
        var mockHtml = getMockHtml();
        var mockHtmlWithContainer = addMockHtmlContainer(mockHtml);
        appendToDocumentBody(mockHtmlWithContainer);
        form = $("#url-shortener");
        submitButton = form.find("button");
        urlField = $("#long-url");
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

    

    function getMockHtml() {
        return '<form id="url-shortener" action="exampleURl" method="POST">' +
            '<input id="long-url" name="LongUrl" type="text" value="">' +
            '<button type="submit" class="btn btn-std btn-primary mt-4">Get Short URL</button>' +
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