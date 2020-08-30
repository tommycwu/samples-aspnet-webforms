using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public partial class Contact : Page
    {
        public string UsageString = string.Empty;

        public class AppId
        {
            public string Name;
            public string Id;
        }

        public async void GetUsage(string apiUrl)
        {
            string retJson = string.Empty;
            try
            {
                var endpoint = new Uri(apiUrl);
                var webRequest = WebRequest.Create(endpoint) as HttpWebRequest;
                if (webRequest != null)
                {
                    var claimsList = HttpContext.Current.GetOwinContext().Authentication.User.Claims.ToList();
                    string token = " ";

                    for (int i = 0; i < claimsList.Count - 1; i++)
                    {
                        if (claimsList[i].Type == "access_token")
                        {
                            token = claimsList[i].Value;
                            break;
                        }
                    }

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var res = await client.GetAsync(apiUrl);

                    List<string> values = new List<string>();
                    if (res.IsSuccessStatusCode)
                    {
                        var json = res.Content.ReadAsStringAsync().Result;
                        Label1.Text = json;
                        //values = JsonConvert.DeserializeObject<List<string>>(json);
                    }
                    else
                    {
                        Label1.Text = res.StatusCode.ToString() + " : " + res.ReasonPhrase;
                        //values = new List<string> { res.StatusCode.ToString(), res.ReasonPhrase };
                    }

                }
                else
                {
                    Label1.Text = "webRequest = null";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetUsage("https://localhost:44384/api/listitems/3");
        }
    }
}
