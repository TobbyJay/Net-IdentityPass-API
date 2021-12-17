using DTOs.Responses.SingleVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVinVerificationTypes
    {
        public Task<VinResponse> LookUpVin(string number, string last_name, string state, string secretKey, string referenceId);

    }
}
