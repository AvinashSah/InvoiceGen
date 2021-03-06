﻿using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InvoiceGen
{
    public partial class ImportData : System.Web.UI.Page
    {
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

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

        protected void productsDataFileButton_Click(object sender, EventArgs e)
        {
            if (productsDataFile.HasFile)
                try
                {

                    string FileName = Path.GetFileName(productsDataFile.PostedFile.FileName);
                    string Extension = Path.GetExtension(productsDataFile.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["productsDataFilePath"];

                    if (Extension == ".xlsx" || Extension == ".xls")
                    {
                        string FilePath = Server.MapPath(FolderPath + Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                        if (!Directory.Exists(FilePath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(FilePath);
                        }
                        FilePath += "\\" + FileName;
                        productsDataFile.SaveAs(FilePath);
                        productsDataFileLabel.Text = "File name: " +
                             productsDataFile.PostedFile.FileName + "<br>" +
                             productsDataFile.PostedFile.ContentLength + " kb<br>" +
                             "Content type: " +
                             productsDataFile.PostedFile.ContentType;
                        productsDataFileLabel.Text = "File Uploaded successfully";

                        Requester requester = new Requester();
                        requester.Name = Convert.ToString(HttpContext.Current.User.Identity.Name);

                        List<object> arguments = new List<object>();
                        arguments.Add(FilePath);
                        arguments.Add(Extension);
                        arguments.Add(requester);


                        this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                        this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
                        this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
                        this.backgroundWorker1.RunWorkerAsync(arguments);

                        productsDataFileLabel.Text = "Product Uploaded successfully";
                    }
                    else
                    {
                        throw new Exception("Invalid File Type !");
                    }
                }
                catch (Exception ex)
                {
                    productsDataFileLabel.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                productsDataFileLabel.Text = "You have not specified a file.";
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BAL.BAL_Products bAL_Products = new BAL.BAL_Products();
            List<ProductsMaster> productList = new List<ProductsMaster>();
            productList = bAL_Products.GetAllProductList();
            BindDatatoTable(productList);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> genericlist = e.Argument as List<object>;
            Requester requester = new Requester();
            requester = (Requester)genericlist[2];
            UploadProducts(requester, genericlist[0].ToString(), genericlist[1].ToString());
        }

        /// <summary>
        /// Upload Product Information to DB
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="isHDR"></param>
        /// <returns></returns>
        private bool UploadProducts(Requester requester, string FilePath, string Extension, string isHDR = "Yes")
        {

            string conStr = "";
            switch (Extension)

            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            List<ProductsMaster> productList = new List<ProductsMaster>();
            if (dt != null && dt.Rows.Count > 0)
            {
                productList = SaveProductsData(dt, requester);
                connExcel.Close();
                return true;
            }
            else
            {
                throw new Exception("No data found in excel to upload !");
            }
        }



        private void BindDatatoTable(List<ProductsMaster> productList)
        {
            string finalstring = "";
            foreach (ProductsMaster product in productList)
            {
                if (product.ID > 0)
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
                else
                {
                    continue;
                }
            }
            uploadedProductsTbody.Controls.Add(new Literal { Text = finalstring.ToString() });
        }

        /// <summary>
        /// saves product data to Products table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<ProductsMaster> SaveProductsData(DataTable dt, Requester requester)
        {
            BAL.BAL_Products bAL_Products = new BAL.BAL_Products();
            return bAL_Products.SaveProductsData(dt, requester);
        }
    }
}