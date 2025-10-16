using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrregAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticController : ControllerBase
    {
        [HttpGet("public")]
        public ActionResult PublicEndpoint()
        {
            return Ok(new
            {
                message = "This is a public endpoint",
                timestamp = DateTime.UtcNow,
                hasCookie = Request.Cookies.ContainsKey("BrregAuth"),
                cookieValue = Request.Cookies.TryGetValue("BrregAuth", out var cookie) ? "Present" : "Missing",
                allCookies = Request.Cookies.Keys.ToList()
            });
        }

        [Authorize]
        [HttpGet("protected")]
        public ActionResult ProtectedEndpoint()
        {
            return Ok(new
            {
                message = "This is a protected endpoint - you are authenticated!",
                timestamp = DateTime.UtcNow,
                user = User.Identity?.Name,
                isAuthenticated = User.Identity?.IsAuthenticated,
                hasCookie = Request.Cookies.ContainsKey("BrregAuth"),
                allCookies = Request.Cookies.Keys.ToList()
            });
        }

        [HttpGet("headers")]
        public ActionResult GetHeaders()
        {
            var headers = Request.Headers
                .Where(h => h.Key.StartsWith("Cookie") || h.Key.Contains("Origin") || h.Key.Contains("Referer"))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            return Ok(new
            {
                message = "Request headers related to cookies and CORS",
                headers,
                cookies = Request.Cookies.ToDictionary(c => c.Key, c => c.Value)
            });
        }
    }
}
