using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string stoken = Application["stoken"].ToString();
            string atoken = Application["atoken"].ToString();
            var stream = atoken;
            var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

            GridViewClaims.DataSource = tokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
            GridViewClaims.DataBind();
        }

        protected void GridViewClaims_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewClaims.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }
    }
}