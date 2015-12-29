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
        public string ScreenName { get; set; }
        public string ProfileImageUrl { get; set; }

        public int Tweets { get; set; }
        public int Followings { get; set; }
        public int Followers { get; set; }

        public string  City { get; set; }
        public string AccountCreatedAt { get; set; }
    }
}
