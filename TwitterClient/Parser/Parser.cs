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
    public abstract class Parser
    {
        public abstract void Save(Tweets tweets, StreamWriter stream);
    }
}
