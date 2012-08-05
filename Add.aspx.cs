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
    protected void btnAddBill_Click(object sender, EventArgs e)
    {


        if (txtComanyName.Text.Length.Equals(0) || txtDueDate.Text.Length.Equals(0))
        {
            lblErrorMessage.Text = "Comanyname or Duedate can't be blank.";
            return;
        }

        DBStuff myDBStuff = new DBStuff();

        try
        {
            myDBStuff.AddBill(txtComanyName.Text, txtDueDate.Text, txtAmount.Text);
        }
        catch(Exception ex)
        {
            lblAdded.Text = "Something happen. You broke it. Error is " + ex.Message;
            return;
        }

        lblAdded.Text = "Bill add for " + txtComanyName.Text + " and Due Date is " + txtDueDate.Text;

        txtAmount.Text = "";
        txtDueDate.Text = "";
        txtComanyName.Text = "";

    }
    protected void lbtnCalendar_Click(object sender, EventArgs e)
    {
        if (calDueDate.Visible == true)
            calDueDate.Visible = false;
        else
        {
            calDueDate.Visible = true;
            calDueDate.SelectedDate = DateTime.Now;
            calDueDate.BorderStyle = BorderStyle.Groove;
            calDueDate.DayNameFormat = DayNameFormat.FirstTwoLetters;
            calDueDate.SelectionMode = CalendarSelectionMode.Day;
            calDueDate.ShowGridLines = true;
            calDueDate.VisibleDate = DateTime.Now;
            
        }

       
    }
    protected void calDueDate_SelectionChanged(object sender, EventArgs e)
    {
        txtDueDate.Text = calDueDate.SelectedDate.ToShortDateString();
        calDueDate.Visible = false;
    }
}