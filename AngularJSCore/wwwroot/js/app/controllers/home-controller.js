var app = angular.module('MyApp');

app.controller('HomeController', function ($scope) {
    $scope.title = "Loading...";
    $scope.siteName = "Duy";
    $scope.Message = "My name is " + $scope.siteName + ".";

    $scope.showAlert = function () {
        alert($scope.siteName);
    };
});