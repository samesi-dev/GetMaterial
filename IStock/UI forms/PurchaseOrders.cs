using IStock.DLL;
using IStock.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.UI_forms
{
    public partial class PurchaseOrders : Form
    {
        public PurchaseOrders()
        {
            InitializeComponent();
            Delivery();
            Status();
        }

        DataTable TransactionDT = new DataTable();
        PurchaseDetailsBLL s = new PurchaseDetailsBLL();
        PurchaseDetailDAL dal = new PurchaseDetailDAL();

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddPurchase add = new AddPurchase();
            add.ShowDialog();
            this.Hide();
        }

        private void clear()
        {
            oidtxt.Text = "";
            optxt.Text = "";
            optxt.Text = "";
            octtxt.Text = "";
            octtxt.Text = "";
            locationtxt.Text = "";
            odelivertxt.Text = "select";
            osttext.Text = "select";
            ototaltxt.Text = "";
            oqtxt.Text = "";
        }


        public void Delivery()
        {
            odelivertxt.Items.Add("Delivered");
            odelivertxt.Items.Add("Not Delivered");
        }

        public void Status()
        {
            osttext.Items.Add("Cancelled");
            osttext.Items.Add("Completed");
            osttext.Items.Add("Not Completed");
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            s.s_ord_id = Convert.ToInt32(oidtxt.Text);
            bool success = dal.Delete(s);

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
            osttext.Text = this.SalesDataGrid.CurrentRow.Cells[2].Value.ToString();
            octxt.Text = this.SalesDataGrid.CurrentRow.Cells[5].Value.ToString();
            octtxt.Text = this.SalesDataGrid.CurrentRow.Cells[4].Value.ToString();
            ototaltxt.Text = this.SalesDataGrid.CurrentRow.Cells[7].Value.ToString();
            oqtxt.Text = this.SalesDataGrid.CurrentRow.Cells[3].Value.ToString();
            odelivertxt.Text = this.SalesDataGrid.CurrentRow.Cells[10].Value.ToString();
            locationtxt.Text = this.SalesDataGrid.CurrentRow.Cells[8].Value.ToString();
            osttext.Text = this.SalesDataGrid.CurrentRow.Cells[9].Value.ToString();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;

            if (osttext.Text != "" && odelivertxt.Text != "")
            {
                cmd = new SqlCommand("UPDATE s_ord_pay  SET status=@status, delivery=@delivery  WHERE s_ord_id=@id", con);
                con.Open();

                cmd.Parameters.AddWithValue("@id", oidtxt.Text);

                if (String.IsNullOrEmpty(osttext.Text))
                {
                    cmd.Parameters.AddWithValue("@status", DBNull.Value);
                    MessageBox.Show("Please Select Order Status");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@status", osttext.Text);
                    if (String.IsNullOrEmpty(osttext.Text))
                    {
                        cmd.Parameters.AddWithValue("@delivery", DBNull.Value);
                        MessageBox.Show("Please Select Delivery Status");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@delivery", odelivertxt.Text);


                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(" Successfuly Updated");
                        clear();
                    }

                }
                DataTable dt = dal.Select();
                SalesDataGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Provide All Details");
            }
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

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            supporder s = new supporder();
            s.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}

