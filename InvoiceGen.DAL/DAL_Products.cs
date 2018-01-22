using InvoiceGen.Entities;
using System;
using System.Collections.Generic;

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
                        context.Products.Add(product);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {

                }
                return listProduct;
            }
        }
    }
}
