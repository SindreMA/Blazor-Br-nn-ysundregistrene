using BrregAPI.Modals.Database;
using BrregAPI.Modals.Response;
using Microsoft.AspNetCore.Identity;

namespace BrregAPI.Helpers
{
    public class AuthHelper
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AuthHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            return true;
        }

        internal async Task<User> CreateUser(string username, string password)
        {
            if (username == null || password == null) throw new Exception("BadRequest");
            var u = await userManager.FindByNameAsync(username.ToLower());
            if (u != null) throw new Exception("Duplicate");

            if (!IsValidPassword(password)) throw new Exception("BadRequest", new Exception("Bad password"));

            var user = new User { UserName = username.ToLower().Trim() };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                await userManager.DeleteAsync(user);
                throw new Exception("BadRequest");
            }

            await signInManager.SignInAsync(user, true);

            var newUser = await userManager.FindByNameAsync(username.ToLower());

            return newUser;
        }

        internal async Task<UserObject> Login(string username, string password)
        {
            var signin = await signInManager.PasswordSignInAsync(username, password, true, true);
            if (!signin.Succeeded)
            {
                throw new Exception("unautorized");
            }
            var user = await userManager.FindByNameAsync(username.ToLower());
            return new GeneralHelper().FirstLoadData(user.Id);
        }

        internal async Task Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}
