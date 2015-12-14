using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using TwitterClient.Controllers;

namespace TwitterClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, LoginController.IWindow
    {
        public string pin
        {
            get {  return TextBoxEncodePin.Text; }
        }

        public LoginWindow()
        {
            InitializeComponent();
        }

        public event EventHandler pinButtonClicked;
        public event EventHandler loginButtonClicked;

        private void ButtonPin_Click(object sender, RoutedEventArgs e)
        {
            CallHandler(pinButtonClicked,EventArgs.Empty);

            //Change Background Color
            var bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#FFCEBEF9");

            //Hide First Button and enable Connection field
            ButtonPin.Visibility = Visibility.Hidden;
            ButtonLogin.Visibility = Visibility.Visible;
            TextBoxEncodePin.Visibility = Visibility.Visible;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            CallHandler(loginButtonClicked, EventArgs.Empty);
        }
        
        public void CallHandler(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
