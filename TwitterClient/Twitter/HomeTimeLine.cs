using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TwitterClient.Models;

namespace TwitterClient.Twitter
{
    public class HomeTimeLine : ListTwitter
    {
        private const string name = "TimeLine";

        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override Tweets GetList()
        {
            var homeTimeLine = Timeline.GetHomeTimeline();
            Tweets tweets = new Tweets();

            if (homeTimeLine != null)
            {
                Models.Tweet tweet;
                foreach (var item in homeTimeLine)
                {
                    tweet = new Models.Tweet(item);
                    tweets += tweet;
                }
            }

            return tweets;
        }

    }
}
