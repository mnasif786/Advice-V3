﻿angular
    .module('configModule', ['ngResource'])
    .service('configService', function ()
    {
        var _config = {
            apiUrlForNgResource: 'http://10.1.246.96\\:8106/api/',  //need to escape the port number when reference in a service that uses hgResource
            apiUrl: 'api/',
            version: 'versionNumber',
            automaticRefreshTime: 90000,

            //***** Dont Change Below - format needed for automatic update 
            advice2Root: '/advicev2_ui:8022/'
        };

        var _templates = {
            TaskSearchResult: { URL: 'Angular/Views/Partials/_taskSearchResult.html' },
            ProActiveTaskSearchResult: { URL: 'Angular/Views/Partials/_proactiveTaskSearchResult.html' },
            TaskDetails: { URL: 'Angular/Views/Partials/_taskDetails.html' },
            ClientSearch: { URL: 'Angular/Views/Partials/_clientSearch.html' },
            Teams: { URL: 'Angular/Maintenance/Views/Partials/Team/_teams.html' },
            Divisions: { URL: 'Angular/Maintenance/Views/Partials/Division/_divisions.html' },
            CorporatePriority: { URL: 'Angular/Maintenance/Views/Partials/CorporatePriority/_corporatepriority.html' }
        };

        function _getConfig() {
            return _config;
        }

        function _getTemplates() {
            return _templates;
        }

        return {
            getConfig: _getConfig,
            getTemplates: _getTemplates,
        };
    });