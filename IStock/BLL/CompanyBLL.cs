using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class CompanyBLL
    {
        public int Sup_id { get; set; }
        public string Sup_name { get; set; }
        public string Org_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string Mobileno { get; set; }
        public string landlineno { get; set; }
        public string email { get; set; }
        public DateTime created { get; set; }
    }
}
