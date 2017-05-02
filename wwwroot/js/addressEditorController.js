//addressEditorController.js
(function () {
    "use strict";

    angular.module("app-address")
        .controller("addressEditorController", addressEditorController);

        function addressEditorController($routeParams) {
            var vm = this;

            vm.id = $routeParams.id;
        }
})();