using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Entities
{
    public class BillMaster
    {
        public int BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillDueDat { get; set; }
        public Customer FromCustomer { get; set; }
        public Customer ToCustomer { get; set; }
        public Address ShippAddress { get; set; }
        public Address BillAddress { get; set; }
    }

    public class BillProductMapp
    {
        public int BillNo { get; set; }
        public int ProductID { get; set; }
        public Products Products { get; set; }
        public string rate { get; set; }
        public int Qty { get; set; }
        public float Discount { get; set; }
        public float CGST { get; set; }
        public float SGST { get; set; }
        public float IGST { get; set; }
    }
}
