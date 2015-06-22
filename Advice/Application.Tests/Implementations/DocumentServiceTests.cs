using Advice.Application.Contracts;
using Advice.Application.Implementations;
using Advice.ScannedDocuments;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations
{
    [TestFixture]
    public class DocumentServiceTests
    {
        private Mock<IScannedDocumentsService> _scannedDocs;
        private IDocumentService _documentService;

        [SetUp]
        public void SetUp()
        {
            _scannedDocs = new Mock<IScannedDocumentsService>();

            var result = new SPGetDocumentByIDResult()
            {
                FilePath = "\\pathway\\pathway1",
                Filename = "filename.xls"
            };

            _scannedDocs.Setup(x => x.GetDocumentById(It.IsAny<int>())).Returns(result);

            _documentService = new DocumentService(_scannedDocs.Object);
        }

        [Test]
        public void Given_Document_Exists_Then_Return_Full_Path()
        {
            var result = _documentService.GetFullDocumentPathByDocumentId(2);

            Assert.That(result, Is.EqualTo("\\\\\\\\pathway\\pathway1\\filename.xls"));
        }

        [Test]
        public void Given_document_doesnt_exist_then_return_empty_null()
        {
            _scannedDocs.Setup(x => x.GetDocumentById(It.IsAny<int>())).Returns((SPGetDocumentByIDResult)null);

            var result = _documentService.GetFullDocumentPathByDocumentId(2);

            Assert.That(result, Is.EqualTo(null));
        }
    }
}
