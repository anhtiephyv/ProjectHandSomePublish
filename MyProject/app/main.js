/// <reference path="Extension/treeview/angular.treeview.js" />
/// <reference path="Extension/treeview/angular.treeview.js" />
/// <reference path="Extension/treeview/angular.treeview.js" />
require.config({

    baseUrl: "Content/js",
    paths: {
       
        'angular': 'angular',
        'angular-route': 'angular-route',
        'angularAMD': 'angularAMD',
        'angular-ui-router': 'angular-ui-router',
        'ngload': 'ngload',
        'restangular': 'restangular',
        'underscore': 'underscore',
        'app': '../../app',
        'bootstrapUi': 'angular-ui/ui-bootstrap-custom-tpls-0.12.1',
        //  'angular-ui-bootstrap': 'angular-ui/ui-bootstrap',
        //  'toastr': 'toastr',
        'authService': '../../common/authService',
        'authData': '../../common/authData',

        'notificationService': '../../common/notificationService',
        'apiService': '../../common/apiService',
        'authenticationService': '../../common/authenticationService',
        'pagerDirective': '../../directives/pagerDirective',
        'dualmultiselect': 'dualmultiselect',
        'dualmultiselect.min': 'dualmultiselect.min',
        'treeview': '../../Extension/treeview/angular.treeview',
        'uitree': '../../Extension/ui-tree/angular-ui-tree',
        'bootstraptreeview': '../../Extension/bootstraptreeview/bootstrap-treeview',
        'ngckEditor': '../../Extension/ngeditor/ng-ckeditor'
        // 'dialogs': '../../common/dialogs.min',
     
    },
    // Đoạn này viết các thằng con phải phụ thuộc vào các js khác để có thể chạy được, đoạn này load trước 
    shim: {
        angular: {
            exports: 'angular'
        },
        'angularAMD': ['angular'],
        'angular-route': ['angular'],
        'angular-ui-router': ['angular'],
        'bootstrapUi': {
            deps: ["angular"],
            exports: "bootstrapUi"
        },
        //  'angular-ui-bootstrap': { deps: ['angular'] },
        'ngload': ['angularAMD'],
        'restangular': ['angular', 'underscore'],

        "app": {
            deps: ['angular', 'angular-ui-router']
        },
        "authService": ['app'],
        "authData": ['app'],
        "authenticationService":['app','authData'],
        "apiService": { deps: ['app', 'notificationService', 'authenticationService'] },
        'notificationService': ['app'],
        'pagerDirective': ['app'],
        'dualmultiselect': ['angular'],
        'dualmultiselect.min': ['angular']
        // 'dialogs': ['bootstrapUi','angular-ui-bootstrap','app']
       
    },
    //
    //deps: ['../../app']
});
require(['app', 'authService', 'authData', 'apiService', 'notificationService', 'authenticationService', 'pagerDirective', 'bootstrapUi'], function (app, authService, authData, apiService, notificationService, authenticationService, pagerDirective, bootstrapUi) {
   // console.log('app.js, services.js and controllers.js files loaded');
//    app.init();
});

