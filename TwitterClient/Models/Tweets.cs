using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace TwitterClient.Models
{
    public class Tweets : ObservableCollection<Tweet>
    {
        //Todo: Surcharge opérateur
        public static Tweets operator +(Tweets list, Tweet tweet)
        {
            list.Add(tweet);
            return list;
        }

        //Todo: Indexer
        public Tweet this[string content]
        {
            get
            {
                return (from i in this where i.Content == content select i).First();
            }
        }

        public Tweet Tweet { get; set; }
    }
}
