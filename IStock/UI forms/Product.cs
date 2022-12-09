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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            Categorydata();
            Supplierdata();
            Autoprimarykey();
        }

        ProBLL p = new ProBLL();
        ProDAL dal = new ProDAL();

        CompanyDAL companyDAL = new CompanyDAL();
        CategoriesDAL Categories = new CategoriesDAL();

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(product_id as int )),0)+1 from Product", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProIDtxt.Text = dt.Rows[0][0].ToString();
            ProNametxt.Focus();
            this.ActiveControl = ProNametxt;

        }

        private void clear()
        {

            ProCattxt.Text = "";
            prounitstxt.Text = "";
            ProCompCombotxt.Text = "";
            ProDesctxt.Text = "";
            ProNametxt.Text = "";
            ProPricetxt.Text = "";
            ProQuantitytxt.Text = "";
        }
        public void Supplierdata()
        {
            DataRow dr;

            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Supplier", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Company--" };
            dt.Rows.InsertAt(dr, 0);

            ProCompCombotxt.ValueMember = "Sup_id";

            ProCompCombotxt.DisplayMember = "Sup_name";
            ProCompCombotxt.DataSource = dt;

            con.Close();
        }


        public void Categorydata()
        {
            DataRow dr;

            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Product_Category", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Category--" };
            dt.Rows.InsertAt(dr, 0);

            ProCattxt.ValueMember = "proCat_id";

            ProCattxt.DisplayMember = "proCatName";
            ProCattxt.DataSource = dt;

            con.Close();
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ProIDtxt.Text != "" && ProNametxt.Text != "" && ProCattxt.Text != "" && prounitstxt.Text != "" && ProCompCombotxt.Text != "" && ProQuantitytxt.Text != "" && ProPricetxt.Text != "")
            {


                p.productName = ProNametxt.Text;
                p.units = prounitstxt.Text;
                p.Description = ProDesctxt.Text;
                p.created = DateTime.Now;
                p.pro_unitprice = int.Parse(ProPricetxt.Text);
                p.pro_quantity = int.Parse(ProQuantitytxt.Text);
                ProBLL com = dal.GetIDFromSuppliername(ProCompCombotxt.Text);
                p.Sup_id = com.Sup_id;
                ProBLL bal = dal.GetIDFromCategoryname(ProCattxt.Text);
                p.proCat = bal.proCat;
           

                bool Success = dal.Insert(p);
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
                DataTable dt = dal.Select();
                ProductGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            p.product_id = Convert.ToInt32(ProIDtxt.Text);
            bool success = dal.Delete(p);

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
            ProductGridView.DataSource = dt;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;
            if (ProIDtxt.Text != "" && ProNametxt.Text != "" && ProCattxt.Text != "" && prounitstxt.Text != "" && ProCompCombotxt.Text != "" && ProQuantitytxt.Text != "" && ProPricetxt.Text != "")
            {
                cmd = new SqlCommand("UPDATE Product  SET productName=@proName,Description=@Description, units=@units ,pro_quantity=@quantity,pro_unitprice =@price ,proCat= @proCat,Sup_id=@Sup_id WHERE product_id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@proName", ProNametxt.Text);
                cmd.Parameters.AddWithValue("@Description", ProDesctxt.Text);
                cmd.Parameters.AddWithValue("@units", prounitstxt.Text);
               
                cmd.Parameters.AddWithValue("@quantity", ProQuantitytxt.Text);
                cmd.Parameters.AddWithValue("@price", ProPricetxt.Text);
                cmd.Parameters.AddWithValue("@id", ProIDtxt.Text);


                int  index = ProCattxt.FindString(ProCattxt.Text);
                    if (index < 0)
                    {
                        MessageBox.Show("Please Select Category");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@proCat", ProCattxt.SelectedValue);

                        index = ProCompCombotxt.FindString(ProCompCombotxt.Text);
                        if (index < 0)
                        {
                            MessageBox.Show("Please Select Company");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Sup_id", ProCompCombotxt.SelectedValue);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show(" Successfuly Updated");
                                 clear();
                        }
                    DataTable dt = dal.Select();
                    ProductGridView.DataSource = dt;
                     }
                
            }
            else
            {
                MessageBox.Show("Please Provide Details");
            }

        }

        private void ProIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {

            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void ProQuantitytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
        }

        private void ProPricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
        }

        private void prounitstxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && !Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Letters Only!");
            }
        }

        private void Product_Load(object sender, EventArgs e)
        {
            //creating datatable to hold data from company 
            DataTable companydt = companyDAL.Select();
            //specify datasource for company combobox
            ProCompCombotxt.DataSource = companydt;
            //specify display member and value member
            ProCompCombotxt.DisplayMember = "Org_name";
            ProCompCombotxt.ValueMember = "Sup_id";

            DataTable categorydt = Categories.Select();
            //specify datasource for company combobox
            ProCattxt.DataSource = categorydt;
            //specify display member and value member
            ProCattxt.DisplayMember = "proCatName";
            ProCattxt.ValueMember = "proCat_id";

            DataTable dt = dal.Select();
            ProductGridView.DataSource = dt;

            ProductGridView.Columns[0].HeaderText = "ID";
            ProductGridView.Columns[1].HeaderText = "Name";
            ProductGridView.Columns[2].HeaderText = "Description";
            ProductGridView.Columns[8].HeaderText = "Units";
            ProductGridView.Columns[3].HeaderText = "Quantity";
            ProductGridView.Columns[4].HeaderText = "Category";
            ProductGridView.Columns[5].HeaderText = "Unit Price";
            ProductGridView.Columns[6].HeaderText = "Company";
            ProductGridView.Columns[7].HeaderText = "Created";
        }

        private void ProductGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProIDtxt.Text = this.ProductGridView.CurrentRow.Cells[0].Value.ToString();
            ProNametxt.Text = this.ProductGridView.CurrentRow.Cells[1].Value.ToString();
            ProDesctxt.Text = this.ProductGridView.CurrentRow.Cells[2].Value.ToString();
 
            ProQuantitytxt.Text = this.ProductGridView.CurrentRow.Cells[3].Value.ToString();
            ProCattxt.Text = this.ProductGridView.CurrentRow.Cells[4].Value.ToString();
          
            prounitstxt.Text = this.ProductGridView.CurrentRow.Cells[8].Value.ToString();
            ProPricetxt.Text = this.ProductGridView.CurrentRow.Cells[5].Value.ToString();
            ProCompCombotxt.Text = this.ProductGridView.CurrentRow.Cells[6].Value.ToString();

        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                ProductGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                ProductGridView.DataSource = dt;
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Preport p = new Preport();
            p.ShowDialog();
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

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            customers c = new customers();
            c.ShowDialog();
            this.Hide();
        }

        private void Expensesbtn_Click(object sender, EventArgs e)
        {
            Expense ex = new Expense();
            ex.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
