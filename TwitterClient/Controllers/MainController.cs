using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;

namespace TwitterClient.Controllers
{
    public class MainController : Controller
    {

        #region interface
        public interface IWindow
        {
            event EventHandler publishTweetButtonClicked;
            event EventHandler disconnectClicked;

            ObservableCollection<ITweet> HomeTimeline { set; }
            string TweetToPublish { get; }

            Models.User User { set; }

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
            Window.HomeTimeline = getTimeline();

            var LoggedUser = User.GetLoggedUser();

            Window.User = new Models.User
            {
                Username = LoggedUser.Name,
                ProfileImageUrl = LoggedUser.ProfileImageUrl400x400,
                Tweets = LoggedUser.Timeline.Count,
                Follower = LoggedUser.Followers.Count,
                Following = LoggedUser.Friends.Count
            };


            Window.Show();
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
        private ObservableCollection<ITweet> getTimeline()
        {
            var tweets = Timeline.GetHomeTimeline();
            ObservableCollection<ITweet> liste = new ObservableCollection<ITweet>();

            foreach (var item in tweets)
            {
                liste.Add(item);
            }

            return liste;
        }
    }
}
