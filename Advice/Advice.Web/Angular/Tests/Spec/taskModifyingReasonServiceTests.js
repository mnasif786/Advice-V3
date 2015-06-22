describe("TaskModifyingReasonService Tests", function() {
    var $httpBackend, config, taskModifyingReasonService;

    beforeEach(function () {
        module('ngResource');
        module('configModule');
        module('taskModifyingReasonServicesModule');
    });

    beforeEach(inject(function ($injector) {

        var configService = $injector.get('configService');
        config = configService.getConfig();
        $httpBackend = $injector.get('$httpBackend');

        taskModifyingReasonService = $injector.get('taskModifyingReasonService');
    }));

    it('Given valid task modifying reasons when getTaskModifyingReasonsForResetGroup is called then all related reasons are returned', function () {
        //GIVEN        
        var taskModifyingReasonList = [{ "Id": '6', "Description": 'Client Request' },
                        { "Id": '7', "Description": 'Management Decision' }
        ];

        $httpBackend
            .when('GET', config.apiUrl + 'taskModifyingReasons/getTaskModifyingReasonsForResetGroup/')
	        .respond(taskModifyingReasonList);

        //WHEN       
        var resultPromise = taskModifyingReasonService.GetTaskModifyingReasonsForResetGroup();

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.length).toEqual(taskModifyingReasonList.length);
    });
});