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

        [ScriptMethod()]
        [WebMethod]
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


        [ScriptMethod()]
        [WebMethod]
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


        [ScriptMethod()]
        [WebMethod]
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


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SubmitAddInvoiceData(Customer Customer, Customer Client, List<ProductsMaster> productList, List<BillProductMapping> productBillMapping, string notesForCustomer, string termsAndCondition)
        {
            Int64 customerID = -1, clientId = -1, billMasterID = -1;
            bool customerExists = false, clientExist = false;
            AddInvoiceResponse addInvoiceResponse = new AddInvoiceResponse();
            BAL_Customers bAL_Customers = new BAL_Customers();
            BAL_Products bAL_Products = new BAL_Products();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                if (Customer.GSTIN == null)
                {
                    customerExists = bAL_Customers.CheckIfCustomerExistByGSTIN(Customer.GSTIN, out customerID);
                }
                else if (Customer.PAN == null)
                {
                    customerExists = bAL_Customers.CheckIfCustomerExistByPAN(Customer.PAN, out customerID);
                }
                else
                {
                    customerExists = bAL_Customers.CheckIfCustomerExistByName(Customer.Name, out customerID);
                }


                if (customerExists)
                {
                    if (Client.GSTIN == null)
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByGSTIN(Client.GSTIN, out clientId);
                    }
                    else if (Client.PAN == null)
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByPAN(Client.PAN, out clientId);
                    }
                    else
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByName(Client.Name, out clientId);
                    }
                }
                else
                {
                    customerID = bAL_Customers.CreateNewCustomer(Customer);
                    if (Client.GSTIN == null)
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByGSTIN(Client.GSTIN, out clientId);
                    }
                    else if (Client.PAN == null)
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByPAN(Client.PAN, out clientId);
                    }
                    else
                    {
                        clientExist = bAL_Customers.CheckIfCustomerExistByName(Client.Name, out clientId);
                    }
                }


                if (clientExist)
                {
                    BAL_Bill bAL_Bill = new BAL_Bill();
                    BillMaster billMaster = new BillMaster();
                    billMaster.BillFromCustID = customerID;
                    billMaster.BillToCustID = clientId;
                    billMaster.BillAddL1 = Client.BillAddL1;
                    billMaster.BillAddL2 = Client.BillAddL2;
                    billMaster.BillStateID = Client.BillStateID;
                    billMaster.BillAddCityID = Client.BillAddCityID;
                    billMaster.NotesForCustomer = notesForCustomer;
                    billMaster.TermsConditions = termsAndCondition;
                    billMaster.ShipAddL1 = Client.ShipAddL1;
                    billMaster.ShipAddL2 = Client.ShipAddL2;
                    billMaster.ShipStateID = Client.ShipStateID;
                    billMaster.ShipAddCityID = Client.ShipAddCityID;
                    billMaster.CreatedBy = 1;
                    billMaster.CreatedOn = DateTime.Now;
                    //Create New Bill
                    billMasterID = bAL_Bill.CreateNewBill(billMaster);

                    foreach (ProductsMaster products in productList)
                    {
                        bool productExist = false;
                        var clientproductID = products.ID;
                        products.ID = 0;
                        productExist = bAL_Products.CheckIfProductExistByHSNCode(products.HSNCode, out Int64 productID);
                        if (productExist)
                        {
                            products.ID = productID;
                        }
                        else
                        {
                            products.ID = bAL_Products.CreateNewProduct(products);
                        }

                        foreach (BillProductMapping productBillMapp in productBillMapping)
                        {
                            if (clientproductID == productBillMapp.ProductID)
                            {
                                productBillMapp.BillID = billMasterID;
                                productBillMapp.ProductID = products.ID;
                                bool created = bAL_Products.CreateNewProductBillMapping(productBillMapp);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
                else
                {
                    clientId = bAL_Customers.CreateNewCustomer(Client);
                }
                addInvoiceResponse.submited = true;
                addInvoiceResponse.message = string.Format("Bill generated with BillNO#:{0}", billMasterID);
                return js.Serialize(addInvoiceResponse);
            }
            catch (Exception ex)
            {
                addInvoiceResponse.submited = false;
                addInvoiceResponse.message = string.Format("Error occured while generating Bill with message:{0}", ex.Message);
                return js.Serialize(addInvoiceResponse);
            }


        }


    }
}
