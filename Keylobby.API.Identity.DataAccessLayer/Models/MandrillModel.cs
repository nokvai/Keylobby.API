using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keylobby.API.Identity.DataAccessLayer.Models
{
    public class SendEmailThruMandrillModel
    {

        public string key { get; set; }

        public string template_name { get; set; }

        public List<MandrillTemplateContent> template_content { get; set; }

        public MandrillMessage message { get; set; }

        public bool WithTemplate { get; set; }
    }

    public struct MandrillTemplateContent
    {

        public string name { get; set; }

        public string content { get; set; }
    }

    public class MandrillMessage
    {

        public string html { get; set; }

        public string subject { get; set; }

        public string from_email { get; set; }

        public string from_name { get; set; }

        public List<MandrillMessageReceipient> to { get; set; }

        public MandrillMessageHeaders headers { get; set; }

        public List<MandrillMerge> merge_vars { get; set; }

        public string[] tags { get; set; }

        public List<MandrillMessageAttachments> attachments { get; set; }
    }


    public struct MandrillMerge
    {

        public string rcpt { get; set; }

        public List<MandrillMergeVars> vars { get; set; }
    }

    public struct MandrillMergeVars
    {

        public string name { get; set; }

        public string content { get; set; }
    }

    public class MandrillMessageReceipient
    {

        public string email { get; set; }

        public string name { get; set; }

        public string type { get; set; }
    }

    public class MandrillMessageHeaders
    {

        [JsonProperty(PropertyName = "Reply-To")]
        public string ReplyTo { get; set; }
    }

    public struct MandrillMessageAttachments
    {

        public string type { get; set; }

        public string name { get; set; }

        public string content { get; set; }
    }

    public class MandrillMessageResponse
    {

        public string email { get; set; }

        public string status { get; set; }

        public string reject_reason { get; set; }

        public string _id { get; set; }
    }

    public class MandrillSendEmail
    {

        public List<MandrillMessageReceipient> Recipients { get; set; }

        public string ReplyTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public long QuoteId { get; set; }

        public bool IsViewOnly { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public string ApiVersion { get; set; }

        [JsonIgnore]
        public string AppVersion { get; set; }
    }

    public class MandrillSendEmailResponse
    {

        public ResponseModel Response { get; set; }

        public List<MandrillMessageResponse> MandrillResponse { get; set; }
    }
}
