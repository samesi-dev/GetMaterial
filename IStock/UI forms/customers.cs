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
    public partial class customers : Form
    {
        public customers()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        CustomerBLL c = new CustomerBLL();
        CustomerDAL dal = new CustomerDAL();


        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(cust_id as int )),0)+1 from Customer", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustIDtxt.Text = dt.Rows[0][0].ToString();
            CustNametxt.Focus();
            this.ActiveControl = CustNametxt;

        }


        private void clear()
        {

            CustContacttxt.Text = "";
            CustEmailtxt.Text = "";
            addresstxt.Text = "";
            CustWhatstxt.Text = "";
            CustNametxt.Text = "";
        }

        bool IsValidEmail(string Emailtxt)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(Emailtxt);
                return addr.Address == Emailtxt;
            }
            catch
            {
                return false;
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (CustIDtxt.Text != "" && CustNametxt.Text != "" && CustContacttxt.Text!="")
            {
                c.cust_name = CustNametxt.Text;
                c.cust_contactno = CustContacttxt.Text;
                c.cust_Whatsappno = CustWhatstxt.Text;
                c.cust_email = CustEmailtxt.Text;
                c.cust_address = addresstxt.Text;
                c.created = DateTime.Now;

                bool check = IsValidEmail(c.cust_email); //check if email is valid
                if (check == true)
                {
                    bool Success = dal.Insert(c); //insertion into database
                    if (Success == true)
                    {
                        MessageBox.Show("Successfully Inserted!");
                        clear();
                    }

                    else
                    {
                        MessageBox.Show("Failed to add new Customer!");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Email in valid format e.g. abc@gmail.com");
                }
                // Refreshing DataGrid View
                DataTable dt = dal.Select();
                CustomerGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void CustNametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void CustIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void CustEmailtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
        }

        private void CustWhatstxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
            if (CustWhatstxt.Text.Length == 11)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustContacttxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
            if (CustContacttxt.Text.Length == 11)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (CustIDtxt.Text != "" && CustNametxt.Text != "")
            {
                c.cust_name = CustNametxt.Text;
                c.cust_contactno = CustContacttxt.Text;
                c.cust_Whatsappno = CustWhatstxt.Text;
                c.cust_email = CustEmailtxt.Text;
                c.cust_address =addresstxt.Text;
                c.cust_id = int.Parse(CustIDtxt.Text);

                bool check = IsValidEmail(c.cust_email); //check if email is valid
                if (check == true)
                {
                    bool Success = dal.update(c); //insertion into database
                    if (Success == true)
                    {
                        MessageBox.Show("Successfully Updated!");
                        clear();
                    }

                    else
                    {
                        MessageBox.Show("Failed to Update!");
                    }
                    // Refreshing DataGrid View
                    DataTable dt = dal.Select();
                    CustomerGridView.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please Enter Email in valid format e.g. abc@gmail.com");
                }
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            c.cust_id = Convert.ToInt32(CustIDtxt.Text);
            bool success = dal.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Deleted Successfully!");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to delete the customer!");
            }
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            CustomerGridView.DataSource = dt;
        }

        private void CustomerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustIDtxt.Text = this.CustomerGridView.CurrentRow.Cells[0].Value.ToString();
            CustNametxt.Text = this.CustomerGridView.CurrentRow.Cells[1].Value.ToString();
            addresstxt.Text = this.CustomerGridView.CurrentRow.Cells[5].Value.ToString();
            CustContacttxt.Text = this.CustomerGridView.CurrentRow.Cells[2].Value.ToString();
            CustWhatstxt.Text = this.CustomerGridView.CurrentRow.Cells[3].Value.ToString();
            CustEmailtxt.Text = this.CustomerGridView.CurrentRow.Cells[4].Value.ToString();
        }

        private void customers_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            CustomerGridView.DataSource = dt;

            CustomerGridView.Columns[0].HeaderText = "ID";
            CustomerGridView.Columns[1].HeaderText = "Name";
            CustomerGridView.Columns[5].HeaderText = "Address";
            CustomerGridView.Columns[2].HeaderText = "Contact No.";
            CustomerGridView.Columns[3].HeaderText = "WhatsApp No.";
            CustomerGridView.Columns[4].HeaderText = "Email";
            CustomerGridView.Columns[6].HeaderText = "Created";
        }

        private void SearchUserTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                CustomerGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                CustomerGridView.DataSource = dt;
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            customers c = new customers();
            c.ShowDialog();
            this.Hide();
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                CustomerGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                CustomerGridView.DataSource = dt;
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            creport c = new creport();
            c.ShowDialog();
            this.Hide();
        }

        private void Expensesbtn_Click(object sender, EventArgs e)
        {
            Expense ex = new Expense();
            ex.ShowDialog();
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
