﻿using DTOs.Responses;
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
        public async Task<(HttpResponseMessage Response, string url)> MakeHTTPRequest(string url, WebhookResponse sendHook)
        {
            if (sendHook == null) return (null, string.Empty);

            if (string.IsNullOrEmpty(url))
            {
                url = "https://localhost:5001/api/Webhook/notify";
            }

            sendHook.Url = url;
            var serialize = JsonSerializer.Serialize(sendHook);

            //=============================
            var data = new StringContent(serialize, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            //==============================
            //var req = new HttpRequestMessage(HttpMethod.Post, url)
            //{
            //    Content = new StringContent(serialize) 
            //};
            //    req.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            //    Client.BaseAddress = new Uri(url);

            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage res = client.PostAsync(url, data).Result;
                //check if status code != 200, send the request again ( 10 retires ) , if > 10, send email
                return (res, url);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

    }
}