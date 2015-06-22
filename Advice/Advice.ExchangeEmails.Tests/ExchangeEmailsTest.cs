using System.Collections.Generic;
using EmailServer.Responses;
using NUnit.Framework;

namespace Advice.ExchangeEmails.Tests
{
    [TestFixture]
    public class ExchangeEmailsTest
    {
         [Test]
         public void Given_service_has_a_valid_response_then_valid_response_is_Set()
         {
             const string subject = "Abc";
             const string body = "<p>This is a body</p>";
             var toRecipients = new List<string> {"na@na.com", "k@rk.com"};
             const int messageId = 1;

             var respone = new GetArchivedMessageResponse
             {
                 Subject = subject,
                 Body = body,
                 ToRecipients = toRecipients,
                 MessageID = messageId
             };

             Assert.That(respone.Subject, Is.EqualTo(subject));
             Assert.That(respone.Body, Is.EqualTo(body));
             Assert.That(respone.MessageID, Is.EqualTo(messageId));
             Assert.That(respone.ToRecipients.Count, Is.EqualTo(toRecipients.Count));
         }
     }
}
