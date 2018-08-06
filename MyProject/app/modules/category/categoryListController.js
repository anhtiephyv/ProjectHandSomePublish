(function (app) {
    'use strict';
    app.controller('categoryListController', ['$scope', 'apiService', 'notificationService', '$filter', '$modal','$rootScope','$http',
    function userListController($scope, apiService, notificationService, $filter,$modal,$rootScope,$http) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $rootScope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;
        $scope.selectAll = selectAll;
        $scope.create = create;
        $scope.editcategory = editcategory;
        $scope.deleteMultiple = deleteMultiple;
        $scope.keyword = '';
        function deleteMultiple() {
            bootbox.confirm({
                message: "Bạn có chắc chắn muốn xóa những nước này?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result) {
                        var listId = [];
                        $.each($scope.selected, function (i, item) {
                            listId.push(item.CategoryID);
                        });
                        var config = {
                            params: {
                                checkedList: JSON.stringify(listId)
                            }
                        }
                        apiService.del('api/category/deletemulti', config, function (result) {
                            notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                            search();
                        }, function (error) {
                            notificationService.displayError('Xóa không thành công');
                        });
                    }
                }
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.data, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.data, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("data", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteItem(id) {
            //bootbox.confirm('Bạn có chắc muốn xóa?')
            //    .then(function () {
            //        var config = {
            //            params: {
            //                id: id
            //            }
            //        }
            //        apiService.del('/api/applicationGroup/delete', config, function () {
            //            notificationService.displaySuccess('Đã xóa thành công.');
            //            search();
            //        },
            //        function () {
            //            notificationService.displayError('Xóa không thành công.');
            //        });
            //    });
            //bootbox.confirm("Bạn có chắc muốn xóa?", function () {
            //    debugger;
            //});
            bootbox.confirm({
                message: "Bạn có chắc chắn muốn xóa?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result) {
                        var config = {
                            params: {
                                id: id
                            }
                        }
                        apiService.del('/api/category/delete/', config, function () {
                            notificationService.displaySuccess('Đã xóa thành công.');
                            search();
                        },
                        function () {
                            notificationService.displayError('Xóa không thành công.');
                        });
                    }
                }
            });

        }
        function search(page) {
            page = page || 0;
            $scope.loading = true;
            var config = {
                params: {
                    keyword:$scope.keyword,
                    page: page,
                    pageSize: 10,
                    orderby: "CategoryID",
                    sortDir: "desc",
                    filter: $scope.filterExpression
                }
            }

            apiService.get('api/category/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' items found');
            }
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        $scope.search();
        function create() {
            var modalHtml = 'modules/category/categoryCreate.html';
         
            require(
           [
            '/app/modules/category/categoryCreateController.js', 'ngckEditor', 'bootstraptreeview'
           ],
           function (categoryCreateController, ngckEditor, boostraptreeview) {
               $scope.myModalInstance = $modal.open({
                   templateUrl: modalHtml, // loads the template
                  
                  // windowClass: 'modal-dialog modal-sm', // windowClass - additional CSS class(es) to be added to a modal window template
                   controller: categoryCreateController,
                   windowClass: 'app-modal-window',
                   backdrop: true,
               });//end of modal.open
           });
            $rootScope.modalClose = function () {
                $scope.myModalInstance.close();
            }

            

        };
        function editcategory(id) {
            var modalHtml = 'modules/category/categoryEdit.html';
            require(
           [
            '/app/modules/category/categoryEditController.js', 'ngckEditor', 'bootstraptreeview'
           ],
           function (categoryEditController, ngckEditor, boostraptreeview) {
               $scope.myModalInstance = $modal.open({
                   templateUrl: modalHtml,
                   controller: categoryEditController,
                   windowClass: 'app-modal-window',
                   backdrop: false,
               });
           });
            $rootScope.modalClose = function () {
                $scope.myModalInstance.close();
            }
            $rootScope.categoryId = id;
        };
    }]);
})(angular.module('MyApp'));
