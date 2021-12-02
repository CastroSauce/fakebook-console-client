using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakebook.Models
{
    class PostResponseModel
    {
        public string text{ get; set; }
        public string username{ get; set; }
        public DateTime postDate{ get; set; }
    }

    class PostResponseListModel
    {
        public List<PostResponseModel> posts { get; set; }

        public int available { get; set; }
        public int next { get; set; }
    }
}
