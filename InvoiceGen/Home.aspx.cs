using InvoiceGen.BAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InvoiceGen
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        LoadAllProducts();

                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void LoadAllProducts()
        {
            BAL_Products bAL_Products = new BAL_Products();
            List<ProductsMaster> productLlist = new List<ProductsMaster>();
            productLlist = bAL_Products.GetAllProductList();
            BindDatatoTable(productLlist);
        }

        private void BindDatatoTable(List<ProductsMaster> productList)
        {
            string finalstring = "";
            foreach (ProductsMaster product in productList)
            {
                string htmlContent = "<tr>";
                htmlContent += "<th scope=\"row\">" + Convert.ToString(product.ID) + "</th>";
                htmlContent += "<td>" + Convert.ToString(product.Name) + "</td>";
                htmlContent += "<td>" + Convert.ToString(string.IsNullOrEmpty(product.HSNCode) ? product.SACCode : product.HSNCode) + "</td>";
                htmlContent += "<td>" + Convert.ToString(product.Description) + "</td>";
                htmlContent += "<td>" + Convert.ToString(product.CessPercentage) + "</td>";
                htmlContent += "<td>" + Convert.ToString(product.GSTPercentage) + "</td>";
                htmlContent += "</tr>";
                finalstring += htmlContent;
            }
            allProductsListByFilter.Controls.Add(new Literal { Text = finalstring.ToString() });
        }
    }
}