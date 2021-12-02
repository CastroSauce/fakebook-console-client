using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakebook.Models
{
    class CommandRequest
    {
        public string username { get; set; }
        public RequestCommand command { get; set; }
        public string commandArgs { get; set; }

    }


    enum RequestCommand
    {
        post,
        timeline,
        follow,
        wall,
        send_message,
        view_messages
    }
}
