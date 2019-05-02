using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keylobby.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IIdentityRepository _identityRepository;

        public EmailController(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Email/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Email
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Email/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [AllowAnonymous]
        [HttpPost("contact-us")]
        public async Task<IActionResult> EmailContactUs(string name, string email, string phone, string message)
        {
            var result = await _identityRepository.EmailContactUs(name, email, phone, message);
            if (result.Status == "Success")
                return Ok();
            else
                return BadRequest();
        }

    }
}
