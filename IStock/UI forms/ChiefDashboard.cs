using IStock.BLL;
using IStock.DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.UI_forms
{
    public partial class ChiefDashboard : Form
    {
        public ChiefDashboard()
        {
            InitializeComponent();
        }

        LoginBLL l = new LoginBLL();
        LoginDAL ldal = new LoginDAL();
        public static string LoggedIn;
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.ShowDialog();
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

        private void StockBtn_Click(object sender, EventArgs e)
        {
            Stock c = new Stock();
            c.ShowDialog();
            this.Hide();
        }

        private void PurchaseBtn_Click(object sender, EventArgs e)
        {
            Purchase pr = new Purchase();
            pr.ShowDialog();
            this.Hide();
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            Sales sale = new Sales();
            sale.ShowDialog();
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

        private void GledgerBtn_Click(object sender, EventArgs e)
        {
            GLedger g = new GLedger();
            g.ShowDialog();
            this.Hide();
        }

        private void PayrollBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            Info i = new Info();
            i.ShowDialog();
            this.Hide();
        }

        private void Dashboardbtn_Click(object sender, EventArgs e)
        {
            ChiefDashboard c = new ChiefDashboard();
            c.ShowDialog();
            this.Hide();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void label19_Click(object sender, EventArgs e)
        {
            
        }

        private void ChiefDashboard_Load(object sender, EventArgs e)
        {
            LoggedIn = l.username;
            if (l.type == "Employee")
            {
                Mbtn.Visible = false;
                Cbtn.Visible = false;
             
            }
         

        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void CreateInvoiceBtn_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
            this.Hide();
        
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            customers c = new customers();
            c.ShowDialog();
            this.Hide();
        }

        private void AddProductBtn_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
            this.Hide();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
