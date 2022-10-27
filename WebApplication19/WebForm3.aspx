<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication19.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body{
            background-image: url('images/battery.jpg')
        }
    </style>
</head>
<body>
    <form id="form3" runat="server">
        <div align=center style="background-color:white; width: 516px; margin-left: 500px; margin-top:50px; padding: 30px 0 30px 0">
            <asp:Button ID="Button1" runat="server" Text="Go Back" OnClick="Button1_Click" />
         <p>
             
         <p>
             <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" >
                 <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                 <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                 <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                 <SortedAscendingCellStyle BackColor="#F7F7F7" />
                 <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                 <SortedDescendingCellStyle BackColor="#E5E5E5" />
                 <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        </div>
    </form>
</body>
</html>
