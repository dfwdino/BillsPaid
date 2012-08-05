using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBStuff
/// </summary>
namespace MyStuff
{
    public class DBStuff : IDisposable
    {
        SqlConnection myConnection;
        SqlDataAdapter myCommand = null;
        string Paid = string.Empty;
        string DatePaid = string.Empty;


        public void DBStuff()
        {
            var currentSession = HttpContext.Current.Session;
            string myValue = currentSession["SQLInfo"].ToString();
            myConnection = new SqlConnection(myValue);
            //myConnection = new SqlConnection(@"server=MASOCHIST\MASOCHISTSQL;database=Bills;User Id=Bills;Password=Bills");
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
}