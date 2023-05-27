using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lab_Project_Final
{
    public partial class cheques : System.Web.UI.Page
    {
        HtmlButton btn;
        int currentId = 0;
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            amount.Style["margin-top"] = "6px";
            account_no.Style["margin-top"] = "6px";
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            conn.Open();
        }
        protected void trasferclick(object sender, EventArgs e)
        {
            string query = "select Accountinfo.Account_Numb from Accountinfo where Account_Numb='"+account_no.Text+"';";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {
                query = "select Accountinfo.Balance from Accountinfo where Account_Numb='"+account_no.Text+"';";
                cmd = new SqlCommand(query, conn);
                rdr.Close();
                rdr = cmd.ExecuteReader();
                long balance = 0;
                if(rdr.Read())
                {
                    balance = (long)rdr[0];
                }
                int amu = int.Parse(amount.Text);
                rdr.Close();
                if(balance > amu)
                {
                    query = "update Accountinfo set Balance=Balance+"+amu+" where Customerid="+data+";";
                    cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                    query = "update Accountinfo set Balance=Balance-"+amu+ " where Account_Numb='"+account_no.Text+"';";
                    cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                    query = "select COUNT(*) from CHEQUES";
                    cmd = new SqlCommand(query, conn);
                    rdr = cmd.ExecuteReader();
                    int rowcount = 0;
                    if(rdr.Read() )
                    {
                        currentId = (int)rdr[0];
                        currentId++;
                    }
                    rdr.Close() ;
                    query = "INSERT INTO CHEQUES (ChequeId, Customerid, ChequeNumber, AccountNumberSender, Amount, CashInDate)VALUES ("+currentId+", "+data+", '"+GenerateConsecutiveId()+"', '"+account_no.Text+"', "+amount.Text+ ",CONVERT(DATE, GETDATE()));";
                    cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Money Transfer to your account through Cheaque');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Cheaque Bounced');", true);
                }
            }
        }
        protected bool StartsWith(string input)
        {
            return input.StartsWith("PK-");
        }
        protected string GenerateConsecutiveId()
        {
            string id = "CH-" + currentId.ToString("D4");
            return id;
        }
    }
}