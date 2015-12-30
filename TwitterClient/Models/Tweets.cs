using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace TwitterClient.Models
{
    public class Tweets : List<Tweet>
    {
        //Todo: Surcharge opérateur
        public static Tweets operator +(Tweets list, Tweet tweet)
        {
            list.Add(tweet);
            return list;
        }

        //Todo: Indexer
        public Tweets this[string content]
        {
            get
            {
                //return (Tweets) (from i in this where i.Content.Contains(content) select i).ToList();
                return null;
            }
        }

    }
}
