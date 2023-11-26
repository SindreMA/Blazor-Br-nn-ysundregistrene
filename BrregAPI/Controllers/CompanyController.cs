using BrregAPI.Helpers;
using BrregAPI.Modals;
using BrregAPI.Modals.Database;
using BrregAPI.Modals.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrregAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        private readonly CompanyHelper _companyHelper;
        public CompanyController(BrregContext context)
        {
            _companyHelper = new CompanyHelper(context);
        }

        [HttpGet]
        public async Task<ActionResult> GetList(int start, int amount) => Ok(await _companyHelper.GetCompanies(start, amount));

        [HttpGet("{id}")]
        public ActionResult GetSpesific(int id) => Ok(_companyHelper.GetCompany(id));

    }
}