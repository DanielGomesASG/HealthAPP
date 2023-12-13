using Entities.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectHealthAPP_1
{
    // Criando os testes de geração de Token, método Get e método Post e método Read
    [TestClass]
    public class UnitTest1
    {
        public static string Token { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var result = CallApiPost("https://localhost:7015/api/List").Result;

            var listAppointment = JsonConvert.DeserializeObject<Appointment[]>(result).ToList();

            Assert.IsTrue(listAppointment.Any());
        }

        // Testando a geração de Token
        public void GetToken()
        {

            string urlApiToken = "https://localhost:7015/api/CreateIdentityToken";

            using (var client = new HttpClient())
            {
                string login = "danielgomesasg@gmail.com";
                string password = "@Crud123";
                var data = new
                {
                    email = login,
                    password = password,
                    cpf = "string"
                };
                string JsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

                var result = client.PostAsync(urlApiToken, content);
                result.Wait();
                if (result.Result.IsSuccessStatusCode)
                {
                    var tokenJson = result.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }

            }
        }

        // Testando o método Get
        public string CallApiGet(string url)
        {
            // Gerando token
            GetToken();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = client.GetStringAsync(url);
                    response.Wait();
                    return response.Result;
                }
            }

            return null;

        }

        // Testando o método Post
        public async Task<string> CallApiPost(string url, object data = null)
        {

            string JsonObject = data != null ? JsonConvert.SerializeObject(data) : "";
            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            // Gerando token
            GetToken();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = client.PostAsync(url, content);
                    response.Wait();
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var _return = await response.Result.Content.ReadAsStringAsync();

                        return _return;
                    }
                }
            }

            return null;

        }
    }
}