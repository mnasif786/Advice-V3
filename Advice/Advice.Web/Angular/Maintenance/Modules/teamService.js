angular
    .module('teamServicesModule', ['configModule'])
    .factory('teamService', function($http, $q, configService) {
        var config = configService.getConfig();

        var getAllTeamsWithDivisionAndDepartment = function() {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'teams/allTeamsWithDivisionAndDepartment/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get teams with division and department' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting teams with division and department.  Service Called: GetAllTeamsWithDivisionAndDepartment' });
                }
                console.log('error getting teams with division and department' + status);
            });

            return deferred.promise;
        };

        var editTeam = function(teamModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'teams/editTeam',
                data: teamModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to edit team. Method Called: editTeam' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error editing team ' + teamModel.Description + '. Method Called: editTeam' });
                }
                console.log('error editing team ' + teamModel.Description + ' with status ' + status);
            });

            return deferred.promise;
        };

        var addTeam = function (teamModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'teams/addTeam',
                data: teamModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to add team. Method Called: addTeam' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error adding team ' + teamModel.Description + '. Method Called: addTeam' });
                }
                console.log('error adding team ' + teamModel.Description + ' with status ' + status);
            });

            return deferred.promise;
        };

        var deleteTeam = function (teamId) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'teams/deleteTeam/' + teamId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to delete team. Method Called: DeleteTeam' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error deleting team. Method Called: DeleteTeam' });
                }
                console.log('error deleting team with status ' + status);
            });

            return deferred.promise;
        };

        var reinstateTeam = function (teamId) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'teams/reinstateTeam/' + teamId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to reinstate team. Method Called: ReinstateTeam' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error reinstating team. Method Called: ReinstateTeam' });
                }
                console.log('error reinstating team with status ' + status);
            });

            return deferred.promise;
        };

        var anyUserAssociatedWithTeam = function (teamId) {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'teams/anyUserAssociatedWithTeam/' + teamId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get user assocaiated status with team. Method Called: hasUsersAssociatedWithTeam' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting user associated status with team Method Called: hasUsersAssociatedWithTeam' });
                }
                console.log('error getting user associated status with team with error status ' + status);
            });

            return deferred.promise;
        };

        return {
            GetAllTeamsWithDivisionAndDepartment: getAllTeamsWithDivisionAndDepartment,
            
            EditTeam: editTeam,
            AddTeam: addTeam,
            AnyUserAssociatedWithTeam: anyUserAssociatedWithTeam,
            DeleteTeam: deleteTeam,
            ReinstateTeam: reinstateTeam
        };
});