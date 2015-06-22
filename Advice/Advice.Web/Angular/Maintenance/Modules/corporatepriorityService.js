angular
    .module('corporatePriorityServicesModule', ['configModule'])
    .factory('corporatePriorityService', function ($http, $q, configService) {
        var config = configService.getConfig();

        var getAllCorporatePriorities = function () {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'maintenance/corporatePriorities/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get corporate priorities' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting corporate priorities.  Service Called: GetAllCorporatePriorities' });
                }
                console.log('error getting corporate priorities' + status);
            });

            return deferred.promise;
        };

        var deleteCorporatePriority = function (corporatePrioritId) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'maintenance/corporatepriority/delete/' + corporatePrioritId,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to delete corporate priority. Method Called: DeleteCorporatePriority' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error deleting corporate priority. Method Called: DeleteCorporatePriority' });
                }
                console.log('error deleting corporate priority with status ' + status);
            });

            return deferred.promise;
        };

        var editCorporatePriority = function (corporatePriorityModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'maintenance/corporatepriority/edit',
                data: corporatePriorityModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to edit Corporate Priority for CAN ' + corporatePriorityModel.Can + '. Method Called: EditCorporatePriority' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error editing Corporate Priority for CAN ' + corporatePriorityModel.Can + '. Method Called: EditCorporatePriority' });
                }
                console.log('error editing Corporate Priority for CAN ' + corporatePriorityModel.Can + ' with status ' + status);
            });

            return deferred.promise;
        };

        var addCorporatePriority = function (corporatePriorityModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'maintenance/corporatepriority/add',
                data: corporatePriorityModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to add corporate priority. Method Called: AddCorporatePriority' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error adding Corporate Priority for CAN ' + corporatePriorityModel.Can + '. Method Called: AddCorporatePriority' });
                }
                console.log('error adding Corporate Priority for CAN ' + corporatePriorityModel.Can + ' with status ' + status);
            });

            return deferred.promise;
        };
        
        var getCansStartWith = function (can) {
            //typeahead works if the data is returned like the following
            return $http.get(config.apiUrl + 'maintenance/cansStartWith/' + can + '/')
                .then(function (response) {
                    return response.data;
                });
        };


        var getCorporatePriorityByCan = function (can) {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'maintenance/corporatePriority/'+ can,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get corporate priorities' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting corporate priorities.  Service Called: getCorporatePriorityByCan' });
                }
                console.log('error getting corporate priorities' + status);
            });

            return deferred.promise;
        };     

        return {
            GetAllCorporatePriorities: getAllCorporatePriorities,
            AddCorporatePriority : addCorporatePriority,
            EditCorporatePriority : editCorporatePriority,
            DeleteCorporatePriority : deleteCorporatePriority,
            GetCansStartWith: getCansStartWith,
            GetCorporatePriorityByCan: getCorporatePriorityByCan,
        };
    });