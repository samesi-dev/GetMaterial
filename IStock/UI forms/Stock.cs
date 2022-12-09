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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            Remainingpaymentdata();
            Supplierdata();
            Categorydata();
            Autoprimarykey();
            Productdata();
           
        }
        ProDAL pro = new ProDAL();
        StockBLL s = new StockBLL();
        StockDAL dal = new StockDAL();
        CompanyDAL companyDAL = new CompanyDAL();
        CategoriesDAL Categories = new CategoriesDAL();
        LoginBLL l = new LoginBLL();
        LoginDAL ldal = new LoginDAL();
        public static string LoggedIn;


        public void Productdata()
        {
            DataRow dr;

            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Product", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Product--" };
            dt.Rows.InsertAt(dr, 0);

            StProCombobox.ValueMember = "productName";

            StProCombobox.DisplayMember = "productName";
            StProCombobox.DataSource = dt;

            con.Close();
        }

        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(st_id as int )),0)+1 from STOCK", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StockIDtxt.Text = dt.Rows[0][0].ToString();
            StProCombobox.Focus();
            this.ActiveControl = StProCombobox;
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void clear()
        {
          
            Stunittxt.Text = "";
            StPaymenttxt.Text = "";
            StPricetxt.Text = "";
            StProCombobox.Text = "";
            StockQuantitytxt.Text = "";
            StCattxt.Text = "";
            StSkutxt.Text = "";
            StCompCombotxt.Text = "";
            StockIDtxt.Text = "";
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
            dr.ItemArray = new object[] { 0, "--Select Supplier--" };
            dt.Rows.InsertAt(dr, 0);

            StCompCombotxt.ValueMember = "Sup_id";

            StCompCombotxt.DisplayMember = "Sup_name";
            StCompCombotxt.DataSource = dt;

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

            StCattxt.ValueMember = "proCat_id";

            StCattxt.DisplayMember = "proCatName";
            StCattxt.DataSource = dt;

            con.Close();
        }
   

        public void Remainingpaymentdata()
        {
            StPaymenttxt.Items.Add("Unfulfilled");
            StPaymenttxt.Items.Add("Pending");
            StPaymenttxt.Items.Add("Fulfilled");
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if ( StockQuantitytxt.Text != "" && StCattxt.Text != "" && StCompCombotxt.Text != "" && StPricetxt.Text != "" && StProCombobox.Text != "" && Stunittxt.Text != "" && StPaymenttxt.Text != "")
            {

                //getting username of the logged in user
                string UserLoggedInName = Login.LoggedIn;
            
                s.stockin_by = 1;
                s.st_quantity = int.Parse(StockQuantitytxt.Text);
                s.product_name = StProCombobox.Text;
                StockBLL cat = dal.GetIDFromCategoryname(StCattxt.Text);
                s.pro_cat = cat.pro_cat;
                s.unit = Stunittxt.Text;
                s.remaining_payment = (StPaymenttxt.Text);
                StockBLL supp = dal.GetIDFromSuppliername(StCompCombotxt.Text);
                s.sup_id = supp.sup_id;
                s.st_price = int.Parse(StPricetxt.Text);
                s.sku = StSkutxt.Text;

                ProBLL p = pro.GetProductIDFromName(ProductName);
                pro.IncreaseProduct(p.product_id, cat.st_quantity);

                bool Success = dal.Insert(s); //insertion into database
                if (Success == true)
                {
                    MessageBox.Show("Successfully Inserted!");
                    clear();
                }

                else
                {
                    MessageBox.Show("Failed to add stock!");
                }
                DataTable dt = dal.Select();
                StockGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please Enter all the information!");
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {

            //creating datatable to hold data from company 
            DataTable companydt = companyDAL.Select();
            //specify datasource for company combobox
            StCompCombotxt.DataSource = companydt;
            //specify display member and value member
            StCompCombotxt.DisplayMember = "Org_name";
            StCompCombotxt.ValueMember = "Sup_id";

            DataTable categorydt = Categories.Select();
            //specify datasource for company combobox
            StCattxt.DataSource = categorydt;
            //specify display member and value member
            StCattxt.DisplayMember = "proCatName";
            StCattxt.ValueMember = "proCat_id";


            DataTable pdt = pro.Select();
            StProCombobox.DataSource = pdt;
            StProCombobox.DisplayMember = "productName";
            StProCombobox.ValueMember = "productName";

            DataTable dt = dal.Select();
            StockGridView.DataSource = dt;

            StockGridView.Columns[0].HeaderText = "ID";
            StockGridView.Columns[9].HeaderText = "Company";
            StockGridView.Columns[4].HeaderText = "Price";
            StockGridView.Columns[2].HeaderText = "Quantity";
            StockGridView.Columns[1].HeaderText = "Product";
            StockGridView.Columns[6].HeaderText = "Category";
            StockGridView.Columns[3].HeaderText = "SKU";
            StockGridView.Columns[5].HeaderText = "Added By";
            StockGridView.Columns[7].HeaderText = "Payment";
            StockGridView.Columns[8].HeaderText = "Units";
            StockGridView.Columns[10].HeaderText = "Created";

            LoggedIn = l.username;
            if (l.type == "Employee")
            {
                GledgerBtn.Visible = false;
                Salesbtn.Visible = false;
                PurchaseBtn.Visible = false;
            }
            else
            {
                GledgerBtn.Visible = true;
                Salesbtn.Visible = true;
                PurchaseBtn.Visible = true;
            }

        }

            

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;

            if (StockQuantitytxt.Text != "" && StCattxt.Text != "" && StCompCombotxt.Text != "" && StPricetxt.Text != "" && StProCombobox.Text != "" && Stunittxt.Text != "" && StPaymenttxt.Text != "")
            {
                cmd = new SqlCommand("UPDATE Stock  SET sup_id=@sup_id, st_quantity=@quantity, st_price=@price, product_name=@name, pro_cat=@cat,unit=@unit ,sku =@sku ,  remaining_payment=@payment  WHERE st_id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@quantity", StockQuantitytxt.Text);
                cmd.Parameters.AddWithValue("@price", StPricetxt.Text);
          
                cmd.Parameters.AddWithValue("@sku", StSkutxt.Text);
                cmd.Parameters.AddWithValue("@unit", Stunittxt.Text);
                cmd.Parameters.AddWithValue("@payment", StPaymenttxt.Text);
                cmd.Parameters.AddWithValue("@id",StockIDtxt.Text );

                int index = StCattxt.FindString(StCattxt.Text);
                    if (index < 0)
                    {
                        MessageBox.Show("Please Select Category");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cat", StCattxt.SelectedValue);

                        index = StCompCombotxt.FindString(StCompCombotxt.Text);
                        if (index < 0)
                        {
                            MessageBox.Show("Please Select Company");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@sup_id", StCompCombotxt.SelectedValue);
                            index = StProCombobox.FindString(StProCombobox.Text);
                            if (index < 0)
                            {
                                MessageBox.Show("Please Select Product");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@name", StProCombobox.SelectedValue);
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show(" Successfuly Updated");
                                clear();
                            }
                           
                        }
                        DataTable dt = dal.Select();
                        StockGridView.DataSource = dt;
                    }
                
            }
            else
            {
                MessageBox.Show("Please Provide All Details");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            s.st_id = Convert.ToInt32(StockIDtxt.Text);
            bool success = dal.Delete(s);

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
            StockGridView.DataSource = dt;
        }

        private void StockGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            StockIDtxt.Text = this.StockGridView.CurrentRow.Cells[0].Value.ToString();
            StCompCombotxt.Text = this.StockGridView.CurrentRow.Cells[9].Value.ToString();
            StPricetxt.Text = this.StockGridView.CurrentRow.Cells[4].Value.ToString();
            StProCombobox.Text = this.StockGridView.CurrentRow.Cells[1].Value.ToString();
            StCattxt.Text = this.StockGridView.CurrentRow.Cells[6].Value.ToString();
            Stunittxt.Text = this.StockGridView.CurrentRow.Cells[8].Value.ToString();
            StPaymenttxt.Text = this.StockGridView.CurrentRow.Cells[7].Value.ToString();
            StockQuantitytxt.Text = this.StockGridView.CurrentRow.Cells[2].Value.ToString();
            StSkutxt.Text = this.StockGridView.CurrentRow.Cells[3].Value.ToString();
        }

        private void SearchUserTxt_TextChanged(object sender, EventArgs e)
        {
            string keywords = SearchUserTxt.Text; //Get keyword from textbox

            // check if keyword is null or not

            if (keywords != null)
            {  //show desired user
                DataTable dt = dal.Search(keywords);
                StockGridView.DataSource = dt;
            }
            else
            {
                //show all users
                DataTable dt = dal.Select();
                StockGridView.DataSource = dt;
            }
           
        }

        private void StPricetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
        }

        private void StockQuantitytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }
        }

        private void StockIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsWhiteSpace(ch))
            {
                e.Handled = true;
                MessageBox.Show("You can't edit this text!");
            }
        }

        private void StockUnittxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Numbers Only!");
            }

        }

        private void Stunittxt_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void StockIDtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void StProCombotxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void StCattxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StPaymenttxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Stockreport st = new Stockreport();
            st.ShowDialog();
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
