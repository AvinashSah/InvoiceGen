using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceGen
{
    public partial class InvoiceGen : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["loginType"]) != "Guest")
            {
                if (Session["user"] != null && Session["key"] != null)
                {
                    string username = Session["user"].ToString();
                    string password = Session["key"].ToString();
                    if (!ValidateUserCreds(username, password))
                    {
                        Response.RedirectPermanent("Login.aspx");
                    }
                }
                else
                {
                    Response.RedirectPermanent("Login.aspx");
                }
            }
        }
        protected void logout(object sender, EventArgs e)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            Session["user"] = null;
            Session["key"] = null;
            Response.RedirectPermanent("Login.aspx");
        }

        private bool ValidateUserCreds(string username, string passWord)
        {
            return string.Equals(username, "GSTSevaAdmin") && string.Equals(passWord, "GSTSevaAdmin@2018");
        }
    }
}