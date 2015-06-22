angular
	.module('userServicesModule', ['ngResource'])
	.factory('userService', function ($rootScope, $http, $q, $filter, $resource, configService)
	{
	    "use strict";

	    var config = configService.getConfig();

	    var teams = [];
	    var users = [];
	    var permissions = [];
	    var loggedInUser = null;
        /*--------------------------------------------------------------------------------------------*/
	    function _loadTeams(callback) {	       
	        var deferred = $q.defer();

	        $http({
	            method: "GET",
	            url: config.apiUrl + 'teams/', withCredentials: true
	        })
           .success(function (data)
           {
               deferred.resolve(data);               
           })
           .error(function (data, status, headers) {
               console.log('error getting list of teams ' + status);
           });

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	    function _loadUsers(callback)
	    {
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
	    function getTeams(callback)
	    {
	        var deferred = $q.defer();

	        if ($.isEmptyObject(teams))
	        {
	            var loadTeamsPromise = _loadTeams();

	            loadTeamsPromise.then(function (data)
	            {
	                teams = data;
	                deferred.resolve(teams);
	            });
	        }
	        else
	        {
	            deferred.resolve(teams);
	        }

	        return deferred.promise;
	    };

	    /*--------------------------------------------------------------------------------------------*/
	    function getUsers(callback)
	    {
	        var deferred = $q.defer();

	        if ($.isEmptyObject(users)) {
	            var loadUsersPromise = _loadUsers();

	            loadUsersPromise.then(function (data)
	            {
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
	    function getUsersByTeams(teams)
	    {
	        var deferred = $q.defer();

	        getUsers().then( function(userList){
	        
	            var teamUsers = [];	            
	            var usersByTeams = [];
                // if any of the team are the "all users" team, return all users			
	            var allUsers = false;
	            angular.forEach(teams, function (team, key)
	            {
	                if (team.TeamId == 0)
	                {
	                    allUsers = true;
	                }
	            });

	            if (allUsers)
	            {
	                teamUsers = userList;
	            }
	            else
	            {
	                
	                angular.forEach(userList, function (user, key) {
	                    angular.forEach(teams, function (team, key) {
	                        if (user.TeamId == team.TeamId) {
	                            teamUsers.push(user);
	                        }
	                    });
	                });

	                //This is to fixw the selection problem on dropdown.
	                //var t = 0;
	                //angular.forEach(teamUsers, function (teamUser, key) {
	                //    if (t < 5) {
	                //        usersByTeams.push(teamUser);
	                //    }
	                //    t++;
	                //});
	                //teamUsers = usersByTeams;
	            }
                
	            deferred.resolve(teamUsers);
	        });

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	    function getUserWithPermissions(callback) {
	        var deferred = $q.defer();
	        //if ($.isEmptyObject(permissions)) {
	            $http({
	                method: "GET",
	                url: config.apiUrl + 'users/loggedin/permissions',
	                withCredentials: true
	            })
	                .success(function (data) {
	                    //permissions = data;
	                    deferred.resolve({ success: true, data: data });
	                })
	                .error(function (data, status, headers) {
	                    console.log('error getting user with permissions' + status);
	                });
	        //} else {
	        //    deferred.resolve({ success: true, data: permissions });
	        //}

	        return deferred.promise;
	    }

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

	    function getLoggedInUser(callback) {
	        var deferred = $q.defer();
	        if ($.isEmptyObject(loggedInUser)) {
	            $http({
	                    method: "GET",
	                    url: config.apiUrl + 'users/getLoggedInUser',
	                    withCredentials: true
	                })
	                .success(function(data) {
	                    loggedInUser = data;
	                    deferred.resolve({ success: true, data: data });
	                })
	                .error(function(data, status, headers) {
	                    console.log('error getting logged in user' + status);
	                });
	        } else {
	            deferred.resolve({ success: true, data: loggedInUser });
	        }

	        return deferred.promise;
	    }

	    /*--------------------------------------------------------------------------------------------*/
	    return {	       
	        GetUsers : getUsers,
	        GetTeams: getTeams,
	        GetUsersByTeams: getUsersByTeams,
	        GetUserWithPermissions: getUserWithPermissions,
	        GetMaintenanceUserWithPermissions: getMaintenanceUserWithPermissions,
	        GetCorporatePriorityUsers: getCorporatePriorityUsers,
	        GetLoggedInUser: getLoggedInUser
	    };
	});
