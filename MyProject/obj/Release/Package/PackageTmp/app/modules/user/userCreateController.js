(function (app) {
    'use strict';

    app.controller('userCreateController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function countryCreateController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.User = {
            UserName: null,
            FirstName: null,
            LastName: null,
            Email: null,
            Phone: null,
            Address: null,
        }
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.AddUser =
        function AddUser() {
            if (!$scope.dataErorr) {

                //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
                apiService.post('api/users/create', $scope.User,
                    function (result) {
                        notificationService.displaySuccess(result.data.UserName + ' đã được thêm mới.');

                        debugger;
                        $rootScope.clearSearch();
                        $rootScope.modalClose();
                        //  $state.go('country_list');
                    }, function (error) {
                        notificationService.displayError('Thêm mới không thành công.');
                    });
            }
        }
        $scope.checkcodeExist = function (element) {
            apiService.get('api/Users/checkcodeExist?UserName='+element.value,null,
            function (result) {
                debugger;
                if (result.data) {
                    //      element.focus();
                    $scope.codeExist = result.data;
                    $scope.dataErorr = true;
                }
                else
                {
                    $scope.codeExist = result.data;
                    $scope.dataErorr = false;
                }
            }, function (error) {
                notificationService.displayError(error);
            });
            debugger;

        }

        $scope.Closemodal = function () {
            debugger;
            $rootScope.modalClose();
        }
    }]);

})(angular.module('MyApp'));
