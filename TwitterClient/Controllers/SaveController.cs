using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tweetinvi;
using TwitterClient.Models;
using TwitterClient.Parser;
using TwitterClient.Twitter;
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

        private IWindow window;
        private IList<Parser.Parser> parsers;
        private IList<ListTwitter> listTwitter;

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
        public IList<Parser.Parser> Parsers
        {
            get { return parsers; }
            set { parsers = value; }
        }
        public IList<ListTwitter> ListTwitter
        {
            get
            {
                return listTwitter;
            }

            set
            {
                listTwitter = value;
            }
        }
        
        private void Window_OnSave(object sender, EventArgs e)
        {
            Tweets tweets = null;
            Parser.Parser parser = null;

            tweets = (from i in ListTwitter where i.Name == Window.ListTypeSelected select i).First().GetList();
            parser = (from i in Parsers where i.Name == Window.ParserSelect select i).First();

            if (parser == null)
            {
                Views.Dialog.ErrorDialog errorDialog = new ErrorDialog("Il n'y a aucun parser avec ce nom");
                return;
            }

            if (tweets == null)
            {
                Views.Dialog.ErrorDialog errorDialog = new ErrorDialog("Il y eu un problème lors du chargement de la liste");
                return;
            }

            try
            {
                string path;
                if (Window.Path.EndsWith(parser.Extension))
                {
                    path = Window.Path;
                }
                else
                {
                    path = String.Concat(Window.Path, parser.Extension);
                }

                FileManager.FileManager fileManager = new FileManager.FileManager(path);
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

        public override void HandleNavigation(object args)
        {
            ListTwitter = new List<ListTwitter> { new HomeTimeLine(), new UserTweet() };
            Window.ListTypes = (from i in ListTwitter select i.Name).ToList();

            Parsers = new List<Parser.Parser> { new XmlParser(), new TextParser() };
            Window.ParserNames = (from i in Parsers select i.Name).ToList();

            Window.Show();
        }


    }
}
