using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IStock.BLL;

namespace IStock.DLL
{
    class infoDAL
    {
        static string myconnstring = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;


        #region Insert Data in Database 
        public bool Insert(infoBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO companyinfo (c_name,address1,address2) VALUES(@name,@address1,@address2)";
                SqlCommand cmd = new SqlCommand(sql, con);

            
                cmd.Parameters.AddWithValue("@name", c.c_name);
                cmd.Parameters.AddWithValue("@address1", c.address1);
                cmd.Parameters.AddWithValue("@address2", c.address2);


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
        public bool update(infoBLL c)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {
                string sql = "UPDATE companyinfo SET name=@name, address1=@address1, address2=@address2 WHERE c_id= @c_id ";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", c.c_name);
                cmd.Parameters.AddWithValue("@address1", c.address1);
                cmd.Parameters.AddWithValue("@address2", c.address2);


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
