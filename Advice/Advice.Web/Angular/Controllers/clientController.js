var clientController = function ($rootScope, $scope, clientService, $modal, $q, $window) {
    var showMoreString = "Show more..";
    $scope.selectedCan = "";
    $scope.selectedClientName = "";
    var clientNames = [];
    var cans = [];
    var searchByString = "";
    var searchType = "";

    $scope.getCansStartWith = function (can) {
        cans = clientService.GetCansStartWith(can);
        searchByString = can;
        searchType = "SearchByCan";
        return cans;
    }

    $scope.getClientNamesStartWith = function (clientName) {
        clientNames = clientService.GetClientNamesStartWith(clientName);
        searchByString = clientName;
        searchType = "SearchByClientName";
        return clientNames;
    }

    $scope.onCanSelect = function ($item) {
        if ($item != showMoreString) {
            clientService.GetClientByCan($item)
                .then(function (result) {
                    if (result.success) {
                        $window.open($rootScope.advice2ClientDetailsSummaryLink + result.data.ClientId, '_blank');
                    }
                });

        } else {

            loadClientDetailsPopUp();
        }

    };

    $scope.onClientNameSelect = function ($item) {

        if ($item != showMoreString) {
            clientService.GetClientByName($item)
                .then(function (result) {
                  if (result.success) {
                      $window.open($rootScope.advice2ClientDetailsSummaryLink + result.data.ClientId, '_blank');
                  }
              });

        } else {

            loadClientDetailsPopUp();
        }
        
    };

    var loadClientDetailsPopUp = function () {

        $scope.selectedCan = "";
        $scope.selectedClientName = "";

        var options = {
            "SearchType": searchType,
            "SearchString": searchByString
        };

        $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/Partials/PopUps/_clientSearchDetails.html',
            controller: clientSearchDetailsController,
            resolve: {
                Options: function() {
                    return options;
                }
            }
        });
    };

    $scope.clientSummaryRedirect = function (clientId) {
        $window.open($rootScope.advice2ClientDetailsSummaryLink + clientId, '_blank');
    };

    /*****************************************Recent Clients Action*******************************************/
    var recentClientsActionController = function ($modalInstance) {
        $scope.recentClientsAction = [];
        $scope.recentClientsActionFound = false;
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };

        clientService.GetRecentClientsAction().then(function (result) {
            if (result.success) {
                $scope.recentClientsAction = result.data;
                if (result.data.length > 0) {
                    $scope.recentClientsActionFound = true;
                }
            }
        });

        
    };

    $scope.getRecentClientsAction = function () {
        $modal.open({
            scope: $scope,
            templateUrl: 'Angular/Views/Partials/PopUps/_recentClientsAction.html',
            controller: recentClientsActionController,
        });
    };
};
