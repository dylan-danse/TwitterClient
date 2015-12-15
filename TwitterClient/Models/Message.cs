using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient
{
    public class Message
    {
        private Person user;
        private string tweet;

        public Person User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public string Tweet
        {
            get
            {
                return tweet;
            }

            set
            {
                tweet = value;
            }
        }

        public Message(Person user, string tweet)
        {
            this.User = user;
            this.Tweet = tweet;
        }

        public override string ToString()
        {
            return string.Concat(User.ToString()," :\n\t", Tweet);
        }
    }
}
