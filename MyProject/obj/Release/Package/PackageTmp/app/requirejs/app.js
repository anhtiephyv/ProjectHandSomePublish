define(['angularAMD', 'angular-route'], function (angularAMD) {
    var app = angular.module("webapp", ['ngRoute']);
    app.config(function ($routeProvider) {
        $routeProvider.when("/home", angularAMD.route({
            templateUrl: 'views/home.html', controller: 'HomeCtrl',
            controllerUrl: 'ctrl/home'
        }))
    });
    return angularAMD.bootstrap(app);
});