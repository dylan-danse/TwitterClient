using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace TwitterClient.Controllers
{
    public class MainController : Controller
    {

        #region interface
        public interface IWindow
        {
            event EventHandler publishTweetButtonClicked;
            event EventHandler disconnectClicked;

            ObservableCollection<Message> HomeTimeline { set; }
            string TweetToPublish { get; }

            string Username { set; }

            void CleanTweetBox();
            void Show();
            void Close();
            void ShowError(string message);
            void ShowMessage(string message);
        }
        #endregion

        /* Var */
        private IWindow window;

        /* Ctor */
        public override void HandleNavigation(object args)
        {
            Window.HomeTimeline = getFormatedTimeline();
            Window.Show();

            Window.Username = User.GetLoggedUser().Name;
        }

        /* Prop */ 
        public IWindow Window
        {
            get { return window; }
            set
            {
                window = value;
                window.publishTweetButtonClicked += Window_publishTweetButtonClicked;
                Window.disconnectClicked += Window_disconnectClicked;
            }
        }

        private void Window_disconnectClicked(object sender, EventArgs e)
        {
            LoginController loginController = new LoginController { Window = new LoginWindow() };
            loginController.HandleNavigation(null);

            Window.Close();
        }

        /* Event */
        private void Window_publishTweetButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Window.TweetToPublish.Length > 140)
                {
                    window.ShowError("Votre tweet ne peut pas dépasser 140 caractères !");
                }
                else
                {
                    Tweet.PublishTweet(window.TweetToPublish.Trim());
                    window.ShowMessage("Votre tweet a été publié avec succès");
                    Window.CleanTweetBox();
                }
            }
            catch (Exception)
            {
                window.ShowError("Votre tweet ne peut pas être nul !");
            }
        }

        /* Methods */
        private ObservableCollection<Message> getFormatedTimeline()
        {
            var tweets = Timeline.GetHomeTimeline();
            ObservableCollection<Message> liste = new ObservableCollection<Message>();

            foreach (var item in tweets)
            {
                liste.Add(new Message(new Person(item.CreatedBy.Name,
                                                 item.CreatedBy.ScreenName,
                                                 item.CreatedBy.ProfileImageUrl400x400)
                                     , item.Text));
            }
            return liste;
        }
    }
}
