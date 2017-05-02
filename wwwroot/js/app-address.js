//app-address.js
(function () {
    "use strict";

    //crear el module
    angular.module('app-address', ['ngRoute', 'reusableControls'])
        .config(function($routeProvider, $locationProvider){
            $locationProvider.hashPrefix('');

            $routeProvider.when('/', {
                controller: "addressController",
                controllerAs: "vm",
                templateUrl: "/views/addressView.html"
            });

            $routeProvider.when("/editor/:id",{
                controller: "addressEditorController",
                controllerAs: "vm",
                templateUrl: "/views/addressEditorView.html"
            });

            //cuando la ruta no hace match con ninguna, apuntar a la principal route.
            $routeProvider.otherwise({ redirectTo: '/'});
        });
})();