using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using fakebook.Helpers;
using fakebook.Models;

namespace fakebook.Services
{
    class UserService
    {
        private HttpService _httpService;

        public UserService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> UserExists(string username)
        {
            HttpResponseMessage response = await _httpService.post("/Authentication/login", new {Username = username});

            return response.IsSuccessStatusCode;
        }


        public async Task CreateUser(string username)
        {
            HttpResponseMessage response = await _httpService.post("/Authentication/register", new { Username = username });
        }

        public async Task<bool> FollowUser(string username, string targetUsername)
        {
            await _httpService.login(username);

            HttpResponseMessage response = await _httpService.post("/User/follow", new { targetUsername = targetUsername });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> sendMessage(string username, string targetUsername, string text)
        {
            await _httpService.login(username);

            HttpResponseMessage response = await _httpService.post("/User/directMessage", new { targetUsername, text });

            return response.IsSuccessStatusCode;
        }

        public async Task<List<PostResponseModel>> GetMessages(string username, string targetUsername)
        {
            var getRequest = new GetRequest()
                { path = "/User/directMessage", requestParams = new { targetUsername } };

            return await Helper.handleHttpGetList<List<PostResponseModel>>(new CommandRequest{username = username}, getRequest, _httpService);
        }

    }
}
