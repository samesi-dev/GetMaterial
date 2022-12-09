using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class ProBLL
    {
        public int product_id { get; set; }
        public string productName { get; set; }
        public string Description { get; set; }
        public string units { get; set; }
        public int pro_quantity { get; set; }
        public int proCat { get; set; }
        public decimal pro_unitprice { get; set; }
        public int Sup_id { get; set; }
        public DateTime created { get; set; }
    }
}
