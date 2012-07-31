using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public class DBStuff : IDisposable
{
    SqlConnection myConnection;
    SqlDataAdapter myCommand = null;
    string Paid = string.Empty;
    string DatePaid = string.Empty;


    public DBStuff()
    {
        myConnection = new SqlConnection(@"server=MASOCHIST\MASOCHISTSQL;database=Bills;User Id=Bills;Password=Bills");
    }

    public DataSet GetAllBills()
    {
        myCommand = new SqlDataAdapter("SELECT * FROM Bills", myConnection);

        DataSet ds = new DataSet();

        myCommand.Fill(ds);

        return ds;

    }

    public void UpDatePaid(string BillID, string Paid, string DatePaid)
    {
        string UpdatePaid = "update [Bills].[dbo].[Bills] " +
                                     "set Paid = '" + Paid + "', DatePaid = '" + DatePaid + "'" +
                                     " where IndexNumber = '" + BillID + "'";


        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand mySQLCommand = new SqlCommand(UpdatePaid, myConnection);
        myConnection.Open();

        mySQLCommand.ExecuteNonQuery();

        mySQLCommand.Dispose();
    }

    public DataSet GetBillsDataRange(DateTime DateStart, DateTime DateEnd)
    {
        myCommand = new SqlDataAdapter("SELECT * FROM Bills where DueDate > '" + DateTime.Now.Date + "' AND DueDate < '" + DateTime.Now.Date.AddDays(30) + "'", myConnection);

        DataSet ds = new DataSet();

        myCommand.Fill(ds);

        return ds;

    }

    public void AddBill(string Company, string DueDate, string Amount)
    {

        string insertbill = "INSERT INTO [Bills].[dbo].[Bills] ([Company],[DueDate],[Amount])" +
                    " VALUES ('" + Company + "','" + DueDate + "'," + Amount + ")";


        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand myCommand = new SqlCommand(insertbill, myConnection);
        myConnection.Open();

        myCommand.ExecuteNonQuery();

    }

    public void UpdateBill(string BillID, string Company, string DueDate, string Amount)
    {

        string UpdatePaid = "update [Bills].[dbo].[Bills] " +
                                   "set company = '" + Company + "', DueDate = '" + DueDate + "'" + ", Amount = '" + Amount + "'" +
                                   " where IndexNumber = '" + BillID + "'";


        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand mySQLCommand = new SqlCommand(UpdatePaid, myConnection);
        myConnection.Open();

        mySQLCommand.ExecuteNonQuery();

        mySQLCommand.Dispose();

    }

    public DataSet GetBill(string BillID)
    {
        myCommand = new SqlDataAdapter("SELECT * FROM Bills where IndexNumber = '" + BillID + "'", myConnection);

        DataSet ds = new DataSet();

        myCommand.Fill(ds);

        return ds;

    }

    public void Dispose()
    {
        myCommand.Dispose();

        myConnection.Close();
        myConnection.Dispose();
    }

}


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBStuff myDB = new DBStuff();
        DataSet ds = null;
        
        if (!Request.QueryString.Count.Equals(0))
        {
            string ID = Request.QueryString["PaidID"];

            if (Request.QueryString.AllKeys.Contains("View"))
            {
                if (Request.QueryString["View"].Equals("All"))
                {
                    lblShowDates.Text = "Showing all bills.";
                    ds = myDB.GetAllBills();
                }

            }
            //else
            //{
            //    lblShowDates.Text = "Bills from " + DateTime.Now.Date.ToString().Split(' ')[0] + " and " + DateTime.Now.Date.AddDays(30).ToString().Split(' ')[0];
            //    myCommand = new SqlDataAdapter("SELECT * FROM Bills where DueDate > '" + DateTime.Now.Date + "' AND DueDate < '" + DateTime.Now.Date.AddDays(30) + "'", myConnection);

            //}

            if (Request.QueryString.AllKeys.Contains("Paid"))
            {
                string Paid = string.Empty;
                string DatePaid = null;

                if (Request.QueryString["Paid"].ToString().ToLower().Equals("yes"))
                    Paid = "No";
                else if (Request.QueryString["Paid"].ToString().ToLower().Equals("no"))
                {
                    Paid = "Yes";
                    DatePaid = DateTime.Now.ToShortDateString();
                }

                myDB.UpDatePaid(ID, Paid, DatePaid);
                lblShowDates.Text = "Bills from " + DateTime.Now.Date.ToString().Split(' ')[0] + " and " + DateTime.Now.Date.AddDays(30).ToString().Split(' ')[0];
                ds = myDB.GetBillsDataRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(30));
            }
            

        }
        else
        {
            lblShowDates.Text = "Bills from " + DateTime.Now.Date.ToString().Split(' ')[0] + " and " + DateTime.Now.Date.AddDays(30).ToString().Split(' ')[0];
            
            ds = myDB.GetBillsDataRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(30));
            

        }

        
        
        
        if(!(ds == null))
        {
            StringBuilder BillsHTML = new StringBuilder();

            

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Paid"].ToString().ToLower().Equals("yes"))
                    BillsHTML.Append("<del>");
                BillsHTML.Append(dr["Company"] + " - ");
                BillsHTML.Append("$" + dr["Amount"] + " - ");
                BillsHTML.Append(dr["DueDate"].ToString().Split(' ')[0] + " - ");
                if (dr["Paid"].ToString().ToLower().Equals("yes"))
                {
                    BillsHTML.Append("<a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Paid on " + dr["DatePaid"].ToString().Split(' ')[0] + "</a>");
                    BillsHTML.Append("</del>");
                }
                else
                    BillsHTML.Append("<a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Not Paid</a>");

                BillsHTML.Append("  <a href=edit.aspx?PaidID=" + dr["IndexNumber"] + ">Edit</a>");

                BillsHTML.Append("<br/>");
            }

            mydiv.InnerHtml = BillsHTML.ToString();
      }
      

      


     
    }
}