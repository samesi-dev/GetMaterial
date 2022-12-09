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
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        infoBLL c = new infoBLL();
        infoDAL dal = new infoDAL();

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(c_id as int )),0)+1 from companyinfo", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            id.Text = dt.Rows[0][0].ToString();
            name.Focus();
            this.ActiveControl = name;

        }

        private void CrossButton_Click(object sender, EventArgs e)
        {
            ChiefDashboard c = new ChiefDashboard();
            c.ShowDialog();
            this.Hide();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (name.Text != "" && address1.Text != "" &&  id.Text != "")
            {
                c.c_name = name.Text;
                c.address1 = address1.Text;
                c.address2 = address2.Text;
            

                bool Success = dal.Insert(c);
                //insertion into database
                if (Success == true)
                {
                    MessageBox.Show("Successfully Inserted!");
                 
                }

                else
                {
                    MessageBox.Show("Failed to insert!");
                }
              
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (name.Text != "" && address1.Text != "" && id.Text !="")
            {
                c.c_name = name.Text;
                c.address1 = address1.Text;
                c.address2 = address2.Text;
                c.c_id = int.Parse(id.Text);


                bool Success = dal.update(c); //insertion into database 
                if (Success == true)
                {
                    MessageBox.Show("Successfully Updated!");
              
                }
                else
                {
                    MessageBox.Show("Failed to update !");
                }
            
            }
            else
            {
                MessageBox.Show("Please Enter all the information !");
            }
        }
    }
}
