(function (app) {
    'use strict';

    app.controller('countryCreateController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
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
        $scope.AddCountry =
        function AddCountry() {
            if (!$scope.dataErorr) {

                //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
                apiService.post('api/country/create', $scope.country,
                    function (result) {
                        notificationService.displaySuccess(result.data.CountryName + ' đã được thêm mới.');

                        debugger;
                        $rootScope.clearSearch();
                        $rootScope.modalClose();
                        //  $state.go('country_list');
                    }, function (error) {
                        notificationService.displayError('Thêm mới không thành công.');
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
            apiService.get('api/Country/checkcodeExist?CountryCode='+element.value,null,
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
