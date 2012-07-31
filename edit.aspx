<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>&nbsp;<asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblDueDate" runat="server" Text="Due Date"></asp:Label>&nbsp;
        <asp:TextBox ID="txtDueDate" runat="server" ></asp:TextBox>&nbsp;<asp:LinkButton ID="lbtnCalendar" runat="server" onclick="lbtnCalendar_Click"> PickDate...</asp:LinkButton><br />
        <asp:Calendar ID="calDueDate" runat="server" Visible="False" OnSelectionChanged="calDueDate_SelectionChanged"></asp:Calendar>
        
        
        <br />
        <asp:Label ID="lblAmount" runat="server" Text="Amount $ "></asp:Label>&nbsp;<asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />&nbsp;
        <asp:Label ID="lblUpdated" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
