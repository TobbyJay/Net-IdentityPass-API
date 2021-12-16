﻿using System.Text.Json.Serialization;

namespace DTOs.Responses
{
    public class DriverseLicenseResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("response_code")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("licenseNo")]
        public string LicenseNo { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("issuedDate")]
        public string IssuedDate { get; set; }

        [JsonPropertyName("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonPropertyName("stateOfIssue")]
        public string StateOfIssue { get; set; }

        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; }

        [JsonPropertyName("photo")]
        public string Photo { get; set; }
    }
}
