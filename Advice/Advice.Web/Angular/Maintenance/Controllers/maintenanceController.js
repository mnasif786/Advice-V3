var maintenanceController = function ($rootScope, $scope, $window, $modal, $filter, configService, teamService, maintenanceUserService, divisionService, corporatePriorityService, clientService, dialogService)
{
    $rootScope.checkForNewVersion($window);
    
    /******************************************************Variable Declaration**************************************************/
    //Local variables
    var cans = [];
    var searchByString = "";
    var searchType = "";

    //Scope variables
    $scope.selectedCan = { value: ''}
    $scope.hasMaintenanceAppPermission = false;
    $scope.teams = [];
    $scope.divisions = [];
    $scope.corporatePriorities = [];
    $scope.maintenanceUserPermission = {
        "hasTeamPermission": false,
        "hasDivisionPermission": false,
        "hasCorporatePriorityPermission": false,
        "hasCorporatePriorityManageUserPermission" : false
    };
    /******************************************************Teams**************************************************/
    var getAllTeamsWithDivisionAndDepartment = function () {
        var promise = teamService.GetAllTeamsWithDivisionAndDepartment();
        promise.then(function (result) {
            if (result.success) {
                $scope.teams = result.data;

            } else {
                $window.alert('Teams could not be retrieved');
            }
        });
    };

    $scope.editTeam = function (teamData) {

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/Team/_editTeam.html',
            controller: editTeamController,
            resolve: {
                TeamDetails: function () {
                    return teamData;
                }
            }
        });

        modalInstance.result.then(function (teamModel) {
            //Save button clicked
            var promise = teamService.EditTeam(teamModel);
            promise.then(function (result) {
                if (result.success) {
                    getAllTeamsWithDivisionAndDepartment();
                } else {
                    $window.alert('Failed!! Team could not be edited');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };

    $scope.addTeam = function () {

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/Team/_addTeam.html',
            controller: addTeamController
        });

        modalInstance.result.then(function (teamModel) {
            //Save button clicked
            var promise = teamService.AddTeam(teamModel);
            promise.then(function (result) {
                if (result.success) {
                    getAllTeamsWithDivisionAndDepartment();
                } else {
                    $window.alert('Failed!! Team could not be added');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };

    $scope.deleteTeam = function (teamData) {

        var team = {
            "teamId": teamData.TeamId,
            "teamName": teamData.Description
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/Team/_deleteTeam.html',
            controller: deleteTeamController,
            resolve: {
                teamDetails: function () {
                    return team;
                }
            }
        });

        modalInstance.result.then(function (teamId) {
            //Save button clicked
            var promise = teamService.DeleteTeam(teamId);
            promise.then(function (result) {
                if (result.success) {
                    getAllTeamsWithDivisionAndDepartment();
                } else {
                    $window.alert('Failed!! Team could not be deleted');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };

    $scope.reinstateTeam = function (teamData) {

        var options = {
            "Heading": 'Reinstate Team',
            "Message": 'Are you sure you want to reinstate the team ' + teamData.Description + '?',
            "ShowYesButton": true,
            "ShowNoButton": true
        };

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/NgTemplates/genericConfirmationPopup.html',
            controller: genericConfirmationPopupController,
            resolve: {
                Options: function () {
                    return options;
                }
            }
        });

        modalInstance.result.then(function () {
            //Yes clicked
            var promise = teamService.ReinstateTeam(teamData.TeamId);

            promise.then(function (result) {
                if (result.success) {
                    getAllTeamsWithDivisionAndDepartment();
                } else {
                    $window.alert('Team could not be reinstated');
                }
            });
        }, function () {
            //No clicked.
        });
    };

    /******************************************************Division**************************************************/
    var getAllDivisions = function () {
        var promise = divisionService.GetAllDivisions();
        promise.then(function (result) {
            if (result.success) {
                $scope.divisions = result.data;
            }
            else {
                $window.alert('Divisions could not be retrieved');
            }
        });
    };


    $scope.editDivision = function (divisionData) {

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/Division/_editDivision.html',
            controller: editDivisionController,
            resolve: {
                DivisionDetails: function () {
                    return divisionData;
                }
            }
        });

        modalInstance.result.then(function (divisionModel) {
            //Save button clicked
            var promise = divisionService.EditDivision(divisionModel);
            promise.then(function (result) {
                if (result.success) {
                    getAllDivisions();
                } else {
                    $window.alert('Failed!! Division could not be edited');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };


    $scope.addDivision = function () {

        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/Division/_addDivision.html',
            controller: addDivisionController
        });

        modalInstance.result.then(function (divisionModel) {
            //Save button clicked
            var promise = divisionService.AddDivision(divisionModel);
            promise.then(function (result) {
                if (result.success) {
                    getAllDivisions();
                } else {
                    $window.alert('Failed!! Division could not be added');
                }
            });

        }, function () {
            //Cancel clicked.
        });
    };

    /******************************************************Corporate Priority**************************************************/
    var getAllCorporatePriorities = function () {
        var promise = corporatePriorityService.GetAllCorporatePriorities();
        promise.then(function (result) {
            if (result.success) {
                $scope.corporatePriorities = result.data;
            }
            else {
                $window.alert('Corporate Priorities could not be retrieved');
            }
        });
    };

    $scope.getAllDivisionsForTab = function (){
        getAllDivisions();
    }

    $scope.refreshTeams = function (){
        getAllTeamsWithDivisionAndDepartment();
    }

    $scope.redirectToUserMaintenance = function () {
        $window.open($rootScope.advice2UserAdmin);
    }

    $scope.getAllCorporatePrioritiesForTab = function() {
        getAllCorporatePriorities();
    }

    $scope.getCansStartWith = function (can) {
        cans = corporatePriorityService.GetCansStartWith(can);
        return cans;
    }

    function addEditCorporatePriorityModel() {
        this.Id = null;
        this.can = null;
        this.clientName = null;
        this.contractValue = null;
        this.contractDetail = null;
        this.contractEndDate = null;
        this.user = null;
        this.addMode = true;

    }

    $scope.addCorporatePriority = function () {
       
        if (isNullOrEmptyString($scope.selectedCan.value)) {
            dialogService.ShowInformationDialog('Invalid CAN','Please enter a valid CAN to proceed.');
            return;
        }

        clientService.GetClientByCan($scope.selectedCan.value)
                .then(function (result) {
                    if (result.success) {
                        corporatePriorityService.GetCorporatePriorityByCan(result.data.Can)
                            .then(function (cpresult) {
                                //if cp already exists in database for that CAN then user cannot create again
                                if (cpresult.success) {
                                    dialogService.ShowInformationDialog('Duplicate','An entry for CAN: ' + result.data.Can + ' already exists.');
                                } else {
                                    var addCorporatePriorityModel = new addEditCorporatePriorityModel();
                                    addCorporatePriorityModel.can = result.data.Can;
                                    addCorporatePriorityModel.clientName = result.data.ClientName;
                                    openCorporatePriorityPopup(addCorporatePriorityModel);
                                }
                        });
                        
                    } else {
                        dialogService.ShowInformationDialog('Invalid CAN', 'The CAN you have entered is not a valid CAN.');
                    }
                });
    };

    $scope.editCorporatePriority = function (model) {
        var editCorporatePriorityModel = new addEditCorporatePriorityModel();
        editCorporatePriorityModel.Id = model.CorporatePriorityId;
        editCorporatePriorityModel.can=model.Can;
        editCorporatePriorityModel.clientName = model.ClientName;
        editCorporatePriorityModel.contractValue = model.ContractValue;
        editCorporatePriorityModel.contractDetail = model.ContractDetail;
        editCorporatePriorityModel.contractEndDate = model.ContractEndDate;
        editCorporatePriorityModel.user = { "UserId": model.UserId, "DisplayName": model.LeadConsultant };
        editCorporatePriorityModel.addMode = false;
        openCorporatePriorityPopup(editCorporatePriorityModel);
    };

    $scope.getCorporatePriorityContractClass = function (corporatePriority) 
    {
        var contractClass = "";

        var now = new Date();
        var endDate = new Date(corporatePriority.ContractEndDate);
        var sixMonthsFromNow = new Date(new Date(now).setMonth(now.getMonth()+6));
        var twelveMonthsFromNow = new Date(new Date(now).setMonth(now.getMonth()+12));
                     
        if ( endDate  < twelveMonthsFromNow)
        {
            contractClass = "contractEndIn12"
        }

        if (endDate < sixMonthsFromNow )
        {
            contractClass = "contractEndIn6"
        }

        if (endDate < now)
        {
            contractClass = "contractEnded"
        }

        return contractClass;
    }

    var openCorporatePriorityPopup = function (model) {
        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/CorporatePriority/_addEditCorporatePriorityPopup.html',
            controller: addEditCorportatePriorityPopupController,
            resolve: {
                model: function () {
                    return model;
                },
            }
        }); 
        modalInstance.result.then(function (requestModel) {
            if (model.addMode == true) {
                addNewCroporatePriority(requestModel);
            } else {
                updateCroporatePriority(requestModel);
            }

        }, function () {
            //Cancel clicked.
        });
    };

    var updateCroporatePriority = function(request) {
        var corporatePriorityEditRequest =
        {
            CorporatePriorityId: request.Id,
            ContractValue: request.ContractValue,
            ContractDetail: request.ContractDetail,
            ContractEndDate: request.ContractEndDate,
            UserId: request.User.UserId
        };
        
        corporatePriorityService.EditCorporatePriority(corporatePriorityEditRequest)
            .then(function (result) {
                if (result.success) {
                    getAllCorporatePriorities();
                } else {
                    $window.alert('Sorry!! CoporatePriority could not be edited.');
                }
            });
    }

    var addNewCroporatePriority = function (request) {
        var corporatePriorityAddRequest =
            { Can: request.Can, ContractValue: request.ContractValue, ContractDetail: request.ContractDetail, ContractEndDate: request.ContractEndDate, UserId: request.User.UserId }

        corporatePriorityService.AddCorporatePriority(corporatePriorityAddRequest)
            .then(function (result) {
            if (result.success) {
                getAllCorporatePriorities();
            } else {
                $window.alert('Sorry!! CoporatePriority could not be added.');
            }
        });
    }

    $scope.deleteCorporatePriority = function (corporatePrioritId) {
        $scope.spinner = false;
        var promise = corporatePriorityService.DeleteCorporatePriority(corporatePrioritId);
        promise.then(function (result) {
            if (result.success) {
                getAllCorporatePriorities();
            } else {
                $window.alert('Failed!! Corporate Priorit could not be deleted');
            }
            $scope.spinner = false;
        });
    }

    $scope.manageCorporatePriorityUsers = function() {
        var modalInstance = $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Maintenance/Views/Partials/CorporatePriority/_manageCorporatePriorityUserPopup.html',
            controller: manageCorporatePriorityUserController
        });

        modalInstance.result.then(function (model) {
            //Save button clicked

        }, function () {
            //Cancel clicked.
        });
    }

    var init = function() {
        $scope.templates = configService.getTemplates();
        
        maintenanceUserService.GetMaintenanceUserWithPermissions().
            then(function (userResult) {
                $scope.maintenanceUserPermission.hasTeamPermission = userResult.data.MaintenancePermissions.HasTeamPermission;
                $scope.maintenanceUserPermission.hasDivisionPermission = userResult.data.MaintenancePermissions.HasDivisionPermission;
                $scope.maintenanceUserPermission.hasCorporatePriorityPermission = userResult.data.MaintenancePermissions.CorporatePriorityPermission.HasCorporatePriorityPermission;
                $scope.maintenanceUserPermission.hasCorporatePriorityManageUserPermission = userResult.data.MaintenancePermissions.CorporatePriorityPermission.HasManageUserPermission;
        });
    }

    init();
};