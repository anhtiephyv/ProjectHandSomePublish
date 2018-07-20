define(['app'], function(app) {
  console.log('mainCtrl.js loaded');
  
  app.controller('MainController', ['$scope', 'Time', function ($scope, Time) {
    console.log('MainCtrl instantiation');
    $scope.name = 'World';
    $scope.time = Time.getTime();
  }]);
});