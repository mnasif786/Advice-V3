angular.module('dialogServicesModule', [])
    .factory('dialogService', function ($modal) {
        var showInformationDialog = function (title, message) {

            $modal.open({
                windowClass: "modal",
                templateUrl: 'Angular/Views/NgTemplates/dialogs.html',
                controller: dialogsController,
                resolve: {
                    options: function () {
                        return {
                            'Title': title,
                            'Message': message,
                            'OkButtonText': 'OK',
                            'CancelButtonText': '',
                            'ShowOkButton': true,
                            'ShowCancelButton': false
                        };
                    }
                }
            });
        };

        var showConfirmationDialog = function (title, message, yesfunction, nofunction) {

            var modalInstance = $modal.open({
                windowClass: "modal",
                templateUrl: 'Angular/Views/NgTemplates/dialogs.html',
                controller: dialogsController,
                resolve: {
                    options: function () {
                        return {
                            'Title': title,
                            'Message': message,
                            'OkButtonText': 'OK',
                            'CancelButtonText': '',
                            'ShowOkButton': true,
                            'ShowCancelButton': false
                        };
                    }
                }
            });

            modalInstance.result.then(yesfunction, nofunction);
        };

        return {
            ShowInformationDialog: showInformationDialog,
            ShowConfirmationDialog: showConfirmationDialog
        };
    });

var dialogsController = function ($rootScope, $scope, $modal, $modalInstance, options) {

    $scope.options = options;
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};