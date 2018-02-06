using InvoiceGen.BAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace InvoiceGen.App_Code
{
    /// <summary>
    /// Summary description for AutoFillService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoFillService : WebService
    {

        public AutoFillService()
        {
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public List<string> SearchCustomers(string prefixText, int count)
        {
            List<string> finalList = new List<string>();
            BAL_Customers bL_Customers = new BAL_Customers();
            List<Entities.Customer> customers = new List<Entities.Customer>();
            customers = bL_Customers.GetAllCustomers();
            if (customers != null && customers.Count > 0)
            {
                foreach (Entities.Customer cust in customers)
                {
                    finalList.Add(cust.ContactName.ToString());
                }
                return finalList;
            }
            else
            {
                return null;
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public List<ProductsMaster> GetProductListByHSNSACCode(string prefixText, int count)
        {
            List<ProductsMaster> finalList = new List<ProductsMaster>();
            BAL_Products bAL_Products = new BAL_Products();
            finalList = bAL_Products.GetProducListByHSNSACCode(prefixText);
            if (finalList != null && finalList.Count > 0)
            {
                ProductsMaster prod = new ProductsMaster();
                prod.Name = "Tests";
                prod.HSNCode = "HSN108";
                prod.Description = "Test";
                prod.GSTPercentage = "18";
                prod.ID = 1;
                finalList.Add(prod);
                return finalList;
            }
            else
            {
                ProductsMaster prod = new ProductsMaster();
                prod.Name = "Tests";
                prod.HSNCode = "HSN108";
                prod.Description = "Test";
                prod.GSTPercentage = "18";
                prod.ID = 1;
                finalList.Add(prod);
                return finalList;
                //return null;
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public List<ProductsMaster> GetProductListByProductName(string prefixText, int count)
        {
            List<ProductsMaster> finalList = new List<ProductsMaster>();
            BAL_Products bAL_Products = new BAL_Products();
            finalList = bAL_Products.GetProductListByProductName(prefixText);
            if (finalList != null && finalList.Count > 0)
            {
                return finalList;
            }
            else
            {
                return null;
            }
        }


        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SubmitAddInvoiceData(Customer Customer, Customer Client, List<ProductsMaster> productList, List<BillProductMapping> productBillMapping)
        {
            AddInvoiceResponse addInvoiceResponse = new AddInvoiceResponse();
            addInvoiceResponse.submited = true;


            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(addInvoiceResponse);

        }


    }
}
