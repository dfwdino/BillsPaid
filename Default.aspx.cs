using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


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
                    pView.InnerHtml = "<a href=Default.aspx>View Only 30 days</a>";
                }

            }

            if (Request.QueryString.AllKeys.Contains("Call"))
            {
                string CallFunction = Request.QueryString["Call"].ToString();

                if(CallFunction.Equals("Delete"))
                    myDB.DeleteBill(ID);

                lblShowDates.Text = "Bills from " + DateTime.Now.Date.ToString().Split(' ')[0] + " and " + DateTime.Now.Date.AddDays(30).ToString().Split(' ')[0];

                ds = myDB.GetBillsDataRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(30));
                pView.InnerHtml = "<a href=Default.aspx?View=All>View All</a>";


            }
                

            
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
                pView.InnerHtml = "<a href=Default.aspx?View=All>View All</a>";
            }
            

        }
        else
        {
            lblShowDates.Text = "Bills from " + DateTime.Now.Date.ToString().Split(' ')[0] + " and " + DateTime.Now.Date.AddDays(30).ToString().Split(' ')[0];
            
            ds = myDB.GetBillsDataRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(30));
            pView.InnerHtml = "<a href=Default.aspx?View=All>View All</a>";

        }
              
        
        if(!(ds == null))
        {
            StringBuilder BillsHTML = new StringBuilder();
            double TotalOfBills = 0;

            BillsHTML.Append("<table border=1>");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BillsHTML.Append("<tr>");
                if (dr["Paid"].ToString().ToLower().Equals("yes"))
                    BillsHTML.Append("<del>");
                BillsHTML.Append("<td>" + dr["Company"] + "</td>");
                if (dr["Paid"].ToString().ToLower().Equals("yes"))
                    BillsHTML.Append("</del>");
                BillsHTML.Append("<td>" + "$" + dr["Amount"] + "</td> ");
                TotalOfBills += (double)dr["Amount"];
                BillsHTML.Append("<td>" + dr["DueDate"].ToString().Split(' ')[0] + "</td>");
                if (dr["Paid"].ToString().ToLower().Equals("yes"))
                {
                    BillsHTML.Append("<td><a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Paid on " + dr["DatePaid"].ToString().Split(' ')[0] + "</a></td>");
                    
                }
                else
                    BillsHTML.Append("<td><a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Paid=" + dr["Paid"] + ">Not Paid</a></td>");

                BillsHTML.Append("<td>  <a href=edit.aspx?PaidID=" + dr["IndexNumber"] + ">Edit</a></td>");

                BillsHTML.Append("<td>  <a href=default.aspx?PaidID=" + dr["IndexNumber"] + "&Call=Delete>Delete</a></td>");
                BillsHTML.Append("</tr>");
                
            }
            BillsHTML.Append("</table>");
            
            BillsHTML.Append("<br/>");
            
            BillsHTML.Append("<b>Total Due: </b>    $" + TotalOfBills);
            
            mydiv.InnerHtml = BillsHTML.ToString();
      }
      

      


     
    }
}