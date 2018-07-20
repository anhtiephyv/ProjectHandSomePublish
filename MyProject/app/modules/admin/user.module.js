
(function () {
    angular.module('MyApp').config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('admin', {
            url: "/admin",
            templateUrl: "/app/admin/adminList.html",
            parent: 'base',
            controller: "adminListController"
        })

    }
})();