using MailServiceClient.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailServiceClient
{
    /// <summary>
    /// Lógica interna para ViewMessage.xaml
    /// </summary>
    public partial class ViewMessage : Window
    {
        Mensagem message;
        string username;

        public ViewMessage(string username, Mensagem msg)
        {
            InitializeComponent();

            this.username = username;
            message = msg;
            remetenteTxt.Text = message.Remetente;
            destinatarioTxt.Text = message.Destinatario;
            assuntoTxt.Text = message.Assunto;
            corpoTxt.Text = message.Corpo;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void respondBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessage response = new NewMessage(username, message)
            {
                Owner = this
            };

            response.ShowDialog();
        }

        private void redirectBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessage redirect = new NewMessage(username, message, true)
            {
                Owner = this
            };

            redirect.ShowDialog();
        }

        private void apagarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Apagar mensagem?", "MailServiceClient", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                if (MailService.DeleteMessage(message.Id).Result)
                {
                    Close();
                }
            }
        }
    }
}
