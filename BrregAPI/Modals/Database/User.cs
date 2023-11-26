using Microsoft.AspNetCore.Identity;

namespace BrregAPI.Modals.Database
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
