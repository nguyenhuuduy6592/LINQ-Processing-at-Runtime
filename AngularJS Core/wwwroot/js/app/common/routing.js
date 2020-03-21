var app = angular.module('MyApp');

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            redirectTo: function () {
                return '/home';
            }
        })
        .when('/home', {
            templateUrl: '/templates/profile.html',
        })
        .when('/article', {
            templateUrl: '/templates/article.html',
        })
        .otherwise({
            templateUrl: '/templates/error.html',
        })

    $routeProvider.caseInsensitiveMatch = true;
    $locationProvider.html5Mode(false).hashPrefix('!');
});