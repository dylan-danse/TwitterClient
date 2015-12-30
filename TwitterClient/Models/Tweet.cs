using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    [Serializable]
    public class Tweet
    {
        public User User { get; set; }
        public string Content { get; set; }


        public override string ToString()
        {
            return String.Format("User: {0} \n Message: {1} \n\n", User.ScreenName, Content);
        }
    }
}
