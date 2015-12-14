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

namespace TwitterClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Message> homeTimeline { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            homeTimeline = getFormatedTimeline();
            DataContext = homeTimeline;
        }

        private void ButtonTweet_Click(object sender, RoutedEventArgs e)
        {
            if (GetTextFromRichTextBox(RichTextBoxTweet).Length <= 140)
            {
                try
                {
                    var tweetPublished = Tweet.PublishTweet(GetTextFromRichTextBox(RichTextBoxTweet));
                    MessageBox.Show("Tweet envoyé avec succès !", "OK");
                    RichTextBoxTweet.Document.Blocks.Clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Votre tweet ne peut pas être vide !", "Erreur");
                }
            }
            else if (GetTextFromRichTextBox(RichTextBoxTweet).Length > 140)
            {
                MessageBox.Show("Votre tweet ne peut pas dépasser 140 caractères !", "Erreur");
            }
            else
            {
                MessageBox.Show("Erreur lors de la publication ...", "Erreur");
            }
        }

        private ObservableCollection<Message> getFormatedTimeline()
        {
            var tweets = Timeline.GetHomeTimeline();
            ObservableCollection<Message> liste = new ObservableCollection<Message>();

            foreach (var item in tweets)
            {
                liste.Add(new Message(new Person(item.CreatedBy.Name,
                                                 item.CreatedBy.ScreenName,
                                                 item.CreatedBy.ProfileImageUrl400x400) 
                                     ,item.Text));
            }

            return liste;
        }

        public static string GetTextFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }
        
        private void RichTextBoxTweet_TextChanged(object sender, TextChangedEventArgs e)
        {
            RichTextBoxTweet.Document.Blocks.Clear();
        }
    }
}
