var addTeamController = function ($scope, $modalInstance, $window, $filter, teamService, departmentService, divisionService) {
    $scope.divisionGroup = { divisions: [], selectedDivision: {} }
    $scope.departmentGroup = { departments: [], selectedDepartment: {} }

    $scope.team = { Description: "" };

    $scope.Save = function () {
        $scope.fields.team = new teamFieldValidator(teamIsNotEntered());
        var teamModel = {
            Description: $scope.team.Description,
            DepartmentId: $scope.departmentGroup.selectedDepartment.DepartmentId,
            DivisionId: $scope.divisionGroup.selectedDivision != null ? $scope.divisionGroup.selectedDivision.DivisionId : 0
        }

        if (!$scope.fields.team.hasError) {
            $modalInstance.close(teamModel);
        }
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    var teamIsNotEntered = function () {
        return isNullOrEmptyString($scope.team.Description);
    }

    function teamFieldValidator(required) {
        this.hasError = required;
        this.errors = {
            Required: required
        }
    }

    var getAllDivisions = function () {
        var promise = divisionService.GetAllDivisions();
        promise.then(function (result) {
            if (result.success) {
                $scope.divisionGroup.divisions = result.data;

            } else {
                $window.alert('Divisions could not be retrieved');
            }
        });
    };

    var getAllDepartments = function () {
        var promise = departmentService.GetAllDepartments();
        promise.then(function (result) {
            if (result.success) {
                $scope.departmentGroup.departments = result.data;
                $scope.departmentGroup.selectedDepartment = $scope.departmentGroup.departments[0];

            } else {
                $window.alert('Departments could not be retrieved');
            }
        });
    };

    var init = function () {
        getAllDivisions();
        getAllDepartments();

        $scope.fields = { team: new teamFieldValidator(false) }
    }

    init();
};