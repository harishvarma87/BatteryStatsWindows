<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication19.WebForm1" %>

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
<body >
    <form id="form1" runat="server" >
    
        <div align="center" style="margin-top:300px">
    
         <p>
        <asp:Button ID="Button2" runat="server" Text="Calculate charge cycles" OnClick="Button2_Click"  />
        </p>
        <p>
        <asp:Button ID="Button3" runat="server" Text="Calculate Charge Pattern" OnClick="Button3_Click" />
        </p>
        
        <asp:GridView ID="GridView1" runat="server" >
        </asp:GridView>
        </div>
    </form>
   
    
</body>

</html>
