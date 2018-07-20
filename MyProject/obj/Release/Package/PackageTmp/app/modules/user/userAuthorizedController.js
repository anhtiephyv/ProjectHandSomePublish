(function (app) {
    'use strict';

    app.controller('userAuthorizedController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function userAuthorizedController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.AddAuthorized =
        function AddAuthorized() {
            if (!$scope.dataErorr) {
             //   var Indata = { 'selectedCountry': $scope.CountryOptions, 'product2': $scope.product2 };
                //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
                apiService.post('api/country/CreateUserCountry?userID=' + $rootScope.userId, $scope.CountryOptions.selectedItems,
                    function (result) {
                        notificationService.displaySuccess('Phân quyền thành công');

                        debugger;
                        $rootScope.clearSearch();
                        $rootScope.modalClose();
                        //  $state.go('country_list');
                    }, function (error) {
                        notificationService.displayError('Phân quyền không thành công.');
                    });
            }
        }
        $scope.Closemodal = function () {
            debugger;
            $rootScope.modalClose();
        }
        $scope.CountryOptions = {
            title: 'Phân quyền các nước cho người dùng',
            filterPlaceHolder: 'Nhập vào đây để lọc các nước.',
            labelAll: 'Tất cả nước',
            labelSelected: 'Nước đã chọn',
            helpMessage: ' Chọn vào tên nước để chuyển đổi',
            /* angular will use this to filter your lists */
            orderProperty: 'name',
            /* this contains the initial list of all items (i.e. the left side) */
            items: $rootScope.listItem,
            /* this list should be initialized as empty or with any pre-selected items */
            selectedItems: $rootScope.selectItem
        };
    

    }]);

})(angular.module('MyApp'));
