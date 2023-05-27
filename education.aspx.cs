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
    public partial class education : System.Web.UI.Page
    {
        HtmlButton btn = new HtmlButton();
        Button pay_now = new Button();
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            conn.Open();
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            ddlBank.Style["margin-top"] = "8px";
            TextBox1.Style["margin-top"] = "8px";
            don_con.Visible = false;
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
            HtmlGenericControl to_h = new HtmlGenericControl("h5");
            HtmlGenericControl from_h = new HtmlGenericControl("h5");
            HtmlGenericControl amount = new HtmlGenericControl("h5");
            to_h.InnerHtml = "To: " + ddlBank.SelectedValue + "";
            to.Controls.Add(to_h);
            dy_div.Controls.Add(to);
            from_h.InnerHtml = "Amount: " + txtAccountNumber.Text + "";
            from.Controls.Add(from_h);
            dy_div.Controls.Add(from);
            don_con.Style["padding-left"] = "36px";
            don_con.Style["padding-right"] = "36px";
            don_con.Style["margin-top"] = "6px";
            don_con.Text = "Pay Now";
            don_con.Visible = true;
            btndiv.Attributes["class"] = "col-12";
            btndiv.Controls.Add(don_con);
            dy_div.Controls.Add(btndiv);
            existing.Controls.Add(dy_div);
        }

        protected void don_con_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length == 4)
            {
                long bal = 0;
                int number = 0;
                string cmd;
                cmd = "select balance from Accountinfo where Customerid=" + data + ";";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bal = reader.GetInt64(0);
                }
                reader.Close();
                int supplierid = 0;
                int.TryParse(txtAccountNumber.Text, out number);
                if (bal >= number)
                {
                    string supplier_accno = "";
                    cmd = "select UserCustomer.Customerid,Account_numb,UserCustomer.Customerid from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Full_Name ='" + ddlBank.SelectedValue + "';";
                    command = new SqlCommand(cmd, conn);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        supplierid = reader.GetInt32(0);
                        supplier_accno = reader.GetString(1);
                    }

                    reader.Close();
                    cmd = "update Accountinfo set Balance=Balance+" + number + " where Customerid=" + supplierid + ";";
                    command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                    cmd = "update Accountinfo set Balance=Balance-" + number + " where Customerid='" + data + "';";
                    command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                    cmd = "select UserCustomer.Full_Name,Accountinfo.Account_Numb from UserCustomer join Accountinfo on UserCustomer.Customerid=Accountinfo.Customerid where UserCustomer.Customerid=" + data + ";";
                    command = new SqlCommand(cmd, conn);
                    SqlDataReader rd = command.ExecuteReader();
                    string user_name = "";
                    string user_acno = "";
                    if (rd.Read())
                    {
                        user_name = rd.GetString(0);
                        user_acno = rd.GetString(1);
                    }

                    rd.Close();
                    cmd = "select count(*) from Transactions";
                    command = new SqlCommand(cmd, conn);
                    rd = command.ExecuteReader();
                    int id = 0;
                    if (rd.Read())
                    {
                        id = rd.GetInt32(0);
                    }
                    id++;
                    rd.Close();
                    cmd = "insert into Transactions values(" + id + "," + data + "," + supplierid + ",'" + supplier_accno + "','" + user_acno + "','" + user_name + "','" + ddlBank.SelectedValue + "'," + txtAccountNumber.Text + ",'Fee Chalan',CONVERT(DATE, GETDATE()))";
                    command = new SqlCommand(cmd, conn); ; command.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Fee Paid');", true);


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the 4 digit Challan No');", true);
                return;
            }
        }
    }
}