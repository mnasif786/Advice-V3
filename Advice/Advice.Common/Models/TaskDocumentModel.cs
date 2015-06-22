using System;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class TaskDocumentModel
    {
        public long TaskDocumentID { get; set; }
        public long TaskID { get; set; }
        public long DocumentID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DocumentModel Document { get; set; }

        public static TaskDocumentModel Create(TaskDocument taskDoc)
        {
            if (taskDoc == null)
                return null;
            
            var taskModel = new TaskDocumentModel()
            {
                TaskDocumentID = taskDoc.TaskDocumentID,
                TaskID = taskDoc.TaskID,
                DocumentID = taskDoc.DocumentID,
                CreatedBy = taskDoc.CreatedBy,
                CreatedDate = taskDoc.CreatedDate,
                LastModifiedBy = taskDoc.LastModifiedBy,
                LastModifiedDate = taskDoc.LastModifiedDate,
                Deleted = taskDoc.Deleted,
                DeletedBy = taskDoc.DeletedBy,
                DeletedDate = taskDoc.DeletedDate,
                Document = DocumentModel.Create(taskDoc.Document)
            };

            return taskModel;
        }
    }
}
