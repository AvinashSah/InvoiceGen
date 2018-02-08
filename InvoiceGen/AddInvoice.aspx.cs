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
using System.Web.Security;

namespace InvoiceGen
{
    public partial class AddInvoice : System.Web.UI.Page
    {
        private bool startConversion = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        #region COMMON METHODS
        protected void generateInvoicePDF_ServerClick(object sender, EventArgs e)
        {
            startConversion = true;
        }
        #endregion



        #region UPLOAD LOGO
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    if (startConversion)
        //    {
        //        //generatePDF.Visible = false;
        //        //CreateBill.Visible = false;
        //        // get html of the page
        //        TextWriter myWriter = new StringWriter();
        //        HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
        //        base.Render(htmlWriter);

        //        // instantiate a html to pdf converter object
        //        HtmlToPdf converter = new HtmlToPdf();

        //        converter.Options.WebPageWidth = 1366;
        //        converter.Options.WebPageHeight = 768;

        //        // create a new pdf document converting the html string of the page
        //        SelectPdf.PdfDocument doc = converter.ConvertHtmlString(
        //            myWriter.ToString(), Request.Url.AbsoluteUri);

        //        // save pdf document
        //        doc.Save(Response, false, "Sample.pdf");

        //        // close pdf document
        //        doc.Close();
        //        // generatePDF.Visible = true;
        //        //CreateBill.Visible = true;
        //    }
        //    else
        //    {
        //        // render web page in browser
        //        base.Render(writer);
        //    }
        //}
        protected void btnCompanyLogoUpload_Click(object sender, EventArgs e)
        {
            if (companyName.Value != null && (compannyGstin.Value != null || companyPan.Value != null))
            {
                if (comapnyLogoUploadFile.PostedFile != null)
                {
                    string FileName = Path.GetFileName(comapnyLogoUploadFile.PostedFile.FileName);
                    string FolderPath = null;
                    if (string.IsNullOrEmpty(compannyGstin.Value))
                    {
                        FolderPath = "Images/" + companyPan.Value;
                    }
                    else if (string.IsNullOrEmpty(companyPan.Value))
                    {
                        FolderPath = "Images/" + compannyGstin.Value;
                    }
                    else
                    {
                        FolderPath = "Images/" + companyName.Value;
                    }
                    FolderPath += FolderPath + Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    string FilePath = Server.MapPath(FolderPath);
                    if (!Directory.Exists(FilePath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(FilePath);
                    }
                    FilePath += "\\" + FileName;
                    companyLogoID.Value = FolderPath;
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


    }
}