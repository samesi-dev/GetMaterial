using System;

namespace IStock.DLL
{
    public class PurchaseDetailsBLL
    {
        public int s_ord_id { get; set; }
        public int s_ord_invoiceno { get; set; }
        public int product_id { get; set; }
        public decimal product_price { get; set; }
        public decimal product_quantity { get; set; }
        public int sup_id { get; set; }
        public DateTime created { get; set; }
        public int added_by { get; set; }
        public decimal total { get; set; }
        public string location { get; set; }
        public string payment { get; set; }
        public string delivery { get; set; }
        public string status { get; set; }

    }
}