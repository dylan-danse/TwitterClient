using System;
using System.Collections.Generic;
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
using TwitterClient.Parser;

namespace TwitterClient.Views
{
    /// <summary>
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window, Controllers.SaveController.IWindow
    {

        public SaveWindow()
        {
            InitializeComponent();
        }

        public IList<string> ListTypes
        {
            set
            {
                ListTypesComboBox.DataContext = value;
            }
        }

        public string ListTypeSelected
        {
            get
            {
                if (TypesComboBox.SelectedItem != null)
                {
                    return ListTypesComboBox.SelectedItem.ToString();
                }
                return null;
            }
        }

        public IList<string> ParserNames
        {
            set
            {
                TypesComboBox.DataContext = value;
            }
        }

        public string ParserSelect
        {
            get
            {
                if (TypesComboBox.SelectedItem != null)
                {
                    return TypesComboBox.SelectedItem.ToString();
                }
                return null;
             }
        }

        public string Path
        {
            get
            {
                return PathTextBox.Text;
            }

            set
            {
                PathTextBox.Text = value;
            }
        }

        public event EventHandler OnSave;
        public event EventHandler OpenSaveDialog;


        protected virtual void CallHandler(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void PathDialogButton_Click(object sender, RoutedEventArgs e)
        {
            CallHandler(OpenSaveDialog, EventArgs.Empty);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Path.Length == 0)
            {
                Views.Dialog.ErrorDialog errorDialog = new Dialog.ErrorDialog("Veuiller selectionner un chemin");
                return;
            }

            if (ListTypeSelected == null)
            {
                Views.Dialog.ErrorDialog errorDialog = new Dialog.ErrorDialog("Veuiller selectionner un type de liste");
                return;
            }

            if (ParserSelect == null)
            {
                Views.Dialog.ErrorDialog errorDialog = new Dialog.ErrorDialog("Veuiller selectionner un type");
                return;
            }
            CallHandler(OnSave, EventArgs.Empty);
        }
    }
}
