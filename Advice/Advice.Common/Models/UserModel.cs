using System.Globalization;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class UserModel
    {
        public long? UserId { get; set; }
        public string Username { get; set; }
        public long? RoleId { get; set; }
        public long? TeamId { get; set; }
        public UserIdentityModel Identity { get; set; }
        
        public UserPermissionModel Permissions { get; set; }

        public TeamModel Team { get; set; }

        //TODO: put this in a more sensible place. 
        public string MachineName { get; set; }

        public string DisplayName
        {
            get { return
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Username.TrimStart().Replace(".", " "));
            }

        }

        public bool IsInDevelopmentTeam()
        {
            if (Team != null)
            {
                return Team.IsDevelopmentTeam();
            }

            return false;
        }

        public static UserModel CreateFromUser(User user)
        {
            if (user == null)
                return null;

            var userModel = new UserModel()
            {
                UserId = user.UserID,
                Username = user.Username,
                RoleId  = user.RoleID,
                TeamId  = user.TeamID
            };

            return userModel;
        }
    }
}
