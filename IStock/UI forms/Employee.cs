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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmployeeBtn_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.ShowDialog();
            this.Hide();
        }

        EmployeeBLL c = new EmployeeBLL();
        EmployeeDAL dal = new EmployeeDAL();


        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(e_id as int )),0)+1 from Employee", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmpIDtxt.Text = dt.Rows[0][0].ToString();
            EmpNametxt.Focus();
            this.ActiveControl = EmpNametxt;

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


        private void clear()
        {

            empContacttxt.Text = "";
            empEmailtxt.Text = "";
            empaddresstxt.Text = "";
            empWhatstxt.Text = "";
            EmpNametxt.Text = "";
            ecnictxt.Text = "";
            emptypetxt.Text = "";
            empsalarytxt.Text = "";
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (EmpIDtxt.Text != "" && EmpNametxt.Text != "" && empContacttxt.Text != "" && empsalarytxt.Text!="")
            {
                c.e_name = EmpNametxt.Text;
                c.e_contactno = empContacttxt.Text;
                c.e_Whatsappno = empWhatstxt.Text;
                c.e_email = empEmailtxt.Text;
                c.e_address = empaddresstxt.Text;
                c.e_cnic = ecnictxt.Text;
                c.e_designation = emptypetxt.Text;
                c.e_salary = int.Parse(empsalarytxt.Text);
                c.created = DateTime.Now;

                bool check = IsValidEmail(c.e_email); //check if email is valid
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
                EmployeeGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (EmpIDtxt.Text != "" && EmpNametxt.Text != "" && empContacttxt.Text != "" && empsalarytxt.Text != "")
            {
                c.e_name = EmpNametxt.Text;
                c.e_contactno = empContacttxt.Text;
                c.e_Whatsappno = empWhatstxt.Text;
                c.e_email = empEmailtxt.Text;
                c.e_address = empaddresstxt.Text;
                c.e_cnic = ecnictxt.Text;
                c.e_designation = emptypetxt.Text;
                c.e_salary = int.Parse(empsalarytxt.Text);
                c.e_id = int.Parse(EmpIDtxt.Text);

                bool check = IsValidEmail(c.e_email); //check if email is valid
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
                    EmployeeGridView.DataSource = dt;
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
            c.e_id = Convert.ToInt32(EmpIDtxt.Text);
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
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            EmployeeGridView.DataSource = dt;
        }

        private void EmployeeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpIDtxt.Text = this.EmployeeGridView.CurrentRow.Cells[0].Value.ToString();
            EmpNametxt.Text = this.EmployeeGridView.CurrentRow.Cells[1].Value.ToString();
            empaddresstxt.Text = this.EmployeeGridView.CurrentRow.Cells[2].Value.ToString();
            empContacttxt.Text = this.EmployeeGridView.CurrentRow.Cells[3].Value.ToString();
            empWhatstxt.Text = this.EmployeeGridView.CurrentRow.Cells[4].Value.ToString();
            empEmailtxt.Text = this.EmployeeGridView.CurrentRow.Cells[5].Value.ToString();
            emptypetxt.Text = this.EmployeeGridView.CurrentRow.Cells[8].Value.ToString();
            ecnictxt.Text = this.EmployeeGridView.CurrentRow.Cells[6].Value.ToString();
            empsalarytxt.Text = this.EmployeeGridView.CurrentRow.Cells[7].Value.ToString();
        
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            EmployeeGridView.DataSource = dt;

            EmployeeGridView.Columns[0].HeaderText = "ID";
            EmployeeGridView.Columns[1].HeaderText = "Name";
            EmployeeGridView.Columns[2].HeaderText = "Address";
            EmployeeGridView.Columns[3].HeaderText = "Contact No.";
            EmployeeGridView.Columns[4].HeaderText = "WhatsApp No.";
            EmployeeGridView.Columns[5].HeaderText = "Email";
            EmployeeGridView.Columns[8].HeaderText = "Designation";
            EmployeeGridView.Columns[6].HeaderText = "CNIC#";
            EmployeeGridView.Columns[7].HeaderText = "Salary";
            EmployeeGridView.Columns[9].HeaderText = "Created";
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                EmployeeGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                EmployeeGridView.DataSource = dt;
            }
        }

        private void EmpNametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void emptypetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void EmpIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void empEmailtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
        }

        private void ecnictxt_KeyPress(object sender, KeyPressEventArgs e)
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
         
        }

        private void empWhatstxt_KeyPress(object sender, KeyPressEventArgs e)
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
            if (empWhatstxt.Text.Length == 11)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void empsalarytxt_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void empContacttxt_KeyPress(object sender, KeyPressEventArgs e)
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
            if (empContacttxt.Text.Length == 11)
            {
                MessageBox.Show("Please write a valid Number", "Textbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PayrollBtn_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            ereport em = new ereport();
            em.ShowDialog();
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
