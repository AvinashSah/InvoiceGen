using InvoiceGen.Entities;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;

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
                        HideControls();
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                        string userRole = roles[0];
                        UserOpMap userOpMap = new UserOpMap();
                        BAL.BAL_Common bAL_Common = new BAL.BAL_Common();
                        userOpMap = bAL_Common.GetUserOperationMapping(HttpContext.Current.User.Identity.Name, userRole);
                        foreach (Operations op in userOpMap.OperationsList)
                        {
                            var ctrl = this.FindControl(op.OperationName);
                            switch (ctrl.ID)
                            {
                                case "ImportProducts":
                                    ImportProducts.Attributes.Add("style", "display:block");
                                    break;
                                case "GenerateInvoice":
                                    GenerateInvoice.Attributes.Add("style", "display:block");
                                    break;
                                case "ManageBills":
                                    ViewBills.Attributes.Add("style", "display:block");
                                    break;
                                case "ManageClient":
                                    ViewClient.Attributes.Add("style", "display:block");
                                    break;
                                case "Dashboard":
                                    homeMenu.Attributes.Add("style", "display:block");
                                    break;
                                default:
                                    ShowAllControls();
                                    break;
                            }
                        }

                        string pageName = GetCurrentPageName();
                        switch (pageName)
                        {
                            case "Home":
                                homeMenu.Attributes.Add("class", "active");
                                ViewClient.Attributes["class"].Replace("active", "");
                                ViewBills.Attributes["class"].Replace("active", "");
                                GenerateInvoice.Attributes["class"].Replace("active", "");
                                ImportProducts.Attributes["class"].Replace("active", "");
                                settingsMainli.Attributes["class"].Replace("active", "dropdown");
                                servicesMainli.Attributes["class"].Replace("active", "dropdown");
                                break;
                            case "Invoice":
                                ViewBills.Attributes.Add("class", "active");
                                ViewClient.Attributes["class"].Replace("active", "");
                                homeMenu.Attributes["class"].Replace("active", "");
                                GenerateInvoice.Attributes["class"].Replace("active", "");
                                ImportProducts.Attributes["class"].Replace("active", "");
                                settingsMainli.Attributes["class"].Replace("active", "dropdown");
                                servicesMainli.Attributes.Add("class", "dropdown active");
                                break;
                            case "ImportData":
                                ImportProducts.Attributes.Add("class", "active");
                                ViewClient.Attributes["class"].Replace("active", "");
                                homeMenu.Attributes["class"].Replace("active", "");
                                GenerateInvoice.Attributes["class"].Replace("active", "");
                                ViewBills.Attributes["class"].Replace("active", "");
                                servicesMainli.Attributes["class"].Replace("active", "dropdown");
                                settingsMainli.Attributes.Add("class", "dropdown active");
                                break;
                            case "AddInvoice":
                                break;
                            //case "Clients":
                            //    break;
                            default:
                                homeMenu.Attributes.Add("class", "active");
                                ViewClient.Attributes["class"].Replace("active", "");
                                ViewBills.Attributes["class"].Replace("active", "");
                                GenerateInvoice.Attributes["class"].Replace("active", "");
                                ImportProducts.Attributes["class"].Replace("active", "");
                                settingsMainli.Attributes["class"].Replace("active", "dropdown");
                                servicesMainli.Attributes["class"].Replace("active", "dropdown");
                                break;


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

        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        private void ShowAllControls()
        {
            ImportProducts.Attributes.Add("style", "display:block");
            GenerateInvoice.Attributes.Add("style", "display:block");
            ViewBills.Attributes.Add("style", "display:block");
            ViewClient.Attributes.Add("style", "display:block");
            homeMenu.Attributes.Add("style", "display:block");
        }

        private void HideControls()
        {
            ImportProducts.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            GenerateInvoice.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            ViewBills.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            ViewClient.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            ViewClient.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            homeMenu.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
        }

        protected void logout(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

    }
}