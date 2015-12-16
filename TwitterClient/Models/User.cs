using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class User
    {
        public string Username { get; set; }
        public string ProfileImageUrl { get; set; }

        public int Tweets { get; set; }
        public int Following { get; set; }
        public int Follower { get; set; }
    }
}
