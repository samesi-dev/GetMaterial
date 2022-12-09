using IStock.BLL;
using IStock.DLL;
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

namespace IStock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Divisionrdata();
        
        }

        LoginBLL l = new LoginBLL();
        LoginDAL dal = new LoginDAL();
        public static string LoggedIn;

        //For loading account type i.e. chief account or employee.
        public void Divisionrdata()
        {
          
            Accounttypecombo.Items.Add("Chief Account");
            Accounttypecombo.Items.Add("Employee");
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Password")
            {
                passwordTextBox.Text = null;
                passwordTextBox.ForeColor = Color.Black;
            }

        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = "Password";
                passwordTextBox.ForeColor = Color.Gray;
            }
        }

        private void usernameTextbox_Enter(object sender, EventArgs e)
        {
            if (usernameTextbox.Text == "Username")
            {
                usernameTextbox.Text = null;
                usernameTextbox.ForeColor = Color.Black;
            }
        }

        private void usernameTextbox_Leave(object sender, EventArgs e)
        {
            if (usernameTextbox.Text == "")
            {
                usernameTextbox.Text = "Username";
                usernameTextbox.ForeColor = Color.Gray;
            }
        }

        private void usernameTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                MessageBox.Show("Please Donot Enter Space!");
            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
          
             if (!(usernameTextbox.Text == string.Empty))
              {
                  if (!(passwordTextBox.Text == string.Empty))
                  {

                      if (!(Accounttypecombo.Text == string.Empty))
                      {

                          l.username = usernameTextbox.Text.Trim();
                          l.password = passwordTextBox.Text.Trim();
                          l.type = Accounttypecombo.Text.Trim();

                          //checking login credentials
                          bool success = dal.LoginCheck(l);

                          if (success == true)
                          {
                              MessageBox.Show("Login Successful");
                              LoggedIn = l.username;
                              switch (l.type)
                              {
                                  case "Employee":
                                      {
                                          //Display Employee Dashboard
                                          EmployeeDashboard employee = new EmployeeDashboard();
                                          employee.ShowDialog();
                                          this.Hide();
                                      }
                                      break;
                                  case "Chief Account":
                                      {
                                          //Display admin Dashboard
                                          ChiefDashboard owner = new ChiefDashboard();
                                          owner.ShowDialog();
                                          this.Hide();
                                      }
                                      break;
                                  default:
                                      {
                                          //Display Error
                                          MessageBox.Show("Invalid User Type");
                                      }
                                      break;
                              }
                          }
                          else
                          {
                              MessageBox.Show("Login Failed! Try Again");
                          }
                      }
                      else
                      {
                          MessageBox.Show("Please Select Account Type");
                      }
                  }
                  else
                  {
                      MessageBox.Show("Don't Leave Password Empty");
                  }
              }
              else
              {
                  MessageBox.Show("Don't leave Username Empty");
              }
        }

        private void Login_Load(object sender, EventArgs e)
        {
           Accounttypecombo.SelectedItem = null;
            Accounttypecombo.SelectedText = "--select--";
        }
    }
}