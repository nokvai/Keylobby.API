using System;
using System.Threading.Tasks;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Keylobby.API.Identity.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Keylobby.API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _iemailSender;

        public EmailController(IEmailSender iemailSender)
        {
            _iemailSender = iemailSender;
        }

        [AllowAnonymous]
        [HttpPost("contact-us")]
       
        public async Task<IActionResult> EmailContactUs([FromBody] ContactUsModel contact)
        {
            ContactUsModel contactUsObj = new ContactUsModel();
            contactUsObj.Name = contact.Name;
            contactUsObj.Email = contact.Email;
            contactUsObj.Phone = contact.Phone;
            contactUsObj.Message = contact.Message;
            string jsonData = JsonConvert.SerializeObject(contactUsObj);

            var result = _iemailSender.SendEmailAsync(jsonData);

            if (result.Status == TaskStatus.RanToCompletion)
                return Ok();
            else
                return BadRequest();
        }

    }
}
