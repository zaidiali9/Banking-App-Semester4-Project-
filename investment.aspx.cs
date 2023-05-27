using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lab_Project_Final
{
    public partial class investment : System.Web.UI.Page
    {
        Table Table=new Table();
        HtmlButton btn=new HtmlButton();
        string data;
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            data = "0";
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            droplist.Visible = false;
            Label3.Visible = false;
            investment_amount.Visible = false;
            amounttitle.Visible = false;
            invertment_pay.Visible = false;
            Investments.Style["margin-top"] = "6px";
            conn.Open();
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            
        }
        protected void trasferclick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string query = "select * from InvestmentOpportunities where investment_type='"+Investments.SelectedValue+"';";
            SqlCommand cmd= new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            TableRow Row_ = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();
            TableCell cell6 = new TableCell();
            TableCell cell7 = new TableCell();
            TableCell cell8 = new TableCell();
            TableCell cell9 = new TableCell();
            cell1.Text = "Name";
            Row_.Cells.Add(cell1);
            cell2.Text = "Investment Type";
            Row_.Cells.Add(cell2);
            cell3.Text = "Minimum Investment";
            Row_.Cells.Add(cell3);
            cell4.Text = "Maximum Investment";
            Row_.Cells.Add(cell4);
            cell5.Text = "Intrest Rate";
            Row_.Cells.Add(cell5);
            cell6.Text = "Duration";
            Row_.Cells.Add(cell6);
            cell7.Text = "Risk Level";
            Row_.Cells.Add(cell7);
            cell8.Text = "Status";
            Row_.Cells.Add(cell8);
            cell9.Text = "Return on Investment";
            Row_.Cells.Add(cell9);
            Table.Rows.Add(Row_);
            while (reader.Read())
            {
                TableRow row=new TableRow();
                for(int i = 1; i < 10; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = reader[i].ToString();
                    row.Cells.Add(cell);
                }
                Table.Rows.Add(row);
                list.Add(reader[1].ToString());
            }
            HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
            Table.Attributes["class"] = "table table-hover table-responsive";
            existing.Controls.Add(Table);
            reader.Close();
            HtmlGenericControl drop_down_div = new HtmlGenericControl("div");
            drop_down_div.Attributes["class"] = "col-md-4";
            drop_down_div.Controls.Add(Label3);
            Label3.Visible = true;
            drop_down_div.Controls.Add(droplist);
            droplist.Style["margin-top"] = "6px";
            existing.Controls.Add(drop_down_div);
            droplist.Visible = true;
            HtmlGenericControl control = new HtmlGenericControl("div");
            control.Attributes["class"] = "col-md-4";
            control.Controls.Add(amounttitle);
            control.Controls.Add(investment_amount);
            investment_amount.Style["margin-top"] = "6px";
            existing.Controls.Add(control);
            investment_amount.Visible = true;
            amounttitle.Visible = true;
            HtmlGenericControl btn_div= new HtmlGenericControl("div");
            btn_div.Attributes["class"] = "col-12";
            btn_div.Controls.Add(invertment_pay);
            existing.Controls.Add(btn_div);
            invertment_pay.Visible = true;
            droplist.DataSource = list;
            droplist.DataBind();
        }

        protected void invertment_pay_Click(object sender, EventArgs e)
        {
            string query = "select InvestmentOpportunities.minimum_investment,InvestmentOpportunities.maximum_investment from InvestmentOpportunities where name_='"+droplist.SelectedValue+"';";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            SqlDataReader sqlData = sqlCommand.ExecuteReader();
            int min=0, max=0;
            if (sqlData.Read())
            {
                min=(int)sqlData[0];
                max = (int)sqlData[1];
            }
            sqlData.Close();
            query = "select UserCustomer.Full_Name,Accountinfo.Account_Numb,Accountinfo.Balance from UserCustomer join Accountinfo on UserCustomer.Customerid=Accountinfo.Customerid where UserCustomer.Customerid="+data+";";
            sqlCommand = new SqlCommand(query, conn);
            sqlData = sqlCommand.ExecuteReader();
            string name = "";
            string acno = "";
            long bal = 0;
            if (sqlData.Read())
            {
                bal = (long)sqlData[2];
                name= (string)sqlData[0];
                acno= (string)sqlData[1];
            }
            sqlData.Close();
            int amu = 0;
            int.TryParse(investment_amount.Text, out amu);
           
                if (amu <= bal)
                {
                    query = "update Accountinfo set Balance=Balance-" + investment_amount.Text + " where Customerid=" + data + ";";
                    sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.ExecuteNonQuery();
                    



                    query = "select UserCustomer.Customerid, Accountinfo.Account_Numb from UserCustomer join Accountinfo on UserCustomer.Customerid=Accountinfo.Customerid where UserCustomer.Full_Name='"+Investments.SelectedValue+"';";
                    sqlCommand=new SqlCommand(query, conn);
                    SqlDataReader f=sqlCommand.ExecuteReader();
                    int supplierid = 0;
                    string supplier_accno = "";
                    if(f.Read())
                    {
                        supplierid = (int)f[0];
                        supplier_accno= (string)f[1];   
                    }
                    f.Close();
                    query = "update Accountinfo set Balance=Balance+" + investment_amount.Text + " where Customerid=" + supplierid + ";";
                    sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.ExecuteNonQuery();

                    int id = 0;
                    query = "select count(*) from Transactions;";
                    sqlCommand= new SqlCommand(query, conn);
                    sqlData = sqlCommand.ExecuteReader();
                    if(sqlData.Read())
                    {
                        id= (int)sqlData[0];
                    }
                    id++;
                    sqlData.Close();
                    query = "insert into Transactions values(" + id + "," + data + "," + supplierid + ",'" + supplier_accno + "','" + acno + "','" + name + "','" + droplist.SelectedValue + "'," + investment_amount.Text + ",'Investment',CONVERT(DATE, GETDATE()))";
                    sqlCommand = new SqlCommand(query, conn); ; sqlCommand.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Investment Made');", true);

            }
            else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);

                 }


        }
    }
}