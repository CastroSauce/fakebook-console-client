using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using fakebook.Models;
using fakebook.Services;

namespace fakebook.Helpers
{
    class Helper
    {
        public  async static Task<T> handleHttpGetList<T>(CommandRequest commandRequest, GetRequest getRequest, HttpService httpService)
        {
            await httpService.login(commandRequest.username);

            var response = await httpService.Get(getRequest.path, getRequest.requestParams);

            if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent) return default; //if we have no data, return an empty list

            var data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(data);
        }


    }
}
