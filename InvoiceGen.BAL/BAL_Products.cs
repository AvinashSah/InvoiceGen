using InvoiceGen.DAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace InvoiceGen.BAL
{
    public class BAL_Products
    {
        /// <summary>
        /// Saves the products data to DB
        /// </summary>
        /// <param name="dt"></param>
        public List<Product> SaveProductsData(DataTable dt, Requester requester)
        {
            DAL_Products dAL_Products = new DAL_Products();
            List<Product> listProduct = new List<Product>();
            if (dt != null & dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Product product = new Product();
                    product.Name = Convert.ToString(row["Name"]);
                    product.HSNCode = Convert.ToString(row["HSNCode"] is DBNull ? DBNull.Value : row["HSNCode"]);
                    product.SACCode = Convert.ToString(row["SACCode"] is DBNull ? DBNull.Value : row["SACCode"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.UoM = Convert.ToString(row["UoM"]);
                    product.PurchaseRate = Convert.ToString(row["PurchaseRate"]);
                    product.SalesRate = Convert.ToString(row["SalesRate"]);
                    product.CessPercentage = Convert.ToString(row["CessPercentage"]);
                    product.GSTPercentage = Convert.ToString(row["GSTPercentage"]);
                    product.IsActive = true;
                    product.CreatedBy = requester.ID;
                    product.CreatedOn = DateTime.Now;
                    product.UpdatedBy = requester.ID;
                    product.UpdatedOn = DateTime.Now;
                    listProduct.Add(product);
                }
                return dAL_Products.SaveProductsData(listProduct);
            }
            return listProduct;
        }

        public List<Product> GetAllProductList()
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.GetAllProductList();
        }
    }
}

