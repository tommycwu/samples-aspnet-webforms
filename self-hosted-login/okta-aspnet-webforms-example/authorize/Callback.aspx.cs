using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example.authorize
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public partial class Callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        protected void Button_Get_Click(object sender, EventArgs e)
        {
            string fullUrl = hdnResultValue.Value;
            string hashUrl = fullUrl.Substring(fullUrl.IndexOf('#') + 1);
            string minusState = hashUrl.Substring(0, hashUrl.IndexOf('&'));
            string idToken = minusState.Substring(minusState.IndexOf('=') + 1);
            var ahandler = new JwtSecurityTokenHandler();
            var ajsonToken = ahandler.ReadToken(idToken);
            var idtokenS = ahandler.ReadToken(idToken) as JwtSecurityToken;

            GridViewID.DataSource = idtokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
            GridViewID.DataBind();
        }

        protected void GridViewID_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewID.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }
    }

}
