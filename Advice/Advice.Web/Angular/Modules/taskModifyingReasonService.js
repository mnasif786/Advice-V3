angular
    .module('taskModifyingReasonServicesModule', ['configModule'])
    .factory('taskModifyingReasonService', function ($http, $q, configService) {
        var config = configService.getConfig();

        var taskModifyingReasonsForResetGroup = null;
        var getTaskModifyingReasonsForResetGroup = function() {

            var deferred = $q.defer();
            if ($.isEmptyObject(taskModifyingReasonsForResetGroup)) {
                $http({
                        method: "GET",
                        url: config.apiUrl + 'taskModifyingReasons/getTaskModifyingReasonsForResetGroup/',
                        withCredentials: true
                    })
                    .success(function (callbackData) {
                        taskModifyingReasonsForResetGroup = callbackData;
                        deferred.resolve({ success: true, data: callbackData });
                    })
                    .error(function(callbackData, status, headers) {
                        if (status == 500) {
                            deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get modifying reasons for reset group.' });
                        } else {
                            deferred.resolve({ success: false, msg: 'error getting modifying reasons for reset group.' });
                        }
                        console.log('error getting modifying reasons for reset group ' + status);
                    });
            } else {
                deferred.resolve({ success: true, data: taskModifyingReasonsForResetGroup });
            }

            return deferred.promise;
        };

        var getTaskModifyingReasonsForReassignGroup = function() {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'taskModifyingReasons/Reassign',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get modifying reasons for reassign group. Service Called: getTaskModifyingReasonsForReassignGroup' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting modifying reasons for reassign group.  Service Called: getTaskModifyingReasonsForReassignGroup' });
                }
                console.log('error getting modifying reasons for reassign group' + status);
            });

            return deferred.promise;
        }

        return {
            GetTaskModifyingReasonsForResetGroup: getTaskModifyingReasonsForResetGroup,
            GetTaskModifyingReasonsForReassignGroup:getTaskModifyingReasonsForReassignGroup
        };
});