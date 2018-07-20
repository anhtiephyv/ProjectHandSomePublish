define(['app', 'restangular'], function (app) {
    var app = angular.module('MyApp', ['restangular']);
    app.controller('loginController', ['$scope', '$injector', '$location', 'authService','authenticationService','authData',
    function ($scope, $injector, $localtion, authService,authenticationService,authData) {
        debugger;
    
        $scope.loginData = {
            userName: "",
            password: "",
            message: ""
        };

        $scope.loginSubmit = function () {
            authService.userAuthentication($scope.loginData.userName, $scope.loginData.password).then(
function success(response) {
    debugger;
    // do successful stuff here  
    debugger;
    localStorage.setItem('TokenInfo', response.data);
//sessionStorage.set
    userInfo = {
        accessToken: response.data.access_token,
        userName: response.data.userName
    };
    authenticationService.setTokenInfo(userInfo);
    authData.authenticationData.IsAuthenticated = true;
    authData.authenticationData.userName = response.data.userName;
    authData.authenticationData.accessToken = response.data.access_token;
    localStorage.setItem('userName', response.data.userName);
    var stateService = $injector.get('$state');
    stateService.go('home');
    //window.onunload = function () {
    //    debugger;
    //    localStorage.removeItem('TokenInfo');
    //}
       
  
},
function err(response) {
    debugger;
    // response.data - find error message here
    // response.status - find error code here 
    // treat error here
    $scope.loginData.message = "Sai thông tin đăng nhập hoặc mật khẩu";

});
        }
  
    }

    ]);
});
