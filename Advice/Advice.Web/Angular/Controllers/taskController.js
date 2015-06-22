var taskController = function ($rootScope, $scope, $window, taskService, userService, configService, $filter, $timeout, $interval, $q, $modal, enums)
{
    $rootScope.checkForNewVersion($window);
    $scope.viewProActiveTask = false;
    /********** Start Of Variable Declaration Section *************/
    
    $scope.teamList         = [];
    $scope.userList = [];
    $scope.allUsersList = [];

   
    $scope.selectedTeams = [];
    $scope.selectedUser = null;

    
    $scope.teamTimelineTotals = "";
    $scope.userTimelineTotals = "";
    $scope.loggedInUserTimelineTotals = [];

    $scope.selectedTeamsIds = "";
    $scope.taskDetailsData = "";
    $scope.taskTypes = "";
    $scope.selectedGridItemIndex = null;
    
    $scope.hasUserDeletePermission = false;
    $scope.hasUserResetPermission = false;

    var slaBallTypes = { Red: 'Red', Amber: 'Amber', Green: 'Green', Platinum: 'Platinum' };
    var taskListType = { None: 'None', Team: 'Team', User: 'User' };
    $scope.selectedTaskListType = taskListType.None; //By default None
    $scope.selectedProactiveTaskListType = taskListType.None; //By default None
    $scope.filterTypes = { Red: 'Red', Amber: 'Amber', Green: 'Green', Platinum: 'Platinum', None: 'None', Delete: 'Delete' };
    $scope.currentFilterType = $scope.filterTypes.None;
    $scope.currentProActiveFilterType = $scope.filterTypes.None;
    $scope.preSelectedFilterType = $scope.filterTypes.None;
    $scope.preSelectedTaskSearchResultData = null;    
    var bulkDelete = false;
    var bulkTasksSendToOutlook = false;
    $scope.taskSelection = [];
    var dropDownChosenType = { BulkDelete: 'BulkDelete', BulkReassign: 'BulkReassign', SendToOutlook: 'SendToOutlook', ShowDeleted: 'ShowDeleted', ClearFilters:'ClearFilters', HideDeleted:'HideDeleted'};

  

    // SG: FOR DEV ONLY
    $scope.dev_tasklist = "";

    $scope.advice2CompleteNotRemove = '';
    $scope.advice2CompleteAndRemove = '';

    /********** End Of Variable Declaration section *************/

    $scope.isCancelledTask = function (item) {
        if (item && (item.Cancelled == true && item.Deleted == false && item.Completed == false))
            return true;

        return false;
    }

    $scope.setDropDowntype = function (dropDownSelection) {
        if (dropDownSelection == dropDownChosenType.BulkDelete)
            $scope.taskSelection = []; //when bulk switched after selection
            bulkTasksSendToOutlook = false;
            setBulkDelete();
        if (dropDownSelection == dropDownChosenType.BulkReassign) {
            bulkDelete = false;
            bulkTasksSendToOutlook = false;
        }
        if (dropDownSelection == dropDownChosenType.SendToOutlook) {
            $scope.taskSelection = []; //when bulk switched after selection
            bulkDelete = false;
            setBulkTasksSendToOutlook();
        }
        if (dropDownSelection == dropDownChosenType.ClearFilters) {
            bulkDelete = false;
            bulkTasksSendToOutlook = false;
        }
        if (dropDownSelection == dropDownChosenType.ShowDeleted) {
            bulkDelete = false;
            bulkTasksSendToOutlook = false;
            $scope.ShowDeletedTasks();
            
        }
        if (dropDownSelection == dropDownChosenType.HideDeleted) {
            bulkDelete = false;
            bulkTasksSendToOutlook = false;
            $scope.HideDeletedTasks();
        }
    }

    var setBulkDelete = function () {
        
        if ($scope.taskSearchResultData && $scope.taskSearchResultData.length > 0)
            bulkDelete = !bulkDelete;
    }

    $scope.getBulkDelete = function() {
         return bulkDelete;
    }

    var setBulkTasksSendToOutlook = function () {

        if ($scope.taskSearchResultData && $scope.taskSearchResultData.length > 0 )
            bulkTasksSendToOutlook = !bulkTasksSendToOutlook;
    }

    $scope.getBulkTasksSendToOutlook = function () {
        return bulkTasksSendToOutlook;
    }

    var setCurrentFilterType = function(type) {
        $scope.currentFilterType = type;
    }

    var setCurrentProactiveFilterType = function (type) {
        $scope.currentProActiveFilterType = type;
    }
    
    $scope.getTaskListType = function() {
        return $scope.selectedTaskListType;
    };
    
    var updateLastTaskListSelection = function (selection) {
        $scope.selectedTaskListType = taskListType[selection];
    };

    var updateLastProactiveTaskListSelection = function (selection) {
        $scope.selectedProactiveTaskListType = taskListType[selection];
    };

    

    $scope.updateSelectedTeamIds = function() {
        $scope.selectedTeamsIds = "";
        angular.forEach($scope.selectedTeams, function (team, key) {
            $scope.selectedTeamsIds = constructQueryString($scope.selectedTeamsIds) + team.TeamId;
        });
    };

    var getSelectedTeamIds = function () {
        var selectedTeamsIds = "";
        angular.forEach($scope.selectedTeams, function (team, key) {
            selectedTeamsIds = constructQueryString(selectedTeamsIds) + team.TeamId;
        });
        return selectedTeamsIds;
    };

    var getSelectedUserName= function() {
        var userName = '';
        if (!isNullOrUndefined($scope.selectedUser) && !isNullOrEmptyString($scope.selectedUser.Username)) {
            userName = $scope.selectedUser.Username;
        }
        return userName;
    }
    
    $scope.selectedTeamsUpdateDetails = function () {
        
        if ($scope.selectedTeams.length > 0) {
            getTimelineAndTasksByTeams(false, $scope.currentFilterType);
            getProActiveTimelinesAndTasksByTeams(false, $scope.currentProActiveFilterType);

            showUsersBySelectedTeams($scope.selectedTeams);
        } else {

            //Resets the Last Selection to none if Last slection is Team type.
            if ($scope.selectedTaskListType == taskListType.Team) {
                updateLastProactiveTaskListSelection(taskListType.None);
                updateLastTaskListSelection(taskListType.None);
            }

            //usual tasks
            $scope.teamTasksSearchResult = {};
            $scope.SelectedTasks[0] = null;

            if ($scope.selectedTaskListType != taskListType.User) {
                $scope.SelectedTasks[0] = null;
                clearTaskGridAndTaskDetailData();
            }
            

            //Proactive tasks
            $scope.teamsProActiveTaskSearchResult = {}; //clears the actual proactive search result data for team(s).
            $scope.SelectedProactiveTasks[0] = null;

            if ($scope.selectedTaskListType != taskListType.User) {
                $scope.SelectedProactiveTasks[0] = null;
                setProactiveTaskGridDataToDefault();
            }

            //refreshes the user dropdown
            ShowAllUsers();
        }
    }

    //Consultant dropdown
    var showUsersBySelectedTeams= function(teams) {
        userService.GetUsersByTeams(teams)
            .then(function(userList) {
                $scope.userList = userList;
                addMyTaskListToUserList();

                

            //if user is already selected but not found in the updated/new userlist then update selected user and userTimelineTotals to empty
            if (!isNullOrUndefined($scope.selectedUser)) {
                var found = false;

                if ($scope.selectedUser.DisplayName == 'My Task List') {
                    $scope.selectedUser = $scope.userList[0];
                    return;
                }
                
                angular.forEach($scope.userList, function (user, key) {
                    if (user.UserId == $scope.selectedUser.UserId) {
                        found = true;
                        //Below commented could is a hack to show the checkbox checked in the User dropdown. However it put empty space at end.
                        //User dropdown shows unchecked value until object that last bind to it has a change value. Thats y and empty space is added at the end.
                        //As this is not part of a requirement so leve this code but commented it out.
                        //$scope.userList[key].DisplayName = $scope.userList[key].DisplayName + ' ';

                        $scope.selectedUser = $scope.userList[key]; // $scope.userList[key];
                    }
                });
                
                if (!found) {
                    $scope.selectedUser = null;
                    //$scope.userTasksSearchResult = {};
                    resetUserView();
                } 
            }

        });
    }

    //$scope.checkUserUnchecked = function() {
    //    if (!$scope.selectedUser) {
    //        $scope.userTasksSearchResult = {};
    //        $scope.userProActiveTaskSearchResult = {}; //clears the actual proactive search result data for user.
    //        setProactiveTaskGridDataToDefault();
    //    }
    //};

    var resetUserView = function() {
        //Resets the Last Selection to none if Last slection is User type.
        if ($scope.selectedTaskListType == taskListType.User) {
            updateLastProactiveTaskListSelection(taskListType.None);
            updateLastTaskListSelection(taskListType.None);
        }

        //Usual Tasks
        $scope.userTasksSearchResult = {};


        if ($scope.selectedTaskListType != taskListType.Team) {
            $scope.SelectedTasks[0] = null;
            clearTaskGridAndTaskDetailData();
        }

        

        //Proactive tasks
        $scope.userProActiveTaskSearchResult = {}; //clears the actual proactive search result data for user.

        if ($scope.selectedTaskListType != taskListType.Team) {
            $scope.SelectedProactiveTasks[0] = null;
            setProactiveTaskGridDataToDefault();
        }
    }

    // Hack because the multiselect dropdown doesn't fire selectedUserUpdateDetails() when item unchecked
    $scope.$watch("selectedUser", function () {
        if (!$scope.selectedUser) {
            resetUserView();
        }
    });

    $scope.selectedUserUpdateDetails = function ()
    {
        if (!isNullOrUndefined($scope.selectedUser) && !isNullOrEmptyString($scope.selectedUser.Username)) {
           
            getTimelineAndTasksByUser(false, $scope.currentFilterType);
            getProActiveTimelinesAndTasksByUser(false, $scope.currentProActiveFilterType);
        } else {
            //issue with dropdown--multiselect dropdown doesn't fire selectedUserUpdateDetails() when item unchecked 
            //so, $scope.$watch("selectedUser", function () function is called.
            
        }
    }
    
    $scope.showTasklistForUser = function (filterType) {
       // $scope.viewProActiveTask = false;

        // dont filter when no user selected
        if ($scope.selectedUser == null)
            return;

        setCurrentFilterType(filterType);
        updateLastTaskListSelection(taskListType.User);
        setTaskGridData(applySlaBallFilter(filterType, $scope.userTasksSearchResult.Tasks));
        clearTaskDetailData();
    }
  
   $scope.showTasklistForTeam = function (filterType) {
       //$scope.viewProActiveTask = false;

       // dont filter when no teams selected
       if ($scope.selectedTeams.length == 0)
           return;

       setCurrentFilterType(filterType);
       updateLastTaskListSelection(taskListType.Team);
       setTaskGridData(applySlaBallFilter(filterType, $scope.teamTasksSearchResult.Tasks));
       clearTaskDetailData();
   }

   $scope.showProactiveTasklistForUser = function (filterType) {
       //$scope.viewProActiveTask = true;

       // dont filter when no user selected
       if ($scope.selectedUser == null)
           return;

       setCurrentProactiveFilterType(filterType);
       updateLastProactiveTaskListSelection(taskListType.User);
       setProactiveTaskGridData(applySlaBallFilter(filterType, $scope.userProActiveTaskSearchResult.Tasks));
       clearTaskDetailData();
   }

   $scope.showProactiveTasklistForTeam = function (filterType) {
       //$scope.viewProActiveTask = true;

       // dont filter when no teams selected
       if ($scope.selectedTeams.length == 0)
           return;

       setCurrentProactiveFilterType(filterType);
       updateLastProactiveTaskListSelection(taskListType.Team);
       setProactiveTaskGridData(applySlaBallFilter(filterType, $scope.teamsProActiveTaskSearchResult.Tasks));
       clearTaskDetailData();
   }

   var applySlaBallFilter = function(filterType, dataToApply) {
       if (filterType == $scope.filterTypes.None) {
           return dataToApply;
       }
       return $filter('filter')(dataToApply, { "Status": slaBallTypes[filterType] }, true);
   }

    var constructQueryString = function(value) {
        if (value === "") {
            return "id=" + value;
        } else {
            return value +"&id=";
        }
    }

    var updateDropDownlists = function () {

        /* TEAM LIST*/
        userService.GetTeams()
            .then(function (data)
            {
                $scope.teamList = data;

                var loggedInUser = $rootScope.loggedInUser;
                
                if (!isNullOrUndefined(loggedInUser) && loggedInUser.Permissions.ViewOtherTaskLists) {
                    var team = angular.copy(loggedInUser.Team);
                    team.Description = "My Team";
                    team.IsSpecial = true;
                    $scope.teamList.splice(0, 0, team);
                }
            });        

        
        /* USER LIST */
        ShowAllUsers();
    }

    /* USER LIST */
    var ShowAllUsers = function ()
    {
        var promise = userService.GetUsers();
        promise.then(function (result) {
            
            $scope.userList = result;
            $scope.allUsersList = result;

            addMyTaskListToUserList();
            
        });
    }

    var addMyTaskListToUserList = function () {

        if ($scope.userList.length > 0 && $scope.userList[0].DisplayName != 'My Task List') {

            var loggedInUser = $rootScope.loggedInUser;

            if (!isNullOrUndefined(loggedInUser) && loggedInUser.Permissions.ViewOtherTaskLists) {
                var user = angular.copy(loggedInUser);
                user.DisplayName = 'My Task List';
                $scope.userList.splice(0, 0, user);
            }
        }
    }

    $scope.RestoreMessageToOutlook = function (taskDetails) {
        if ($scope.activityMonitor.TaskSendToOutlookkInProgress == false) {
            console.log('RestoreMessageToOutlook called.');
            if (taskDetails.EmailTask == null) {
                $window.alert('Task is not an Email Task');
                return;
            }

            $scope.activityMonitor.TaskSendToOutlookkInProgress = true;

            var promise = taskService.RestoreMessageToOutlook(taskDetails.TaskId, taskDetails.EmailTask.MessageId);
            promise.then(function (result) {
                $scope.activityMonitor.TaskSendToOutlookkInProgress = false;
                if (result.success) {
                    updateUI();
                } else {
                    $window.alert('Email was not restored to outlook');
                }
            });
        }
    };

    $scope.DeleteTaskById = function (taskId, viewProActiveTask) {
        var options = {
            "Heading": 'Confirm',
            "Message": 'Are you sure you want to delete this task?',
            "ShowYesButton": true,
            "ShowNoButton": true
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/NgTemplates/genericConfirmationPopup.html',
            controller: genericConfirmationPopupController,
            resolve: {
                Options: function () {
                    return options;
                }
            }
        });

        modalInstance.result.then(function() {
            //Yes clicked
            $scope.activityMonitor.TaskDeleteInProgress = true;
            var promise = taskService.DeleteTaskById(taskId);

            promise.then(function (result) {
                if (result.success) {
                    clearTaskDetailData();
                    if (viewProActiveTask) {
                        //getProActiveTasksForLoggedInUser();  //Refreshes the data
                        clearSelectedProactiveTaskDetail();
                        refreshProactiveTimelinesAndTasks();
                    }
                    else {
                        clearSelectedTaskDetail();
                        refreshTimelinesAndTasks();
                        //updateUI();
                    }

                } else {
                    $window.alert('Task could not be deleted');
                }

                $scope.activityMonitor.TaskDeleteInProgress = false;
            });
        }, function() {
            //No clicked.
        });
    };

    var updateUI = function () {

        var selectedTask = $scope.SelectedTasks[0]; //First item in the array of currenty Selected tasks
        $scope.taskDetailsData = null; //Task Detail section would be gone
        $scope.taskSearchResultData.splice($scope.selectedGridItemIndex, 1); //Removes from the current task collection

        //Updates the timeline counters
        updateTimelineCounters(selectedTask.Status, $scope.teamTimelineTotals);
        updateTimelineCounters(selectedTask.Status, $scope.userTimelineTotals);
    }

    var updateTimelineCounters = function (status, timelineTotals) {
        if (timelineTotals != '' && timelineTotals.Total > 0) {
            timelineTotals.Total--;
            switch (status) {
                case 'Red':
                    timelineTotals.Red--;
                    break;
                case 'Green':
                    timelineTotals.Green--;
                    break;
                case 'Amber':
                    timelineTotals.Amber--;
                    break;
                case 'Platinum':
                    timelineTotals.Platinum--;
                    break;
                default:
            }
        }
    }

    $scope.resetSla = function (taskDetailsData) {

        var taskDetails = {
            "TaskId": taskDetailsData.TaskId,
            "DueDate": taskDetailsData.DueDate
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/Partials/Forms/_resetSlaForm.html',
            controller: resetSlaController,
            resolve: {
                TaskDetails: function () {
                    return taskDetails;
                }
            }
        });

        modalInstance.result.then(function (resetTaskSlaModel) {
            //Reset Task button clicked
            var data = resetTaskSlaModel;
            var promise = taskService.ResetTaskSla(resetTaskSlaModel);
            promise.then(function (result) {
                if (result.success) {
                    getTaskDetailsByTaskId(resetTaskSlaModel.TaskId);
                } else {
                    $window.alert('Sorry!! Task SLA could not be reset');
                }
            });
        }, function () {
            //Cancel clicked.
        });
    };


    var getTaskDetailsByTaskId = function (taskId) {
        $scope.taskDetailsData = null;
        var promise = taskService.GetTaskDetailsByTaskId(taskId);
        promise.then(function (result) {
            if (result.success) {
                $scope.taskDetailsData = result.data;
                getUserDeletePermission($scope.taskDetailsData);
                getUserResetPermission();

                var completeButtonsUrl = getAdvice2CompleteButtonsUrl($scope.taskDetailsData);
                $scope.advice2CompleteNotRemove = completeButtonsUrl;
                $scope.advice2CompleteAndRemove = completeButtonsUrl + "&CompleteTaskOnActionSave=true&AvVersion=3";
            }
        });
    }

    var getAdvice2CompleteButtonsUrl = function(taskDetail) {
        var url = configService.getConfig().advice2Root;
        if (taskDetail.RecordedClientId != null) {
            url = url + "ClientDetailSummary.aspx?TaskID=" + taskDetail.TaskId + "&CustomerID=" + taskDetail.RecordedClientId;
        } else {
            url = url + "ClientSearch.aspx?TaskID=" + taskDetail.TaskId;
        }
        return url;
    }

    $scope.getTaskHistoryByTaskId = function (taskId) {
        $scope.taskHistory = null;
        var promise = taskService.GetTaskHistoryByTaskId(taskId);
        promise.then(function (result) {
            if (result.success) {
                $scope.taskHistory = result.data;
                $('#taskHistoryModal').modal('show');
            }
        });
    }
    
    //Array of currently Selected tasks
    $scope.SelectedTasks = [{ "TaskId": 0 }];

    $scope.$watch("SelectedTasks[0].TaskId", function () {
        getTaskDetail($scope.SelectedTasks[0]);
    });

    $scope.setSelectedTaskIndex = function (gridItem) {
        
        $scope.selectedGridItemIndex = $scope.taskSearchResultData.indexOf(gridItem);
        gridItem.IsRead = true;
    }
 
    var setTaskGridData = function(data) {
        $scope.taskSearchResultData = data;
    };

    var setProactiveTaskGridData = function (data) {
        $scope.proactiveTaskSearchResultData = data;
    };
    
   $scope.ShowDeletedTasks = function() {
        if ($scope.selectedTaskListType != taskListType.None) {

            //Data already bound to Grid
            $scope.preSelectedTaskSearchResultData = $scope.taskSearchResultData;

            switch ($scope.selectedTaskListType) {
                case 'Team':
                    $scope.ShowDeletedTasksByTeams();
                    break;
                case 'User':
                    $scope.ShowDeletedTasksByUser();
                    break;
                default:
            }

            $scope.preSelectedFilterType = $scope.currentFilterType == null ? $scope.filterTypes.None : $scope.currentFilterType;
            
            $scope.taskDetailsData = null; //Sets Detail Pannel to null
        }
    }

    $scope.HideDeletedTasks = function () {
        //Toggling --Show data in a previous state that was before applying delete filter.
        if ($scope.selectedTaskListType != taskListType.None) {

            $scope.preSelectedFilterType = $scope.filterTypes.None;

            //Restores the original i.e. data and filter type
            setCurrentFilterType($scope.preSelectedFilterType);
            setTaskGridData($scope.preSelectedTaskSearchResultData);

            $scope.taskDetailsData = null; //Sets Detail Pannel to null
        }
    }

    $scope.ShowDeletedTasksByTeams = function () {

        var teamIds = getSelectedTeamIds();
        if (teamIds == "") return;

        setCurrentFilterType($scope.filterTypes.Delete);

        var promise = taskService.GetDeletedTasksByTeams(teamIds);
        promise.then(function (result) {
            if (result.success) {
                setTaskGridData(result.data);
            }
        });
    }

    $scope.ShowDeletedTasksByUser = function () {
        
        if ($scope.selectedUser == null) return;

        setCurrentFilterType($scope.filterTypes.Delete);
        var promise = taskService.GetDeletedTasksByUser($scope.selectedUser.Username);
        promise.then(function (result) {
            if (result.success) {
                setTaskGridData(result.data);
            }
        });
    }

    $scope.deleteBulkTasks = function () {
        var options = {
            "Heading": 'Confirm',
            "Message": 'Are you sure you want to delete these tasks?',
            "ShowYesButton": true,
            "ShowNoButton": true
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/NgTemplates/genericConfirmationPopup.html',
            controller: genericConfirmationPopupController,
            resolve: {
                Options: function() {
                    return options;
                }
            }
        });

        modalInstance.result.then(function() {
            if ($scope.taskSelection.length > 0) {
               var taskIds = "";
                angular.forEach($scope.taskSelection, function(task, key) {
                    taskIds = constructQueryString(taskIds) + task.TaskId;
                });

                var promise = taskService.DeleteBulkTasks(taskIds);
                promise.then(function(result) {
                    if (result.success) {
                        $scope.clearTasks();
                        $scope.refreshTaskList();
                       
                    } else {
                        $window.alert('Tasks could not be deleted');
                    }
                }), function() {};
            }
        });
    };

    $scope.sendBulkTasksToOutlook = function () {
        var options = {
            "Heading": 'Confirm',
            "Message": 'Are you sure you want to send the selected tasks to outlook?',
            "ShowYesButton": true,
            "ShowNoButton": true
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/NgTemplates/genericConfirmationPopup.html',
            controller: genericConfirmationPopupController,
            resolve: {
                Options: function () {
                    return options;
                }
            }
        });

        modalInstance.result.then(function () {
            if ($scope.taskSelection.length > 0) {
                var taskIds = "";
                angular.forEach($scope.taskSelection, function (task, key) {
                    taskIds = constructQueryString(taskIds) + task.TaskId;
                });

                var promise = taskService.RestoreBulkMessagesToOutlook(taskIds);
                promise.then(function (result) {
                    if (result.success) {
                        $scope.clearTasks();
                        $scope.refreshTaskList();

                    } else {
                        $window.alert('Tasks could not be restored to outlook');
                    }
                });
            }
        }, function() {
                
            });
    };

    $scope.toggleSelection = function(taskItem) {
        var idx = $scope.taskSelection.indexOf(taskItem);
        
        if (idx > -1) {
            $scope.taskSelection.splice(idx, 1);
        }
        else {
            $scope.taskSelection.push(taskItem);
        }
    }

    $scope.clearTasks = function() {
        $scope.taskSelection = [];
        if (bulkDelete) {
            setBulkDelete();
        }
        if (bulkTasksSendToOutlook) {
            setBulkTasksSendToOutlook();
        }
    }

    var getUserDeletePermission = function (task) {
        var loggedInUser = $rootScope.loggedInUser;
        var deleteTaskOwn = loggedInUser.Permissions.DeleteTaskOwn; 
        var deleteTaskOther = loggedInUser.Permissions.DeleteTaskOtherUser; 

        if (
            (deleteTaskOwn && !$.isEmptyObject(task.AssignedUser) && task.AssignedUser.toLowerCase() == loggedInUser.Username.toLowerCase()) ||
                                                                                                                                            deleteTaskOther) {
            $scope.hasUserDeletePermission = true;
        }
    }

    var getUserResetPermission = function () {
        var loggedInUser = $rootScope.loggedInUser;
        var hasResetPermission = loggedInUser.Permissions.ResetTask;

        if (hasResetPermission) {
            $scope.hasUserResetPermission = true;
        }
    }
    
    $scope.checkUserHasNotDeletePermissions = function () {
        var loggedInUser = $rootScope.loggedInUser;

        if (!loggedInUser)
            return false;

       if (loggedInUser.Permissions.DeleteTaskOwn || loggedInUser.Permissions.DeleteTaskOtherUser)
            return false;
        else {
            return true;
        }
    }

    $scope.bulkDeleteCheckBoxDisable = function(task) {
        var loggedInUser = $rootScope.loggedInUser;

        // delete own task permissions
        if (task.AssignedUser && (task.AssignedUser.toLowerCase() == loggedInUser.Username.toLowerCase()) && loggedInUser.Permissions.DeleteTaskOwn)
            return false;
        // delete other tasks permissions
        if (task.AssignedUser && (task.AssignedUser.toLowerCase() != loggedInUser.Username.toLowerCase()) && loggedInUser.Permissions.DeleteTaskOtherUser)
            return false;
        // task assigned to user is null or team and delete others permission
        if (!task.AssignedUser && loggedInUser.Permissions.DeleteTaskOtherUser)
            return false;

        // disable by default
        return true;
    };

    $scope.bulkSendToOutlookCheckBoxDisable = function (task) {

        if (task.TaskTypeId == enums.taskTypes.Email || task.TaskTypeId == enums.taskTypes.CCEmail || task.TaskTypeId == enums.taskTypes.InternalEmail || task.TaskTypeId == enums.taskTypes.InternalCCEmail) {
            return false;
        }

        //default disabled
        return true;
    };

    $scope.ReinstateTask = function (taskId) {
        var promise = taskService.ReinstateTask(taskId);
        promise.then(function(result) {
            if (result.success) {
                $scope.taskDetailsData = null; //To Clear Task Detail section
                $scope.taskSearchResultData.splice($scope.selectedGridItemIndex, 1); //Removes from the current task collection
            }
        });
    }

    $scope.ReAssignTask = function (taskId, dueDate, urgent) {
        var taskDetails = {
            "taskId": taskId,
            "dueDate": dueDate
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/Partials/Forms/_reAssignTask.html',
            controller: reAssignTaskController,
            resolve: {
                taskDetails: function () 
                {
                    return taskDetails;
                },
                urgent: function () 
                {
                    return urgent;
                },
                teams: function() 
                {
                    return $scope.teamList;
                },
                users: function() 
                {
                    return $scope.allUsersList;                
                }
            }
        });
        modalInstance.result.then(function (reassignTaskModel) {
            
            var promise = taskService.ReassignTask(reassignTaskModel);
            promise.then(function (result) {
                if (result.success) {
                    getTaskDetailsByTaskId(reassignTaskModel.TaskId);
                } else {
                    $window.alert('Sorry!! Task could not be reassigned');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };
    
    var clearTaskGridAndTaskDetailData= function ()
    {
        $scope.taskSearchResultData = null; //Clear grid Data
        $scope.taskDetailsData = null; //clear Task Detail Data
    }

    var clearTaskDetailData = function () {
        $scope.taskDetailsData = null; //clear Task Detail Data
    }

    var clearTaskGridData= function() {
        $scope.taskSearchResultData = null;
    }

    var setProactiveTaskGridDataToDefault = function() {
        $scope.proactiveTaskSearchResultData = $scope.proActiveTaskSearchResult.Tasks; //LoogedIn User Data
    }

    var clearSelectedProactiveTaskDetail = function() {
        $scope.SelectedProactiveTasks[0] = null;
    }
    
    var clearSelectedTaskDetail = function () {
        $scope.SelectedTasks[0] = null;
    }


    ///Pro Active Tasks section Starts/////
    var getProActiveTasksForLoggedInUser = function (updateGridData) {

        $scope.activityMonitor.UserProActiveTasksSearchInProgress = true;
        var promise = taskService.GetProActiveTaskForLoggedInUser();
        promise.then(function (result) {
            $scope.proActiveTaskSearchResult = result.data;
            if (updateGridData) {
                setProactiveTaskGridData($scope.proActiveTaskSearchResult.Tasks);
            }
            $scope.activityMonitor.UserProActiveTasksSearchInProgress = false;
        });
    }

    var getProActiveTimelinesAndTasksByTeams = function (updateGridData, filterType) {
        var teamIds = getSelectedTeamIds();
        if (!isNullOrEmptyString(teamIds)) {
            $scope.activityMonitor.TeamProActiveTasksSearchInProgress = true;
            var promise = taskService.GetProactiveTimelineAndTasksByTeams(teamIds);
            promise.then(function (result) {
                $scope.teamsProActiveTaskSearchResult = result.data;
                if (updateGridData) {
                    setProactiveTaskGridData(applySlaBallFilter(filterType, $scope.teamsProActiveTaskSearchResult.Tasks));
                }
                $scope.activityMonitor.TeamProActiveTasksSearchInProgress = false;
            });
        }
    }

    var getProActiveTimelinesAndTasksByUser = function (updateGridData, filterType) {
        var userName = getSelectedUserName();
        if (!isNullOrEmptyString(userName)) {
            $scope.activityMonitor.ProActiveTasksSearchInProgress = true;
            var promise = taskService.GetProActiveTimelineAndTasksByUser(userName);
            promise.then(function (result) {
                $scope.userProActiveTaskSearchResult = result.data;
                if (updateGridData) {
                    setProactiveTaskGridData(applySlaBallFilter(filterType, $scope.userProActiveTaskSearchResult.Tasks));
                }
                $scope.activityMonitor.ProActiveTasksSearchInProgress = false;
            });
        }
    }

    //Array of currently Selected proactive tasks
    $scope.SelectedProactiveTasks = [{ "TaskId": 0 }];
    $scope.selectedProActiveTaskGridIndex = null;
    
    $scope.$watch("SelectedProactiveTasks[0].TaskId", function () {
        getTaskDetail($scope.SelectedProactiveTasks[0]);
    });

    $scope.setSelectedProActiveTaskIndex = function (gridItem) {
        $scope.selectedProActiveTaskGridIndex = $scope.proactiveTaskSearchResultData.indexOf(gridItem);
        gridItem.IsRead = true;
    }

    
    $scope.$watch("viewProActiveTask", function () {
        clearTaskDetailData();
        if ($scope.viewProActiveTask) {
            getTaskDetail($scope.SelectedProactiveTasks[0]);
        } 
        else {
            getTaskDetail($scope.SelectedTasks[0]);
        }
    });
    ///Pro Active Tasks section ends/////

    var getTaskDetail= function(task) {
        if (task && task.TaskId > 0) {
            getTaskDetailsByTaskId(task.TaskId);
        }
    }


    var getTimelineAndTasksByTeams = function (updateGridData, filterType) {
        console.log('getTimelineAndTasksByTeams called.');

        var teamIds = getSelectedTeamIds();

        if (!isNullOrEmptyString(teamIds)) {
            $scope.activityMonitor.TasksSearchByTeamInProgress = true;

            var promise = taskService.GetTimelineAndTasksByTeams(teamIds);
            promise.then(function(result) {
                if (result.success) {
                    $scope.teamTasksSearchResult = result.data;
                    if (updateGridData) {
                        setTaskGridData(applySlaBallFilter(filterType,$scope.teamTasksSearchResult.Tasks));
                    }
                    $scope.activityMonitor.TasksSearchByTeamInProgress = false;

                }
            });
        }
    }

    var getTimelineAndTasksByUser = function (updateGridData, filterType) {
        console.log('getTimelineAndTasksByUser called.');

        var userName = getSelectedUserName();

        if (!isNullOrEmptyString(userName)) {
            $scope.activityMonitor.TasksSearchByUserInProgress = true;

            var promise = taskService.GetTimelineAndTasksByUser(userName);
            promise.then(function(result) {
                if (result.success) {
                    $scope.userTasksSearchResult = result.data;
                    if (updateGridData) {
                        setTaskGridData(applySlaBallFilter(filterType, $scope.userTasksSearchResult.Tasks));
                    }
                    $scope.activityMonitor.TasksSearchByUserInProgress = false;

                }
            });
        }
    }

    var getTimelinesAndTasksforLoggedInUser = function () {

        $scope.activityMonitor.TasksSearchByUserInProgress = true;

        var promise = taskService.GetTimelinesAndTasksForLoggedInUser();
        promise.then(function (result) {
            if (result.success) {
                $scope.userTasksSearchResult = result.data;
                setTaskGridData($scope.userTasksSearchResult.Tasks);
                updateLastTaskListSelection(taskListType.User);
                $scope.activityMonitor.TasksSearchByUserInProgress = false;
                $scope.selectedUser = $scope.userList[0]; //Selects My Task List
            }
        });
    }

    $scope.refresh = function () {

        refreshTimelinesAndTasks();

        refreshProactiveTimelinesAndTasks();

    }

    var refreshTimelinesAndTasks = function () {

        var teamTaskListTypeSelected = $scope.selectedTaskListType == taskListType.Team;
        var userTaskListTypeSelected = $scope.selectedTaskListType == taskListType.User;
        var deleteFilterApplied = $scope.currentFilterType == $scope.filterTypes.Delete;

        if (teamTaskListTypeSelected && deleteFilterApplied) {
            $scope.ShowDeletedTasksByTeams();
        } else if (userTaskListTypeSelected && deleteFilterApplied) {
            $scope.ShowDeletedTasksByUser();
        }

        //Update timelines only and not grid if delete filter is applied
        getTimelineAndTasksByTeams(teamTaskListTypeSelected && !deleteFilterApplied, $scope.currentFilterType);
        //Update timelines only and not grid if delete filter is applied
        getTimelineAndTasksByUser(userTaskListTypeSelected && !deleteFilterApplied, $scope.currentFilterType);
    }

    var refreshProactiveTimelinesAndTasks = function () {
        //proactive stuff
        var teamProactiveTaskListTypeSelected = $scope.selectedProactiveTaskListType == taskListType.Team;
        var userProactiveTaskListTypeSelected = $scope.selectedProactiveTaskListType == taskListType.User;

        //var selectedTeamIds = getSelectedTeamIds();
        //var selectedUser = getSelectedUserName();

        //if (selectedProactiveTaskListTypeCriteria) {
        getProActiveTasksForLoggedInUser(!teamProactiveTaskListTypeSelected && !userProactiveTaskListTypeSelected);
        //} else {
        //    getProActiveTasksForLoggedInUser(isNullOrEmptyString(selectedTeamIds) && isNullOrEmptyString(selectedUser));
        //}

        getProActiveTimelinesAndTasksByTeams(teamProactiveTaskListTypeSelected, $scope.currentProActiveFilterType);
        getProActiveTimelinesAndTasksByUser(userProactiveTaskListTypeSelected, $scope.currentProActiveFilterType);
    }

    var _init = function () {

        $scope.activityMonitor = {
            "TaskSendToOutlookkInProgress": false, //flag to animate buttons that perfom some invidual actions on task i.e. SendToOutlook, Delete etc
            "TasksSearchByTeamInProgress": false, //flag to animate timeline balls
            "TasksSearchByUserInProgress": false, //flag to animate timeline balls
            "TeamTimeLineSearchInProgress": false,
            "UserTimeLineSearchInProgress": false,
            "ProActiveTasksSearchInProgress": false,
            "TeamProActiveTasksSearchInProgress": false,
            "UserProActiveTasksSearchInProgress": false,
            "TaskDeleteInProgress": false
        };
        
        updateDropDownlists();

        getProActiveTasksForLoggedInUser(true);

        $scope.templates = configService.getTemplates();

        $scope.apiUrl = configService.getConfig().apiUrl;

        $scope.teamTaskSearchResultFromDatabase = null;
        $scope.userTaskSearchResultFromDatabase = null;

        $scope.teamTasksSearchResult = {}
        $scope.userTasksSearchResult = {}
        $scope.proActiveTaskSearchResult = {};
        
        
        $scope.taskSearchResultData = null;
        $scope.taskDetailsData = null;


        getTimelinesAndTasksforLoggedInUser();

        $interval($scope.refresh, configService.getConfig().automaticRefreshTime);
        //$interval($scope.refresh, 10000);
       
    };

    _init();
};