using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class EmployeeBLL
    {
        public int e_id { get; set; }
        public string e_name { get; set; }
        public string e_contactno { get; set; }
        public string e_Whatsappno { get; set; }
        public string e_email { get; set; }
        public string e_address { get; set; }
        public string e_cnic { get; set; }
        public decimal e_salary { get; set; }
        public string e_designation { get; set; }
        public DateTime created { get; set; }
    }
}
