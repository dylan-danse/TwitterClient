using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;
using TwitterClient.Models;

namespace TwitterClient.Parser
{
    public class TextParser : Parser
    {

        private const string extension = ".txt";

        public override string Extension
        {
            get
            {
                return extension;
            }
        }

        public override void Save(Tweets tweets, StreamWriter stream)
        {
            using (stream)
            {
                foreach (Tweet item in tweets)
                {
                    stream.WriteLine(item.ToString());
                }
            }
        }
    }
}
