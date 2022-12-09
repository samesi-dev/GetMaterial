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
    public partial class Stockreport : Form
    {
        public Stockreport()
        {
            InitializeComponent();
        }

        private void Stockreport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'st.Stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.st.Stock);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Stock s = new Stock();
            s.ShowDialog();
            this.Hide();
        }
    }
}
