using BrregAPI.Helpers;
using BrregAPI.Modals.Database;
using BrregAPI.Modals.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrregAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly GeneralHelper _generalHelper;
        private readonly AuthHelper _authHelper;

        public AuthController(SignInManager<User> signInManager, IHttpContextAccessor ctx) : base(ctx)
        {
            _generalHelper = new GeneralHelper();
            _authHelper = new AuthHelper(__userManager, signInManager);
        }

        [Authorize]
        [HttpGet]
        public ActionResult LoginCheck() => 
            Ok(_generalHelper.FirstLoadData(__userid));

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLogin logininfo) => 
            Ok(await _authHelper.Login(logininfo.username, logininfo.password));

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout() {
            await _authHelper.Logout();
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] UserLogin logininfo)
        {
            var user = await _authHelper.CreateUser(logininfo.username, logininfo.password);
            return Ok(_generalHelper.FirstLoadData(user.Id));
        }
    }
}