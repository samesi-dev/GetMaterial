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
    public partial class ereport : Form
    {
        public ereport()
        {
            InitializeComponent();
        }

        private void ereport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'emplo.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.emplo.Employee);

            this.reportViewer1.RefreshReport();
        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.ShowDialog();
            this.Hide();
        }
    }
}
