using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient
{
    class Message
    {
        private Person user;
        private string message;

        public Message(Person user, string message)
        {
            this.user = user;
            this.message = message;
        }

        public override string ToString()
        {
            return string.Concat(user.ToString()," :\n\t",message);
        }
    }
}
