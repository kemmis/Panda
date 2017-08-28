using Newtonsoft.Json;
using Panda.Core.Contracts;
using Panda.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Service
{
    public class ReCaptchaValidator : IReCaptchaValidator
    {
        private const string API_URL = "https://www.google.com/recaptcha/api/siteverify";

        private readonly HttpClient _httpClient;

        public ReCaptchaValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReCaptchaValidationResponse> Validate(ReCaptchaValidationRequest request)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("secret",  request.Secret),
                 new KeyValuePair<string, string>("response", request.Resonse)
            });

            var postResponse = await _httpClient.PostAsync(API_URL, content);
            string json = await postResponse.Content.ReadAsStringAsync();
            var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaValidationResponse>(json);

            return reCaptchaResponse;
        }
    }
}
