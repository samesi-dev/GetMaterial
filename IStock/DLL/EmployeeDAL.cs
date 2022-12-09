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
    class EmployeeDAL
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
                string sql = "SELECT * FROM  Employee ";
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
        public bool Insert(EmployeeBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO Employee (e_name, e_address, e_contactno, e_whatsappno,e_email,e_salary,e_designation,e_cnic,created) VALUES(@e_name,@e_address, @e_contactNo, @e_whatsappNo,@e_email,@e_salary,@e_designation,@e_cnic,@created)";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@e_name", c.e_name);
                cmd.Parameters.AddWithValue("@e_address", c.e_address);
                cmd.Parameters.AddWithValue("@e_contactNo", c.e_contactno);
                cmd.Parameters.AddWithValue("@e_Whatsappno", c.e_Whatsappno);
                cmd.Parameters.AddWithValue("@e_email", c.e_email);
                cmd.Parameters.AddWithValue("@e_cnic", c.e_cnic);
                cmd.Parameters.AddWithValue("@e_salary", c.e_salary);
                cmd.Parameters.AddWithValue("@e_designation", c.e_designation);
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
        public bool update(EmployeeBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE Employee SET e_name=@e_name, e_address=@e_address, e_contactno=@e_contactNo, e_Whatsappno=@e_Whatsappno, e_email=@e_email, e_salary= @e_salary, e_cnic=@e_cnic ,e_designation=@e_designation WHERE e_id= @e_id ";
                SqlCommand cmd = new SqlCommand(sql, con); 

                cmd.Parameters.AddWithValue("@e_name", c.e_name);
                cmd.Parameters.AddWithValue("@e_address", c.e_address);
                cmd.Parameters.AddWithValue("@e_contactNo", c.e_contactno);
                cmd.Parameters.AddWithValue("@e_Whatsappno", c.e_Whatsappno);
                cmd.Parameters.AddWithValue("@e_email", c.e_email);
                cmd.Parameters.AddWithValue("@e_cnic", c.e_cnic);
                cmd.Parameters.AddWithValue("@e_salary", c.e_salary);
                cmd.Parameters.AddWithValue("@e_designation", c.e_designation);
                cmd.Parameters.AddWithValue("@e_id", c.e_id);


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

        public bool Delete(EmployeeBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  Employee WHERE e_id=@e_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@e_id", c.e_id);
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
                string sql = "SELECT * FROM Employee WHERE e_id LIKE '%" + keywords + "%' OR e_name LIKE '%" + keywords + "%' ";
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
        public EmployeeBLL SearchCustomerForTransaction(string keywords)
        {
           EmployeeBLL b = new EmployeeBLL();

            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT e_name FROM Employee WHERE e_id LIKE '%" + keywords + "%' OR e_name LIKE '%" + keywords + "%' ";
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
                    b.e_name = dt.Rows[0]["e_name"].ToString();
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



        public EmployeeBLL GetEmpIDFromName(string Name)
        {
            EmployeeBLL c = new EmployeeBLL();

            //sql connection 

            SqlConnection con = new SqlConnection(myconnstring);
            //datatable to hold the data temporarily 
            DataTable dt = new DataTable();
            try
            {
                //Sql query to get ID based on Name

                string Sql = "SELECT e_id FROM Employee WHERE e_name'" + Name + "'";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Sql, con);
                con.Open();
                //passing value from dataadapter to database
                sqlDataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    c.e_id = int.Parse(dt.Rows[0]["e_id"].ToString());
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
