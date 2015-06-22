﻿using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class EmailModel
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public bool? CrossPosted { get; set; }
        public long? MessageId { get; set; }
        public OutlookEmailMessageModel OutlookEmailMessage { get; set; }

        public static EmailModel Create(EmailTask emailTask)
        {
            if (emailTask == null)
                return null;

            var emailModel = new EmailModel()
            {
                Sender = emailTask.Sender,
                Subject = emailTask.Subject,
                CrossPosted = emailTask.CrossPosted,
                MessageId = emailTask.MessageId
            };
            
            return emailModel;
        }
    }
}