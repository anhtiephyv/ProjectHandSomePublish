(function (app) {
    'use strict';

    app.controller('categoryEditController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function categoryCreateController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.category = {
            CategoryID:null,
            CategoryName: "",
            CategoryLevel: "1",
            ParentCategoryID: null,
            ParentName: null,
            Description: null,
            DisplayOrder: null,
        }
        function GetCategoryTreeData() {
            var datareturn = null;
            apiService.get('api/category/gettreedata', null,
                     function (result) {
                         debugger;
                         $('#treeview-checkable').treeview({
                             data: result.data,
                             showIcon: false,
                             lazyLoad: function (node, display) {
                                 apiService.get('api/category/gettreedata?ParentCategory=' + node.id, null,
                                   function (result) {
                                       display(result.data);
                                   },
                                   function (result) {
                                       notificationService.displayError(result.data);
                                   });

                             },
                             loadingIcon: "glyphicon glyphicon-hourglass",
                             showCheckbox: false,
                             color: "#428bca",
                             onNodeSelected: function (event, node) {
                                 debugger;
                                 $scope.category.ParentCategory = node.id;
                                 $scope.category.ParentName = node.text;
                             },
                         });

                     },
                     function (result) {
                         notificationService.displayError(result.data);
                     });
        }
        $scope.dataErorr = false;
        $scope.Updatecategory =
        function Updatecategory() {
            if (!$scope.dataErorr) {

                //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
                apiService.post('api/category/Update', $scope.category,
                    function (result) {
                        notificationService.displaySuccess(result.data.categoryName + ' đã được cập nhật.');

                        debugger;
                        $rootScope.clearSearch();
                        $rootScope.modalClose();
                        //  $state.go('category_list');
                    }, function (error) {
                        notificationService.displayError('Cập nhật không thành công.');
                    });
            }
        }
        function loadDetail() {
            apiService.get('/api/category/detail/' + $rootScope.categoryId, null,
            function (result) {
                debugger;
                $scope.category = result.data;
                $scope.category.categoryLevel = result.data.CategoryLevel.toString();
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
        GetCategoryTreeData();
    }]);
   
})(angular.module('MyApp'));
