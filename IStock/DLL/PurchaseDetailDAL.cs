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
    class PurchaseDetailDAL
    {
        static string myconnstring = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Insert Data in Database 
        public bool InsertTransactionDetail(PurchaseDetailsBLL td)
        {
            bool isSuccess = false;

            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO s_ord_pay (sup_id, product_id,product_quantity,product_price,total,added_by,created,location) VALUES(@sup_id, @product_id,@product_quantity,@product_price,@total,@added_by,@created,@location)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@sup_id", td.sup_id);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@product_price", td.product_price);
                cmd.Parameters.AddWithValue("@product_quantity", td.product_quantity);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);
                cmd.Parameters.AddWithValue("@location", td.location);
            

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

        #region Select Data from Database
        public DataTable Select()
        { //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT * FROM s_ord_pay ";
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

        #region Search user usingkeyword
        public DataTable Search(string keywords)
        { //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT * FROM s_ord_pay WHERE s_ord_id LIKE '%" + keywords + "%' OR sup_id LIKE '%" + keywords + "%' ";
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


        #region Delete data from database

        public bool Delete(PurchaseDetailsBLL e)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  s_ord_pay  WHERE s_ord_id=@s_ord_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@s_ord_id", e.s_ord_id);
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
    }
}
