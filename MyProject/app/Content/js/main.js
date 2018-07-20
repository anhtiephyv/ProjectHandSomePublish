require.config({

    // alias libraries paths
    paths: {
        //'domReady': '../lib/requirejs-domready/domReady',
        'angular': 'angular'
    },

    // angular does not support AMD out of the box, put it in a shim
    shim: {
        // Đoạn này viết các thằng con phải phụ thuộc vào các js khác để có thể chạy được, đoạn này load trước 
        'angular': {
            exports: 'angular'
        },
        'appConfig': {
            exports: '../appConfig'
        }
    },

    //// kick start application
    //deps: ['./bootstrap']
});

//requirejs dependencies replace script loading order
//for angular dependencies to work we only need the files
//to load as they are applied on instantiation.
// Eg: changing services and controllers file order doesn't
//   create any errors even when controllers depend on services
//   they are only instantiated by DI after angular has been 
//   bootstrapped
require(['app/appConfig'], function (app) {
    console.log('app.js, services.js and controllers.js files loaded');
    app.init();
});