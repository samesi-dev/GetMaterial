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
    public partial class Catreport : Form
    {
        public Catreport()
        {
            InitializeComponent();
        }

        private void Catreport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cat.Product_Category' table. You can move, or remove it, as needed.
            this.product_CategoryTableAdapter.Fill(this.cat.Product_Category);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.ShowDialog();
            this.Hide();
        }
    }
}
