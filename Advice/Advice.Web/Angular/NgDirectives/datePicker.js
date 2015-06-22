angular.module('adviceapp')
    .directive('datepicker', function () {

    return {
        require: 'ngModel',

        link: function (scope, el, attr, ngModel) {
            var picker = $(el).datepicker({
                format: "dd/mm/yyyy",
                autoclose: true
            }).on('changeDate', function (ev) {
                var day = picker.datepicker('getDate');
                if (day == null || day == undefined) {
                    return;
                }
                scope.$apply(function () {
                    $(el).datepicker("setDate", $(el).datepicker("getDate"));
                    ngModel.$setViewValue(day.toJSON());
                });
            });

            // Update the date picker when the model changes
            ngModel.$render = function () {
                var date = ngModel.$viewValue;

                if (date == "") {
                    date = null;
                    $(el)[0].value = '';
                }

                if (angular.isDefined(date) && date != null && date != '') {
                    if (!angular.isDate(date)) {
                        date = ParseDate(date);
                    }

                    if (!angular.isDate(date)) {
                        throw new Error('ng-Model value must be a Date object - currently it is a ' + typeof date + ' -  convert it from a string');
                    }


                    $(el).datepicker("setDate", date);
                }

            };

            $(el).next().children().click(function () {
                $(el).datepicker('show');
            });
        }
    };
});
