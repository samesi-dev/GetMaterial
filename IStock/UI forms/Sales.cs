using IStock.BLL;
using IStock.DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.UI_forms
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
        }
        DataTable TransactionDT = new DataTable();
        TransactionDetailsBLL c = new TransactionDetailsBLL();
        TransactionDetailsDAL dal = new TransactionDetailsDAL();

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddSale add = new AddSale();
            add.ShowDialog();
            this.Hide();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            SalesDataGrid.DataSource = dt;

            SalesDataGrid.Columns[0].HeaderText = "Order ID";
            SalesDataGrid.Columns[1].HeaderText = "Product";
            SalesDataGrid.Columns[2].HeaderText = "Product Price";
            SalesDataGrid.Columns[3].HeaderText = "Quantity";
            SalesDataGrid.Columns[4].HeaderText = "Customer";
            SalesDataGrid.Columns[7].HeaderText = "Total";
            SalesDataGrid.Columns[5].HeaderText = "Created";
            SalesDataGrid.Columns[6].HeaderText = "Added by";


        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                SalesDataGrid.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                SalesDataGrid.DataSource = dt;
            }
        }
        private void clear()
        {

           oidtxt.Text = "";
            optxt.Text = "";
            oprtxt.Text = "";
            ototaltxt.Text = "";
            octxt.Text = "";
            oqtxt.Text = "";
            octtxt.Text = "";

        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {

           c.c_ord_id = Convert.ToInt32(oidtxt.Text);
            bool success = dal.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Deleted Successfully!");
                clear();
            }

            else
            {
                MessageBox.Show("Failed to delete!");
            }
            DataTable dt = dal.Select();
            SalesDataGrid.DataSource = dt;

        }

        private void SalesDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oidtxt.Text = this.SalesDataGrid.CurrentRow.Cells[0].Value.ToString();
            optxt.Text = this.SalesDataGrid.CurrentRow.Cells[1].Value.ToString();
            oprtxt.Text = this.SalesDataGrid.CurrentRow.Cells[2].Value.ToString();
            octxt.Text = this.SalesDataGrid.CurrentRow.Cells[5].Value.ToString();
            octtxt.Text = this.SalesDataGrid.CurrentRow.Cells[4].Value.ToString();
            ototaltxt.Text = this.SalesDataGrid.CurrentRow.Cells[7].Value.ToString();
            oqtxt.Text = this.SalesDataGrid.CurrentRow.Cells[3].Value.ToString();
        }

        private void oidtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void oqtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void octtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void optxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void Expensesbtn_Click(object sender, EventArgs e)
        {
            Expense ex = new Expense();
            ex.ShowDialog();
            this.Hide();
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            customers c = new customers();
            c.ShowDialog();
            this.Hide();
        }

        private void EmployeeBtn_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.ShowDialog();
            this.Hide();
        }

        private void GledgerBtn_Click(object sender, EventArgs e)
        {
            GLedger g = new GLedger();
            g.ShowDialog();
            this.Hide();
        }

        private void StockBtn_Click(object sender, EventArgs e)
        {
            Stock s = new Stock();
            s.ShowDialog();
            this.Hide();
        }

        private void PurchaseBtn_Click(object sender, EventArgs e)
        {
            Purchase p = new Purchase();
            p.ShowDialog();
            this.Hide();
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            Sales s = new Sales();
            s.ShowDialog();
            this.Hide();
        }

        private void OrdersBtn_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
            this.Hide();
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {

            Supplier s = new Supplier();
            s.ShowDialog();
            this.Hide();
        }

        private void CategoriesBtn_Click(object sender, EventArgs e)
        {

            Category c = new Category();
            c.ShowDialog();
            this.Hide();
        }

        private void Productsbtn_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
