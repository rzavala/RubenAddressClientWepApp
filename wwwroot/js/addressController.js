//addressController.js
//separar codigo js del global scope.
(function () {
    "use strict";

    //obtener el mudule creado en app-address.js
    angular.module("app-address")
        .controller("addressController", addressController);

    function addressController($http) {
        var vm = this; //vm es de viewmodel y es para no usar this...

        vm.ClientAdresses = []; //llenar con llamada hacia la api

        vm.newAddress = {};
        
        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/clientaddress")
            .then(function (response) {
                //results when it works
                angular.copy(response.data, vm.ClientAdresses);

            }, function (error) {
                //results when it fails
                vm.errorMessage = "Failed to load data from the server: " + error;
            })
            .finally( function () {
                vm.isBusy = false;
            });

        vm.addAddressClient = function (){
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/clientaddress", vm.newAddress)
                .then(function (response) {
                    //results when it works
                    vm.ClientAdresses.push(response.data);
                    vm.newAddress = {}; //objeto vacio, limpia el form

                }, function (error) {
                    //results when it fails
                    vm.errorMessage = "Failed to save new client address. " + error;
                })
                .finally( function () {
                    vm.isBusy = false;
                });
        };

   /*     "id": 1,
    "street": "Alamo 313",
    "city": "Escobedo",
    "state": "Nuevo Leon",
    "zip": "66058",
    "intersection1": "Pino y Cedro",
    "active": true,
    "dateStamp": "2017-05-01T05:45:39.979137Z"*/

    }
})();