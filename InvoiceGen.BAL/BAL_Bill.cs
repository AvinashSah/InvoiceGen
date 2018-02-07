using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.DAL;
using InvoiceGen.Entities;

namespace InvoiceGen.BAL
{
    public class BAL_Bill
    {
        public long CreateNewBill(BillMaster billMaster)
        {
            DAL_Bill dAL_Bill = new DAL_Bill();
            return dAL_Bill.CreateNewBill(billMaster);
        }
    }
}
