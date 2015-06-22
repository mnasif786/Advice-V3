namespace Advice.Application.Contracts
{
    public interface IDocumentService
    {
        string GetFullDocumentPathByDocumentId(int documentId);
    }
}
