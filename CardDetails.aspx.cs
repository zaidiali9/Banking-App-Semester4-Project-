using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Reflection.Emit;

namespace Lab_Project_Final
{
    public partial class CardDetails : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = "0";
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id=int.Parse(data);
            }
            conn.Open();
            SqlCommand cmdCommand;
            string query = "SELECT Balance from Accountinfo where Customerid="+data+"";
            cmdCommand=new SqlCommand(query, conn);
            SqlDataReader reader = cmdCommand.ExecuteReader();
            int balance=0;
            if (reader.Read())
            {
                balance = (int)reader.GetInt64(0);
            }
            cmdCommand.Dispose();
            this.balance.Text = balance.ToString();
            conn.Close();
            string cardnumber = GenerateCardNumber();
            Cardnumber.Text=cardnumber;
        }
        public static string GenerateCardNumber()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 19; i++)
            {
                if (i == 4 || i == 9 || i == 14)
                {
                    sb.Append(' ');
                    i++;
                }
                int digit = random.Next(0, 10);
                sb.Append(digit);
            }
            return sb.ToString();
        }
    }

}