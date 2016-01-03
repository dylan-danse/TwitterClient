using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using TwitterClient.Auth;

namespace TwitterClient.Controllers
{
    public class LoginController : Controller
    {
        
        #region interface
        public interface IWindow
        {
            event EventHandler pinButtonClicked;
            event EventHandler loginButtonClicked;

            string pin { get; }

            bool RememberChecked { get; }

            void Show();
            void Close();
            void ShowMessage(string message);
            void ShowError(string message);
            void BackToGetPin();
        }
        #endregion

        /* Var */
        private TwitterCredentials appCredentials;
        private IWindow window;
        private RememberToken rememberToken;

        /* Ctor */
        public override void HandleNavigation(object args)
        {
            rememberToken = new RememberToken();
            appCredentials = new TwitterCredentials("BYOymVcc9Yyk2mA67pLhWxS46", "YSsfV0VmFPEcQqjiZpEvZw28Ngdz4MzO6AEugef5sRzDNGkgmb");

            if (rememberToken.IsTokenExist)
            {
                var token =  rememberToken.GetToken();
                Tweetinvi.Auth.SetCredentials(new TwitterCredentials(appCredentials.ConsumerKey, appCredentials.ConsumerSecret, token.AccessToken, token.AccessTokenSecret));
                if (Tweetinvi.User.GetLoggedUser() != null)
                {
                    new MainController { Window = new MainWindow() }.HandleNavigation(null);
                }
            }
            else
            {
                Window.Show();
            }
        }

        /* Prop */
        public IWindow Window
        {
            get { return window; }
            set
            {
                window = value;
                window.pinButtonClicked += Window_pinButtonClicked;
                window.loginButtonClicked += Window_loginButtonClicked;
            }
        }
        public TwitterCredentials AppCredentials
        {
            get { return appCredentials; }
            set { appCredentials = value; }
        }
        
        /* Event */
        private void Window_loginButtonClicked(object sender, EventArgs e)
        {
            ITwitterCredentials cred = CredentialsCreator.GetCredentialsFromVerifierCode(Window.pin, AppCredentials);
            if (cred != null)
            {
                Tweetinvi.Auth.SetCredentials(cred);
            }

            if (Tweetinvi.User.GetLoggedUser() != null)
            {
                new MainController { Window = new MainWindow() }.HandleNavigation(null);
                
                if (Window.RememberChecked)
                {
                    rememberToken.SaveRememberToken(new Models.Token(Tweetinvi.User.GetLoggedUser().Name, cred.AccessToken, cred.AccessTokenSecret));
                }
                Window.Close();
            }
            else
            {
                Window.ShowError("Pin incorrect !");
                AppCredentials.AuthorizationKey = null;
                AppCredentials.AuthorizationSecret = null;
                Window.BackToGetPin();
            }

        }

        private void Window_pinButtonClicked(object sender, EventArgs e)
        {
            Process.Start(CredentialsCreator.GetAuthorizationURL(AppCredentials));
        }
    }
}
