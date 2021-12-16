using DTOs.Responses.SingleVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICreditBureauVerificationType
    {
        public Task<CreditBureau> VerifyCreditBureau(string phoneNumber, string firstName, string secretKey, string referenceId);

    }
}
