using MailServiceClient.Classes;
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

namespace MailServiceClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void GetMail()
        {
            List<Mensagem> list = new List<Mensagem>();

            if (username == "super")
            {
                list = MailService.GetAllMail().Result;
            }
            else
            {
                list = MailService.GetUserMail(username).Result;
            }
            mailListView.ItemsSource = list;
        }

        // EVENT HANDLERS
        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTxb.Text != "")
            {
                username = usernameTxb.Text;

                GetMail();

                error.Visibility = Visibility.Collapsed;
                UsernameQuery.Visibility = Visibility.Collapsed;
                welcomeTxt.Text = "Bem-vindo, " + username;
            }
            else
            {
                error.Visibility = Visibility.Visible;
            }
        }

        private void mailListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mailListView.SelectedValue != null)
            {
                seeBtn.IsEnabled = true;
            }
            else
            {
                seeBtn.IsEnabled = false;
            }
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessage newMessage = new NewMessage(username)
            {
                Owner = this
            };

            newMessage.ShowDialog();

            GetMail();
        }

        private void seeBtn_Click(object sender, RoutedEventArgs e)
        {
            Mensagem msg = mailListView.SelectedItem as Mensagem;

            ViewMessage viewMessage = new ViewMessage(username, msg)
            {
                Owner = this
            };

            viewMessage.ShowDialog();

            GetMail();
        }

        private void changeUsrBtn_Click(object sender, RoutedEventArgs e)
        {
            UsernameQuery.Visibility = Visibility.Visible;
        }
    }
}
