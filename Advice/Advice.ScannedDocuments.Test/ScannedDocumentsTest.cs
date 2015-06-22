using NUnit.Framework;

namespace Advice.ScannedDocuments.Test
{
    [TestFixture]
    public class ScannedDocumentsTest
    {
        private IScannedDocumentsService _scannedDocs;

        [SetUp]
        public void ScannedDocuments()
        {
            _scannedDocs = new ScannedDocumentsService();
        }

        [Test]
        public void Given_GetDocumentsByID_Called_then_valid_response_returned()
        {
            var response = _scannedDocs.GetDocumentById(5890312);

            Assert.That(response.Filename, Is.EqualTo("5890312.xls"));
            Assert.That(response.FilePath, Is.EqualTo("\\Advice\\Email Attachments\\2011-02-09"));
        }

        [Test]
        public void Given_GetDocumentsByID_Called_when_id_not_exist_then_null_response_returned()
        {
            var response = _scannedDocs.GetDocumentById(5);

            Assert.That(response, Is.Null);
        }
    }
}
