angular
    .module('departmentServicesModule', ['configModule'])
    .factory('departmentService', function($http, $q, configService) {
    
        var config = configService.getConfig();

        var getAllDepartments = function () {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: config.apiUrl + 'departments/',
                withCredentials: true
            })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to get departments' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error getting departments.  Service Called: GetAllDepartments' });
                }
                console.log('error getting departments' + status);
            });

            return deferred.promise;
        };

        return {
            GetAllDepartments: getAllDepartments
        };
});