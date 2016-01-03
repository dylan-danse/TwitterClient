using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class Token
    {

        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public Token()
        {

        }

        public Token(string name, string accessToken, string accessTokenSecret)
        {
            Name = name;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
        }
    }
}
