angular.module('MyApp', [])
    .controller('ArticleController', function ($scope) {
        $scope.Message = "My name is " + siteName + ".";
    });