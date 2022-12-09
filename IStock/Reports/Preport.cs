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
    public partial class Preport : Form
    {
        public Preport()
        {
            InitializeComponent();
        }

        private void Preport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'inventoryDataSet2.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.inventoryDataSet2.Product);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void SystemName_Click(object sender, EventArgs e)
        {

        }

        private void productBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
