require.config({
    baseUrl: "js",    
    paths: {
        'angular': 'angular',
        'angular-route': 'angular-route',
        'angularAMD': 'angularAMD.min'
    },
    shim: { 'angularAMD': ['angular'], 'angular-route': ['angular'] },
    deps: ['app']
});