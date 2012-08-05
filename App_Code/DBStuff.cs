using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MyTest
/// </summary>
/// 

public class DBStuff : IDisposable
{
    SqlConnection myConnection;
    SqlDataAdapter myCommand = null;
    string Paid = string.Empty;
    string DatePaid = string.Empty;

    string SQLGetAllBills;
    string SQLUpdatePaid;
    string SQLSearchBetweenDates;
    string SQLInsertNewBill;
    string SQLUpdateBill;
    string SQLGetBill;
    string SQLDeleteBill;


    public DBStuff()
    {
        //var currentSession = HttpContext.Current.Session;
        //string myValue = currentSession["SQLInfo"].ToString();
        //myConnection = new SqlConnection(myValue);
        myConnection = new SqlConnection(@"server=MASOCHIST\MASOCHISTSQL;database=Bills;User Id=Bills;Password=Bills");

        SQLGetAllBills = "SELECT * FROM Bills";
        SQLUpdatePaid = "update [Bills].[dbo].[Bills] set Paid = '{0}', DatePaid = '{1}' where IndexNumber = '{2}'";
        SQLSearchBetweenDates = "SELECT * FROM Bills where DueDate >= '{0}' AND DueDate <= '{1}'";
        SQLInsertNewBill = "INSERT INTO [Bills].[dbo].[Bills] ([Company],[DueDate],[Amount]) VALUES ('{0}','{1}',{2})";
        SQLUpdateBill = "update [Bills].[dbo].[Bills] set company = '{0}', DueDate = '{1}'" + ", Amount = '{2}' where IndexNumber = '{3}'";
        SQLGetBill = "SELECT * FROM Bills where IndexNumber = '{0}'";
        SQLDeleteBill = "DELETE FROM [Bills].[dbo].[Bills] WHERE [IndexNumber] = '{0}'";
    }

    public DataSet GetAllBills()
    {
        myCommand = new SqlDataAdapter(SQLGetAllBills, myConnection);

        DataSet ds = new DataSet();

        myCommand.Fill(ds);

        return ds;

    }

    public void UpDatePaid(string BillID, string Paid, string DatePaid)
    {
        string UpdatePaid = string.Format(SQLUpdatePaid, Paid, DatePaid, BillID);

        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand mySQLCommand = new SqlCommand(UpdatePaid, myConnection);
        myConnection.Open();

        mySQLCommand.ExecuteNonQuery();

        mySQLCommand.Dispose();
    }

    public DataSet GetBillsDataRange(DateTime DateStart, DateTime DateEnd)
    {
        myCommand = new SqlDataAdapter(string.Format(SQLSearchBetweenDates,DateTime.Now,DateTime.Now), myConnection);

        DataSet ds = new DataSet();
        try
        {
            myCommand.Fill(ds);
        }
        catch (Exception)
        {
            return null;
        }



        return ds;

    }

    public void AddBill(string Company, string DueDate, string Amount)
    {

        string insertbill = string.Format(SQLInsertNewBill, Company, DueDate, Amount);


        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand myCommand = new SqlCommand(insertbill, myConnection);
        myConnection.Open();

        myCommand.ExecuteNonQuery();

    }

    public void UpdateBill(string BillID, string Company, string DueDate, string Amount)
    {

        SQLUpdateBill = string.Format(SQLUpdateBill, Company, DueDate, Amount, BillID);
        

        // Connect to the SQL database using a SQL SELECT query to get 
        // all the data from the "Titles" table.
        SqlCommand mySQLCommand = new SqlCommand(SQLUpdateBill, myConnection);
        myConnection.Open();

        mySQLCommand.ExecuteNonQuery();

        mySQLCommand.Dispose();

    }

    public DataSet GetBill(string BillID)
    {
        myCommand = new SqlDataAdapter(string.Format(SQLGetBill,BillID), myConnection);

        DataSet ds = new DataSet();

        myCommand.Fill(ds);

        return ds;

    }

    public void DeleteBill(string BillID)
    {
        SqlCommand mySQLCommand = new SqlCommand(string.Format(SQLDeleteBill,BillID), myConnection);
        myConnection.Open();

        mySQLCommand.ExecuteNonQuery();

        mySQLCommand.Dispose();
    }

    public void Dispose()
    {
        myCommand.Dispose();

        myConnection.Close();
        myConnection.Dispose();
    }

}
