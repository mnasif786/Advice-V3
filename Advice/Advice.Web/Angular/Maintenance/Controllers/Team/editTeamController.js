var editTeamController = function ($scope, $modalInstance, $window, $filter, teamService, departmentService, divisionService, TeamDetails) {
    $scope.divisionGroup = {divisions: [], selectedDivision : {}}
    $scope.departmentGroup = { departments: [], selectedDepartment: {} }
    
    $scope.team = {
        TeamId: TeamDetails.TeamId,
        Description: TeamDetails.Description,
        DepartmentId: TeamDetails.DepartmentId,
        DepartmentDescription: TeamDetails.DepartmentDescription,
        DivisionId: TeamDetails.DivisionId,
        DivisionDescription: TeamDetails.DivisionDescription
    };

    $scope.Save = function () {
        $scope.fields.team = new teamFieldValidator(teamIsNotEntered());
        var teamModel = {
            TeamId: TeamDetails.TeamId,
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

    var getAllDivisions = function() {
        var promise = divisionService.GetAllDivisions();
        promise.then(function(result) {
            if (result.success) {
                $scope.divisionGroup.divisions = result.data;
                var index = 0;
                var indexCount = 0;
                var found = false;
                angular.forEach($scope.divisionGroup.divisions, function (teamInfo) {
                    if (teamInfo.DivisionId == $scope.team.DivisionId) {
                        index = indexCount;
                        found = true;
                    }

                    indexCount++;
                });

                if (found) {
                    $scope.divisionGroup.selectedDivision = $scope.divisionGroup.divisions[index];
                }

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
                var index = 0;
                var indexCount = 0;
                angular.forEach($scope.departmentGroup.departments, function (teamInfo) {
                    if (teamInfo.DepartmentId == $scope.team.DepartmentId) {
                        index = indexCount;
                    }
                    indexCount++;
                });

                $scope.departmentGroup.selectedDepartment = $scope.departmentGroup.departments[index];

            } else {
                $window.alert('Departments could not be retrieved');
            }
        });
    };

    var init = function() {
        getAllDivisions();
        getAllDepartments();

        $scope.fields = { team: new teamFieldValidator(false) }
    }

    init();
};