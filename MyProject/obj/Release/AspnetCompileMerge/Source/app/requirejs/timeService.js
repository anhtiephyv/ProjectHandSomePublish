define(['app'], function(app) {
  console.log('timeService.js loaded');
  
  app.factory('Time', function () {
    return {
      getTime: function() {
        return new Date();
      }
    };
  });
});