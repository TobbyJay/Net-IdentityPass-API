﻿using DTOs.Responses;
using DTOs.Responses.SingleVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBvnVerficationTypes
    {
        public Task<BvnResponse> VerfifyBvnInfoLevel2(string number, string secretKey, string referenceId);

    }
}
