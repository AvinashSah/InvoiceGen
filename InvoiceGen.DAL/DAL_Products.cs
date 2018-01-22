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
        public List<Product> SaveProductsData(List<Product> listProduct)
        {
            using (var context = new InvoiceGenEntities())
            {
                try
                {
                    foreach (var product in listProduct)
                    {
                        Product prod = new Product();
                        if (product.HSNCode != null)
                        {
                            prod = context.Products.SqlQuery("Select * from Products where HSNCode=@HSNCode", new SqlParameter("@HSNCode", product.HSNCode)).FirstOrDefault();
                        }
                        else if (product.SACCode != null)
                        {
                            prod = context.Products.SqlQuery("Select * from Products where SACCode=@SACCode", new SqlParameter("@SACCode", product.SACCode)).FirstOrDefault();
                        }

                        if (prod == null)
                        {
                            context.Products.Add(product);
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
        public List<Product> GetAllProductList()
        {
            List<Product> listProduct = new List<Product>();
            using (var context = new InvoiceGenEntities())
            {
                listProduct = (from a in context.Products
                               where a.Name != null
                               select a).ToList();
            }
            return listProduct;
        }
    }
}
