angular
    .module('divisionServicesModule', ['configModule'])
    .factory('divisionService', function($http, $q, configService) {
        var config = configService.getConfig();

        var getAllDivisions = function () {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'divisions/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get divisions' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting divisions.  Service Called: GetAllDivisions' });
                }
                console.log('error getting divisions' + status);
            });

            return deferred.promise;
        };

        var editDivision = function (divisionModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'divisions/editDivision',
                data: divisionModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to edit division. Method Called: editDivision' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error editing team ' + divisionModel.Description + '. Method Called: editDivision' });
                }
                console.log('error editing division ' + divisionModel.Description + ' with status ' + status);
            });

            return deferred.promise;
        };

        var addDivision = function (divisionModel) {
            var deferred = $q.defer();

            $http({
                method: "POST",
                url: config.apiUrl + 'divisions/addDivision',
                data: divisionModel,
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to add division. Method Called: addDivision' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error adding division ' + divisionModel.Description + '. Method Called: addDivision' });
                }
                console.log('error adding division ' + divisionModel.Description + ' with status ' + status);
            });

            return deferred.promise;
        };


        return {
            GetAllDivisions: getAllDivisions,
            EditDivision: editDivision,
            AddDivision: addDivision
        };
    });