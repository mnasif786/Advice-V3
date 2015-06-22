var deleteTeamController = function ($scope, $modalInstance, $window, teamService, teamDetails) {
    
    $scope.save = function (teamId) {
        $modalInstance.close(teamId);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    var areUserAssociatedWithTeam = function(teamId) {
        var promise = teamService.AnyUserAssociatedWithTeam(teamId);
        promise.then(function (result) {
            if (result.success) {
                $scope.deleteNotAllowed = result.data === "true";
            } else {
                $window.alert('Error: Associated users status could not be retrieved. Please try again later');
                $scope.cancel();
            }
        });
    };

    var init = function () {
        $scope.deleteNotAllowed = true;
        $scope.team = { "teamId": teamDetails.teamId, "teamName": teamDetails.teamName };
        areUserAssociatedWithTeam(teamDetails.teamId);
    }

    init();
};