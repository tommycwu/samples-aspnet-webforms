using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Okta.Sdk;
using Okta.Sdk.Configuration;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private class LegacyUser
        {
            public string UserName;
            public string Password;
            public string FirstName;
            public string LastName;
        }

        private static async Task<string[]> GetTokens(string u, string p)
        {
            var domain = ConfigurationManager.AppSettings["okta:OktaDomain"];
            var redirectUrl = ConfigurationManager.AppSettings["okta:RedirectUri"];
            var oktaAuthorizationServer = ConfigurationManager.AppSettings["alterra:AuthZserver"];
            var clientId = ConfigurationManager.AppSettings["alterra:ClientId"];
            var redirectUrlEncoded = System.Net.WebUtility.UrlEncode(redirectUrl);
            var responseType = System.Net.WebUtility.UrlEncode("id_token token");
            var state = "testing";
            var nonce = "testing nonce";
            var scope = System.Net.WebUtility.UrlEncode("openid profile");
            var authnUri = $"{domain}/api/v1/authn";
            var username = u;
            var password = p;

            dynamic bodyOfRequest = new
            {
                username,
                password,
                options = new
                {
                    multiOptionalFactorEnroll = true,
                    warnBeforePasswordExpired = true,
                },
            };

            var body = JsonConvert.SerializeObject(bodyOfRequest);

            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            string sessionToken;

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = false;

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage authnResponse = await httpClient.PostAsync(authnUri, stringContent);

                if (authnResponse.IsSuccessStatusCode)
                {
                    var authnResponseContent = await authnResponse.Content.ReadAsStringAsync();
                    dynamic authnObject = JsonConvert.DeserializeObject(authnResponseContent);
                    sessionToken = authnObject.sessionToken;

                    var authorizeUri = $"{domain}/oauth2/{oktaAuthorizationServer}/v1/authorize?client_id={clientId}&redirect_uri={redirectUrlEncoded}" +
                        $"&response_type={responseType}&sessionToken={sessionToken}&state={state}&nonce={nonce}&scope={scope}";

                    HttpResponseMessage authorizeResponse = await httpClient.GetAsync(authorizeUri);
                    var statusCode = (int)authorizeResponse.StatusCode;

                    if (statusCode == (int)HttpStatusCode.Found)
                    {
                        var redirectUri = authorizeResponse.Headers.Location;
                        var queryDictionary = HttpUtility.ParseQueryString(redirectUri.AbsoluteUri);
                        int i;
                        {
                            for (i = 0; i < queryDictionary.Count - 1; i++)
                            {
                                if (queryDictionary.AllKeys[i].Contains("id_token"))
                                {
                                    break;
                                }
                            }
                        }

                        var retArray = new string[]
                        {
                            queryDictionary[i],
                            queryDictionary["access_token"],
                        };

                        return retArray;
                    }
                }
            }

            return null;
        }


        private bool LegacyCreds(string u, string p, ref string f, ref string l)
        {
            LegacyUser aUser = new LegacyUser();
            aUser.FirstName = "Afirst";
            aUser.LastName = "Alast";
            aUser.UserName = "Afirst.ALast@oktatest.net";
            aUser.Password = "1 Password";

            LegacyUser bUser = new LegacyUser();
            bUser.FirstName = "BFirst";
            bUser.LastName = "BLast";
            bUser.UserName = "BFirst.BLast@oktatest.net";
            bUser.Password = "2 Password";

            LegacyUser cUser = new LegacyUser();
            cUser.FirstName = "CFirst";
            cUser.LastName = "CLast";
            cUser.UserName = "CFirst.CLast@oktatest.net";
            aUser.Password = "3 Password";

            var userList = new List<LegacyUser>();
            userList.Add(aUser);
            userList.Add(bUser);
            userList.Add(cUser);

            if ((u.Length > 5) && (p.Length > 5))
            {
                //loop thru userList to check for user... set it it the ref vars
                Random rngFirst = new Random();
                var i = rngFirst.Next(1, 10);
                Random rngLast = new Random();
                var j = rngLast.Next(1, 10);
                f = "First" + i.ToString();
                l = "Last" + j.ToString();

                return true;
            }

            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] != null)
            {
                Label4.Text = "User/Password is invalid";
            }
        }

        protected async void Button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string[] getArray = new string[2];
                //check against okta db
                getArray = await GetTokens(TextBox1.Text, TextBox2.Text);
                if (getArray != null) //if user is there take the session token access token to the next step
                {
                    Application["itoken"] = getArray[0];
                    Application["atoken"] = getArray[1];
                    Response.Redirect("/usertokens.aspx");
                }
                else //create the user
                {
                    string fName = string.Empty;
                    string lName = string.Empty;
                    if (LegacyCreds(TextBox1.Text, TextBox2.Text, ref fName, ref lName)) //check against db for valid credentials
                    {
                        var client = new OktaClient(new OktaClientConfiguration
                        {
                            OktaDomain = ConfigurationManager.AppSettings["okta:OktaDomain"].ToString(),
                            Token = ConfigurationManager.AppSettings["okta:APIkey"].ToString(),
                        });

                        var results = await client.Users.CreateUserAsync(new CreateUserWithPasswordOptions
                        {
                            Profile = new UserProfile
                            {
                                FirstName = fName,
                                LastName = lName,
                                Email = TextBox1.Text,
                                Login = TextBox1.Text,
                            },
                            Password = TextBox2.Text,
                            Activate = true,
                        });
                        getArray = await GetTokens(TextBox1.Text, TextBox2.Text);
                        Application["atoken"] = getArray[1];
                        Response.Redirect("/tokensuser.aspx");
                    }
                    else
                    {
                        Label4.Text = "User/Password is invalid";
                    }
                }
            }
            catch
            {
                Label4.Text = "User/Password is invalid";
            }
        }
    }
}