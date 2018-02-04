using InvoiceGen.BAL;
using InvoiceGen.Entities;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;

namespace InvoiceGen
{
    public partial class AddInvoice : System.Web.UI.Page
    {
        private bool startConversion = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStateListToControl(companyAddrState);
                companyAddrState.Items.FindByText("--Select State--").Selected = true;
                BindStateListToControl(billToClientStateList);
                billToClientStateList.Items.FindByText("--Select State--").Selected = true;
                BindStateListToControl(shipToClientStateList);
                shipToClientStateList.Items.FindByText("--Select State--").Selected = true;
                amoutCalculationIntraState.Visible = false;
                amoutCalculationInterState.Visible = false;
            }
        }

        #region COMMON METHODS
        private void BindStateListToControl(DropDownList dropDownList)
        {
            BAL_Common bAL_Common = new BAL_Common();
            dropDownList.DataSource = bAL_Common.GetStateListForDropDown();
            dropDownList.DataTextField = "Name";
            dropDownList.DataValueField = "ID";
            dropDownList.DataBind();
        }
        private void BindCityToControl(string selectedState, DropDownList dropDownList)
        {
            BAL_Common bAL_Common = new BAL_Common();
            dropDownList.DataSource = bAL_Common.GetCityByStateIDForDropDown(selectedState);
            dropDownList.DataTextField = "Name";
            dropDownList.DataValueField = "ID";
            dropDownList.DataBind();
            dropDownList.Items.FindByText("--Select City--").Selected = true;
        }
        protected void generateInvoicePDF_ServerClick(object sender, EventArgs e)
        {
            startConversion = true;
        }
        #endregion

        #region CUSTOMER INFO
        protected void companyAddrState_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedState = companyAddrState.SelectedValue;
            BindCityToControl(selectedState, companyAddrCity);
        }
        /// <summary>
        /// City sleected event for Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void companyAddrCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Company GSTIN Changes Event to display the state based on GSTIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void compannyGstin_TextChanged(object sender, EventArgs e)
        {
            BAL_Common bAL_Common = new BAL_Common();
            var textValue = compannyGstin.Text.Substring(0, 2);
            int number;
            if (Int32.TryParse(textValue, out number))
            {
                // do something, like:
                if (number >= 10 || number <= 99)
                {
                    State state = bAL_Common.GetStateByID(textValue);
                    string id = Convert.ToString(state.ID);
                    companyAddrState.ClearSelection();
                    companyAddrState.SelectedValue = id;
                    BindCityToControl(id, companyAddrCity);
                    if (BAL_Common.StringHasCharacters(billToClientGSTIN.Text))
                    {
                        var billClientGStin = billToClientGSTIN.Text.Substring(0, 2);
                        if (billClientGStin == textValue)
                        {
                            amoutCalculationIntraState.Visible = true;
                            amoutCalculationInterState.Visible = false;
                        }
                        else
                        {
                            amoutCalculationIntraState.Visible = false;
                            amoutCalculationInterState.Visible = true;
                        }
                    }
                }
            }
            else
            {

            }
        }
        #endregion

        #region PDF GEN
        protected override void Render(HtmlTextWriter writer)
        {
            if (startConversion)
            {
                //generatePDF.Visible = false;
                //CreateBill.Visible = false;
                // get html of the page
                TextWriter myWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                base.Render(htmlWriter);

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                converter.Options.WebPageWidth = 1366;
                converter.Options.WebPageHeight = 768;

                // create a new pdf document converting the html string of the page
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(
                    myWriter.ToString(), Request.Url.AbsoluteUri);

                // save pdf document
                doc.Save(Response, false, "Sample.pdf");

                // close pdf document
                doc.Close();
               // generatePDF.Visible = true;
                //CreateBill.Visible = true;
            }
            else
            {
                // render web page in browser
                base.Render(writer);
            }
        }
        protected void btnCompanyLogoUpload_Click(object sender, EventArgs e)
        {
            if (companyName.Text != null && (compannyGstin.Text != null || companyPan.Value != null))
            {
                if (comapnyLogoUploadFile.PostedFile != null)
                {
                    string FileName = Path.GetFileName(comapnyLogoUploadFile.PostedFile.FileName);
                    string FolderPath = "Images/" + compannyGstin.Text;
                    string FilePath = Server.MapPath(FolderPath + Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                    if (!Directory.Exists(FilePath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(FilePath);
                    }
                    FilePath += "\\" + FileName;
                    companyLogoID.InnerText = FilePath;
                    //Save files to images folder
                    comapnyLogoUploadFile.SaveAs(FilePath);
                    Stream fs = comapnyLogoUploadFile.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    companyLogo.ImageUrl = "data:image/png;base64," + base64String;
                    comapnyLogoUploadFile.Visible = false;
                    comapnyLogoUploadFile.Visible = false;
                    panelLogo.Visible = false;
                    companyLogo.Visible = true;
                }
            }
        }
        #endregion

        #region PAGE LEVEL OPERATIONS
        protected void saveInvoice_ServerClick(object sender, EventArgs e)
        {
            //Get Customer Data
            Customer customer = new Customer();
            customer.Name = companyName.Text;
            customer.GSTIN = compannyGstin.Text;
            customer.PAN = companyPan.Value;
            customer.IsActive = true;
            customer.BillAddL1 = companyAddrLine1.Value;
            customer.BillAddL2 = companyAddrLine2.Value;
            customer.BillAddCityID = Convert.ToInt64(companyAddrCity.SelectedValue);
            customer.BillStateID = Convert.ToInt64(companyAddrState.SelectedValue);
            customer.CustomerLogoPath = companyLogoID.InnerText;

            //Get Client data
            Customer client = new Customer();
            client.Name = billToClientName.Value;
            client.GSTIN = billToClientGSTIN.Text;
            client.PAN = billToClientPAN.Value;
            client.IsActive = true;
            client.BillAddL1 = billToClientAddline1.Value;
            client.BillAddL2 = billToClientAddline2.Value;
            client.BillAddCityID = Convert.ToInt64(billToClientCityList.SelectedValue);
            client.BillStateID = Convert.ToInt64(billToClientStateList.SelectedValue);

            client.ShipAddL1 = shipToClientAddLine1.Value;
            client.ShipAddL2 = shipToClientAddLine2.Value;
            client.ShipAddCityID = Convert.ToInt64(shipToClientCityList.SelectedValue);
            client.ShipStateID = Convert.ToInt64(shipToClientStateList.SelectedValue);

            BillMaster billMaster = new BillMaster();
            billMaster.NotesForCustomer = notesForCustomer.Text;
            billMaster.TermsConditions = termsAndCondition.Text;

            List<ProductsMaster> listProducts = new List<ProductsMaster>();

            for (int i = 1; i <= 100; i++)
            {
                ProductsMaster productsMaster = new ProductsMaster();
            }

        }
        #endregion

        #region BILL CLIENT OPERATIONS
        /// <summary>
        /// On BILL to state selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void billToClientStateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedState = billToClientStateList.SelectedValue;
            BindCityToControl(selectedState, billToClientCityList);
        }
        protected void billToClientCityList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void billToClientGSTIN_TextChanged(object sender, EventArgs e)
        {
            BAL_Common bAL_Common = new BAL_Common();
            var textValue = billToClientGSTIN.Text.Substring(0, 2);
            int number;
            if (Int32.TryParse(textValue, out number))
            {
                if (number >= 10 || number <= 99)
                {
                    State state = bAL_Common.GetStateByID(textValue);
                    string id = Convert.ToString(state.ID);
                    billToClientStateList.ClearSelection();
                    billToClientStateList.SelectedValue = id;
                    BindCityToControl(id, billToClientCityList);
                    if (BAL_Common.StringHasCharacters(compannyGstin.Text))
                    {
                        var companyGstin = compannyGstin.Text.Substring(0, 2);
                        if (companyGstin == textValue)
                        {
                            amoutCalculationIntraState.Visible = true;
                            amoutCalculationInterState.Visible = false;
                        }
                        else
                        {
                            amoutCalculationIntraState.Visible = false;
                            amoutCalculationInterState.Visible = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region SHIP CLIENT OPERATION
        protected void shipToClientStateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedState = shipToClientStateList.SelectedValue;
            BindCityToControl(selectedState, shipToClientCityList);
        }
        protected void shipToClientCityList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkSameAsBillAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsBillAddress.Checked)
            {
                shipToClientName.Value = billToClientName.Value;
                shipToClientName.Disabled = true;
                shipToClientContactName.Value = billToClientContactName.Value;
                shipToClientContactName.Disabled = true;
                shipToClientAddLine1.Value = billToClientAddline1.Value;
                shipToClientAddLine1.Disabled = true;
                shipToClientAddLine2.Value = billToClientAddline2.Value;
                shipToClientAddLine2.Disabled = true;

                var stateID = billToClientStateList.SelectedValue;
                shipToClientStateList.SelectedValue = stateID;
                shipToClientStateList.Enabled = false;

                var cityID = billToClientCityList.SelectedValue;
                BindCityToControl(stateID.ToString(), shipToClientCityList);
                shipToClientCityList.ClearSelection();
                System.Web.UI.WebControls.ListItem selectedListItem = shipToClientCityList.Items.FindByValue(cityID);
                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
                //shipToClientCityList.SelectedItem = cityName;
                shipToClientCityList.Enabled = false;
            }
            else
            {
                shipToClientName.Disabled = false;
                shipToClientName.Value = null;
                shipToClientContactName.Disabled = false;
                shipToClientContactName.Value = null;
                shipToClientAddLine1.Disabled = false;
                shipToClientAddLine1.Value = null;
                shipToClientAddLine2.Disabled = false;
                shipToClientAddLine2.Value = null;
                shipToClientStateList.Enabled = true;
                shipToClientCityList.Enabled = true;
                shipToClientStateList.ClearSelection();
                shipToClientCityList.ClearSelection();
            }
        }
        #endregion

        #region CREATE ITEM MODAL 
        protected void existingListItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void AddItemToInvoice_ServerClick(object sender, EventArgs e)
        {
            BAL_Products bAL_Products = new BAL_Products();
            List<ProductsMaster> products = new List<ProductsMaster>();
            products = bAL_Products.GetAllProductList();
            //newItem.Controls.Add(new Literal { Text = htmlContent.ToString() });
        }
        #endregion


    }
}