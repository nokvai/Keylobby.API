using System;
using System.Collections.Generic;
using System.Text;

namespace Keylobby.API.Identity.DataAccessLayer.Models
{
    public class ResponseModel
    {
        public int Code { get; set; } = 200;

        public string Status { get; set; }

        public string Message { get; set; }
    }
}
