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
                tweet = new Models.Tweet
                {
                    User = new Models.User
                    {
                        Username = item.CreatedBy.Name,
                        ScreenName = item.CreatedBy.ScreenName,
                        ProfileImageUrl = item.CreatedBy.ProfileImageUrl400x400
                    },
                    Content = item.Text
                };
                //Todo : Surcharge d'opérateur
                tweets += tweet;
            }

            return tweets;
        }

    }
}
