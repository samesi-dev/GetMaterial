using IStock.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.DLL
{
    class LoginDAL
    {
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        public bool LoginCheck(LoginBLL l)
        {
            bool isSuccess = false;

            SqlConnection con = new SqlConnection(myconnstring);

            try
            { //sql query to get data from database
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND type=@type";
                // for executing command
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@type", l.type);
                //getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //login successful
                    isSuccess = true;
                }


                else
                {
                    //login failed
                    isSuccess = false;
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
            return isSuccess;
        }
    }
}
