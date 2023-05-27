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
    public partial class Acountinfo : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data = "4";
        Table table=new Table();
        Table table1=new Table();
        Table table2=new Table();
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
            HtmlGenericControl send = new HtmlGenericControl("div");
            HtmlGenericControl recv = new HtmlGenericControl("div");
            HtmlGenericControl loan = new HtmlGenericControl("div");
            HtmlGenericControl send_heading = new HtmlGenericControl("h5");
            HtmlGenericControl recv_heading = new HtmlGenericControl("h5");
            HtmlGenericControl loan_heading = new HtmlGenericControl("h5");
            send_heading.InnerHtml = "Send Money Record:";
            recv_heading.InnerHtml = "Receive Money Record: ";
            table.Attributes["class"] = "table table-hover";
            TableRow Row_ = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell6 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();
            TableCell cell7 = new TableCell();
            TableCell cell8 = new TableCell();
            cell1.Text = "Reciever ID";
            Row_.Cells.Add(cell1);
            cell2.Text = "Reciver Acc NO";
            Row_.Cells.Add(cell2);
            cell3.Text = "Sender Acc No";
            Row_.Cells.Add(cell3);
            cell6.Text = "Name Sender";
            Row_.Cells.Add(cell6);
            cell4.Text = "Name Reciever";
            Row_.Cells.Add(cell4);
            cell5.Text = "Amount";
            Row_.Cells.Add(cell5);
            cell7.Text = "Trans Type";
            Row_.Cells.Add(cell7);
            cell8.Text = "Date";
            Row_.Cells.Add(cell8);
            table.Rows.Add(Row_);
            string query = "select * from Transactions where CustomerIDSender=" + data + ";";
            SqlCommand cmd= new SqlCommand(query,conn);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read())
            {
                TableRow row = new TableRow();
                for (int i=2;i<10;i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text=reader[i].ToString();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }
            existing.Controls.Add(send_heading);
            existing.Controls.Add(table);
            Row_ = new TableRow();
            cell1 = new TableCell();
            cell2 = new TableCell();
            cell3 = new TableCell();
            cell6 = new TableCell();
            cell4 = new TableCell();
            cell5 = new TableCell();
            cell7 = new TableCell();
            cell8 = new TableCell();
            cell1.Text = "Reciever ID";
            Row_.Cells.Add(cell1);
            cell2.Text = "Reciver Acc NO";
            Row_.Cells.Add(cell2);
            cell3.Text = "Sender Acc No";
            Row_.Cells.Add(cell3);
            cell6.Text = "Name Sender";
            Row_.Cells.Add(cell6);
            cell4.Text = "Name Reciever";
            Row_.Cells.Add(cell4);
            cell5.Text = "Amount";
            Row_.Cells.Add(cell5);
            cell7.Text = "Trans Type";
            Row_.Cells.Add(cell7);
            cell8.Text = "Date";
            Row_.Cells.Add(cell8);
            table1.Rows.Add(Row_);
            query = "select * from Transactions where CustomerIDReceiver=" + data + ";";
            reader.Close();
            table1.Attributes["class"] = "table table-hover";
            cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TableRow row = new TableRow();
                for (int i = 2; i < 10; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = reader[i].ToString();
                    row.Cells.Add(cell);
                }
                table1.Rows.Add(row);
            }
            existing.Controls.Add(recv_heading);
            existing.Controls.Add(table1);
            query = "select loan.LoanAmount,loan.LoanDuration,loan.InterestRate from loan where Customerid=" + data + "";
            reader.Close();
            cmd= new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            table2.Attributes["class"] = "table table-hover";
            Row_ = new TableRow();
            cell1 = new TableCell();
            cell2 = new TableCell();
            cell3 = new TableCell();
            cell1.Text = "Loan Amount";
            Row_.Cells.Add(cell1);
            cell2.Text = "Loan Duration";
            Row_.Cells.Add(cell2);
            cell3.Text = "Intrest Rate";
            Row_.Cells.Add(cell3);
            table2.Rows.Add(Row_);
            while (reader.Read())
            {
                TableRow row = new TableRow();
                for(int i = 0; i < 3; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = reader[i].ToString();
                    row.Cells.Add(cell);
                }
                table2.Rows.Add(row);
            }
            loan_heading.InnerHtml = "Loans: ";
            existing.Controls.Add(loan_heading);
            existing.Controls.Add(table2);
        }
    }
}