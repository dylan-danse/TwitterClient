using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TwitterClient.Models;

namespace TwitterClient.Twitter
{
    public static class HomeTimeLine
    {

        public static Tweets getTimeline()
        {
            var homeTimeLine = Timeline.GetHomeTimeline();
            Tweets tweets = new Tweets();

            Models.Tweet tweet;
            foreach (var item in homeTimeLine)
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
                tweets += tweet;
            }

            return tweets;
        }
    }
}
