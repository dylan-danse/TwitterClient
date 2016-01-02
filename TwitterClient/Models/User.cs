using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

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

        public User(IUser user)
        {
            Username = user.Name;
            ScreenName = user.ScreenName;
            ProfileImageUrl = user.ProfileImageUrl400x400;
            Tweets = user.StatusesCount;
            Followers = user.FollowersCount;
            Followings = user.FriendsCount;

            City = user.Location;
            AccountCreatedAt = "Inscrit en " + String.Format("{0:MMMM  yyyy}", user.CreatedAt);
        }

        public User()
        {

        }
    }
}
