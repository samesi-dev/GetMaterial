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
    public partial class GLedger : Form
    {
        public GLedger()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        LedgerBLL c = new LedgerBLL();
        LedgerDAL dal = new LedgerDAL();

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(l_id as int )),0)+1 from ledger", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            id.Text = dt.Rows[0][0].ToString();
            accname.Focus();
            this.ActiveControl = accname;

        }

        private void clear()
        {

            accname.Text = "";
            acctype.Text = "";
            acccredit.Text = "";
            desc.Text = "";
       
            accdebit.Text = "";

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (id.Text != "" && accname.Text != "" && acctype.Text != "")
            {
                if ((acccredit.Text == ""))
                {
                    MessageBox.Show("Please Enter 0 for credit if you don't want to insert any other value!");
                }
                else
                {
                    if ((accdebit.Text == ""))
                    {
                        MessageBox.Show("Please Enter 0 for debit if you don't want to insert any other value!");
                    }
                    else
                    {

                        c.account_type = acctype.Text;
                        c.account_name = accname.Text;
                        c.description = desc.Text;
                        c.credit = Decimal.Parse(acccredit.Text);
                        c.debit = Decimal.Parse(accdebit.Text);
                    
                        c.date = DateTime.Now;

                        bool Success = dal.Insert(c);
                        //insertion into database
                        if (Success == true)
                        {
                            MessageBox.Show("Successfully Inserted!");
                            clear();
                        }

                        else
                        {
                            MessageBox.Show("Failed to insert!");
                        }
                        // Refreshing DataGrid View
                        DataTable dt = dal.Select();
                        TransactionGridView.DataSource = dt;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            c.l_id = Convert.ToInt32(id.Text);
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
           TransactionGridView.DataSource = dt;
        }

        private void TransactionGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id.Text = this.TransactionGridView.CurrentRow.Cells[0].Value.ToString();
            accname.Text = this.TransactionGridView.CurrentRow.Cells[2].Value.ToString();
            acctype.Text = this.TransactionGridView.CurrentRow.Cells[1].Value.ToString();
            accdebit.Text = this.TransactionGridView.CurrentRow.Cells[4].Value.ToString();
            acccredit.Text = this.TransactionGridView.CurrentRow.Cells[3].Value.ToString();
            desc.Text = this.TransactionGridView.CurrentRow.Cells[5].Value.ToString();
        }

        private void GLedger_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            TransactionGridView.DataSource = dt;

            TransactionGridView.Columns[0].HeaderText = "ID";
            TransactionGridView.Columns[1].HeaderText = "Account Type"; 
            TransactionGridView.Columns[2].HeaderText = "Account Name";
            TransactionGridView.Columns[3].HeaderText = "Credit";
            TransactionGridView.Columns[4].HeaderText = "Debit";
            TransactionGridView.Columns[5].HeaderText = "Description";
            TransactionGridView.Columns[6].HeaderText = "Date";
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {

            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
              TransactionGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
              TransactionGridView.DataSource = dt;
            }
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void acccredit_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Amount in Numbers Only!");
            }
        }

        private void accname_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            legreport l = new legreport();
            l.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
