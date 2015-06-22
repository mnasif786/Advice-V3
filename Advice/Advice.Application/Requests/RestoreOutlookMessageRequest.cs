namespace Advice.Application.Requests
{
    public class RestoreOutlookMessageRequest
    {
        public string MailManagerEmailAddress { get; set; }
        public string MailManagerLoginName { get; set; }
        public long MessageId { get; set; }
        public string UserToRestoreTo { get; set; }
    }
}
