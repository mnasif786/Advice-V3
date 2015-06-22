describe("userService tests", function () {

    var $httpBackend, $rootScope, config, userService;


    beforeEach(function () {
        module('ngResource');
        module('configModule');      
        module('userServicesModule');
    });


    beforeEach(inject(function ($injector) {

        var configService = $injector.get('configService');
        config = configService.getConfig();

        $httpBackend = $injector.get('$httpBackend');
        $rootScope = $injector.get('$rootScope');
      
        userService = $injector.get('userService');
    }));
    
    it('Given valid team data when GetTeams is called then all teams are returned', function () {
        //GIVEN        
        var teamList = [{ "Id": '0', "name": 'Bedrock Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '1', "name": 'Flintstone Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '2', "name": 'Rubble Team', "stats": { red: '1', amber: '2', green: '3', platinum: '4', total: '10' } },
                        { "Id": '3', "name": 'Dinorama Team', "stats": { red: '9', amber: '8', green: '7', platinum: '6', total: '30' } }
        ];

        $httpBackend
            .when('GET', config.apiUrl + 'teams/')
            .respond(teamList);

        //WHEN       
        var resultPromise = userService.GetTeams();
        
        //THEN
        var result;
        resultPromise.then(function (data)
        {
            result = data;
        });

        $httpBackend.flush();

        expect(result.length).toEqual(4);
    });


    it('Given valid user data when GetUsers is called then all users are returned', function ()
    {
        //GIVEN        
        var userList = [{ "UserId": '0', "Username": 'Barney Rubble',        'RoleId':'1', 'TeamId':'1'},
                        { "UserId": '1', "Username": 'Betty Rubble',        'RoleId':'2', 'TeamId':'2'},
                        { "UserId": '2', "Username": 'Fred Flintstone',     'RoleId':'3', 'TeamId':'3'},
                        { "UserId": '3', "Username": 'Wilma Flintstone',    'RoleId':'4', 'TeamId':'4'}
        ];

        

        $httpBackend
            .when('GET', config.apiUrl + 'users/')
            .respond(userList);

        //WHEN       
        var resultPromise = userService.GetUsers();

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.length).toEqual(4);
    });

    it('Given valid user data when GetUsersByTeams is called then all users in teams are returned', function ()
    {
        //GIVEN        
        var userList = [{ "UserId": '0', "Username": 'Barney Rubble', 'RoleId': '1', 'TeamId': '1' },
                        { "UserId": '1', "Username": 'Betty Rubble', 'RoleId': '2', 'TeamId': '2' },
                        { "UserId": '2', "Username": 'Fred Flintstone', 'RoleId': '3', 'TeamId': '3' },
                        { "UserId": '3', "Username": 'Wilma Flintstone', 'RoleId': '4', 'TeamId': '3' }
        ];

        $httpBackend
            .when('GET', config.apiUrl + 'users/')
            .respond(userList);        

        //WHEN       
        var resultPromise = userService.GetUsersByTeams( [ {'TeamId':'2'}, {'TeamId':'3'} ] );

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.length).toEqual(3);
    });
    
});