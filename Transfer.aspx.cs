using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Collections;

namespace Lab_Project_Final
{
    public partial class Transfer : System.Web.UI.Page
    {
        HtmlButton btn = new HtmlButton();
        Button pay_now = new Button();
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        static int rec_id = 0;
        static string name = "";
        string data = "1";
        static string send_name;
        static string send_acno;
        static long balance = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            btn =(HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            payment_con.Visible = false;
            ddlBank.Style["margin-top"] = "10px";
            amount_pay1.Style["margin-top"] = "10px";
            conn.Open();
        }
        protected void trasferclick(object sender, EventArgs e)
        { 

            // if the user to whome we havve to send money does exist to agay jahnay day warnah user not foun kah msg throw karain 
            HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
            HtmlGenericControl dy_div = new HtmlGenericControl("div");
            dy_div.ID = "dydiv";
            dy_div.Attributes["class"] = "d-flex flex-column";
            HtmlGenericControl to = new HtmlGenericControl("div");
            HtmlGenericControl from = new HtmlGenericControl("div");
            HtmlGenericControl btndiv = new HtmlGenericControl("div");
            HtmlGenericControl to_h=new HtmlGenericControl("h5");
            HtmlGenericControl from_h = new HtmlGenericControl("h5");
            HtmlGenericControl amount= new HtmlGenericControl("h5");
            HtmlGenericControl amount_ = new HtmlGenericControl("div");
            if (StartsWith(txtAccountNumber.Text))
            {
                string cmd = "select* from Accountinfo join UserCustomer on UserCustomer.Customerid = Accountinfo.Customerid where Account_Numb = '"+txtAccountNumber.Text+"'; ";
                SqlCommand sqlCommand=new SqlCommand(cmd,conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        rec_id = (int)reader[1];
                        name = (string)reader[6];
                    }
                    reader.Close();
                    cmd = "select* from Accountinfo join UserCustomer on UserCustomer.Customerid = Accountinfo.Customerid where UserCustomer.Customerid =" + data + "; ";
                    sqlCommand = new SqlCommand(cmd, conn);
                    reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {

                        send_name = (string)reader[6];
                        send_acno = (string)reader[0];
                        balance = (long)reader[2];
                    }
                    int number;
                    reader.Close();
                    if(int.TryParse(amount_pay1.Text,out number))
                    {
                        to_h.InnerHtml = "To: " + name+ "";
                        to.Controls.Add(to_h);
                        dy_div.Controls.Add(to);
                        from_h.InnerHtml = "From: " + send_name + "";
                        from.Controls.Add(from_h);
                        dy_div.Controls.Add(from);
                        amount.InnerHtml = "Amount: " + amount_pay1.Text + "";
                        amount_.Controls.Add(amount);
                        dy_div.Controls.Add(amount_);
                        payment_con.Style["padding-left"] = "36px";
                        payment_con.Style["padding-right"] = "36px";
                        payment_con.Style["margin-top"] = "6px";
                        payment_con.Text = "Pay Now";
                        payment_con.Visible = true;
                        btndiv.Attributes["class"] = "col-12";
                        btndiv.Controls.Add(payment_con);
                        dy_div.Controls.Add(btndiv);
                        existing.Controls.Add(dy_div);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Amount');", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The AccountNo do not exist');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Account No Starting with PK-');", true);
            }
        


        }
        protected void confirm_pay(object sender, EventArgs e)
        {
            

        }

        protected void payment_con_Click(object sender, EventArgs e)
        {
            int temp;
            int.TryParse(amount_pay1.Text, out temp);
            if (temp <= balance)
            {
                string query;
                SqlCommand cmd;
                query = "update Accountinfo set Balance=Balance+" + amount_pay1.Text + "where Account_Numb ='" + txtAccountNumber.Text + "';";
                cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                query = "update Accountinfo set Balance=Balance-" + amount_pay1.Text + " where Customerid='" + data + "';";
                cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                query = "select count(*) from Transactions";
                cmd = new SqlCommand(query, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                int id = 0;
                if (rd.Read())
                {
                    id = rd.GetInt32(0);
                }
                id++;
                rd.Close();
                query = "insert into Transactions values(" + id + "," + data + "," + rec_id + ",'" + txtAccountNumber.Text + "','" + send_acno + "','" + send_name + "','" + name + "'," + amount_pay1.Text + ",'Money Transfer',CONVERT(DATE, GETDATE()))";
                cmd = new SqlCommand(query, conn); ; cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Money Transfered');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);
            }

        }
        protected bool StartsWith(string input)
        {
            return input.StartsWith("PK-");
        }
    }
}