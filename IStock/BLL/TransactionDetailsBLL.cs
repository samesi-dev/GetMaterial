using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class TransactionDetailsBLL
    {
        public int c_ord_id { get; set; }
        public int c_ord_invoiceno { get; set; }
        public int product_id { get; set; }
        public decimal product_price { get; set; }
        public decimal product_quantity { get; set; }
        public int cust_id { get; set; }
        public DateTime created { get; set; }
        public int added_by { get; set; }
        public decimal total { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public string delivery { get; set; }
    }
}
