angular.module('MyApp', ['ngRoute'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                redirectTo: function () {
                    return '/home';
                }
            })
            .when('/home', {
                templateUrl: '/templates/profile.html',
                controller: 'HomeController'
            })
            .when('/article', {
                templateUrl: '/templates/article.html',
                controller: 'ArticleController'
            })
            .when('/blog/:id', {
                templateUrl: '/templates/blog.html',
                controller: 'BlogController'
            })
            .otherwise({
                templateUrl: '/templates/error.html',
                controller: 'ErrorController'
            });
        $routeProvider.caseInsensitiveMatch = true;
        $locationProvider.html5Mode(false).hashPrefix('!');
    })
    .controller('HomeController', function ($scope) {
        $scope.Message = "http://www.c-sharpcorner.com/members/satyaprakash-samantaray";
    })
    .controller('ArticleController', function ($scope) {
        $scope.Message = "http://www.c-sharpcorner.com/article/introduction-to-sql-operations-studio-and-make/";
    })
    .controller('BlogController', function ($scope, $routeParams) {
        $scope.Message = "http://www.c-sharpcorner.com/blogs/career-booster-through-c-sharp-corner" + "Of Id" + $routeParams.id;
    })
    .controller('ErrorController', function ($scope) {
        $scope.Message = "Page You Requested Not Found!";
    }); 