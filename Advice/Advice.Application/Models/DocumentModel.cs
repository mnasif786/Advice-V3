using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;

namespace Advice.Application.Models
{
    public class DocumentModel
    {
        public long DocumentID { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        
        public DateTime? DeletedDate { get; set; }
        public long? DocumentLibraryId { get; set; }
        public long? DocumentTypeId { get; set; }

        public long? ScannedDocumentId { get; set; }

        public static DocumentModel Create(Document document)
        {
            if (document == null)
                return null;

            var doc = new DocumentModel
            {
                CreatedBy = document.CreatedBy,
                CreatedDate = document.CreatedDate,
                Description = document.Description,
                DocumentID = document.DocumentID,
                DocumentLibraryId = document.DocumentLibraryId,
                DocumentTypeId = document.DocumentTypeId,
                Extension = document.Extension,
                Filename = document.Filename,
                LastModifiedBy = document.LastModifiedBy,
                LastModifiedDate = document.LastModifiedDate,
                ScannedDocumentId = document.ScannedDocumentsID
            };

            return doc;
        }
    }
}
