using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IStock.UI_forms;

namespace IStock.Reports
{
    public partial class creport : Form
    {
        public creport()
        {
            InitializeComponent();
        }

        private void creport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'customers.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.customers.Customer);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
