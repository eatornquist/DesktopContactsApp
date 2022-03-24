using DesktopContactsApp.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Contact> contacts;


        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();
            ReadDatabase();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }


        void ReadDatabase()
        {
            
            using(SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = (conn.Table<Contact>().ToList()).OrderBy(c => c.Name).ToList();

                /* This is an example of how work the statement before.
                var variable = from c2 in contacts
                               orderby c2.Name
                               select c2;
                */
            }

            if(contacts != null)
            {
                //{
                //    contactsListView.Items.Add(new ListViewItem() { Content = c});
                //}

                contactsListView.ItemsSource = contacts;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = (TextBox)sender;
            
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList(); //We are filtering the contacts that contains what the user has written. 


            /* This is an example of how work the statement before.
            var filteredList2 = (from c2 in contacts
                                where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                                select c2).ToList();
            */

            contactsListView.ItemsSource = filteredList;
        }

    }
}
