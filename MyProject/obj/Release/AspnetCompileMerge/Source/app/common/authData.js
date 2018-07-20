define(['app'], function (app) {

    var app = angular.module('MyApp', []);
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})
