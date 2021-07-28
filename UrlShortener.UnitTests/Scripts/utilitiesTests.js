/// <reference path="../../UrlShortener/wwwroot/lib/jquery/jquery.js"/>
/// <reference path="../../UrlShortener/wwwroot/lib/jquery-validation/jquery.validate.js"/>
/// <reference path="../../UrlShortener/wwwroot/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"/>
/// <reference path="../../UrlShortener/wwwroot/js/utilities.js"/>
/// <reference path="../Lib/Jasmine/jasmine.js"/>
/// <reference path="../Lib/Jasmine/jasmine-html.js"/>

//These tests should be run using chutzpah

describe("urlShortener.utilities", function () {

    var utilitiesModule = urlShortener.utilities

    describe("AjaxFormInit", function () {
        beforeEach(function () {
            var mockHtml = "<form id='target-form' action='/actionUrl' method='get'><input type='text' name='name' value='Peter'></input></form>";
            var mockHtmlWithContainer = addMockHtmlContainer(mockHtml);
            appendToDocumentBody(mockHtmlWithContainer);
        });

        afterEach(function () {
            removeMockHtml();
        });

        it("should prevent submit event", function () {
            // arrange
            var event = $.Event('submit');
            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger(event);

            // assert
            expect(event.isDefaultPrevented()).toBe(true);
        });

        it("should call jquery validate", function () {
            // arrange
            spyOn($.fn, "validate").and.callThrough();
            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            expect($.fn.validate).toHaveBeenCalled();
        });

        it("should call jquery valid", function () {
            // arrange
            spyOn($.fn, "valid").and.callThrough();
            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            expect($.fn.valid).toHaveBeenCalled();
        });

        it("should not make ajax request when jquery.valid returns false", function () {
            // arrange
            spyOn($.fn, "valid").and.returnValue(false);
            spyOn($, "ajax");
            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            expect($.ajax.calls.count()).toBe(0);
        });

        it("should make ajax request with correct method when jquery.valid returns true", function () {
            // arrange
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, "ajax");
            var targetForm = $("#target-form");
            var expectedMethod = targetForm.attr("method");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            var actualMethod = $.ajax.calls.argsFor(0)[0]["type"];

            expect(actualMethod).toBe(expectedMethod);
        });

        it("should make ajax request to correct url when jquery.valid returns true", function () {
            // arrange
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, "ajax");
            var targetForm = $("#target-form");
            var expectedUrl = targetForm.attr("action");
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            var actualUrl = $.ajax.calls.argsFor(0)[0]["url"];

            expect(actualUrl).toBe(expectedUrl);
        });

        it("should make ajax request with correct data when jquery.valid returns true", function () {
            // arrange
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, "ajax");
            var targetForm = $("#target-form");
            var expectedData = targetForm.serialize();
            utilitiesModule.ajaxFormInit(targetForm);

            // act
            targetForm.trigger("submit");

            // assert
            var actualData = $.ajax.calls.argsFor(0)[0]["data"];

            expect(actualData).toBe(expectedData);
        });

        it("should call success callback when sucess callback is passed in and ajax request returns success", function () {
            // arrange
            var successCallback = jasmine.createSpy('successCallback');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.success();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, successCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(successCallback).toHaveBeenCalled();
        });

        it("should not call error callback ajax request returns success", function () {
            // arrange
            var errorCallback = jasmine.createSpy('errorCallback');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.success();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, null, errorCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(errorCallback).not.toHaveBeenCalled();
        });

        it("should call error callback when callback is passed in and ajax request returns error", function () {
            // arrange
            var errorCallback = jasmine.createSpy('errorCallback');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.error();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, null, errorCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(errorCallback).toHaveBeenCalled();
        });

        it("should not call success callback when ajax request returns error", function () {
            // arrange
            var successCallback = jasmine.createSpy('successCallback');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.error();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, successCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(successCallback).not.toHaveBeenCalled();
        });

        it("should call finally callback when callback is passed in and ajax request returns success", function () {
            // arrange
            var finallyCallback = jasmine.createSpy('finallyCallbackWhenFormValid');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.success();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, null, null, finallyCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(finallyCallback).toHaveBeenCalled();
        });

        it("should call finally callback when callback is passed in and ajax request returns error", function () {
            // arrange
            var finallyCallback = jasmine.createSpy('finallyCallbackWhenFormValid');
            spyOn($.fn, "valid").and.returnValue(true);
            spyOn($, 'ajax').and.callFake(function (params) {
                params.error();
            });

            var targetForm = $("#target-form");
            utilitiesModule.ajaxFormInit(targetForm, null, null, finallyCallback);

            // act
            targetForm.trigger("submit");

            // assert
            expect(finallyCallback).toHaveBeenCalled();
        });
    });

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