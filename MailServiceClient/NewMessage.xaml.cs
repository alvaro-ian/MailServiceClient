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
    /// Lógica interna para NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public NewMessage(string username)
        {
            InitializeComponent();

            remetenteTxt.Text = username;
        }

        // Construtor para resposta
        public NewMessage(string username, Mensagem msg, bool encaminhar = false)
        {
            InitializeComponent();

            if (!encaminhar) 
            {
                remetenteTxt.Text = username;
                destinatarioTxb.Text = msg.Remetente == username ? msg.Destinatario : msg.Remetente;
                assuntoTxb.Text = "RE: " + msg.Assunto;
            }
            else
            {
                remetenteTxt.Text = username;
                assuntoTxb.Text = msg.Assunto;
                corpoTxb.Text = msg.Corpo;
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (destinatarioTxb.Text == "" || assuntoTxb.Text == "" || corpoTxb.Text == "")
            {
                errorTxt.Text = "Preencha todos os campos para mandar a Mensagem";
                return;
            }

            Mensagem send = new Mensagem()
            {
                Remetente = remetenteTxt.Text,
                Destinatario = destinatarioTxb.Text,
                Assunto = assuntoTxb.Text,
                Corpo = corpoTxb.Text
            };

            if (MailService.SendMessage(send).Result)
            {
                MessageBox.Show("Sua mensagem foi enviada com êxito!", "MailServiceClient");
                Close();
            }
            else
            {
                errorTxt.Text = "Sua mensagem não pode ser enviada";
            }
        }
    }
}
