using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MailServiceClient.Classes
{
    class MailService
    {
        public const string BASE_URL = "http://localhost:8080/mail/";
        public const string MESSAGE_BY_ID_ENDPOINT = "?id={0}";
        public const string USER_MESSAGES_ENDPOINT = "?username={0}";
        public const string DELETE_MESSAGE_ENDPOINT = "?id={0}";

        // POST ENDPOINT
        public static async Task<bool> SendMessage(Mensagem msg)
        {
            bool successful = false;

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(msg);
                
                var response = await client.PostAsync(BASE_URL, new StringContent(json)).ConfigureAwait(false);
                successful = response.IsSuccessStatusCode;
            }

            return successful;
        }

        // GET ALL ENDPOINT
        public static async Task<List<Mensagem>> GetAllMail()
        {
            List<Mensagem> mail = new List<Mensagem>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(BASE_URL).ConfigureAwait(false);
                string json = await response.Content.ReadAsStringAsync();

                mail = JsonConvert.DeserializeObject<List<Mensagem>>(json);
            }

            return mail;
        }

        // GET BY ID ENDPOINT
        public static async Task<Mensagem> GetMessage(int id)
        {
            Mensagem msg;

            using (HttpClient client = new HttpClient())
            {
                string url = BASE_URL + string.Format(MESSAGE_BY_ID_ENDPOINT, id);

                var response = await client.GetAsync(url).ConfigureAwait(false);
                string json = await response.Content.ReadAsStringAsync();

                msg = JsonConvert.DeserializeObject<Mensagem>(json);
            }

            return msg;
        }

        // GET BY USERNAME ENDPOINT
        public static async Task<List<Mensagem>> GetUserMail(string username)
        {
            List<Mensagem> mail = new List<Mensagem>();

            using (HttpClient client = new HttpClient())
            {
                string url = BASE_URL + string.Format(USER_MESSAGES_ENDPOINT, username);

                var response = await client.GetAsync(url).ConfigureAwait(false);
                string json = await response.Content.ReadAsStringAsync();

                mail = JsonConvert.DeserializeObject<List<Mensagem>>(json);
            }

            return mail;
        }

        // DELETE ENDPOINT
        public static async Task<bool> DeleteMessage(int id)
        {
            bool successful = false;

            using (HttpClient client = new HttpClient())
            {
                string url = BASE_URL + string.Format(DELETE_MESSAGE_ENDPOINT, id);

                var response = await client.DeleteAsync(url).ConfigureAwait(false);
                successful = response.IsSuccessStatusCode;
            }

            return successful;
        }
    }
}
