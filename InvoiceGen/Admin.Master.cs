using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceGen
{
    public partial class InvoiceGen : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                        string userRole = roles[0];
                        if (string.Equals(userRole, RoleTypeConstant.SysAdmin, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ImportProducts.Visible = true;
                            ViewClient.Visible = true;
                            ViewBills.Visible = true;
                            GenerateInvoice.Visible = true;
                        }
                        else if (string.Equals(userRole, RoleTypeConstant.Admin, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ImportProducts.Visible = true;
                            ViewClient.Visible = true;
                            ViewBills.Visible = true;
                            GenerateInvoice.Visible = true;
                        }
                        else if (string.Equals(userRole, RoleTypeConstant.EndUser, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ImportProducts.Visible = false;
                            ViewClient.Visible = false;
                            ViewBills.Visible = false;
                            GenerateInvoice.Visible = true;
                            settingsMenu.Visible = false;
                            homeMenu.Visible = false;
                        }
                        else
                        {
                            ImportProducts.Visible = false;
                            ViewClient.Visible = false;
                            ViewBills.Visible = false;
                            GenerateInvoice.Visible = true;
                            settingsMenu.Visible = false;
                            homeMenu.Visible = false;
                        }

                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }

            }
        }
        protected void logout(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

    }
}