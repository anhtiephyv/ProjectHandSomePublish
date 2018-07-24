(function (app) {
    'use strict';

    app.controller('categoryCreateController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function categoryCreateController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.category = {
            categoryName: "",
            categoryLevel: "1",
            ParentCategory: null,
            Description: null,
            DisplayOrder: null,
        }
        $scope.treedata =
        [
	{
	    "label": "User", "id": "role1", "collapsed": true, "children": [
          { "label": "subUser1", "id": "role11", "children": [] },
          {
              "label": "subUser2", "id": "role12", "children": [
                {
                    "label": "subUser2-1", "id": "role121", "children": [
                      { "label": "subUser2-1-1", "id": "role1211", "children": [] },
                      { "label": "subUser2-1-2", "id": "role1212", "children": [] }
                    ]
                }
              ]
          }
	    ]
	},
	{ "label": "Admin", "id": "role2", "children": [] },
	{ "label": "Guest", "id": "role3", "children": [] }
        ];
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.Addcategory =
        function Addcategory() {
            alert('wow');
            //if (!$scope.dataErorr) {

            //    //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
            //    apiService.post('api/category/create', $scope.category,
            //        function (result) {
            //            notificationService.displaySuccess(result.data.categoryName + ' đã được thêm mới.');

            //            debugger;
            //            $rootScope.clearSearch();
            //            $rootScope.modalClose();
            //            //  $state.go('category_list');
            //        }, function (error) {
            //            notificationService.displayError('Thêm mới không thành công.');
            //        });
            //}
        }
        $scope.setFiles =
function setFiles(element) {
    debugger;
    var files = event.target.files;
    debugger;

    if (event.target.files[0] != undefined && event.target.files[0] != null) {
        var reader = new FileReader();
        reader.onload = function (loadEvent) {
            $scope.category.FileUpLoad = loadEvent.target.result.split(",")[1];

        }
        $scope.category.FileUploadName = event.target.files[0].name;
        $scope.category.FileUploadType = event.target.files[0].type;
        reader.readAsDataURL(event.target.files[0]);
        var readerText = new FileReader();
        readerText.onload = function (loadEvent) {
            debugger;
            $scope.category.NumberLine = loadEvent.target.result.split("\n").length;

        }
        readerText.readAsText(event.target.files[0]);
    }
    else {
        $scope.category.FileUpLoad = null;
        $scope.category.FileUploadName = null;
        $scope.category.FileUploadType = null;
    }

}
        $scope.checkcodeExist = function (element) {
            apiService.get('api/category/checkcodeExist?categoryCode=' + element.value, null,
            function (result) {
                debugger;
                if (result.data) {
                    //      element.focus();
                    $scope.codeExist = result.data;
                    $scope.dataErorr = true;
                }
                else {
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
