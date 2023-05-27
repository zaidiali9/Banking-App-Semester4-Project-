using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab_Project_Final
{
    public partial class login_signup : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=ALI\\SQLEXPRESS;Initial Catalog=Bank_lab;Integrated Security=True");
        int rowcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        protected void Signin_Click(object sender, EventArgs e)
        {
            string count_query = "select user__name,Customerid from UserCustomer where user__name='"+Username_signin.Text+"'";
            SqlCommand sql = new SqlCommand(count_query, conn);
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                count_query = "select user__name,Customerid from UserCustomer where user__name='"+Username_signin.Text+"' and Passowrd_u='"+Password_Signin.Text+"';";
                reader.Close();
                sql = new SqlCommand(count_query, conn);
                reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Session["data"] = reader[1].ToString();
                        Response.Redirect("main.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Password');", true);

                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct UserName');", true);

                return;
            }
        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            string count_query = "select count(*) from UserCustomer";
            SqlCommand sql=new SqlCommand(count_query, conn);
            SqlDataReader rdr = sql.ExecuteReader();
            if (rdr.Read())
            {
                rowcount= (int)rdr[0];
            }
            rowcount++;
            rdr.Close();

            SqlCommand cmdCommand;
            string query = "INSERT INTO UserCustomer (Customerid, user__name, Passowrd_u, Full_Name, DoB, Address_u, Phone_Numb, CNIC, Gender) " +
                           "VALUES (@Customerid, @Username, @Password, @FullName, @DOB, @Address, @Phone, @CNIC, @Gender)";

            // Validate user inputs
            if (string.IsNullOrWhiteSpace(Ph_no.Text) || Ph_no.Text.Length != 11 || !Ph_no.Text.StartsWith("03"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Make Sure Your Phno starts with 03 or have 11 chars');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(CNIC.Text) || CNIC.Text.Length != 13)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct CNIC');", true);

                return;
            }
            if (string.IsNullOrWhiteSpace(Password_signup1.Text) || Password_signup1.Text.Length < 8)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Password lenght must be 8 chars');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(Username_signup.Text) || string.IsNullOrWhiteSpace(FullName.Text) || string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(sex_g.Text) || string.IsNullOrWhiteSpace(Email_signup.Text))
            {
                // Show error message and return
                return;
            }
            if (!Email_signup.Text.Contains("@"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter the Correct Email with @');", true);

                return;
            }
            if (DateTime.TryParse(calender.Text, out DateTime dob) && (DateTime.Now - dob).TotalDays < 16 * 365)
            {
                // Show error message and return
                return;
            }

            // Create and set parameter values
            cmdCommand = new SqlCommand(query, conn);
            cmdCommand.Parameters.AddWithValue("@Customerid", rowcount);
            cmdCommand.Parameters.AddWithValue("@Username", Username_signup.Text);
            cmdCommand.Parameters.AddWithValue("@Password", Password_signup1.Text);
            cmdCommand.Parameters.AddWithValue("@FullName", FullName.Text);
            cmdCommand.Parameters.AddWithValue("@DOB", calender.Text);
            cmdCommand.Parameters.AddWithValue("@Address", Address.Text);
            cmdCommand.Parameters.AddWithValue("@Phone", Ph_no.Text);
            cmdCommand.Parameters.AddWithValue("@CNIC", CNIC.Text);
            cmdCommand.Parameters.AddWithValue("@Gender", sex_g.Text);
           
            
             
            // Execute query
            cmdCommand.ExecuteNonQuery();
            cmdCommand.Dispose();
            string newquery = "insert into Accountinfo values('"+GenerateAccountNumber()+"',"+rowcount+",'0')";
            SqlCommand cmd = new SqlCommand(newquery, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public string GenerateAccountNumber()
        {
            Random random = new Random();
            int lastFourDigits = random.Next(10, 100); // Generate a random 4-digit number

            return "PK-9790" + lastFourDigits.ToString();
        }
    }
}