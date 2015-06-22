using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Peninsula.Domain.Entities;

namespace Advice.Common.Models
{
    public class JobModel
    {
        public long JobId { get; set; }
        public string Subject { get; set; }
        public long? ContactId { get; set; }
        public CompanyContactModel Contact { get; set; }
        public bool Closed { get; set; }
        public static JobModel Create(Job job)
        {
            return new JobModel()
            {
                JobId = job.JobID,
                Subject = job.Subject,
                Closed = job.Closed,
                ContactId = job.ContactID
                
            };
        }

        public void CreateContact(TBLCompanyContact contact)
        {
            if (contact != null)
            {
                Contact = new CompanyContactModel
                {
                    CompanyContactId = contact.CompanyContactID,
                    Title = contact.Title,
                    ForeName = contact.Forename,
                    Initial = contact.Initial,
                    Surname = contact.Surname,
                    EmailAddress = contact.EMail,
                    TelephoneNumberMain = contact.TelNoMain,
                    TelephoneNumberAlternative = contact.TelNoAlt
                };
            }
        }
    }
}
