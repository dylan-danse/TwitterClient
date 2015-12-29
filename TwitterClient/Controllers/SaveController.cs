using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tweetinvi;
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

            IList<string> ParserNames { set; }
            
            string Path { get; set; }
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
            Parser.Parser parser;
            if (Parsers.TryGetValue(Window.ParserSelect, out parser))
            {
                try
                {
                    FileManager.FileManager fileManager = new FileManager.FileManager(Window.Path + "." + Window.ParserSelect.ToLower());
                    parser.Save(Twitter.HomeTimeLine.getTimeline(), fileManager.Stream);
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
            Parsers = new Dictionary<string, Parser.Parser>();
            Parsers.Add("Xml", new XmlParser());

            Window.ParserNames = Parsers.Keys.ToList();

            Window.Show();
        }


    }
}
