//addressEditorController.js
(function () {
    "use strict";

    angular.module("app-address")
        .controller("addressEditorController", addressEditorController);

    function addressEditorController($routeParams, $http, $location) {
        var vm = this;

        vm.id = $routeParams.id;

        vm.editAddress = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/clientaddress/" + $routeParams.id) //obtener el registro a editar
            .then(function (response) {
                //results when it works
                angular.copy(response.data, vm.editAddress);

            }, function (error) {
                //results when it fails
                vm.errorMessage = "Failed to load data from the server: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.editAddressClient = function () {
            vm.isBusy = true;
            vm.errorMessage = "";


            $http.put("/api/clientaddress/" + vm.editAddress.id, vm.editAddress)
                .then(function (response) {
                    //results when it works
                    //go back
                    vm.editAddress = {}; //objeto vacio, limpia el form
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