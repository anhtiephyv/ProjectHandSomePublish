(function (app) {
    'use strict';

    app.controller('categoryCreateController', ['$scope', 'apiService', 'notificationService', '$filter', '$injector', '$rootScope',
    function categoryCreateController($scope, apiService, notificationService, $filter, $injector, $rootScope) {
        $scope.category = {
            categoryName: "",
            categoryLevel: "1",
            ParentCategoryID: null,
            ParentName: null,
            Description: null,
            DisplayOrder: null,
         
        }
        $scope.selectdNode = null;
        function GetCategoryTreeData() {
            var datareturn = null;
            apiService.get('api/category/gettreedata', null,
                     function (result) {
                         $('#treeview-checkable').treeview({
                             data: result.data,
                             showIcon: false,
                        //     showTags:true,
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
                                 $scope.category.ParentCategory = node.id;
                                 $scope.category.ParentName = node.text;
                                 $scope.selectdNode = node;
                             },
                         });

                     },
                     function (result) {
                         notificationService.displayError(result.data);
                     });
        }
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.Addcategory =
        function () {
            //$scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.post('api/category/create', $scope.category,
                function (result) {
                    notificationService.displaySuccess(result.data.CategoryName + ' đã được thêm mới.');
                    $rootScope.clearSearch();
                    $rootScope.modalClose();
                    //  $state.go('category_list');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });

        }
        $scope.UnselectNode = function UnselectNode() {
            $('#treeview-checkable').treeview('unselectNode', [$scope.selectdNode, { silent: true }]);
            $scope.category.ParentCategory = null;
            $scope.category.ParentName = null;
        }
        $scope.Closemodal = function () {
            $rootScope.modalClose();
        }
        GetCategoryTreeData();
    }]);

})(angular.module('MyApp'));
