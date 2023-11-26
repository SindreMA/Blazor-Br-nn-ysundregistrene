using BrregAPI.Modals.Database;

namespace BrregAPI.Modals.Response
{
    public class UserObject
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }

        public UserObject(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            IsAdmin = user.IsAdmin;
        }
    }
    
}