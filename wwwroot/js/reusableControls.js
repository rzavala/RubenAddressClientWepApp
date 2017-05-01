//reusableControls.js
(function () {
    "use strict";

    angular.module("reusableControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope:{
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }


})();