using System;
using System.Web.Security;
using System.Web.UI;

namespace InvoiceGen
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }
        //protected void GuestLoginClick(Object sender, EventArgs e)
        //{
        //    Response.RedirectPermanent("AddInvoice.aspx", true);
        //}

        protected void adminLoginClick(Object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string passWord = txtPassword.Text.Trim();
            bool rememberMe = chkRememberMe.Checked;
            if (ValidateCreds(username, passWord))
            {
                FormsAuthentication.RedirectFromLoginPage(username, rememberMe);
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