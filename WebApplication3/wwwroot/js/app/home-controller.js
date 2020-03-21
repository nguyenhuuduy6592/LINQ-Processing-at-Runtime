angular.module('MyApp', [])
    .controller('HomeController', function ($scope) {
        $scope.title = "Loading...";
        $scope.siteName = "Duy";
        $scope.Message = "My name is " + siteName + ".";

        $scope.showAlert = function () {
            alert($scope.siteName);
        };
    });