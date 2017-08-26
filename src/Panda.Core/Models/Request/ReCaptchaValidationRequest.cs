using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Core.Models.Request
{
    public class ReCaptchaValidationRequest
    {
        public string Secret { get; set; }
        public string Resonse { get; set; }
        public string RemoteIp { get; set; }
    }

    public class ReCaptchaValidationResponse
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
        public string[] errorcodes { get; set; }
    }
}
