using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses
{
    public class ApiResponseDTO<T>
    {
        public T Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
