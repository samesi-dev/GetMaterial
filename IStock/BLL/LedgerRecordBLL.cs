using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class LedgerRecordBLL
    {
        public int r_id { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public int l_id { get; set; }
        public DateTime created { get; set; }
    }
}
