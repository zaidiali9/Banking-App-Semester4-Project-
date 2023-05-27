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
    public partial class Profile : System.Web.UI.Page
    {
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
            string query = "select UserCustomer.Full_Name,Accountinfo.Account_Numb,Accountinfo.Balance,UserCustomer.Phone_Numb from UserCustomer join Accountinfo on UserCustomer.Customerid=Accountinfo.Customerid where UserCustomer.Customerid=" + data + ";";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader= cmd.ExecuteReader();
            string name = "";
            string accout_no = "";
            long balance = 0;
            string ph_no = "";
            if(reader.Read())
            {
                name=reader.GetString(0);
                balance = reader.GetInt64(2);
                accout_no=reader.GetString(1);
                ph_no=reader.GetString(3);
            }
            reader.Close();
            HtmlGenericControl name_div = new HtmlGenericControl("div");
            HtmlGenericControl balance_div = new HtmlGenericControl("div");
            HtmlGenericControl accno_div = new HtmlGenericControl("div");
            HtmlGenericControl ph_div = new HtmlGenericControl("div");
            HtmlGenericControl exiting = (HtmlGenericControl)FindControl("main_div");
            name_div.Attributes["class"] = "col-md-4";
            balance_div.Attributes["class"] = "col-md-4";
            accno_div.Attributes["class"] = "col-md-4";
            ph_div.Attributes["class"] = "col-md-4";
            exiting.Controls.Add(name_div);
            exiting.Controls.Add(balance_div);
            exiting.Controls.Add(accno_div);
            exiting.Controls.Add(ph_div);
            HtmlGenericControl name_div_h = new HtmlGenericControl("h3");
            HtmlGenericControl balance_div_h = new HtmlGenericControl("h3");
            HtmlGenericControl accno_div_h = new HtmlGenericControl("h3");
            HtmlGenericControl ph_div_h = new HtmlGenericControl("h3");
            name_div_h.Attributes["class"] = "h3";
            balance_div_h.Attributes["class"] = "h3";
            accno_div_h.Attributes["class"] = "h3";
            ph_div_h.Attributes["class"] = "h3";
            name_div_h.InnerHtml = "Name: " + name + "";
            balance_div_h.InnerHtml = "Balance: $ "+balance+"";
            accno_div_h.InnerText = "Account No: " + accout_no + "";
            ph_div_h.InnerHtml = "Phone No: " + ph_no + "";
            name_div.Controls.Add(name_div_h);
            balance_div.Controls.Add(balance_div_h);
            accno_div.Controls.Add(accno_div_h);
            ph_div.Controls.Add(ph_div_h);
        }
    }
}