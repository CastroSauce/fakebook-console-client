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
    class PostService
    {
        private HttpService _httpService;

        public PostService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async void post(PostModel post)
        {
            await _httpService.login(post.username);

            await _httpService.post("/Post", new {Text = post.text});
        }

        public async Task<PostResponseListModel> GetPosts(CommandRequest commandRequest)
        {
            var getRequest = new GetRequest()
                {path = "/Post/PostsByUsername", requestParams = new {username = commandRequest.commandArgs}};

            return await Helper.handleHttpGetList<PostResponseListModel>(commandRequest, getRequest, _httpService);
        }

        public async Task<PostResponseListModel> GetWall(CommandRequest commandRequest)
        {
            var getRequest = new GetRequest() { path = "/Post/Wall"};

            return await Helper.handleHttpGetList<PostResponseListModel>(commandRequest, getRequest, _httpService);
        }

    }
}
