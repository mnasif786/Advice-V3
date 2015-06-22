angular
	.module('maintenanceUserServicesModule', ['ngResource'])
	.factory('maintenanceUserService', function ($rootScope, $http, $q, $filter, $resource, configService) {
	    "use strict";

	    var config = configService.getConfig();
	  
	    var users = [];
	    /*--------------------------------------------------------------------------------------------*/
	    function loadUsers(callback) {
	        var deferred = $q.defer();

	        $http({
	            method: "GET",
	            url: config.apiUrl + 'users/', withCredentials: true
	        })
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (data, status, headers) {
                console.log('error getting list of teams ' + status);
            });

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	    function getUsers(callback) {
	        var deferred = $q.defer();

	        if ($.isEmptyObject(users)) {
	            var loadUsersPromise = loadUsers();

	            loadUsersPromise.then(function (data) {
	                users = data;
	                deferred.resolve(users);
	            });
	        }
	        else {
	            deferred.resolve(users);
	        }

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	  
	    function getMaintenanceUserWithPermissions() {
	        var deferred = $q.defer();
	        $http({
	            method: "GET",
	            url: config.apiUrl + 'maintenance/user/loggedin/permissions',
	            withCredentials: true
	        })
                .success(function (data) {
                    //permissions = data;
                    deferred.resolve({ success: true, data: data });
                })
                .error(function (data, status, headers) {
                    console.log('error getting maintenance user with permissions' + status);
                });

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	    function getCorporatePriorityUsers() {
	        var deferred = $q.defer();
	        $http({
	            method: "GET",
	            url: config.apiUrl + 'maintenance/corporatepriority/users',
	            withCredentials: true
	        })
                .success(function (data) {
                    //permissions = data;
                    deferred.resolve({ success: true, data: data });
                })
                .error(function (data, status, headers) {
                    console.log('error getting corporate priority users' + status);
                });

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/

	    var addCorporatePriorityUser = function (userId) {
	        var deferred = $q.defer();
	        $http({
	            method: "POST",
	            url: config.apiUrl + 'maintenance/corporatepriority/user/add/' + userId,
	            withCredentials: true
	        })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to add Corporate Priority User. Method Called: AddCorporatePriorityUser' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error adding Corporate Priority User ' + user.Description + '. Method Called: AddCorporatePriorityUser' });
                }
                console.log('error adding Corporate Priority User Id ' + userId + ' with status ' + status);
            });

	        return deferred.promise;
	    };

	    var deleteCorporatePriorityUser = function (maintenanceUserPermissionId) {
	        var deferred = $q.defer();

	        $http({
	            method: "POST",
	            url: config.apiUrl + 'maintenance/corporatepriority/user/delete/' + maintenanceUserPermissionId,
	            withCredentials: true
	        })
            .success(function (callbackData) {
                deferred.resolve({ success: true, data: callbackData });
            })
            .error(function (callbackData, status, headers) {
                if (status == 500) {
                    deferred.resolve({ success: false, msg: 'An error has occurred.  Unable to delete Corporate Priority User. Method Called: DeleteCorporatePriorityUser' });
                }
                else {
                    deferred.resolve({ success: false, msg: 'error deleting Corporate Priority User. Method Called: DeleteCorporatePriorityUser' });
                }
                console.log('error deleting Corporate Priority User with status ' + status);
            });

	        return deferred.promise;
	    };

	    return {
	        GetUsers: getUsers,
	        GetMaintenanceUserWithPermissions: getMaintenanceUserWithPermissions,
	        GetCorporatePriorityUsers: getCorporatePriorityUsers,
	        AddCorporatePriorityUser: addCorporatePriorityUser,
	        DeleteCorporatePriorityUser: deleteCorporatePriorityUser
	        
	    };
	});
