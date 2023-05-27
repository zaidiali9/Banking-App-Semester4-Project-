using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace Lab_Project_Final
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data_str = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["data"] != null)
            {
                data_str = Session["data"].ToString();
                int id = int.Parse(data_str);
            }
            List<long> list = new List<long>();
            conn.Open();
            string query = "SELECT MONTH(Date_Time) AS Month,SUM(Amount) AS TotalAmount FROM Transactions WHERE CustomerIDSender = "+data_str+" GROUP BY MONTH(Date_Time) ORDER BY MONTH(Date_Time);";
            SqlCommand cmd=new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                for(; i <= 12; i++)
                {
                    if (i == reader.GetInt32(0))
                    {
                        list.Add((long)reader[1]);
                        break;
                    }
                    else
                    {
                        list.Add(0);
                    }
                }
                i++;
            }
            long[] data=list.ToArray();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData= serializer.Serialize(data);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jasonData", "var data = " + jsonData + ";", true);
        }
    }

}