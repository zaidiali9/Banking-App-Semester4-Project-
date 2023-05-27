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
    public partial class mobiletopup : System.Web.UI.Page
    {
        HtmlButton btn=new HtmlButton();
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            ddlBank.Style["margin-top"] = "8px";
            top_con.Visible = false;
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
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
            to_h.InnerHtml = "To: " + txtAccountNumber.Text + "";
            to.Controls.Add(to_h);
            dy_div.Controls.Add(to);
            from_h.InnerHtml = "From: " + ddlBank.SelectedValue + "";
            from.Controls.Add(from_h);
            dy_div.Controls.Add(from);
            top_con.Style["padding-left"] = "36px";
            top_con.Style["padding-right"] = "36px";
            top_con.Style["margin-top"] = "6px";
            top_con.Text = "Pay Now";
            top_con.Visible = true;
            btndiv.Attributes["class"] = "col-12";
            btndiv.Controls.Add(top_con);
            dy_div.Controls.Add(btndiv);
            existing.Controls.Add(dy_div);

        }

        protected void top_con_Click(object sender, EventArgs e)
        {

            int number = 0;
            string cmd = "";
            int supplierid = 0;
            if (int.TryParse(txtAccountNumber.Text, out number))
            {
                
                
                    long bal = 0;
                    cmd = "select balance from Accountinfo where Customerid=" + data + ";";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        bal = reader.GetInt64(0);
                    }
                    reader.Close();

                    if (bal >= number)
                    {
                    string acno_sup = "";
                        cmd = "select UserCustomer.Customerid,Account_Numb from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Full_Name ='" + ddlBank.SelectedValue + "';";
                        command = new SqlCommand(cmd, conn);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            supplierid = reader.GetInt32(0);
                            acno_sup = reader.GetString(1);
                        }

                        reader.Close();
                        cmd = "select Account_Numb,Full_Name from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Customerid ='" + data + "';";
                        command = new SqlCommand(cmd, conn);
                        reader = command.ExecuteReader();
                        string acno = "";
                        string name = "";
                        if (reader.Read())
                        {
                           acno = reader.GetString(0);
                           name = reader.GetString(1);
                        }
                        reader.Close();
                        cmd = "update Accountinfo set Balance=Balance+" + number + " where Customerid=" + supplierid + ";";
                        command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                        cmd = "update Accountinfo set Balance=Balance-" + number + " where Customerid='" + data + "';";
                        command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                        cmd = "select count(*) from Transactions";
                        command = new SqlCommand(cmd, conn);
                        reader = command.ExecuteReader();
                        int id = 0;
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                        id++;
                        reader.Close();
                        cmd = "insert into Transactions values(" + id + "," + data + "," + supplierid + ",'" + acno_sup + "','" + acno + "','" + name + "','" + ddlBank.SelectedValue + "'," + number + ",'MobileTopUp',CONVERT(DATE, GETDATE()))";
                        command = new SqlCommand(cmd, conn); ; command.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mobile Balance Transfered');", true);

                }
                else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);

                    }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Correct Format');", true);
            }
        }
    }
}