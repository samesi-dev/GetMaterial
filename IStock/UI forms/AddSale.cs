using DGVPrinterHelper;
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
using System.Transactions;
using System.Windows.Forms;

namespace IStock.UI_forms
{
    public partial class AddSale : Form
    {
        public AddSale()
        {
            InitializeComponent();
            Autoprimarykey();
        }
        CustomerDAL cdal = new CustomerDAL();
        ProDAL pdal = new ProDAL();
        DataTable TransactionDT = new DataTable();
        TransactionBLL TransactionBLL = new TransactionBLL();
        TransactionDAL tDAL = new TransactionDAL();
        TransactionDetailsDAL tdDAL = new TransactionDetailsDAL();

        private void CrossButton_Click(object sender, EventArgs e)
        {
            Orders o = new Orders();
            o.ShowDialog();
            this.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }
        public void Autoprimarykey()
        {
            string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
            SqlConnection con = new SqlConnection(myconnstring);
            SqlDataAdapter sda = new SqlDataAdapter("Select  isnull(Max(cast(c_ord_id  as int )),0)+1 from c_ord_b", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderIDtxt.Text = dt.Rows[0][0].ToString();


            CustSearchtxt.Focus();
            this.ActiveControl = CustSearchtxt;

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ProNametxt.Text != "" && ProQuantitytxt.Text != "" && ProPricetxt.Text != "" && CustomerNametxt.Text != "")
            {
                string productName = ProNametxt.Text;
                decimal quantity = decimal.Parse(ProQuantitytxt.Text);
                decimal price = decimal.Parse(ProPricetxt.Text);
                decimal subtotal = decimal.Parse(Subtotaltxt.Text);


                decimal total = quantity * price; //calculates total
                subtotal = subtotal + total; //calculates subtotal

                if (productName == "")
                {
                    MessageBox.Show("Please select product first and try again");
                }
                else
                {  //add product to the datagridview

                    TransactionDT.Rows.Add(productName, quantity, price, total);
                    //display data in gridview
                    BillGrid.DataSource = TransactionDT;
                    //display the subtotal
                    Subtotaltxt.Text = subtotal.ToString();

                    // clears the textboxes
                    SearchProducttxt.Text = "";
                    ProNametxt.Text = "";
                    ProQuantitytxt.Text = "0";
                    ProPricetxt.Text = "0.00";

                }
            }
            else
            {
                MessageBox.Show("Please Provide all inforamtion and try again");
            }
        }

        private void SearchProducttxt_TextChanged(object sender, EventArgs e)
        {
            string keyword = SearchProducttxt.Text;
            if (keyword == "")
            {

                ProNametxt.Text = "";
                ProPricetxt.Text = "";


                return;
            }

            ProBLL p = pdal.GetProductsBLL(keyword);

            ProNametxt.Text = p.productName;
            ProPricetxt.Text = p.pro_unitprice.ToString();
        }

        private void AddSale_Load(object sender, EventArgs e)
        {
            TransactionDT.Columns.Add("Product Name");
            TransactionDT.Columns.Add("Quantity");
            TransactionDT.Columns.Add("Price");
            TransactionDT.Columns.Add("Total Amount");
        }

        private void DiscountTxt_TextChanged(object sender, EventArgs e)
        {

            string value = DiscountTxt.Text;

            if (value == "")
            {
                MessageBox.Show("Please Enter Discount");
            }
            else
            {
                //get the discount in decimal
                decimal subtotal = decimal.Parse(Subtotaltxt.Text);
                decimal discount = decimal.Parse(DiscountTxt.Text);
                // calculate the grandtotal based on discount
                decimal grandtotal = ((100 - discount) / 100) * subtotal;

                //display grandtotal
                Grandtotaltxt.Text = grandtotal.ToString();
            }
        }

