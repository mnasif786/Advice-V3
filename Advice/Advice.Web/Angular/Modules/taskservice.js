angular
    .module('taskServicesModule', ['configModule'])
    .factory('taskService', function ($http, $q, configService)
    {    
        var summaryTypeList = [];

        var config = configService.getConfig();

       /*--------------------------------------------------------------------------------------------*/
        var getAllTasksFromWebAPI = function (callback)
        {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks ' + status);
            });


            return deferred.promise;
        };
    
        /*--------------------------------------------------------------------------------------------*/
        var getTimelineForUserFromWebAPI = function (username)
        {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/timeline/user/' + username + '/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500)
                {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get timeline for user.' });
                }
                else
                {
                    deferred.resolve({ success: false, msg: 'error getting timeline for user.' });
                }
                console.log('error getting timeline for user ' + status);
            });

            return deferred.promise;
        };       

        /*--------------------------------------------------------------------------------------------*/
        var getTimelineForTeams = function (teamIdList) {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/timeline/team/teamId?' + teamIdList,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get timeline for teamIds.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting timeline for teams.' });
                }
                console.log('error getting timeline for teams ' + status);
            });

            return deferred.promise;
        }

        /*--------------------------------------------------------------------------------------------*/
        var getTimelineForTasklist = function (taskListId)
        {        
            var result = $.grep(summaryTypeList, function (x) { return x.id == taskListId; });

            return result[0].stats;
        }

        /*--------------------------------------------------------------------------------------------*/
        var getSummaryTypeList = function()
        {       
            return summaryTypeList;
        }

        /*--------------------------------------------------------------------------------------------*/
        var getTasksByTeams = function (teamIds) {

            var deferred = $q.defer();
           
            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/teamId?'+ teamIds,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });                
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var getTasksByUser = function (userName) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/teamuser/' + userName + '/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });                
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var getTasksDetailByTaskId = function (taskId) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/taskDetails/' + taskId
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };


        /*--------------------------------------------------------------------------------------------*/
        var getTaskHistoryByTaskId = function (taskId)
        {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/taskHistory/' + taskId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get history for task.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting history for task.' });
                }
                console.log('error getting history for task ' + status);
            });

            return deferred.promise;
        }



        var restoreMessageToOutlook = function(taskId, messageId) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'tasks/restoreOutlookMessage/' + taskId + '/'  + messageId
             })
                .success(function(callbackData) {
                    deferred.resolve({ success: true, data: callbackData });
                })
                .error(function(callbackData, status, headers) {
                    if (status == 500) {
                        deferred.resolve({ success: false, msg: 'An error has occured.  Unable to restore message to outlook.' });
                    } else {
                        deferred.resolve({ success: false, msg: 'error restoring message to outlook' });
                    }
                });

            return deferred.promise;
        }
        
         var deleteTaskById = function (taskId) {
            var deferred = $q.defer();

            $http({
                method: "DELETE",
                url: config.apiUrl + 'tasks/deleteTask/' + taskId
            })
                .success(function (callbackData) {
                    deferred.resolve({ success: true, data: callbackData });
                })
                .error(function (callbackData, status, headers) {
                    if (status == 500) {
                        deferred.resolve({ success: false, msg: 'An error has occured.  Unable to delete the task.' });
                    } else {
                        deferred.resolve({ success: false, msg: 'error deleting the task' });
                    }
                });

            return deferred.promise;
        }


        /*--------------------------------------------------------------------------------------------*/
        var getDeletedTasksByUser = function (userName) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/deleted/' + userName + '/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks. Method Called: getDeletedTasksByUser' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks. Method Called: getDeletedTasksByUser' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var getDeletedTasksByTeams = function (teamIds) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/deleted/teams?' + teamIds,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks. Method Called: getDeletedTasksByTeams' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks. Method Called: getDeletedTasksByTeams' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var deleteBulkTasks = function(taskIds) {
            var deferred = $q.defer();

            $http({
                method: "DELETE",
                url: config.apiUrl + 'tasks/bulkTasks?' + taskIds,
                withCredentials: true
            })
            .success(function(callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to delete tasks. Method Called: bulkDelete' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks. Method Called: bulkDelete' });
                }
                console.log('error deleting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var restoreBulkMessagesToOutlook = function (taskIds) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'tasks/restoreBulkMessagesToOutlook?' + taskIds,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to send bulk tasks to outlook. Method Called: restoreOutlookMessageInBulk' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error sending tasks to outlook. Method Called: restoreOutlookMessageInBulk' });
                }
                console.log('error sending tasks to outlook' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var reinstateTask = function (taskId) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'tasks/reinstate',
                data: taskId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to reinstate task. Method Called: reinstateTask' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error reinstate task. Method Called: reinstateTask' });
                }
                console.log('error reinstate task' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        var resetTaskSla = function (resetTaskSlaModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'tasks/resetTaskSla',
                data: resetTaskSlaModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to reset task SLA. Method Called: resetTaskSla' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error resetting task SLA. Method Called: resetTaskSla' });
                }
                console.log('error resetting task SLA' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        var reassignTask = function (reassignTaskModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'tasks/reassign',
                data: reassignTaskModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to reassign task. Method Called: reassignTask' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error reassigning task. Method Called: reassignTask' });
                }
                console.log('error reassigning task SLA' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
        var getProActiveTaskForLoggedInUser = function () {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/proactive/loggedinuser',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get proactive task timeline for loggedInUser .' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting proactive task timeline for loggedInUser.' });
                }
                console.log('error getting proactive task timeline for loggedInUser ' + status);
            });

            return deferred.promise;
        }

        /*--------------------------------------------------------------------------------------------*/
        var getTimelineAndTasksByTeams = function (teamIds) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/teams?' + teamIds,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/
       
        var getTimelineAndTasksByUser = function (userName) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/user/' + userName + '/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        var getProActiveTimelineAndTasksByUser = function (userName) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/proactive/user/' + userName + '/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        var getProactiveTimelineAndTasksByTeams = function (teamIds) {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/proactive/teams?' + teamIds,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        var getTimelinesAndTasksForLoggedInUser = function () {

            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'tasks/loggedinuser/', withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get tasks.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting tasks.' });
                }
                console.log('error getting tasks' + status);
            });

            return deferred.promise;
        };

        /*--------------------------------------------------------------------------------------------*/

        //_load();

        return {
            GetAllTasks: getAllTasksFromWebAPI,
            GetSummaryTypeList: getSummaryTypeList,
            GetTimelineForTaskList: getTimelineForTasklist,
            GetTimelineForTeams: getTimelineForTeams,
            GetTimelineForUser: getTimelineForUserFromWebAPI,
            GetTasksByTeams: getTasksByTeams,
            GetTasksByUser: getTasksByUser,
            GetTaskDetailsByTaskId: getTasksDetailByTaskId,
            GetTaskHistoryByTaskId: getTaskHistoryByTaskId,
            RestoreMessageToOutlook: restoreMessageToOutlook,
            DeleteTaskById: deleteTaskById,
            GetDeletedTasksByUser: getDeletedTasksByUser,
            GetDeletedTasksByTeams: getDeletedTasksByTeams,
            DeleteBulkTasks: deleteBulkTasks,
            RestoreBulkMessagesToOutlook: restoreBulkMessagesToOutlook,
            ReinstateTask: reinstateTask,
            ResetTaskSla: resetTaskSla,
            ReassignTask: reassignTask,
            GetProActiveTaskForLoggedInUser: getProActiveTaskForLoggedInUser,
            GetTimelineAndTasksByTeams: getTimelineAndTasksByTeams,
            GetTimelineAndTasksByUser: getTimelineAndTasksByUser,
            GetProActiveTimelineAndTasksByUser: getProActiveTimelineAndTasksByUser,
            GetProactiveTimelineAndTasksByTeams: getProactiveTimelineAndTasksByTeams,
            GetTimelinesAndTasksForLoggedInUser: getTimelinesAndTasksForLoggedInUser
            
    };

});