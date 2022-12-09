using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class ExpenseBLL
    {
        public int expense_id { get; set; }
        public string expense_type { get; set; }
        public string description { get; set; }
        public Decimal expense_amount { get; set; }
        public int userid { get; set; }
        public DateTime created { get; set; }
    }
}
