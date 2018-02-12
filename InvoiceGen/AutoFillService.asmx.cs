using InvoiceGen.BAL;
using InvoiceGen.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;

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

        [ScriptMethod()]
        [WebMethod]
        public List<DropDownState> GetListOfStates()
        {
            BAL_Common bAL_Common = new BAL_Common();
            List<State> stateList = new List<State>();
            stateList = bAL_Common.GetStateListForDropDown();
            List<DropDownState> dropDownStateList = new List<DropDownState>();
            foreach (State state in stateList)
            {
                DropDownState dropDownState = new DropDownState();
                dropDownState.text = state.Name;
                dropDownState.Value = state.ID;
                dropDownStateList.Add(dropDownState);
            }
            return dropDownStateList;
        }

        [ScriptMethod()]
        [WebMethod]
        public List<DropDownCity> GetListOfCitiesByStates(string valueSelected)
        {
            List<DropDownCity> listDropDownCisty = new List<DropDownCity>();
            BAL_Common bAL_Common = new BAL_Common();
            List<City> cityList = new List<City>();
            cityList = bAL_Common.GetCityByStateIDForDropDown(valueSelected);

            foreach (City city in cityList)
            {
                DropDownCity dropDownCity = new DropDownCity();
                dropDownCity.StateID = city.StateID;
                dropDownCity.text = city.Name;
                dropDownCity.Value = city.ID;
                listDropDownCisty.Add(dropDownCity);
            }
            return listDropDownCisty;
        }

        [WebMethod]
        [ScriptMethod()]
        public DropDownState GetStateIDByGSTIN(string gstin)
        {
            DropDownState dropDownState = new DropDownState();
            BAL_Common bAL_Common = new BAL_Common();
            State state = new State();
            state = bAL_Common.GetStateByGSTIN(gstin);
            if (state != null)
            {
                dropDownState.text = state.Name;
                dropDownState.Value = state.ID;
            }
            return dropDownState;
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
                if (Customer.GSTIN != null)
                {
                    Customer.CustomerType = "Company";
                    customerExists = bAL_Customers.CheckIfCustomerExistByGSTIN(Customer.GSTIN, out customerID);
                }
                else if (Customer.PAN != null)
                {
                    Customer.CustomerType = "Individual";
                    customerExists = bAL_Customers.CheckIfCustomerExistByPAN(Customer.PAN, out customerID);
                }
                else
                {
                    customerExists = bAL_Customers.CheckIfCustomerExistByName(Customer.Name, out customerID);
                }


                if (!customerExists)
                {
                    customerID = bAL_Customers.CreateNewCustomer(Customer);
                }

                if (Client.GSTIN != null)
                {
                    Client.CustomerType = "Company";
                    clientExist = bAL_Customers.CheckIfCustomerExistByGSTIN(Client.GSTIN, out clientId);
                }
                else if (Client.PAN != null)
                {
                    Client.CustomerType = "Individual";
                    clientExist = bAL_Customers.CheckIfCustomerExistByPAN(Client.PAN, out clientId);
                }
                else
                {
                    clientExist = bAL_Customers.CheckIfCustomerExistByName(Client.Name, out clientId);
                }


                if (!clientExist)
                {
                    clientId = bAL_Customers.CreateNewCustomer(Client);
                }

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
                    Int64 productID = -1;
                    if (!string.IsNullOrEmpty(products.HSNCode))
                    {
                        productExist = bAL_Products.CheckIfProductExistByHSNCode(products.HSNCode, out productID);
                    }
                    else if (!string.IsNullOrEmpty(products.SACCode))
                    {
                        productExist = bAL_Products.CheckIfProductExistBySACCode(products.SACCode, out productID);
                    }

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
                            //Create Customer Product mapping
                            CustomerProductMapping customerProductMapping = new CustomerProductMapping();
                            customerProductMapping.CustomerID = customerID;
                            customerProductMapping.ProductID = products.ID;
                            customerProductMapping.IsActive = true;
                            customerProductMapping.SalesRate = productBillMapp.SalesRate;
                            Int64 id = bAL_Customers.SaveCustomerProductMapping(customerProductMapping);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
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

        public void GenerateReport(Customer Customer, Customer Client, List<ProductsMaster> productList, List<BillProductMapping> productBillMapping, string notesForCustomer, string termsAndCondition)
        {
            BAL_Common bAL_Common = new BAL_Common();
            PDFGenerator pDFGenerator = new PDFGenerator();
            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK);
            document.AddTitle("Invoice");
            document.AddSubject("This is Invoice against your Items ");
            document.AddKeywords("GSTSeva");
            document.AddCreator("GSTSeva");
            document.AddAuthor("GSTSeva");
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;
            }
        }


    }
}