        private void PaidAmounttxt_TextChanged(object sender, EventArgs e)
        {
            //display the paid amount
            decimal grandtotal = decimal.Parse(Grandtotaltxt.Text);
            decimal paidamount = decimal.Parse(PaidAmounttxt.Text);

            decimal returnamount = paidamount - grandtotal;

            //display the return amount
            ReturnAmounttxt.Text = returnamount.ToString();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            TransactionBLL c = new TransactionBLL();

            //  string cust_name = CustomerNametxt.Text;
            //  CustomerBLL suppCustBLL = cdal.GetCustIDFromName(cust_name);
            c.cust_id = int.Parse(CustSearchtxt.Text);
            c.grandtotal = Math.Round(decimal.Parse(Grandtotaltxt.Text), 2);
            c.created = DateTime.Now;
            c.discount = decimal.Parse(DiscountTxt.Text);
            c.TransactionDetails = TransactionDT;


            bool success = false;
            //Actual Code to insert Order and order details
            using (TransactionScope scope = new TransactionScope())
            {
                int TransactionID = -1;
                //create a boolean value and insert transaction
                bool w = tDAL.Insert_Transaction(c, out TransactionID);

                for (int i = 0; i < TransactionDT.Rows.Count; i++)
                {
                    TransactionDetailsBLL td = new TransactionDetailsBLL();
                    string ProductName = TransactionDT.Rows[i][0].ToString();
                    ProBLL p = pdal.GetProductIDFromName(ProductName);

                    td.product_id = p.product_id;
                    td.product_quantity = decimal.Parse(TransactionDT.Rows[i][1].ToString());
                    td.product_price = decimal.Parse(TransactionDT.Rows[i][2].ToString());
                    td.total = Math.Round(decimal.Parse(TransactionDT.Rows[i][3].ToString()), 2);
                    td.cust_id = c.cust_id;
                    td.created = DateTime.Now;
                    td.added_by = 1;
                    td.location = locationtxt.Text; 

                    //Here Decrease Product Quantity based on sales

                    bool x = false;
                    //Decrease the Product Quntiyt
                    x = pdal.DecreaseProduct(td.product_id, td.product_quantity);

                    //insert transaction details inside the database
                    bool y = tdDAL.InsertTransactionDetail(td);
                    success = w && x && y;

                }

                if (success == true)
                {
                    scope.Complete();

                    //Code to Print Bill
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n Mahar Modern Myanmar Company limited \r\n\r\n";
                    printer.SubTitle = " \r\n  \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: " + DiscountTxt.Text + "% \r\n" + "Paid: " + PaidAmounttxt.Text + " \r\n" + "Return Amount: " + ReturnAmounttxt.Text + " \r\n" + "Grand Total: " + Grandtotaltxt.Text + "\r\n\r\n" + "Thank you for Shopping.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(BillGrid);
                    //transaction complete
                    MessageBox.Show("Transaction Completed!");
                    BillGrid.DataSource = null;
                    BillGrid.Rows.Clear();
                    SearchProducttxt.Text = "";
                    ProNametxt.Text = "";
                    ProQuantitytxt.Text = "0";
                    ProPricetxt.Text = "0.00";
                    CustSearchtxt.Text = "";
                    CustomerNametxt.Text = "";
                    Grandtotaltxt.Text = "0.00";
                    DiscountTxt.Text = "0";
                    Subtotaltxt.Text = "0.00";
                    PaidAmounttxt.Text = "0.00";
                    ReturnAmounttxt.Text = "0.00";
                }
                else
                {
                    MessageBox.Show("transaction failed");
                }
            }
        }

        private void CustSearchtxt_TextChanged(object sender, EventArgs e)
        {
            string keyword = CustSearchtxt.Text;
            if (keyword == "")
            {

                CustomerNametxt.Text = "";


                return;
            }

            CustomerBLL c = cdal.SearchCustomerForTransaction(keyword);

            CustomerNametxt.Text = c.cust_name;
        }

        private void BillGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
