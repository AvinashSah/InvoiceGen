using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceGen
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Response.Cookies["UserName"].Value != null && Response.Cookies["Password"].Value != null)
            {
                string username = Response.Cookies["UserName"].Value;
                string passWord = Response.Cookies["Password"].Value;
                if (ValidateCreds(username, passWord))
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }
        protected void GuestLoginClick(Object sender, EventArgs e)
        {
            Session["loginType"] = "Guest";
            Response.RedirectPermanent("AddInvoice.aspx");
        }

        protected void adminLoginClick(Object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string passWord = txtPassword.Text.Trim();
            if (ValidateCreds(username, passWord))
            {
                if (chkRememberMe.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(2);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(2);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                Session["user"] = txtUserName.Text.Trim();
                Session["userID"] = 1;
                Session["key"] = txtPassword.Text.Trim();

                Response.Redirect("Home.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
            }
        }

        private bool ValidateCreds(string username, string passWord)
        {
            return string.Equals(username, "GSTSevaAdmin") && string.Equals(passWord, "GSTSevaAdmin@2018");
        }
    }
}