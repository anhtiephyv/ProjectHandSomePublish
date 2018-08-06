(function (app) {
    'use strict';
    var app = angular.module('plunker', ['ngTagsInput']);
    app.controller('productCreateController', ['$scope', 'apiService', 'seoService', 'notificationService', '$filter', '$injector', '$rootScope',
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

        }

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            debugger;
            $scope.product.Alias = seoService.getSeoTitle($scope.product.ProductName);
        }
        $scope.codeExist = false;
        $scope.dataErorr = false;
        $scope.Addproduct =
        function Addproduct() {

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
        $scope.Tags = [];
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
        // test
        $scope.countryList = ["Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua &amp; Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia &amp; Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Islands", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cruise Ship", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kuwait", "Kyrgyz Republic", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mauritania", "Mauritius", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Namibia", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway", "Oman", "Pakistan", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre &amp; Miquelon", "Samoa", "San Marino", "Satellite", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "South Africa", "South Korea", "Spain", "Sri Lanka", "St Kitts &amp; Nevis", "St Lucia", "St Vincent", "St. Lucia", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad &amp; Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks &amp; Caicos", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe"];
        // Hàm khi nhập
        $scope.complete = function (string) {
            if (event.keyCode == 13) { // Nếu enter thì tạo tag
                $scope.Tags.push({ id: null, text: string, isNew: true });
                $scope.TextSearch = null;
                $scope.filterTag = null;
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
