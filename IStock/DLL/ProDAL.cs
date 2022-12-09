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
    class ProDAL
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
                string sql = "SELECT * FROM Product ";
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
        public bool Insert(ProBLL p)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO Product (units,productName,Description,pro_quantity,pro_unitprice,proCat,Sup_id,created) VALUES(@units,@productName,@Description,@pro_quantity,@pro_unitprice,@proCat,@Sup_id,@created)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@units", p.units);
                cmd.Parameters.AddWithValue("@productName", p.productName);
                cmd.Parameters.AddWithValue("@Description", p.Description);
                cmd.Parameters.AddWithValue("@pro_quantity", p.pro_quantity);
                cmd.Parameters.AddWithValue("@proCat", p.proCat);
                cmd.Parameters.AddWithValue("@Sup_id", p.Sup_id);
                cmd.Parameters.AddWithValue("@pro_unitprice", p.pro_unitprice);
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

        public bool Delete(ProBLL p)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  Product WHERE product_id=@product_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@product_id", p.product_id
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
                string sql = "SELECT * FROM Product WHERE product_id LIKE '%" + keywords + "%' OR productName LIKE '%" + keywords + "%' ";
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


        #region Getting User ID from Supplier name

        public ProBLL GetIDFromSuppliername(string Supname)
        {

            ProBLL p = new ProBLL();
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
                    p.Sup_id = int.Parse(dt.Rows[0]["Sup_id"].ToString());
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
            return p;//return value in datatable
        }
        #endregion


        #region Getting User ID from Category name

        public ProBLL GetIDFromCategoryname(string Categoryname)
        {
            ProBLL p = new ProBLL();
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
                    p.proCat = int.Parse(dt.Rows[0]["proCat_id"].ToString());
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
            return p;//return value in datatable
        }
        #endregion




        public ProBLL GetProductsBLL(string keyword)
        {
            ProBLL p = new ProBLL();
            SqlConnection con = new SqlConnection(myconnstring);
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "Select productName,pro_unitprice FROM Product where product_id LIKE '%" + keyword + "%' OR productName LIKE'%" + keyword + "%' ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, con);

                con.Open();
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    p.productName = dataTable.Rows[0]["productName"].ToString();
                    p.pro_unitprice = decimal.Parse(dataTable.Rows[0]["pro_unitprice"].ToString());


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
            return p;
        }




        #region method to get products id based on their name
        public ProBLL GetProductIDFromName(string ProductName)
        {
            ProBLL pr = new ProBLL();

            //sql connection 

            SqlConnection con = new SqlConnection(myconnstring);
            //datatable to hold the data temporarily 
            DataTable dt = new DataTable();
            try
            {
                //Sql query to get ID based on Name

                string Sql = "SELECT product_id FROM Product WHERE productName LIKE'%" + ProductName + "%'";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, con);
                con.Open();
                //passing value from dataadapter to database
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    pr.product_id = int.Parse(dt.Rows[0]["product_id"].ToString());
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
            return pr;
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
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            //Create Boolean Variable and SEt its Value to false
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstring);

            try
            {
                //Get the Current product Quantity
                decimal currentQty = GetProductQty(ProductID);

                //Decrease the Product Quantity based on product sales
                decimal NewQty = currentQty - Qty;

                //Update Product in Database
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
        #region DISPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
            //Sql Connection First
            SqlConnection conn = new SqlConnection(myconnstring);

            DataTable dt = new DataTable();

            try
            {
                //SQL Query to Display Product Based on CAtegory
                string sql = "SELECT * FROM  Product WHERE proCat='" + category + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection Here
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion

    }
}
