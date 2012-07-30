using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection myConnection;
        SqlDataAdapter myCommand = null; ;
        string Paid = string.Empty;
        string DatePaid = string.Empty;

        myConnection = new SqlConnection(@"server=MASOCHIST\MASOCHISTSQL;database=Bills;User Id=Bills;Password=Bills");

        //Request.QueryString.AllKeys.Contains("");

        if (!Request.QueryString.Count.Equals(0))
        {
            string ID = Request.QueryString["PaidID"];

            if (Request.QueryString["View"].Equals("All"))
                myCommand = new SqlDataAdapter("SELECT * FROM Bills", myConnection);
            
            if (Request.QueryString["Paid"].ToString().ToLower().Equals("yes"))
            {
                Paid = "No";
                DatePaid = null;
            }
            else if(Request.QueryString["Paid"].ToString().ToLower().Equals("no"))
            {
                Paid = "Yes";
                DatePaid = DateTime.Now.Date.ToString();
            }

            string UpdatePaid = "update [Bills].[dbo].[Bills] " +
                                 "set Paid = '" + Paid + "', DatePaid = '" + DatePaid + "'" + 
                                 " where IndexNumber = '" + ID + "'";
           
            // Connect to the SQL database using a SQL SELECT query to get 
            // all the data from the "Titles" table.
            SqlCommand mySQLCommand = new SqlCommand(UpdatePaid, myConnection);
            myConnection.Open();

            mySQLCommand.ExecuteNonQuery();
            
            mySQLCommand.Dispose();


        }
        else
            myCommand = new SqlDataAdapter("SELECT * FROM Bills where DueDate > '" + DateTime.Now.Date + "' AND DueDate < '" + DateTime.Now.Date.AddDays(30) + "'", myConnection);


        
      // Connect to the SQL database using a SQL SELECT query to get 
      // all the data from the "Titles" table.
       
      // Create and fill a DataSet.
      DataSet ds = new DataSet();
      myCommand.Fill(ds);
      // Bind MyRepeater to the  DataSet. MyRepeater is the ID of the
      // Repeater control in the HTML section of the page.

      foreach (DataRow dr in ds.Tables[0].Rows)
      {
          if (dr["Paid"].ToString().ToLower().Equals("yes"))
              Response.Write("<del>");
          Response.Write(dr["Company"] + " - ");
          Response.Write("$" + dr["Amount"] + " - ");
          Response.Write(dr["DueDate"].ToString().Split(' ')[0] + " - ");
          if (dr["Paid"].ToString().ToLower().Equals("yes"))
          {
              Response.Write("<a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Paid on " + dr["DatePaid"].ToString().Split(' ')[0] + ".</a>");
              Response.Write("</del>");
          }
          else
              Response.Write("<a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Not Paid.</a>");
          Response.Write("<br/>");
      }
      myCommand.Dispose();

      myConnection.Close();
      myConnection.Dispose();

      


     
    }
}