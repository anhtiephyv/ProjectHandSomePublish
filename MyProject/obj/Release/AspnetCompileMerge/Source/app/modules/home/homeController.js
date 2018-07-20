
(function (app) {
    app.controller('homeController', ['$scope', '$injector', '$location', 'apiService', 'notificationService', '$modal', '$rootScope', '$http',
        function ($scope, $injector, $location, apiService, notificationService, $modal, $rootScope, $http) {
            //   $injector = 
            if (!localStorage.TokenInfo) {
                var stateService = $injector.get('$state');
                stateService.go('login');
            }
            else {
                debugger;
                $scope.UserName = localStorage.getItem("userName");
                $scope.LogOut = function () {
                    localStorage.clear();
                    var stateService = $injector.get('$state');
                    stateService.go('login');
                }
            }
            $scope.ChangeProfile = function () {
                var modalHtml = 'modules/home/changeProfile.html';
                require(
      [
       '/app/modules/home/changeProfileController.js'
      ],
      function (changeProfileController) {
          $scope.myModalInstance = $modal.open({
              templateUrl: modalHtml, // loads the template

              // windowClass: 'modal-dialog modal-sm', // windowClass - additional CSS class(es) to be added to a modal window template
              controller: changeProfileController,
              windowClass: 'app-modal-window',
              backdrop: true,
          });//end of modal.open
      });
                $rootScope.modalClose = function () {
                    $scope.myModalInstance.close();
                }
            }
        }]);
})
	(angular.module('MyApp'));