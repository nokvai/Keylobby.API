using Keylobby.API.Identity.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Keylobby.API.Identity.BusinessLogicLayer.Interface
{
    public interface IEmailSender
    {
         Task SendEmailAsync(string message);
    }
}
