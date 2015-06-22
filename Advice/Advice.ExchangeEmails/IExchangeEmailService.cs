namespace Advice.ExchangeEmails
{
    public interface IExchangeEmailService
    {
        EmailServer.Responses.GetArchivedMessageResponse GetOutlookEmailMailMessage(long messageId);
        string RestoreMessageToOutlook(long messageId, string userAssignedTo);
    }
}
