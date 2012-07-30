using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection myConnection;

        myConnection = new SqlConnection(@"server=MASOCHIST\MASOCHISTSQL;" +
 "database=Bills;User Id=Bills;Password=Bills");

        if (txtComanyName.Text.Length.Equals(0) || txtDueDate.Text.Length.Equals(0))
        {
            lblErrorMessage.Text = "Comanyname or Duedate can't be blank.";
            return;
        }

        string insertbill = "INSERT INTO [Bills].[dbo].[Bills] ([Company],[DueDate],[Amount])" +
                            " VALUES ('" + txtComanyName.Text + "','" + txtDueDate.Text + "'," + txtAmount.Text + ")";


        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand myCommand = new SqlCommand(insertbill, myConnection);
        myConnection.Open();

        myCommand.ExecuteNonQuery();

        
        lblAdded.Text = "Bill add for " + txtComanyName.Text + " and Due Date is " + txtDueDate.Text;


    }
}