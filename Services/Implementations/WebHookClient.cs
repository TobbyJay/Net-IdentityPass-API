using DTOs.Responses;
using DTOs.Responses.BulkVerifications;
using DTOs.Responses.SingleVerifications;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class WebHookClient : IWebHookClient
    {
        private readonly HttpClient Client;
        public WebHookClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<(HttpResponseMessage Response, string url)> MakeHTTPRequest(string url, Object sendHook)
        {
            if (sendHook == null) return (null, string.Empty);

            if (string.IsNullOrEmpty(url))
            {
                url = "https://localhost:5001/api/Webhook/notify";
            }

            //sendHook.WebhookUrl = url;
            var serialize = JsonSerializer.Serialize(sendHook);

            //=============================
            var data = new StringContent(serialize, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage res = Client.PostAsync(url, data).Result;
                //check if status code != 200, send the request again ( 10 retires ) , if > 10, send email
                Client.Dispose();
                return (res, url);
            }
            catch (HttpRequestException e)
            {
                Client.Dispose();
                throw new HttpRequestException(e.Message);
            }
        }

        

        /// Sample bulk verification Data
        //        {
        //  "userReferenceId": "20213455ttyllo",
        //  "type": "bvn , drivers license",
        //  "bvnNumber": "54651333604",
        //  "phoneNumber": "",
        //  "firstName": "",
        //  "dob": "1999-12-21",
        //  "frscNumber": "AAD23208212298",
        //  "url": "https://webhook.site/76af3f3b-abd9-418a-9a5c-ca55b645c339"
        //}

    }
}
