<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblShowDates" runat="server"></asp:Label>
        <br />
        <br />  
       
            <div runat="server" id="mydiv">
                blah<br />
                blah<br />
            </div>
        <br />  
        <a href="Add.aspx">Add New Bill</a> <a href="Default.aspx?View=All">View All</a></asp:PlaceHolder>
        <br />
    
    </div>
    </form>
</body>
</html>
