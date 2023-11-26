using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BrregAPI.Modals.Database;
using Microsoft.AspNetCore.Identity;
using BrregAPI.Modals;

namespace BrregAPI.Controllers
{
    public class BaseController : Controller
    {
        string errorMsg = "You need to pass IHttpContextAccessor to base if you want to autoinject properties in constructor";
        private HttpContext _httpContext;
        private BrregContext _context;
        protected BrregContext __context => _context ?? (_context = (HttpContext ?? _httpContext ?? throw new Exception(errorMsg)).RequestServices.GetService<BrregContext>());
        private UserManager<User> _userManager;
        protected UserManager<User> __userManager => _userManager ?? (_userManager = (HttpContext ?? _httpContext ?? throw new Exception(errorMsg)).RequestServices.GetService<UserManager<User>>());
        private string _username;
        protected string __username => _username ?? (_username = (HttpContext ?? _httpContext ?? throw new Exception(errorMsg)).User.Identity.Name);
        private string _userid;
        protected string __userid => _userid ?? __userManager.GetUserId((HttpContext ?? _httpContext ?? throw new Exception(errorMsg)).User);


        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }


        public BaseController()
        {

        }

        public Stopwatch sw = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            sw.Restart();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            List<string> ls = new List<string>()
            {
                $"Method: {context.HttpContext.Request.Method}",
                $"Time used: {sw.ElapsedMilliseconds}ms",
                $"Controller: {context.Controller.ToString().Split('.').LastOrDefault()}",
                $"Request: {context.HttpContext.Request.Path.Value}",
            };

            ls[0] = addSpacers(ls[0], 12);
            ls[1] = addSpacers(ls[1], 15);
            ls[2] = addSpacers(ls[2], 43);

            var tabbed = string.Join(' ', ls.Select(x => x + "\t"));

            Console.WriteLine(tabbed);
            base.OnActionExecuted(context);
        }
        
        private string addSpacers(string val, int length)
        {
            var newVal = val;
            if (val.Length < length)
            {
                for (int i = 0; i < (length - val.Length); i++)
                {
                    newVal = newVal + " ";
                }
            }
            return newVal;
        }
    }
}
