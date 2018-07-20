(function (app) {
    'use strict';

    app.controller('countryEditController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function countryCreateController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.country = {
            CountryName: "",
            CountryCronyms: "",
            CountryFlag: null,
            CountryStatus: "1",
            FileUpLoad: null,
            FileUploadName: null,
            FileUploadType: null,
            NumberLine: null,
        }
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.UpdateCountry =
        function UpdateCountry() {
            if (!$scope.dataErorr) {

                //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
                apiService.post('api/country/Update', $scope.country,
                    function (result) {
                        notificationService.displaySuccess(result.data.CountryName + ' đã được cập nhật.');

                        debugger;
                        $rootScope.clearSearch();
                        $rootScope.modalClose();
                        //  $state.go('country_list');
                    }, function (error) {
                        notificationService.displayError('Cập nhật không thành công.');
                    });
            }
        }
        $scope.setFiles =
function setFiles(element) {
    debugger;
    var files = event.target.files;
    debugger;

    if (event.target.files[0] != undefined && event.target.files[0] != null) {
        var reader = new FileReader();
        reader.onload = function (loadEvent) {
            $scope.country.FileUpLoad = loadEvent.target.result.split(",")[1];

        }
        $scope.country.FileUploadName = event.target.files[0].name;
        $scope.country.FileUploadType = event.target.files[0].type;
        reader.readAsDataURL(event.target.files[0]);
        var readerText = new FileReader();
        readerText.onload = function (loadEvent) {
            debugger;
            $scope.country.NumberLine = loadEvent.target.result.split("\n").length;

        }
        readerText.readAsText(event.target.files[0]);
    }
    else {
        $scope.country.FileUpLoad = null;
        $scope.country.FileUploadName = null;
        $scope.country.FileUploadType = null;
    }

}
        $scope.checkcodeExist = function (element) {
            apiService.get('api/Country/checkcodeExistEdit?CountryCode=' + element.value + '&Id=' + $rootScope.countryId, null,
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
        function loadDetail() {
            apiService.get('/api/country/detail/' + $rootScope.countryId, null,
            function (result) {
                debugger;
                $scope.country = result.data;
                $scope.country.CountryStatus = result.data.CountryStatus.toString();
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }
        $scope.Closemodal = function () {
            debugger;
            $rootScope.modalClose();
        }
        loadDetail();
    }]);
   
})(angular.module('MyApp'));
