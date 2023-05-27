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
    public partial class planer : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string query = "";
        string data = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            HtmlGenericControl htmlGenericControl =(HtmlGenericControl)FindControl("main_div");
            query = "SELECT datename(MONTH,Date_Time) AS Month,avg(Amount) AS TotalAmount FROM Transactions WHERE CustomerIDSender = " + data + " GROUP BY Datename(MONTH,Date_Time) ORDER BY datename(MONTH,Date_Time);";
            conn.Open();
            Table table = new Table();
            table.Attributes["class"] = "table table-hover";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = "Month";
            row.Cells.Add(cell);
            TableCell cell2 = new TableCell();
            cell2.Text = "Average";
            row.Cells.Add(cell2);
            table.Rows.Add(row);
            while (reader.Read())
            {
                row = new TableRow();
                TableCell tableCell = new TableCell();
                tableCell.Text = (string)reader[0];
                row.Cells.Add(tableCell);
                tableCell = new TableCell();
                tableCell.Text = reader[1].ToString();
                row.Cells.Add(tableCell);
                table.Rows.Add(row);
            }
            HtmlGenericControl heading = new HtmlGenericControl("h2");
            heading.Attributes["class"] = "h2";
            heading.InnerHtml = "Aveage Money Spent";
            HtmlGenericControl strong = new HtmlGenericControl("strong");
            strong.InnerHtml = "Note: Plan your Budget keeping this in Mind";
            htmlGenericControl.Controls.AddAt(0, heading);
            htmlGenericControl.Controls.AddAt(1, table);
            htmlGenericControl.Controls.AddAt(2, strong);
        }
    }
}