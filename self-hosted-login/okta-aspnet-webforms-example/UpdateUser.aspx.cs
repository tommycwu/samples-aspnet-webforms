using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Okta.Sdk;
using Okta.Sdk.Configuration;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        private OktaClient client;

        protected void Page_Load(object sender, EventArgs e)
        {
            client = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = ConfigurationManager.AppSettings["okta:OktaDomain"].ToString(),
                Token = ConfigurationManager.AppSettings["okta:APIkey"].ToString(),
            });
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length > 5)
            {
                var userUpdate = client.Users.GetUserAsync(TextBox1.Text);
                userUpdate.Result.Profile["firstName"] = TextBox2.Text;
                userUpdate.Result.Profile["lastName"] = TextBox3.Text;
                userUpdate.Result.Profile["email"] = TextBox4.Text;
                var updResults = await userUpdate.Result.UpdateAsync();
                Label5.Text = updResults.StatusChanged.ToString();
                if (TextBox5.Text.Length > 5)
                {
                    var userPwd = client.Users.GetUserAsync(TextBox1.Text);
                    userPwd.Result.Credentials.Password.Value = TextBox5.Text;
                    var pwdResults = await userPwd.Result.UpdateAsync();
                    Label5.Text = pwdResults.StatusChanged.ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var userFound = client.Users.GetUserAsync(TextBox1.Text);
            TextBox2.Text = userFound.Result.Profile.FirstName;
            TextBox3.Text = userFound.Result.Profile.LastName;
            TextBox4.Text = userFound.Result.Profile.Email;
        }
    }
}