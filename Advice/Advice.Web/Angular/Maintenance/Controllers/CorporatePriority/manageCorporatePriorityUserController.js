var manageCorporatePriorityUserController = function ($scope, $window, $timeout, $modalInstance, maintenanceUserService) {
    $scope.doesUserExist = false;
    $scope.hasUserAdded = false;
    $scope.hasUserDeleted = false;
    $scope.deletedUserName = "";
    $scope.userList = [];
    $scope.selectedUser = {
        User: null
    };
    $scope.corporatePriorityUserList = [];

    var loadUsers = function () {
        maintenanceUserService.GetUsers()
            .then(function (result) {
                $scope.userList = result;
            });
    }

    var loadCorporatePriorityUsers = function () {
        var promise = maintenanceUserService.GetCorporatePriorityUsers();
        promise.then(function (result) {
            if (result.success) {
                $scope.corporatePriorityUserList = result.data;
            } else {
                $window.alert('Error: Corporate Priority Users could not be retrieved. Please try again later');
            }
        });
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.selectedUserChange = function() {
        $scope.doesUserExist = false;
        $scope.hasUserAdded = false;
    };

    $scope.addCorporatePriorityUser = function () {
        if (sucessfullValidation()) {
            var promise = maintenanceUserService.AddCorporatePriorityUser($scope.selectedUser.User.UserId);
            promise.then(function(result) {
                if (result.success) {
                    $scope.doesUserExist = result.data.UserAlreadyExist;
                    $scope.hasUserAdded = result.data.UserAdded;
                    loadCorporatePriorityUsers();
                } else {
                    $window.alert('Failed!! Corporate Priority User could not be added');
                }
            });
        }
    }

    $scope.deleteCorporatePriorityUser = function (corporatePriorityUser) {
        $scope.spinner = false;
        var promise = maintenanceUserService.DeleteCorporatePriorityUser(corporatePriorityUser.MaintenanceUserPermissionId);
        promise.then(function (result) {
            if (result.success) {
                $scope.hasUserDeleted = true;
                $scope.deletedUserName = corporatePriorityUser.UserName;
                loadCorporatePriorityUsers();
            } else {
                $window.alert('Failed!! Corporate Priority User could not be deleted');
            }

            $scope.spinner = false;
            $timeout(function () {
                $scope.hasUserDeleted = false;
            }, 2000);
        });
    };

    /*Validation Object classes*/
    function requiredValidator(requiredExp) {
        this.hasError = requiredExp;
        this.errors = {
            Required: requiredExp
        }
    }

    var isUserSelected = function () {
        return isNullOrUndefined($scope.selectedUser.User) || $scope.selectedUser.User.length <= 0;
    }

    var sucessfullValidation = function () {
        $scope.fields.user = new requiredValidator(isUserSelected());
        return !$scope.fields.user.hasError;
    }

    var init = function () {
        loadUsers();
        loadCorporatePriorityUsers();

        $scope.fields = {
            user: new requiredValidator(false)
        }
    }

    init();
};