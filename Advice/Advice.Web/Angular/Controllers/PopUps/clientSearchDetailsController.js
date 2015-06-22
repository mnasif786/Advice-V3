var clientSearchDetailsController = function ($rootScope, $scope, $modalInstance, clientService, Options, $window) {

    var options = Options;
    $scope.clients = [];
    clientSearchResultsGenerator(options.SearchType, options.SearchString);

    $scope.close = function () {
        $modalInstance.dismiss('cancel');
    };

    function clientSearchResultsGenerator(searchType, searchString) {

        this.searchByCan = function () {
            clientService.GetAllClientsStartWithCan(options.SearchString).then(function(result) {
                if (result.success) {
                    $scope.clients = result.data;
                }
            });
        };

        this.searchByCompanyName = function () {
            clientService.GetAllClientsStartWithClientName(options.SearchString).then(function (result) {
                if (result.success) {
                    $scope.clients = result.data;
                }
            });
        };

        if (searchType == "SearchByClientName") {
            this.searchByCompanyName(searchString);
        } else {
            this.searchByCan(searchString);
        }
    }
};
