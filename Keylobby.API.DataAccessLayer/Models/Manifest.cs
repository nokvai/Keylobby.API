using System;
using System.Collections.Generic;
using System.Text;

namespace Keylobby.API.DataAccessLayer.Models
{
    public class Manifest
    {

        #region Info

        public bool SendEmail { get; set; }
        public string Environment { get; set; }
        public string Protocol { get; set; }

        #endregion

        #region Mandrill

        public string MandrillUri { get; set; }
        public string MandrillRegisteredEmail { get; set; }
        public string MandrillSendMessageRoute { get; set; }
        public string MandrillSendMessageWithTemplateRoute { get; set; }
        public string MandrilApiKey { get; set; }
        public string MandrilTestApiKey { get; set; }
        //public string MandrilConfirmationEmailTemplate { get; set; }
        //public string MandrilResetPasswordTemplate { get; set; }
        //public string MandrilResetConfirmationTemplate { get; set; }
        //public string MandrilPreOrderConfirmationTemplate { get; set; }
        //public string MandrilEmailVerificationTemplate { get; set; }
        public string MandrilEmailContactUsTemplate { get; set; }
        //public string MandrilEmailAddFundsSucceeded { get; set; }
        public string MandrillEmailSender { get; set; }
        public string MandrillBccEmailJames { get; set; }
        public string MandrillBccEmailTeam { get; set; }

        #endregion

    }
}
