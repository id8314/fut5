using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace fut5.Services
{
    public class Fut5ApiService
    {
        private static string _Token = "";
        public static string Token
        {
            get { return _Token; }
            set { 
                _Token = value; 
                if(_Token != "")
                {
                    LoggedIn = true;
                }
                else 
                {
                    LoggedIn = false;
                }
            }
        }
        //public static string BaseUrl { get; set; } = "https://fut5umapi.azurewebsites.net/";
        public static string BaseUrl { get; set; } = "";

        public static string ResponseReason { get; set; } = "";
        public static bool LoggedIn { get; set; } = false;
        public static bool IsAdmin { get; set; } = false;
        public static string Roles { get; set; } = "";
        public static readonly Dictionary<string, string> Claims = new();
        public static readonly HttpClient apiClient = new();

        public Fut5ApiService() {}

        public static async Task<bool> FetchTokenAsync(string email, string password) 
        {
            bool isSuccessStatusCode;
            var apiUrl = "";
            try
            {

                if (Fut5ApiService.BaseUrl == "")
                {
                    Fut5ApiService.BaseUrl = Fut5Config.API_URL;
                }

                apiUrl = Fut5ApiService.BaseUrl + "Login";
                // what if pass contains invalid json ? ###
                var json = "{\"email\":\"@1@\",\"password\":\"@2@\"}".Replace("@1@", email).Replace("@2@", password);

                var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await apiClient.PostAsync(apiUrl, data);
                isSuccessStatusCode = response.IsSuccessStatusCode;
                if (isSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var tokenString = JObject.Parse(result)["token"].ToString();
                    Fut5ApiService.Token = tokenString;

                    // fetch roles
                    apiUrl = Fut5ApiService.BaseUrl + "GetClaims";

                    apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    var response2 = await apiClient.GetStringAsync(apiUrl);
                    var claims = JObject.Parse(response2);
                    foreach (var item in claims)
                    {
                        var val = item.Value.Value<string>().ToLower();
                        Fut5ApiService.Claims.Add(item.Key, val);
                        if (item.Key.ToLower() == "role")
                        {
                            if (Fut5ApiService.Roles.Length > 0)
                            {
                                Fut5ApiService.Roles = Roles + ",";
                            }
                            Fut5ApiService.Roles = Roles + val;
                            if (val=="admin" || val=="administrator")
                            {
                                Fut5ApiService.IsAdmin = true;
                            }
                        }
                    }
                }
                Fut5ApiService.ResponseReason = response.ReasonPhrase;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                System.Diagnostics.Debug.WriteLine(exception);
                throw new Exception($"Error contacting API:\n{apiUrl}\n{exception.Message}");
            }

            return isSuccessStatusCode;
        }
    }

    public class Credentials
    {
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
}
