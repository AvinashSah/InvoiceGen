using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGen.Entities
{
    public class Products
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HSNCode { get; set; }
        public string SACCode { get; set; }
        public string UoM { get; set; }
        public string PurchaseRate { get; set; }
        public string SaleRate { get; set; }
        public string GSTPercentage { get; set; }
    }
}

