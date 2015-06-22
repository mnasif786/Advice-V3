var addDivisionController = function ($scope, $modalInstance) {
    
    $scope.division = { Description: "" };

    $scope.Save = function () {
        $scope.fields.division = new divisionFieldValidator(divisionIsNotEntered());
        var divisionModel = {
            Description: $scope.division.Description,            
        }

        if (!$scope.fields.division.hasError) {
            $modalInstance.close(divisionModel);
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    var divisionIsNotEntered = function () {
        return isNullOrEmptyString($scope.division.Description);
    }

    function divisionFieldValidator(required) {
        this.hasError = required;
        this.errors = {
            Required: required
        }
    }


    var init = function () {
        $scope.fields = { division: new divisionFieldValidator(false) }
    }

    init();
};