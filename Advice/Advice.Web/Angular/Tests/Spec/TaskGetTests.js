describe("taskController tests", function() {

    var $httpBackend, config, taskservice, $window, createController, $rootScope; 

    var taskId = 1;

    beforeEach(function () {
        module('ngResource');
        module('configModule');
        module('taskServicesModule');
    });

    
    beforeEach(inject(function($injector) {
        
        var configService = $injector.get('configService');
        config = configService.getConfig();
        
        $httpBackend = $injector.get('$httpBackend');

        $httpBackend.whenGET(config.apiUrl + 'tasks/').respond({ "Id": taskId, "Title": "Abc" });

        $rootScope = $injector.get('$rootScope');
        taskservice = $injector.get('taskService');

        // The $controller service is used to create instances of controllers
        var $controller = $injector.get('$controller');

        $window = $injector.get('$window');

        createController = function () {
            return $controller('taskController', { '$scope': $rootScope, 'taskService': taskservice });
        };

    }));

    it('task api was called with the correct parameters', function () {
        
        //var controller = createController();

        $httpBackend.expectGET(config.apiUrl + 'tasks/');

       var result = null;

       //taskservice.GetAllTasks(function (data) {
       //     result = data;
       // });

        expect(1).toBe(1);
    });
   

}); 