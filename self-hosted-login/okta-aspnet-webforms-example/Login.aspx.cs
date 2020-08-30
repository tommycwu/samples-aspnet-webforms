using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private static async Task<string[]> GetOktaToken(string u, string p)
        {
            var domain = ConfigurationManager.AppSettings["okta:OktaDomain"];
            var oktaAuthorizationServer = ConfigurationManager.AppSettings["okta:AuthZserver"];
            var clientId = ConfigurationManager.AppSettings["okta:ClientId"];
            var redirectUrl = ConfigurationManager.AppSettings["okta:RedirectUri"];
            var redirectUrlEncoded = System.Net.WebUtility.UrlEncode(redirectUrl);
            var responseType = System.Net.WebUtility.UrlEncode("id_token token");
            var state = "testing";
            var nonce = "testing nonce";
            var scope = System.Net.WebUtility.UrlEncode("manage:lists openid email profile");
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

                    var authorizeUri = $"{domain}/oauth2/{oktaAuthorizationServer}/v1/authorize?client_id={clientId}&redirect_uri={redirectUrlEncoded}&response_type={responseType}&sessionToken={sessionToken}&state={state}&nonce={nonce}&scope={scope}";

                    HttpResponseMessage authorizeResponse = await httpClient.GetAsync(authorizeUri);
                    var statusCode = (int)authorizeResponse.StatusCode;

                    if (statusCode == (int)HttpStatusCode.Found)
                    {
                        var redirectUri = authorizeResponse.Headers.Location;
                        var queryDictionary = HttpUtility.ParseQueryString(redirectUri.AbsoluteUri);
                        // var idTokenKey = $"{oktaAuthRequestInformation.RedirectUrl}#id_token";

                        var retArray = new string[] { sessionToken, queryDictionary["access_token"] };
                        return retArray;
                    }
                }
            }

            return null;
        }


        private bool LegacyCreds(string u, string p)
        {
            if ((u.Length > 2) && (p.Length > 5))
            {
            }

            return true;
        }

        private bool OktaCreds(string u, string p)
        {
            if ((u.Length > 2) && (p.Length > 5))
            {
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void Button1_ClickAsync(object sender, EventArgs e)
        {
            string[] getArray = new string[2];
            //check against okta db
            getArray = await GetOktaToken(TextBox1.Text, TextBox2.Text);
            if (getArray != null) //if user is there take the session token access token to the next step
            {
                Application["stoken"] = getArray[0];
                Application["atoken"] = getArray[1];
                Response.Redirect("/webform4.aspx");
            }
            else //create the user
            {
                if (LegacyCreds(TextBox1.Text, TextBox2.Text)) //check against db for valid credentials
                {
                    var client = new OktaClient(new OktaClientConfiguration
                    {
                        OktaDomain = ConfigurationManager.AppSettings["okta:OktaDomain"].ToString(),
                        Token = ConfigurationManager.AppSettings["okta:APIkey"].ToString(),
                    });

                    Random rngFirst = new Random();
                    rngFirst.Next(1, 10);
                    Random rngLast = new Random();
                    rngLast.Next(1, 10);

                    var vader = await client.Users.CreateUserAsync(new CreateUserWithPasswordOptions
                    {
                        Profile = new UserProfile
                        {
                            FirstName = "FirstName" + rngFirst.ToString(), //pull user profile info from db to use here
                            LastName = "LastName" + rngLast.ToString(), //pull user profile info from db to use here
                            Email = TextBox1.Text,
                            Login = TextBox1.Text,
                        },
                        Password = TextBox2.Text,
                        Activate = true,
                    });

                    if (!Request.IsAuthenticated)
                    {
                        getArray = await GetOktaToken(TextBox1.Text, TextBox2.Text);
                        Application["stoken"] = getArray[0];
                        Application["atoken"] = getArray[1];
                        Response.Redirect("/webform4.aspx");
                    }
                }
                else
                {
                    Label4.Text = "User/Password is invalid";
                }
            }
        }
    }
}