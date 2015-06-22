var editDivisionController = function ($scope, $modalInstance, $window, $filter, DivisionDetails) {

    $scope.division = {
        DivisionId: DivisionDetails.DivisionId,
        Description: DivisionDetails.Description
    };    
 
    $scope.Save = function () {
        $scope.fields.division = new divisionFieldValidator(descriptionIsNotEntered());

        var divisionModel = {
            DivisionId: DivisionDetails.DivisionId,
            Description: $scope.division.Description
        }

        if (!$scope.fields.division.hasError) {
            $modalInstance.close(divisionModel);
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    var descriptionIsNotEntered = function () {
        return isNullOrEmptyString($scope.division.Description);
    }

    function divisionFieldValidator(required) {      
        this.hasError = required;
        this.errors = {
            Required: required
        }
    }

    var init = function ()
    {
        $scope.fields = { division: new divisionFieldValidator(false) };
    }


    init();
};