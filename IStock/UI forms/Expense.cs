using IStock.BLL;
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
    public partial class Expense : Form
    {
        public Expense()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        userBLL usr = new userBLL();
        ExpenseBLL c = new ExpenseBLL();
        ExpenseDAL dal = new ExpenseDAL();

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(expense_id as int )),0)+1 from Daily_Expense", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ExIDtxt.Text = dt.Rows[0][0].ToString();
            ExTypetxt.Focus();
            this.ActiveControl = ExTypetxt;

        }

        private void clear()
        {

            ExAmounttxt.Text = "";
            ExTypetxt.Text = "";
       
            ExDesctxt.Text = "";
         
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ExIDtxt.Text != "" && ExTypetxt.Text != "" && ExAmounttxt.Text != "")
            {
                c.expense_type = ExTypetxt.Text;
                c.expense_amount = int.Parse(ExAmounttxt.Text);
                c.description = ExDesctxt.Text;
                //getting username of the logged in user
                string UserLoggedInName = Login.LoggedIn;
                 userBLL usr = dal.GetIDFromUsername(UserLoggedInName);
                 c.userid = usr.userid;
                c.created = DateTime.Now;

                bool Success = dal.Insert(c);
                //insertion into database
                if (Success == true)
                {
                    MessageBox.Show("Successfully Inserted!");
                    clear();
                }

                else
                {
                    MessageBox.Show("Failed to insert Expense!");
                }
                // Refreshing DataGrid View
                DataTable dt = dal.Select();
                ExpenseGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (ExIDtxt.Text != "" && ExTypetxt.Text != "" && ExAmounttxt.Text != "")
            {
                c.expense_type = ExTypetxt.Text;
                c.expense_amount = decimal.Parse(ExAmounttxt.Text);
                c.description = ExDesctxt.Text;
                c.expense_id = int.Parse(ExIDtxt.Text);
                string UserLoggedInName = Login.LoggedIn;
                 userBLL usr = dal.GetIDFromUsername(UserLoggedInName);
                c.userid = usr.userid;


                bool Success = dal.update(c); //insertion into database 
                if (Success == true)
                {
                    MessageBox.Show("Successfully Updated!");
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update !");
                }
                // Refreshing DataGrid View
                DataTable dt = dal.Select();
                ExpenseGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information !");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            c.expense_id = Convert.ToInt32(ExIDtxt.Text);
            bool success = dal.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Deleted Successfully!");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to delete the user!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            ExpenseGrid.DataSource = dt;
        }

        private void ExIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void ExAmounttxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Amount in Numbers Only!");
            }
        }

        private void ExTypetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void CategoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ExIDtxt.Text = this.ExpenseGrid.CurrentRow.Cells[0].Value.ToString();
            ExTypetxt.Text = this.ExpenseGrid.CurrentRow.Cells[1].Value.ToString();
            ExDesctxt.Text = this.ExpenseGrid.CurrentRow.Cells[2].Value.ToString();
            ExAmounttxt.Text = this.ExpenseGrid.CurrentRow.Cells[3].Value.ToString();
       
        }

        private void Expense_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            ExpenseGrid.DataSource = dt;

            ExpenseGrid.Columns[0].HeaderText = "ID";
            ExpenseGrid.Columns[1].HeaderText = "Type";
            ExpenseGrid.Columns[2].HeaderText = "Description";
            ExpenseGrid.Columns[3].HeaderText = "Amount";
            ExpenseGrid.Columns[4].HeaderText = "Added by";
            ExpenseGrid.Columns[5].HeaderText = "Created";
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {

            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                ExpenseGrid.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                ExpenseGrid.DataSource = dt;
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

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            exreport ex = new exreport();
            ex.ShowDialog();
            this.Hide();
        }

        private void Productsbtn_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
            this.Hide();
        }

        private void CategoriesBtn_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.ShowDialog();
            this.Hide();
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier();
            s.ShowDialog();
            this.Hide();
        }

        private void OrdersBtn_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
            this.Hide();
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            Sales s = new Sales();
            s.ShowDialog();
            this.Hide();
        }

        private void PurchaseBtn_Click(object sender, EventArgs e)
        {
            Purchase p = new Purchase();
            p.ShowDialog();
            this.Hide();
        }

        private void StockBtn_Click(object sender, EventArgs e)
        {
            Stock s = new Stock();
            s.ShowDialog();
            this.Hide();
        }

        private void GledgerBtn_Click(object sender, EventArgs e)
        {
            GLedger g = new GLedger();
            g.ShowDialog();
            this.Hide();
        }

        private void EmployeeBtn_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.ShowDialog();
            this.Hide();
        }
    }
}
