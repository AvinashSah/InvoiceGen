using InvoiceGen.DAL;
using InvoiceGen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.BAL
{
    public class BAL_Customers
    {
        public List<Customer> GetAllCustomers()
        {
            DAL_Customers dL_Customers = new DAL_Customers();
            return dL_Customers.GetAllProductList();
        }
    }
}
