using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Controllers
{
    public class MainController : Controller
    {
        public MainWindow Window { get; internal set; }

        public override void HandleNavigation(object args)
        {
            throw new NotImplementedException();
        }
    }
}
