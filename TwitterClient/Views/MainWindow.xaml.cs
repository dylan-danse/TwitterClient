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
        public event EventHandler disconnectClicked;
        public event EventHandler savedTweets;

        public MainWindow()
        {
            InitializeComponent();
        }

        public Models.Tweets HomeTimeline
        {
            set { TimelineListBox.DataContext = value; }
        }

        public string TweetToPublish
        {
            get
            {
                return new TextRange(RichTextBoxTweet.Document.ContentStart, RichTextBoxTweet.Document.ContentEnd).Text;
            }
        }

        public Models.User User
        {
            set
            {
                UserPanel.DataContext = value;
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

        public void CleanTweetBox()
        {
            RichTextBoxTweet.Document.Blocks.Clear();
        }

        private void DisconnectLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CallHandler(disconnectClicked, EventArgs.Empty);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CallHandler(savedTweets, EventArgs.Empty);
        }
    }
}
