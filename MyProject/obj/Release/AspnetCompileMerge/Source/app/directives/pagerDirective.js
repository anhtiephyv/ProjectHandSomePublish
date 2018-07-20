define(['app'], function (app) {


    'use strict';
    var app = angular.module('MyApp', []);
    app.directive('pagerDirective', pagerDirective);

    function pagerDirective() {
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@'
            },
            replace: true,
            restrict: 'E',
            templateUrl: '/app/directives/pagerDirective.html',
            controller: [
                '$scope', function ($scope) {
                    $scope.search = function (i) {
                        if ($scope.searchFunc) {
                            $scope.searchFunc({ page: i });
                        }
                    };

                    $scope.range = function () {
                        if (!$scope.pagesCount) { return []; }
                        var step = 2;
                        var doubleStep = step * 2;
                        var start = Math.max(0, $scope.page - step);
                        var end = start + 1 + doubleStep;
                        if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                        var ret = [];
                        for (var i = start; i != end; ++i) {
                            ret.push(i);
                        }

                        return ret;
                    };

                    $scope.pagePlus = function (count) {
                        return +$scope.page + count;
                    }

                }]
        }
    }

    app.directive("fileread", [function () {
        return {
            scope: {
                fileread: "=",
            },
            restrict: 'A',
            link: function (scope, element, attributes) {
                debugger;
                element.bind("change", function (changeEvent) {
                    debugger;
              
                    if (changeEvent.target.files[0] != undefined && changeEvent.target.files[0] != null) {
                        var reader = new FileReader();
                        reader.onload = function (loadEvent) {
                            scope.$apply(function () {
                                scope.fileread = loadEvent.target.result.split(",")[1];
                                var rawLog = reader.result;
                            });
                        }
                        reader.readAsDataURL(changeEvent.target.files[0]);
                    }
                    else
                    {
                        scope.fileread = null;
                    }
                });
            }
        }

    }]);

    app.directive('nxEqual', function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, model) {
                if (!attrs.nxEqual) {
                    console.error('nxEqual expects a model as an argument!');
                    return;
                }
                scope.$watch(attrs.nxEqual, function (value) {
                    model.$setValidity('nxEqual', value === model.$viewValue);
                });
                model.$parsers.push(function (value) {
                    var isValid = value === scope.$eval(attrs.nxEqual);
                    model.$setValidity('nxEqual', isValid);
                    return isValid ? value : undefined;
                });
            }
        };
    });
    app.directive("filedirective", [function () {
        return {
            scope: {
                filetype: "=",
                filerName: "="
            },
            link: function (scope, element, attributes) {
                debugger;
                element.bind("change", function (changeEvent) {
                    if (changeEvent.target.files[0] != undefined && changeEvent.target.files[0] != null) {
                        scope.$apply(function () {
                            scope.filerName = changeEvent.target.files[0].name;
                            scope.filetype = changeEvent.target.files[0].type;
                        });
                    }
                    else
                    {
                        scope.filerName = null;
                        scope.filetype = null;
                    }
                });
            }
        }
    }]);
    app.directive('customOnChange', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var onChangeHandler = scope.$eval(attrs.customOnChange);
                element.on('change', onChangeHandler);
                element.on('$destroy', function () {
                    element.off();
                });

            }
        };
    });
})