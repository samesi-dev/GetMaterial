using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class StockBLL
    {
        public int st_id { get; set; }
        public string product_name { get; set; }
        public int st_quantity { get; set; }
        public string sku { get; set; }
        public decimal st_price { get; set; }
        public int stockin_by { get; set; }
        public int pro_cat { get; set; }
        public string remaining_payment { get; set; }
        public string unit { get; set; }
        public int sup_id { get; set; }
        public DateTime created { get; set; }
    }
}
