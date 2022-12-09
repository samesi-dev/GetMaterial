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
    class LedgerDAL
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
                string sql = "SELECT * FROM ledger ";
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
        public bool Insert(LedgerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO ledger (account_name,account_type,date,credit,debit,description) VALUES(@account_name,@account_type,@date,@credit,@debit,@description)";
                SqlCommand cmd = new SqlCommand(sql, con);

                // cmd.Parameters.AddWithValue("@proCat_id", c.proCat_id);
                cmd.Parameters.AddWithValue("@account_name", c.account_name);
                cmd.Parameters.AddWithValue("@account_type", c.account_type);
                cmd.Parameters.AddWithValue("@credit", c.credit);
                cmd.Parameters.AddWithValue("@debit", c.debit);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@description", c.description);


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


        #region Update Data in Database
        public bool update(LedgerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE ledger SET account_name=@name,description=@description, account_type=@type, debit=@debit, credit=@credit WHERE l_id= @l_id ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", c.account_name);
                cmd.Parameters.AddWithValue("@type", c.account_type);
                cmd.Parameters.AddWithValue("@debit", c.debit);
                cmd.Parameters.AddWithValue("@credit", c.credit);
                cmd.Parameters.AddWithValue("@description", c.description);


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

        public bool Delete(LedgerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM ledger WHERE l_id=@l_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@l_id", c.l_id);
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
                string sql = "SELECT * FROM ledger WHERE l_id LIKE '%" + keywords + "%' OR account_name LIKE '%" + keywords + "%' ";
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
    }
}
