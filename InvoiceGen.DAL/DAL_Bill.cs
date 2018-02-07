using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Entities;

namespace InvoiceGen.DAL
{
    public class DAL_Bill
    {
        public long CreateNewBill(BillMaster billMaster)
        {
            using (var context = new InvoiceGenEntities())
            {
                context.BillMasters.Add(billMaster);
                context.SaveChanges();//this generates the Id for customer
                return billMaster.ID;
            }
        }
    }
}
