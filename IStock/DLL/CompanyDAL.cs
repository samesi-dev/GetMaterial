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
    class CompanyDAL
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
                string sql = "SELECT * FROM Supplier ";
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
        public bool Insert(CompanyBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO Supplier (Sup_name,Org_name,address,city,country,created,Mobileno,email,landlineno) VALUES(@Sup_name,@Org_name,@Sup_add,@city,@country,@created,@Mobileno,@landlineno,@email)";
                SqlCommand cmd = new SqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@Sup_name", c.Sup_name);
                cmd.Parameters.AddWithValue("@Org_name", c.Org_name);
                cmd.Parameters.AddWithValue("@Sup_add", c.address);
                cmd.Parameters.AddWithValue("@Mobileno", c.Mobileno);
                cmd.Parameters.AddWithValue("@landlineno", c.landlineno);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@city", c.city);
                cmd.Parameters.AddWithValue("@country", c.country);
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
        public bool update(CompanyBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE Supplier SET Sup_name=@Sup_name,Org_name=@Org_name, address=@Sup_add ,city= @city,country=@country ,Mobileno=@Mobileno,landlineno=@landlineno,email=@email WHERE Sup_id= @Sup_id ";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@Sup_name", c.Sup_name);
                cmd.Parameters.AddWithValue("@Org_name", c.Org_name);
                cmd.Parameters.AddWithValue("@Sup_add", c.address);
                cmd.Parameters.AddWithValue("@Mobileno", c.Mobileno);
                cmd.Parameters.AddWithValue("@landlineno", c.landlineno);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@city", c.city);
                cmd.Parameters.AddWithValue("@country", c.country);
                cmd.Parameters.AddWithValue("@Sup_id", c.Sup_id);

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

        public bool Delete(CompanyBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "DELETE FROM  Supplier WHERE Sup_id=@Sup_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Sup_id", c.Sup_id);
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
                string sql = "SELECT * FROM Supplier WHERE Sup_id LIKE '%" + keywords + "%' OR Sup_name LIKE '%" + keywords + "%' ";
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
        public CompanyBLL SearchCompanyForTransaction(string keywords)
        {
            CompanyBLL b = new CompanyBLL();

            //stattic method to connect database
            SqlConnection con = new SqlConnection(myconnstring);
            //to hold data from database
            DataTable dt = new DataTable();
            try
            { //sql query to get data from database
                string sql = "SELECT Sup_name FROM Supplier WHERE Sup_id LIKE '%" + keywords + "%' OR Sup_name LIKE '%" + keywords + "%' ";
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
                    b.Sup_name = dt.Rows[0]["Sup_name"].ToString();
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
    }
}
