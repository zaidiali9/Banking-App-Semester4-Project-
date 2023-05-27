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
    public partial class Billpayments : System.Web.UI.Page
    {
        HtmlButton btn = new HtmlButton();
        Button pay_now = new Button();
        string data = "0";
        string query;
        SqlConnection SqlConnection = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        SqlCommand cmd = null;
        SqlDataReader SqlDataReader = null;
        int number = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            SqlConnection.Open();
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            ddlBank.Style["margin-top"] = "8px";
            bill_con.Visible = false;
        }
        protected void trasferclick(object sender, EventArgs e)
        {

            // if the user to whome we havve to send money does exist to agay jahnay day warnah user not foun kah msg throw karain 
            if(int.TryParse(txtAccountNumber.Text, out number))
            {
                query = "select Full_Name,Balance from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Customerid="+data+";";
                cmd=new SqlCommand(query,SqlConnection);
                SqlDataReader=cmd.ExecuteReader();
                long bal = 0;
                string full_name="";
                if (SqlDataReader.Read())
                {
                    bal= SqlDataReader.GetInt64(1);
                    full_name = SqlDataReader.GetString(0);
                }
                SqlDataReader.Close();
                if (bal >= number)
                {
                    HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
                    HtmlGenericControl dy_div = new HtmlGenericControl("div");
                    dy_div.ID = "dydiv";
                    dy_div.Attributes["class"] = "d-flex flex-column";
                    HtmlGenericControl to = new HtmlGenericControl("div");
                    HtmlGenericControl from = new HtmlGenericControl("div");
                    HtmlGenericControl btndiv = new HtmlGenericControl("div");
                    HtmlGenericControl to_h = new HtmlGenericControl("h7");
                    HtmlGenericControl from_h = new HtmlGenericControl("h7");
                    HtmlGenericControl amount = new HtmlGenericControl("h7");
                    HtmlGenericControl amount_ = new HtmlGenericControl("h7");
                    to_h.InnerHtml = "To: " + ddlBank.SelectedValue + "";
                    to.Controls.Add(to_h);
                    dy_div.Controls.Add(to);
                    from_h.InnerHtml = "From: " + full_name + "";
                    amount_.InnerHtml = "Amount: " + txtAccountNumber.Text + "";
                    from.Controls.Add(from_h);
                    dy_div.Controls.Add(from);
                    bill_con.Style["padding-left"] = "36px";
                    bill_con.Style["padding-right"] = "36px";
                    bill_con.Style["margin-top"] = "6px";
                    bill_con.Text = "Pay Now";
                    bill_con.Visible = true;
                    btndiv.Attributes["class"] = "col-12";
                    btndiv.Controls.Add(bill_con);
                    dy_div.Controls.Add(btndiv);
                    existing.Controls.Add(dy_div);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Balance Insufficent');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Format');", true);

            }
        }

        protected void bill_con_Click(object sender, EventArgs e)
        {
            int supplierid = 0;
            query = "select UserCustomer.Customerid,Account_numb from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Full_Name ='" + ddlBank.SelectedValue + "';";
            cmd = new SqlCommand(query, SqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            string supplier_accno = "";
            if(reader.Read())
            {
                supplierid = reader.GetInt32(0);
                supplier_accno = reader.GetString(1);
            }
            int.TryParse(txtAccountNumber.Text, out number);
            reader.Close();
            query = "update Accountinfo set Balance=Balance+" + number + " where Customerid=" + supplierid + ";";
            cmd = new SqlCommand(query, SqlConnection); cmd.ExecuteNonQuery();
            query = "update Accountinfo set Balance=Balance-" + number + " where Customerid='" + data + "';";
            cmd = new SqlCommand(query, SqlConnection); cmd.ExecuteNonQuery();
            query = "select count(*) from Transactions";
            cmd = new SqlCommand(query, SqlConnection);
            reader = cmd.ExecuteReader();
            int id = 0;
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            id++;
            reader.Close();
            query = "select UserCustomer.Full_Name,Account_numb from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Customerid ='" + data + "';";
            cmd = new SqlCommand(query, SqlConnection);
            reader = cmd.ExecuteReader();
            string user_acno = "";
            string user_name = "";
            if(reader.Read())
            {
                user_acno = reader.GetString(1);
                user_name = reader.GetString(0);
            }
            reader.Close(); 
            query = "insert into Transactions values(" + id + "," + data + "," + supplierid + ",'" + supplier_accno + "','" + user_acno + "','" + user_name + "','" + ddlBank.SelectedValue + "'," + number + ",'Bill Payments',CONVERT(DATE, GETDATE()))";
            cmd = new SqlCommand(query, SqlConnection); ; cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Paid');", true);

        }
    }
}