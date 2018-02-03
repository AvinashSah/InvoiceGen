using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.DAL
{
    public class DAL_Customers
    {

        /// <summary>
        /// Returns List of all products
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllProductList()
        {
            List<Customer> listCustomer = new List<Customer>();
            using (var context = new InvoiceGenEntities())
            {
                listCustomer = (from a in context.Customers
                                where a.IsActive==true
                                select a).ToList();
            }
            return listCustomer;
        }
    }
}
