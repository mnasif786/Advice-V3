angular
    .module('clientServicesModule', ['configModule'])
    .factory('clientService', function($http, $q, configService) {
        
        var config = configService.getConfig();

        var getClientsStartWith = function(clientName) {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'client/clientsStartWithClientName/' + clientName + '/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get client name.' });
                } else {
                    deferred.resolve({ success: false, msg: 'error getting client names.' });
                }
                console.log('error getting client names ' + status);
            });

            return deferred.promise;
        };

        var getClientNamesStartWith = function (clientName) {
            //typeahead works if the data is returned like the following
            return $http.get(config.apiUrl + 'client/clientNamesStartWith/' + clientName + '/')
                .then(function(response) {
                    return response.data;
                });
        };

        var getCansStartWith = function (can) {
            //typeahead works if the data is returned like the following
            return $http.get(config.apiUrl + 'client/cansStartWith/' + can + '/')
                .then(function (response) {
                    return response.data;
                });
        };

        var getClientByName = function(clientName) {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'client/clientByName/' + clientName + '/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get client by name.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting client by name.' });
                }
                console.log('error getting client by name' + status);
            });

            return deferred.promise;
        }

        var getClientByCan = function (can) {
            var deferred = $q.defer();

            $http({
                method: "GET",
                url: config.apiUrl + 'client/clientByCan/' + can + '/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get client by can.' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting client by can.' });
                }
                console.log('error getting client by can' + status);
            });

            return deferred.promise;
        }

        var getAllClientsStartWithClientName = function (clientName) {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'client/allClientsStartWithClientName/' + clientName + '/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get all clients by name.' });
                } else {
                    deferred.resolve({ success: false, msg: 'error getting all clients by name.' });
                }
                console.log('error getting all clients by name ' + status);
            });

            return deferred.promise;
        };

        var getAllClientsStartWithCan = function (can) {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'client/allClientsStartWithCan/' + can + '/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get all clients by CAN.' });
                } else {
                    deferred.resolve({ success: false, msg: 'error getting all clients by CAN.' });
                }
                console.log('error getting all clients by CAN' + status);
            });

            return deferred.promise;
        };


        var getRecentClientsAction = function (can) {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'client/recentClientsAction',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get recent clients action.' });
                } else {
                    deferred.resolve({ success: false, msg: 'error getting recent clients action.' });
                }
                console.log('error getting recent clients action' + status);
            });

            return deferred.promise;
        };



        return {
            GetClientsStartWith : getClientsStartWith,
            GetClientNamesStartWith : getClientNamesStartWith,
            GetClientByName : getClientByName,
            GetClientByCan : getClientByCan,
            GetAllClientsStartWithClientName : getAllClientsStartWithClientName,
            GetAllClientsStartWithCan : getAllClientsStartWithCan,
            GetCansStartWith: getCansStartWith,
            GetRecentClientsAction: getRecentClientsAction
        };
});
