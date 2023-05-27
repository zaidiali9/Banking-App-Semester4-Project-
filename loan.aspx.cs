using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace Lab_Project_Final
{
    public partial class loan : System.Web.UI.Page
    {
        HtmlButton btn = new HtmlButton();
        Button pay_now = new Button();
        static decimal loanamu = 0;
        decimal intrestamount = 0;
        HtmlGenericControl chart;
        string data = "1";
        static decimal am = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            loan_confirm.Visible = false;
            chart= (HtmlGenericControl)FindControl("chartContainer");
            chart.Visible = false;
        }
        protected void trasferclick(object sender, EventArgs e)
        {
            HtmlGenericControl dynamicdiv = new HtmlGenericControl("div");
            HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
            CalculateLoanPercentage(loan_amount.Text, int_rate.Text);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(int_rate.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jasonData", "let intamu = " + jsonData + ";", true);
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            string jsonData1 = serializer1.Serialize(loanamu);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jasonData1", "let loanamu = " + jsonData1 + ";", true);
            HtmlGenericControl heading = new HtmlGenericControl("h2");
            HtmlGenericControl head = new HtmlGenericControl("div");
            head.Attributes["align-item"] = "center";
            heading.InnerHtml = "Loan Analytics";
            head.Controls.Add(heading);
            existing.Controls.Add(head);
            existing.Controls.Add(chart);
            chart.Visible= true;
            HtmlGenericControl btndiv = new HtmlGenericControl("div");
            loan_confirm.Style["padding-left"] = "36px";
            loan_confirm.Style["padding-right"] = "36px";
            loan_confirm.Style["margin-top"] = "6px";
            loan_confirm.Text = "Confirm Loan";
            loan_confirm.Visible = true;
            btndiv.Attributes["class"] = "col-12";
            btndiv.Controls.Add(loan_confirm);
            loan_confirm.Visible = true;
            existing.Controls.Add(btndiv);
            
            
        }

        protected void loan_confirm_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
            long bal = 0;
            conn.Open();
            string query = "select balance from Accountinfo where Customerid=" + data + "";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader=cmd.ExecuteReader();
            if (reader.Read())
            {
                bal = (long)reader[0];
            }
            reader.Close();
            am = am / 2;
            if (bal >= am)
            {
                query = "update Accountinfo set Balance=Balance+" + loanamu + " where Customerid=" + data + ";";
                cmd = new SqlCommand(query, conn); cmd.ExecuteNonQuery();
                query = "select count(*) from loan";
                cmd = new SqlCommand(query, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                int id_ = 0;
                if(sqlDataReader.Read())
                {
                    id_ = (int)sqlDataReader[0];
                }
                id_++;
                sqlDataReader.Close();
                query = "insert into loan values("+id_+","+data+","+am*2+","+loan_term.Text+ ","+int_rate.Text+");";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Not Eligible for the Loan');", true);

            }
        }
        private void CalculateLoanPercentage(string loanAmount, string interestRate)
        {
            decimal it=0;
            decimal.TryParse(loanAmount, out am);
            decimal.TryParse(interestRate, out it);
            decimal interest = am * (it / 100);
            decimal totalamu = interest + am;
            totalamu = (am / totalamu) * 100;
            loanamu = totalamu;
        }
    }
}