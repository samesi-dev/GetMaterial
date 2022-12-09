using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class CustomerBLL
    {
        public int cust_id { get; set; }
        public string cust_name { get; set; }
        public string cust_contactno { get; set; }
        public string cust_Whatsappno { get; set; }
        public string cust_email { get; set; }
        public string cust_address { get; set; }
        public DateTime created { get; set; }
    }
}
