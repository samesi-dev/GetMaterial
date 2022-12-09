using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IStock.DLL
{
    class TransactionDAL
    {
        static string myconnstring = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Insert Data in Database 
        public bool Insert_Transaction(TransactionBLL b, out int TransactionID)
        {
            bool isSuccess = false;
            TransactionID = -1;
            SqlConnection con = new SqlConnection(myconnstring);

            try
            {

                string sql = "INSERT INTO c_ord_b (cust_id, discount,grandtotal,created) VALUES(@cust_id, @discount,@grandtotal,@created); SELECT @@IDENTITY";
                SqlCommand cmd = new SqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@cust_id", b.cust_id);
            
                cmd.Parameters.AddWithValue("@discount", b.discount);
                cmd.Parameters.AddWithValue("@grandtotal", b.grandtotal);
                cmd.Parameters.AddWithValue("@created", DateTime.Now);

                con.Open();

                object o = cmd.ExecuteScalar();

                //if the query is executed successfully then the value will be greater than 0 else it will be less than 0.
                if (o != null)
                {
                    //query successful
                    TransactionID = int.Parse(o.ToString());
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
