(function (app) {
    'use strict';
    var app = angular.module('plunker', ['ngTagsInput']);
    app.controller('productEditController', ['$scope', 'apiService', 'seoService', 'notificationService', '$filter', '$injector', '$rootScope',
    function productCreateController($scope, apiService, seoService, notificationService, $filter, $injector, $rootScope) {
        $scope.product = {
            ProductName: "",
            CategoryID: null,
            CategoryName: null,
            ProductStatus: "1",
            MetaDescription: null,
            HomeFlag: false,
            HotFlag: false,
            Image: null,
            MoreImages: null,
            Tags: [],
            Alias: null,
            MetaKeyword: null,
        }

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            debugger;
            $scope.product.Alias = seoService.getSeoTitle($scope.product.ProductName);
        }
        $scope.GetSeoTitle = GetSeoTitle;
        function loadDetail() {
            apiService.get('/api/product/detail/' + $rootScope.productId, null,
            function (result) {
                debugger;
                $scope.product = result.data;
                $scope.moreImages = JSON.parse(result.data.MoreImages);
                $scope.product.ProductStatus = result.data.ProductStatus.toString();
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }
        loadDetail();
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.Tags = [];
        $scope.Updateproduct =
        function Updateproduct() {

            $scope.product.Tags = $scope.Tags;
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.post('api/product/create', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.ProductName + ' đã được thêm mới.');
                    $rootScope.clearSearch();
                    $rootScope.modalClose();
                    //  $state.go('product_list');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });

        }
        function GetTagByProductId() {
            apiService.get('api/product/getlisttagbyproductid/' + $rootScope.productId, null,
         function (result) {
             $scope.Tags = result.data;
         },
         function (result) {
             notificationService.displayError(result.data);
         });
        }
        GetTagByProductId();
        $scope.ListTag = null;
        function GetListTag() {
            apiService.get('api/product/getlisttag', null,
                     function (result) {
                         $scope.ListTag = result.data;
                     },
                     function (result) {
                         notificationService.displayError(result.data);
                     });
        }
        GetListTag();
        $scope.TextSearch = null;
        // Hàm khi nhập
        $scope.complete = function (string) {
            if (event.keyCode == 13) { // Nếu enter thì tạo tag

                $scope.Tags.push({ id: null, text: string, isNew: true });
                $scope.TextSearch = null;
                $scope.filterTag = null;
                return false;
            }
            else {
                var output = [];
                angular.forEach($scope.ListTag, function (Tag) {
                    if (Tag.text.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                        debugger;
                        output.push(Tag);
                    }
                });
                $scope.filterTag = output;
            }
        }
        // Disable submit form khi enter
        $('#myFormId').on('keyup keypress', function (e) {
            var keyCode = e.keyCode || e.which;
            if (keyCode === 13) {
                e.preventDefault();
                return false;
            }
        });
        // RemoveTag
        $scope.removeTag = function (DeleteTag) {
            debugger;
            angular.forEach($scope.Tags, function (Tag, index) {
                if (Tag.text.indexOf(DeleteTag.text) >= 0) {
                    $scope.Tags.splice(index, 1);
                }
            });
        }
        // Hàm khi click vào danh sách tag gợi ý
        $scope.fillTextbox = function (obj) {
            var IsExisted = false;
            // check xem tag đã được thêm chưa?
            for (var i = 0; i < $scope.Tags.length; i++) {
                // look for the entry with a matching `code` value
                if ($scope.Tags[i].id == obj.id) {
                    IsExisted = true;
                }
            }
            if (IsExisted === false) {
                $scope.Tags.push(obj);
            }
            $scope.TextSearch = null;
            $scope.filterTag = null;

        }
        // endtesst
        function GetCategoryTreeData() {
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
                                 $scope.product.CategoryID = node.id;
                                 $scope.product.CategoryName = node.text;
                             },
                         });

                     },
                     function (result) {
                         notificationService.displayError(result.data);
                     });
        }
        GetCategoryTreeData();
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    if ($scope.moreImages.indexOf(fileUrl) === -1) {
                        $scope.moreImages.push(fileUrl);
                    }

                })

            }
            finder.popup();
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }
        $scope.DeleteImage = function () {
            $scope.product.Image = null;
        }
        $scope.DeleteMoreImage = function () {
            $scope.moreImages.splice(-1, 1)
        }
        $scope.moreImages = [];
        $scope.Closemodal = function () {
            $rootScope.modalClose();
        }
    }]);

})(angular.module('MyApp'), ['ngTagsInput']);
