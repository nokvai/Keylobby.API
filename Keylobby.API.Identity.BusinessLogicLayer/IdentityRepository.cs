using Keylobby.API.DataAccessLayer.Models;
using Keylobby.API.Identity.BusinessLogicLayer.Interface;
using Keylobby.API.Identity.DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Keylobby.API.Identity.BusinessLogicLayer
{
      public class IdentityRepository : IIdentityRepository {

        private readonly IOptions<Manifest> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityRepository( IOptions<Manifest> options,  IHttpContextAccessor httpContextAccessor) {

            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        public async Task<ResponseModel> EmailContactUs(string name, string email, string phone, string message)
        {

            var mandrillResponse = new List<MandrillMessageResponse>();

            if (_options.Value.SendEmail)
            {
                Task.WaitAll(
                    Task.Run(async () => mandrillResponse = await SendEmail(PopulateSendEmailModel())));
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = "Successfully sent email inquiry."
            };


            SendEmailThruMandrillModel PopulateSendEmailModel()
            {
                var baseUrl = _httpContextAccessor.HttpContext.Request.Host;

                
                var mandrillMergeVars = new List<MandrillMergeVars> {
                    new MandrillMergeVars {
                        name = "name",
                        content = $"{name}"
                    },
                    new MandrillMergeVars {
                        name = "email",
                        content = $"{email}"
                    },
                    new MandrillMergeVars {
                        name = "phone",
                        content = $"{phone}"
                    },
                    new MandrillMergeVars {
                        name = "message",
                        content = $"{message}"
                    }
                };

                var mandrillMerge = new List<MandrillMerge> {
                    new MandrillMerge {
                        rcpt = _options.Value.MandrillRegisteredEmail,
                        vars = mandrillMergeVars
                    }
                };

                var messageTo = new List<MandrillMessageReceipient> {
                    new MandrillMessageReceipient {
                        email = _options.Value.MandrillRegisteredEmail,
                        name = _options.Value.MandrillEmailSender,
                        type = "to"
                    }
                };

                return new SendEmailThruMandrillModel
                {
                    WithTemplate = true,
                    key = _options.Value.MandrilApiKey,
                    template_name = _options.Value.MandrilEmailContactUsTemplate,
                    message = new MandrillMessage
                    {
                        subject = $"[{_options.Value.Environment}] Keylobby Inquiry",
                        from_email = _options.Value.MandrillRegisteredEmail,
                        from_name = _options.Value.MandrillEmailSender,
                        to = messageTo,
                        merge_vars = mandrillMerge,
                        tags = new[] { "keylobby-contact-us" }
                    }
                };
            }
        }

        public async Task<List<MandrillMessageResponse>> SendEmail(SendEmailThruMandrillModel model)
        {
            List<MandrillMessageResponse> mandrillResponse;

            using (var client = new HttpClient())
            {

                model.key = _options.Value.MandrilApiKey;
                client.BaseAddress = new Uri(_options.Value.MandrillUri);

                var myContent = JsonConvert.SerializeObject(model);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(
                    string.Format("{0}{1}"
                        , _options.Value.MandrillUri
                        , model.WithTemplate ?
                            _options.Value.MandrillSendMessageWithTemplateRoute :
                            _options.Value.MandrillSendMessageRoute)
                    , byteContent).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK) return null;

                var responseBody = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                mandrillResponse = JsonConvert
                    .DeserializeObject<List<MandrillMessageResponse>>(responseBody);
            }

            return mandrillResponse;
        }


    }

}
