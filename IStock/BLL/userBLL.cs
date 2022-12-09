using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStock.BLL
{
    class userBLL
    {

        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public int created_by { get; set; }
        public DateTime created { get; set; }
    }
}
