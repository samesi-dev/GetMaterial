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

    public partial class Settings : Form
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        public Settings()
        {
            InitializeComponent();
            Autoprimarykey();
            Divisionrdata();
        }

        userBLL u = new userBLL(); //BLL-> Business Logic Layer
        userDAL dal = new userDAL(); //DAL -> Data Access Layer
     

        public void Divisionrdata()
        {

            typecombotxt.Items.Add("ChiefAccount");
            typecombotxt.Items.Add("Employee");
        }

        private void clear()
        {
           
            usernametxt.Text = "";
            passwordtxt.Text = "";
            emailtxt.Text = "";
            typecombotxt.Text = "";
        }

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(userid as int )),0)+1 from tbl_users", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            uidtxt.Text = dt.Rows[0][0].ToString();
            usernametxt.Focus();
            this.ActiveControl = usernametxt;

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

        private void LoginButton_Click(object sender, EventArgs e)
        {

            if (usernametxt.Text != "" && passwordtxt.Text != "" && emailtxt.Text != ""  && typecombotxt.Text != "")
            {

                u.username = usernametxt.Text;
                u.password = passwordtxt.Text;
                u.email =   emailtxt.Text;

                u.type = typecombotxt.Text;
                //getting username of the logged in user
                string UserLoggedInName = Login.LoggedIn;
                userBLL usr = dal.GetIDFromUsername(UserLoggedInName);
                u.created_by = usr.userid;

                bool check = IsValidEmail(u.email); //check if email is valid
                if (check == true)
                {
                    bool Success = dal.Insert(u); //insertion into database
                    if (Success == true)
                    {
                        MessageBox.Show("Successfully Inserted!");
                        clear();
                    }

                    else
                    {
                        MessageBox.Show("Failed to add new user!");
                    }
                    // Refreshing DataGrid View
                    DataTable dt = dal.Select();
                    UserDataGrid.DataSource = dt;
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

        private void Settings_Load(object sender, EventArgs e)
        {
            // Refreshing DataGrid View
            DataTable dt = dal.Select();
            UserDataGrid.DataSource = dt;


            UserDataGrid.Columns[0].HeaderText = "ID";
            UserDataGrid.Columns[1].HeaderText = "Username";
            UserDataGrid.Columns[2].HeaderText = "Password";
            UserDataGrid.Columns[3].HeaderText = "Email";
            UserDataGrid.Columns[6].HeaderText = "Account Type";
            UserDataGrid.Columns[4].HeaderText = "Added by";
            UserDataGrid.Columns[5].HeaderText = "Created";
        }

        private void UserDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            uidtxt.Text = this.UserDataGrid.CurrentRow.Cells[0].Value.ToString();
            usernametxt.Text = this.UserDataGrid.CurrentRow.Cells[1].Value.ToString();
            passwordtxt.Text = this.UserDataGrid.CurrentRow.Cells[2].Value.ToString();
            emailtxt.Text = this.UserDataGrid.CurrentRow.Cells[3].Value.ToString();
            typecombotxt.Text = this.UserDataGrid.CurrentRow.Cells[6].Value.ToString();
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                UserDataGrid.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                UserDataGrid.DataSource = dt;
            }
        }

        private void UserDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)

            {

                // set the exact display value

                e.Value = new String('*', e.Value.ToString().Length);

            }


        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            u.userid = Convert.ToInt32(uidtxt.Text);
            bool success = dal.Delete(u);

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
            UserDataGrid.DataSource = dt;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (usernametxt.Text != "" && passwordtxt.Text != "" && emailtxt.Text != "" &&  typecombotxt.Text != "")
            {
                u.username = usernametxt.Text;
                u.password = passwordtxt.Text;
                u.email = emailtxt.Text;
                u.type = typecombotxt.Text;
                u.created_by = 1;
                bool check = IsValidEmail(u.email); //check if email is valid
                if (check == true)
                {


                    bool Success = dal.update(u); //insertion into database
                    if (Success == true)
                    {
                        MessageBox.Show("Successfully Updated!");
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update  user!");
                    }

                    // Refreshing DataGrid View
                    DataTable dt = dal.Select();
                    UserDataGrid.DataSource = dt;


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

        private void emailtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
        }

        private void uidtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void usernametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserDataGrid_SelectionChanged(object sender, EventArgs e)
        {
                             
        }

        private void UserDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
          
        }

        public void Accesstoggle_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void emailtxt_TextChanged(object sender, EventArgs e)
        {

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
