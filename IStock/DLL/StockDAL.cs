using IStock.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.DLL
{
    class StockDAL
    {
        static string myconnstring = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        { //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT * FROM STOCK ";
                // for executing command
                SqlCommand cmd = new SqlCommand(sql, con);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();
            }
            return dt; // return value in datatable
        }
        #endregion


        #region Insert Data in Database 
        public bool Insert(StockBLL s)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO STOCK (sup_id,st_quantity,st_price,stockin_by,product_name,pro_cat,pro_type,sku,discount,remaining_payment,created) VALUES(@sup_id,@st_quantity,@st_price,@stockin_by,@product_name,@pro_cat,@pro_type,@sku,@discount,@remaining_payment,@created)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@sup_id", s.sup_id);
                cmd.Parameters.AddWithValue("@st_quantity", s.st_quantity);
                cmd.Parameters.AddWithValue("@st_price", s.st_price);
                cmd.Parameters.AddWithValue("@stockin_by", s.stockin_by);
                cmd.Parameters.AddWithValue("@product_name", s.product_name);
                cmd.Parameters.AddWithValue("@pro_cat", s.pro_cat);
                cmd.Parameters.AddWithValue("@sku", s.sku);
                cmd.Parameters.AddWithValue("@remaining_payment", s.remaining_payment);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);


                con.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the value will be greater than 0 else it will be less than 0.
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                }
                else
                { //query unsuccessful
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Delete data from database

        public bool Delete(StockBLL s)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  STOCK WHERE st_id=@st_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@st_id", s.st_id
);
                con.Open();


                int rows = cmd.ExecuteNonQuery();

                //if the query is executed successfully then the value will be greater than 0 else it will be less than 0.
                if (rows > 0)
                {
                    //query successful
                    isSuccess = true;
                }
                else
                { //query unsuccessful
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion


        #region Search user usingkeyword
        public DataTable Search(string keywords)
        { //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT * FROM STOCK WHERE st_id LIKE '%" + keywords + "%' OR sup_id" + " LIKE '%" + keywords + "%' ";
                // for executing command
                SqlCommand cmd = new SqlCommand(sql, con);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();


            }
            return dt; // return value in datatable
        }
        #endregion


        #region Getting ID from Supplier name

        public StockBLL GetIDFromSuppliername(string Supname)
        {

            StockBLL s = new StockBLL();
            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT Sup_id FROM Supplier WHERE  Org_name LIKE '%" + Supname + "%' ";

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    s.sup_id = int.Parse(dt.Rows[0]["Sup_id"].ToString());
                }
            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();
            }
            return s;//return value in datatable
        }
        #endregion


        #region Getting ID from Category name

        public StockBLL GetIDFromCategoryname(string Categoryname)
        {

            StockBLL s = new StockBLL();
            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT proCat_id FROM Product_Category WHERE  proCatName LIKE '%" + Categoryname + "%' ";

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    s.pro_cat = int.Parse(dt.Rows[0]["proCat_id"].ToString());
                }
            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();
            }
            return s;//return value in datatable
        }
        #endregion



        #region Getting ID from Product name

        public StockBLL GetIDFromProductname(string Proname)
        {

            StockBLL s = new StockBLL();
            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT product_id FROM Product WHERE  product_name LIKE '%" + Proname + "%' ";

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    s.product_name = (dt.Rows[0]["product_name"].ToString());
                }
            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();
            }
            return s;//return value in datatable
        }
        #endregion


        #region Getting User ID from user name

        public StockBLL GetIDFromUsername(string username)
        {

            StockBLL u = new StockBLL();
            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT userid FROM tbl_users WHERE  username LIKE '%" + username + "%' ";

                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    u.stockin_by = int.Parse(dt.Rows[0]["userid"].ToString());
                }


            }
            catch (Exception ex)
            {  // throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            { //closing connection
                con.Close();


            }
            return u;//return value in datatable
        }
        #endregion


        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            //Create a Boolean Variable and Set its value to false
            bool success = false;

            //SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE Product SET pro_quantity=@qty WHERE product_id=@id";

                //Create SQL Command to Pass the calue into Queyr
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the VAlue trhough parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
                if (rows > 0)
                {
                    //Query Executed Successfully
                    success = true;
                }
                else
                {
                    //Failed to Execute Query
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion


        #region METHOD TO GET CURRENT QUantity from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
            //SQl Connection First
            SqlConnection conn = new SqlConnection(myconnstring);
            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;

            //Create Data Table to save the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write WQL Query to Get Quantity from Database
                string sql = "SELECT pro_quantity FROM Product WHERE product_id = " + ProductID;

                //Cerate A SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create a SQL Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open DAtabase Connection
                conn.Open();

                //PAss the calue from Data Adapter to DataTable
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["pro_quantity"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return qty;
        }
        #endregion

        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
            //Create a Boolean Variable and SEt its value to False
            bool success = false;

            //Create SQL Connection To Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //Get the Current Qty From dAtabase based on id
                decimal currentQty = GetProductQty(ProductID);

                //Increase the Current Quantity by the qty purchased from Dealer
                decimal NewQty = currentQty + IncreaseQty;

                //Update the Prudcty Quantity Now
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion

    }
}
