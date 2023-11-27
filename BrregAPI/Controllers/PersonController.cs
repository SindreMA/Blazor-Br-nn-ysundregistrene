using BrregAPI.Helpers;
using BrregAPI.Modals;
using BrregAPI.Modals.Database;
using BrregAPI.Modals.Request;
using BrregAPI.Modals.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrregAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        private readonly PersonHelper _personHelper;
        public PersonController(BrregContext context)
        {
            _personHelper = new PersonHelper(context);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson([FromBody]PersonObject person, int id) {
            await _personHelper.UpdatePerson(person, id);
            return Ok();
        }
    }
}