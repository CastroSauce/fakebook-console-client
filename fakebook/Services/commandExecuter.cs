using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using fakebook.Models;

namespace fakebook.Services
{
    class commandExecuter
    {
        private HttpService _httpService = new();
        private UserService _userService;
        private PostService _postService;

        public commandExecuter()
        {
            _userService = new UserService(_httpService);
            _postService = new PostService(_httpService);
        }

        public async Task executeCommand(CommandRequest commandRequest)
        {
            Console.WriteLine(""); // add space between command and execution

            switch (commandRequest.command)
            {
                case RequestCommand.post:
                    await Post(commandRequest);
                    break;

                case RequestCommand.timeline:
                    await Timeline(commandRequest);
                    break;

                case RequestCommand.follow:
                    await Follow(commandRequest);
                    break;

                case RequestCommand.wall:
                    await Wall(commandRequest);
                    break;

                case RequestCommand.send_message:
                    await SendMessage(commandRequest);
                    break;

                case RequestCommand.view_messages:
                    await ViewMessage(commandRequest);
                    break;
            }
        }

        private async Task Post(CommandRequest commandRequest)
        {
            if (!await _userService.UserExists(commandRequest.username))
            {
               await _userService.CreateUser(commandRequest.username);
            }
            _postService.post(new PostModel(){username = commandRequest.username, text = commandRequest.commandArgs});
        }


        private async Task Timeline(CommandRequest commandRequest)
        {
            var posts = await _postService.GetPosts(commandRequest);

            if (posts.posts.Count == 0)
            {
                Console.WriteLine("No posts available");
                return;
            }
            foreach (var post in posts.posts)
            {
                Console.Write(post.username);
                Console.Write(" ");
                Console.WriteLine(post.postDate);
                Console.WriteLine(post.text);
                Console.WriteLine("");
            }
        }

        private async Task Follow(CommandRequest commandRequest)
        {
            await _userService.FollowUser(commandRequest.username, commandRequest.commandArgs);
        }

        private async Task Wall(CommandRequest commandRequest)
        {
            var posts = await _postService.GetWall(commandRequest);

            if (posts.posts == null)
            {
                Console.WriteLine("No posts available");
                return;
            }

            foreach (var post in posts.posts)
            {
                Console.Write(post.username);
                Console.Write(" ");
                Console.WriteLine(post.postDate);
                Console.WriteLine(post.text);
                Console.WriteLine("");
            }
        }

        private async Task SendMessage(CommandRequest commandRequest)
        {
            Console.Write("Message to send: ");
            var message = Console.ReadLine();

            await _userService.sendMessage(commandRequest.username, commandRequest.commandArgs, message);
        }

        private async Task ViewMessage(CommandRequest commandRequest)
        {
            var posts = await _userService.GetMessages(commandRequest.username, commandRequest.commandArgs);

            if (posts == null)
            {
                Console.WriteLine("No posts available");
                return;
            }

            foreach (var post in posts)
            {
                Console.Write(post.username);
                Console.Write(" ");
                Console.WriteLine(post.postDate);
                Console.WriteLine(post.text);
                Console.WriteLine("");
            }
        }
    }
}
