using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailServer.Responses;
using System.IO;
using Advice.Logging.Contracts;
using Advice.Logging;

namespace Advice.ExchangeEmails
{
    /// <summary>
    /// Mock implementation of the IExchangeEmailService for dev testing
    /// </summary>
    public class MockExchangeEmailService : IExchangeEmailService
    {
        public GetArchivedMessageResponse GetOutlookEmailMailMessage(long messageId)
        {            
            GetArchivedMessageResponse resp = new GetArchivedMessageResponse();

            resp.ArchivedDate =   DateTime.Today.AddDays(-1);
            resp.ArchivedMessageGuid = Guid.NewGuid();
            resp.ArchivedScannedDocuments = new List<ArchivedScannedDocument>();

            resp.Body = "dummy body for dummy email. It's a fake. Nothing to see here - move along.";
            resp.BodyType = BodyType.Text;
            
            resp.DateTimeSent =  DateTime.Today.AddDays(-2);
            resp.ExchangeItemId = "12345678";
            resp.FromEmailAddress = "fred.flintstone@yabba.dabba.doo.com";
            resp.MailboxEmailAddress = "scooby.doo@mystery.machine.com";
            resp.MessageID  = messageId;
            resp.Outbound = false; ;
            resp.Subject = "Dummy email message for id: " + messageId.ToString();

            resp.CcRecipients = new List<string>();
            resp.BccRecipients = new List<string>(){ "betty.rubble@rockville.com" };
            resp.ToRecipients = new List<string>(){ "barney.rubble@rockville.com" };

            return resp;
        }

        public string RestoreMessageToOutlook(long messageId, string userAssignedTo)
        {                   
            try
            {
                StreamWriter writer = new StreamWriter(@"D:\RestoreMessages.txt", true);
                writer.WriteLine(String.Format("{0} Message ID: {1} UserAssignedTo: {2}", DateTime.Now.ToString(), messageId, userAssignedTo));
                writer.Close();

                //IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                //Log.Info( new InfoLogRequest() 
                //                { 
                //                    IndexType = ElkIndexTypeProvider.EventsTasklog, 
                //                    Message = String.Format("{0} Message ID: {1} UserAssignedTo: {2}", DateTime.Now.ToString(), messageId, userAssignedTo) 
                //                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            string result = "Completed";

            return result;
        }
    }
}
