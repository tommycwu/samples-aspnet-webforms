using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private string ssnValue;
        private string atokenStr;
        private string itokenStr;

        public class OktaToken
        {
            [JsonProperty(PropertyName = "access_token")]
            public string AccessToken { get; set; }

            [JsonProperty(PropertyName = "expires_in")]
            public int ExpiresIn { get; set; }

            public DateTime ExpiresAt { get; set; }

            public string Scope { get; set; }

            [JsonProperty(PropertyName = "token_type")]
            public string TokenType { get; set; }

            public bool IsValidAndNotExpiring
            {
                get
                {
                    return !string.IsNullOrEmpty(this.AccessToken) && this.ExpiresAt > DateTime.UtcNow.AddSeconds(30);
                }
            }
        }

        //private async Task<OktaToken> GetTokenForAPI()
        //{
        //    string retJson = string.Empty;
        //    var token = new OktaToken();
        //    var client = new HttpClient();
        //    var client_id = ConfigurationManager.AppSettings["api:ClientId"].ToString();
        //    var client_secret = ConfigurationManager.AppSettings["api:ClientSecret"].ToString();
        //    var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");
        //    client.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(clientCreds));
        //    var token_url = ConfigurationManager.AppSettings["api:Issuer"].ToString() + "/v1/token";
        //    var postMessage = new Dictionary<string, string>();
        //    postMessage.Add("grant_type", "client_credentials");
        //    postMessage.Add("scope", "daysleft");
        //    var request = new HttpRequestMessage(HttpMethod.Post, token_url)
        //    {
        //        Content = new FormUrlEncodedContent(postMessage),
        //    };
        //    var response = await client.SendAsync(request);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        token = JsonConvert.DeserializeObject<OktaToken>(json);
        //        token.ExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    return token;
        //}

        private async Task<string> GetDataFromAPI(string atoken, string rfid)
        {
            try
            {
                var daysleft_url = ConfigurationManager.AppSettings["api:EndPoint"].ToString();
                List<string> values = new List<string>();
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", atoken);
                var res = await client.GetAsync(daysleft_url + rfid);
                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    _ = res.StatusCode.ToString() + res.ReasonPhrase;
                    return "Data Not Found";
                }

            }
            catch (Exception e)
            {
                _ = e;
                return "Unable To Retrieve Data.";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ImageButton1.Enabled = true;

                itokenStr = string.Empty;
                atokenStr = string.Empty;

                //extract the id token and access token from the claims sent back from the sign-in widget
                var claimsList = HttpContext.Current.GetOwinContext().Authentication.User.Claims.ToList();
                foreach (var claimItem in claimsList)
                {
                    if (claimItem.Type == "id_token")
                    {
                        itokenStr = claimItem.Value;
                    }
                    else if (claimItem.Type == "access_token")
                    {
                        atokenStr = claimItem.Value;
                    }

                    if ((itokenStr.Length > 0) && (atokenStr.Length > 0))
                    {
                        break;
                    }
                }

                //display id token claims in a table
                if (itokenStr != string.Empty)
                {
                    var ihandler = new JwtSecurityTokenHandler();
                    var ijsonToken = ihandler.ReadToken(itokenStr);
                    var itokenS = ihandler.ReadToken(itokenStr) as JwtSecurityToken;

                    GridViewID.DataSource = itokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                    GridViewID.DataBind();
                }

                //display access token claims in a table
                if (atokenStr != string.Empty)
                {
                    var ahandler = new JwtSecurityTokenHandler();
                    var ajsonToken = ahandler.ReadToken(atokenStr);
                    var atokenS = ahandler.ReadToken(atokenStr) as JwtSecurityToken;

                    GridViewAccess.DataSource = atokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                    GridViewAccess.DataBind();

                    var tokenList = atokenS.Claims.ToList();
                    foreach (var tokenItem in tokenList)
                    {
                        if (tokenItem.Type == "ssn")
                        {
                            ssnValue = tokenItem.Value;
                            break;
                        }
                    }
                }

                Label3.Visible = true;
                Label4.Visible = true;
            }
            else
            {
                Label1.Text = "Please authenticate to get to your data.";
            }
        }

        protected async void ImageButton1_ClickAsync(object sender, ImageClickEventArgs e)
        {
            //var token = await GetTokenForAPI();
            //if (token != null)
            //{
            //var atoken = token.AccessToken;

            var atoken = atokenStr;
            if (atoken.Length > 1)
            {
                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(atoken);
                //var tokenS = handler.ReadToken(atoken) as JwtSecurityToken;

                //GridViewAccess.DataSource = tokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                //GridViewAccess.DataBind();
                //Label3.Visible = true;
                //Label4.Visible = true;

                var returnedData = "No Data Found";
                returnedData = await GetDataFromAPI(atoken, ssnValue);
                Label1.Text = returnedData;
            }
            else
            {
                Label1.Text = "You do not have rights to retrieve data.";
            }

            //}
            //else
            //{
            //    Label1.Text = "Error retrieving data.";
            //}
        }

        protected void GridViewAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewAccess.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }

        protected void GridViewID_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewID.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType);
            }

            Response.Redirect("AgentSignin.aspx");
        }
    }
}