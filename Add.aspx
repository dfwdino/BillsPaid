﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCompanyName" runat="server" Text="Company:"></asp:Label>
&nbsp;<asp:TextBox ID="txtComanyName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblAmount" runat="server" Text="Amount: $"></asp:Label>
&nbsp;<asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblDateDue" runat="server" Text="Due Date:"></asp:Label>
&nbsp;<asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
&nbsp;ex: 6/17/2011<br />
        <br />
        <a href="Default.aspx">See Due Bills</a>&nbsp;&nbsp;
        <asp:Button ID="btnAddBill" runat="server" OnClick="Button1_Click" Text="Add" />
&nbsp;<asp:Label ID="lblAdded" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
