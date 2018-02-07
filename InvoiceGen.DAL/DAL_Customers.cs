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
                                where a.IsActive == true
                                select a).ToList();
            }
            return listCustomer;
        }

        /// <summary>
        /// Check if customer exist by GSTIN
        /// </summary>
        /// <param name="gSTIN"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool CheckIfCustomerExistByGSTIN(string gSTIN, out Int64 customerID)
        {
            customerID = -1;
            Customer customer = new Customer();
            using (var context = new InvoiceGenEntities())
            {
                customer = (from a in context.Customers
                            where a.IsActive == true && a.GSTIN == gSTIN
                            select a).FirstOrDefault();
            }
            if (customer == null)
            {
                return false;
            }
            else
            {
                customerID = customer.ID;
                return true;
            }
        }

        public long SaveCustomerProductMapping(CustomerProductMapping customerProductMapping)
        {
            using (var context = new InvoiceGenEntities())
            {

                context.CustomerProductMappings.Add(customerProductMapping);
                context.SaveChanges();//this generates the Id for customer
                return customerProductMapping.ID;
            }
        }

        public long CreateNewCustomer(Customer customer)
        {
            using (var context = new InvoiceGenEntities())
            {
                context.Customers.Add(customer);
                context.SaveChanges();//this generates the Id for customer
                return customer.ID;
            }
        }

        public bool CheckIfCustomerExistByName(string name, out long customerID)
        {
            customerID = -1;
            Customer customer = new Customer();
            using (var context = new InvoiceGenEntities())
            {
                customer = (from a in context.Customers
                            where a.IsActive == true && a.Name == name
                            select a).FirstOrDefault();
            }
            if (customer == null)
            {
                return false;
            }
            else
            {
                customerID = customer.ID;
                return true;
            }
        }

        /// <summary>
        /// check if customer exist by PAN
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool CheckIfCustomerExistByPAN(string pan, out long customerID)
        {
            customerID = -1;
            Customer customer = new Customer();
            using (var context = new InvoiceGenEntities())
            {
                customer = (from a in context.Customers
                            where a.IsActive == true && a.PAN == pan
                            select a).FirstOrDefault();
            }
            if (customer == null)
            {
                return false;
            }
            else
            {
                customerID = customer.ID;
                return true;
            }
        }
    }
}
