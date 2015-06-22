var genericConfirmationPopupController = function ($rootScope, $scope, $modalInstance, Options) {

    $scope.options = Options;
    $scope.yes = function () {
        $modalInstance.close();
    };

    $scope.no = function () {
        $modalInstance.dismiss('cancel');
    };
};
