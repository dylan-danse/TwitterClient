using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tweetinvi;
using Tweetinvi.Core.Credentials;

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

            void Show();
            void Close();
        }
        #endregion

        /* Var */
        private TwitterCredentials appCredentials = new TwitterCredentials("BYOymVcc9Yyk2mA67pLhWxS46", "YSsfV0VmFPEcQqjiZpEvZw28Ngdz4MzO6AEugef5sRzDNGkgmb");
        private IWindow window;

        /* Ctor */
        public override void HandleNavigation(object args)
        {
            Window.Show();
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
            get
            {
                return appCredentials;
            }

            set
            {
                appCredentials = value;
            }
        }
        
        /* Event */
        private void Window_loginButtonClicked(object sender, EventArgs e)
        {
            Auth.SetCredentials(CredentialsCreator.GetCredentialsFromVerifierCode(Window.pin, AppCredentials));

            MainController mainController = new MainController { Window = new MainWindow() };
            mainController.HandleNavigation(null);

            Window.Close();
        }
        
        private void Window_pinButtonClicked(object sender, EventArgs e)
        {
            Process.Start(CredentialsCreator.GetAuthorizationURL(AppCredentials));
        }
    }
}
