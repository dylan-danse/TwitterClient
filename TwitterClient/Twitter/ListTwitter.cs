using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Models;

namespace TwitterClient.Twitter
{
    public abstract class ListTwitter
    {
        public abstract Tweets getList();
    }
}
