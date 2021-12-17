using DTOs.Responses.SingleVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface INinVerificationTypes
    {
        public Task<NinResponse> LookUpNin(string number, string secretKey, string referenceId);

    }
}
