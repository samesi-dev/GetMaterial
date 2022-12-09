using IStock.UI_forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.Reports
{
    public partial class salorder : Form
    {
        public salorder()
        {
            InitializeComponent();
        }

        private void saleorder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'saleorder1.c_ord_pay' table. You can move, or remove it, as needed.
            this.c_ord_payTableAdapter.Fill(this.saleorder1.c_ord_pay);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
            this.Hide();
        }
    }
}
