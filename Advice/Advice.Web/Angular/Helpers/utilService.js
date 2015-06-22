angular
    .module('utilServicesModule', [])
    .factory('utilService', function (constants, $filter) {
        var getSlaTimes = function (datePickerDate) {
            if (datePickerDate != null) {
                var currentTimeString = $filter('date')(new Date(), 'HH:mm');
                var currentHour = parseInt(currentTimeString.substring(0, 2));
                var today = $filter('date')(new Date(), 'dd/MM/yyyy');;
                var pickedDate = $filter('date')(datePickerDate, 'dd/MM/yyyy');

                if (pickedDate == today) {
                    var slaTimes = [];
                    angular.copy(constants.slaTime, slaTimes);
                    var indexCount = 1;
                    angular.forEach(slaTimes, function (value) {
                        var slaHour = parseInt(value.substring(0, 2));
                        if (currentHour >= slaHour) {
                            indexCount++;
                        }
                    });

                    var currentMin = parseInt(currentTimeString.substring(3));
                    if (currentMin > 30) {
                        indexCount--;
                    } else {
                        indexCount -= 2;
                    }
                    slaTimes.splice(0, indexCount);

                    return slaTimes;
                }
            }

            return constants.slaTime;
        };

        var datePickerSettings = function() {
            this.opened = false;
            this.format = 'dd/MM/yyyy';
            this.dateOptions = { formatYear: 'yy', startingDay: 1 };
            this.open = function($event) {
                $event.preventDefault();
                $event.stopPropagation();
                this.opened = true;
            };
            this.maxDate = new Date(new Date().setDate(new Date().getDate() + 30));
            this.minDate = new Date();
        };

        var getDatePickerSettings = function() {
            return new datePickerSettings();
        }

        return {
            GetSlaTimes: getSlaTimes,
            GetDatePickerSettings: getDatePickerSettings
        };
    });