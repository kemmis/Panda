using Panda.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Core.Contracts
{
    public interface IReCaptchaValidator
    {
        Task<ReCaptchaValidationResponse> Validate(ReCaptchaValidationRequest request);
    }
}
