using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;
using TwitterClient.Controllers;

namespace TwitterClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, MainController.IWindow
    {
        public event EventHandler publishTweetButtonClicked;

        public MainWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<Message> HomeTimeline
        {
            set { listViewTimeLine.DataContext = value; }
        }

        public string TweetToPublish
        {
            get
            {
                return new TextRange(RichTextBoxTweet.Document.ContentStart, RichTextBoxTweet.Document.ContentEnd).Text;
            }
        }

        private void ButtonTweet_Click(object sender, RoutedEventArgs e)
        {
            CallHandler(publishTweetButtonClicked, EventArgs.Empty);
        }

        public void CallHandler(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message,"Erreur",MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message,"Succès",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
