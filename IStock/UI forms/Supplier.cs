using IStock.BLL;
using IStock.DLL;
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
    public partial class Supplier : Form
    {
        public Supplier()
        {
            InitializeComponent();
            Autoprimarykey();
        }
        CompanyBLL c = new CompanyBLL();
        CompanyDAL dal = new CompanyDAL();



        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(Sup_id as int )),0)+1 from Supplier", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            compIDtxt.Text = dt.Rows[0][0].ToString();
            OwnNametxt.Focus();
            this.ActiveControl = OwnNametxt;

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
        private void guna2ShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void clear()
        {
         
            OwnNametxt.Text = "";
            citytxt.Text = "";
            emailtxt.Text = "";
            countrytxt.Text = "";
            addresstxt.Text = "";
            mobiletxt.Text = "";
           landlinetxt.Text = "";
            orgnametxt.Text="";
        }
        private void Supplier_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            CompanyDataGrid.DataSource = dt;

            CompanyDataGrid.Columns[0].HeaderText = "ID";
            CompanyDataGrid.Columns[1].HeaderText = "Owner Name";
            CompanyDataGrid.Columns[2].HeaderText = "Company Name";
            CompanyDataGrid.Columns[3].HeaderText = "Address";
            CompanyDataGrid.Columns[4].HeaderText = "City";
            CompanyDataGrid.Columns[5].HeaderText = "Country";
            CompanyDataGrid.Columns[7].HeaderText = "Landline#";
            CompanyDataGrid.Columns[8].HeaderText = "Phone No.";
            CompanyDataGrid.Columns[9].HeaderText = "Email";
            CompanyDataGrid.Columns[6].HeaderText = "Created";
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (compIDtxt.Text != "" && OwnNametxt.Text != "" && orgnametxt.Text != "" && citytxt.Text != "" && addresstxt.Text != "" && landlinetxt.Text != "" && mobiletxt.Text != "" )
            {
                c.Sup_name = OwnNametxt.Text;
                c.Org_name = orgnametxt.Text;
                c.Mobileno = mobiletxt.Text;
                c.landlineno = landlinetxt.Text;
                c.address = addresstxt.Text;
                c.city = citytxt.Text;
                c.country = countrytxt.Text;
                c.email = emailtxt.Text;
                c.created = DateTime.Now;
                
                bool check = IsValidEmail(c.email); //check if email is valid
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
                        MessageBox.Show("Failed to add new Supplier!");
                    }
                    // Refreshing DataGrid View
                    DataTable dt = dal.Select();
                    CompanyDataGrid.DataSource = dt;
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

        private void OwnNametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void orgnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void citytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void countrytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void compIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void mobiletxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
            if (mobiletxt.Text.Length == 11)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void landlinetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
            if (landlinetxt.Text.Length == 10)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompanyDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            compIDtxt.Text = this.CompanyDataGrid.CurrentRow.Cells[0].Value.ToString();
            OwnNametxt.Text = this.CompanyDataGrid.CurrentRow.Cells[1].Value.ToString();
            orgnametxt.Text = this.CompanyDataGrid.CurrentRow.Cells[2].Value.ToString();
            mobiletxt.Text = this.CompanyDataGrid.CurrentRow.Cells[8].Value.ToString();
            landlinetxt.Text = this.CompanyDataGrid.CurrentRow.Cells[7].Value.ToString();
            addresstxt.Text = this.CompanyDataGrid.CurrentRow.Cells[3].Value.ToString();
            citytxt.Text = this.CompanyDataGrid.CurrentRow.Cells[4].Value.ToString();
            countrytxt.Text = this.CompanyDataGrid.CurrentRow.Cells[5].Value.ToString();
            emailtxt.Text = this.CompanyDataGrid.CurrentRow.Cells[9].Value.ToString();

          
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                CompanyDataGrid.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                CompanyDataGrid.DataSource = dt;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            c.Sup_id = Convert.ToInt32(compIDtxt.Text);
            bool success = dal.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Deleted Successfully!");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to delete the supplier!");
            }

            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            CompanyDataGrid.DataSource = dt;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (compIDtxt.Text != "" && OwnNametxt.Text != "" && orgnametxt.Text != "" && citytxt.Text != "" && addresstxt.Text != "" && landlinetxt.Text != "" && mobiletxt.Text!="" )
             {
                c.Sup_name = OwnNametxt.Text;
                c.Org_name = orgnametxt.Text;
                c.Mobileno = mobiletxt.Text;
                c.landlineno = landlinetxt.Text;
                c.address = addresstxt.Text;
                c.city = citytxt.Text;
                c.country = countrytxt.Text;
                c.email = emailtxt.Text;
                c.Sup_id = int.Parse(compIDtxt.Text);

                bool Success = dal.update(c); //insertion into database 
                if (Success == true)
                {
                    MessageBox.Show("Successfully Updated!");
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update Supplier!");
                }
                // Refreshing DataGrid View
                DataTable dt = dal.Select();
                CompanyDataGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier();
            s.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Productsbtn_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
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
