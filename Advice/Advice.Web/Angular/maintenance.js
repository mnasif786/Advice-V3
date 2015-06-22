/* Maintenance App Module */
'use strict';

var maintenanceApp = angular.module('maintenanceApp', [
    'ngRoute',
    'ui.bootstrap',
    'maintenanceUserServicesModule',
    'configModule',
    'teamServicesModule',
    'departmentServicesModule',
    'divisionServicesModule',
    'corporatePriorityServicesModule',
    'clientServicesModule',
    'multiselectDirectiveModule',
    'dialogServicesModule'
]);

maintenanceApp.config(['$routeProvider', '$locationProvider', '$httpProvider', '$sceProvider', function ($routeProvider, $locationProvider, $httpProvider, $sceProvider) {
    
    //================================================
    // Routes
    //================================================
    $routeProvider.when('/maintenance',
      {
          templateUrl: 'Angular/Maintenance/Views/maintenance.html',
          controller: maintenanceController
      });

    $sceProvider.enabled(false);
    /* if using an HTML5 browser, remove # from URLs*/
    /* $locationProvider.html5Mode(true)*/
  
}]);



//================================================
// Application Startup
//================================================
maintenanceApp.run(['$http', '$rootScope', 'configService', 'maintenanceUserService', '$window', function ($http, $rootScope, configService, maintenanceUserService, $window) {

    //Get the loggedInUser
    $rootScope.loggedInUser = null;
    maintenanceUserService.GetMaintenanceUserWithPermissions().
        then(function (userResult) {
            $rootScope.loggedInUser = userResult.data;
        });

    $rootScope.advice2AddTask = configService.getConfig().advice2Root + "CreateUpdateTask.aspx";
    $rootScope.advice2UserAdmin = configService.getConfig().advice2Root + "UserMaintenance.aspx";

    $rootScope.advice2Reports = "http://pbsreports/SQLReports2008R2/Pages/Folder.aspx?ItemPath=%2fAdvice+v2.0&ViewMode=List";
    $rootScope.advice2MyWork = "http://pbsreports/SQLReports2008R2/Pages/Report.aspx?ItemPath=%2fAdvice+v2.0%2f1_Performance+Management%2fAdvisorAdviceProductivity";

    $rootScope.appCacheStatus = "";

    try {
        // Fired after the first cache of the manifest.
        $window.applicationCache.addEventListener('cached', handleCacheEvent, false);

        // Checking for an update. Always the first event fired in the sequence.
        $window.applicationCache.addEventListener('checking', handleCacheEvent, false);

        // An update was found. The browser is fetching resources.
        $window.applicationCache.addEventListener('downloading', handleCacheEvent, false);

        // The manifest returns 404 or 410, the download failed,
        // or the manifest changed while the download was in progress.
        $window.applicationCache.addEventListener('error', handleCacheError, false);

        // Fired after the first download of the manifest.
        $window.applicationCache.addEventListener('noupdate', handleCacheEvent, false);

        // Fired if the manifest file returns a 404 or 410.
        // This results in the application cache being deleted.
        $window.applicationCache.addEventListener('obsolete', handleCacheEvent, false);

        // Fired for each resource listed in the manifest as it is being fetched.
        $window.applicationCache.addEventListener('progress', handleCacheEvent, false);

        // Fired when the manifest resources have been newly redownloaded.
        $window.applicationCache.addEventListener('updateready', handleCacheEvent, false);
    } catch (e) {
        console.log('Error creating event handlers for the application cache');
        console.log(e);
    }

    function handleCacheEvent(e) {
        var appCache = $window.applicationCache;

        switch (appCache.status) {
            case appCache.UNCACHED: // UNCACHED == 0
                $rootScope.appCacheStatus = "";
                break;
            case appCache.IDLE: // IDLE == 1
                $rootScope.appCacheStatus = "";
                break;
            case appCache.CHECKING: // CHECKING == 2
                $rootScope.appCacheStatus = "Checking for update...  ";
                break;
            case appCache.DOWNLOADING: // DOWNLOADING == 3
                $rootScope.appCacheStatus = "Downloading update...  ";
                break;
            case appCache.UPDATEREADY: // UPDATEREADY == 4
                cacheUpdateReady();
                break;
            case appCache.OBSOLETE: // OBSOLETE == 5
                $rootScope.appCacheStatus = "Error downloading application cache";
                break;
            default:
                $rootScope.appCacheStatus = "Unknown application cache status";
                break;
        };
    }


    $rootScope.checkForNewVersion = function (windowObject)
    {      
        try {
            if (angular.isDefined(windowObject.applicationCache))
            {
                switch (windowObject.applicationCache.status)
                {
                    case windowObject.applicationCache.IDLE:
                        windowObject.applicationCache.update();
                        break;
                    case windowObject.applicationCache.UPDATEREADY:
                        cacheUpdateReady();
                        break;
                }

            }
        }
        catch (err)
        {
            console.log("Error checking for latest version");
            console.log(err);
        }            
    };


    //=================================================================================
    // Check for new versions
    //================================================================================
    function cacheUpdateReady() {
        $rootScope.appCacheStatus = "Update ready   ";

        if (confirm('A new version of this site is available. Load it?')) {           
            try {
                $window.applicationCache.swapCache();
                $window.location.reload();
                
            } catch (e) {
                console.log("Error reloading page. " + e);
                alert("Error reloading page. " + e);                
            }

        }
    }

    function handleCacheError(e) {
        console.log('Error: Cache failed to update!');
        $rootScope.appCacheStatus = "Error downloading application cache";
    };

}]);

function isNullOrUndefined(val) {
    return (val == null || typeof val === 'undefined');
}

function isNullOrEmptyString(value) {
    return value == null || value == '' || value.length==0;
}

function isNullOrUndefinedorEmpty(val) {
    return val == null || typeof val === 'undefined' || (typeof val !== 'undefined' && val == '') || (typeof val !== 'undefined' && val.length == 0);
}


function isDecimal(value) {
    var decimalRegex = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
    return decimalRegex.test(value);
}

function DatePickerSettings() {
    this.opened = false;
    this.format = 'dd/MM/yyyy';
    this.dateOptions = { formatYear: 'yy', startingDay: 1 };
    this.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        this.opened = true;
    };
    this.maxDate = new Date(new Date().setDate(new Date().getDate() + 30));
    this.minDate = new Date();
};
