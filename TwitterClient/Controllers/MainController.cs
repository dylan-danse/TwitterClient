using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;
using TwitterClient.Models;
using TwitterClient.Twitter;
using TwitterClient.Views;

namespace TwitterClient.Controllers
{
    public class MainController : Controller
    {

        #region interface
        public interface IWindow
        {
            event EventHandler publishTweetButtonClicked;
            event EventHandler disconnectClicked;
            event EventHandler savedTweets;

            Tweets HomeTimeline { get; set; }
            void AddTweet(ITweet tweet);
            string TweetToPublish { get; set; }

            Models.User User { set; }

            void Show();
            void Close();
        }
        #endregion

        /* Var */
        private IWindow window;

        /* Ctor */
        public override void HandleNavigation(object args)
        {
            Window.HomeTimeline = new HomeTimeLine().GetList();

            Window.User = new Models.User(Tweetinvi.User.GetLoggedUser());

            Window.Show();

            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByFriend += (sender, a) =>
            {
                Window.AddTweet(a.Tweet);
            };
            stream.TweetCreatedByMe += (sender, a) =>
            {
                Window.AddTweet(a.Tweet);
            };
            stream.StartStreamAsync();
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
                Window.savedTweets += Window_savedTweets;
            }
        }

        private void Window_savedTweets(object sender, EventArgs e)
        {
            SaveController saveController = new SaveController { Window = new SaveWindow() };
            saveController.HandleNavigation(null);
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
                    Views.Dialog.ErrorDialog errorDialog = new Views.Dialog.ErrorDialog("Votre tweet ne peut pas dépasser 140 caractères !");
                }
                else
                {
                    Tweetinvi.Tweet.PublishTweet(window.TweetToPublish.Trim());
                    Views.Dialog.SuccessDialog successDialog = new Views.Dialog.SuccessDialog("Votre tweet a été publié avec succès");

                    Window.TweetToPublish = string.Empty;
                }
            }
            catch (Exception)
            {
                Views.Dialog.ErrorDialog errorDialog = new Views.Dialog.ErrorDialog("Votre tweet ne peut pas être nul !");
            }
        }

    }
}
