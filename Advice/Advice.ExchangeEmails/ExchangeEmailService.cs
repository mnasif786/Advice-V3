using Advice.ExchangeEmails.WS_ExchangeArchive;
using Advice.Infrastructure.Configuration;
using GetArchivedMessageResponse = EmailServer.Responses.GetArchivedMessageResponse;

namespace Advice.ExchangeEmails
{
    public class ExchangeEmailService : IExchangeEmailService
    {
        private static Exchange.ExchangeEmailService _emailService;
        private readonly IConfigurationManagerAdvice _configurationManager;

        public ExchangeEmailService(IConfigurationManagerAdvice configManager)
        {
            _configurationManager = configManager;
            _emailService = new Exchange.ExchangeEmailService();
        }

        //NA: This method uses email server service dll which is included in the packages folder at the moment.
        //Also exchange database connection string has been added into the Web.config file.
        //if you need more information, download the project (EmailServer) from mercurial. The actual code is pointed to EmailServerService/Exchange.
        public GetArchivedMessageResponse GetOutlookEmailMailMessage(long messageId)
        {
            return _emailService.GetEmailMessage(messageId);
        }

        public string RestoreMessageToOutlook(long messageId, string userAssignedTo)
        {
            var exchangeArchive = new WS_ExchangeArchive.WS_ExchangeArchive();
            
            var request = new WS_ExchangeArchive.RestoreMessageRequest()
            {
                MailManagerEmailAddress = _configurationManager.GetApplicationSettingValueFromKey("MailManager_EmailAddress"),
                MailManagerLoginName = _configurationManager.GetApplicationSettingValueFromKey("MailManager_Username"),
                MessageId = messageId,
                UserToRestoreTo = userAssignedTo
            };
            return exchangeArchive.RestoreMessage(request);
        }
    }
}
