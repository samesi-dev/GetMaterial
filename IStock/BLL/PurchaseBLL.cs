using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class PurchaseBLL
    {
        public int s_ord_id { get; set; }
        public int s_ord_invoiceno { get; set; }
        public int sup_id { get; set; } 
        public decimal grandtotal { get; set; }
        public decimal discount { get; set; }
        public DateTime created { get; set; } 
        public DataTable PurchaseDetails { get; set; }
    }
}
