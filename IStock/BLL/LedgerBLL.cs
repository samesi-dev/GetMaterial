using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class LedgerBLL
    {
        public int l_id { get; set; }
        public string account_name { get; set; }
        public string account_type { get; set; }
        public DateTime date { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public string description { get; set; }
    }
}
