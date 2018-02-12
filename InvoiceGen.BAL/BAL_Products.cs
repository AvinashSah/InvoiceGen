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
        public List<ProductsMaster> SaveProductsData(DataTable dt, Requester requester)
        {
            DAL_Products dAL_Products = new DAL_Products();
            DAL_Common dAL_Common = new DAL_Common();
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            UserMaster userMaster = new UserMaster();

            userMaster = dAL_Common.GetUserdetailsByUsername(requester.Name);
            if (dt != null & dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    ProductsMaster product = new ProductsMaster();
                    product.Name = Convert.ToString(row["Name"]);
                    product.HSNCode = Convert.ToString(row["HSNCode"] is DBNull ? DBNull.Value : row["HSNCode"]);
                    product.SACCode = Convert.ToString(row["SACCode"] is DBNull ? DBNull.Value : row["SACCode"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.CessPercentage = Convert.ToString(row["CessPercentage"]);
                    product.GSTPercentage = Convert.ToString(row["GSTPercentage"]).Equals("NIL", StringComparison.InvariantCultureIgnoreCase) ? null : Convert.ToString(row["GSTPercentage"]);
                    product.IsActive = true;
                    product.CreatedOn = DateTime.Now;
                    product.UpdatedOn = DateTime.Now;
                    if (userMaster != null)
                    {
                        product.CreatedBy = userMaster.ID;
                        product.UpdatedBy = userMaster.ID;
                    }
                    else
                    {
                        product.CreatedBy = 1;
                        product.UpdatedBy = 1;
                    }
                    listProduct.Add(product);
                }
                return dAL_Products.SaveProductsData(listProduct);
            }
            return listProduct;
        }

        public List<ProductsMaster> GetAllProductList()
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.GetAllProductList();
        }

        public List<ProductsMaster> GetProducListByHSNSACCode(string hSNPrefix)
        {
            DAL_Products dAL_Products = new DAL_Products();
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            listProduct = dAL_Products.GetProducListByHSNSACCode(hSNPrefix);
            return listProduct;
        }

        public List<ProductsMaster> GetProductListByProductName(string hSNPrefix)
        {
            DAL_Products dAL_Products = new DAL_Products();
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            listProduct = dAL_Products.GetProductListByProductName(hSNPrefix);
            return listProduct;
        }

        public bool CheckIfProductExistByHSNCode(string hSNCode, out long productID)
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.CheckIfProductExistByHSNCode(hSNCode, out productID);
        }

        public bool CheckIfProductExistBySACCode(string sACCode, out long productID)
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.CheckIfProductExistBySACode(sACCode, out productID);
        }

        public long CreateNewProduct(ProductsMaster products)
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.CreateNewProduct(products);
        }

        public bool CreateNewProductBillMapping(BillProductMapping productBillMapp)
        {
            DAL_Products dAL_Products = new DAL_Products();
            return dAL_Products.CreateNewProductBillMapping(productBillMapp);
        }
    }
}

