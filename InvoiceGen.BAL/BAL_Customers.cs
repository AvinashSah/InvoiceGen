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

        public bool CheckIfCustomerExistByGSTIN(string gSTIN, out Int64 customerID)
        {
            DAL_Customers dL_Customers = new DAL_Customers();
            return dL_Customers.CheckIfCustomerExistByGSTIN(gSTIN, out customerID);
        }

        public bool CheckIfCustomerExistByPAN(string pan, out long customerID)
        {
            DAL_Customers dL_Customers = new DAL_Customers();
            return dL_Customers.CheckIfCustomerExistByPAN(pan, out customerID);
        }

        public bool CheckIfCustomerExistByName(string name, out long customerID)
        {
            DAL_Customers dL_Customers = new DAL_Customers();
            return dL_Customers.CheckIfCustomerExistByName(name, out customerID);
        }

        public long CreateNewCustomer(Customer customer)
        {
            DAL_Customers dL_Customers = new DAL_Customers();
            return dL_Customers.CreateNewCustomer(customer);
        }
    }
}
