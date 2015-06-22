var reAssignTaskController = function ($rootScope, $scope, $modalInstance, $filter, taskModifyingReasonService, utilService, taskDetails, urgent, teams, users) {

    $scope.ResetTaskPermission = $rootScope.loggedInUser.Permissions.ResetTask;
    $scope.teamList = $filter('filter')(teams, { "IsSpecial": false }, true);
    $scope.userList = users;
    $scope.DueDate = taskDetails.dueDate;
    $scope.taskModifyingReasonsList = null;
    $scope.selectedItems = {
        Team: null,
        User: null,
        ResetSla: false,
        Urgent: urgent,
        TaskModifyingReason: null,
        SlaResetDate: null,
        SlaTime: { time: '' },
        Comments: { comment: '' },
        Manual: false,
        ManualTime: { Hour: "", Min: "" }
    }

    /***Validation Object classes*/
    function slaTimeFieldValidator(requiredIf) {
        this.errors = {
            RequiredIf: requiredIf,
            LessThanTodaysTime: !requiredIf && isSelectedManualTimeLessThanTodaysTime()
        }
        this.hasError = requiredIf || this.errors.LessThanTodaysTime;
    }

    function teamFieldValidator(requiredIf) {
        this.hasError = requiredIf;
        this.errors = {
            RequiredIf:  requiredIf
        }
    }

    function userFieldValidator(requiredIf) {
        this.hasError = requiredIf;
        this.errors = {
            RequiredIf: requiredIf
        }
    }

    function commentFieldValidator(requiredIf) {
        this.hasError = requiredIf; // || maxLength;
        this.errors =  {
            RequiredIf: requiredIf,
            //MaxLength: !requiredIf && maxLength //In a case of field requie more thenone validation. i.e. if requiredif not validated then do not need to show this message
        }
    }

    function reasonFieldValidator(required) {
        this.hasError = required;
        this.errors = {
            Required: required
        }
    }

    function dateFieldValidator(requiredIf) {
        this.hasError = requiredIf;
        this.errors = {
            RequiredIf: requiredIf
        }
    }

    /****************************/

    $scope.teamSelectionChange = function () {
        $scope.selectedItems.User = null;

        if ($scope.selectedItems.Team != null) {
            var selectedTeamId = $scope.selectedItems.Team.TeamId;
            $scope.userList = $filter('filter')(users, { "TeamId": selectedTeamId }, true);
        } else {
            $scope.userList = users;
        }
    }

    $scope.resetSlaChange = function() {
        if ($scope.selectedItems.ResetSla == false) {
            $scope.selectedItems.SlaResetDate = null;
        }
    }

    $scope.selectedUserUpdateDetails= function() {
        
    }



    $scope.save = function () {
        
        $scope.fields.team = new teamFieldValidator(teamOrUserIsNotSelected());

        $scope.fields.user = new userFieldValidator(teamOrUserIsNotSelected());

        $scope.fields.slaResetDate = new dateFieldValidator($scope.selectedItems.ResetSla && isNullOrUndefined($scope.selectedItems.SlaResetDate));

        $scope.fields.slaTime = new slaTimeFieldValidator($scope.selectedItems.ResetSla && $scope.selectedItems.Manual && !validateTime($scope.selectedItems.ManualTime.Hour, $scope.selectedItems.ManualTime.Min));
        
        $scope.fields.reason = new reasonFieldValidator(isNullOrUndefined($scope.selectedItems.TaskModifyingReason));

        $scope.fields.comment = new commentFieldValidator($scope.selectedItems.ResetSla && $scope.selectedItems.Comments.comment == '');
        
       if (!$scope.fields.team.hasError &&
            !$scope.fields.user.hasError && 
             !$scope.fields.comment.hasError &&
                !$scope.fields.reason.hasError && 
                    !$scope.fields.slaResetDate.hasError &&
                        !$scope.fields.slaTime.hasError) {

           $modalInstance.close(getReassignTaskModel());
        }
    }

    

    var getReassignTaskModel = function () {

        var reassignTaskModel = { TaskId: taskDetails.taskId, AssignedUser: null, AssignedTeamId: null, Urgent: false, Comments: '', DueDate: null, ReasonId: 0 }

        //Only pick team if user is not selected i.e. Only team or a user can be asssigned at a time. User has high precedence
        if ($scope.selectedItems.User == null) {
            reassignTaskModel.AssignedTeamId = $scope.selectedItems.Team.TeamId;
        }

        if (!isNullOrUndefined($scope.selectedItems.User)) {
            reassignTaskModel.AssignedUser =  $scope.selectedItems.User.Username;    
        }
        
        reassignTaskModel.ReasonId = $scope.selectedItems.TaskModifyingReason.Id;
        reassignTaskModel.Comments = $scope.selectedItems.Comments.comment;

        if ($scope.selectedItems.ResetSla) {
            var newSlaDate = $filter('date')($scope.selectedItems.SlaResetDate, 'EEE MMM dd yyyy'); //format chosen to make it working for IE
            var newSlaTime = $filter('date')($scope.selectedItems.Manual ? getManualTime() : $scope.selectedItems.SlaTime.time, 'HH:mm');

            reassignTaskModel.DueDate = newSlaDate + " " + newSlaTime;
        }

        reassignTaskModel.Urgent = $scope.selectedItems.Urgent;

        return reassignTaskModel;
    }

   

    var teamOrUserIsNotSelected = function() {
        return isNullOrUndefined($scope.selectedItems.Team) && isNullOrUndefined($scope.selectedItems.User);
    }

   
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    var loadTaskModifiedReasons = function() {
        var promise = taskModifyingReasonService.GetTaskModifyingReasonsForReassignGroup();
        promise.then(function (result) {
            if (result.success) {
                $scope.taskModifyingReasonsList = result.data;
            }
        });
    }

    $scope.setSlaTimes = function () {
        var slaTimes = utilService.GetSlaTimes($scope.selectedItems.SlaResetDate);
        $scope.selectedItems.SlaTime.time = slaTimes[0];
        $scope.slaTimeList = slaTimes;
    };

    var getManualTime = function () {
        return $scope.selectedItems.ManualTime.Hour + ':' + $scope.selectedItems.ManualTime.Min;
    }

    var isSelectedManualTimeLessThanTodaysTime = function () {

        var today = $filter('date')(new Date(), 'dd/MM/yyyy');
        var pickedDate = $filter('date')($scope.selectedItems.SlaResetDate, 'dd/MM/yyyy');

        var selectedTime = $filter('date')(getManualTime(), 'HH:mm');
        var currentTime = $filter('date')(new Date(), 'HH:mm');

        if (today == pickedDate && (selectedTime < currentTime)) {
            return true;
        }

        return false;
    }

    var init = function() {

        loadTaskModifiedReasons();

        //Populate field validators
        $scope.fields = {
            team: new teamFieldValidator(false),
            user: new userFieldValidator(false),
            reason: new reasonFieldValidator(false),
            comment: new commentFieldValidator(false),
            slaResetDate: new dateFieldValidator(false),
            slaTime: new slaTimeFieldValidator(false)
        }

        $scope.dtPickerickerSettings = utilService.GetDatePickerSettings();

        $scope.setSlaTimes();
    }

    init();
}