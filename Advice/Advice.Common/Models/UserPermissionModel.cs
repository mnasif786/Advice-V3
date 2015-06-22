using System.Collections.Generic;
using System.Linq;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Common.Models
{
    public class UserPermissionModel
    {
        public bool CreateNewTask {get; set;}
        public bool CreateNewTaskOtherUser {get; set;}
        public bool ReAssignTaskToEsAdvisor {get; set;}
        public bool ReAssignTaskToEsa {get; set;}
        public bool ReAssignTaskToEsSupportConsultant {get; set;}
        public bool ReAssignTaskToEsCaseHandler {get; set;}
        public bool ReAssignTaskToEsTeamLeader {get; set;}
        public bool ReAssignTaskToEsManager {get; set;}
        public bool EditTaskOwnTask {get; set;}
        public bool EditTaskOtherUser {get; set;}
        public bool DeleteTaskOwn {get; set;}
        public bool DeleteTaskOtherUser {get; set;}
        public bool ResetTask {get; set;}
        public bool CompleteOwnTask {get; set;}
        public bool CompleteOtherUserTask {get; set;}
        public bool ViewOtherTaskLists {get; set;}
        public bool AddNewJob {get; set;}
        public bool AddNewAction {get; set;}
        public bool EditActionAll {get; set;}
        public bool EditActionInternalNotes {get; set;}
        public bool EditJobDetails {get; set;}
        public bool DeleteAction {get; set;}
        public bool DeleteJob {get; set;}
        public bool MoveAction {get; set;}
        public bool MoveJob {get; set;}
        public bool CreateGeneralContact {get; set;}
        public bool CreateAuthorisedContact {get; set;}
        public bool EditGeneralContact {get; set;}
        public bool EditAuthorisedContact {get; set;}
        public bool CreateAdviceNote {get; set;}
        public bool EditAdviceNote {get; set;}
        public bool DeleteAdviceNote {get; set;}
        public bool EditUser {get; set;}
        public bool ChangeUserTeam {get; set;}
        public bool ChangeUserRole {get; set;}
        public bool ProcessEmailInbox {get; set;}
        public bool BulkReassignRecipient {get; set;}
        public bool BypassCallerNameCheck {get; set;}
        public bool ModifyActionOtherUser {get; set;}
        public bool ReAssignTaskToCentralAdministration {get; set;}
        public bool ReAssignTaskToNonEsOvertimeWorker {get; set;}
        public bool ReAssignTaskToRoiNiClientServices {get; set;}
        public bool ReAssignTaskToRoiNiTraineeAdvisor {get; set;}
        public bool ReAssignTaskToTaxWiseUser {get; set;}
        public bool ReAssignTaskToTaxWiseManager {get; set;}
        public bool ReAssignTaskToHsAdvisor {get; set;}
        public bool ViewOutOfHoursTaskList {get; set;}
        public bool ReAssignTaskToEsSpecialProjects {get; set;}
        public bool ReAssignTaskToEsOutofHoursUser {get; set;}
        public bool ReAssignTaskToReadOnly {get; set;}
        public bool ModifyJobOtherUser {get; set;}
        public bool EditContactType {get; set;}
        public bool ViewHsOutOfHoursTeam {get; set;}
        public bool ViewSuspendedClient {get; set;}
        public bool ReAssignTaskToHsOutofHoursUser {get; set;}
        public bool ReAssignTaskToHsManager {get; set;}
        public bool ReAssignTaskToHsSpecialProjects {get; set;}
        public bool ReAssignTaskToHsTeamLeader {get; set;}
        public bool AccessCallRecorder {get; set;}
        public bool ViewQa {get; set;}
        public bool AddQa {get; set;}
        public bool EditQa {get; set;}
        public bool DeleteQa {get; set;}
        public bool ActionTyperestrictedtoInternal {get; set;}
        public bool ViewGraphitePremiumClient {get; set;}
        public bool AddGraphiteConsultancyAction {get; set;}
        public bool ViewPeninsulaClient {get; set;}
        public bool DeleteAdviceContact {get; set;}
        public bool Developer { get; set; }

        public static UserPermissionModel Create(IList<Permission> permissions)
        {
            var userPermissionModel  = new  UserPermissionModel
            {
                CreateNewTask = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CreateNewTask),
                CreateNewTaskOtherUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CreateNewTaskOtherUser),
                ReAssignTaskToEsAdvisor = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsAdvisor),
                ReAssignTaskToEsa = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsa),
                ReAssignTaskToEsSupportConsultant = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsSupportConsultant),
                ReAssignTaskToEsCaseHandler = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsCaseHandler),
                ReAssignTaskToEsTeamLeader = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsTeamLeader),
                ReAssignTaskToEsManager = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsManager),
                EditTaskOwnTask = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditTaskOwnTask),
                EditTaskOtherUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditTaskOtherUser),
                DeleteTaskOwn = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteTaskOwn),
                DeleteTaskOtherUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteTaskOtherUser),
                ResetTask = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ResetTask),
                CompleteOwnTask = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CompleteOwnTask),
                CompleteOtherUserTask = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CompleteOtherUserTask),
                ViewOtherTaskLists = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewOtherTaskLists),
                AddNewJob = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.AddNewJob),
                AddNewAction = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.AddNewAction),
                EditActionAll = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditActionAll),
                EditActionInternalNotes = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditActionInternalNotes),
                EditJobDetails = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditJobDetails),
                DeleteAction = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteAction),
                DeleteJob = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteJob),
                MoveAction = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.MoveAction),
                MoveJob = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.MoveJob),
                CreateGeneralContact = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CreateGeneralContact),
                CreateAuthorisedContact = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CreateAuthorisedContact),
                EditGeneralContact = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditGeneralContact),
                EditAuthorisedContact = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditAuthorisedContact),
                CreateAdviceNote = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.CreateAdviceNote),
                EditAdviceNote = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditAdviceNote),
                DeleteAdviceNote = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteAdviceNote),
                EditUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditUser),
                ChangeUserTeam = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ChangeUserTeam),
                ChangeUserRole = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ChangeUserRole),
                ProcessEmailInbox = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ProcessEmailInbox),
                BulkReassignRecipient = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.BulkReassignRecipient),
                BypassCallerNameCheck = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.BypassCallerNameCheck),
                ModifyActionOtherUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ModifyActionOtherUser),
                ReAssignTaskToCentralAdministration = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToCentralAdministration),
                ReAssignTaskToNonEsOvertimeWorker = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToNonEsOvertimeWorker),
                ReAssignTaskToRoiNiClientServices = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToRoiNiClientServices),
                ReAssignTaskToRoiNiTraineeAdvisor = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToRoiNiTraineeAdvisor),
                ReAssignTaskToTaxWiseUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToTaxWiseUser),
                ReAssignTaskToTaxWiseManager = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToTaxWiseManager),
                ReAssignTaskToHsAdvisor = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToHsAdvisor),
                ViewOutOfHoursTaskList = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewOutOfHoursTaskList),
                ReAssignTaskToEsSpecialProjects = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsSpecialProjects),
                ReAssignTaskToEsOutofHoursUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToEsOutofHoursUser),
                ReAssignTaskToReadOnly = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToReadOnly),
                ModifyJobOtherUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ModifyJobOtherUser),
                EditContactType = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditContactType),
                ViewHsOutOfHoursTeam = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewHsOutOfHoursTeam),
                ViewSuspendedClient = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewSuspendedClient),
                ReAssignTaskToHsOutofHoursUser = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToHsOutofHoursUser),
                ReAssignTaskToHsManager = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToHsManager),
                ReAssignTaskToHsSpecialProjects = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToHsSpecialProjects),
                ReAssignTaskToHsTeamLeader = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ReAssignTaskToHsTeamLeader),
                AccessCallRecorder = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.AccessCallRecorder),
                ViewQa = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewQa),
                AddQa = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.AddQa),
                EditQa = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.EditQa),
                DeleteQa = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteQa),
                ActionTyperestrictedtoInternal = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ActionTyperestrictedtoInternal),
                ViewGraphitePremiumClient = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewGraphitePremiumClient),
                AddGraphiteConsultancyAction = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.AddGraphiteConsultancyAction),
                ViewPeninsulaClient = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.ViewPeninsulaClient),
                DeleteAdviceContact = permissions.Any(p => p.PermissionID == (int)PermissionsEnum.DeleteAdviceContact)
            };

            return userPermissionModel;
        }
    }

    
}
