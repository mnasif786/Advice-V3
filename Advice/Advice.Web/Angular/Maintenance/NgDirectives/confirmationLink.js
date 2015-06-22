var confirmationLinkBoxController = function ($scope, $modalInstance, params) {

    $scope.options = params;
    $scope.yes = function () {
        $modalInstance.close();
    };

    $scope.no = function () {
        $modalInstance.dismiss('cancel');
    };
};

angular.module('maintenanceApp')
    .directive('confirmationLink', ['$modal', function ($modal) {
        return {
            restrict: 'E',
            templateUrl: 'Angular/Maintenance/Views/NgTemplates/ConfirmationLink/confirmationLink.tmpl.html',
            scope: {
                'yes': '&onYes',
                'no': '&onNo',
                'spin': '=spinVar'
            },
            link: function (scope, element, attrs) {

                scope.caption = attrs.caption;
                scope.cssClass = attrs.cssClass;
                scope.anchor = attrs.type.toLowerCase() == "anchor";
                scope.button = attrs.type.toLowerCase() == "button";
                scope.anchorWithSpinner = attrs.type.toLowerCase() == "anchorwithspinner";

                scope.openDialog = function () {
                    var options = {
                        "Title": attrs.confirmationTitle,
                        "Message": attrs.confirmationMessage
                    };

                    var modalInstance = $modal.open({
                        scope: scope,
                        templateUrl: 'Angular/Maintenance/Views/NgTemplates/ConfirmationLink/confirmationLinkBox.tmpl.html',
                        controller: confirmationLinkBoxController,
                        resolve: {
                            params: function () {
                                return options;
                            }
                        }
                    });

                    modalInstance.result.then(function () {
                        scope.spin = true;
                        scope.yes();
                    }, function () {
                        scope.no();
                    });
                }
            }
        };
    }]);