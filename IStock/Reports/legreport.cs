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
    public partial class legreport : Form
    {
        public legreport()
        {
            InitializeComponent();
        }

        private void legreport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ledger._ledger' table. You can move, or remove it, as needed.
            this.ledgerTableAdapter.Fill(this.ledger._ledger);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            GLedger l = new GLedger();
            l.ShowDialog();
            this.Hide();
        }
    }
}
