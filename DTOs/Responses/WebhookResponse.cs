using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses
{
    public class WebhookResponse
    {
        //public string Status { get; set; }
        public string Url { get; set; }

        //public string Message { get; set; }

    }

    public class ClientResponse<T>
    {
        private T _value;
        public string UserReferenceId { get; set; }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }


        public static implicit operator T(ClientResponse<T> value)
        {
            return value.Value;
        }
     
        public static implicit operator ClientResponse<T>(T value)
        {
            return new ClientResponse<T> { Value = value};
        }
    }
}
