using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Models;

namespace TwitterClient.Twitter
{
    public class UserTweet : ListTwitter
    {
        private const string name = "Tweet de l'utilisateur"; 

        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Tweets GetList()
        {
            var userTweets = Tweetinvi.Timeline.GetUserTimeline(Tweetinvi.User.GetLoggedUser().Id);
            Tweets tweets = new Tweets();

            Models.Tweet tweet;
            foreach (var item in userTweets)
            {
                tweet = new Models.Tweet(item);
                //Todo : Surcharge d'opérateur
                tweets += tweet;
            }

            return tweets;
        }

    }
}
