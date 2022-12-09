using System;
using System.Data;

namespace IStock.DLL
{
    public class TransactionBLL
    {
        public int c_ord_id { get; set; }
        public int c_ord_invoiceno { get; set; }
        public int cust_id { get; set; }
        public decimal grandtotal { get; set; }
        public decimal discount { get; set; }
        public DateTime created { get; set; }
        public DataTable TransactionDetails { get; set; }
    }
}