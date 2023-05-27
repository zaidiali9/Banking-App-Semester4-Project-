using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lab_Project_Final
{
    public partial class transportaspx : System.Web.UI.Page
    {
        Table Table = new Table();
        HtmlButton btn = new HtmlButton();
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        string data = "0";
        static List<string> list = new List<string>();
        static string selectedval = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
                int id = int.Parse(data);
            }
            droplist.Visible = false;
            Label3.Visible = false;
            purchase_pay.Visible = false;
            arrval.Style["margin-top"] = "6px";
            dep.Style["margin-top"] = "6px";
            
            mode.Style["margin-top"] = "6px";
            conn.Open();
            btn = (HtmlButton)FindControl("send");
            btn.ServerClick += new EventHandler(trasferclick);
            
            
        }
        protected void trasferclick(object sender, EventArgs e)
        {
            
            string query;
            if (mode.SelectedValue == "All")
            {
                query = "select transportation_mode,departure_city,destination_city,ticket_price from TransportationOffers where departure_city='" + dep.SelectedValue + "' and destination_city='" + arrval.SelectedValue + "';";
            }
            else
            {
                query = "select transportation_mode,departure_city,destination_city,ticket_price from TransportationOffers where transportation_mode='" + mode.SelectedValue + "' and departure_city='" + dep.SelectedValue + "' and destination_city='" + arrval.SelectedValue + "';";
            }
            
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            TableRow Row_ = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell6 = new TableCell();
            cell1.Text = "Transportation Mode";
            Row_.Cells.Add(cell1);
            cell2.Text = "Departure";
            Row_.Cells.Add(cell2);
            cell3.Text = "Destination";
            Row_.Cells.Add(cell3);
            cell6.Text = "Ticket Price";
            Row_.Cells.Add(cell6);
            Table.Rows.Add(Row_);
            list.Add("Chose...");
            while (reader.Read())
            {
                TableRow row = new TableRow();
                for (int i = 0; i < 4; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Text = reader[i].ToString();
                    row.Cells.Add(cell);
                }
                Table.Rows.Add(row);
                list.Add(reader[3].ToString());
            }
            HtmlGenericControl existing = (HtmlGenericControl)FindControl("main_div");
            Table.Attributes["class"] = "table table-hover";
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
            HtmlGenericControl btn_div = new HtmlGenericControl("div");
            btn_div.Attributes["class"] = "col-12";
            btn_div.Controls.Add(purchase_pay);
            existing.Controls.Add(btn_div);
            purchase_pay.Visible = true;
           
        }

        protected void purchase_pay_Click(object sender, EventArgs e)
        {
            
            int number = 0;
            string cmd = "";
            int supplierid = 0;
            if(int.TryParse(droplist.Text, out number))
            {
                bool flag=false;
                for (int i = 0; i < list.Count; i++)
                {
                    int k = 0;
                    int.TryParse(list.ElementAt(i), out k);
                    if (number==k)
                    {
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    long bal = 0;
                    cmd = "select balance from Accountinfo where Customerid=" + data + ";";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        bal=reader.GetInt64(0);
                    }
                    reader.Close();

                    if(bal>=number)
                    {
                        string supplier_accno = "";
                        cmd = "select UserCustomer.Customerid,Account_numb,UserCustomer.Customerid from Accountinfo join UserCustomer on Accountinfo.Customerid=UserCustomer.Customerid where UserCustomer.Full_Name ='" + mode.SelectedValue + "';";
                        command = new SqlCommand(cmd, conn);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            supplierid = reader.GetInt32(0);
                            supplier_accno=reader.GetString(1);
                        }

                        reader.Close();
                        cmd = "update Accountinfo set Balance=Balance+" + number + " where Customerid=" + supplierid + ";";
                        command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                        cmd = "update Accountinfo set Balance=Balance-" + number + " where Customerid='" + data + "';";
                        command = new SqlCommand(cmd, conn); command.ExecuteNonQuery();
                        cmd = "select UserCustomer.Full_Name,Accountinfo.Account_Numb from UserCustomer join Accountinfo on UserCustomer.Customerid=Accountinfo.Customerid where UserCustomer.Customerid=" + data + ";";
                        command = new SqlCommand(cmd, conn);
                        SqlDataReader rd= command.ExecuteReader();
                        string user_name = "";
                        string user_acno = "";
                        if(rd.Read())
                        {
                            user_name = rd.GetString(0);
                            user_acno=rd.GetString(1);
                        }
                        
                        rd.Close();
                        cmd = "select count(*) from Transactions";
                        command = new SqlCommand(cmd, conn);
                        rd = command.ExecuteReader();
                        int id = 0;
                        if(rd.Read())
                        {
                            id=rd.GetInt32(0);
                        }
                        id++;
                        rd.Close();
                        cmd = "insert into Transactions values("+id+","+data+","+supplierid+",'"+supplier_accno+"','"+user_acno+"','"+user_name+"','"+mode.SelectedValue+"',"+number+ ",'Transpotation',CONVERT(DATE, GETDATE()))";
                        command = new SqlCommand(cmd, conn); ; command.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ticket Booked');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insufficent Balance');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Correct Option');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Format');", true);
            }

        }
    }
}