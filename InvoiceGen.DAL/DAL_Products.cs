using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace InvoiceGen.DAL
{
    public class DAL_Products
    {
        /// <summary>
        /// Saves List of Products to Database
        /// </summary>
        /// <param name="listProduct"></param>
        public List<ProductsMaster> SaveProductsData(List<ProductsMaster> listProduct)
        {
            using (var context = new InvoiceGenEntities())
            {
                try
                {
                    foreach (var product in listProduct)
                    {
                        ProductsMaster prod = new ProductsMaster();
                        if (product.HSNCode != null)
                        {
                            prod = context.ProductsMasters.SqlQuery("Select * from ProductsMaster where HSNCode=@HSNCode", new SqlParameter("@HSNCode", product.HSNCode)).FirstOrDefault();
                        }
                        else if (product.SACCode != null)
                        {
                            prod = context.ProductsMasters.SqlQuery("Select * from ProductsMaster where SACCode=@SACCode", new SqlParameter("@SACCode", product.SACCode)).FirstOrDefault();
                        }

                        if (prod == null)
                        {
                            context.ProductsMasters.Add(product);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                return listProduct;
            }
        }

        /// <summary>
        /// Returns List of all products
        /// </summary>
        /// <returns></returns>
        public List<ProductsMaster> GetAllProductList()
        {
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            using (var context = new InvoiceGenEntities())
            {
                listProduct = (from a in context.ProductsMasters
                               where a.Name != null
                               select a).ToList();
            }
            return listProduct;
        }

        public List<ProductsMaster> GetProducListByHSNSACCode(string hSNPrefix)
        {
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            using (var context = new InvoiceGenEntities())
            {
                listProduct = (from a in context.ProductsMasters
                               where a.HSNCode.Contains(hSNPrefix) || a.SACCode.Contains(hSNPrefix)
                               select a).ToList();
            }
            return listProduct;
        }

        public List<ProductsMaster> GetProductListByProductName(string productName)
        {
            List<ProductsMaster> listProduct = new List<ProductsMaster>();
            using (var context = new InvoiceGenEntities())
            {
                listProduct = (from a in context.ProductsMasters
                               where a.Name.Contains(productName)
                               select a).ToList();
            }
            return listProduct;
        }

        public long CreateNewProduct(ProductsMaster products)
        {
            using (var context = new InvoiceGenEntities())
            {
                context.ProductsMasters.Add(products);
                context.SaveChanges();//this generates the Id for customer
                return products.ID;
            }
        }

        public bool CreateNewProductBillMapping(BillProductMapping productBillMapp)
        {
            using (var context = new InvoiceGenEntities())
            {
                context.BillProductMappings.Add(productBillMapp);
                context.SaveChanges();//this generates the Id for customer
            }
            if (productBillMapp.ID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfProductExistByHSNCode(string hSNCode, out long productID)
        {
            productID = -1;
            ProductsMaster productsMaster = new ProductsMaster();
            using (var context = new InvoiceGenEntities())
            {
                productsMaster = (from a in context.ProductsMasters
                                  where a.HSNCode == hSNCode
                                  select a).FirstOrDefault();
            }
            if (productsMaster != null)
            {
                productID = productsMaster.ID;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
