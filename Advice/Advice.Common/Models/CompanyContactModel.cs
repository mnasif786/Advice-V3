using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peninsula.Domain.Entities;


namespace Advice.Common.Models
{
    public class CompanyContactModel
    {
        public long CompanyContactId { get; set; }
        public string Title { get; set; }
        public string ForeName { get; set; }
        public string Initial { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumberMain { get; set; }
        public string TelephoneNumberAlternative { get; set; }

        public string Name
        {
            get { return string.Format("{0} {1} {2}", ForeName, Initial, Surname); }

        }

        public static CompanyContactModel Create(TBLCompanyContact contact)
        {
            if (contact == null)
                return null;

            return new CompanyContactModel()
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
