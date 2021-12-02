using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using fakebook.Models;
using System.Collections.Generic;
namespace fakebook.Services
{
    class HttpService
    {
        private const string host = "https://localhost:5001";
        private HttpClient client = new();
        private string authToken = "";

        public async Task<HttpResponseMessage> post(string path, object values)
        {
            if (authToken != "") SetAuthHeader(client, authToken);



            return await client.PostAsJsonAsync($"{host}{path}", values);
        }

        public async Task<HttpResponseMessage> Get(string path, object values = null)
        {
            if (!string.IsNullOrEmpty(authToken)) SetAuthHeader(client, authToken);

            var requestParams = "";

            if (values != null)
            {
                requestParams = values.GetType().GetProperties()
                    .Aggregate("?", (current, next) => $"{next.Name}={next.GetValue(values)}&");
            }

            return await client.GetAsync($"{host}{path}?{requestParams}");
        }




        public async Task<bool> login(string username)
        {

            var response = await post("/Authentication/login", new { Username = username });

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            authToken = result.Token;

            return true;
        }

        public async Task<bool> HealthCheck()
        {
            var response = await Get("/Authentication/health");

            Console.WriteLine(response.IsSuccessStatusCode ? "Api was detected" : "Api was not detected");


            return response.IsSuccessStatusCode;
        }



        private void SetAuthHeader(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }



    }
}
