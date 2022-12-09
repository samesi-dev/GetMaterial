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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            Autoprimarykey();
        }

        CategoriesBLL c = new CategoriesBLL(); //BLL-> Business Logic Layer
        CategoriesDAL dal = new CategoriesDAL();



        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(proCat_id as int )),0)+1 from Product_Category", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatIDText.Text = dt.Rows[0][0].ToString();
            CatNameTxt.Focus();
            this.ActiveControl = CatNameTxt;

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (CatIDText.Text != "" && CatNameTxt.Text != "")
            {


                c.proCatName = CatNameTxt.Text;

                c.Description = CatDesTxt.Text;
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
                    MessageBox.Show("Failed to insert  Category!");
                }
                DataTable dt = dal.Select();
                CategoryDataGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (CatIDText.Text != "" && CatNameTxt.Text != "")
            {
                c.proCatName = CatNameTxt.Text;

                c.Description = CatDesTxt.Text;
                c.proCat_id = int.Parse(CatIDText.Text);
                bool Success = dal.update(c); //insertion into database 
                if (Success == true)
                {
                    MessageBox.Show("Successfully Updated!");
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to update Category!");
                }
                DataTable dt = dal.Select();
                CategoryDataGrid.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            c.proCat_id = Convert.ToInt32(CatIDText.Text);
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
            DataTable dt = dal.Select();
            CategoryDataGrid.DataSource = dt;
        }

        private void CatNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void CatIDText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void clear()
        {
            CatDesTxt.Text = "";
            CatIDText.Text = "";
            CatNameTxt.Text = "";


        }

        private void guna2ShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Category_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            CategoryDataGrid.DataSource = dt;

            CategoryDataGrid.Columns[0].HeaderText = "ID";
            CategoryDataGrid.Columns[1].HeaderText = "Category Name";

            CategoryDataGrid.Columns[2].HeaderText = "Description";
            CategoryDataGrid.Columns[3].HeaderText = "Created";
        }

        private void CategoriesBtn_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.ShowDialog();
            this.Hide();
        }

        private void SearchUserTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                CategoryDataGrid.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                CategoryDataGrid.DataSource = dt;
            }
        }

        private void CategoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIDText.Text = this.CategoryDataGrid.CurrentRow.Cells[0].Value.ToString();
            CatNameTxt.Text = this.CategoryDataGrid.CurrentRow.Cells[1].Value.ToString();

            CatDesTxt.Text = this.CategoryDataGrid.CurrentRow.Cells[2].Value.ToString();

        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Catreport c = new Catreport();
            c.ShowDialog();
            this.Hide();
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.ShowDialog();
            this.Hide();
        }

        private void OrdersBtn_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
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

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier();
            s.ShowDialog();
            this.Hide();
        }

        private void Productsbtn_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
            this.Hide();
        }
    } }
