var resetSlaController = function ($scope, $modalInstance, $filter, taskModifyingReasonService, utilService, TaskDetails) {

    /*****************Variable Initialisation *************************/
    $scope.selectedTaskModifyingReasonsForResetGroup = { selectedReason: null }; //popup stores value into model when variable is accessed through a dot in html.
    $scope.taskDetailsData = TaskDetails;
    $scope.resetTaskSlaModel = { TaskId: $scope.taskDetailsData.TaskId, DueDate: new Date(), Urgent: false, TaskModifyingReasonId: null, Comments: "", Manual : false, ManualTime: { Hour: "", Min: "" } };


    /*******************************************reset Task/Cancel Buttons*******************************/
    $scope.Save = function () {

        //var manualTime = $scope.resetTaskSlaModel.ManualTime.Hour + ':' + $scope.resetTaskSlaModel.ManualTime.Min;
        

        $scope.fields.comments = new commentsValidator(true);
        $scope.fields.reason = new reasonValidator(true);
        $scope.fields.newSla = new datePickerValidator(true);
        $scope.fields.slaTime = new slaTimerValidator($scope.resetTaskSlaModel.Manual);

        if (!$scope.fields.comments.hasError
            && !$scope.fields.reason.hasError
            && !$scope.fields.newSla.hasError
            && !$scope.fields.slaTime.hasError) {
            $modalInstance.close(getResetTaskSlaModel());
        }
        
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    /*************************************Saving Reset Task SLA*****************************/
    var getResetTaskSlaModel = function () {
        var newSlaDate = $filter('date')($scope.datePicker.datePicked, 'EEE MMM dd yyyy'); //format chosen to make it working for IE
        var newSlaTime = $filter('date')( $scope.resetTaskSlaModel.Manual ? getManualTime() : $scope.selectedTime.time, 'HH:mm');
        $scope.resetTaskSlaModel.DueDate = newSlaDate + " " + newSlaTime;
        $scope.resetTaskSlaModel.TaskModifyingReasonId = $scope.selectedTaskModifyingReasonsForResetGroup.selectedReason.Id;
        var data = $scope.resetTaskSlaModel;
        return data;
    };

    /**********************Loading Task Modifying Reason For Reset Group*****************/


    $scope.getTaskModifyingReasonsForResetGroup = function () {

        taskModifyingReasonService.GetTaskModifyingReasonsForResetGroup().
            then(function (taskModifyingReasonResult) {
                $scope.taskModifyingReasonsForResetGroup = taskModifyingReasonResult.data;
            });
    };

    $scope.getTaskModifyingReasonsForResetGroup();

    /***********************************Controls Validation****************************/
    $scope.fields = {
        comments: new commentsValidator(false),
        reason: new reasonValidator(false),
        newSla: datePickerValidator(false),
        slaTime:  new slaTimerValidator(false)
    }

    function slaTimerValidator(validate) {
        this.hasError = false;
        this.errors = {
            RequiredIf: { On: false, Message: '' },
            LessThanTodaysTime: { On: false, Message: ''}
        }

        this.validate = function () {
            var isManualTimeValid = validateTime($scope.resetTaskSlaModel.ManualTime.Hour, $scope.resetTaskSlaModel.ManualTime.Min);
            if (isManualTimeValid == false) {
                this.hasError = true;
                this.errors.RequiredIf.On = true;
                this.errors.RequiredIf.Message = "Please enter time in format 'hh:mm'";
            }

            if (isManualTimeValid && isSelectedManualTimeLessThanTodaysTime()) {
                this.hasError = true;
                this.errors.LessThanTodaysTime.On = true;
                this.errors.LessThanTodaysTime.Message = 'Cannot specify time in past.';
            }
        }

        if (validate) {
            this.validate();
        }
    }

    function datePickerValidator(validate) {
        this.hasError = false;
        this.errors = {
            RequiredIf: { On: false, Message: '' }
        }

        this.validate = function () {
            if ($scope.datePicker.datePicked == null) {
                this.hasError = true;
                this.errors.RequiredIf.On = true;
                this.errors.RequiredIf.Message = 'Please select date';
            }
        }

        if (validate) {
            this.validate();
        }
    }

    function reasonValidator(validate) {
        this.hasError = false;
        this.errors = {
            RequiredIf: { On: false, Message: '' }
        }
        this.validate = function () {
            if ($scope.selectedTaskModifyingReasonsForResetGroup.selectedReason == null) {
                this.hasError = true;
                this.errors.RequiredIf.On = true;
                this.errors.RequiredIf.Message = 'Please enter reason';
            }
        }

        if (validate) {
            this.validate();
        }
    }

    function commentsValidator(validate) {

        this.hasError = false;
        this.errors = {
            RequiredIf: { On: false, Message: '' }
        }
        this.validate = function () {
            if ($scope.resetTaskSlaModel.Comments.length <= 0) {
                this.hasError = true;
                this.errors.RequiredIf.On = true;
                this.errors.RequiredIf.Message = 'Please enter comments';
            }
        }

        if (validate) {
            this.validate();
        }
    }
    /**********************Date Picker Setting **************************************/
    $scope.dtPickerickerSettings = utilService.GetDatePickerSettings();
    $scope.datePicker = { datePicked: null };

    /**********************Time Picker Setting **************************************/
    $scope.setSlaTimes = function () {
        var slaTimes = utilService.GetSlaTimes($scope.datePicker.datePicked);
        $scope.selectedTime = { time: slaTimes[0] };
        $scope.slaTime = slaTimes;
    };

    $scope.setSlaTimes();

   //validates time as per business rule
    //function validateTime(hour, min) {

    //    if (hour.trim().length < 2 || min.trim().length < 2) // to enforce two digit format
    //        return false;

    //    var validHour = (parseInt(hour) != NaN) && (parseInt(hour) < 24);
    //    var validMin = (parseInt(min) != NaN) && (parseInt(min) <= 59);
        
    //    return (validHour && validMin);
    //}

    var getManualTime = function() {
        return $scope.resetTaskSlaModel.ManualTime.Hour + ':' + $scope.resetTaskSlaModel.ManualTime.Min;
    }

    var isSelectedManualTimeLessThanTodaysTime = function() {
        
        var today = $filter('date')(new Date(), 'dd/MM/yyyy');
        var pickedDate = $filter('date')($scope.datePicker.datePicked, 'dd/MM/yyyy');

        var selectedTime = $filter('date')(getManualTime(), 'HH:mm');
        var currentTime = $filter('date')(new Date(), 'HH:mm');
       
        if (today == pickedDate && (selectedTime < currentTime)) {

            //var currentHour  = 
            return true;
        }

        return false;
    }

};
