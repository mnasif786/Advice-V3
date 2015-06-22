namespace Advice.Common.Models
{
    public class UserIdentityModel
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }

        public UserIdentityModel(string name, string domain, string firstName, string surName, string userName)
        {
            Name = name;
            Domain = domain;
            FirstName = firstName;
            SurName = surName;
            UserName = userName;
        }
    }
}
