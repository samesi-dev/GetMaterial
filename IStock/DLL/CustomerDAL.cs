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
    class CustomerDAL
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
                string sql = "SELECT * FROM  Customer ";
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
        public bool Insert(CustomerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO Customer (cust_name, cust_address, cust_contactno, cust_whatsappno,cust_email,created) VALUES(@cust_name,@cust_address, @cust_contactNo, @cust_whatsappNo,@cust_email,@created)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@cust_name", c.cust_name);
                cmd.Parameters.AddWithValue("@cust_address", c.cust_address);
                cmd.Parameters.AddWithValue("@cust_contactNo", c.cust_contactno);
                cmd.Parameters.AddWithValue("@cust_WhatsappNo", c.cust_Whatsappno);
                cmd.Parameters.AddWithValue("@cust_email", c.cust_email);
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


        #region Update Data in Database
        public bool update(CustomerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE Customer SET cust_name=@cust_name, cust_address=@cust_address, cust_contactno=@cust_contactNo, cust_Whatsappno=@cust_Whatsappno, cust_email=@cust_email WHERE cust_id= @cust_id ";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@cust_name", c.cust_name);
                cmd.Parameters.AddWithValue("@cust_address", c.cust_address);
                cmd.Parameters.AddWithValue("@cust_contactNo", c.cust_contactno);
                cmd.Parameters.AddWithValue("@cust_Whatsappno", c.cust_Whatsappno);
                cmd.Parameters.AddWithValue("@cust_email", c.cust_email);
                cmd.Parameters.AddWithValue("@cust_id", c.cust_id);


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

        public bool Delete(CustomerBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  Customer WHERE cust_id=@cust_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cust_id", c.cust_id);
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
                string sql = "SELECT * FROM Customer WHERE cust_id LIKE '%" + keywords + "%' OR cust_name LIKE '%" + keywords + "%' ";
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
        public CustomerBLL SearchCustomerForTransaction(string keywords)
        {
            CustomerBLL b = new CustomerBLL();

            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT cust_name FROM Customer WHERE cust_id LIKE '%" + keywords + "%' OR cust_name LIKE '%" + keywords + "%' ";
                // for executing command
                SqlCommand cmd = new SqlCommand(sql, con);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //database connection open
                con.Open();
                //fill data in our data table 
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    b.cust_name = dt.Rows[0]["cust_name"].ToString();
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
            return b; // return value in datatable
        }
        #endregion



        public CustomerBLL GetCustIDFromName(string Name)
        {
            CustomerBLL c = new CustomerBLL();

            //sql connection 

            SqlConnection con = new SqlConnection(myconnstring);
            //datatable to hold the data temporarily 
            DataTable dt = new DataTable();
            try
            {
                //Sql query to get ID based on Name

                string Sql = "SELECT cust_id FROM Customer WHERE cust_name'" + Name + "'";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, con);
                con.Open();
                //passing value from dataadapter to database
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    c.cust_id = int.Parse(dt.Rows[0]["cust_id"].ToString());
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
            return c;
        }
    }
}
