describe("taskController tests", function() {
    var $httpBackend, $rootScope, createController, taskService, configService, userService, $q, deferred, $modal, enums, scope, $interval, $window;

    var teamList = [
        { "TeamId": '0', "Description": 'Bedrock Team' },
        { "TeamId": '1', "Description": 'Flintstone Team' },
        { "TeamId": '2', "Description": 'Rubble Team' },
        { "TeamId": '3', "Description": 'Dinorama Team' }
    ];

    var selectedUser = [{ "UserId": '487', "Username": 'Sheryll.Northern', "RoleId": '12', "TeamId": '42' }];

    // summary list
    var summaryTypeList = [
        {
            name: 'My Task List',
            id: '1',
            stats: { Red: '1', Amber: '2', Green: '3', Platinum: '4', Total: '10' }
        },
        {
            name: 'My Team',
            id: '2',
            stats: { Red: '2', Amber: '3', Green: '4', Platinum: '5', Total: '14' }
        },
        {
            name: 'Free Advice',
            id: '3',
            stats: { Red: '3', Amber: '4', Green: '5', Platinum: '6', Total: '18' }
        },
        {
            name: 'GB Out Of Hours',
            id: '4',
            stats: { Red: '4', Amber: '5', Green: '6', Platinum: '7', Total: '22' }
        },
        {
            name: 'NI Out Of Hours',
            id: '5',
            stats: { Red: '5', Amber: '6', Green: '7', Platinum: '8', Total: '26' }
        },
        {
            name: 'Priority Out Of Hours',
            id: '6',
            stats: { Red: '6', Amber: '7', Green: '8', Platinum: '9', Total: '30' }
        }
    ];

    var proactive = {
        Tasks: [
            { "TaskId": 3876255, "Can": "DEMO002", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "This is the description", "DueDate": "2014-10-28T11:05:23.657", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-28T10:05:23.657", "DocumentCount": 1, "Status": "Amber", "Deleted": false },
            { "TaskId": 4876255, "Can": "DEMO002", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "This is the description", "DueDate": "2014-10-28T11:05:23.657", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-28T10:05:23.657", "DocumentCount": 1, "Status": "Amber", "Deleted": false },
            { "TaskId": 5876255, "Can": "DEMO002", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "This is the description", "DueDate": "2014-10-28T11:05:23.657", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-28T10:05:23.657", "DocumentCount": 1, "Status": "Amber", "Deleted": false }
        ]
    };

    // task list
    var taskList = [
        { "TaskId": 3876162, "Can": "ZPEN018", "TaskTypeId": 22, "TaskTypeDescription": "hronline Close Job Request", "Description": "IT TEST ONLY", "DueDate": "2014-09-01T13:52:17.58", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "JP Admin", "CreatedDate": "2014-09-01T09:52:17.657", "DocumentCount": 0, "Status": "Red", "Deleted": false },
        { "TaskId": 3876154, "Can": "HAR106", "TaskTypeId": 21, "TaskTypeDescription": "hronline Client Action Added", "Description": "Maggie Simpson has added an action to the job '2111386': dsf", "DueDate": "2014-10-23T10:24:16.973", "AssignedUser": "linda.howe", "AssignedTeamId": null, "CreatedBy": "Maggie Simpson", "CreatedDate": "2014-08-11T14:01:20.897", "DocumentCount": 1, "Status": "Red", "Deleted": false },
        { "TaskId": 3876240, "Can": "", "TaskTypeId": 6, "TaskTypeDescription": "Document Review", "Description": "Test Document attached", "DueDate": "2014-10-23T11:01:16.973", "AssignedUser": "Rana.Khan", "AssignedTeamId": null, "CreatedBy": "Rana.Khan", "CreatedDate": "2014-10-17T12:24:18.013", "DocumentCount": 8, "Status": "Red", "Deleted": false },
        { "TaskId": 3876250, "Can": "", "TaskTypeId": 6, "TaskTypeDescription": "Document Review", "Description": null, "DueDate": "2014-10-27T08:43:15.917", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-24T14:13:15.917", "DocumentCount": 2, "Status": "Green", "Deleted": false },
        { "TaskId": 3876253, "Can": "", "TaskTypeId": 6, "TaskTypeDescription": "Document Review", "Description": null, "DueDate": "2014-10-27T08:58:39.843", "AssignedUser": "Scott.Gilhooly", "AssignedTeamId": null, "CreatedBy": "scott.gilhooly", "CreatedDate": "2014-10-24T14:28:39.843", "DocumentCount": 1, "Status": "Green", "Deleted": false },
        { "TaskId": 3876257, "Can": "", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent in magna molestie, auctor quam qu", "DueDate": "2014-10-28T10:38:27.347", "AssignedUser": "Mark.Jepson", "AssignedTeamId": null, "CreatedBy": "Mark.Jepson", "CreatedDate": "2014-10-28T09:38:27.347", "DocumentCount": 0, "Status": "Platinum", "Deleted": false },
        { "TaskId": 3876258, "Can": "DEMO002", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "This is the description", "DueDate": "2014-10-28T11:05:23.657", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-28T10:05:23.657", "DocumentCount": 1, "Status": "Amber", "Deleted": false }
    ];

    var userTaskList = [
        { "TaskId": 3876162, "Can": "ZPEN018", "TaskTypeId": 22, "TaskTypeDescription": "hronline Close Job Request", "Description": "IT TEST ONLY", "DueDate": "2014-09-01T13:52:17.58", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "JP Admin", "CreatedDate": "2014-09-01T09:52:17.657", "DocumentCount": 0, "Status": "Red", "Deleted": false },
        { "TaskId": 3876154, "Can": "HAR106", "TaskTypeId": 21, "TaskTypeDescription": "hronline Client Action Added", "Description": "Maggie Simpson has added an action to the job '2111386': dsf", "DueDate": "2014-10-23T10:24:16.973", "AssignedUser": "linda.howe", "AssignedTeamId": null, "CreatedBy": "Maggie Simpson", "CreatedDate": "2014-08-11T14:01:20.897", "DocumentCount": 1, "Status": "Red", "Deleted": false },
        { "TaskId": 3876250, "Can": "", "TaskTypeId": 6, "TaskTypeDescription": "Document Review", "Description": null, "DueDate": "2014-10-27T08:43:15.917", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-24T14:13:15.917", "DocumentCount": 2, "Status": "Green", "Deleted": false },
        { "TaskId": 3876258, "Can": "DEMO002", "TaskTypeId": 3, "TaskTypeDescription": "Callback 1 Hour", "Description": "This is the description", "DueDate": "2014-10-28T11:05:23.657", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "Linda.Howe", "CreatedDate": "2014-10-28T10:05:23.657", "DocumentCount": 1, "Status": "Amber", "Deleted": false }
    ];

    var userTaskList1 = [
        { "TaskId": 3876162, "Can": "ZPEN018", "TaskTypeId": 22, "TaskTypeDescription": "hronline Close Job Request", "Description": "IT TEST ONLY", "DueDate": "2014-09-01T13:52:17.58", "AssignedUser": "Linda.Howe", "AssignedTeamId": null, "CreatedBy": "JP Admin", "CreatedDate": "2014-09-01T09:52:17.657", "DocumentCount": 0, "Status": "Red", "Deleted": false }
    ];

    
    
    var mockProActiveTasksResponse = { success: true, data: proactive.Tasks };
    var mockGetTeamTasksResponse = { success: true, data: taskList };
    var mockGetUsersTasksResponse = { success: true, data: userTaskList };
    var mockGetUser1TaskResponse = { success: true, data: userTaskList1 };
    var mockGetUsersTimeline = { success: true, data: summaryTypeList[2] };
    var mockDeleteTasksResponse = { success: true, data: true };
    
    

    var httpUserResponse = { User: 'user1' };
    var httpTeamResponse = { Team: 'Team1' };
    

    beforeEach(function () {
        module('configModule');
        module('taskServicesModule');
        module('userServicesModule');
    });    

    beforeEach(angular.mock.inject(function ($injector) {
        $rootScope = $injector.get('$rootScope');
        
        scope = $rootScope.$new();
        configService = $injector.get('configService');
        userService = $injector.get('userService');
        taskService = $injector.get('taskService');
        $q = $injector.get('$q');
      
        $modal = {
            close: jasmine.createSpy('modalInstance.close'),
            dismiss: jasmine.createSpy('modalInstance.dismiss'),
            result: {
                then: jasmine.createSpy('modalInstance.close')
            },
            open: jasmine.createSpy('modalInstance.dismiss')
        }

        $httpBackend = $injector.get('$httpBackend');

        var $controller = $injector.get('$controller');
       
        $httpBackend.whenGET('api/teams/').respond(httpTeamResponse);
        $httpBackend.whenGET('api/users/').respond(httpUserResponse); 
        

        createController = function () {
            return $controller('taskController', {
                $rootScope: $rootScope,
                $scope: scope,
                $window: window,  
                $modal: $modal,
                enums: enums,
                configService: configService,
                taskService: taskService,
                userService: userService,
                $interval: $interval,
                $q: $q
            });
        };
    }));


    beforeEach(function() {
        deferred = $q.defer();
        

        // mock this interval function, doesn't do anything, just need it for tests to work
        $interval = function (fn, delay, count, invokeApply) {
            return $q.promise;
        };

        $rootScope.checkForNewVersion = function ($window)
        {
        }

        taskService = {
            GetTasksByTeams: function() {
                deferred.resolve(mockGetTeamTasksResponse);
                return deferred.promise;
            },
            GetTasksByUser: function() {
                deferred.resolve(mockGetUsersTasksResponse);
                return deferred.promise;
            },
            GetSummaryTypeList: function() {
                return summaryTypeList;
            },
            GetTimelineForTaskList: function() {
                return summaryTypeList[0].stats;
            },
            GetTimelineForUser: function() {
                deferred.resolve(mockGetUsersTimeline);
                return deferred.promise;
            },
            GetTimelineForTeams: function() {
                deferred.resolve(mockGetUsersTimeline);
                return deferred.promise;
            },
            DeleteBulkTasks: function() {
                deferred.resolve(mockDeleteTasksResponse);
                return deferred.promise;
            },
            GetProActiveTaskForLoggedInUser: function() {
                deferred.resolve(mockProActiveTasksResponse);
                return deferred.promise;
            },
        };
    });


    //it('on refresh assert methods are called correctly', function () {
    //    debugger;
    //    var controller = createController();
    //    spyOn(scope, 'getTimelineAndTasksByTeams()');
    //    spyOn(scope, 'getTimelineAndTasksByUser()');
        
    //    scope.refresh();
    //    expect(getTimelineAndTasksByTeams()).toHaveBeenCalled();
    //    expect(getTimelineAndTasksByUser()).toHaveBeenCalled();
        
    //});

    //it('on refresh team tasklist with filter assert methods are called correctly', function() {
    //    var controller = createController();
    //    spyOn(scope, 'filterOnTeamsBalls');
    //    scope.getTasksByTeams(1);
    //    scope.$apply();
    //    scope.setFilterType('None');
    //    scope.refreshTaskList();
    //    expect(scope.filterOnTeamsBalls).not.toHaveBeenCalled();
    //    scope.setFilterType('Green');
    //    scope.refreshTaskList();
    //    expect(scope.filterOnTeamsBalls).toHaveBeenCalled();
    //    scope.setFilterType('Red');
    //    scope.refreshTaskList();
    //    expect(scope.filterOnTeamsBalls).toHaveBeenCalled();
    //});

    //it('on getTaskByTeam assert lastSelectedList is team list', function() {
    //    var controller = createController();
    //    scope.getTasksByTeams(1);
    //    scope.$apply();
    //    expect(scope.getTaskListType()).toEqual('Team');
    //});

    //it('on getTaskByUser assert lastSelectedList is user list', function() {
    //    var controller = createController();
    //    scope.getTasksByUser('user1');
    //    scope.$apply();
    //    expect(scope.getTaskListType()).toEqual('User');
    //});

    //it('on refreshTaskList and last selectedType is Team then assert showTasklistForTeam is called', function() {
    //    var controller = createController();
    //    spyOn(scope, 'showTasklistForTeam');
    //    spyOn(scope, 'showTasklistForUser');
    //    scope.getTasksByTeams(1);
    //    scope.$apply();
    //    scope.refreshTaskList();
    //    expect(scope.showTasklistForTeam).toHaveBeenCalled();
    //    expect(scope.showTasklistForUser).not.toHaveBeenCalled();
    //});

    //it('on refreshTaskList and last selectedType is User then assert showTasklistForUser is called', function() {
    //    var controller = createController();
    //    spyOn(scope, 'showTasklistForTeam');
    //    spyOn(scope, 'showTasklistForUser');
    //    scope.getTasksByUser('user1');
    //    scope.$apply();
    //    scope.refreshTaskList();
    //    expect(scope.showTasklistForTeam).not.toHaveBeenCalled();
    //    expect(scope.showTasklistForUser).toHaveBeenCalled();
    //});

    //it('on team ball filters check model tasks results updated correctly with applied ball filters', function() {
    //    var controller = createController();
    //    spyOn(scope, 'taskSearchResultData');
    //    scope.getTasksByTeams(1);
    //    scope.$apply();
    //    scope.selectedTeams = teamList;
    //    scope.filterOnTeamsBalls('Red');
    //    expect(scope.taskSearchResultData.length).toEqual(3);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(3);

    //    scope.filterOnTeamsBalls('Green');
    //    expect(scope.taskSearchResultData.length).toEqual(2);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(2);

    //    scope.filterOnTeamsBalls('Amber');
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(1);

    //    scope.filterOnTeamsBalls('Platinum');
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(1);
    //});

    //it('on user ball filters check model tasks results updated correctly with applied ball filters', function() {
    //    var controller = createController();
    //    spyOn(scope, 'taskSearchResultData');
    //    scope.getTasksByUser('user1');
    //    scope.$apply();
    //    scope.selectedUser = selectedUser;
    //    scope.filterOnUsersBalls('Red');
    //    expect(scope.taskSearchResultData.length).toEqual(2);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(2);

    //    scope.filterOnUsersBalls('Green');
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(1);

    //    scope.filterOnUsersBalls('Amber');
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(1);

    //    scope.filterOnUsersBalls('Platinum');
    //    expect(scope.taskSearchResultData.length).toEqual(0);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(0);
    //});

    //it('on team balls filtered applied before tasks retrieved from database get tasks from DB and filter', function() {
    //    var controller = createController();
    //    spyOn(scope, 'getSynchronousTasksByTeams').and.callThrough();

    //    scope.selectedTeams = teamList;
    //    scope.teamTaskSearchResultFromDatabase = "";
    //    scope.filterOnTeamsBalls('Red');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(3);

    //    scope.teamTaskSearchResultFromDatabase = "";
    //    scope.filterOnTeamsBalls('Green');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(2);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(2);

    //    scope.filterOnTeamsBalls('Platinum');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //    expect(scope.taskSearchResultData.length).not.toBeGreaterThan(1);
    //});

    //it('on user balls filtered applied before tasks retrieved from database get tasks from DB and filter', function() {
    //    var controller = createController();
    //    spyOn(scope, 'getSynchronousTasksByUsers').and.callThrough();

    //    scope.selectedUser = selectedUser;
    //    scope.userTaskSearchResultFromDatabase = "";
    //    scope.filterOnUsersBalls('Red');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(2);

    //    scope.userTaskSearchResultFromDatabase = "";
    //    scope.filterOnUsersBalls('Green');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(1);

    //    scope.userTaskSearchResultFromDatabase = "";
    //    scope.filterOnUsersBalls('Amber');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(1);

    //    scope.filterOnUsersBalls('Platinum');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(0);
    //});

    // ignore for now
    //it('on team balls filtered before refresh check balls still filtered after refresh', function() {
    //    var controller = createController();
    //    spyOn(scope, 'getSynchronousTasksByTeams').and.callThrough();

    //    scope.selectedTeams = teamList;
    //    scope.teamTaskSearchResultFromDatabase = "";
    //    scope.filterOnTeamsBalls('Red');
    //    scope.$apply();
    //    scope.refreshTaskList();
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(3);
    //});

    //xit('on user balls filtered before refresh check balls still filtered after refresh', function() {
    //    var controller = createController();
    //    spyOn(scope, 'getSynchronousTasksByUsers').and.callThrough();

    //    scope.selectedUser = selectedUser;
    //    scope.userTaskSearchResultFromDatabase = "";
    //    scope.filterOnUsersBalls('Red');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(2);
    //    scope.getTasksByUser('user1');
    //    scope.$apply();
    //    scope.refreshTaskList();
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(2);
    //});

    //it('on user changed check tasks are updated with new user tasks', function() {
    //    var controller = createController();
    //    spyOn(taskService, 'GetTasksByUser').and.callFake(function() {
    //        deferred = $q.defer();
    //        deferred.resolve(mockGetUsersTasksResponse);
    //        return deferred.promise;
    //    });

    //    scope.selectedUser = selectedUser;
    //    scope.userTaskSearchResultFromDatabase = "";
    //    scope.filterOnUsersBalls('Red');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(2);

    //    taskService.GetTasksByUser.and.callFake(function() {
    //        deferred = $q.defer();
    //        deferred.resolve(mockGetUser1TaskResponse);
    //        return deferred.promise;
    //    });

    //    scope.selectedUserUpdateDetails();
    //    scope.filterOnUsersBalls('Red');
    //    scope.$apply();
    //    expect(scope.taskSearchResultData.length).toEqual(1);
    //});

    //it('on no teams selected and currently viewing team tasks ensure grid details are emptied', function() {
    //    var controller = createController();
    //    scope.selectedTeams = "";
    //    spyOn(scope, 'teamTimelineTotals');
    //    spyOn(scope, 'getTaskListType');
    //    scope.selectedTeamsUpdateDetails();
    //    expect(scope.getTaskListType).toHaveBeenCalled();
    //    expect(scope.getTaskListType).not.toEqual("User");
    //    expect(scope.getTaskListType).not.toEqual("Summary");
    //    expect(scope.teamTimelineTotals.length).toEqual(0);
    //    expect(scope.taskSearchResultData).toEqual(null);
    //});

    //it('on tasks bulk deleted assert deleted tasks are removed from grid', function() {
    //    var controller = createController();
    //    scope.selectedTeams = "";
    //    spyOn(scope, 'teamTimelineTotals');
    //    spyOn(scope, 'getTaskListType');
    //    scope.selectedTeamsUpdateDetails();
    //    expect(scope.getTaskListType).toHaveBeenCalled();
    //    expect(scope.getTaskListType).not.toEqual("User");
    //    expect(scope.getTaskListType).not.toEqual("Summary");
    //    expect(scope.teamTimelineTotals.length).toEqual(0);
    //    expect(scope.taskSearchResultData).toEqual(null);
    //});
}); 