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
                generatePDF.Visible = false;
                CreateBill.Visible = false;
                // get html of the page
                TextWriter myWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                base.Render(htmlWriter);

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                converter.Options.WebPageWidth = 1366;
                converter.Options.WebPageHeight = 768;

                // create a new pdf document converting the html string of the page
                PdfDocument doc = converter.ConvertHtmlString(
                    myWriter.ToString(), Request.Url.AbsoluteUri);

                // save pdf document
                doc.Save(Response, false, "Sample.pdf");

                // close pdf document
                doc.Close();
                generatePDF.Visible = true;
                CreateBill.Visible = true;
            }
            else
            {
                // render web page in browser
                base.Render(writer);
            }
        }
        protected void btnCompanyLogoUpload_Click(object sender, EventArgs e)
        {
            if (comapnyLogoUploadFile.PostedFile != null)
            {
                string FileName = Path.GetFileName(comapnyLogoUploadFile.PostedFile.FileName);
                //Save files to images folder
                comapnyLogoUploadFile.SaveAs(Server.MapPath("App_Data/Images/" + FileName));
                System.IO.Stream fs = comapnyLogoUploadFile.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                companyLogo.ImageUrl = "data:image/png;base64," + base64String;
                comapnyLogoUploadFile.Visible = false;
                comapnyLogoUploadFile.Visible = false;
                panelLogo.Visible = false;
                companyLogo.Visible = true;
            }
        }
        #endregion

        #region PAGE LEVEL OPERATIONS
        protected void saveInvoice_ServerClick(object sender, EventArgs e)
        {

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

        protected void shipToClientGSTIN_TextChanged(object sender, EventArgs e)
        {
            BAL_Common bAL_Common = new BAL_Common();
            var textValue = shipToClientGSTIN.Text.Substring(0, 2);
            int number;
            if (Int32.TryParse(textValue, out number))
            {
                if (number >= 10 || number <= 99)
                {
                    State state = bAL_Common.GetStateByID(textValue);
                    string id = Convert.ToString(state.ID);
                    shipToClientStateList.ClearSelection();
                    shipToClientStateList.SelectedValue = id;
                    BindCityToControl(id, shipToClientCityList);
                }
            }
            else
            {

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