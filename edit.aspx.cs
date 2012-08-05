using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class edit : System.Web.UI.Page
{
    string ID;
    DBStuff myDB;
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["PaidID"];
        myDB = new DBStuff();

        if (!Page.IsPostBack)
        {
            if (!Request.QueryString.Count.Equals(0))
            {
                // Create and fill a DataSet.
                DataSet ds = myDB.GetBill(ID);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtAmount.Text = dr["Amount"].ToString();
                    txtCompany.Text = dr["Company"].ToString();
                    txtDueDate.Text = dr["DueDate"].ToString().Split(' ')[0];

                }
            }
            else
                Response.Redirect("default.aspx");
        }
       

    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        calDueDate.SelectedDate = DateTime.Now.Date;
        
        myDB.UpdateBill(ID, txtCompany.Text, txtDueDate.Text, txtAmount.Text);

        lblUpdated.Text = "Company was updated.";
        btnCancel.Text = "View List";
        

    }

    protected void lbtnCalendar_Click(object sender, EventArgs e)
    {

        if (calDueDate.Visible == true)
            calDueDate.Visible = false;
        else
            calDueDate.Visible = true;

        calDueDate.SelectedDate = DateTime.Now;
        calDueDate.SelectionMode = CalendarSelectionMode.Day;
        calDueDate.TodaysDate = DateTime.Now;
        
         
    }
    protected void calDueDate_SelectionChanged(object sender, EventArgs e)
    {
       
        txtDueDate.Text = calDueDate.SelectedDate.ToShortDateString();
        calDueDate.SelectedDate = DateTime.Now;
        calDueDate.Visible = false;
        
    }

}