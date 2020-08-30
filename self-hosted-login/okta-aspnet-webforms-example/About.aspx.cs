using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Okta.Sdk;
using Okta.Sdk.Configuration;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_ClickAsync(object sender, EventArgs e)
        {
            if ((TextBox1.Text.Length > 2) && (TextBox2.Text.Length > 5)) //check against db for valid credentials
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
            }
        }
    }
}
