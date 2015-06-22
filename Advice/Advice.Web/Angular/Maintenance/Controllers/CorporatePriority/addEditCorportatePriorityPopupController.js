var addEditCorportatePriorityPopupController = function ($rootScope, $scope, $modalInstance,$filter, maintenanceUserService, model) {

    $scope.userList = [];
    $scope.selectedItems = {
        Id: model.Id,
        Can: model.can,
        ClientName: model.clientName,
        User: model.user,
        ContractValue: model.contractValue,
        ContractDetail: model.contractDetail,
        ContractEndDate: Date.parse(model.contractEndDate)
    }

    $scope.selectedUserUpdateDetails = function () {

    }

    $scope.save = function() {

        if (sucessfullValidation()) {
            //format chosen to make it working for IE
            $scope.selectedItems.ContractEndDate = $filter('date')($scope.selectedItems.ContractEndDate, 'EEE MMM dd yyyy');
            $modalInstance.close($scope.selectedItems);
        }
        
    }

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    //**********Private functions and classes****************//
    var loadUsers = function () {
        maintenanceUserService.GetUsers()
            .then(function (result) {
                $scope.userList = result;
            });
    }

    var isUserSelected = function () {
        return isNullOrUndefined($scope.selectedItems.User) || $scope.selectedItems.User.length <= 0;
    }

    /*Validation Object classes*/
    function requiredValidator(requiredExp) {
        this.hasError = requiredExp;
        this.errors = {
            Required: requiredExp
        }
    }

    function requiredIfValidator(requiredIfExp, requiredExp) {
        if (requiredIfExp) {
            this.hasError = requiredExp;
            this.errors = {
                Required: requiredExp
            }
        }
    }

    var sucessfullValidation = function () {
        $scope.fields.user = new requiredValidator(isUserSelected());
        $scope.fields.contractValue = new requiredIfValidator(!isNullOrUndefinedorEmpty($scope.selectedItems.ContractValue), !isDecimal($scope.selectedItems.ContractValue));

        return !$scope.fields.user.hasError &&
                    !$scope.fields.contractValue.hasError;
    }

    var init = function() {
        loadUsers();
     
        $scope.dtPickerickerSettings = new DatePickerSettings();
        $scope.fields = {
            user: new requiredValidator(false),
            contractValue: new requiredIfValidator(false)
        }
    }

    //*************************************************************//

    init();
}