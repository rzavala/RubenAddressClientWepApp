//addressRemoveController.js
(function () {
    "use strict";

    angular.module("app-address")
        .controller("addressRemoveController", addressRemoveController);

    function addressRemoveController($routeParams, $http, $location) {
        var vm = this;

        vm.id = $routeParams.id;

        vm.removeAddress = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/clientaddress/" + $routeParams.id) //obtener el registro a borrar
            .then(function (response) {
                //results when it works
                angular.copy(response.data, vm.removeAddress);

            }, function (error) {
                //results when it fails
                vm.errorMessage = "Failed to load data from the server: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.removeAddressClient = function () {
            vm.isBusy = true;
            vm.errorMessage = "";


            $http.delete("/api/clientaddress/" + vm.removeAddress.id, vm.removeAddress)
                .then(function (response) {
                    //results when it works
                    //go back
                    vm.removeAddress = {}; //objeto vacio, limpia el form
                    $location.path("/");


                }, function (error) {
                    //results when it fails
                    vm.errorMessage = "Failed to save new client address. " + error;
                })
                .finally( function () {
                    vm.isBusy = false;
                });
        };
    }
})();