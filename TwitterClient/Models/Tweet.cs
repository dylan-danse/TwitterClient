using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace TwitterClient.Models
{
    [Serializable]
    public class Tweet
    {
        public User User { get; set; }
        public string Content { get; set; }

        public Tweet(ITweet tweet)
        {
            Content = tweet.Text;
            User = new User(tweet.CreatedBy);
        }

        public Tweet(User user, string content)
        {
            this.User = user;
            this.Content = content;
        }

        public Tweet()
        {

        }

        public override string ToString()
        {
            return String.Format("User: {0} \n Message: {1} \n\n", User.ScreenName, Content);
        }
    }
}
