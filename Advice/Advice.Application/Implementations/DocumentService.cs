using Advice.Application.Contracts;
using Advice.ScannedDocuments;

namespace Advice.Application.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly IScannedDocumentsService _scannedDocumentsService;

        public DocumentService(IScannedDocumentsService scannedDocumentsService)
        {
            _scannedDocumentsService = scannedDocumentsService;
        }

        public string GetFullDocumentPathByDocumentId(int documentId)
        {
            var document = _scannedDocumentsService.GetDocumentById(documentId);

            if (document == null) return null;

            return string.Format("\\\\{0}\\{1}{2}\\{3}", document.Server,document.Share,document.FilePath, document.Filename);
        }
    }
}
