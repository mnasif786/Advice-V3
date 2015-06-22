describe("Task Service Tests" , function(){

	var $httpBackend, $rootScope, config, taskService;

	beforeEach(function () {
        module('ngResource');
        module('configModule');      
        module('taskServicesModule');
	});

	beforeEach(inject(function ($injector) {

	    var configService = $injector.get('configService');
	    config = configService.getConfig();

	    $httpBackend = $injector.get('$httpBackend');
	    $rootScope = $injector.get('$rootScope');

	    taskService = $injector.get('taskService');
	}));

    /*                          
          GetSummaryTypeList: getSummaryTypeList,                         
    */

	it('Given valid task data when GetAllTasks is called then all tasks are returned', function ()
	{
	    //GIVEN        
	    var taskList = [{ "Id": '0', "name": 'Bedrock Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '1', "name": 'Flintstone Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '2', "name": 'Rubble Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '3', "name": 'Dinorama Team', "stats": { red: '9', amber: '8', green: '7', platinum: '6', total: '30' } }
	    ];

	    $httpBackend
            .when('GET', config.apiUrl + 'tasks/')
	        .respond(taskList);

	    $httpBackend
            .when('GET', config.apiUrl + 'tasks/teamuser')
	        .respond(taskList);

	    //WHEN       
	    var resultPromise = taskService.GetAllTasks();

	    //THEN
	    var result;
	    resultPromise.then(function (data) 
	    {
	        result = data;
	    });

	    $httpBackend.flush();
        
	    expect(result.data.length).toEqual(taskList.length);
	});
	
    // SGG: test may be redundant?
	it('Given valid task data for user when GetTasksByUser is called then tasks for user are returned', function ()
	{    
	    //GIVEN        
	    var taskList = [{ "TaskId": '0', "Description": 'Lift rock',    AssignedUser: 'Fred.Flintstone' },
                      { "TaskId": '1', "Description": 'Drop rock',      AssignedUser: 'Fred.Flintstone' },
                      { "TaskId": '2', "Description": 'Throw rock',     AssignedUser: 'Fred.Flintstone' },
                      { "TaskId": '3', "Description": 'catch Rock',     AssignedUser: 'Fred.Flintstone' }
	    ];

	    var userName = "Fred.Flintstone";

	    $httpBackend
            .when('GET', config.apiUrl + 'tasks/teamuser/' + userName + '/')
	        .respond(taskList);
       
	    //WHEN       
	    var resultPromise = taskService.GetTasksByUser(userName);

	    //THEN
	    var result;
	    resultPromise.then(function (data) {
	        result = data;
	    });

	    $httpBackend.flush();

	    expect(result.data.length).toEqual(taskList.length);
	});


    // SGG: test may be redundant?
	it('Given valid task data for team when GetTasksByTeam is called then tasks for team are returned', function () {
	    //GIVEN        
	    var taskList = [{ "TaskId": '0', "Description": 'Lift rock',    AssignedTeamID: 1},
                        { "TaskId": '1', "Description": 'Drop rock',    AssignedTeamID: 1},
                        { "TaskId": '2', "Description": 'Throw rock',   AssignedTeamID: 1},
                        { "TaskId": '3', "Description": 'catch Rock',   AssignedTeamID: 1}
	    ];
	           
	    var teamID = 123;

	    $httpBackend
            .when('GET', config.apiUrl + 'tasks/teamId?' + teamID)
	        .respond(taskList);

	    //WHEN       
	    var resultPromise = taskService.GetTasksByTeams(teamID);

	    //THEN
	    var result;
	    resultPromise.then(function (data) {
	        result = data;
	    });

	    $httpBackend.flush();

	    expect(result.data.length).toEqual(taskList.length);
	});

	it('Given valid task data when GetTimelineForUser is called then timeline tasks for user are returned', function () {

	    //GIVEN       
	    var testusername = "Scooby.Doo";
	    var timeline = { Red: 1, Amber: 2, Green: 3, Platinum: 4, Total: 10 };
                        
	    $httpBackend
            .when('GET', config.apiUrl + 'tasks/timeline/user/' + testusername + '/' )
	        .respond(timeline);

	    //WHEN       
	    var resultPromise = taskService.GetTimelineForUser(testusername);

	    //THEN
	    var result;
	    resultPromise.then(function (retdTimeline) {
	        result = retdTimeline.data;
	    });

	    $httpBackend.flush();

	    expect(result.Red).toEqual(timeline.Red);
	    expect(result.Green).toEqual(timeline.Green);
	    expect(result.Amber).toEqual(timeline.Amber);
	    expect(result.Platinum).toEqual(timeline.Platinum);
	    expect(result.Total).toEqual(timeline.Total);
	});

	it('Given valid task data when GetTimelineForTeams is called then timeline tasks for teams are returned', function () {

	    //GIVEN       
	    var testTeamIdList = [123, 234, 345, 456];

	    var timeline = { Red: 1, Amber: 2, Green: 3, Platinum: 4, Total: 10 };

	    var regex = new RegExp(config.apiUrl + 'tasks/timeline/team/*', '');

	    $httpBackend
            .when('GET', regex)
	        .respond(timeline);

	    //WHEN       
	    var resultPromise = taskService.GetTimelineForTeams(testTeamIdList);

	    //THEN
	    var result;
	    resultPromise.then(function (retdTimeline) {
	        result = retdTimeline.data;
	    });

	    $httpBackend.flush();
	    
	    //expect(result.Red).toEqual(timeline.Red * testTeamIdList.length);
	    //expect(result.Green).toEqual(timeline.Green * testTeamIdList.length);
	    //expect(result.Amber).toEqual(timeline.Amber * testTeamIdList.length);
	    //expect(result.Platinum).toEqual(timeline.Platinum * testTeamIdList.length);
	    //expect(result.Total).toEqual(timeline.Total * testTeamIdList.length);

	    expect(result.Red).toEqual(timeline.Red);
	    expect(result.Green).toEqual(timeline.Green);
	    expect(result.Amber).toEqual(timeline.Amber);
	    expect(result.Platinum).toEqual(timeline.Platinum);
	    expect(result.Total).toEqual(timeline.Total);
	});

    // RE_ADD TEST WHEN TASKLIST STATS ARE IMPLEMENTED
	//it('Given valid task data when GetTimelineForTaskList is called then timeline tasks for tasklist are returned', function () {
	//    //GIVEN       
	//    var taskListId = 1;

	//    var timeline = { Red: 1, Amber: 2, Green: 3, Platinum: 4, Total: 10 };
	//    var summaryTypeList = [
	//        {
	//            name: 'My Task List',
	//            id: '1',
	//            stats: { Red: '1', Amber: '2', Green: '3', Platinum: '4', Total: '10' }
	//        },
	//        {
	//            name: 'My Team',
	//            id: '2',
	//            stats: { Red: '2', Amber: '3', Green: '4', Platinum: '5', Total: '14' }
	//        }
	//    ];

        
	//    var regex = new RegExp(config.apiUrl + 'tasks/timeline/team/*', '');

	//    $httpBackend
    //        .when('GET', regex)
	//        .respond(timeline);
        
    //     TASKLIST TIMELINE NOT IMPLEMENTED YET SO NO CALLS BACK 

	//    WHEN       
	//    var resultPromise = taskService.GetTimelineForTaskList(taskListId);

	//    THEN
	//    var result;
	//    resultPromise.then(function (retdTimeline) {
	//        result = retdTimeline.data;
	//    });

	//    //$httpBackend.flush();

	//    expect(result.Red).toEqual(timeline.Red * testTeamIdList.length);
	//    expect(result.Green).toEqual(timeline.Green * testTeamIdList.length);
	//    expect(result.Amber).toEqual(timeline.Amber * testTeamIdList.length);
	//    expect(result.Platinum).toEqual(timeline.Platinum * testTeamIdList.length);
	//    expect(result.Total).toEqual(timeline.Total * testTeamIdList.length);
	//});

	it("Should return all the tasks when GetTasksByTeams is called", function() {
	        
            //GIVEN
	    var teamIds = 'id=17&id=18';
	        var tasksListToSearch = [
                { AssignedUser: 'Ra', TeamId: 17 },
                { AssignedUser: 'Sg', TeamId: 18 },
                { AssignedUser: 'gw', TeamId: 19 }
            ];
	    
            $httpBackend
                .when('GET', config.apiUrl + 'tasks/teamId?' + teamIds)
                .respond(tasksListToSearch);

	    //WHEN 
	    var resultPromise = taskService.GetTasksByTeams(teamIds);
       
	    var result;

            //THEN
            resultPromise.then(function (taskResult) {
                result = taskResult.data;
            });
        
            $httpBackend.flush();
            expect(result.length).toEqual(3);
	});
});