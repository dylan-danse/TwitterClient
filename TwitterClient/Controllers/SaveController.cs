using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tweetinvi;
using TwitterClient.Models;
using TwitterClient.Parser;
using TwitterClient.Views.Dialog;

namespace TwitterClient.Controllers
{
    public class SaveController : Controller
    {

        public interface IWindow
        {
            event EventHandler OpenSaveDialog;
            event EventHandler OnSave;

            string Path { get; set; }

            IList<string> ListTypes { set; }
            string ListTypeSelected { get; }

            IList<string> ParserNames { set; }
            string ParserSelect { get; }

            void Show();
            void Hide();
        }

        private IDictionary<string, Parser.Parser> parsers;
        private IWindow window;

        public IWindow Window
        {
            get { return window; }
            set
            {
                window = value;
                window.OpenSaveDialog += Window_OpenSaveDialog;
                window.OnSave += Window_OnSave;
            }
        }

        private void Window_OnSave(object sender, EventArgs e)
        {
            Tweets tweets = null;
            Parser.Parser parser;
            if (Parsers.TryGetValue(Window.ParserSelect, out parser))
            {
                switch (Window.ListTypeSelected)
                {
                    case "Timeline":
                        tweets = Twitter.HomeTimeLine.getTimeline();
                        break;

                    case "Tweet de l'utilisateur":
                        tweets = Twitter.UserTweet.GetList();
                        break;

                    default:
                        break;
                }
                try
                {
                    FileManager.FileManager fileManager = new FileManager.FileManager(String.Concat(Window.Path, parser.Extension));
                    parser.Save(tweets, fileManager.Stream);
                    fileManager.close();

                    Views.Dialog.SuccessDialog successDialog = new SuccessDialog("La sauvegarde a été effectuée avec succès");
                    Window.Hide();

                }
                catch (Exception ex)
                {
                    Views.Dialog.ErrorDialog errorDialog = new ErrorDialog(ex.Message);
                }
            }
        }

        private void Window_OpenSaveDialog(object sender, EventArgs e)
        {
            PathDialog pathDialog = new PathDialog();
            pathDialog.OnPathChanged += PathDialog_OnPathChanged;
            pathDialog.OpenPathDialog();
        }

        private void PathDialog_OnPathChanged(object sender, PathDialogEventArgs e)
        {
            Window.Path = e.Path;
        }

        public IDictionary<string, Parser.Parser> Parsers
        {
            get { return parsers; }
            set { parsers = value; }
        }

        public override void HandleNavigation(object args)
        {
            Window.ListTypes = new List<string> { "Timeline", "Tweet de l'utilisateur" };

            Parsers = new Dictionary<string, Parser.Parser>();
            Parsers.Add("Xml", new XmlParser());
            Parsers.Add("Texte", new TextParser());
            Window.ParserNames = Parsers.Keys.ToList();

            Window.Show();
        }


    }
}
