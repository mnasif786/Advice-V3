using System;
using System.Collections.Generic;
using EmailServer.Responses;

namespace Advice.Common.Models
{
    public class OutlookEmailMessageModel
    {
        public string Body { get; set; }
        public string BodyType { get; set; }
        public List<string> Recipients { get; set; }
        public List<string> CcRecipients { get; set; }
        public string From { get; set; }
        public DateTime DateSent { get; set; }
        public string EmailSubject { get; set; }

        public static OutlookEmailMessageModel Create(GetArchivedMessageResponse response)
        {
            if (response == null) return null;

            var outlookEmailMessageModel = new OutlookEmailMessageModel
            {
                Recipients = response.ToRecipients ?? new List<string>(),
                CcRecipients = response.CcRecipients ?? new List<string>(), 
                From = response.FromEmailAddress,
                DateSent = response.DateTimeSent,
                EmailSubject = response.Subject ?? String.Empty,
                Body = response.Body ?? String.Empty,
                BodyType = response.BodyType.ToString()
            };

            return outlookEmailMessageModel;
        }
    }

    
}
