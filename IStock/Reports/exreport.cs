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
    public partial class exreport : Form
    {
        public exreport()
        {
            InitializeComponent();
        }

        private void exreport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'expense.Daily_Expense' table. You can move, or remove it, as needed.
            this.daily_ExpenseTableAdapter.Fill(this.expense.Daily_Expense);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Expense ex = new Expense();
            ex.ShowDialog();
            this.Hide();
        }
    }
}
