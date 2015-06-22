using System;
using System.Configuration;
using System.Linq;

namespace Advice.ScannedDocuments
{
    public class ScannedDocumentsService : IScannedDocumentsService
    {
        public SPGetDocumentByIDResult GetDocumentById(int documentId)
        {
            if (ConfigurationManager.ConnectionStrings["ScannedDocuments"] == null)
            {
                throw new Exception("Must specify connection string for 'ScannedDocuments'");
            }

            string connString = ConfigurationManager.ConnectionStrings["ScannedDocuments"].ConnectionString;

            if (string.IsNullOrEmpty(connString))
            {
                throw new Exception("Must specify connection string for 'ScannedDocuments'");
            }

            var doc = new ScannedDocumentsDataContext(connString);

            var document = doc.SPGetDocumentByID(documentId);

            // can only call a method on this once, otherwise throws exception. returns empty if doc not found so ok
            return document.FirstOrDefault();
        }
    }
}
