using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tweetinvi.Core.Interfaces;
using TwitterClient.Models;

namespace TwitterClient.Parser
{
    public class XmlParser : Parser
    {

        public override void Save(Tweets tweets, StreamWriter stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(tweets.GetType());

            xmlSerializer.Serialize(stream, tweets);
        }

    }
}
