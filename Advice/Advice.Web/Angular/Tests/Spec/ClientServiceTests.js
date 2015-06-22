describe("ClientService Tests", function () {
    var $httpBackend, config, clientService;

    beforeEach(function () {
        module('ngResource');
        module('configModule');
        module('clientServicesModule');
    });

    beforeEach(inject(function ($injector) {

        var configService = $injector.get('configService');
        config = configService.getConfig();
        $httpBackend = $injector.get('$httpBackend');

        clientService = $injector.get('clientService');
    }));

    it('Given clients data when GetClientsStartWith is called then clients starting with initials are returned', function () {
        //GIVEN        
        var clientList = [{ "ClientId": '6', "Can": 'CAN102',  "ClientName": 'Client Services'},
                             { "ClientId": '7', "Can": 'CAN1002', "ClientName": 'Client Data' }
                            ];

        $httpBackend
            .when('GET', config.apiUrl + 'client/clientsStartWithClientName/Clie/')
	        .respond(clientList);

        //WHEN       
        var resultPromise = clientService.GetClientsStartWith('Clie');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.length).toEqual(clientList.length);
    });

    it('Given clients data when GetClientNamesStartWith is called then clients names starting with initials are returned', function () {
        //GIVEN        
        var clientList = ['Client Services', 'Client Data' ];

        $httpBackend
            .expectGET(config.apiUrl + 'client/clientNamesStartWith/Clie/')
	        .respond(clientList);

        //WHEN       
        var resultPromise = clientService.GetClientNamesStartWith('Clie');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.length).toEqual(clientList.length);
    });

    it('Given clients data when GetCansStartWith is called then cans starting with initials are returned', function () {
        //GIVEN        
        var cans = ['ZPEN001', 'ZPEN002'];

        $httpBackend
            .expectGET(config.apiUrl + 'client/cansStartWith/ZPEN/')
	        .respond(cans);

        //WHEN       
        var resultPromise = clientService.GetCansStartWith('ZPEN');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.length).toEqual(cans.length);
    });

    it('Given clients data when GetClientByName is called then correct client is returned', function () {
        //GIVEN        
        var client = { "ClientId": '7', "Can": 'CAN1002', "ClientName": 'Client Data' };


        $httpBackend
            .when('GET', config.apiUrl + 'client/clientByName/Client Data/')
	        .respond(client);

        //WHEN       
        var resultPromise = clientService.GetClientByName('Client Data');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.ClientName).toEqual('Client Data');
    });

    it('Given clients data when GetClientByCan is called then correct client is returned', function () {
        //GIVEN        
        var client = { "ClientId": '7', "Can": 'CAN1002', "ClientName": 'Client Data' };


        $httpBackend
            .when('GET', config.apiUrl + 'client/clientByCan/CAN1002/')
	        .respond(client);

        //WHEN       
        var resultPromise = clientService.GetClientByCan('CAN1002');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.Can).toEqual('CAN1002');
    });

    it('Given clients data when getAllClientsStartWithClientName is called then all clients starting with those characters are returned', function () {
        //GIVEN        
        var clients = [{ "ClientId": '7', "Can": 'CAN1002', "ClientName": 'Client Data' },
                      { "ClientId": '8', "Can": 'CAN1003', "ClientName": 'Client Data Services' }];


        $httpBackend
            .when('GET', config.apiUrl + 'client/allClientsStartWithClientName/Client Data/')
	        .respond(clients);

        //WHEN       
        var resultPromise = clientService.GetAllClientsStartWithClientName('Client Data');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.length).toEqual(clients.length);
    });

    it('Given clients data when getAllClientsStartWithCan is called then all clients starting with those characters are returned', function () {
        //GIVEN        
        var clients = [{ "ClientId": '7', "Can": 'CAN1002', "ClientName": 'Client Data' },
                      { "ClientId": '8', "Can": 'CAN1003', "ClientName": 'Client Data Services' }];


        $httpBackend
            .when('GET', config.apiUrl + 'client/allClientsStartWithCan/CAN/')
	        .respond(clients);

        //WHEN       
        var resultPromise = clientService.GetAllClientsStartWithCan('CAN');

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.length).toEqual(clients.length);
    });

    it('Given recent clients Action when GetRecentClientsAction is called then all recent clients actions are returned', function () {
        //GIVEN        
        var clients = [{ "Can": 'CAN1002', "ClientName": 'Client Data', "LastAction": new Date() },
                      { "Can": 'CAN1003', "ClientName": 'Client Data Services', "LastAction": new Date() }];


        $httpBackend
            .when('GET', config.apiUrl + 'client/recentClientsAction')
	        .respond(clients);

        //WHEN       
        var resultPromise = clientService.GetRecentClientsAction();

        //THEN
        var result;
        resultPromise.then(function (data) {
            result = data;
        });

        $httpBackend.flush();

        expect(result.data.length).toEqual(clients.length);
    });
});